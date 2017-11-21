Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectCore
Imports ProjectIO
Imports ProjectAOI
Imports ProjectFeedback
Imports Cognex.VisionPro
Imports Cognex.VisionPro.PMAlign
Imports MapData
Imports WetcoConveyor
Imports System.Drawing
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Random
Imports System.IO

Public Class frmRecipe04

    'Sue20170731 視窗關閉防護
    ''Sue0712
    ''防止Alt+F4 關閉Form
    'Protected Overrides ReadOnly Property CreateParams As CreateParams
    '    Get
    '        Dim cp As CreateParams = MyBase.CreateParams
    '        Const cs_NOCLOSE As Integer = &H200
    '        cp.ClassStyle = cp.ClassStyle Or cs_NOCLOSE
    '        Return cp
    '    End Get
    'End Property

    Dim myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe04))
    Dim ErrMessage As String
    ''' <summary>對外接入操作系統設定, 或是內部配接使用</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam
    ''' <summary>繪製層級</summary>
    ''' <remarks></remarks>
    Public mDrawIdx As sLevelIndexCollection

    '20170106_ 新增紀錄Aligment node 路徑
    Dim AlignNodePath As String = ""

    'Sue20170731 視窗關閉防護 
    Dim ExitMode As Boolean = False

    ''' <summary>按鍵依序排列</summary>
    ''' <param name="btnList"></param>
    ''' <remarks></remarks>
    Sub AppendButton(ByRef btnList As List(Of Button))
        Dim basicPosX As Integer
        Dim basicPosY As Integer
        Dim height As Integer
        Dim width As Integer
        If btnList.Count > 0 Then
            basicPosX = btnList(0).Location.X
            basicPosY = btnList(0).Location.Y
            height = btnList(0).Height
            width = btnList(0).Width
        End If
        Dim visibleBtnY As Integer = 0
        Dim visibleBtnX As Integer = 0
        For i As Integer = 0 To btnList.Count - 1
            If btnList(i).Visible Then
                btnList(i).Location = New Point(basicPosX + visibleBtnX * width, basicPosY + visibleBtnY * height)
                visibleBtnY += 1
                If visibleBtnY = 4 Then
                    visibleBtnY = 0
                    visibleBtnX += 1
                End If
            End If
        Next

    End Sub

    ''' <summary>角度(度)</summary>
    ''' <remarks></remarks>
    Public Angle As Decimal
    Dim mPosX(3) As Decimal
    Dim mPosY(3) As Decimal
    Dim mPosZ(3) As Decimal

#Region "Basic"

    ' ''' <summary>流量監控系統設定介面</summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub btnFMCS_Click(sender As Object, e As EventArgs)  ', btnHomeConveyor.Click, btnHomeDispenser.Click
    '    gSyslog.Save("[frmRecipe04]" & vbTab & "[btnFeedback]" & vbTab & "Click")
    '    If gfrmFeedback Is Nothing Then
    '        gfrmFeedback = New frmFeedback
    '    ElseIf gfrmFeedback.IsDisposed Then
    '        gfrmFeedback = New frmFeedback
    '    End If
    '    With gfrmFeedback
    '        .StartPosition = FormStartPosition.CenterParent
    '        .Show()
    '        .BringToFront()
    '    End With
    'End Sub

    ''' <summary>微量天平設定介面A</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMachineABalance_Click(sender As Object, e As EventArgs) Handles btnMachineABalance.Click, mnuBalance.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnScale]" & vbTab & "Click")
        btnMachineABalance.Enabled = False
        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnPark.Enabled = False
        btnMachineBBalance.Enabled = False
        btnLoad.Enabled = False
        btnLoadB.Enabled = False
        btnUnload.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        If gfrmWeightValve1 Is Nothing Then
            gfrmWeightValve1 = New frmWeight
        ElseIf gfrmWeightValve1.IsDisposed Then
            gfrmWeightValve1 = New frmWeight
        End If
        With gfrmWeightValve1
            .StartPosition = FormStartPosition.CenterScreen
            '20170321
            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
            '20160812
            .sys = gSYS(eSys.DispStage1)
            .Text = "Valve1 FlowRate"
            .ShowDialog()
            .BringToFront()
        End With
        btnMachineABalance.Enabled = True
        '20170602按鍵保護
        btnAlignUnlock.Enabled = True
        btnPark.Enabled = True
        btnMachineBBalance.Enabled = True
        btnLoad.Enabled = True
        btnLoadB.Enabled = True
        btnUnload.Enabled = True
        btnUnloadB.Enabled = True
        btnRun.Enabled = True
        btnPause.Enabled = True
        btnCCDRun.Enabled = True
        btnDryRun.Enabled = True
        btnBack.Enabled = True
        btnAlign.Enabled = True
        btnArray.Enabled = True
        btnHeight.Enabled = True
        btnAlignUnlock.Enabled = True
        btnInspect.Enabled = True

    End Sub

    ''' <summary>微量天平設定介面B</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMachineBBalance_Click(sender As Object, e As EventArgs) Handles btnMachineBBalance.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnScale]" & vbTab & "Click")
        btnMachineBBalance.Enabled = False
        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnPark.Enabled = False
        btnMachineABalance.Enabled = False
        btnLoad.Enabled = False
        btnLoadB.Enabled = False
        btnUnload.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        If gfrmWeightValve1 Is Nothing Then
            gfrmWeightValve1 = New frmWeight
        ElseIf gfrmWeightValve1.IsDisposed Then
            gfrmWeightValve1 = New frmWeight
        End If
        With gfrmWeightValve1
            .StartPosition = FormStartPosition.CenterScreen
            '20170321
            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
            .sys = gSYS(eSys.DispStage1)
            ' .sys = gSYS(eSys.MachineB)
            .Text = "Valve2 FlowRate"
            .ShowDialog()
            .BringToFront()
        End With
        btnMachineBBalance.Enabled = True
        '20170602按鍵保護
        btnAlignUnlock.Enabled = True
        btnPark.Enabled = True
        btnMachineABalance.Enabled = True
        btnLoad.Enabled = True
        btnLoadB.Enabled = True
        btnUnload.Enabled = True
        btnUnloadB.Enabled = True
        btnRun.Enabled = True
        btnPause.Enabled = True
        btnCCDRun.Enabled = True
        btnDryRun.Enabled = True
        btnBack.Enabled = True
        btnAlign.Enabled = True
        btnArray.Enabled = True
        btnHeight.Enabled = True
        btnAlignUnlock.Enabled = True
        btnInspect.Enabled = True
    End Sub
#End Region

#Region "切換頁"

    ''' <summary>離開此頁</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click, mnuExit.Click

        'Sue20170731 視窗關閉防護 
        ExitMode = True

        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnBack]" & vbTab & "Click")
        If CheckLoadRecipeStatus() = False Then
            Exit Sub
        End If

        ''[說明]:防止Recipe Add Path 後沒儲存,去跑AutoRun. 新增線路未儲存部分用舊Recip蓋過
        'If gRecipeEdit.strName <> "" Then
        '    Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
        '    DefaultDirectory = gRecipeEdit.strFileName ' Application.StartupPath & "\Recipe\" & gRecipeEdit.strName Soni / 2017.07.21 修正路徑錯誤
        '    'Recipe讀檔
        '    LoadRecipe(DefaultDirectory) '檔案讀取
        'End If

        ''[Note]:產生搜尋樹、產生拍照搜尋樹&定位&Laser&點膠搜尋樹
        'If gRecipeEdit.SearchTree.Count > 0 Then
        '    gRecipeEdit.ScanSort()
        '    gRecipeEdit.CCDFixSort()
        '    gRecipeEdit.LaserSort()
        '    gRecipeEdit.DispenseSort()
        'End If

        ''[Note]:Update StageMap資料
        'For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
        '    gRecipeEdit.Initial_StageMap(mStageNo, gSSystemParameter.IsBypassCCD)
        'Next

        'If gRecipeEdit.CheckPattern = False Then
        '    MsgBox("Recipe Pattern Fail !!! Please Check Pattern & Process Time", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    Exit Sub
        'End If

        'For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
        '    gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
        'Next

        UcDisplay1.EndLive(False)
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & "frmRecipe04 " & Reflection.MethodBase.GetCurrentMethod().Name)
        Me.Close()

    End Sub

    ''' <summary>取得節點CCD編號</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNodeCCD() As Integer
        If SelectedNodePath = "" Then
            Return 0
        End If

        Dim TempPath() As String
        TempPath = SelectedNodePath.Split(",")
        If TempPath.Length < 2 Then
            Return 0
        End If
        Dim CCDNo As Integer = CInt(TempPath(1))
        Return CCDNo
    End Function

    Private Sub frmRecipe04_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'If gfrmSinglePattern Is Nothing Then
        '    gfrmSinglePattern = New frmSinglePattern
        'ElseIf gfrmSinglePattern.IsDisposed Then
        '    gfrmSinglePattern = New frmSinglePattern
        'End If
        If lstPatternID.SelectedIndex >= 0 Then
            mDrawIdx.PatternName = treePattern.SelectedNode.Text
        End If
        '20170616_Start
        'RefreshUI()
        '20170616_End
        Debug.Print("frmRecipe04_Activated")
    End Sub

#End Region

#Region "Pattern操作"

    Private Sub btnPatternAdd_Click(sender As Object, e As EventArgs) Handles btnPatternAdd.Click, mnuPatternAdd.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnPatternAdd]" & vbTab & "Click")
        btnPatternAdd.Enabled = False
        If Not gRecipeEdit.Editable Then
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnPatternAdd.Enabled = True
            Exit Sub
        End If

        Dim mPatternName As String
        mPatternName = InputBox("Input Pattern ID", "Recipe")

        If mPatternName = "" Then '無輸入保護
            '請先選取Recipe Pattern
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            MsgBox(gMsgHandler.GetMessage(Warn_3000013), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnPatternAdd.Enabled = True
            Exit Sub
        End If

        For i As Integer = 0 To gRecipeEdit.Pattern.Count - 1 '輸入重複保護
            If mPatternName = gRecipeEdit.Pattern.Keys(i) Then
                gSyslog.Save("Pattern ID has Existed!", , eMessageLevel.Warning)
                MsgBox("Please ID has Existed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Recipe")
                btnPatternAdd.Enabled = True
                Exit Sub
            End If
        Next

        '=== 建立預設值 ===
        Dim mRecipePattern As New CRecipePattern
        With mRecipePattern
            .Name = mPatternName
            .RoundCount = 1
            .ProcessTimeType = eProcessTimeType.None
            .Round.Add(New CPatternRound)
        End With

        '=== 建立預設值 ===

        gRecipeEdit.Pattern.Add(mPatternName, mRecipePattern) '原資料建立完成
        'lstPatternID.Items.Add(mPatternName) ' 增加選單顯示
        RefreshPatternEditor(lstPatternID.Items.Count - 1) '建立Pattern選單並選取新增項目

        '20170616_Start
        'RefreshUI()
        '20170616_End
        btnPatternAdd.Enabled = True
        RefreshUI()
    End Sub

    ''' <summary>刪除Pattern</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPatternDelete_Click(sender As Object, e As EventArgs) Handles btnPatternDelete.Click, mnuPatternDelete.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnPatternAdd]" & vbTab & "Click")
        btnPatternDelete.Enabled = False
        If Not gRecipeEdit.Editable Then '禁止編輯
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnPatternDelete.Enabled = True
            Exit Sub
        End If

        If lstPatternID.SelectedIndex < 0 Then '未選擇Pattern
            If lstPatternID.Items.Count = 0 Then
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            btnPatternDelete.Enabled = True
            Exit Sub
        End If
        Dim idx As Integer = lstPatternID.SelectedIndex
        Dim mDeletedPatternID As String = lstPatternID.SelectedItem
        If Not gRecipeEdit.Pattern.ContainsKey(mDeletedPatternID) Then '物件不存在
            lstPatternID.Items.Remove(lstPatternID.SelectedItem) '清除選擇的物件
            If lstPatternID.Items.Count > 0 Then
                If idx < lstPatternID.Items.Count Then
                    lstPatternID.SelectedIndex = idx
                Else
                    lstPatternID.SelectedIndex = 0
                End If
            End If
            Exit Sub
        End If

        '=== Soni + 2016.09.21 任一節點使用中不能刪除, 避免資料刪除下使用 ===
        For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1 '對每個平台下的
            For mNodeNo As Integer = 0 To gRecipeEdit.Node(mStageNo).Keys.Count - 1 '每個節點
                '20161115
                'If gRecipeEdit.Node(mStageNo)(gRecipeEdit.Node(0).Keys(mNodeNo)).PatternName = mDeletedPatternID Then
                If gRecipeEdit.Node(mStageNo)(gRecipeEdit.Node(mStageNo).Keys(mNodeNo)).PatternName = mDeletedPatternID Then
                    'Pattern使用中無法刪除
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000042))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000042), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'jiimmy 20170717
                    btnPatternDelete.Enabled = True
                    Exit Sub
                End If
            Next
        Next
        '=== Soni + 2016.09.21 任一節點使用中不能刪除, 避免資料刪除下使用 ===

        If MsgBox("Are you sure to delete this Pattern?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
            btnPatternDelete.Enabled = True
            Exit Sub
        End If

        gRecipeEdit.Pattern.Remove(mDeletedPatternID) '刪除實際物件

        RefreshPatternEditor(0) '更新選單
        '20170616_Start
        'RefreshTreeView(treePattern)
        'RefreshUI()
        '20170616_End
        btnPatternDelete.Enabled = True
        RefreshUI()

    End Sub

    ''' <summary>選取Pattern</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lstPatternID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPatternID.SelectedIndexChanged
        If lstPatternID.SelectedIndex < 0 Then '未選取例外
            Exit Sub
        End If
        If Not gRecipeEdit.Pattern.ContainsKey(lstPatternID.SelectedItem) Then '項目不存在
            'Pattern不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000043))
            MsgBox(gMsgHandler.GetMessage(Warn_3000043), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            lstPatternID.Items.Remove(lstPatternID.SelectedItem) '移除不正常項目
            Exit Sub
        End If

        '20170616_Start
        'RoundRefresh(0)
        'tabView.SelectTab(1) '選取TreeView
        'RefreshUI()
        '20170616_End

    End Sub

    ''' <summary>建立Pattern選單並選取預設項目</summary>
    ''' <param name="selectedIndex"></param>
    ''' <remarks></remarks>
    Sub RefreshPatternEditor(ByVal selectedIndex As Integer)
        With lstPatternID.Items
            .Clear()
            For i As Integer = 0 To gRecipeEdit.Pattern.Count - 1
                .Add(gRecipeEdit.Pattern.Keys(i))
            Next
            If .Count > 0 Then
                lstPatternID.SelectedIndex = selectedIndex
            End If
        End With

    End Sub

    Dim mPatternCopy As New CRecipePattern
    ''' <summary>樣本複製</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuPatternCopy_Click(sender As Object, e As EventArgs) Handles mnuPatternCopy.Click
        'Eason 20170113 Ticket:100011 , Add Pattern Copy Function [S]
        'Dim mPattern As String = lstPatternID.SelectedItem
        'mPatternCopy = gRecipeEdit.Pattern(mPattern)
        If (lstPatternID.SelectedItem <> Nothing) Then
            Dim mSelectPatternName As String = lstPatternID.SelectedItem
            Dim mPatternCopyName As String = mSelectPatternName
            mPatternCopy = New CRecipePattern()
            mPatternCopy = gRecipeEdit.Pattern(mSelectPatternName).Clone()
            RefreshUI()
        End If
        'Eason 20170113 Ticket:100011 , Add Pattern Copy Function [E]
    End Sub
    ''' <summary>樣本貼上</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuPatternPaste_Click(sender As Object, e As EventArgs) Handles mnuPatternPaste.Click
        If mPatternCopy.Name = "" Then '無資料
            Exit Sub
        End If
        'Eason 20170113 Ticket:100011 , Add Pattern Copy Function [S]

        Dim mPatternName As String = ""
        mPatternName = InputBox("Input Paste Pattern ID", "Pattern")

        If mPatternName = "" Then '無資料
            Exit Sub
        End If

        'Avoid Paste two Times ,Clone new one will be sure the data as the same First
        Dim mPatternCopyBuffer As New CRecipePattern()
        mPatternCopyBuffer = mPatternCopy.Clone()

        mPatternCopyBuffer.Name = mPatternName 'Eason 20170222 Ticket:100011 , Bug fix

        'Dim mPattern As String = mPatternCopy.Name 'Eason - 20170113 Ticket:100011 , Add Pattern Copy Function
        ''[Use the Old Name]
        'Dim mPattern As String = mPatternCopyBuffer.Name 'Eason + 20170113 Ticket:100011 , Add Pattern Copy Function
        ''[USe Input Name]
        Dim mPattern As String = mPatternName 'Eason + 20170113 Ticket:100011 , Add Pattern Copy Function
        Dim mPastedName As String
        Dim mCopyIndex As Integer = 0
        mPastedName = mPattern
        Do
            If gRecipeEdit.Pattern.ContainsKey(mPastedName) Then '找不重覆名稱
                mCopyIndex += 1
                mPastedName = mPattern & mCopyIndex
            Else
                Exit Do
            End If
        Loop
        'gRecipeEdit.Pattern.Add(mPastedName, mPatternCopy) 'Eason - 20170113 Ticket:100011 , Add Pattern Copy Function
        gRecipeEdit.Pattern.Add(mPastedName, mPatternCopyBuffer) 'Eason + 20170113 Ticket:100011 , Add Pattern Copy Function
        RefreshPatternEditor(lstPatternID.Items.Count - 1) '建立Pattern選單並選取新增項目
        RefreshUI()
        'Eason 20170113 Ticket:100011 , Add Pattern Copy Function [E]
    End Sub
#End Region

#Region "Round操作"
    Sub RoundRefresh(ByVal selectedRound As Integer)
        lstRoundNo.Items.Clear() '顯示Pattern下有幾個Round
        '20170616_Start

        For i As Integer = 0 To gRecipeEdit.Pattern(treePattern.SelectedNode.Text).Round.Count - 1
            lstRoundNo.Items.Add((i + 1).ToString)
        Next
        If lstRoundNo.Items.Count > selectedRound Then
            lstRoundNo.SelectedIndex = selectedRound
        End If
        '20170616_End
    End Sub
    Dim CopyRound As CPatternRound
    Dim CopyRoundIndex As Object

    ''' <summary>新增Round</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRoundAdd_Click(sender As Object, e As EventArgs) Handles btnRoundAdd.Click, mnuRoundAdd.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnRoundAdd]" & vbTab & "Click")
        btnRoundAdd.Enabled = False
        If Not gRecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnRoundAdd.Enabled = True
            Exit Sub
        End If
        If lstPatternID.SelectedIndex < 0 Then
            If lstPatternID.Items.Count = 0 Then
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            btnRoundAdd.Enabled = True
            Exit Sub
        End If

        '20170616_Start
        Dim treeNode As String
        If SelectedNodePath.Length > 8 Then
            treeNode = treePattern.SelectedNode.Text
            Dim mPatternRound As New CPatternRound
            gRecipeEdit.Pattern(treeNode).Round.Add(mPatternRound) '新增Round
            gRecipeEdit.Pattern(treeNode).RoundCount = gRecipeEdit.Pattern(treeNode).Round.Count '數量變更

            lstRoundNo.Items.Clear()
            For i As Integer = 0 To gRecipeEdit.Pattern(treeNode).Round.Count - 1
                lstRoundNo.Items.Add((i + 1).ToString)
            Next
            lstRoundNo.SelectedIndex = lstRoundNo.Items.Count - 1 '選取項目
            RefreshUI()
            btnRoundAdd.Enabled = True
        Else
            Exit Sub
        End If
        If chkNodeConnect.CheckState = CheckState.Checked Then
            If CheckNodeConnect() = True Then
                chkNodeConnect.Checked = True
            Else
                chkNodeConnect.Checked = False
            End If
        End If
        'Dim mPatternRound As New CPatternRound
        'gRecipeEdit.Pattern(lstPatternID.SelectedItem).Round.Add(mPatternRound) '新增Round
        'gRecipeEdit.Pattern(lstPatternID.SelectedItem).intRoundCount = gRecipeEdit.Pattern(lstPatternID.SelectedItem).Round.Count '數量變更

        'lstRoundNo.Items.Clear()
        'For i As Integer = 0 To gRecipeEdit.Pattern(lstPatternID.SelectedItem).Round.Count - 1
        '    lstRoundNo.Items.Add((i + 1).ToString)
        'Next
        'lstRoundNo.SelectedIndex = lstRoundNo.Items.Count - 1 '選取項目
        'RefreshUI()
        'btnRoundAdd.Enabled = True

        '20170616_End

    End Sub

    ''' <summary>刪除Round</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRoundDelete_Click(sender As Object, e As EventArgs) Handles btnRoundDelete.Click, mnuRoundDelete.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnRoundDelete]" & vbTab & "Click")
        btnRoundDelete.Enabled = False
        If Not gRecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnRoundDelete.Enabled = True
            Exit Sub
        End If
        If lstRoundNo.Items.Count = 0 Then '沒項目
            btnRoundDelete.Enabled = True
            Exit Sub
        End If
        If lstPatternID.SelectedIndex < 0 Then
            If lstPatternID.Items.Count = 0 Then
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            btnRoundDelete.Enabled = True
            Exit Sub
        End If

        '20170616_Start
        If SelectedNodePath.Length < 8 Then
            Exit Sub
        End If
        Dim treeNode As String
        treeNode = treePattern.SelectedNode.Text

        Dim mPattern As String = lstPatternID.SelectedItem
        If Not gRecipeEdit.Pattern.ContainsKey(treeNode) Then
            lstPatternID.Items.Remove(treeNode)
            btnRoundDelete.Enabled = True
            Exit Sub
        End If
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        If mRoundNo < 0 Then
            btnRoundDelete.Enabled = True
            Exit Sub
        End If

        If mRoundNo >= gRecipeEdit.Pattern(treeNode).Round.Count Then
            btnRoundDelete.Enabled = True
            Exit Sub
        End If
        If MsgBox("Delete this Round? ", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Recipe") <> MsgBoxResult.Yes Then
            btnRoundDelete.Enabled = True
            Exit Sub
        End If
        'Dim mPattern As String = lstPatternID.SelectedItem
        'If Not gRecipeEdit.Pattern.ContainsKey(mPattern) Then
        '    lstPatternID.Items.Remove(mPattern)
        '    btnRoundDelete.Enabled = True
        '    Exit Sub
        'End If
        'Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        'If mRoundNo < 0 Then
        '    btnRoundDelete.Enabled = True
        '    Exit Sub
        'End If

        'If mRoundNo >= gRecipeEdit.Pattern(treeNode).Round.Count Then
        '    btnRoundDelete.Enabled = True
        '    Exit Sub
        'End If
        'If MsgBox("Delete this Round? ", MsgBoxStyle.YesNo, "Recipe") <> MsgBoxResult.Yes Then
        '    btnRoundDelete.Enabled = True
        '    Exit Sub
        'End If
        '20170616_End

        '20170616_Start
        If SelectedNodePath.Length > 8 Then
            gRecipeEdit.Pattern(treeNode).Round.RemoveAt(mRoundNo)
            gRecipeEdit.Pattern(treeNode).RoundCount = gRecipeEdit.Pattern(treeNode).Round.Count

            '=== Soni + 2017.01.20 刪Round時強制清零, 第一道保護 ===
            If gRecipeEdit.Pattern(treeNode).Round.Count > 0 Then
                gRecipeEdit.Pattern(treeNode).Round(gRecipeEdit.Pattern(treeNode).Round.Count - 1).ProcessTime = 0
            End If
            '=== Soni + 2017.01.20 刪Round時強制清零, 第一道保護 ===

            lstRoundNo.Items.RemoveAt(lstRoundNo.Items.Count - 1) '移除最後一個 確保編號顯示一致
            If lstRoundNo.Items.Count > 0 Then
                If mRoundNo = gRecipeEdit.Pattern(treeNode).Round.Count Then
                    lstRoundNo.SelectedIndex = mRoundNo - 1
                Else
                    lstRoundNo.SelectedIndex = mRoundNo
                End If

            End If
            RefreshUI()
            btnRoundDelete.Enabled = True
        Else
            Exit Sub
        End If

        If chkNodeConnect.CheckState = CheckState.Checked Then
            If CheckNodeConnect() = True Then
                chkNodeConnect.Checked = True
            Else
                chkNodeConnect.Checked = False
            End If
        End If

        'gRecipeEdit.Pattern(mPattern).Round.RemoveAt(mRoundNo)
        'gRecipeEdit.Pattern(mPattern).intRoundCount = gRecipeEdit.Pattern(mPattern).Round.Count

        ''=== Soni + 2017.01.20 刪Round時強制清零, 第一道保護 ===
        'If gRecipeEdit.Pattern(mPattern).Round.Count > 0 Then
        '    gRecipeEdit.Pattern(mPattern).Round(gRecipeEdit.Pattern(mPattern).Round.Count - 1).ProcessTime = 0 '
        'End If
        ''=== Soni + 2017.01.20 刪Round時強制清零, 第一道保護 ===

        'lstRoundNo.Items.RemoveAt(lstRoundNo.Items.Count - 1) '移除最後一個 確保編號顯示一致
        'If lstRoundNo.Items.Count > 0 Then
        '    lstRoundNo.SelectedIndex = mRoundNo - 1
        'End If
        'RefreshUI()
        'btnRoundDelete.Enabled = True

        '20170616_End

    End Sub
    ''' <summary>上移Round</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRoundUp_Click(sender As Object, e As EventArgs) Handles btnRoundUp.Click, mnuRoundMoveUp.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnRoundUp]" & vbTab & "Click")
        btnRoundUp.Enabled = False
        If Not gRecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnRoundUp.Enabled = True
            Exit Sub
        End If
        If lstRoundNo.SelectedIndex < 0 Then
            '請先選擇執行次數
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000044))
            btnRoundUp.Enabled = True
            Exit Sub
        End If
        '20170616_Start
        If SelectedNodePath.Length < 8 Then
            '請先選取Recipe Pattern
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            btnRoundUp.Enabled = True
            Exit Sub
        End If
        Dim nodename As String = treePattern.SelectedNode.Text
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        Dim mRound As CPatternRound = gRecipeEdit.Pattern(nodename).Round(mRoundNo) '備份
        gRecipeEdit.Pattern(nodename).Round.RemoveAt(mRoundNo)
        If mRoundNo = 0 Then mRoundNo = 1 '最上面沒得上
        gRecipeEdit.Pattern(nodename).Round.Insert(mRoundNo - 1, mRound) '向上插入
        RoundRefresh(mRoundNo - 1)
        btnRoundUp.Enabled = True
        'If lstPatternID.SelectedIndex < 0 Then
        '    '請先選取Recipe Pattern
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
        '    btnRoundUp.Enabled = True
        '    Exit Sub
        'End If
        'Dim mPatternID As String = lstPatternID.SelectedItem
        'Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        'Dim mRound As CPatternRound = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo) '備份
        'gRecipeEdit.Pattern(mPatternID).Round.RemoveAt(mRoundNo)
        'If mRoundNo = 0 Then mRoundNo = 1 '最上面沒得上
        'gRecipeEdit.Pattern(mPatternID).Round.Insert(mRoundNo - 1, mRound) '向上插入
        'RoundRefresh(mRoundNo - 1)
        'btnRoundUp.Enabled = True
        '20170616_End
        'jimmy 20170718
        If chkNodeConnect.Checked = True Then
            Dim tempPath() As String = SelectedNodePath.Split(",")
            Dim treeNode1 As String
            treeNode1 = treePattern.SelectedNode.Text
            If tempPath.Count >= 2 Then
                If gRecipeEdit.Node(tempPath(1)).ContainsKey(SelectedNodePath) Then
                    Dim tempNode1 As String
                    For Each tempNode1 In gRecipeEdit.Node(tempPath(1)).Keys
                        'jimmy 20170718
                        '只有一個node時不能用nodeconnect , 超過一個round也不行
                        If gRecipeEdit.Node(tempPath(1)).Count = 1 Or gRecipeEdit.Pattern(treeNode1).Round.Count > 1 Then
                            gRecipeEdit.Node(tempPath(1))(tempNode1).IsNodeConnect = False
                            chkNodeConnect.Checked = False
                        Else
                            gRecipeEdit.Node(tempPath(1))(tempNode1).IsNodeConnect = chkNodeConnect.Checked
                        End If
                    Next
                Else

                End If
            Else
                chkNodeConnect.Checked = False
            End If
        End If
    End Sub
    ''' <summary>下移Round</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRoundDown_Click(sender As Object, e As EventArgs) Handles btnRoundDown.Click, mnuRoundMoveDown.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnRoundDown]" & vbTab & "Click")
        btnRoundDown.Enabled = False
        If Not gRecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnRoundDown.Enabled = True
            Exit Sub
        End If
        If lstRoundNo.SelectedIndex < 0 Then
            '請先選擇執行次數
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000044))
            btnRoundDown.Enabled = True
            Exit Sub
        End If

        '20170616_Start
        Dim nodename As String = treePattern.SelectedNode.Text
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        '已經在最後一個round,不應該在下移
        If mRoundNo = gRecipeEdit.Pattern(nodename).Round.Count - 1 Then
            btnRoundDown.Enabled = True
            Exit Sub
        End If

        Dim mRound As CPatternRound = gRecipeEdit.Pattern(nodename).Round(mRoundNo) '備份
        gRecipeEdit.Pattern(nodename).Round.RemoveAt(mRoundNo)

        If gRecipeEdit.Pattern(nodename).Round.Count = 0 Then '沒Round可以往下...
            gRecipeEdit.Pattern(nodename).Round.Insert(0, mRound) '向下插入
        Else '有其他Round可以互換..
            gRecipeEdit.Pattern(nodename).Round.Insert(mRoundNo + 1, mRound) '向下插入
        End If
        RoundRefresh(mRoundNo + 1)
        btnRoundDown.Enabled = True
        'Dim mPatternID As String = lstPatternID.SelectedItem
        'Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        'If mRoundNo = gRecipeEdit.Pattern(mPatternID).Round.Count - 1 Then mRoundNo = gRecipeEdit.Pattern(mPatternID).Round.Count - 1 '最下面沒得下
        'Dim mRound As CPatternRound = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo) '備份
        'gRecipeEdit.Pattern(mPatternID).Round.RemoveAt(mRoundNo)

        'If gRecipeEdit.Pattern(mPatternID).Round.Count = 0 Then '沒Round可以往下...
        '    gRecipeEdit.Pattern(mPatternID).Round.Insert(0, mRound) '向下插入
        'Else '有其他Round可以互換..
        '    gRecipeEdit.Pattern(mPatternID).Round.Insert(mRoundNo + 1, mRound) '向下插入
        'End If
        'RoundRefresh(mRoundNo + 1)
        'btnRoundDown.Enabled = True
        '20170616_End
    End Sub

    Private Sub lstRoundNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRoundNo.SelectedIndexChanged
        If lstRoundNo.SelectedIndex < 0 Then
            Exit Sub
        End If
        '20170616_Start
        ListRefresh(0)
        '20170616_End
    End Sub

    Private Sub mnuRoundCopy_Click(sender As Object, e As EventArgs) Handles mnuRoundCopy.Click
        If (lstPatternID.SelectedItem <> Nothing AndAlso lstRoundNo.SelectedItem <> Nothing) Then
            'Dim selectPatternName As String = lstPatternID.SelectedItem
            Dim TreeNode As String = treePattern.SelectedNode.Text
            Dim selectRoundNo As String = lstRoundNo.SelectedIndex
            CopyRound = New CPatternRound()
            CopyRound = gRecipeEdit.Pattern(TreeNode).Round(selectRoundNo).Clone()
        End If
    End Sub

    Private Sub mnuRoundPaste_Click(sender As Object, e As EventArgs) Handles mnuRoundPaste.Click
        If (lstPatternID.SelectedItem <> Nothing AndAlso lstRoundNo.SelectedItem <> Nothing) Then
            'Dim selectPatternName As String = lstPatternID.SelectedItem
            Dim TreeNode As String = treePattern.SelectedNode.Text
            gRecipeEdit.Pattern(TreeNode).Round.Insert(lstRoundNo.SelectedIndex + 1, CopyRound)
            Dim NewIndex As Integer = lstRoundNo.SelectedIndex + 1

            lstRoundNo.Items.Clear()
            For i As Integer = 0 To gRecipeEdit.Pattern(TreeNode).Round.Count - 1
                lstRoundNo.Items.Add((i + 1).ToString)
            Next
            lstRoundNo.SelectedIndex = NewIndex '選取項目
            RefreshUI()
        End If
    End Sub
#End Region

#Region "Step操作"
    Private Sub btnStepDelete_Click(sender As Object, e As EventArgs) Handles btnStepDelete.Click, mnuStepDelete.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepDelete]" & vbTab & "Click")
        btnStepDelete.Enabled = False
        If Not gRecipeEdit.Editable Then '不可編輯
            BtnReadOnlyBehavior(sender)
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnStepDelete.Enabled = True
            Exit Sub
        End If

        If lstPatternID.SelectedIndex < 0 Then '未選Pattern
            '請先選取Recipe Pattern
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            btnStepDelete.Enabled = True
            Exit Sub
        End If

        If lstRoundNo.SelectedIndex < 0 Then '未選Round
            '請先選擇執行次數
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000044))
            btnStepDelete.Enabled = True
            Exit Sub
        End If

        If dgvStep.SelectedCells(0).RowIndex < 0 Then '未選Step
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(0).Selected = True
                'lstStep.SelectedIndex = 0
            End If
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            btnStepDelete.Enabled = True
            Exit Sub
        End If
        '20170616_Start
        Dim treeNode As String = treePattern.SelectedNode.Text
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        If gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep.Count = 0 Then '沒步
            RefreshTreeView(treePattern)
            btnStepDelete.Enabled = True
            Exit Sub
        End If

        'Dim intI As Integer
        'If mRecipeStepNo <> -1 And mStepNo <> -1 Then
        If MsgBox("Is Delete the Step ? ", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Recipe") <> MsgBoxResult.Yes Then '取消動作
            btnStepDelete.Enabled = True
            Exit Sub
        End If

        gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep.RemoveAt(mStepNo)
        gRecipeEdit.Pattern(treeNode).Round(mRoundNo).StepCount = gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep.Count
        'ContiRefresh() '更新連續運動
        SelectValveRefresh() '更新選閥動作
        ListRefresh(mStepNo) '更新清單
        If mStepNo = 0 Then 'And gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count > 0 Then '自動選擇其他行
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(mStepNo).Selected = True
            End If
        Else
            dgvStep.Rows(mStepNo - 1).Selected = True
        End If
        RefreshUI()
        btnStepDelete.Enabled = True
        '20170616_End
    End Sub

    Private Sub btnStepUp_Click(sender As Object, e As EventArgs) Handles btnStepUp.Click, mnuStepMoveUp.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepUp]" & vbTab & "Click")
        btnStepUp.Enabled = False
        If Not gRecipeEdit.Editable Then
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnStepUp.Enabled = True
            Exit Sub
        End If

        If dgvStep.SelectedCells.Count <= 0 Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            btnStepUp.Enabled = True
            Exit Sub
        End If

        If dgvStep.SelectedCells(0).RowIndex <= 0 Then
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(0).Selected = True
                'lstStep.SelectedIndex = 0
            End If
            gSyslog.Save("Please, Select Step First!", , eMessageLevel.Warning)
            btnStepUp.Enabled = True
            Exit Sub
        End If
        '20170616_Start
        If lstPatternID.SelectedIndex < 0 Then
            If lstPatternID.Items.Count > 0 Then
                lstPatternID.SelectedIndex = 0
            End If
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            btnStepUp.Enabled = True
            Exit Sub
        End If
        Dim nodename As String = treePattern.SelectedNode.Text
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        Dim lStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex

        If mStepNo >= gRecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            btnStepUp.Enabled = True
            Exit Sub
        End If
        Dim mCStep As CPatternStep = gRecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
        gRecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.RemoveAt(mStepNo)
        If mStepNo = 0 Then mStepNo = 1
        gRecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Insert(mStepNo - 1, mCStep)
        SelectValveRefresh()
        ListRefresh(mStepNo - 1)
        If dgvStep.SelectedCells.Count > 0 Then 'Soni + 2016.09.13 未選保謢
            Call DrawSingleStepGraphicsTest(treePattern.SelectedNode.Text, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, picPcsSingleGraph, sys, mDrawIdx) 'Soni / 2016.12.07
        End If

        btnStepUp.Enabled = True
        'Dim mPatternID As String = lstPatternID.SelectedItem
        'Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        'Dim lStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        'Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex

        'If mStepNo >= gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    btnStepUp.Enabled = True
        '    Exit Sub
        'End If
        'Dim mCStep As CPatternStep = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(mStepNo)
        'gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.RemoveAt(mStepNo)
        'If mStepNo = 0 Then mStepNo = 1
        'gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Insert(mStepNo - 1, mCStep)
        'SelectValveRefresh()
        'ListRefresh(mStepNo - 1)
        'If dgvStep.SelectedCells.Count > 0 Then 'Soni + 2016.09.13 未選保謢
        '    Call DrawSingleStepGraphicsTest(lstPatternID.SelectedItem, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, picPcsSingleGraph, sys, mDrawIdx) 'Soni / 2016.12.07
        'End If

        'btnStepUp.Enabled = True
        '20170616_End
    End Sub

    Private Sub btnStepDown_Click(sender As Object, e As EventArgs) Handles btnStepDown.Click, mnuStepMoveDown.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepDown]" & vbTab & "Click")
        btnStepDown.Enabled = False
        If Not gRecipeEdit.Editable Then
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnStepDown.Enabled = True
            Exit Sub
        End If
        If dgvStep.SelectedCells.Count <= 0 Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            btnStepDown.Enabled = True
            Exit Sub
        End If

        If dgvStep.SelectedCells(0).RowIndex < 0 Then
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(0).Selected = True
            End If
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            btnStepDown.Enabled = True
            Exit Sub
        End If
        If lstPatternID.SelectedIndex < 0 Then
            If lstPatternID.Items.Count > 0 Then
                lstPatternID.SelectedIndex = 0
            End If
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            btnStepDown.Enabled = True
            Exit Sub
        End If
        '20170616_Start
        Dim treeNode As String = treePattern.SelectedNode.Text
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        Dim mMaxStepNo As Integer = gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep.Count - 1 ' dgvStep.Rows.Count
        Dim mCStep As CPatternStep = gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep(mStepNo)

        '[說明]:RoundNo 保護  20161010
        If mRoundNo <= -1 Then
            '請先選擇執行次數
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000044))
            MsgBox(gMsgHandler.GetMessage(Warn_3000044), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Else
            gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep.RemoveAt(mStepNo)
            If mStepNo = mMaxStepNo Then
                mStepNo = mMaxStepNo - 1
            End If
            gRecipeEdit.Pattern(treeNode).Round(mRoundNo).CStep.Insert(mStepNo + 1, mCStep)
            SelectValveRefresh()
            ListRefresh(mStepNo + 1)
            If dgvStep.SelectedCells.Count > 0 Then 'Soni + 2016.09.13 未選保謢
                Call DrawSingleStepGraphicsTest(treePattern.SelectedNode.Text, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, picPcsSingleGraph, sys, mDrawIdx) 'Soni / 2016.12.07
            End If
        End If


        btnStepDown.Enabled = True
        '20170616_End
    End Sub

    Dim CopyObj As DataGridViewRow = New DataGridViewRow
    Dim CopyStep As CPatternStep

    Private Sub btnStepCopy_Click(sender As Object, e As EventArgs) Handles mnuStepCopy.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepCopy]" & vbTab & "Click")
        If Not gRecipeEdit.Editable Then
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            Exit Sub
        End If

        If dgvStep.SelectedCells.Count <= 0 Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            Exit Sub
        End If

        If dgvStep.SelectedCells(0).RowIndex <= 0 Then
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(0).Selected = True
            End If
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            Exit Sub
        End If

        'If lstPatternID.SelectedIndex < 0 Then
        '    If lstPatternID.Items.Count > 0 Then
        '        lstPatternID.SelectedIndex = 0
        '    End If
        '    '請選擇場景
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
        '    Exit Sub
        'End If

        If SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If

        Dim TreeNode As String = treePattern.SelectedNode.Text

        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        Dim lStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex


        CopyObj = CType(dgvStep.Rows(lStepNo).Clone(), DataGridViewRow)
        For i = 0 To dgvStep.Rows(lStepNo).Cells.Count - 1
            CopyObj.Cells(i).Value = dgvStep.Rows(lStepNo).Cells(i).Value
        Next

        ' CopyObj = dgvStep.Rows(lStepNo)
        CopyStep = gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep(mStepNo).Clone()

    End Sub

    Private Sub btnStepPaste_Click(sender As Object, e As EventArgs) Handles mnuStepPaste.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepPaste]" & vbTab & "Click")
        If Not gRecipeEdit.Editable Then
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            Exit Sub
        End If

        If dgvStep.SelectedCells.Count <= 0 Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            Exit Sub
        End If

        If dgvStep.SelectedCells(0).RowIndex <= 0 Then
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(0).Selected = True
            End If
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            Exit Sub
        End If

        'If lstPatternID.SelectedIndex < 0 Then
        '    If lstPatternID.Items.Count > 0 Then
        '        lstPatternID.SelectedIndex = 0
        '    End If
        '    '請選擇場景
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
        '    Exit Sub
        'End If

        If SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If

        Dim TreeNode As String = treePattern.SelectedNode.Text
        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        Dim lStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        Try

            dgvStep.Rows.Insert(dgvStep.SelectedCells(0).RowIndex, CopyObj)
            gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Insert(dgvStep.SelectedCells(0).RowIndex, CopyStep)
        Catch ex As Exception

            MessageBox.Show(ex.ToString & " " & ex.GetType.ToString)
        End Try


        'dgvStep.Rows.Add(mStepNo, mValve, mContiType, mFunctionType, mWeight, mComment)
        ' dgvStep.Rows.Insert(dgvStep.SelectedCells(0).RowIndex, CopyObj)
    End Sub

    ''' <summary>編輯用步驟</summary>
    ''' <remarks></remarks>
    Dim mCStep As New CPatternStep

    ''' <summary>選閥設定全面更新</summary>
    ''' <remarks></remarks>
    Public Sub SelectValveRefresh()
        Dim mCStep As New CPatternStep
        Dim TreeNode As String = treePattern.SelectedNode.Text
        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim mRoundNo As Integer '選取Round號

        mRoundNo = lstRoundNo.SelectedIndex
        Dim valveNo As enmValve = enmValve.No1
        For i As Integer = 0 To gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Count - 1 '對每一步

            If gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep(i).StepType = eStepFunctionType.SelectValve Then '到ContiEnd結束
                Select Case gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep(i).SelectValve.ValveNo
                    Case eValveWorkMode.Valve1
                        valveNo = enmValve.No1 '閥號暫存
                    Case eValveWorkMode.Valve2
                        'jimmy 20170630
                        If gSSystemParameter.MultiDispenseEnable = True Then
                            valveNo = enmValve.No1
                            Exit Sub
                        End If
                        valveNo = enmValve.No2 '閥號暫存
                End Select
            Else
                gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep(i).SelectValve.ValveNo = valveNo '沿用暫存閥號
            End If
        Next
    End Sub
    ''' <summary>
    ''' 步驟清除, 只剩標題
    ''' </summary>
    ''' <remarks></remarks>
    Sub StepClear()
        dgvStep.Rows.Clear()
        With dgvStep.Columns '清除時一併處理Column

            .Clear()
            .Add("Step", "Step")
            .Add("Valve", "Valve")
            .Add("Conti", "Conti")
            .Add("Func", "Func")
            '.Add("Basic", "Basic")
            .Add("Weight", "Weight")
            '.Add("PatternID", "PatternID")
            .Add("Comment", "Comment")

            dgvStep.ColumnHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 9)
            For i As Integer = 0 To dgvStep.Columns.Count - 1 '
                dgvStep.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable '把每個Column的排序功能鎖住
            Next

        End With
    End Sub
    ''' <summary>Step顯示介面全面更新</summary>
    ''' <remarks></remarks>
    Public Sub ListRefresh(ByVal selectedRow As Integer)
        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim treenode As String
        Dim mRoundNo As Integer
        Dim mStepNo As String
        Dim mValve As String
        Dim mContiType As String = ""
        Dim mFunctionType As String = "##"
        Dim mSpd As String = 0
        Dim mWeight As String = ""
        Dim mComment As String = ""
        If SelectedNodePath.Length < 8 Then
            Exit Sub
        End If

        mRoundNo = lstRoundNo.SelectedIndex
        StepClear()

        If sys Is Nothing Then '物件不存在
            Exit Sub
        End If
        If sys.StageNo < 0 Then '索引超過範圍
            Exit Sub
        End If
        If sys.StageNo >= gRecipeEdit.StageNodeID.Count Then '索引超過範圍
            Exit Sub
        End If
        If mPatternID = "" Then '名稱不存在
            Exit Sub
        End If
        If mRoundNo < 0 Then
            Exit Sub
        End If


        '20170616_Start
        treenode = treePattern.SelectedNode.Text
        If Not gRecipeEdit.Pattern.ContainsKey(treenode) Then '名稱不存在
            Exit Sub
        End If
        If mRoundNo >= gRecipeEdit.Pattern(treenode).Round.Count Then '索引超過範圍
            Exit Sub
        End If
        'If Not gRecipeEdit.Pattern.ContainsKey(mPatternID) Then '名稱不存在
        '    Exit Sub
        'End If
        'If mRoundNo >= gRecipeEdit.Pattern(mPatternID).Round.Count Then '索引超過範圍
        '    Exit Sub
        'End If
        '20170616_End

        '20170616_Start


        For intI = 0 To gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep.Count - 1
            'Dim mStepPatternID As String = "" 'Soni / 2016.09.28 往迴圈內移動 因為設定後下面列的顯示是錯的
            mStepNo = intI.ToString().PadLeft(gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep.Count.ToString().Length, "0")
            mValve = GetStepValveType(treenode, mRoundNo, intI, sys)
            mFunctionType = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).enmGraphicTypeToString()
            mWeight = ""
            '20160920
            mComment = ""
            Select Case gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).StepType
                Case eStepFunctionType.Arc2D
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    mWeight = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Arc2D.WeightControl.Weight
                    '20160920
                    mComment = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Arc2D.Comment
                Case eStepFunctionType.Arc3D
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    mWeight = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Arc3D.WeightControl.Weight
                    'mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Arc3D.WeightControl.Weight
                    'Case eStepFunctionType.Array
                    '    mStepPatternID = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Array.PatternID
                Case eStepFunctionType.CCDEnd
                Case eStepFunctionType.CCDLine
                Case eStepFunctionType.CCDStart
                Case eStepFunctionType.Circle2D
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    mWeight = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Circle2D.WeightControl.Weight
                    '20160920
                    mComment = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Circle2D.Comment
                Case eStepFunctionType.Circle3D
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    mWeight = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Circle3D.WeightControl.Weight
                Case eStepFunctionType.ContiEnd
                Case eStepFunctionType.ContiStart

                Case eStepFunctionType.Dots3D
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    mWeight = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Dots3D.WeightControl.Weight
                Case eStepFunctionType.EndLine
                Case eStepFunctionType.ExtendOff
                Case eStepFunctionType.ExtendOn
                Case eStepFunctionType.FirstLine
                Case eStepFunctionType.Inspect
                Case eStepFunctionType.Line3D
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    mWeight = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Line3D.WeightControl.Weight
                    '20160920
                    mComment = gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep(intI).Line3D.Comment
                Case eStepFunctionType.Move3D
                Case eStepFunctionType.Picture
                Case eStepFunctionType.Rectangle
                Case eStepFunctionType.SelectValve
                    'Case eStepFunctionType.SubPattern
                    '    mStepPatternID = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).SubPattern.PatternID
                Case eStepFunctionType.Wait

            End Select
            '20160920 JeffDel
            'mComment = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Comment
            dgvStep.Rows.Add(mStepNo, mValve, mContiType, mFunctionType, mWeight, mComment) ' mStepPatternID, mComment) ' mXs, mYs, mZs, mXe, mYe, mZe, mXc, mYc, mZc, keyName, mSpd, mAcc, mDec)
        Next



        'For intI = 0 To gRecipeEdit.Pattern(treenode).Round(mRoundNo).CStep.Count - 1
        '    'Dim mStepPatternID As String = "" 'Soni / 2016.09.28 往迴圈內移動 因為設定後下面列的顯示是錯的
        '    mStepNo = intI.ToString().PadLeft(gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count.ToString().Length, "0")
        '    mValve = GetStepValveType(mPatternID, mRoundNo, intI, sys)
        '    mFunctionType = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).enmGraphicTypeToString()
        '    mWeight = ""
        '    '20160920
        '    mComment = ""
        '    Select Case gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).StepType
        '        Case eStepFunctionType.Arc2D
        '            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
        '            mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Arc2D.WeightControl.Weight
        '            '20160920
        '            mComment = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Arc2D.Comment
        '        Case eStepFunctionType.Arc3D
        '            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
        '            mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Arc3D.WeightControl.Weight
        '            'Case eStepFunctionType.Array
        '            '    mStepPatternID = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Array.PatternID
        '        Case eStepFunctionType.CCDEnd
        '        Case eStepFunctionType.CCDLine
        '        Case eStepFunctionType.CCDStart
        '        Case eStepFunctionType.Circle2D
        '            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
        '            mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Circle2D.WeightControl.Weight
        '            '20160920
        '            mComment = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Circle2D.Comment
        '        Case eStepFunctionType.Circle3D
        '            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
        '            mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Circle3D.WeightControl.Weight
        '        Case eStepFunctionType.ContiEnd
        '        Case eStepFunctionType.ContiStart

        '        Case eStepFunctionType.Dots3D
        '            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
        '            mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Dots3D.WeightControl.Weight
        '        Case eStepFunctionType.EndLine
        '        Case eStepFunctionType.ExtendOff
        '        Case eStepFunctionType.ExtendOn
        '        Case eStepFunctionType.FirstLine
        '        Case eStepFunctionType.Inspect
        '        Case eStepFunctionType.Line3D
        '            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
        '            mWeight = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Line3D.WeightControl.Weight
        '            '20160920
        '            mComment = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Line3D.Comment
        '        Case eStepFunctionType.Move3D
        '        Case eStepFunctionType.Picture
        '        Case eStepFunctionType.Rectangle
        '        Case eStepFunctionType.SelectValve
        '            'Case eStepFunctionType.SubPattern
        '            '    mStepPatternID = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).SubPattern.PatternID
        '        Case eStepFunctionType.Wait

        '    End Select
        '    '20160920 JeffDel
        '    'mComment = gRecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(intI).Comment
        '    dgvStep.Rows.Add(mStepNo, mValve, mContiType, mFunctionType, mWeight, mComment) ' mStepPatternID, mComment) ' mXs, mYs, mZs, mXe, mYe, mZe, mXc, mYc, mZc, keyName, mSpd, mAcc, mDec)
        'Next

        If selectedRow >= dgvStep.Rows.Count Then '無法選
            Exit Sub
        End If
        dgvStep.Rows(selectedRow).Selected = True
        dgvStep.Refresh()
    End Sub

    Private Sub lstStepNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgvStep.DoubleClick

        'If lstPatternID.SelectedIndex = -1 Then '沒選時代選
        '    If lstPatternID.Items.Count > 0 Then
        '        lstPatternID.SelectedIndex = 0
        '    End If
        '    Exit Sub
        'End If
        If SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        If lstRoundNo.SelectedIndex < 0 Then
            If lstRoundNo.Items.Count > 0 Then
                lstRoundNo.SelectedIndex = 0
            End If
            Exit Sub
        End If
        If dgvStep.SelectedCells.Count <= 0 Then 'Step沒選
            Exit Sub
        End If
        If dgvStep.SelectedCells(0).RowIndex < 0 Then 'Step沒選
            Exit Sub
        End If

        Dim nodename As String = treePattern.SelectedNode.Text
        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex
        If Not gRecipeEdit.Pattern.ContainsKey(nodename) Then
            '輸入資料錯誤(Pattern Error)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If mRoundNo >= gRecipeEdit.Pattern(nodename).Round.Count Then
            '輸入資料錯誤(Round Error)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If dgvStep.SelectedCells(0).RowIndex >= gRecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then 'Step選在最末空白
            Exit Sub
        End If

        'Jeffadd20160310
        If IsNothing(SelectedNodePath) = True Then
            '請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        ElseIf SelectedNodePath = "" Then
            '請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        Else
            If Not SelectedNodePath.Contains(",") Then '選取資料異常
                Exit Sub
            End If
            Dim splitedString() As String = SelectedNodePath.Split(",")
            If splitedString.Length < 2 Then '資料長度不足
                Exit Sub
            End If
            Dim str As String = splitedString(1)
            If gRecipeEdit.Node(CInt(str)).ContainsKey(SelectedNodePath) = False Then
                'tabStep.Enabled = False
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            Else
                'tabStep.Enabled = True
            End If
        End If

        DrawPCSSingleGraphics()
        tabView.SelectTab(0) '協助切換至CCD畫面

        '[說明]:新增Recipe04Step顯示 20160615 Jeffadd
        gfrmRecipe04Step = New frmRecipe04Step(Me, False, gRecipeEdit)   '20160820
        gfrmRecipe04Step.Step = gRecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(dgvStep.SelectedCells(0).RowIndex)
        '20170606_Toby add
        gfrmRecipe04Step.AddNewStep = False
        gfrmRecipe04Step.RecipeEdit = gRecipeEdit
        SetStepLocation(gfrmRecipe04Step)
        'gfrmRecipe04Step.Show() 20160805
        gfrmRecipe04Step.ShowDialog()
        gfrmRecipe04Step.BringToFront()
        'ShowSelectRowFunction() '顯示選取的設定
        '[說明]:顯示資料 20160615 Jeffadd
        Call gfrmRecipe04Step.ShowSelectRowFunction(treePattern.SelectedNode.Text, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex)

    End Sub


    Public Sub DrawPCSSingleGraphics()

        If lstPatternID.SelectedIndex >= 0 Then
            mDrawIdx.PatternName = treePattern.SelectedNode.Text

            '20170505
            If SelectedNodePath <> "" And SelectedNodePath.Length > 8 Then
                Select Case SelectedNodePath.Substring(2, 1)
                    Case "0"
                        mDrawIdx.StageNo = enmStage.No1
                    Case "1"
                        mDrawIdx.StageNo = enmStage.No2
                    Case "2"
                        mDrawIdx.StageNo = enmStage.No3
                    Case "3"
                        mDrawIdx.StageNo = enmStage.No4
                End Select
                mDrawIdx.path = treePattern.SelectedNode.Name
            End If
            'mDrawIdx.path = treePattern.SelectedNode.Name
        Else
            Exit Sub
        End If
        If sys Is Nothing Then
            Exit Sub
        End If

        '畫圖   20161111
        Call DrawSingleGraphicsTest(picPcsSingleGraph, sys, mDrawIdx)
        '選擇哪個線段 那個線段就變成紅色 20160805
        If dgvStep.SelectedCells.Count > 0 Then 'Soni + 2016.09.13 未選保謢
            Call DrawSingleStepGraphicsTest(treePattern.SelectedNode.Text, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, picPcsSingleGraph, sys, mDrawIdx)
        End If

    End Sub


#End Region

    Private Sub frmRecipe04_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        With lstPatternID.Items
            .Clear()
            For i As Integer = 0 To gRecipeEdit.Pattern.Count - 1
                .Add(gRecipeEdit.Pattern.Keys(i))
            Next
            If lstPatternID.Items.Count > 0 Then
                lstPatternID.SelectedIndex = 0
            End If
        End With

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                btnLoadB.Visible = True
                btnUnloadB.Visible = True

                '20161010 Barry提議Recipe不放置秤重
                btnMachineABalance.Visible = False
                btnMachineBBalance.Visible = False
               
                mnuLoadB.Visible = True
                mnuUnloadB.Visible = True
            Case enmMachineType.DCS_500AD
                btnLoadB.Visible = False
                btnUnloadB.Visible = False
                btnMachineBBalance.Visible = False
                btnLoad.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Load
                btnUnload.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Unload
                btnMachineABalance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Weight
                mnuLoadB.Visible = False
                mnuUnloadB.Visible = False

            Case enmMachineType.DCS_350A
                btnLoadB.Visible = True
                btnUnloadB.Visible = True
                btnMachineBBalance.Visible = False
                btnLoad.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Load
                btnUnload.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Unload
                btnMachineABalance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Weight
                '20170321
                ' Case enmMachineType.DCS_350A
                btnMachineABalance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Weight1
                btnMachineBBalance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Weight2
                mnuLoadB.Visible = True
                mnuUnloadB.Visible = True

            Case Else
                btnLoadB.Visible = False
                btnUnloadB.Visible = False
                btnMachineBBalance.Visible = False
                '20160901
                btnLoad.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Load
                btnUnload.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Unload
                btnMachineABalance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Weight
               
                mnuLoadB.Visible = False
                mnuUnloadB.Visible = False
                

        End Select

        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height
        Select Case gAOICollection.GetCCDType(sys.CCDNo) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        btnSelect.Visible = True

        'Soni / 2017.07.21 功能未開放 只有最高權限能用
        If gUserLevel = enmUserLevel.eSoftwareMaker Then
            btnMove3D.Visible = True
            btnWait.Visible = True
            btnRun.Visible = True
            btnDryRun.Visible = True
            'btnCCDRun.Visible = True
            btnPause.Visible = True
        Else
            btnMove3D.Visible = False
            btnWait.Visible = False
            btnRun.Visible = False
            'btnCCDRun.Visible = False

            '20171001
            btnPause.Visible = False
            btnCCDRun.Visible = False
            btnDryRun.Visible = False
        End If
        'Soni / 2017.07.21 功能未開放 只有最高權限能用

        '按鍵排列
        Dim btnList As New List(Of Button)
        btnList.Add(btnSelect)
        btnList.Add(btnLine3D)
        btnList.Add(btnExtendOn)
        btnList.Add(btnContiStart)
        btnList.Add(btnCircle)
        btnList.Add(btnWait)
        btnList.Add(btnExtendOff)
        btnList.Add(btnContiEnd)
        btnList.Add(btnDots3D)
        btnList.Add(btnArc)
        btnList.Add(btnMove3D)
        btnList.Add(btnAddArray) 'Soni + 2016.09.28 Call 陣列Pattern按鍵
        AppendButton(btnList)

        UILoad()

        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)

    End Sub

    Private Sub btnStepBasicEdit_Click(sender As Object, e As EventArgs) Handles btnStepBasicEdit.Click, mnuStepParameter.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnStepBasicEdit]" & vbTab & "Click")
        btnStepBasicEdit.Enabled = False
        If Not gRecipeEdit.Editable Then
            BtnReadOnlyBehavior(btnLock) 'Soni / 2017.07.03 按鍵閃爍顯示
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btnStepBasicEdit.Enabled = True
            Exit Sub
        End If

        If gfrmRecipeStepParameter Is Nothing Then
            gfrmRecipeStepParameter = New frmRecipeStepParameter
        ElseIf gfrmRecipeStepParameter.IsDisposed Then
            gfrmRecipeStepParameter = New frmRecipeStepParameter
        End If
        With gfrmRecipeStepParameter
            .RecipeEdit = gRecipeEdit
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .ShowDialog()
        End With
        RefreshPatternEditor(lstPatternID.SelectedIndex)
        btnStepBasicEdit.Enabled = True
    End Sub


#Region "Function"

    Private Sub btnDots3D_Click(sender As Object, e As EventArgs) Handles btnDots3D.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnDots3D]" & vbTab & "Click")
        SelectStepButton(btnDots3D, eStepFunctionType.Dots3D)
    End Sub

    Private Sub btnLine3D_Click(sender As Object, e As EventArgs) Handles btnLine3D.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLine3D]" & vbTab & "Click")
        SelectStepButton(btnLine3D, eStepFunctionType.Line3D)
    End Sub

    ''' <summary>通用化步驟選擇功能</summary>
    ''' <param name="btn"></param>
    ''' <param name="stepType"></param>
    ''' <remarks></remarks>
    Sub SelectStepButton(ByRef btn As Button, ByVal stepType As eStepFunctionType)
        If btn.Enabled = False Then '防連按
            Exit Sub
        End If
        btn.Enabled = False

        '[說明]:Unluck 保護功能
        If Not gRecipeEdit.Editable Then
            btnLock.BackColor = Color.Green
            System.Threading.Thread.CurrentThread.Join(300)
            btnLock.BackColor = SystemColors.Control
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btn.Enabled = True
            Exit Sub
        End If

        '[說明]:PatternID 保護功能
        If lstPatternID.SelectedIndex = -1 Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            If lstPatternID.Items.Count = 0 Then
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            btn.Enabled = True
            Exit Sub
        End If

        '[說明]:RoundNo 保護功能
        If lstRoundNo.SelectedIndex = -1 Then
            '請先選擇執行次數
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000044))
            lstRoundNo.BackColor = Color.Green
            System.Threading.Thread.CurrentThread.Join(300)
            lstRoundNo.BackColor = Color.White
            btn.Enabled = True
            Exit Sub
        End If

        '[說明]:TreePattern 保護功能
        Dim mStepNo As Integer
        If SelectedNodePath = "" Then
            If treePattern.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.Nodes(0)
            End If
            btn.Enabled = True
            Exit Sub
        End If

        '[說明]: SelectedNodePath長度 保護
        If SelectedNodePath.Length <= 8 Then
            If treePattern.SelectedNode.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.SelectedNode.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            Else
                btnNodeAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnNodeAdd.BackColor = SystemColors.Control
                btnNodeAdd.UseVisualStyleBackColor = True
            End If

            btn.Enabled = True
            Exit Sub
        End If

        '[說明]: dgvStep Counter數 保護
        If dgvStep.SelectedCells.Count <= 0 Then
            mStepNo = 0 '沒選項時預設第一筆
        Else
            If dgvStep.SelectedCells(0).RowIndex < 0 Then
                If dgvStep.Rows.Count = 0 Then
                    mStepNo = 0
                Else
                    mStepNo = dgvStep.Rows.Count
                End If
            Else
                mStepNo = dgvStep.SelectedCells(0).RowIndex + 1
            End If
        End If

        '20170616_Start
        Dim TreeNode As String = treePattern.SelectedNode.Text
        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex

        If Not gRecipeEdit.Pattern.ContainsKey(TreeNode) Then '
            '輸入資料錯誤(Pattern Error)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btn.Enabled = True
            Exit Sub
        End If
        If mRoundNo >= gRecipeEdit.Pattern(TreeNode).Round.Count Then
            '輸入資料錯誤(Round Error)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btn.Enabled = True
            Exit Sub
        End If

        If mStepNo > gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Count Then
            mStepNo = gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If
        '[說明]:新增後Index需指向新物件

        '20170616_End

        mCStep = New CPatternStep '新開步驟避免記憶體重複

        With mCStep
            .StepType = stepType '外部給定
            .StepNo = mStepNo
            .Arc3D.ArcParameterName = "Default" 'Soni / 2017.04.26
            .Arc2D.ArcParameterName = "Default" 'Soni / 2017.04.26
            .Line3D.LineParameterName = "Default" 'Soni / 2017.04.26
            .Dots3D.DotParameterName = "Default" 'Soni / 2017.04.26
            .Circle2D.ArcParameterName = "Default" 'Soni / 2017.04.26
            .Circle3D.ArcParameterName = "Default" 'Soni / 2017.04.26
            .Arc2D.WeightControl.DotCounts = 200 'Soni / 2017.04.26
            .Arc3D.WeightControl.DotCounts = 200 'Soni / 2017.04.26
            .Line3D.WeightControl.DotCounts = 200 'Soni / 2017.04.26
            .Dots3D.WeightControl.DotCounts = 200 'Soni / 2017.04.26
            .Circle2D.WeightControl.DotCounts = 200 'Soni / 2017.04.26
            .Circle3D.WeightControl.DotCounts = 200 'Soni / 2017.04.26
        End With

        tabView.SelectTab(0)

        '[說明]:新增Recipe04Step顯示 20160615 Jeffadd
        gfrmRecipe04Step = New frmRecipe04Step(Me, True, gRecipeEdit) '20160820
        gfrmRecipe04Step.Step = mCStep
        gfrmRecipe04Step.RecipeEdit = gRecipeEdit
        gfrmRecipe04Step.AddNewStep = True
        SetStepLocation(gfrmRecipe04Step)
        gfrmRecipe04Step.ShowDialog()
        DrawPCSSingleGraphics()
        If mStepNo >= dgvStep.Rows.Count Then
            btn.Enabled = True
            Exit Sub
        End If
        If dgvStep.Rows(mStepNo).Cells.Count = 0 Then
            btn.Enabled = True
            Exit Sub
        End If

        dgvStep.CurrentCell = dgvStep.Rows(mStepNo).Cells(0)
        btn.Enabled = True
    End Sub

    Private Sub btnMove3D_Click(sender As Object, e As EventArgs) Handles btnMove3D.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnMove3D]" & vbTab & "Click")
        SelectStepButton(btnMove3D, eStepFunctionType.Move3D)
    End Sub

    Private Sub btnArc_Click(sender As Object, e As EventArgs) Handles btnArc.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArc]" & vbTab & "Click")
        SelectStepButton(btnArc, eStepFunctionType.Arc2D)
    End Sub

    Private Sub btnCircle_Click(sender As Object, e As EventArgs) Handles btnCircle.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircle]" & vbTab & "Click")
        SelectStepButton(btnCircle, eStepFunctionType.Circle2D)
    End Sub

    ''' <summary>通用化步驟選擇功能(不顯示進階設定)</summary>
    ''' <param name="btn"></param>
    ''' <param name="stepType"></param>
    ''' <remarks></remarks>
    Sub SelectStepButtonWithOutConfig(ByRef btn As Button, ByVal stepType As eStepFunctionType)
        If btn.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btn.Enabled = False
        If Not gRecipeEdit.Editable Then
            btnLock.BackColor = Color.Green
            System.Threading.Thread.CurrentThread.Join(300)
            btnLock.BackColor = SystemColors.Control
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            btn.Enabled = True
            Exit Sub
        End If

        If lstPatternID.SelectedIndex = -1 Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            If lstPatternID.Items.Count = 0 Then
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            btn.Enabled = True
            Exit Sub
        End If

        If lstRoundNo.SelectedIndex = -1 Then
            '請先選擇執行次數
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000044))
            lstRoundNo.BackColor = Color.Green
            System.Threading.Thread.CurrentThread.Join(300)
            lstRoundNo.BackColor = Color.White
            btn.Enabled = True
            Exit Sub
        End If

        Dim mStepNo As Integer
        If SelectedNodePath = "" Then
            If treePattern.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.Nodes(0)
            End If

            btn.Enabled = True
            Exit Sub
        End If
        If SelectedNodePath.Length <= 8 Then
            treePattern.SelectedNode.BackColor = Color.Red
            System.Threading.Thread.CurrentThread.Join(300)
            treePattern.SelectedNode.BackColor = Color.White
            btn.Enabled = True
            Exit Sub
        End If


        If dgvStep.SelectedCells.Count <= 0 Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            MsgBox(gMsgHandler.GetMessage(Warn_3000014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btn.Enabled = True
            Exit Sub
        End If

        If dgvStep.SelectedCells(0).RowIndex < 0 Then
            If dgvStep.Rows.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = dgvStep.Rows.Count
            End If
        Else
            mStepNo = dgvStep.SelectedCells(0).RowIndex + 1
        End If


        '20170616_Start
        Dim TreeNode As String = treePattern.SelectedNode.Text
        Dim mPatternID As String = lstPatternID.SelectedItem
        Dim mRoundNo As Integer = lstRoundNo.SelectedIndex


        If mStepNo > gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Count Then
            mStepNo = gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        mCStep = New CPatternStep '新開步驟避免記憶體重複

        With mCStep
            .StepType = stepType
            .StepNo = mStepNo
        End With

        gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Insert(mStepNo, mCStep)
        gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).StepCount = gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep.Count
        '20170616_End

        SelectValveRefresh()
        ListRefresh(mStepNo)

        If mStepNo >= dgvStep.Rows.Count Then
            MsgBox("Step Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btn.Enabled = True
            Exit Sub
        End If
        If dgvStep.Rows(mStepNo).Cells.Count = 0 Then
            MsgBox("Cell Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btn.Enabled = True
            Exit Sub
        End If

        '[說明]:新增後Index需指向新物件
        dgvStep.CurrentCell = dgvStep.Rows(mStepNo).Cells(0)

        With gRecipeEdit.Pattern(TreeNode).Round(mRoundNo).CStep(mStepNo)
            Dim logData As String = "TreeNode(" & TreeNode & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
        End With
        btn.Enabled = True
    End Sub

    Private Sub btnContiStart_Click(sender As Object, e As EventArgs) Handles btnContiStart.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnContiStart]" & vbTab & "Click")
        SelectStepButtonWithOutConfig(btnContiStart, eStepFunctionType.ContiStart)
    End Sub

    Private Sub btnContiEnd_Click(sender As Object, e As EventArgs) Handles btnContiEnd.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnContiEnd]" & vbTab & "Click")
        SelectStepButtonWithOutConfig(btnContiEnd, eStepFunctionType.ContiEnd)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnSelect]" & vbTab & "Click")
        SelectStepButton(btnSelect, eStepFunctionType.SelectValve)
    End Sub

    Sub SetStepLocation(ByRef frm As Form)
        Dim posX As Integer = Me.Location.X + Me.Width - frm.Width '  button.Location.X + button.Parent.Location.X - gfrmRecipe04Step.Width + button.Parent.Parent.Location.X
        Dim posY As Integer = Me.Location.Y + Me.Height - frm.Height ' button.Location.Y + button.Parent.Location.Y + button.Parent.Parent.Location.Y
        frm.StartPosition = FormStartPosition.Manual
        frm.Location = PointToScreen(New Point(posX, posY))
    End Sub

    Private Sub btnWait_Click(sender As Object, e As EventArgs) Handles btnWait.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnWait]" & vbTab & "Click")
        SelectStepButton(btnWait, eStepFunctionType.Wait)
    End Sub

    ''' <summary>延伸路徑開</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExtendOn_Click(sender As Object, e As EventArgs) Handles btnExtendOn.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnExtendOn]" & vbTab & "Click")
        SelectStepButtonWithOutConfig(btnExtendOn, eStepFunctionType.ExtendOn)
    End Sub

    ''' <summary>延伸路徑關</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExtendOff_Click(sender As Object, e As EventArgs) Handles btnExtendOff.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnExtendOff]" & vbTab & "Click")
        SelectStepButtonWithOutConfig(btnExtendOff, eStepFunctionType.ExtendOff)
    End Sub

#End Region

    ''' <summary>
    ''' 更新介面Enable/Disable
    ''' </summary>
    ''' <remarks></remarks>
    Sub RefreshUI()

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
            btnLoad.Enabled = True
            btnUnload.Enabled = True
            btnLoadB.Enabled = True
            btnUnloadB.Enabled = True
            btnPark.Enabled = True
            btnRun.Enabled = True
            btnCCDRun.Enabled = True
            btnDryRun.Enabled = True
            mnuLoadA.Enabled = True 'Soni 2017.01.20 介面操作保護
            mnuUnloadA.Enabled = True 'Soni 2017.01.20 介面操作保護
            mnuLoadB.Enabled = True 'Soni 2017.01.20 介面操作保護
            mnuUnloadB.Enabled = True 'Soni 2017.01.20 介面操作保護
            mnuPark.Enabled = True 'Soni 2017.01.20 介面操作保護
        Else
            btnLoad.Enabled = False
            btnUnload.Enabled = False
            btnLoadB.Enabled = False
            btnUnloadB.Enabled = False
            btnPark.Enabled = False
            btnRun.Enabled = False
            btnCCDRun.Enabled = False
            btnDryRun.Enabled = False
            mnuLoadA.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuUnloadA.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuLoadB.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuUnloadB.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuPark.Enabled = False 'Soni 2017.01.20 介面操作保護
        End If

        'Soni / 2016.08.20 增加按鍵保護判斷
        If gRecipeEdit.strFileName = "" Then '未讀檔
            btnSaveRecipe.Enabled = False
            mnuFileSave.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuFileSaveAs.Enabled = False 'Soni 2017.01.20 介面操作保護
            btnLock.Enabled = False
            mnuPatternRename.Enabled = False
            mnuRoundCopy.Enabled = False
            mnuRoundPaste.Enabled = False
            mnuStepAdd.Enabled = False
            gRecipeEdit.Editable = False
            mnuRunRun.Enabled = False
            mnuRunCCDRun.Enabled = False
            mnuRunDryRun.Enabled = False
            mnuRunPause.Enabled = False
        Else
            btnSaveRecipe.Enabled = True
            mnuFileSave.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuFileSaveAs.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            btnLock.Enabled = True
            mnuPatternRename.Enabled = gRecipeEdit.Editable
            mnuRoundCopy.Enabled = gRecipeEdit.Editable
            mnuRoundPaste.Enabled = gRecipeEdit.Editable
            mnuStepAdd.Enabled = gRecipeEdit.Editable
            mnuRunRun.Enabled = True
            mnuRunCCDRun.Enabled = True
            mnuRunDryRun.Enabled = True
            mnuRunPause.Enabled = True
        End If

        If gRecipeEdit.Editable Then
            btnLock.BackgroundImage = My.Resources.Unlock1
        Else
            btnLock.BackgroundImage = My.Resources.Lock
        End If
        btnPatternAdd.Enabled = gRecipeEdit.Editable
        mnuPatternAdd.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護


        If gRecipeEdit.Pattern.Count = 0 Then '無Pattern不能刪
            btnPatternDelete.Enabled = False
            mnuPatternDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
        Else
            btnPatternDelete.Enabled = gRecipeEdit.Editable
            mnuPatternDelete.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
        End If


        If SelectedNodePath = "" Or SelectedNodePath Is Nothing Or treePattern.SelectedNode Is Nothing Or SelectedNodePath.Length <= 4 Then
            btnAlign.Enabled = False
            btnInspect.Enabled = False
            btnArray.Enabled = False
            btnHeight.Enabled = False
            mnuNodeFIDs.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodeArray.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodeHeight.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodeCopy.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodePaste.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuMAP.Enabled = False 'Soni 2017.01.20 介面操作保護
            btnNodeMap.Enabled = False 'Soni 2017.01.20 介面操作保護
        ElseIf SelectedNodePath.Length <= 8 Then

            btnAlign.Enabled = False
            btnInspect.Enabled = False
            btnArray.Enabled = False
            btnHeight.Enabled = False
            mnuNodeFIDs.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodeArray.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodeHeight.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuNodeCopy.Enabled = False 'Soni 2017.01.20 介面操作保護
            If mNodeCopy.Array.Count = 0 Then
                mnuNodePaste.Enabled = False 'Soni 2017.01.20 介面操作保護
            End If
            mnuMAP.Enabled = False 'Soni 2017.01.20 介面操作保護
            btnNodeMap.Enabled = False 'Soni 2017.01.20 介面操作保護
        Else
            btnAlign.Enabled = gRecipeEdit.Editable
            btnInspect.Enabled = gRecipeEdit.Editable
            btnArray.Enabled = gRecipeEdit.Editable
            btnHeight.Enabled = gRecipeEdit.Editable
            mnuNodeFIDs.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuNodeArray.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuNodeHeight.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuNodeCopy.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuNodePaste.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuMAP.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            btnNodeMap.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
        End If

        If lstPatternID.SelectedIndex >= 0 Then
            '20170803 Toby
            'btnRoundAdd.Enabled = gRecipeEdit.Editable
            'btnRoundDelete.Enabled = gRecipeEdit.Editable
            'btnRoundDown.Enabled = gRecipeEdit.Editable
            'btnRoundUp.Enabled = gRecipeEdit.Editable
            mnuRoundAdd.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuRoundDelete.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuRoundMoveDown.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            mnuRoundMoveUp.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
            If gRecipeEdit.Pattern(lstPatternID.SelectedItem).Round.Count = 0 Then '無Round不能刪,當然也不能上下
                btnRoundDelete.Enabled = False
                btnRoundDown.Enabled = False
                btnRoundUp.Enabled = False
                mnuRoundDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
                mnuRoundMoveDown.Enabled = False 'Soni 2017.01.20 介面操作保護
                mnuRoundMoveUp.Enabled = False 'Soni 2017.01.20 介面操作保護
            End If
        Else
            btnRoundAdd.Enabled = False
            btnRoundDelete.Enabled = False
            btnRoundDown.Enabled = False
            btnRoundUp.Enabled = False
            mnuRoundAdd.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuRoundDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuRoundMoveDown.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuRoundMoveUp.Enabled = False 'Soni 2017.01.20 介面操作保護
        End If

        Dim mIsAllNodeEmpty As Boolean = True
        For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            If gRecipeEdit.Node(i).Count <> 0 Then '任一Stage不為空
                mIsAllNodeEmpty = False
            End If
        Next
        If mIsAllNodeEmpty = True Then
            btnNodeDelete.Enabled = False '都空的不能刪
            mnuNodeDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
        Else
            btnNodeDelete.Enabled = gRecipeEdit.Editable
            mnuNodeDelete.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
        End If
        If gRecipeEdit.Pattern.Count = 0 Then '無Pattern可按
            btnNodeAdd.Enabled = False
            mnuNodeAdd.Enabled = False 'Soni 2017.01.20 介面操作保護
        Else
            btnNodeAdd.Enabled = gRecipeEdit.Editable
            mnuNodeAdd.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
        End If

        '20170616_Start
        Dim treeNode As String
        If SelectedNodePath.Length > 8 Then
            treeNode = treePattern.SelectedNode.Text
            If lstRoundNo.SelectedIndex >= 0 Then '有選才能編輯
                RoundRefresh(lstRoundNo.SelectedIndex)
                grpStep.Enabled = gRecipeEdit.Editable
                btnStepBasicEdit.Enabled = gRecipeEdit.Editable
                mnuStepParameter.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
                If gRecipeEdit.Pattern(treeNode).Round(lstRoundNo.SelectedIndex).CStep.Count = 0 Then
                    btnStepDelete.Enabled = False
                    btnStepDown.Enabled = False
                    btnStepUp.Enabled = False
                    mnuStepDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
                    mnuStepMoveDown.Enabled = False 'Soni 2017.01.20 介面操作保護
                    mnuStepMoveUp.Enabled = False 'Soni 2017.01.20 介面操作保護
                    mnuStepCopy.Enabled = False 'Soni 2017.01.20 介面操作保護
                    mnuStepPaste.Enabled = False 'Soni 2017.01.20 介面操作保護
                Else
                    'btnStepDelete.Enabled = gRecipeEdit.Editable
                    'btnStepDown.Enabled = gRecipeEdit.Editable
                    'btnStepUp.Enabled = gRecipeEdit.Editable
                    mnuStepDelete.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
                    mnuStepMoveDown.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
                    mnuStepMoveUp.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
                    mnuStepCopy.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
                    mnuStepPaste.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
                End If
            Else
                dgvStep.Rows.Clear() 'Soni + 2017.04.25 無Round時,不顯示Step
                btnStepBasicEdit.Enabled = False
                mnuStepParameter.Enabled = False 'Soni 2017.01.20 介面操作保護
                btnStepDelete.Enabled = False
                btnStepDown.Enabled = False
                btnStepUp.Enabled = False
                mnuStepDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
                mnuStepMoveDown.Enabled = False 'Soni 2017.01.20 介面操作保護
                mnuStepMoveUp.Enabled = False 'Soni 2017.01.20 介面操作保護
                mnuStepCopy.Enabled = False 'Soni 2017.01.20 介面操作保護
                mnuStepPaste.Enabled = False 'Soni 2017.01.20 介面操作保護
                btnSelect.Enabled = False
                btnLine3D.Enabled = False
                btnCircle.Enabled = False
                btnWait.Enabled = False
                btnDots3D.Enabled = False
                btnArc.Enabled = False
                btnMove3D.Enabled = False
                btnExtendOn.Enabled = False
                btnExtendOff.Enabled = False
                btnContiStart.Enabled = False
                btnContiEnd.Enabled = False
                grpStep.Enabled = False
                '20170803 Toby
                btnRoundAdd.Enabled = False
                btnRoundDelete.Enabled = False
                btnStepDown.Enabled = False
                btnStepUp.Enabled = False
                mnuRoundAdd.Enabled = False
                mnuRoundDelete.Enabled = False
                mnuRoundMoveDown.Enabled = False
                mnuRoundMoveUp.Enabled = False
            End If
            mnuRotae.Enabled = gRecipeEdit.Editable
            '20171031 Toby
            mnuProcessTime.Enabled = gRecipeEdit.Editable
        Else
            lstRoundNo.Items.Clear() 'Soni + 2017.04.25 無Pattern時, 不顯示Round
            dgvStep.Rows.Clear() 'Soni + 2017.04.25 無Round時,不顯示Step
            btnStepBasicEdit.Enabled = False
            mnuStepParameter.Enabled = False 'Soni 2017.01.20 介面操作保護
            btnStepDelete.Enabled = False
            btnStepDown.Enabled = False
            btnStepUp.Enabled = False
            mnuStepDelete.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuStepMoveDown.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuStepMoveUp.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuStepCopy.Enabled = False 'Soni 2017.01.20 介面操作保護
            mnuStepPaste.Enabled = False 'Soni 2017.01.20 介面操作保護
            btnSelect.Enabled = False
            btnLine3D.Enabled = False
            btnCircle.Enabled = False
            btnWait.Enabled = False
            btnDots3D.Enabled = False
            btnArc.Enabled = False
            btnMove3D.Enabled = False
            btnExtendOn.Enabled = False
            btnExtendOff.Enabled = False
            btnContiStart.Enabled = False
            btnContiEnd.Enabled = False
            grpStep.Enabled = False
            mnuRotae.Enabled = False
            '20170803 Toby
            btnRoundAdd.Enabled = False
            btnRoundDelete.Enabled = False
            mnuRoundAdd.Enabled = False
            mnuRoundDelete.Enabled = False
            mnuRoundMoveDown.Enabled = False
            mnuRoundMoveUp.Enabled = False
            '20171031 Toby
            mnuProcessTime.Enabled = False
        End If

        btnSaveRecipe.Enabled = gRecipeEdit.Editable
        btnMachineABalance.Enabled = gRecipeEdit.Editable
        mnuBalance.Enabled = gRecipeEdit.Editable 'Soni 2017.01.20 介面操作保護
        btnMachineBBalance.Enabled = gRecipeEdit.Editable
        grpStep.Text = myResource.GetString("grpStep.Text") & gRecipeEdit.strName

        'Eason 20170113 Ticket:100011 , Add Pattern Copy Function [S]
        mnuPatternAdd.Enabled = gRecipeEdit.Editable
        mnuPatternCopy.Enabled = gRecipeEdit.Editable And lstPatternID.SelectedItem <> Nothing
        mnuPatternPaste.Enabled = gRecipeEdit.Editable And mPatternCopy.Name <> ""
        mnuPatternDelete.Enabled = gRecipeEdit.Editable
        'Eason 20170113 Ticket:100011 , Add Pattern Copy Function [E]


        'Toby  add_20170106
        If SelectedNodePath = "" Or SelectedNodePath Is Nothing Or treePattern.SelectedNode Is Nothing Or SelectedNodePath.Length <= 8 Then
            btnAlignUnlock.Enabled = False
            gRecipeEdit.Alignable = False
        Else
            'btnAlignUnlock.Enabled = False
            If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                If gRecipeEdit.Node(sys.StageNo).ContainsKey(SelectedNodePath) Then
                    If gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).AlignmentEnable And gRecipeEdit.Editable = True Then
                        btnAlignUnlock.Enabled = True
                        gRecipeEdit.Alignable = False
                        If AlignNodePath <> gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).NodePath Then
                            btnSelect.Enabled = False
                            btnLine3D.Enabled = False
                            btnCircle.Enabled = False
                            btnWait.Enabled = False
                            btnDots3D.Enabled = False
                            btnArc.Enabled = False
                            btnMove3D.Enabled = False
                            btnExtendOn.Enabled = False
                            btnExtendOff.Enabled = False
                            btnContiStart.Enabled = False
                            btnContiEnd.Enabled = False
                            btnStepDelete.Enabled = False
                            '20170803 Toby
                            btnRoundAdd.Enabled = False
                            btnRoundDelete.Enabled = False
                            btnStepDown.Enabled = False
                            btnStepUp.Enabled = False
                            btnRoundDown.Enabled = False
                            btnRoundUp.Enabled = False
                            mnuRoundAdd.Enabled = False
                            mnuRoundDelete.Enabled = False
                            mnuRoundMoveDown.Enabled = False
                            mnuRoundMoveUp.Enabled = False
                            mnuStepDelete.Enabled = False
                            mnuStepMoveDown.Enabled = False
                            mnuStepMoveUp.Enabled = False
                            mnuStepCopy.Enabled = False
                            mnuStepPaste.Enabled = False
                        Else
                            gRecipeEdit.Alignable = True
                            btnSelect.Enabled = True
                            btnLine3D.Enabled = True
                            btnCircle.Enabled = True
                            btnWait.Enabled = True
                            btnDots3D.Enabled = True
                            btnArc.Enabled = True
                            btnMove3D.Enabled = True
                            btnExtendOn.Enabled = True
                            btnExtendOff.Enabled = True
                            btnContiStart.Enabled = True
                            btnContiEnd.Enabled = True
                            btnStepDelete.Enabled = True
                            grpStep.Enabled = True
                            '20170803 Toby
                            btnRoundAdd.Enabled = True
                            btnRoundDelete.Enabled = True
                            btnStepDown.Enabled = True
                            btnStepUp.Enabled = True
                            btnRoundDown.Enabled = True
                            btnRoundUp.Enabled = True
                            mnuRoundAdd.Enabled = True
                            mnuRoundDelete.Enabled = True
                            mnuRoundMoveDown.Enabled = True
                            mnuRoundMoveUp.Enabled = True
                            mnuStepDelete.Enabled = True
                            mnuStepMoveDown.Enabled = True
                            mnuStepMoveUp.Enabled = True
                            mnuStepCopy.Enabled = True
                            mnuStepPaste.Enabled = True
                        End If
                    Else

                        gRecipeEdit.Alignable = gRecipeEdit.Editable
                        btnAlignUnlock.Enabled = False
                        btnSelect.Enabled = True
                        btnLine3D.Enabled = True
                        btnCircle.Enabled = True
                        btnWait.Enabled = True
                        btnDots3D.Enabled = True
                        btnArc.Enabled = True
                        btnMove3D.Enabled = True
                        btnExtendOn.Enabled = True
                        btnExtendOff.Enabled = True
                        btnContiStart.Enabled = True
                        btnContiEnd.Enabled = True
                        btnStepDelete.Enabled = True
                        grpStep.Enabled = True
                        '20170803 Toby
                        btnRoundAdd.Enabled = True
                        btnRoundDelete.Enabled = True
                        btnStepDown.Enabled = True
                        btnStepUp.Enabled = True
                        btnRoundDown.Enabled = True
                        btnRoundUp.Enabled = True
                        mnuRoundAdd.Enabled = True
                        mnuRoundDelete.Enabled = True
                        mnuRoundMoveDown.Enabled = True
                        mnuRoundMoveUp.Enabled = True
                        mnuStepDelete.Enabled = True
                        mnuStepMoveDown.Enabled = True
                        mnuStepMoveUp.Enabled = True
                        mnuStepCopy.Enabled = True
                        mnuStepPaste.Enabled = True
                    End If
                End If
            End If
        End If
        'Toby  add_20170106_End




        DrawPCSSingleGraphics()
    End Sub


#Region "檔案操作"

    ''' <summary>
    ''' Cycle Time檢查 僅雙閥同動檢查
    ''' </summary>
    ''' <remarks></remarks>
    Sub CheckCycleTime()

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                    Case eMechanismModule.TwoValveOneStage

                        Dim valveName1 As String = gRecipeEdit.StageParts(0).ValveName(0)
                        Dim valveName2 As String = gRecipeEdit.StageParts(0).ValveName(1)
                        Dim mValve1 As Decimal = GetTriggerCycleTime(valveName1)
                        Dim mValve2 As Decimal = GetTriggerCycleTime(valveName2)
                        If gSSystemParameter.MultiDispenseEnable = True Then '雙閥同動時
                            If valveName1 <> Nothing And valveName2 <> Nothing Then '如果兩閥都有選才比較
                                If mValve1 <> mValve2 Then '兩閥Cycle Time不同時
                                    MsgBox("Cycle Time Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                    valveName2 = "" '清掉一閥
                                End If
                            End If
                        End If

                End Select
        End Select
    End Sub

    ''' <summary>共用讀取Recipe方法</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadRecipe(ByVal fileName As String) As Boolean

        Dim mStageNo As Integer

        gRecipeEdit.strFileName = fileName
        StepClear()

        '[說明]:讀取Recipe
        If gRecipeEdit.ReadRecipe(fileName) = False Then
            gEqpMsg.AddHistoryAlarm("Error_1002020", "frmRecipe btnLoadRecipe", , gMsgHandler.GetMessage(Error_1002020), eMessageLevel.Error)  'Recipe Load Failed!
            Return False
        End If
        If Not Unit.A_Roller Is Nothing Then
            If (gRecipeEdit.ConveyorSpeed > 0) Then
                If Unit.A_Roller IsNot Nothing Then
                    If (Unit.A_Roller.SetSpeed(0, gRecipeEdit.ConveyorSpeed) <> True) Then
                        gEqpMsg.AddHistoryAlarm("Error_1009014", "frmRecipe btnLoadRecipe", , gMsgHandler.GetMessage(Error_1009014), eMessageLevel.Warning)  'Conveyor Roller 速度設定失敗!
                    End If
                End If
                If Unit.B_Roller IsNot Nothing Then
                    If (Unit.B_Roller.SetSpeed(0, gRecipeEdit.ConveyorSpeed) <> True) Then
                        gEqpMsg.AddHistoryAlarm("Error_1009014", "frmRecipe btnLoadRecipe", , gMsgHandler.GetMessage(Error_1009014), eMessageLevel.Warning)  'Conveyor Roller 速度設定失敗!
                    End If
                End If
            Else
                gEqpMsg.AddHistoryAlarm("Error_1009014", "frmRecipe btnLoadRecipe", , gMsgHandler.GetMessage(Error_1009014), eMessageLevel.Warning)  'Conveyor Roller 速度設定失敗!
            End If
        End If
        CheckCycleTime()
        'TODO: 補UIViewer顯示Recipe名稱
        If gVolumneControl.Load(fileName) = False Then '待刪除
            Return False
        End If
        If gRecipeEdit.LoadStageParts(fileName) = False Then
            Return False
        End If


        '[Note]:產生搜尋樹、產生拍照搜尋樹&定位&Laser&點膠搜尋樹
        gRecipeEdit.GenSearchTree()

        If gRecipeEdit.SearchTree.Count > 0 Then
            gRecipeEdit.ScanSort()
            gRecipeEdit.CCDFixSort()
            gRecipeEdit.LaserSort()
            gRecipeEdit.DispenseSort()
        End If

        For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
            '[Note]:膠材壽命是獨立存放的 目的是如果切換程式時,壽命計時是延續的
            If gSSystemParameter.StageParts.PasteLifeTime(mStageNo).Load("D:\PIIData\SysConfigStage" & (mStageNo + 1).ToString & ".ini") = False Then
                Return False
            End If
        Next
        gCRecipe = gRecipeEdit.Clone()
        gAOICollection.LoadRecipeScene() '因為gCRecipe還沒清掉, 先放這才有資料

        '[Note]:Update StageMap資料
        For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
            gRecipeEdit.Initial_StageMap(mStageNo, gSSystemParameter.IsBypassCCD)
        Next

        If gRecipeEdit.CheckPattern = False Then
            MsgBox("Recipe Pattern Fail !!! Please Check Pattern & Process Time", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
        Next


        Return True
    End Function

    ''' <summary>[更新介面顯示]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UILoad() As Boolean

        '[Note]:先保留GenSearchTree，原因是RefreshTreeView會出錯，在Add TreeView時加不進去，原因不明
        gRecipeEdit.GenSearchTree()

        '[Note]:顯示Pattern清單
        RefreshPatternEditor(0)
        RefreshTreeView(treePattern)
        If treePattern.Nodes.Count > 0 Then
            treePattern.SelectedNode = treePattern.Nodes(0)
        End If

        RefreshUI() '介面選項Enable與繪圖

        Return True
    End Function


    ''' <summary>開啟舊檔</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub btnLoadRecipe_Click(sender As Object, e As EventArgs) Handles btnLoadRecipe.Click, mnuFileOpen.Click

        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLoadRecipe]" & vbTab & "Click")
        btnLoadRecipe.Enabled = False

        Dim mProductName As String

        mProductName = "Default"

        '[說明]:檢查資料夾是否存在
        Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
        DefaultDirectory = Application.StartupPath & "\Recipe\"

        '[說明]:選取Recipe檔案
        With OFDLoadRecipe
            .InitialDirectory = DefaultDirectory
            .Filter = "文字檔 (*.rcp)|*.rcp"
            .FilterIndex = 2
            .RestoreDirectory = True
            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                btnLoadRecipe.Enabled = True
                btnLock.Enabled = True
                Exit Sub
            End If
            LoadRecipe(.FileName) '檔案讀取
            UILoad()
        End With

        btnLoadRecipe.Enabled = True
        btnLock.Enabled = True

    End Sub


    ''' <summary>開新檔案</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCreateRecipe_Click(sender As Object, e As EventArgs) Handles btnCreateRecipe.Click, mnuFileCreate.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCreateRecipe]" & vbTab & "Click")
        btnCreateRecipe.Enabled = False
        With SFDSaveRecipe
            .FileName = "Untitled"
            .InitialDirectory = Application.StartupPath & "\Recipe\"
            .Filter = "文字檔 (*.rcp)|*.rcp"
            .FilterIndex = 2
            .RestoreDirectory = True
        End With
        If SFDSaveRecipe.ShowDialog() <> Windows.Forms.DialogResult.OK Then '放棄確認
            btnCreateRecipe.Enabled = True
            Exit Sub
        End If
        lstPatternID.Items.Clear()
        lstRoundNo.Items.Clear()
        dgvStep.Rows.Clear()

        Dim newfileName As String = SFDSaveRecipe.FileName
        File.Delete(SFDSaveRecipe.FileName)

        LoadRecipe(SFDSaveRecipe.FileName)
        UILoad()

        '新增recipe 產生預設值_20170620 Toby add_Start
        If IsNothing(gRecipeEdit) = False Then
            Select Case gSSystemParameter.MeasureType
                Case enmMeasureType.Contact
                    gRecipeEdit.LaserFixMode = eHeightModel.Contact
                Case enmMeasureType.Laser
                    gRecipeEdit.LaserFixMode = eHeightModel.Laser_NonOnFly
                Case enmMeasureType.Both ' 若系統設定為both 也先預設為Laser_NonOnFly
                    gRecipeEdit.LaserFixMode = eHeightModel.Laser_NonOnFly
            End Select
        End If
        '新增recipe 產生預設值_20170620 Toby add_End

        SelectedNodePath = "" '清除選擇項目
        btnCreateRecipe.Enabled = True
        btnLock.Enabled = True
    End Sub

    '20160830
    ''' <summary>複製檔案</summary>
    ''' <param name="sourceFile">來源檔案</param>
    ''' <param name="destFile">目標檔案</param>
    ''' <param name="extName">副檔名</param>
    ''' <remarks></remarks>
    Public Sub CopyFile(ByVal sourceFile As String, ByVal destFile As String, ByVal extName As String)
        '[說明]:確認檔案有無存在,存在就砍檔再複製檔案
        If (sourceFile & extName) <> (destFile & extName) Then '來源與目的不同
            If File.Exists(destFile & extName) Then '目標檔案存在
                '[說明]:
                'Soni - 2016.09.16 去除刪除目標檔案 懷疑目標檔案存在,來源檔案被搬走. 目標檔案刪除..!!
                '20160901
                If File.Exists(sourceFile & extName) Then '如果來源檔案存在
                    File.Copy(sourceFile & extName, destFile & extName, True)
                End If
            Else
                '20160901
                If File.Exists(sourceFile & extName) Then '
                    File.Copy(sourceFile & extName, destFile & extName, True)
                End If
            End If
        End If
    End Sub

    ''' <summary>另存新檔</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSaveRecipe_Click(sender As Object, e As EventArgs) Handles btnSaveRecipe.Click, mnuFileSaveAs.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnSaveRecipe]" & vbTab & "Click")
        btnSaveRecipe.Enabled = False
        '[說明]:檢查資料夾是否存在
        If System.IO.Directory.Exists(Application.StartupPath & "\Recipe\") = False Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\Recipe\")
        End If

        '[說明]:選擇存取Recipe的檔案
        With SFDSaveRecipe

            .Filter = "文字檔 (*.rcp)|*.rcp"
            .FilterIndex = 2
            .RestoreDirectory = True
            If gRecipeEdit.strFileName <> "" Then '如果已開檔案
                .FileName = gRecipeEdit.strFileName '預設檔名
                Dim fInfo As New FileInfo(.FileName)
                .InitialDirectory = fInfo.Directory.ToString
            Else
                .InitialDirectory = Application.StartupPath & "\Recipe\"
            End If

            If .ShowDialog = Windows.Forms.DialogResult.OK Then

                '[說明]:Copy .ini .Vpp   '20160830
                Dim CopyName As String '目標檔名
                Dim StrName As String '來源檔名
                For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1 ' gSSystemParameter.StageMax - 1 Soni / 2016.08.30 修正 StageMax不是這個用途, 有機會跳錯誤.
                    Dim mNodeID As String
                    For j As Integer = 0 To gRecipeEdit.Node(i).Keys.Count - 1
                        mNodeID = gRecipeEdit.Node(i).Keys(j)
                        For mConveyorNo As Integer = 0 To 1
                            Select Case gRecipeEdit.Node(i)(mNodeID).AlignType
                                Case enmAlignType.DevicePos1
                                    StrName = Application.StartupPath & "\Recipe\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene 'Soni / 2016.09.13 修正來源檔名錯誤
                                    CopyName = System.IO.Path.GetDirectoryName(.FileName) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene
                                    CopyFile(StrName, CopyName, ".ini")
                                    CopyFile(StrName, CopyName, ".vpp") 'Soni / 2016.09.13 修正副檔名錯誤 Vpp->vpp
                                Case enmAlignType.DevicePos2
                                    StrName = Application.StartupPath & "\Recipe\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene 'Soni / 2016.09.13 修正來源檔名錯誤
                                    CopyName = System.IO.Path.GetDirectoryName(.FileName) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene
                                    CopyFile(StrName, CopyName, ".ini")
                                    CopyFile(StrName, CopyName, ".vpp") 'Soni / 2016.09.13 修正副檔名錯誤 Vpp->vpp

                                    StrName = Application.StartupPath & "\Recipe\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene 'Soni / 2016.09.13 修正來源檔名錯誤
                                    CopyName = System.IO.Path.GetDirectoryName(.FileName) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene
                                    CopyFile(StrName, CopyName, ".ini")
                                    CopyFile(StrName, CopyName, ".vpp") 'Soni / 2016.09.13 修正副檔名錯誤 Vpp->vpp
                                Case enmAlignType.DevicePos3
                                    StrName = Application.StartupPath & "\Recipe\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene 'Soni / 2016.09.13 修正來源檔名錯誤
                                    CopyName = System.IO.Path.GetDirectoryName(.FileName) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene
                                    CopyFile(StrName, CopyName, ".ini")
                                    CopyFile(StrName, CopyName, ".vpp") 'Soni / 2016.09.13 修正副檔名錯誤 Vpp->vpp

                                    StrName = Application.StartupPath & "\Recipe\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene 'Soni / 2016.09.13 修正來源檔名錯誤
                                    CopyName = System.IO.Path.GetDirectoryName(.FileName) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene
                                    CopyFile(StrName, CopyName, ".ini")
                                    CopyFile(StrName, CopyName, ".vpp") 'Soni / 2016.09.13 修正副檔名錯誤 Vpp->vpp

                                    StrName = Application.StartupPath & "\Recipe\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene 'Soni / 2016.09.13 修正來源檔名錯誤
                                    CopyName = System.IO.Path.GetDirectoryName(.FileName) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene
                                    CopyFile(StrName, CopyName, ".ini")
                                    CopyFile(StrName, CopyName, ".vpp") 'Soni / 2016.09.13 修正副檔名錯誤 Vpp->vpp
                            End Select
                        Next

                    Next
                Next

                grpStep.Text = myResource.GetString("grpStep.Text") & System.IO.Path.GetFileName(.FileName)
                If File.Exists(.FileName) Then
                    File.Delete(.FileName)
                End If
                '[說明]:存取Recipe
                If gRecipeEdit.SaveRecipe(.FileName) = False Then
                    gEqpMsg.AddHistoryAlarm("Error_1002021", "frmRecipe btnSaveAsRecipe", , gMsgHandler.GetMessage(Error_1002021), eMessageLevel.Error)  'Save File Fail !
                End If

                '[說明]:讀取Recipe
                If gCRecipe.ReadRecipe(.FileName) = True Then
                    '[Note]:產生搜尋樹、產生拍照搜尋樹&定位&Laser&點膠搜尋樹
                    If gRecipeEdit.SearchTree.Count > 0 Then
                        gRecipeEdit.ScanSort()
                        gRecipeEdit.CCDFixSort()
                        gRecipeEdit.LaserSort()
                        gRecipeEdit.DispenseSort()
                    End If

                    '[Note]:Update StageMap資料
                    For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
                        gRecipeEdit.Initial_StageMap(mStageNo, gSSystemParameter.IsBypassCCD)
                    Next

                    If gRecipeEdit.CheckPattern = False Then
                        MsgBox("Recipe Pattern Fail !!! Please Check Pattern & Process Time", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If

                    For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                        gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
                    Next

                Else
                    gEqpMsg.AddHistoryAlarm("Error_1002020", "frmRecipe btnSaveAsRecipe", , gMsgHandler.GetMessage(Error_1002020), eMessageLevel.Error)  'Load File Fail !
                End If
            End If
        End With
        btnSaveRecipe.Enabled = True
    End Sub

    ''' <summary>儲存檔案</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuFileSave_Click(sender As Object, e As EventArgs) Handles mnuFileSave.Click
        '[說明]:存取Recipe
        If gRecipeEdit.SaveRecipe(gRecipeEdit.strFileName) = False Then
            gEqpMsg.AddHistoryAlarm("Error_1002021", "frmRecipe btnSaveAsRecipe", , gMsgHandler.GetMessage(Error_1002021), eMessageLevel.Error)  'Save File Fail !
        End If

        '[說明]:讀取Recipe
        If gRecipeEdit.ReadRecipe(gRecipeEdit.strFileName) = True Then

        Else
            gEqpMsg.AddHistoryAlarm("Error_1002020", "frmRecipe btnSaveAsRecipe", , gMsgHandler.GetMessage(Error_1002020), eMessageLevel.Error)  'Load File Fail !
        End If
    End Sub

    ''' <summary>編輯保護</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLock_Click(sender As Object, e As EventArgs) Handles btnLock.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLock]" & vbTab & "Click")
        If btnLock.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnLock.Enabled = False
        gRecipeEdit.Editable = Not gRecipeEdit.Editable
        If gRecipeEdit.Editable Then
            gSyslog.Save("Recipe Editable: " & gRecipeEdit.strFileName)
        Else
            gSyslog.Save("Recipe Lock: " & gRecipeEdit.strFileName)
        End If

        RefreshUI()
        btnLock.Enabled = True
    End Sub

#End Region

    ''' <summary>陣列設定</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnArray_Click(sender As Object, e As EventArgs) Handles btnArray.Click, mnuNodeArray.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArray]" & vbTab & "Click")
        If btnArray.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnArray.Enabled = False
        If lstPatternID.SelectedIndex < 0 Then
            If lstPatternID.Items.Count = 0 Then
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            btnArray.Enabled = True

            Exit Sub
        End If
        If SelectedNodePath = "" Then
            If treePattern.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.Nodes(0)
            End If

            btnArray.Enabled = True
            tabView.SelectTab(TabPage2)
            Exit Sub
        End If

        If treePattern.SelectedNode Is Nothing Then '沒選則預選
            If treePattern.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            End If

            btnArray.Enabled = True
            Exit Sub
        End If

        If SelectedNodePath.Length <= 8 Then
            tabView.SelectTab(TabPage2)

            If treePattern.SelectedNode.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.SelectedNode.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            Else
                btnNodeAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnNodeAdd.BackColor = SystemColors.Control
                btnNodeAdd.UseVisualStyleBackColor = True
            End If
            btnArray.Enabled = True
            Exit Sub
        End If

        Dim StageNo As Integer = CInt(SelectedNodePath.Split(",")(1))


        If Not gRecipeEdit.Node(StageNo).ContainsKey(SelectedNodePath) Then
            'Recipe錯誤,請重新建立檔案
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
            MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnArray.Enabled = True
            Exit Sub
        End If

        Dim mFrmRecipe02 As New frmRecipe02
        With mFrmRecipe02
            Select Case SelectedNodePath.Substring(2, 1)
                Case "0"
                    .sys = gSYS(eSys.DispStage1)
                Case "1"
                    .sys = gSYS(eSys.DispStage2)
                Case "2"
                    .sys = gSYS(eSys.DispStage3)
                Case "3"
                    .sys = gSYS(eSys.DispStage4)
            End Select
            .RecipeEdit = gRecipeEdit
            .NodeID = SelectedNodePath
            .WindowState = FormWindowState.Maximized
            .PatternID = treePattern.SelectedNode.Text
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        RefreshPatternEditor(lstPatternID.SelectedIndex)
        btnArray.Enabled = True
    End Sub

    Function IsCanSetNode() As Boolean
        '=== Pattern未選例外 ===
        If lstPatternID.SelectedIndex < 0 Then
            If lstPatternID.Items.Count = 0 Then '沒Pattern則Add鍵提示
                btnPatternAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnPatternAdd.BackColor = SystemColors.Control
                btnPatternAdd.UseVisualStyleBackColor = True
            Else '有Pattern 則選單提示
                lstPatternID.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                lstPatternID.BackColor = Color.White
            End If
            Return False
        End If
        '=== Pattern未選例外 ===

        '=== 節點未選例外 ===
        If treePattern.SelectedNode Is Nothing Then '沒選
            If treePattern.Nodes.Count > 0 Then '有節點則代選根節點
                treePattern.SelectedNode = treePattern.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            End If
            Return False
        End If
        '=== 節點未選例外 ===

        '=== 已選節點路徑為空 ===
        If SelectedNodePath = "" OrElse SelectedNodePath Is Nothing Then
            If treePattern.Nodes.Count > 0 Then '有節點則代選根節點
                treePattern.SelectedNode = treePattern.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            End If
            tabView.SelectTab(TabPage2) '代切TreePath頁籤
            Return False
        End If
        '=== 已選節點路徑為空 ===

        '=== 已選節點名稱異常 ===
        If Not SelectedNodePath.Contains(",") Then
            If treePattern.Nodes.Count > 0 Then '有節點則代選根節點
                treePattern.SelectedNode = treePattern.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            End If
            Return False
        End If
        '=== 已選節點名稱異常 ===

        '=== 已選節點非可設定節點 ===
        If SelectedNodePath.Length <= 8 Then
            tabView.SelectTab(TabPage2) '代切TreePath頁籤
            If treePattern.SelectedNode.Nodes.Count > 0 Then '已選節點有下層可用, 代選下層第一個節點
                treePattern.SelectedNode = treePattern.SelectedNode.Nodes(0)
                treePattern.SelectedNode.BackColor = Color.Red
                System.Threading.Thread.CurrentThread.Join(300)
                treePattern.SelectedNode.BackColor = Color.White
                treePattern.SelectedNode.ForeColor = Color.Black
            Else '已選節點無下一層可用 提示新增節點
                btnNodeAdd.BackColor = Color.Green
                System.Threading.Thread.CurrentThread.Join(300)
                btnNodeAdd.BackColor = SystemColors.Control
                btnNodeAdd.UseVisualStyleBackColor = True
            End If
            Return False
        End If
        '=== 已選節點非可設定節點 ===

        '取得平台名稱 
        Dim StageNo As Integer = CInt(SelectedNodePath.Split(",")(1))

        '=== Recipe內無已選節點名稱 ===
        If Not gRecipeEdit.Node(StageNo).ContainsKey(SelectedNodePath) Then
            '請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        '=== Recipe內無已選節點名稱 ===
        Return True
    End Function
    ''' <summary>定位教導</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAlign_Click(sender As Object, e As EventArgs) Handles btnAlign.Click, mnuNodeFIDs.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnAlign]" & vbTab & "Click")
        If btnAlign.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnAlign.Enabled = False

        If IsCanSetNode() = False Then
            btnAlign.Enabled = True
            Exit Sub
        End If

        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.Loading Then
            '場景載入中，請稍後
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000030))
            MsgBox(gMsgHandler.GetMessage(Warn_3000030), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnAlign.Enabled = True
            Exit Sub
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.NG Then
            '場景載入失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000038))
            MsgBox(gMsgHandler.GetMessage(Warn_3000038), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnAlign.Enabled = True
        End If

        Dim mFrmRecipe01 As New frmRecipe01
        With mFrmRecipe01
            Select Case SelectedNodePath.Substring(2, 1)
                Case "0"
                    .sys = gSYS(eSys.DispStage1)
                Case "1"
                    .sys = gSYS(eSys.DispStage2)
                Case "2"
                    .sys = gSYS(eSys.DispStage3)
                Case "3"
                    .sys = gSYS(eSys.DispStage4)
            End Select
            .RecipeEdit = gRecipeEdit
            .NodeID = SelectedNodePath
            .WindowState = FormWindowState.Maximized
            .Location = New Point(0, 0)
            .StartPosition = FormStartPosition.Manual
            .ShowDialog()
            Me.RefreshUI()
            .Dispose()
        End With
        RefreshPatternEditor(lstPatternID.SelectedIndex)
        btnAlign.Enabled = True
    End Sub

    ''' <summary>測高設定</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHeight_Click(sender As Object, e As EventArgs) Handles btnHeight.Click, mnuNodeHeight.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnHeight]" & vbTab & "Click")
        If btnHeight.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnHeight.Enabled = False

        If IsCanSetNode() = False Then
            btnHeight.Enabled = True
            Exit Sub
        End If

        Dim StageNo As Integer = CInt(SelectedNodePath.Split(",")(1))
        Dim mFrmRecipe03 As New frmRecipe03
        'gfrmRecipe03 = New frmRecipe03
        With mFrmRecipe03
            Select Case StageNo
                Case 0
                    .sys = gSYS(eSys.DispStage1)
                Case 1
                    .sys = gSYS(eSys.DispStage2)
                Case 2
                    .sys = gSYS(eSys.DispStage3)
                Case 3
                    .sys = gSYS(eSys.DispStage4)
            End Select
            .RecipeEdit = gRecipeEdit
            .NodeID = SelectedNodePath 'treePattern.SelectedNode.FullPath 'treePattern.SelectedNode.Text
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        RefreshPatternEditor(lstPatternID.SelectedIndex)
        btnHeight.Enabled = True
    End Sub

    ''' <summary>檢測教導</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnInspect_Click(sender As Object, e As EventArgs) Handles btnInspect.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnInspect]" & vbTab & "Click")
        If btnInspect.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnInspect.Enabled = False
        If IsCanSetNode() = False Then
            btnInspect.Enabled = True
            Exit Sub
        End If
        btnInspect.Enabled = True
    End Sub

    ''' <summary>停駐點</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPark_Click(sender As Object, e As EventArgs) Handles btnPark.Click, mnuPark.Click
        gSyslog.Save("[frmOpStatus]" & vbTab & "[btnChangeGluePos]" & vbTab & "Click")
        If btnPark.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnPark.Enabled = False
        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnMachineABalance.Enabled = False
        btnMachineBBalance.Enabled = False
        btnLoad.Enabled = False
        btnLoadB.Enabled = False
        btnUnload.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        Dim strBuffer As String

        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
            btnPark.Enabled = True
            Exit Sub
        End If

        If gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            If gSYS(eSys.OverAll).ExternalPause = False Then '增加外部暫停判斷
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
                btnPark.Enabled = True
                '20170602按鍵保護
                btnAlignUnlock.Enabled = True
                btnMachineABalance.Enabled = True
                btnMachineBBalance.Enabled = True
                btnLoad.Enabled = True
                btnLoadB.Enabled = True
                btnUnload.Enabled = True
                btnUnloadB.Enabled = True
                btnRun.Enabled = True
                btnPause.Enabled = True
                btnCCDRun.Enabled = True
                btnDryRun.Enabled = True
                btnBack.Enabled = True
                btnAlign.Enabled = True
                btnArray.Enabled = True
                btnHeight.Enabled = True
                btnAlignUnlock.Enabled = True
                btnInspect.Enabled = True

                Exit Sub
            End If
        End If
        Dim mSelectedSysStage As Integer = eSys.DispStage1
        If SelectedNodePath Is Nothing Or SelectedNodePath = "" Or SelectedNodePath.Length <= 2 Then
            '請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnPark.Enabled = True
            '20170602按鍵保護
            btnAlignUnlock.Enabled = True
            btnMachineABalance.Enabled = True
            btnMachineBBalance.Enabled = True
            btnLoad.Enabled = True
            btnLoadB.Enabled = True
            btnUnload.Enabled = True
            btnUnloadB.Enabled = True
            btnRun.Enabled = True
            btnPause.Enabled = True
            btnCCDRun.Enabled = True
            btnDryRun.Enabled = True
            btnBack.Enabled = True
            btnAlign.Enabled = True
            btnArray.Enabled = True
            btnHeight.Enabled = True
            btnAlignUnlock.Enabled = True
            btnInspect.Enabled = True

            Exit Sub
        End If

        Select Case SelectedNodePath.Substring(2, 1)
            Case "0"
                mSelectedSysStage = eSys.SubDisp1
            Case "1"
                mSelectedSysStage = eSys.SubDisp2
            Case "2"
                mSelectedSysStage = eSys.SubDisp3
            Case "3"
                mSelectedSysStage = eSys.SubDisp4
        End Select

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做ChangeGlue
        If gSYS(mSelectedSysStage).RunStatus = enmRunStatus.Running Then '動作中
            If gSYS(mSelectedSysStage).ExternalPause = False Then '增加外部暫停判斷
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
                btnPark.Enabled = True
                '20170602按鍵保護
                btnAlignUnlock.Enabled = True
                btnMachineABalance.Enabled = True
                btnMachineBBalance.Enabled = True
                btnLoad.Enabled = True
                btnLoadB.Enabled = True
                btnUnload.Enabled = True
                btnUnloadB.Enabled = True
                btnRun.Enabled = True
                btnPause.Enabled = True
                btnCCDRun.Enabled = True
                btnDryRun.Enabled = True
                btnBack.Enabled = True
                btnAlign.Enabled = True
                btnArray.Enabled = True
                btnHeight.Enabled = True
                btnAlignUnlock.Enabled = True
                btnInspect.Enabled = True

                Exit Sub
            End If
        End If

        btnPark.Enabled = False
        'If gblnChangeGlueComeBack = True Then
        strBuffer = "[Jetting Controller]-->[Menu]-->[Purge]-->[Valve]-->[Start]"
        'Else
        'strBuffer = "[Jetting Controller]-->[Menu]-->[Purge]-->[Valve]-->[Stop]"
        'End If

        If MsgBox(strBuffer, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "閥座拆裝時，請參照下述步驟調整控制器") = MsgBoxResult.Yes Then
            btnPark.BackColor = SystemColors.Control
            btnPark.UseVisualStyleBackColor = True
            gSYS(mSelectedSysStage).Command = eSysCommand.ChangeGlue
            gblnUpdateChangeGlue = True
            btnPark.Enabled = True
            '20170602按鍵保護
            btnAlignUnlock.Enabled = True
            btnMachineABalance.Enabled = True
            btnMachineBBalance.Enabled = True
            btnLoad.Enabled = True
            btnLoadB.Enabled = True
            btnUnload.Enabled = True
            btnUnloadB.Enabled = True
            btnRun.Enabled = True
            btnPause.Enabled = True
            btnCCDRun.Enabled = True
            btnDryRun.Enabled = True
            btnBack.Enabled = True
            btnAlign.Enabled = True
            btnArray.Enabled = True
            btnHeight.Enabled = True
            btnAlignUnlock.Enabled = True
            btnInspect.Enabled = True
        Else
            btnPark.Enabled = True
            '20170602按鍵保護
            btnAlignUnlock.Enabled = True
            btnMachineABalance.Enabled = True
            btnMachineBBalance.Enabled = True
            btnLoad.Enabled = True
            btnLoadB.Enabled = True
            btnUnload.Enabled = True
            btnUnloadB.Enabled = True
            btnRun.Enabled = True
            btnPause.Enabled = True
            btnCCDRun.Enabled = True
            btnDryRun.Enabled = True
            btnBack.Enabled = True
            btnAlign.Enabled = True
            btnArray.Enabled = True
            btnHeight.Enabled = True
            btnAlignUnlock.Enabled = True
            btnInspect.Enabled = True
        End If
    End Sub

#Region "彈性流程"

    ''' <summary>更新樹狀圖</summary>
    ''' <param name="treeView"></param>
    ''' <remarks></remarks>
    Public Sub RefreshTreeView(ByRef treeView As TreeView)
        Dim mTreeNode As TreeNode

        treeView.Nodes.Clear()
        'treeView.Nodes
        For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            If gRecipeEdit.SearchTree.Count > mStageNo Then
                mTreeNode = gRecipeEdit.SearchTree(mStageNo)
                treeView.Nodes.Add(mTreeNode) '顯示搜尋樹
            End If
        Next
    End Sub

    Private Sub treePattern_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treePattern.AfterSelect

        Dim mPatternID As String = treePattern.SelectedNode.Text
        Dim TempPath() As String
        Dim i As Integer
        Dim tempLength As Integer

        If mPatternID.StartsWith("MainBody(") And mPatternID.EndsWith(")") Then '起始Pattern
            mPatternID = mPatternID.Substring(9).TrimEnd(")")
        End If
        If Not lstPatternID.Items.Contains(mPatternID) And e.Node.Level > 1 Then '如果不包含則離開
            Exit Sub
        End If
        '20170616_Start
        'lstPatternID.SelectedItem = treePattern.SelectedNode.Text
        '20170616_End
        '判斷選擇的節點位置
        If e.Node.Level > 0 Then
            Dim temp As TreeNode = e.Node
            Dim level As Integer = temp.Level
            Dim index As Integer = temp.Index
            Dim str As String = level.ToString() & "," & index.ToString() & "," '先層,後索引
            '找出樹狀路徑
            While level <> 0 '往前串接到最頂
                str = temp.Parent.Level.ToString() & "," & temp.Parent.Index.ToString() & "," & str
                level -= 1
                temp = temp.Parent
            End While
            '將樹狀路徑寫入
            SelectedNodePath = str
            'Debug.Print("treePattern_AfterSelect:" & str)
        Else
            '將樹狀路徑寫入
            SelectedNodePath = e.Node.TreeView.SelectedNode.Level.ToString() & "," & e.Node.TreeView.SelectedNode.Index.ToString()
            'Debug.Print("treePattern_AfterSelect:" & e.Node.TreeView.SelectedNode.Index.ToString() & "," & e.Node.TreeView.SelectedNode.Level.ToString())
        End If
        '拆分路徑為層數與索引值
        TempPath = SelectedNodePath.Split(",")
        tempLength = CInt(TempPath.Length)
        tempLength = (tempLength \ 2)
        tempLength -= 1
        ReDim LastTreePath(TreePath.Length - 1)
        LastTreePath = TreePath
        ReDim TreePath(tempLength)
        For i = 0 To (tempLength)
            TreePath(i) = TempPath(i * 2 + 1)
            'TreePath(i) = TempPath((tempLength - i) * 2)
        Next

        If IsNothing(PreNode) = False Then
            PreNode.BackColor = treePattern.BackColor
            PreNode.ForeColor = treePattern.ForeColor
        End If
        e.Node.BackColor = SystemColors.Highlight
        e.Node.ForeColor = Color.White
        PreNode = treePattern.SelectedNode
        Dim mStageNo As Integer = CInt(TempPath(1))



        Dim CCDNo As Integer = CInt(mStageNo)
        Select Case SelectedNodePath.Substring(2, 1)
            Case "0"
                sys = gSYS(eSys.DispStage1)
            Case "1"
                sys = gSYS(eSys.DispStage2)
            Case "2"
                sys = gSYS(eSys.DispStage3)
            Case "3"
                sys = gSYS(eSys.DispStage4)
        End Select
        If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
            gEqpMsg.Add("Error_1012002", Error_1012002, eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1012002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

        If Not gRecipeEdit.Node(mStageNo).ContainsKey(SelectedNodePath) Then
            '20170616_Start
            RefreshUI()
            '20170616_End
            Exit Sub
        End If
        chkNodeConnect.Checked = gRecipeEdit.Node(mStageNo)(SelectedNodePath).IsNodeConnect
        If SelectedNodePath.Length > 8 Then '0,0,1,0, 八個字元
            '2017061_Start
            Dim TreeNode As String
            TreeNode = treePattern.SelectedNode.Text
            lstRoundNo.Items.Clear()
            For j As Integer = 0 To gRecipeEdit.Pattern(TreeNode).Round.Count - 1
                lstRoundNo.Items.Add((j + 1).ToString)
            Next
            lstRoundNo.SelectedIndex = lstRoundNo.Items.Count - 1

            '20170616_End
            If gRecipeEdit.Node(mStageNo)(SelectedNodePath).AlignmentEnable = True Then
                If gRecipeEdit.Node(mStageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData.Count > 0 Then
                    Dim mScene As String = gRecipeEdit.Node(TempPath(1))(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene
                    SelectScene(mScene)

                End If
            End If
        End If
        DrawPCSSingleGraphics()
        RefreshUI()

    End Sub

    Sub SelectScene(ByVal sceneName As String)
        If sceneName Is Nothing Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(sceneName) Then
            Dim light As enmLight
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No1), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No1))
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No2), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No2))
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No3), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No3))
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No4), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No4))
        Else
            '場景不存在
            gSyslog.Save(sceneName & gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(sceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    ''' <summary>
    ''' [TreeView所選擇的路徑]
    ''' </summary>
    ''' <remarks></remarks>
    Public SelectedNodePath As String = ""
    ''' <summary>
    ''' [拆解後的Tree路徑，資料為{索引值}，層數為個數]
    ''' </summary>
    ''' <remarks></remarks>
    Private TreePath(0) As Integer
    ''' <summary>
    ''' [上一次所選擇的Tree路徑，資料為{索引值}，層數為個數]
    ''' </summary>
    ''' <remarks></remarks>
    Private LastTreePath() As Integer

    Private PreNode As TreeNode


    ''' <summary>
    ''' [找到所選的Pattern並新增節點至PatternList]
    ''' </summary>
    ''' <param name="Path">[輸入選擇的路徑]</param>
    ''' <param name="FirstNode">[輸入最上層節點Pattern]</param>
    ''' <param name="Name">[輸入新增名稱]</param>
    ''' <remarks></remarks>
    Public Sub SelectTreeNodeAdd(ByVal Path() As Integer, ByRef FirstNode As CRecipePattern, ByVal Name As String)
        Dim i As Integer
        Dim tempPattern As CRecipePattern
        Dim NewPattern As New CRecipePattern(Name)
        Dim NodeNewData As NodeOffset
        Dim treenumber As Integer = 0
        Dim usenumber As Integer = 0
        Dim tempNowTreeNode As New CRecipePattern
        Dim tempLastTreeNode As New List(Of CRecipePattern)
        '找到所選的Pattern
        tempPattern = FirstNode
        For i = 1 To Path.GetLength(0) - 1
            tempPattern = tempPattern.Nodes(Path(i))
        Next
        NodeNewData.NodeName = NewPattern.Name
        tempPattern.Nodes.Add(NewPattern)
        'tempPattern.NodeData.Add(NodeNewData)
        '切開PatternList之間的連結
        For i = 0 To PatternList.Count - 1
            PatternList(i) = PatternList(i).Clone()
        Next
        '找到PatternList中的項目
        For i = 0 To PatternList.Count - 1
            tempNowTreeNode = PatternList(i)
            '檢查單層是否有相同名稱之Pattern
            If tempNowTreeNode.Name = tempPattern.Name Then
                tempNowTreeNode.Nodes.Add(NewPattern)
            End If
            '保護只有一層
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '層資料讀取
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(j))
                            '更新既有項目
                            If tempLastTreeNode(treenumber).Name = tempPattern.Name Then '
                                tempLastTreeNode(treenumber).Nodes.Add(NewPattern)
                            End If
                            treenumber += 1
                        Next
                    End If
                    tempNowTreeNode = tempLastTreeNode(usenumber)
                    usenumber += 1
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If

        Next i
        '將Pattern更新回去
        For i = Path.GetLength(0) - 1 To 1 Step -1
            tempPattern = tempPattern.Parent
        Next
        FirstNode = tempPattern

    End Sub

    ''' <summary>
    ''' [找到所選的Pattern並新增節點至PatternList]
    ''' </summary>
    ''' <param name="Path">[輸入選擇的路徑]</param>
    ''' <param name="FirstNode">[輸入最上層節點Pattern]</param>
    ''' <param name="NewNode">[輸入新增節點]</param>
    ''' <remarks></remarks>
    Public Sub SelectTreeNodeAdd(ByVal Path() As Integer, ByRef FirstNode As CRecipePattern, ByVal NewNode As CRecipePattern)
        Dim i As Integer
        Dim tempPattern As CRecipePattern
        Dim NewPattern As CRecipePattern = NewNode
        Dim NodeNewData As NodeOffset
        Dim tempNumber As Integer = 0
        Dim treenumber As Integer = 0
        Dim usenumber As Integer = 0
        Dim tempNowTreeNode As New CRecipePattern
        Dim tempLastTreeNode As New List(Of CRecipePattern)
        '找到所選的Pattern
        tempPattern = FirstNode.Clone()
        '保護選到相同Pattern
        If tempPattern.Name = NewNode.Name Then
            '檔案名稱已存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000046))
            MsgBox(gMsgHandler.GetMessage(Warn_3000046), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        For i = 1 To Path.GetLength(0) - 1
            tempPattern = tempPattern.Nodes(Path(i))
            '保護選到相同Pattern
            If tempPattern.Name = NewNode.Name Then
                '檔案名稱已存在
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000046))
                MsgBox(gMsgHandler.GetMessage(Warn_3000046), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
        Next
        NodeNewData.NodeName = NewPattern.Name
        tempPattern.Nodes.Add(NewNode)
        '切開PatternList之間的連結
        For i = 0 To PatternList.Count - 1
            PatternList(i) = PatternList(i).Clone()
        Next
        '找到PatternList中的項目
        For i = 0 To PatternList.Count - 1
            tempNowTreeNode = PatternList(i)
            '檢查單層是否有相同名稱之Pattern
            If tempNowTreeNode.Name = tempPattern.Name Then
                tempNowTreeNode.Nodes.Add(NewNode)
            End If
            '保護只有一層
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '層資料讀取
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(j))
                            '更新既有項目
                            If tempLastTreeNode(treenumber).Name = tempPattern.Name Then '
                                tempLastTreeNode(treenumber).Nodes.Add(NewNode)
                            End If
                            treenumber += 1
                        Next
                    End If
                    tempNowTreeNode = tempLastTreeNode(usenumber)
                    usenumber += 1
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If

        Next i

        '將Pattern更新回去
        For i = Path.GetLength(0) - 1 To 1 Step -1
            tempPattern = tempPattern.Parent
        Next
        FirstNode = tempPattern
    End Sub


    ' ''' <summary>
    ' ''' [顯示更新]
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub PatternDataView()
    '    Dim i As Integer
    '    Dim tempPattern As CRecipePattern


    '    If IsNothing(TreePath) = False Then
    '        '找到所選的Pattern
    '        tempPattern = gPattern(TreePath(0))
    '        For i = 1 To TreePath.GetLength(0) - 1
    '            tempPattern = tempPattern.Nodes(TreePath(i))
    '        Next
    '    End If
    'End Sub


    ''' <summary>
    ''' [取得Pattern的Index路徑]
    ''' </summary>
    ''' <param name="PatternData"></param>
    ''' <returns>[輸入Pattern]</returns>
    ''' <remarks>[輸出Index路徑，但不包含最上層Stage的Index]</remarks>
    Private Function GetIndex(ByVal PatternData As CRecipePattern) As String
        Dim StrIndex As String = ""
        Dim NowPattern As CRecipePattern = PatternData
        Dim NextPattern As CRecipePattern
        Dim i As Integer

        For i = 0 To PatternData.Level - 1
            StrIndex = StrIndex & NowPattern.Index.ToString & ","
            NextPattern = NowPattern
            NowPattern = NextPattern.Parent
        Next

        Return StrIndex
    End Function

    ''' <summary>
    ''' [重建Treeview顯示]
    ''' </summary>
    ''' <param name="TreeData"></param>
    ''' <remarks>[輸入需要更新的TreeNode]</remarks>
    Private Sub TreePatternRebuild(ByVal TreeData As List(Of CRecipePattern))
        Dim treenumber As Integer = 0
        Dim usenumber As Integer = 0
        Dim tempNowTreeNode As New CRecipePattern
        Dim tempLastTreeNode As New List(Of CRecipePattern)
        Dim UpdateFlag As Boolean = True
        Dim i, j, k As Integer

        ReDim TreePath(0)


        '找到所選的Pattern
        For i = enmStage.No1 To gSSystemParameter.StageCount - 1
            tempNowTreeNode = TreeData(i)
            '保護只有一層WorkPiece
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                '處理後續資料流，會依照先層再個別索引去讀取
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '層資料讀取
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(j))
                            treenumber += 1
                            '判斷Pattern是否已經存在於PatternList，若存在則更新，不存在則新增
                            UpdateFlag = True
                            For k = 0 To PatternList.Count - 1
                                If PatternList(k).Name = tempNowTreeNode.Nodes(j).Name Then
                                    UpdateFlag = False
                                    Exit For
                                End If
                            Next k
                            If UpdateFlag = True Then
                                PatternList.Add(tempNowTreeNode.Nodes(j))
                                lstPatternID.Items.Add(tempNowTreeNode.Nodes(j).Name)
                            Else
                                tempNowTreeNode.Nodes(j) = PatternList(k).Clone()
                            End If
                        Next
                    End If
                    tempNowTreeNode = tempLastTreeNode(usenumber)
                    usenumber += 1
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If
        Next
        '更新顯示部分
        treePattern.Nodes.Clear()
        '依照WorkPiece分開，處理個別資料流
        For i = enmStage.No1 To gSSystemParameter.StageCount - 1
            If TreeData(i).Enable = True Then
                tempNowTreeNode = TreeData(i).Clone()
                treePattern.Nodes.Add(tempNowTreeNode)
            End If
        Next
    End Sub

#End Region

    ''' <summary>新增節點</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNodeAdd_Click(sender As Object, e As EventArgs) Handles btnNodeAdd.Click, mnuNodeAdd.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnNodeAdd]" & vbTab & "Click")
        If btnNodeAdd.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnNodeAdd.Enabled = False
        Dim tempNode As TreeNode
        Dim i As Integer
        Dim newPattern As New CRecipeNode
        Dim parentPath As String = SelectedNodePath '父節點路徑

        If SelectedNodePath = "" Then
            If treePattern.Nodes.Count > 0 Then
                treePattern.SelectedNode = treePattern.Nodes(0)
            End If
            btnNodeAdd.Enabled = True
            Exit Sub
        End If

        If SelectedNodePath.Length <= 4 Then
            'MsgBox("Select Node First!")
            treePattern.SelectedNode.BackColor = Color.Red
            System.Threading.Thread.CurrentThread.Join(300)
            treePattern.SelectedNode.BackColor = Color.White
            treePattern.SelectedNode.ForeColor = Color.Black
            btnNodeAdd.Enabled = True
            Exit Sub
        End If

        '找到目前所選的Node
        tempNode = treePattern.Nodes(TreePath(0))
        For i = 1 To TreePath.Count - 1
            tempNode = tempNode.Nodes(TreePath(i))
        Next
        '加入所選的Pattern於目前所選的Node之下一層
        Dim mPatternID As String = lstPatternID.SelectedItem

        'Jeffadd 保護
        If mPatternID = Nothing Then
            lstPatternID.BackColor = Color.Green
            System.Threading.Thread.CurrentThread.Join(300)
            lstPatternID.BackColor = Color.White
            btnNodeAdd.Enabled = True
            Exit Sub
        End If

        tempNode.Nodes.Add(gRecipeEdit.Pattern(mPatternID).Name)
        newPattern.PatternName = gRecipeEdit.Pattern(mPatternID).Name

        For mConveyorNo As Integer = 0 To newPattern.ConveyorPos.Count - 1 'Soni 2017.02.13
            '新建立節點時同時也建立AlignmentData，以防空集合
            For i = 0 To 2
                Dim tempAlign As New AlignmentStructure
                newPattern.ConveyorPos(mConveyorNo).AlignmentData.Add(tempAlign)
            Next
            '建立SkipMark，以防空集合
            Dim tempSkipMark As New AlignmentStructure
            newPattern.ConveyorPos(mConveyorNo).SkipMarkData.Add(tempSkipMark)

            Dim tempLaser As New LaserStructure
            newPattern.ConveyorPos(mConveyorNo).LaserData.Add(tempLaser)
        Next

        '20170616_Start 更新round 數
        lstRoundNo.Items.Clear()
        For j As Integer = 0 To gRecipeEdit.Pattern(mPatternID).Round.Count - 1
            lstRoundNo.Items.Add((j + 1).ToString)
        Next
        lstRoundNo.SelectedIndex = lstRoundNo.Items.Count - 1
        '20170616_End

        '將Treeview顯示選到目前新增的Node
        treePattern.SelectedNode = tempNode.LastNode
        treePattern.SelectedNode.ToolTipText = SelectedNodePath
        treePattern.SelectedNode.Name = SelectedNodePath
        newPattern.NodePath = SelectedNodePath
        newPattern.ParentNode = "Nothing"


        '因為選擇變更所以選擇路徑(SelectedNodePath)也會更新，將新增節點加入NodeDictionary
        If gRecipeEdit.Node(TreePath(0)).ContainsKey(SelectedNodePath) = False Then
            If gRecipeEdit.Node(TreePath(0)).ContainsKey(parentPath) Then '如果父節點存在
                If gRecipeEdit.Node(TreePath(0))(parentPath).ChildNodes.Contains(SelectedNodePath) = False Then
                    gRecipeEdit.Node(TreePath(0))(parentPath).ChildNodes.Add(SelectedNodePath) '在父節點下的ChildNodes加上這個節點的路徑
                    newPattern.ParentNode = parentPath
                End If
            End If
            '[Note]:加入Node時，必須告知ParentNode(但MainBody是以"Nothing"表示)
            gRecipeEdit.Node(TreePath(0)).Add(SelectedNodePath, newPattern)
            If gRecipeEdit.Node(sys.StageNo).ContainsKey(parentPath) Then '如果有父節點
                For mConveyorNo As Integer = 0 To newPattern.ConveyorPos.Count - 1 'Soni 2017.02.13
                    gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(mConveyorNo).BasicPositionX = gRecipeEdit.Node(sys.StageNo)(parentPath).ConveyorPos(mConveyorNo).BasicPositionX '以父節點基準點為本層基準點
                    gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(mConveyorNo).BasicPositionY = gRecipeEdit.Node(sys.StageNo)(parentPath).ConveyorPos(mConveyorNo).BasicPositionY
                    gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(mConveyorNo).BasicPositionZ = gRecipeEdit.Node(sys.StageNo)(parentPath).ConveyorPos(mConveyorNo).BasicPositionZ

                    'AlignmentEnable 預設為false Toby 20170710
                    'Toby  add_20170106
                    'gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).AlignmentEnable = True
                    'Toby  add_20170106_End

                Next
            End If
            gRecipeEdit.NodeCount(TreePath(0)) += 1

            '=== 建立基本陣列參數 ===
            Dim mTemp As New CArray
            Dim mLevel As New CRecipeNodeLevel
            mLevel.LevelType = eLevelType.Array
            mTemp.CountX = 1
            mTemp.CountY = 1
            mTemp.PitchX = 0
            mTemp.PitchY = 0
            mLevel.Array = mTemp
            mLevel.NonArray = New List(Of NonArray)
            mLevel.NonArray.Clear()
            mLevel.NonArray.Add(New NonArray)
            gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).Array.Add(mLevel)
            '=== 建立基本陣列參數 ===

        Else
            treePattern.SelectedNode.BackColor = Color.Red
            System.Threading.Thread.CurrentThread.Join(300)
            treePattern.SelectedNode.BackColor = Color.White
        End If

        'jimmy 20170718
        If chkNodeConnect.CheckState = CheckState.Checked Then

            If CheckNodeConnect() = True Then

                chkNodeConnect.Checked = True
            Else
                chkNodeConnect.Checked = False
            End If
        End If
        RefreshUI()
        btnNodeAdd.Enabled = True
    End Sub
    ''' <summary>刪除節點</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNodeDelete_Click(sender As Object, e As EventArgs) Handles btnNodeDelete.Click, mnuNodeDelete.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnNodeDelete]" & vbTab & "Click")
        If btnNodeDelete.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnNodeDelete.Enabled = False
        '=== 刪除保護 ===
        If MsgBox("Are yoy sure to Delete this Node?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
            btnNodeDelete.Enabled = True
            Exit Sub
        End If
        '=== 刪除保護 ===

        Dim tempNode As TreeNode
        Dim i As Integer
        '保護選到MAINBODY以上的節點
        If TreePath.Length < 3 Then
            btnNodeDelete.Enabled = True
            Exit Sub
        End If
        '找到目前所選的節點，並刪除
        tempNode = treePattern.Nodes(TreePath(0))
        For i = 1 To TreePath.GetLength(0) - 2
            tempNode = tempNode.Nodes(TreePath(i))
        Next i

        '找到要刪除的節點兄弟，將刪除之後的兄弟節點做整併
        Dim brotherNode As New List(Of String)
        Dim tNextNode As New TreeNode
        '判斷是否有兄弟節點，若有則把全部兄弟結點找出來
        If IsNothing(tempNode.Nodes(TreePath(i)).NextNode) = False Then
            tNextNode = tempNode.Nodes(TreePath(i)).NextNode
            brotherNode.Add(tNextNode.Name)
            Do
                If IsNothing(tNextNode.NextNode) = False Then
                    tNextNode = tNextNode.NextNode
                    brotherNode.Add(tNextNode.Name)
                Else
                    Exit Do
                End If
            Loop Until False
        End If

        '刪除子節點資料
        If gRecipeEdit.Node(TreePath(0))(SelectedNodePath).ParentNode <> "Nothing" Then
            gRecipeEdit.Node(TreePath(0))(gRecipeEdit.Node(TreePath(0))(SelectedNodePath).ParentNode).ChildNodes.Remove(SelectedNodePath)
        End If

        '刪除Node資料
        Dim strNode As String = SelectedNodePath
        Dim DelCount As Integer = 0
        Dim NodeCount As Integer = 0
        Dim DelNode As New List(Of String)
        Do
            '找到目前節點的子節點資料，並把以下每代相關子節點找出來
            For i = 0 To gRecipeEdit.Node(TreePath(0))(strNode).ChildNodes.Count - 1
                DelNode.Add(gRecipeEdit.Node(TreePath(0))(strNode).ChildNodes(i))
                NodeCount += 1
            Next
            If DelNode.Count > DelCount Then
                strNode = DelNode(DelCount)
                DelCount += 1
            End If
        Loop Until NodeCount = DelCount And gRecipeEdit.Node(TreePath(0))(strNode).ChildNodes.Count = 0
        '刪除自身節點
        gRecipeEdit.Node(TreePath(0)).Remove(SelectedNodePath)
        '刪除每一代子節點
        For i = 0 To DelNode.Count - 1
            gRecipeEdit.Node(TreePath(0)).Remove(DelNode(i))
        Next
        '更新節點數量
        gRecipeEdit.NodeCount(TreePath(0)) -= (DelNode.Count + 1)
        '更新兄弟節點
        For i = 0 To brotherNode.Count - 1
            Dim RecipeNode As New CRecipeNode
            Dim BrotherPath() As String
            Dim newPath As String = ""
            RecipeNode = gRecipeEdit.Node(TreePath(0))(brotherNode(i))
            BrotherPath = RecipeNode.NodePath.Split(",")
            '找到目前弟弟節點，因哥哥被刪除則弟弟需補哥哥位置
            BrotherPath(BrotherPath.GetLength(0) - 2) = (CInt(BrotherPath(BrotherPath.GetLength(0) - 2)) - 1).ToString()
            Dim strCount As Integer
            '重組哥哥位置的路徑
            For strCount = 0 To BrotherPath.Count - 2
                newPath &= BrotherPath(strCount) & ","
            Next
            RecipeNode.NodePath = newPath
            '判斷兄弟節點的親代是否為Nothing，Nothing則不用更新親代childnode表，若否則需更新childnode表
            If gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ParentNode <> "Nothing" Then
                gRecipeEdit.Node(TreePath(0))(gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ParentNode).ChildNodes.Remove(brotherNode(i))
                gRecipeEdit.Node(TreePath(0))(gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ParentNode).ChildNodes.Add(newPath)
            End If
            Dim ChildNode As New List(Of String)
            '找到兄弟節點的子節點開始，並更新兄弟節點的childnode表
            For strCount = 0 To gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ChildNodes.Count - 1
                ChildNode.Add(gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ChildNodes(strCount))
                gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ChildNodes(strCount) = gRecipeEdit.Node(TreePath(0))(brotherNode(i)).ChildNodes(strCount).Replace(brotherNode(i), newPath)
            Next
            strCount = 0
            Do
                '把兄弟節點以下的每代子節點找出來塞入list中
                If ChildNode.Count > strCount Then
                    If gRecipeEdit.Node(TreePath(0)).ContainsKey(ChildNode(strCount)) Then
                        For Each astr In gRecipeEdit.Node(TreePath(0))(ChildNode(strCount)).ChildNodes
                            ChildNode.Add(astr)
                        Next
                        strCount += 1
                    End If
                End If
            Loop Until ChildNode.Count = strCount
            '兄弟節點子節點找完
            '刪除原本就的節點
            gRecipeEdit.Node(TreePath(0)).Remove(brotherNode(i))
            '更新成新的節點
            gRecipeEdit.Node(TreePath(0)).Add(newPath, RecipeNode)
            '進行兄弟節點每代子節點更新
            For strCount = 0 To ChildNode.Count - 1
                Dim newChild As New CRecipeNode
                Dim OldPath As String
                Dim ChangePath As String
                newChild = gRecipeEdit.Node(TreePath(0))(ChildNode(strCount))
                OldPath = newChild.NodePath
                ChangePath = newChild.NodePath.Replace(brotherNode(i), newPath)
                newChild.NodePath = ChangePath
                ChangePath = newChild.ParentNode.Replace(brotherNode(i), newPath)
                newChild.ParentNode = ChangePath
                Dim ChildCount As Integer
                '更新childnode表
                For ChildCount = 0 To newChild.ChildNodes.Count - 1
                    ChangePath = newChild.ChildNodes(ChildCount).Replace(brotherNode(i), newPath)
                    newChild.ChildNodes(ChildCount) = ChangePath
                Next
                gRecipeEdit.Node(TreePath(0)).Remove(OldPath)
                gRecipeEdit.Node(TreePath(0)).Add(newChild.NodePath, newChild)
                treePattern.Nodes.Find(OldPath, True)(0).ToolTipText = newChild.NodePath
                treePattern.Nodes.Find(OldPath, True)(0).Name = newChild.NodePath
            Next
            treePattern.Nodes.Find(brotherNode(i), True)(0).ToolTipText = newPath
            treePattern.Nodes.Find(brotherNode(i), True)(0).Name = newPath
        Next
        '刪除treePattern中的節點
        tempNode.Nodes(TreePath(TreePath.GetLength(0) - 1)).Remove()
        '將選擇改成所刪除的節點上一層
        treePattern.SelectedNode = tempNode
        treePattern.SelectedNode.Expand()

        '20170616_Start 根據目前所選到的更新round 數
        If SelectedNodePath.Length > 8 Then
            lstRoundNo.Items.Clear()
            For j As Integer = 0 To gRecipeEdit.Pattern(treePattern.SelectedNode.Text).Round.Count - 1
                lstRoundNo.Items.Add((j + 1).ToString)
            Next
            lstRoundNo.SelectedIndex = lstRoundNo.Items.Count - 1
        Else
            Exit Sub
        End If

        '20170616_End
        'jimmy 20170718
        If chkNodeConnect.CheckState = CheckState.Checked Then
            If CheckNodeConnect() = True Then
                chkNodeConnect.Checked = True
            Else
                chkNodeConnect.Checked = False
            End If
        End If
        RefreshUI()
        btnNodeDelete.Enabled = True
    End Sub

    Dim mNodeCopy As New CRecipeNode

    ''' <summary>
    ''' Node Copy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuNodeCopy_Click(sender As Object, e As EventArgs) Handles mnuNodeCopy.Click
        If SelectedNodePath = "" Then
            Exit Sub
        End If
        If SelectedNodePath.Length < 8 Then 'MainBody與Stage
            '請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim mStageNo As Integer = TreePath(0)
        'TODO: 2017.02.10 待整理到ConveyorPos類別

        mNodeCopy = gRecipeEdit.Node(mStageNo)(SelectedNodePath).Clone()

    End Sub
    ''' <summary>
    ''' Node Paste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuNodePaste_Click(sender As Object, e As EventArgs) Handles mnuNodePaste.Click
        Try

            Dim tempNode As New TreeNode
            Dim newPattern As CRecipeNode = mNodeCopy
            Dim parentPath As String = SelectedNodePath '父節點路徑
            Dim i As Integer
            '找到目前所選的Node
            tempNode = treePattern.Nodes(TreePath(0))
            For i = 1 To TreePath.Count - 1
                tempNode = tempNode.Nodes(TreePath(i))
            Next
            '加入所選的Pattern於目前所選的Node之下一層
            tempNode.Nodes.Add(mNodeCopy.PatternName)

            '將Treeview顯示選到目前新增的Node
            treePattern.SelectedNode = tempNode.LastNode
            treePattern.SelectedNode.ToolTipText = SelectedNodePath
            treePattern.SelectedNode.Name = SelectedNodePath
            newPattern.NodePath = SelectedNodePath
            newPattern.ParentNode = "Nothing"
            newPattern.ChildNodes.Clear()
            '因為選擇變更所以選擇路徑(SelectedNodePath)也會更新，將新增節點加入NodeDictionary
            If gRecipeEdit.Node(TreePath(0)).ContainsKey(SelectedNodePath) = False Then
                If gRecipeEdit.Node(TreePath(0)).ContainsKey(parentPath) Then '如果父節點存在
                    If gRecipeEdit.Node(TreePath(0))(parentPath).ChildNodes.Contains(SelectedNodePath) = False Then
                        gRecipeEdit.Node(TreePath(0))(parentPath).ChildNodes.Add(SelectedNodePath) '在父節點下的ChildNodes加上這個節點的路徑
                        newPattern.ParentNode = parentPath
                    End If
                End If
                gRecipeEdit.Node(TreePath(0)).Add(SelectedNodePath, newPattern)
                gRecipeEdit.NodeCount(TreePath(0)) += 1
            Else
                MessageBox.Show("Node has existed")
            End If

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    Private Sub chkNodeConnect_CheckedChanged(sender As Object, e As EventArgs) Handles chkNodeConnect.CheckedChanged
        Dim tempPath() As String = SelectedNodePath.Split(",")
        If CheckNodeConnect() = True Then
            If tempPath.Count >= 2 Then
                If gRecipeEdit.Node(tempPath(1)).ContainsKey(SelectedNodePath) Then
                    Dim tempNode As String
                    For Each tempNode In gRecipeEdit.Node(tempPath(1)).Keys
                        gRecipeEdit.Node(tempPath(1))(tempNode).IsNodeConnect = chkNodeConnect.Checked
                    Next
                Else
                    chkNodeConnect.Checked = False
                End If
            Else
                chkNodeConnect.Checked = False
            End If
        Else
            chkNodeConnect.Checked = False
        End If
    End Sub

#Region "進退料"
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click, mnuLoadA.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLoad]" & vbTab & "Click")
        btnLoad.Enabled = False
        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnPark.Enabled = False
        btnMachineBBalance.Enabled = False
        btnMachineABalance.Enabled = False
        btnLoadB.Enabled = False
        btnUnload.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        If IsChuckVacuumReady(enmMachineStation.MachineA) = False Then
            gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
            gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
        End If
        btnLoad.Enabled = True
        '20170602按鍵保護
        btnAlignUnlock.Enabled = True
        btnPark.Enabled = True
        btnMachineBBalance.Enabled = True
        btnMachineABalance.Enabled = True
        btnLoadB.Enabled = True
        btnUnload.Enabled = True
        btnUnloadB.Enabled = True
        btnRun.Enabled = True
        btnPause.Enabled = True
        btnCCDRun.Enabled = True
        btnDryRun.Enabled = True
        btnBack.Enabled = True
        btnAlign.Enabled = True
        btnArray.Enabled = True
        btnHeight.Enabled = True
        btnAlignUnlock.Enabled = True
        btnInspect.Enabled = True

    End Sub

    Private Sub btnUnload_Click(sender As Object, e As EventArgs) Handles btnUnload.Click, mnuUnloadA.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnUnload]" & vbTab & "Click")
        btnUnload.Enabled = False

        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnPark.Enabled = False
        btnMachineBBalance.Enabled = False
        btnMachineABalance.Enabled = False
        btnLoad.Enabled = False
        btnLoadB.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
        btnUnload.Enabled = True
        '20170602按鍵保護
        btnAlignUnlock.Enabled = True
        btnPark.Enabled = True
        btnMachineBBalance.Enabled = True
        btnMachineABalance.Enabled = True
        btnLoad.Enabled = True
        btnLoadB.Enabled = True
        btnUnloadB.Enabled = True
        btnRun.Enabled = True
        btnPause.Enabled = True
        btnCCDRun.Enabled = True
        btnDryRun.Enabled = True
        btnBack.Enabled = True
        btnAlign.Enabled = True
        btnArray.Enabled = True
        btnHeight.Enabled = True
        btnAlignUnlock.Enabled = True
        btnInspect.Enabled = True

    End Sub

    Private Sub btnLoadB_Click(sender As Object, e As EventArgs) Handles btnLoadB.Click, mnuLoadB.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLoadB]" & vbTab & "Click")
        btnLoadB.Enabled = False

        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnPark.Enabled = False
        btnMachineBBalance.Enabled = False
        btnMachineABalance.Enabled = False
        btnLoad.Enabled = False
        btnUnload.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        If IsChuckVacuumReady(enmMachineStation.MachineB) = False Then
            If (gSSystemParameter.MachineType = enmMachineType.DCS_350A) Then
                gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.None
                gSYS(eSys.Conveyor2).Command = eSysCommand.LoadB
            Else
                gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                gSYS(eSys.Conveyor1).Command = eSysCommand.LoadB
            End If
        End If

        btnLoadB.Enabled = True

        '20170602按鍵保護
        btnAlignUnlock.Enabled = True
        btnPark.Enabled = True
        btnMachineBBalance.Enabled = True
        btnMachineABalance.Enabled = True
        btnLoad.Enabled = True
        btnUnload.Enabled = True
        btnUnloadB.Enabled = True
        btnRun.Enabled = True
        btnPause.Enabled = True
        btnCCDRun.Enabled = True
        btnDryRun.Enabled = True
        btnBack.Enabled = True
        btnAlign.Enabled = True
        btnArray.Enabled = True
        btnHeight.Enabled = True
        btnAlignUnlock.Enabled = True
        btnInspect.Enabled = True

    End Sub

    Private Sub btnUnloadB_Click(sender As Object, e As EventArgs) Handles btnUnloadB.Click, mnuUnloadB.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnUnloadB]" & vbTab & "Click")
        btnUnloadB.Enabled = False

        '20170602按鍵保護
        btnAlignUnlock.Enabled = False
        btnPark.Enabled = False
        btnMachineBBalance.Enabled = False
        btnMachineABalance.Enabled = False
        btnLoad.Enabled = False
        btnLoadB.Enabled = False
        btnUnload.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnAlignUnlock.Enabled = False
        btnInspect.Enabled = False

        If (gSSystemParameter.MachineType = enmMachineType.DCS_350A) Then
            gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.None
            gSYS(eSys.Conveyor2).Command = eSysCommand.UnloadB
        Else
            gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
            gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadB
        End If

        btnUnloadB.Enabled = True
        '20170602按鍵保護
        btnAlignUnlock.Enabled = True
        btnPark.Enabled = True
        btnMachineBBalance.Enabled = True
        btnMachineABalance.Enabled = True
        btnLoad.Enabled = True
        btnLoadB.Enabled = True
        btnUnload.Enabled = True
        btnRun.Enabled = True
        btnPause.Enabled = True
        btnCCDRun.Enabled = True
        btnDryRun.Enabled = True
        btnBack.Enabled = True
        btnAlign.Enabled = True
        btnArray.Enabled = True
        btnHeight.Enabled = True
        btnAlignUnlock.Enabled = True
        btnInspect.Enabled = True

    End Sub
#End Region


    Private Sub frmRecipe04_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        UcDisplay1.EndLive()
        'Eason 20170120 Ticket:100031 , Memory Freed
        UcDisplay1.ManualDispose()
        'Eason 20170209 Ticket:100060 :Memory Log

        ''[說明]:防止Recipe Add Path 後沒儲存,去跑AutoRun. 新增線路未儲存部分用舊Recip蓋過
        'If gRecipeEdit.strName <> "" Then
        '    Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
        '    DefaultDirectory = gRecipeEdit.strFileName ' Application.StartupPath & "\Recipe\" & gRecipeEdit.strName Soni / 2017.07.21 修正路徑異常
        '    'Recipe讀檔
        '    LoadRecipe(DefaultDirectory) '檔案讀取
        'End If

        Me.Dispose()
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    ''' <summary>
    ''' 單點繪圖
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvStep_Click(sender As Object, e As EventArgs) Handles dgvStep.Click
        If lstPatternID.SelectedIndex < 0 Then
            Exit Sub
        End If
        If lstRoundNo.SelectedIndex < 0 Then
            Exit Sub
        End If
        '20170616_Start

        Call DrawSingleGraphicsTest(picPcsSingleGraph, sys, mDrawIdx)
        If dgvStep.SelectedCells.Count > 0 Then 'Soni + 2016.09.13 未選保謢
            Call DrawSingleStepGraphicsTest(treePattern.SelectedNode.Text, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, picPcsSingleGraph, sys, mDrawIdx)
        End If
        '20170616_End

    End Sub

    Private Sub dgvStep_KeyUp(sender As Object, e As KeyEventArgs) Handles dgvStep.KeyUp
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down
                '20170616_Start
                Call DrawSingleGraphicsTest(picPcsSingleGraph, sys, mDrawIdx)
                If dgvStep.SelectedCells.Count > 0 Then 'Soni + 2016.09.13 未選保謢
                    Call DrawSingleStepGraphicsTest(treePattern.SelectedNode.Text, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, picPcsSingleGraph, sys, mDrawIdx)
                End If
                '20170616_End
        End Select
    End Sub


    '20160901
    Private Sub mnuProcessTime_Click(sender As Object, e As EventArgs) Handles mnuProcessTime.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[ProcessTime]" & vbTab & "Click")

        Dim mPatternID As String = treePattern.SelectedNode.Text

        mnuProcessTime.Enabled = False
        If Not gRecipeEdit.Editable Then
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            MsgBox(gMsgHandler.GetMessage(Warn_3000012), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            mnuProcessTime.Enabled = True
            Exit Sub
        End If

        If SelectedNodePath.Length > 8 Then
            If gRecipeEdit.Pattern(mPatternID).Round.Count = 1 Then
                mnuProcessTime.Enabled = True
                Exit Sub
            End If
        End If
     
        gfrmSetProcessTime = New frmSetProcessTime(Me)
        gfrmSetProcessTime.RecipeEdit = gRecipeEdit
        gfrmSetProcessTime.StartPosition = FormStartPosition.CenterScreen
        gfrmSetProcessTime.ShowDialog()
        gfrmSetProcessTime.Dispose()

        '20161116
        mnuProcessTime.Enabled = True
    End Sub

    Private Sub btnCCDRun_Click(sender As Object, e As EventArgs) Handles btnCCDRun.Click
        '[說明]:回Home完成才能執行 
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnCCDRun.Enabled = True
                    '20171031 隱藏 Toby
                    'mnuProcessTime.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If

            Case enmMachineType.DCSW_800AQ
                'Select Case gEqpInfo.RunMode
                '    Case enmMachineRunMode.AutoRun '整機生產,整機復歸
                If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnCCDRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnCCDRun.Enabled = True
                    Exit Sub
                End If
                If gDICollection.GetState(enmDI.DoorClose2, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2082102", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Alarm_2082102), eMessageLevel.Warning)
                    btnCCDRun.Enabled = True
                    Exit Sub
                End If
            Case enmMachineType.DCS_500AD
                'Select Case gEqpInfo.RunMode
                '    Case enmMachineRunMode.AutoRun '整機生產,整機復歸
                If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnCCDRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnCCDRun.Enabled = True
                    Exit Sub
                End If

            Case enmMachineType.DCS_F230A, enmMachineType.DCS_350A
                If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnCCDRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnCCDRun.Enabled = True
                    Exit Sub
                End If

        End Select

        '[說明]:判斷有無開啟Recipe
        If gRecipeEdit.strName = "" Then
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmRecipe04 btnCCDRun", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCCDRun.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.Loading Then
            '場景載入中，請稍後
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000030))
            MsgBox(gMsgHandler.GetMessage(Warn_3000030), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCCDRun.Enabled = True
            Exit Sub
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.NG Then
            '場景載入失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000038))
            MsgBox(gMsgHandler.GetMessage(Warn_3000038), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCCDRun.Enabled = True
            Exit Sub
        End If

        '[說明]:是否為InterLock之Alarm
        If gInterlockCollection.IsAlarm = True Then
            btnCCDRun.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        '[Note]有沒有選擇Stage, 選擇哪一個stage, 就看哪一隻相機
        If tabView.SelectedIndex = 1 Then
            'Dim CCDNo As Integer = CInt(mStageNo)
            Select Case SelectedNodePath.Substring(2, 1)
                Case "0"
                    sys = gSYS(eSys.DispStage1)
                Case "1"
                    sys = gSYS(eSys.DispStage2)
                Case "2"
                    sys = gSYS(eSys.DispStage3)
                Case "3"
                    sys = gSYS(eSys.DispStage4)
            End Select
        End If

        '[Note]切換至CCD頁面
        tabView.SelectTab(0)

        gSYS(eSys.OverAll).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        gSYS(eSys.Conveyor1).ExternalPause = False '暫停清除

        'Soni + 2016.9.20 使用VideoRun 使傳回值為0
        For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = True
        Next
        gSSystemParameter.EnableVideoRun = True
        gSSystemParameter.EnableDryRun = True
        UcDisplay1.EndLive(sys.CCDNo)
        'gCSystemTopAutoRun2Conveyor.IsJustOneRun = gRecipeEdit.IsJustOneRun
        gSYS(eSys.OverAll).Command = eSysCommand.AutoRun

        '20171026_Toby
        Timer1.Enabled = True

    End Sub

    Private Sub btnAddArray_Click(sender As Object, e As EventArgs) Handles btnAddArray.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnAddArray]" & vbTab & "Click")
        SelectStepButton(btnAddArray, eStepFunctionType.Array)
    End Sub

    Private Sub tabView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabView.SelectedIndexChanged
        If tabView.SelectedIndex = 0 Then
            If lstPatternID.SelectedIndex < 0 Then '未選取例外
                Exit Sub
            End If

            If gAOICollection.IsIndexOutOfRange(sys.CCDNo) Then 'CCDNo異常略過
                Exit Sub
            End If
            'UcDisplay1.EndLive()
            If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
                gEqpMsg.Add("Error_1012002", Error_1012002, eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1012002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End If
    End Sub


    Private Sub btnNodeMap_Click(sender As Object, e As EventArgs) Handles btnNodeMap.Click, mnuMAP.Click
        gSyslog.Save("[btnNodeMap]" & vbTab & "[btnNodeMap]" & vbTab & "Click")
        If gfrmNodeMap Is Nothing Then
            gfrmNodeMap = New frmNode
        ElseIf gfrmNodeMap.IsDisposed Then
            gfrmNodeMap = New frmNode
        End If

        With gfrmNodeMap
            .RecipeEdit = gRecipeEdit
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .ShowDialog()
            .Dispose()
        End With
    End Sub

    '20170105
    Private Sub mnuFileImport_Click(sender As Object, e As EventArgs) Handles mnuFileImport.Click

        '[說明]:檢查資料夾是否存在
        Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
        Dim NamePath As String
        Dim FileExists As Boolean = False
        Dim msg As String = ""
        Dim str As String
        Dim SceneName As String

        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eEnglish
                msg = "File duplication is overwritten?!"
            Case enmLanguageType.eSimplifiedChinese
                msg = "档案重复是否覆盖?!"
            Case enmLanguageType.eTraditionalChinese
                msg = "檔案重複是否覆蓋?!"
        End Select

        '[說明]:選取檔案資料夾
        With FolderBrowserImport
            .SelectedPath = DefaultDirectory
            .ShowNewFolderButton = False

            If .ShowDialog() = DialogResult.OK Then
                NamePath = .SelectedPath
            Else
                Exit Sub
            End If
        End With

        '[說明]:Recipe資料
        str = NamePath & "\Recipe\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Recipe\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".rcp") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".rcp", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Recipe\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Recipe") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Recipe\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Recipe\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Recipe\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Recipe\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Recipe\" & fileInfo.Name)
                    End If
                End If
            Next
        End If

        '[說明]:Temperature資料
        str = NamePath & "\Temperature\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Temperature\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".tdb") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".tdb", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Database\Temperature\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Temperature") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Database\Temperature\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Temperature\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Temperature\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Database\Temperature\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Temperature\" & fileInfo.Name)
                    End If
                End If
            Next
        End If

        '[說明]:Valve資料
        str = NamePath & "\PicoTouch\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Database\JetValve\PicoTouch\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".pst") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".pst", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Database\JetValve\PicoTouch\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "PicoTouch") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Database\JetValve\PicoTouch\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\JetValve\PicoTouch\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\JetValve\PicoTouch\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Database\JetValve\PicoTouch\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\JetValve\PicoTouch\" & fileInfo.Name)
                    End If
                End If
            Next
        End If

        str = NamePath & "\Advanjet\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Database\JetValve\Advanjet\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".pst") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".pst", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Database\JetValve\Advanjet\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Advanjet") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Database\JetValve\Advanjet\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\JetValve\Advanjet\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\JetValve\Advanjet\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Database\JetValve\Advanjet\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\JetValve\Advanjet\" & fileInfo.Name)
                    End If
                End If
            Next
        End If


        '[說明]:FlowRate資料
        str = NamePath & "\Weight\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Weight\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".wdb") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".wdb", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Database\Weight\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Weight") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Database\Weight\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Weight\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Weight\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Database\Weight\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Weight\" & fileInfo.Name)
                    End If
                End If
            Next
        End If

        '[說明]:Purge資料
        str = NamePath & "\Purge\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Purge\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".pdb") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".pdb", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Database\Purge\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Purge") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Database\Purge\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Purge\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Purge\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Database\Purge\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Purge\" & fileInfo.Name)
                    End If
                End If
            Next
        End If

        '[說明]:膠材資料
        str = NamePath & "\Paste\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Purge\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".pst") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".pst", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Database\Paste\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Paste") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Database\Paste\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Paste\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Paste\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Database\Paste\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Database\Paste\" & fileInfo.Name)
                    End If
                End If
            Next
        End If


        '[說明]:CCD,Light資料
        str = NamePath & "\CCD\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".vpp") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".vpp", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CCD") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                    End If
                End If
            Next
        End If


        str = NamePath & "\Light\"
        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\")
        '[說明]:檔案夾是否存在
        If System.IO.Directory.Exists(str) = True Then
            For Each files In System.IO.Directory.GetFiles(str)
                If files.EndsWith(".ini") Then
                    Dim fileInfo As New System.IO.FileInfo(files)
                    SceneName = fileInfo.Name.Replace(".ini", "")
                    '[說明]:判斷是否覆蓋
                    If FileExists = False Then
                        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name) = True Then
                            If MsgBox(msg, MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Light") <> MsgBoxResult.Ok Then
                                File.Delete(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                                File.Copy(str & fileInfo.Name, Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                                FileExists = True
                            Else
                                Exit Sub
                            End If
                        Else
                            File.Copy(str & fileInfo.Name, Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                        End If
                    ElseIf FileExists = True Then
                        File.Delete(Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                        File.Copy(str & fileInfo.Name, Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & fileInfo.Name)
                    End If
                End If
            Next
        End If
        InitDataBase()        '讀取資料庫
        MessageBox.Show("File Import complete in " & NamePath)
    End Sub



    '20170105
    Private Sub mnuFileExport_Click(sender As Object, e As EventArgs) Handles mnuFileExport.Click

        Dim folderPath As String
        Dim fileName As String
        Dim RecipeName As String

        '[說明]:判斷有無開啟Recipe
        If gRecipeEdit.strName = "" Then
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmRecipe04 FileExport", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
            '^^^^^^^
        End If

        '[說明]:長度不回空時,取.rcp前字串當檔名
        If gRecipeEdit.strName.Length <> 0 Then
            RecipeName = gRecipeEdit.strName.Remove(gRecipeEdit.strName.Length - 4, 4)
        Else
            MessageBox.Show("Recipe is not null value!!!")
            Exit Sub
        End If

        Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
        Dim NamePath As String
        '[說明]:選取檔案資料夾
        With FolderBrowserExport
            .SelectedPath = DefaultDirectory
            .ShowNewFolderButton = False

            If .ShowDialog() = DialogResult.OK Then
                NamePath = .SelectedPath
            Else
                Exit Sub
            End If
        End With
        NamePath = NamePath & "\" & RecipeName & "\"

        '[說明]:檢查資料夾是否存在,存在則刪除在新建
        If System.IO.Directory.Exists(NamePath) = False Then
            System.IO.Directory.CreateDirectory(NamePath)
        Else
            System.IO.Directory.Delete(NamePath, True)

            System.Threading.Thread.Sleep(10)
            System.IO.Directory.CreateDirectory(NamePath)
        End If

        '[說明]:Recipe資料
        If My.Computer.FileSystem.FileExists(gRecipeEdit.strFileName) = True Then
            System.IO.Directory.CreateDirectory(NamePath & "\Recipe\")
            File.Copy(gRecipeEdit.strFileName, NamePath & "\Recipe\" & gRecipeEdit.strName)
        End If

        '[說明]:Temperature資料
        folderPath = Application.StartupPath & "\Database\Temperature\"
        fileName = folderPath & gRecipeEdit.TempName & ".tdb"
        If My.Computer.FileSystem.FileExists(fileName) = True Then
            System.IO.Directory.CreateDirectory(NamePath & "\Temperature\")
            File.Copy(fileName, NamePath & "\Temperature\" & gRecipeEdit.TempName & ".tdb")
        End If

        Dim mValveMax As enmValve


        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                mValveMax = enmValve.Max

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                mValveMax = enmValve.No2

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        mValveMax = enmValve.No1

                    Case eMechanismModule.TwoValveOneStage
                        mValveMax = enmValve.No2

                End Select

        End Select

        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                '[說明]:Valve資料
                Select Case gSSystemParameter.StageParts.ValveData(mStageNo).JetValve(mValveNo)
                    Case eValveModel.PicoPulse
                        folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"
                        fileName = folderPath & gRecipeEdit.StageParts(mStageNo).ValveName(mValveNo) & ".pst"
                        If My.Computer.FileSystem.FileExists(fileName) = True Then
                            System.IO.Directory.CreateDirectory(NamePath & "\PicoTouch\")
                            If My.Computer.FileSystem.FileExists(NamePath & "\PicoTouch\" & gRecipeEdit.StageParts(mStageNo).ValveName(mValveNo) & ".pst") = False Then
                                File.Copy(fileName, NamePath & "\PicoTouch\" & gRecipeEdit.StageParts(mStageNo).ValveName(mValveNo) & ".pst")
                            End If
                        End If
                    Case eValveModel.Advanjet
                        folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"
                        fileName = folderPath & gRecipeEdit.StageParts(mStageNo).ValveName(mValveNo) & ".pst"
                        If My.Computer.FileSystem.FileExists(fileName) = True Then
                            System.IO.Directory.CreateDirectory(NamePath & "\Advanjet\")
                            If My.Computer.FileSystem.FileExists(NamePath & "\Advanjet\" & gRecipeEdit.StageParts(mStageNo).ValveName(mValveNo) & ".pst") = False Then
                                File.Copy(fileName, NamePath & "\Advanjet\" & gRecipeEdit.StageParts(mStageNo).ValveName(mValveNo) & ".pst")
                            End If
                        End If
                End Select

                '[說明]:Purge資料
                folderPath = Application.StartupPath & "\Database\Purge\"
                fileName = folderPath & gRecipeEdit.StageParts(mStageNo).PurgeName(mValveNo) & ".pdb"
                If My.Computer.FileSystem.FileExists(fileName) = True Then
                    System.IO.Directory.CreateDirectory(NamePath & "\Purge\")
                    If My.Computer.FileSystem.FileExists(NamePath & "\Purge\" & gRecipeEdit.StageParts(mStageNo).PurgeName(mValveNo) & ".pdb") = False Then
                        File.Copy(fileName, NamePath & "\Purge\" & gRecipeEdit.StageParts(mStageNo).PurgeName(mValveNo) & ".pdb")
                    End If
                End If
                '[說明]:膠材資料
                folderPath = Application.StartupPath & "\Database\Paste\"
                fileName = folderPath & gRecipeEdit.StageParts(mStageNo).PasteName(mValveNo) & ".pst"
                If My.Computer.FileSystem.FileExists(fileName) = True Then
                    System.IO.Directory.CreateDirectory(NamePath & "\Paste\")
                    If My.Computer.FileSystem.FileExists(NamePath & "\Paste\" & gRecipeEdit.StageParts(mStageNo).PasteName(mValveNo) & ".pst") = False Then
                        File.Copy(fileName, NamePath & "\Paste\" & gRecipeEdit.StageParts(mStageNo).PasteName(mValveNo) & ".pst")
                    End If
                End If
                '[說明]:FlowRate資料
                folderPath = Application.StartupPath & "\Database\Weight\"
                fileName = folderPath & gRecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo) & ".wdb"
                If My.Computer.FileSystem.FileExists(fileName) = True Then
                    System.IO.Directory.CreateDirectory(NamePath & "\Weight\")
                    If My.Computer.FileSystem.FileExists(NamePath & "\Weight\" & gRecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo) & ".wdb") = False Then
                        File.Copy(fileName, NamePath & "\Weight\" & gRecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo) & ".wdb")
                    End If
                End If
            Next
        Next

        '[說明]:CCD,Light資料
        System.IO.Directory.CreateDirectory(NamePath & "\CCD\")
        System.IO.Directory.CreateDirectory(NamePath & "\Light\")

        '[說明]:CCD場景與ini燈源資料
        For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            Dim mNodeID As String
            For j As Integer = 0 To gRecipeEdit.Node(i).Keys.Count - 1
                mNodeID = gRecipeEdit.Node(i).Keys(j)
                For mConveyorNo As Integer = 0 To gRecipeEdit.Node(i)(mNodeID).ConveyorPos.Count - 1 'Soni 2017.02.13
                    Select Case gRecipeEdit.Node(i)(mNodeID).AlignType
                        Case enmAlignType.DevicePos1
                            '[說明]:VPP檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp") = False Then
                                    File.Copy(fileName, NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp")
                                End If
                            End If

                            '[說明]:ini檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini") = False Then
                                    File.Copy(fileName, NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini")
                                End If
                            End If
                        Case enmAlignType.DevicePos2
                            '[說明]:VPP檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp") = False Then
                                    File.Copy(fileName, NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp")
                                End If
                            End If

                            '[說明]:ini檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini") = False Then
                                    File.Copy(fileName, NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini")
                                End If
                            End If
                            '=======================================================================================================================================================
                            '[說明]:VPP檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".vpp"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".vpp") = False Then
                                    File.Copy(fileName, NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".vpp")
                                End If
                            End If

                            '[說明]:ini檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".ini"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".ini") = False Then
                                    File.Copy(fileName, NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".ini")
                                End If
                            End If
                        Case enmAlignType.DevicePos3
                            '[說明]:VPP檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp") = False Then
                                    File.Copy(fileName, NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".vpp")
                                End If
                            End If

                            '[說明]:ini檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini") = False Then
                                    File.Copy(fileName, NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene & ".ini")
                                End If
                            End If
                            '=======================================================================================================================================================
                            '[說明]:VPP檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".vpp"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".vpp") = False Then
                                    File.Copy(fileName, NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".vpp")
                                End If
                            End If

                            '[說明]:ini檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".ini"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".ini") = False Then
                                    File.Copy(fileName, NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(1).AlignScene & ".ini")
                                End If
                            End If
                            '=======================================================================================================================================================
                            '[說明]:VPP檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene & ".vpp"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene & ".vpp") = False Then
                                    File.Copy(fileName, NamePath & "\CCD\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene & ".vpp")
                                End If
                            End If

                            '[說明]:ini檔
                            fileName = Application.StartupPath & "\Scene\" & MachineTypeToString(gSSystemParameter.MachineType) & "\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene & ".ini"
                            If My.Computer.FileSystem.FileExists(fileName) = True Then
                                If My.Computer.FileSystem.FileExists(NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene & ".ini") = False Then
                                    File.Copy(fileName, NamePath & "\Light\" & gRecipeEdit.Node(i)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(2).AlignScene & ".ini")
                                End If
                            End If
                    End Select
                Next

            Next
        Next
        MessageBox.Show("File Export complete in " & NamePath)
    End Sub

    'Toby add_20170106
    Private Async Sub btnAlignUnlock_Click(sender As Object, e As EventArgs) Handles btnAlignUnlock.Click 'Soni / 2017.05.16 去除DoEvents
        Await Task.Run(Sub()
                           Dim Align_Pass As Boolean
                           Align_Pass = False
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnAlignUnlock.Enabled = False
                                                  '20170602按鍵保護
                                                  btnPark.Enabled = False
                                                  btnMachineABalance.Enabled = False
                                                  btnMachineBBalance.Enabled = False
                                                  btnLoad.Enabled = False
                                                  btnLoadB.Enabled = False
                                                  btnUnload.Enabled = False
                                                  btnUnloadB.Enabled = False
                                                  btnRun.Enabled = False
                                                  btnPause.Enabled = False
                                                  btnCCDRun.Enabled = False
                                                  btnDryRun.Enabled = False
                                                  btnBack.Enabled = False
                                                  btnAlign.Enabled = False
                                                  btnArray.Enabled = False
                                                  btnHeight.Enabled = False
                                                  btnAlignUnlock.Enabled = False
                                                  btnInspect.Enabled = False
                                              End Sub)
                           End If

                           Dim AlignNo1 As Premtek.sPos
                           Select Case gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).AlignType
                               Case enmAlignType.DevicePos1
                                   If gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene = "" Then '未選場景
                                       '請選擇場景
                                       gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                                       MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       btnAlignUnlock.Enabled = True
                                       '20170602按鍵保護
                                       btnPark.Enabled = True
                                       btnMachineABalance.Enabled = True
                                       btnMachineBBalance.Enabled = True
                                       btnLoad.Enabled = True
                                       btnLoadB.Enabled = True
                                       btnUnload.Enabled = True
                                       btnUnloadB.Enabled = True
                                       btnRun.Enabled = True
                                       btnPause.Enabled = True
                                       btnCCDRun.Enabled = True
                                       btnDryRun.Enabled = True
                                       btnBack.Enabled = True
                                       btnAlign.Enabled = True
                                       btnArray.Enabled = True
                                       btnHeight.Enabled = True
                                       btnAlignUnlock.Enabled = True
                                       btnInspect.Enabled = True
                                       Exit Sub
                                   End If

                                   Align_Pass = alignment_1(AlignNo1)


                               Case enmAlignType.DevicePos2
                                   If gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene = "" Or gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene = "" Then '未選場景
                                       '請選擇場景
                                       gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                                       MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       btnAlignUnlock.Enabled = True
                                       '20170602按鍵保護
                                       btnPark.Enabled = True
                                       btnMachineABalance.Enabled = True
                                       btnMachineBBalance.Enabled = True
                                       btnLoad.Enabled = True
                                       btnLoadB.Enabled = True
                                       btnUnload.Enabled = True
                                       btnUnloadB.Enabled = True
                                       btnRun.Enabled = True
                                       btnPause.Enabled = True
                                       btnCCDRun.Enabled = True
                                       btnDryRun.Enabled = True
                                       btnBack.Enabled = True
                                       btnAlign.Enabled = True
                                       btnArray.Enabled = True
                                       btnHeight.Enabled = True
                                       btnAlignUnlock.Enabled = True
                                       btnInspect.Enabled = True
                                       Exit Sub
                                   End If
                                   If alignment_1(AlignNo1) Then
                                       Align_Pass = alignment_2(AlignNo1)
                                   Else
                                       Align_Pass = False
                                   End If

                               Case enmAlignType.DevicePos3
                                   '[Note] 目前的架構沒用到
                                   If gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene = "" Or gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene = "" Or gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene = "" Then '未選場景
                                       '請選擇場景
                                       gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                                       MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       btnAlignUnlock.Enabled = True
                                       '20170602按鍵保護
                                       btnPark.Enabled = True
                                       btnMachineABalance.Enabled = True
                                       btnMachineBBalance.Enabled = True
                                       btnLoad.Enabled = True
                                       btnLoadB.Enabled = True
                                       btnUnload.Enabled = True
                                       btnUnloadB.Enabled = True
                                       btnRun.Enabled = True
                                       btnPause.Enabled = True
                                       btnCCDRun.Enabled = True
                                       btnDryRun.Enabled = True
                                       btnBack.Enabled = True
                                       btnAlign.Enabled = True
                                       btnArray.Enabled = True
                                       btnHeight.Enabled = True
                                       btnAlignUnlock.Enabled = True
                                       btnInspect.Enabled = True
                                       Exit Sub
                                   End If
                                   If alignment_1(AlignNo1) Then
                                       If alignment_2(AlignNo1) Then
                                           Align_Pass = alignment_3()
                                       Else
                                           Align_Pass = False
                                       End If
                                   Else
                                       Align_Pass = False
                                   End If

                           End Select

                           If Align_Pass Then
                               MsgBox("Alignment Pass", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               AlignNodePath = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).NodePath
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      gRecipeEdit.Alignable = True
                                                      btnSelect.Enabled = True
                                                      btnLine3D.Enabled = True
                                                      btnCircle.Enabled = True
                                                      btnWait.Enabled = True
                                                      btnDots3D.Enabled = True
                                                      btnArc.Enabled = True
                                                      btnMove3D.Enabled = True
                                                      btnExtendOn.Enabled = True
                                                      btnExtendOff.Enabled = True
                                                      btnContiStart.Enabled = True
                                                      btnContiEnd.Enabled = True
                                                      btnStepDelete.Enabled = True
                                                      grpStep.Enabled = True
                                                      '20170803 Toby
                                                      btnRoundAdd.Enabled = True
                                                      btnRoundDelete.Enabled = True
                                                      btnStepDown.Enabled = True
                                                      btnStepUp.Enabled = True
                                                      btnRoundDown.Enabled = True
                                                      btnRoundUp.Enabled = True
                                                      btnRoundAdd.Enabled = True
                                                      btnRoundDelete.Enabled = True
                                                      btnStepDown.Enabled = True
                                                      btnStepUp.Enabled = True
                                                      btnRoundDown.Enabled = True
                                                      btnRoundUp.Enabled = True

                                                      mnuRoundAdd.Enabled = True
                                                      mnuRoundDelete.Enabled = True
                                                      mnuRoundMoveDown.Enabled = True
                                                      mnuRoundMoveUp.Enabled = True
                                                      mnuStepDelete.Enabled = True
                                                      mnuStepMoveDown.Enabled = True
                                                      mnuStepMoveUp.Enabled = True
                                                      mnuStepCopy.Enabled = True
                                                      mnuStepPaste.Enabled = True
                                                  End Sub)
                               End If

                           Else
                               MsgBox("Alignment Failed", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           End If

                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnAlignUnlock.Enabled = True
                                                  '20170602按鍵保護
                                                  btnPark.Enabled = True
                                                  btnMachineABalance.Enabled = True
                                                  btnMachineBBalance.Enabled = True
                                                  btnLoad.Enabled = True
                                                  btnLoadB.Enabled = True
                                                  btnUnload.Enabled = True
                                                  btnUnloadB.Enabled = True
                                                  btnRun.Enabled = True
                                                  btnPause.Enabled = True
                                                  btnCCDRun.Enabled = True
                                                  btnDryRun.Enabled = True
                                                  btnBack.Enabled = True
                                                  btnAlign.Enabled = True
                                                  btnArray.Enabled = True
                                                  btnHeight.Enabled = True
                                                  btnAlignUnlock.Enabled = True
                                                  btnInspect.Enabled = True
                                              End Sub)
                           End If

                       End Sub)

    End Sub
    Private Function alignment_1(ByRef RealSampleOffsetNo1 As Premtek.sPos) As Boolean '一點定位

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then 'CCD 不存在
            MsgBox("Acquisition Object Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            Exit Function
        End If

        If gAOICollection.SceneDictionary.ContainsKey(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene) Then
            '20170525_Toby Modify
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightEnable(enmValveLight.No1) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No1), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightEnable(enmValveLight.No2) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No2), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightEnable(enmValveLight.No3) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No3), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightEnable(enmValveLight.No4) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No4), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), False)
            End If
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No1), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No2), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No3), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene).LightValue(enmValveLight.No4), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), True)
        End If

        If gRecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Return False
            Exit Function
        End If

        '[說明]:X、Y、Z軸
        If sys.AxisX > -1 Then
            gCMotion.SetVelAccDec(sys.AxisX)
        End If
        If sys.AxisY > -1 Then
            gCMotion.SetVelAccDec(sys.AxisY)
        End If
        If sys.AxisZ > -1 Then
            gCMotion.SetVelAccDec(sys.AxisZ)
        End If
        If sys.AxisB > -1 Then
            gCMotion.SetVelAccDec(sys.AxisB)
        End If
        If sys.AxisC > -1 Then
            gCMotion.SetVelAccDec(sys.AxisC)
        End If

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC
        'TargetPos(0) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosX
        'TargetPos(1) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosY
        'TargetPos(2) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosZ
        TargetPos(0) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).TeachPosX
        TargetPos(1) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).TeachPosY
        TargetPos(2) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).TeachPosZ
        TargetPos(3) = 0
        TargetPos(4) = 0

        ButtonSafeMovePos(Nothing, AxisNo, TargetPos, sys)
        gAOICollection.SetCCDScene(sys.CCDNo, gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene) '選擇場景

        Dim ticket As Integer = 0
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
        System.Threading.Thread.CurrentThread.Join(10)
        ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

        Dim timeOutStopWatch As New Stopwatch '逾時計時器
        timeOutStopWatch.Restart()
        Do
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                Select Case sys.StageNo
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

                Return False
                Exit Function
            End If
        Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
        'Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)

        timeOutStopWatch.Restart()
        Do
            If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                Exit Do
            End If
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 計算TimeOut
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select

                Return False
                Exit Function
            End If
        Loop

        If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
            'Pattern不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000043))
            MsgBox(gMsgHandler.GetMessage(Warn_3000043), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            Exit Function
        End If

        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        'Dim RealSampleNo1 As sPos
        RealSampleOffsetNo1.PosX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        RealSampleOffsetNo1.PosY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosX = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).BasicPositionX + gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignOffsetX - RealSampleOffsetNo1.PosX
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosY = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).BasicPositionY + gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignOffsetY - RealSampleOffsetNo1.PosY
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosC = degree - gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignRoation
        '20170830
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosZ = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).BasicPositionZ
        Return True
    End Function

    Private Function alignment_2(ByVal RealSampleOffsetNo1 As Premtek.sPos) As Boolean '二點定位

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then 'CCD 不存在
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            Exit Function
        End If

        If gAOICollection.SceneDictionary.ContainsKey(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene) Then
            '20170525_Toby Modify
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightEnable(enmValveLight.No1) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No1), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightEnable(enmValveLight.No2) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No2), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightEnable(enmValveLight.No3) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No3), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightEnable(enmValveLight.No4) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No4), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), False)
            End If
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No1), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No2), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No3), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene).LightValue(enmValveLight.No4), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), True)
        End If

        If gRecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Return False
            Exit Function
        End If

        '[說明]:X、Y、Z軸
        If sys.AxisX > -1 Then
            gCMotion.SetVelAccDec(sys.AxisX)
        End If
        If sys.AxisY > -1 Then
            gCMotion.SetVelAccDec(sys.AxisY)
        End If
        If sys.AxisZ > -1 Then
            gCMotion.SetVelAccDec(sys.AxisZ)
        End If
        If sys.AxisB > -1 Then
            gCMotion.SetVelAccDec(sys.AxisB)
        End If
        If sys.AxisC > -1 Then
            gCMotion.SetVelAccDec(sys.AxisC)
        End If

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC
        'TargetPos(0) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosX
        'TargetPos(1) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosY
        'TargetPos(2) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosZ
        TargetPos(0) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).TeachPosX
        TargetPos(1) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).TeachPosY
        TargetPos(2) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).TeachPosZ
        TargetPos(3) = 0
        TargetPos(4) = 0

        ButtonSafeMovePos(Nothing, AxisNo, TargetPos, sys)
        gAOICollection.SetCCDScene(sys.CCDNo, gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignScene) '選擇場景

        Dim ticket As Integer = 0
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
        System.Threading.Thread.CurrentThread.Join(10)
        ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

        Dim timeOutStopWatch As New Stopwatch '逾時計時器
        timeOutStopWatch.Restart()
        Do
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 取像TimeOut
                Select Case sys.StageNo
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
                Return False
                Exit Function
            End If
        Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
        'Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)

        timeOutStopWatch.Restart()
        Do
            If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                Exit Do
            End If
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 計算TimeOut
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select

                Return False
                Exit Function
            End If
        Loop

        If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
            'Pattern不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000043))
            MsgBox(gMsgHandler.GetMessage(Warn_3000043), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            Exit Function
        End If

        Dim GoldenSampleNo1 As Premtek.sPos
        Dim GoldenSampleNo2 As Premtek.sPos
        Dim RealSampleNo1 As Premtek.sPos
        Dim RealSampleNo2 As Premtek.sPos
        GoldenSampleNo1.PosX = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosX - gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignOffsetX
        GoldenSampleNo1.PosY = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosY - gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignOffsetY
        GoldenSampleNo2.PosX = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosX - gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignOffsetX
        GoldenSampleNo2.PosY = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosY - gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignOffsetY
        RealSampleNo1.PosX = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosX - RealSampleOffsetNo1.PosX
        RealSampleNo1.PosY = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosY - RealSampleOffsetNo1.PosY
        RealSampleNo2.PosX = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosX - gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        RealSampleNo2.PosY = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosY - gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY

        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        Dim angle As Decimal '= gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        EstimateRotationByTwoAlign(GoldenSampleNo1, GoldenSampleNo2, RealSampleNo1, RealSampleNo2, angle)

        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosX = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).BasicPositionX - RealSampleOffsetNo1.PosX
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosY = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).BasicPositionY - RealSampleOffsetNo1.PosY
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosC = angle
        '20170830
        gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).RealBasicPosZ = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).BasicPositionZ
        Return True
    End Function
    Private Function alignment_3() As Boolean '三點定位

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then 'CCD 不存在
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            Exit Function
        End If

        If gAOICollection.SceneDictionary.ContainsKey(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene) Then
            '20170525_Toby Modify
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightEnable(enmValveLight.No1) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No1), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightEnable(enmValveLight.No2) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No2), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightEnable(enmValveLight.No3) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No3), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), False)
            End If
            If gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightEnable(enmValveLight.No4) = True Then
                gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No4), True)
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), True)
            Else
                gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), False)
            End If
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No1), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No2), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No3), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), True)
            'gLightCollection.SetCCDLight(sys.CCDNo, gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene).LightValue(enmValveLight.No4), True)
            'gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), True)
        End If

        If gRecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Return False
            Exit Function
        End If

        '[說明]:X、Y、Z軸
        If sys.AxisX > -1 Then
            gCMotion.SetVelAccDec(sys.AxisX)
        End If
        If sys.AxisY > -1 Then
            gCMotion.SetVelAccDec(sys.AxisY)
        End If
        If sys.AxisZ > -1 Then
            gCMotion.SetVelAccDec(sys.AxisZ)
        End If
        If sys.AxisB > -1 Then
            gCMotion.SetVelAccDec(sys.AxisB)
        End If
        If sys.AxisC > -1 Then
            gCMotion.SetVelAccDec(sys.AxisC)
        End If

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC
        TargetPos(0) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).TeachPosX
        TargetPos(1) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).TeachPosY
        TargetPos(2) = gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(0).TeachPosZ
        TargetPos(3) = 0
        TargetPos(4) = 0

        ButtonSafeMovePos(Nothing, AxisNo, TargetPos, sys)
        gAOICollection.SetCCDScene(sys.CCDNo, gRecipeEdit.Node(sys.StageNo)(SelectedNodePath).ConveyorPos(sys.ConveyorNo).AlignmentData(2).AlignScene) '選擇場景

        Dim ticket As Integer = 0
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
        System.Threading.Thread.CurrentThread.Join(10)
        ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

        Dim timeOutStopWatch As New Stopwatch '逾時計時器
        timeOutStopWatch.Restart()
        Do
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 取像TimeOut
                Select Case sys.StageNo
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
                Return False
                Exit Function
            End If
        Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
        'Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)

        timeOutStopWatch.Restart()
        Do
            If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                Exit Do
            End If
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 計算TimeOut
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select

                Return False
                Exit Function
            End If
        Loop

        If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
            'Pattern不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000043))
            MsgBox(gMsgHandler.GetMessage(Warn_3000043), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            Exit Function
        End If
        Return True
    End Function

    Private Sub frmRecipe04_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Debug.Print("GotFocus")
    End Sub





    '20170830
    Private Sub Rotae180_Click(sender As Object, e As EventArgs) Handles mnuRotae180.Click
        '[說明]:固定角度
        Angle = 180

        '[說明]:取得Pattern下所有節點
        With gRecipeEdit
            For mRoundNo = 0 To .Pattern(lstPatternID.SelectedItem).Round.Count - 1
                Dim num As Integer = .Pattern(lstPatternID.SelectedItem).Round.Count
                With .Pattern(lstPatternID.SelectedItem).Round(mRoundNo)
                    If .StepCount > 0 Then
                        For mStepNo = 0 To .StepCount - 1
                            With .CStep(mStepNo)
                                Select Case .StepType
                                    Case eStepFunctionType.Dots3D '點
                                        CMath.Rotation(.Dots3D.PosX, .Dots3D.PosY, Angle, mPosX(0), mPosY(0))

                                        .Dots3D.PosX = mPosX(0)
                                        .Dots3D.PosY = mPosY(0)
                                        .Dots3D.PosZ = .Dots3D.PosZ

                                    Case eStepFunctionType.Move3D
                                        CMath.Rotation(.Move3D.EndPosX, .Move3D.EndPosY, Angle, mPosX(0), mPosY(0))

                                        .Move3D.EndPosX = mPosX(0)
                                        .Move3D.EndPosY = mPosY(0)
                                        .Move3D.EndPosZ = .Move3D.EndPosZ

                                    Case eStepFunctionType.Line3D
                                        CMath.Rotation(.Line3D.StartPosX, .Line3D.StartPosY, Angle, mPosX(0), mPosY(0))
                                        CMath.Rotation(.Line3D.EndPosX, .Line3D.EndPosY, Angle, mPosX(1), mPosY(1))

                                        .Line3D.StartPosX = mPosX(0)
                                        .Line3D.StartPosY = mPosY(0)
                                        .Line3D.StartPosZ = .Line3D.StartPosZ
                                        .Line3D.EndPosX = mPosX(1)
                                        .Line3D.EndPosY = mPosY(1)
                                        .Line3D.EndPosZ = .Line3D.EndPosZ


                                    Case eStepFunctionType.Circle2D
                                        CMath.Rotation(.Circle2D.StartPosX, .Circle2D.StartPosY, Angle, mPosX(0), mPosY(0))
                                        CMath.Rotation(.Circle2D.MiddlePosX, .Circle2D.MiddlePosY, Angle, mPosX(1), mPosY(1))
                                        CMath.Rotation(.Circle2D.Middle2PosX, .Circle2D.Middle2PosY, Angle, mPosX(2), mPosY(2))
                                        CMath.Rotation(.Circle2D.CenterPosX, .Circle2D.CenterPosY, Angle, mPosX(3), mPosY(3))

                                        .Circle2D.StartPosX = mPosX(0)
                                        .Circle2D.StartPosY = mPosY(0)

                                        .Circle2D.MiddlePosX = mPosX(1)
                                        .Circle2D.MiddlePosY = mPosY(1)

                                        .Circle2D.Middle2PosX = mPosX(2)
                                        .Circle2D.Middle2PosY = mPosY(2)

                                        .Circle2D.CenterPosX = mPosX(3)
                                        .Circle2D.CenterPosY = mPosY(3)

                                    Case eStepFunctionType.Arc2D
                                        CMath.Rotation(.Arc2D.StartPosX, .Arc2D.StartPosY, Angle, mPosX(0), mPosY(0))
                                        CMath.Rotation(.Arc2D.MiddlePosX, .Arc2D.MiddlePosY, Angle, mPosX(1), mPosY(1))
                                        CMath.Rotation(.Arc2D.EndPosX, .Arc2D.EndPosY, Angle, mPosX(2), mPosY(2))
                                        CMath.Rotation(.Arc2D.CenterPosX, .Arc2D.CenterPosY, Angle, mPosX(3), mPosY(3))

                                        .Arc2D.StartPosX = mPosX(0)
                                        .Arc2D.StartPosY = mPosY(0)
                                        .Arc2D.MiddlePosX = mPosX(1)
                                        .Arc2D.MiddlePosY = mPosY(1)
                                        .Arc2D.EndPosX = mPosX(2)
                                        .Arc2D.EndPosY = mPosY(2)
                                        .Arc2D.CenterPosX = mPosX(3)
                                        .Arc2D.CenterPosY = mPosY(3)
                                End Select
                            End With
                        Next
                    End If
                End With
            Next
        End With

        '[說明]:繪製Path
        Call gfrmRecipe04.DrawPCSSingleGraphics()
    End Sub


    Private Sub MirrorX_Click(sender As Object, e As EventArgs) Handles mnuMirrorX.Click

        '[說明]:取得Pattern下所有節點
        With gRecipeEdit
            For mRoundNo = 0 To .Pattern(lstPatternID.SelectedItem).Round.Count - 1
                Dim num As Integer = .Pattern(lstPatternID.SelectedItem).Round.Count
                With .Pattern(lstPatternID.SelectedItem).Round(mRoundNo)
                    If .StepCount > 0 Then
                        For mStepNo = 0 To .StepCount - 1
                            With .CStep(mStepNo)
                                Select Case .StepType
                                    Case eStepFunctionType.Dots3D '點
                                        mPosX(0) = .Dots3D.PosX
                                        mPosY(0) = -.Dots3D.PosY

                                        .Dots3D.PosX = mPosX(0)
                                        .Dots3D.PosY = mPosY(0)
                                        .Dots3D.PosZ = .Dots3D.PosZ
                                    Case eStepFunctionType.Move3D
                                        mPosX(0) = .Move3D.EndPosX
                                        mPosY(0) = -.Move3D.EndPosY

                                        .Move3D.EndPosX = mPosX(0)
                                        .Move3D.EndPosY = mPosY(0)
                                        .Move3D.EndPosZ = .Move3D.EndPosZ
                                    Case eStepFunctionType.Line3D
                                        mPosX(0) = .Line3D.StartPosX
                                        mPosY(0) = -.Line3D.StartPosY
                                        mPosX(1) = .Line3D.EndPosX
                                        mPosY(1) = -.Line3D.EndPosY

                                        .Line3D.StartPosX = mPosX(0)
                                        .Line3D.StartPosY = mPosY(0)
                                        .Line3D.StartPosZ = .Line3D.StartPosZ
                                        .Line3D.EndPosX = mPosX(1)
                                        .Line3D.EndPosY = mPosY(1)
                                        .Line3D.EndPosZ = .Line3D.EndPosZ
                                    Case eStepFunctionType.Circle2D
                                        mPosX(0) = .Circle2D.StartPosX
                                        mPosY(0) = -.Circle2D.StartPosY
                                        mPosX(1) = .Circle2D.MiddlePosX
                                        mPosY(1) = -.Circle2D.MiddlePosY
                                        mPosX(2) = .Circle2D.Middle2PosX
                                        mPosY(2) = -.Circle2D.Middle2PosY
                                        mPosX(3) = .Circle2D.CenterPosX
                                        mPosY(3) = -.Circle2D.CenterPosY

                                        .Circle2D.StartPosX = mPosX(0)
                                        .Circle2D.StartPosY = mPosY(0)
                                        .Circle2D.MiddlePosX = mPosX(1)
                                        .Circle2D.MiddlePosY = mPosY(1)
                                        .Circle2D.Middle2PosX = mPosX(2)
                                        .Circle2D.Middle2PosY = mPosY(2)
                                        .Circle2D.CenterPosX = mPosX(3)
                                        .Circle2D.CenterPosY = mPosY(3)

                                    Case eStepFunctionType.Arc2D
                                        mPosX(0) = .Arc2D.StartPosX
                                        mPosY(0) = -.Arc2D.StartPosY
                                        mPosX(1) = .Arc2D.MiddlePosX
                                        mPosY(1) = -.Arc2D.MiddlePosY
                                        mPosX(2) = .Arc2D.EndPosX
                                        mPosY(2) = -.Arc2D.EndPosY
                                        mPosX(3) = .Arc2D.CenterPosX
                                        mPosY(3) = -.Arc2D.CenterPosY

                                        .Arc2D.StartPosX = mPosX(0)
                                        .Arc2D.StartPosY = mPosY(0)
                                        .Arc2D.MiddlePosX = mPosX(1)
                                        .Arc2D.MiddlePosY = mPosY(1)
                                        .Arc2D.EndPosX = mPosX(2)
                                        .Arc2D.EndPosY = mPosY(2)
                                        .Arc2D.CenterPosX = mPosX(3)
                                        .Arc2D.CenterPosY = mPosY(3)
                                End Select
                            End With
                        Next
                    End If
                End With
            Next
        End With

        '[說明]:繪製Path
        Call gfrmRecipe04.DrawPCSSingleGraphics()
    End Sub


    Private Sub MirrorY_Click(sender As Object, e As EventArgs) Handles mnuMirrorY.Click

        '[說明]:取得Pattern下所有節點
        With gRecipeEdit
            For mRoundNo = 0 To .Pattern(lstPatternID.SelectedItem).Round.Count - 1
                Dim num As Integer = .Pattern(lstPatternID.SelectedItem).Round.Count
                With .Pattern(lstPatternID.SelectedItem).Round(mRoundNo)
                    If .StepCount > 0 Then
                        For mStepNo = 0 To .StepCount - 1
                            With .CStep(mStepNo)
                                Select Case .StepType
                                    Case eStepFunctionType.Dots3D '點
                                        mPosX(0) = -.Dots3D.PosX
                                        mPosY(0) = .Dots3D.PosY

                                        .Dots3D.PosX = mPosX(0)
                                        .Dots3D.PosY = mPosY(0)
                                        .Dots3D.PosZ = .Dots3D.PosZ
                                    Case eStepFunctionType.Move3D
                                        mPosX(0) = -.Move3D.EndPosX
                                        mPosY(0) = .Move3D.EndPosY

                                        .Move3D.EndPosX = mPosX(0)
                                        .Move3D.EndPosY = mPosY(0)
                                        .Move3D.EndPosZ = .Move3D.EndPosZ
                                    Case eStepFunctionType.Line3D
                                        mPosX(0) = -.Line3D.StartPosX
                                        mPosY(0) = .Line3D.StartPosY
                                        mPosX(1) = -.Line3D.EndPosX
                                        mPosY(1) = .Line3D.EndPosY

                                        .Line3D.StartPosX = mPosX(0)
                                        .Line3D.StartPosY = mPosY(0)
                                        .Line3D.StartPosZ = .Line3D.StartPosZ
                                        .Line3D.EndPosX = mPosX(1)
                                        .Line3D.EndPosY = mPosY(1)
                                        .Line3D.EndPosZ = .Line3D.EndPosZ
                                    Case eStepFunctionType.Circle2D
                                        mPosX(0) = -.Circle2D.StartPosX
                                        mPosY(0) = .Circle2D.StartPosY
                                        mPosX(1) = -.Circle2D.MiddlePosX
                                        mPosY(1) = .Circle2D.MiddlePosY
                                        mPosX(2) = -.Circle2D.Middle2PosX
                                        mPosY(2) = .Circle2D.Middle2PosY
                                        mPosX(3) = -.Circle2D.CenterPosX
                                        mPosY(3) = .Circle2D.CenterPosY

                                        .Circle2D.StartPosX = mPosX(0)
                                        .Circle2D.StartPosY = mPosY(0)
                                        .Circle2D.MiddlePosX = mPosX(1)
                                        .Circle2D.MiddlePosY = mPosY(1)
                                        .Circle2D.Middle2PosX = mPosX(2)
                                        .Circle2D.Middle2PosY = mPosY(2)
                                        .Circle2D.CenterPosX = mPosX(3)
                                        .Circle2D.CenterPosY = mPosY(3)

                                    Case eStepFunctionType.Arc2D
                                        mPosX(0) = -.Arc2D.StartPosX
                                        mPosY(0) = .Arc2D.StartPosY
                                        mPosX(1) = -.Arc2D.MiddlePosX
                                        mPosY(1) = .Arc2D.MiddlePosY
                                        mPosX(2) = -.Arc2D.EndPosX
                                        mPosY(2) = .Arc2D.EndPosY
                                        mPosX(3) = -.Arc2D.CenterPosX
                                        mPosY(3) = .Arc2D.CenterPosY

                                        .Arc2D.StartPosX = mPosX(0)
                                        .Arc2D.StartPosY = mPosY(0)
                                        .Arc2D.MiddlePosX = mPosX(1)
                                        .Arc2D.MiddlePosY = mPosY(1)
                                        .Arc2D.EndPosX = mPosX(2)
                                        .Arc2D.EndPosY = mPosY(2)
                                        .Arc2D.CenterPosX = mPosX(3)
                                        .Arc2D.CenterPosY = mPosY(3)
                                End Select
                            End With
                        Next
                    End If
                End With
            Next
        End With

        '[說明]:繪製Path
        Call gfrmRecipe04.DrawPCSSingleGraphics()
    End Sub
    '20170830



    'Sue 20170601
    Private Sub frmRecipe04_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        Me.Enabled = False
        Me.Location = New Point(0, 0)
        Me.Width = 1920
        Me.Height = 1080
        Me.Enabled = True
    End Sub

    'jimmy20170719
    Public Function CheckNodeConnect() As Boolean
        Dim tempPath() As String = SelectedNodePath.Split(",")
        Dim treeNode As String = treePattern.SelectedNode.Text
        If tempPath.Count >= 2 Then
            If gRecipeEdit.Node(tempPath(1)).ContainsKey(SelectedNodePath) Then
                If gRecipeEdit.Node(tempPath(1)).Count = 1 Or gRecipeEdit.Pattern(treeNode).Round.Count > 1 Then
                    Return False
                Else
                    Return True
                End If
                Return False

            End If
            Return False
        End If
        Return True

    End Function
    'Sue20170731 視窗關閉防護 
    Private Sub frmRecipe04_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ExitMode = False Then '如果不是由離開按鍵關閉視窗，則取消執行關閉動作
            e.Cancel = True
        End If
    End Sub

    Private Sub mnuPatternRename_Click(sender As Object, e As EventArgs) Handles mnuPatternRename.Click
        Dim mPatternName As String
        If (lstPatternID.SelectedItem <> Nothing) Then
            mPatternName = InputBox("Input Pattern ID", "Recipe")
            If mPatternName = "" Then '無輸入保護
                '請先選取Recipe Pattern
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
                MsgBox(gMsgHandler.GetMessage(Warn_3000013), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
            For i As Integer = 0 To gRecipeEdit.Pattern.Count - 1 '輸入重複保護
                If mPatternName = gRecipeEdit.Pattern.Keys(i) Then
                    gSyslog.Save("Pattern ID has Existed!", , eMessageLevel.Warning)
                    MsgBox("Please ID has Existed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Recipe")
                    btnPatternAdd.Enabled = True
                    Exit Sub
                End If
            Next

            'copy 
            Dim mSelectPatternName As String = lstPatternID.SelectedItem
            Dim mPatternCopyName As String = mSelectPatternName
            mPatternCopy = New CRecipePattern()
            mPatternCopy = gRecipeEdit.Pattern(mSelectPatternName).Clone()

            'delete
            Dim idx As Integer = lstPatternID.SelectedIndex
            Dim mDeletedPatternID As String = lstPatternID.SelectedItem
            If Not gRecipeEdit.Pattern.ContainsKey(mDeletedPatternID) Then '物件不存在
                lstPatternID.Items.Remove(lstPatternID.SelectedItem) '清除選擇的物件
                If lstPatternID.Items.Count > 0 Then
                    If idx < lstPatternID.Items.Count Then
                        lstPatternID.SelectedIndex = idx
                    Else
                        lstPatternID.SelectedIndex = 0
                    End If
                End If
                Exit Sub
            End If

            '=== Soni + 2016.09.21 任一節點使用中不能刪除, 避免資料刪除下使用 ===
            For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1 '對每個平台下的
                For mNodeNo As Integer = 0 To gRecipeEdit.Node(mStageNo).Keys.Count - 1 '每個節點
                    If gRecipeEdit.Node(mStageNo)(gRecipeEdit.Node(mStageNo).Keys(mNodeNo)).PatternName = mDeletedPatternID Then
                        'Pattern使用中無法刪除
                        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000042))
                        MsgBox(gMsgHandler.GetMessage(Warn_3000042), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        'jiimmy 20170717
                        btnPatternDelete.Enabled = True
                        Exit Sub
                    End If
                Next
            Next
            '=== Soni + 2016.09.21 任一節點使用中不能刪除, 避免資料刪除下使用 ===
            gRecipeEdit.Pattern.Remove(mDeletedPatternID) '刪除實際物件
            RefreshPatternEditor(0) '更新選單

            'paste
            mPatternCopy.Name = mPatternName
            gRecipeEdit.Pattern.Add(mPatternName, mPatternCopy)
            RefreshPatternEditor(lstPatternID.Items.Count - 1)
            RefreshUI()
        Else
            MessageBox.Show("You did not select the pattern.")
        End If
    End Sub

    Dim mVideoFlag As Boolean = False
    '20171001
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case gEqpInfo.Status '設備當前狀態
            Case enmEqpStatus.Running
                btnSelect.Enabled = False
                btnLine3D.Enabled = False
                btnCircle.Enabled = False
                btnWait.Enabled = False
                btnDots3D.Enabled = False
                btnArc.Enabled = False
                btnMove3D.Enabled = False
                btnExtendOn.Enabled = False
                btnExtendOff.Enabled = False
                btnContiStart.Enabled = False
                btnContiEnd.Enabled = False
                btnStepDelete.Enabled = False
                '20170803 Toby
                btnRoundAdd.Enabled = False
                btnRoundDelete.Enabled = False
                btnStepDown.Enabled = False
                btnStepUp.Enabled = False
                btnRoundDown.Enabled = False
                btnRoundUp.Enabled = False
                mnuRoundAdd.Enabled = False
                mnuRoundDelete.Enabled = False
                mnuRoundMoveDown.Enabled = False
                mnuRoundMoveUp.Enabled = False
                mnuStepDelete.Enabled = False
                mnuStepMoveDown.Enabled = False
                mnuStepMoveUp.Enabled = False
                mnuStepCopy.Enabled = False
                mnuStepPaste.Enabled = False
                btnAlignUnlock.Enabled = False
                btnPark.Enabled = False
                btnMachineABalance.Enabled = False
                btnMachineBBalance.Enabled = False
                btnLoad.Enabled = False
                btnLoadB.Enabled = False
                btnUnload.Enabled = False
                btnUnloadB.Enabled = False
                btnRun.Enabled = False
                btnCCDRun.Enabled = False
                btnDryRun.Enabled = False
                btnBack.Enabled = False
                btnAlign.Enabled = False
                btnArray.Enabled = False
                btnHeight.Enabled = False
                btnAlignUnlock.Enabled = False
                btnInspect.Enabled = False
                btnNodeMap.Enabled = False
                btnStepBasicEdit.Enabled = False

                btnPause.Enabled = True
                If gSSystemParameter.EnableVideoRun Then
                    Select Case sys.ExecuteCommand
                        Case eSysCommand.Dispensing '[Note]開啟膠路視覺
                            If mVideoFlag = False Then
                                If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
                                    gEqpMsg.Add("Error_1012002", Error_1012002, eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1012002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                End If
                            End If
                            mVideoFlag = True
                        Case Else
                            'UcDisplay1.EndLive(sys.CCDNo)
                            mVideoFlag = False
                    End Select
                End If


            Case enmEqpStatus.Alarm

                btnSelect.Enabled = False
                btnLine3D.Enabled = False
                btnCircle.Enabled = False
                btnWait.Enabled = False
                btnDots3D.Enabled = False
                btnArc.Enabled = False
                btnMove3D.Enabled = False
                btnExtendOn.Enabled = False
                btnExtendOff.Enabled = False
                btnContiStart.Enabled = False
                btnContiEnd.Enabled = False
                btnStepDelete.Enabled = False
                '20170803 Toby
                btnRoundAdd.Enabled = False
                btnRoundDelete.Enabled = False
                btnStepDown.Enabled = False
                btnStepUp.Enabled = False
                btnRoundDown.Enabled = False
                btnRoundUp.Enabled = False
                mnuRoundAdd.Enabled = False
                mnuRoundDelete.Enabled = False
                mnuRoundMoveDown.Enabled = False
                mnuRoundMoveUp.Enabled = False
                mnuStepDelete.Enabled = False
                mnuStepMoveDown.Enabled = False
                mnuStepMoveUp.Enabled = False
                mnuStepCopy.Enabled = False
                mnuStepPaste.Enabled = False
                btnAlignUnlock.Enabled = False
                btnPark.Enabled = False
                btnMachineABalance.Enabled = False
                btnMachineBBalance.Enabled = False
                btnLoad.Enabled = False
                btnLoadB.Enabled = False
                btnUnload.Enabled = False
                btnUnloadB.Enabled = False
                btnRun.Enabled = False
                btnPause.Enabled = False
                btnCCDRun.Enabled = False
                btnDryRun.Enabled = False
                btnAlign.Enabled = False
                btnArray.Enabled = False
                btnHeight.Enabled = False
                btnAlignUnlock.Enabled = False
                btnInspect.Enabled = False
                btnNodeMap.Enabled = False
                btnStepBasicEdit.Enabled = False

                btnBack.Enabled = True
                gSSystemParameter.EnableDryRun = False
                Timer1.Enabled = False

                For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                    gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
                Next
                mVideoFlag = False

            Case enmEqpStatus.RunFinish, enmEqpStatus.RunPause, enmEqpStatus.RunStop
                btnAlignUnlock.Enabled = True
                btnMachineABalance.Enabled = True
                btnMachineBBalance.Enabled = True
                btnLoad.Enabled = True
                btnLoadB.Enabled = True
                btnUnload.Enabled = True
                btnUnloadB.Enabled = True
                btnRun.Enabled = True
                btnPause.Enabled = True
                btnCCDRun.Enabled = True
                btnDryRun.Enabled = True
                btnAlign.Enabled = True
                btnArray.Enabled = True
                btnHeight.Enabled = True
                btnInspect.Enabled = True

                btnNodeMap.Enabled = True
                btnStepBasicEdit.Enabled = True

                btnBack.Enabled = True
                gSSystemParameter.EnableDryRun = False
                gSSystemParameter.EnableVideoRun = False
                Timer1.Enabled = False

                For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                    gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
                Next
                mVideoFlag = False
        End Select
    End Sub


    '20171001
    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnPause]" & vbTab & "Click")
        If btnPause.Enabled = False Then '防連點
            Exit Sub
            '^^^^^^^
        End If
        btnPause.Enabled = False

        gSYS(eSys.OverAll).ExternalPause = True '整機生產, 整機暫停
        gSYS(eSys.DispStage1).ExternalPause = True 'A機暫停
        gSYS(eSys.DispStage2).ExternalPause = True 'A機暫停
        gSYS(eSys.DispStage3).ExternalPause = True 'B機暫停
        gSYS(eSys.DispStage4).ExternalPause = True 'B機暫停

        For mSysNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
            gAOICollection.SetLiveTriggerMode(gSYS(mSysNo).CCDNo, eTriggerType.SoftwareTrigger)
        Next

        '[說明]: 紀錄EndTime 20161205
        gSSystemParameter.AutoRunEndTime = DateTime.Now '生產開始時間
        '[說明]:記錄開始結束時間   
        gSyslog.Save("AutoRunEndTime is " & Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00"))

        btnPause.Enabled = True
    End Sub


    '20171001
    Private Sub btnDryRun_Click(sender As Object, e As EventArgs) Handles btnDryRun.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnDryRun]" & vbTab & "Click")
        '20171026 Toby 不用判斷recipe 是否鎖定
        'If Not gRecipeEdit.Editable Then
        '    BtnReadOnlyBehavior(sender)
        '    '請先解除Recipe鎖定
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
        '    Exit Sub
        'End If

        '[說明]:回Home完成才能執行 
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A, enmMachineType.eDTS_2S2V
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnDryRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
            Case enmMachineType.DCS_F230A, enmMachineType.DCS_350A
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnDryRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    '  btnStart.Enabled = True
                    Exit Sub
                End If
            Case enmMachineType.DCS_500AD
                If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnDryRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnDryRun.Enabled = True
                    Exit Sub
                End If
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnDryRun.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnDryRun.Enabled = True
                    Exit Sub
                End If
                If gDICollection.GetState(enmDI.DoorClose2, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2082102", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Alarm_2082102), eMessageLevel.Warning)
                    btnDryRun.Enabled = True
                    Exit Sub
                End If
        End Select

        '[說明]:判斷有無開啟Recipe
        If gRecipeEdit.strName = "" Then
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnDryRun.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.Loading Then
            '場景載入中，請稍後
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000030))
            MsgBox(gMsgHandler.GetMessage(Warn_3000030), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnDryRun.Enabled = True
            Exit Sub
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.NG Then
            '場景載入失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000038))
            MsgBox(gMsgHandler.GetMessage(Warn_3000038), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnDryRun.Enabled = True
            Exit Sub
        End If

        If gSSystemParameter.IsCompareWithMapData = True Then
            '[說明]:判斷使用Map功能時Recipe有無選擇Node
            Dim isNodeReady As Boolean = True
            If (gSSystemParameter.StageMax = eSys.DispStage1) Then
                If (gRecipeEdit.NodeToMap(enmStage.No1) IsNot Nothing) Then
                    If (gRecipeEdit.NodeToMap(enmStage.No1).Count = 0) Then
                        isNodeReady = False
                    End If
                End If
            ElseIf (gSSystemParameter.StageMax = eSys.DispStage4) Then
                If (gRecipeEdit.NodeToMap(enmStage.No1) IsNot Nothing) AndAlso (gRecipeEdit.NodeToMap(enmStage.No2) IsNot Nothing) AndAlso (gRecipeEdit.NodeToMap(enmStage.No3) IsNot Nothing) AndAlso (gRecipeEdit.NodeToMap(enmStage.No4) IsNot Nothing) Then
                    If (gRecipeEdit.NodeToMap(enmStage.No1).Count = 0) AndAlso (gRecipeEdit.NodeToMap(enmStage.No2).Count = 0) AndAlso (gRecipeEdit.NodeToMap(enmStage.No3).Count = 0) AndAlso (gRecipeEdit.NodeToMap(enmStage.No4).Count = 0) Then
                        isNodeReady = False
                    End If
                End If
            End If

            If isNodeReady = False Then
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnDryRun.Enabled = True
                Exit Sub
            End If
        End If


        '[說明]:是否為InterLock之Alarm
        If gInterlockCollection.IsAlarm = True Then
            btnDryRun.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        '[說明]:膠量偵測步驟
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No2).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No3).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor3) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019206", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019206), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No4).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor3) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019306", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019306), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                    End If
                End If
            
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No2).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                    End If
                End If
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                            If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                'jimmy 20170725
                                btnDryRun.Enabled = True
                                Exit Sub
                            End If
                        End If
                    Case eMechanismModule.TwoValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                            If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                'jimmy 20170725
                                btnDryRun.Enabled = True
                                Exit Sub
                            End If
                        End If
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve2) = True Then
                            If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                                gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                                'jimmy 20170725
                                btnDryRun.Enabled = True
                                Exit Sub
                            End If
                        End If
                End Select
        End Select


        If gEqpInfo.Status = enmEqpStatus.RunPause Then '暫停後開始, 確認項目
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gEqpInfo.IsW800AQPauseCanContinue = False Then
                        'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000039))
                        gEqpMsg.AddHistoryAlarm("Warn_3000039", "frmRecipe04 btnDryRun", , gMsgHandler.GetMessage(Warn_3000039), eMessageLevel.Warning)
                        MsgBox(gMsgHandler.GetMessage(Warn_3000039), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        'jimmy 20170725
                        btnDryRun.Enabled = True
                        Exit Sub
                        '^^^^^^^
                    End If
                Case Else
            End Select
        End If


        '=== Soni + 2016.09.20 不使用VideoRun ===
        'Jimmy 20161006
        For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
        Next
        ''=== Soni + 2016.09.20 不使用VideoRun ===


        gSYS(eSys.OverAll).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        gSYS(eSys.Conveyor1).ExternalPause = False '暫停清除

        gDOCollection.SetState(enmDO.StartButtonLight, True)
        gDOCollection.SetState(enmDO.PauseButtonLight, False)
        gDOCollection.SetState(enmDO.StartButtonLight2, True)
        gDOCollection.SetState(enmDO.PauseButtonLight2, False)
        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖


        gSYS(eSys.OverAll).Command = eSysCommand.AutoRun
        gSSystemParameter.AutoRunStartTime = DateTime.Now '生產開始時間

        btnAlignUnlock.Enabled = False
        btnMachineABalance.Enabled = False
        btnMachineBBalance.Enabled = False
        btnLoad.Enabled = False
        btnLoadB.Enabled = False
        btnUnload.Enabled = False
        btnUnloadB.Enabled = False
        btnRun.Enabled = False
        btnPause.Enabled = False
        btnCCDRun.Enabled = False
        btnDryRun.Enabled = False
        btnBack.Enabled = False
        btnAlign.Enabled = False
        btnArray.Enabled = False
        btnHeight.Enabled = False
        btnInspect.Enabled = False


        gSSystemParameter.EnableDryRun = True
        Timer1.Enabled = True
    End Sub


    ''' <summary>取得步驟閥型別</summary>
    ''' <param name="roundNo"></param>
    ''' <param name="stepNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStepValveType(ByVal patternID As String, ByVal roundNo As Integer, ByVal stepNo As Integer, ByVal sys As sSysParam) As String

        '[Note]:基本上都是以StageNo1作為參考
        Dim valveType As String = "N"
        Select Case gRecipeEdit.Pattern(patternID).Round(roundNo).CStep(stepNo).SelectValve.ValveNo
            Case enmValve.No1
                Select Case gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1)
                    Case enmValveType.Jet
                        valveType = "J"
                    Case enmValveType.Auger
                        valveType = "A"
                    Case enmValveType.None
                        valveType = "N"
                End Select
            Case enmValve.No2
                Select Case gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2)
                    Case enmValveType.Jet
                        valveType = "J"
                    Case enmValveType.Auger
                        valveType = "A"
                    Case enmValveType.None
                        valveType = "N"
                End Select
        End Select

        Return valveType
    End Function


    Private PatternMaxMinOnce As Boolean
    ' ''' <summary>找Pattern的最大最小範圍</summary>
    ' ''' <param name="minX"></param>
    ' ''' <param name="minY"></param>
    ' ''' <param name="maxX"></param>
    ' ''' <param name="maxY"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function FindPatternMaxMinOffset(ByRef minX As Decimal, ByRef minY As Decimal, ByRef maxX As Decimal, ByRef maxY As Decimal, ByVal sys As sSysParam) As Boolean
    '    If gCRecipe Is Nothing Then
    '        minX = 0
    '        minY = 0
    '        maxX = 0
    '        maxY = 0
    '        Return False
    '    End If
    '    If gCRecipe.Pattern Is Nothing Then
    '        minX = 0
    '        minY = 0
    '        maxX = 0
    '        maxY = 0
    '        Return False
    '    End If

    '    minX = Decimal.MaxValue
    '    minY = Decimal.MaxValue
    '    maxX = Decimal.MinValue
    '    maxY = Decimal.MinValue

    '    '無步驟預設值
    '    If gCRecipe.Pattern.Count = 0 Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If
    '    '無步驟預設值


    '    If sys.StageNo < 0 Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If
    '    If sys.StageNo >= gCRecipe.StageNodeID.Count Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If

    '    Dim mPatternName As String

    '    If gCRecipe.StageNodeID(sys.StageNo) = "" Then '找不到開頭
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If
    '    If Not gCRecipe.Node(sys.StageNo).ContainsKey(gCRecipe.StageNodeID(sys.StageNo)) Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If

    '    mPatternName = gCRecipe.Node(sys.StageNo)(gCRecipe.StageNodeID(sys.StageNo)).PatternName
    '    If mPatternName = "" Then '找不到開頭
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If
    '    If Not gCRecipe.Pattern.ContainsKey(mPatternName) Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If
    '    If gCRecipe.Pattern(mPatternName).Round.Count = 0 Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If

    '    '無步驟預設值
    '    If gCRecipe.Pattern(mPatternName).Round(0).CStep.Count = 0 Then
    '        minX = 0
    '        minY = 0
    '        maxX = 10
    '        maxY = 10
    '        Return False
    '    End If
    '    '無步驟預設值

    '    For mRoundNo As Integer = 0 To gCRecipe.Pattern(mPatternName).Round.Count - 1
    '        For mStepNo As Integer = 0 To gCRecipe.Pattern(mPatternName).Round(mRoundNo).CStep.Count - 1
    '            With gCRecipe.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo)
    '                Select Case .StepType
    '                    Case eStepFunctionType.Circle3D
    '                        Dim radius As Decimal = GetDistance(.Circle3D.EndPosX, .Circle3D.EndPosY, .Circle3D.CenterPosX, .Circle3D.CenterPosY) '找半徑
    '                        CompareMaxMin(.Circle3D.CenterPosX - radius, .Circle3D.CenterPosY - radius, maxX, minX, maxY, minY) '比大小更新最大最小值
    '                        CompareMaxMin(.Circle3D.CenterPosX + radius, .Circle3D.CenterPosY + radius, maxX, minX, maxY, minY) '比大小更新最大最小值

    '                    Case eStepFunctionType.Arc3D
    '                        Dim StartPos(1) As Decimal
    '                        Dim EndPos(1) As Decimal
    '                        Dim Center(1) As Decimal
    '                        StartPos(0) = .Arc3D.StartPosX
    '                        StartPos(1) = .Arc3D.StartPosY
    '                        EndPos(0) = .Arc3D.EndPosX
    '                        EndPos(1) = .Arc3D.EndPosY
    '                        If gCRecipe.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle >= 0 Then
    '                            CenterCalculate(StartPos, EndPos, Math.Abs(gCRecipe.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 0)
    '                        Else
    '                            CenterCalculate(StartPos, EndPos, Math.Abs(gCRecipe.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 1)
    '                        End If

    '                        Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))
    '                        CompareMaxMin(Center(0) - mRadius, Center(1) - mRadius, maxX, minX, maxY, minY)
    '                        CompareMaxMin(Center(0) + mRadius, Center(1) + mRadius, maxX, minX, maxY, minY)

    '                    Case Else
    '                        CompareMaxMin(.Line3D.StartPosX, .Line3D.StartPosY, maxX, minX, maxY, minY)
    '                        CompareMaxMin(.Line3D.EndPosX, .Line3D.StartPosY, maxX, minX, maxY, minY)

    '                End Select
    '            End With


    '        Next
    '    Next



    '    Return True
    'End Function

    '    ''' <summary>
    '    ''' 描繪出劃膠示意圖(單一顆)  限定只能在主畫面做繪圖
    '    ''' </summary>
    '    ''' <param name="index">繪製參考索引</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function DrawGraphicsMultiDeviceForDieState(ByVal sys As sSysParam, ByVal index As sLevelIndexCollection) As Boolean

    '        Dim mDraw As Graphics
    '        Dim mPen As New Pen(Color.Black)
    '        Dim mBrush As New Drawing.SolidBrush(Color.DimGray)
    '        Dim mBitmap As Bitmap
    '        Dim mDrawing As New Drawing.Point
    '        Dim mFont As Font


    '        Dim GraphicsPictureBox As PictureBox
    '        Dim mGraphicsStartX As Decimal                           '轉換成畫布大小StartX
    '        Dim mGraphicsStartY As Decimal                           '轉換成畫布大小StartY
    '        Dim mGraphicsWidth As Decimal                            '轉換成畫布大小Width
    '        Dim mGraphicsHeight As Decimal                           '轉換成畫布大小Height
    '        Dim mGraphicsXAxisPitch As Decimal                       '轉換成畫布大小XAxisPitch


    '        GraphicsPictureBox = frmOperator.UcWaferMapA.PictureBox1 ' gfrmOpStatus.picRecipeGraph

    '        mBitmap = GraphicsPictureBox.Image

    '        If index.PatternName Is Nothing Then 'Soni + 2016.09.16 PatternName不存在...
    '            GraphicsPictureBox.Image = mBitmap
    '            Return False
    '        End If
    '        If Not gCRecipe.Node(sys.StageNo).ContainsKey(index.PatternName) Then 'Soni + 2016.09.16 PatternName不存在...
    '            GraphicsPictureBox.Image = mBitmap
    '            Return False
    '        End If

    '        If mBitmap Is Nothing Then
    '            mBitmap = New Bitmap(CInt(GraphicsPictureBox.Width), CInt(GraphicsPictureBox.Height))
    '        End If

    '        mDraw = Graphics.FromImage(mBitmap)
    '        'mDraw.Clear(Color.AliceBlue)

    '        '[說明]:畫外框(chuck大小)
    '        With mPen
    '            .Width = 3
    '            .DashStyle = Drawing2D.DashStyle.Solid
    '        End With

    '        Dim StartPosX As Decimal '起點顆
    '        Dim StartPosY As Decimal '起點顆
    '        Dim startIdx As New sLevelIndexCollection
    '        startIdx.PatternName = index.PatternName
    '        startIdx.Xf = 0
    '        startIdx.Yf = 0
    '        Call GetCCDScanPos(sys, startIdx, StartPosX, StartPosY, enmAlignType.DevicePos1)
    '        Dim maxIndex As New sLevelIndexCollection
    '        maxIndex.PatternName = index.PatternName
    '        Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(sys.StageNo)(index.PatternName).Array)
    '        maxIndex.Xf = mMultiArrayAdapter.GetMemoryCountX() - 1
    '        maxIndex.Yf = mMultiArrayAdapter.GetMemoryCountY() - 1

    '        Dim endPosX As Decimal '最末端顆起點
    '        Dim endPosY As Decimal '最末端顆起點
    '        Call GetCCDScanPos(sys, maxIndex, endPosX, endPosY, enmAlignType.DevicePos1)
    '        Dim MinX As Decimal
    '        Dim MinY As Decimal
    '        Dim MaxX As Decimal
    '        Dim MaxY As Decimal
    '        Dim PatternX As Decimal
    '        Dim PatternY As Decimal

    '        Call FindPatternMaxMinOffset(MinX, MinY, MaxX, MaxY, sys) '取得Pattern極限範圍
    '        PatternX = Math.Abs(MaxX - MinX)
    '        PatternY = Math.Abs(MaxY - MinY)
    '        If PatternX > PatternY Then
    '            PatternY = PatternX
    '        Else
    '            PatternX = PatternY
    '        End If

    '        Dim TotalDistanceX As Decimal '總距離X
    '        Dim TotalDistanceY As Decimal '總距離Y
    '        TotalDistanceX = Math.Abs(endPosX - StartPosX) + 2 * PatternX
    '        TotalDistanceY = Math.Abs(endPosY - StartPosY) + 2 * PatternY
    '        Dim scaleX As Decimal
    '        Dim scaleY As Decimal
    '        Dim boardX As Decimal = 25
    '        Dim boardY As Decimal = 25
    '        If TotalDistanceX <> 0 Then
    '            scaleX = (GraphicsPictureBox.Width - 2 * boardX) / TotalDistanceX
    '        Else
    '            scaleX = 1
    '        End If
    '        If TotalDistanceY <> 0 Then
    '            scaleY = (GraphicsPictureBox.Height - 2 * boardY) / TotalDistanceY
    '        Else
    '            scaleY = 1
    '        End If
    '        '[說明]:畫Recipe圖形  先轉換成畫布Size再描畫圖形
    '        With mPen
    '            .Width = 2
    '            .DashStyle = Drawing2D.DashStyle.Solid
    '            .Color = Color.White
    '        End With
    '        mBrush.Color = Color.White
    '        mPen.Width = 2

    '        mGraphicsWidth = Math.Abs(MaxX - MinX) * scaleX
    '        mGraphicsHeight = Math.Abs(MaxY - MinY) * scaleY
    '        If mGraphicsWidth = 0 Then mGraphicsWidth = 25
    '        If mGraphicsHeight = 0 Then mGraphicsHeight = 25

    '        '[說明]:
    '        With gStageMap(sys.StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf)
    '            Select Case .DieState
    '                Case enmDieState.OK
    '                    Select Case .enmDispenserState
    '                        Case enmDispenserState.Done
    '                            Select Case .enmDieDetectState
    '                                Case enmDieDetectState.None, enmDieDetectState.OK
    '                                    mPen.Color = Color.Green
    '                                    mBrush.Color = Color.Green

    '                                Case enmDieDetectState.NG
    '                                    mPen.Color = Color.Red
    '                                    mBrush.Color = Color.Red

    '                            End Select

    '                        Case enmDispenserState.None
    '                            mPen.Color = Color.White
    '                            mBrush.Color = Color.White

    '                    End Select

    '                Case enmDieState.NG
    '                    mPen.Color = Color.Red
    '                    mBrush.Color = Color.Red

    '                Case enmDieState.NoDie
    '                    mPen.Color = Color.Yellow
    '                    mBrush.Color = Color.Yellow

    '                Case enmDieState.None
    '                    mPen.Color = Color.White
    '                    mBrush.Color = Color.White

    '            End Select
    '        End With

    '        Dim posX As Decimal, posY As Decimal
    '        '[說明]:

    '        Select Case gSSystemParameter.MachineType
    '            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A 'DrawGraphics
    '                If GetChuckStatus(index.Xf, index.Yf, sys) = True Then
    '                    mBrush.Color = Color.Green
    '                Else
    '                    mBrush.Color = Color.White
    '                End If
    '                Call GetCCDScanPos(sys, index, posX, posY, enmAlignType.DevicePos1) '取得座標
    '                mGraphicsStartX = (posX - StartPosX) * scaleX + boardX
    '                mGraphicsStartY = GraphicsPictureBox.Height + (posY - StartPosY) * scaleY - mGraphicsHeight - boardY
    '                mDraw.FillRectangle(mBrush, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight))

    '        End Select

    '        mDrawing.X = mGraphicsStartX
    '        mDrawing.Y = mGraphicsStartY
    '        mFont = New Font("", 9)

    '        If mGraphicsWidth > 100 Then
    '            mDraw.DrawString("FS: " & gStageMap(sys.StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf).intFixSimilar.ToString & "%", mFont, Brushes.Black, mDrawing)
    '        Else
    '            mDraw.DrawString(gStageMap(sys.StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf).intFixSimilar.ToString & "%", mFont, Brushes.Black, mDrawing)
    '        End If

    '        mDrawing.X = mGraphicsStartX
    '        mDrawing.Y = mGraphicsStartY + CInt(mGraphicsHeight / 2)
    '        If mGraphicsXAxisPitch > 100 Then
    '            mDraw.DrawString("C: " & gStageMap(sys.StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf).intGlueCoverRate.ToString & "%", mFont, Brushes.Black, mDrawing)
    '        Else
    '            mDraw.DrawString(gStageMap(sys.StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf).intGlueCoverRate.ToString & "%", mFont, Brushes.Black, mDrawing)
    '        End If



    '        GraphicsPictureBox.Image = mBitmap

    '        Return True

    'ErrorHandler:

    '        If Err.Number <> 0 Then  ' No error. Do nothing.
    '            Err.Clear()         '清除錯誤資訊
    '        End If

    '        Return False
    '    End Function

    '    ''' <summary>
    '    ''' 判斷晶粒的輸出結果
    '    ''' </summary>
    '    ''' <param name="index">索引由0開始</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function DetermineResultState(ByVal index As sLevelIndexCollection, ByVal sys As sSysParam) As Boolean


    '        With gStageMap(sys.StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf)
    '            Select Case .DieState
    '                Case enmDieState.OK
    '                    Select Case .enmDispenserState
    '                        Case enmDispenserState.Done
    '                            Select Case .enmDieDetectState
    '                                Case enmDieDetectState.None, enmDieDetectState.OK
    '                                    .enmResultState = enmResultState.OK

    '                                Case enmDieDetectState.NG
    '                                    .enmResultState = enmResultState.NG

    '                            End Select

    '                        Case enmDispenserState.None
    '                            .enmResultState = enmResultState.Unfinished

    '                    End Select

    '                Case enmDieState.NoDie
    '                    .enmResultState = enmResultState.NoDie

    '                Case enmDieState.NG
    '                    .enmResultState = enmResultState.NG

    '                Case enmDieState.None
    '                    .enmResultState = enmResultState.None

    '            End Select
    '        End With

    '        Return True

    'ErrorHandler:

    '        If Err.Number <> 0 Then  ' No error. Do nothing.
    '            Err.Clear()         '清除錯誤資訊
    '        End If

    '        Return False
    '    End Function



    'Sub CompareMaxMin(ByVal x As Decimal, ByVal y As Decimal, ByRef maxX As Decimal, ByRef minX As Decimal, ByRef maxY As Decimal, ByRef minY As Decimal)
    '    If x < minX Then minX = x
    '    If x > maxX Then maxX = x
    '    If y < minY Then minY = y
    '    If y > maxY Then maxY = y
    'End Sub

    '20170505
    Function FindPatternMaxMinTest(ByRef minX As Double, ByRef minY As Double, ByRef maxX As Double, ByRef maxY As Double, ByVal sys As sSysParam, ByVal index As sLevelIndexCollection) As Boolean

        If gRecipeEdit Is Nothing Then
            minX = 0
            minY = 0
            maxX = 0
            maxY = 0
            Return False
        End If
        If gRecipeEdit.Pattern Is Nothing Then
            minX = 0
            minY = 0
            maxX = 0
            maxY = 0
            Return False
        End If
        If gRecipeEdit.strFileName = "" Or index.PatternName = "" Then
            Return False
        End If

        Dim bXz As Double
        Dim bYz As Double
        Dim mXz As Double
        Dim mYz As Double

        '20161010
        PatternMaxMinOnce = True


        Dim mNodeID As String
        With gRecipeEdit

            '[說明]:單Stage內有幾個Pattern
            For j As Integer = 0 To gRecipeEdit.Node(index.StageNo).Keys.Count - 1
                mNodeID = gRecipeEdit.Node(index.StageNo).Keys(j)
                Dim Name As String = gRecipeEdit.Node(index.StageNo)(mNodeID).PatternName
                Dim NodeBasicX As Decimal = gRecipeEdit.Node(index.StageNo)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionX
                Dim NodeBasicY As Decimal = gRecipeEdit.Node(index.StageNo)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionY
                If .Pattern.ContainsKey(Name) Then
                    '[說明]:每個Pattern內有幾個Round
                    For mRoundNo = 0 To .Pattern(Name).Round.Count - 1
                        Dim num As Integer = .Pattern(Name).Round.Count
                        With .Pattern(Name).Round(mRoundNo)
                            If .CStep.Count > 0 Then 'Soni / 2016.08.20 StepCount改為CStep.Count
                                num = .CStep.Count 'Soni / 2016.08.20 StepCount改為CStep.Count
                                For mStepNo = 0 To .CStep.Count - 1 'Soni / 2016.08.20 StepCount改為CStep.Count
                                    With .CStep(mStepNo)
                                        Select Case .StepType
                                            Case eStepFunctionType.Circle2D
                                                '20170215
                                                Dim EndPos(1) As Decimal
                                                Dim Center(1) As Decimal
                                                Center(0) = NodeBasicX + .Circle2D.CenterPosX
                                                Center(1) = NodeBasicY + .Circle2D.CenterPosY
                                                EndPos(0) = NodeBasicX + .Circle2D.Middle2PosX
                                                EndPos(1) = NodeBasicY + .Circle2D.Middle2PosY
                                                Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))

                                                '20170505
                                                bXz = max(NodeBasicX + .Circle2D.CenterPosX + 2 * mRadius, NodeBasicX + .Circle2D.EndPosX + 2 * mRadius)
                                                mXz = min(NodeBasicX + .Circle2D.CenterPosX - 2 * mRadius, NodeBasicX + .Circle2D.EndPosX - 2 * mRadius)
                                                bYz = max(NodeBasicY + .Circle2D.CenterPosY + 2 * mRadius, NodeBasicY + .Circle2D.EndPosY + 2 * mRadius)
                                                mYz = min(NodeBasicY + .Circle2D.CenterPosY - 2 * mRadius, NodeBasicY + .Circle2D.EndPosY - 2 * mRadius)

                                                'bXz = max(NodeBasicX + .Circle2D.CenterPosX, NodeBasicX + .Circle2D.EndPosX)
                                                'mXz = min(NodeBasicX + .Circle2D.CenterPosX, NodeBasicX + .Circle2D.EndPosX)
                                                'bYz = max(NodeBasicY + .Circle2D.CenterPosY, NodeBasicY + .Circle2D.EndPosY)
                                                'mYz = min(NodeBasicY + .Circle2D.CenterPosY, NodeBasicY + .Circle2D.EndPosY)


                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If

                                            Case eStepFunctionType.Arc2D
                                                '20170505
                                                Dim EndPos(1) As Decimal
                                                Dim Center(1) As Decimal
                                                Center(0) = NodeBasicX + .Arc2D.CenterPosX
                                                Center(1) = NodeBasicY + .Arc2D.CenterPosY
                                                EndPos(0) = NodeBasicX + .Arc2D.EndPosX
                                                EndPos(1) = NodeBasicY + .Arc2D.EndPosY
                                                Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))

                                                bXz = max(NodeBasicX + .Arc2D.CenterPosX + 2 * mRadius, NodeBasicX + .Arc2D.EndPosX + 2 * mRadius)
                                                mXz = min(NodeBasicX + .Arc2D.CenterPosX - 2 * mRadius, NodeBasicX + .Arc2D.EndPosX - 2 * mRadius)
                                                bYz = max(NodeBasicY + .Arc2D.CenterPosY + 2 * mRadius, NodeBasicY + .Arc2D.EndPosY + 2 * mRadius)
                                                mYz = min(NodeBasicY + .Arc2D.CenterPosY - 2 * mRadius, NodeBasicY + .Arc2D.EndPosY - 2 * mRadius)

                                                'bXz = max(NodeBasicX + .Arc2D.CenterPosX, NodeBasicX + .Arc2D.EndPosX)
                                                'mXz = min(NodeBasicX + .Arc2D.CenterPosX, NodeBasicX + .Arc2D.EndPosX)
                                                'bYz = max(NodeBasicY + .Arc2D.CenterPosY, NodeBasicY + .Arc2D.EndPosY)
                                                'mYz = min(NodeBasicY + .Arc2D.CenterPosY, NodeBasicY + .Arc2D.EndPosY)

                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If

                                            Case eStepFunctionType.Dots3D
                                                bXz = max(NodeBasicX + .Dots3D.PosX, NodeBasicX + .Dots3D.PosX)
                                                mXz = min(NodeBasicX + .Dots3D.PosX, NodeBasicX + .Dots3D.PosX)
                                                bYz = max(NodeBasicY + .Dots3D.PosY, NodeBasicY + .Dots3D.PosY)
                                                mYz = min(NodeBasicY + .Dots3D.PosY, NodeBasicY + .Dots3D.PosY)

                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If
                                            Case eStepFunctionType.Line3D
                                                bXz = max(NodeBasicX + .Line3D.StartPosX, NodeBasicX + .Line3D.EndPosX)
                                                mXz = min(NodeBasicX + .Line3D.StartPosX, NodeBasicX + .Line3D.EndPosX)
                                                bYz = max(NodeBasicY + .Line3D.StartPosY, NodeBasicY + .Line3D.EndPosY)
                                                mYz = min(NodeBasicY + .Line3D.StartPosY, NodeBasicY + .Line3D.EndPosY)

                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If
                                            Case eStepFunctionType.Circle3D
                                                bXz = max(NodeBasicX + .Circle3D.CenterPosX, NodeBasicX + .Circle3D.EndPosX)
                                                mXz = min(NodeBasicX + .Circle3D.CenterPosX, NodeBasicX + .Circle3D.EndPosX)
                                                bYz = max(NodeBasicY + .Circle3D.CenterPosY, NodeBasicY + .Circle3D.EndPosY)
                                                mYz = min(NodeBasicY + .Circle3D.CenterPosY, NodeBasicY + .Circle3D.EndPosY)

                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If
                                            Case eStepFunctionType.Arc3D
                                                bXz = max(NodeBasicX + .Arc3D.CenterPosX, NodeBasicX + .Arc3D.EndPosX)
                                                mXz = min(NodeBasicX + .Arc3D.CenterPosX, NodeBasicX + .Arc3D.EndPosX)
                                                bYz = max(NodeBasicY + .Arc3D.CenterPosY, NodeBasicY + .Arc3D.EndPosY)
                                                mYz = min(NodeBasicY + .Arc3D.CenterPosY, NodeBasicY + .Arc3D.EndPosY)

                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If
                                            Case eStepFunctionType.Move3D
                                                bXz = max(.Move3D.EndPosX, .Move3D.EndPosX)
                                                mXz = min(.Move3D.EndPosX, .Move3D.EndPosX)
                                                bYz = max(.Move3D.EndPosY, .Move3D.EndPosY)
                                                mYz = min(.Move3D.EndPosY, .Move3D.EndPosY)

                                                '20161010
                                                If PatternMaxMinOnce = True Then
                                                    maxX = bXz
                                                    minX = mXz
                                                    maxY = bYz
                                                    minY = mYz
                                                    PatternMaxMinOnce = False
                                                ElseIf PatternMaxMinOnce = False Then
                                                    maxX = max(maxX, bXz)
                                                    minX = min(minX, mXz)
                                                    maxY = max(maxY, bYz)
                                                    minY = min(minY, mYz)
                                                End If
                                        End Select
                                    End With
                                Next
                            End If
                        End With
                    Next
                End If

            Next
        End With

        Return True
    End Function


    ''' <summary>繪製座標系</summary>
    ''' <param name="draw"></param>
    ''' <remarks></remarks>
    Public Sub DrawCoord(ByRef draw As Graphics, ByVal Width As Decimal, ByVal Height As Decimal)
        Dim bluePen As New Pen(Brushes.Blue, 3)
        Dim GreenPen As New Pen(Brushes.Green, 3)
        Dim RedPen As New Pen(Brushes.Red, 3)
        Select Case gSSystemParameter.CoordType
            Case enmCoordinateRelationType.eGN2
                'TH
                draw.DrawArc(RedPen, 0, 0, 30, 30, 0, 90)
                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 25, 25)
                draw.DrawLine(RedPen, 8, 28, 15, 35)
                draw.DrawLine(RedPen, 8, 32, 15, 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, 10, 10, 35, 10)
                draw.DrawLine(bluePen, 37, 12, 30, 5)
                draw.DrawLine(bluePen, 37, 8, 30, 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, 37, 2)
                '綠色箭頭向下
                draw.DrawLine(GreenPen, 10, 10, 10, 35)
                draw.DrawLine(GreenPen, 12, 37, 5, 30)
                draw.DrawLine(GreenPen, 8, 37, 15, 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, 2, 37)
            Case enmCoordinateRelationType.eDTS
                'TH
                draw.DrawArc(RedPen, 0, CSng(Height) - 30, 30, 30, 0, -90)
                draw.DrawString("C", New Font("Arial", 12), Brushes.Red, 15, CSng(Height) - 45)
                draw.DrawLine(RedPen, 8, CSng(Height) - 28, 15, CSng(Height) - 35)
                draw.DrawLine(RedPen, 8, CSng(Height) - 32, 15, CSng(Height) - 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, 10, CSng(Height) - 10, 35, CSng(Height) - 10)
                draw.DrawLine(bluePen, 37, CSng(Height) - 12, 30, CSng(Height) - 5)
                draw.DrawLine(bluePen, 37, CSng(Height) - 8, 30, CSng(Height) - 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, 37, CSng(Height) - 28)

                '綠色箭頭向上
                draw.DrawLine(GreenPen, 10, CSng(Height) - 10, 10, CSng(Height) - 35)
                draw.DrawLine(GreenPen, 12, CSng(Height) - 37, 5, CSng(Height) - 30)
                draw.DrawLine(GreenPen, 8, CSng(Height) - 37, 15, CSng(Height) - 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, 2, CSng(Height) - 57)
        End Select

    End Sub


    '20170215
    Public Function DrawSingleStepGraphicsTest(ByVal patternID As String, ByVal RoundNo As Integer, ByVal StepNo As Integer, ByVal GraphicsPictureBox As PictureBox, ByVal sys As sSysParam, ByVal index As sLevelIndexCollection) As Boolean
        Dim mRoundNo As Integer
        Dim mStepNo As Integer
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.Black)
        Dim mBitmap As Bitmap
        Dim mScaleHeight As Decimal                              '轉換成畫布大小的Scale
        Dim mScaleWidth As Decimal                               '轉換成畫布大小的Scale
        Dim mGraphicsStartX As Decimal                           '轉換成畫布大小StartX
        Dim mGraphicsStartY As Decimal                           '轉換成畫布大小StartY
        Dim mGraphicsEndX As Decimal                             '轉換成畫布大小EndX
        Dim mGraphicsEndY As Decimal                             '轉換成畫布大小EndY
        Dim mGraphicsWidth As Decimal                            '轉換成畫布大小Width
        Dim mGraphicsHeight As Decimal                           '轉換成畫布大小Height
        Dim mGraphicsRadius As Decimal                           '轉換成畫布大小Radius
        Dim mGraphicsAngle As Decimal                            '轉換成畫布大小Angle
        Dim mShiftX As Decimal
        Dim mShiftY As Decimal
        Dim mPointSize As Integer                                 '單點的大小


        mBitmap = GraphicsPictureBox.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(GraphicsPictureBox.Width), CInt(GraphicsPictureBox.Height))
        End If

        mDraw = Graphics.FromImage(mBitmap)

        'mShiftX = 0
        'mShiftY = 0
        'mScaleWidth = GraphicsPictureBox.Width / 16
        'mScaleHeight = GraphicsPictureBox.Height / 16
        'mPointSize = CInt(GraphicsPictureBox.Width / 100) '繪點大小

        '--- Soni + 2014.10.30 圖型過大時自動調整比例 ---
        Dim maxPosX As Decimal, maxPosY As Decimal, minPosX As Decimal, minPosY As Decimal
        'FindPatternMaxMinOffset(minPosX, minPosY, maxPosX, maxPosY, sys) '找最大最小點
        FindPatternMaxMinTest(minPosX, minPosY, maxPosX, maxPosY, sys, index) '找最大最小點   20161102

        '--- 增加邊界空白 ---
        maxPosX += 3
        maxPosY += 3
        minPosX -= 3
        minPosY -= 3
        '--- 增加邊界空白 ---


        '[說明]:換算比例大小
        GetDrawShiftScale(GraphicsPictureBox, maxPosX, minPosX, maxPosY, minPosY, mShiftX, mShiftY, mScaleWidth, mScaleHeight, mPointSize) 'Soni / 2016.12.07
        'mShiftX = 6.5
        'mShiftY = 6.5
        'mScaleWidth = GraphicsPictureBox.Width / 13
        'mScaleHeight = GraphicsPictureBox.Height / 13
        'mPointSize = CInt(GraphicsPictureBox.Width / 100) '繪點大小

        'If maxPosX * mScaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
        '    mScaleWidth = 0.25 * GraphicsPictureBox.Width / maxPosX
        '    mScaleHeight = mScaleWidth
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        'If -minPosX * mScaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
        '    mScaleWidth = -0.25 * GraphicsPictureBox.Width / minPosX
        '    mScaleHeight = mScaleWidth
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        'If maxPosY * mScaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
        '    mScaleHeight = 0.25 * GraphicsPictureBox.Height / maxPosY
        '    mScaleWidth = mScaleHeight
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        'If -minPosY * mScaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
        '    mScaleHeight = -0.25 * GraphicsPictureBox.Height / minPosY
        '    mScaleWidth = mScaleHeight
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        '--- Soni + 2014.10.30 圖型過大時自動調整比例 ---

        If index.PatternName = "" Then 'Pattern名稱不存在
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If

        '=== Soni + 2016.09.09 Copy from DrawSingleGraphics ===
        If Not gRecipeEdit.Pattern.ContainsKey(index.PatternName) Then 'Pattern清單內不存在指定名稱
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        If gRecipeEdit.Pattern(index.PatternName).Round.Count = 0 Then '數量為零不繪製
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        '=== Soni + 2016.09.09 Copy from DrawSingleGraphics ===

        '[說明]:畫Recipe圖形  先轉換成畫布Size再描畫圖形
        With mPen
            .Width = GraphicsPictureBox.Width / 500
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.Red
        End With

        mBrush.Color = Color.Red
        Dim mNodeID As String

        For i As Integer = index.StageNo To index.StageNo
            For j As Integer = 0 To gRecipeEdit.Node(i).Keys.Count - 1
                mNodeID = gRecipeEdit.Node(i).Keys(j)
                Dim Name As String = gRecipeEdit.Node(i)(mNodeID).PatternName
                Dim NodeBasicX As Decimal = gRecipeEdit.Node(i)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionX
                Dim NodeBasicY As Decimal = gRecipeEdit.Node(i)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionY

                With gRecipeEdit
                    For mRoundNo = 0 To .Pattern(index.PatternName).Round.Count - 1
                        Dim num As Integer = .Pattern(index.PatternName).Round.Count
                        With .Pattern(index.PatternName).Round(mRoundNo)
                            If .StepCount > 0 Then
                                For mStepNo = 0 To .StepCount - 1   '20160907
                                    '20170215
                                    If mStepNo = StepNo And mRoundNo = RoundNo And index.path = mNodeID Then
                                        With .CStep(mStepNo)
                                            Select Case .StepType
                                                Case eStepFunctionType.Dots3D '點
                                                    Select Case gSSystemParameter.CoordType
                                                        Case enmCoordinateRelationType.eGN2
                                                            mGraphicsStartX = (NodeBasicX + .Dots3D.PosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                            mGraphicsStartY = (NodeBasicY + .Dots3D.PosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                        Case enmCoordinateRelationType.eDTS
                                                            mGraphicsEndX = (NodeBasicX + .Dots3D.PosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                            mGraphicsEndY = GraphicsPictureBox.Height - (NodeBasicY + .Dots3D.PosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                    End Select

                                                    mDraw.FillEllipse(mBrush, CInt(mGraphicsEndX), CInt(mGraphicsEndY), mPointSize, mPointSize)
                                                Case eStepFunctionType.Move3D
                                                    Select Case gSSystemParameter.CoordType
                                                        Case enmCoordinateRelationType.eGN2
                                                            mGraphicsEndX = (NodeBasicX + .Move3D.EndPosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                            mGraphicsEndY = (NodeBasicY + .Move3D.EndPosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                        Case enmCoordinateRelationType.eDTS
                                                            mGraphicsEndX = (NodeBasicX + .Move3D.EndPosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                            mGraphicsEndY = GraphicsPictureBox.Height - (NodeBasicY + .Move3D.EndPosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                    End Select

                                                    mDraw.FillEllipse(mBrush, CInt(mGraphicsEndX), CInt(mGraphicsEndY), mPointSize, mPointSize)

                                                Case eStepFunctionType.Line3D
                                                    Select Case gSSystemParameter.CoordType
                                                        Case enmCoordinateRelationType.eGN2
                                                            mGraphicsStartX = (NodeBasicX + .Line3D.StartPosX + mShiftX) * mScaleWidth
                                                            mGraphicsStartY = (NodeBasicY + .Line3D.StartPosY + mShiftY) * mScaleHeight
                                                            mGraphicsEndX = (NodeBasicX + .Line3D.EndPosX + mShiftX) * mScaleWidth
                                                            mGraphicsEndY = (NodeBasicY + .Line3D.EndPosY + mShiftY) * mScaleHeight
                                                        Case enmCoordinateRelationType.eDTS
                                                            mGraphicsStartX = (NodeBasicX + .Line3D.StartPosX + mShiftX) * mScaleWidth
                                                            mGraphicsStartY = GraphicsPictureBox.Height - (NodeBasicY + .Line3D.StartPosY + mShiftY) * mScaleHeight
                                                            mGraphicsEndX = (NodeBasicX + .Line3D.EndPosX + mShiftX) * mScaleWidth
                                                            mGraphicsEndY = GraphicsPictureBox.Height - (NodeBasicY + .Line3D.EndPosY + mShiftY) * mScaleHeight
                                                    End Select

                                                    If mGraphicsStartY = mGraphicsEndY And mGraphicsStartX = mGraphicsEndX Then '共點
                                                        'Jeffadd
                                                        mDraw.FillEllipse(mBrush, CInt(mGraphicsStartX - mPointSize / 2), CInt(mGraphicsStartY - mPointSize / 2), mPointSize, mPointSize)
                                                    Else
                                                        mDraw.DrawLine(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsEndX), CInt(mGraphicsEndY))
                                                    End If
                                                    '20160805
                                                Case eStepFunctionType.Circle2D '圓
                                                    Dim EndPos(1) As Decimal
                                                    Dim Center(1) As Decimal
                                                    Center(0) = NodeBasicX + .Circle2D.CenterPosX
                                                    Center(1) = NodeBasicY + .Circle2D.CenterPosY
                                                    EndPos(0) = NodeBasicX + .Circle2D.Middle2PosX
                                                    EndPos(1) = NodeBasicY + .Circle2D.Middle2PosY
                                                    If Center(0) = EndPos(0) And Center(1) = EndPos(1) Then
                                                    Else
                                                        Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))
                                                        Select Case gSSystemParameter.CoordType
                                                            Case enmCoordinateRelationType.eGN2
                                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                                mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                            Case enmCoordinateRelationType.eDTS
                                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                                mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                        End Select
                                                        mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                        mGraphicsWidth = mRadius * 2 * mScaleWidth
                                                        mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), 0, 360)
                                                    End If

                                                    'DrawArc
                                                    'Pen()
                                                    'Pen，決定弧形的色彩、寬度和樣式。
                                                    'rect()
                                                    'RectangleF 結構，定義橢圓形的邊界。
                                                    'startAngle()
                                                    '以度為單位，依順時針方向測量之從 X 軸到弧形開始點的角度。
                                                    'sweepAngle()
                                                    '以度為單位，依順時針方向測量之從 startAngle 參數到弧形結束點的角度。
                                                    '20160805
                                                Case eStepFunctionType.Arc2D
                                                    Dim AngleX As Double = .Arc2D.Angle
                                                    Dim StartPos(1) As Decimal
                                                    Dim EndPos(1) As Decimal
                                                    Dim Center(1) As Decimal
                                                    Dim CenterF(1) As Decimal

                                                    StartPos(0) = NodeBasicX + .Arc2D.StartPosX
                                                    StartPos(1) = NodeBasicY + .Arc2D.StartPosY
                                                    EndPos(0) = NodeBasicX + .Arc2D.EndPosX
                                                    EndPos(1) = NodeBasicY + .Arc2D.EndPosY

                                                    Dim Circle As Circle
                                                    Dim x, y, z As CPoint
                                                    'Jeffadd 20160615
                                                    x = New CPoint(Val(NodeBasicX + .Arc2D.StartPosX), Val(NodeBasicY + .Arc2D.StartPosY))
                                                    y = New CPoint(Val(NodeBasicX + .Arc2D.MiddlePosX), Val(NodeBasicY + .Arc2D.MiddlePosY))
                                                    z = New CPoint(Val(NodeBasicX + .Arc2D.EndPosX), Val(NodeBasicY + .Arc2D.EndPosY))

                                                    '[說明]:計算Arc圓心座標
                                                    Circle = CMath.GetCircleby3Point(x, y, z)

                                                    Center(0) = Circle.PointX
                                                    Center(1) = Circle.PointY

                                                    Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))

                                                    mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                    mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight

                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth

                                                    '起始值
                                                    '  Dim StartAngle As Integer = CInt(GetAngleJeffTest(StartPos(0), StartPos(1), Center(0), Center(1)))

                                                    Dim StartAngle As Integer = CInt(GetAngleJeffTest(Center(0), Center(1), StartPos(0), StartPos(1)))
                                                    Dim StartAngleTest As Integer
                                                    Dim EndAngleTest As Integer
                                                    StartAngleTest = -StartAngle
                                                    'If StartAngle > 0 Then
                                                    '    'StartAngleTest = 180 - StartAngle
                                                    '    StartAngleTest = -StartAngle
                                                    'Else
                                                    '    StartAngleTest = -(StartAngle) '+ 180
                                                    'End If

                                                    Circle.clockwise = Not Circle.clockwise


                                                    If Circle.clockwise = True Then
                                                        EndAngleTest = -Circle.Angle
                                                    ElseIf Circle.clockwise = False Then
                                                        '    'Dim Middle As Integer
                                                        '    'EndAngleTest = StartAngleTest - Circle.Angle
                                                        '    'Middle = EndAngleTest
                                                        '    'StartAngleTest = Middle

                                                        EndAngleTest = Circle.Angle
                                                    End If


                                                    '結束值
                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), StartAngleTest, EndAngleTest)
                                                    '  mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), CInt(StartAngle), CInt(Circle.Angle))
                                                Case eStepFunctionType.Circle3D
                                                    Dim EndPos(1) As Decimal
                                                    Dim Center(1) As Decimal
                                                    Center(0) = NodeBasicX + .Circle3D.CenterPosX
                                                    Center(1) = NodeBasicY + .Circle3D.CenterPosY
                                                    EndPos(0) = NodeBasicX + .Circle3D.EndPosX
                                                    EndPos(1) = NodeBasicY + .Circle3D.EndPosY
                                                    If Center(0) = EndPos(0) And Center(1) = EndPos(1) Then
                                                    Else
                                                        Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))
                                                        Select Case gSSystemParameter.CoordType
                                                            Case enmCoordinateRelationType.eGN2
                                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                                mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY - mRadius) * mScaleHeight
                                                            Case enmCoordinateRelationType.eDTS
                                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                                mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                        End Select
                                                        mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                        mGraphicsWidth = mRadius * 2 * mScaleWidth
                                                        mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), 0, 360)
                                                    End If

                                                Case eStepFunctionType.Arc3D

                                                    '[說明]:配合劃膠之角度轉換 實際劃膠起始點為180(-1,0)順時針 
                                                    '描繪的圖形起始點為0(1,0)順時針 起始點為矩形的為左上角之座標
                                                    '(StartX,StartY)定義為圓心之座標
                                                    If .Arc3D.Angle >= 180 Then
                                                        mGraphicsAngle = (.Arc3D.Angle + 180) Mod 360 + 180
                                                    Else
                                                        mGraphicsAngle = (.Arc3D.Angle + 180) Mod 360 - 180
                                                    End If

                                                    Dim StartPos(1) As Decimal
                                                    Dim EndPos(1) As Decimal
                                                    Dim Center(1) As Decimal

                                                    StartPos(0) = NodeBasicX + .Arc3D.StartPosX
                                                    StartPos(1) = NodeBasicY + .Arc3D.StartPosY
                                                    EndPos(0) = NodeBasicX + .Arc3D.EndPosX
                                                    EndPos(1) = NodeBasicY + .Arc3D.EndPosY
                                                    If StartPos(0) = EndPos(0) And StartPos(1) = EndPos(1) Then
                                                    Else
                                                        '計算原點座標(圓心) 正向和反向 利用向量計算
                                                        If gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle >= 0 Then
                                                            CenterCalculate(StartPos, EndPos, Math.Abs(gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 0)
                                                        Else
                                                            CenterCalculate(StartPos, EndPos, Math.Abs(gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 1)
                                                        End If

                                                        Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))
                                                        Select Case gSSystemParameter.CoordType
                                                            Case enmCoordinateRelationType.eGN2
                                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                                mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
                                                            Case enmCoordinateRelationType.eDTS
                                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                                mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                        End Select

                                                        mGraphicsEndX = (NodeBasicX + .Arc3D.EndPosX + mShiftX) * mScaleWidth
                                                        mGraphicsEndY = (NodeBasicY + .Arc3D.EndPosY + mShiftY) * mScaleHeight

                                                        mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                        mGraphicsWidth = mRadius * 2 * mScaleWidth
                                                        '起始值
                                                        Dim StartAngle As Integer = CInt(GetAngle(StartPos(0), StartPos(1), Center(0), Center(1)))
                                                        '結束值
                                                        mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsHeight), CInt(mGraphicsHeight), StartAngle, CInt(mGraphicsAngle))

                                                        '判斷正反轉
                                                        If gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle > 0 Then
                                                            '正轉
                                                            If mGraphicsAngle > StartAngle Then
                                                                mGraphicsAngle -= StartAngle
                                                            Else
                                                                mGraphicsAngle += (360 - StartAngle)
                                                            End If

                                                        Else
                                                            '反轉
                                                            If (mGraphicsAngle - StartAngle) > 0 Then
                                                                mGraphicsAngle -= (360 + StartAngle)
                                                            Else
                                                                mGraphicsAngle -= StartAngle
                                                            End If


                                                        End If
                                                        mDraw.DrawArc(mPen, CInt(mGraphicsStartX - mGraphicsRadius), CInt(mGraphicsStartY - mGraphicsRadius), CInt(mGraphicsRadius) * 2, CInt(mGraphicsRadius) * 2, 180, CInt(mGraphicsAngle))
                                                    End If

                                            End Select
                                        End With


                                    End If
                                Next
                            End If
                        End With
                    Next
                End With
            Next
        Next


        GraphicsPictureBox.Image = mBitmap

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="GraphicsPictureBox">繪製用介面控制項</param>
    ''' <param name="maxPosX">最大物理座標X</param>
    ''' <param name="minPosX">最小物理座標X</param>
    ''' <param name="maxPosY">最大物理座標Y</param>
    ''' <param name="minPosY">最小物理座標Y</param>
    ''' <param name="shiftX">特徵中心點物理座標反向修正</param>
    ''' <param name="shiftY">特徵中心點物理座標反向修正</param>
    ''' <param name="scaleWidth">繪製縮放比例</param>
    ''' <param name="scaleHeight">繪製縮放比例</param>
    ''' <param name="pointSize">繪點尺寸</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDrawShiftScale(ByRef GraphicsPictureBox As PictureBox, ByVal maxPosX As Double, ByVal minPosX As Double, ByVal maxPosY As Double, ByVal minPosY As Double, ByRef shiftX As Double, ByRef shiftY As Double, ByRef scaleWidth As Double, ByRef scaleHeight As Double, ByRef pointSize As Integer) As Boolean


        '[說明]:換算比例大小

        '預設值
        shiftX = 6.5
        shiftY = 6.5
        scaleWidth = GraphicsPictureBox.Width / 13
        scaleHeight = GraphicsPictureBox.Height / 13
        pointSize = CInt(GraphicsPictureBox.Width / 100) '繪點大小
        '       Dim lineWidthCoef As Double = 44.8


        Dim RangeX As Double = maxPosX - minPosX '範圍
        Dim RangeY As Double = maxPosY - minPosY
        scaleWidth = GraphicsPictureBox.Width / RangeX
        scaleHeight = GraphicsPictureBox.Height / RangeY
        If scaleWidth < scaleHeight Then scaleHeight = scaleWidth
        If scaleHeight < scaleWidth Then scaleWidth = scaleHeight
        shiftX = GraphicsPictureBox.Width * 0.5 / scaleWidth - (maxPosX + minPosX) / 2
        shiftY = GraphicsPictureBox.Height * 0.5 / scaleHeight - (maxPosY + minPosY) / 2

        Return True
        '20161028
        If maxPosX * scaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
            scaleWidth = 0.25 * GraphicsPictureBox.Width / maxPosX
            scaleHeight = scaleWidth
            shiftX = GraphicsPictureBox.Width * 0.5 / scaleWidth
            shiftY = GraphicsPictureBox.Height * 0.5 / scaleHeight
        End If
        If -minPosX * scaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
            scaleWidth = -0.25 * GraphicsPictureBox.Width / minPosX
            scaleHeight = scaleWidth
            shiftX = GraphicsPictureBox.Width * 0.5 / scaleWidth
            shiftY = GraphicsPictureBox.Height * 0.5 / scaleHeight
        End If
        If maxPosY * scaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
            scaleHeight = 0.25 * GraphicsPictureBox.Height / maxPosY
            scaleWidth = scaleHeight
            shiftX = GraphicsPictureBox.Width * 0.5 / scaleWidth
            shiftY = GraphicsPictureBox.Height * 0.5 / scaleHeight
        End If
        If -minPosY * scaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
            scaleHeight = -0.25 * GraphicsPictureBox.Height / minPosY
            scaleWidth = scaleHeight
            shiftX = GraphicsPictureBox.Width * 0.5 / scaleWidth
            shiftY = GraphicsPictureBox.Height * 0.5 / scaleHeight
        End If
        Return True
    End Function

    '20170215
    Public Function DrawSingleGraphicsTest(ByVal GraphicsPictureBox As PictureBox, ByVal sys As sSysParam, ByVal index As sLevelIndexCollection) As Boolean

        Dim mRoundNo As Integer
        Dim mStepNo As Integer
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.Black)
        Dim mBitmap As Bitmap
        Dim mScaleHeight As Decimal                              '轉換成畫布大小的Scale
        Dim mScaleWidth As Decimal                               '轉換成畫布大小的Scale
        Dim mGraphicsStartX As Decimal                           '轉換成畫布大小StartX
        Dim mGraphicsStartY As Decimal                           '轉換成畫布大小StartY
        Dim mGraphicsEndX As Decimal                             '轉換成畫布大小EndX
        Dim mGraphicsEndY As Decimal                             '轉換成畫布大小EndY
        Dim mGraphicsWidth As Decimal                            '轉換成畫布大小Width
        Dim mGraphicsHeight As Decimal                           '轉換成畫布大小Height
        Dim mGraphicsRadius As Decimal                           '轉換成畫布大小Radius
        Dim mGraphicsAngle As Decimal                            '轉換成畫布大小Angle
        Dim mShiftX As Decimal
        Dim mShiftY As Decimal
        Dim mPointSize As Integer                                 '單點的大小


        mBitmap = GraphicsPictureBox.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(GraphicsPictureBox.Width), CInt(GraphicsPictureBox.Height))
        End If

        mDraw = Graphics.FromImage(mBitmap)

        '[說明]:畫外框(chuck大小)
        mDraw.FillRectangle(mBrush, 0, 0, CInt(GraphicsPictureBox.Width) - 1, CInt(GraphicsPictureBox.Height) - 1)


        With mPen
            .Width = 3
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.Black
        End With
        mDraw.DrawRectangle(mPen, 0, 0, CInt(GraphicsPictureBox.Width) - 1, CInt(GraphicsPictureBox.Height) - 1)


        '--- Soni + 2014.10.30 圖型過大時自動調整比例 ---
        Dim maxPosX As Decimal, maxPosY As Decimal, minPosX As Decimal, minPosY As Decimal

        FindPatternMaxMinTest(minPosX, minPosY, maxPosX, maxPosY, sys, index) '找最大最小點

        '--- 增加邊界空白 ---
        maxPosX += 3
        maxPosY += 3
        minPosX -= 3
        minPosY -= 3
        '--- 增加邊界空白 ---


        ''[說明]:換算比例大小
        GetDrawShiftScale(GraphicsPictureBox, maxPosX, minPosX, maxPosY, minPosY, mShiftX, mShiftY, mScaleWidth, mScaleHeight, mPointSize) 'Soni / 2016.12.07
        'mShiftX = 6.5
        'mShiftY = 6.5
        'mScaleWidth = GraphicsPictureBox.Width / 13
        'mScaleHeight = GraphicsPictureBox.Height / 13
        'mPointSize = CInt(GraphicsPictureBox.Width / 100) '繪點大小
        ''       Dim lineWidthCoef As Double = 44.8


        ''20161028
        'If maxPosX * mScaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
        '    mScaleWidth = 0.25 * GraphicsPictureBox.Width / maxPosX
        '    mScaleHeight = mScaleWidth
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        'If -minPosX * mScaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
        '    mScaleWidth = -0.25 * GraphicsPictureBox.Width / minPosX
        '    mScaleHeight = mScaleWidth
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        'If maxPosY * mScaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
        '    mScaleHeight = 0.25 * GraphicsPictureBox.Height / maxPosY
        '    mScaleWidth = mScaleHeight
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If
        'If -minPosY * mScaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
        '    mScaleHeight = -0.25 * GraphicsPictureBox.Height / minPosY
        '    mScaleWidth = mScaleHeight
        '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
        '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
        'End If

        ''--- Soni + 2014.10.30 圖型過大時自動調整比例 ---


        'mScaleHeight = GraphicsPictureBox.Height / (maxPosY - minPosY)
        'mScaleWidth = GraphicsPictureBox.Width / (maxPosX - minPosX)
        'If mScaleHeight > mScaleWidth Then mScaleHeight = mScaleWidth '兩者相比取其小
        'If mScaleWidth > mScaleHeight Then mScaleWidth = mScaleHeight
        'mShiftX = -minPosX / (maxPosX - minPosX) * GraphicsPictureBox.Width / mScaleWidth
        'mShiftY = maxPosY / (maxPosY - minPosY) * GraphicsPictureBox.Height / mScaleHeight



        'mScaleHeight = -mScaleHeight
        'mShiftY = -mShiftY

        ''-----------------------------------------------------------------------
        'lineWidthCoef = Math.Abs(135 / mScaleHeight)
        ''--- #001座標翻轉 ---
        'mScaleHeight = -mScaleHeight
        'mShiftY = -mShiftY
        ''--- #001座標翻轉 ---

        ''[說明]:畫Recipe圖形  先轉換成畫布Size再描畫圖形
        'With mPen
        '    .Width = GraphicsPictureBox.Width / lineWidthCoef
        '    .DashStyle = Drawing2D.DashStyle.Solid
        '    .Color = Color.White
        'End With
        'mPointSize = CInt(GraphicsPictureBox.Width / lineWidthCoef)
        ''-----------------------------------------------------------------------


        If sys Is Nothing Then 'Soni + 2016.09.14 外部未傳入sys!!??
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        If sys.SelectValve < 0 Then '閥號不可用
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        If sys.StageNo >= gRecipeEdit.StageNodeID.Count Then '閥號不可用
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        If index.PatternName = "" Then 'Pattern名稱不存在
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        If Not gRecipeEdit.Pattern.ContainsKey(index.PatternName) Then 'Pattern清單內不存在指定名稱
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If
        If gRecipeEdit.Pattern(index.PatternName).Round.Count = 0 Then '數量為零不繪製
            DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
            GraphicsPictureBox.Image = mBitmap
            Return True
        End If

        '保護20170703
        If gRecipeEdit.Node(index.StageNo).Count = 0 Then
            Return True
        End If

        If index.StageNo = -1 Then
            Return True
        End If

        If index.path = "" Then
            Return True
        End If


        Dim mNodeID As String
        '劃中心線_以stage上層Pattern 的 Basix Pos 為主
        With mPen
            .Width = GraphicsPictureBox.Width / 250
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.LightGreen
        End With
        If gRecipeEdit.Node(index.StageNo).ContainsKey(index.path) Then
            Dim BasicX As Decimal = (gRecipeEdit.Node(index.StageNo)(index.path).ConveyorPos(sys.ConveyorNo).BasicPositionX + mShiftX) * mScaleWidth
            Dim BasicY As Decimal = GraphicsPictureBox.Height - ((gRecipeEdit.Node(index.StageNo)(index.path).ConveyorPos(sys.ConveyorNo).BasicPositionY + mShiftY) * mScaleHeight)
            mDraw.DrawLine(mPen, 0, CInt(BasicY), GraphicsPictureBox.Width, CInt(BasicY))
            mDraw.DrawLine(mPen, CInt(BasicX), 0, CInt(BasicX), GraphicsPictureBox.Height)
        Else

        End If

        '繪製座標系
        DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)


        '[說明]:畫Recipe圖形  先轉換成畫布Size再描畫圖形
        With mPen
            .Width = GraphicsPictureBox.Width / 500
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.White
        End With
        mBrush.Color = Color.White

        For i As Integer = index.StageNo To index.StageNo
            For j As Integer = 0 To gRecipeEdit.Node(i).Keys.Count - 1
                mNodeID = gRecipeEdit.Node(i).Keys(j)
                Dim Name As String = gRecipeEdit.Node(i)(mNodeID).PatternName
                Dim NodeBasicX As Decimal = gRecipeEdit.Node(i)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionX
                Dim NodeBasicY As Decimal = gRecipeEdit.Node(i)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionY
                With gRecipeEdit
                    For mRoundNo = 0 To .Pattern(Name).Round.Count - 1
                        Dim num As Integer = .Pattern(Name).Round.Count
                        With .Pattern(Name).Round(mRoundNo)
                            If .StepCount > 0 Then
                                For mStepNo = 0 To .StepCount - 1 'Soni / 2016.09.22 第0筆沒畫到
                                    With .CStep(mStepNo)
                                        Select Case .StepType
                                            Case eStepFunctionType.Dots3D '點
                                                Select Case gSSystemParameter.CoordType
                                                    Case enmCoordinateRelationType.eGN2

                                                        mGraphicsStartX = (NodeBasicX + .Dots3D.PosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                        mGraphicsStartY = (NodeBasicY + .Dots3D.PosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                    Case enmCoordinateRelationType.eDTS
                                                        mGraphicsEndX = (NodeBasicX + .Dots3D.PosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                        mGraphicsEndY = GraphicsPictureBox.Height - (NodeBasicY + .Dots3D.PosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                End Select

                                                mDraw.FillEllipse(mBrush, CInt(mGraphicsEndX), CInt(mGraphicsEndY), mPointSize, mPointSize)
                                            Case eStepFunctionType.Move3D
                                                Select Case gSSystemParameter.CoordType
                                                    Case enmCoordinateRelationType.eGN2
                                                        mGraphicsEndX = (NodeBasicX + .Move3D.EndPosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                        mGraphicsEndY = (NodeBasicY + .Move3D.EndPosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                    Case enmCoordinateRelationType.eDTS
                                                        mGraphicsEndX = (NodeBasicX + .Move3D.EndPosX + mShiftX) * mScaleWidth - mPointSize * 0.5
                                                        mGraphicsEndY = GraphicsPictureBox.Height - (NodeBasicY + .Move3D.EndPosY + mShiftY) * mScaleHeight - mPointSize * 0.5
                                                End Select

                                                mDraw.FillEllipse(mBrush, CInt(mGraphicsEndX), CInt(mGraphicsEndY), mPointSize, mPointSize)

                                            Case eStepFunctionType.Line3D '線
                                                Select Case gSSystemParameter.CoordType
                                                    Case enmCoordinateRelationType.eGN2
                                                        mGraphicsStartX = (NodeBasicX + .Line3D.StartPosX + mShiftX) * mScaleWidth
                                                        mGraphicsStartY = (NodeBasicY + .Line3D.StartPosY + mShiftY) * mScaleHeight
                                                        mGraphicsEndX = (NodeBasicX + .Line3D.EndPosX + mShiftX) * mScaleWidth
                                                        mGraphicsEndY = (NodeBasicY + .Line3D.EndPosY + mShiftY) * mScaleHeight
                                                    Case enmCoordinateRelationType.eDTS
                                                        Dim SY As Double = .Line3D.StartPosY
                                                        Dim EY As Double = .Line3D.EndPosY
                                                        mGraphicsStartX = (NodeBasicX + .Line3D.StartPosX + mShiftX) * mScaleWidth
                                                        mGraphicsStartY = GraphicsPictureBox.Height - (NodeBasicY + .Line3D.StartPosY + mShiftY) * mScaleHeight
                                                        mGraphicsEndX = (NodeBasicX + .Line3D.EndPosX + mShiftX) * mScaleWidth
                                                        mGraphicsEndY = GraphicsPictureBox.Height - (NodeBasicY + .Line3D.EndPosY + mShiftY) * mScaleHeight
                                                End Select

                                                If mGraphicsStartY = mGraphicsEndY And mGraphicsStartX = mGraphicsEndX Then '共點
                                                    'Jeffadd
                                                    mDraw.FillEllipse(mBrush, CInt(mGraphicsStartX - mPointSize / 2), CInt(mGraphicsStartY - mPointSize / 2), mPointSize, mPointSize)
                                                Else
                                                    mDraw.DrawLine(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsEndX), CInt(mGraphicsEndY))
                                                End If
                                                '20160805
                                            Case eStepFunctionType.Circle2D '圓
                                                Dim EndPos(1) As Decimal
                                                Dim Center(1) As Decimal
                                                Center(0) = NodeBasicX + .Circle2D.CenterPosX
                                                Center(1) = NodeBasicY + .Circle2D.CenterPosY
                                                EndPos(0) = NodeBasicX + .Circle2D.Middle2PosX
                                                EndPos(1) = NodeBasicY + .Circle2D.Middle2PosY
                                                If Center(0) = EndPos(0) And Center(1) = EndPos(1) Then
                                                Else
                                                    Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))
                                                    Select Case gSSystemParameter.CoordType
                                                        Case enmCoordinateRelationType.eGN2
                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
                                                        Case enmCoordinateRelationType.eDTS
                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                            mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                    End Select
                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth
                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), 0, 360)
                                                End If

                                                '20160808  
                                            Case eStepFunctionType.Arc2D
                                                Dim AngleX As Double = .Arc2D.Angle
                                                'Pen()
                                                'Type:                                   System.Drawing.Pen()
                                                'Pen that determines the color, width, and style of the arc.
                                                'x()
                                                'Type:                                   System.Single()
                                                'The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.
                                                'y()
                                                'Type:                                   System.Single()
                                                'The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.
                                                'width()
                                                'Type:                                   System.Single()
                                                'Width of the rectangle that defines the ellipse.
                                                'height()
                                                'Type:                                   System.Single()
                                                'Height of the rectangle that defines the ellipse.
                                                'startAngle()
                                                'Type:                                   System.Single()
                                                'Angle in degrees measured clockwise from the x-axis to the starting point of the arc.
                                                'sweepAngle()
                                                'Type:                                   System.Single()
                                                'Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.
                                                Dim StartPos(1) As Decimal
                                                Dim EndPos(1) As Decimal
                                                Dim Center(1) As Decimal
                                                Dim CenterF(1) As Decimal
                                                StartPos(0) = NodeBasicX + .Arc2D.StartPosX
                                                StartPos(1) = NodeBasicY + .Arc2D.StartPosY
                                                EndPos(0) = NodeBasicX + .Arc2D.EndPosX
                                                EndPos(1) = NodeBasicY + .Arc2D.EndPosY

                                                Dim Circle As Circle
                                                Dim x, y, z As CPoint
                                                'Jeffadd 20160615
                                                x = New CPoint(Val(NodeBasicX + .Arc2D.StartPosX), Val(NodeBasicY + .Arc2D.StartPosY))
                                                y = New CPoint(Val(NodeBasicX + .Arc2D.MiddlePosX), Val(NodeBasicY + .Arc2D.MiddlePosY))
                                                z = New CPoint(Val(NodeBasicX + .Arc2D.EndPosX), Val(NodeBasicY + .Arc2D.EndPosY))

                                                '[說明]:計算Arc圓心座標
                                                Circle = CMath.GetCircleby3Point(x, y, z)

                                                Center(0) = Circle.PointX
                                                Center(1) = Circle.PointY

                                                Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))

                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight

                                                mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                mGraphicsWidth = mRadius * 2 * mScaleWidth

                                                '起始值
                                                Dim StartAngle As Integer = CInt(GetAngleJeffTest(Center(0), Center(1), StartPos(0), StartPos(1)))
                                                Dim StartAngleTest As Integer
                                                Dim EndAngleTest As Integer
                                                StartAngleTest = -StartAngle
                                                'If StartAngle > 0 Then
                                                '    StartAngleTest = 180 - StartAngle
                                                'Else
                                                '    StartAngleTest = -(StartAngle) + 180
                                                'End If

                                                Circle.clockwise = Not Circle.clockwise


                                                If Circle.clockwise = True Then
                                                    EndAngleTest = -Circle.Angle
                                                ElseIf Circle.clockwise = False Then
                                                    '    'Dim Middle As Integer
                                                    '    'EndAngleTest = StartAngleTest - Circle.Angle
                                                    '    'Middle = EndAngleTest
                                                    '    'StartAngleTest = Middle

                                                    EndAngleTest = Circle.Angle
                                                End If


                                                '結束值
                                                mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), StartAngleTest, EndAngleTest)
                                                '  mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), CInt(StartAngle), CInt(Circle.Angle))
                                            Case eStepFunctionType.Circle3D
                                                Dim EndPos(1) As Decimal
                                                Dim Center(1) As Decimal
                                                Center(0) = NodeBasicX + .Circle3D.CenterPosX
                                                Center(1) = NodeBasicY + .Circle3D.CenterPosY
                                                EndPos(0) = NodeBasicX + .Circle3D.EndPosX
                                                EndPos(1) = NodeBasicY + .Circle3D.EndPosY
                                                If Center(0) = EndPos(0) And Center(1) = EndPos(1) Then
                                                Else
                                                    Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))
                                                    Select Case gSSystemParameter.CoordType
                                                        Case enmCoordinateRelationType.eGN2
                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
                                                        Case enmCoordinateRelationType.eDTS
                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                            mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                    End Select
                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth
                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), 0, 360)
                                                End If

                                            Case eStepFunctionType.Arc3D

                                                '[說明]:配合劃膠之角度轉換 實際劃膠起始點為180(-1,0)順時針 
                                                '描繪的圖形起始點為0(1,0)順時針 起始點為矩形的為左上角之座標
                                                '(StartX,StartY)定義為圓心之座標
                                                If .Arc3D.Angle >= 180 Then
                                                    mGraphicsAngle = (.Arc3D.Angle + 180) Mod 360 + 180
                                                Else
                                                    mGraphicsAngle = (.Arc3D.Angle + 180) Mod 360 - 180
                                                End If

                                                Dim StartPos(1) As Decimal
                                                Dim EndPos(1) As Decimal
                                                Dim Center(1) As Decimal

                                                StartPos(0) = NodeBasicX + .Arc3D.StartPosX
                                                StartPos(1) = NodeBasicY + .Arc3D.StartPosY
                                                EndPos(0) = NodeBasicX + .Arc3D.EndPosX
                                                EndPos(1) = NodeBasicY + .Arc3D.EndPosY
                                                If StartPos(0) = EndPos(0) And StartPos(1) = EndPos(1) Then
                                                Else
                                                    '計算原點座標(圓心) 正向和反向 利用向量計算
                                                    If gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle >= 0 Then
                                                        CenterCalculate(StartPos, EndPos, Math.Abs(gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 0)
                                                    Else
                                                        CenterCalculate(StartPos, EndPos, Math.Abs(gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 1)
                                                    End If

                                                    Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))
                                                    Select Case gSSystemParameter.CoordType
                                                        Case enmCoordinateRelationType.eGN2
                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
                                                        Case enmCoordinateRelationType.eDTS
                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
                                                            mGraphicsStartY = GraphicsPictureBox.Height - (Center(1) + mShiftY + mRadius) * mScaleHeight
                                                    End Select

                                                    mGraphicsEndX = (.Arc3D.EndPosX + mShiftX) * mScaleWidth
                                                    mGraphicsEndY = (.Arc3D.EndPosY + mShiftY) * mScaleHeight

                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth
                                                    '起始值
                                                    Dim StartAngle As Integer = CInt(GetAngle(StartPos(0), StartPos(1), Center(0), Center(1)))
                                                    '結束值
                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsHeight), CInt(mGraphicsHeight), StartAngle, CInt(mGraphicsAngle))

                                                    '判斷正反轉
                                                    If gRecipeEdit.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle > 0 Then
                                                        '正轉
                                                        If mGraphicsAngle > StartAngle Then
                                                            mGraphicsAngle -= StartAngle
                                                        Else
                                                            mGraphicsAngle += (360 - StartAngle)
                                                        End If

                                                    Else
                                                        '反轉
                                                        If (mGraphicsAngle - StartAngle) > 0 Then
                                                            mGraphicsAngle -= (360 + StartAngle)
                                                        Else
                                                            mGraphicsAngle -= StartAngle
                                                        End If
                                                    End If

                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX - mGraphicsRadius), CInt(mGraphicsStartY - mGraphicsRadius), CInt(mGraphicsRadius) * 2, CInt(mGraphicsRadius) * 2, 180, CInt(mGraphicsAngle))
                                                End If
                                        End Select
                                    End With
                                Next
                            End If
                        End With
                    Next
                End With
            Next
        Next

        GraphicsPictureBox.Image = mBitmap

        Return True


    End Function

End Class


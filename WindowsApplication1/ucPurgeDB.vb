Imports ProjectRecipe
Imports ProjectCore

Public Class ucPurgeDB

    Public RecipeEdit As CRecipe

    Public Sub SetValve(ByVal valveMax As enmValve)
        Select Case valveMax
            Case enmValve.No1
                lblValve1.Visible = True
                lblValve2.Visible = False
                lblValve3.Visible = False
                lblValve4.Visible = False
                cmbValvePurge1.Visible = True
                cmbValvePurge2.Visible = False
                cmbValvePurge3.Visible = False
                cmbValvePurge4.Visible = False
            Case enmValve.No2
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = False
                lblValve4.Visible = False
                cmbValvePurge1.Visible = True
                cmbValvePurge2.Visible = True
                cmbValvePurge3.Visible = False
                cmbValvePurge4.Visible = False
            Case enmValve.No3
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = True
                lblValve4.Visible = False
                cmbValvePurge1.Visible = True
                cmbValvePurge2.Visible = True
                cmbValvePurge3.Visible = True
                cmbValvePurge4.Visible = False
            Case enmValve.No4
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = True
                lblValve4.Visible = True
                cmbValvePurge1.Visible = True
                cmbValvePurge2.Visible = True
                cmbValvePurge3.Visible = True
                cmbValvePurge4.Visible = True
        End Select

    End Sub

    Public Function GetString(ByVal value As String) As String
        Select Case value
            Case "Purge"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "排胶"
                    Case enmLanguageType.eTraditionalChinese
                        Return "排膠"
                End Select
            Case "NozzleCleaner"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "喷嘴清洁"
                    Case enmLanguageType.eTraditionalChinese
                        Return "噴嘴清潔"
                End Select
            Case "AugerCleaner"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "针嘴清洁"
                    Case enmLanguageType.eTraditionalChinese
                        Return "針嘴清潔"
                End Select
                '20161129
            Case "PurgeNozzleCleaner"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "排胶喷嘴清洁"
                    Case enmLanguageType.eTraditionalChinese
                        Return "排膠噴嘴清潔"
                End Select
            Case "PurgeAugerCleaner"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "排胶针嘴清洁"
                    Case enmLanguageType.eTraditionalChinese
                        Return "排膠針嘴清潔"
                End Select

                'Sue20170523
            Case "None"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "不执行"
                    Case enmLanguageType.eTraditionalChinese
                        Return "不執行"
                End Select
            Case "Timer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "时间"
                    Case enmLanguageType.eTraditionalChinese
                        Return "時間"
                End Select
            Case "Runs"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "次数"
                    Case enmLanguageType.eTraditionalChinese
                        Return "次數"
                End Select
            Case "Timer & Runs"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eSimplifiedChinese
                        Return "时间 & 次数"
                    Case enmLanguageType.eTraditionalChinese
                        Return "時間 & 次數"
                End Select
        End Select

        Return value
    End Function

    ''' <summary>
    ''' 新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If txtName.Text.Trim = "" Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Parent)) Then
                Me.BeginInvoke(Sub()
                                   txtName.BackColor = Color.Yellow
                                   txtName.Refresh() 'Soni / 2017.05.10
                                   System.Threading.Thread.Sleep(300)
                                   txtName.BackColor = Color.White
                               End Sub)
            End If
            Exit Sub
        End If
        Dim mPurgeDB As New CPurgeParameter(txtName.Text)
        mPurgeDB.Name = txtName.Text
        If IsillegalFileName(mPurgeDB.Name) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return
        Else
            If gPurgeDB.ContainsKey(mPurgeDB.Name) Then
                If MsgBox("Weight Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstItem.SelectedItem = mPurgeDB.Name
                    GetPurgeDB(gPurgeDB(lstItem.SelectedItem))
                End If
                Exit Sub
            End If

            If GetPurgeDB(mPurgeDB) Then
                gPurgeDB.Add(mPurgeDB.Name, mPurgeDB)
                Dim folderPath As String = Application.StartupPath & "\Database\Purge\"
                Dim fileName As String = folderPath & mPurgeDB.Name & ".pdb"
                If System.IO.File.Exists(fileName) Then
                    If MsgBox("Weight File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                        mPurgeDB.Save(fileName)
                    End If
                Else
                    mPurgeDB.Save(fileName)
                End If

                'Jeffadd 20160805
                UpdateNewNamePurgeUI(mPurgeDB.Name)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lstItem.SelectedIndex < 0 Then
            lstItem.BackColor = Color.Yellow
            lstItem.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstItem.BackColor = Color.White
            lstItem.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Dim folderPath As String = Application.StartupPath & "\Database\Purge\" '噴射閥資料
        Dim fileName As String = folderPath & lstItem.SelectedItem & ".pdb"

        'jimmy 20170704
        Dim bDBDelectCheck As Boolean = False
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No3).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No4).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case Else
                Select Case gSSystemParameter.StageUseValveCount

                    Case eMechanismModule.OneValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                    Case eMechanismModule.TwoValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) = lstItem.SelectedItem Or
                            RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve2) = lstItem.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                End Select

        End Select

        If bDBDelectCheck = True Then
            If System.IO.File.Exists(fileName) Then
                System.IO.File.Delete(fileName)
            End If
            gPurgeDB.Remove(lstItem.SelectedItem)
            UpdateNewNamePurgeUI(lstItem.SelectedItem)

            If Not gPurgeDB.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
                gPurgeDB.Add("Default", New CPurgeParameter("Default"))
                gPurgeDB("Default").Save(folderPath & "Default.pdb")
                UpdateNewNamePurgeUI(lstItem.SelectedItem)
            End If
        Else
            MessageBox.Show("Operation Could Not be Completed")
        End If


    End Sub
    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If lstItem.SelectedIndex < 0 Then
            lstItem.BackColor = Color.Yellow
            lstItem.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstItem.BackColor = Color.White
            lstItem.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If GetPurgeDB(gPurgeDB(lstItem.SelectedItem)) Then
            Dim folderPath As String = Application.StartupPath & "\Database\Purge\" '溫控資料
            Dim fileName As String = folderPath & gPurgeDB(lstItem.SelectedItem).Name & ".pdb"
            gPurgeDB(lstItem.SelectedItem).Save(fileName)

        End If
    End Sub

    ''' <summary>
    ''' 介面到物件
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurgeDB(ByRef item As CPurgeParameter) As Boolean

        'Jeffadd 20160726
        With item
            'Sue 20170523
            If cboRunType.SelectedIndex = 0 Then
                .BaseOn = eInspectionType.Noen
            ElseIf cboRunType.SelectedIndex = 1 Then
                .BaseOn = eInspectionType.OnTimer
            ElseIf cboRunType.SelectedIndex = 2 Then
                .BaseOn = eInspectionType.OnRuns
            ElseIf cboRunType.SelectedIndex = 3 Then
                .BaseOn = eInspectionType.OnTimerOrRuns
            End If
            .OnRuns = Val(nmuOnRuns.Value)
            .OnTimer = Val(nmuOnTimer.Value)
            Select Case cboCleanType.SelectedIndex
                Case 0
                    .CleanType = eCleanType.VacuumClean
                    If chkJettingPurge.Checked = True Then
                        .JettingPurge = True
                    Else
                        .JettingPurge = False
                    End If
                Case 1
                    .CleanType = eCleanType.JetClean
                Case 2
                    .CleanType = eCleanType.AugerClean
                    '20160920
                Case 3
                    .CleanType = eCleanType.VacuumJetClean
                    If chkJettingPurge.Checked = True Then
                        .JettingPurge = True
                    Else
                        .JettingPurge = False
                    End If
                Case 4
                    .CleanType = eCleanType.VacuumAugerClean
                    If chkJettingPurge.Checked = True Then
                        .JettingPurge = True
                    Else
                        .JettingPurge = False
                    End If
            End Select

            If chkPreDispenePurge.Checked = True Then
                .IsPreDispenePurge = True
            Else
                .IsPreDispenePurge = False
            End If


        End With
        Return True
    End Function

    ''' <summary>
    ''' 物件到介面
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetPurgeDB(ByRef item As CPurgeParameter) As Boolean
        cboCleanType.Items.Clear()
        'Sue20170523
        cboRunType.Items.Clear()

        'cboCleanType.Items.Add(GetString("Purge"))
        'cboCleanType.Items.Add(GetString("NozzleCleaner"))   '滾輪
        'cboCleanType.Items.Add(GetString("AugerCleaner")) 'X '螺桿夾
        'cboCleanType.Items.Add(GetString("PurgeNozzleCleaner"))
        'cboCleanType.Items.Add(GetString("PurgeAugerCleaner")) 'X

        '[說明]:800目前只有Purge一個選項,因為硬體條件   20161206
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                cboCleanType.Items.Add(GetString("Purge"))

            Case Else
                cboCleanType.Items.Add(GetString("Purge"))
                cboCleanType.Items.Add(GetString("NozzleCleaner"))   '滾輪
                cboCleanType.Items.Add(GetString("AugerCleaner")) 'X '螺桿夾
                cboCleanType.Items.Add(GetString("PurgeNozzleCleaner"))
                cboCleanType.Items.Add(GetString("PurgeAugerCleaner")) 'X

        End Select

        'Jeffadd 20160726
        With item
            'Sue20170523
            cboRunType.Items.Add(GetString("None")) '不做
            cboRunType.Items.Add(GetString("Timer")) '時間
            cboRunType.Items.Add(GetString("Runs")) '次數
            cboRunType.Items.Add(GetString("Timer & Runs")) '時間&次數
            Select Case .BaseOn
                Case eInspectionType.Noen
                    cboRunType.SelectedIndex = 0
                    'rdoNone.Checked = True
                Case eInspectionType.OnTimer
                    cboRunType.SelectedIndex = 1
                    'rdoOnTimer.Checked = True
                Case eInspectionType.OnRuns
                    cboRunType.SelectedIndex = 2
                    'rdoOnRuns.Checked = True
                Case eInspectionType.OnTimerOrRuns
                    cboRunType.SelectedIndex = 3
                    'rdoTimerandRuns.Checked = True
            End Select
            Premtek.ControlMisc.SetNumericValue(nmuOnRuns, .OnRuns)
            Premtek.ControlMisc.SetNumericValue(nmuOnTimer, .OnTimer)

            Select Case .CleanType
                Case eCleanType.VacuumClean
                    cboCleanType.SelectedIndex = 0
                    If .JettingPurge = True Then
                        chkJettingPurge.Checked = True
                    Else
                        chkJettingPurge.Checked = False
                    End If
                Case eCleanType.JetClean
                    cboCleanType.SelectedIndex = 1
                Case eCleanType.AugerClean
                    cboCleanType.SelectedIndex = 2
                    '20160920
                Case eCleanType.VacuumJetClean
                    cboCleanType.SelectedIndex = 3
                    If .JettingPurge = True Then
                        chkJettingPurge.Checked = True
                    Else
                        chkJettingPurge.Checked = False
                    End If
                Case eCleanType.VacuumAugerClean
                    cboCleanType.SelectedIndex = 4
                    If .JettingPurge = True Then
                        chkJettingPurge.Checked = True
                    Else
                        chkJettingPurge.Checked = False
                    End If
            End Select

            If .IsPreDispenePurge = True Then
                chkPreDispenePurge.Checked = True
            Else
                chkPreDispenePurge.Checked = False
            End If

        End With
        Return True
    End Function
    ''' <summary>更新選單</summary>   Jeffadd 20160805
    ''' <param name="NewName">新增項目</param>
    ''' <remarks></remarks>
    Public Sub UpdateNewNamePurgeUI(ByRef NewName As String)

        '=== 選單更新 ===
        lstItem.Items.Clear()
        cmbValvePurge1.Items.Clear()
        cmbValvePurge2.Items.Clear()
        cmbValvePurge3.Items.Clear()
        cmbValvePurge4.Items.Clear()
        For i As Integer = 0 To gPurgeDB.Count - 1
            lstItem.Items.Add(gPurgeDB.Keys(i))
            cmbValvePurge1.Items.Add(gPurgeDB.Keys(i))
            cmbValvePurge2.Items.Add(gPurgeDB.Keys(i))
            cmbValvePurge3.Items.Add(gPurgeDB.Keys(i))
            cmbValvePurge4.Items.Add(gPurgeDB.Keys(i))
        Next
        '=== 選單更新 ===

        '=== 選取新建項目 ===
        If Not NewName Is Nothing Then
            If lstItem.Items.Contains(NewName) Then
                lstItem.SelectedItem = NewName
            End If
        End If
        '=== 選取新建項目 ===


        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                SelectComboBox(cmbValvePurge1, RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1))
                SelectComboBox(cmbValvePurge2, RecipeEdit.StageParts(enmStage.No2).PurgeName(eValveWorkMode.Valve1))
                SelectComboBox(cmbValvePurge3, RecipeEdit.StageParts(enmStage.No3).PurgeName(eValveWorkMode.Valve1))
                SelectComboBox(cmbValvePurge4, RecipeEdit.StageParts(enmStage.No4).PurgeName(eValveWorkMode.Valve1))

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                SelectComboBox(cmbValvePurge1, RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1))
                SelectComboBox(cmbValvePurge2, RecipeEdit.StageParts(enmStage.No2).PurgeName(eValveWorkMode.Valve1))

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        SelectComboBox(cmbValvePurge1, RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1))

                    Case eMechanismModule.TwoValveOneStage
                        SelectComboBox(cmbValvePurge1, RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1))
                        SelectComboBox(cmbValvePurge2, RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve2))

                End Select

        End Select
    End Sub


    Public Sub UpdatePurgeUI()

        '=== 選單更新 ===
        lstItem.Items.Clear()
        cmbValvePurge1.Items.Clear()
        cmbValvePurge2.Items.Clear()
        cmbValvePurge3.Items.Clear()
        cmbValvePurge4.Items.Clear()

        For mI As Integer = 0 To gPurgeDB.Count - 1
            lstItem.Items.Add(gPurgeDB.Keys(mI))
            cmbValvePurge1.Items.Add(gPurgeDB.Keys(mI))
            cmbValvePurge2.Items.Add(gPurgeDB.Keys(mI))
            cmbValvePurge3.Items.Add(gPurgeDB.Keys(mI))
            cmbValvePurge4.Items.Add(gPurgeDB.Keys(mI))
            '[說明]顯示No1的資料
            If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) Then
                txtName.Text = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1)
                lstItem.SelectedIndex = mI
            End If
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) Then
                        cmbValvePurge1.SelectedIndex = mI
                    End If
                    If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No2).PurgeName(eValveWorkMode.Valve1) Then
                        cmbValvePurge2.SelectedIndex = mI
                    End If
                    If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No3).PurgeName(eValveWorkMode.Valve1) Then
                        cmbValvePurge3.SelectedIndex = mI
                    End If
                    If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No4).PurgeName(eValveWorkMode.Valve1) Then
                        cmbValvePurge4.SelectedIndex = mI
                    End If

                Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                    If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) Then
                        cmbValvePurge1.SelectedIndex = mI
                    End If
                    If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No2).PurgeName(eValveWorkMode.Valve1) Then
                        cmbValvePurge2.SelectedIndex = mI
                    End If

                Case Else
                    Select Case gSSystemParameter.StageUseValveCount
                        Case eMechanismModule.OneValveOneStage
                            If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) Then
                                cmbValvePurge1.SelectedIndex = mI
                            End If

                        Case eMechanismModule.TwoValveOneStage
                            If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve1) Then
                                cmbValvePurge1.SelectedIndex = mI
                            End If
                            If gPurgeDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PurgeName(eValveWorkMode.Valve2) Then
                                cmbValvePurge2.SelectedIndex = mI
                            End If

                    End Select

            End Select
        Next



        '=== 選單更新 ===

        'SelectComboBox(cmbValvePurge1, RecipeEdit.PurgeName(enmValve.No1))
        'SelectComboBox(cmbValvePurge2, RecipeEdit.PurgeName(enmValve.No2))
        'SelectComboBox(cmbValvePurge3, RecipeEdit.PurgeName(enmValve.No3))
        'SelectComboBox(cmbValvePurge4, RecipeEdit.PurgeName(enmValve.No4))

    End Sub

    ''' <summary>如果Index小於0且存在,選取Index=0</summary>
    ''' <param name="cmbValve"></param>
    ''' <remarks></remarks>
    Sub SelectComboBox(ByRef cmbValve As ComboBox, ByVal selectedName As String)
        If Not selectedName Is Nothing Then
            If cmbValve.Items.Contains(selectedName) Then
                cmbValve.SelectedItem = selectedName
            End If
        End If
        If cmbValve.SelectedIndex < 0 Then
            If cmbValve.Items.Count > 0 Then
                cmbValve.SelectedIndex = 0
            End If
        End If
    End Sub



    Dim mSelectedItem As String
    Private Sub lstItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstItem.SelectedIndexChanged
        If lstItem.SelectedIndex < 0 Then
            lstItem.BackColor = Color.Yellow
            lstItem.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstItem.BackColor = Color.White
            lstItem.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        mSelectedItem = lstItem.SelectedItem
        txtName.Text = lstItem.SelectedItem
        SetPurgeDB(gPurgeDB(lstItem.SelectedItem))
    End Sub


    Private Sub txtOnTimer_KeyPress(sender As Object, e As KeyPressEventArgs)
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub


    Private Sub txtOnRuns_KeyPress(sender As Object, e As KeyPressEventArgs)
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub


    Private Sub cboCleanType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCleanType.SelectedIndexChanged
        'Select Case Me.cboCleanType.SelectedIndex
        '    Case eCleanType.VacuumClean
        '        chkJettingPurge.Visible = True
        '    Case eCleanType.JetClean
        '        chkJettingPurge.Visible = False
        '    Case eCleanType.AugerClean
        '        chkJettingPurge.Visible = False
        '    Case eCleanType.VacuumJetClean
        '        chkJettingPurge.Visible = True
        '    Case eCleanType.VacuumAugerClean
        '        chkJettingPurge.Visible = True

        'End Select
    End Sub

    Private Sub cboRunType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRunType.SelectedIndexChanged
        Select Case cboRunType.SelectedIndex
            Case eInspectionType.Noen
                nmuOnTimer.Visible = False
                nmuOnRuns.Visible = False
                Label50.Visible = False
                Label82.Visible = False
                lblRuns.Visible = False
                lblTimer.Visible = False
            Case eInspectionType.OnTimer
                nmuOnTimer.Visible = True
                Label50.Visible = True
                lblTimer.Visible = True
                nmuOnRuns.Visible = False
                Label82.Visible = False
                lblRuns.Visible = False
            Case eInspectionType.OnRuns
                nmuOnTimer.Visible = False
                Label50.Visible = False
                lblTimer.Visible = False
                nmuOnRuns.Visible = True
                Label82.Visible = True
                lblRuns.Visible = True

                lblRuns.Location = New Point(536, 80)
                nmuOnRuns.Location = New Point(671, 80)
                Label82.Location = New Point(797, 80)

            Case eInspectionType.OnTimerOrRuns
                nmuOnTimer.Visible = True
                nmuOnRuns.Visible = True
                Label50.Visible = True
                Label82.Visible = True
                lblTimer.Visible = True

                lblRuns.Location = New Point(536, 130)
                nmuOnRuns.Location = New Point(671, 130)
                Label82.Location = New Point(797, 130)
        End Select
    End Sub
End Class

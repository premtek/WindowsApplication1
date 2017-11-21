Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports ProjectCore
Imports ProjectIO

Public Class frmSetUserLevel

    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetUserLevel))

    ''' <summary>Resource內的資料又消失...另開Function處理</summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetString(ByVal value As String) As String
        Select Case value
            Case "SoftwareMaker", "Developer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Developer"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "开发人员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "開發人員"
                End Select
            Case "Administrator"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Administrator"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "系统管理员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "系統管理員"
                End Select
            Case "Manager"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Manager"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "管理员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "管理員"
                End Select
            Case "Engineer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Engineer"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "工程师"
                    Case enmLanguageType.eTraditionalChinese
                        Return "工程師"
                End Select
            Case "Operator"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Operator"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "操作员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "作業員"
                End Select
        End Select
        Return ""
    End Function
    Sub ShowData()
        gSetUserLevel.LoadTable(MachineName)
        dgwWriteLevel.DataSource = gSetUserLevel.GetTable(gUserLevel)
        'jimmy 20170726
        For index = 0 To dgwWriteLevel.RowCount - 1
            dgwWriteLevel.Rows(index).DefaultCellStyle.Font = New Font("微軟正黑體", 9)
        Next

        dgwWriteLevel.Columns(1).Visible = False
        dgwWriteLevel.Refresh()

    End Sub

    Private Sub frmSetUserLevel_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        AuthorityUpdate()
    End Sub

    Private Sub frmLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowData()
        '[說明]:只有Manager以上的等級才能進入

        cboUserLevel.Items.Clear()
        cboUserLevel.Items.Add(enmUserLevel.eSoftwareMaker & GetString("SoftwareMaker"))
        cboUserLevel.Items.Add(enmUserLevel.eAdministrator & GetString("Administrator"))
        cboUserLevel.Items.Add(enmUserLevel.eManager & GetString("Manager"))
        cboUserLevel.Items.Add(enmUserLevel.eEngineer & GetString("Engineer"))
        cboUserLevel.Items.Add(enmUserLevel.eOperator & GetString("Operator"))

        SetAuthorityTag()
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub


    Private Sub dgwWriteLevel_Click(sender As Object, e As EventArgs) Handles dgwWriteLevel.Click
        gSyslog.Save("[frmSetUserLevel]" & vbTab & "[dgwWriteLevel]" & vbTab & "Click")
        For Each c As DataGridViewRow In dgwWriteLevel.SelectedRows
            '[說明]:只有上一階或同階的才看的到密碼
            If CInt(dgwWriteLevel.Rows(c.Index).Cells(2).Value) > CInt(gUserLevel) Then
                txtPassword.PasswordChar = ""
                txtID.Text = dgwWriteLevel.Rows(c.Index).Cells(0).Value.ToString()
                txtPassword.Text = dgwWriteLevel.Rows(c.Index).Cells(1).Value.ToString()
                cboUserLevel.SelectedIndex = CInt(dgwWriteLevel.Rows(c.Index).Cells(2).Value)
            Else
                txtPassword.PasswordChar = "*"
                txtPassword.Text = ""
                txtID.Text = dgwWriteLevel.Rows(c.Index).Cells(0).Value.ToString()
                cboUserLevel.SelectedIndex = CInt(dgwWriteLevel.Rows(c.Index).Cells(2).Value)
            End If
        Next
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        gSyslog.Save("[frmSetUserLevel]" & vbTab & "[btnModify]" & vbTab & "Click")
        '[說明]:確認ID是否存在   20161208
        Dim CheckId As Boolean = False

        If btnModify.Enabled = False Then
            Exit Sub
        End If
        btnModify.Enabled = False
        '[說明]:只有上一階或同階的才能刪除
        For Each row As DataRow In gSetUserLevel.GetTable(gUserLevel).Rows
            If row("Id") = txtID.Text.Replace("'", "''") Then
                If row("Level") < gUserLevel Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000002), "Warn_3000002", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnModify.Enabled = True
                    Exit Sub
                End If
                '20161208
                CheckId = True
            End If
        Next
        '20161208
        If CheckId = False Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("This ID does not exist!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("此ID不存在!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("此ID不存在!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select

            btnModify.Enabled = True
            Exit Sub
        End If

        If cboUserLevel.SelectedIndex < gUserLevel Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000003), "Warn_3000003", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnModify.Enabled = True
            Exit Sub
        End If

        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eEnglish
                If MsgBox("Are You Sure?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
                    btnModify.Enabled = True
                    Exit Sub
                End If
            Case enmLanguageType.eSimplifiedChinese
                If MsgBox("您确定要执行?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
                    btnModify.Enabled = True
                    Exit Sub
                End If
            Case enmLanguageType.eTraditionalChinese
                If MsgBox("您確定要執行?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
                    btnModify.Enabled = True
                    Exit Sub
                End If
        End Select

        gSetUserLevel.Update(txtID.Text.Replace("'", "''"), txtPassword.Text.Replace("'", "''"), cboUserLevel.SelectedIndex)

        ShowData()
        btnModify.Enabled = True
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '[說明]:確認ID是否存在   20161208
        Dim CheckId As Boolean = False

        gSyslog.Save("[frmSetUserLevel]" & vbTab & "[btnDelete]" & vbTab & "Click")
        If btnDelete.Enabled = False Then
            Exit Sub
        End If

        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eEnglish
                If MsgBox("Are you sure to delete this ID?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            Case enmLanguageType.eSimplifiedChinese
                If MsgBox("您确定要删除此ID?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            Case enmLanguageType.eTraditionalChinese
                If MsgBox("您確定要刪除此ID?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
        End Select

        btnDelete.Enabled = False
        '[說明]:只有上一階或同階的才能刪除


        For Each row As DataRow In gSetUserLevel.GetTable(gUserLevel).Rows
            If row("Id") = txtID.Text.Replace("'", "''") Then
                If row("Level") < gUserLevel Then '權限不足
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000002), "Warn_3000002", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnDelete.Enabled = True
                    Exit Sub
                End If
                CheckId = True

                If row("Password") <> txtPassword.Text Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Please Key Password!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("请输入密码!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("請輸入密碼!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnDelete.Enabled = True
                    Exit Sub
                End If
            End If
        Next

        If CheckId = False Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("This ID does not exist!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("此ID不存在!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("此ID不存在!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnDelete.Enabled = True
            Exit Sub
        End If

        gSetUserLevel.Delete(txtID.Text.Replace("'", "''"))

        ShowData()
        btnDelete.Enabled = True
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        gSyslog.Save("[frmSetUserLevel]" & vbTab & "[btnAdd]" & vbTab & "Click")
        If btnAdd.Enabled = False Then
            Exit Sub
        End If
        btnAdd.Enabled = False
        '[說明]:只有Manager以上的等級才能進入
        '       不能越級設定
        For Each row As DataRow In gSetUserLevel.GetTable(gUserLevel).Rows
            If row("Id") = txtID.Text.Replace("'", "''") Then
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000004), "Warn_3000004", eMessageLevel.Warning)
                MsgBox(gMsgHandler.GetMessage(Warn_3000004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnAdd.Enabled = True
                Exit Sub
            End If
        Next
        '20161206
        If txtID.Text = "" Or txtPassword.Text = "" Then

            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("This ID or Password does not exist!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("此ID或密码不存在!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("此ID或密碼不存在!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnAdd.Enabled = True
            Exit Sub
        End If

        If cboUserLevel.SelectedIndex < gUserLevel Then
            gEqpMsg.AddHistoryAlarm("Warn_3000002", "frmSetUserLevel btnAdd", , gMsgHandler.GetMessage(Warn_3000002), eMessageLevel.Warning) '[使用者帳號異常(不能越級設定)!
            MsgBox(gMsgHandler.GetMessage(Warn_3000002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnAdd.Enabled = True
            Exit Sub
        End If
        gSetUserLevel.Add(txtID.Text.Replace("'", "''"), txtPassword.Text.Replace("'", "''"), cboUserLevel.SelectedIndex)
        ShowData()
        btnAdd.Enabled = True

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSetUserLevel]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()

    End Sub

#Region "使用者權限設定"
    ''' <summary>
    ''' 更新權限顯示
    ''' </summary>
    ''' <remarks></remarks>
    Sub AuthorityUpdate()
        userAuth = gSSystemParameter.UserAuth.Clone

        UserAuthToButton(1, btnAuth1, gUserLevel)
        UserAuthToButton(2, btnAuth2, gUserLevel)
        UserAuthToButton(3, btnAuth3, gUserLevel)
        UserAuthToButton(4, btnAuth4, gUserLevel)
        UserAuthToButton(5, btnAuth5, gUserLevel)
        UserAuthToButton(6, btnAuth6, gUserLevel)
        UserAuthToButton(7, btnAuth7, gUserLevel)
        UserAuthToButton(8, btnAuth8, gUserLevel)
        UserAuthToButton(9, btnAuth9, gUserLevel)
        UserAuthToButton(10, btnAuth10, gUserLevel)
        UserAuthToButton(11, btnAuth11, gUserLevel)
        UserAuthToButton(12, btnAuth12, gUserLevel)
        UserAuthToButton(13, btnAuth13, gUserLevel)
        UserAuthToButton(14, btnAuth14, gUserLevel)
        UserAuthToButton(15, btnAuth15, gUserLevel)
        UserAuthToButton(16, btnAuth16, gUserLevel)
        UserAuthToButton(17, btnAuth17, gUserLevel)
        UserAuthToButton(18, btnAuth18, gUserLevel)
        UserAuthToButton(19, btnAuth19, gUserLevel)
        UserAuthToButton(20, btnAuth20, gUserLevel)
        UserAuthToButton(21, btnAuth21, gUserLevel)
        UserAuthToButton(22, btnAuth22, gUserLevel)
        UserAuthToButton(23, btnAuth23, gUserLevel)
        UserAuthToButton(24, btnAuth24, gUserLevel)
        UserAuthToButton(25, btnAuth25, gUserLevel)
        UserAuthToButton(26, btnAuth26, gUserLevel)
        UserAuthToButton(27, btnAuth27, gUserLevel)
        UserAuthToButton(28, btnAuth28, gUserLevel)
        UserAuthToButton(29, btnAuth29, gUserLevel)
        UserAuthToButton(30, btnAuth30, gUserLevel)
        UserAuthToButton(31, btnAuth31, gUserLevel)
        UserAuthToButton(32, btnAuth32, gUserLevel)
        UserAuthToButton(33, btnAuth33, gUserLevel)
    End Sub
    ''' <summary>
    ''' 設定權限按鍵Tag
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetAuthorityTag()
        btnAuth1.Tag = enmUserAuthItem.Manual
        btnAuth2.Tag = enmUserAuthItem.Recipe
        btnAuth3.Tag = enmUserAuthItem.IOSetup
        btnAuth4.Tag = enmUserAuthItem.SetModuleConveyor
        btnAuth5.Tag = enmUserAuthItem.SetUserLevel
        btnAuth6.Tag = enmUserAuthItem.SetInterlock
        btnAuth7.Tag = enmUserAuthItem.setHardwareConfig
        btnAuth8.Tag = enmUserAuthItem.SystemMotor
        btnAuth9.Tag = enmUserAuthItem.IOTable
        btnAuth10.Tag = enmUserAuthItem.SetModuleAOI
        btnAuth11.Tag = enmUserAuthItem.Calibration
        btnAuth12.Tag = enmUserAuthItem.EngineMode
        btnAuth13.Tag = enmUserAuthItem.ManualMotor
        btnAuth14.Tag = enmUserAuthItem.SetPurgePos
        btnAuth15.Tag = enmUserAuthItem.SetClearPos
        btnAuth16.Tag = enmUserAuthItem.SetChangePos
        btnAuth17.Tag = enmUserAuthItem.SetWeightPos
        btnAuth18.Tag = enmUserAuthItem.SetValveController
        btnAuth19.Tag = enmUserAuthItem.SetModuleTowerLight
        btnAuth20.Tag = enmUserAuthItem.SetMessageLanguage
        btnAuth21.Tag = enmUserAuthItem.SetPartHotPlate
        btnAuth22.Tag = enmUserAuthItem.SetCCD
        btnAuth23.Tag = enmUserAuthItem.SetLight
        btnAuth24.Tag = enmUserAuthItem.SetTriggerController
        btnAuth25.Tag = enmUserAuthItem.SetBalance
        btnAuth26.Tag = enmUserAuthItem.SetLaserReader
        btnAuth27.Tag = enmUserAuthItem.SetFMCS
        btnAuth28.Tag = enmUserAuthItem.SetConveyor
        btnAuth29.Tag = enmUserAuthItem.SetElectroPneumaticValve
        btnAuth30.Tag = enmUserAuthItem.SetTilt
        btnAuth31.Tag = enmUserAuthItem.SetElectricCylinder
        btnAuth32.Tag = enmUserAuthItem.SetTemperature
        btnAuth33.Tag = enmUserAuthItem.SetStageSafe
    End Sub

    ''' <summary>使用者權限暫存</summary>
    ''' <remarks></remarks>
    Dim userAuth(enmUserAuthItem.Max) As enmUserLevel
    Private Sub btnAuth1_Click(sender As Object, e As EventArgs) Handles btnAuth1.Click, btnAuth2.Click, btnAuth3.Click, btnAuth4.Click, btnAuth5.Click, btnAuth6.Click, btnAuth7.Click, btnAuth8.Click, btnAuth9.Click, btnAuth10.Click, btnAuth11.Click, btnAuth12.Click, btnAuth13.Click, btnAuth14.Click, btnAuth15.Click, btnAuth16.Click, btnAuth17.Click, btnAuth18.Click, btnAuth19.Click, btnAuth20.Click, btnAuth21.Click, btnAuth22.Click, btnAuth23.Click, btnAuth24.Click, btnAuth25.Click, btnAuth26.Click, btnAuth27.Click, btnAuth28.Click, btnAuth29.Click, btnAuth30.Click, btnAuth31.Click, btnAuth32.Click, btnAuth33.Click
        gSyslog.Save("[frmLogin]" & vbTab & "[btnAuth]" & vbTab & "Click")
        Dim item As Button = CType(sender, Button)
        Dim index As Integer = Val(item.Tag)

        userAuth(index) -= 1
        If userAuth(index) < 0 Then userAuth(index) = enmUserLevel.eOperator '點擊循環
        If userAuth(index) < gUserLevel Then userAuth(index) = enmUserLevel.eOperator '高級權限保護

        UserAuthToButton(index, item, gUserLevel)
        gSSystemParameter.UserAuth = userAuth.Clone '轉回原Mapping
    End Sub


    Sub UserAuthToButton(ByVal index As Integer, ByRef item As Button, ByRef currentUserLevel As enmUserLevel)
        Select Case userAuth(index)
            Case enmUserLevel.eAdministrator
                item.Text = GetString("Administrator")
                Select Case currentUserLevel
                    Case enmUserLevel.eSoftwareMaker
                        item.Enabled = True
                    Case enmUserLevel.eAdministrator
                        item.Enabled = True
                    Case enmUserLevel.eManager
                        item.Enabled = False
                    Case enmUserLevel.eEngineer
                        item.Enabled = False
                    Case enmUserLevel.eOperator
                        item.Enabled = False
                End Select
            Case enmUserLevel.eEngineer
                item.Text = GetString("Engineer")
                Select Case currentUserLevel
                    Case enmUserLevel.eSoftwareMaker
                        item.Enabled = True
                    Case enmUserLevel.eAdministrator
                        item.Enabled = True
                    Case enmUserLevel.eManager
                        item.Enabled = True
                    Case enmUserLevel.eEngineer
                        item.Enabled = True
                    Case enmUserLevel.eOperator
                        item.Enabled = False
                End Select
            Case enmUserLevel.eManager
                item.Text = GetString("Manager")
                Select Case currentUserLevel
                    Case enmUserLevel.eSoftwareMaker
                        item.Enabled = True
                    Case enmUserLevel.eAdministrator
                        item.Enabled = True
                    Case enmUserLevel.eManager
                        item.Enabled = True
                    Case enmUserLevel.eEngineer
                        item.Enabled = False
                    Case enmUserLevel.eOperator
                        item.Enabled = False
                End Select
            Case enmUserLevel.eOperator
                item.Text = GetString("Operator")
                Select Case currentUserLevel
                    Case enmUserLevel.eSoftwareMaker
                        item.Enabled = True
                    Case enmUserLevel.eAdministrator
                        item.Enabled = True
                    Case enmUserLevel.eManager
                        item.Enabled = True
                    Case enmUserLevel.eEngineer
                        item.Enabled = True
                    Case enmUserLevel.eOperator
                        item.Enabled = True
                End Select
            Case enmUserLevel.eSoftwareMaker
                item.Text = GetString("SoftwareMaker")
                Select Case currentUserLevel
                    Case enmUserLevel.eSoftwareMaker
                        item.Enabled = True
                    Case enmUserLevel.eAdministrator
                        item.Enabled = False
                    Case enmUserLevel.eManager
                        item.Enabled = False
                    Case enmUserLevel.eEngineer
                        item.Enabled = False
                    Case enmUserLevel.eOperator
                        item.Enabled = False
                End Select
        End Select
    End Sub

#End Region

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSetUserLevel]" & vbTab & "[btnOK]" & vbTab & "Click")
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        btnOK.Enabled = False
        gSSystemParameter.SaveAuthority(Application.StartupPath & "\System\" & MachineName & "\SysAuthority.ini") '儲存使用者權限(by機型)
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        btnOK.Enabled = True
        'Me.Close()
    End Sub


    Private Sub frmSetUserLevel_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

End Class
'20170520
Imports ProjectCore
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectMotion
Imports WetcoConveyor
Imports ProjectValveController

Public Class ucValveParameterAdvanjet
    Dim frm As Form
    Public gSys As sSysParam
    Dim JetValveSelectedItem As String = ""

    Sub New(ByVal frm As Form)

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.frm = frm
    End Sub

    Private Sub ucValveParameterAdvanjet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        JetValveDB_Update(gCRecipe.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
    End Sub

    Public Sub JetValveDB_Update(ByVal dataBaseName As String)
        Dim mSelectIndex As Integer = 0

        lstJetValve.Items.Clear()
        txtJetValveName.Text = ""

        ''[Note]:顯示上是顯示StageNo1的ValveNo1
        For mI As Integer = 0 To gJetValveDB.Count - 1
            If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                    lstJetValve.Items.Add(gJetValveDB.Keys(mI))
                    If dataBaseName = gJetValveDB.Keys(mI) Then
                        SetJetValveDB(gJetValveDB(dataBaseName))
                        txtJetValveName.Text = dataBaseName
                        lstJetValve.SelectedIndex = mSelectIndex
                    End If
                    mSelectIndex = mSelectIndex + 1
                End If
            End If
        Next
    End Sub

    Public Sub SetJetValveDB(ByRef jetValveParam As CJetValveParameter)
        With jetValveParam
            txtAdvanjetParameterRefillTime.Text = .Advanjet.RefillTime
            txtAdvanjetParameterCycleTime.Text = .Advanjet.CycleTime
            txtAdvanjetParameterJetTime.Text = .Advanjet.JetTime
        End With
    End Sub

    Private Sub btnSaveAdvanjetParameter_Click(sender As Object, e As EventArgs) Handles btnSaveAdvanjetParameter.Click
        If lstJetValve.SelectedIndex < 0 Then
            lstJetValve.BackColor = Color.Yellow
            lstJetValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstJetValve.BackColor = Color.White
            Exit Sub
        End If

        If gJetValveDB.ContainsKey(lstJetValve.SelectedItem) = True Then
            If GetJetValveDB(gJetValveDB(lstJetValve.SelectedItem)) Then
                '20161102
                Dim folderPath As String = "" '噴射閥資料

                folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"

                Dim fileName As String = folderPath & gJetValveDB(lstJetValve.SelectedItem).Name & ".pst"
                gJetValveDB(lstJetValve.SelectedItem).Save(fileName)

                'Sue0822
                '存檔成功 
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
                MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                txtJetValveFrequency_TextChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub txtJetValveFrequency_TextChanged(sender As Object, e As EventArgs)
        Dim ChangeColor As Color = Color.Red
        If JetValveSelectedItem = "" Then
            Exit Sub
        End If
        '[Note]:沒有此DateBase
        If gJetValveDB.ContainsKey(JetValveSelectedItem) = False Then
            Exit Sub
        End If

        With gJetValveDB(JetValveSelectedItem)
            If .Advanjet.RefillTime = Val(txtAdvanjetParameterRefillTime.Text) Then
                txtAdvanjetParameterRefillTime.BackColor = Color.White
            Else
                txtAdvanjetParameterRefillTime.BackColor = ChangeColor
            End If

            If .Advanjet.CycleTime = Val(txtAdvanjetParameterCycleTime.Text) Then
                txtAdvanjetParameterCycleTime.BackColor = Color.White
            Else
                txtAdvanjetParameterCycleTime.BackColor = ChangeColor
            End If

            If .Advanjet.JetTime = Val(txtAdvanjetParameterJetTime.Text) Then
                txtAdvanjetParameterJetTime.BackColor = Color.White
            Else
                txtAdvanjetParameterJetTime.BackColor = ChangeColor
            End If
        End With
    End Sub


    Function GetJetValveDB(ByRef jetValveParam As CJetValveParameter) As Boolean

        '[說明]:判斷是否為整數

        If (IsNumeric(txtAdvanjetParameterRefillTime.Text) AndAlso Val(txtAdvanjetParameterRefillTime.Text) >= 0) And
            (IsNumeric(txtAdvanjetParameterCycleTime.Text) AndAlso Val(txtAdvanjetParameterCycleTime.Text) >= 0) And
           (IsNumeric(txtAdvanjetParameterJetTime.Text) AndAlso Val(txtAdvanjetParameterJetTime.Text) >= 0) Then

            With jetValveParam
                .Advanjet.RefillTime = Val(txtAdvanjetParameterRefillTime.Text)
                .Advanjet.CycleTime = Val(txtAdvanjetParameterCycleTime.Text)
                .Advanjet.JetTime = Val(txtAdvanjetParameterJetTime.Text)
                jetValveParam.ValveModel = eValveModel.Advanjet
            End With
            Return True
        Else
            '資料格式錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
            MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        Return True
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frm.Close()
    End Sub

    Private Sub txtAdvanjetParameterRefillTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAdvanjetParameterRefillTime.KeyPress, txtAdvanjetParameterJetTime.KeyPress, txtAdvanjetParameterCycleTime.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub

    Private Sub lstJetValve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstJetValve.SelectedIndexChanged
        If lstJetValve.SelectedIndex < 0 Then
            lstJetValve.BackColor = Color.Yellow
            lstJetValve.Refresh()
            System.Threading.Thread.Sleep(300)
            lstJetValve.BackColor = Color.White
            Exit Sub
        End If
        JetValveSelectedItem = lstJetValve.SelectedItem
        txtJetValveName.Text = lstJetValve.SelectedItem
        SetJetValveDB(gJetValveDB(lstJetValve.SelectedItem))

        '[說明]:暫時修改名稱 frm關閉後再Load回來   
        gCRecipe.StageParts(gSYS.StageNo).ValveName(gSYS.SelectValve) = lstJetValve.SelectedItem
    End Sub

    Private Sub btnValveDBAdd_Click(sender As Object, e As EventArgs) Handles btnValveDBAdd.Click
        If txtJetValveName.Text.Trim = "" Then
            txtJetValveName.BackColor = Color.Yellow
            txtJetValveName.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtJetValveName.BackColor = Color.White
            txtJetValveName.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        Dim newJetValve As New CJetValveParameter(txtJetValveName.Text)
        newJetValve.Name = txtJetValveName.Text
        If IsillegalFileName(newJetValve.Name) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return
        Else
            '[說明]:確認是否執行
            If gJetValveDB.ContainsKey(newJetValve.Name) Then
                If MsgBox("Jet Valve Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstJetValve.SelectedItem = newJetValve.Name
                    GetJetValveDB(gJetValveDB(lstJetValve.SelectedItem))
                End If
                Exit Sub
            End If

            If GetJetValveDB(newJetValve) Then
                gJetValveDB.Add(newJetValve.Name, newJetValve)

                Dim folderPath As String = ""
                folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"


                Dim fileName As String = folderPath & newJetValve.Name & ".pst"

                If System.IO.File.Exists(fileName) Then
                    If MsgBox("Jet Valve File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                        newJetValve.Save(fileName)
                    End If
                Else
                    newJetValve.Save(fileName)
                End If
                JetValveDB_Update(newJetValve.Name)
            End If
        End If
    End Sub

    Private Sub btnValveDBDelete_Click(sender As Object, e As EventArgs) Handles btnValveDBDelete.Click
        If lstJetValve.SelectedIndex < 0 Then
            lstJetValve.BackColor = Color.Yellow
            lstJetValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstJetValve.BackColor = Color.White
            lstJetValve.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Dim folderPath As String = ""

        folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"
        Dim fileName As String = folderPath & lstJetValve.SelectedItem & ".pst"

        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If

        If gJetValveDB.ContainsKey(lstJetValve.SelectedItem) = True Then
            gJetValveDB.Remove(lstJetValve.SelectedItem)
            '[Note]:顯示上是顯示StageNo1的ValveNo1
            JetValveDB_Update(gCRecipe.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
        End If

        If Not gJetValveDB.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
            gJetValveDB.Add("Default", New CJetValveParameter("Default"))
            gJetValveDB("Default").Save(folderPath & "Default.pst")
            JetValveDB_Update(gCRecipe.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
        End If
    End Sub
End Class

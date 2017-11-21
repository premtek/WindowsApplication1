'20170520
Imports ProjectCore
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectMotion
Imports WetcoConveyor
Imports ProjectValveController

Public Class ucValveParameterPico
    Dim frm As Form
    Public gSys As sSysParam
    Dim JetValveSelectedItem As String = ""
    Sub New(ByVal frm As Form)

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.frm = frm
    End Sub

    Private Sub ucValveParameterPico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        JetValveDB_Update(gCRecipe.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
    End Sub


    Private Sub btnSavePicoParameter_Click(sender As Object, e As EventArgs) Handles btnSavePicoParameter.Click
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

                folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"

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
            If .PicoTouch.OpenTime = Val(txtPicoParameterOpenTime.Text) Then
                txtPicoParameterOpenTime.BackColor = Color.White
            Else
                txtPicoParameterOpenTime.BackColor = ChangeColor
            End If
            If .PicoTouch.ValveOnTime = Val(txtPicoParameterPulseTime.Text) Then
                txtPicoParameterPulseTime.BackColor = Color.White
            Else
                txtPicoParameterPulseTime.BackColor = ChangeColor
            End If
            If .PicoTouch.CloseTime = Val(txtPicoParameterCloseTime.Text) Then
                txtPicoParameterCloseTime.BackColor = Color.White
            Else
                txtPicoParameterCloseTime.BackColor = ChangeColor
            End If
            If .PicoTouch.ValveOffTime = Val(txtPicoParameterValveOffTime.Text) Then
                txtPicoParameterValveOffTime.BackColor = Color.White
            Else
                txtPicoParameterValveOffTime.BackColor = ChangeColor
            End If

            '********************************************************************
            txtPicoParameterCycleTime.Text = .PicoTouch.CycleTime
            txtPicoParameterFrequency.Text = .PicoTouch.Frequence
            '********************************************************************


            If .PicoTouch.Stroke = Val(txtPicoParameterStroke.Text) Then
                txtPicoParameterStroke.BackColor = Color.White
            Else
                txtPicoParameterStroke.BackColor = ChangeColor
            End If

            If .PicoTouch.JetTime = Val(txtPicoParameterJetTime.Text) Then
                txtPicoParameterJetTime.BackColor = Color.White
            Else
                txtPicoParameterJetTime.BackColor = ChangeColor
            End If

        End With
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frm.Close()
    End Sub



    Private Sub txtPicoParameterOpenTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPicoParameterOpenTime.KeyPress, txtPicoParameterPulseTime.KeyPress, txtPicoParameterCycleTime.KeyPress, txtPicoParameterStroke.KeyPress, txtPicoParameterCloseVoltage.KeyPress, txtPicoParameterCloseTime.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub


    Public Sub SetJetValveDB(ByRef jetValveParam As CJetValveParameter)
        With jetValveParam
            txtPicoParameterJetTime.Text = .PicoTouch.JetTime
            txtPicoParameterCloseVoltage.Text = .PicoTouch.CloseVoltage
            txtPicoParameterStroke.Text = .PicoTouch.Stroke
            txtPicoParameterOpenTime.Text = .PicoTouch.OpenTime
            txtPicoParameterPulseTime.Text = .PicoTouch.ValveOnTime
            txtPicoParameterCloseTime.Text = .PicoTouch.CloseTime
            txtPicoParameterValveOffTime.Text = .PicoTouch.ValveOffTime
            txtPicoParameterCycleTime.Text = CStr(.PicoTouch.CycleTime)
            txtPicoParameterFrequency.Text = .PicoTouch.Frequence.ToString("0.00")
        End With
    End Sub

    Function GetJetValveDB(ByRef jetValveParam As CJetValveParameter) As Boolean

        '[說明]:判斷是否為整數

        If (IsNumeric(txtPicoParameterCycleTime.Text) AndAlso Val(txtPicoParameterCycleTime.Text) >= 0) And
            (IsNumeric(txtPicoParameterCloseVoltage.Text) AndAlso Val(txtPicoParameterCloseVoltage.Text) >= 0) And
           (IsNumeric(txtPicoParameterStroke.Text) AndAlso Val(txtPicoParameterStroke.Text) >= 0) And
           (IsNumeric(txtPicoParameterPulseTime.Text) AndAlso Val(txtPicoParameterPulseTime.Text) >= 0) And
           (IsNumeric(txtPicoParameterOpenTime.Text) AndAlso Val(txtPicoParameterOpenTime.Text) >= 0) And
           (IsNumeric(txtPicoParameterJetTime.Text) AndAlso Val(txtPicoParameterJetTime.Text) > 0) And
           (IsNumeric(txtPicoParameterCloseTime.Text) AndAlso Val(txtPicoParameterCloseTime.Text) >= 0) Then

            With jetValveParam
                .PicoTouch.OpenTime = Val(txtPicoParameterOpenTime.Text)
                .PicoTouch.ValveOnTime = Val(txtPicoParameterPulseTime.Text)
                .PicoTouch.CloseTime = Val(txtPicoParameterCloseTime.Text)
                .PicoTouch.ValveOffTime = Val(txtPicoParameterValveOffTime.Text)
                .PicoTouch.Stroke = Val(txtPicoParameterStroke.Text)
                .PicoTouch.JetTime = Val(txtPicoParameterJetTime.Text)
                .PicoTouch.CloseVoltage = Val(txtPicoParameterCloseVoltage.Text)
                jetValveParam.ValveModel = eValveModel.PicoPulse
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


    Public Sub JetValveDB_Update(ByVal dataBaseName As String)
        Dim mSelectIndex As Integer = 0

        lstJetValve.Items.Clear()
        txtJetValveName.Text = ""

        ''[Note]:顯示上是顯示StageNo1的ValveNo1
        For mI As Integer = 0 To gJetValveDB.Count - 1
            If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
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
        gCRecipe.StageParts(gSys.StageNo).ValveName(gSys.SelectValve) = lstJetValve.SelectedItem
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
                folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"


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

        folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"
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

Imports ProjectCore
Imports ProjectIO
Imports ProjectRecipe

Public Class frmExchange
    Private mStageNo As enmStage
    Private mValveNo As eValveWorkMode

    Public Sub New(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode)
        InitializeComponent()
        mStageNo = stageNo
        mValveNo = valveNo
    End Sub

    ''' <summary>儲存設定</summary>
    ''' <remarks></remarks>
    Sub Save()
        If gCRecipe.StageParts(mStageNo) Is Nothing Then 'Soni + 2017.03.28
            '平台數量錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000015))
            MsgBox(gMsgHandler.GetMessage(Error_1000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Stage Count Error!")
            Exit Sub
        End If
        Dim mPasteName As String = gCRecipe.StageParts(mStageNo).PasteName(mValveNo)
        If Not gPasteDataBase.ContainsKey(mPasteName) Then
            '膠材名稱不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000069))
            MsgBox(gMsgHandler.GetMessage(Warn_3000069), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Glue Name Not Exists.")
            Exit Sub
        End If

        With gPasteDataBase(mPasteName)
            .PotLifeEnable = chkPasteExpireTime.Checked
            .PotLife = txtPasteExpiredTime.Text
            .PotLifeCountEnable = chkPasteExpiredCount.Checked
            .PotLifeCount = txtPasteExpiredCount.Text
        End With

        Dim folderPath As String = Application.StartupPath & "\Database\Paste\" '膠材資料
        Dim fileName As String = folderPath & gPasteDataBase(mPasteName).Name & ".pst"
        gPasteDataBase(mPasteName).Save(fileName)
    End Sub


    Private Sub frmExchange_Load(sender As Object, e As EventArgs) Handles Me.Load
        '=== 膠材壽命重置 ===
        With gSSystemParameter.StageParts.PasteLifeTime(mStageNo)
            .StartLifeTime(eValveWorkMode.Valve1) = DateTime.Now
            .DotsCount(eValveWorkMode.Valve1) = 0
        End With
        '=== 膠材壽命重置 ===

        If IsNothing(gCRecipe.StageParts(mStageNo)) Then 'Soni + 2017.03.28
            '平台數量錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000015))
            MsgBox(gMsgHandler.GetMessage(Error_1000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Stage Count Error!")
            Exit Sub
        End If
        Dim mPasteName As String = gCRecipe.StageParts(mStageNo).PasteName(mValveNo)
        If Not gPasteDataBase.ContainsKey(mPasteName) Then
            '膠材名稱不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000069))
            MsgBox(gMsgHandler.GetMessage(Warn_3000069), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Glue Name Not Exists.")
            Exit Sub
        End If
        With gPasteDataBase(mPasteName)
            chkPasteExpireTime.Checked = .PotLifeEnable
            txtPasteExpiredTime.Text = .PotLife
            chkPasteExpiredCount.Checked = .PotLifeCountEnable
            txtPasteExpiredCount.Text = .PotLifeCount
        End With
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnChangeGluePos]" & vbTab & "Click")
        Save() '存檔
        'Sue20170627
        '存檔成功 
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    End Sub

    Private Sub txtPasteExpiredTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasteExpiredTime.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub

    Private Sub txtPasteExpiredCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasteExpiredCount.KeyPress
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        'Sue20170627
        gSyslog.Save("[frmExchange]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
Public Class frmMotionLoopConfig

    'Private Sub frmMotionLoopConfig_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
    '    Me.Hide()
    'End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Sue0829
        '待補AlarmCode
        MsgBox("設定完成", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Hide()
    End Sub

    Private Sub btnHide_Click(sender As Object, e As EventArgs) Handles btnHide.Click
        Me.Hide()
    End Sub
End Class
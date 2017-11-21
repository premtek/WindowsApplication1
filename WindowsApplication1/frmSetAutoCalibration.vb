Imports ProjectCore
Imports ProjectMotion

Public Class frmSetAutoCalibration
    ''' <summary>表單是否已載入</summary>
    ''' <remarks></remarks>
    Dim mIsLoaded As Boolean

    ''' <summary>外部載入設定</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    Private Sub frmSetAutoCalibration_Activated(sender As Object, e As EventArgs) Handles Me.Activated
       
    End Sub
    Private Sub btnSetCcdPos_Click(sender As Object, e As EventArgs) Handles btnSetCcdPos.Click
        'Sue20170627
        gSyslog.Save("[frmSetAutoCalibration]" & vbTab & "[btnSetCcdPos]" & vbTab & "Click")
        txtTeachCCD1X.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtTeachCCD1Y.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtTeachCCD1Z.Text = gCMotion.GetPositionValue(sys.AxisZ)
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'Sue20170627
        gSyslog.Save("[frmSetAutoCalibration]" & vbTab & "[btnOK]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
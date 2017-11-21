Imports ProjectAOI
Imports ProjectCore

Public Class frmAlignImageMask
    Public Sys As sSysParam

    Dim ClassName As String = "frmAlignImageMask"

    Public mInputImage As Cognex.VisionPro.ICogImage

    Public mImageMask As Cognex.VisionPro.ICogImage


    Private Sub frmAlignImageMask_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If mInputImage Is Nothing Then

        End If
        CogImageMaskEditV21.Image = mInputImage
        CogImageMaskEditV21.MaskImage = mImageMask
    End Sub

    Private Sub frmAlignImageMask_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        CogImageMaskEditV21.Dispose()
        Me.Dispose(True)
    End Sub



    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click")
        CogImageMaskEditV21.OnApplied()
        mImageMask = CogImageMaskEditV21.MaskImage
        mInputImage = CogImageMaskEditV21.Image

        'Sue20170627
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
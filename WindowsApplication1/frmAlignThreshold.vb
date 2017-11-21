Imports System

Imports ProjectCore
'Imports ProjectMotion
'Imports ProjectRecipe
'Imports ProjectIO
Imports ProjectAOI

Imports Cognex.VisionPro
Imports Cognex.VisionPro.PixelMap

Public Class frmAlignThreshold
    Dim ClassName As String = "frmAlignThreshold"

    Public myPixelMapTool As Cognex.VisionPro.PixelMap.CogPixelMapTool

    Public mInputDownThreshold As Integer

    Public mInputUpThreshold As Integer

    Public mOutputDownThreshold As Integer

    Public mOutputUpThreshold As Integer

    Private mytmpPixelMapTool As Cognex.VisionPro.PixelMap.CogPixelMapTool

    Private Sub frmAlignThreshold_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not myPixelMapTool Is Nothing Then
            mytmpPixelMapTool = myPixelMapTool
            Dim tmpRecord As Cognex.VisionPro.ICogRecord
            If mytmpPixelMapTool.InputImage IsNot Nothing Then

                mytmpPixelMapTool.RunParams.SetReferencePointInputAbsolute(0, mInputDownThreshold)
                mytmpPixelMapTool.RunParams.SetReferencePointInputAbsolute(1, mInputUpThreshold)
                mytmpPixelMapTool.RunParams.SetReferencePointOutputAbsolute(0, mOutputDownThreshold)
                mytmpPixelMapTool.RunParams.SetReferencePointOutputAbsolute(1, mOutputUpThreshold)

                mytmpPixelMapTool.Run()
                tmpRecord = mytmpPixelMapTool.CreateCurrentRecord
                tmpRecord = tmpRecord.SubRecords.Item("GraphicImage")
                CogRecordDisplay1.Record = tmpRecord
                CogRecordDisplay1.Fit(True)
            End If
        End If
    End Sub

    Private Sub frmAlignThreshold_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CogRecordDisplay1.Dispose()
        CogRecordDisplay2.Dispose()
        Me.Dispose(True)

    End Sub

    Private Function GetThreshold(ByVal Value As Integer) As Integer
        Dim Max As Integer = 255 '[Note]灰階
        If Value > Max Then
            Return Max
        End If
        Return Value
    End Function

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        If btnRun.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRun]" & vbTab & "Click")
        btnRun.Enabled = False

        If Not mytmpPixelMapTool Is Nothing Then
            Dim tmpRecord As Cognex.VisionPro.ICogRecord
            Dim tmpResultRecord As Cognex.VisionPro.ICogRecord
            If mytmpPixelMapTool.InputImage IsNot Nothing Then

                mytmpPixelMapTool.Run()
                tmpRecord = mytmpPixelMapTool.CreateCurrentRecord
                tmpRecord = tmpRecord.SubRecords.Item("GraphicImage")
                CogRecordDisplay1.Record = tmpRecord
                CogRecordDisplay1.Fit(True)

                tmpResultRecord = mytmpPixelMapTool.CreateLastRunRecord()
                tmpResultRecord = tmpResultRecord.SubRecords.Item("OutputImage")
                CogRecordDisplay2.Record = tmpResultRecord
                CogRecordDisplay2.Fit(True)

                Debug.Print("mytmpPixelMapTool.RunParams.GetReferencePointInputAbsolute(0)" & mytmpPixelMapTool.RunParams.GetReferencePointInputAbsolute(0))
                Debug.Print("mytmpPixelMapTool.RunParams.GetReferencePointInputAbsolute(1)" & mytmpPixelMapTool.RunParams.GetReferencePointInputAbsolute(1))
                Debug.Print("mytmpPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0)" & mytmpPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0))
                Debug.Print("mytmpPixelMapTool.RunParams.GetReferencePointOutputAbsolute(1)" & mytmpPixelMapTool.RunParams.GetReferencePointOutputAbsolute(1))


            End If
        End If

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRun]" & vbTab & "ClickEnd")
        btnRun.Enabled = True
    End Sub


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click")
        btnOK.Enabled = False

        myPixelMapTool = mytmpPixelMapTool
        mInputDownThreshold = GetThreshold(mytmpPixelMapTool.RunParams.GetReferencePointInputAbsolute(0))
        mInputUpThreshold = GetThreshold(mytmpPixelMapTool.RunParams.GetReferencePointInputAbsolute(1))
        mOutputDownThreshold = GetThreshold(mytmpPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0))
        mOutputUpThreshold = GetThreshold(mytmpPixelMapTool.RunParams.GetReferencePointOutputAbsolute(1))
        Debug.Print("mInputDownThreshold" & mInputDownThreshold)
        Debug.Print("mInputUpThreshold" & mInputUpThreshold)
        Debug.Print("mOutputDownThreshold" & mOutputDownThreshold)
        Debug.Print("mOutputUpThreshold" & mOutputUpThreshold)

        'Sue0822
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        btnOK.Enabled = True
        'Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
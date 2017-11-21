Imports ProjectMotion
Imports ProjectCore

Public Class frmIOStatus
    Public AxisNo As Integer
    Private Sub tmrOption_Tick(sender As Object, e As EventArgs) Handles tmrOption.Tick
        With gCMotion.AxisParameter(AxisNo).MotionIOStatus
            SetSensorBackgroundImage(palRDY, .blnRDY, False)
            SetSensorBackgroundImage(palALM, .blnALM, False)
            SetSensorBackgroundImage(palALRM, .blnALRM, False)
            SetSensorBackgroundImage(palCLR, .blnCLR, False)
            SetSensorBackgroundImage(palDIR, .blnDIR, False)
            SetSensorBackgroundImage(palEMG, .blnEMG, False)
            SetSensorBackgroundImage(palERC, .blnERC, False)
            SetSensorBackgroundImage(palEZ, .blnEZ, False)
            SetSensorBackgroundImage(palINP, .blnINP, False)
            SetSensorBackgroundImage(palLTC, .blnLTC, False)
            SetSensorBackgroundImage(palNEL, .blnNEL, False)
            SetSensorBackgroundImage(palNSEL, .blnSNEL, False)
            SetSensorBackgroundImage(palORG, .blnORG, False)
            SetSensorBackgroundImage(palPCS, .blnPCS, False)
            SetSensorBackgroundImage(palPEL, .blnPEL, False)
            SetSensorBackgroundImage(palSD, .blnSD, False)
            SetSensorBackgroundImage(palNSEL, .blnSNEL, False)
            SetSensorBackgroundImage(palPSEL, .blnSPEL, False)
            SetSensorBackgroundImage(palSVON, .blnSVON, False)
            SetSensorBackgroundImage(palTRIG, .blnTRIG, False)
        End With
    End Sub

    Private Sub frmIOStatus_Load(sender As Object, e As EventArgs) Handles Me.Load
        tmrOption.Enabled = True
    End Sub


#Region "外部同步移動"
    Public SyncForm As Form


    Private Sub frmMotionOption_Move(sender As Object, e As EventArgs) Handles Me.Move
        If Not Me.SyncForm Is Nothing Then
            Me.SyncForm.Location = New Point(Me.Location.X - Me.SyncForm.Width, Me.Location.Y)
        End If
    End Sub
#End Region

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        gSyslog.Save("[frmRecipe01]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
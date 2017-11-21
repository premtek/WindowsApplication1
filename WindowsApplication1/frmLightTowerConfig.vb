Imports ProjectIO
Imports ProjectCore
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmLightTowerConfig

    Dim EditParam(eMessageLevel.Count - 1) As CMessageParameter

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmLightTowerConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmLightTowerConfig]" & vbTab & "[btnOK]" & vbTab & "Click")

        gEqpStatusHandler.MessageParam = EditParam
        gEqpStatusHandler.SaveMessageParam(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\EqpStatusHandler.ini") '燈號設定
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
    End Sub

    Sub RefreshItem(ByVal dobehavior As eDOBehavior, ByRef chkOn As CheckBox, ByRef chkOff As CheckBox, ByRef chkFlash As CheckBox)
        Select Case dobehavior
            Case eDOBehavior.On
                chkOn.Checked = True
                chkOff.Checked = False
                chkFlash.Checked = False
            Case eDOBehavior.Off
                chkOn.Checked = False
                chkOff.Checked = True
                chkFlash.Checked = False
            Case eDOBehavior.Flash
                chkOn.Checked = False
                chkOff.Checked = False
                chkFlash.Checked = True
        End Select
    End Sub

    Private Sub frmLightTowerConfig_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        EditParam = gEqpStatusHandler.MessageParam
        RefreshItem(EditParam(eMessageLevel.Error).DOutput(eIndicator.Red).DOutput, chkErrorRedOn, chkErrorRedOff, chkErrorRedFlash)
        RefreshItem(EditParam(eMessageLevel.Error).DOutput(eIndicator.Yellow).DOutput, chkErrorYellowOn, chkErrorYellowOff, chkErrorYellowFlash)
        RefreshItem(EditParam(eMessageLevel.Error).DOutput(eIndicator.Green).DOutput, chkErrorGreenOn, chkErrorGreenOff, chkErrorGreenFlash)
        RefreshItem(EditParam(eMessageLevel.Error).DOutput(eIndicator.Blue).DOutput, chkErrorBlueOn, chkErrorBlueOff, chkErrorBlueFlash)
        RefreshItem(EditParam(eMessageLevel.Error).DOutput(eIndicator.Buzzer).DOutput, chkErrorBuzzerOn, chkErrorBuzzerOff, chkErrorBuzzerFlash)

        RefreshItem(EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Red).DOutput, chkAlarmRedOn, chkAlarmRedOff, chkAlarmRedFlash)
        RefreshItem(EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Yellow).DOutput, chkAlarmYellowOn, chkAlarmYellowOff, chkAlarmYellowFlash)
        RefreshItem(EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Green).DOutput, chkAlarmGreenOn, chkAlarmGreenOff, chkAlarmGreenFlash)
        RefreshItem(EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Blue).DOutput, chkAlarmBlueOn, chkAlarmBlueOff, chkAlarmBlueFlash)
        RefreshItem(EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Buzzer).DOutput, chkAlarmBuzzerOn, chkAlarmBuzzerOff, chkAlarmBuzzerFlash)

        RefreshItem(EditParam(eMessageLevel.Warning).DOutput(eIndicator.Red).DOutput, chkWarnRedOn, chkWarnRedOff, chkWarnRedFlash)
        RefreshItem(EditParam(eMessageLevel.Warning).DOutput(eIndicator.Yellow).DOutput, chkWarnYellowOn, chkWarnYellowOff, chkWarnYellowFlash)
        RefreshItem(EditParam(eMessageLevel.Warning).DOutput(eIndicator.Green).DOutput, chkWarnGreenOn, chkWarnGreenOff, chkWarnGreenFlash)
        RefreshItem(EditParam(eMessageLevel.Warning).DOutput(eIndicator.Blue).DOutput, chkWarnBlueOn, chkWarnBlueOff, chkWarnBlueFlash)
        RefreshItem(EditParam(eMessageLevel.Warning).DOutput(eIndicator.Buzzer).DOutput, chkWarnBuzzerOn, chkWarnBuzzerOff, chkWarnBuzzerFlash)

        RefreshItem(EditParam(eMessageLevel.Idle).DOutput(eIndicator.Red).DOutput, chkIdleRedOn, chkIdleRedOff, chkIdleRedFlash)
        RefreshItem(EditParam(eMessageLevel.Idle).DOutput(eIndicator.Yellow).DOutput, chkIdleYellowOn, chkIdleYellowOff, chkIdleYellowFlash)
        RefreshItem(EditParam(eMessageLevel.Idle).DOutput(eIndicator.Green).DOutput, chkIdleGreenOn, chkIdleGreenOff, chkIdleGreenFlash)
        RefreshItem(EditParam(eMessageLevel.Idle).DOutput(eIndicator.Blue).DOutput, chkIdleBlueOn, chkIdleBlueOff, chkIdleBlueFlash)
        RefreshItem(EditParam(eMessageLevel.Idle).DOutput(eIndicator.Buzzer).DOutput, chkIdleBuzzerOn, chkIdleBuzzerOff, chkIdleBuzzerFlash)

        RefreshItem(EditParam(eMessageLevel.Information).DOutput(eIndicator.Red).DOutput, chkInfoRedOn, chkInfoRedOff, chkInfoRedFlash)
        RefreshItem(EditParam(eMessageLevel.Information).DOutput(eIndicator.Yellow).DOutput, chkInfoYellowOn, chkInfoYellowOff, chkInfoYellowFlash)
        RefreshItem(EditParam(eMessageLevel.Information).DOutput(eIndicator.Green).DOutput, chkInfoGreenOn, chkInfoGreenOff, chkInfoGreenFlash)
        RefreshItem(EditParam(eMessageLevel.Information).DOutput(eIndicator.Blue).DOutput, chkInfoBlueOn, chkInfoBlueOff, chkInfoBlueFlash)
        RefreshItem(EditParam(eMessageLevel.Information).DOutput(eIndicator.Buzzer).DOutput, chkInfoBuzzerOn, chkInfoBuzzerOff, chkInfoBuzzerFlash)

        RefreshItem(EditParam(eMessageLevel.Running).DOutput(eIndicator.Red).DOutput, chkRunRedOn, chkRunRedOff, chkRunRedFlash)
        RefreshItem(EditParam(eMessageLevel.Running).DOutput(eIndicator.Yellow).DOutput, chkRunYellowOn, chkRunYellowOff, chkRunYellowFlash)
        RefreshItem(EditParam(eMessageLevel.Running).DOutput(eIndicator.Green).DOutput, chkRunGreenOn, chkRunGreenOff, chkRunGreenFlash)
        RefreshItem(EditParam(eMessageLevel.Running).DOutput(eIndicator.Blue).DOutput, chkRunBlueOn, chkRunBlueOff, chkRunBlueFlash)
        RefreshItem(EditParam(eMessageLevel.Running).DOutput(eIndicator.Buzzer).DOutput, chkRunBuzzerOn, chkRunBuzzerOff, chkRunBuzzerFlash)

    End Sub

#Region "Error Red"

    Private Sub chkErrorRedOn_Click(sender As Object, e As EventArgs) Handles chkErrorRedOn.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkErrorRedOn.Checked = True
        chkErrorRedOff.Checked = False
        chkErrorRedFlash.Checked = False
        chkErrorRedOn.BackColor = Color.Red
        chkErrorRedOff.BackColor = SystemColors.Control

    End Sub
    Private Sub chkErrorRedOff_Click(sender As Object, e As EventArgs) Handles chkErrorRedOff.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
        chkErrorRedOn.Checked = False
        chkErrorRedOff.Checked = True
        chkErrorRedFlash.Checked = False
    End Sub
    Private Sub chkErrorRedFlash_Click(sender As Object, e As EventArgs) Handles chkErrorRedFlash.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Red).DOutput = eDOBehavior.Flash
        chkErrorRedOn.Checked = False
        chkErrorRedOff.Checked = False
        chkErrorRedFlash.Checked = True
    End Sub
#End Region
    
#Region "Error Yellow"
    Private Sub chkErrorYellowOn_Click(sender As Object, e As EventArgs) Handles chkErrorYellowOn.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkErrorYellowOn.Checked = True
        chkErrorYellowOff.Checked = False
        chkErrorYellowFlash.Checked = False
    End Sub

    Private Sub chkErrorYellowOff_Click(sender As Object, e As EventArgs) Handles chkErrorYellowOff.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
        chkErrorYellowOn.Checked = False
        chkErrorYellowOff.Checked = True
        chkErrorYellowFlash.Checked = False
    End Sub
    Private Sub chkErrorYellowFlash_Click(sender As Object, e As EventArgs) Handles chkErrorYellowFlash.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Flash
        chkErrorYellowOn.Checked = False
        chkErrorYellowOff.Checked = False
        chkErrorYellowFlash.Checked = True
    End Sub
#End Region
    
#Region "Error Green"
    Private Sub chkErrorGreenOn_Click(sender As Object, e As EventArgs) Handles chkErrorGreenOn.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkErrorGreenOn.Checked = True
        chkErrorGreenOff.Checked = False
        chkErrorGreenFlash.Checked = False
    End Sub

    Private Sub chkErrorGreenOff_Click(sender As Object, e As EventArgs) Handles chkErrorGreenOff.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
        chkErrorGreenOn.Checked = False
        chkErrorGreenOff.Checked = True
        chkErrorGreenFlash.Checked = False
    End Sub

    Private Sub chkErrorGreenFlash_Click(sender As Object, e As EventArgs) Handles chkErrorGreenFlash.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Green).DOutput = eDOBehavior.Flash
        chkErrorGreenOn.Checked = False
        chkErrorGreenOff.Checked = False
        chkErrorGreenFlash.Checked = True
    End Sub
#End Region
   
#Region "Error Blue"
    Private Sub chkErrorBlueOn_Click(sender As Object, e As EventArgs) Handles chkErrorBlueOn.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkErrorBlueOn.Checked = True
        chkErrorBlueOff.Checked = False
        chkErrorBlueFlash.Checked = False
    End Sub

    Private Sub chkErrorBlueOff_Click(sender As Object, e As EventArgs) Handles chkErrorBlueOff.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
        chkErrorBlueOn.Checked = False
        chkErrorBlueOff.Checked = True
        chkErrorBlueFlash.Checked = False
    End Sub

    Private Sub chkErrorBlueFlash_Click(sender As Object, e As EventArgs) Handles chkErrorBlueFlash.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Flash
        chkErrorBlueOn.Checked = False
        chkErrorBlueOff.Checked = False
        chkErrorBlueFlash.Checked = True
    End Sub
#End Region

#Region "Error Buzzer"
    Private Sub chkErrorBuzzerOn_Click(sender As Object, e As EventArgs) Handles chkErrorBuzzerOn.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkErrorBuzzerOn.Checked = True
        chkErrorBuzzerOff.Checked = False
        chkErrorBuzzerFlash.Checked = False
    End Sub

    Private Sub chkErrorBuzzerOff_Click(sender As Object, e As EventArgs) Handles chkErrorBuzzerOff.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off
        chkErrorBuzzerOn.Checked = False
        chkErrorBuzzerOff.Checked = True
        chkErrorBuzzerFlash.Checked = False
    End Sub
    Private Sub chkErrorBuzzerFlash_Click(sender As Object, e As EventArgs) Handles chkErrorBuzzerFlash.Click
        EditParam(eMessageLevel.Error).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Flash
        chkErrorBuzzerOn.Checked = False
        chkErrorBuzzerOff.Checked = False
        chkErrorBuzzerFlash.Checked = True
    End Sub
#End Region

#Region "Alarm Red"
    Private Sub chkAlarmRedOn_Click(sender As Object, e As EventArgs) Handles chkAlarmRedOn.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkAlarmRedOn.Checked = True
        chkAlarmRedOff.Checked = False
        chkAlarmRedFlash.Checked = False
    End Sub

    Private Sub chkAlarmRedOff_Click(sender As Object, e As EventArgs) Handles chkAlarmRedOff.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
        chkAlarmRedOn.Checked = False
        chkAlarmRedOff.Checked = True
        chkAlarmRedFlash.Checked = False
    End Sub

    Private Sub chkAlarmRedFlash_Click(sender As Object, e As EventArgs) Handles chkAlarmRedFlash.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Red).DOutput = eDOBehavior.Flash
        chkAlarmRedOn.Checked = False
        chkAlarmRedOff.Checked = False
        chkAlarmRedFlash.Checked = True
    End Sub
#End Region
    
#Region "Alarm Yellow"
    Private Sub chkAlarmYellowOn_Click(sender As Object, e As EventArgs) Handles chkAlarmYellowOn.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkAlarmYellowOn.Checked = True
        chkAlarmYellowOff.Checked = False
        chkAlarmYellowFlash.Checked = False
    End Sub

    Private Sub chkAlarmYellowOff_Click(sender As Object, e As EventArgs) Handles chkAlarmYellowOff.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
        chkAlarmYellowOn.Checked = False
        chkAlarmYellowOff.Checked = True
        chkAlarmYellowFlash.Checked = False
    End Sub

    Private Sub chkAlarmYellowFlash_Click(sender As Object, e As EventArgs) Handles chkAlarmYellowFlash.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Flash
        chkAlarmYellowOn.Checked = False
        chkAlarmYellowOff.Checked = False
        chkAlarmYellowFlash.Checked = True
    End Sub
#End Region
    
#Region "Alarm Green"
    Private Sub chkAlarmGreenOn_Click(sender As Object, e As EventArgs) Handles chkAlarmGreenOn.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkAlarmGreenOn.Checked = True
        chkAlarmGreenOff.Checked = False
        chkAlarmGreenFlash.Checked = False
    End Sub

    Private Sub chkAlarmGreenOff_Click(sender As Object, e As EventArgs) Handles chkAlarmGreenOff.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
        chkAlarmGreenOn.Checked = False
        chkAlarmGreenOff.Checked = True
        chkAlarmGreenFlash.Checked = False
    End Sub

    Private Sub chkAlarmGreenFlash_Click(sender As Object, e As EventArgs) Handles chkAlarmGreenFlash.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Green).DOutput = eDOBehavior.Flash
        chkAlarmGreenOn.Checked = False
        chkAlarmGreenOff.Checked = False
        chkAlarmGreenFlash.Checked = True
    End Sub
#End Region

#Region "Alarm Blue"
    Private Sub chkAlarmBlueOn_Click(sender As Object, e As EventArgs) Handles chkAlarmBlueOn.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkAlarmBlueOn.Checked = True
        chkAlarmBlueOff.Checked = False
        chkAlarmBlueFlash.Checked = False
    End Sub

    Private Sub chkAlarmBlueOff_Click(sender As Object, e As EventArgs) Handles chkAlarmBlueOff.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
        chkAlarmBlueOn.Checked = False
        chkAlarmBlueOff.Checked = True
        chkAlarmBlueFlash.Checked = False
    End Sub

    Private Sub chkAlarmBlueFlash_Click(sender As Object, e As EventArgs) Handles chkAlarmBlueFlash.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Flash
        chkAlarmBlueOn.Checked = False
        chkAlarmBlueOff.Checked = False
        chkAlarmBlueFlash.Checked = True
    End Sub
#End Region

#Region "Alarm Buzzer"
    Private Sub chkAlarmBuzzerOn_Click(sender As Object, e As EventArgs) Handles chkAlarmBuzzerOn.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkAlarmBuzzerOn.Checked = True
        chkAlarmBuzzerOff.Checked = False
        chkAlarmBuzzerFlash.Checked = False
    End Sub

    Private Sub chkAlarmBuzzerOff_Click(sender As Object, e As EventArgs) Handles chkAlarmBuzzerOff.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off
        chkAlarmBuzzerOn.Checked = False
        chkAlarmBuzzerOff.Checked = True
        chkAlarmBuzzerFlash.Checked = False
    End Sub

    Private Sub chkAlarmBuzzerFlash_Click(sender As Object, e As EventArgs) Handles chkAlarmBuzzerFlash.Click
        EditParam(eMessageLevel.Alarm).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Flash
        chkAlarmBuzzerOn.Checked = False
        chkAlarmBuzzerOff.Checked = False
        chkAlarmBuzzerFlash.Checked = True
    End Sub
#End Region

#Region "Warn Red"
    Private Sub chkWarnRedOn_Click(sender As Object, e As EventArgs) Handles chkWarnRedOn.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkWarnRedOn.Checked = True
        chkWarnRedOff.Checked = False
        chkWarnRedFlash.Checked = False
    End Sub

    Private Sub chkWarnRedOff_Click(sender As Object, e As EventArgs) Handles chkWarnRedOff.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
        chkWarnRedOn.Checked = False
        chkWarnRedOff.Checked = True
        chkWarnRedFlash.Checked = False
    End Sub

    Private Sub chkWarnRedFlash_Click(sender As Object, e As EventArgs) Handles chkWarnRedFlash.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Red).DOutput = eDOBehavior.Flash
        chkWarnRedOn.Checked = False
        chkWarnRedOff.Checked = False
        chkWarnRedFlash.Checked = True
    End Sub

#End Region

#Region "Warn Yellow"
    Private Sub chkWarnYellowOn_Click(sender As Object, e As EventArgs) Handles chkWarnYellowOn.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkWarnYellowOn.Checked = True
        chkWarnYellowOff.Checked = False
        chkWarnYellowFlash.Checked = False
    End Sub

    Private Sub chkWarnYellowOff_Click(sender As Object, e As EventArgs) Handles chkWarnYellowOff.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
        chkWarnYellowOn.Checked = False
        chkWarnYellowOff.Checked = True
        chkWarnYellowFlash.Checked = False
    End Sub

    Private Sub chkWarnYellowFlash_Click(sender As Object, e As EventArgs) Handles chkWarnYellowFlash.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Flash
        chkWarnYellowOn.Checked = False
        chkWarnYellowOff.Checked = False
        chkWarnYellowFlash.Checked = True
    End Sub

#End Region

#Region "Warn Green"
    Private Sub chkWarnGreenOn_Click(sender As Object, e As EventArgs) Handles chkWarnGreenOn.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkWarnGreenOn.Checked = True
        chkWarnGreenOff.Checked = False
        chkWarnGreenFlash.Checked = False
    End Sub

    Private Sub chkWarnGreenOff_Click(sender As Object, e As EventArgs) Handles chkWarnGreenOff.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
        chkWarnGreenOn.Checked = False
        chkWarnGreenOff.Checked = True
        chkWarnGreenFlash.Checked = False
    End Sub

    Private Sub chkWarnGreenFlash_Click(sender As Object, e As EventArgs) Handles chkWarnGreenFlash.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Green).DOutput = eDOBehavior.Flash
        chkWarnGreenOn.Checked = False
        chkWarnGreenOff.Checked = False
        chkWarnGreenFlash.Checked = True
    End Sub

#End Region

#Region "Warn Blue"
    Private Sub chkWarnBlueOn_Click(sender As Object, e As EventArgs) Handles chkWarnBlueOn.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkWarnBlueOn.Checked = True
        chkWarnBlueOff.Checked = False
        chkWarnBlueFlash.Checked = False
    End Sub

    Private Sub chkWarnBlueOff_Click(sender As Object, e As EventArgs) Handles chkWarnBlueOff.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
        chkWarnBlueOn.Checked = False
        chkWarnBlueOff.Checked = True
        chkWarnBlueFlash.Checked = False
    End Sub

    Private Sub chkWarnBlueFlash_Click(sender As Object, e As EventArgs) Handles chkWarnBlueFlash.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Flash
        chkWarnBlueOn.Checked = False
        chkWarnBlueOff.Checked = False
        chkWarnBlueFlash.Checked = True
    End Sub

#End Region

#Region "Warn Buzzer"
    Private Sub chkWarnBuzzerOn_Click(sender As Object, e As EventArgs) Handles chkWarnBuzzerOn.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkWarnBuzzerOn.Checked = True
        chkWarnBuzzerOff.Checked = False
        chkWarnBuzzerFlash.Checked = False
    End Sub

    Private Sub chkWarnBuzzerOff_Click(sender As Object, e As EventArgs) Handles chkWarnBuzzerOff.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off
        chkWarnBuzzerOn.Checked = False
        chkWarnBuzzerOff.Checked = True
        chkWarnBuzzerFlash.Checked = False
    End Sub

    Private Sub chkWarnBuzzerFlash_Click(sender As Object, e As EventArgs) Handles chkWarnBuzzerFlash.Click
        EditParam(eMessageLevel.Warning).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Flash
        chkWarnBuzzerOn.Checked = False
        chkWarnBuzzerOff.Checked = False
        chkWarnBuzzerFlash.Checked = True
    End Sub

#End Region

#Region "Idle Red"
    Private Sub chkIdleRedOn_Click(sender As Object, e As EventArgs) Handles chkIdleRedOn.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkIdleRedOn.Checked = True
        chkIdleRedOff.Checked = False
        chkIdleRedFlash.Checked = False
    End Sub

    Private Sub chkIdleRedOff_Click(sender As Object, e As EventArgs) Handles chkIdleRedOff.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
        chkIdleRedOn.Checked = False
        chkIdleRedOff.Checked = True
        chkIdleRedFlash.Checked = False
    End Sub

    Private Sub chkIdleRedFlash_Click(sender As Object, e As EventArgs) Handles chkIdleRedFlash.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Red).DOutput = eDOBehavior.Flash
        chkIdleRedOn.Checked = False
        chkIdleRedOff.Checked = False
        chkIdleRedFlash.Checked = True
    End Sub
#End Region
    
#Region "Idle Yellow"
    Private Sub chkIdleYellowOn_Click(sender As Object, e As EventArgs) Handles chkIdleYellowOn.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkIdleYellowOn.Checked = True
        chkIdleYellowOff.Checked = False
        chkIdleYellowFlash.Checked = False
    End Sub

    Private Sub chkIdleYellowOff_Click(sender As Object, e As EventArgs) Handles chkIdleYellowOff.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
        chkIdleYellowOn.Checked = False
        chkIdleYellowOff.Checked = True
        chkIdleYellowFlash.Checked = False
    End Sub

    Private Sub chkIdleYellowFlash_Click(sender As Object, e As EventArgs) Handles chkIdleYellowFlash.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Flash
        chkIdleYellowOn.Checked = False
        chkIdleYellowOff.Checked = False
        chkIdleYellowFlash.Checked = True
    End Sub
#End Region
    
#Region "Idle Green"
    Private Sub chkIdleGreenOn_Click(sender As Object, e As EventArgs) Handles chkIdleGreenOn.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkIdleGreenOn.Checked = True
        chkIdleGreenOff.Checked = False
        chkIdleGreenFlash.Checked = False
    End Sub

    Private Sub chkIdleGreenOff_Click(sender As Object, e As EventArgs) Handles chkIdleGreenOff.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
        chkIdleGreenOn.Checked = False
        chkIdleGreenOff.Checked = True
        chkIdleGreenFlash.Checked = False
    End Sub

    Private Sub chkIdleGreenFlash_Click(sender As Object, e As EventArgs) Handles chkIdleGreenFlash.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Green).DOutput = eDOBehavior.Flash
        chkIdleGreenOn.Checked = False
        chkIdleGreenOff.Checked = False
        chkIdleGreenFlash.Checked = True
    End Sub
#End Region
    
#Region "Idle Blue"
    Private Sub chkIdleBlueOn_Click(sender As Object, e As EventArgs) Handles chkIdleBlueOn.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkIdleBlueOn.Checked = True
        chkIdleBlueOff.Checked = False
        chkIdleBlueFlash.Checked = False
    End Sub

    Private Sub chkIdleBlueOff_Click(sender As Object, e As EventArgs) Handles chkIdleBlueOff.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
        chkIdleBlueOn.Checked = False
        chkIdleBlueOff.Checked = True
        chkIdleBlueFlash.Checked = False
    End Sub

    Private Sub chkIdleBlueFlash_Click(sender As Object, e As EventArgs) Handles chkIdleBlueFlash.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Flash
        chkIdleBlueOn.Checked = False
        chkIdleBlueOff.Checked = False
        chkIdleBlueFlash.Checked = True
    End Sub
#End Region
   
#Region "Idle Buzzer"
    Private Sub chkIdleBuzzerOn_Click(sender As Object, e As EventArgs) Handles chkIdleBuzzerOn.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkIdleBuzzerOn.Checked = True
        chkIdleBuzzerOff.Checked = False
        chkIdleBuzzerFlash.Checked = False
    End Sub

    Private Sub chkIdleBuzzerOff_Click(sender As Object, e As EventArgs) Handles chkIdleBuzzerOff.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off
        chkIdleBuzzerOn.Checked = False
        chkIdleBuzzerOff.Checked = True
        chkIdleBuzzerFlash.Checked = False
    End Sub

    Private Sub chkIdleBuzzerFlash_Click(sender As Object, e As EventArgs) Handles chkIdleBuzzerFlash.Click
        EditParam(eMessageLevel.Idle).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Flash
        chkIdleBuzzerOn.Checked = False
        chkIdleBuzzerOff.Checked = False
        chkIdleBuzzerFlash.Checked = True
    End Sub
#End Region
   
#Region "Info Red"
    Private Sub chkInfoRedOn_Click(sender As Object, e As EventArgs) Handles chkInfoRedOn.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkInfoRedOn.Checked = True
        chkInfoRedOff.Checked = False
        chkInfoRedFlash.Checked = False
    End Sub

    Private Sub chkInfoRedOff_Click(sender As Object, e As EventArgs) Handles chkInfoRedOff.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
        chkInfoRedOn.Checked = False
        chkInfoRedOff.Checked = True
        chkInfoRedFlash.Checked = False
    End Sub

    Private Sub chkInfoRedFlash_Click(sender As Object, e As EventArgs) Handles chkInfoRedFlash.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Red).DOutput = eDOBehavior.Flash
        chkInfoRedOn.Checked = False
        chkInfoRedOff.Checked = False
        chkInfoRedFlash.Checked = True
    End Sub
#End Region

#Region "Info Yellow"
    Private Sub chkInfoYellowOn_Click(sender As Object, e As EventArgs) Handles chkInfoYellowOn.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkInfoYellowOn.Checked = True
        chkInfoYellowOff.Checked = False
        chkInfoYellowFlash.Checked = False
    End Sub

    Private Sub chkInfoYellowOff_Click(sender As Object, e As EventArgs) Handles chkInfoYellowOff.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
        chkInfoYellowOn.Checked = False
        chkInfoYellowOff.Checked = True
        chkInfoYellowFlash.Checked = False
    End Sub

    Private Sub chkInfoYellowFlash_Click(sender As Object, e As EventArgs) Handles chkInfoYellowFlash.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Flash
        chkInfoYellowOn.Checked = False
        chkInfoYellowOff.Checked = False
        chkInfoYellowFlash.Checked = True
    End Sub
#End Region

#Region "Info Green"
    Private Sub chkInfoGreenOn_Click(sender As Object, e As EventArgs) Handles chkInfoGreenOn.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkInfoGreenOn.Checked = True
        chkInfoGreenOff.Checked = False
        chkInfoGreenFlash.Checked = False
    End Sub

    Private Sub chkInfoGreenOff_Click(sender As Object, e As EventArgs) Handles chkInfoGreenOff.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
        chkInfoGreenOn.Checked = False
        chkInfoGreenOff.Checked = True
        chkInfoGreenFlash.Checked = False
    End Sub

    Private Sub chkInfoGreenFlash_Click(sender As Object, e As EventArgs) Handles chkInfoGreenFlash.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Green).DOutput = eDOBehavior.Flash
        chkInfoGreenOn.Checked = False
        chkInfoGreenOff.Checked = False
        chkInfoGreenFlash.Checked = True
    End Sub

#End Region

#Region "Info Blue"
    Private Sub chkInfoBlueOn_Click(sender As Object, e As EventArgs) Handles chkInfoBlueOn.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkInfoBlueOn.Checked = True
        chkInfoBlueOff.Checked = False
        chkInfoBlueFlash.Checked = False
    End Sub

    Private Sub chkInfoBlueOff_Click(sender As Object, e As EventArgs) Handles chkInfoBlueOff.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
        chkInfoBlueOn.Checked = False
        chkInfoBlueOff.Checked = True
        chkInfoBlueFlash.Checked = False
    End Sub

    Private Sub chkInfoBlueFlash_Click(sender As Object, e As EventArgs) Handles chkInfoBlueFlash.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Flash
        chkInfoBlueOn.Checked = False
        chkInfoBlueOff.Checked = False
        chkInfoBlueFlash.Checked = True
    End Sub
#End Region

#Region "Info Buzzer"
    Private Sub chkInfoBuzzerOn_Click(sender As Object, e As EventArgs) Handles chkInfoBuzzerOn.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkInfoBuzzerOn.Checked = True
        chkInfoBuzzerOff.Checked = False
        chkInfoBuzzerFlash.Checked = False
    End Sub

    Private Sub chkInfoBuzzerOff_Click(sender As Object, e As EventArgs) Handles chkInfoBuzzerOff.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off
        chkInfoBuzzerOn.Checked = False
        chkInfoBuzzerOff.Checked = True
        chkInfoBuzzerFlash.Checked = False
    End Sub

    Private Sub chkInfoBuzzerFlash_Click(sender As Object, e As EventArgs) Handles chkInfoBuzzerFlash.Click
        EditParam(eMessageLevel.Information).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Flash
        chkInfoBuzzerOn.Checked = False
        chkInfoBuzzerOff.Checked = False
        chkInfoBuzzerFlash.Checked = True
    End Sub

#End Region

#Region "Run Red"
    Private Sub chkRunRedOn_Click(sender As Object, e As EventArgs) Handles chkRunRedOn.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkRunRedOn.Checked = True
        chkRunRedOff.Checked = False
        chkRunRedFlash.Checked = False
    End Sub

    Private Sub chkRunRedOff_Click(sender As Object, e As EventArgs) Handles chkRunRedOff.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkRunRedOn.Checked = False
        chkRunRedOff.Checked = True
        chkRunRedFlash.Checked = False
    End Sub

    Private Sub chkRunRedFlash_Click(sender As Object, e As EventArgs) Handles chkRunRedFlash.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
        chkRunRedOn.Checked = False
        chkRunRedOff.Checked = False
        chkRunRedFlash.Checked = True
    End Sub

#End Region

#Region "Run Yellow"
    Private Sub chkRunYellowOn_Click(sender As Object, e As EventArgs) Handles chkRunYellowOn.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkRunYellowOn.Checked = True
        chkRunYellowOff.Checked = False
        chkRunYellowFlash.Checked = False
    End Sub

    Private Sub chkRunYellowOff_Click(sender As Object, e As EventArgs) Handles chkRunYellowOff.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkRunYellowOn.Checked = False
        chkRunYellowOff.Checked = True
        chkRunYellowFlash.Checked = False
    End Sub

    Private Sub chkRunYellowFlash_Click(sender As Object, e As EventArgs) Handles chkRunYellowFlash.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
        chkRunYellowOn.Checked = False
        chkRunYellowOff.Checked = False
        chkRunYellowFlash.Checked = True
    End Sub

#End Region

#Region "Run Green"
    Private Sub chkRunGreenOn_Click(sender As Object, e As EventArgs) Handles chkRunGreenOn.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkRunGreenOn.Checked = True
        chkRunGreenOff.Checked = False
        chkRunGreenFlash.Checked = False
    End Sub

    Private Sub chkRunGreenOff_Click(sender As Object, e As EventArgs) Handles chkRunGreenOff.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkRunGreenOn.Checked = False
        chkRunGreenOff.Checked = True
        chkRunGreenFlash.Checked = False
    End Sub

    Private Sub chkRunGreenFlash_Click(sender As Object, e As EventArgs) Handles chkRunGreenFlash.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
        chkRunGreenOn.Checked = False
        chkRunGreenOff.Checked = False
        chkRunGreenFlash.Checked = True
    End Sub

#End Region

#Region "Run Blue"
    Private Sub chkRunBlueOn_Click(sender As Object, e As EventArgs) Handles chkRunBlueOn.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkRunBlueOn.Checked = True
        chkRunBlueOff.Checked = False
        chkRunBlueFlash.Checked = False
    End Sub

    Private Sub chkRunBlueOff_Click(sender As Object, e As EventArgs) Handles chkRunBlueOff.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkRunBlueOn.Checked = False
        chkRunBlueOff.Checked = True
        chkRunBlueFlash.Checked = False
    End Sub

    Private Sub chkRunBlueFlash_Click(sender As Object, e As EventArgs) Handles chkRunBlueFlash.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Blue).DOutput = eDOBehavior.On
        chkRunBlueOn.Checked = False
        chkRunBlueOff.Checked = False
        chkRunBlueFlash.Checked = True
    End Sub
#End Region

#Region "Run Buzzer"
    Private Sub chkRunBuzzerOn_Click(sender As Object, e As EventArgs) Handles chkRunBuzzerOn.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkRunBuzzerOn.Checked = True
        chkRunBuzzerOff.Checked = False
        chkRunBuzzerFlash.Checked = False
    End Sub

    Private Sub chkRunBuzzerOff_Click(sender As Object, e As EventArgs) Handles chkRunBuzzerOff.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off
        chkRunBuzzerOn.Checked = False
        chkRunBuzzerOff.Checked = True
        chkRunBuzzerFlash.Checked = False
    End Sub

    Private Sub chkRunBuzzerFlash_Click(sender As Object, e As EventArgs) Handles chkRunBuzzerFlash.Click
        EditParam(eMessageLevel.Running).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On
        chkRunBuzzerOn.Checked = False
        chkRunBuzzerOff.Checked = False
        chkRunBuzzerFlash.Checked = True
    End Sub
#End Region

    Private Sub frmLightTowerConfig_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
    
 
    Private Sub frmLightTowerConfig_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
End Class
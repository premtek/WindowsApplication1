Imports ProjectCore
Imports ProjectValveController

''' <summary>
''' 手動連線介面
''' </summary>
''' <remarks></remarks>
Public Class frmPicoController


    Private mPicoParamFileName As String
    Public Property _mPicoParamFileName() As String
        Get
            Return mPicoParamFileName
        End Get
        Set(ByVal value As String)
            mPicoParamFileName = value
        End Set
    End Property

    Private mSelectValve As Integer
    Public Property _mSelectValve() As Integer
        Get
            Return mSelectValve
        End Get
        Set(ByVal value As Integer)
            mSelectValve = value
        End Set
    End Property


    ''' <summary>
    ''' 讀取參數檔
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Function LoadPicocontrolerParam(ByVal fileName As String) As Boolean
        Try

            cbxValveMode.SelectedIndex = ReadIniString("PicocontrolerParam", "Mode", fileName, "")
            txtPulse.Text = ReadIniString("PicocontrolerParam", "Pulse", fileName, "")
            txtOpen.Text = ReadIniString("PicocontrolerParam", "Open", fileName, "")
            txtClose.Text = ReadIniString("PicocontrolerParam", "Close", fileName, "")
            txtCycle.Text = ReadIniString("PicocontrolerParam", "Cycle", fileName, "")
            txtStroke.Text = ReadIniString("PicocontrolerParam", "Stroke", fileName, "")
            txtCount.Text = ReadIniString("PicocontrolerParam", "Count", fileName, "")
            txtCloseVolts.Text = ReadIniString("PicocontrolerParam", "CloseVolts", fileName, "")
            'txtv.Text = ReadIniString("PicocontrolerParam", "CloseVolts", sTemp, "")
            txtNozzleHeaters.Text = ReadIniString("PicocontrolerParam", "NozzleHeaters", fileName, "")
            lbFreq.Text = 1 / CDbl(txtCycle.Text)
            'txt.Text = ReadIniString("PicocontrolerParam", "CloseVolts", sTemp, "")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub frmPicoController_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        gValvecontrollerCollection.RegCommandResponse(mSelectValve, txtCommandResponse)
    End Sub



    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub


    Private Sub btnValveStatus_Click(sender As Object, e As EventArgs) Handles btnValveStatus.Click
        lstValveStatus.Items.Clear()
        Dim vStatus As sPicoValveStatus = New sPicoValveStatus
        gValvecontrollerCollection.GetValveStatus(mSelectValve, vStatus, True)
        lstValveStatus.Items.Add("ValvePower:" & vStatus.sValvePower)
        lstValveStatus.Items.Add("Mode:" & vStatus.sMode)
        lstValveStatus.Items.Add("Pulse:" & vStatus.sPulse)
        lstValveStatus.Items.Add("Cycle:" & vStatus.sCycle)
        lstValveStatus.Items.Add("Count:" & vStatus.sCount)
        lstValveStatus.Items.Add("Profile Rise:" & vStatus.sProfileRise)
        lstValveStatus.Items.Add("Profile Fall:" & vStatus.sProfileFall)
        lstValveStatus.Items.Add("Stroke:" & vStatus.sStroke)
        lstValveStatus.Items.Add("Up Ramp Time:" & vStatus.sUpRampTime)
        lstValveStatus.Items.Add("DwnRampTime:" & vStatus.sDwnRampTime)
        lstValveStatus.Items.Add("NumShots:" & vStatus.sNumShots)

    End Sub

    Private Sub btnHeaterStatus_Click(sender As Object, e As EventArgs) Handles btnHeaterStatus.Click
        lstHeaterStatus.Items.Clear()
        Dim vStatus As sPicoValveHeaterStatus = New sPicoValveHeaterStatus
        gValvecontrollerCollection.GetHeaterStatus(mSelectValve, vStatus, True)
        lstHeaterStatus.Items.Add("ValvePower:" & vStatus.sMode)
        lstHeaterStatus.Items.Add("ValveMode:" & vStatus.sACT)
        lstHeaterStatus.Items.Add("ValvePulse:" & vStatus.sStack)
    End Sub

    Private Sub btnValvePowerOnOff_Click(sender As Object, e As EventArgs) Handles btnValvePowerOnOff.Click
        'Dim sTemp As String = ""
        If btnValvePowerOnOff.Text = "ON" Then
            btnValvePowerOnOff.Text = "Off"
            gValvecontrollerCollection.SetValvePower(mSelectValve, False, True)
        Else
            btnValvePowerOnOff.Text = "ON"
            gValvecontrollerCollection.SetValvePower(mSelectValve, True, True)
        End If

        'gVavlecontrolerCollection.SetValvePower(mSelectValve, True, sTemp, True)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAlarmReset.Click
        gValvecontrollerCollection.ResetAlarm(mSelectValve, False)
    End Sub

    Private Sub btnSetValveMode_Click(sender As Object, e As EventArgs) Handles btnSetValveMode.Click
        'Dim sTemp As String = ""
        If cbxValveMode.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            ' gVavlecontrolerCollection.SetValveMode(mSelectValve, CInt(cbxValveMode.Text.Substring(0, 1)), sTemp, True)
            Select Case cbxValveMode.SelectedIndex
                Case 0
                    gValvecontrollerCollection.SetValveMode(mSelectValve, enmValveModeType.Timed, True)
                Case 1
                    gValvecontrollerCollection.SetValveMode(mSelectValve, enmValveModeType.Purge, True)
                Case 2
                    gValvecontrollerCollection.SetValveMode(mSelectValve, enmValveModeType.Continuous, True)
                Case 3
                    gValvecontrollerCollection.SetValveMode(mSelectValve, enmValveModeType.ReadCurrentMode, True)

            End Select

        End If


    End Sub

    Private Sub btnValvePulse_Click(sender As Object, e As EventArgs) Handles btnValvePulse.Click
        'Dim sTemp As String = ""
        If txtPulse.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetValveOnTime(mSelectValve, CDbl(txtPulse.Text), True)
        End If


    End Sub

    Private Sub btnValveOpen_Click(sender As Object, e As EventArgs) Handles btnValveOpen.Click
        'Dim sTemp As String = ""
        If txtOpen.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetOpenTime(mSelectValve, CDbl(txtOpen.Text), True)
        End If

    End Sub

    Private Sub btnValveClose_Click(sender As Object, e As EventArgs) Handles btnValveClose.Click
        'Dim sTemp As String = ""
        If txtClose.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetCloseTime(mSelectValve, CDbl(txtClose.Text), True)
        End If

    End Sub

    Private Sub btnValveCycle_Click(sender As Object, e As EventArgs) Handles btnValveCycle.Click
        'Dim sTemp As String = ""
        If txtCycle.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetValveCycleTime(mSelectValve, CDbl(txtCycle.Text), True)
        End If

    End Sub

    Private Sub btnValveStroke_Click(sender As Object, e As EventArgs) Handles btnValveStroke.Click
        'Dim sTemp As String = ""
        If txtStroke.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetStrokeValve(mSelectValve, CDbl(txtStroke.Text), True)
        End If

    End Sub

    Private Sub btnValveCount_Click(sender As Object, e As EventArgs) Handles btnValveCount.Click
        ' Dim sTemp As String = ""
        If txtCount.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetValveDispenseCount(mSelectValve, CInt(txtCount.Text), True)
        End If


    End Sub

    Private Sub btnCloseVolts_Click(sender As Object, e As EventArgs) Handles btnCloseVolts.Click
        ' Dim sTemp As String = ""
        If txtCloseVolts.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetCloseVoltage(mSelectValve, CInt(txtCloseVolts.Text), True)
        End If


    End Sub

    Private Sub btnNozzleHeaters_Click(sender As Object, e As EventArgs) Handles btnNozzleHeaters.Click
        'Dim sTemp As String = ""
        If txtNozzleHeaters.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetHeaterTemperature(mSelectValve, CInt(txtNozzleHeaters.Text), True)
        End If


    End Sub
    Private Sub txtPulse_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPulse.KeyPress, txtStroke.KeyPress, txtOpen.KeyPress, txtCycle.KeyPress, txtClose.KeyPress
        If e.KeyChar = Chr(48) Or e.KeyChar = Chr(49) Or e.KeyChar = Chr(50) Or
           e.KeyChar = Chr(51) Or e.KeyChar = Chr(52) Or e.KeyChar = Chr(53) Or
           e.KeyChar = Chr(54) Or e.KeyChar = Chr(55) Or e.KeyChar = Chr(56) Or
           e.KeyChar = Chr(57) Or e.KeyChar = Chr(13) Or e.KeyChar = Chr(8) Or
           e.KeyChar = Chr(46) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub
    Private Sub txtCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCount.KeyPress, txtNozzleHeaters.KeyPress
        If e.KeyChar = Chr(48) Or e.KeyChar = Chr(49) Or e.KeyChar = Chr(50) Or
           e.KeyChar = Chr(51) Or e.KeyChar = Chr(52) Or e.KeyChar = Chr(53) Or
           e.KeyChar = Chr(54) Or e.KeyChar = Chr(55) Or e.KeyChar = Chr(56) Or
           e.KeyChar = Chr(57) Or e.KeyChar = Chr(13) Or e.KeyChar = Chr(8) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub

    Private Sub btnSetHeaterMode_Click(sender As Object, e As EventArgs) Handles btnSetHeaterMode.Click
        'Dim sTemp As String = ""
        If cbxHeaterMode.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetHeaterMode(mSelectValve, CInt(cbxHeaterMode.Text.Substring(0, 1)), True)
        End If


    End Sub



    Private Sub btnCycleOnOff_Click(sender As Object, e As EventArgs) Handles btnCycleOnOff.Click
        Dim sTemp As String = ""
        If btnCycleOnOff.Text = "ON" Then
            gValvecontrollerCollection.SetCycleOnOff(mSelectValve, False, sTemp, True)
            btnCycleOnOff.Text = "OFF"
        Else
            gValvecontrollerCollection.SetCycleOnOff(mSelectValve, True, sTemp, True)
            btnCycleOnOff.Text = "ON"
        End If

        ' gVavlecontrolerCollection.SetCycleOnOff(mSelectValve, True, sTemp, True)
    End Sub



    Private Sub btnSetOffTime_Click(sender As Object, e As EventArgs) Handles btnSetOffTime.Click
        'Dim sTemp As String = ""
        If txtOffTime.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetValveOffTime(mSelectValve, txtOffTime.Text, True)
        End If
    End Sub

    Private Sub btnSetCloseProfile_Click(sender As Object, e As EventArgs) Handles btnSetCloseProfile.Click
        'Dim sTemp As String = ""
        If cbxCloseProfile.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetCloseProfile(mSelectValve, CInt(cbxCloseProfile.Text.Substring(0, 1)), True)
        End If


    End Sub

    Private Sub btnSetOpenProfile_Click(sender As Object, e As EventArgs) Handles btnSetOpenProfile.Click
        'Dim sTemp As String = ""
        If cbxCloseProfile.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("檻位未輸入")
        Else
            gValvecontrollerCollection.SetOpenProfile(mSelectValve, CInt(cbxOpenProfile.Text.Substring(0, 1)), True)
        End If


    End Sub




    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnManyCommandTest.Click
        Dim sTemp As String = ""
        Dim Stopwatch As Stopwatch = New Stopwatch

        Dim dTime As Long = Stopwatch.ElapsedMilliseconds
        Call Stopwatch.Restart()
        gValvecontrollerCollection.SetValveMode(mSelectValve, CInt(cbxValveMode.Text.Substring(0, 1)), sTemp, True)
        gValvecontrollerCollection.SetValveOnTime(mSelectValve, CDbl(txtPulse.Text), sTemp, True)
        gValvecontrollerCollection.SetValveOffTime(mSelectValve, txtOffTime.Text, sTemp, True)
        gValvecontrollerCollection.SetValveDispenseCount(mSelectValve, CInt(txtCount.Text), sTemp, True)
        lbTime.Text = Math.Abs(dTime - Stopwatch.ElapsedMilliseconds).ToString()
    End Sub

    Private Sub frmPicoController_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        gValvecontrollerCollection.RegCommandResponse(mSelectValve, Nothing)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        gSyslog.Save("[frmPicoController]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
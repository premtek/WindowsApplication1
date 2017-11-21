Imports ProjectCore
Imports ProjectAOI

Public Class frmProgramControlLight

    Function GetString(ByVal value As String) As String
        Select Case value
            Case "Controller1"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Controller1"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "控制器1"
                End Select
            Case "Controller2"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Controller2"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "控制器2"
                End Select
            Case "Controller3"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Controller3"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "控制器3"
                End Select
            Case "Controller4"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Controller4"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "控制器4"
                End Select
            Case Else
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "No Controller"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "無此控制器"
                End Select

        End Select
        Return "No Controller"
    End Function
    Private Sub frmProgramControlLight_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Select Case enmLightController.Max
            Case 0
                cmbController.Items.Add(GetString("Controller1"))
                cmbController.SelectedIndex = 0
                cmbController.Visible = False
            Case 1
                cmbController.Items.Add(GetString("Controller1"))
                cmbController.Items.Add(GetString("Controller2"))
                cmbController.SelectedIndex = 0
                cmbController.Visible = True
            Case 2
                cmbController.Items.Add(GetString("Controller1"))
                cmbController.Items.Add(GetString("Controller2"))
                cmbController.Items.Add(GetString("Controller3"))
                cmbController.SelectedIndex = 0
                cmbController.Visible = True
            Case 3
                cmbController.Items.Add(GetString("Controller1"))
                cmbController.Items.Add(GetString("Controller2"))
                cmbController.Items.Add(GetString("Controller3"))
                cmbController.Items.Add(GetString("Controller4"))
                cmbController.SelectedIndex = 0
                cmbController.Visible = True
        End Select

        '[Note]Ernest要求電流正規化 0-255
        nmuLightValue1.Maximum = 255 'gLightCollection.GetCCDLightMaxValue(cmbController.SelectedIndex, enmValveLight.No1)
        nmuLightValue2.Maximum = 255 'gLightCollection.GetCCDLightMaxValue(cmbController.SelectedIndex, enmValveLight.No2)
        nmuLightValue3.Maximum = 255 'gLightCollection.GetCCDLightMaxValue(cmbController.SelectedIndex, enmValveLight.No3)
        nmuLightValue4.Maximum = 255 'gLightCollection.GetCCDLightMaxValue(cmbController.SelectedIndex, enmValveLight.No4)
    End Sub




    Private Sub frmProgramControlLight_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If cmbController.SelectedIndex < 0 Then cmbController.SelectedIndex = 0
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'Sue20170627
        gSyslog.Save("[frmPicoValveControllerTest]" & vbTab & "[btnOK]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnSetlight4_Click(sender As Object, e As EventArgs) Handles btnSetlight4.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnSetlight4]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnSetlight4.Enabled = False Then
            Exit Sub
        End If
        btnSetlight4.Enabled = False
        Dim LightCh As Integer   'wenda 20160614
        LightCh = 3

        'gLightCollection.Items(cmbController.SelectedIndex).SetLightValue(LightCh, nmuLightValue4.Value, True)
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh4.Text = value

        '[Note]電流正規化0-255
        gLightCollection.SetCCDLight(cmbController.SelectedIndex, LightCh, nmuLightValue4.Value, True)
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh4.Text = value

        btnSetlight4.Enabled = True
    End Sub

    Private Sub btnSetlight3_Click(sender As Object, e As EventArgs) Handles btnSetlight3.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnSetlight3]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnSetlight3.Enabled = False Then
            Exit Sub
        End If
        btnSetlight3.Enabled = False
        Dim LightCh As Integer   'wenda 20160614
        LightCh = 2

        'gLightCollection.Items(cmbController.SelectedIndex).SetLightValue(LightCh, nmuLightValue3.Value, True)
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh3.Text = value

        '[Note]電流正規化0-255
        gLightCollection.SetCCDLight(cmbController.SelectedIndex, LightCh, nmuLightValue3.Value, True)
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh3.Text = value

        btnSetlight3.Enabled = True
    End Sub

    Private Sub btnSetlight2_Click(sender As Object, e As EventArgs) Handles btnSetlight2.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnSetlight2]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnSetlight2.Enabled = False Then
            Exit Sub
        End If
        btnSetlight2.Enabled = False
        Dim LightCh As Integer   'wenda 20160614
        LightCh = 1

        'gLightCollection.Items(cmbController.SelectedIndex).SetLightValue(LightCh, nmuLightValue2.Value, True)
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh2.Text = value

        '[Note]電流正規化0-255
        gLightCollection.SetCCDLight(cmbController.SelectedIndex, LightCh, nmuLightValue2.Value, True)
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh2.Text = value

        btnSetlight2.Enabled = True
    End Sub

    Private Sub btnSetlight_Click(sender As Object, e As EventArgs) Handles btnSetlight.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnSetlight]" & vbTab & "Click")

        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnSetlight.Enabled = False Then
            Exit Sub
        End If
        btnSetlight.Enabled = False
        Dim LightCh As Integer   'wenda 20160614
        LightCh = 0

        'gLightCollection.Items(cmbController.SelectedIndex).SetLightValue(LightCh, nmuLightValue1.Value, True)
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh1.Text = value


        '[Note]電流正規化0-255
        gLightCollection.SetCCDLight(cmbController.SelectedIndex, LightCh, nmuLightValue1.Value, True)
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh1.Text = value

        btnSetlight.Enabled = True
    End Sub

    Private Sub btnGetLight4_Click(sender As Object, e As EventArgs) Handles btnGetLight4.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnGetLight4]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnGetLight4.Enabled = False Then
            Exit Sub
        End If
        btnGetLight4.Enabled = False
        Dim LightCh As Integer

        LightCh = 3
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh4.Text = value

        '[Note]電流正規化
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh4.Text = value

        btnGetLight4.Enabled = True
    End Sub

    Private Sub btnGetLight3_Click(sender As Object, e As EventArgs) Handles btnGetLight3.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnGetLight3]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnGetLight3.Enabled = False Then
            Exit Sub
        End If
        btnGetLight3.Enabled = False
        Dim LightCh As Integer

        LightCh = 2
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh3.Text = value

        '[Note]電流正規化
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh3.Text = value

        btnGetLight3.Enabled = True
    End Sub

    Private Sub btnGetLight2_Click(sender As Object, e As EventArgs) Handles btnGetLight2.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnGetLight2]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnGetLight2.Enabled = False Then
            Exit Sub
        End If
        btnGetLight2.Enabled = False
        Dim LightCh As Integer

        LightCh = 1
        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh2.Text = value

        '[Note]電流正規化
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh2.Text = value

        btnGetLight2.Enabled = True
    End Sub


    Private Sub btnGetLight_Click(sender As Object, e As EventArgs) Handles btnGetLight.Click
        'Sue20170627
        gSyslog.Save("[frmProgramControlLight]" & vbTab & "[btnGetLight]" & vbTab & "Click")
        If cmbController.SelectedIndex < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000040))
            MsgBox(gMsgHandler.GetMessage(Warn_3000040), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If btnGetLight.Enabled = False Then
            Exit Sub
        End If
        btnGetLight.Enabled = False
        Dim LightCh As Integer

        LightCh = 0

        'Dim value As Integer
        'gLightCollection.Items(cmbController.SelectedIndex).GetLightValue(LightCh, value, True)
        'lblLightCh1.Text = value

        '[Note]電流正規化
        Dim value As Integer
        gLightCollection.GetCCDLight(cmbController.SelectedIndex, LightCh, value, True)
        lblLightCh1.Text = value

        btnGetLight.Enabled = True
    End Sub

End Class
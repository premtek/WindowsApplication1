Imports ProjectAOI
Imports ProjectCore
Imports ProjectIO

Public Class ucLightControl
    Public SceneName As String = ""
    Public CCDNo As Integer


    Public Sub ShowUI()

        'lightBar1.Maximum = gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No1)
        'lightBar2.Maximum = gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No2)
        'lightBar3.Maximum = gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No3)
        'lightBar4.Maximum = gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No4)
        'gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No1)
        'gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No2)
        'gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No3)
        'gLightCollection.GetCCDLightMaxValue(CCDNo, enmValveLight.No4)

        'SetLightBarValue(lightBar1, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1))
        'SetLightBarValue(lightBar2, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2))
        'SetLightBarValue(lightBar3, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3))
        'SetLightBarValue(lightBar4, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4))
        If Not SceneName Is Nothing Then 'Soni + 2016.09.24 場景不能為空
            If SceneName <> "" Then 'Soni + 2016.09.24 場景不能是空字串
                If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then 'Soni + 2016.09.24 需要包含該場景
                    SetNumericUpDownValue(nmcLight1, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1))
                    SetNumericUpDownValue(nmcLight2, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2))
                    SetNumericUpDownValue(nmcLight3, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3))
                    SetNumericUpDownValue(nmcLight4, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4))

                    chkLight1.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
                    chkLight2.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
                    chkLight3.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
                    chkLight4.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)
                End If
            End If
        End If
        
        chkLight1_CheckedChanged(Nothing, Nothing)
        chkLight2_CheckedChanged(Nothing, Nothing)
        chkLight3_CheckedChanged(Nothing, Nothing)
        chkLight4_CheckedChanged(Nothing, Nothing)

        Dim light1 As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No1)
        If light1 = -1 Then
            'lightBar1.Enabled = False
            nmcLight1.Enabled = False
            chkLight1.Enabled = False
        End If
        Dim light2 As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No2)
        If light2 = -1 Then
            'lightBar2.Enabled = False
            nmcLight2.Enabled = False
            chkLight2.Enabled = False
        End If
        Dim light3 As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No3)
        If light3 = -1 Then
            'lightBar3.Enabled = False
            nmcLight3.Enabled = False
            chkLight3.Enabled = False
        End If
        Dim light4 As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No4)
        If light4 = -1 Then
            'lightBar4.Enabled = False
            nmcLight4.Enabled = False
            chkLight4.Enabled = False
        End If


    End Sub

    Private Sub chkLight1_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight1.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1) = chkLight1.Checked
        'lightBar1.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
        nmcLight1.Enabled = chkLight1.Checked 'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No1)
        SetLightOnOff(light, chkLight1.Checked)
    End Sub

    Private Sub chkLight2_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight2.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2) = chkLight2.Checked
        'lightBar2.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
        nmcLight2.Enabled = chkLight2.Checked 'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No2)
        SetLightOnOff(light, chkLight2.Checked)
    End Sub

    Private Sub chkLight3_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight3.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3) = chkLight3.Checked
        'lightBar3.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
        nmcLight3.Enabled = chkLight3.Checked 'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No3)
        SetLightOnOff(light, chkLight3.Checked)
    End Sub

    Private Sub chkLight4_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight4.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4) = chkLight4.Checked
        'lightBar4.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)
        nmcLight4.Enabled = chkLight4.Checked 'gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4) 
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No4)
        SetLightOnOff(light, chkLight4.Checked)
    End Sub

    Sub SetLightOnOff(ByVal light As enmLight, ByVal value As Boolean)
        Select Case light
            Case enmLight.No1
                gDOCollection.SetState(enmDO.CCDLight, value)
            Case enmLight.No2
                gDOCollection.SetState(enmDO.CCDLight2, value)
            Case enmLight.No3
                gDOCollection.SetState(enmDO.CCDLight3, value)
            Case enmLight.No4
                gDOCollection.SetState(enmDO.CCDLight4, value)
            Case enmLight.No5
                gDOCollection.SetState(enmDO.CCDLight5, value)
            Case enmLight.No6
                gDOCollection.SetState(enmDO.CCDLight6, value)
            Case enmLight.No7
                gDOCollection.SetState(enmDO.CCDLight7, value)
            Case enmLight.No8
                gDOCollection.SetState(enmDO.CCDLight8, value)
        End Select

    End Sub



    ''' <summary>設定上下按鈕控制項數值(含最大最小值保護)</summary>
    ''' <param name="nmc"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Sub SetNumericUpDownValue(ByRef nmc As System.Windows.Forms.NumericUpDown, ByVal value As Decimal)
        If value > nmc.Maximum Then
            nmc.Value = nmc.Maximum
            Exit Sub
        End If
        If value < nmc.Minimum Then
            nmc.Value = nmc.Minimum
            Exit Sub
        End If
        nmc.Value = value
    End Sub

    'Sub SetLightBarValue(ByRef lightBar As System.Windows.Forms.TrackBar, ByVal value As Decimal)
    '    If value > lightBar.Maximum Then
    '        lightBar.Value = lightBar.Maximum
    '        Exit Sub
    '    End If
    '    If value < lightBar.Minimum Then
    '        lightBar.Value = lightBar.Minimum
    '        Exit Sub
    '    End If
    '    lightBar.Value = value
    'End Sub

    'Private Sub lightBar2_Scroll(sender As Object, e As EventArgs)
    '    SetNumericUpDownValue(nmcLight2, lightBar2.Value)
    'End Sub

    'Private Sub lightBar3_Scroll(sender As Object, e As EventArgs)
    '    SetNumericUpDownValue(nmcLight3, lightBar3.Value)
    'End Sub

    'Private Sub lightBar4_Scroll(sender As Object, e As EventArgs)
    '    SetNumericUpDownValue(nmcLight4, lightBar4.Value)
    'End Sub

    Sub SetLight1Value(ByVal value As Decimal)
        nmcLight1.Value = value
    End Sub
    Sub SetLight2Value(ByVal value As Decimal)
        nmcLight2.Value = value
    End Sub
    Sub SetLight3Value(ByVal value As Decimal)
        nmcLight3.Value = value
    End Sub
    Sub SetLight4Value(ByVal value As Decimal)
        nmcLight4.Value = value
    End Sub

    Sub SetLight1_OnOff(ByVal State As Boolean)
        chkLight1.Checked = State
    End Sub
    Sub SetLight2_OnOff(ByVal State As Boolean)
        chkLight2.Checked = State
    End Sub
    Sub SetLight3_OnOff(ByVal State As Boolean)
        chkLight3.Checked = State
    End Sub
    Sub SetLight4_OnOff(ByVal State As Boolean)
        chkLight4.Checked = State
    End Sub

    Private Sub nmcLight1_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight1.ValueChanged
        Dim value As Decimal = nmcLight1.Value
        ''SetLightBarValue(lightBar1, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No1)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1) = value
    End Sub

    Private Sub nmcLight2_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight2.ValueChanged
        Dim value As Decimal = nmcLight2.Value
        ''SetLightBarValue(lightBar2, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No2)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2) = value
    End Sub

    Private Sub nmcLight3_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight3.ValueChanged
        Dim value As Decimal = nmcLight3.Value
        ''SetLightBarValue(lightBar3, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No3)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3) = value
    End Sub

    Private Sub nmcLight4_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight4.ValueChanged
        Dim value As Decimal = nmcLight4.Value
        ''SetLightBarValue(lightBar4, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No4)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4) = value
    End Sub

    'Private Sub ucLightControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    'End Sub
    Public Function GetLight1Value() As Integer
        Return nmcLight1.Value
    End Function

    Public Function GetLight2Value() As Integer
        Return nmcLight2.Value
    End Function

    Public Function GetLight3Value() As Integer
        Return nmcLight3.Value
    End Function

    Public Function GetLight4Value() As Integer
        Return nmcLight4.Value
    End Function

    Public Function GetLight1_OnOff() As Boolean
        If chkLight1.Checked Then
            Return True
        End If
        Return False
    End Function

    Public Function GetLight2_OnOff() As Boolean
        If chkLight2.Checked Then
            Return True
        End If
        Return False
    End Function

    Public Function GetLight3_OnOff() As Boolean
        If chkLight3.Checked Then
            Return True
        End If
        Return False
    End Function

    Public Function GetLight4_OnOff() As Boolean
        If chkLight4.Checked Then
            Return True
        End If
        Return False
    End Function

    Private Sub btnSetLight1_Click(sender As Object, e As EventArgs)

        Dim value As Decimal = nmcLight1.Value
        'SetLightBarValue(lightBar1, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No1)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1) = value
    End Sub

    Private Sub btnSetLight2_Click(sender As Object, e As EventArgs)

        Dim value As Decimal = nmcLight2.Value
        'SetLightBarValue(lightBar2, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No2)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2) = value
    End Sub

    Private Sub btnSetLight3_Click(sender As Object, e As EventArgs)

        Dim value As Decimal = nmcLight3.Value
        'SetLightBarValue(lightBar3, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No3)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3) = value
    End Sub

    Private Sub btnSetLight4_Click(sender As Object, e As EventArgs)

        Dim value As Decimal = nmcLight4.Value
        'SetLightBarValue(lightBar4, value)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(CCDNo, enmValveLight.No4)
        gLightCollection.SetCCDLight(CCDNo, light, value, True)
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4) = value
    End Sub
    'Eason 20170120 Ticket:100030 , Memory Freed [S]
    Public Sub ManualDispose()
        Me.Dispose(True)
    End Sub
    'Eason 20170120 Ticket:100030 , Memory Freed [E]

End Class

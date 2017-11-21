Imports ProjectCore
Imports ProjectIO

Public Class frmEPVConfig
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEPVConfig))

    Function GetString(ByVal value As String) As String
        Select Case value
            Case "Valve1"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Valve1"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "閥1"
                End Select

            Case "Valve2"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Valve2"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "閥2"
                End Select
            Case "Valve3"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Valve3"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "閥3"
                End Select
            Case "Valve4"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Valve4"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "閥4"
                End Select
        End Select
        Return "Unknown"
    End Function
    Private Sub frmEPVConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbCardType.Items.Clear()
        cmbCardType.Items.Add("0 None")
        cmbCardType.Items.Add("1 AI Adapter")
        cmbCardType.Items.Add("2 ITV Series")
        cmbEPV.Items.Clear()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cmbEPV.Items.Add(GetString("Valve1"))
                cmbEPV.Items.Add(GetString("Valve2"))
                cmbEPV.Items.Add(GetString("Valve3"))
                cmbEPV.Items.Add(GetString("Valve4"))
                lblSelectIem.Visible = True
                cmbEPV.Visible = True

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                cmbEPV.Items.Add(GetString("Valve1"))
                cmbEPV.Items.Add(GetString("Valve2"))
                lblSelectIem.Visible = True
                cmbEPV.Visible = True

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cmbEPV.Items.Add(GetString("Valve1"))
                        lblSelectIem.Visible = False
                        cmbEPV.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        cmbEPV.Items.Add(GetString("Valve1"))
                        cmbEPV.Items.Add(GetString("Valve2"))
                        lblSelectIem.Visible = True
                        cmbEPV.Visible = True
                End Select
        End Select

        With cmbBaudRate.Items
            .Clear()
            .Add(CInt(enmBaudRate.e9600))
            .Add(CInt(enmBaudRate.e14400))
            .Add(CInt(enmBaudRate.e19200))
            .Add(CInt(enmBaudRate.e38400))
            .Add(CInt(enmBaudRate.e57600))
            .Add(CInt(enmBaudRate.e115200))
        End With
        cmbCOMPort.Items.Clear()
        cmbCOMPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames)

        If cmbEPV.Items.Count > 0 Then cmbEPV.SelectedIndex = 0

    End Sub

    Private Sub cmbEPV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEPV.SelectedIndexChanged
        If cmbEPV.SelectedIndex < 0 Then
            '請先選擇電空閥!
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000068))
            MsgBox(gMsgHandler.GetMessage(Warn_3000068), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Select Electro Pneumatic Valve First, Please!")
            Exit Sub
        End If
        With gEPVCollection.Cards.Parameters(cmbEPV.SelectedIndex)
            If cmbCardType.Items.Count > .CardType Then
                cmbCardType.SelectedIndex = CInt(.CardType)
            End If
            If cmbCOMPort.Items.Contains(.ITV_Series.PortName) Then
                cmbCOMPort.SelectedItem = .ITV_Series.PortName
            End If
            If cmbBaudRate.Items.Contains(.ITV_Series.BaudRate) Then
                cmbBaudRate.SelectedItem = .ITV_Series.BaudRate
            End If

        End With
    End Sub


    Private Sub btnSet_Click(sender As Object, e As EventArgs) Handles btnSet.Click
        gSyslog.Save("[frmEPVConfig]" & vbTab & "[btnSet]" & vbTab & "Click")
        If btnSet.Enabled = False Then
            Exit Sub
        End If
        btnSet.Enabled = False
        If cmbEPV.SelectedIndex < 0 Then
            '請先選擇電空閥!
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000068))
            MsgBox(gMsgHandler.GetMessage(Warn_3000068), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Select Electro Pneumatic Valve First, Please!")
            btnSet.Enabled = True
            Exit Sub
        End If
        gEPVCollection.SetValue(cmbEPV.SelectedIndex, nmuValue.Value, True)
        gSyslog.Save("Set Value:" & nmuValue.Value)
        btnSet.Enabled = True
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        gSyslog.Save("[frmEPVConfig]" & vbTab & "[btnGet]" & vbTab & "Click")
        If btnGet.Enabled = False Then
            Exit Sub
        End If
        btnGet.Enabled = False
        If cmbEPV.SelectedIndex < 0 Then
            '請先選擇電空閥!
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000068))
            MsgBox(gMsgHandler.GetMessage(Warn_3000068), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Select Electro Pneumatic Valve First, Please!")
            btnGet.Enabled = True
            Exit Sub
        End If
        Dim value As Decimal
        gEPVCollection.GetValue(cmbEPV.SelectedIndex, value, True) '2016.06.25 仿Trigger版修改
        lblValue.Text = value.ToString("F2") '顯示小數點後2位
        gSyslog.Save("Get Value:" & value)
        btnGet.Enabled = True
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmEPVConfig]" & vbTab & "[btnOK]" & vbTab & "Click")
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        btnOK.Enabled = False
        gEPVCollection.Save(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigEPValve.ini")

        '20161010
        With gEPVCollection.Cards.Parameters(cmbEPV.SelectedIndex)
            Dim mCard As New sEPVCardParameter
            Dim mITV_Series As New System.IO.Ports.SerialPort

            mCard.CardType = cmbCardType.SelectedIndex

            If cmbCOMPort.SelectedItem <> Nothing Then
                mITV_Series.PortName = cmbCOMPort.SelectedItem
            End If

            If cmbBaudRate.SelectedItem <> Nothing Then
                mITV_Series.BaudRate = cmbBaudRate.SelectedItem
            End If
            mCard.ITV_Series = mITV_Series

            gEPVCollection.Cards.Parameters(cmbEPV.SelectedIndex) = mCard

        End With

        Dim strFileName = Application.StartupPath & "\System\" & MachineName & "\CardIO.ini"
        gEPVCollection.Cards.Save(strFileName)

        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        btnOK.Enabled = True
        'Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmEPVConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
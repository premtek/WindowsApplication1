Imports ProjectCore

Public Class frmSafePos


    Private Sub frmSafePos_Load(sender As Object, e As EventArgs) Handles Me.Load

        '[Note]:無防撞判定需求
        If Not (gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Or gSSystemParameter.MachineType = enmMachineType.eDTS_2S2V Or gSSystemParameter.MachineType = enmMachineType.DCS_500AD) Then
            Me.Close()
            Exit Sub
        End If

        Dim strSection As String

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                strSection = "MachineA Safe Data"
                gSSystemParameter.MachineSafeData(0).Load(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", strSection)
                strSection = "MachineB Safe Data"
                gSSystemParameter.MachineSafeData(1).Load(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", strSection)

                grpSpanA.Visible = True
                grpSpanB.Visible = True


                SetNumericValue(nmcSafeDistanceAX, gSSystemParameter.MachineSafeData(0).SafeDistanceX)
                SetNumericValue(nmcSafeDistanceAY, gSSystemParameter.MachineSafeData(0).SafeDistanceY)
                SetNumericValue(nmcSpanAX, gSSystemParameter.MachineSafeData(0).SpreadX)
                SetNumericValue(nmcSpanAY, gSSystemParameter.MachineSafeData(0).SpreadY)

                SetNumericValue(nmcSafeDistanceBX, gSSystemParameter.MachineSafeData(1).SafeDistanceX)
                SetNumericValue(nmcSafeDistanceBY, gSSystemParameter.MachineSafeData(1).SafeDistanceY)
                SetNumericValue(nmcSpanBX, gSSystemParameter.MachineSafeData(1).SpreadX)
                SetNumericValue(nmcSpanBY, gSSystemParameter.MachineSafeData(1).SpreadY)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                strSection = "MachineA Safe Data"
                gSSystemParameter.MachineSafeData(0).Load(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", strSection)

                grpSpanA.Visible = True
                grpSpanB.Visible = False

                SetNumericValue(nmcSafeDistanceAX, gSSystemParameter.MachineSafeData(0).SafeDistanceX)
                SetNumericValue(nmcSafeDistanceAY, gSSystemParameter.MachineSafeData(0).SafeDistanceY)
                SetNumericValue(nmcSpanAX, gSSystemParameter.MachineSafeData(0).SpreadX)
                SetNumericValue(nmcSpanAY, gSSystemParameter.MachineSafeData(0).SpreadY)
        End Select

    End Sub

    ''' <summary>設定數值 過大過小時,背景顯示為紅色而不套用數值</summary>
    ''' <param name="nmc"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Sub SetNumericValue(ByRef nmc As NumericUpDown, ByVal value As Decimal)
        If value > nmc.Maximum Then
            nmc.BackColor = Color.Red
            Exit Sub
        End If
        If value < nmc.Minimum Then
            nmc.BackColor = Color.Red
            Exit Sub
        End If
        nmc.Value = value
        nmc.BackColor = Color.White
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSafePos]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSafePos]" & vbTab & "[btnOK]" & vbTab & "Click")
        Dim mMachineA As SSaftPosData
        Dim mMachineB As SSaftPosData
        Dim strSection As String


        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ

                With mMachineA
                    .SafeDistanceX = nmcSafeDistanceAX.Value
                    .SafeDistanceY = nmcSafeDistanceAY.Value
                    .SpreadX = nmcSpanAX.Value
                    .SpreadY = nmcSpanAY.Value
                End With

                With mMachineB
                    .SafeDistanceX = nmcSafeDistanceBX.Value
                    .SafeDistanceY = nmcSafeDistanceBY.Value
                    .SpreadX = nmcSpanBX.Value
                    .SpreadY = nmcSpanBY.Value
                End With

                gSSystemParameter.MachineSafeData.Clear()
                gSSystemParameter.MachineSafeData.Add(mMachineA)
                gSSystemParameter.MachineSafeData.Add(mMachineB)

                strSection = "MachineA Safe Data"
                gSSystemParameter.MachineSafeData(0).Save(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", strSection)
                strSection = "MachineB Safe Data"
                gSSystemParameter.MachineSafeData(1).Save(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", strSection)
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD

                With mMachineA
                    .SafeDistanceX = nmcSafeDistanceAX.Value
                    .SafeDistanceY = nmcSafeDistanceAY.Value
                    .SpreadX = nmcSpanAX.Value
                    .SpreadY = nmcSpanAY.Value
                End With

                gSSystemParameter.MachineSafeData.Clear()
                gSSystemParameter.MachineSafeData.Add(mMachineA)

                strSection = "MachineA Safe Data"
                gSSystemParameter.MachineSafeData(0).Save(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", strSection)
        End Select


        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
    End Sub
End Class
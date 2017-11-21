Imports WetcoConveyor

Public Class ucChuckStatus

    Public Property Machine As Integer
    ''' <summary>更新目標值</summary>
    ''' <param name="targetValue1"></param>
    ''' <param name="targetValue2"></param>
    ''' <param name="targetValue3"></param>
    ''' <param name="targetValue4"></param>
    ''' <param name="targetValue5"></param>
    ''' <param name="targetValue6"></param>
    ''' <remarks></remarks>
    Public Sub UpdateTarget(ByVal targetValue1 As String, ByVal targetValue2 As String, ByVal targetValue3 As String, ByVal targetValue4 As String, ByVal targetValue5 As String, ByVal targetValue6 As String)
        lblSV1.Text = targetValue1
        lblSV2.Text = targetValue2
        lblSV3.Text = targetValue3
        lblSV4.Text = targetValue4
        lblSV5.Text = targetValue5
        lblSV6.Text = targetValue6
    End Sub

    ''' <summary>更新目標溫度</summary>
    ''' <remarks></remarks>
    Public Sub UpdateSV()
        Dim SV1 As String = "NA"
        Dim SV2 As String = "NA"
        Dim SV3 As String = "NA"
        Dim SV4 As String = "NA"
        Dim SV5 As String = "NA"
        Dim SV6 As String = "NA"
        Select Case Machine
            Case 0
                SV1 = Val(Unit.TempController.A1_PidController.SV).ToString
                SV2 = Val(Unit.TempController.A2_PidController.SV).ToString
                SV3 = Val(Unit.TempController.A3_PidController.SV).ToString
                SV4 = Val(Unit.TempController.A4_PidController.SV).ToString
                SV5 = Val(Unit.TempController.A5_PidController.SV).ToString
                SV6 = Val(Unit.TempController.A6_PidController.SV).ToString
            Case 1
                SV1 = Val(Unit.TempController.B1_PidController.SV).ToString
                SV2 = Val(Unit.TempController.B2_PidController.SV).ToString
                SV3 = Val(Unit.TempController.B3_PidController.SV).ToString
                SV4 = Val(Unit.TempController.B4_PidController.SV).ToString
                SV5 = Val(Unit.TempController.B5_PidController.SV).ToString
                SV6 = Val(Unit.TempController.B6_PidController.SV).ToString
        End Select

        UpdateTarget(SV1, SV2, SV3, SV4, SV5, SV6)
    End Sub

    ''' <summary>更新實際溫度</summary>
    ''' <remarks></remarks>
    Public Sub UpdatePV()
        Select Case Machine
            Case 0
                lblPV1.Text = Val(Unit.TempController.A1_PidController.PV).ToString
                lblPV2.Text = Val(Unit.TempController.A2_PidController.PV).ToString
                lblPV3.Text = Val(Unit.TempController.A3_PidController.PV).ToString
                lblPV4.Text = Val(Unit.TempController.A4_PidController.PV).ToString
                lblPV5.Text = Val(Unit.TempController.A5_PidController.PV).ToString
                lblPV6.Text = Val(Unit.TempController.A6_PidController.PV).ToString

            Case 1
                lblPV1.Text = Val(Unit.TempController.B1_PidController.PV).ToString
                lblPV2.Text = Val(Unit.TempController.B2_PidController.PV).ToString
                lblPV3.Text = Val(Unit.TempController.B3_PidController.PV).ToString
                lblPV4.Text = Val(Unit.TempController.B4_PidController.PV).ToString
                lblPV5.Text = Val(Unit.TempController.B5_PidController.PV).ToString
                lblPV6.Text = Val(Unit.TempController.B6_PidController.PV).ToString
        End Select
    End Sub
End Class

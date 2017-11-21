Public Class ucCalibrationStatus
    Public Enum enmCalibrationStatus
        ''' <summary>CCD對閥1校正</summary>
        ''' <remarks></remarks>
        CCD2Valve1 = 0
        ''' <summary>CCD對閥2校正</summary>
        ''' <remarks></remarks>
        CCD2Valve2 = 1
        ''' <summary>Z軸校正</summary>
        ''' <remarks></remarks>
        ZHeight = 2
    End Enum

    Dim mStatus As enmCalibrationStatus


    Public Property Status As enmCalibrationStatus
        Get
            Return mStatus
        End Get
        Set(value As enmCalibrationStatus)
            mStatus = value
            UnselectAll()
            SelectOne(value)
        End Set
    End Property

    Sub UnselectAll()
        'Button1.FlatAppearance.BorderColor = Color.Blue
        'Button2.FlatAppearance.BorderColor = Color.Blue
        'Button3.FlatAppearance.BorderColor = Color.Blue
        'Button4.FlatAppearance.BorderColor = Color.Blue
        Button1.BackColor = SystemColors.Control
        Button2.BackColor = SystemColors.Control
        Button4.BackColor = SystemColors.Control
    End Sub

    Sub SelectOne(ByVal selectedStatus As enmCalibrationStatus)
        If ProjectCore.gSSystemParameter.StageUseValveCount = ProjectCore.eMechanismModule.TwoValveOneStage Then
            Button2.Visible = True
            Button2.Top = Button1.Top + Button1.Height
            Button4.Top = Button2.Top + Button2.Height

        Else
            Button2.Visible = False
            Button4.Top = Button1.Top + Button1.Height

        End If

        Select Case selectedStatus
            Case enmCalibrationStatus.CCD2Valve1
                Button1.BackColor = Color.Lime
                'Button1.FlatAppearance.BorderColor = Color.Lime
            Case enmCalibrationStatus.CCD2Valve2
                'Button2.FlatAppearance.BorderColor = Color.Lime
                Button2.BackColor = Color.Lime
                'Case enmCalibrationStatus.CCD2HeightSensor
                '    'Button3.FlatAppearance.BorderColor = Color.Lime
                '    Button3.BackColor = Color.Lime
            Case enmCalibrationStatus.ZHeight
                'Button4.FlatAppearance.BorderColor = Color.Lime
                Button4.BackColor = Color.Lime
        End Select
    End Sub
End Class


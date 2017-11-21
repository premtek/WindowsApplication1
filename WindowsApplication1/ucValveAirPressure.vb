Imports ProjectRecipe
Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports WetcoConveyor
Imports ProjectValveController
Imports Premtek

Public Class ucValveAirPressure
    Structure sValveAirPressure
        ''' <summary>平台索引</summary>
        ''' <remarks></remarks>
        Dim StageNo As enmStage
        ''' <summary>單平台作業閥No.</summary>
        ''' <remarks></remarks>
        Dim ValveWorkMode As eValveWorkMode
        ''' <summary>
        ''' Syringe電空閥虛擬編號
        ''' </summary>
        ''' <remarks></remarks>
        Dim SyringeEPV As enmEPV
        ''' <summary>
        ''' ValveAP電空閥虛擬編號
        ''' </summary>
        ''' <remarks></remarks>
        Dim ValveAPEPV As enmEPV
        ''' <summary>[Jet Vavle的種類]</summary>
        ''' <remarks></remarks>
        Dim ValveModel As eValveModel
        ''' <summary>閥膠管正壓</summary>
        ''' <remarks></remarks>   
        Dim SyringePressure As Integer
        ''' <summary>閥體正壓 電磁閥</summary>
        ''' <remarks></remarks>
        Dim ValvePressure As Integer

    End Structure
    Private mValveAirPressureData As sValveAirPressure
    Public Sub SetUcValveData(ByVal sValveAirPressureTemp As sValveAirPressure)
        mValveAirPressureData = sValveAirPressureTemp
        If mValveAirPressureData.ValveModel = eValveModel.Advanjet Then
            btnSetSyringeAP.Enabled = True
            btnSetSyringeOnOff.Enabled = True
            btnSetValveAP.Enabled = True
            btnSetValveOnOff.Enabled = True
        ElseIf mValveAirPressureData.ValveModel = eValveModel.PicoPulse Then
            btnSetSyringeAP.Enabled = True
            btnSetSyringeOnOff.Enabled = True
            btnSetValveAP.Visible = False
            btnSetValveOnOff.Visible = False
            lblValveAPSetUnit.Visible = False
            txtValveAPSet.Visible = False
            lbValveApNow.Visible = False
            lbValveApMax.Visible = False
            lblValveAPSet.Visible = False

        End If
        Me.grpValveAir.Text = " Valve Air Pressure " & (mValveAirPressureData.StageNo + 1).ToString
    End Sub

    Private Sub btnSetSyringeAP_Click(sender As Object, e As EventArgs) Handles btnSetSyringeAP.Click
        btnSetSyringeAP.BackColor = Color.Yellow
        'gEPVCollection.SetValue(mValveAirPressureData.SyringeEPV, Val(txtSyringeAPSet.Text), True)
        gEPVCollection.SetValue(eEPVPressureType.Syringe, mValveAirPressureData.SyringeEPV, Val(txtSyringeAPSet.Text), True)
        btnSetSyringeAP.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetSyringeOnOff_Click(sender As Object, e As EventArgs) Handles btnSetSyringeOnOff.Click
        gDOCollection.SetState(mValveAirPressureData.SyringePressure, Not gDOCollection.GetState(mValveAirPressureData.SyringePressure))

        Select Case gDOCollection.GetState(mValveAirPressureData.SyringePressure)
            Case True
                btnSetSyringeOnOff.Text = GetString("On")
            Case False
                btnSetSyringeOnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetValveAP_Click(sender As Object, e As EventArgs) Handles btnSetValveAP.Click
        btnSetSyringeAP.BackColor = Color.Yellow
        'gEPVCollection.SetValue(mValveAirPressureData.ValveAPEPV, Val(txtValveAPSet.Text), True)
        gEPVCollection.SetValue(eEPVPressureType.Syringe, mValveAirPressureData.ValveAPEPV, Val(txtValveAPSet.Text), True)
        btnSetSyringeAP.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetValveOnOff_Click(sender As Object, e As EventArgs) Handles btnSetValveOnOff.Click
        gDOCollection.SetState(mValveAirPressureData.ValvePressure, Not gDOCollection.GetState(mValveAirPressureData.ValvePressure))
        Select Case gDOCollection.GetState(mValveAirPressureData.ValvePressure)
            Case True
                btnSetValveOnOff.Text = GetString("On")
            Case False
                btnSetValveOnOff.Text = GetString("Off")
        End Select
    End Sub


    'Soni + 2016.12.10
    ''' <summary>本頁用語系轉換</summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetString(ByVal value As String) As String
        Select Case value
            Case "On"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "開"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "Off"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "關"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "Save OK."
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "存檔完成"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "None"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "無"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "OnTimer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "計時"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "OnRuns"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "計次"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "OnTimerOrRuns"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "計時或計次先到"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select

        End Select
        Return value
    End Function

    

    'Private mStageNo As enmStage
    ' ''' <summary>平台索引</summary>
    ' ''' <remarks></remarks>
    'Public Property _StageNo As enmStage
    '    Get
    '        Return mStageNo
    '    End Get
    '    Set(ByVal value As enmStage)
    '        Me.grpValveAir.Text = "Valve Air Pressure " & value.ToString
    '        mStageNo = value
    '    End Set
    'End Property

    'Private mValveWorkMode As eValveWorkMode
    ' ''' <summary>單平台作業閥No.</summary>
    ' ''' <remarks></remarks>
    'Public Property _ValveWorkMode As eValveWorkMode
    '    Get
    '        Return mValveWorkMode
    '    End Get
    '    Set(ByVal value As eValveWorkMode)
    '        mValveWorkMode = value
    '    End Set
    'End Property

    'Private mSyringeEPV As enmEPV
    ' ''' <summary>
    ' ''' Syringe電空閥虛擬編號
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property _SyringeEPV() As enmEPV
    '    Get
    '        Return mSyringeEPV
    '    End Get
    '    Set(ByVal value As enmEPV)
    '        mSyringeEPV = value
    '    End Set
    'End Property

    'Private mValveAPEPV As enmEPV
    ' ''' <summary>
    ' ''' Syringe電空閥虛擬編號
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property _ValveAPEPV() As enmEPV
    '    Get
    '        Return mValveAPEPV
    '    End Get
    '    Set(ByVal value As enmEPV)
    '        mValveAPEPV = value
    '    End Set
    'End Property

    'Private mValveModel As eValveModel
    ' ''' <summary>[Jet Vavle的種類]</summary>
    ' ''' <remarks></remarks>
    'Public Property _ValveModel() As eValveModel
    '    Get
    '        Return mValveModel
    '    End Get
    '    Set(ByVal value As eValveModel)
    '        mValveModel = value
    '    End Set
    'End Property


    'Private mSyringePressure As Integer
    ' ''' <summary>
    ' ''' 氣壓Do編號
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property _SyringePressure As Integer
    '    Get
    '        Return mSyringePressure
    '    End Get
    '    Set(ByVal value As Integer)
    '        mSyringePressure = value
    '    End Set
    'End Property

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        mValveAirPressureData = New sValveAirPressure
    End Sub
End Class

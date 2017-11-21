Imports ProjectIO
Imports ProjectCore

Public Structure HotPlate
    Public HotPlate1 As Boolean
    Public HotPlate2 As Boolean
    Public HotPlate3 As Boolean
    Public HotPlate4 As Boolean
    Public HotPlate5 As Boolean
    Public HotPlate6 As Boolean
End Structure

Public Module mGlobalPool
    Public CvSMEMA As New List(Of ISMEMA)
    'Public SMEMA As New clsSMEMA
    'Public SMEMA2 As New clsCv2SMEMA
    Public Unit As clsUnit
    Public cls800AQ_LUL As cls800AQLul

    Public License As clsLicens

    ''' <summary>
    '''  溫度Alarm range
    ''' </summary>
    ''' <remarks>SV = 50, AlarmRange = 10, 則溫度大於60或小於40時發出alarm</remarks>
    Public AlarmRange As Integer

    Dim _sv As UInteger
    ''' <summary>
    ''' 溫控器設定溫度
    ''' </summary>
    Public Property SV As UInteger
        Get
            Return _sv
        End Get
        Set(value As UInteger)
            _sv = value
            TempUpperLimit = IIf(_sv + AlarmRange > 0, _sv + AlarmRange, 0)
            TempLowerLimit = IIf(_sv - AlarmRange > 0, _sv - AlarmRange, 0)
        End Set
    End Property

    ''' <summary>
    ''' Alarm的溫度上限
    ''' </summary>
    Public TempUpperLimit As Integer

    ''' <summary>
    ''' Alarm的溫度下限
    ''' </summary>
    Public TempLowerLimit As Integer

    ''' <summary>
    ''' 監控旗標:溫度進入上下限時旗標為true並開始執行溫度監控
    ''' </summary>
    Dim LimitFlag(11) As Boolean

    ''' <summary>
    ''' hot plate是否過溫
    ''' </summary>
    Public Overheat As Boolean

    Dim SvFlag As UInteger
    ''' <summary>
    ''' 檢查Hot Plate是否過溫
    ''' </summary>
    ''' <param name="sv">溫控器目標溫度</param>
    ''' <param name="max">Alarm溫度上限</param>
    ''' <param name="min">Alarm溫度下限</param>
    ''' <returns>false : 合理溫度(溫度於上下限範圍內), true : 過高溫/過低溫(溫度於上下限範圍外)</returns>   
    ''' <remarks></remarks>
    Public Function CheckOverheat(ByVal sv As UInteger, ByVal max As UInteger, ByVal min As UInteger) As Boolean
        Dim hpIO() As Boolean = {gDOCollection.GetState(enmDO.HeaterOn1), gDOCollection.GetState(enmDO.HeaterOn2), gDOCollection.GetState(enmDO.HeaterOn3), gDOCollection.GetState(enmDO.HeaterOn4), gDOCollection.GetState(enmDO.HeaterOn5), gDOCollection.GetState(enmDO.HeaterOn6), _
                                 gDOCollection.GetState(enmDO.HeaterOn7), gDOCollection.GetState(enmDO.HeaterOn8), gDOCollection.GetState(enmDO.HeaterOn9), gDOCollection.GetState(enmDO.HeaterOn10), gDOCollection.GetState(enmDO.HeaterOn11), gDOCollection.GetState(enmDO.HeaterOn12)}

        If (SvFlag <> sv) Then '防止目標溫度改變時造成錯誤判斷
            SvFlag = sv
            For i = 0 To hpIO.Length - 1
                LimitFlag(i) = False
            Next
        End If

        For i = 0 To hpIO.Length - 1
            If hpIO(i) Then
                If (WetcoConveyor.Unit.TempController.arrPidController(i).PV < max AndAlso _
                    WetcoConveyor.Unit.TempController.arrPidController(i).PV > min AndAlso _
                    LimitFlag(i) = False) Then
                    LimitFlag(i) = True '須先進入極限範圍內,才開始執行監控
                End If

                If (LimitFlag(i)) Then
                    If (WetcoConveyor.Unit.TempController.arrPidController(i).PV > max Or _
                        WetcoConveyor.Unit.TempController.arrPidController(i).PV < min) Then
                        LimitFlag(i) = False
                        'gEqpMsg.AddHistoryAlarm("A0000", "CheckOverheat", 0, "NO." & (i + 1).ToString() & " temperature controller over heat", eMessageLevel.Alarm)
                        Return True
                    End If
                End If
            Else
                LimitFlag(i) = False '防止加熱IO關閉太久,溫度下降至最小值之下
            End If
        Next
        Return False
    End Function

    'Public CvUnit As New List(Of IConveyorUnit)
    'Public CvSMEMA As New List(Of ISMEMA)
    'Public CvRoller As New List(Of IRoller)
    'Public PidWatcher As New clsPidWatcher

End Module
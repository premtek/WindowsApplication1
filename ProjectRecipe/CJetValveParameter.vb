Imports ProjectCore


' ''' <summary>[Stroke數值範圍]</summary>
' ''' <remarks></remarks>
'Public Structure sStrokeRange
'    ''' <summary>[上限值(volts)]</summary>
'    ''' <remarks></remarks>
'    Public MaxValue As Integer
'    ''' <summary>[下限值(volts)]</summary>
'    ''' <remarks></remarks>
'    Public MinValue As Integer
'    ''' <summary>[設定值(%)]</summary>
'    ''' <remarks></remarks>
'    Public Value As Integer
'End Structure

Public Class CJetValveParameter
    ''' <summary>[名稱]</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>[Jet Valve種類]</summary>
    ''' <remarks></remarks>
    Public ValveModel As eValveModel

    ''' <summary>[備註]</summary>
    ''' <remarks></remarks>
    Public Comment As String

    Public PicoTouch As sPicoTouch

    Public Advanjet As sAdvanjet

    ''' <summary>[Pico Touch]</summary>
    ''' <remarks></remarks>
    Public Structure sPicoTouch
        ''' <summary>[上昇時間(ms)]
        ''' **Open Time(T0~T1) in ms**</summary>
        ''' <remarks></remarks>
        Public OpenTime As Decimal
        ''' <summary>[打開時間(ms)]
        ''' **Valve On Time(T0~T2) in ms**</summary>
        ''' <remarks></remarks>
        Public ValveOnTime As Decimal
        ''' <summary>[下降時間(ms)]
        ''' **Close Time(T2~T3) in ms**</summary>
        ''' <remarks></remarks>
        Public CloseTime As Decimal
        ''' <summary>[ValveOffTime(ms)]
        ''' **Valve Off Time(T2~T4) in ms**</summary>
        ''' <remarks></remarks>
        Public ValveOffTime As Decimal
        ''' <summary>閥頭衝程(%)
        ''' **Stroke = --> 0~100%**</summary>
        ''' <remarks></remarks>
        Public Stroke As Integer
        ''' <summary>清膠型式</summary>
        ''' <remarks></remarks>
        Public CleanType As eCleanType
        ''' <summary>[閥體溫度]</summary>
        ''' <remarks></remarks>
        Public MaxTemp As Decimal
        ''' <summary>[Jet Time(ms)，給投彈用(投彈提前量)]</summary>
        ''' <remarks></remarks>
        Public JetTime As Decimal
        ''' <summary>[Close Voltage(V)，關閥電壓]</summary>
        ''' <remarks></remarks>
        Public CloseVoltage As Decimal

        ''' <summary>[打點Frequence(Hz)]</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Frequence As Decimal
            Get
                Dim mCycleTime As Decimal
                Dim mFrequence As Decimal

                mCycleTime = ValveOnTime + ValveOffTime

                If mCycleTime > 0 Then
                    mFrequence = 1000 * 1 / mCycleTime
                Else
                    mFrequence = 0
                End If

                Return mFrequence
            End Get
        End Property

        ''' <summary>[打點Cycle(ms)]
        ''' **Cycle Time(T0~T4) in ms**</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CycleTime As Decimal
            Get
                Dim mCycleTime As Decimal
                mCycleTime = ValveOnTime + ValveOffTime
                Return mCycleTime
            End Get
        End Property

        ''' <summary>[Pulse Time]</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PulseTime As Decimal
            Get
                Dim mPulseTime As Decimal
                mPulseTime = ValveOnTime
                Return mPulseTime
            End Get
        End Property
    End Structure

    ''' <summary>[Advanjet]</summary>
    ''' <remarks></remarks>
    Public Structure sAdvanjet
        ''' <summary>[開閥時間(ms)]</summary>
        ''' <remarks></remarks>
        Public RefillTime As Decimal
        ''' <summary>[等待時間(包含訊號延遲時間(0.6ms)+關閥時間+靜置時間)(ms)]</summary>
        ''' <remarks></remarks>
        Public WaitTime As Decimal
        ''' <summary>[閥體氣壓(MPa)(閥體內部壓力非膠管壓力)]</summary>
        ''' <remarks></remarks>
        Public ValveAirPressure As Decimal
        ''' <summary>清膠型式</summary>
        ''' <remarks></remarks>
        Public CleanType As eCleanType
        ''' <summary>[閥體溫度]</summary>
        ''' <remarks></remarks>
        Public MaxTemp As Decimal
        ''' <summary>[Jet Time(ms)，給投彈用(投彈提前量)]</summary>
        ''' <remarks></remarks>
        Public JetTime As Decimal

        ''' <summary>[打點Frequence(Hz)]</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Frequence As Decimal
            Get
                Dim mFrequence As Decimal
                If CycleTime = 0 Then
                    mFrequence = 0
                Else
                    mFrequence = 1000 * 1 / CycleTime
                End If
                Return mFrequence
            End Get
        End Property

        ''' <summary>[打點Cycle(ms)]
        ''' **Cycle Time(T0~T4) in ms**</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CycleTime As Decimal
            Get
                Dim mCycleTime As Decimal
                mCycleTime = RefillTime + WaitTime
                Return mCycleTime
            End Get
            Set(value As Decimal)
                Dim mCycleTime As Decimal
                mCycleTime = value
                WaitTime = mCycleTime - RefillTime
            End Set
        End Property

        ''' <summary>[Pulse Time]</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PulseTime As Decimal
            Get
                Dim mPulseTime As Decimal
                mPulseTime = RefillTime
                Return mPulseTime
            End Get
        End Property
    End Structure

    ''' <summary>[New]</summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Sub New(ByVal name As String)
        Me.Name = name
        Me.ValveModel = eValveModel.PicoPulse
        Me.Comment = ""

        Me.PicoTouch.MaxTemp = 20
        Me.PicoTouch.CleanType = eCleanType.VacuumClean
        Me.PicoTouch.CloseVoltage = 100
        Me.PicoTouch.Stroke = 70
        Me.PicoTouch.OpenTime = 0.3
        Me.PicoTouch.ValveOnTime = 1
        Me.PicoTouch.CloseTime = 0.3
        Me.PicoTouch.ValveOffTime = 9
        Me.PicoTouch.JetTime = 0.45

        Me.Advanjet.MaxTemp = 20
        Me.Advanjet.RefillTime = 3
        Me.Advanjet.WaitTime = 7
        Me.Advanjet.CycleTime = 10
        Me.Advanjet.JetTime = 1
        Me.Advanjet.ValveAirPressure = 0.1
    End Sub

    ''' <summary>[Save File]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "JetValve"
            SaveIniString(strSection, "Name", Name, fileName)
            SaveIniString(strSection, "ValveModel", ValveModel, fileName)
            SaveIniString(strSection, "Comment", Comment, fileName)

            Select Case ValveModel
                Case eValveModel.PicoPulse
                    SaveIniString(strSection, "MaxTemp", PicoTouch.MaxTemp, fileName)
                    SaveIniString(strSection, "OpenTime", PicoTouch.OpenTime, fileName)
                    SaveIniString(strSection, "ValveOnTime", PicoTouch.ValveOnTime, fileName)
                    SaveIniString(strSection, "CloseTime", PicoTouch.CloseTime, fileName)
                    SaveIniString(strSection, "ValveOffTime", PicoTouch.ValveOffTime, fileName)
                    SaveIniString(strSection, "Stroke", PicoTouch.Stroke, fileName)
                    SaveIniString(strSection, "JetTime", PicoTouch.JetTime, fileName)
                    SaveIniString(strSection, "CloseVoltage", PicoTouch.CloseVoltage, fileName)

                Case eValveModel.Advanjet
                    SaveIniString(strSection, "MaxTemp", Advanjet.MaxTemp, fileName)
                    SaveIniString(strSection, "WaitTime", Advanjet.WaitTime, fileName)
                    SaveIniString(strSection, "CycleTime", Advanjet.CycleTime, fileName)
                    SaveIniString(strSection, "RefillTime", Advanjet.RefillTime, fileName)
                    SaveIniString(strSection, "ValveAirPressure", Advanjet.ValveAirPressure, fileName)
                    SaveIniString(strSection, "JetTime", Advanjet.JetTime, fileName)

            End Select
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002007), "Error_1002007", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002007) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

    ''' <summary>[Load File]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "JetValve"
            Name = ReadIniString(strSection, "Name", fileName)
            ValveModel = Val(ReadIniString(strSection, "ValveModel", fileName, eValveModel.PicoPulse))
            Comment = Val(ReadIniString(strSection, "Comment", fileName))

            Select Case ValveModel
                Case eValveModel.PicoPulse
                    PicoTouch.MaxTemp = Val(ReadIniString(strSection, "MaxTemp", fileName, 20))
                    PicoTouch.OpenTime = Val(ReadIniString(strSection, "OpenTime", fileName, 0.3))
                    PicoTouch.ValveOnTime = Val(ReadIniString(strSection, "ValveOnTime", fileName, 1))
                    PicoTouch.CloseTime = Val(ReadIniString(strSection, "CloseTime", fileName, 0.3))
                    PicoTouch.ValveOffTime = Val(ReadIniString(strSection, "ValveOffTime", fileName, 9))
                    PicoTouch.Stroke = Val(ReadIniString(strSection, "Stroke", fileName, 70))
                    PicoTouch.JetTime = Val(ReadIniString(strSection, "JetTime", fileName, 0.45))
                    PicoTouch.CloseVoltage = Val(ReadIniString(strSection, "CloseVoltage", fileName, 100))

                Case eValveModel.Advanjet
                    Advanjet.MaxTemp = Val(ReadIniString(strSection, "MaxTemp", fileName, 20))
                    Advanjet.RefillTime = Val(ReadIniString(strSection, "RefillTime", fileName, 3))
                    Advanjet.WaitTime = Val(ReadIniString(strSection, "WaitTime", fileName, 7))
                    Advanjet.CycleTime = Val(ReadIniString(strSection, "CycleTime", fileName, 10))
                    Advanjet.ValveAirPressure = Val(ReadIniString(strSection, "ValveAirPressure", fileName, 0))
                    Advanjet.JetTime = Val(ReadIniString(strSection, "JetTime", fileName, 1))

            End Select
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002006), "Error_1002006", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002006) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

End Class
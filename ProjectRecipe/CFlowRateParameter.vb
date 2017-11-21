Imports ProjectCore

''' <summary>[秤重資料庫]</summary>
''' <remarks></remarks>
Public Class CFlowRateParameter

    ''' <summary>[名稱]</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>[秤重觸發的條件是參考哪一項條件]</summary>
    ''' <remarks></remarks>
    Public BaseOn As eInspectionType
    ''' <summary>[時間(幾分鐘做一次)]</summary>
    ''' <remarks></remarks>
    Public OnTimer As Long
    ''' <summary>[盤數(幾盤做一次)]</summary>
    ''' <remarks></remarks>
    Public OnRuns As Integer
    ''' <summary>[微量天平秤重EnableWeightFunc(是否要啟用秤重功能)]</summary>               
    ''' <remarks></remarks>
    Public WeighingEnableWeightFunc As Boolean 'Soni / 2016.08.29 修正宣告型別錯誤
    ''' <summary>[微量天平秤重次數]</summary>               
    ''' <remarks></remarks>
    Public WeighingTimes As Integer 'Soni / 2016.08.29 修正宣告型別錯誤
    ''' <summary>[微量天平秤重打點數]</summary>
    ''' <remarks></remarks>
    Public WeighingPointNumber As Integer
    ''' <summary>[微量天平秤重EnableAverageDot(是否開啟重量驗證功能)]</summary>               
    ''' <remarks></remarks>
    Public WeighingEnableDoAverageWeight As Boolean 'Soni / 2016.08.29 修正宣告型別錯誤
    ''' <summary>[微量天平秤重EnableAverageDot(是否開啟單點重量驗證功能)]</summary>               
    ''' <remarks></remarks>
    Public WeighingEnableDoAverageDot As Boolean 'Soni / 2016.08.29 修正宣告型別錯誤
    ''' <summary>[微量天平膠段期望重量]</summary>               
    ''' <remarks></remarks>
    Public WeighingWeight As Double
    ''' <summary>[微量天平秤重WeighingWaitCalibrationTime(驗證校準前等待時間)]</summary>               
    ''' <remarks></remarks>
    Public WeighingWaitCalibrationTime As Double
    ''' <summary>[微量天平秤重Ratio]</summary>               
    ''' <remarks></remarks>
    Public WeighingTolerance As Double
    ''' <summary>[微量天平秤重WeightDotMin(單點重量驗證Min)]</summary>               
    ''' <remarks></remarks>
    Public WeighingWeightDotMin As Double
    ''' <summary>[微量天平秤重WeightDotMax(單點重量驗證Max)]</summary>               
    ''' <remarks></remarks>
    Public WeighingWeightDotMax As Double
    ''' <summary>[微量天平秤重EnableProductionRunFail(是否開啟Fail警報功能)]</summary>               
    ''' <remarks></remarks>
    Public WeighingEnableProductionRunFail As Boolean 'Soni / 2016.08.29 修正宣告型別錯誤
    ''' <summary>[微量天平秤PulseTime]</summary>               
    ''' <remarks></remarks>
    Public WeighingPulseTimes As Double
    ''' <summary>[微量天平秤重CycleTime]</summary>               
    ''' <remarks></remarks>
    Public WeighingCycleTime As Double
    ''' <summary>[微量天平 SteadyTime等待多久再讀值]</summary>               
    ''' <remarks></remarks>
    Public WeighingBlanceSteadyTimes As Double
    ''' <summary>[微量天平秤重WeighingTableTimes(移至天平上後等待多久才開始出膠)]</summary>               
    ''' <remarks></remarks>
    Public WeighingTableTimes As Double
    ''' <summary>[微量天平秤重counter計數器微量天平資料]</summary>               
    ''' <remarks></remarks>
    Public WeighingDataCounter As Integer
    ''' <summary>[微量天平秤重WeighingAverageWeightDot(微量天平資料平均單點重量)]</summary>                        
    ''' <remarks></remarks>
    Public WeighingAverageWeightDot As Double
    ''' <summary>[膠管壓力]</summary>
    ''' <remarks></remarks>
    Public WeighingPressure As Double
    ''' <summary>[天平取值次數]</summary>
    ''' <remarks></remarks>
    Public WeighingGetBalance As Double

    'TODO:FlowRate的方式後續要再補

    ''' <summary>[備註]</summary>
    ''' <remarks></remarks>
    Public Comment As String

    Sub New(ByVal name As String)
        Me.Name = name
        Me.BaseOn = eInspectionType.Noen
        Me.Comment = ""
        Me.OnRuns = 1
        Me.OnTimer = 60
        Me.WeighingAverageWeightDot = 0.02 'mg
        Me.WeighingBlanceSteadyTimes = 2 'sec
        Me.WeighingCycleTime = 2 'ms
        Me.WeighingDataCounter = 0
        Me.WeighingEnableDoAverageDot = False
        Me.WeighingEnableDoAverageWeight = False
        Me.WeighingEnableProductionRunFail = False
        Me.WeighingEnableWeightFunc = False
        Me.WeighingGetBalance = 1
        Me.WeighingPointNumber = 100
        Me.WeighingPressure = 0.1
        Me.WeighingPulseTimes = 0.2
        Me.WeighingTableTimes = 1 'sec
        Me.WeighingTimes = 0
        Me.WeighingTolerance = 20 '%
        Me.WeighingWaitCalibrationTime = 1 'Sec
        Me.WeighingWeight = 10 'mg
        Me.WeighingWeightDotMax = 0.1 'mg
        Me.WeighingWeightDotMin = 0.01 'mg
    End Sub

    Public Function Save(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "FlowRate"
            SaveIniString(strSection, "Name", Name, fileName)
            SaveIniString(strSection, "BaseOn", BaseOn, fileName)
            SaveIniString(strSection, "OnTimerSetting", OnTimer, fileName)
            SaveIniString(strSection, "OnRunsSetting", OnRuns, fileName)
            SaveIniString(strSection, "Comment", Comment, fileName)

            'Jeffadd 20160726
            SaveIniString(strSection, "WeightEnableWeightFunc", CInt(WeighingEnableWeightFunc), fileName)
            SaveIniString(strSection, "WeightTimes", WeighingTimes, fileName)
            SaveIniString(strSection, "WeightPointNumber", WeighingPointNumber, fileName)
            SaveIniString(strSection, "WeightEnableDoAverageDot", CInt(WeighingEnableDoAverageDot), fileName)
            SaveIniString(strSection, "WeightWeight", WeighingWeight, fileName)
            SaveIniString(strSection, "WeightWaitCalibrationTime", WeighingWaitCalibrationTime, fileName)
            SaveIniString(strSection, "WeightTolerance", WeighingTolerance, fileName)
            SaveIniString(strSection, "WeightWeightDotMin", WeighingWeightDotMin, fileName)
            SaveIniString(strSection, "WeightWeightDotMax", WeighingWeightDotMax, fileName)
            SaveIniString(strSection, "WeightEnableProductionRunFail", CInt(WeighingEnableProductionRunFail), fileName)

            SaveIniString(strSection, "WeightPulseTimes", WeighingPulseTimes, fileName)
            SaveIniString(strSection, "WeightCycleTime", WeighingCycleTime, fileName)


            SaveIniString(strSection, "WeightBlanceSteadyTimes", WeighingBlanceSteadyTimes, fileName)
            SaveIniString(strSection, "WeightTableTimes", WeighingTableTimes, fileName)

            SaveIniString(strSection, "WeightPressure", WeighingPressure, fileName)

            'SaveIniString(strSection, "WeightDataCounter", WeighingDataCounter, fileName)   20161122
            SaveIniString(strSection, "WeighingEnableDoAverageWeight", CInt(WeighingEnableDoAverageWeight), fileName) 'Soni + 2016.08.29 存檔缺漏

            '20161114
            SaveIniString(strSection, "WeighingAverageWeightDot", WeighingAverageWeightDot, fileName)

            '20161213
            SaveIniString(strSection, "WeighingGetBalance", WeighingGetBalance, fileName)
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002007), "Error_1002007", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002007) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

    Public Function Load(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "FlowRate"
            Name = ReadIniString(strSection, "Name", fileName)
            BaseOn = CInt(ReadIniString(strSection, "BaseOn", fileName, "0"))
            OnTimer = CDec(ReadIniString(strSection, "OnTimerSetting", fileName, "0"))
            OnRuns = CDec(ReadIniString(strSection, "OnRunsSetting", fileName, "0"))
            Comment = ReadIniString(strSection, "Comment", fileName, "")

            'Jeffadd 20160726
            WeighingEnableWeightFunc = Val(ReadIniString(strSection, "WeightEnableWeightFunc", fileName, "0"))
            WeighingTimes = Val(ReadIniString(strSection, "WeightTimes", fileName, "0"))
            WeighingPointNumber = CInt(ReadIniString(strSection, "WeightPointNumber", fileName, "0"))
            WeighingEnableDoAverageDot = Val(ReadIniString(strSection, "WeightEnableDoAverageDot", fileName, "0"))
            WeighingWeight = CDbl(ReadIniString(strSection, "WeightWeight", fileName, "0"))
            WeighingWaitCalibrationTime = CDbl(ReadIniString(strSection, "WeightWaitCalibrationTime", fileName, "0"))
            WeighingTolerance = CDbl(ReadIniString(strSection, "WeightTolerance", fileName, "0"))

            '20170505
            WeighingWeightDotMin = CDbl(ReadIniString(strSection, "WeightWeightDotMin", fileName, "0.0002"))
            WeighingWeightDotMax = CDbl(ReadIniString(strSection, "WeightWeightDotMax", fileName, "0.9"))

            WeighingEnableProductionRunFail = CInt(ReadIniString(strSection, "WeightEnableProductionRunFail", fileName, "0"))
            WeighingPulseTimes = CDbl(ReadIniString(strSection, "WeightPulseTimes", fileName, "0"))
            WeighingCycleTime = CDbl(ReadIniString(strSection, "WeightCycleTime", fileName, "0"))
            WeighingBlanceSteadyTimes = CDbl(ReadIniString(strSection, "WeightBlanceSteadyTimes", fileName, "0"))
            WeighingTableTimes = CDbl(ReadIniString(strSection, "WeightTableTimes", fileName, "0"))

            WeighingPressure = CDbl(ReadIniString(strSection, "WeightPressure", fileName, "0"))
            'WeighingDataCounter = CDbl(ReadIniString(strSection, "WeightDataCounter", fileName, "0"))  ' 20161122
            WeighingEnableDoAverageWeight = Val(ReadIniString(strSection, "WeighingEnableDoAverageWeight", fileName, "0")) 'Soni + 2016.08.29 功能開關需存檔

            '20161114
            WeighingAverageWeightDot = CDbl(ReadIniString(strSection, "WeighingAverageWeightDot", fileName, "0.01"))

            '微量天平資料平均單點重量 WeighingAverageWeightDot 此部分需不需歸零

            '20161213
            WeighingGetBalance = CDbl(ReadIniString(strSection, "WeighingGetBalance", fileName, "10"))

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002006), "Error_1002006", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002006) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function
End Class

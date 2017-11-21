Imports ProjectCore
Imports ProjectTriggerBoard
Imports ProjectRecipe
Imports ProjectRecipe.CJetValveParameter
''' <summary>
''' 校正閥體參數
''' </summary>
''' <remarks></remarks>
Public Class CCalibrationValveParameter
    Public CyleParam As sTriggerTPCmdParam

    Public PicoTouch As sPicoTouch
    Public Advanjet As sAdvanjet

    ''' <summary>儲存</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePicoTouch(ByVal fileName As String) As Boolean
        Dim strSection As String = "CalibrationValveParameter"


        Call SaveIniString(strSection, "CycleTime", CyleParam.CycleTime, fileName)
        Call SaveIniString(strSection, "PulseTime", CyleParam.PulseTime, fileName)
        Call SaveIniString(strSection, "OpenTime", CyleParam.OpenTime, fileName)
        Call SaveIniString(strSection, "CloseTime", CyleParam.CloseTime, fileName)
        Call SaveIniString(strSection, "Stroke", CyleParam.Stroke, fileName)
        Call SaveIniString(strSection, "CloseVoltage", CyleParam.CloseVoltage, fileName)

        Return True
    End Function

    ''' <summary>讀檔</summary>
    ''' <param name="fileName">設定檔名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadPicoTouch(ByVal fileName As String) As Boolean
        Dim strSection As String = "CalibrationValveParameter"

        CyleParam.CycleTime = Val(ReadIniString(strSection, "CycleTime", fileName, 6000))
        CyleParam.PulseTime = Val(ReadIniString(strSection, "PulseTime", fileName, 1000))
        CyleParam.OpenTime = Val(ReadIniString(strSection, "OpenTime", fileName, 300))
        CyleParam.CloseTime = Val(ReadIniString(strSection, "CloseTime", fileName, 300))
        CyleParam.Stroke = Val(ReadIniString(strSection, "Stroke", fileName, 70))
        CyleParam.CloseVoltage = Val(ReadIniString(strSection, "CloseVoltage", fileName, 100))


        Return True
    End Function

    Public Function SaveAdvanjet(ByVal fileName As String) As Boolean
        Dim strSection As String = "CalibrationValveParameter"


        Call SaveIniString(strSection, "RefillTime", Advanjet.RefillTime, fileName)
        Call SaveIniString(strSection, "CycleTime", Advanjet.CycleTime, fileName)
        Call SaveIniString(strSection, "JetTime", Advanjet.JetTime, fileName)
        Return True
    End Function

    Public Function LoadAdvanjet(ByVal fileName As String) As Boolean
        Dim strSection As String = "CalibrationValveParameter"

        Advanjet.RefillTime = Val(ReadIniString(strSection, "RefillTime", fileName, 1000))
        Advanjet.CycleTime = Val(ReadIniString(strSection, "CycleTime", fileName, 10000))
        Advanjet.JetTime = Val(ReadIniString(strSection, "JetTime", fileName, 300))
        Return True
    End Function

End Class

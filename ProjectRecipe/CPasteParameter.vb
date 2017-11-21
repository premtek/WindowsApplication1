Imports ProjectCore

''' <summary>膠材參數</summary>
''' <remarks></remarks>
Public Class CPasteParameter
    ''' <summary>名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>壽命計時(Hr)</summary>
    ''' <remarks></remarks>
    Public PotLife As Double
    ''' <summary>壽命計時致能</summary>
    ''' <remarks></remarks>
    Public PotLifeEnable As Boolean
    ''' <summary>壽命計數</summary>
    ''' <remarks></remarks>
    Public PotLifeCount As Integer
    ''' <summary>壽命計數致能</summary>
    ''' <remarks></remarks>
    Public PotLifeCountEnable As Boolean
    ''' <summary>黏度</summary>
    ''' <remarks></remarks>
    Public Viscocity As Double
    ''' <summary>最大適用溫度</summary>
    ''' <remarks></remarks>
    Public TempMax As Double
    ''' <summary>最大適用溫度致能</summary>
    ''' <remarks></remarks>
    Public TempMaxEnable As Boolean
    ''' <summary>最小適用溫度</summary>
    ''' <remarks></remarks>
    Public TempMin As Double
    ''' <summary>最小適用溫度致能</summary>
    ''' <remarks></remarks>
    Public TempMinEnable As Boolean
    ''' <summary>搖溶比</summary>
    ''' <remarks></remarks>
    Public Thixotropy As Double
    ''' <summary>比重</summary>
    ''' <remarks></remarks>
    Public SpecificGravity As Double
    ''' <summary>燃點</summary>
    ''' <remarks></remarks>
    Public FlashPoint As Double

    Sub New(ByVal name As String)
        Me.Name = name
        PotLife = 8
        PotLifeEnable = False
        PotLifeCount = 100000
        PotLifeCountEnable = False
        Viscocity = 1000
        TempMax = 120
        TempMin = 20
        TempMaxEnable = False
        TempMinEnable = False
        Thixotropy = 1
        SpecificGravity = 1
        FlashPoint = 150
    End Sub
    ''' <summary>存檔</summary>
    ''' <param name="fileName">完整路徑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean

        Dim strSection As String
        strSection = "PasteParameter"
        SaveIniString(strSection, "Name", Name, fileName)
        SaveIniString(strSection, "PotLife", PotLife, fileName)
        SaveIniString(strSection, "PotLifeEnable", CInt(PotLifeEnable), fileName)
        SaveIniString(strSection, "PotLifeCount", PotLifeCount, fileName)
        SaveIniString(strSection, "PotLifeCountEnable", PotLifeCountEnable, fileName)
        SaveIniString(strSection, "Viscocity", Viscocity, fileName)
        SaveIniString(strSection, "TempMax", TempMax, fileName)
        SaveIniString(strSection, "TempMaxEnable", CInt(TempMaxEnable), fileName)
        SaveIniString(strSection, "TempMin", TempMin, fileName)
        SaveIniString(strSection, "TempMinEnable", CInt(TempMinEnable), fileName)
        SaveIniString(strSection, "Thixotropy", Thixotropy, fileName)
        SaveIniString(strSection, "SpecificGravity", SpecificGravity, fileName)
        SaveIniString(strSection, "FlashPoint", FlashPoint, fileName)

        Return True
    End Function

    ''' <summary>讀檔</summary>
    ''' <param name="fileName">完整路徑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean

        Dim strSection As String
        strSection = "PasteParameter"
        Name = ReadIniString(strSection, "Name", fileName)
        PotLife = Val(ReadIniString(strSection, "PotLife", fileName, Double.NaN))
        PotLifeEnable = Val(ReadIniString(strSection, "PotLifeEnable", fileName, 0))
        PotLifeCount = Val(ReadIniString(strSection, "PotLifeCount", fileName, 10000))
        PotLifeCountEnable = Val(ReadIniString(strSection, "PotLifeCountEnable", fileName, 0))
        Viscocity = Val(ReadIniString(strSection, "Viscocity", fileName, Double.NaN))
        TempMax = Val(ReadIniString(strSection, "TempMax", fileName, Double.NaN))
        TempMaxEnable = Val(ReadIniString(strSection, "TempMaxEnable", fileName, 0))
        TempMin = Val(ReadIniString(strSection, "TempMin", fileName, Double.NaN))
        TempMinEnable = Val(ReadIniString(strSection, "TempMinEnable", fileName, 0))
        Thixotropy = Val(ReadIniString(strSection, "Thixotropy", fileName, Double.NaN))
        SpecificGravity = Val(ReadIniString(strSection, "SpecificGravity", fileName, Double.NaN))
        FlashPoint = Val(ReadIniString(strSection, "FlashPoint", fileName, Double.NaN))

        Return True
    End Function
End Class


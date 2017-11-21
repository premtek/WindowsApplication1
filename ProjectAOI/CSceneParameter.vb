Imports ProjectCore

Public Enum eAlignType
    ''' <summary>[特徵定位]</summary>
    ''' <remarks></remarks>
    PMAlign = 0
    ''' <summary>[找角]</summary>
    ''' <remarks></remarks>
    Corner = 1
    ''' <summary>[找圓]</summary>
    ''' <remarks></remarks>
    Circle = 2
    ''' <summary>[手機膠道]</summary>
    ''' <remarks></remarks>
    Lane = 3
    ''' <summary>[載入預設]</summary>
    ''' <remarks></remarks>
    LoadFile = 4
    ''' <summary>[面積找圓]</summary>
    ''' <remarks></remarks>
    Blob = 5
End Enum


Public Enum eROIType
    ''' <summary>[特徵定位]</summary>
    ''' <remarks></remarks>
    PMAlign = 0
    ''' <summary>[面積]</summary>
    ''' <remarks></remarks>
    Blob = 1

End Enum

''' <summary>
''' 場景參數
''' </summary>
''' <remarks></remarks>
Public Class CSceneParameter
    'Public CCDExposureTime As Double

    ''' <summary>
    ''' 定位場景類別
    ''' </summary>
    ''' <remarks></remarks>
    Public AlignType As eAlignType

    ''' <summary>程控光源亮度值</summary>
    ''' <remarks></remarks>
    Public LightValue(enmValveLight.Max) As Integer

    ''' <summary>程控光源功能啟用</summary>
    ''' <remarks></remarks>
    Public LightEnable(enmValveLight.Max) As Boolean

    ' ''' <summary>
    ' ''' 定位場景的參考點
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public AlignBasicX As Double

    'Public AlignBasicY As Double

    'Public AlignBasicAngle As Double

    ''' <summary>
    ''' 讀取場景參數
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="section"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String, ByVal section As String) As Boolean
        With Me
            'Dim Exp As Decimal
            'Select Case gSSystemParameter.MachineType
            '    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V
            '        Exp = 10
            '    Case enmMachineType.DCS_F230A
            '        Exp = 15
            '    Case enmMachineType.DCS_350A
            '        Exp = 0.3
            '    Case Else

            'End Select

            '.CCDExposureTime = Val(ReadIniString(section, "CCDExposureTime", fileName, Exp)) '避免沒有設定值使曝光為0
            .AlignType = Val(ReadIniString(section, "AlignType", fileName, eAlignType.PMAlign)) '預設值為特徵定位


            For mLightNo As Integer = 0 To enmValveLight.Max
                .LightValue(mLightNo) = Val(ReadIniString(section, "Light" & (mLightNo + 1).ToString & "Value", fileName, 0))
                If .LightValue(mLightNo) > 255 Then '[Note]電流正規化 0-255
                    .LightValue(mLightNo) = 255
                End If
                .LightEnable(mLightNo) = CBool(ReadIniString(section, "Light" & (mLightNo + 1).ToString & "Enable", fileName, 0))
                'Debug.WriteLine("Load   LightNo: " & mLightNo & " LightValue:" & .LightValue(mLightNo))
            Next
            ' .AlignBasicX = Val(ReadIniString(section, "AlignBasicX", fileName, 0))
            '.AlignBasicY = Val(ReadIniString(section, "AlignBasicY", fileName, 0))
            '.AlignBasicAngle = Val(ReadIniString(section, "AlignBasicAngle", fileName, 0))

        End With
        Return True
    End Function

    ''' <summary>
    ''' 儲存場景參數
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="section"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String, ByVal section As String) As Boolean
        With Me
            'SaveIniString(section, "CCDExposureTime", .CCDExposureTime, fileName)

            SaveIniString(section, "AlignType", .AlignType, fileName)

            For mLightNo As Integer = 0 To enmValveLight.Max
                SaveIniString(section, "Light" & (mLightNo + 1).ToString & "Value", .LightValue(mLightNo), fileName)
                SaveIniString(section, "Light" & (mLightNo + 1).ToString & "Enable", .LightEnable(mLightNo), fileName)
            Next
            'SaveIniString(section, "AlignBasicX", .AlignBasicX, fileName)
            'SaveIniString(section, "AlignBasicY", .AlignBasicY, fileName)
            'SaveIniString(section, "AlignBasicAngle", .AlignBasicAngle, fileName)
        End With
        Return True
    End Function
End Class
Public Module MCommonDefineAI


    ''' <summary>
    ''' AI列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure enmAI
        ''' <summary>通道索引最大值</summary>
        ''' <remarks></remarks>
        Public Shared Max As Integer = -1
        ''' <summary>閥1膠管壓力值</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo1EPRegulator As Integer = -1             'AI_人機顯示值-1號膠槍電空閥(ElectricityPressureRegulator)壓力顯示
        ''' <summary>閥2膠管壓力值</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo2EPRegulator As Integer = -1             'AI_人機顯示值-2號膠槍電空閥(ElectricityPressureRegulator)壓力顯示
        ''' <summary>閥3膠管壓力值</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo3EPRegulator As Integer = -1             'AI_人機顯示值-3號膠槍電空閥(ElectricityPressureRegulator)壓力顯示
        ''' <summary>閥4膠管壓力值</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo4EPRegulator As Integer = -1             'AI_人機顯示值-4號膠槍電空閥(ElectricityPressureRegulator)壓力顯示

        ''' <summary>閥1過載電流值</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo1OverloadCT As Integer = -1              'AI_人機顯示值-1號膠槍過載偵測(CT)
        ''' <summary>閥2過載電流值</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo2OverloadCT As Integer = -1              'AI_人機顯示值-2號膠槍過載偵測(CT)
        ''' <summary>閥1雷射測高值</summary>
        ''' <remarks></remarks>
        Public Shared LaserReader As Integer = -1                         'AI_人機顯示值-測高Sensor顯示值
        ''' <summary>閥2雷射測高值</summary>
        ''' <remarks></remarks>
        Public Shared LaserReader2 As Integer = -1
        ''' <summary>閥3雷射測高值</summary>
        ''' <remarks></remarks>
        Public Shared LaserReader3 As Integer = -1
        ''' <summary>閥4雷射測高值</summary>
        ''' <remarks></remarks>
        Public Shared LaserReader4 As Integer = -1

#Region "enmAI存取"

        ''' <summary>讀取AI索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub LoadAIIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AIIndex"
            enmAI.DispenserNo1EPRegulator = CInt(ReadIniString(strSection, "DispenserNo1EPRegulator", strFileName, -1))
            enmAI.DispenserNo1OverloadCT = CInt(ReadIniString(strSection, "DispenserNo1OverloadCT", strFileName, -1))
            enmAI.DispenserNo2EPRegulator = CInt(ReadIniString(strSection, "DispenserNo2EPRegulator", strFileName, -1))
            enmAI.DispenserNo2OverloadCT = CInt(ReadIniString(strSection, "DispenserNo2OverloadCT", strFileName, -1))
            enmAI.LaserReader = CInt(ReadIniString(strSection, "LaserReader", strFileName, -1))
            enmAI.Max = CInt(ReadIniString(strSection, "Max", strFileName, -1))
        End Sub
        ''' <summary>儲存AI索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub SaveAIIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AIIndex"
            Call SaveIniString(strSection, "DispenserNo1EPRegulator", CInt(enmAI.DispenserNo1EPRegulator), strFileName)
            Call SaveIniString(strSection, "DispenserNo1OverloadCT", CInt(enmAI.DispenserNo1OverloadCT), strFileName)
            Call SaveIniString(strSection, "DispenserNo2EPRegulator", CInt(enmAI.DispenserNo2EPRegulator), strFileName)
            Call SaveIniString(strSection, "DispenserNo2OverloadCT", CInt(enmAI.DispenserNo2OverloadCT), strFileName)
            Call SaveIniString(strSection, "LaserReader", CInt(enmAI.LaserReader), strFileName)
            Call SaveIniString(strSection, "Max", CInt(enmAI.Max), strFileName)
        End Sub

#End Region
    End Structure

End Module

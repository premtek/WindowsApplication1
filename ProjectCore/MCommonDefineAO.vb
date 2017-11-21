Public Module MCommonDefineAO

    ''' <summary>
    ''' AO列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure enmAO
        ''' <summary>通道索引最大值</summary>
        ''' <remarks></remarks>
        Public Shared Max As Integer = -1
        ''' <summary>[1號膠槍塗膠轉速控制] </summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo1ScrewValveSpeed As Integer = -1
        ''' <summary>[2號膠槍塗膠轉速控制] </summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo2ScrewValveSpeed As Integer = -1
        ''' <summary>[1號膠槍電空閥(ElectricityPressureRegulator)壓力設定]</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo1EPRegulator As Integer = -1
        ''' <summary>[2號膠槍電空閥(ElectricityPressureRegulator)壓力設定]</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo2EPRegulator As Integer = -1
        ''' <summary>膠管氣壓3</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo3EPRegulator As Integer = -1
        ''' <summary>膠管氣壓4</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo4EPRegulator As Integer = -1

#Region "enmAO存取"

        ''' <summary>讀取AO索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub LoadAOIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AOIndex"
            enmAO.DispenserNo1EPRegulator = CInt(ReadIniString(strSection, "DispenserNo1EPRegulator", strFileName, -1))
            enmAO.DispenserNo1ScrewValveSpeed = CInt(ReadIniString(strSection, "DispenserNo1ScrewValveSpeed", strFileName, -1))
            enmAO.DispenserNo2EPRegulator = CInt(ReadIniString(strSection, "DispenserNo2EPRegulator", strFileName, -1))
            enmAO.DispenserNo2ScrewValveSpeed = CInt(ReadIniString(strSection, "DispenserNo2ScrewValveSpeed", strFileName, -1))
            enmAO.Max = CInt(ReadIniString(strSection, "Max", strFileName, -1))
        End Sub
        ''' <summary>儲存AO索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub SaveAOIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AOIndex"
            Call SaveIniString(strSection, "DispenserNo1EPRegulator", CInt(enmAO.DispenserNo1EPRegulator), strFileName)
            Call SaveIniString(strSection, "DispenserNo1ScrewValveSpeed", CInt(enmAO.DispenserNo1ScrewValveSpeed), strFileName)
            Call SaveIniString(strSection, "DispenserNo2EPRegulator", CInt(enmAO.DispenserNo2EPRegulator), strFileName)
            Call SaveIniString(strSection, "DispenserNo2ScrewValveSpeed", CInt(enmAO.DispenserNo2ScrewValveSpeed), strFileName)
            Call SaveIniString(strSection, "Max", CInt(enmAO.Max), strFileName)
        End Sub

#End Region
    End Structure

End Module

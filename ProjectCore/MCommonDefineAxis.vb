Public Module MCommonDefineAxis

    ''' <summary>
    ''' 軸號 XYZABCUVW等皆參考工具機座標系
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure enmAxis
        '命名定義: 直角座標 XYZ UVW RST OPQ
        '          旋轉座標 ABC DEF GHI JKL
        ''' <summary>點膠平台第一組X軸</summary>
        ''' <remarks></remarks>
        Public Shared XAxis As Integer = -1
        ''' <summary>點膠平台第一組同動X軸</summary>
        ''' <remarks></remarks>
        Public Shared X2Axis As Integer = -1
        ''' <summary>點膠平台第一組Y軸</summary>
        ''' <remarks></remarks>
        Public Shared Y1Axis As Integer = -1
        ''' <summary>點膠平台第一組同動Y軸</summary>
        ''' <remarks></remarks>
        Public Shared Y2Axis As Integer = -1
        ''' <summary>點膠平台第一組Z軸</summary>
        ''' <remarks></remarks>
        Public Shared ZAxis As Integer = -1
        ''' <summary>點膠平台第二組X軸</summary>
        ''' <remarks></remarks>
        Public Shared UAxis As Integer
        ''' <summary>點膠平台第二組同動X軸</summary>
        ''' <remarks></remarks>
        Public Shared U2Axis As Integer = -1
        ''' <summary>點膠平台第二組Y軸</summary>
        ''' <remarks></remarks>
        Public Shared VAxis As Integer
        ''' <summary>點膠平台第二組Z軸</summary>
        ''' <remarks></remarks>
        Public Shared WAxis As Integer

        ''' <summary>點膠平台第三組X軸</summary>
        ''' <remarks></remarks>
        Public Shared RAxis As Integer
        ''' <summary>點膠平台第三組同動X軸</summary>
        ''' <remarks></remarks>
        Public Shared R2Axis As Integer = -1
        ''' <summary>點膠平台第三組Y軸</summary>
        ''' <remarks></remarks>
        Public Shared SAxis As Integer
        ''' <summary>點膠平台第三組Z軸</summary>
        ''' <remarks></remarks>
        Public Shared TAxis As Integer

        ''' <summary>點膠平台第四組X軸</summary>
        ''' <remarks></remarks>
        Public Shared OAxis As Integer
        ''' <summary>點膠平台第四組同動X軸</summary>
        ''' <remarks></remarks>
        Public Shared O2Axis As Integer = -1
        ''' <summary>點膠平台第四組Y軸</summary>
        ''' <remarks></remarks>
        Public Shared PAxis As Integer
        ''' <summary>點膠平台第四組Z軸</summary>
        ''' <remarks></remarks>
        Public Shared QAxis As Integer

        ''' <summary>點膠平台第一組A軸</summary>
        ''' <remarks></remarks>
        Public Shared AAxis As Integer = -1
        ''' <summary>點膠平台第一組B軸</summary>
        ''' <remarks></remarks>
        Public Shared BAxis As Integer = -1
        ''' <summary>點膠平台第一組C軸</summary>
        ''' <remarks></remarks>
        Public Shared CAxis As Integer = -1


        ''' <summary>點膠平台第二組A軸</summary>
        ''' <remarks></remarks>
        Public Shared DAxis As Integer = -1
        ''' <summary>點膠平台第二組B軸</summary>
        ''' <remarks></remarks>
        Public Shared EAxis As Integer = -1
        ''' <summary>點膠平台第二組C軸</summary>
        ''' <remarks></remarks>
        Public Shared FAxis As Integer = -1

        ''' <summary>點膠平台第三組A軸</summary>
        ''' <remarks></remarks>
        Public Shared GAxis As Integer = -1
        ''' <summary>點膠平台第三組B軸</summary>
        ''' <remarks></remarks>
        Public Shared HAxis As Integer = -1
        ''' <summary>點膠平台第三組C軸</summary>
        ''' <remarks></remarks>
        Public Shared IAxis As Integer = -1

        ''' <summary>點膠平台第四組A軸</summary>
        ''' <remarks></remarks>
        Public Shared JAxis As Integer = -1
        ''' <summary>點膠平台第四組B軸</summary>
        ''' <remarks></remarks>
        Public Shared KAxis As Integer = -1
        ''' <summary>點膠平台第四組C軸</summary>
        ''' <remarks></remarks>
        Public Shared LAxis As Integer = -1

        ''' <summary>控制流道</summary>
        ''' <remarks></remarks>
        Public Shared Converter As Integer = -1


        ''' <summary>流道第一組X軸</summary>
        ''' <remarks></remarks>
        Public Shared Conveyor1 As Integer = -1
        ''' <summary>流道第一組反向同動X軸</summary>
        ''' <remarks></remarks>
        Public Shared Conveyor2 As Integer = -1
        ''' <summary>流道第一組頂升平台2 Z軸</summary>
        ''' <remarks></remarks>
        Public Shared Station2 As Integer = -1
        ''' <summary>流道第一組加熱模組1</summary>
        ''' <remarks></remarks>
        Public Shared Heater1 As Integer = -1
        ''' <summary>流道第一組加熱模組2</summary>
        ''' <remarks></remarks>
        Public Shared Heater2 As Integer = -1
        ''' <summary>流道第一組加熱模組3</summary>
        ''' <remarks></remarks>
        Public Shared Heater3 As Integer = -1

        ''' <summary>A機頂昇平台1 (單軌四閥)</summary>
        ''' <remarks></remarks>
        Public Shared MachineAChuck1 As Integer = -1
        ''' <summary>B機頂昇平台1 (單軌四閥)</summary>
        ''' <remarks></remarks>
        Public Shared MachineBChuck1 As Integer = -1

        ''' <summary>最大軸數</summary>
        ''' <remarks></remarks>
        Public Shared Max As Integer = -1

        Public Shared L_DP_X As Integer = -1
        Public Shared L_DP_Y As Integer = -1
        Public Shared L_DP_Z As Integer = -1

#Region "sAxis存取"

        ''' <summary>讀取軸索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub LoadAxisIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AxisIndex"
            enmAxis.XAxis = CInt(ReadIniString(strSection, "XAxis", strFileName, -1))
            enmAxis.X2Axis = CInt(ReadIniString(strSection, "X2Axis", strFileName, -1))
            enmAxis.Y1Axis = CInt(ReadIniString(strSection, "Y1Axis", strFileName, -1))
            enmAxis.Y2Axis = CInt(ReadIniString(strSection, "Y2Axis", strFileName, -1))
            enmAxis.ZAxis = CInt(ReadIniString(strSection, "ZAxis", strFileName, -1))
            enmAxis.Conveyor1 = CInt(ReadIniString(strSection, "Conveyor1", strFileName, -1))
            enmAxis.Conveyor2 = CInt(ReadIniString(strSection, "Conveyor2", strFileName, -1))
            enmAxis.Station2 = CInt(ReadIniString(strSection, "Station2", strFileName, -1))
            enmAxis.Heater1 = CInt(ReadIniString(strSection, "Heater1", strFileName, -1))
            enmAxis.Heater2 = CInt(ReadIniString(strSection, "Heater2", strFileName, -1))
            enmAxis.Heater3 = CInt(ReadIniString(strSection, "Heater3", strFileName, -1))
            enmAxis.AAxis = CInt(ReadIniString(strSection, "AAxis", strFileName, -1))
            enmAxis.BAxis = CInt(ReadIniString(strSection, "BAxis", strFileName, -1))
            enmAxis.CAxis = CInt(ReadIniString(strSection, "CAxis", strFileName, -1))
            enmAxis.DAxis = CInt(ReadIniString(strSection, "DAxis", strFileName, -1))
            enmAxis.EAxis = CInt(ReadIniString(strSection, "EAxis", strFileName, -1))
            enmAxis.FAxis = CInt(ReadIniString(strSection, "FAxis", strFileName, -1))
            enmAxis.GAxis = CInt(ReadIniString(strSection, "GAxis", strFileName, -1))
            enmAxis.HAxis = CInt(ReadIniString(strSection, "HAxis", strFileName, -1))
            enmAxis.IAxis = CInt(ReadIniString(strSection, "IAxis", strFileName, -1))
            enmAxis.JAxis = CInt(ReadIniString(strSection, "JAxis", strFileName, -1))
            enmAxis.KAxis = CInt(ReadIniString(strSection, "KAxis", strFileName, -1))
            enmAxis.LAxis = CInt(ReadIniString(strSection, "LAxis", strFileName, -1))
            enmAxis.OAxis = CInt(ReadIniString(strSection, "OAxis", strFileName, -1))
            enmAxis.O2Axis = CInt(ReadIniString(strSection, "O2Axis", strFileName, -1))
            enmAxis.PAxis = CInt(ReadIniString(strSection, "PAxis", strFileName, -1))
            enmAxis.QAxis = CInt(ReadIniString(strSection, "QAxis", strFileName, -1))
            enmAxis.RAxis = CInt(ReadIniString(strSection, "RAxis", strFileName, -1))
            enmAxis.R2Axis = CInt(ReadIniString(strSection, "R2Axis", strFileName, -1))
            enmAxis.SAxis = CInt(ReadIniString(strSection, "SAxis", strFileName, -1))
            enmAxis.TAxis = CInt(ReadIniString(strSection, "TAxis", strFileName, -1))
            enmAxis.UAxis = CInt(ReadIniString(strSection, "UAxis", strFileName, -1))
            enmAxis.U2Axis = CInt(ReadIniString(strSection, "U2Axis", strFileName, -1))
            enmAxis.VAxis = CInt(ReadIniString(strSection, "VAxis", strFileName, -1))
            enmAxis.WAxis = CInt(ReadIniString(strSection, "WAxis", strFileName, -1))
            enmAxis.MachineAChuck1 = CInt(ReadIniString(strSection, "MachineAChuck1", strFileName, -1))
            enmAxis.MachineBChuck1 = CInt(ReadIniString(strSection, "MachineBChuck1", strFileName, -1))
            enmAxis.Converter = CInt(ReadIniString(strSection, "Converter", strFileName, -1))
            enmAxis.Max = CInt(ReadIniString(strSection, "Max", strFileName, -1))
        End Sub
        ''' <summary>儲存軸索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub SaveAxisIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AxisIndex"
            Call SaveIniString(strSection, "XAxis", CInt(enmAxis.XAxis), strFileName)
            Call SaveIniString(strSection, "X2Axis", CInt(enmAxis.X2Axis), strFileName)
            Call SaveIniString(strSection, "Y1Axis", CInt(enmAxis.Y1Axis), strFileName)
            Call SaveIniString(strSection, "Y2Axis", CInt(enmAxis.Y2Axis), strFileName)
            Call SaveIniString(strSection, "ZAxis", CInt(enmAxis.ZAxis), strFileName)
            Call SaveIniString(strSection, "UAxis", CInt(enmAxis.UAxis), strFileName)
            Call SaveIniString(strSection, "U2Axis", CInt(enmAxis.U2Axis), strFileName)
            Call SaveIniString(strSection, "VAxis", CInt(enmAxis.VAxis), strFileName)
            Call SaveIniString(strSection, "WAxis", CInt(enmAxis.WAxis), strFileName)
            Call SaveIniString(strSection, "RAxis", CInt(enmAxis.RAxis), strFileName)
            Call SaveIniString(strSection, "R2Axis", CInt(enmAxis.R2Axis), strFileName)
            Call SaveIniString(strSection, "SAxis", CInt(enmAxis.SAxis), strFileName)
            Call SaveIniString(strSection, "TAxis", CInt(enmAxis.TAxis), strFileName)
            Call SaveIniString(strSection, "OAxis", CInt(enmAxis.OAxis), strFileName)
            Call SaveIniString(strSection, "O2Axis", CInt(enmAxis.O2Axis), strFileName)
            Call SaveIniString(strSection, "PAxis", CInt(enmAxis.PAxis), strFileName)
            Call SaveIniString(strSection, "QAxis", CInt(enmAxis.QAxis), strFileName)
            Call SaveIniString(strSection, "Conveyor1", CInt(enmAxis.Conveyor1), strFileName)
            Call SaveIniString(strSection, "Conveyor2", CInt(enmAxis.Conveyor2), strFileName)
            Call SaveIniString(strSection, "Station2", CInt(enmAxis.Station2), strFileName)
            Call SaveIniString(strSection, "Heater1", CInt(enmAxis.Heater1), strFileName)
            Call SaveIniString(strSection, "Heater2", CInt(enmAxis.Heater2), strFileName)
            Call SaveIniString(strSection, "Heater3", CInt(enmAxis.Heater3), strFileName)
            Call SaveIniString(strSection, "AAxis", CInt(enmAxis.AAxis), strFileName)
            Call SaveIniString(strSection, "BAxis", CInt(enmAxis.BAxis), strFileName)
            Call SaveIniString(strSection, "CAxis", CInt(enmAxis.CAxis), strFileName)
            Call SaveIniString(strSection, "DAxis", CInt(enmAxis.DAxis), strFileName)
            Call SaveIniString(strSection, "EAxis", CInt(enmAxis.EAxis), strFileName)
            Call SaveIniString(strSection, "FAxis", CInt(enmAxis.FAxis), strFileName)
            Call SaveIniString(strSection, "GAxis", CInt(enmAxis.GAxis), strFileName)
            Call SaveIniString(strSection, "HAxis", CInt(enmAxis.HAxis), strFileName)
            Call SaveIniString(strSection, "IAxis", CInt(enmAxis.IAxis), strFileName)
            Call SaveIniString(strSection, "JAxis", CInt(enmAxis.JAxis), strFileName)
            Call SaveIniString(strSection, "KAxis", CInt(enmAxis.KAxis), strFileName)
            Call SaveIniString(strSection, "LAxis", CInt(enmAxis.LAxis), strFileName)
            Call SaveIniString(strSection, "MachineAChuck1", CInt(enmAxis.MachineAChuck1), strFileName)
            Call SaveIniString(strSection, "MachineBChuck1", CInt(enmAxis.MachineBChuck1), strFileName)
            Call SaveIniString(strSection, "Converter", CInt(enmAxis.Converter), strFileName)
            Call SaveIniString(strSection, "Max", CInt(enmAxis.Max), strFileName)
        End Sub

#End Region
    End Structure



End Module

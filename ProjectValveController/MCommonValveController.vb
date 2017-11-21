Imports ProjectCore
''' <summary>
''' 全域共用操作
''' </summary>
''' <remarks></remarks>
Public Module MCommonValveController
    'Private mMachineName As String
    'Public Property _mMachineName() As String
    '    Get
    '        Return mMachineName
    '    End Get
    '    Set(ByVal value As String)
    '        mMachineName = value
    '    End Set
    'End Property

    ''' <summary>[通訊異常後 允取再從送幾次]</summary>
    ''' <remarks></remarks>
    Public Const gVavleCmdMaxFailCounts As Integer = 3

    Public gValvecontrollerCollection As New CValveControllerCollection
    ''' <summary>
    ''' ValveControler初始化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitialValveController() As Boolean
        gValvecontrollerCollection.LoadValveReaderConnection(Application.StartupPath & "\System\" & MachineName & "\ValvecontrollerParam.ini")
        If gValvecontrollerCollection.Initial(gValvecontrollerCollection.ConnectionParameter) = False Then
            Return False
        End If
        Return True
    End Function


    Public Function Close_Vavlecontroler() As Boolean
        Return gValvecontrollerCollection.Close()
    End Function
End Module

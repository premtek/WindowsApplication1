Public Module MCommonIO

    ''' <summary>
    ''' IO切換狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmIOstatus
        IO_Nono = -1
        IO_Off = 0
        IO_On = 1
    End Enum

    'DI物件關係:
    '(物件放置) (DI卡片集合) (DI卡片公用介面) (實作DI卡片轉接)
    'MCommonIO->CDICollection->IDIInterface->CDI_PCI_1756
    '                                      ->CDI_PCI_1710

    'DO物件關係:
    '(物件放置) (DO卡片集合) (DO卡片公用介面) (實作DO卡片轉接)
    'MCommonIO->CDOCollection->IDOInterface->CDO_PCI_1756
    '                                      ->CDO_PCI_1710

    'AI物件關係:
    '(物件放置) (AI卡片集合) (AI卡片公用介面) (實作AI卡片轉接)
    'MCommonIO->CAICollection->IAIInterface->CAI_PCI_1710

    'AO物件關係:
    '(物件放置) (AO卡片集合) (AO卡片公用介面) (實作AO卡片轉接)
    'MCommonIO->CAOCollection->IAOInterface->CAO_PCI_1723

    '其他：
    'frmIOSet       IO位置設定
    'frmIOTable     IO設定一覽表
    'frmDIConfig    小型DI設定介面
    'frmDOConfig    小型DO設定介面
    'MCommonAlarm   警報訊息相關
    'MIOUse         原方法待處理
   

    ''' <summary>DI卡集合</summary>
    ''' <remarks></remarks>
    Public gDICollection As New Premtek.CDICollection

    ''' <summary>DO卡集合</summary>
    ''' <remarks></remarks>
    Public gDOCollection As New Premtek.CDOCollection

    ''' <summary>AI卡集合</summary>
    ''' <remarks></remarks>
    Public gAICollection As New CAICollection

    ''' <summary>AO卡集合</summary>
    ''' <remarks></remarks>
    Public gAOCollection As New CAOCollection
   
    ''' <summary>電空閥集合</summary>
    ''' <remarks></remarks>
    Public gEPVCollection As New CElectroPneumaticValveCollection


    ''' <summary>互鎖保護集合</summary>
    ''' <remarks></remarks>
    Public gInterlockCollection As New CInterlockCollection

    ''' <summary>設備訊息處理</summary>
    ''' <remarks></remarks>
    Public gEqpMsg As New Premtek.Base.CEqpMsgHandler

    ''' <summary>設備狀態Handler</summary>
    ''' <remarks></remarks>
    Public WithEvents gEqpStatusHandler As New CLightTowerHandler

    ''' <summary>[通訊異常後 允取再從送幾次]</summary>
    ''' <remarks></remarks>
    Public Const gEPVCmdMaxFailCounts As Integer = 3

End Module

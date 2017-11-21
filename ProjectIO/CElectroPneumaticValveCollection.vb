Imports ProjectCore

''' <summary>電空閥控制單元型號</summary>
''' <remarks></remarks>
Public Enum enmEPVCardType
    None = 0
    ''' <summary>
    ''' AIO接點配接
    ''' </summary>
    ''' <remarks></remarks>
    AIOAdapter = 1
    ''' <summary>
    ''' SMC ITV Series 電空比例閥
    ''' </summary>
    ''' <remarks></remarks>
    ITV_Series = 2
End Enum

''' <summary>[電工閥使用方式(Pressure)]</summary>
''' <remarks></remarks>
Public Enum eEPVPressureType
    ''' <summary>[膠管壓力用電工閥]</summary>
    ''' <remarks></remarks>
    Syringe = 0
    ''' <summary>[閥體壓力用電工閥]</summary>
    ''' <remarks></remarks>
    Valve = 1
    ''' <summary>[Max]</summary>
    ''' <remarks></remarks>
    Max = Valve
End Enum


''' <summary>
''' AI/O配接
''' </summary>
''' <remarks></remarks>
Public Structure sAIOAdapter
    ''' <summary>
    ''' 對應的AI接點
    ''' </summary>
    ''' <remarks></remarks>
    Public AI As Integer
    ''' <summary>
    ''' 對應的AO接點
    ''' </summary>
    ''' <remarks></remarks>
    Public AO As Integer
End Structure


''' <summary>
''' 通道參數
''' </summary>
''' <remarks></remarks>
Public Structure sChannelParaemeter
    ''' <summary>
    ''' 名稱
    ''' </summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>
    ''' 相應卡號
    ''' </summary>
    ''' <remarks></remarks>
    Public ItemNo As Integer
    ''' <summary>是否略過</summary>
    ''' <remarks></remarks>
    Public ByPass As Boolean 'Soni 2017.03.22
End Structure

''' <summary>
''' 電空閥集合
''' </summary>
''' <remarks></remarks>
Public Class CElectroPneumaticValveCollection
    ''' <summary>電空閥連線設定集合</summary>
    ''' <remarks></remarks>
    Public Cards As New CElectroPneumaticValveCards
    ''' <summary>[卡片初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsCardIntialOK As Boolean
        Get
            Return mIsCardIntialOK
        End Get
    End Property


    ''' <summary>[卡片初始化狀態]</summary>
    ''' <remarks></remarks>
    Private mIsCardIntialOK As Boolean

    ''' <summary>[是否走Simulation模式]</summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property IsSimulationType As Boolean
        Set(value As Boolean)
            mIsSimulationType = value
        End Set
    End Property

    ''' <summary>[是否走Simulation模式]</summary>
    ''' <remarks></remarks>
    Private mIsSimulationType As Boolean

    ''' <summary>卡集合</summary>
    ''' <remarks></remarks>
    Public Items As New List(Of IElectroPneumaticValve)

    ''' <summary>總點數</summary>
    ''' <remarks></remarks>
    Dim TotalBits As Integer = 31 '預設值

    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cards As List(Of sEPVCardParameter)) As Boolean
        Try
            Dim ErrMessage As String = ""

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                'gSyslog.Save(gMsgHandler.GetMessage(INFO_6008000), "INFO_6008000")
                Return True
            End If

            TotalBits = 0
            For mCardNo As Integer = 0 To cards.Count - 1 '對每一張卡
                Select Case cards(mCardNo).CardType '依卡型號做初始化
                    Case enmEPVCardType.None '虛擬卡
                        Items.Add(New CElectroPneumaticValveVirtual)
                    Case enmEPVCardType.AIOAdapter

                    Case enmEPVCardType.ITV_Series
                        Items.Add(New CElectroPneumaticValve_ITV)
                        If Items(Items.Count - 1).Initial(cards(mCardNo).ITV_Series) = False Then
                            ErrMessage += "" & cards(mCardNo).ITV_Series.PortName & "初始化失敗."
                        End If
                End Select
                TotalBits += Items(Items.Count - 1).ChannelPerCard  '計算總埠數
            Next

            If ErrMessage <> "" Then
                mIsCardIntialOK = False
                'gSyslog.Save(gMsgHandler.GetMessage(Error_1008000), "Error_1008000", eMessageLevel.Error)
                MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CElectroPneumaticValveCollection@Initial")
                Return False
            Else
                mIsCardIntialOK = True
                'gSyslog.Save(gMsgHandler.GetMessage(INFO_6008000), "INFO_6008000")
                Return True
            End If


        Catch ex As Exception
            mIsCardIntialOK = False
            'gSyslog.Save(gMsgHandler.GetMessage(Error_1008000), "Error_1008000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1008000) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CElectroPneumaticValveCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[資源釋放]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Try
            Dim intI As Integer

            If mIsSimulationType = True Then
                Return True
            Else
                If mIsCardIntialOK = True Then
                    '[說明]:卡片關閉
                    For intI = 0 To Items.Count - 1
                        Items(intI).Close()
                    Next
                    'gSyslog.Save(gMsgHandler.GetMessage(INFO_6008001), "INFO_6008001")
                    Return True
                Else
                    'gSyslog.Save(gMsgHandler.GetMessage(INFO_6008001), "INFO_6008001")
                    Return True
                End If
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1008001), "Error_1008001", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1008001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAOCollection@Initial")
            Return False
        End Try

    End Function

    ''' <summary>通道參數</summary>
    ''' <remarks></remarks>
    Dim Channel() As sChannelParaemeter

    ''' <summary>[透過PressureType、Index-->通道Index]</summary>
    ''' <remarks></remarks>
    Dim ChannelMap(,) As Integer

    ''' <summary>[透過Stage、PressureType、ValveNo-->找對應的Index]</summary>
    ''' <remarks></remarks>
    Dim Map(,,) As Integer


    ''' <summary>
    ''' 儲存通道參數
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal strFileName As String) As Boolean
        Dim mChNo As Long
        Dim strSection = "CONFIGURATION"
        For mChNo = Channel.GetLowerBound(0) To Channel.GetUpperBound(0) '紀錄
            Call SaveIniString(strSection, "EPV-" & mChNo & "-Name", Channel(mChNo).Name, strFileName)
            Call SaveIniString(strSection, "EPV-" & mChNo & "-ItemNo", CInt(Channel(mChNo).ItemNo), strFileName)
            Call SaveIniString(strSection, "EPV-" & mChNo & "-ByPass", CInt(Channel(mChNo).ByPass), strFileName)
        Next

        Return True
    End Function
    ''' <summary>
    ''' 讀取通道參數
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal strFileName As String) As Boolean
        Try
            Dim strSection = "CONFIGURATION"

            ReDim Channel(Items.Count - 1)

            For mChNo = Channel.GetLowerBound(0) To Channel.GetUpperBound(0)
                Channel(mChNo).Name = ReadIniString(strSection, "EPV-" & mChNo & "-Name", strFileName, "")
                Channel(mChNo).ItemNo = Val(ReadIniString(strSection, "EPV-" & mChNo & "-ItemNo", strFileName, ""))
                Channel(mChNo).ByPass = Val(ReadIniString(strSection, "EPV-" & mChNo & "-ByPass", strFileName, False))
            Next

            Return True
        Catch ex As Exception
            'gSyslog.Save(gMsgHandler.GetMessage(Error_1002014), "Error_1002014", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CElectroPneumaticValveCollection")
            Return False
        End Try
    End Function


    Public Function LoadMap(ByVal strFileName As String) As Boolean
        Try
            Dim strSection = "Map"

            Dim mType As eEPVPressureType
            Dim mIndex As Integer
            Dim mStage As enmStage
            Dim mValve As eValveWorkMode
            ReDim ChannelMap(eEPVPressureType.Max, Items.Count - 1)
            ReDim Map(gSSystemParameter.StageCount - 1, gSSystemParameter.StageUseValveCount - 1, eEPVPressureType.Max)

            Dim mI As Integer
            Dim mJ As Integer
            Dim mK As Integer

            For mI = 0 To eEPVPressureType.Max
                For mJ = 0 To Items.Count - 1
                    ChannelMap(mI, mJ) = -1
                Next
            Next

            For mI = 0 To gSSystemParameter.StageCount - 1
                For mJ = 0 To gSSystemParameter.StageUseValveCount - 1
                    For mK = 0 To eEPVPressureType.Max
                        Map(mI, mJ, mK) = -1
                    Next
                Next
            Next


            For mChNo = Channel.GetLowerBound(0) To Channel.GetUpperBound(0)
                mType = CInt(ReadIniString(strSection, "EPV-" & mChNo & "-Type", strFileName, 0))
                mIndex = CInt(ReadIniString(strSection, "EPV-" & mChNo & "-Index", strFileName, 0))
                mStage = CInt(ReadIniString(strSection, "EPV-" & mChNo & "-Stage", strFileName, 0))
                mValve = CInt(ReadIniString(strSection, "EPV-" & mChNo & "-Valve", strFileName, 0))
                '[Note]:超出範圍就不做配接
                If mValve < gSSystemParameter.StageUseValveCount Then
                    If mStage < gSSystemParameter.StageCount Then
                        Map(mStage, mValve, mType) = Channel(mChNo).ItemNo
                    End If
                End If
                ChannelMap(mType, mIndex) = Channel(mChNo).ItemNo
            Next

            Return True
        Catch ex As Exception
            'gSyslog.Save(gMsgHandler.GetMessage(Error_1002014), "Error_1002014", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CElectroPneumaticValveCollection")
            Return False
        End Try
    End Function



    ''' <summary>是否索引超出範圍</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsIndexOutOfRange(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType) As Boolean

        Dim mItemNo As Integer
        mItemNo = Map(stageNo, valveNo, type)

        If mItemNo < 0 Then '索引不存在
            Return True
        End If
        If mItemNo >= Channel.Count Then '索引不存在
            Return True
        End If
        If Channel(mItemNo).ItemNo < 0 Then '指定Item不存在
            Return True
        End If
        If Channel(mItemNo).ItemNo >= Items.Count Then '指定Item不存在
            Return True
        End If
        Return False
    End Function
    ''' <summary>是否索引超出範圍</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsIndexOutOfRange(ByVal type As eEPVPressureType, ByVal index As Integer) As Boolean

        Dim mItemNo As Integer

        If index > ChannelMap.GetUpperBound(1) Then
            Return True
        End If
        mItemNo = ChannelMap(type, index)

        If mItemNo < 0 Then '索引不存在
            Return True
        End If
        If mItemNo >= Channel.Count Then '索引不存在
            Return True
        End If
        If Channel(mItemNo).ItemNo < 0 Then '指定Item不存在
            Return True
        End If
        If Channel(mItemNo).ItemNo >= Items.Count Then '指定Item不存在
            Return True
        End If
        Return False
    End Function
    ''' <summary>是否索引超出範圍</summary>
    ''' <param name="enmEPV">enmEPV</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsIndexOutOfRange(ByVal enmEPV As Integer) As Boolean
        If enmEPV < 0 Then 'CCD索引不存在
            Return True
        End If
        If enmEPV >= Channel.Count Then 'CCD索引不存在
            Return True
        End If
        If Channel(enmEPV).ItemNo < 0 Then '指定Item不存在
            Return True
        End If
        If Channel(enmEPV).ItemNo >= Items.Count Then '指定Item不存在
            Return True
        End If
        Return False
    End Function


    ''' <summary>是否忙碌中</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType) As Boolean
        Get
            Dim mItemNo As Integer = Map(stageNo, valveNo, type)
            If mItemNo < 0 Then '索引不存在
                Return False
            End If
            If IsIndexOutOfRange(stageNo, valveNo, type) Then
                Return False
            End If
            Return Items(mItemNo).IsBusy
        End Get
    End Property
    ''' <summary>是否忙碌中</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy(ByVal type As eEPVPressureType, ByVal index As Integer) As Boolean
        Get
            If index > ChannelMap.GetUpperBound(1) Then
                Return False
            End If
            Dim mItemNo As Integer = ChannelMap(type, index)
            If mItemNo < 0 Then '索引不存在
                Return False
            End If
            If IsIndexOutOfRange(type, index) Then
                Return False
            End If
            Return Items(mItemNo).IsBusy
        End Get
    End Property
    ''' <summary>是否忙碌中</summary>
    ''' <param name="enmEPV"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy(ByVal enmEPV As Integer) As Boolean
        Get
            Dim mItemNo As Integer = Channel(enmEPV).ItemNo
            If IsIndexOutOfRange(enmEPV) Then
                Return False
            End If
            Return Items(mItemNo).IsBusy
        End Get
    End Property


    ''' <summary>是否逾時</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType) As Boolean
        Get
            Dim mItemNo As Integer = Map(stageNo, valveNo, type)
            If mItemNo < 0 Then '索引不存在
                Return False
            End If
            If IsIndexOutOfRange(stageNo, valveNo, type) Then
                Return False
            End If
            Return Items(mItemNo).IsTimeOut
        End Get
    End Property
    ''' <summary>是否逾時</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut(ByVal type As eEPVPressureType, ByVal index As Integer) As Boolean
        Get
            If index > ChannelMap.GetUpperBound(1) Then
                Return False
            End If
            Dim mItemNo As Integer = ChannelMap(type, index)
            If mItemNo < 0 Then '索引不存在
                Return False
            End If
            If IsIndexOutOfRange(type, index) Then
                Return False
            End If
            Return Items(mItemNo).IsTimeOut
        End Get
    End Property
    ''' <summary>是否逾時</summary>
    ''' <param name="enmEPV"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut(ByVal enmEPV As Integer) As Boolean
        Get
            Dim mItemNo As Integer = Channel(enmEPV).ItemNo
            If IsIndexOutOfRange(enmEPV) Then
                Return False
            End If
            Return Items(mItemNo).IsTimeOut
        End Get
    End Property

    ''' <summary>設定參數</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValue(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType, ByVal value As Decimal, ByVal waitReturn As Boolean) As Boolean

        Dim mItemNo As Integer = Map(stageNo, valveNo, type)

        If mItemNo < 0 Then
            Return False
        End If

        If Channel(mItemNo).ByPass Then
            Return True
        End If
        If IsIndexOutOfRange(stageNo, valveNo, type) Then
            Return False
        End If
        Return Items(mItemNo).SetValue(value, waitReturn)
    End Function
    ''' <summary>設定參數</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValue(ByVal type As eEPVPressureType, ByVal index As Integer, ByVal value As Decimal, ByVal waitReturn As Boolean) As Boolean

        If index > ChannelMap.GetUpperBound(1) Then
            Return False
        End If
        Dim mItemNo As Integer = ChannelMap(type, index)

        If mItemNo < 0 Then
            Return False
        End If
        If Channel(mItemNo).ByPass Then
            Return True
        End If
        If IsIndexOutOfRange(type, index) Then
            Return False
        End If
        Return Items(mItemNo).SetValue(value, waitReturn)
    End Function
    ''' <summary>設定參數</summary>
    ''' <param name="enmEPV"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValue(ByVal enmEPV As Integer, ByVal value As Decimal, ByVal waitReturn As Boolean) As Boolean
        If Channel(enmEPV).ByPass Then 'Soni 2017.03.22
            Return True
        End If
        Dim mItemNo As Integer = Channel(enmEPV).ItemNo
        If IsIndexOutOfRange(enmEPV) Then
            Return False
        End If
        Return Items(mItemNo).SetValue(value, waitReturn)
    End Function

    ''' <summary>讀取參數</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValue(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType, Optional ByRef value As Decimal = 0, Optional ByVal waitReturn As Boolean = False) As Boolean

        Dim mItemNo As Integer = Map(stageNo, valveNo, type)

        If mItemNo < 0 Then
            Return False
        End If
        If Channel(mItemNo).ByPass Then
            Return True
        End If
        If IsIndexOutOfRange(mItemNo) Then
            Return False
        End If
        Return Items(mItemNo).GetValue(value, waitReturn)
    End Function
    ''' <summary>讀取參數</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValue(ByVal type As eEPVPressureType, ByVal index As Integer, Optional ByRef value As Decimal = 0, Optional ByVal waitReturn As Boolean = False) As Boolean

        If index > ChannelMap.GetUpperBound(1) Then
            Return False
        End If
        Dim mItemNo As Integer = ChannelMap(type, index)

        If mItemNo < 0 Then
            Return False
        End If

        If Channel(mItemNo).ByPass Then
            Return True
        End If
        If IsIndexOutOfRange(mItemNo) Then
            Return False
        End If
        Return Items(mItemNo).GetValue(value, waitReturn)
    End Function
    ''' <summary>
    ''' 讀取參數
    ''' </summary>
    ''' <param name="enmEPV"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValue(ByVal enmEPV As Integer, Optional ByRef value As Decimal = 0, Optional ByVal waitReturn As Boolean = False) As Boolean
        If Channel(enmEPV).ByPass Then 'Soni 2017.03.22
            Return True
        End If
        Dim mItemNo As Integer = Channel(enmEPV).ItemNo

        If IsIndexOutOfRange(enmEPV) Then
            Return False
        End If
        Return Items(mItemNo).GetValue(value, waitReturn)
    End Function

    ''' <summary>傳回結果資料結構</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType) As sReceiveStatus
        Get
            Dim mItemNo As Integer = Map(stageNo, valveNo, type)
            If mItemNo < 0 Then
                Return New sReceiveStatus
            End If
            If IsIndexOutOfRange(stageNo, valveNo, type) Then
                Return New sReceiveStatus
            End If

            If Channel(mItemNo).ByPass Then
                Dim X As New sReceiveStatus
                X.Status = True
                Return X
            End If

            Return Items(mItemNo).Result
        End Get
    End Property
    ''' <summary>傳回結果資料結構</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result(ByVal type As eEPVPressureType, ByVal index As Integer) As sReceiveStatus
        Get
            If index > ChannelMap.GetUpperBound(1) Then
                Return New sReceiveStatus
            End If
            Dim mItemNo As Integer = ChannelMap(type, index)
            If mItemNo < 0 Then
                Return New sReceiveStatus
            End If
            If IsIndexOutOfRange(type, index) Then
                Return New sReceiveStatus
            End If
            Return Items(mItemNo).Result
        End Get
    End Property
    ''' <summary>傳回結果資料結構</summary>
    ''' <param name="enmEPV"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result(ByVal enmEPV As Integer) As sReceiveStatus
        Get
            Dim mItemNo As Integer = Channel(enmEPV).ItemNo
            If IsIndexOutOfRange(enmEPV) Then
                Return New sReceiveStatus
            End If
            Return Items(mItemNo).Result
        End Get
    End Property

    ''' <summary>讀取設定最大值</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaxMpa(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType, ByRef value As Decimal) As Boolean

        Dim mItemNo As Integer = Map(stageNo, valveNo, type)
        If mItemNo < 0 Then
            Return False
        End If
        If IsIndexOutOfRange(mItemNo) Then
            Return False
        End If
        value = Items(mItemNo).Max_Mpa
        Return True

    End Function
    ''' <summary>讀取設定最大值</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaxMpa(ByVal type As eEPVPressureType, ByVal index As Integer, ByRef value As Decimal) As Boolean

        If index > ChannelMap.GetUpperBound(1) Then
            Return False
        End If
        Dim mItemNo As Integer = ChannelMap(type, index)
        If mItemNo < 0 Then
            Return False
        End If
        If IsIndexOutOfRange(mItemNo) Then
            Return False
        End If
        value = Items(mItemNo).Max_Mpa
        Return True

    End Function
    ''' <summary>
    ''' 讀取設定最大值
    ''' </summary>
    ''' <param name="enmEPV"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaxMpa(ByVal enmEPV As Integer, ByRef value As Decimal) As Boolean

        Dim mItemNo As Integer = Channel(enmEPV).ItemNo
        If mItemNo < 0 Then
            Return False
        End If
        If IsIndexOutOfRange(enmEPV) Then
            Return False
        End If
        value = Items(mItemNo).Max_Mpa
        Return True

    End Function

End Class

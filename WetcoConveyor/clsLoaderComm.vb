Imports ProjectCore
Imports ProjectIO

Public Class clsLoaderComm
    Dim WithEvents PLC_FX3U As New clsPLC_FX3U
    Dim SwTimeOut As New Stopwatch

    Enum enmStatus
        [Stop] = 0
        Run = 1
        Alarm = 2
        Idle = 3


        Null
    End Enum

    Enum enmTask
        Null
        GetMachineStatus
        GetPassModel
        SetPassModel
        GetAlarmCode
        GetProductType
        SetProductType
        GetHpTemperatures
        GetTargetTemp
        SetTargetTemp
        GetProductCount
        GetProductNum
        SetProductNum
        GetSlotStatus
        GetCaseteBarCode
        SetCaseteBarCode
        CaseteAbort
    End Enum

    Enum enmStep
        Start
        [End]
        SendCmd
        GetReceive
        SendCmd2
        GetReceive2
        RecipeChange
        RecipeChangeFinish
    End Enum

    ''' <summary>
    ''' 是否忙碌中 (有無執行中任務)
    ''' </summary>
    Public IsBusy As Boolean
    ''' <summary>
    ''' 執行中任務
    ''' </summary>
    Public Task As enmTask = enmTask.Null
    ''' <summary>
    ''' 任務執行到的步驟
    ''' </summary>
    Public TaskStep As enmStep = enmStep.Start

    Dim PlcTimeout As Integer = gSSystemParameter.TimeOut4
    Dim ReceivedData() As String

    Dim _isRecieveBusy As Boolean
    ''' <summary>
    ''' 是否忙碌中(執行資料接收中)
    ''' </summary>
    Public ReadOnly Property IsRecieveBusy As Boolean
        Get
            Return Not PLC_FX3U.IsDataRecieved
        End Get
    End Property

    ''' <summary>
    ''' Loader資料
    ''' </summary>
    Public Data As New clsULoaderData

    Dim _alarmMsg As String
    ''' <summary>
    ''' 錯誤訊息
    ''' </summary>
    ReadOnly Property AlarmMessage As String
        Get
            Return _alarmMsg
        End Get
    End Property

    Dim _alarmCode As String
    ''' <summary>
    ''' 錯誤訊息代碼
    ''' </summary>
    ReadOnly Property AlarmCode As String
        Get
            Return _alarmCode
        End Get
    End Property

#Region "Loader IO 交握訊號"
    ''' <summary>
    ''' 表示點膠機A是否Alarm
    ''' </summary>
    Public Property IsAMachineAlarm As Boolean
        Get
            Return gDOCollection.GetState(enmDO.SystemAlarm)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.SystemAlarm, value)
        End Set
    End Property

    ''' <summary>
    ''' 指出Loader是否Alarm
    ''' </summary>
    Public ReadOnly Property IsAlarm As Boolean
        Get
            Return gDICollection.GetState(enmDI.PrevAlarm, False)
        End Get
    End Property

    ''' <summary>
    ''' 表示點膠機Casete Bar-Code資訊是否接收完成
    ''' </summary>
    Public Property IsCaseteBarCodeReceiveFinish As Boolean
        Get
            Return gDOCollection.GetState(enmDO.CassetteBarcodeReceiveFinish1)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.CassetteBarcodeReceiveFinish1, value)
        End Set
    End Property

    ''' <summary>
    ''' 指出點膠機是否可讀取Casette Bar-Code 資訊
    ''' </summary>
    Public ReadOnly Property IsCaseteBarCodeReady As Boolean
        Get
            Return gDICollection.GetState(enmDI.CassetteBarcodeReady1, True)
        End Get
    End Property

    ''' <summary>
    ''' 表示點膠機是否接收完成Loader Mapping Data資訊
    ''' </summary>
    Public Property IsMappingReceiveFinish As Boolean
        Get
            Return gDOCollection.GetState(enmDO.MappingDataReceiveFinish1)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.MappingDataReceiveFinish1, value)
        End Set
    End Property

    ''' <summary>
    ''' 指出點膠機是否可讀取Loader Mapping data 資訊
    ''' </summary>
    Public ReadOnly Property IsMappingFinish As Boolean
        Get
            Return gDICollection.GetState(enmDI.CassetteMappingReady1, True)
        End Get
    End Property

    ''' <summary>
    ''' 表示Loader Recipe 是否需執行變更
    ''' </summary>
    Public Property IsRecipeChange As Boolean
        Get
            Return gDOCollection.GetState(enmDO.ExchangeRecipe1)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.ExchangeRecipe1, value)
        End Set
    End Property

    ''' <summary>
    ''' 指出Loader Recipe 是否接收完成
    ''' </summary>
    Public ReadOnly Property IsRecipeChangeFinish As Boolean
        Get
            Return gDICollection.GetState(enmDI.ExchangeRecipeFinish1, True)
        End Get
    End Property

    ''' <summary>
    ''' 表示Loader是否需要把Cassette退出
    ''' </summary>
    Public Property IsCaseteAbort As Boolean
        Get
            Return gDOCollection.GetState(enmDO.CassetteAbort1)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.CassetteAbort1, value)
        End Set
    End Property

    ''' <summary>
    ''' 指出Loader是否已把Cassette退出
    ''' </summary>
    Public ReadOnly Property IsCaseteAbortFinish As Boolean
        Get
            Return gDICollection.GetState(enmDI.CassetteAbortFinish1, True)
        End Get
    End Property
#End Region

    Dim ID_Pass As String = "D7010"
    Dim ID_TargetTemp As String = "D7020"
    Dim ID_ProductType As String = "D7050"
    Dim ID_MachineStatus As String = "D7500"
    Dim ID_AlarmCode() As String = {"D7501", "D7502", "D7503", "D7504", "D7505"}
    Dim ID_UpperTempreatures() As String = {"D7520", "D7521", "D7522", "D7523", "D7524", "D7525"}
    Dim ID_LowerTempreatures() As String = {"D7530", "D7531", "D7532", "D7533", "D7534", "D7535"}
    'Dim ID_ProductCount As String = "D7550"
    'Dim ID_SlotsStatus() As String = {"D7551", "D7552", "D7553", "D7554", "D7555", "D7556", "D7557", "D7558", "D7559", "D7560"}
    Dim ID_CaseteDataA() As String = {"D7550", "D7551", "D7552", "D7553", "D7554", "D7555", "D7556", "D7557", "D7558", "D7559", "D7560"}
    Dim ID_CaseteDataB As String = "D7650"
    Dim ID_CaseteBarCode() As String = {"D7600", "D7601", "D7602", "D7603", "D7604", "D7605", "D7606", "D7607", "D7608", "D7609"}
    Dim ID_GetProductNum As String = "D7620"
    Dim ID_SetProductNum As String = "D7120"
    Dim ID_SetLastProduct As String = "D7130"

    Sub New()

    End Sub

    ''' <summary>
    ''' 是否已開啟
    ''' </summary>
    Public ReadOnly Property IsOpen As Boolean
        Get
            Return PLC_FX3U.IsOpen
        End Get
    End Property

    ''' <summary>
    ''' 開啟PLC連線
    ''' </summary>
    Public Function Open(portName As String, baudRate As Integer, parity As System.IO.Ports.Parity, dataBits As Integer, stopBits As System.IO.Ports.StopBits) As Boolean
        If PLC_FX3U.Open(portName, baudRate, parity, dataBits, stopBits) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 關閉PLC連線
    ''' </summary>
    Public Sub Close()
        PLC_FX3U.Close()
    End Sub

    ''' <summary>
    ''' [流程]:取得設備狀態
    ''' </summary>
    Public Function GetMachineStatus() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetMachineStatus
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054006"
                        _alarmMsg = "Loader PLC Timeout : Get Machine Status."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_MachineStatus, 1)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054006"
                        _alarmMsg = "Loader PLC Timeout : Get Machine Status."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.Status = Convert.ToInt32(ReceivedData(0), 16)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取Pass模式
    ''' </summary>
    Public Function GetPassModel() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetPassModel
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054007"
                        _alarmMsg = "Loader PLC Timeout : Get Pass Model."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_Pass, 1)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054007"
                        _alarmMsg = "Loader PLC Timeout : Get Pass Model."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.Pass = Convert.ToInt32(ReceivedData(0), 16)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:設定Pass模式
    ''' </summary>
    Public Function SetPassModel(ByVal model As Integer) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.SetPassModel
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054008"
                        _alarmMsg = "Loader PLC Timeout : Set Pass Model."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.WriteWords(ID_Pass, 1, model.ToString("X4"))
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054008"
                        _alarmMsg = "Loader PLC Timeout : Set Pass Model."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取錯誤訊息命令
    ''' </summary>
    Public Function GetAlarmCode() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetAlarmCode
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054009"
                        _alarmMsg = "Loader PLC Timeout : Get Alarm Code."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_AlarmCode(0), 5)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054009"
                        _alarmMsg = "Loader PLC Timeout : Get Alarm Code."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            For i = 0 To ReceivedData.Length - 1
                                Data.AlarmCodes(i) = Convert.ToInt32(ReceivedData(i), 16)
                            Next
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:設定產品類別
    ''' </summary>
    Public Function SetProductType(ByVal type As Integer) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.SetProductType
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054010"
                        _alarmMsg = "Loader PLC Timeout : Set Product Type."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.WriteWords(ID_ProductType, 1, type.ToString("X4"))
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054010"
                        _alarmMsg = "Loader PLC Timeout : Set Product Type."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RecipeChange
                        End If
                    End If

                Case enmStep.RecipeChange
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054010"
                        _alarmMsg = "Loader PLC Timeout : Set Product Type."
                        Return enmMotionStatus.Alarm
                    Else
                        IsRecipeChange = True
                        If (IsRecipeChangeFinish) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RecipeChangeFinish
                        End If
                    End If

                Case enmStep.RecipeChangeFinish
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054010"
                        _alarmMsg = "Loader PLC Timeout : Set Product Type."
                        Return enmMotionStatus.Alarm
                    Else
                        IsRecipeChange = False
                        If (IsRecipeChangeFinish = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取產品類別
    ''' </summary>
    Public Function GetProductType() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetProductType
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054011"
                        _alarmMsg = "Loader PLC Timeout : Get Product Type."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_ProductType, 1)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054011"
                        _alarmMsg = "Loader PLC Timeout : Get Product Type."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.ProductType = Convert.ToInt32(ReceivedData(0), 16)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取Hot plate溫度
    ''' </summary>
    Public Function GetHpTemperatures() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetHpTemperatures
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054012"
                        _alarmMsg = "Loader PLC Timeout : Get Hot Plate Temperatures."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_UpperTempreatures(0), 6)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054012"
                        _alarmMsg = "Loader PLC Timeout : Get Hot Plate Temperatures."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            For i = 0 To ReceivedData.Length - 1
                                Data.Temperatures(i) = Convert.ToInt32(ReceivedData(i), 16)
                            Next
                            SwTimeOut.Restart()
                            TaskStep = enmStep.SendCmd2
                        End If
                    End If

                Case enmStep.SendCmd2
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054012"
                        _alarmMsg = "Loader PLC Timeout : Get Hot Plate Temperatures."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_LowerTempreatures(0), 6)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive2
                        End If
                    End If

                Case enmStep.GetReceive2
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054012"
                        _alarmMsg = "Loader PLC Timeout : Get Hot Plate Temperatures."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            For i = 0 To ReceivedData.Length - 1
                                Data.Temperatures(i + 6) = Convert.ToInt32(ReceivedData(i), 16)
                            Next
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取溫度設定值
    ''' </summary>
    Public Function GetTargetTemp() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetTargetTemp
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054013"
                        _alarmMsg = "Loader PLC Timeout : Get Target Temperature."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_TargetTemp, 1)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054013"
                        _alarmMsg = "Loader PLC Timeout : Get Target Temperature."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.TargetTemp = Convert.ToInt32(ReceivedData(0), 16)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:設定溫度設定值
    ''' </summary>
    Public Function SetTargetTemp(ByVal temperature As Integer) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.SetTargetTemp
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054014"
                        _alarmMsg = "Loader PLC Timeout : Set Target Temperature."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.WriteWords(ID_TargetTemp, 1, temperature.ToString("X4"))
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054014"
                        _alarmMsg = "Loader PLC Timeout : Set Target Temperature."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RecipeChange
                        End If
                    End If

                Case enmStep.RecipeChange
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054014"
                        _alarmMsg = "Loader PLC Timeout : Set Target Temperature."
                        Return enmMotionStatus.Alarm
                    Else
                        IsRecipeChange = True
                        If (IsRecipeChangeFinish) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RecipeChangeFinish
                        End If
                    End If

                Case enmStep.RecipeChangeFinish
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054014"
                        _alarmMsg = "Loader PLC Timeout : Set Target Temperature."
                        Return enmMotionStatus.Alarm
                    Else
                        IsRecipeChange = False
                        If (IsRecipeChangeFinish = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取Loader入料產品資訊 (Loader給主機)
    ''' </summary>
    Public Function GetProductNum() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetProductNum
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054015"
                        _alarmMsg = "Loader PLC Timeout : Get Product Number."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_GetProductNum, 1)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054015"
                        _alarmMsg = "Loader PLC Timeout : Get Product Number."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.ProductNum = Convert.ToInt32(ReceivedData(0), 16)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:設定Unloader出料產品資訊 (主機給Unloader)
    ''' </summary>
    Public Function SetProductNum(ByVal num As Integer) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.SetProductNum
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054016"
                        _alarmMsg = "Loader PLC Timeout : Set Product Number."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.WriteWords(ID_SetProductNum, 1, num.ToString("X4"))
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054016"
                        _alarmMsg = "Loader PLC Timeout : Set Product Number."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取Cassette資料
    ''' </summary>
    Public Function GetCaseteData(Optional smemaIO As Boolean = True) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetSlotStatus
                    IsBusy = True
                    SwTimeOut.Start()
                    If (IsMappingFinish Or smemaIO = False) Then
                        TaskStep = enmStep.SendCmd
                    End If

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054017"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Data."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_CaseteDataA(0), 21)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054017"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Data."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.ProductCountA = Convert.ToInt32(ReceivedData(0), 16)
                            For i = 1 To ReceivedData.Length - 1
                                Data.SlotStatusA(i - 1) = Convert.ToInt32(ReceivedData(i), 16)
                                If (Data.SlotStatusA(i - 1) = 1) Then
                                    Data.LastProductNumA = i + 100
                                End If
                            Next
                            SwTimeOut.Restart()
                            TaskStep = enmStep.SendCmd2
                        End If
                    End If

                Case enmStep.SendCmd2
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054017"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Data."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_CaseteDataB, 21)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive2
                        End If
                    End If

                Case enmStep.GetReceive2
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054017"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Data."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Data.ProductCountB = Convert.ToInt32(ReceivedData(0), 16)
                            For i = 1 To ReceivedData.Length - 1
                                Data.SlotStatusB(i - 1) = Convert.ToInt32(ReceivedData(i), 16)
                                If (Data.SlotStatusB(i - 1) = 1) Then
                                    Data.LastProductNumB = i + 200
                                End If
                            Next
                            IsMappingReceiveFinish = True
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    If (IsMappingFinish = False Or smemaIO = False) Then
                        IsMappingReceiveFinish = False
                        Reset()
                        Return enmMotionStatus.Finish
                    End If
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:讀取Bar-Code資料
    ''' </summary>
    Public Function GetCaseteBarCode() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.GetCaseteBarCode
                    IsBusy = True
                    SwTimeOut.Start()
                    If (IsCaseteBarCodeReady) Then
                        TaskStep = enmStep.SendCmd
                    End If

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054018"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Barcode."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_CaseteBarCode(0), 10)
                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054018"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Barcode."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then

                            Dim barCode As String = ""
                            For i = 0 To ReceivedData.Length - 1

                                Dim arrHex(1) As String
                                arrHex(0) = ReceivedData(i).Substring(0, 2)
                                arrHex(1) = ReceivedData(i).Substring(2, 2)

                                For Each hexx In arrHex
                                    Dim value As Integer = Convert.ToInt32(hexx, 16)
                                    Dim stringValue As String = Char.ConvertFromUtf32(value)
                                    barCode = barCode & stringValue
                                Next
                            Next
                            Data.CaseteBarCode = barCode

                            IsCaseteBarCodeReceiveFinish = True
                            SwTimeOut.Restart()
                            TaskStep = 1400
                        End If
                    End If

                Case 1400   '判斷Barcode正確性
                    Dim b As Boolean = BarcodeCompare()
                    If (b) Then
                        TaskStep = enmStep.End
                    Else
                        TaskStep = 1500
                    End If

                Case 1500   '退Cassette
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054018"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Barcode."
                        Return enmMotionStatus.Alarm
                    Else
                        IsCaseteAbort = True
                        If (IsCaseteAbortFinish) Then
                            SwTimeOut.Restart()
                            TaskStep = 1600
                        End If
                    End If

                Case 1600
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054018"
                        _alarmMsg = "Loader PLC Timeout : Get Cassette Barcode."
                        Return enmMotionStatus.Alarm
                    Else
                        IsCaseteAbort = False
                        If (IsCaseteAbortFinish = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    If (IsCaseteBarCodeReady = False) Then
                        IsCaseteBarCodeReceiveFinish = False
                        Reset()
                        Return enmMotionStatus.Finish
                    End If
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:設定Bar-Code資料
    ''' </summary>
    Public Function SetCaseteBarCode(ByVal barCode As String) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.SetCaseteBarCode
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.SendCmd

                Case enmStep.SendCmd
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054019"
                        _alarmMsg = "Loader PLC Timeout : Set Cassette Barcode."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            Dim values() As Char = barCode.ToCharArray()
                            Dim hex As String = ""
                            For Each value In values
                                Dim data As Integer = Convert.ToInt32(value)
                                hex = hex + String.Format("{0:X}", data)
                            Next
                            Dim length As Integer = barCode.Length / 2
                            'length = length + (barCode.Length Mod 2)
                            PLC_FX3U.WriteWords(ID_CaseteBarCode(0), length, hex)

                            SwTimeOut.Restart()
                            TaskStep = enmStep.GetReceive
                        End If
                    End If

                Case enmStep.GetReceive
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054019"
                        _alarmMsg = "Loader PLC Timeout : Set Cassette Barcode."
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' [流程]:退Cassette
    ''' </summary>
    Public Function CaseteAbort() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.CaseteAbort
                    IsBusy = True
                    SwTimeOut.Start()
                    TaskStep = enmStep.RecipeChange

                Case enmStep.RecipeChange
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054020"
                        _alarmMsg = "Loader PLC Timeout : Cassette Abort."
                        Return enmMotionStatus.Alarm
                    Else
                        IsCaseteAbort = True
                        If (IsCaseteAbortFinish) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RecipeChangeFinish
                        End If
                    End If

                Case enmStep.RecipeChangeFinish
                    If (TimeOutCheck(PlcTimeout)) Then
                        _alarmCode = "Alarm_2054020"
                        _alarmMsg = "Loader PLC Timeout : Cassette Abort."
                        Return enmMotionStatus.Alarm
                    Else
                        IsCaseteAbort = False
                        If (IsCaseteAbortFinish = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmCode = "Alarm_2054021"
            _alarmMsg = "Loader PLC : " & ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    Private Sub PLC_DataReceived(sender As Object, e As DataEventArgs) Handles PLC_FX3U.ReadBitsDaataRecieved
        Dim count As Integer = Convert.ToInt32(e.Data2)
        ReDim ReceivedData(count - 1)
        For i = 0 To count - 1
            ReceivedData(i) = e.Data.Substring(4 * i, 4)
        Next
    End Sub

    Private Sub PLC_CommandSuccess(sender As Object, e As DataEventArgs) Handles PLC_FX3U.CommandSuccess

    End Sub

    Private Sub PLC_ErrorOccurred(sender As Object, e As DataEventArgs) Handles PLC_FX3U.ErrorOccurred

    End Sub

    Private Function AsciiToString(ByVal ascii As String, ByVal length As Integer) As String
        Dim myByte As Byte
        Dim bytes(length - 1) As Byte
        For i = 0 To length - 1
            Dim d As String = ascii.Substring(4 * i, 4)
            Byte.TryParse(d, myByte)
            bytes(i) = myByte
        Next

        Dim str As String = System.Text.Encoding.Default.GetString(bytes)
        Return str
    End Function

    ''' <summary>
    ''' 判斷動作是否逾時
    ''' </summary>
    ''' <param name="time">毫秒</param>
    Private Function TimeOutCheck(ByVal time As Integer) As Boolean
        If (SwTimeOut.ElapsedMilliseconds > time) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 工作重置
    ''' </summary>
    ''' <remarks>發生異常後須執行重製動作</remarks>
    Public Sub Reset()
        Task = enmTask.Null
        SwTimeOut.Reset()
        TaskStep = enmStep.Start
        IsBusy = False
    End Sub

    Private Function BarcodeCompare() As Boolean
        Return True
    End Function

End Class

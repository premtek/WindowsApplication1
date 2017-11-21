Imports ProjectIO
Imports ProjectCore
Imports System.Threading.Thread

Public Class cls800AQLul

    Enum enmDevice
        Loader
        Unloader
    End Enum

    Enum enmPlcTask
        Null
        AlwaysRun
        GetMachineStatus

        GetPassModel
        SetPassModel

        GetProductType
        SetProductType

        GetTempreature

        GetTargetTemp
        SetTargetTemp

        GetCaseteData
        GetCaseteBarCode
        SetCaseteBarCode

        GetProductNum
        SetProductNum
        SetLastProductNum

        GetAlarmCode

        CassetteAbort
    End Enum

    Dim LoaderTask As enmPlcTask = enmPlcTask.AlwaysRun
    Dim UnloaderTask As enmPlcTask = enmPlcTask.AlwaysRun

    Dim LoaderAlwaysRunTask As enmPlcTask = enmPlcTask.GetMachineStatus
    Dim UnloaderAlwaysRunTask As enmPlcTask = enmPlcTask.GetMachineStatus

    Public Loader As New clsLoaderComm
    Public Unloader As New clsUnloaderComm

    Public LoaderPort As New clsPortData
    Public UnloaderPort As New clsPortData

    ''' <summary>
    ''' Loader資料
    ''' </summary>
    Public LoaderData As clsULoaderData = Loader.Data
    ''' <summary>
    ''' Unloader資料
    ''' </summary>
    Public UnloaderData As clsULoaderData = Unloader.Data

    Dim threadLoader As Threading.Thread
    Dim threadUnloader As Threading.Thread

    ''' <summary>
    ''' 是否Pass Loader : true則Pass Loader PLC 通訊
    ''' </summary>
    Public IsLoaderPass As Boolean
    ''' <summary>
    ''' 是否Pass Unloader : true則Pass Unloader PLC 通訊
    ''' </summary>
    Public IsUnloaderPass As Boolean

    ''' <summary>
    ''' A機上的產品編號 : 由前站Loader提供, Cassette A 產品編號由 100~199, Cassette B 產品編號由 200~299
    ''' </summary>
    Public A_ProductNum As Integer
    ''' <summary>
    ''' B機上的產品編號 : 由前站Loader提供, Cassette A 產品編號由 100~199, Cassette B 產品編號由 200~299
    ''' </summary>
    Public B_ProductNum As Integer

    Dim BarCodeValue As String

    Dim Sw As New Stopwatch

    Sub New()
        If (MachineName = "DCSW-800AQ") Then
            Dim ConveyorIniPath As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName & "\Conveyor.ini"
            Try
                LoaderPort.PortName = ReadIniString("Loader", "PortName", ConveyorIniPath, "COM60")
                LoaderPort.BuadRade = Convert.ToInt32(ReadIniString("Loader", "BuadRade", ConveyorIniPath, 9600))
                LoaderPort.Parity = Convert.ToInt32(ReadIniString("Loader", "Parity", ConveyorIniPath, CInt(System.IO.Ports.Parity.Even)))
                LoaderPort.DataBits = Convert.ToInt32(ReadIniString("Loader", "DataBits", ConveyorIniPath, 7))
                LoaderPort.StopBits = Convert.ToInt32(ReadIniString("Loader", "StopBits", ConveyorIniPath, CInt(System.IO.Ports.StopBits.One)))

                UnloaderPort.PortName = ReadIniString("Unloader", "PortName", ConveyorIniPath, "COM61")
                UnloaderPort.BuadRade = Convert.ToInt32(ReadIniString("Unloader", "BuadRade", ConveyorIniPath, 9600))
                UnloaderPort.Parity = Convert.ToInt32(ReadIniString("Unloader", "Parity", ConveyorIniPath, CInt(System.IO.Ports.Parity.Even)))
                UnloaderPort.DataBits = Convert.ToInt32(ReadIniString("Unloader", "DataBits", ConveyorIniPath, 7))
                UnloaderPort.StopBits = Convert.ToInt32(ReadIniString("Unloader", "StopBits", ConveyorIniPath, CInt(System.IO.Ports.StopBits.One)))
            Catch ex As Exception
                'Sue0605
                gEqpMsg.AddHistoryAlarm("Error_1002024", "clsDTSConveyor.vb", 0, "Sub New() :Read " & ConveyorIniPath & "ini error", eMessageLevel.Alarm)
            End Try

            If (Loader.Open(LoaderPort.PortName, LoaderPort.BuadRade, LoaderPort.Parity, LoaderPort.DataBits, LoaderPort.StopBits) <> True) Then
                'Sue0605
                'Loader PLC連結失敗
                MsgBox(gMsgHandler.GetMessage(Alarm_2054037), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Loader PLC connect fail.")
            End If
            If (Unloader.Open(UnloaderPort.PortName, UnloaderPort.BuadRade, UnloaderPort.Parity, UnloaderPort.DataBits, UnloaderPort.StopBits) <> True) Then
                'Sue0605
                'Unloader PLC連結失敗
                MsgBox(gMsgHandler.GetMessage(Alarm_2054038), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Unloader PLC connect fail.")
            End If

            threadLoader = New Threading.Thread(AddressOf BackgroundLoader)
            threadUnloader = New Threading.Thread(AddressOf BackgroundUnloader)

            'If (Loader.IsOpen Or Unloader.IsOpen) Then
            '    timerPLC.Interval = 100
            '    AddHandler timerPLC.Elapsed, AddressOf timerPLC_OnTimedEvent
            '    timerPLC.Start()
            'End If
        End If

    End Sub

#Region "Loader/Unloader"

    Dim _isLoaderBusy As Boolean
    ''' <summary>
    ''' Loader 是否忙碌中
    ''' </summary>
    Public ReadOnly Property IsLoaderBusy As Boolean
        Get
            Return _isLoaderBusy
        End Get
    End Property

    Dim _isUnloaderBusy As Boolean
    ''' <summary>
    ''' Loader 是否忙碌中
    ''' </summary>
    Public ReadOnly Property IsUnloaderBusy As Boolean
        Get
            Return _isUnloaderBusy
        End Get
    End Property

    ''' <summary>
    ''' 暫無使用
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timerPLC_OnTimedEvent(source As Object, e As Timers.ElapsedEventArgs)
        If (Loader.IsOpen) Then
            If (threadLoader.IsAlive <> True AndAlso IsLoaderPass = False) Then
                threadLoader = New Threading.Thread(AddressOf BackgroundLoader)
                threadLoader.Name = "cls800AQLul.timerPLC_OnTimedEvent"
                threadLoader.IsBackground = True
                threadLoader.Start()
            End If
        End If

        If (Unloader.IsOpen) Then
            If (threadUnloader.IsAlive <> True AndAlso IsUnloaderPass = False) Then
                threadUnloader = New Threading.Thread(AddressOf BackgroundUnloader)
                threadUnloader.Name = "cls800AQLul.timerPLC_OnTimedEvent"
                threadUnloader.IsBackground = True
                threadUnloader.Start()
            End If
        End If
    End Sub

    Dim PreLoader As enmPlcTask = enmPlcTask.AlwaysRun
    Public Sub BackgroundLoader()
        Dim status As enmMotionStatus = enmMotionStatus.Null
        '變更任務時重製動作
        If (PreLoader = enmPlcTask.AlwaysRun AndAlso LoaderTask <> enmPlcTask.AlwaysRun) Then
            PreLoader = LoaderTask
            Loader.Reset()
        End If

        Select Case LoaderTask
            Case enmPlcTask.AlwaysRun
                Select Case LoaderAlwaysRunTask
                    Case enmPlcTask.GetMachineStatus
                        status = Loader.GetMachineStatus()
                        If (status = enmMotionStatus.Finish) Then
                            LoaderAlwaysRunTask = enmPlcTask.GetTempreature
                        End If
                    Case enmPlcTask.GetTempreature
                        status = Loader.GetHpTemperatures()
                        If (status = enmMotionStatus.Finish) Then
                            LoaderAlwaysRunTask = enmPlcTask.GetMachineStatus
                        End If
                End Select

            Case enmPlcTask.GetPassModel
                _isLoaderBusy = True
                status = Loader.GetPassModel()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.SetPassModel
                _isLoaderBusy = True
                status = Loader.SetPassModel(Loader.Data.Pass)
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.GetProductType
                _isLoaderBusy = True
                status = Loader.GetProductType()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.SetProductType
                _isLoaderBusy = True
                status = Loader.SetProductType(Loader.Data.ProductType)
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.GetTargetTemp
                _isLoaderBusy = True
                status = Loader.GetTargetTemp()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.SetTargetTemp
                _isLoaderBusy = True
                status = Loader.SetTargetTemp(Loader.Data.TargetTemp)
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

                'Case enmPlcTask.GetCaseteData
                '    _isLoaderBusy = True
                '    status = Loader.GetCaseteData()
                '    If (status = enmMotionStatus.Finish) Then
                '        _isLoaderBusy = False
                '    End If

            Case enmPlcTask.GetCaseteData
                _isLoaderBusy = True
                If (Loader.GetCaseteData(SmemaIO) = enmMotionStatus.Finish) Then
                    LoaderTask = enmPlcTask.SetLastProductNum
                End If

            Case enmPlcTask.SetLastProductNum
                If (Unloader.IsOpen) Then
                    If (SetLastProductNum(Loader.Data.LastProductNumA, Loader.Data.LastProductNumB)) Then
                        status = enmMotionStatus.Finish
                        _isLoaderBusy = False
                    End If
                Else
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.GetCaseteBarCode
                _isLoaderBusy = True
                status = Loader.GetCaseteBarCode()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.SetCaseteBarCode
                _isLoaderBusy = True
                status = Loader.SetCaseteBarCode(BarCodeValue)
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.GetProductNum
                _isLoaderBusy = True
                status = Loader.GetProductNum()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.CassetteAbort
                _isLoaderBusy = True
                status = Loader.CaseteAbort()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.GetAlarmCode
                _isLoaderBusy = True
                status = Loader.GetAlarmCode()
                If (status = enmMotionStatus.Finish) Then
                    _isLoaderBusy = False
                End If

            Case enmPlcTask.Null
                Loader.Reset()
                _isLoaderBusy = False
                Sw.Start()
        End Select

        Select Case status
            Case enmMotionStatus.Finish
                LoaderTask = enmPlcTask.AlwaysRun
                PreLoader = enmPlcTask.AlwaysRun
            Case enmMotionStatus.Stop

            Case enmMotionStatus.Alarm
                Loader.Close()
                LoaderTask = enmPlcTask.AlwaysRun
                Loader.Reset()
                _isLoaderBusy = False
                'Sue0605
                gEqpMsg.AddHistoryAlarm(Loader.AlarmCode, LoaderTask.ToString(), , Loader.TaskStep.ToString() & Loader.AlarmMessage, eMessageLevel.Alarm)
        End Select

    End Sub

    Dim PreUnloader As enmPlcTask = enmPlcTask.AlwaysRun
    Public Sub BackgroundUnloader()
        Dim status As enmMotionStatus = enmMotionStatus.Null
        '變更任務時重製動作
        If (PreUnloader = enmPlcTask.AlwaysRun AndAlso UnloaderTask <> enmPlcTask.AlwaysRun) Then
            PreUnloader = UnloaderTask
            Unloader.Reset()
        End If

        Select Case UnloaderTask
            Case enmPlcTask.AlwaysRun
                Select Case UnloaderAlwaysRunTask
                    Case enmPlcTask.GetMachineStatus
                        status = Unloader.GetMachineStatus()
                        If (status = enmMotionStatus.Finish) Then
                            UnloaderAlwaysRunTask = enmPlcTask.GetTempreature
                        End If
                    Case enmPlcTask.GetTempreature
                        status = Unloader.GetHpTemperatures()
                        If (status = enmMotionStatus.Finish) Then
                            UnloaderAlwaysRunTask = enmPlcTask.GetMachineStatus
                        End If
                End Select

            Case enmPlcTask.GetPassModel
                _isUnloaderBusy = True
                status = Unloader.GetPassModel()
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.SetPassModel
                _isUnloaderBusy = True
                status = Unloader.SetPassModel(Unloader.Data.Pass)
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.GetProductType
                _isUnloaderBusy = True
                status = Unloader.GetProductType()
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.SetProductType
                _isUnloaderBusy = True
                status = Unloader.SetProductType(Unloader.Data.ProductType)
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.GetTargetTemp
                _isUnloaderBusy = True
                status = Unloader.GetTargetTemp()
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.SetTargetTemp
                _isUnloaderBusy = True
                status = Unloader.SetTargetTemp(Unloader.Data.TargetTemp)
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.GetCaseteData
                _isUnloaderBusy = True
                status = Unloader.GetCaseteData()
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.SetProductNum
                _isUnloaderBusy = True
                status = Unloader.SetProductNum(Unloader.Data.ProductNum)
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.SetLastProductNum
                _isUnloaderBusy = True
                status = Unloader.SetLastProductNum(Unloader.Data.LastProductNumA, Unloader.Data.LastProductNumB)
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.CassetteAbort
                _isUnloaderBusy = True
                status = Unloader.CaseteAbort()
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.GetAlarmCode
                _isUnloaderBusy = True
                status = Unloader.GetAlarmCode()
                If (status = enmMotionStatus.Finish) Then
                    _isUnloaderBusy = False
                End If

            Case enmPlcTask.Null
                Unloader.Reset()
                status = enmMotionStatus.Finish

        End Select

        Select Case status
            Case enmMotionStatus.Finish
                UnloaderTask = enmPlcTask.AlwaysRun
                PreUnloader = enmPlcTask.AlwaysRun
            Case enmMotionStatus.Stop

            Case enmMotionStatus.Alarm
                Unloader.Close()
                UnloaderTask = enmPlcTask.AlwaysRun
                Unloader.Reset()
                _isUnloaderBusy = False
                'Sue0605
                gEqpMsg.AddHistoryAlarm(Unloader.AlarmCode, UnloaderTask.ToString(), , Unloader.TaskStep.ToString() & Unloader.AlarmMessage, eMessageLevel.Warning)
        End Select

    End Sub

    ''' <summary>
    ''' 讀取LD/UL設備狀態
    ''' </summary>
    Public Function GetMachineStatus(ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                LoaderTask = enmPlcTask.GetMachineStatus
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                UnloaderTask = enmPlcTask.GetMachineStatus
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定Loader是否開啟Pass模式
    ''' </summary>
    Public Function SetPassModel(ByVal enable As Boolean) As Boolean
        If (_isLoaderBusy = False) Then
            _isLoaderBusy = True
            Loader.Data.Pass = IIf(enable, 1, 0)
            LoaderTask = enmPlcTask.SetPassModel
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定LD/UL產品類型
    ''' </summary>
    Public Function SetProductType(ByVal type As Integer, ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                Loader.Data.ProductType = type
                LoaderTask = enmPlcTask.SetProductType
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                Unloader.Data.ProductType = type
                UnloaderTask = enmPlcTask.SetProductType
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取LD/UL hot plate溫度值
    ''' </summary>
    Public Function GetTempreature(ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                LoaderTask = enmPlcTask.GetTempreature
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                UnloaderTask = enmPlcTask.GetTempreature
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定LD/UL hot plate溫度
    ''' </summary>
    Public Function SetTargetTemp(ByVal temperature As Double, ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                Loader.Data.TargetTemp = temperature
                LoaderTask = enmPlcTask.SetTargetTemp
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                Unloader.Data.TargetTemp = temperature
                UnloaderTask = enmPlcTask.SetTargetTemp
                Return True
            End If
        End If
        Return False
    End Function

    Dim SmemaIO As Boolean
    ''' <summary>
    ''' 讀取LD/UL Casete資料
    ''' </summary>
    Public Function GetCastetData(ByVal device As enmDevice, Optional ByVal smemaIO As Boolean = True) As Boolean
        Me.SmemaIO = smemaIO
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                LoaderTask = enmPlcTask.GetCaseteData
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                UnloaderTask = enmPlcTask.GetCaseteData
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取Loader Cassette BarCode
    ''' </summary>
    Public Function GetCaseteBarCode() As Boolean
        If (_isLoaderBusy = False) Then
            _isLoaderBusy = True
            LoaderTask = enmPlcTask.GetCaseteBarCode
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定Loader Cassette BarCode
    ''' </summary>
    Public Function SetCaseteBarCode(ByVal barCode As String) As Boolean
        If (barCode.Length Mod 2) = 0 Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                BarCodeValue = barCode
                LoaderTask = enmPlcTask.SetCaseteBarCode
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取產品編號
    ''' </summary>
    Public Function GetProductNum(ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                LoaderTask = enmPlcTask.GetProductNum
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                UnloaderTask = enmPlcTask.GetProductNum
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定產品編號
    ''' </summary>
    Public Function SetProductNum(ByVal num As Integer, ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                Loader.Data.ProductNum = num
                LoaderTask = enmPlcTask.SetProductNum
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                Unloader.Data.ProductNum = num
                UnloaderTask = enmPlcTask.SetProductNum
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' LD/UL退Cassette
    ''' </summary>
    Public Function CassetteAbort(ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                LoaderTask = enmPlcTask.CassetteAbort
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                UnloaderTask = enmPlcTask.CassetteAbort
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取LD/UL錯誤代碼
    ''' </summary>
    Public Function GetAlarmCode(ByVal device As enmDevice) As Boolean
        If (device = enmDevice.Loader) Then
            If (_isLoaderBusy = False) Then
                _isLoaderBusy = True
                LoaderTask = enmPlcTask.GetAlarmCode
                Return True
            End If
        ElseIf (device = enmDevice.Unloader) Then
            If (_isUnloaderBusy = False) Then
                _isUnloaderBusy = True
                UnloaderTask = enmPlcTask.GetAlarmCode
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Cassette A/B 末片編號
    ''' </summary>
    ''' <param name="CANum">Cassette A 末片編號</param>
    ''' <param name="CBNum">Cassette B 末片編號</param>
    Public Function SetLastProductNum(ByVal CANum As Integer, ByVal CBNum As Integer) As Boolean
        If (_isUnloaderBusy = False) Then
            _isUnloaderBusy = True
            Unloader.Data.LastProductNumA = CANum
            Unloader.Data.LastProductNumB = CBNum
            UnloaderTask = enmPlcTask.SetLastProductNum
            Return True
        End If
        Return False
    End Function

    Public Sub Reset(ByVal device As enmDevice)
        If (device = enmDevice.Loader) Then
            LoaderTask = enmPlcTask.Null
        ElseIf (device = enmDevice.Unloader) Then
            UnloaderTask = enmPlcTask.Null
        End If
    End Sub

#End Region

End Class

''' <summary>
''' 800AQ Loader/Unloader data
''' </summary>
Public Class clsULoaderData
    ''' <summary>
    ''' 加熱版溫度
    ''' </summary>
    Public Temperatures(11) As Integer

    ''' <summary>
    ''' 0:Stop、1:Run、2:Alarm、3:Idle
    ''' </summary>
    Public Status As Integer

    ''' <summary>
    ''' 機台錯誤代碼
    ''' </summary>
    Public AlarmCodes(4) As String

    ''' <summary>
    ''' 產品編號
    ''' </summary>
    Public ProductNum As Integer

    ''' <summary>
    ''' 產品種類
    ''' </summary>
    Public ProductType As Integer

    ''' <summary>
    ''' Cassette Bar-Code
    ''' </summary>
    Public CaseteBarCode As String

    ''' <summary>
    ''' Recipe 是否變更完畢
    ''' </summary>
    Public IsRecipeChangeFinish As Boolean

    ''' <summary>
    ''' Pass模式
    ''' </summary>
    Public Pass As Integer

    ''' <summary>
    ''' 加熱溫度
    ''' </summary>
    Public TargetTemp As Integer

    ''' <summary>
    ''' Cassette A 產品數目
    ''' </summary>
    Public ProductCountA As Integer

    ''' <summary>
    ''' Cassette A Slot 狀態
    ''' </summary>
    Public SlotStatusA(19) As Integer

    ''' <summary>
    ''' Cassette A 末片編號
    ''' </summary>
    Public LastProductNumA As Integer

    ''' <summary>
    ''' Cassette B 產品數目
    ''' </summary>
    Public ProductCountB As Integer

    ''' <summary>
    ''' Cassette B Slot 狀態
    ''' </summary>
    Public SlotStatusB(19) As Integer

    ''' <summary>
    ''' Cassette B 末片編號
    ''' </summary>
    Public LastProductNumB As Integer

End Class

Public Class clsPortData
    Public PortName As String = "COM1"
    Public BuadRade As Integer = 9600
    Public Parity As System.IO.Ports.Parity = IO.Ports.Parity.Even
    Public DataBits As Integer = 8
    Public StopBits As System.IO.Ports.StopBits = IO.Ports.StopBits.One
End Class
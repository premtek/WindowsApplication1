Imports System.IO.Ports
Imports System.Text
Imports ProjectCore

''' <summary>微量天平</summary>
''' <remarks></remarks>
Public Class CBalance
    Implements IBalance

    ''' <summary>通訊序列埠</summary>
    ''' <remarks></remarks>
    Private WithEvents mSerialPort As New SerialPort
    ''' <summary>資料接收事件</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event OnDataRecieved(sender As Object, ByVal e As DataEventArgs) Implements IBalance.OnDataRecieved

    Private mRecievedData As StringBuilder
    ''' <summary>
    ''' 逾時計時器
    ''' </summary>
    ''' <remarks></remarks>
    Private mTimeOutStopWatch As Stopwatch
    ''' <summary>
    ''' 錯誤訊息
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrMsg As String Implements IBalance.ErrMsg

    ''' <summary>
    ''' 忙碌中
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsBusy As Boolean
    Public ReadOnly Property IsBusy As Boolean Implements IBalance.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    ''' <summary>
    ''' TimeOut(逾時)
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsTimeOut As Boolean
    Public ReadOnly Property IsTimeOut As Boolean Implements IBalance.IsTimeOut
        Get
            Try
                If (mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mSerialPort.DiscardInBuffer()
                    mTimeOutStopWatch.Stop()
                    mRecievedData.Length = 0
                End If
                Return mIsTimeOut

            Catch ex As Exception
                ErrMsg = ex.ToString()
                If (mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mTimeOutStopWatch.Stop()
                End If
                Return mIsTimeOut
            End Try
        End Get
    End Property

    ''' <summary>
    ''' 設定Timeout時間
    ''' </summary>
    ''' <remarks></remarks>
    Private mTimeoutTimer As Integer
    Public Property TimeoutTimer As Integer Implements IBalance.TimeoutTimer
        Get
            Return mTimeoutTimer
        End Get
        Set(value As Integer)
            mTimeoutTimer = value
        End Set
    End Property

    ''' <summary>
    ''' Port Is Open
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PortIsOpen As Boolean Implements IBalance.PortIsOpen
        Get
            Return mSerialPort.IsOpen
        End Get
    End Property

    ''' <summary>是否初始化成功</summary>
    ''' <remarks></remarks>
    Dim mIsInitialOK As Boolean
    ''' <summary>是否初始化成功</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsInitialOK As Boolean Implements IBalance.IsInitialOK
        Get
            Return mIsInitialOK
        End Get
    End Property
    Public Sub New()
        mTimeOutStopWatch = New Stopwatch
        mRecievedData = New StringBuilder

       
    End Sub

    ''' <summary>
    ''' ComPort Initial
    ''' </summary>
    ''' <param name="PortName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal PortName As String, ByVal baudRate As String, ByVal TimeoutTimer As Double) As Boolean Implements IBalance.Initial

        Dim IsPortExist As Boolean  '[確認Com Port 是否存在]
        Try
            With mSerialPort
                .PortName = PortName                                '[連線方式]
                .BaudRate = Val(baudRate)                           '[每秒傳輸位元]
                .Parity = Parity.Even                               '[同位檢查]
                .DataBits = 7                                       '[資料位元]
                .StopBits = StopBits.One                            '[停止位元]
                .Handshake = IO.Ports.Handshake.None                '[流量控制]
                .Encoding = Encoding.ASCII                        '[資料的編碼格式]
                '.RtsEnable = True
                .ReceivedBytesThreshold = 1
                .NewLine = vbCrLf '"\r\n" 
            End With
            mTimeoutTimer = TimeoutTimer

            mIsBusy = False
            mIsTimeOut = False

            IsPortExist = False
            For Each GetPortName As String In SerialPort.GetPortNames()
                If mSerialPort.PortName = GetPortName Then
                    IsPortExist = True
                    Exit For
                End If
            Next

            If mSerialPort.IsOpen = True Then
                mSerialPort.Close()
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2016000), "Alarm_2016000", eMessageLevel.Alarm)
                gSyslog.Save("COM Port:" & mSerialPort.PortName & " BaudRate: " & mSerialPort.BaudRate, , eMessageLevel.Alarm)
                mIsInitialOK = False
                Return False
            End If

            If IsPortExist = True Then
                mSerialPort.Open()
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015000), "INFO_6015000")
                gSyslog.Save("COM Port:" & mSerialPort.PortName & " BaudRate: " & mSerialPort.BaudRate)
                mIsInitialOK = True
                Return True
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
            gSyslog.Save("COM Port:" & mSerialPort.PortName, , eMessageLevel.Error)
            mIsInitialOK = False
            Return False


        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            '[說明:少加這樣項，造成若一開始就沒這個Port，卻還不跳TimeOut
            mIsTimeOut = True
            mIsInitialOK = False
            Return False
        End Try
    End Function
    ''' <summary>
    ''' 關閉ComPort
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements IBalance.Close

        Try
            If mSerialPort.IsOpen = True Then
                mSerialPort.Close()
            End If
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mSerialPort = Nothing
        End Try

    End Sub

    Dim mResult As sReceiveStatus
    ''' <summary>天平讀值</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result As sReceiveStatus Implements IBalance.Result
        Get
            Return mResult
        End Get
    End Property


    ''' <summary>要求讀取穩定值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestStableValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestStableValue
        Return SendCommandToSerialPort("S", value, waitReturn)
    End Function
    ''' <summary>要求讀取現在值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestCurrentValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestCurrentValue
        Return SendCommandToSerialPort("Q", value, waitReturn)
    End Function
    ''' <summary>歸零</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Rezero(Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.Rezero
        Return SendCommandToSerialPort("R", 0, waitReturn)
    End Function
    ''' <summary>重啟</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestReStart(Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestReStart
        Return True
    End Function
    ''' <summary>
    ''' 發送命令
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommandToSerialPort(ByVal strCmd As String, ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.SendCommandToSerialPort
        Try

            If Not mSerialPort.IsOpen Then
                mResult.Status = False
                mResult.STR = "Port Not Open"
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2016100), "Alarm_2016100", eMessageLevel.Alarm)
                gSyslog.Save("COM Port: " & mSerialPort.PortName, , eMessageLevel.Alarm)
                Return False
            End If
            mIsBusy = True
            mIsTimeOut = False
            mResult.Status = False
            mTimeOutStopWatch.Restart()
            mSerialPort.WriteLine(strCmd)

            If waitReturn = False Then
                Return True
            Else
                Do
                    System.Threading.Thread.CurrentThread.Join(1)
                    If mResult.Status = True Then
                        Exit Do
                    ElseIf IsTimeOut Then
                        mResult.Status = False
                        Return False
                    End If
                Loop
                mTimeOutStopWatch.Stop()
                value = mResult.Value
            End If

            Return True


        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015003), "Error_1015003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Data Received
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub mSerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived

        'Dim SerialTemp As SerialPort

        Try
            '[說明]:判斷接收資料是否為字元
            If e.EventType = SerialData.Chars Then

                mResult.STR = mSerialPort.ReadLine()
                mResult.Value = Val(mResult.STR.Substring(3).TrimEnd("g").TrimEnd("m").Trim())
                RaiseEvent OnDataRecieved(Me, New DataEventArgs(mResult.Value))
                mIsBusy = False
                'Debug.Print("天平讀值:" & StrReadString)
                'InvokeLabel(frmManual.lblWeight, StrReadString.Substring(3))
            End If
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mTimeOutStopWatch.Stop()
        End Try
    End Sub

    ''' <summary>清除結果</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Reset() As Boolean Implements IBalance.Reset
        mResult.Value = 0
        mResult.Status = True
        mResult.STR = ""
        Return True
    End Function
End Class

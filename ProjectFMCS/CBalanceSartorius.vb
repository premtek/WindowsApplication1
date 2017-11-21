Imports System.IO.Ports
Imports System.Text
Imports ProjectCore

Public Class CBalanceSartorius
    Implements IBalance
    Private WithEvents mSerialPort As New SerialPort
    ''' <summary>
    ''' TimeOut(逾時)
    ''' </summary>
    ''' <remarks></remarks>
    Public mIsTimeOut As Boolean
    ''' <summary>
    ''' 設定Timeout時間
    ''' </summary>
    ''' <remarks></remarks>
    Private mTimeoutTimer As Integer = 1000
    ''' <summary>
    ''' 忙碌中
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsBusy As Boolean
    ''' <summary>是否初始化成功</summary>
    ''' <remarks></remarks>
    Dim mIsInitialOK As Boolean
    ''' <summary>
    ''' Stopwatch
    ''' </summary>
    ''' <remarks></remarks>
    Private mTimeOutStopWatch As Stopwatch
    Private mRecievedData As StringBuilder

    Public Sub New()
        mTimeOutStopWatch = New Stopwatch
        mRecievedData = New StringBuilder
    End Sub

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

    Public Property ErrMsg As String Implements IBalance.ErrMsg

    Public Function Initial(PortName As String, baudRate As String, TimeoutTimer As Double) As Boolean Implements IBalance.Initial
        Dim IsPortExist As Boolean  '[確認Com Port 是否存在]
        Try
            With mSerialPort
                .PortName = PortName                                '[連線方式]
                .BaudRate = Val(baudRate)                           '[每秒傳輸位元]
                .Parity = Parity.Odd                               '[同位檢查]
                .DataBits = 8                                      '[資料位元]
                .StopBits = StopBits.One                            '[停止位元]
                ' .Handshake = IO.Ports.Handshake.None                '[流量控制]
                .Handshake = IO.Ports.Handshake.None               '[流量控制]
                .Encoding = Encoding.ASCII                        '[資料的編碼格式]
                '.RtsEnable = True
                .ReceivedBytesThreshold = 1
                .NewLine = vbCrLf '"\r\n" 
            End With
            mTimeoutTimer = TimeoutTimer

            mIsBusy = False
            Me.mIsTimeOut = False
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
            Me.mIsTimeOut = True
            mIsInitialOK = False
            Return False
        End Try
    End Function

    Public ReadOnly Property IsBusy As Boolean Implements IBalance.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    Public ReadOnly Property IsInitialOK As Boolean Implements IBalance.IsInitialOK
        Get
            Return mIsInitialOK
        End Get
    End Property

    Public ReadOnly Property IsTimeOut As Boolean Implements IBalance.IsTimeOut
        Get
            Try
                If (mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    Me.mIsTimeOut = True
                    mSerialPort.DiscardInBuffer()
                    mTimeOutStopWatch.Stop()
                    mRecievedData.Length = 0
                End If
                Return mIsTimeOut

            Catch ex As Exception
                ErrMsg = ex.ToString()
                If (mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    Me.mIsTimeOut = True
                    mTimeOutStopWatch.Stop()
                End If
                Return mIsTimeOut
            End Try
        End Get
    End Property

    Public Event OnDataRecieved(sender As Object, ByVal e As DataEventArgs) Implements IBalance.OnDataRecieved

    Public ReadOnly Property PortIsOpen As Boolean Implements IBalance.PortIsOpen
        Get
            Return mSerialPort.IsOpen
        End Get
    End Property
    ''' <summary>要求讀取現在值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestCurrentValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestCurrentValue
        Return SendCommandToSerialPort("P", value, waitReturn)

        ' Return SendCommandToSerialPort("M")
    End Function
    ''' <summary>要求讀取穩定值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestStableValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestStableValue
        Return SendCommandToSerialPort("P", value, waitReturn)
        'Return SendCommandToSerialPort("K")
    End Function
    ''' <summary>歸零</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Rezero(Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.Rezero
        'Return SendCommandToSerialPort("V")
        Return SendCommandToSerialPort("T", 0, waitReturn)
    End Function
    ''' <summary>重啟</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestReStart(Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestReStart
        Return SendCommandToSerialPort("S", 0, waitReturn)
    End Function


    Public Function SendCommandToSerialPort(strCmd As String, ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.SendCommandToSerialPort
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
            Me.mIsTimeOut = True
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


        Try
            '[說明]:判斷接收資料是否為字元
            If e.EventType = SerialData.Chars Then


                mResult.STR = mSerialPort.ReadLine()
                mResult.Value = Val(mResult.STR.Substring(3).TrimEnd("g").TrimEnd("m").Trim())
                mResult.Status = True
                mIsBusy = False
                RaiseEvent OnDataRecieved(Me, New DataEventArgs(mResult.Value))
                '  Debug.Print("Balance: " & mResult.Value)
            End If
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            Me.mIsTimeOut = True
            mTimeOutStopWatch.Stop()
        End Try
    End Sub

    Public Property TimeoutTimer As Integer Implements IBalance.TimeoutTimer
        Get
            Return mTimeoutTimer
        End Get
        Set(value As Integer)
            mTimeoutTimer = value
        End Set
    End Property

    Dim mResult As sReceiveStatus
    Public ReadOnly Property Result As sReceiveStatus Implements IBalance.Result
        Get
            Return mResult
        End Get
    End Property
    Public Function Reset() As Boolean Implements IBalance.Reset
        mResult.Value = 0
        mResult.Status = True
        mResult.STR = ""
        Return True
    End Function
End Class

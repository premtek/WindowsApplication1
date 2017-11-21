Imports ProjectCore
Imports System.IO.Ports
Imports System.Text

''' <summary>使用Keyencec原廠RS232通訊模組</summary>
''' <remarks></remarks>
Public Class CLaserReader_DLRS1A
    Implements ILaserReader

    ''' <summary>通訊序列埠</summary>
    ''' <remarks></remarks>
    Private WithEvents mSerialPort As New SerialPort

    ''' <summary>逾時計時器</summary>
    ''' <remarks></remarks>
    Private mTimeOutStopWatch As New Stopwatch

    ''' <summary>[傳送指令] </summary>
    ''' <remarks></remarks>
    Private SendCmd() As Byte

    ''' <summary>接收資料結果</summary>
    ''' <remarks></remarks>
    Dim mResult(3) As sReceiveStatus
    ''' <summary>接收資料結果</summary>
    ''' <param name="channelNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result(ByVal channelNo As Integer) As sReceiveStatus Implements ILaserReader.Result
        Get
            Return mResult(channelNo)
        End Get
    End Property

    ''' <summary>[忙碌中]</summary>
    ''' <remarks></remarks>
    Dim mIsBusy As Boolean
    ''' <summary>忙碌中</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy As Boolean Implements ILaserReader.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    ''' <summary>
    ''' TimeOut(逾時)
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsTimeOut As Boolean
    Public ReadOnly Property IsTimeOut As Boolean Implements ILaserReader.IsTimeOut
        Get
            Try
                If (mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mSerialPort.DiscardInBuffer()
                    mTimeOutStopWatch.Stop()
                End If
                Return mIsTimeOut

            Catch ex As Exception
                mResult(0).STR = ex.ToString()
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
    Private mTimeoutTimer As Integer = 4000
    Public Property TimeoutTimer As Integer Implements ILaserReader.TimeoutTimer
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
    Public ReadOnly Property PortIsOpen As Boolean Implements ILaserReader.PortIsOpen
        Get
            Return mSerialPort.IsOpen
        End Get
    End Property

    ''' <summary>更換Recipe</summary>
    ''' <param name="ProgramID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeProgram(ProgramID As Integer) As Boolean Implements ILaserReader.ChangeProgram
        Debug.Print("")
        Return True
    End Function

    ''' <summary>關閉連線</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean Implements ILaserReader.Close
        Return True
    End Function



    ''' <summary>
    ''' ComPort Initial
    ''' </summary>
    ''' <param name="PortName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal PortName As String, ByVal BaudRate As String) As Boolean Implements ILaserReader.Initial

        Dim IsPortExist As Boolean  '[確認Com Port 是否存在]
        Try

            With mSerialPort
                .PortName = PortName          '[連線方式]
                .BaudRate = BaudRate                                '[每秒傳輸位元] 出廠預設值9600
                .Parity = Parity.None                               '[同位檢查] '出廠預設值None
                .DataBits = 8                                       '[資料位元] '出廠預設值8
                .StopBits = StopBits.One                            '[停止位元] '出廠預設值1
                .NewLine = vbCrLf
                .Encoding = System.Text.Encoding.ASCII
                .Handshake = Handshake.None
                '.Handshake = IO.Ports.Handshake.None                '[流量控制]
                '.Encoding = Encoding.ASCII                          '[資料的編碼格式]
                '.RtsEnable = True
                '.ReceivedBytesThreshold = 1
                '.NewLine = "\r\n"
            End With

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
                Return False
            Else
                If IsPortExist = True Then
                    mSerialPort.Open()
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            mResult(0).STR = ex.ToString()
            mIsBusy = False
            '[說明:少加這樣項，造成若一開始就沒這個Port，卻還不跳TimeOut
            mIsTimeOut = True
            Return False
        End Try
    End Function


    Public Function EthernetOpen(IP As String, Port As Integer) As Boolean Implements ILaserReader.EthernetOpen
        gSyslog.Save("EthernetOpen Function is Not Supported at CLaserReader_DLRS1A.")
        Return False
    End Function

    Public Function GetValue(ByVal Mode As String, ByRef value As String, Optional aiIndex As Integer = 0, Optional ByVal waitReturn As Boolean = False) As Boolean Implements ILaserReader.GetValue

        Dim cmd As String

        If Mode.Equals("Contact") Then
            cmd = "SR,00,002" & vbCrLf
        Else
            cmd = "SR,00,037" & vbCrLf
        End If

        'Dim cmd As String = "SR,00,002" & vbCrLf
        Dim bytCmd(cmd.Length) As Byte
        Dim ret As Boolean
        ConverterValue(cmd, bytCmd)
        ret = SendCommandToSerialPort(bytCmd, waitReturn)
        If waitReturn Then
            If mResult(0).Status Then '取得結果
                value = mResult(0).Value
                Dim mDec As Decimal

                If Decimal.TryParse(value, mDec) Then
                    value = Math.Round(mDec, 3)
                End If

            End If
        End If
        Return ret
    End Function
    Private Function ConverterValue(ByVal data As String, ByRef bytCmd() As Byte) As Boolean
        Dim chr() As Char = data.ToArray()
        ReDim bytCmd(data.Count - 1)
        For i As Integer = 0 To data.Count - 1
            bytCmd(i) = Asc(chr(i))
            'Debug.Print("bytCmd(" & i & ")" & bytCmd(i))
        Next

        Return True
    End Function

    Public Function GetVersion(ByRef Version As String) As Boolean Implements ILaserReader.GetVersion
        Version = "DL-RS1A"
        Return True
    End Function

    Public Function RebootController() As Boolean Implements ILaserReader.RebootController
        Return True
    End Function

    ''' <summary>
    ''' Send Command
    ''' </summary>
    ''' <param name="CommandBtye"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SendCommandToSerialPort(ByVal CommandBtye() As Byte, Optional ByVal waitReturn As Boolean = False) As Boolean
        Try
            SendCmd = CommandBtye
            If Not mSerialPort.IsOpen Then
                Return False
            End If

            mIsBusy = True
            mIsTimeOut = False

            mTimeOutStopWatch.Restart()
            mResult(0).Status = False
            mSerialPort.Write(SendCmd, 0, SendCmd.Length)
            'Debug.Print("TriggerBoard Cmd:" & Chr(CommandBtye(0)))
            If waitReturn = False Then
                Return True
            End If

            Do
                System.Threading.Thread.CurrentThread.Join(1)
                If mResult(0).Status = True Then
                    Exit Do
                ElseIf IsTimeOut Then
                    mResult(0).Status = False
                    Return False
                End If
            Loop
            mTimeOutStopWatch.Stop()
            Return True


        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1016003), "Error_1016003", eMessageLevel.Error)
            gSyslog.Save("Exception Message" & ex.Message, , eMessageLevel.Error)
            mResult(0).Status = False
            mResult(0).STR = ex.ToString()
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
        'Dim intReadByte As Integer
        'Dim intTemp As Integer

        'Dim intI As Integer

        'Dim DataBuffer(1023) As Byte
        'Dim length As Integer

        Dim cmd As String
        Dim IdNo As String
        Dim DataNo As String
        Dim Data As String

        Try
            mResult(0).STR = mSerialPort.ReadLine()

            Dim SplitedData() As String = mResult(0).STR.Split(",")
            If SplitedData.Length >= 1 Then
                cmd = SplitedData(0)
            Else
                cmd = ""
            End If

            If SplitedData.Length >= 2 Then
                IdNo = SplitedData(1) '同一個通訊埠有多個元件
            Else
                IdNo = ""
            End If

            If SplitedData.Length >= 3 Then
                DataNo = SplitedData(2) '所屬資料編號
            Else
                DataNo = ""
            End If

            If SplitedData.Length >= 4 Then
                Data = SplitedData(3) '實際資料內容
            Else
                Data = ""
            End If

            If cmd = "SR" Then '讀取
                If IdNo = "00" Then '主站
                    If DataNo = "037" Or "002" Then '實際值
                        mResult(0).Value = Data
                        mResult(0).Status = True
                        mTimeOutStopWatch.Stop()
                        mIsBusy = False
                    End If
                End If
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1016004), "Error_1016004", eMessageLevel.Error)
            gSyslog.Save("Exception Message" & ex.Message, , eMessageLevel.Error)
            mResult(0).Status = False
            mResult(0).STR = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mTimeOutStopWatch.Stop()
        End Try
    End Sub


End Class

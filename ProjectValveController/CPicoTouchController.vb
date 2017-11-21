Imports System.Text
Imports System.IO.Ports

''' <summary>
''' Picocontroler
''' </summary>
''' <remarks></remarks>
Public Class CPicoTouchcontroller
    Implements IValveControllerInterface
    Dim lstReceiveData As New List(Of Byte)
    ''' <summary>
    ''' 通訊計時器
    ''' </summary>
    ''' <remarks></remarks>
    Private mStopWatch As Stopwatch
    ''' <summary>
    ''' 通訊計時,WaitCommand回應時才做用 
    ''' </summary>
    ''' <remarks></remarks>
    Private mStartRecive As Stopwatch
    Private WithEvents mSerialPort As SerialPort
    ''' <summary>
    ''' 目前的命今Type
    ''' </summary>
    ''' <remarks></remarks>
    Private eCommandType As enmValveCommandType

    ''' <summary>[傳送指令] </summary>
    ''' <remarks></remarks>
    Private SendCmd() As Byte

    ''' <summary>狀態機</summary>
    ''' <remarks>Picocontroler無使用</remarks>
    Dim mStateMachine As enmCommandState

    ''' <summary>
    ''' 設定Timeout時間
    ''' </summary>
    ''' <remarks></remarks>
    Private mTimeoutTimer As Integer = 6000
    ''' <summary>
    ''' 設定Timouer時間
    ''' </summary>
    ''' <remarks>WaitCommand回應時才作用 </remarks>
    Private mTimeoutTimerNoRespone As Integer = 6000
    ''' <summary>
    ''' 存取mTimeoutTimerNoRespone
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>WaitCommand回應時才作用</remarks>
    Public Property TimeoutTimerRespone As Integer
        Get
            Return mTimeoutTimerNoRespone
        End Get
        Set(value As Integer)
            mTimeoutTimerNoRespone = value
        End Set
    End Property

    Private mIsTimeOutResponse As Boolean
    ''' <summary>
    ''' 是否TimeOut
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>WaitCommand回應時才作用 </remarks>
    Public ReadOnly Property IsTimeOutResponse() As Boolean
        Get
            Try
                If (mStartRecive.ElapsedMilliseconds >= mTimeoutTimerNoRespone) Then
                    mIsTimeOutResponse = True
                    mStartRecive.Stop()
                End If
                Return mIsTimeOutResponse
            Catch ex As Exception
                ErrMsg = ex.ToString()
                If (mStartRecive.ElapsedMilliseconds >= mTimeoutTimerNoRespone) Then
                    mIsTimeOutResponse = True
                    mStartRecive.Stop()
                End If
                Return mIsTimeOutResponse
            End Try
        End Get
    End Property

    'Private mIsNoResponse As Boolean
    'Public Property IsNoResponse() As Boolean
    '    Get
    '        Return mIsNoResponse
    '    End Get
    '    Set(ByVal value As Boolean)
    '        mIsNoResponse = value
    '    End Set
    'End Property

    Public Property TimeoutTimer As Integer Implements IValveControllerInterface.TimeoutTimer
        Get
            Return mTimeoutTimer
        End Get
        Set(value As Integer)
            mTimeoutTimer = value
        End Set
    End Property

    ''' <summary>
    ''' 通訊忙錄中
    ''' </summary>
    ''' <remarks></remarks>
    Private mIsBusy As Boolean
    ''' <summary>
    ''' 判斷是否忙錄
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy As Boolean Implements IValveControllerInterface.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    ''' <summary>
    ''' TimeOut(逾時)
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsTimeOut As Boolean
    ''' <summary>
    ''' 是否逾時
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut As Boolean Implements IValveControllerInterface.IsTimeOut
        Get
            Try
                If (mStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    lstReceiveData.Clear()
                    mIsBusy = False
                    mIsTimeOut = True
                    mSerialPort.DiscardInBuffer()
                    mStopWatch.Stop()

                End If
                Return mIsTimeOut

            Catch ex As Exception
                ErrMsg = ex.ToString()
                If (mStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mStopWatch.Stop()
                End If
                Return mIsTimeOut
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Port Open
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PortIsOpen As Boolean Implements IValveControllerInterface.PortIsOpen
        Get
            Return mSerialPort.IsOpen
        End Get
    End Property

    ''' <summary>
    ''' Port Close
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements IValveControllerInterface.Close
        Try
            If mSerialPort.IsOpen = True Then
                mSerialPort.Close()
            End If

        Catch ex As Exception
            ErrMsg = ex.ToString()
            mSerialPort = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IValveControllerInterface.Dispose

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrMsg As String Implements IValveControllerInterface.ErrMsg

    Private Delegate Sub ImInvoketTextBox(ByVal ctrl As Windows.Forms.TextBox, ByVal [str] As String)
    ''' <summary>
    ''' 回呼訊息用
    ''' </summary>
    ''' <param name="ctrl">textbox 控制項</param>
    ''' <param name="str">回呼訊息</param>
    ''' <remarks></remarks>
    Public Sub InvokeTextBox(ByVal ctrl As Windows.Forms.TextBox, ByVal [str] As String)
        If ctrl.InvokeRequired Then
            Dim t As New ImInvoketTextBox(AddressOf InvokeTextBox)
            ctrl.Invoke(t, New Object() {ctrl, str})
        Else
            If str = "" Then
                ctrl.Text = ""
            Else
                ctrl.Text = str
            End If
        End If
    End Sub
    ''' <summary>
    ''' 顯示Command回應
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCmdResponse As TextBox
    ''' <summary>
    ''' 設定元件COMMAND後回應值
    ''' </summary>
    ''' <remarks></remarks>
    Public Property _mCommandResponse() As Windows.Forms.TextBox Implements IValveControllerInterface.mCommandResponse
        Get
            Return mCmdResponse
        End Get
        Set(ByVal value As Windows.Forms.TextBox)
            mCmdResponse = value
        End Set
    End Property



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Try
            mStateMachine = enmCommandState.WaitCommand
            mSerialPort = New SerialPort
            mStopWatch = New Stopwatch
            mStartRecive = New Stopwatch

            'mResponseData = New sPicoValveCommandResponseData
        Catch ex As Exception
            ErrMsg = ex.ToString()
            mSerialPort = Nothing
            mStopWatch = Nothing

        End Try
    End Sub

    ''' <summary>
    ''' GetPortID
    ''' </summary>
    ''' <param name="PortIDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortIDs(ByRef PortIDs() As String) As Boolean Implements IValveControllerInterface.GetPortIDs
        Return True
    End Function

    ''' <summary>
    ''' Initial
    ''' </summary>
    ''' <param name="PortName">COM</param>
    ''' <param name="BaudRate"></param>
    ''' <returns>true :開port成功 Fasle:開port失敗</returns>
    ''' <remarks>PicoCotroler固定115200</remarks>
    Public Function Initial(ByVal PortName As String, ByVal BaudRate As String) As Boolean Implements IValveControllerInterface.Initial
        Dim IsPortExist As Boolean  '[確認Com Port 是否存在]
        Try
            '            Communication Specifications
            'The controler acts as a terminal to the remote host PC. The controler communicates using the following settings:
            '• Synchronous mode: half duplex
            '• Baud rate: 115200
            '• Start bit: 1
            '• Data length: 8 bit (ASCII)
            '• Parity bit: None
            '• Stop bit: 1
            With mSerialPort
                .PortName = PortName                                '[連線方式]
                .BaudRate = 115200                                  '[每秒傳輸位元]
                .Parity = Parity.None                               '[同位檢查] '出廠預設值None
                .DataBits = 8                                       '[資料位元] '出廠預設值8
                .StopBits = StopBits.One                            '[停止位元] '出廠預設值1
                .Handshake = IO.Ports.Handshake.None                '[流量控制]
                .ReceivedBytesThreshold = 1
                .NewLine = vbCrLf '"\r\n" 
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
            ErrMsg = ex.ToString()
            mIsBusy = False
            '[說明:少加這樣項，造成若一開始就沒這個Port，卻還不跳TimeOut
            mIsTimeOut = True
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 送出命令
    ''' </summary>
    ''' <param name="CommandBtye"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommandToSerialPort(CommandBtye As String) As Boolean Implements IValveControllerInterface.SendCommandToSerialPort
        Try
            If mSerialPort.IsOpen = True Then
                mIsBusy = True
                mIsTimeOut = False
                mStopWatch.Restart()
                mSerialPort.WriteLine(CommandBtye)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 資料接收
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub mSerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived
        Dim DataBuffer(1023) As Byte
        Dim receivedValue As Integer
        Try
            If e.EventType = SerialData.Chars Then
                System.Threading.Thread.CurrentThread.Join(20)
                While (mSerialPort.BytesToRead > 0) = True
                    'System.Threading.Thread.CurrentThread.Join(1)
                    receivedValue = mSerialPort.ReadByte()

                    ' Debug.Write("")
                    ' Debug.Write((CType(receivedValue, Byte)) & " ")
                    Select Case receivedValue
                        '結尾為 60 63 
                        Case 63
                            If lstReceiveData.Item(lstReceiveData.Count - 1) = 60 Then
                                lstReceiveData.Add(CType(receivedValue, Byte))
                                parseReceiveData(lstReceiveData)
                            End If
                        Case Else
                            lstReceiveData.Add(CType(receivedValue, Byte))
                    End Select
                End While
            End If
        Catch ex As Exception
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
        End Try
    End Sub

    ''' <summary>
    ''' 訊息分析
    ''' </summary>
    ''' <param name="_lstReceiveData"></param>
    ''' <remarks></remarks>
    Private Sub parseReceiveData(ByVal _lstReceiveData As List(Of Byte))
        Dim sReceiveData As String = ""
        sReceiveData &= String.Format("{0}{1}", Encoding.ASCII.GetString(_lstReceiveData.ToArray()), Environment.NewLine)
        Select Case eCommandType

            Case enmValveCommandType.ValveMode
                mValveMode.Status = True
                mValveMode.STR = sReceiveData
                mValveMode.value = sReceiveData.Trim.Replace("Driver 1:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
            Case enmValveCommandType.ValveDispenseCount
                mValveDispenseCount.Status = True
                mValveDispenseCount.STR = sReceiveData
                'mValveDispenseCount.value = sReceiveData.Replace("Dispense Count (DCNT) = ", "").Trim.Substring(0, 4)
                mValveDispenseCount.value = sReceiveData.Trim.Replace("Dispense Count (DCNT) = ", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
            Case enmValveCommandType.ValveStatus
                'Dim str1() As String = sReceiveData.Split(vbCrLf)
                If sReceiveData.IndexOf("Power:") <> -1 And sReceiveData.IndexOf("MODE :") <> -1 And sReceiveData.IndexOf("PULSE:") <> -1 And
                    sReceiveData.IndexOf("CYCLE:") <> -1 And sReceiveData.IndexOf("COUNT:") <> -1 And sReceiveData.IndexOf("Profile Rise.:") <> -1 And
                    sReceiveData.IndexOf("Profile Fall.") <> -1 And sReceiveData.IndexOf("Stroke.......:") <> -1 And sReceiveData.IndexOf("Up Ramp Time.:") <> -1 And
                    sReceiveData.IndexOf("Dwn Ramp Time:") <> -1 And sReceiveData.IndexOf("Close Voltage:") <> -1 And sReceiveData.IndexOf("Numb Shots") <> -1 Then

                    mValveStatus.sValvePower = sReceiveData.Substring(sReceiveData.IndexOf("Power:") + Len("Power:"), 4) ' str1(1).Replace("Power:", "").Trim
                    mValveStatus.sMode = sReceiveData.Substring(sReceiveData.IndexOf("MODE :") + Len("MODE :"), 5) ' str1(2).Replace("MODE :", "") '.Trim.Substring(0, 4)
                    mValveStatus.sPulse = sReceiveData.Substring(sReceiveData.IndexOf("PULSE:") + Len("PULSE:"), 8) '(3).Replace("PULSE:", "") '.Trim.Substring(0, 9)
                    mValveStatus.sCycle = sReceiveData.Substring(sReceiveData.IndexOf("CYCLE:") + Len("CYCLE:"), 8) 'str1(4).Replace("CYCLE:", "") '.Trim.Substring(0, 9)
                    mValveStatus.sCount = sReceiveData.Substring(sReceiveData.IndexOf("COUNT:") + Len("COUNT:"), 5) ',str1(5).Replace("COUNT:", "") '.Trim.Substring(0, 4)
                    mValveStatus.sProfileRise = sReceiveData.Substring(sReceiveData.IndexOf("Profile Rise.:") + Len("Profile Rise.:"), 2) ' str1(6).Replace("Profile Rise.:", "") '.Trim.Substring(0, 1)
                    mValveStatus.sProfileFall = sReceiveData.Substring(sReceiveData.IndexOf("Profile Fall.") + Len("Profile Fall.:"), 2) 'str1(7).Replace("Profile Fall.:", "") '.Trim.Substring(0, 1)
                    mValveStatus.sStroke = sReceiveData.Substring(sReceiveData.IndexOf("Stroke.......:") + Len("Stroke.......:"), 5) 'str1(8).Replace("Stroke.......:", "") '.Trim.Substring(0, 4)
                    mValveStatus.sUpRampTime = sReceiveData.Substring(sReceiveData.IndexOf("Up Ramp Time.:") + Len("Up Ramp Time.:"), 8) 'str1(9).Replace("Up Ramp Time.:", "") '.Trim.Substring(0, 9)
                    mValveStatus.sDwnRampTime = sReceiveData.Substring(sReceiveData.IndexOf("Dwn Ramp Time:") + Len("Dwn Ramp Time:"), 8) 'str1(10).Replace("Dwn Ramp Time:", "") '.Trim.Substring(0, 9)
                    mValveStatus.sCloseVoltage = sReceiveData.Substring(sReceiveData.IndexOf("Close Voltage:") + Len("Close Voltage:"), 4) 'str1(11).Replace("Close Voltage:", "") '.Trim.Substring(0, 3)
                    mValveStatus.sNumShots = sReceiveData.Substring(sReceiveData.IndexOf("Numb Shots...:") + Len("Numb Shots...:"), 11) 'str1(12).Replace("Num Shots:", "") '.Trim.Substring(0, 10)

                    mValveStatus.Status = True
                Else
                    mValveStatus.Status = False
                End If

                'mValveStatus.sValvePower = sReceiveData.Substring(sReceiveData.IndexOf("Power:") + Len("Power:"), 4) ' str1(1).Replace("Power:", "").Trim
                'mValveStatus.sMode = sReceiveData.Substring(sReceiveData.IndexOf("MODE :") + Len("MODE :"), 5) ' str1(2).Replace("MODE :", "") '.Trim.Substring(0, 4)
                'mValveStatus.sPulse = sReceiveData.Substring(sReceiveData.IndexOf("PULSE:") + Len("PULSE:"), 8) '(3).Replace("PULSE:", "") '.Trim.Substring(0, 9)
                'mValveStatus.sCycle = sReceiveData.Substring(sReceiveData.IndexOf("CYCLE:") + Len("CYCLE:"), 8) 'str1(4).Replace("CYCLE:", "") '.Trim.Substring(0, 9)
                'mValveStatus.sCount = sReceiveData.Substring(sReceiveData.IndexOf("COUNT:") + Len("COUNT:"), 5) ',str1(5).Replace("COUNT:", "") '.Trim.Substring(0, 4)
                'mValveStatus.sProfileRise = sReceiveData.Substring(sReceiveData.IndexOf("Profile Rise.:") + Len("Profile Rise.:"), 2) ' str1(6).Replace("Profile Rise.:", "") '.Trim.Substring(0, 1)
                'mValveStatus.sProfileFall = sReceiveData.Substring(sReceiveData.IndexOf("Profile Fall.") + Len("Profile Fall.:"), 2) 'str1(7).Replace("Profile Fall.:", "") '.Trim.Substring(0, 1)
                'mValveStatus.sStroke = sReceiveData.Substring(sReceiveData.IndexOf("Stroke.......:") + Len("Stroke.......:"), 5) 'str1(8).Replace("Stroke.......:", "") '.Trim.Substring(0, 4)
                'mValveStatus.sUpRampTime = sReceiveData.Substring(sReceiveData.IndexOf("Up Ramp Time.:") + Len("Up Ramp Time.:"), 8) 'str1(9).Replace("Up Ramp Time.:", "") '.Trim.Substring(0, 9)
                'mValveStatus.sDwnRampTime = sReceiveData.Substring(sReceiveData.IndexOf("Dwn Ramp Time:") + Len("Dwn Ramp Time:"), 8) 'str1(10).Replace("Dwn Ramp Time:", "") '.Trim.Substring(0, 9)
                'mValveStatus.sCloseVoltage = sReceiveData.Substring(sReceiveData.IndexOf("Close Voltage:") + Len("Close Voltage:"), 4) 'str1(11).Replace("Close Voltage:", "") '.Trim.Substring(0, 3)
                'mValveStatus.sNumShots = sReceiveData.Substring(sReceiveData.IndexOf("Numb Shots:") + Len("Numb Shots:"), 11) 'str1(12).Replace("Num Shots:", "") '.Trim.Substring(0, 10)

            Case enmValveCommandType.HeaterStatus
                Dim str1() As String = sReceiveData.Split(vbCrLf)
                mHeaterStatus.sMode = str1(1).Replace("MODE =", "").Trim
                mHeaterStatus.sSet = str1(2).Replace("SET =", "").Trim
                mHeaterStatus.sACT = str1(3).Replace("ACT =", "").Trim
                mHeaterStatus.sStack = str1(4).Replace("STACK =", "").Trim
                mHeaterStatus.Status = True

            Case enmValveCommandType.ValveCycleOnOff
                mValveCycleONOFFStatus.STR = sReceiveData
                mValvePower.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mValveCycleONOFFStatus.Status = True
                    mValveCycleONOFFStatus.value = sReceiveData.Trim.Replace("Cycle:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mValveCycleONOFFStatus.Status = False
                End If

            Case enmValveCommandType.ValvePower
                mValvePower.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mValvePower.Status = True
                    mValvePower.value = sReceiveData.Trim().Replace("Valve Driver Power:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mValvePower.Status = False
                End If
            Case enmValveCommandType.ValvePlok
                mValvePlok.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mValvePlok.Status = True
                    mValvePlok.value = sReceiveData.Trim.Replace("Pulse OK Time Adj:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mValvePlok.Status = False
                End If
            Case enmValveCommandType.HeaterMode
                mHeaterMode.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mHeaterMode.Status = True
                    mHeaterMode.value = sReceiveData.Trim.Replace("Heate:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mHeaterMode.Status = False
                End If

            Case enmValveCommandType.HeaterTemperature
                mHeaterTemperature.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mHeaterTemperature.Status = True
                    mHeaterTemperature.value = sReceiveData.Trim.Replace("Set Temperature =", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mHeaterTemperature.Status = False
                End If

            Case enmValveCommandType.CloseProfile
                mCloseProfile.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mCloseProfile.Status = True
                    mCloseProfile.value = sReceiveData.Trim.Replace("Profile:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mCloseProfile.Status = False
                End If

            Case enmValveCommandType.OpenProfile
                mOpenProfile.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mOpenProfile.Status = True
                    mOpenProfile.value = sReceiveData.Trim.Replace("Profile:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mOpenProfile.Status = False
                End If

            Case enmValveCommandType.Stroke
                mStroke.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mStroke.Status = True
                    mStroke.value = sReceiveData.Trim.Replace("Stroke Adjusted:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mStroke.Status = False
                End If
            Case enmValveCommandType.CloseVoltage
                mCloseVoltage.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mCloseVoltage.Status = True
                    mCloseVoltage.value = sReceiveData.Trim.Replace("Voltage Adjust:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mCloseVoltage.Status = False
                End If

            Case enmValveCommandType.OpenTime
                mOpenTime.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mOpenTime.Status = True
                    mOpenTime.value = sReceiveData.Trim.Replace("Profile Time Adj:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mOpenTime.Status = False
                End If

            Case enmValveCommandType.CloseTime
                mCloseTime.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mCloseTime.Status = True
                    mCloseTime.value = sReceiveData.Trim.Replace("Profile Time Adj:", "").Replace(":", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mCloseTime.Status = False
                End If
            Case enmValveCommandType.ValveOnTime
                mValveOnTime.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mValveOnTime.Status = True
                    mValveOnTime.value = sReceiveData.Trim.Replace("Time Set To =", "").Replace("Set To =", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mValveOnTime.Status = False
                End If
            Case enmValveCommandType.ValveOffTime
                mValveOffTime.STR = sReceiveData
                If sReceiveData.IndexOf("Invalid") = -1 Then
                    mValveOffTime.Status = True
                    mValveOffTime.value = sReceiveData.Trim.Replace("Time Set To =", "").Replace("Set To =", "").Replace("<3", "").Replace("?", "").Replace("<", "").Trim
                Else
                    mValveOffTime.Status = False
                End If

            Case enmValveCommandType.DisplayInfo
                mDisplayInfo.Status = True
                mDisplayInfo.STR = sReceiveData
                mDisplayInfo.value = sReceiveData
            Case enmValveCommandType.AlarmInfo
                mAlarmInfo.Status = True
                mAlarmInfo.STR = sReceiveData
                mAlarmInfo.value = sReceiveData
            Case enmValveCommandType.ResetAlarm

        End Select
        lstReceiveData.Clear()
        mIsBusy = False
        If (mCmdResponse Is Nothing) = False Then
            'If eCommandType = enmValveCommandType.ValveStatus Or eCommandType = enmValveCommandType.HeaterStatus Then
            'Else

            InvokeTextBox(_mCommandResponse, sReceiveData)
            'End If
        End If
        _lstReceiveData.Clear()
        mStopWatch.Reset()
    End Sub

#Region "命令Function "
    ''' <summary>
    ''' Resets a currently active alarm
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResetAlarmStatus() As enmCommandState Implements IValveControllerInterface.ResetAlarmStatus
        Try
            eCommandType = enmValveCommandType.ResetAlarm
            Dim sTemp As String = "arst" & vbCrLf
            SendCommandToSerialPort(sTemp)
            Return enmCommandState.Success
            'If bWaitUntilResponse = True Then
            '    If WaitValveStatus(mValveStatus) = True Then
            '        sVavleStatus = mValveStatus
            '        Return enmCommandState.Success
            '    Else
            '        sVavleStatus = mValveStatus
            '        Return enmCommandState.Failed
            '    End If
            'End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Retrieves the last 40 (0–39) alarm conditions that occurred; includes time and alarm name
    ''' </summary>
    ''' <param name="sAlarmInfo"></param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlarmInfo(Optional ByRef sAlarmInfo As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetAlarmInfo
        Try
            mAlarmInfo = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.AlarmInfo
            Dim sTemp As String = "ralr" & vbCrLf
            SendCommandToSerialPort(sTemp)
            Return enmCommandState.Success
            If bWaitUntilResponse = True Then
                If WaitRespnse(mAlarmInfo) = True Then
                    sAlarmInfo = mAlarmInfo.value
                    Return enmCommandState.Success
                Else
                    sAlarmInfo = mAlarmInfo.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 取得Picocontroler顯示資料
    ''' </summary>
    ''' <param name="sDisplayInfo">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDisplayInfo(Optional ByRef sDisplayInfo As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetDisplayInfo
        Try
            mDisplayInfo = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.DisplayInfo
            Dim sTemp As String = "info" & vbCrLf
            SendCommandToSerialPort(sTemp)
            Return enmCommandState.Success
            If bWaitUntilResponse = True Then
                If WaitRespnse(mDisplayInfo) = True Then
                    sDisplayInfo = mDisplayInfo.value
                    Return enmCommandState.Success
                Else
                    sDisplayInfo = mDisplayInfo.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 取得Picocontroler Heater狀態
    ''' </summary>
    ''' <param name="sHeaterStatus">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHeaterStatus(Optional ByRef sHeaterStatus As sPicoValveHeaterStatus = Nothing, Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetHeaterStatus
        Try
            mHeaterStatus = New sPicoValveHeaterStatus
            eCommandType = enmValveCommandType.HeaterStatus
            Dim sTemp As String = "rhtr" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitValveHeaterStatus(mHeaterStatus) = True Then
                    sHeaterStatus = mHeaterStatus
                    Return enmCommandState.Success
                Else
                    sHeaterStatus = mHeaterStatus
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 取得Picocontroler Valve狀態
    ''' </summary>
    ''' <param name="sVavleStatus">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValveStatus(Optional ByRef sVavleStatus As sPicoValveStatus = Nothing, Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetValveStatus
        Try
            mValveStatus = New sPicoValveStatus
            eCommandType = enmValveCommandType.ValveStatus
            Dim sTemp As String = "rdr1" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitValveStatus(mValveStatus) = True Then
                    sVavleStatus = mValveStatus
                    Return enmCommandState.Success
                Else
                    sVavleStatus = mValveStatus
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the close (rise) profile of the valve
    ''' </summary>
    ''' <param name="iSelect">選擇1~6 profile</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCloseProfile(iSelect As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCloseProfile
        Try
            mCloseProfile = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.CloseProfile
            Dim sTemp As String = iSelect.ToString & "rzpr" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mCloseProfile) = True Then
                    sResponseContent = mCloseProfile.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mCloseProfile.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            sResponseContent = ""
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the close voltage of the valve
    ''' </summary>
    ''' <param name="iCloseVoltage">close voltage (XXX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks>電壓最大3位數</remarks>
    Public Function SetCloseVoltage(iCloseVoltage As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCloseVoltage
        Try
            mCloseVoltage = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.CloseVoltage
            Dim sTemp As String = Format(iCloseVoltage, "###000") & "volp" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mCloseProfile) = True Then
                    sResponseContent = mCloseVoltage.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mCloseVoltage.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Cycles the valve (mimics the CYCLE icon on the touchscreen)
    ''' </summary>
    ''' <param name="bCycleOnOff">0:Cycle Off 1:Cycle On</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCycleOnOff(bCycleOnOff As Boolean, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCycleOnOff
        Try
            mValveCycleONOFFStatus = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValveCycleOnOff
            Dim sTemp As String
            If bCycleOnOff = True Then
                sTemp = "1" & "cycl" & vbCrLf
            Else
                sTemp = "0" & "cycl" & vbCrLf
            End If
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValveCycleONOFFStatus) = True Then
                    sResponseContent = mValveCycleONOFFStatus.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValveCycleONOFFStatus.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the open (fall) time of the valve
    ''' </summary>
    ''' <param name="iOpenTime">open time in μs (XXXX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetOpenTime(iOpenTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetOpenTime
        Try
            mOpenTime = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.OpenTime
            Dim sTemp As String = Format(iOpenTime, "####0000") & "opnt" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mOpenTime) = True Then
                    sResponseContent = mOpenTime.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mOpenTime.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve ON time (PULSE)
    ''' </summary>
    ''' <param name="iVavleOnTime">ON time in ms(XXXX,XX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValveOnTime(iVavleOnTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveOnTime
        Try
            mValveOnTime = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValveOnTime
            Dim sTemp As String = Format(iVavleOnTime, "######0000.00") & "ont1" & vbCrLf 'dVavleOnTime .ToString & "ont1" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValveOnTime) = True Then
                    sResponseContent = mValveOnTime.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValveOnTime.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try

    End Function

    ''' <summary>
    ''' Sets the close (rise) time of the valve
    ''' </summary>
    ''' <param name="iCloseTime">close time in μs(XXXX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks>最大四位數 單位us</remarks>
    Public Function SetCloseTime(iCloseTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCloseTime
        Try
            mCloseTime = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.CloseTime
            Dim sTemp As String = Format(iCloseTime, "####0000") & "clst" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mCloseTime) = True Then
                    sResponseContent = mCloseTime.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mCloseTime.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve OFF time(CYCLE)(Where OFF time + ONTime = Cycle)
    ''' </summary>
    ''' <param name="iVavleOffTime">OFF time in ms(XXXX.XX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValveOffTime(iVavleOffTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveOffTime
        Try
            mValveOffTime = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValveOffTime
            Dim sTemp As String = Format(iVavleOffTime, "######0000.00") & "oft1" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValveOffTime) = True Then
                    sResponseContent = mValveOffTime.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValveOffTime.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve Cycle time
    ''' </summary>
    ''' <param name="iCycleTime">Cycle time in ms(XXXX.XX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetValveCycleTime(iCycleTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveCycleTime
        Try
            '[Note]:沒有CycleTime的指令，是用ValveOnTime+ValveOffTime去組合而成的
            mValveOffTime = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValveStatus
            Dim vStatus As sPicoValveStatus = New sPicoValveStatus
            'Cycle Time = Pulse Time + OFF Time 
            GetValveStatus(vStatus, True)
            iCycleTime = iCycleTime - CDbl(vStatus.sPulse.Replace("ms", ""))
            eCommandType = enmValveCommandType.ValveOffTime
            Dim sTemp As String = Format(iCycleTime, "######0000.00") & "oft1" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValveOffTime) = True Then
                    sResponseContent = mValveOffTime.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValveOffTime.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function




    ''' <summary>
    ''' Sets the heater mode
    ''' </summary>
    ''' <param name="iHeatherMode">0: Disables the corresponding channel 1: Enables the corresponding channel 2: Reads back the status (enabled /disabled) of the corresponding channel 3: Sets the heater mode to remote</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetHeaterMode(iHeatherMode As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetHeaterMode
        Try
            mHeaterMode = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.HeaterMode
            Dim sTemp As String = iHeatherMode.ToString & "chtr" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mHeaterMode) = True Then
                    sResponseContent = mHeaterMode.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mHeaterMode.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the heater temperature setpoint
    ''' </summary>
    ''' <param name="dHeaterTemperature">temperature setting in degrees C(XXX.X)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetHeaterTemperature(dHeaterTemperature As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetHeaterTemperature
        Try
            mHeaterTemperature = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.HeaterTemperature
            Dim sTemp As String = Format(dHeaterTemperature, "####000.0") & "stmp" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mHeaterTemperature) = True Then
                    sResponseContent = mHeaterTemperature.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mHeaterTemperature.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the open (fall) profile of the valve
    ''' </summary>
    ''' <param name="iSelect">選擇 1~6</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetOpenProfile(iSelect As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetOpenProfile
        Try
            mOpenProfile = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.OpenProfile
            Dim sTemp As String = iSelect.ToString & "flpr" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mOpenProfile) = True Then
                    sResponseContent = mOpenProfile.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mOpenProfile.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the stroke of the valve
    ''' </summary>
    ''' <param name="dStrokeValve">stroke adjustment in volts(.XXX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks>小數點後3位</remarks>
    Public Function SetStrokeValve(dStrokeValve As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetStrokeValve
        Try
            mStroke = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.Stroke
            dStrokeValve = dStrokeValve * 0.001
            Dim sTemp As String = Format(dStrokeValve, "###.000") & "strk" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mStroke) = True Then
                    sResponseContent = mStroke.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mStroke.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve dispense count (COUNT)
    ''' </summary>
    ''' <param name="iDispenseCount">Dispense Count</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks>位數範圍0001–9999</remarks>
    Public Function SetValveDispenseCount(iDispenseCount As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveDispenseCount
        Try
            mValveDispenseCount = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValveDispenseCount
            Dim sTemp As String = Format(iDispenseCount, "####0000") & "dcn1" & vbCrLf ' iDispenseCount.ToString & "dcn1" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValveDispenseCount) = True Then
                    sResponseContent = mValveDispenseCount.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValveDispenseCount.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve mode(MODE)
    ''' </summary>
    ''' <param name="eModeType">1: Timed 2: Sets MODE to Purge 3: Sets MODE to Continuous 5: Reads the current mode</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValveMode(eModeType As enmValveModeType, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveMode
        Try
            mValveMode = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValveMode
            Dim sTemp As String = eModeType & "drv1" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse Then
                If WaitRespnse(mValveMode) = True Then
                    sResponseContent = mValveMode.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValveMode.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

  
  
    ''' <summary>
    ''' Sets the duration of the PULSE OK TIME I/O pin output
    ''' </summary>
    ''' <param name="iPlokTime">PULSE OK TIME in ms(XXX)</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValvePlok(iPlokTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValvePlok
        Try
            mValvePlok = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValvePlok
            Dim sTemp As String = Format(iPlokTime, "###000") & "plok" & vbCrLf 'iPlokTime.ToString & "plok" & vbCrLf
            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValvePlok) = True Then
                    sResponseContent = mValvePlok.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValvePlok.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve power control
    ''' </summary>
    ''' <param name="bValvePower">0:Off 1:On</param>
    ''' <param name="sResponseContent">回應內容</param>
    ''' <param name="bWaitUntilResponse">是否等待回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValvePower(bValvePower As Boolean, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValvePower
        Try
            mValvePower = New sPicoValveCommandResponseData
            eCommandType = enmValveCommandType.ValvePower
            Dim sTemp As String
            If bValvePower = True Then
                sTemp = "1" & "dpwr" & vbCrLf
            Else
                sTemp = "0" & "dpwr" & vbCrLf
            End If


            SendCommandToSerialPort(sTemp)
            If bWaitUntilResponse = True Then
                If WaitRespnse(mValvePower) = True Then
                    sResponseContent = mValvePower.value
                    Return enmCommandState.Success
                Else
                    sResponseContent = mValvePower.Status
                    Return enmCommandState.Failed
                End If
            Else
                Return enmCommandState.Success
            End If
        Catch ex As Exception
            mStartRecive.Stop()
            ErrMsg = ex.ToString()
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 命令送出後等待回應
    ''' </summary>
    ''' <param name="sData">回應內容</param>
    ''' <returns>true:OK false:timeout</returns>
    ''' <remarks></remarks>
    Private Function WaitRespnse(ByRef sData As sPicoValveCommandResponseData) As Boolean
        mStartRecive.Start()
        'IsNoResponse = False
        Do While (IsBusy = True)
            'System.Threading.Thread.CurrentThread.Join(1)
            If IsTimeOut() = True Then
                Exit Do
            End If
        Loop
        If IsTimeOut = True Then
            If IsTimeOutResponse = True Then
                sData.Status = "NoResponse"
            Else
                sData.Status = "TimeOut"
            End If
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' 命令送出後等待回應
    ''' </summary>
    ''' <param name="sData">回應內容</param>
    ''' <returns>true:OK false:timeout</returns>
    ''' <remarks></remarks>
    Private Function WaitValveStatus(ByRef sData As sPicoValveStatus) As Boolean
        mStartRecive.Start()
        'IsNoResponse = False
        Do While (IsBusy = True)
            System.Threading.Thread.CurrentThread.Join(1)
            If IsTimeOut() = True Then
                Exit Do
            End If
        Loop
        If IsTimeOut = True Then
            If IsTimeOutResponse = True Then
                sData.Status = "NoResponse"
            Else
                sData.Status = "TimeOut"
            End If
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' 命令送出後等待回應
    ''' </summary>
    ''' <param name="sData">回應內容</param>
    ''' <returns>true:OK false:timeout</returns>
    ''' <remarks></remarks>
    Private Function WaitValveHeaterStatus(ByRef sData As sPicoValveHeaterStatus) As Boolean
        mStartRecive.Start()
        'IsNoResponse = False
        Do While (IsBusy = True)
            System.Threading.Thread.CurrentThread.Join(1)
            If IsTimeOut() = True Then
                Exit Do
            End If
        Loop
        If IsTimeOut = True Then
            If IsTimeOutResponse = True Then
                sData.Status = "NoResponse"
            Else
                sData.Status = "TimeOut"
            End If
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "外部讀取屬性"

    Private mAlarmInfo As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,Alarminfo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AlarmInfo As sPicoValveCommandResponseData Implements IValveControllerInterface.AlarmInfo
        Get
            Return mAlarmInfo
        End Get
    End Property

    Private mDisplayInfo As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,DisplayInfo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplayInfo As sPicoValveCommandResponseData Implements IValveControllerInterface.DisplayInfo
        Get
            Return mDisplayInfo
        End Get
    End Property

    Private mOpenProfile As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,OpenProfile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OpenProfile As sPicoValveCommandResponseData Implements IValveControllerInterface.OpenProfile
        Get
            Return mOpenProfile
        End Get
    End Property

    Private mCloseProfile As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,CloseProfile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CloseProfile As sPicoValveCommandResponseData Implements IValveControllerInterface.CloseProfile
        Get
            Return mCloseProfile
        End Get
    End Property

    Private mCloseVoltage As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,CloseVoltage
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CloseVoltage As sPicoValveCommandResponseData Implements IValveControllerInterface.CloseVoltage
        Get
            Return mCloseVoltage
        End Get
    End Property

    Private mOpenTime As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,OpenTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OpenTime As sPicoValveCommandResponseData Implements IValveControllerInterface.OpenTime
        Get
            Return mOpenTime
        End Get
    End Property

    Private mValveOnTime As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValveOnTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValveOnTime As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveOnTime
        Get
            Return mValveOnTime
        End Get
    End Property

    Private mCloseTime As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,CloseTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CloseTime As sPicoValveCommandResponseData Implements IValveControllerInterface.CloseTime
        Get
            Return mCloseTime
        End Get
    End Property

    Private mValveOffTime As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValveOffTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValveOffTime As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveOffTime
        Get
            Return mValveOffTime
        End Get
    End Property

    Private mStroke As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,Stroke
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Stroke As sPicoValveCommandResponseData Implements IValveControllerInterface.Stroke
        Get
            Return mStroke
        End Get
    End Property

    Private mValveDispenseCount As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValveDispenseCount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValveDispenseCount As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveDispenseCount
        Get
            Return mValveDispenseCount
        End Get
    End Property

    Private mValveMode As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValveMode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValveMode As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveMode
        Get
            Return mValveMode
        End Get
    End Property

    Private mHeaterMode As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,HeaterMode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HeaterMode As sPicoValveCommandResponseData Implements IValveControllerInterface.HeaterMode
        Get
            Return mHeaterMode
        End Get
    End Property

    Private mHeaterTemperature As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,HeaterTemperature
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HeaterTemperature As sPicoValveCommandResponseData Implements IValveControllerInterface.HeaterTemperature
        Get
            Return mHeaterTemperature
        End Get
    End Property

    Private mResetAlarm As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ResetAlarm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>此命令儀器不回應</remarks>
    Public ReadOnly Property ResetAlarm As sPicoValveCommandResponseData Implements IValveControllerInterface.ResetAlarm
        Get
            Return mResetAlarm
        End Get
    End Property

    Private mValveCycleONOFFStatus As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValveCycleONOFFStatus
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValveCycleONOFFStatus As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveCycleONOFFStatus
        Get
            Return mValveCycleONOFFStatus
        End Get
    End Property

    Private mValvePlok As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValvePlok
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValvePlok As sPicoValveCommandResponseData Implements IValveControllerInterface.ValvePlok
        Get
            Return mValvePlok
        End Get
    End Property

    Private mValvePower As sPicoValveCommandResponseData
    ''' <summary>
    ''' 取得Command回應值,ValvePower
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValvePower As sPicoValveCommandResponseData Implements IValveControllerInterface.ValvePower
        Get
            Return mValvePower
        End Get
    End Property

    Private mValveStatus As sPicoValveStatus
    ''' <summary>
    ''' 取得Command回應值,ValveStatus
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValveStatus As sPicoValveStatus Implements IValveControllerInterface.ValveStatus
        Get
            Return mValveStatus
        End Get
    End Property
    Private mHeaterStatus As sPicoValveHeaterStatus
    ''' <summary>
    ''' 取得Command回應值,HeaterStatus
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HeaterStatus As sPicoValveHeaterStatus Implements IValveControllerInterface.HeaterStatus
        Get
            Return mHeaterStatus
        End Get
    End Property

#End Region
End Class

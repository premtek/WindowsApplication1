Imports System.IO.Ports
Imports ProjectCore
Imports System.Text

''' <summary>FMCS(Fluid Monitor Control System)</summary>
''' <remarks></remarks>
Public Class CFMCS
    Implements IFMCS

    WithEvents mSerialPort As New SerialPort

    Public Event OnRecievedData(sender As Object, ByVal e As FMCSEventArgs) Implements IFMCS.OnRecievedData
    Private mRecievedData As StringBuilder
    ''' <summary>
    ''' Stopwatch
    ''' </summary>
    ''' <remarks></remarks>
    Private mStopWatch As Stopwatch
    ''' <summary>
    ''' Error Message
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrMsg As String Implements IFMCS.ErrMsg
    ''' <summary>
    ''' FMCSIndex
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FMCSIndex As Integer Implements IFMCS.ValveIndex

    Dim intStartX As Integer
    Dim intStartY As Integer
    Dim intEndX As Integer
    Dim intEndY As Integer

    Public IsDataUpdated As Boolean
    ''' <summary>輸出平均流量</summary>
    ''' <remarks></remarks>
    Public Property OutputAvgFlow As Double Implements IFMCS.OutputAvgFlow
    ''' <summary>輸出體積</summary>
    ''' <remarks></remarks>
    Public Property OutputVolume As Double Implements IFMCS.OutputVolume

    ''' <summary>
    ''' 忙碌中
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsBusy As Boolean
    Public ReadOnly Property IsBusy As Boolean Implements IFMCS.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    ''' <summary>
    ''' TimeOut(逾時)
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsTimeOut As Boolean
    Public ReadOnly Property IsTimeOut As Boolean Implements IFMCS.IsTimeOut
        Get
            Try
                If (mStopWatch.ElapsedMilliseconds >= mTimeoutTimer) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mSerialPort.DiscardInBuffer()
                    mStopWatch.Stop()
                    mRecievedData.Length = 0
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
    ''' 設定Timeout時間
    ''' </summary>
    ''' <remarks></remarks>
    Private mTimeoutTimer As Integer
    Public Property TimeoutTimer As Integer Implements IFMCS.TimeoutTimer
        Get
            Return mTimeoutTimer
        End Get
        Set(value As Integer)
            mTimeoutTimer = value
        End Set
    End Property


    ''' <summary>是否初始化成功</summary>
    ''' <remarks></remarks>
    Dim mIsInitialOK As Boolean
    ''' <summary>是否初始化成功</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsInitialOK As Boolean Implements IFMCS.IsInitialOK
        Get
            Return mIsInitialOK
        End Get
    End Property
    ''' <summary>通訊部是否開啟</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsPortOpen() As Boolean Implements IFMCS.IsPortOpen
        Return mSerialPort.IsOpen
    End Function
    ''' <summary>通訊初始化</summary>
    ''' <param name="portName"></param>
    ''' <param name="baudRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal portName As String, ByVal baudRate As Integer) As Boolean Implements IFMCS.Initial
        Try
            mSerialPort.PortName = portName
            mSerialPort.BaudRate = baudRate '9600
            If mSerialPort.IsOpen Then
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2017000), "Alarm_2017000", eMessageLevel.Alarm) 'FMCS1初始化失敗! 通訊埠被佔用!
                gSyslog.Save("COM Port:" & portName)
                MsgBox(gMsgHandler.GetMessage(Alarm_2017000) & "COM Port:" & portName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'gSyslog.Save("COM Port" & mSerialPort.PortName & ") Is Opened.", , eMessageLevel.Error)
                Return True
            Else
                mSerialPort.Open()
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6017000), "INFO_6017000")
                gSyslog.Save("COM Port: " & mSerialPort.PortName & " BaudRate: " & mSerialPort.BaudRate) 'FMCS1初始化完成!
                Return True
            End If
            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1017000), "Error_1017000", eMessageLevel.Error) 'FMCS1初始化失敗!
            gSyslog.Save("Exception Message:" & ex.Message)
            MsgBox(gMsgHandler.GetMessage(Error_1017000) & "Exception Message:" & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "FMCS")
            Return False
        End Try
    End Function

    ''' <summary>開始記錄</summary>
    ''' <returns></returns>
    ''' <param name="intX">相應X索引</param>
    ''' <param name="intY">相應Y索引</param>
    ''' <remarks></remarks>
    Public Function RecordStart(ByVal intX As Integer, ByVal intY As Integer) As String Implements IFMCS.RecordStart
        Dim cmd As String
        cmd = "S" + vbCr
        If mSerialPort.IsOpen Then
            mSerialPort.Write(cmd)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6017001), "INFO_6017001") 'FMCS1開始記錄.
            gSyslog.Save("(" & intX & "," & intY & ")")
        End If
        intStartX = intX
        intStartY = intY
        'Debug.Print("FMCS Start" + intX.ToString() + "," + intY.ToString())

        IsDataUpdated = False
        Return cmd
    End Function

    ''' <summary>結束紀錄</summary>
    ''' <returns></returns>
    ''' <param name="intX">相應X索引</param>
    ''' <param name="intY">相應Y索引</param>
    ''' <remarks></remarks>
    Public Function RecordEnd(ByVal intX As Integer, ByVal intY As Integer) As String Implements IFMCS.RecordEnd
        Dim cmd As String
        cmd = "T" + vbCr
        If mSerialPort.IsOpen Then
            mSerialPort.Write(cmd)
            mStopWatch.Restart()
            mIsBusy = True
            mIsTimeOut = False
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6017002), "INFO_6017002") 'FMCS1結束記錄.
            gSyslog.Save("(" & intX & "," & intY & ")")
        End If

        intEndX = intX
        intEndY = intY
        If intStartX <> intX Or intStartY <> intY Then '如果開始與結束紀錄不符
            intEndX = -1 '-1表示無法辨識索引 供外部使用 如果開始結束不同,不做修正
            intEndY = -1 '-1表示無法辨識索引
        End If

        Return cmd
    End Function
    Dim outputTemp As String = ""



    Private Sub mSerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived
        Dim concateData As String = mSerialPort.ReadExisting() '串接用資料
        outputTemp += concateData '資料串接
        If outputTemp.EndsWith(vbCr) Then '如果是結尾
            Dim splitedData() As String = outputTemp.Split(",") '將資料轉移到輸出資料
            If splitedData(0) <> "Fnan" Then
                OutputAvgFlow = Convert.ToDouble(splitedData(0).Substring(1)) '輸出平均流量
            Else
                OutputAvgFlow = 0
            End If
            OutputVolume = Convert.ToDouble(splitedData(1).Substring(1)) '輸出體積
            IsDataUpdated = True
            mIsBusy = False
            mIsTimeOut = False
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6017003), "INFO_6017003") 'FMCS1取得資料
            gSyslog.Save("Recieved Data:" & outputTemp)
            Dim data As New FMCSData
            data.avgFlow = OutputAvgFlow
            data.volume = OutputVolume
            data.intX = intEndX
            data.intY = intEndY
            data.dispenserNo = FMCSIndex
            RaiseEvent OnRecievedData(Me, New FMCSEventArgs(data))
            outputTemp = "" '清除串接暫存
        End If
    End Sub
    
    ''' <summary>
    ''' 關閉ComPort
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements IFMCS.Close

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
End Class

Imports ProjectCore

Public Class CElectroPneumaticValve_ITV
    Implements IElectroPneumaticValve



    Public Property ChannelPerCard As Integer = 1 Implements IElectroPneumaticValve.ChannelPerCard
    WithEvents mSerialPort As System.IO.Ports.SerialPort
    ''' <summary>讀值暫存</summary>
    ''' <remarks></remarks>
    Dim mValue As Decimal
    Enum Cmd
        ''' <summary>
        ''' 無命令
        ''' </summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>
        ''' 設定值
        ''' </summary>
        ''' <remarks></remarks>
        SetValue = 1
        ''' <summary>
        ''' 取得值
        ''' </summary>
        ''' <remarks></remarks>
        GetValue = 2
    End Enum
    Dim mCmd As Cmd = Cmd.None
    ''' <summary>逾時計時器</summary>
    ''' <remarks></remarks>
    Dim mTimeOutStopWatch As New Stopwatch

    ''' <summary>初始化, 物件由外部傳入</summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByRef item As System.IO.Ports.SerialPort) As Boolean Implements IElectroPneumaticValve.Initial
        mSerialPort = item
        If mSerialPort.IsOpen() Then
            Return True
        End If
        Try
            mSerialPort.Open()
            mSerialPort.ReadExisting() '清空buffer
            mSerialPort.DiscardInBuffer()
            mIsBusy = False
            Return True
        Catch ex As Exception
            Return False
        End Try 
    End Function

    Public Function Close() As Boolean Implements IElectroPneumaticValve.Close
        If mSerialPort Is Nothing Then
            Return True
        End If
        mSerialPort.Close()
        Return True
    End Function

    ''' <summary>
    ''' 最大氣壓值 TODO:改設定檔
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Max_Mpa As Decimal = 0.9 Implements IElectroPneumaticValve.Max_Mpa
    ''' <summary>
    ''' 最小氣壓值 TODO:改設定檔
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Min_Mpa As Decimal = 0 Implements IElectroPneumaticValve.Min_Mpa
    ''' <summary>
    ''' 最大刻度值
    ''' </summary>
    ''' <remarks></remarks>
    Dim Max_nn As Decimal = 1023
    ''' <summary>
    ''' 最小刻度值
    ''' </summary>
    ''' <remarks></remarks>
    Dim Min_nn As Decimal = 0
    Dim mTimeOutTimer As Integer = 1000

    ''' <summary>[是否忙碌中]</summary>
    ''' <remarks></remarks>
    Dim mIsBusy As Boolean

    ''' <summary>
    ''' 是否忙碌中
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy As Boolean Implements IElectroPneumaticValve.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    Function NNToValue(ByVal nn As String) As Decimal
        Dim value As Decimal = CDec(nn.Replace("\r\n", "").Trim()) * Max_Mpa / Max_nn
        If value < Min_Mpa Then value = Min_Mpa
        If value > Max_Mpa Then value = Max_Mpa
        value = Math.Round(value, 3)
        Return value
    End Function

    ''' <summary>輸入氣壓值轉為刻度</summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValueToNN(ByVal value As Decimal) As String

        Dim nn As Integer = CInt(Max_nn * value / Max_Mpa)
        If nn > Max_nn Then nn = Max_nn
        If nn < Min_nn Then nn = Min_nn
        Return nn
    End Function

    ''' <summary>逾時</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut As Boolean Implements IElectroPneumaticValve.IsTimeOut
        Get
            If mTimeOutStopWatch.ElapsedMilliseconds > mTimeOutTimer Then
                mIsBusy = False
                mTimeOutStopWatch.Stop()
                If mSerialPort.IsOpen Then
                    mSerialPort.DiscardInBuffer()
                End If
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>讀取資料</summary>
    ''' <param name="value">不等待則是前一值,等待則是本次取值</param>
    ''' <param name="waitReturn">等到傳回值或逾時</param>
    ''' <param name="stationNo">站號,預留</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValue(ByRef value As Decimal, Optional ByVal waitReturn As Boolean = False, Optional stationNo As Integer = -1) As Boolean Implements IElectroPneumaticValve.GetValue
        mIsBusy = True
        value = mResult.Value
        mCmd = Cmd.GetValue
        mTimeOutStopWatch.Restart()
        mResult = Nothing
        If mSerialPort.IsOpen Then
            mSerialPort.WriteLine("MON") '要求資料更新 Soni / 2017.06.22 由REQ(讀取設定值)改為MON(讀取當前值)

            If waitReturn = False Then '等待傳回
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
        End If
        Return mResult.Status
    End Function

    ''' <summary>設定氣壓值</summary>
    ''' <param name="value">設定值</param>
    ''' <param name="waitReturn">等待傳回值或逾時</param>
    ''' <param name="stationNo">站號,預留</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValue(value As Decimal, Optional ByVal waitReturn As Boolean = False, Optional stationNo As Integer = -1) As Boolean Implements IElectroPneumaticValve.SetValue
        mIsBusy = True
        mCmd = Cmd.SetValue
        Dim mData As String = "SET " & ValueToNN(value)
        mTimeOutStopWatch.Restart()
        mResult = Nothing
        If mSerialPort.IsOpen Then
            mSerialPort.WriteLine(mData)
        End If

        If waitReturn = False Then '等待傳回
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

        Return mResult.Status
    End Function

    Private Sub mSerialPort_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived
        mResult.STR = mSerialPort.ReadExisting
        'Debug.Print("電空閥傳回值:" & mResult.STR & " 命令:" & mCmd & " 是否數值:" & IsNumeric(mResult.STR))
        Select Case mCmd
            Case Cmd.GetValue
                If IsNumeric(mResult.STR) Then
                    mValue = Val(mResult.STR)
                    mResult.Value = NNToValue(mValue)
                    mTimeOutStopWatch.Stop()
                    mResult.Status = True
                    mIsBusy = False
                    'Debug.Print("電空閥讀值:" & mResult.Value & " " & mResult.STR)
                End If
            Case Cmd.SetValue
                mTimeOutStopWatch.Stop()
                mResult.Status = True
                mIsBusy = False

        End Select
    End Sub

    ''' <summary>傳回結果記錄結構</summary>
    ''' <remarks></remarks>
    Dim mResult As sReceiveStatus
    ''' <summary>傳回結果記錄結構</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Result As sReceiveStatus Implements IElectroPneumaticValve.Result
        Get
            Return mResult
        End Get
    End Property


End Class

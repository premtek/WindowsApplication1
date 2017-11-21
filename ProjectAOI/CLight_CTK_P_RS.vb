Imports System.IO.Ports
Imports ProjectCore


''' <summary>晶毓科技Createk 多通道RS232定電流 LED閃頻控制器</summary>
''' <remarks></remarks>
Public Class CLight_CTK_P_RS
    Implements ILightInterface

    WithEvents mSerialPort As New SerialPort
    Dim mTimeOutStopWatch As New Stopwatch

    ''' <summary>
    ''' 光控器初始化
    ''' </summary>
    ''' <param name="portName"></param>
    ''' <param name="baudRate"></param>
    ''' <param name="dataBits"></param>
    ''' <returns></returns>
    ''' <remarks>後續將介面改為傳遞SerialPort</remarks>
    Public Function Initial(ByVal portName As String, ByVal baudRate As Integer, ByVal dataBits As Integer) As Boolean Implements ILightInterface.Initial
        Try
            mSerialPort.PortName = portName 'gSSystemParameter.sLightControl(0).PortName
            mSerialPort.BaudRate = baudRate '115200
            mSerialPort.DataBits = dataBits '8
            mSerialPort.Parity = Parity.None
            mSerialPort.StopBits = StopBits.One
            mSerialPort.ReadTimeout = 10000
            mSerialPort.WriteTimeout = 10000

            If Not SerialPort.GetPortNames().Contains(portName) Then
                gSyslog.Save("Light COM Port(" & portName & ") Not Exists.", , eMessageLevel.Alarm)
                Return False
            End If
            If mSerialPort.IsOpen Then
                Return True
            Else
                mSerialPort.Open()
                mSerialPort.DiscardInBuffer() '清空buffer
                Return True
            End If

            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CLight_CTK_P_RS")
            gSyslog.Save("Light Initial Error.", , eMessageLevel.Alarm)
            gSyslog.Save(ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try
    End Function

    Public Function HEX_to_DEC(ByVal Hex As String) As Integer
        Return Convert.ToInt32("0X" + Hex, 16)
    End Function

    ''' <summary>光源/模式資料串接</summary>
    ''' <param name="lightValue"></param>
    ''' <param name="mode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLightData(ByVal lightValue As Integer, ByVal mode As Integer)
        Dim lsb As Integer
        Dim usb As Integer = Math.DivRem(lightValue, 256, lsb)
        Dim hexLSB As String = Hex(lsb).PadLeft(2, "0")
        Dim hexUSB As String = Hex(usb)
        Return hexUSB & mode & " " & hexLSB
    End Function
    ''' <summary>光源模式 0:常亮</summary>
    ''' <remarks></remarks>
    Dim mode As Integer = 0

    ''' <summary>完整命令</summary>
    ''' <param name="cmd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFullCmd(ByVal cmd As String, ByVal data As String) As String
        Dim SyncBytes As String = "55 AA"

        Dim dataCount As String
        If data <> "" Then
            dataCount = data.Split(" ").Count
        Else
            dataCount = 0
        End If

        Dim packetLength As String = Hex(dataCount + 5 + 128)
        Dim iPacketLength As Integer = HEX_to_DEC(packetLength)
        Dim iSyncBytesSum As Integer = 255
        Dim iCommand As Integer = HEX_to_DEC(cmd)
        Dim iData As Integer = 0
        Dim sData As String() = data.Split(" ")
        For i As Integer = sData.GetLowerBound(0) To sData.GetUpperBound(0)
            iData += HEX_to_DEC(sData(i))
        Next

        Dim iCheckSum As Integer = (iPacketLength + iSyncBytesSum + iCommand + iData) Mod 256
        Dim checkSum As String = Hex(iCheckSum).PadLeft(2, "0")
        If data = "" Then
            Return packetLength & " " & SyncBytes & " " & cmd & " " & checkSum
        Else
            Return packetLength & " " & SyncBytes & " " & cmd & " " & data & " " & checkSum
        End If


    End Function

    ''' <summary>16進位字串轉為Byte陣列</summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HexStr2ByteArray(ByVal data As String) As Byte()
        Dim sendData As Byte() = New Byte((data.Length / 2) - 1) {}
        For i As Integer = 0 To (sendData.Length - 1)
            sendData(i) = CByte(Convert.ToInt32(data.Substring(i * 2, 2), 16))
        Next i
        Return sendData
    End Function

    Function FullCmdToByte(ByVal fullCmd As String) As Byte()
         Dim replacedCmd As String = fullCmd.Replace(" ", "")
        Dim bytData() As Byte = HexStr2ByteArray(replacedCmd)
       
        Return bytData
    End Function
    ''' <summary>設定通道的光源亮度</summary>
    ''' <param name="ChNum">通道</param>
    ''' <param name="lightValue">光源亮度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetLightValue(ByVal ChNum As Integer, ByVal lightValue As Integer, Optional ByVal waitReturn As Boolean = False) As String Implements ILightInterface.SetLightValue
        Dim data As String = GetLightData(lightValue, mode) & " 00"
        Dim fullCmd As String = GetFullCmd("23", ChNum.ToString().PadLeft(2, "0") & " " & data)
        Dim bytCmd() As Byte = FullCmdToByte(fullCmd)

        If Not mSerialPort.IsOpen Then
            mResult(ChNum).Status = False
            mResult(ChNum).STR = "Port Not Open."
            Return fullCmd
        End If
        mResult(ChNum).Status = False
        mIsBusy = True
        mTimeOutStopWatch.Restart()
        mSerialPort.Write(bytCmd, 0, bytCmd.Count)
        If waitReturn = False Then
            Return fullCmd
        Else

            Do
                System.Threading.Thread.CurrentThread.Join(1)
                If mResult(ChNum).Status = True Then
                    Exit Do
                ElseIf IsTimeOut Then
                    mResult(ChNum).Status = False
                    Return False
                End If
            Loop
            mTimeOutStopWatch.Stop()

        End If

        Return fullCmd
    End Function

    ''' <summary>取得光源亮度</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLightValue(ByVal ChNum As Integer, ByRef value As Integer, Optional ByVal waitReturn As Boolean = False) As String Implements ILightInterface.GetLightValue
        Dim fullCmd As String = GetFullCmd("21", ChNum.ToString().PadLeft(2, "0"))
        Dim bytCmd() As Byte = FullCmdToByte(fullCmd)
        If Not mSerialPort.IsOpen Then
            mResult(ChNum).Status = False
            mResult(ChNum).STR = "Port Not Open."
            Return fullCmd
        End If

        mResult(ChNum).Status = False
        mIsBusy = True
        mTimeOutStopWatch.Restart()
        mSerialPort.Write(bytCmd, 0, bytCmd.Count)
        If waitReturn = False Then
            Return fullCmd
        Else

            Do
                System.Threading.Thread.CurrentThread.Join(1)
                If mResult(ChNum).Status = True Then
                    Exit Do
                ElseIf IsTimeOut Then
                    mResult(ChNum).Status = False
                    Return False
                End If
            Loop
            mTimeOutStopWatch.Stop()
            value = mResult(ChNum).Value
        End If


        Return fullCmd
    End Function

    ''' <summary>光源亮度轉換 16進位轉10進位</summary>
    ''' <param name="data"></param>
    ''' <param name="data2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLightValue(ByVal data As Byte, ByVal data2 As Byte) As Integer
        Dim hexLSB As String = Hex(data).Substring(0, 1)
        Return HEX_to_DEC(hexLSB & Hex(data2).PadLeft(2, "0"))
    End Function

    ''' <summary>判斷傳回值檢查</summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsReturnDataCheckSumOK(ByVal data() As Byte) As Boolean
        If data.Length = 0 Then
            Return False
        End If

        Dim length As Integer = data(0) - 128 '取得傳回資料總長度
        Dim sum As Integer = 0

        If length <> data.Count Then
            Return False
        End If

        For i As Integer = 0 To length - 2 '將checksum以外資料加總
            sum += data(i)
        Next

        If sum Mod 256 = data(length - 1) Then '資料餘數與checksum比對
            Return True
        End If

        Return False
    End Function

    Private Sub mSerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived

        Dim i As Integer = 0
        Dim count As Integer = mSerialPort.BytesToRead - 1
        Dim buffer(count) As Byte

        If count < 0 Then
            Exit Sub
        End If

        For i = 0 To count
            buffer(i) = mSerialPort.ReadByte
        Next

        '讀取資料
        If IsReturnDataCheckSumOK(buffer) Then
            Select Case Hex(buffer(3))
                Case "DF" '讀取1~4通道設定值
                Case "DE" '讀取單通道設定值
                    Select Case Hex(buffer(4)).PadLeft(2, "0")
                        Case "00"
                            mResult(0).Status = True
                            mResult(0).Value = GetLightValue(buffer(5), buffer(6))
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False
                        Case "01"
                            mResult(1).Status = True
                            mResult(1).Value = GetLightValue(buffer(5), buffer(6))
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False
                        Case "02"
                            mResult(2).Status = True
                            mResult(2).Value = GetLightValue(buffer(5), buffer(6))
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False
                        Case "03"
                            mResult(3).Status = True
                            mResult(3).Value = GetLightValue(buffer(5), buffer(6))
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False
                    End Select
                Case "DD" '設定1~4通道設定值
                Case "DC" '設定單通道設定值
                    '[Note]:若送89 55 AA 23 02 02 10 58 00 chksum-->
                    '                             -  --          -->value:0x158
                    '                           -                -->channel:3

                    '回傳:      87 AA 55 DC 02 01 chksum        -->
                    '                        -                  -->channel:3
                    '                          --               -->接收成功
                    Select Case Hex(buffer(4)).PadLeft(2, "0")
                        Case "00"
                            If Hex(buffer(5)).PadLeft(2, "0") = "01" Then
                                mResult(0).Status = True
                            Else
                                mResult(0).Status = False
                            End If
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False

                        Case "01"
                            If Hex(buffer(5)).PadLeft(2, "0") = "01" Then
                                mResult(1).Status = True
                            Else
                                mResult(1).Status = False
                            End If
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False

                        Case "02"
                            If Hex(buffer(5)).PadLeft(2, "0") = "01" Then
                                mResult(2).Status = True
                            Else
                                mResult(2).Status = False
                            End If
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False

                        Case "03"
                            If Hex(buffer(5)).PadLeft(2, "0") = "01" Then
                                mResult(3).Status = True
                            Else
                                mResult(3).Status = False
                            End If
                            mTimeOutStopWatch.Stop()
                            mIsBusy = False

                    End Select

                Case "DB" '讀取溫度設定值
                Case "DA" '讀取出廠設定值
                Case "D8" '重設所有設定值
                Case "D6" '讀取每通道最大電流

            End Select
        Else
            Debug.WriteLine("CLight_CTK_P_RS: ReturnDataCheck Failed")
        End If

    End Sub

    Public Function Close() As Boolean Implements ILightInterface.Close
        mSerialPort.Close()
        Return True
    End Function

    Dim mIsBusy As Boolean
    Public ReadOnly Property IsBusy As Boolean Implements ILightInterface.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    Dim mTimeOutTimer As Integer = 1000

    Public ReadOnly Property IsTimeOut As Boolean Implements ILightInterface.IsTimeOut
        Get
            If mTimeOutStopWatch.ElapsedMilliseconds > mTimeOutTimer Then
                mIsBusy = False
                mTimeOutStopWatch.Stop()
                mSerialPort.DiscardInBuffer()
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>接收資料結果</summary>
    ''' <remarks></remarks>
    Dim mResult(3) As sReceiveStatus
    Public ReadOnly Property Result(ByVal channelNo As Integer) As sReceiveStatus Implements ILightInterface.Result
        Get
            Return mResult(channelNo)
        End Get
    End Property
End Class

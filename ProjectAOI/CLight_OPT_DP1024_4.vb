Imports System.IO.Ports
Imports ProjectCore


Public Class CLight_OPT_DP1024_4
    Implements ILightInterface

    WithEvents mSerialPort As New SerialPort

   

    Dim mChLightValue(3) As Integer

    Public Function Initial(portName As String, baudRate As Integer, dataBits As Integer) As Boolean Implements ILightInterface.Initial
        Try
            mSerialPort.PortName = portName 'gSSystemParameter.sLightControl(0).PortName
            mSerialPort.BaudRate = baudRate '115200
            mSerialPort.DataBits = dataBits '8
            mSerialPort.Parity = Parity.None
            mSerialPort.StopBits = StopBits.One
            mSerialPort.ReadTimeout = 15000 '10000
            mSerialPort.WriteTimeout = 10000

            'Debug.WriteLine("----CLight_OPT_DP1024_4----   ")

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
            MsgBox(ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CLight_OPT_DP1024_4")
            gSyslog.Save("Light Initial Error.", , eMessageLevel.Alarm)
            gSyslog.Save(ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try
    End Function


    Public Function HexStr2ByteArray(ByVal data As String) As Byte()
        Dim sendData As Byte() = New Byte((data.Length / 2) - 1) {}
        For i As Integer = 0 To (sendData.Length - 1)
            sendData(i) = CByte(Convert.ToInt32(data.Substring(i * 2, 2), 16))
            'Debug.WriteLine("HexStr2ByteArray " & i & " :" & sendData(i))
        Next i
        Return sendData
    End Function

    Public Function GetFullCmdString(ByVal data As String)
        Dim FullCmd As String
        Dim Count As Integer = data.Length
        Dim ascii() As Byte = System.Text.Encoding.Default.GetBytes(data)
        Dim HighByte As Integer
        Dim LowByte As Integer
        Dim hexArray(Count - 1) As String
        For idx As Integer = 0 To Count - 1
            hexArray(idx) = ascii(idx).ToString("x2")
            'Debug.WriteLine("hexArray " & idx & ": " & hexArray(idx))
            HighByte = HighByte Xor Convert.ToInt32(hexArray(idx).Substring(0, 1), 16)
            'Debug.WriteLine("SubArray :" & hexArray(idx).Substring(0, 1) & "  HighByte: " & HighByte)
            LowByte = LowByte Xor Convert.ToInt32(hexArray(idx).Substring(1, 1), 16)
            'Debug.WriteLine("SubArray :" & hexArray(idx).Substring(1, 1) & "  LowByte: " & LowByte)
        Next
        FullCmd = data + Hex(HighByte).ToString + Hex(LowByte).ToString
        'Debug.WriteLine("FullCmd : " & FullCmd)
        Return FullCmd

    End Function


    Public Function ASCIIToHex(ByVal asciiString As String) As Byte()


        Dim ascii() As Byte = System.Text.Encoding.Default.GetBytes(asciiString)
        Dim count As Integer = ascii.Length
        Dim bytCmd As Byte() = New Byte(count - 1) {}
        For i As Integer = 0 To (bytCmd.Length - 1)
            'bytCmd(i) = CByte(Convert.ToInt32(hexArray(i).Substring(0, 2), 16))
            bytCmd(i) = CByte(Convert.ToInt32(ascii(i).ToString("x2").Substring(0, 2), 16))
            'Debug.WriteLine("bytCmd " & i & " : " & bytCmd(i))
        Next i

        Return bytCmd
    End Function



    Public Function SetLightValue(ChNo As Integer, ByVal lightValue As Integer, Optional ByVal waitReturn As Boolean = False) As String Implements ILightInterface.SetLightValue
        If lightValue > 255 Then
            lightValue = 255
        End If
        Dim data As String = Hex(lightValue).ToString
        Dim ChNoStr As String = (ChNo + 1).ToString   'OPT 通道為1234

        Dim asciiString As String = "$3" + ChNoStr + data.ToString().PadLeft(3, "0")   '"$310ff"
        'Debug.WriteLine("asciiString: " & asciiString)
        Dim ascii() As Byte = System.Text.Encoding.Default.GetBytes(asciiString)
        Dim count As Integer = ascii.Length

        Dim fullcmd As String

        fullcmd = GetFullCmdString(asciiString)
       
        Dim bytCmd() As Byte = ASCIIToHex(fullcmd)

        If Not mSerialPort.IsOpen Then
            mResult(ChNo).Status = False
            mResult(ChNo).STR = "Port Not Open"
            Return fullcmd
        End If
        mSerialPort.Write(bytCmd, 0, bytCmd.Count)
        mIsBusy = True

        Return fullcmd
    End Function

   



    Public Function GetLightValue(ChNo As Integer, ByRef value As Integer, Optional ByVal waitReturn As Boolean = False) As String Implements ILightInterface.GetLightValue
        Dim ChNoStr As String = (ChNo + 1).ToString

        Dim asciiString As String = "$4" + ChNoStr + "000"   '"$310ff"
        'Debug.WriteLine("asciiString: " & asciiString)
        Dim ascii() As Byte = System.Text.Encoding.Default.GetBytes(asciiString)
        Dim count As Integer = ascii.Length

        Dim fullcmd As String

        fullcmd = GetFullCmdString(asciiString)
        'Debug.WriteLine("----CLight-- RS232  FullCmd : " & fullcmd)


        Dim bytCmd() As Byte = ASCIIToHex(fullcmd)


        If mSerialPort.IsOpen Then
            mSerialPort.Write(bytCmd, 0, bytCmd.Count)
            mIsBusy = True
        End If
        Return fullcmd

    End Function






    ''' <summary>判斷傳回值檢查</summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsReturnDataCheckSumOK(ByVal data() As Byte) As Boolean
        Dim length As Integer = 8 '取得傳回資料總長度
        Dim sum As Integer = 0



        If data.Count <> length Then
            If data.Count = 1 And data(0) = 36 Then
                Return True 'Set Light Value only return $
            End If
            'Debug.WriteLine("---CLight--- ReturnData ERROR")
            Return False
        End If


        For i As Integer = 0 To length - 1
            If (data(i) = 0) Then
                'Debug.WriteLine("---CLight--- ReturnData ERROR  " & i & ":  " & data(i))
                Return False
            End If
        Next

        Return True
    End Function



    Public Function HEX_to_DEC(ByVal Hex As String) As Integer
        Return Convert.ToInt32("0X" + Hex, 16)
    End Function



    Private Sub mSerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived

        Dim receiving As Boolean = True
        System.Threading.Thread.Sleep(40)
        Dim count As Integer = mSerialPort.BytesToRead - 1
        Dim buffer(count) As Byte

        For i As Integer = 0 To count
            buffer(i) = mSerialPort.ReadByte  'ascii

        Next

        '讀取資料


        If IsReturnDataCheckSumOK(buffer) Then

            If buffer.Count = 1 Then
                ' Debug.WriteLine("---CLight---光源設定成功") 'setLight return $
            Else
                Dim LightCh As Char = Chr(buffer(2))

                Dim LightChStr As String = Chr(buffer(3)).ToString & Chr(buffer(4)).ToString & Chr(buffer(5)).ToString
               
                Select Case LightCh
                    Case "1" '讀取1通道設定值
                        mTimeOutStopWatch.Stop()
                        mResult(0).STR = LightChStr
                        mResult(0).Value = HEX_to_DEC(LightChStr)
                        mResult(0).Status = True
                        mIsBusy = False
                    Case "2" '讀取2通道設定值
                        mTimeOutStopWatch.Stop()
                        mResult(1).STR = LightChStr
                        mResult(1).Value = HEX_to_DEC(LightChStr)
                        mResult(1).Status = True
                        mIsBusy = False
                    Case "3"
                        mTimeOutStopWatch.Stop()
                        mResult(2).STR = LightChStr
                        mResult(2).Value = HEX_to_DEC(LightChStr)
                        mResult(2).Status = True
                        mIsBusy = False
                    Case "4"
                        mTimeOutStopWatch.Stop()
                        mResult(3).STR = LightChStr
                        mResult(3).Value = HEX_to_DEC(LightChStr)
                        mResult(3).Status = True
                        mIsBusy = False
                End Select

            End If

        Else
            'Debug.WriteLine("CLight OPT ReturnDataCheck ERROR")

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

    Dim mTimeOutStopWatch As New Stopwatch
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
    ''' <summary>接收資料結果</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Result(channelNo As Integer) As sReceiveStatus Implements ILightInterface.Result
        Get
            Return mResult(channelNo)
        End Get
    End Property
End Class


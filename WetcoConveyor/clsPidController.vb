Imports System.IO.Ports
Imports System.Threading

Public Class clsPidController

    Dim Receiving As Boolean

    Dim _isBusy As Boolean
    Public ReadOnly Property IsBusy As Boolean
        Get
            Return _isBusy
        End Get
    End Property

    Public ReadOnly Property IsOpen As Boolean
        Get
            Return serialport.IsOpen()
        End Get
    End Property

    Public Data As New clsPidData

    Dim serialport As New SerialPort
    Dim ErrorMessage As String
    Dim SendData As String

    ''' <summary>
    ''' 取得溫度
    ''' </summary>
    Dim cmdGetPV As String = "0103008A0001"
    ''' <summary>
    ''' 取得目標溫度
    ''' </summary>
    Dim cmdGetSV As String = "010300000001"
    ''' <summary>
    ''' 設定目標溫度
    ''' </summary>
    Dim cmdSetSV As String = "01060000"
    Dim cSetSV As String
    ''' <summary>
    ''' 取得PV補償值
    ''' </summary>
    Dim cmdGetPVOS As String = "010300650001"
    ''' <summary>
    ''' 設定PV補償值
    ''' </summary>
    Dim cmdSetPVOS As String = "01060065"
    Dim cSetPVOS As String

    Sub New()
        serialport.ReadTimeout = 100
        serialport.WriteTimeout = 100
    End Sub

    ''' <summary>
    ''' 開啟控制器
    ''' </summary>
    Public Function Open(ByVal portName As String,
                        Optional ByVal buadRate As Integer = 38400,
                        Optional ByVal parity As System.IO.Ports.Parity = IO.Ports.Parity.Even,
                        Optional ByVal dataBits As Integer = 8,
                        Optional ByVal stopBits As System.IO.Ports.StopBits = StopBits.One) As Boolean
        Try
            serialport.PortName = portName
            serialport.BaudRate = buadRate
            serialport.Parity = parity
            serialport.DataBits = dataBits
            serialport.StopBits = stopBits

            serialport.Open()
            Data.IsOpen = serialport.IsOpen
            If (serialport.IsOpen) Then
                Data.Comport = portName
                Return True
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = "Com port open fail"
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 關閉控制器
    ''' </summary>
    Public Sub Close()
        serialport.Close()
        Data.IsOpen = serialport.IsOpen
    End Sub

    ''' <summary>
    ''' 讀取溫度值
    ''' </summary>
    Public Function GetPV() As Boolean
        If Not IsBusy Then
            SendData = cmdGetPV
            Dim t As Threading.Thread = New Threading.Thread(AddressOf DoSendData)
            t.IsBackground = True
            t.Name = "clsdPidController.GetPV" '確認有哪些緒是這邊開的
            t.Start()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取目標溫度
    ''' </summary>
    Public Function GetSV() As Boolean
        If Not IsBusy Then
            SendData = cmdGetSV
            Dim t As Threading.Thread = New Threading.Thread(AddressOf DoSendData)
            t.IsBackground = True
            t.Name = "clsdPidController.GetSV" '確認有哪些緒是這邊開的
            t.Start()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定目標溫度
    ''' </summary>
    ''' <param name="temperature">溫度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSV(ByVal temperature As Double) As Boolean
        If Not IsBusy Then
            'Dim myHex As String = CInt(temperature * 10).ToString("X4")
            Dim myHex As String = IntegerTo4ByteHex(CInt(temperature * 10))
            cSetSV = cmdSetSV & myHex
            SendData = cSetSV
            Dim t As Threading.Thread = New Threading.Thread(AddressOf DoSendData)
            t.IsBackground = True
            t.Name = "clsdPidController.SetSV" '確認有哪些緒是這邊開的
            t.Start()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取溫度補償值
    ''' </summary>
    Public Function GetPVOS() As Boolean
        If Not IsBusy Then
            SendData = cmdGetPVOS
            Dim t As Threading.Thread = New Threading.Thread(AddressOf DoSendData)
            t.IsBackground = True
            t.Name = "clsdPidController.GetPVOS" '確認有哪些緒是這邊開的
            t.Start()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定溫度補償值
    ''' </summary>
    ''' <param name="temperature"> 溫度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPVOS(ByVal temperature As Double) As Boolean
        If Not IsBusy Then
            'Dim myHex As String = CInt(temperature * 10).ToString("X4")
            Dim myHex As String = IntegerTo4ByteHex(CInt(temperature * 10))
            cSetPVOS = cmdSetPVOS & myHex
            SendData = cSetPVOS
            Dim t As Threading.Thread = New Threading.Thread(AddressOf DoSendData)
            t.Name = "clsdPidController.SetPVOS" '確認有哪些緒是這邊開的
            t.IsBackground = True
            t.Start()
            Return True
        End If
        Return False
    End Function



    'Modbus Data Send
    Private Function ModbusSend() As Boolean
        Dim data() As Byte

        Try
            'Check COM Port Open
            If serialport.IsOpen = False Then
                ErrorMessage = "COM Port Not OPEN"
                Return False
            End If
            'Check Data is Even
            If (SendData.Length And 1) = 1 Then
                ErrorMessage = "Send Data ERROR"
                Return False
            End If
            ReDim data(SendData.Length \ 2 + 1)
            data = CRC_Cal(SendData)
            serialport.Write(data, 0, data.Length)
            Return True
        Catch ex As Exception
            ErrorMessage = ex.Message
            Return False
        End Try
    End Function

    'Modbus Data Get Receive
    Private Function ModbusGetRecieve(ByRef data() As Byte) As Boolean
        Dim str As String = ""
        Dim i As Integer
        Dim chkdata() As Byte = {}
        Dim tempdata() As Byte = {}

        If serialport.IsOpen = False Then Return False ' COM Port not open

        Try
            If serialport.BytesToRead = 0 Then
                ErrorMessage = "No data"
                Return False
            End If
            ReDim data(serialport.BytesToRead)
            ReDim tempdata(serialport.BytesToRead)
            serialport.Read(data, 0, data.Length - 1)
            For i = 0 To data.Length - 1
                tempdata(i) = data(i)
            Next

            chkdata = CRC_Cal(tempdata)
            If chkdata(chkdata.Length - 1) = data(data.Length - 1) And
                chkdata(chkdata.Length - 2) = data(data.Length - 2) Then

                Return True
            Else
                ErrorMessage = "check sum error"
                Return False
            End If
        Catch ex As Exception
            ErrorMessage = ex.Message
            Return False
        End Try

    End Function

    'Modbus CRC Calculate
    Private Overloads Function CRC_Cal(ByVal TransData As String) As Byte()
        Dim len As Integer = TransData.Length \ 2
        Dim data(len + 1) As Byte
        Dim i As Integer
        Dim j As Integer
        Dim Crc As UInt16 = Convert.ToUInt16("FFFF", 16)

        Try
            'CRC data calculate 
            For i = 0 To len - 1
                data(i) = Convert.ToByte(TransData.Substring(i * 2, 2), 16)
            Next

            For i = 0 To len - 1
                Crc = Crc Xor data(i)
                For j = 8 To 1 Step -1
                    If (Crc And 1) <> 0 Then
                        Crc = Crc >> 1
                        Crc = Crc Xor Convert.ToUInt16("A001", 16)
                    Else
                        Crc = Crc >> 1
                    End If
                Next
            Next

            data(len + 1) = Crc >> 8
            data(len) = Crc And 255
            Return data
        Catch ex As Exception
            ErrorMessage = ex.Message
            Return data
        End Try
    End Function

    Private Overloads Function CRC_Cal(ByVal TransData As Byte()) As Byte()
        Dim len As Integer = TransData.Length
        Dim data(len - 1) As Byte
        Dim i As Integer
        Dim j As Integer
        Dim Crc As UInt16 = Convert.ToUInt16("FFFF", 16)

        Try
            'CRC data calculate 
            data = TransData

            For i = 0 To len - 1
                Crc = Crc Xor data(i)
                For j = 8 To 1 Step -1
                    If (Crc And 1) <> 0 Then
                        Crc = Crc >> 1
                        Crc = Crc Xor Convert.ToUInt16("A001", 16)
                    Else
                        Crc = Crc >> 1
                    End If
                Next
            Next

            data(len + 1) = Crc >> 8
            data(len) = Crc And 255
            Return data
        Catch ex As Exception
            ErrorMessage = ex.Message
            Return data
        End Try
    End Function

    Public Function CRC16(ByVal data() As Byte) As Byte()
        Dim CRC16Lo As Byte, CRC16Hi As Byte   'CRC暫存器 
        Dim CL As Byte, CH As Byte        '多項式碼&HA001 
        Dim SaveHi As Byte, SaveLo As Byte
        Dim i As Integer
        Dim Flag As Integer
        CRC16Lo = &HFF
        CRC16Hi = &HFF
        CL = &H1
        CH = &HA0
        For i = 0 To UBound(data)
            CRC16Lo = CRC16Lo Xor data(i) '每一個資料與CRC暫存器進行異或 
            For Flag = 0 To 7
                SaveHi = CRC16Hi
                SaveLo = CRC16Lo
                CRC16Hi = CRC16Hi \ 2      '高位右移一位 
                CRC16Lo = CRC16Lo \ 2      '低位右移一位 
                If ((SaveHi And &H1) = &H1) Then '如果高位字節最後一位為1 
                    CRC16Lo = CRC16Lo Or &H80   '則低位字節右移後前面補1 
                End If              '否則自動補0 
                If ((SaveLo And &H1) = &H1) Then '如果LSB為1，則與多項式碼進行異或 
                    CRC16Hi = CRC16Hi Xor CH
                    CRC16Lo = CRC16Lo Xor CL
                End If
            Next Flag
        Next i
        Dim ReturnData(1) As Byte
        ReturnData(0) = CRC16Hi       'CRC高位 
        ReturnData(1) = CRC16Lo       'CRC低位 
        Return ReturnData
    End Function

    Private Sub DoSendData()
        Try
            _isBusy = True
            If (ModbusSend()) Then
                Receiving = True
                DoReceive()
            Else
                _isBusy = False
            End If
        Catch ex As Exception
            _isBusy = False
        Finally
            _isBusy = False
        End Try
    End Sub

    Private Function DoReceive() As Boolean
        Dim tempList As New List(Of Byte)
        Dim buffer(1023) As Byte
        Dim ticks As Int32 = 0
        While Receiving = True
            Thread.Sleep(100)
            If serialport.BytesToRead > 0 Then
                Try
                    Dim receivedLength As Int32 = serialport.Read(buffer, 0, buffer.Length)
                    Array.Resize(buffer, receivedLength)
                    tempList.AddRange(buffer)
                    Array.Resize(buffer, 1024)
                Catch timeEx As TimeoutException
                    tempList.Clear()
                    Receiving = False
                Catch ex As Exception
                    Receiving = False
                End Try
            End If

            If tempList.Count > 3 Then
                Dim value As Double
                Select Case SendData
                    Case cmdGetPV
                        If (tempList.Count = 7) Then
                            If (CheckCRC16(tempList)) Then
                                If (DataToTemperature(tempList, value)) Then
                                    Data.PV = value
                                    Receiving = False
                                    Return True
                                End If
                            End If
                        ElseIf (tempList.Count > 7) Then
                            tempList.RemoveRange(7, tempList.Count - 7)
                        End If

                    Case cmdGetSV
                        If (tempList.Count = 7) Then
                            If (CheckCRC16(tempList)) Then
                                If (DataToTemperature(tempList, value)) Then
                                    Data.SV = value
                                    Receiving = False
                                    Return True
                                End If
                            End If
                        ElseIf (tempList.Count > 7) Then
                            tempList.RemoveRange(7, tempList.Count - 7)
                        End If

                    Case cSetSV
                        If (tempList.Count = 8) Then
                            If (CheckCRC16(tempList)) Then
                                Receiving = False
                                Return True
                            End If
                        ElseIf (tempList.Count > 8) Then
                            tempList.RemoveRange(8, tempList.Count - 8)
                        End If

                    Case cmdGetPVOS
                        If (tempList.Count = 7) Then
                            If (CheckCRC16(tempList)) Then
                                If (DataToTemperature(tempList, value)) Then
                                    Data.PVOS = value
                                    Receiving = False
                                    Return True
                                End If
                            End If
                        ElseIf (tempList.Count > 7) Then
                            tempList.RemoveRange(7, tempList.Count - 7)
                        End If

                    Case cSetPVOS
                        If (tempList.Count = 8) Then
                            If (CheckCRC16(tempList)) Then
                                Receiving = False
                                Return True
                            End If
                        ElseIf (tempList.Count > 8) Then
                            tempList.RemoveRange(8, tempList.Count - 8)
                        End If

                End Select
            End If

            ticks += 1
            If ticks >= 20 Then
                Receiving = False
                ErrorMessage = "Receiving time out"
            End If
        End While

        Return False
    End Function

    ''' <summary>
    ''' PID回傳值轉為溫度值
    ''' </summary>
    Private Function DataToTemperature(ByVal data As Byte(), ByRef value As Double) As Boolean
        If (data IsNot Nothing) Then
            If data(0).ToString("X2") = "01" AndAlso _
               data(1).ToString("X2") = "03" AndAlso _
               data(2).ToString("X2") = "02" Then
                Dim myHex As String = data(3).ToString("X2") & data(4).ToString("X2")
                value = Convert.ToInt32(myHex, 16)
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' PID回傳值轉為溫度值
    ''' </summary>
    Private Function DataToTemperature(ByVal tempList As List(Of Byte), ByRef value As Double) As Boolean
        If (tempList.Count > 3) Then
            If tempList(0).ToString("X2") = "01" AndAlso _
               tempList(1).ToString("X2") = "03" AndAlso _
               tempList(2).ToString("X2") = "02" Then
                Dim myHex As String = tempList(3).ToString("X2") & tempList(4).ToString("X2")
                value = Convert.ToInt32(myHex, 16) / 10
                Return True
            End If
        End If
        Return False
    End Function

    Private Function CheckCRC16(ByVal tempList As List(Of Byte)) As Boolean
        Dim CRCdata() As Byte = {}
        Dim str As String = ""

        For i = 0 To tempList.Count - 3
            str = str & tempList(i).ToString("X2")
        Next

        CRCdata = CRC_Cal(str)
        If (CRCdata(CRCdata.Length - 1) = tempList(tempList.Count - 1) AndAlso
            CRCdata(CRCdata.Length - 2) = tempList(tempList.Count - 2)) Then
            Return True
        End If

        Return False
    End Function

    Public Function IntegerTo4ByteHex(ByVal int As Integer) As String
        Dim myHex As String = int.ToString("X4")
        If (int < 0 AndAlso myHex.Length = 8) Then
            myHex = myHex.Remove(1, 4)
        End If
        Return myHex
    End Function
End Class
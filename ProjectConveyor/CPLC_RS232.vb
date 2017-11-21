Imports System.IO.Ports
Imports ProjectCore

Public Class CPLC_RS232
    WithEvents mSerialPort As New SerialPort

    Public Event ErrorOccurred(sender As Object, ByVal e As DataEventArgs)
    Public Event ReadBitsDaataRecieved(sender As Object, ByVal e As DataEventArgs)
    Public Event CommandSuccess(sender As Object, ByVal e As DataEventArgs)

    ''' <summary>命令開始時間</summary>
    ''' <remarks></remarks>
    Dim mCmdStartTime As Double

    ''' <summary>逾時計時</summary>
    ''' <remarks></remarks>
    Public TimeOut As Double = 1000
    Public Const ENQ = Chr(&H5)
    Public Const ACK = Chr(&H6)
    Public Const NAK = Chr(&H15)
    Public Const STX = Chr(&H2)
    Public Const ETX = Chr(&H3)
    Public NetworkNo As String = "05"
    Public Const PCNo = "FF"
    ''' <summary>命令類型列舉</summary>
    ''' <remarks></remarks>
    Public Enum enmCmdType
        None
        BatchReadOfBitDevice
        BatchReadOfWordDevice
        BatchWriteOfBitDevice
        BatchWriteOfWordDevice
        TestOfBitDevice
        TestOfWordDevice
        LoopbackTest
    End Enum

    Public Const ReadWord = "WR"
    Public Const ReadBit = "BR"
    Public Const WriteWord = "WW"
    Public Const WriteBit = "BW"
    Public Const TestBit = "BT"
    Public Const TestWord = "WT"
    Public Const Loopback = "TT"
    ''' <summary>發出命令內容</summary>
    ''' <remarks></remarks>
    Dim SendCmd As String
    ''' <summary>發出命令類型</summary>
    ''' <remarks></remarks>
    Dim sendCmdType As enmCmdType
    ''' <summary>環迴測試資料</summary>
    ''' <remarks></remarks>
    Public loopBackData As String
    ''' <summary>環迴測試字數</summary>
    ''' <remarks></remarks>
    Public loopBackHexCharCount As String
    Public ReadBitCount As Integer
    Public ErrorMsg As String
    ''' <summary>ReadBits指向的Device</summary>
    ''' <remarks></remarks>
    Public ReadBitDevice As String

    ''' <summary>資料已接收</summary>
    ''' <remarks></remarks>
    Public IsDataRecieved As Boolean

    ''' <summary>迴路測試</summary>
    ''' <param name="iCharCount"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoopbackTest(ByVal iCharCount As Integer, ByVal data As String) As String
        Const DelayTime = "0"
        Dim cmdPart As String
        Dim charCount As String
        charCount = iCharCount.ToString("00")
        cmdPart = NetworkNo + PCNo + Loopback + DelayTime + charCount + data
        SendCmd = ENQ + cmdPart + GetCheckSum(cmdPart).PadLeft(2, "0")
        loopBackHexCharCount = Hex(iCharCount).PadLeft(2, "0")
        loopBackData = data
        If mSerialPort.IsOpen Then
            'RecievedString = ""
            mSerialPort.WriteLine(SendCmd)
            sendCmdType = enmCmdType.LoopbackTest
            IsDataRecieved = False
        End If
        Return SendCmd
    End Function
    Public Function GetCheckSum(ByVal Cmd As String) As String
        Dim sum As Integer
        sum = 0
        For i = 0 To Cmd.Length - 1
            sum += AscW(Cmd(i))
        Next
        sum = sum Mod 256
        Return Hex(sum).PadLeft(2, "0")
    End Function
    ''' <summary>Bit測試</summary>
    ''' <param name="iDeviceCount"></param>
    ''' <param name="device"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TestBits(ByVal iDeviceCount As Integer, ByVal device As String) As String
        Const DelayTime = "0"
        Dim DeviceCount As String
        DeviceCount = Hex(iDeviceCount).PadLeft(2, "0")
        Dim cmdPart As String

        cmdPart = NetworkNo + PCNo + TestBit + DelayTime + DeviceCount + device
        SendCmd = ENQ + cmdPart + GetCheckSum(cmdPart).PadLeft(2, "0")
        If mSerialPort.IsOpen Then
            'RecievedString = ""
            mSerialPort.WriteLine(SendCmd)
            sendCmdType = enmCmdType.TestOfBitDevice
            IsDataRecieved = False
        End If
        Return SendCmd
    End Function
    Public Function WriteBits(ByVal device As String, ByVal iDeviceCount As Integer, ByVal data As String) As String

        Const DelayTime = "0"
        Dim DeviceCount As String
        DeviceCount = Hex(iDeviceCount).PadLeft(2, "0")
        Dim cmdPart As String
        cmdPart = NetworkNo + PCNo + WriteBit + DelayTime + device + DeviceCount + data
        SendCmd = ENQ + cmdPart + GetCheckSum(cmdPart).PadLeft(2, "0")
        If mSerialPort.IsOpen Then
            'RecievedString = ""
            mSerialPort.WriteLine(SendCmd)
            sendCmdType = enmCmdType.BatchWriteOfBitDevice
            IsDataRecieved = False
        End If
        Return SendCmd
    End Function



    Public Function ReadBits(ByVal device As String, ByVal iDeviceCount As Integer) As String

        Const DelayTime = "A"
        Dim DeviceCount As String
        ReadBitCount = iDeviceCount
        DeviceCount = Hex(iDeviceCount).PadLeft(2, "0")
        Dim cmdPart As String
        cmdPart = NetworkNo + PCNo + ReadBit + DelayTime + device + DeviceCount
        SendCmd = ENQ + cmdPart + GetCheckSum(cmdPart)
        gPLC.ReadBitDevice = device
        If mSerialPort.IsOpen Then
            'RecievedString = ""
            mSerialPort.WriteLine(SendCmd)
            sendCmdType = enmCmdType.BatchReadOfBitDevice
            IsDataRecieved = False
        End If
        Return SendCmd
    End Function

    Public Function IsOpen() As Boolean
        Return mSerialPort.IsOpen
    End Function
    Public Sub Close()
        mSerialPort.Close()
    End Sub
    Public Function Open(ByVal portName As String) As Boolean
        Try
            mSerialPort.PortName = portName
            mSerialPort.BaudRate = 9600
            mSerialPort.DataBits = 7
            mSerialPort.Parity = Parity.Even
            mSerialPort.StopBits = StopBits.One
            mSerialPort.ReadTimeout = 1000
            mSerialPort.Open()
            Return mSerialPort.IsOpen
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "PLC")
            Return False
        End Try

    End Function
    Dim RecievedString As String
    Dim lockObj As Object

    Private Sub mSerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived

        Dim tmpData As String
        Dim isIgnoreCheckSum As Boolean
        isIgnoreCheckSum = False

        tmpData = mSerialPort.ReadExisting()
        If tmpData(0) = STX Then
            RecievedString = ""
            tmpData = tmpData.Substring(1)
        End If
        If tmpData(0) = ACK Then
            RecievedString = ""
            tmpData = tmpData.Substring(1)
            isIgnoreCheckSum = True
        End If
        If tmpData(0) = NAK Then
            ErrorMsg = "NAK Error Code:" & tmpData.Substring(4)
            RecievedString = ""
            IsDataRecieved = True
            RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
            Exit Sub
        End If
        RecievedString += tmpData
        If Not IsCheckSum(RecievedString) And isIgnoreCheckSum = False Then
            Exit Sub
        End If

        Select Case sendCmdType
            Case enmCmdType.BatchReadOfBitDevice
                If RecievedString.Substring(0, 2) = NetworkNo Then
                    If RecievedString.Substring(2, 2) = PCNo Then
                        Dim data As String
                        If RecievedString.Length > 4 + ReadBitCount Then
                            data = RecievedString.Substring(4, ReadBitCount)
                            'Debug.Print("Read Bit Data" & data)
                            mSerialPort.WriteLine(ACK + NetworkNo + PCNo) 'ACK
                            IsDataRecieved = True
                            RaiseEvent ReadBitsDaataRecieved(Me, New DataEventArgs(data, ReadBitCount))
                        End If
                    Else
                        ErrorMsg = "Batch Read Of Bit Device PC No. Error"
                        IsDataRecieved = True
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                    End If


                Else
                    ErrorMsg = "Batch Read Of Bit Device Station No. Error"
                    IsDataRecieved = True
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                End If
            Case enmCmdType.BatchWriteOfBitDevice
                If RecievedString.Substring(0, 2) = NetworkNo Then
                    If RecievedString.Substring(2, 2) = PCNo Then
                        IsDataRecieved = True
                        RaiseEvent CommandSuccess(Me, New DataEventArgs("Batch Write Of Bit Device OK"))
                    Else
                        ErrorMsg = "Batch Write Of Bit Device PC No. Error"
                        IsDataRecieved = True
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                    End If
                Else
                    ErrorMsg = "Batch Write Of Bit Device Station No. Error"
                    IsDataRecieved = True
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                End If

            Case enmCmdType.LoopbackTest
                If loopBackHexCharCount.PadLeft(2, "0") = (RecievedString.Substring(4, 2)) Then
                    If RecievedString.Substring(6, CInt(loopBackHexCharCount)) = loopBackData Then
                        IsDataRecieved = True
                        RaiseEvent CommandSuccess(Me, New DataEventArgs("Loop back OK"))
                    Else
                        ErrorMsg = "Loopback Data Error"
                        IsDataRecieved = True
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                    End If
                Else
                    ErrorMsg = "Loopback Count Error"
                    IsDataRecieved = True
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                End If
            Case enmCmdType.TestOfBitDevice
                If RecievedString.Substring(0, 2) = NetworkNo Then
                    If RecievedString.Substring(2, 2) = PCNo Then
                        IsDataRecieved = True
                        RaiseEvent CommandSuccess(Me, New DataEventArgs("Test Of Bit Device OK"))
                    Else
                        ErrorMsg = "Test Of Bit Device PC No. Error"
                        IsDataRecieved = True
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                    End If
                Else
                    ErrorMsg = "Test Of Bit Device Station No. Error"
                    IsDataRecieved = True
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMsg))
                End If
        End Select
        RecievedString = ""

    End Sub

    Public Function IsCheckSum(ByVal data As String) As Boolean
        If data = "" Then
            Return False
        End If
        Dim sumcheckdata As String
        Dim checksum As String
        sumcheckdata = data.Substring(0, data.Length - 2)
        checksum = data.Substring(data.Length - 2)
        If GetCheckSum(sumcheckdata) = checksum Then
            Return True
        Else
            Return False
        End If
    End Function

End Class

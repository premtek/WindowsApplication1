Imports System.IO.Ports
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports ProjectCore
Imports ProjectRecipe
Imports ProjectMotion

''' <summary>程控光源</summary>
''' <remarks></remarks>
Public Class CLightGLC_PD
    Implements ILightInterface


    WithEvents mSerialPort As New SerialPort
    ''' <summary>程控光源命令</summary>
    ''' <remarks></remarks>
    Public Enum enmLightCommand
        None = 0 '無
        SetLightValue = 1 '設定光源亮度
        GetLightValue = 2 '取得光源亮度
    End Enum

    Dim mCmdType As enmLightCommand
  

    Dim GetCata As Boolean = False

    Public Function Initial(ByVal portName As String, ByVal baudRate As Integer, ByVal dataBits As Integer) As Boolean Implements ILightInterface.Initial
        Try
            mSerialPort.PortName = portName 'gSSystemParameter.sLightControl(0).PortName
            mSerialPort.BaudRate = baudRate 'gSSystemParameter.sLightControl(0).BaudRate
            mSerialPort.DataBits = dataBits 'gSSystemParameter.sLightControl(0).DataBit
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
            MsgBox(gMsgHandler.GetMessage(Error_1022000) & vbCrLf & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save(gMsgHandler.GetMessage(Error_1022000), "Error_1022000", eMessageLevel.Error)
            gSyslog.Save(ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>設定通道的光源亮度</summary>
    ''' <param name="ChNum">通道</param>
    ''' <param name="lightValue">光源亮度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetLightValue(ByVal ChNum As Integer, ByVal lightValue As Integer, Optional ByVal waitReturn As Boolean = False) As String Implements ILightInterface.SetLightValue
        Dim cmdPart As String
        Dim fullCmd As String
        ' cmdPart = "01060001" & Hex(lightValue).PadLeft(4, "0")
        cmdPart = "0106000" & Val(ChNum) & Hex(lightValue).PadLeft(4, "0")
        fullCmd = ":" + cmdPart + CheckLRC(cmdPart) & vbCrLf
        If Not mSerialPort.IsOpen Then
            mResult(ChNum).STR = "Port Not Open"
            mResult(ChNum).Status = False
            Return fullCmd
        End If
        mIsBusy = True
        mTimeOutStopWatch.Restart()
        mCmdType = enmLightCommand.SetLightValue
        mSerialPort.WriteLine(fullCmd)

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
        Dim cmdPart As String
        Dim fullCmd As String
        'Dim returnStr As String
        GetCata = True
        cmdPart = "01030001000" & Val(ChNum)
        fullCmd = ":" + cmdPart + CheckLRC(cmdPart) & vbCrLf
        ChNumGet = ChNum
        If Not mSerialPort.IsOpen Then
            mResult(ChNum).STR = "Port Not Open"
            mResult(ChNum).Status = False
            Return fullCmd
        End If

        mIsBusy = True
        mTimeOutStopWatch.Restart()
        mSerialPort.WriteLine(fullCmd)
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

    Function CheckLRC(ByVal data As String) As String
        Dim sum As Integer
        Dim val As Integer
        sum = 256
        For i = 0 To data.Length - 1 Step 2 '每兩位取一次
            val = Convert.ToInt32(data.Substring(i, 2), 16)
            sum -= val
        Next
        Dim tmp As String
        tmp = Hex(sum).PadLeft(2, "0")
        Return tmp.Substring(tmp.Length - 2)
    End Function


  
    ''' <summary>Ch Num</summary>
    ''' <remarks></remarks>
    Dim ChNumGet As Integer


    Private Sub mSerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived

        Dim receiving As Boolean = True
        Dim buffer(1023) As Byte

        '確認讀取長度正確後再進行解碼
        If GetCata Then
            While receiving
                Dim Read As Integer = mSerialPort.BytesToRead
                Select Case (ChNumGet)
                    Case 1
                        If mSerialPort.BytesToRead = 15 Then
                            receiving = False
                        End If
                    Case 2
                        If mSerialPort.BytesToRead = 19 Then
                            receiving = False
                        End If
                    Case 3
                        If mSerialPort.BytesToRead = 23 Then
                            receiving = False
                        End If
                    Case 4
                        If mSerialPort.BytesToRead = 27 Then
                            receiving = False
                        End If
                End Select
            End While

            GetCata = False
        End If

        '讀取資料
        Dim data As String = mSerialPort.ReadExisting()
        '判斷回傳值
        Dim header As String = data.Substring(0, 1)
        If header <> ":" Then
            Exit Sub
        End If
        Dim funType As String = data.Substring(3, 2)
        '判斷是讀取還是寫入回傳值
        Select Case funType
            'CH讀取數值
            Case "03" '
                Select Case (ChNumGet)
                    Case 1
                        mResult(0).Value = Convert.ToInt32("0X" + data.Substring(7, 4), 16) '16進制轉10進制
                    Case 2
                        mResult(1).Value = Convert.ToInt32("0X" + data.Substring(11, 4), 16) '16進制轉10進制
                    Case 3
                        mResult(2).Value = Convert.ToInt32("0X" + data.Substring(15, 4), 16) '16進制轉10進制
                    Case 4
                        mResult(3).Value = Convert.ToInt32("0X" + data.Substring(19, 4), 16) '16進制轉10進制
                End Select
                mIsBusy = False
                mResult(ChNumGet - 1).Status = True
                'CH寫入數值
            Case "06" '寫入
                mResult(ChNumGet - 1).Value = Convert.ToInt32("0X" + data.Substring(9, 4), 16) '16進制轉10進制
                mIsBusy = False
                mResult(ChNumGet - 1).Status = True
        End Select

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

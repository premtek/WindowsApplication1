
Imports System.IO
'Imports System.Data
'Imports System.Data.SqlClient

Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports ProjectCore

''' <summary>
''' 通訊狀態
''' </summary>
''' <remarks></remarks>
Public Enum SockStatusInfo
    sckConnected = 0
    sckError = 1
    sckDisconnect = 2
End Enum

Public Class CUDPIP
    Implements IDisposable

    Private mUdpClient As UdpClient

    Private mListenThread As System.Threading.Thread

    Private mIPEndPoint As IPEndPoint

    ''' <summary>
    ''' [ErrorMessage事件]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnErrorMessage(ByVal sender As Object, ByVal e As DataEventArgs)

    ''' <summary>
    ''' [接收到CCD的字串內容]
    ''' </summary>
    ''' <remarks></remarks>
    Public ReceiveString As String

    ''' <summary>
    ''' [IP Address]
    ''' </summary>
    ''' <remarks></remarks>
    Public UDPIpAdress As IPAddress
    ''' <summary>[CCD Send Addreee]</summary>
    ''' <remarks></remarks>
    Public CCDSendAddress As SAddress

    ''' <summary>
    ''' [Server 端 Port 號碼]
    ''' </summary>
    ''' <remarks></remarks>
    Public UDPPort As Integer

    Public SockStatus As SockStatusInfo

    ''' <summary>[資料收抵事件]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnReceiveData(ByVal sender As Object, ByVal e As DataEventArgs)

    ''' <summary>
    ''' 自訂傳遞資訊包裝
    ''' </summary>
    ''' <remarks></remarks>
    Private Class CSState
        Public ClientSocket As Socket           '回傳遠端 Socket 類別
        Public mystring As String               '附帶訊息
    End Class

    ''' <summary>
    ''' 接收資料間隔時間
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RECEIVE_DATA_DELAY As Int32 = 17

 

    ''' <summary>[連線]</summary>
    ''' <param name="ip"></param>
    ''' <param name="portName"></param>
    ''' <remarks></remarks>
    Public Sub ClientConnect(ByVal ip As String, ByVal portName As Integer)

        Try
            Try
                UDPIpAdress = IPAddress.Parse(ip)
                UDPPort = portName
            Catch ex As Exception
                Dim e As New DataEventArgs
                e.Data = "Set Server IP Fail"
                RaiseEvent OnErrorMessage(Me, e)
                SockStatus = SockStatusInfo.sckError
                Exit Sub
            End Try

            mUdpClient = New UdpClient(UDPPort)
            mIPEndPoint = New IPEndPoint(UDPIpAdress, UDPPort)

            mListenThread = New System.Threading.Thread(AddressOf StartListen)
            mListenThread.IsBackground = True
            If mListenThread.IsAlive = False Then
                mListenThread.Start()
            End If

            SockStatus = SockStatusInfo.sckConnected


        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            RaiseEvent OnErrorMessage(Me, New DataEventArgs(ex.ToString))
        End Try
    End Sub

    ''' <summary>
    ''' [中斷連線]
    ''' </summary>
    Public Sub ClientDisconnect()
        Try
            SockStatus = SockStatusInfo.sckDisconnect
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' [中斷UDP 、 Thread]
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close()
        isClosing = True
        ReceiveThread.Abort()
        mListenThread.Abort()
        mUdpClient = Nothing
        mIPEndPoint = Nothing
    End Sub


    ''' <summary>[傳送通訊命令]</summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommand(ByVal msg As String) As Boolean
        Try

            Dim bytSendString() As Byte

            bytSendString = Encoding.Default.GetBytes(msg)

            With mUdpClient
                .Send(bytSendString, bytSendString.Length, CCDSendAddress.strIP, CCDSendAddress.intPort)
            End With
            Return True

        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            RaiseEvent OnErrorMessage(Me, New DataEventArgs(ex.ToString))
            Return False
        End Try
    End Function

    ''' <summary>[傳送通訊命令] </summary>
    ''' <param name="msg"></param>
    ''' <param name="strIP"></param>
    ''' <param name="intPort"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommand(ByVal msg As String, ByVal strIP As String, ByVal intPort As Integer) As Boolean
        Try

            Dim bytSendString() As Byte

            bytSendString = Encoding.Default.GetBytes(msg)

            With mUdpClient
                .Send(bytSendString, bytSendString.Length, strIP, intPort)
            End With
            Return True

        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            RaiseEvent OnErrorMessage(Me, New DataEventArgs(ex.ToString))
            Return False
        End Try
    End Function

    Dim ReceiveThread As System.Threading.Thread

    Private Sub StartListen()

        Dim myObj As New CSState
        ReceiveThread = New System.Threading.Thread(AddressOf ReceiveData_Server)

        Do
            If mUdpClient Is Nothing Then
                Exit Do
            ElseIf mUdpClient.Client Is Nothing Then
                Exit Do
            Else
                myObj.mystring = Now.ToString("yyyy/MM/dd HH:mm:ss") & "已連線"
                ReceiveThread.IsBackground = True
                ReceiveThread.Start()
                Exit Do
            End If
        Loop
    End Sub

    ''' <summary>程式結束中</summary>
    ''' <remarks></remarks>
    Dim isClosing As Boolean
    ''' <summary>
    ''' 主Thread 處理接收資料
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReceiveData_Server() As Boolean
        'mUdpClient.Client.ReceiveTimeout = 100
        Try
            Dim bytReceiveData() As Byte

            Do While Not isClosing
                Try
                    bytReceiveData = mUdpClient.Receive(mIPEndPoint)
                    ReceiveString = Encoding.Default.GetString(bytReceiveData)
                    System.Threading.Thread.Sleep(RECEIVE_DATA_DELAY)
                    RaiseEvent OnReceiveData(Me, New DataEventArgs(ReceiveString))
                Catch ex As Exception
                    Debug.Print(ex.Message & "@ReceiveData_Server")
                End Try
            Loop

            Return True

        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            RaiseEvent OnErrorMessage(Me, New DataEventArgs(ex.ToString()))
            Return False
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mUdpClient.Close()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


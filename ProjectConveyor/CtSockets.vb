' CtSockets.vb -使用 TCP / IP 主要程序
'
' Copyright (c) 2013 by Castec, Inc.
'
' +----------------------------------------------------------------------+
' | The information set forth in this document is the property of        |
' | Castec, Inc. and is to be held in trust and confidence.				 |
' | Publication, duplication, disclosure, or use for any purpose not     |
' | expressly authorized by Castec in writing is prohibited.			 |
' |                                                                      |
' | The information in this document is subject to change without notice |
' | and should not be construed as a commitment by Castec.				 |
' |                                                                      |
' | Castec makes no warranty as to the suitability of this				 |
' | material for use by the recipient, and assumes no responsibility for |
' | any consequences resulting from such use.                            |
' +----------------------------------------------------------------------+
'
' DESCRIPTION:	使用 TCP / IP 主要程序，提供IP、Port方式連結傳遞字串資料
'
'               Server 端監聽時於 Client 端進行連結時驗證 Port 號碼是否正確，
'               正確的話給予 Client 端一個目前無使用的 Port 號進行連結，可複數接收 Client 連結
'
'               Client 端進行連結時，目前無法得知本身取得的 Port 號碼，
'               連線取得後無法得知 Server 端是否還是繼續監聽或接收訊息
'
'               字串資料接收間隔時間建議25毫秒以上
'
' Author: William Huang, 2013/01/23

Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ProjectCore

Public Class CtSockets
    Implements IDisposable

    ''' <summary>
    ''' 自訂傳遞資訊包裝
    ''' </summary>
    ''' <remarks></remarks>
    Private Class CSState
        Public myTcpListener As TcpListener     '本地端監聽類別
        Public ClientSocket As Socket           '回傳遠端 Socket 類別
        Public mystring As String               '附帶訊息
    End Class

#Region " Definitions "
    ''' <summary>傳遞/接收資料長度</summary>
    Public Const RECEIVE_DATA_LENGTH As Int32 = 1024
    ''' <summary>接收資料間隔時間</summary>
    Public Const RECEIVE_DATA_DELAY As Int32 = 17

    Enum CodePage
        Big = 950
        ASCII = 1252
    End Enum

    Enum SockStatusInfo
        sckConnected
        sckError
        sckDisconnect
    End Enum

    Enum CommuicationMode
        TCPIP
        Telnet
    End Enum

#End Region

#Region " Properties "

    Private SocketObj As New CSState                    '宣告自訂 Socket 資訊包裝
    '
    Public mConnectedSockets As ArrayList               'Server 端目前已建立連線的 Socket 列表
    Public mIpAdress As IPAddress                       'IP Address
    Public mPort As Int32                               'Server 端 Port 號碼

    Public mServer As TcpListener                       'Socket 作為 Server 端物件包裝
    Public mClient As TcpClient                         'Socket 作為 Client 端物件包裝
    Public mCommuMode As CommuicationMode

    Public isConnected As Boolean                       '顯示目前是否連線中
    Public SockStatus As SockStatusInfo

    Public Event ConnectSuccess(sender As Object, ByVal e As DataEventArgs) ' ByVal Info As String)   '連線成功事件
    Public Event ConnectFail(sender As Object, ByVal e As DataEventArgs)      'Client 端連線逾時失敗事件
    Public Event Disconnected(sender As Object, ByVal e As DataEventArgs)     '連線中斷事件 (此事件 Client 無法分辨是正常斷線還是 Error 斷線)
    Public Event StopListen(sender As Object, ByVal e As DataEventArgs)       'Server 端停止連線監聽事件，並斷開所有連線
    Public Event ReciveData(sender As Object, ByVal e As DataEventArgs)    '收到傳輸資料事件

#End Region

#Region " Methods "

    ''' <summary>
    ''' 建立 Server 端連線功能
    ''' </summary>
    ''' <param name="PortNum">Port 號碼</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PortNum As Int32, Optional CMode As CommuicationMode = CommuicationMode.TCPIP)

        mPort = PortNum
        Me.mConnectedSockets = New ArrayList
        SockStatus = SockStatusInfo.sckDisconnect
        mCommuMode = CMode

    End Sub

    ''' <summary>
    ''' 建立 Client 端連線功能
    ''' </summary>
    ''' <param name="Ip">IP 位址</param>
    ''' <param name="PortNum">Port 號碼</param>
    Public Sub New(ByVal Ip As String, ByVal PortNum As Int32, Optional CMode As CommuicationMode = CommuicationMode.TCPIP)

        Dim tmpIp As IPAddress = IPAddress.Parse(Ip)
        mIpAdress = tmpIp
        mPort = PortNum
        SockStatus = SockStatusInfo.sckDisconnect
        mCommuMode = CMode

    End Sub

    ''' <summary>
    ''' 將 Socket KeepAlive 參數由數字轉換成 byte，方便套入 IOControl 引數
    ''' </summary>
    ''' <param name="OnOff">開 1 / 關 0</param>
    ''' <param name="KeepAliveTime">當開始 Listen 後多久時間開始偵測連線狀態 (ms)</param>
    ''' <param name="KeepAliveInterval">每次偵測的間隔時間(ms)</param>
    ''' <returns>回傳將設定值轉成 byte array 的 Socket KeepAlive 引數</returns>
    ''' <remarks></remarks>
    Private Function KeepAlive(ByVal OnOff As Int16, ByVal KeepAliveTime As Int16, ByVal KeepAliveInterval As Int16) As Byte()

        Dim buffer(11) As Byte

        Try
            BitConverter.GetBytes(OnOff).CopyTo(buffer, 0)
            BitConverter.GetBytes(KeepAliveTime).CopyTo(buffer, 4)
            BitConverter.GetBytes(KeepAliveInterval).CopyTo(buffer, 8)
        Catch ex As Exception
            Throw ex
        End Try

        Return buffer

    End Function

    ''' <summary>
    ''' 如果 Socket 建立成 Client，用來建立與伺服器連線
    ''' </summary>
    Public Sub ClientConnect()
        Try
            Dim myIPEndPoint As New IPEndPoint(IPAddress.Any, 0)
            mClient = New TcpClient(myIPEndPoint)
            Dim RemoteIpEndPoint As New IPEndPoint(mIpAdress, mPort)
            Try
                mClient.Connect(RemoteIpEndPoint)
            Catch ex As Exception
                'MsgBox(" [ClientConnect] - 連線失敗")
                RaiseEvent ConnectFail(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " 連線失敗"))
                SockStatus = SockStatusInfo.sckDisconnect
                isConnected = False
                Exit Sub
            End Try

            Do
                If mClient.Connected = True Then

                    Dim ReceiveThread As New Thread(AddressOf ReceiveData_Client)
                    ReceiveThread.IsBackground = True
                    ReceiveThread.Start(SocketObj)

                    Dim iIP As IPEndPoint = CType(mClient.Client.LocalEndPoint, IPEndPoint)

                    RaiseEvent ConnectSuccess(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " 已連線 Port:" & iIP.Port.ToString))
                    SockStatus = SockStatusInfo.sckConnected
                    isConnected = True
                    Exit Do
                End If
            Loop

        Catch ex As Exception
            'MsgBox(" [ClientConnect] - " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 中斷與 Server 連線
    ''' </summary>
    Public Sub ClientDisconnect()
        Try
            Me.mClient.Client.Disconnect(False)
            '使用下列方式也可以關閉連線，但是 Server 可能會認為是不正常斷線。
            'Me.mClient.Client.Close()

            RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " 手動中斷連線"))
            SockStatus = SockStatusInfo.sckDisconnect
            isConnected = False
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' 如果 Socket 建立成 Server，開始監聽 Client 連線
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartListen()
        Try
            Dim ListenThread As New Thread(AddressOf ServerListen)
            ListenThread.IsBackground = True
            ListenThread.Start(mPort)
        Catch ex As Exception
            'MsgBox(" [StartListen] - " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 停止 Server 監聽動作，並且切斷所有連結
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ServerStopListen()
        Try

            Do While Me.mConnectedSockets.Count > 0
                CType(mConnectedSockets.Item(0), Socket).Close()
                Me.mConnectedSockets.RemoveAt(0)
            Loop
            mServer.Stop()
            RaiseEvent StopListen(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " 中斷所有連線 Server 停止"))
            SockStatus = SockStatusInfo.sckDisconnect
            isConnected = False
        Catch ex As Exception
            'MsgBox(" [ServerStopListen] - " & ex.Message)
        End Try
    End Sub

#End Region

#Region " Function "

    ''' <summary>
    ''' 進行 Server 對指定 port 進行監聽連線要求訊息，如果發現連線要求則建立連線資訊及接收資料 task
    ''' </summary>
    ''' <param name="iPort"></param>
    ''' <remarks></remarks>
    Private Sub ServerListen(ByVal iPort As Object)
        Dim mSocket As Socket

        Try
            mServer = New TcpListener(IPAddress.Any, Integer.Parse(iPort))
            mServer.Start()
            Do
                mSocket = mServer.AcceptSocket
                If mSocket.Connected Then
                    SocketObj.myTcpListener = mServer
                    SocketObj.ClientSocket = mSocket
                    'SocketObj.myTcpListener.Server.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 1000, 1000), Nothing)
                    Dim tIP As String = CType(SocketObj.ClientSocket.RemoteEndPoint, IPEndPoint).Address.ToString
                    Dim tPort As String = CType(SocketObj.ClientSocket.RemoteEndPoint, IPEndPoint).Port.ToString
                    SocketObj.mystring = Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " 已連線"
                    RaiseEvent ConnectSuccess(Me, New DataEventArgs(SocketObj.mystring))
                    SockStatus = SockStatusInfo.sckConnected
                    isConnected = True
                    Dim ReceiveThread As New Thread(AddressOf ReceiveData_Server)
                    ReceiveThread.IsBackground = True
                    ReceiveThread.Start(SocketObj)
                    Me.mConnectedSockets.Add(SocketObj.ClientSocket)
                End If
            Loop

        Catch ex As SocketException
            'Status.Report(stt, " [ServerListen] - " & ex.Message)
        Catch ex As Exception
            'MsgBox(" [ServerListen] - " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 接收傳送到 Server 端的字串資料，並偵測是否發生斷線
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub ReceiveData_Server(ByVal state As Object)
        Dim myObj As New CSState
        myObj.ClientSocket = CType(state, CSState).ClientSocket
        myObj.myTcpListener = CType(state, CSState).myTcpListener
        myObj.mystring = ""
        Dim myNetworkStream As New NetworkStream(myObj.ClientSocket)
        Dim InBytesCount As Integer = 0
        Dim tIP As String = CType(myObj.ClientSocket.RemoteEndPoint, IPEndPoint).Address.ToString
        Dim tPort As String = CType(myObj.ClientSocket.RemoteEndPoint, IPEndPoint).Port.ToString

        While True
            Try
                If Me.mServer IsNot Nothing Then
                    If myObj.ClientSocket.Connected = False Then
                        Exit While
                    End If

                    Dim myReceiveBytes(RECEIVE_DATA_LENGTH) As Byte
                    InBytesCount = myNetworkStream.Read(myReceiveBytes, 0, myReceiveBytes.Length)

                    System.Threading.Thread.Sleep(RECEIVE_DATA_DELAY)
                    If InBytesCount = 0 Then
                        myObj.ClientSocket.Disconnect(True)
                        RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " 已中斷連線"))
                        SockStatus = SockStatusInfo.sckDisconnect
                        Exit While
                    End If
                    myObj.mystring = Encoding.GetEncoding(950).GetString(myReceiveBytes).TrimEnd().TrimStart()
                    RaiseEvent ReciveData(Me, New DataEventArgs(myObj.mystring))
                End If
            Catch ex As IO.IOException
                RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " Error 中斷連線"))
                SockStatus = SockStatusInfo.sckDisconnect
            Catch ex As Exception
                'MsgBox(" [ReceiveData_Server] - " & ex.Message)
                Exit Sub
            End Try
        End While
    End Sub

    ''' <summary>
    ''' 接收傳送到 Client 端的字串資料，並偵測是否發生斷線
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub ReceiveData_Client(ByVal state As Object)
        Dim RStr As String
        Dim InBytesCount As Integer = 0
        While True
            Try
                If Me.mClient IsNot Nothing Then
                    Dim myReceiveBytes(RECEIVE_DATA_LENGTH) As Byte
                    Dim NetStream As NetworkStream
                    If mClient.Connected = False Then
                        'RaiseEvent Disconnected(Now.ToString("yyyy/MM/dd HH:mm:ss") & " 連線已中斷")
                        Exit While
                    End If

                    NetStream = mClient.GetStream()
                    InBytesCount = NetStream.Read(myReceiveBytes, 0, myReceiveBytes.Length)

                    System.Threading.Thread.Sleep(RECEIVE_DATA_DELAY)
                    If InBytesCount = 0 Then
                        '無法觸發事件，原因不明
                        'RaiseEvent Disconnected(Now.ToString("yyyy/MM/dd HH:mm:ss") & " Server: 已中斷連線")
                        Exit While
                    End If

                    Dim combByte() As Byte = Nothing
                    For ii As Int32 = 0 To myReceiveBytes.Length - 1
                        If myReceiveBytes(ii) = 0 Then
                            ReDim combByte(ii - 1)
                            Array.Copy(myReceiveBytes, combByte, ii)
                            Exit For
                        End If
                    Next
                    If combByte Is Nothing Then
                        RStr = Encoding.GetEncoding(950).GetString(myReceiveBytes).TrimEnd().TrimStart()
                    Else
                        RStr = Encoding.GetEncoding(950).GetString(combByte).TrimEnd().TrimStart()
                    End If

                    RaiseEvent ReciveData(Me, New DataEventArgs(RStr))
                End If
            Catch ex As IO.IOException
                RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " Server: 已中斷連線"))
                SockStatus = SockStatusInfo.sckDisconnect
                isConnected = False
            Catch ex As Exception
                'MsgBox(" [ReceiveData_Client] - " & ex.Message)
            End Try
        End While

    End Sub

    ''' <summary>
    ''' 傳送字串資料至目標位置，Server 目前只能送給最後一個連線的位址
    ''' </summary>
    ''' <param name="Str"></param>
    ''' <returns></returns>
    ''' <remarks>之後可增加一個 task 來發送給所有連至 Server 端 ArrayList內所有位置的迴圈或指定某個位址</remarks>
    Public Function SendData(ByVal Str As String, Optional ByVal CP As CodePage = CodePage.Big) As Int32
        Try
            Dim SendDatas(RECEIVE_DATA_LENGTH) As Byte
            If Me.mServer IsNot Nothing Then
                Dim myNetworkStream As New NetworkStream(SocketObj.ClientSocket)
                SendDatas = Encoding.GetEncoding(CP).GetBytes(Trim(Str).TrimEnd())
                myNetworkStream.Write(SendDatas, 0, SendDatas.Length)
            End If

            If Me.mClient IsNot Nothing Then
                Dim myNetworkStream As NetworkStream

                If Me.mCommuMode = CommuicationMode.Telnet Then
                    SendDatas = Encoding.GetEncoding(CP).GetBytes(Trim(Str).TrimEnd() & vbCrLf)
                Else
                    SendDatas = Encoding.GetEncoding(CP).GetBytes(Trim(Str).TrimEnd())
                End If
                myNetworkStream = mClient.GetStream()
                myNetworkStream.Write(SendDatas, 0, SendDatas.Length)
            End If

        Catch ex As Exception
            'MsgBox(" [DataWrite] - " & ex.Message)
        End Try
        Return 0
    End Function

    Public Function SendDataByAscii(ByVal Str As String) As Int32
        Try
            Dim myNetworkStream As NetworkStream
            Dim R() As Byte = System.Text.Encoding.ASCII.GetBytes(Str)
            myNetworkStream = mClient.GetStream()
            myNetworkStream.Write(R, 0, R.Length)
        Catch ex As Exception

        End Try
        Return 0
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        Try
            If Me.mClient IsNot Nothing Then
                Me.ClientDisconnect()
            End If
            If Me.mServer IsNot Nothing Then
                Me.ServerStopListen()
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
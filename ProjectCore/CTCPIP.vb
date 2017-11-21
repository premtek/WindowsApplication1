Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports ProjectCore


Public Class CTCPIP
    Implements IDisposable

    ''' <summary>傳遞/接收資料長度</summary>
    Public Const RECEIVE_DATA_LENGTH As Int32 = 1024
    ''' <summary>接收資料間隔時間</summary>
    Public Const RECEIVE_DATA_DELAY As Int32 = 17

    Enum CodePage
        Big = 950
        ASCII = 1252
    End Enum

    Enum SockStatusInfo
        sckConnected = 0
        sckError = 1
        sckDisconnect = 2
    End Enum

    ''' <summary>
    ''' 自訂傳遞資訊包裝
    ''' </summary>
    ''' <remarks></remarks>
    Private Class CSState
        Public myTcpListener As TcpListener     '本地端監聽類別
        Public mystring As String               '附帶訊息
    End Class
    Private myTcpClient As TcpClient
    ''' <summary>命令發出事件
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CommandSended(ByVal sender As Object, ByVal e As DataEventArgs)
    ''' <summary>資料收抵事件
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnReceiveData(ByVal sender As Object, ByVal e As DataEventArgs)


    ''' <summary>
    ''' [ErrorMessage事件]
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnErrorMessage(ByVal sender As Object, ByVal e As DataEventArgs)

    ''' <summary>
    ''' 接收到的原始字串
    ''' </summary>
    ''' <remarks></remarks>
    Public ReceiveString As String

    Public SockStatus As SockStatusInfo

    Public mPort As Integer
    Public mHostName As String
    Dim mIpAddress As IPAddress

    Dim ListenThread As Thread


    Public Function SendCommand(ByVal msg As String) As Integer
        Try
            Dim RemoteIpEndPoint As New IPEndPoint(mIpAddress, mPort)
            Dim myNetworkStream As NetworkStream
            Dim myBytes As Byte()
            myBytes = Encoding.GetEncoding(950).GetBytes(Trim(msg))

            myNetworkStream = myTcpClient.GetStream()
            myNetworkStream.Write(myBytes, 0, myBytes.Length)
            Dim e As New DataEventArgs
            e.Data = msg
            RaiseEvent CommandSended(Me, e)
            Return 0
        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            Dim e As New DataEventArgs
            e.Data = ex.ToString
            RaiseEvent OnErrorMessage(Me, e)
            'MessageBox.Show(ex.ToString)
            Return -1
        End Try
    End Function


    ''' <summary>[關閉連線]</summary>
    ''' <remarks></remarks>
    Public Sub Disconnect()
        myTcpClient.Client.Disconnect(True)
        ListenThread.Abort()
        ListenThread = Nothing
    End Sub
    Public ReadOnly Property IsConnected As Boolean
        Get
            If myTcpClient Is Nothing Then '物件不存在
                Return False
            End If
            Return myTcpClient.Connected
        End Get
    End Property

    ''' <summary>[連線] </summary>
    ''' <param name="ip"></param>
    ''' <param name="port"></param>
    ''' <remarks></remarks>
    Public Sub Connect(ByVal ip As String, ByVal port As Integer)
        Try
            mIpAddress = IPAddress.Parse(ip)
            mHostName = ip
        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            Dim e As New DataEventArgs
            e.Data = "Server IP設定錯誤"
            RaiseEvent OnErrorMessage(Me, e)
            Exit Sub
        End Try
        mPort = port

        Dim myIPEndPoint As New IPEndPoint(IPAddress.Any, 0)
        myTcpClient = New TcpClient(myIPEndPoint)

        Dim RemoteIpEndPoint As New IPEndPoint(mIpAddress, mPort)
        Try
            myTcpClient.Connect(RemoteIpEndPoint)
            Do
                If myTcpClient.Connected = True Then
                    ListenThread = New Thread(AddressOf StartListen)
                    ListenThread.IsBackground = True
                    If ListenThread.IsAlive = False Then
                        ListenThread.Start()
                    End If
                    SockStatus = SockStatusInfo.sckConnected
                    Exit Do
                End If
            Loop

        Catch ex As Exception
            SockStatus = SockStatusInfo.sckError
            Dim e As New DataEventArgs
            e.Data = ex.ToString()
            RaiseEvent OnErrorMessage(Me, e)
        End Try
    End Sub

    Private Sub StartListen()
        Dim myObj As New CSState
        Do
            If myTcpClient Is Nothing Then
                Exit Do
            ElseIf myTcpClient.Client Is Nothing Then
                Exit Do
            ElseIf myTcpClient.Connected = True Then

                myObj.myTcpListener = Nothing 'myTcpListener
                'myObj.ClientSocket = Nothing
                myObj.mystring = Now.ToString("yyyy/MM/dd HH:mm:ss") & "已連線"
                'DisplayMsg1(myObj)
                Dim ReceiveThread As New Thread(AddressOf ReceiveData_Server)
                ReceiveThread.IsBackground = True
                ReceiveThread.Start(myObj)
                Exit Do
                'iCount += 1
            End If
        Loop
    End Sub
    Private Sub ReceiveData_Server(ByVal state As Object)
        If myTcpClient Is Nothing Then
            Exit Sub
        End If
        If myTcpClient.Connected = False Then
            Exit Sub
        End If
        Dim myObj As New CSState
        myObj.myTcpListener = CType(state, CSState).myTcpListener
        myObj.mystring = ""
        Dim myNetworkStream As NetworkStream

        myNetworkStream = myTcpClient.GetStream()
        Dim InBytesCount As Integer = 0

        While True
            Try
                'If Me.myTcpListener IsNot Nothing Then
                If myTcpClient.Connected = False Then
                    Exit While
                End If

                Dim myReceiveBytes(RECEIVE_DATA_LENGTH) As Byte
                InBytesCount = myNetworkStream.Read(myReceiveBytes, 0, myReceiveBytes.Length)

                System.Threading.Thread.Sleep(RECEIVE_DATA_DELAY)
                If InBytesCount = 0 Then
                    myTcpClient.Close()
                    'myObj.ClientSocket.Disconnect(True)
                    'RaiseEvent Disconnected(Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " 已中斷連線")
                    SockStatus = SockStatusInfo.sckDisconnect
                    Exit While
                End If
                myObj.mystring = Encoding.GetEncoding(CInt(CodePage.Big)).GetString(myReceiveBytes) '.TrimEnd().TrimStart()
                ReceiveString = myObj.mystring
                Dim e As New DataEventArgs
                e.Data = myObj.mystring
                RaiseEvent OnReceiveData(Me, e)
                'InvokeTextBox(txtStrRecieve, vbNewLine & myObj.mystring, True)
                'RaiseEvent ReciveData(myObj.mystring)
                'End If
            Catch ex As IO.IOException
                'RaiseEvent Disconnected(Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " Error 中斷連線")
                SockStatus = SockStatusInfo.sckDisconnect
            Catch ex As Exception
                SockStatus = SockStatusInfo.sckError
                Dim e As New DataEventArgs
                e.Data = " [ReceiveData_Server] - " & ex.Message
                RaiseEvent OnErrorMessage(Me, e)
                Exit Sub
            End Try
        End While
    End Sub

    Public Sub Save(ByVal fileName As String)
        SaveIniString("TCPIP", "IP", mHostName, fileName)
        SaveIniString("TCPIP", "Port", mPort, fileName)
    End Sub
    Public Sub Load(ByVal fileName As String)
        mHostName = ReadIniString("TCPIP", "IP", fileName, "192.168.0.10")
        mPort = ReadIniString("TCPIP", "Port", fileName, "8500")
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                myTcpClient.Close()
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

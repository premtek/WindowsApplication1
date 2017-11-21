' CtSockets.vb -�ϥ� TCP / IP �D�n�{��
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
' DESCRIPTION:	�ϥ� TCP / IP �D�n�{�ǡA����IP�BPort�覡�s���ǻ��r����
'
'               Server �ݺ�ť�ɩ� Client �ݶi��s�������� Port ���X�O�_���T�A
'               ���T���ܵ��� Client �ݤ@�ӥثe�L�ϥΪ� Port ���i��s���A�i�ƼƱ��� Client �s��
'
'               Client �ݶi��s���ɡA�ثe�L�k�o���������o�� Port ���X�A
'               �s�u���o��L�k�o�� Server �ݬO�_�٬O�~���ť�α����T��
'
'               �r���Ʊ������j�ɶ���ĳ25�@��H�W
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
    ''' �ۭq�ǻ���T�]��
    ''' </summary>
    ''' <remarks></remarks>
    Private Class CSState
        Public myTcpListener As TcpListener     '���a�ݺ�ť���O
        Public ClientSocket As Socket           '�^�ǻ��� Socket ���O
        Public mystring As String               '���a�T��
    End Class

#Region " Definitions "
    ''' <summary>�ǻ�/������ƪ���</summary>
    Public Const RECEIVE_DATA_LENGTH As Int32 = 1024
    ''' <summary>������ƶ��j�ɶ�</summary>
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

    Private SocketObj As New CSState                    '�ŧi�ۭq Socket ��T�]��
    '
    Public mConnectedSockets As ArrayList               'Server �ݥثe�w�إ߳s�u�� Socket �C��
    Public mIpAdress As IPAddress                       'IP Address
    Public mPort As Int32                               'Server �� Port ���X

    Public mServer As TcpListener                       'Socket �@�� Server �ݪ���]��
    Public mClient As TcpClient                         'Socket �@�� Client �ݪ���]��
    Public mCommuMode As CommuicationMode

    Public isConnected As Boolean                       '��ܥثe�O�_�s�u��
    Public SockStatus As SockStatusInfo

    Public Event ConnectSuccess(sender As Object, ByVal e As DataEventArgs) ' ByVal Info As String)   '�s�u���\�ƥ�
    Public Event ConnectFail(sender As Object, ByVal e As DataEventArgs)      'Client �ݳs�u�O�ɥ��Ѩƥ�
    Public Event Disconnected(sender As Object, ByVal e As DataEventArgs)     '�s�u���_�ƥ� (���ƥ� Client �L�k����O���`�_�u�٬O Error �_�u)
    Public Event StopListen(sender As Object, ByVal e As DataEventArgs)       'Server �ݰ���s�u��ť�ƥ�A���_�}�Ҧ��s�u
    Public Event ReciveData(sender As Object, ByVal e As DataEventArgs)    '����ǿ��ƨƥ�

#End Region

#Region " Methods "

    ''' <summary>
    ''' �إ� Server �ݳs�u�\��
    ''' </summary>
    ''' <param name="PortNum">Port ���X</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PortNum As Int32, Optional CMode As CommuicationMode = CommuicationMode.TCPIP)

        mPort = PortNum
        Me.mConnectedSockets = New ArrayList
        SockStatus = SockStatusInfo.sckDisconnect
        mCommuMode = CMode

    End Sub

    ''' <summary>
    ''' �إ� Client �ݳs�u�\��
    ''' </summary>
    ''' <param name="Ip">IP ��}</param>
    ''' <param name="PortNum">Port ���X</param>
    Public Sub New(ByVal Ip As String, ByVal PortNum As Int32, Optional CMode As CommuicationMode = CommuicationMode.TCPIP)

        Dim tmpIp As IPAddress = IPAddress.Parse(Ip)
        mIpAdress = tmpIp
        mPort = PortNum
        SockStatus = SockStatusInfo.sckDisconnect
        mCommuMode = CMode

    End Sub

    ''' <summary>
    ''' �N Socket KeepAlive �ѼƥѼƦr�ഫ�� byte�A��K�M�J IOControl �޼�
    ''' </summary>
    ''' <param name="OnOff">�} 1 / �� 0</param>
    ''' <param name="KeepAliveTime">��}�l Listen ��h�[�ɶ��}�l�����s�u���A (ms)</param>
    ''' <param name="KeepAliveInterval">�C�����������j�ɶ�(ms)</param>
    ''' <returns>�^�ǱN�]�w���ন byte array �� Socket KeepAlive �޼�</returns>
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
    ''' �p�G Socket �إߦ� Client�A�Ψӫإ߻P���A���s�u
    ''' </summary>
    Public Sub ClientConnect()
        Try
            Dim myIPEndPoint As New IPEndPoint(IPAddress.Any, 0)
            mClient = New TcpClient(myIPEndPoint)
            Dim RemoteIpEndPoint As New IPEndPoint(mIpAdress, mPort)
            Try
                mClient.Connect(RemoteIpEndPoint)
            Catch ex As Exception
                'MsgBox(" [ClientConnect] - �s�u����")
                RaiseEvent ConnectFail(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " �s�u����"))
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

                    RaiseEvent ConnectSuccess(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " �w�s�u Port:" & iIP.Port.ToString))
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
    ''' ���_�P Server �s�u
    ''' </summary>
    Public Sub ClientDisconnect()
        Try
            Me.mClient.Client.Disconnect(False)
            '�ϥΤU�C�覡�]�i�H�����s�u�A���O Server �i��|�{���O�����`�_�u�C
            'Me.mClient.Client.Close()

            RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " ��ʤ��_�s�u"))
            SockStatus = SockStatusInfo.sckDisconnect
            isConnected = False
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' �p�G Socket �إߦ� Server�A�}�l��ť Client �s�u
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
    ''' ���� Server ��ť�ʧ@�A�åB���_�Ҧ��s��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ServerStopListen()
        Try

            Do While Me.mConnectedSockets.Count > 0
                CType(mConnectedSockets.Item(0), Socket).Close()
                Me.mConnectedSockets.RemoveAt(0)
            Loop
            mServer.Stop()
            RaiseEvent StopListen(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " ���_�Ҧ��s�u Server ����"))
            SockStatus = SockStatusInfo.sckDisconnect
            isConnected = False
        Catch ex As Exception
            'MsgBox(" [ServerStopListen] - " & ex.Message)
        End Try
    End Sub

#End Region

#Region " Function "

    ''' <summary>
    ''' �i�� Server ����w port �i���ť�s�u�n�D�T���A�p�G�o�{�s�u�n�D�h�إ߳s�u��T�α������ task
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
                    SocketObj.mystring = Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " �w�s�u"
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
    ''' �����ǰe�� Server �ݪ��r���ơA�ð����O�_�o���_�u
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
                        RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " �w���_�s�u"))
                        SockStatus = SockStatusInfo.sckDisconnect
                        Exit While
                    End If
                    myObj.mystring = Encoding.GetEncoding(950).GetString(myReceiveBytes).TrimEnd().TrimStart()
                    RaiseEvent ReciveData(Me, New DataEventArgs(myObj.mystring))
                End If
            Catch ex As IO.IOException
                RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " IP: " & tIP & " Port: " & tPort & " Error ���_�s�u"))
                SockStatus = SockStatusInfo.sckDisconnect
            Catch ex As Exception
                'MsgBox(" [ReceiveData_Server] - " & ex.Message)
                Exit Sub
            End Try
        End While
    End Sub

    ''' <summary>
    ''' �����ǰe�� Client �ݪ��r���ơA�ð����O�_�o���_�u
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
                        'RaiseEvent Disconnected(Now.ToString("yyyy/MM/dd HH:mm:ss") & " �s�u�w���_")
                        Exit While
                    End If

                    NetStream = mClient.GetStream()
                    InBytesCount = NetStream.Read(myReceiveBytes, 0, myReceiveBytes.Length)

                    System.Threading.Thread.Sleep(RECEIVE_DATA_DELAY)
                    If InBytesCount = 0 Then
                        '�L�kĲ�o�ƥ�A��]����
                        'RaiseEvent Disconnected(Now.ToString("yyyy/MM/dd HH:mm:ss") & " Server: �w���_�s�u")
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
                RaiseEvent Disconnected(Me, New DataEventArgs(Now.ToString("yyyy/MM/dd HH:mm:ss") & " Server: �w���_�s�u"))
                SockStatus = SockStatusInfo.sckDisconnect
                isConnected = False
            Catch ex As Exception
                'MsgBox(" [ReceiveData_Client] - " & ex.Message)
            End Try
        End While

    End Sub

    ''' <summary>
    ''' �ǰe�r���ƦܥؼЦ�m�AServer �ثe�u��e���̫�@�ӳs�u����}
    ''' </summary>
    ''' <param name="Str"></param>
    ''' <returns></returns>
    ''' <remarks>����i�W�[�@�� task �ӵo�e���Ҧ��s�� Server �� ArrayList���Ҧ���m���j��Ϋ��w�Y�Ӧ�}</remarks>
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
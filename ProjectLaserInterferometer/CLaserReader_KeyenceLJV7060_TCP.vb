Imports ProjectCore

''' <summary>原CLaserInterferometer</summary>
''' <remarks></remarks>
Public Class CLaserReader_KeyenceLJV7060_TCP
    Implements ILaserReader

    ''' <summary>[雷射干涉儀資料陣列大小]</summary>
    ''' <remarks></remarks>
    Const gLaserInterferometerUBound = 4
    ''' <summary>[Ethernet settings structure] </summary>
    ''' <remarks></remarks>
    Private mEthernetConfig As LJV7IF_ETHERNET_CONFIG

    ''' <summary>[Device ID (fixed to 0)]</summary>
    ''' <remarks></remarks>
    Private Const mDeviceID = 0



    Dim mIsOpen As Boolean

    ''' <summary>[Ethernet Open]</summary>
    ''' <param name="IP"></param>
    ''' <param name="Port"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EthernetOpen(ByVal IP As String, ByVal Port As Integer) As Boolean Implements ILaserReader.EthernetOpen

        Try
            Dim Status As Rc = Rc.Ok

            '[說明]:Initialize the DLL
            Status = CType(NativeMethods.LJV7IF_Initialize(), Rc)

            If Not Status = Rc.Ok Then
                gSyslog.Save(gMsgHandler.GetMessage(Error_1014000), "Error_1014000", eMessageLevel.Error)
                mResult(0).Status = False
                mResult(0).Value = gMsgHandler.GetMessage(Error_1014000) '"Initialize the DLL Error"
                mIsOpen = False
                Return False
            End If

            'Return True

            'Dim Status As Rc = Rc.Ok
            Dim IPstring() As String

            '[說明]:IPstring
            IPstring = Split(IP, ".")
            If IPstring.GetUpperBound(0) <> 3 Then
                mResult(0).Status = False
                mResult(0).Value = gMsgHandler.GetMessage(Error_1014003) '"IP Error"
                gSyslog.Save(gMsgHandler.GetMessage(Error_1014003), "Error_1014003", eMessageLevel.Error)
                mIsOpen = False
                Return False
            End If

            '[說明]:Open the communication path
            '[說明]:USB
            '       NativeMethods.LJV7IF_UsbOpen(Define.DEVICE_ID)

            '[說明]:Generate the settings for Ethernet communication.
            With mEthernetConfig
                .abyIpAddress = New Byte() {Convert.ToByte(IPstring(0)), Convert.ToByte(IPstring(1)), Convert.ToByte(IPstring(2)), Convert.ToByte(IPstring(3))}
                .wPortNo = Convert.ToUInt16(Port)
            End With

            Status = CType(NativeMethods.LJV7IF_EthernetOpen(mDeviceID, mEthernetConfig), Rc)

            If Not Status = Rc.Ok Then
                gSyslog.Save(gMsgHandler.GetMessage(Error_1014000), "Error_1014000", eMessageLevel.Error)
                mResult(0).Status = False
                mResult(0).Value = gMsgHandler.GetMessage(Error_1014000) '"Ethernet Open Error"
                mIsOpen = False
                Return False
            End If

            mIsOpen = True
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014000), "Error_1014000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            mResult(0).Status = False
            mResult(0).STR = ex.ToString()
            mIsOpen = False
            Return False
        End Try

    End Function

    ''' <summary>
    ''' [Finalize the DLL and Close the communication]
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean Implements ILaserReader.Close

        Try
            Dim Status As Rc = Rc.Ok

            '[說明]:Close the communication
            Status = CType(NativeMethods.LJV7IF_CommClose(mDeviceID), Rc)
            If Not Status = Rc.Ok Then
                gSyslog.Save(gMsgHandler.GetMessage(Error_1014001), "Error_1014001", eMessageLevel.Error)
                gSyslog.Save("Close the communication Error", , eMessageLevel.Error)
                mResult(0).Status = False
                mResult(0).Value = "Close the communication Error"
                Return False
            End If

            '[說明]:Finalize the DLL
            Status = CType(NativeMethods.LJV7IF_Finalize(), Rc)
            If Not Status = Rc.Ok Then
                gSyslog.Save(gMsgHandler.GetMessage(Error_1014001), "Error_1014001", eMessageLevel.Error)
                gSyslog.Save("Finalize the DLL Error", , eMessageLevel.Error)
                mResult(0).Status = False
                mResult(0).Value = "Finalize the DLL Error"
                Return False
            End If

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014001), "Error_1014001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            mResult(0).Status = False
            mResult(0).STR = ex.ToString()

            Return False
        End Try

    End Function

    ''' <summary>讀取測高值</summary>
    ''' <param name="bit">如一對多時,指定Port</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValue(ByVal Mode As String, ByRef value As String, Optional bit As Integer = 0, Optional ByVal waitReturn As Boolean = False) As Boolean Implements ILaserReader.GetValue
        'Dim value As String '傳回資料
        Dim intI As Integer
        Dim Data(gLaserInterferometerUBound) As Double
        Dim Status As Rc = Rc.Ok
        Dim measureData As LJV7IF_MEASURE_DATA() = New LJV7IF_MEASURE_DATA(NativeMethods.MeasurementDataCount) {}

        '[說明]:讀取資料
        Status = CType(NativeMethods.LJV7IF_GetMeasurementValue(mDeviceID, measureData), Rc)
        If Not Status = Rc.Ok Then
            value = "" '讀取失敗,無資料
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error) '雷射干涉儀1讀值失敗!
            Return value
            'Return False
        End If

        '[說明]:
        value = ""
        For intI = 0 To gLaserInterferometerUBound
            Data(intI) = CDbl(measureData(intI).fValue)
            value += Data(intI).ToString & "|"
        Next

        Return True

    End Function

    ''' <summary>
    ''' [GetVersion]
    ''' </summary>
    ''' <param name="Version"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVersion(ByRef Version As String) As Boolean Implements ILaserReader.GetVersion

        Version = NativeMethods.LJV7IF_GetVersion().ToString("x")
        Return True

    End Function

    ''' <summary>
    ''' [RebootController]
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RebootController() As Boolean Implements ILaserReader.RebootController

        Dim Status As Rc = Rc.Ok

        Status = NativeMethods.LJV7IF_RebootController(mDeviceID)

        If Not Status = Rc.Ok Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' [切換干涉儀Program] 
    ''' </summary>
    ''' <param name="ProgramID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeProgram(ByVal ProgramID As Integer) As Boolean Implements ILaserReader.ChangeProgram

        Dim Status As Rc = Rc.Ok

        Status = NativeMethods.LJV7IF_ChangeActiveProgram(mDeviceID, CByte(ProgramID))

        If Not Status = Rc.Ok Then
            Return False
        End If

        Return True

    End Function


    Public ReadOnly Property IsBusy As Boolean Implements ILaserReader.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsTimeOut As Boolean Implements ILaserReader.IsTimeOut
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property PortIsOpen As Boolean Implements ILaserReader.PortIsOpen
        Get
            Return mIsOpen
        End Get
    End Property

    Public Property TimeoutTimer As Integer Implements ILaserReader.TimeoutTimer

    Public Function Initial(PortName As String, BaudRate As String) As Boolean Implements ILaserReader.Initial
        gSyslog.Save("Initial Function is Not Supported at CLaserReader_KeyenceLJV7060_TCP.")
        Return False
    End Function

    'Public Sub ResetState() Implements ILaserReader.ResetState

    'End Sub

    ''' <summary>接收資料結果</summary>
    ''' <remarks></remarks>
    Dim mResult(3) As sReceiveStatus
    Public ReadOnly Property Result(ByVal channelNo As Integer) As sReceiveStatus Implements ILaserReader.Result
        Get
            Return mResult(channelNo)
        End Get
    End Property
End Class

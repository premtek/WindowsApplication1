Imports ProjectCore
Imports ProjectIO

Public Class CLaserReader_KeyenceILS065Voltage
    Implements ILaserReader


    Public Function ChangeProgram(ProgramID As Integer) As Boolean Implements ILaserReader.ChangeProgram
        gSyslog.Save("ChangeProgram Function Not Supported.")
        Return True
    End Function

    Public Function Close() As Boolean Implements ILaserReader.Close
        gSyslog.Save("ChangeProgram Function Not Supported.")
        Return True
    End Function


    Public Function EthernetOpen(IP As String, Port As Integer) As Boolean Implements ILaserReader.EthernetOpen
        gSyslog.Save("EthernetOpen Function Not Supported.")
        Return True
    End Function

    Public Function GetValue(ByVal Mode As String, ByRef value As String, Optional aiIndex As Integer = 0, Optional ByVal waitReturn As Boolean = False) As Boolean Implements ILaserReader.GetValue
        value = gAICollection.Value(aiIndex)
        Return True
    End Function

    Public Function GetVersion(ByRef Version As String) As Boolean Implements ILaserReader.GetVersion
        gSyslog.Save("GetVersion Function Not Supported.")
        Return True
    End Function

    Public Function RebootController() As Boolean Implements ILaserReader.RebootController
        gSyslog.Save("RebootController Function Not Supported.")
        Return True
    End Function

    Public ReadOnly Property IsTimeOut As Boolean Implements ILaserReader.IsTimeOut
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property PortIsOpen As Boolean Implements ILaserReader.PortIsOpen
        Get
            Return True
        End Get
    End Property

    Public Property TimeoutTimer As Integer Implements ILaserReader.TimeoutTimer

    Public ReadOnly Property IsBusy As Boolean Implements ILaserReader.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Function Initial(PortName As String, BaudRate As String) As Boolean Implements ILaserReader.Initial
        gSyslog.Save("Initial Function is Not Supported at CLaserReader_KeyenceILS065Voltage.")
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

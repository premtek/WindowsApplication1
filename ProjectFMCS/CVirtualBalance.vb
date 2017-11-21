Imports ProjectCore

Public Class CVirtualBalance
    Implements IBalance

    Public Sub Close() Implements IBalance.Close

    End Sub

    Public Property ErrMsg As String Implements IBalance.ErrMsg

    Public Function Initial(PortName As String, baudRate As String, ByVal TimeoutTimer As Double) As Boolean Implements IBalance.Initial
        Return True
    End Function

    Public ReadOnly Property IsBusy As Boolean Implements IBalance.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsInitialOK As Boolean Implements IBalance.IsInitialOK
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsTimeOut As Boolean Implements IBalance.IsTimeOut
        Get
            Return False
        End Get
    End Property

    Public Event OnDataRecieved(sender As Object, ByVal e As DataEventArgs) Implements IBalance.OnDataRecieved

    Public ReadOnly Property PortIsOpen As Boolean Implements IBalance.PortIsOpen
        Get
            Return True
        End Get
    End Property

    Public Function RequestCurrentValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestCurrentValue
        Return True
    End Function

    Public Function RequestStableValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestStableValue
        Return True
    End Function

    Public Function Rezero(Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.Rezero
        Return True
    End Function

    Public Function RequestReStart(Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.RequestReStart
        Return True
    End Function

    Public Function SendCommandToSerialPort(strCmd As String, ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean Implements IBalance.SendCommandToSerialPort
        Return True
    End Function

    Public Property TimeoutTimer As Integer Implements IBalance.TimeoutTimer

    Dim mResult As sReceiveStatus
    Public ReadOnly Property Result As sReceiveStatus Implements IBalance.Result
        Get
            Return mResult
        End Get
    End Property

    Public Function Reset() As Boolean Implements IBalance.Reset
        Return True
    End Function
End Class

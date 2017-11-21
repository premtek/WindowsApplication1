Imports ProjectCore

Public Class CVirtualFMCS
    Implements IFMCS

    Public Sub Close() Implements IFMCS.Close

    End Sub

    Public Property ErrMsg As String Implements IFMCS.ErrMsg

    Public Property FMCSIndex As Integer Implements IFMCS.ValveIndex

    Public Function Initial(portName As String, baudRate As Integer) As Boolean Implements IFMCS.Initial
        Return True
    End Function

    Public ReadOnly Property IsBusy As Boolean Implements IFMCS.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsInitialOK As Boolean Implements IFMCS.IsInitialOK
        Get
            Return True
        End Get
    End Property

    Public Function IsPortOpen() As Boolean Implements IFMCS.IsPortOpen
        Return True
    End Function

    Public ReadOnly Property IsTimeOut As Boolean Implements IFMCS.IsTimeOut
        Get
            Return False
        End Get
    End Property

    Public Event OnRecievedData(sender As Object, ByVal e As FMCSEventArgs) Implements IFMCS.OnRecievedData

    Public Property OutputAvgFlow As Double Implements IFMCS.OutputAvgFlow

    Public Property OutputVolume As Double Implements IFMCS.OutputVolume

    Public Function RecordEnd(intX As Integer, intY As Integer) As String Implements IFMCS.RecordEnd
        Return True
    End Function

    Public Function RecordStart(intX As Integer, intY As Integer) As String Implements IFMCS.RecordStart
        Return True
    End Function

    Public Property TimeoutTimer As Integer Implements IFMCS.TimeoutTimer
End Class

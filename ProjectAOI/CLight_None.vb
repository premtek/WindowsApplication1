Imports System.IO.Ports
Imports ProjectCore

Public Class CLight_None
    Implements ILightInterface

    Public Function Initial(portName As String, baudRate As Integer, dataBits As Integer) As Boolean Implements ILightInterface.Initial

        For i As Integer = 0 To 3
            mResult(i).Status = True
            mResult(i).Value = 0
        Next

        Return True
    End Function

    Public Function GetLightValue(ChNo As Integer, ByRef value As Integer, Optional waitReturn As Boolean = False) As String Implements ILightInterface.GetLightValue
        Return 0
    End Function

    Public Function SetLightValue(ChNo As Integer, lightValue As Integer, Optional waitReturn As Boolean = False) As String Implements ILightInterface.SetLightValue
        Return 0
    End Function

    Dim mIsBusy As Boolean = False
    Public ReadOnly Property IsBusy As Boolean Implements ILightInterface.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property

    Public ReadOnly Property IsTimeOut As Boolean Implements ILightInterface.IsTimeOut
        Get
            Return False
        End Get
    End Property
    Dim mResult(3) As sReceiveStatus
    Public ReadOnly Property Result(channelNo As Integer) As ProjectCore.sReceiveStatus Implements ILightInterface.Result
        Get
            Return mResult(channelNo)
        End Get
    End Property



    Public Function Close() As Boolean Implements ILightInterface.Close
        Return True
    End Function

End Class

Imports ProjectCore

Public Class CElectroPneumaticValveVirtual
    Implements IElectroPneumaticValve

    Sub New()
        mResult.Status = True
    End Sub

    Public Property ChannelPerCard As Integer Implements IElectroPneumaticValve.ChannelPerCard

    Public Function Close() As Boolean Implements IElectroPneumaticValve.Close
        Return True
    End Function

    Public Function GetValue(ByRef value As Decimal, Optional ByVal waitReturn As Boolean = False, Optional stationNo As Integer = -1) As Boolean Implements IElectroPneumaticValve.GetValue
        Return True
    End Function

    Public Function Initial(ByRef item As IO.Ports.SerialPort) As Boolean Implements IElectroPneumaticValve.Initial
        Return True
    End Function

    Public Function SetValue(value As Decimal, Optional ByVal waitReturn As Boolean = False, Optional stationNo As Integer = -1) As Boolean Implements IElectroPneumaticValve.SetValue
        Return True
    End Function

    Public Property Max_Mpa As Decimal = 0.5 Implements IElectroPneumaticValve.Max_Mpa

    Public Property Min_Mpa As Decimal = 0 Implements IElectroPneumaticValve.Min_Mpa

    Dim mResult As sReceiveStatus

    Public ReadOnly Property Result As sReceiveStatus Implements IElectroPneumaticValve.Result
        Get
            Return mResult
        End Get
    End Property

    ''' <summary>永不TimeOut</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut As Boolean Implements IElectroPneumaticValve.IsTimeOut
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' 永不忙碌
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy As Boolean Implements IElectroPneumaticValve.IsBusy
        Get
            Return False
        End Get
    End Property
End Class

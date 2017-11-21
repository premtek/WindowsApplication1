Imports ProjectIO
Imports ProjectCore

Public Class cls800aqSMEMA : Implements ISMEMA

    Public ReadOnly Property IsLoaderReady As Boolean Implements ISMEMA.IsLoaderReady
        Get
            Return gDICollection.GetState(enmDI.BoardAvailable, True)
        End Get
    End Property

    Public Property IsReadyToRecieve As Boolean Implements ISMEMA.IsReadyToRecieve
        Get
            Return gDOCollection.GetState(enmDO.MachineReadyToRecieve)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.MachineReadyToRecieve, value)
        End Set
    End Property

    Public Property IsReadyToSend As Boolean Implements ISMEMA.IsReadyToSend
        Get
            Return gDOCollection.GetState(enmDO.BoardAvailable2)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.BoardAvailable2, value)
        End Set
    End Property

    Public ReadOnly Property IsUnloaderReady As Boolean Implements ISMEMA.IsUnloaderReady
        Get
            Return gDICollection.GetState(enmDI.MachineReadyToRecieve2, True)
        End Get
    End Property
End Class

Public Class cls500sdSMEMA : Implements ISMEMA

    Public ReadOnly Property IsLoaderReady As Boolean Implements ISMEMA.IsLoaderReady
        Get
            Return gDICollection.GetState(enmDI.BoardAvailable, True)
        End Get
    End Property

    Public Property IsReadyToRecieve As Boolean Implements ISMEMA.IsReadyToRecieve
        Get
            Return gDOCollection.GetState(enmDO.MachineReadyToRecieve)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.MachineReadyToRecieve, value)
        End Set
    End Property

    Public Property IsReadyToSend As Boolean Implements ISMEMA.IsReadyToSend
        Get
            Return gDOCollection.GetState(enmDO.BoardAvailable)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.BoardAvailable, value)
        End Set
    End Property

    Public ReadOnly Property IsUnloaderReady As Boolean Implements ISMEMA.IsUnloaderReady
        Get
            Return gDICollection.GetState(enmDI.MachineReadyToRecieve, True)
        End Get
    End Property
End Class



Public Class CTriggerBoard_Virtual
    Implements ITriggerBoard

    Public ReadOnly Property CycleArray As sReceiveStatus Implements ITriggerBoard.CycleArray
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property CycleRecipe As sReceiveStatus Implements ITriggerBoard.CycleRecipe
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property DispenseRun As sReceiveStatus Implements ITriggerBoard.DispenseRun
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property DispenseCounts As sReceiveStatus Implements ITriggerBoard.DispenseCounts
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = "0"
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property VisionCounts As sReceiveStatus Implements ITriggerBoard.VisionCounts
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = "0"
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property DummyRun As sReceiveStatus Implements ITriggerBoard.DummyRun
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property ErrorCode As sReceiveStatus Implements ITriggerBoard.ErrorCode
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property JetParameter As sReceiveStatus Implements ITriggerBoard.JetParameter
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property JetParamRecipe As sReceiveStatus Implements ITriggerBoard.JetParamRecipe
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property VisionRecipe As sReceiveStatus Implements ITriggerBoard.VisionRecipe
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property JetRecipe As sReceiveStatus Implements ITriggerBoard.JetRecipe
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property JetRecipeUseTransmissionResuming As sReceiveStatus Implements ITriggerBoard.JetRecipeUseTransmissionResuming
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property PitchRecipe As sReceiveStatus Implements ITriggerBoard.PitchRecipe
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property ResetAlarm As sReceiveStatus Implements ITriggerBoard.ResetAlarm
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property Parameter As sReceiveStatus Implements ITriggerBoard.Parameter
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property Version As sReceiveStatus Implements ITriggerBoard.Version
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ""
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public ReadOnly Property Temperature As sReceiveStatus Implements ITriggerBoard.Temperature
        Get
            Dim mStringStatus As sReceiveStatus
            mStringStatus.STR = ",,,"
            mStringStatus.Status = True
            mStringStatus.Value = ""
            Return mStringStatus
        End Get
    End Property

    Public Property ErrMsg As String Implements ITriggerBoard.ErrMsg
    Public Property TimeoutTimes As Integer Implements ITriggerBoard.TimeoutTimes

    Public ReadOnly Property PortIsOpen As Boolean Implements ITriggerBoard.PortIsOpen
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsBusy As Boolean Implements ITriggerBoard.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsInitialOK As Boolean Implements ITriggerBoard.IsInitialOK
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property TransmissionResumingOfStepCount As Integer Implements ITriggerBoard.TransmissionResumingOfStepCount
        Get
            Return 20
        End Get
    End Property

    Public ReadOnly Property IsTimeOut As Boolean Implements ITriggerBoard.IsTimeOut
        Get
            Return False
        End Get
    End Property

    Public Function GetPortIDs(ByRef portIDs() As String) As Boolean Implements ITriggerBoard.GetPortIDs
        Return True
    End Function

    Public Sub Dispose() Implements ITriggerBoard.Dispose

    End Sub
    Public Sub Close() Implements ITriggerBoard.Close

    End Sub

    Public Function Initial(portName As String, baudRate As String) As Boolean Implements ITriggerBoard.Initial
        Return True
    End Function

    Public Function SendCommandToSerialPort(commandBtye() As Byte) As Boolean Implements ITriggerBoard.SendCommandToSerialPort
        Return True
    End Function

    Public Function AddJetParamRecipe(is1stStep As Boolean, zoneNo As Integer, patternStep As sTriggerJCmdStep, isLastStep As Boolean, Optional parameter As sTriggerJCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddJetParamRecipe
        Return True
    End Function

    Public Function SetJetParamRecipe(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetParamRecipe
        Return True
    End Function

    Public Function AddJetRecipe(is1stStep As Boolean, patternStep As sTriggerFCmdStep, isLastStep As Boolean, Optional parameter As sTriggerFCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddJetRecipe
        Return True
    End Function

    Public Function SetJetRecipe(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetRecipe
        Return True
    End Function
    Public Function AddJetRecipeUseTransmissionResuming(is1stStep As Boolean, patternStep As sTriggerFCmdStep, isLastStep As Boolean, Optional parameter As sTriggerFCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddJetRecipeUseTransmissionResuming
        Return True
    End Function

    Public Function SetJetRecipeByTransmissionResuming(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetRecipeByTransmissionResuming
        Return True
    End Function
    Public Function SetJetParameter(parameter As sTriggerGCmdParam, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetParameter
        Return True
    End Function

    Public Function SetCycleRecipe(parameter As sTriggerTPCmdParam, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetCycleRecipe
        Return True
    End Function

    Public Function SetPitchRecipe(parameter As sTriggerTPCmdParam, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetPitchRecipe
        Return True
    End Function

    Public Function SetDummyRun(dispType As enmTriggerDispType, valveNo As ProjectCore.eValveWorkMode, zoneNo As Integer, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetDummyRun
        Return True
    End Function

    Public Function SetDispenseRun(dispType As enmTriggerDispType, valveNo As ProjectCore.eValveWorkMode, zoneNo As Integer, degree As Decimal, reworkDotCounts As Integer, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetDispenseRun
        Return True
    End Function

    Public Function SetPressure(valve As ProjectCore.eValveWorkMode, value As Decimal, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetPressure
        Return True
    End Function

    Public Function SetTempture(valve As ProjectCore.eValveWorkMode, value As Decimal, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetTempture
        Return True
    End Function

    Public Function SetValvePower(valve As ProjectCore.eValveWorkMode, powerOn As Boolean, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetValvePower
        Return True
    End Function

    Public Function SetPurge(valve As ProjectCore.eValveWorkMode, purgeOn As Boolean, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetPurge
        Return True
    End Function

    '20170920
    Public Function SetTemptureOnOff(valve As ProjectCore.eValveWorkMode, TemptureOn As Boolean, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetTemptureOnOff
        Return True
    End Function

    Public Function SetResetAlarm(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetResetAlarm
        Return True
    End Function

    Public Function GetDispCycle(Optional waitReturn As Boolean = False, Optional ByRef cycleArray As String = "") As Boolean Implements ITriggerBoard.GetDispCycle
        cycleArray = ""
        Return True
    End Function

    Public Function GetDispenseCounts(Optional waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean Implements ITriggerBoard.GetDispenseCounts
        dotCounts = 0
        Return True
    End Function

    Public Function GetVisionCounts(Optional waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean Implements ITriggerBoard.GetVisionCounts
        dotCounts = 0
        Return True
    End Function

    Public Function GetErrorCode(Optional waitReturn As Boolean = False, Optional ByRef errorCode As String = "") As Boolean Implements ITriggerBoard.GetErrorCode
        errorCode = ""
        Return True
    End Function

    Public Function GetVersion(Optional waitReturn As Boolean = False, Optional ByRef version As String = "") As Boolean Implements ITriggerBoard.GetVersion
        version = ""
        Return True
    End Function

    Public Function AddVisionRecipe(is1stStep As Boolean, patternStep As sTriggerVisionCmdStep, isLastStep As Boolean, Optional parameter As sTriggerVisionCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddVisionRecipe
        Return True
    End Function

    Public Function SetVisionRecipe(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetVisionRecipe
        Return True
    End Function

    Public Function GetTemperature(Optional waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean Implements ITriggerBoard.GetTemperature
        Return True
    End Function

    '20171010
    Public Function GetSwitch(Optional waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean Implements ITriggerBoard.GetSwitch
        Return True
    End Function

End Class

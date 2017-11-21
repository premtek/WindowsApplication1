
Public Class CVirtualValveController
    Implements IValveControllerInterface
    'Virtual Return
    Private msVirtualPicoResponseData As sPicoValveCommandResponseData = New sPicoValveCommandResponseData
    Private meCommandState As enmCommandState
    Private msPicoValveHeaterStatus As sPicoValveHeaterStatus = New sPicoValveHeaterStatus

    Private msPicoValveStatus As sPicoValveStatus = New sPicoValveStatus

    Private mVirtualValue As String = "20"
    Private mVirtualString As String = ""
    Public Sub New()
        msVirtualPicoResponseData.STR = mVirtualString
        msVirtualPicoResponseData.value = mVirtualValue
        msVirtualPicoResponseData.Status = True

        meCommandState = enmCommandState.Success

        msPicoValveHeaterStatus.sACT = mVirtualValue
        msPicoValveHeaterStatus.sMode = mVirtualString
        msPicoValveHeaterStatus.sSet = mVirtualValue
        msPicoValveHeaterStatus.sStack = mVirtualValue
        msPicoValveHeaterStatus.Status = True

        msPicoValveStatus.sCloseVoltage = mVirtualValue
        msPicoValveStatus.sCount = mVirtualValue
        msPicoValveStatus.sCycle = mVirtualValue
        msPicoValveStatus.sDwnRampTime = mVirtualValue
        msPicoValveStatus.sMode = mVirtualString
        msPicoValveStatus.sNumShots = mVirtualValue
        msPicoValveStatus.sProfileFall = mVirtualValue
        msPicoValveStatus.sProfileRise = mVirtualValue
        msPicoValveStatus.sPulse = mVirtualValue
        msPicoValveStatus.sStroke = mVirtualValue
        msPicoValveStatus.Status = mVirtualString
        msPicoValveStatus.sUpRampTime = mVirtualValue
        msPicoValveStatus.sValvePower = mVirtualValue

    End Sub

    Public ReadOnly Property AlarmInfo As sPicoValveCommandResponseData Implements IValveControllerInterface.AlarmInfo
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property CloseProfile As sPicoValveCommandResponseData Implements IValveControllerInterface.CloseProfile
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property CloseVoltage As sPicoValveCommandResponseData Implements IValveControllerInterface.CloseVoltage
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property DisplayInfo As sPicoValveCommandResponseData Implements IValveControllerInterface.DisplayInfo
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property OpenTime As sPicoValveCommandResponseData Implements IValveControllerInterface.OpenTime
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property CloseTime As sPicoValveCommandResponseData Implements IValveControllerInterface.CloseTime
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property HeaterMode As sPicoValveCommandResponseData Implements IValveControllerInterface.HeaterMode
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property HeaterStatus As sPicoValveHeaterStatus Implements IValveControllerInterface.HeaterStatus
        Get
            Return msPicoValveHeaterStatus
        End Get
    End Property

    Public ReadOnly Property HeaterTemperature As sPicoValveCommandResponseData Implements IValveControllerInterface.HeaterTemperature
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property IsBusy As Boolean Implements IValveControllerInterface.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsTimeOut As Boolean Implements IValveControllerInterface.IsTimeOut
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property OpenProfile As sPicoValveCommandResponseData Implements IValveControllerInterface.OpenProfile
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property PortIsOpen As Boolean Implements IValveControllerInterface.PortIsOpen
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ResetAlarm As sPicoValveCommandResponseData Implements IValveControllerInterface.ResetAlarm
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property Stroke As sPicoValveCommandResponseData Implements IValveControllerInterface.Stroke
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValveCycleONOFFStatus As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveCycleONOFFStatus
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValveDispenseCount As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveDispenseCount
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValveMode As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveMode
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValveOffTime As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveOffTime
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValveOnTime As sPicoValveCommandResponseData Implements IValveControllerInterface.ValveOnTime
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValvePlok As sPicoValveCommandResponseData Implements IValveControllerInterface.ValvePlok
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValvePower As sPicoValveCommandResponseData Implements IValveControllerInterface.ValvePower
        Get
            Return msVirtualPicoResponseData
        End Get
    End Property

    Public ReadOnly Property ValveStatus As sPicoValveStatus Implements IValveControllerInterface.ValveStatus
        Get
            Return msPicoValveStatus
        End Get
    End Property


    Public Property TimeoutTimer As Integer Implements IValveControllerInterface.TimeoutTimer
    Public Property ErrMsg As String Implements IValveControllerInterface.ErrMsg
    Public Property mCommandResponse As TextBox Implements IValveControllerInterface.mCommandResponse


    Public Function Initial(PortName As String, BaudRate As String) As Boolean Implements IValveControllerInterface.Initial
        Return True
    End Function

    Public Sub Dispose() Implements IValveControllerInterface.Dispose

    End Sub

    Public Sub Close() Implements IValveControllerInterface.Close

    End Sub

    Public Function GetAlarmInfo(Optional ByRef sAlarmInfo As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetAlarmInfo
        Return meCommandState
    End Function

    Public Function GetDisplayInfo(Optional ByRef sDisplayInfo As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetDisplayInfo
        Return meCommandState
    End Function

    Public Function GetHeaterStatus(Optional ByRef sHeaterStatus As sPicoValveHeaterStatus = Nothing, Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetHeaterStatus
        Return meCommandState
    End Function

    Public Function GetPortIDs(ByRef PortIDs() As String) As Boolean Implements IValveControllerInterface.GetPortIDs
        Return True
    End Function

    Public Function GetValveStatus(Optional ByRef sVavleStatus As sPicoValveStatus = Nothing, Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.GetValveStatus
        Return meCommandState
    End Function

    Public Function SetOpenTime(iOpenTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetOpenTime
        Return meCommandState
    End Function

    Public Function SetCloseTime(iCloseTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCloseTime
        Return meCommandState
    End Function

    Public Function ResetAlarmStatus() As enmCommandState Implements IValveControllerInterface.ResetAlarmStatus
        Return meCommandState
    End Function

    Public Function SendCommandToSerialPort(CommandBtye As String) As Boolean Implements IValveControllerInterface.SendCommandToSerialPort
        Return meCommandState
    End Function

    Public Function SetCloseProfile(iSelect As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCloseProfile
        Return meCommandState
    End Function

    Public Function SetCloseVoltage(iCloseVoltage As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCloseVoltage
        Return meCommandState
    End Function

    Public Function SetCycleOnOff(bCycleOnOff As Boolean, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetCycleOnOff
        Return meCommandState
    End Function

    Public Function SetHeaterMode(iHeatherMode As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetHeaterMode
        Return meCommandState
    End Function

    Public Function SetHeaterTemperature(dHeaterTemperature As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetHeaterTemperature
        Return meCommandState
    End Function

    Public Function SetOpenProfile(iSelect As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetOpenProfile
        Return meCommandState
    End Function

    Public Function SetStrokeValve(dStrokeValve As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetStrokeValve
        Return meCommandState
    End Function

    Public Function SetValveCycleTime(dVavleOffTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveCycleTime
        Return meCommandState
    End Function

    Public Function SetValveDispenseCount(iDispenseCount As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveDispenseCount
        Return meCommandState
    End Function

    Public Function SetValveMode(eModeType As enmValveModeType, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveMode
        Return meCommandState
    End Function

    Public Function SetValveOffTime(dVavleOffTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveOffTime
        Return meCommandState
    End Function

    Public Function SetValveOnTime(dVavleOnTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValveOnTime
        Return meCommandState
    End Function

    Public Function SetValvePlok(iPlokTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValvePlok
        Return meCommandState
    End Function

    Public Function SetValvePower(bValvePower As Boolean, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState Implements IValveControllerInterface.SetValvePower
        Return meCommandState
    End Function

End Class

Imports ProjectCore
Imports ProjectIO

Imports ProjectMotion
Public Class clsWetcoTilt

#Region "Private Property"
    Private TiltAL As clsTiltBase
    Private TiltAR As clsTiltBase
    Private TiltBL As clsTiltBase
    Private TiltBR As clsTiltBase
    Private TiltController As clsTiltBase
    Private thread As System.Threading.Thread = New Threading.Thread(AddressOf Action)
    Private ResumeEvent As System.Threading.AutoResetEvent = New System.Threading.AutoResetEvent(False)
    'Private mFileName As String = "D:\\testTiltParam\\" & "tiltparam.ini"
    Private mTask As enmTiltMotion
    Private mTilt As enmTiltIndex
    Private mRunningStatus As enmRunningStatus
#End Region

#Region "Public Property"
    Public ReadOnly Property _NowTilt() As clsTiltBase
        Get
            Return TiltController
        End Get
    End Property

    'Public Property _FileName() As String
    '    Get
    '        Return mFileName
    '    End Get
    '    Set(ByVal value As String)
    '        mFileName = value
    '    End Set
    'End Property

    Property _Tilt() As String
        Get
            Return mTilt
        End Get
        Set(ByVal value As String)
            mTilt = value
        End Set
    End Property

    ReadOnly Property GetRunningStatus() As String
        Get
            Return mRunningStatus
        End Get
    End Property
#End Region

#Region "Public Method"
    Public Sub SavePos(ByVal Tilt As enmTiltIndex, ByVal pos As TiltPos)
        SetNowTilt(Tilt)
        Dim fName As String = Application.StartupPath & "\System\Tilt" & Tilt.ToString
        TiltController.Save(fName, pos)
    End Sub
    Public Function LoadPos(ByVal Tilt As enmTiltIndex) As TiltPos
        SetNowTilt(Tilt)
        Dim fName As String = Application.StartupPath & "\System\Tilt" & Tilt.ToString
        Return TiltController.Load(fName)
    End Function

    Public Sub New()
        Select Case gSSystemParameter.enmMachineType
            Case enmMachineType.DCSW_800AQ
            Case enmMachineType.eDTS_2S2V
            Case enmMachineType.eDTS300A
            Case enmMachineType.eDTS330A
            Case enmMachineType.eDTS330ACR1
        End Select

        TiltAL = New clsTiltValveA_L()
        TiltAR = New clsTiltValveA_R()
        TiltBL = New clsTiltValveB_L()
        TiltBR = New clsTiltValveB_R()
        TiltController = New clsTiltBase
        ResetRunningStatus()
        ResetMotionCommand()
        'thread.Start()
        Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Action))
    End Sub
    Public Sub ResetRunningStatus()
        mRunningStatus = enmRunningStatus.None
    End Sub
    Public Sub ResetMotionCommand()
        mTask = enmTiltMotion.None
    End Sub
    Public Sub Motion(ByVal motion As enmTiltMotion, ByVal Tilt As enmTiltIndex)
        SetNowTilt(Tilt)
        mTask = motion
        ResetRunningStatus()
        resumeEvent.Set()
    End Sub

    Public Sub SetNowTilt(ByVal Tilt As enmTiltIndex)
        Select Case Tilt
            Case enmTiltIndex.TiltA
                TiltController = TiltAL
            Case enmTiltIndex.TiltB
                TiltController = TiltAR
            Case enmTiltIndex.TiltC
                TiltController = TiltBL
            Case enmTiltIndex.TiltD
                TiltController = TiltBR
        End Select
    End Sub
    Public Sub close()
        If thread.IsAlive = True Then
            thread.Abort()
        End If

    End Sub
#End Region

#Region "Private Method"
    Private Sub Action() 'ByVal motion As enmTiltMotion)
        While (thread.IsAlive)
            thread.Join(10)
            Select Case mTask
                Case enmTiltMotion.None
                    ResumeEvent.WaitOne()
                Case enmTiltMotion.Initial
                    TiltController.Work(enmTiltMotion.Initial)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
                Case enmTiltMotion.DegreesZero
                    TiltController.Work(enmTiltMotion.DegreesZero)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
                Case enmTiltMotion.DegreesType1
                    TiltController.Work(enmTiltMotion.DegreesType1)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
                Case enmTiltMotion.DegreesType2
                    TiltController.Work(enmTiltMotion.DegreesType2)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
                Case enmTiltMotion.DegreesType3
                    TiltController.Work(enmTiltMotion.DegreesType3)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
                Case enmTiltMotion.DegreesType4
                    TiltController.Work(enmTiltMotion.DegreesType4)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
                Case enmTiltMotion.DegreesType5
                    TiltController.Work(enmTiltMotion.DegreesType5)
                    mRunningStatus = TiltController._MotionRunningStatus
                    If mRunningStatus = enmRunningStatus.Finish Then
                        'Finish
                    ElseIf (TiltController._MotionRunningStatus = enmRunningStatus.Alarm) Then
                        'Alarm Message
                    End If
            End Select
            Select Case mRunningStatus
                Case enmRunningStatus.None
                Case enmRunningStatus.Running
                Case enmRunningStatus.Alarm
                    ResumeEvent.WaitOne()
                Case enmRunningStatus.Finish
                    ResumeEvent.WaitOne()
            End Select
        End While
    End Sub
#End Region

    'Public Sub TiltHome(ByVal _TiltIndex As Integer, ByRef ActionIndex As Integer)
    '    Dim eTemp As enmTiltMotionStatus
    '    Select Case _TiltIndex
    '        Case 1
    '            eTemp = TiltAL.Work(enmTiltMotion.Initial)
    '        Case 2
    '            eTemp = TiltAR.Work(enmTiltMotion.Initial)
    '        Case 3
    '            eTemp = TiltBL.Work(enmTiltMotion.Initial)
    '        Case 4
    '            eTemp = TiltBR.Work(enmTiltMotion.Initial)
    '    End Select
    '    Return eTemp
    'End Sub

    'Private mInitialRunningStatus As enmRunningStatus
    'ReadOnly Property GetInitialRunningStatus() As enmRunningStatus
    '    Get
    '        Return mInitialRunningStatus
    '    End Get
    'End Property
    'Private mDegrees2RunningStatus As enmRunningStatus
    'ReadOnly Property GetDegrees2RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees2RunningStatus
    '    End Get
    'End Property
    'Private mDegrees3RunningStatus As enmRunningStatus
    'ReadOnly Property GetDegrees3RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees3RunningStatus
    '    End Get
    'End Property
    'Private mDegrees4RunningStatus As enmRunningStatus
    'ReadOnly Property GetDegrees4RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees4RunningStatus
    '    End Get
    'End Property
End Class



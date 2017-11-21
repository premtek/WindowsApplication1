Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion
''' <summary>
''' Tilt基礎類別
''' </summary>
''' <remarks></remarks>
Public Class clsTiltBase

#Region "Private Property"
    ''' <summary>
    ''' 馬達參數
    ''' </summary>
    ''' <remarks></remarks>
    Private mTiltMotor As TiltMotor
    ''' <summary>
    ''' 流程狀態
    ''' </summary>
    ''' <remarks></remarks>
    Private mMotionRunningStatus As enmRunningStatus
    ''' <summary>
    ''' Initial流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mInitialIndex As Integer
    ''' <summary>
    ''' DegreesTypeZero流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDegreesTypeZeroIndex As Integer
    ''' <summary>
    ''' DegreesType1流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDegreesType1Index As Integer
    ''' <summary>
    ''' DegreeType2流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDegreesType2Index As Integer
    ''' <summary>
    ''' DegreeType3流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDegreesType3Index As Integer
    ''' <summary>
    ''' DegreeType4流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDegreesType4Index As Integer
    ''' <summary>
    ''' DegreeType5流程Index
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDegreesType5Index As Integer
    ''' <summary>
    ''' 馬達工作位置
    ''' </summary>
    ''' <remarks></remarks>
    Dim mTiltPos As TiltPos
#End Region

#Region "Public Property"
    Public Property _mTiltMotor() As TiltMotor
        Get
            Return mTiltMotor
        End Get
        Set(ByVal value As TiltMotor)
            mTiltMotor = value
        End Set
    End Property

    Public Property _MotionRunningStatus() As enmRunningStatus
        Get
            Return mMotionRunningStatus
        End Get
        Set(ByVal value As enmRunningStatus)
            mMotionRunningStatus = value
        End Set
    End Property

    ReadOnly Property _GetDegreesType1Index() As Integer
        Get
            Return mDegreesType1Index
        End Get

    End Property

    ReadOnly Property _GetDegreesType2Index() As Integer
        Get
            Return mDegreesType2Index
        End Get
    End Property

    ReadOnly Property _GetDegreesType3Index() As Integer
        Get
            Return mDegreesType3Index
        End Get

    End Property

    ReadOnly Property _GetDegreesType4Index() As Integer
        Get
            Return mDegreesType4Index
        End Get
    End Property

    ReadOnly Property _GetDegreesType5Index() As Integer
        Get
            Return mDegreesType5Index
        End Get
    End Property

    ReadOnly Property _GetTiltPos() As TiltPos
        Get
            Return mTiltPos
        End Get
        'Set(ByVal value As TiltPos)
        '    mTiltPos = value
        'End Set
    End Property
#End Region

#Region "Public Method"

    ''' <summary>
    ''' 流程重置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Reset()
        mInitialIndex = 0
        mDegreesTypeZeroIndex = 0
        mDegreesType1Index = 0
        mDegreesType2Index = 0
        mDegreesType3Index = 0
        mDegreesType4Index = 0
        mDegreesType5Index = 0
    End Sub

    ''' <summary>
    ''' 流程動作選擇
    ''' </summary>
    ''' <param name="_TiltMotion"></param>
    ''' <remarks></remarks>
    Public Sub Work(ByVal _TiltMotion As enmTiltMotion) ' As enmRunningStatus
        Select Case _TiltMotion
            Case enmTiltMotion.Initial
                mMotionRunningStatus = enmRunningStatus.Running
                If Initial(mInitialIndex) = True Then
                    mMotionRunningStatus = enmRunningStatus.Finish
                End If
            Case enmTiltMotion.DegreesZero
            Case enmTiltMotion.DegreesType1
            Case enmTiltMotion.DegreesType2
            Case enmTiltMotion.DegreesType3
            Case enmTiltMotion.DegreesType4
            Case enmTiltMotion.DegreesType5
        End Select
    End Sub

    Public Sub Save(ByVal sFileName As String, ByVal Pos As TiltPos)

        If System.IO.Directory.Exists(sFileName) Then
            SaveIniString("TiltPos", "TiltPosX0", Pos.dTiltPosX0, sFileName)
            SaveIniString("TiltPos", "TiltPosY0", Pos.dTiltPosY0, sFileName)
            SaveIniString("TiltPos", "TiltPosZ0", Pos.dTiltPosZ0, sFileName)
            SaveIniString("TiltPos", "WorkDegrees0", Pos.dWorkDegrees0, sFileName)

            SaveIniString("TiltPos", "TiltPosX1", Pos.dTiltPosX1, sFileName)
            SaveIniString("TiltPos", "TiltPosY1", Pos.dTiltPosY1, sFileName)
            SaveIniString("TiltPos", "TiltPosZ1", Pos.dTiltPosZ1, sFileName)
            SaveIniString("TiltPos", "WorkDegrees1", Pos.dWorkDegrees1, sFileName)

            SaveIniString("TiltPos", "TiltPosX2", Pos.dTiltPosX2, sFileName)
            SaveIniString("TiltPos", "TiltPosY2", Pos.dTiltPosY2, sFileName)
            SaveIniString("TiltPos", "TiltPosZ2", Pos.dTiltPosZ2, sFileName)
            SaveIniString("TiltPos", "WorkDegrees2", Pos.dWorkDegrees2, sFileName)

            SaveIniString("TiltPos", "TiltPosX3", Pos.dTiltPosX3, sFileName)
            SaveIniString("TiltPos", "TiltPosY3", Pos.dTiltPosY3, sFileName)
            SaveIniString("TiltPos", "TiltPosZ3", Pos.dTiltPosZ3, sFileName)
            SaveIniString("TiltPos", "WorkDegrees3", Pos.dWorkDegrees3, sFileName)

            SaveIniString("TiltPos", "TiltPosX4", Pos.dTiltPosX4, sFileName)
            SaveIniString("TiltPos", "TiltPosY4", Pos.dTiltPosY4, sFileName)
            SaveIniString("TiltPos", "TiltPosZ4", Pos.dTiltPosZ4, sFileName)
            SaveIniString("TiltPos", "WorkDegrees4", Pos.dWorkDegrees4, sFileName)

            SaveIniString("TiltPos", "TiltPosX5", Pos.dTiltPosX5, sFileName)
            SaveIniString("TiltPos", "TiltPosY5", Pos.dTiltPosY5, sFileName)
            SaveIniString("TiltPos", "TiltPosZ5", Pos.dTiltPosZ5, sFileName)
            SaveIniString("TiltPos", "WorkDegrees5", Pos.dWorkDegrees5, sFileName)
        End If


    End Sub

    Public Function Load(ByVal sFileName As String) As TiltPos
        If System.IO.Directory.Exists(sFileName) Then
            mTiltPos.dTiltPosX0 = ReadIniString("TiltPos", "TiltPosX0", sFileName)
            mTiltPos.dTiltPosY0 = ReadIniString("TiltPos", "TiltPosY0", sFileName)
            mTiltPos.dTiltPosZ0 = ReadIniString("TiltPos", "TiltPosZ0", sFileName)
            mTiltPos.dWorkDegrees0 = ReadIniString("TiltPos", "WorkDegrees0", sFileName)

            mTiltPos.dTiltPosX1 = ReadIniString("TiltPos", "TiltPosX1", sFileName)
            mTiltPos.dTiltPosY1 = ReadIniString("TiltPos", "TiltPosY1", sFileName)
            mTiltPos.dTiltPosZ1 = ReadIniString("TiltPos", "TiltPosZ1", sFileName)
            mTiltPos.dWorkDegrees1 = ReadIniString("TiltPos", "WorkDegrees1", sFileName)

            mTiltPos.dTiltPosX2 = ReadIniString("TiltPos", "TiltPosX2", sFileName)
            mTiltPos.dTiltPosY2 = ReadIniString("TiltPos", "TiltPosY2", sFileName)
            mTiltPos.dTiltPosZ2 = ReadIniString("TiltPos", "TiltPosZ2", sFileName)
            mTiltPos.dWorkDegrees2 = ReadIniString("TiltPos", "WorkDegrees2", sFileName)

            mTiltPos.dTiltPosX3 = ReadIniString("TiltPos", "TiltPosX3", sFileName)
            mTiltPos.dTiltPosY3 = ReadIniString("TiltPos", "TiltPosY3", sFileName)
            mTiltPos.dTiltPosZ3 = ReadIniString("TiltPos", "TiltPosZ3", sFileName)
            mTiltPos.dWorkDegrees3 = ReadIniString("TiltPos", "WorkDegrees3", sFileName)

            mTiltPos.dTiltPosX4 = ReadIniString("TiltPos", "TiltPosX4", sFileName)
            mTiltPos.dTiltPosY4 = ReadIniString("TiltPos", "TiltPosY4", sFileName)
            mTiltPos.dTiltPosZ4 = ReadIniString("TiltPos", "TiltPosZ4", sFileName)
            mTiltPos.dWorkDegrees4 = ReadIniString("TiltPos", "WorkDegrees4", sFileName)

            mTiltPos.dTiltPosX5 = ReadIniString("TiltPos", "TiltPosX5", sFileName)
            mTiltPos.dTiltPosY5 = ReadIniString("TiltPos", "TiltPosY5", sFileName)
            mTiltPos.dTiltPosZ5 = ReadIniString("TiltPos", "TiltPosZ5", sFileName)
            mTiltPos.dWorkDegrees5 = ReadIniString("TiltPos", "WorkDegrees5", sFileName)
            Return mTiltPos
        End If
    End Function
#End Region

#Region "Private Method"
    Private Function Initial(ByRef index As Integer) As Boolean
        ''判斷目前位置, 方向 DD只能往一邊找
        'gCMotion.GetPositionValue()
        Select Case index
            Case 1

            Case 2

            Case Else

        End Select

        Return True
    End Function

    Private Function DoDegreesTypeZero() As Boolean
        Return True
    End Function

    Private Function DoDegreesType1() As Boolean
        Return True
    End Function

    Private Function DoDegreesType2() As Boolean
        Return True
    End Function

    Private Function DoDegreesType3() As Boolean
        Return True
    End Function

    Private Function DoDegreesType4() As Boolean
        Return True
    End Function

    Private Function DoDegreesType5() As Boolean
        Return True
    End Function
#End Region


















    ' ''' <summary>
    ' ''' 初始化狀態
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private mInitialStatus As enmRunningStatus
    'Public Property _InitialStatus() As enmRunningStatus
    '    Get
    '        Return mInitialStatus
    '    End Get
    '    Set(ByVal value As enmRunningStatus)
    '        mInitialStatus = value
    '    End Set
    'End Property
    'Private mDegrees1RunningStatus As enmRunningStatus
    'Public Property _Degrees1RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees1RunningStatus
    '    End Get
    '    Set(ByVal value As enmRunningStatus)
    '        mDegrees1RunningStatus = value
    '    End Set
    'End Property
    'Private mDegrees2RunningStatus As enmRunningStatus
    'Public Property _Degrees2RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees2RunningStatus
    '    End Get
    '    Set(ByVal value As enmRunningStatus)
    '        mDegrees2RunningStatus = value
    '    End Set
    'End Property
    'Private mDegrees3RunningStatus As enmRunningStatus
    'Public Property _Degrees3RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees3RunningStatus
    '    End Get
    '    Set(ByVal value As enmRunningStatus)
    '        mDegrees3RunningStatus = value
    '    End Set
    'End Property
    'Private mDegrees4RunningStatus As enmRunningStatus
    'Public Property _Degrees4RunningStatus() As enmRunningStatus
    '    Get
    '        Return mDegrees4RunningStatus
    '    End Get
    '    Set(ByVal value As enmRunningStatus)
    '        mDegrees4RunningStatus = value
    '    End Set
    'End Property
End Class

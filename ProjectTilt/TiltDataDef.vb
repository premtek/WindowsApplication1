'Public Enum enmTiltMotionStatus
'    Initialing
'    InitialFinish
'    Degrees1ing
'    Degrees1Finish
'    Degrees2ing
'    Degrees2Finish
'    Degrees3ing
'    Degrees3Finish
'    Degrees4ing
'    Degrees4Finish
'End Enum

'Public bUseTiltFunction As Boolean
'Private OffsetZero As TiltOffsetPos
'Private OffsetType1 As TiltOffsetPos
'Private OffsetType2 As TiltOffsetPos
'Private OffsetType3 As TiltOffsetPos
'Private OffsetType4 As TiltOffsetPos
'Private OffsetPos As TiltOffsetPos
''' <summary>
''' 動作類型
''' </summary>
''' <remarks></remarks>　
Public Enum enmTiltMotion
    Initial
    ''' <summary>
    ''' 馬達0度動作
    ''' </summary>
    ''' <remarks></remarks>
    DegreesZero
    ''' <summary>
    ''' 馬達Type1動作
    ''' </summary>
    ''' <remarks></remarks>
    DegreesType1
    ''' <summary>
    ''' 馬達Type2動作
    ''' </summary>
    ''' <remarks></remarks>
    DegreesType2
    ''' <summary>
    ''' 馬達Type3動作
    ''' </summary>
    ''' <remarks></remarks>
    DegreesType3
    ''' <summary>
    ''' 馬達Type4動作
    ''' </summary>
    ''' <remarks></remarks>
    DegreesType4
    ''' <summary>
    ''' 馬達Type5動作
    ''' </summary>
    ''' <remarks></remarks>
    DegreesType5
    None = -1
End Enum
''' <summary>
''' 選擇Tilt
''' </summary>
''' <remarks></remarks>
Public Enum enmTiltIndex
    TiltA
    TiltB
    TiltC
    TiltD
End Enum
''' <summary>
''' 馬達變數
''' </summary>
''' <remarks></remarks>
Public Structure TiltMotor
    Dim X As Integer
    Dim Y As Integer
    Dim Z As Integer
    Dim U As Integer
End Structure
''' <summary>
''' IO變數
''' </summary>
''' <remarks></remarks>
Public Structure TitltIO

End Structure

''' <summary>
''' 動作執行狀態
''' </summary>
''' <remarks></remarks>
Public Enum enmRunningStatus
    None
    Running
    Alarm
    Finish
End Enum
''' <summary>
''' Tilt位置
''' </summary>
''' <remarks></remarks>
Public Structure TiltPos

    ''' <summary>
    ''' 0度與Title工作角度偏移量X
    ''' </summary>
    ''' <remarks></remarks>
    Public dTiltPosX0 As Double
    ''' <summary>
    ''' 0度與Title工作角度偏移量Y
    ''' </summary>
    ''' <remarks></remarks>
    Public dTiltPosY0 As Double

    ''' <summary>
    ''' 0度與Title工作角度偏移量Z
    ''' </summary>
    ''' <remarks></remarks>
    Public dTiltPosZ0 As Double
    ''' <summary>
    ''' 工作角度
    ''' </summary>
    ''' <remarks></remarks>
    Public dWorkDegrees0 As Double

    Public dTiltPosX1 As Double
    Public dTiltPosY1 As Double
    Public dTiltPosZ1 As Double
    Public dWorkDegrees1 As Double

    Public dTiltPosX2 As Double
    Public dTiltPosY2 As Double
    Public dTiltPosZ2 As Double
    Public dWorkDegrees2 As Double

    Public dTiltPosX3 As Double
    Public dTiltPosY3 As Double
    Public dTiltPosZ3 As Double
    Public dWorkDegrees3 As Double

    Public dTiltPosX4 As Double
    Public dTiltPosY4 As Double
    Public dTiltPosZ4 As Double
    Public dWorkDegrees4 As Double

    Public dTiltPosX5 As Double
    Public dTiltPosY5 As Double
    Public dTiltPosZ5 As Double
    Public dWorkDegrees5 As Double

End Structure


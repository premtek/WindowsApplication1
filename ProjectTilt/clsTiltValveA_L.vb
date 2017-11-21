Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion
''' <summary>
''' A機左站
''' </summary>
''' <remarks></remarks>
Public Class clsTiltValveA_L
    Inherits clsTiltBase
   
    Dim TiltMotor As TiltMotor
    Public Sub New()
        '軸設定
        TiltMotor.X = enmAxis.XAxis
        TiltMotor.Y = enmAxis.Y1Axis
        TiltMotor.Z = enmAxis.ZAxis
        TiltMotor.U = enmAxis.BAxis
        MyBase._mTiltMotor = TiltMotor
    End Sub
End Class

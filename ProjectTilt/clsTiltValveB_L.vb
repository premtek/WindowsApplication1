Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion
''' <summary>
''' B機左站
''' </summary>
''' <remarks></remarks>
Public Class clsTiltValveB_L
    Inherits clsTiltBase
  
    Dim TiltMotor As TiltMotor
    Public Sub New()
        '軸設定
        TiltMotor.X = enmAxis.RAxis
        TiltMotor.Y = enmAxis.R2Axis
        TiltMotor.Z = enmAxis.SAxis
        TiltMotor.U = enmAxis.WAxis
        MyBase._mTiltMotor = TiltMotor
    End Sub
End Class

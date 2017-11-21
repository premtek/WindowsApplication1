Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion
''' <summary>
''' B機右站
''' </summary>
''' <remarks></remarks>
Public Class clsTiltValveB_R : Inherits clsTiltBase
    ' Inherits clsTiltBase

    Dim TiltMotor As TiltMotor
    Public Sub New()
        '軸設定
        TiltMotor.X = enmAxis.UAxis
        TiltMotor.Y = enmAxis.VAxis
        TiltMotor.Z = enmAxis.WAxis
        TiltMotor.U = enmAxis.WAxis
        MyBase._mTiltMotor = TiltMotor
    End Sub

End Class

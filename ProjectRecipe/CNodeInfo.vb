Public Class CNodeInfo

    Public StartXPos As Decimal
    Public StartYPos As Decimal
    Public EndXPos As Decimal
    Public EndYPos As Decimal

    Public Sub New(ByVal StartX As Decimal, ByVal StartY As Decimal, ByVal EndX As Decimal, ByVal EndY As Decimal)
        StartXPos = StartX
        StartYPos = StartY
        EndXPos = EndX
        EndYPos = EndY
    End Sub
End Class

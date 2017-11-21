Public Class ucSelectArrayGoPosition
    Private mLevelNo As Integer
    Public Property _LevelNo() As Integer
        Get
            Return mLevelNo
        End Get
        Set(ByVal value As Integer)
            Me.grpLevel.Text = "Level: " & value.ToString
            mLevelNo = value
        End Set
    End Property
End Class

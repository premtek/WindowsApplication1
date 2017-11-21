Imports ProjectIO

Public Class ucHeavyLight
    Public Property Status As eInterlock
    ''' <summary>
    ''' 更新狀態
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Public Sub UpdateStatus(ByVal status As eInterlock)
        Select Case status
            Case eInterlock.Alarm
                rdbHeavy1.PerformClick()
            Case eInterlock.Warn
                rdbLight1.PerformClick()
            Case eInterlock.None
                rdbNone1.PerformClick()
        End Select
    End Sub
    Private Sub rdbHeavy1_CheckedChanged(sender As Object, e As EventArgs) Handles rdbHeavy1.CheckedChanged
        Status = eInterlock.Alarm
    End Sub

    Private Sub rdbLight1_CheckedChanged(sender As Object, e As EventArgs) Handles rdbLight1.CheckedChanged
        Status = eInterlock.Warn
    End Sub

    Private Sub rdbNone1_CheckedChanged(sender As Object, e As EventArgs) Handles rdbNone1.CheckedChanged
        Status = eInterlock.None
    End Sub

End Class

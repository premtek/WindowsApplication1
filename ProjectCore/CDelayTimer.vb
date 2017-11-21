''' <summary>Delay time /使用在Time Counter 裡面</summary>
''' <remarks></remarks>
Public Class CDelayTimer
    Private STime As DateTime = Nothing        '開始時間
    Private ETime As DateTime = Nothing        '結束時間

    ''' <summary>
    ''' Delay time
    ''' </summary>
    ''' <param name="Interval">毫秒</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function MillisecondDelay(ByVal Interval As Double) As Long

        If STime = Nothing Then
            STime = DateTime.Now
        End If

        ETime = DateTime.Now

        If ETime.Subtract(STime).TotalMilliseconds >= Interval Then
            Restart()
            Return 1
        End If

        Return 0
    End Function

    ''' <summary>
    ''' Restart Start Time
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Restart()
        STime = Nothing
    End Sub

End Class
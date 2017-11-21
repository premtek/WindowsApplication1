Public Interface IAOInterface
    ''' <summary>關閉卡片</summary>
    ''' <remarks></remarks>
    Sub Close()
    ''' <summary>初始化</summary>
    ''' <param name="deviceDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal deviceDescription As String) As Boolean
    ''' <summary>寫入資料</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Write(portStart As Integer, portEnd As Integer, ByRef val() As Double) As Integer
    ''' <summary>埠數/卡</summary>
    ''' <remarks></remarks>
    Property PortPerCard As Integer

End Interface

Public Class CAOVirtual
    Implements IAOInterface

    Sub New()
        PortPerCard = 8
    End Sub
    Public Sub Close() Implements IAOInterface.Close

    End Sub

    Public Function Initial(deviceDescription As String) As Boolean Implements IAOInterface.Initial
        Return True
    End Function

    Public Property PortPerCard As Integer Implements IAOInterface.PortPerCard

    Public Function Write(portStart As Integer, portEnd As Integer, ByRef val() As Double) As Integer Implements IAOInterface.Write
        Return 0
    End Function
End Class
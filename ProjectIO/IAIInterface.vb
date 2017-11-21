Public Interface IAIInterface
    ''' <summary>關閉卡片</summary>
    ''' <remarks></remarks>
    Sub Close()
    ''' <summary>初始化</summary>
    ''' <param name="deviceDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal deviceDescription As String) As Boolean
    ''' <summary>讀取資料</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Read(portStart As Integer, portEnd As Integer, ByRef val() As Double) As Integer
    ''' <summary>埠數/卡</summary>
    ''' <remarks></remarks>
    Property PortPerCard As Integer
    
End Interface

Public Class CAIVirtual
    Implements IAIInterface

    Sub New()
        PortPerCard = 8
    End Sub

    Public Sub Close() Implements IAIInterface.Close

    End Sub

    Public Function Initial(deviceDescription As String) As Boolean Implements IAIInterface.Initial
        Return True
    End Function

    Public Property PortPerCard As Integer Implements IAIInterface.PortPerCard

    Public Function Read(portStart As Integer, portEnd As Integer, ByRef val() As Double) As Integer Implements IAIInterface.Read
        Dim cnt As Integer = portEnd - portStart
        ReDim val(cnt)
        Return 0
    End Function
End Class
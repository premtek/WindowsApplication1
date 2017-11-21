''' <summary>資料傳遞事件參數</summary>
''' <remarks></remarks>
Public Class DataEventArgs
    Inherits EventArgs
    ''' <summary>要傳遞的資料 </summary>
    ''' <remarks></remarks>
    Public Data As String
    ''' <summary>第二筆資料(如果有的話)</summary>
    ''' <remarks></remarks>
    Public Data2 As String
    Public Sub New()

    End Sub
    Public Sub New(ByVal data As String)
        Me.Data = data
    End Sub
    Public Sub New(ByVal data As String, data2 As String)
        Me.Data = data
        Me.Data2 = data2
    End Sub
End Class

''' <summary>時間格式
''' </summary>
''' <remarks></remarks>
Public Structure TimeFormat
    ''' <summary>年</summary>
    ''' <remarks></remarks>
    Public Yy As Integer
    ''' <summary>月</summary>
    ''' <remarks></remarks>
    Public Mo As Integer
    ''' <summary>日</summary>
    ''' <remarks></remarks>
    Public Dd As Integer
    ''' <summary>時</summary>
    ''' <remarks></remarks>
    Public Hh As Integer
    ''' <summary>分</summary>
    ''' <remarks></remarks>
    Public Mi As Integer
    ''' <summary>秒</summary>
    ''' <remarks></remarks>
    Public Ss As Integer
End Structure
Public Class TimeEventArgs
    Inherits EventArgs
    Public Value As TimeFormat
    Public Sub New()
    End Sub
    Public Sub New(ByVal value As TimeFormat)
        Me.Value = value
    End Sub
End Class

Public Class DataArrayEventArgs
    Inherits EventArgs
    Public Value() As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal value() As String)
        Me.Value = value
    End Sub
End Class

''' <summary>檢測設定
''' </summary>
''' <remarks></remarks>
Public Structure ConfigOfDetection
    ''' <summary>記憶卡編號</summary>
    ''' <remarks></remarks>
    Public CardID As Integer
    ''' <summary>檢測設定(0-999)</summary>
    ''' <remarks></remarks>
    Public ItemID As Integer
End Structure
''' <summary>
''' 卡片設定事件傳遞資料型別 
''' </summary>
''' <remarks></remarks>
Public Class ConfigEventArgs
    Inherits EventArgs
    Public Config As ConfigOfDetection
    Public Sub New()
    End Sub
    Public Sub New(ByVal value As ConfigOfDetection)
        Me.Config = value
    End Sub
End Class

Public Enum ControllerMode
    ''' <summary>不明</summary>
    ''' <remarks></remarks>
    Unknown = 0
    ''' <summary>設定</summary>
    ''' <remarks></remarks>
    Setup = 1
    ''' <summary>運行</summary>
    ''' <remarks></remarks>
    Run = 2
End Enum

''' <summary>
''' 控制模式事件傳遞資料型別 
''' </summary>
''' <remarks></remarks>
Public Class ControllerModeEventArgs
    Inherits EventArgs
    Public Type As ControllerMode
    Public Sub New()
    End Sub
    Public Sub New(ByVal value As ControllerMode)
        Me.Type = value
    End Sub
End Class


''' <summary>訊息輸出接點</summary>
''' <remarks></remarks>
Public Enum eIndicator
    ''' <summary>紅燈</summary>
    ''' <remarks></remarks>
    Red = 0
    ''' <summary>
    ''' 黃燈
    ''' </summary>
    ''' <remarks></remarks>
    Yellow = 1
    ''' <summary>
    ''' 綠燈
    ''' </summary>
    ''' <remarks></remarks>
    Green = 2
    ''' <summary>
    ''' 藍燈
    ''' </summary>
    ''' <remarks></remarks>
    Blue = 3
    ''' <summary>
    ''' 蜂鳴器
    ''' </summary>
    ''' <remarks></remarks>
    Buzzer = 4
    ''' <summary>
    ''' 總數
    ''' </summary>
    ''' <remarks></remarks>
    Count = 5
End Enum
''' <summary>
''' 指示器事件傳遞資料型別 
''' </summary>
''' <remarks></remarks>
Public Class IndicatorEventArgs
    Inherits EventArgs
    ''' <summary>
    ''' 傳遞的指示器
    ''' </summary>
    ''' <remarks></remarks>
    Public indicator As eIndicator
    ''' <summary>
    ''' 開關
    ''' </summary>
    ''' <remarks></remarks>
    Public value As Boolean
    Public Sub New()
    End Sub
    Public Sub New(ByVal indicator As eIndicator, ByVal value As Boolean)
        Me.indicator = indicator
        Me.value = value
    End Sub
End Class

Public Structure FMCSData
    Public avgFlow As Double
    Public volume As Double
    Public intX As Integer
    Public intY As Integer
    Public dispenserNo As enmValve
End Structure
Public Class FMCSEventArgs
    Inherits EventArgs
    Public Data As FMCSData
    Public Sub New()

    End Sub
    Public Sub New(ByVal data As FMCSData)
        Me.Data = data
    End Sub
End Class
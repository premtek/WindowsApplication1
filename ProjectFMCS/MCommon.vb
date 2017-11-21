Imports ProjectRecipe
Imports ProjectIO
Imports System.Math
Imports ProjectFeedback
Imports ProjectMotion
Imports ProjectCore

Public Module MCommon
    Public weightCorrectedValue As Double
    ''' <summary>稱重時氣壓記錄,避免Feedback修改紀錄造成記錄錯誤</summary>
    ''' <remarks></remarks>
    Public RecordAP1 As Double

    ''' <summary>天平物件集合</summary>
    ''' <remarks></remarks>
    Public gBalanceCollection As New CBalanceCollection

End Module

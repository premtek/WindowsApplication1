Imports ProjectCore

Public Class CScaleParameter

    Public Function Clone() As CScaleParameter
        Dim mTemp As New CScaleParameter
        mTemp.CountInPcs = Me.CountInPcs
        mTemp.TimerInSec = Me.TimerInSec
        mTemp.WeightControlType = Me.WeightControlType
        Return mTemp
    End Function

    ''' <summary>重量控制形式</summary>
    ''' <remarks></remarks>
    Public Enum eWeightControlType
        ''' <summary>不控制</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>時間控制</summary>
        ''' <remarks></remarks>
        Time = 1
        ''' <summary>計數控制</summary>
        ''' <remarks></remarks>
        PCS = 2
    End Enum

    ''' <summary>重量控制方式</summary>
    ''' <remarks></remarks>
    Public WeightControlType As eWeightControlType
    ''' <summary>多久秤重一次(Sec)</summary>
    ''' <remarks></remarks>
    Public TimerInSec As Double
    ''' <summary>多少顆秤重一次(PCS)</summary>
    ''' <remarks></remarks>
    Public CountInPcs As Integer

    Public Function Save(ByVal fileName As String) As Boolean
        Try

            Dim strSection As String
            strSection = "WeighteParameter"
            SaveIniString(strSection, "WeightControlType", CInt(WeightControlType), fileName)
            SaveIniString(strSection, "TimerInSec", TimerInSec, fileName)
            SaveIniString(strSection, "CountInPcs", CountInPcs, fileName)
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002027), "Error_1002027", eMessageLevel.Error)
            gSyslog.Save("Excception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002027) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function
    Public Function Load(ByVal fileName As String) As Boolean
        Try

            Dim strSection As String
            strSection = "WeighteParameter"
            WeightControlType = Val(ReadIniString(strSection, "WeightControlType", fileName))
            TimerInSec = Val(ReadIniString(strSection, "TimerInSec", fileName, 200))
            CountInPcs = Val(ReadIniString(strSection, "CountInPcs", fileName, 200))

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002026), "Error_1002026", eMessageLevel.Error)
            gSyslog.Save("Excception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002026) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function

End Class

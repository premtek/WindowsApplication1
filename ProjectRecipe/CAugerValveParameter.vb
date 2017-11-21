Imports ProjectCore

Public Enum eValveControlType
    ''' <summary>外部設定開關</summary>
    ''' <remarks></remarks>
    Manual = 0
    ''' <summary>單次觸發,開啟一段時間</summary>
    ''' <remarks></remarks>
    OneShot = 1
End Enum
''' <summary>螺桿閥參數</summary>
''' <remarks></remarks>
Public Class CAugerValveParameter
    ''' <summary>名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>旋轉速度</summary>
    ''' <remarks></remarks>
    Public RotationSpeed As Double
    ''' <summary>閥控制方式</summary>
    ''' <remarks></remarks>
    Public ValveControlType As eValveControlType
    ''' <summary>單擊,開啟時間(ms)</summary>
    ''' <remarks></remarks>
    Public OneShotOpenTime As Double

    Sub New(ByVal name As String)
        Me.Name = name
        RotationSpeed = 200
        ValveControlType = eValveControlType.Manual
        OneShotOpenTime = 1
    End Sub

    Public Function Save(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "AugerValveParameter"
            SaveIniString(strSection, "Name", Name, fileName)
            SaveIniString(strSection, "RotationSpeed", RotationSpeed, fileName)
            SaveIniString(strSection, "ValveControlType", CInt(ValveControlType), fileName)
            SaveIniString(strSection, "OneShotOpenTime", OneShotOpenTime, fileName)

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002007), "Error_1002007", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002007) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function
    Public Function Load(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "AugerValveParameter"
            Name = ReadIniString(strSection, "Name", fileName)
            RotationSpeed = Val(ReadIniString(strSection, "RotationSpeed", fileName, 200))
            ValveControlType = Val(ReadIniString(strSection, "ValveControlType", fileName, 200))
            OneShotOpenTime = Val(ReadIniString(strSection, "OneShotOpenTime", fileName, 1))

            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002006), "Error_1002006", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002006) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function

End Class

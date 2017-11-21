Imports ProjectCore

''' <summary>[Purge資料庫]</summary>
''' <remarks></remarks>
Public Class CPurgeParameter
    ''' <summary>[名稱]</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>[Purge觸發的條件是參考哪一項條件]</summary>
    ''' <remarks></remarks>
    Public BaseOn As eInspectionType
    ''' <summary>[時間(幾分鐘做一次)]</summary>
    ''' <remarks></remarks>
    Public OnTimer As Decimal
    ''' <summary>[盤數(幾盤做一次)]</summary>
    ''' <remarks></remarks>
    Public OnRuns As Integer
    ''' <summary>清膠型式</summary>
    ''' <remarks></remarks>
    Public CleanType As eCleanType
    ''' <summary>[點膠前強制Purge]</summary>
    ''' <remarks></remarks>
    Public IsPreDispenePurge As Boolean
    '20170329
    ''' <summary>[JettingPurge]</summary>
    ''' <remarks></remarks>
    Public JettingPurge As Boolean
    'TODO:Purge的方式後續要再補

    ''' <summary>[備註]</summary>
    ''' <remarks></remarks>
    Public Comment As String

    Sub New(ByVal name As String) 'Soni + 2017.04.26 產生預設值
        Me.Name = name
        Me.BaseOn = eInspectionType.Noen
        Me.OnRuns = 1
        Me.OnTimer = 60
        Me.CleanType = eCleanType.VacuumClean
        Me.IsPreDispenePurge = False
        Me.JettingPurge = False
        Me.Comment = ""
    End Sub

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Function Save(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "Purge"
            SaveIniString(strSection, "Name", Name, fileName)
            SaveIniString(strSection, "BaseOn", BaseOn, fileName)
            SaveIniString(strSection, "OnTimer", OnTimer, fileName)
            SaveIniString(strSection, "OnRuns", OnRuns, fileName)
            SaveIniString(strSection, "IsPreDispenePurge", CInt(IsPreDispenePurge), fileName)
            '20170329
            SaveIniString(strSection, "JettingPurge", CInt(JettingPurge), fileName)
            SaveIniString(strSection, "Comment", Comment, fileName)

            '20161206
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                    SaveIniString(strSection, "CleanType", CInt(eCleanType.VacuumClean), fileName)

                Case Else
                    SaveIniString(strSection, "CleanType", CleanType, fileName)

            End Select

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
            strSection = "Purge"
            Name = ReadIniString(strSection, "Name", fileName)
            BaseOn = CInt(ReadIniString(strSection, "BaseOn", fileName, "0"))
            OnTimer = CDec(ReadIniString(strSection, "OnTimer", fileName, "0"))
            OnRuns = CInt(ReadIniString(strSection, "OnRuns", fileName, "0"))
            IsPreDispenePurge = CBool(ReadIniString(strSection, "IsPreDispenePurge", fileName, "0"))
            '20170329
            JettingPurge = CBool(ReadIniString(strSection, "JettingPurge", fileName, "0"))
            Comment = ReadIniString(strSection, "Comment", fileName, "")

            '20161206
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    CleanType = eCleanType.VacuumClean

                Case Else
                    CleanType = CInt(ReadIniString(strSection, "CleanType", fileName, 0))
            End Select

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002006), "Error_1002006", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002006) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

End Class

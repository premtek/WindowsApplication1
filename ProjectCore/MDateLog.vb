
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Module MDateLog
    ''' <summary>系統Log記錄</summary>
    ''' <remarks></remarks>
    Public gSyslog As New CSystemLog
    ''' <summary>訊息處理</summary>
    ''' <remarks></remarks>
    Public gMsgHandler As New CMsgHandler

    Public gMeEventLog As MyEventLog
  
End Module

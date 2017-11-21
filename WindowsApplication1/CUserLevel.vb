Imports System.Data.SqlClient

''' <summary>
''' 使用者權限設定
''' </summary>
''' <remarks></remarks>
Public Class CUserLevel
    ''' <summary>
    ''' 權限資料集
    ''' </summary>
    ''' <remarks></remarks>
    Dim mDataSet As New DataSet
    ''' <summary>
    '''機台名稱 
    ''' </summary>
    ''' <remarks></remarks>
    Dim mMachineName As String = ""

    Dim mTableName As String = "Level建立"
    ''' <summary>連線路徑</summary>
    ''' <remarks></remarks>
    Dim mConnStr As String = ""

    Sub New()

    End Sub
    Sub New(ByVal machineName As String)
        LoadTable(machineName)

    End Sub

    ''' <summary>
    ''' 資源釋放
    ''' </summary>
    ''' <remarks></remarks>
    Sub ManualDispose()
        mDataSet.Clear()
        mDataSet.Dispose()
        mMachineName = ""
    End Sub

    Public Function LoadTable(ByVal machineName As String) As Boolean
        Try
            mMachineName = machineName
            mConnStr = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" & Application.StartupPath & "\System\" & mMachineName & "\MyDB.mdf;integrated security=sspi"
            Using cn As New SqlConnection()
                cn.ConnectionString = mConnStr
                Dim daTable As New SqlDataAdapter("SELECT * FROM " & mTableName & " ORDER BY Id DESC", cn)
                mDataSet = New DataSet
                daTable.Fill(mDataSet, "table")
                cn.Close()
            End Using
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
        
    End Function

    ''' <summary>取得權限表</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTable(ByVal level As String) As DataTable
        If mDataSet.Tables.Count > 0 Then
            Dim mDataView As New DataView(mDataSet.Tables(0))
            mDataView.RowFilter = "level>=" & level
            mDataView.ApplyDefaultSort = True
            Return mDataView.ToTable()
            'Return mDataSet.Tables(0) 'mDataSet.Tables(0).Select("level >=" & level, "DESC")
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>指定ID的資料更新</summary>
    ''' <param name="id"></param>
    ''' <param name="password"></param>
    ''' <param name="level"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal id As String, ByVal password As String, ByVal level As String) As Boolean
        If mMachineName = "" Then
            Return False
        End If
        If id = "" Then
            Return False
        End If
        If password = "" Then
            Return False
        End If
        If level = "" Then
            Return False
        End If
        If mConnStr = "" Then
            mConnStr = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" & Application.StartupPath & "\System\" & mMachineName & "\MyDB.mdf;integrated security=sspi"
        End If
        Using cn As New SqlConnection()        '   
            cn.ConnectionString = mConnStr
            cn.Open()
            Dim sqlStr As String = "UPDATE " & mTableName & " SET Password = '" & password & "',Level= '" & level & "' WHERE Id = '" & id & "'"
            Dim cmd As New SqlCommand(sqlStr, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        End Using

        Return True
    End Function

    ''' <summary>指定ID資料刪除</summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal id As String) As Boolean
        If mMachineName = "" Then
            Return False
        End If
        If id = "" Then
            Return False
        End If
        If mConnStr = "" Then
            mConnStr = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" & Application.StartupPath & "\System\" & mMachineName & "\MyDB.mdf;integrated security=sspi"
        End If
        Using cn As New SqlConnection()
            cn.ConnectionString = mConnStr
            cn.Open()
            Dim sqlStr As String = "DELETE FROM " & mTableName & " WHERE Id = '" & id & "'"
            Dim cmd As New SqlCommand(sqlStr, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        End Using
        Return True
    End Function

    ''' <summary>新增指定ID資料</summary>
    ''' <param name="id"></param>
    ''' <param name="password"></param>
    ''' <param name="level"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal id As String, ByVal password As String, ByVal level As String) As Boolean
        If mMachineName = "" Then
            Return False
        End If
        If id = "" Then
            Return False
        End If
        If password = "" Then
            Return False
        End If
        If level = "" Then
            Return False
        End If
        If mConnStr = "" Then
            mConnStr = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" & Application.StartupPath & "\System\" & mMachineName & "\MyDB.mdf;integrated security=sspi"
        End If
        Using cn As New SqlConnection()
            cn.ConnectionString = mConnStr
            cn.Open()
            Dim sqlStr As String = "INSERT INTO " & mTableName & "(Id, Password, Level) VALUES('" & id & "','" & password & "','" & level & "')"
            Dim cmd As New SqlCommand(sqlStr, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        End Using
        Return True
    End Function

End Class

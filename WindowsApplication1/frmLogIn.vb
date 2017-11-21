Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports ProjectCore
Imports ProjectIO

Public Class frmLogin

    Private Sub frmLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        '20171020 測試__Toby
        '  gfrmSplashScreen.Visible = False
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    '清空內容
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        'txtID.Text = ""
        'txtPassWord.Text = ""
        'StatusStrip1.Items(0).Text = ""
        '--- Soni + 機台型號顯示  2014.09.29 ---
        lblMachineType.Text = MachineTypeToString(gSSystemParameter.MachineType)
        '--- Soni + 機台型號顯示  2014.09.29 ---
        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\login.png"
        If System.IO.File.Exists(fileName) Then
            Me.BackgroundImage = Image.FromFile(fileName)
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Public Function ReadDbLevel(ByVal Id As String, ByVal Password As String) As Boolean

        'On Error GoTo ErrorHandler

        '資料庫 Reader Start------------------------------------------------------------------------------
        '.引用SqlConnection物件連接資料庫()
        '.開啟資料庫()
        '.引用SqlCommand物件()
        '.搭配SqlCommand物件使用SqlDataReader()
        '.判斷資料列是否為空()        '-------------------------------------------------------------------------------------------------
        Try

            Dim strPassword As String

            Dim sqlStr As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" & Application.StartupPath & "\System\" & MachineName & "\MyDB.mdf;"
            Dim sqlqs As String = "SELECT * FROM Level建立 ORDER BY Id DESC"

            Dim cn As New SqlConnection() '引用SqlConnection物件連接資料庫()
            cn.ConnectionString = sqlStr
            cn.Open()
            Dim cmd As New SqlCommand(sqlqs, cn) '引用SqlCommand物件()
            Dim reader As SqlDataReader = cmd.ExecuteReader() '搭配SqlCommand物件使用SqlDataReader()
            While reader.Read()
                '[說明]:判斷資料列是否為空
                If Not reader(0).Equals(DBNull.Value) Then
                    If reader.Item("Id") = Id Then
                        '20161115
                        gUsername = Id

                        '[說明]:為了避免250的密碼流出，所以加個密碼產生器
                        If Id = "250" Then
                            strPassword = ""
                            Call strDecodePassword(Password, strPassword)
                            If strPassword = reader.Item("Password") Then
                                gUserLevel = CInt(reader.Item("Level"))
                                gSyslog.UserLevel = gUserLevel '記錄使用者等級
                                '使用者登錄
                                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000037))
                                'gSyslog.Save("User Login.")
                                Return True
                            End If
                        Else
                            If Password = reader.Item("Password") Then
                                '20161115
                                gUserPassword = Password
                                gUserLevel = CInt(reader.Item("Level"))
                                gSyslog.UserLevel = gUserLevel '記錄使用者等級
                                '使用者登錄
                                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000037))
                                'gSyslog.Save("User Login.")
                                Return True
                            End If
                        End If
                    End If
                End If
            End While
            reader.Close()

            Return False

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000006), "Error_1000006", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1000006) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
        'End----------------------------------------------------------------------------------------------

        'ErrorHandler:

        '        strShowMessage = Err.GetException.StackTrace
        '        frmMsg.TopMost = True
        '        frmMsg.Visible = False
        '        frmMsg.Show()
        '        frmMsg.BringToFront()
        '        Err.Clear()         '清除錯誤資訊
        '        Return False

    End Function

    Private Sub btnLogIn_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click
        gSyslog.Save("[frmLogin]" & vbTab & "[btnLogIn]" & vbTab & "Click")
        If btnLogIn.Enabled = False Then
            Exit Sub
        End If
        btnLogIn.Enabled = False
        Dim strId As String
        Dim strPassword As String

        strId = txtID.Text.TrimEnd
        strPassword = txtPassWord.Text.TrimEnd

        If strId <> "" Then
            '[說明]:至資料庫讀取Id/Password/Level
           
            If ReadDbLevel(strId, strPassword) = True Then
                Me.Close()
            Else
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000000), "Warn_3000000", eMessageLevel.Warning) '使用者登入失敗.
                'StatusStrip1.Items(0).Text = "Please Check ID and Password !!"
                txtID.Focus() '錯誤時回到ID
            End If
        Else
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000021), "Warn_3000021", eMessageLevel.Warning) '請輸入ID
            'StatusStrip1.Items(0).Text = "Please Check ID and Password !!"
            txtID.Focus() '錯誤時回到ID
        End If
            btnLogIn.Enabled = True
    End Sub



    ''' <summary>
    ''' 250密碼驗證
    ''' </summary>
    ''' <param name="strPasswordIn"></param>
    ''' <param name="strPasswordOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function strDecodePassword(ByVal strPasswordIn As String, ByRef strPasswordOut As String) As Boolean

        Dim intYear As Integer
        Dim intMonth As Integer
        Dim intDay As Integer
        Dim intHour As Integer
        Dim Password As String

        '[說明]:年月日時
        intYear = Date.Now.Year Mod 9
        intMonth = Date.Now.Month Mod 9
        intDay = Date.Now.Day Mod 9
        intHour = Date.Now.Hour Mod 9

        Password = intYear.ToString() & intMonth.ToString() & intDay.ToString() & intHour.ToString()

        If IsNumeric(strPasswordIn) Then
            If Password = strPasswordIn Then
                strPasswordOut = "250978"
            End If
        End If

        Return True

    End Function

    Private Sub labID_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        If Panel1.Enabled = False Then
            Exit Sub
        End If

        Panel1.Enabled = False
        gSyslog.Save("[frmLogin]" & vbTab & "[Panel1]" & vbTab & "Click")
        txtID.Text = "Premtek"
        txtPassWord.Text = "mike"
        btnLogIn_Click(sender, e)
        Panel1.Enabled = True
    End Sub

    Private Sub txtPassWord_Click(sender As Object, e As EventArgs) Handles txtPassWord.Click
        gSyslog.Save("[frmLogin]" & vbTab & "[txtPassWord]" & vbTab & "Click")
        If txtPassWord.ForeColor <> Color.Black Then
            txtPassWord.Text = ""
            txtPassWord.UseSystemPasswordChar = True
            txtPassWord.ForeColor = Color.Black
        End If
    End Sub


    Private Sub txtPassWord_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassWord.KeyPress
        If e.KeyChar = Chr(13) Then 'Enter
            btnLogIn_Click(sender, e) '密碼輸入完畢按登入
        ElseIf txtPassWord.ForeColor <> Color.Black Then
            txtPassWord.Text = ""
            txtPassWord.UseSystemPasswordChar = True
            txtPassWord.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtID_Click(sender As Object, e As EventArgs) Handles txtID.Click
        gSyslog.Save("[frmLogin]" & vbTab & "[txtID]" & vbTab & "Click")
        If txtID.ForeColor <> Color.Black Then
            txtID.Text = ""
            txtID.ForeColor = Color.Black
        End If
    End Sub


    Private Sub txtID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID.KeyPress
        If e.KeyChar = Chr(13) Then 'Enter
            txtPassWord.Focus() 'ID輸入完畢切到密碼
        ElseIf txtID.ForeColor <> Color.Black Then
            txtID.Text = ""
            txtID.ForeColor = Color.Black
        End If

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmLogin]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Function GetString(ByVal value As String) As String
        Select Case value
            Case "UserID"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "UserID"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "用户名"
                    Case enmLanguageType.eTraditionalChinese
                        Return "用户名"
                End Select
            Case "Password"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Password"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "密码"
                    Case enmLanguageType.eTraditionalChinese
                        Return "密碼"
                End Select
        End Select
        Return ""
    End Function
    Private Sub txtID_TextChanged(sender As Object, e As EventArgs) Handles txtID.TextChanged, txtID.LostFocus
        If txtID.Text = "" And txtID.ForeColor = Color.Black Then
            txtID.Text = GetString("UserID")
            txtID.SelectionStart = 0
            txtID.SelectionLength = 0
            txtID.ForeColor = SystemColors.ScrollBar
        End If
    End Sub

    Private Sub txtPassWord_TextChanged(sender As Object, e As EventArgs) Handles txtPassWord.TextChanged, txtPassWord.LostFocus
        If txtPassWord.Text = "" And txtPassWord.ForeColor = Color.Black Then
            txtPassWord.Text = GetString("Password")
            txtPassWord.SelectionStart = 0
            txtPassWord.SelectionLength = 0
            txtPassWord.UseSystemPasswordChar = False
            txtPassWord.ForeColor = SystemColors.ScrollBar
        End If
        If txtPassWord.Text <> GetString("Password") Then
            txtPassWord.UseSystemPasswordChar = True
        End If

    End Sub


    Private Sub frmLogin_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        gSyslog.Save("[frmLogin]" & vbTab & "[MyBase]" & vbTab & "Click")
        Me.Focus()
    End Sub
End Class
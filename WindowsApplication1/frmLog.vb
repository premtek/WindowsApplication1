Imports ProjectCore

Public Class frmLog
    ' ''' <summary>資料表</summary>
    ' ''' <remarks></remarks>
    'Dim mDataTable As New DataTable

    Private Sub frmLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpEndDate.Value = Date.Now '到目前為預設值
        dtpStartDate.Value = Date.Today.AddDays(-1) '往前一天開始
        chkInfo.Checked = True
        chkWarn.Checked = True
        chkAlarm.Checked = True
        chkError.Checked = True
    
        DataGridView1.Columns.Clear() '清除
        Select Case gUserLevel
            Case enmUserLevel.eOperator

                chkOperator.Visible = True
                chkEngineer.Visible = True
                chkManager.Visible = True
                chkAdministrator.Visible = False
                chkManufacturer.Visible = False

                chkOperator.Enabled = True
                chkEngineer.Enabled = False
                chkManager.Enabled = False
                chkAdministrator.Enabled = False
                chkManufacturer.Enabled = False

                chkOperator.Checked = True
                chkEngineer.Checked = False
                chkManager.Checked = False
                chkAdministrator.Checked = False
                chkManufacturer.Checked = False

            Case enmUserLevel.eEngineer
                chkOperator.Visible = True
                chkEngineer.Visible = True
                chkManager.Visible = True
                chkAdministrator.Visible = False
                chkManufacturer.Visible = False

                chkOperator.Enabled = True
                chkEngineer.Enabled = True
                chkManager.Enabled = False
                chkAdministrator.Enabled = False
                chkManufacturer.Enabled = False

                chkOperator.Checked = True
                chkEngineer.Checked = True
                chkManager.Checked = False
                chkAdministrator.Checked = False
                chkManufacturer.Checked = False

            Case enmUserLevel.eManager
                chkOperator.Visible = True
                chkEngineer.Visible = True
                chkManager.Visible = True
                chkAdministrator.Visible = False
                chkManufacturer.Visible = False

                chkOperator.Enabled = True
                chkEngineer.Enabled = True
                chkManager.Enabled = True
                chkAdministrator.Enabled = False
                chkManufacturer.Enabled = False

                chkOperator.Checked = True
                chkEngineer.Checked = True
                chkManager.Checked = True
                chkAdministrator.Checked = False
                chkManufacturer.Checked = False

            Case enmUserLevel.eAdministrator
                chkOperator.Visible = True
                chkEngineer.Visible = True
                chkManager.Visible = True
                chkAdministrator.Visible = True
                chkManufacturer.Visible = False

                chkOperator.Enabled = True
                chkEngineer.Enabled = True
                chkManager.Enabled = True
                chkAdministrator.Enabled = True
                chkManufacturer.Enabled = False

                chkOperator.Checked = True
                chkEngineer.Checked = True
                chkManager.Checked = True
                chkAdministrator.Checked = True
                chkManufacturer.Checked = False

            Case enmUserLevel.eSoftwareMaker
                chkOperator.Visible = True
                chkEngineer.Visible = True
                chkManager.Visible = True
                chkAdministrator.Visible = True
                chkManufacturer.Visible = True

                chkOperator.Enabled = True
                chkEngineer.Enabled = True
                chkManager.Enabled = True
                chkAdministrator.Enabled = True
                chkManufacturer.Enabled = True

                chkOperator.Checked = True
                chkEngineer.Checked = True
                chkManager.Checked = True
                chkAdministrator.Checked = True
                chkManufacturer.Checked = True

        End Select
    End Sub

    Private Sub frmLog_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DataGridView1.Height = Me.Height - 200
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'Sue20170627
        gSyslog.Save("[frmLog]" & vbTab & "[btnRefresh]" & vbTab & "Click")
        UpdateDataGridView()
    End Sub

    Sub UpdateDataGridView()
        Dim mFolderInfo As New System.IO.DirectoryInfo("D:\PIIData\Datalog")
        Dim mSubFolderInfo As System.IO.DirectoryInfo
        Dim sr As System.IO.StreamReader

        DataGridView1.Columns.Clear() '重建欄位
        DataGridView1.Columns.Add("Item", "Item")
        DataGridView1.Columns.Add("Start Time", "Start Time")
        DataGridView1.Columns.Add("End Time", "End Time")
        DataGridView1.Columns.Add("During Time", "During Time")
        DataGridView1.Columns.Add("User", "User")
        DataGridView1.Columns.Add("Level", "Level")
        DataGridView1.Columns.Add("Code", "Code")
        DataGridView1.Columns.Add("Description", "Description")
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Try
            ProgressBar1.Visible = True
            For Each folder In mFolderInfo.GetDirectories
                Dim mfolderName As String = folder.Name
                'Debug.Print("Length:" & mfolderName.Length)
                If mfolderName.Length = 8 Then '只處理符合長度的資料夾
                    Dim mfolderDate As Date = New Date(mfolderName.Substring(0, 4), mfolderName.Substring(4, 2), mfolderName.Substring(6, 2)) 'Ex:20151221 ->2015 12 21
                    Dim dateCondition1 As Integer = Date.Compare(dtpStartDate.Value.Date, mfolderDate) '左邊大於右邊是1, 等於是0, 左邊小於右邊是-1
                    Dim dateCondition2 As Integer = Date.Compare(mfolderDate, dtpEndDate.Value.Date)

                    If dateCondition1 = 1 Then '開始日期之前
                    ElseIf dateCondition2 = 1 Then '結束日期之後
                    Else '時間介於範圍內
                        mSubFolderInfo = New System.IO.DirectoryInfo(folder.FullName) '取得子資料夾路徑

                        For Each file In mSubFolderInfo.GetFiles '取得子資料夾內的所有檔案
                            If file.Name.EndsWith(".log") Then '只有log檔才處理
                                sr = New System.IO.StreamReader(file.FullName)
                                Dim allData As String = sr.ReadToEnd()
                                Dim lineData() As String = allData.Split(vbCrLf)
                                ProgressBar1.Maximum = lineData.Count - 1
                                For i As Integer = 1 To lineData.Count - 1 '標頭列略過
                                    Dim SplitedData() As String = lineData(i).Split("|") '分切
                                    If IsShow(SplitedData) Then
                                        DataGridView1.Rows.Add(SplitedData) '填入資料表

                                    End If
                                    ProgressBar1.Value = i
                                    ProgressBar1.Refresh()
                                Next

                            End If
                            'DataGridView1.DataSource = mDataTable
                            'DataGridView1.Refresh()
                        Next
                        DataGridView1.Refresh()
                    End If
                End If
            Next
            DataGridView1.Refresh()
            ProgressBar1.Visible = False
        Catch ex As Exception
            ProgressBar1.Visible = False
            MessageBox.Show(ex.Message)
        End Try



    End Sub
    
    Function IsShow(ByVal SplitedData() As String) As Boolean
        Dim mIsShow As Boolean = True '預設顯示
        If SplitedData.Count < 6 Then '資料欄數不足
            Return False
        End If
        Select Case SplitedData(4)
            Case "OPERATOR"
                If chkOperator.Checked <> True Then '既定選項未勾選表示不顯示
                    mIsShow = False
                End If
            Case "ENGINEER"
                If chkEngineer.Checked <> True Then
                    mIsShow = False
                End If
            Case "MANAGER"
                If chkManager.Checked <> True Then
                    mIsShow = False
                End If
            Case "ADMIN"
                If chkAdministrator.Checked <> True Then
                    mIsShow = False
                End If
            Case "PREMTEK"
                If chkManufacturer.Checked <> True Then
                    mIsShow = False
                End If
            Case "UNKNOWN"

        End Select
        Select Case SplitedData(5)
            Case "INFO"
                If chkInfo.Checked <> True Then
                    mIsShow = False
                End If
            Case "ERROR"
                If chkError.Checked <> True Then
                    mIsShow = False
                End If
            Case "ALARM"
                If chkAlarm.Checked <> True Then
                    mIsShow = False
                End If
            Case "WARN"
                If chkWarn.Checked <> True Then
                    mIsShow = False
                End If
            Case "IDLE" '未設計的強迫顯示
            Case "RUN"
            Case "????"
        End Select
        Return mIsShow
    End Function
End Class
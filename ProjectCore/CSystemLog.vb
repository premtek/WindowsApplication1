Imports System.IO.File
Imports System.Threading

''' <summary>訊息等級</summary>
''' <remarks></remarks>
Public Enum eMessageLevel
    ''' <summary>運行中</summary>
    ''' <remarks></remarks>
    Running = 0
    ''' <summary>資訊顯示</summary>
    ''' <remarks></remarks>
    Information = 1
    ''' <summary>OP通知</summary>
    ''' <remarks></remarks>
    Warning = 2
    ''' <summary>工程師通知</summary>
    ''' <remarks></remarks>
    Alarm = 3
    ''' <summary>異常停止</summary>
    ''' <remarks></remarks>
    [Error] = 4
    ''' <summary>閒置</summary>
    ''' <remarks></remarks>
    Idle = 5
    Count = 6
End Enum

''' <summary>系統Log記錄工具</summary>
''' <remarks></remarks>
Public Class CSystemLog
    Implements IDisposable

#Region "內部變數"
    ''' <summary>Log記錄佇列</summary>
    ''' <remarks></remarks>
    Dim mLogQueue As New Queue '(Of String)
    ''' <summary>計算作業派工執行緒</summary>
    ''' <remarks></remarks>
    Dim mThreadStart As New System.Threading.ThreadStart(AddressOf Dispatch)
    ''' <summary>作業派工執行緒</summary>
    ''' <remarks></remarks>
    Dim mThread As New System.Threading.Thread(mThreadStart)

    ''' <summary>執行緒事件通知</summary>
    ''' <remarks></remarks>
    Dim mAutoWait As New System.Threading.AutoResetEvent(False)

    Dim sw As System.IO.StreamWriter
    ''' <summary>檔案名稱</summary>
    ''' <remarks></remarks>
    Dim fileName As String
    ''' <summary>單次項次記錄</summary>
    ''' <remarks></remarks>
    Dim mItemCount As Integer
    ''' <summary>資料夾名稱</summary>
    ''' <remarks></remarks>
    Dim folderName As String
    Dim ExtendIndex As Integer
#End Region
    
    ''' <summary></summary>外部設定使用者, 預設為OP
    ''' <remarks></remarks>
    Public UserLevel As enmUserLevel = enmUserLevel.eOperator

#Region "內部方法"

    ''' <summary>取得可用檔名</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFileName() As String
        Do
            fileName = folderName & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & ExtendIndex & ".log"
            If Not System.IO.File.Exists(fileName) Then '檔案不存在
                Return fileName
            End If

            Dim fInfo As New System.IO.FileInfo(fileName) '檔案存在,資料取得
            If fInfo.Length < 1048576 Then 'log檔小於1MB時, Append進去
                Return fileName
            End If
            ExtendIndex += 1
        Loop '持續比對直到有新檔名, 或著有可使用的檔名
    End Function

    ''' <summary>派工作業</summary>
    ''' <remarks></remarks>
    Sub Dispatch()
        Do
            mAutoWait.WaitOne()
            If mLogQueue.Count > 0 Then '有資料
                fileName = GetFileName()
                If System.IO.File.Exists(fileName) Then '檔案存在
                    Try
                        sw = New IO.StreamWriter(fileName, True) 'Append資料
                        Dim mSyncQueue = Queue.Synchronized(mLogQueue) '建立同步Wrapper
                        For i As Integer = 0 To 19 '一次20筆
                            'If mLogQueue.Count > 0 Then
                            '    sw.WriteLine(mLogQueue.Dequeue)
                            'End If
                            If mSyncQueue.Count > 0 Then
                                sw.WriteLine(mSyncQueue.Dequeue)
                            End If
                        Next
                        sw.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Try
                    
                Else '檔案不存在,另開新檔
                    sw = New IO.StreamWriter(fileName, False)
                    Dim header As String = "項次|發生時間|結束時間|經過時間|使用者|類型|代碼|訊息"
                    'mItemCount = 0
                    sw.WriteLine(header)
                    Dim mSyncQueue = Queue.Synchronized(mLogQueue) '建立同步Wrapper
                    For i As Integer = 0 To 19 '一次20筆
                        'If mLogQueue.Count > 0 Then
                        '    sw.WriteLine(mLogQueue.Dequeue)
                        'End If
                        If mSyncQueue.Count > 0 Then
                            sw.WriteLine(mSyncQueue.Dequeue)
                        End If
                    Next
                    sw.Close()
                End If
            End If
            If disposedValue Then
                Exit Sub
            End If
        Loop

    End Sub

#End Region

    ''' <summary>寫入訊息記錄</summary>
    ''' <param name="msgLevel">訊息等級</param>
    ''' <param name="msgId">訊息代碼</param>
    ''' <param name="log">訊息內文</param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal log As String, Optional ByVal msgId As String = " ", Optional ByVal msgLevel As eMessageLevel = eMessageLevel.Information)
        Dim mStartTime As String = Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00") & "." & Format(Now.Millisecond, "000")
        Dim mEndTime As String = " "
        Dim mDuringTime As String = " "
        Dim mUserdata As String
        Dim mMsglv As String
        Dim mMsgID As String
        Select Case UserLevel
            Case enmUserLevel.eAdministrator
                mUserdata = "ADMIN"
            Case enmUserLevel.eEngineer
                mUserdata = "ENGINEER"
            Case enmUserLevel.eManager
                mUserdata = "MANAGER"
            Case enmUserLevel.eOperator
                mUserdata = "OPERATOR"
            Case enmUserLevel.eSoftwareMaker
                mUserdata = "PREMTEK"
            Case Else
                mUserdata = "UNKNOWN"
        End Select
        Select Case msgLevel
            Case eMessageLevel.Alarm
                mMsglv = "ALARM"
            Case eMessageLevel.Error
                mMsglv = "ERROR"
            Case eMessageLevel.Idle
                mMsglv = "IDLE"
            Case eMessageLevel.Information
                mMsglv = "INFO"
            Case eMessageLevel.Running
                mMsglv = "RUN"
            Case eMessageLevel.Warning
                mMsglv = "WARN"
            Case Else
                mMsglv = "????"
        End Select
        If msgId = " " Then
            mMsgID = msgId
        Else
            mMsgID = msgId.PadLeft(7, "0") ' Format(msgId, "0000000")
        End If
        Dim data As String = Format(mItemCount, "00000") & "|" & mStartTime & "|" & mEndTime & "|" & mDuringTime & "|" & mUserdata & "|" & mMsglv & "|" & mMsgID & "|" & log
        folderName = "D:\PIIData\DataLog\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"
        If Not System.IO.Directory.Exists(folderName) Then
            System.IO.Directory.CreateDirectory(folderName)
        End If
        'Debug.Print("mLogQueue.Enqueue:" & data)
        mLogQueue.Enqueue(data) '資料塞入佇列
        mItemCount += 1 '項次計數
        mAutoWait.Set() '非同步觸發起動
        

    End Sub

    Public Sub New()
        mThread.SetApartmentState(Threading.ApartmentState.STA) 'Soni + 2016.09.04 存檔衝突排除測試
        'Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Dispatch))
        mThread.Start()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mAutoWait.Dispose()
                sw.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region



#Region "CCD存檔"
    Dim CCDsw As System.IO.StreamWriter
    Dim CCDfolderName As String
    Dim CCDfileName As String
    Private CCDMessage As New List(Of String)
    ''' <summary>訊息等級</summary>
    ''' <remarks></remarks>
    Public Enum eCCDMessageProcess
        ''' <summary>訊息重置</summary>
        ''' <remarks></remarks>
        Restart = 0
        ''' <summary>資訊加入</summary>
        ''' <remarks></remarks>
        Add = 1
        ''' <summary>統一存檔</summary>
        ''' <remarks></remarks>
        Save = 2

    End Enum

    Function GetCCDFileName() As String
        Do
            CCDfileName = CCDfolderName & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & ExtendIndex & ".log"
            If Not System.IO.File.Exists(CCDfileName) Then '檔案不存在
                Return CCDfileName
            End If

            Dim fInfo As New System.IO.FileInfo(CCDfileName) '檔案存在,資料取得
            If fInfo.Length < 1048576 Then 'log檔小於1MB時, Append進去
                Return CCDfileName
            End If
            ExtendIndex += 1
        Loop '持續比對直到有新檔名, 或著有可使用的檔名
    End Function



    Public Sub CCDSave(ByVal log As String, ByVal CCDMessageProcess As eCCDMessageProcess)
        Dim mStartTime As String = Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00") & "." & Format(Now.Millisecond, "000")
        Dim mEndTime As String = " "
        Dim mDuringTime As String = " "
        Try
            '[Note]開檔
            CCDfolderName = "D:\PIIData\DataLog\CCD\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"
            If Not System.IO.Directory.Exists(CCDfolderName) Then
                System.IO.Directory.CreateDirectory(CCDfolderName)
            End If



            Dim data As String = Format(mItemCount, "00000") & "|" & mStartTime & "|" & mEndTime & "|" & mDuringTime & "|" & log
            Select Case CCDMessageProcess
                Case eCCDMessageProcess.Restart
                    CCDMessage.Clear()
                    Dim header As String = "項次|發生時間|結束時間|經過時間|訊息"
                    CCDMessage.Add(header)
                Case eCCDMessageProcess.Add
                    CCDMessage.Add(data)

                Case eCCDMessageProcess.Save
                    CCDMessage.Add("===============================================================================")
                    '[Note]一次存檔
                    CCDfileName = GetCCDFileName()
                    CCDsw = New IO.StreamWriter(CCDfileName, True)
                    For i As Integer = 0 To CCDMessage.Count - 1
                        CCDsw.WriteLine(CCDMessage.Item(i))
                    Next
                    CCDsw.Close()

            End Select

            'mLogQueue.Enqueue(data) '資料塞入佇列
            mItemCount += 1 '項次計數
            'mAutoWait.Set() '非同步觸發起動
        Catch ex As Exception
            MsgBox("CCDSaveLog Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try


    End Sub



#End Region
   
End Class

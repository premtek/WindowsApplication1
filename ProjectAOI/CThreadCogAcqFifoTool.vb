Imports Cognex.VisionPro

Public Class CThreadCogAcqFifoTool
    Implements IDisposable

    ''' <summary>錯誤訊息</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorMessage As String
    ''' <summary>影像工具</summary>
    ''' <remarks></remarks>
    Public Subject As CogAcqFifoTool
    ''' <summary>執行緒事件通知</summary>
    ''' <remarks></remarks>
    Dim mAutoWait As New System.Threading.AutoResetEvent(False)
    ''' <summary>Queue的執行緒</summary>
    ''' <remarks></remarks>
    Dim threadStart As New System.Threading.ThreadStart(AddressOf AcqTool_Run)
    ''' <summary>命令發送Queue的執行緒</summary>
    ''' <remarks></remarks>
    Dim thread As New System.Threading.Thread(threadStart)
    ''' <summary>關閉中</summary>
    ''' <remarks></remarks>
    Dim mIsDisposing As Boolean
    Public Property IsStop As Boolean
        Get
            Return mStop
        End Get
        Set(state As Boolean)
            mStop = state
        End Set
    End Property
    ''' <summary>暫停中</summary>
    ''' <remarks></remarks>
    Dim mStop As Boolean
    ''' <summary>外部設是否硬體觸發</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsHardwareTrigger As Boolean
        Get
            Return mIsHardwareTrigger
        End Get
        Set(state As Boolean)
            mIsHardwareTrigger = state
            '[Note]:為了讓mAutoWait醒過來
            'If state = True Then
            '    mAutoWait.Set()
            'Else

            'End If
        End Set
    End Property
    ''' <summary>是否硬體觸發</summary>
    ''' <remarks></remarks>
    Dim mIsHardwareTrigger As Boolean
    ''' <summary>外部讀取Ticket</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Ticket As Integer
        Get
            Return mTicket
        End Get
        Set(value As Integer)
            mTicket = value
        End Set
    End Property
    ''' <summary>內部傳遞Ticket</summary>
    ''' <remarks></remarks>
    Dim mTicket As Integer
    ''' <summary>CCD索引</summary>
    ''' <remarks></remarks>
    Public CCDNo As Integer
    ''' <summary>外部設置是否只取像</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsAcqImageOnly As Boolean
        Get
            Return mIsAcqImageOnly
        End Get
        Set(state As Boolean)
            mIsAcqImageOnly = state
        End Set
    End Property
    ''' <summary>是否純取像,不計算</summary>
    ''' <remarks></remarks>
    Dim mIsAcqImageOnly As Boolean = False
    ''' <summary>運行結束</summary>
    ''' <remarks></remarks>
    Dim mIsRunFinish As Boolean = True
  
    ''' <summary>是否運轉完畢</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsRunFinish As Boolean
        Get
            Return mIsRunFinish
        End Get
    End Property

    ''' <summary>輸出影像</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OutputImage As Cognex.VisionPro.ICogImage   'wenda 20160614



    ''' <summary>場景編號</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SceneName As String

    Sub Start()
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
        'System.Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf ToolBlock_Run)) '改為ThreadPool
        'Task.Run(Sub()
        '             ToolBlock_Run()
        '         End Sub)
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]
        'If thread.ThreadState <> Threading.ThreadState.WaitSleepJoin Then
        '    thread.Start()
        'End If

        '[Note]參考Cognex取像做修改
        AddHandler Subject.Operator.Complete, AddressOf AcqTool_Run
    End Sub
    ''' <summary>用途</summary>
    ''' <remarks></remarks>
    Sub New()
        
    End Sub
    ''' <summary>物件建立</summary>
    ''' <param name="isHardwareTrigger"></param>
    ''' <remarks></remarks>
    Sub New(ByVal isHardwareTrigger As Boolean)
        mIsHardwareTrigger = isHardwareTrigger
    End Sub

    ''' <summary>運行後</summary>
    ''' <remarks></remarks>
    Public Event OnRunSuccess(sender As Object, ByVal e As AOIEventArgs)
    ''' <summary>運行前</summary>
    ''' <remarks></remarks>
    Public Event OnBeforeRun(sender As Object, ByVal e As AOIEventArgs)
    Private Sub AcqTool_Run()

        Dim numReadyVal As Integer, numPendingVal As Integer
        Dim busyVal As Boolean
        Dim info As New CogAcqInfo
        Dim mStopWatch As New Stopwatch
        'Dim Value As Integer
        Try
            If Subject Is Nothing Then
                ErrorMessage = "取像工具錯誤!Subject Is Nothing"
                MsgBox(ErrorMessage, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
            'Do
            '[Note]切換軟硬體觸發時需stop，不然會偶發hang住
            If mStop Then
                'System.Threading.Thread.CurrentThread.Join(1)
                Exit Sub
            End If

            If mIsDisposing = True Then
                Exit Sub
            End If

            'If Not mIsHardwareTrigger Then
            '    mAutoWait.WaitOne()
            'End If


            'mStopWatch.Restart()

            mIsRunFinish = False '開始運轉前, 清除記錄
            Dim e As New AOIEventArgs
            mStopWatch.Restart()
            SyncLock e
                e.ChannelNo = 0
                e.SceneName = SceneName
                e.Ticket = mTicket
                e.CCDNo = CCDNo
                e.IsAcqImageOnly = mIsAcqImageOnly '20160928
                RaiseEvent OnBeforeRun(Me, e)

                'Do
                Subject.Operator.GetFifoState(numPendingVal, numReadyVal, busyVal)
                'If numReadyVal > 0 Then
                '    Exit Do
                'End If
                'Loop Until mStopWatch.ElapsedMilliseconds > 1000
                If numReadyVal > 0 Then
                    OutputImage = Subject.Operator.CompleteAcquireEx(info)
                    'Debug.Print("CCD" & (CCDNo + 1).ToString & " 場景 " & SceneName & "取像(" & mTicket & ") 完成" & mStopWatch.ElapsedMilliseconds & " , " & Subject.RunStatus.ProcessingTime)
                    'Debug.Print("CCD" & (CCDNo + 1).ToString & " 曝光 " & Subject.Operator.OwnedExposureParams.Exposure)
                    RaiseEvent OnRunSuccess(Me, e)
                    mTicket += 1
                End If
                'If Not mIsHardwareTrigger Then
                '    Subject.Operator.StartAcquire()
                '    'Debug.Print("StartAcquire: " & Value)
                'End If

                'Subject.Run() 
                'OutputImage = Subject.OutputImage
                'If OutputImage IsNot Nothing Then
                '    Debug.Print("CCD" & (CCDNo + 1).ToString & " 場景 " & SceneName & "取像(" & mTicket & ") 完成" & mStopWatch.ElapsedMilliseconds & " , " & Subject.RunStatus.ProcessingTime)
                '    RaiseEvent OnRunSuccess(Me, e)
                '    mTicket += 1
                'End If
                'Debug.Print("CCD" & (CCDNo + 1).ToString & " 場景 " & SceneName & "取像(" & mTicket & ") 完成") ' & mStopWatch.ElapsedMilliseconds)
                mIsRunFinish = True '運轉完成
            End SyncLock
            'Debug.Print("完成" & mStopWatch.ElapsedMilliseconds)
            'Loop
        Catch ex As Exception
            Debug.Print(ex.ToString)
            mIsRunFinish = True
        End Try

    End Sub


    ''' <summary>開始動作(取像/定位/計算)</summary>
    ''' <param name="ticket">-1表示採用預設累計 其他為自行指定. 自行指定可能發生無法預期之行為</param>
    ''' <remarks></remarks>
    Public Function Run(ByVal isAcqImageOnly As Boolean, ByVal ticket As Integer) As Integer
        mIsRunFinish = False '開始運轉前, 清除記錄
        'mTicket = ticket     '觸發開始,Ticket
        mIsAcqImageOnly = isAcqImageOnly
        If ticket <> -1 Then mTicket = ticket
        'Debug.Print("CCD" & (CCDNo + 1).ToString & "取像:" & mTicket)
        'mAutoWait.Set()
        If Not mIsHardwareTrigger Then
            If Subject.Operator Is Nothing Then

            Else
                Subject.Operator.StartAcquire()
                'Debug.Print("StartAcquire: " & Value)
            End If

        End If
        Return mTicket
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mAutoWait.Dispose()
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

End Class
Imports Cognex.VisionPro
Imports Cognex.VisionPro.ToolBlock
Imports ProjectCore


Public Class CThreadCogToolBlock
    Implements IDisposable
    ''' <summary>輸出結構</summary>
    ''' <remarks></remarks>
    Public OutputParam As New COutputParam
    ''' <summary>錯誤訊息</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorMessage As String

    ''' <summary>影像工具</summary>
    ''' <remarks></remarks>
    Public Subject As CogToolBlock
    'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
    '2017.02.17 [S]
    ' ''' <summary>執行緒事件通知</summary>
    ' ''' <remarks></remarks>
    'Dim mAutoWait As New System.Threading.AutoResetEvent(False)
    ' ''' <summary>Queue的執行緒</summary>
    ' ''' <remarks></remarks>
    'Dim threadStart As New System.Threading.ThreadStart(AddressOf ToolBlock_Run)
    ' ''' <summary>命令發送Queue的執行緒</summary>
    ' ''' <remarks></remarks>
    'Dim thread As New System.Threading.Thread(threadStart)
    '2017.02.17 [E]
    'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
    ''' <summary>關閉中</summary>
    ''' <remarks></remarks>
    Dim mIsDisposing As Boolean
    ''' <summary>是否硬體觸發</summary>
    ''' <remarks></remarks>
    Dim mIsHardwareTrigger As Boolean

    ''' <summary>外部讀取Ticket</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Ticket As Integer
        Get
            Return mTicket
        End Get
    End Property
    ''' <summary>內部傳遞Ticket</summary>
    ''' <remarks></remarks>
    Dim mTicket As Integer
    ''' <summary>CCD索引</summary>
    ''' <remarks></remarks>
    Public CCDNo As Integer
    Public ChannelNo As Integer
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
    ''' <summary>輸入影像</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputImage As Cognex.VisionPro.ICogImage     'wenda 20160614


    ''' <summary>場景編號</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SceneName As String

    Sub New(ByVal enmCCDNo As Integer, Optional ByVal Trigger As Boolean = False)

        CCDNo = enmCCDNo
        mIsHardwareTrigger = Trigger
        'Eason 20170221 Ticket:100034 , Memory Free Part5 About AOI
        Subject = New CogToolBlock()
        Subject.GarbageCollectionEnabled = True
        Subject.GarbageCollectionFrequency = 5

    End Sub

    ''' <summary>運行後</summary>
    ''' <remarks></remarks>
    Public Event OnRunSuccess(sender As Object, ByVal e As AOIEventArgs)
    ''' <summary>運行前</summary>
    ''' <remarks></remarks>
    Public Event OnBeforeRun(sender As Object, ByVal e As AOIEventArgs)

    Dim mCCDTimeStopWatch As New Stopwatch

    'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
    ''' <summary>開始動作(取像/定位/計算)</summary>
    ''' <param name="ticket">-1表示採用預設累計 其他為自行指定. 自行指定可能發生無法預期之行為</param>
    ''' <remarks></remarks>
    Public Function Run(ByVal isAcqImageOnly As Boolean, ByVal ticket As Integer, ByVal CCDNo As Integer, ByVal waitReturn As Boolean) As Integer
        mCCDTimeStopWatch.Restart()
        mIsRunFinish = False '開始運轉前, 清除記錄
        'mTicket = ticket     '觸發開始,Ticket
        mIsAcqImageOnly = isAcqImageOnly
        If ticket <> -1 Then mTicket = ticket
        Me.CCDNo = CCDNo
        'If waitReturn Then'2017.02.17 'Eason 20170217 Ticket:100032 , Memory Free Part3
        If Subject Is Nothing Then
            ErrorMessage = "定位工具錯誤!Subject Is Nothing"
            MsgBox(ErrorMessage, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return mTicket
        End If
        If Subject.Inputs.Contains("InputImage") Then
            Subject.Inputs("InputImage").Value = InputImage
            OutputImage = InputImage
        End If
        Dim e As New AOIEventArgs
        e.ChannelNo = ChannelNo
        e.SceneName = SceneName
        e.Ticket = mTicket
        e.CCDNo = CCDNo
        RaiseEvent OnBeforeRun(Me, e) '運行前清除參數 
        gSyslog.CCDSave("CThreadCogToolBlock Run time 1: " & mCCDTimeStopWatch.ElapsedMilliseconds & " ms", CSystemLog.eCCDMessageProcess.Add)
        Subject.Run()
        gSyslog.CCDSave("CThreadCogToolBlock Run time 2: " & mCCDTimeStopWatch.ElapsedMilliseconds & " ms", CSystemLog.eCCDMessageProcess.Add)
        RaiseEvent OnRunSuccess(Me, e) '運行後剖析參數
        gSyslog.CCDSave("CThreadCogToolBlock Run time 3: " & mCCDTimeStopWatch.ElapsedMilliseconds & " ms", CSystemLog.eCCDMessageProcess.Add)
        mIsRunFinish = True '運轉完成
        Return mTicket
        'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
        '2017.02.17 [S]
        'Else
        '    mAutoWait.Set()
        '    Return mTicket
        'End If
        '2017.02.17 [E]
        'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not Subject Is Nothing Then
                    Subject.Dispose() 'Soni 2017.03.22
                End If
                'mAutoWait.Dispose() '2017.02.17 'Eason 20170217 Ticket:100032 , Memory Free Part3
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。

        'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
        OutputImage = Nothing
        InputImage = Nothing
        If (Not IsNothing(Subject)) Then
            Subject.GarbageCollectionFrequency = 0
            Subject = New CogToolBlock()
            Subject = Nothing
            GC.Collect() 'Eason 20170221 Ticket:100034 , Memory Free Part5 About AOI
        End If

        'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

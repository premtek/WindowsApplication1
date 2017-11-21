Imports System.Globalization

Public Class ucMemoryMonitor

    Private MonitorTimer As System.Windows.Forms.Timer = New Timer()

    Private BaseValue As Decimal
    Private CalBuffer As Decimal

    Private LastRecordMemoryValue As Decimal
    Private LastRecordThreadCountValue As Integer

    Private CountBuffer As Integer
    Private CountMax As Integer
    Private NowMemoryValue As Decimal
    Private NowThreadCount As Integer

    Private mIsEnableMemoryThresholdValue As Boolean
    Private mIsEnableThreadThresholdValue As Boolean

    Private mSetMemoryThresholdValue As Decimal
    Private mSetThreadThresholdValue As Integer

    Private IsStartCalculateBaseValue As Boolean

    Private mScanIntervalTime As Integer = 1000

    Public Delegate Sub NotifyMessageDelegate(ByVal Message As String)
    Public Event NotifyRuntimeMessageHandle As NotifyMessageDelegate

    Protected Overrides Sub OnParentChanged(e As System.EventArgs)
        parent = Me.ParentForm
        MyBase.OnParentChanged(e)
    End Sub

    Public Property SetMemoryThresholdValue As Decimal
        Set(value As Decimal)
            mSetMemoryThresholdValue = value
        End Set
        Get
            Return mSetMemoryThresholdValue
        End Get
    End Property

    Public Property SetThreadThresholdValue As Integer
        Set(value As Integer)
            mSetThreadThresholdValue = value
        End Set
        Get
            Return mSetThreadThresholdValue
        End Get
    End Property

    Public Property IsEnableMemoryThresholdValue As Boolean
        Set(value As Boolean)
            mIsEnableMemoryThresholdValue = value
        End Set
        Get
            Return mIsEnableMemoryThresholdValue
        End Get
    End Property

    Public Property IsEnableThreadThresholdValue As Boolean
        Set(value As Boolean)
            mIsEnableThreadThresholdValue = value
        End Set
        Get
            Return mIsEnableThreadThresholdValue
        End Get
    End Property

    Public ReadOnly Property IsMonitorValueChange As Boolean
        Get
            If (IsEnableMemoryThresholdValue AndAlso Math.Abs(LastRecordMemoryValue - NowMemoryValue) > SetMemoryThresholdValue) Then
                Return True
            End If
            If (IsEnableThreadThresholdValue AndAlso Math.Abs(LastRecordThreadCountValue - NowThreadCount) > SetThreadThresholdValue) Then
                Return True
            End If
            Return False
        End Get

    End Property

    Public Sub UpdateThreadCount(iCount As Integer)
        NowThreadCount = iCount
    End Sub

    Public Sub UpdateMemoryValue(MemorySize As Decimal)
        NowMemoryValue = MemorySize
        If (IsStartCalculateBaseValue) Then
            CalBuffer = CalBuffer + MemorySize
            CountBuffer = CountBuffer + 1
            If (CountBuffer >= CountMax) Then
                BaseValue = CalBuffer / (CountBuffer + 1)
                IsStartCalculateBaseValue = False
            End If
        End If
    End Sub

    Public Property StartCalculateBase As Boolean
        Set(value As Boolean)
            If (value = True) Then
                IsStartCalculateBaseValue = True
                CalBuffer = 0
                BaseValue = 0
                CountBuffer = 0
            End If
            IsStartCalculateBaseValue = value

        End Set
        Get
            Return IsStartCalculateBaseValue
        End Get
    End Property

    Public Property ScanIntervalTime As Integer
        Set(value As Integer)
            mScanIntervalTime = value
            MonitorTimer.Interval = mScanIntervalTime
        End Set
        Get
            Return mScanIntervalTime
        End Get
    End Property

    Public Sub Open()
        IsEnableMemoryThresholdValue = True
        IsEnableThreadThresholdValue = True
        MonitorTimer.Enabled = True
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               cbEnable.Checked = True
                           End Sub)
        End If

    End Sub

    Public Sub Close()
        MonitorTimer.Enabled = False
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               cbEnable.Checked = False
                           End Sub)
        End If
    End Sub

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        BaseValue = 0
        CalBuffer = 0
        IsStartCalculateBaseValue = False
        CountMax = 3
        NowMemoryValue = 0
        LastRecordMemoryValue = 0
        NowThreadCount = 0
        MonitorTimer.Enabled = False
        AddHandler MonitorTimer.Tick, AddressOf tmMemoryThreadMonitor

    End Sub

    Private Sub tmMemoryThreadMonitor(sender As Object, e As EventArgs)

        Dim MemorySize As Decimal = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64
        Dim ThreadCount As Integer = System.Diagnostics.Process.GetCurrentProcess().Threads.Count

        UpdateMemoryValue(MemorySize)
        UpdateThreadCount(ThreadCount)

        Dim ShowMessage As String

        If (IsMonitorValueChange) Then
            ShowMessage = GetMemoryMessage()
            If (Not IsNothing(Parent)) Then
                Me.BeginInvoke(Sub()
                                   'Dim strTimeAndMessage As String = System.DateTime.Now.ToString(" HH:mm:ss:fff => ")
                                   Dim strTimeAndMessage As String = System.DateTime.Now.ToString(" HH:mm:ss ,")
                                   strTimeAndMessage += ShowMessage

                                   If (lstRecord.Items.Count > 1024) Then
                                       lstRecord.Items.RemoveAt(0)
                                   End If
                                   lstRecord.Items.Add(strTimeAndMessage)
                                   If (cbListNoToEnd.Checked = False) Then
                                       lstRecord.TopIndex = lstRecord.Items.Count - 1
                                   End If
                               End Sub)
            End If
            RaiseEvent NotifyRuntimeMessageHandle(ShowMessage)

        End If

    End Sub

    Public Function GetMemoryMessage() As String
        LastRecordThreadCountValue = NowThreadCount
        LastRecordMemoryValue = NowMemoryValue
        Dim strReSultString As String = ""
        If (BaseValue > 0) Then
            Dim MinValue As Decimal = NowMemoryValue - BaseValue
            If (MinValue > 1) Then
                strReSultString = "[M]: " _
                        & NowMemoryValue.ToString("0,0", CultureInfo.InvariantCulture) & " (+) " & (MinValue).ToString("0,0", CultureInfo.InvariantCulture) _
                        & " , [T]: " & NowThreadCount.ToString()
                Return strReSultString
            ElseIf (MinValue < -1) Then
                strReSultString = "[M]: " _
                & NowMemoryValue.ToString("0,0", CultureInfo.InvariantCulture) & " (-) " & (MinValue).ToString("0,0", CultureInfo.InvariantCulture) _
                & " , [T]: " + NowThreadCount.ToString()
                Return strReSultString
            End If
        End If
        strReSultString = "[M]: " + NowMemoryValue.ToString("0,0", CultureInfo.InvariantCulture) + " , [T]: " + NowThreadCount.ToString()
        Return strReSultString
    End Function

    Private Sub cbEnable_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnable.CheckedChanged
        If (cbEnable.Checked) Then
            Open()
        Else
            Close()
        End If
    End Sub

End Class

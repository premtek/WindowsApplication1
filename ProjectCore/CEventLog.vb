Imports System.Threading
Imports System.IO
Imports System.Timers

Public Class MyEventLog

    Inherits System.Threading.SynchronizationContext

#Region "[Private] Variable"

    Private mEventSendTask As Task
    Private mEventSendTaskTokenSource As CancellationTokenSource
    Private mEventSendTaskToken As CancellationToken
    Private AutoRecordInterval As System.Timers.Timer = New System.Timers.Timer()
    Private ReadOnly messages As Queue(Of String) = New Queue(Of String)()
    Private ReadOnly syncHandle As Object = New Object()

    Private isRunning As Boolean = True
    Private isTimerTrigger As Boolean = False
    Private WriteMessageCount As Integer = 100
    Private FileMessageCount As Integer = 1000
    Private AutoRecordIntervalTime As Integer = 5000
    Private strSaveLogPath As String = String.Empty

    Public IsEnable As Boolean = True

#End Region

    Public Sub New(SaveLogPath As String)

        If (Not Directory.Exists(SaveLogPath)) Then
            Directory.CreateDirectory(SaveLogPath)
        End If
        strSaveLogPath = SaveLogPath

        AutoRecordInterval.Interval = AutoRecordIntervalTime

        AddHandler AutoRecordInterval.Elapsed, New ElapsedEventHandler(Sub(obj, tar)

                                                                           SyncLock (syncHandle)
                                                                               isTimerTrigger = True
                                                                               SignalContinue()
                                                                           End SyncLock


                                                                       End Sub)

        mEventSendTaskTokenSource = New CancellationTokenSource()
        mEventSendTaskToken = mEventSendTaskTokenSource.Token
        mEventSendTask = New Task(Sub()
                                      MyEventSendThread(mEventSendTaskToken)
                                  End Sub, mEventSendTaskToken)
        mEventSendTask.Start()
        AutoRecordInterval.Start()
    End Sub

    Public Sub ManualDispose()

        Cancel()
        AutoRecordInterval.Enabled = False
        mEventSendTaskTokenSource.Cancel()

    End Sub

    Public Sub Log(strMessage As String)

        If (IsEnable) Then
            SyncLock (syncHandle)
                Dim TotalString As String = String.Format("{0} , {1}", System.DateTime.Now.ToString("yyyy/MM/dd , HH:mm:ss:fff "), strMessage)
                messages.Enqueue(TotalString)
                SignalContinue()
            End SyncLock
        End If
    End Sub

    Private Sub SignalContinue()
        Monitor.Pulse(syncHandle)
    End Sub

    Private Function CanContinue() As Boolean
        SyncLock (syncHandle)
            Return isRunning
        End SyncLock
    End Function

    Public Sub Cancel()
        SyncLock (syncHandle)
            isRunning = False
            SignalContinue()
        End SyncLock
    End Sub

    Private Sub MyEventSendThread(_Token As CancellationToken)

        While (Not _Token.IsCancellationRequested)
            RunMessagePump()
        End While
        If (_Token.IsCancellationRequested) Then

        End If
    End Sub

    Private Function GrabItem() As Queue(Of String)
        SyncLock (syncHandle)

            While (CanContinue() And messages.Count < WriteMessageCount)

                Monitor.Wait(syncHandle)
                If (isTimerTrigger) Then
                    Exit While
                End If

            End While

            AutoRecordInterval.Stop()
            isTimerTrigger = False
            AutoRecordInterval.Start()

            Dim qMessageBuffer As Queue(Of String) = New Queue(Of String)

            If (messages.Count = 0) Then
                qMessageBuffer = Nothing
            Else
                Do While (messages.Count > 0)
                    qMessageBuffer.Enqueue(messages.Dequeue())
                Loop
            End If
            Return qMessageBuffer
        End SyncLock

    End Function

    Private Sub RunMessagePump()
        While (CanContinue())

            Dim vectorMessage As Queue(Of String) = GrabItem()
            If (Not IsNothing(vectorMessage) AndAlso vectorMessage.Count > 0) Then

                Dim FileGroup As String() = Directory.GetFiles(strSaveLogPath)
                Dim MaxCount As ULong = 0

                For Each item In FileGroup
                    Dim strGetFile As String = Path.GetFileNameWithoutExtension(item)
                    If (System.Text.RegularExpressions.Regex.IsMatch(strGetFile, "^Log_\d{1,5}$")) Then
                        Dim GetNumString As String = strGetFile.Replace("Log_", "")
                        Dim inum As Long = 0
                        If (Int64.TryParse(GetNumString, inum)) Then
                            If (inum > MaxCount) Then
                                MaxCount = inum
                            End If
                        End If
                    End If
                Next

                If (MaxCount = 0) Then
                    MaxCount = 1
                End If
                Dim SaveLogFileName As String = String.Format("{0}\\Log_{1}.txt", strSaveLogPath, MaxCount.ToString())
                Dim FileRecordCount As Integer = 0

                While (vectorMessage.Count > 0)

                    If (File.Exists(SaveLogFileName)) Then

                        Using Open As StreamReader = New StreamReader(SaveLogFileName)
                            While (Not IsNothing(Open.ReadLine()))
                                FileRecordCount = FileRecordCount + 1
                            End While
                            Open.Close()
                        End Using
                    End If

                    Dim iNeedRecordMessageCount As Integer = FileMessageCount - FileRecordCount

                    If (iNeedRecordMessageCount > 0) Then

                    End If

                    Using Write As StreamWriter = File.AppendText(SaveLogFileName)
                        While (iNeedRecordMessageCount > 0 And vectorMessage.Count > 0)
                            iNeedRecordMessageCount = iNeedRecordMessageCount - 1
                            Write.WriteLine(vectorMessage.Dequeue())
                        End While
                        Write.Close()
                    End Using
                    FileRecordCount = 0
                    MaxCount = MaxCount + 1
                    SaveLogFileName = String.Format("{0}\\Log_{1}.txt", strSaveLogPath, MaxCount.ToString())

                End While

            End If


        End While
    End Sub

End Class




'Eason Ticket Define:
'100010 , Add Pattern Copy Function
'100011 , Bug fix

'100030 , Memory Free
'100031 , Memory Free Part2
'100032 , Memory Free Part3
'100033 , Memory Free Part4
'
'100050 , Add Jet Time
'100060 , Memory Log
'100070 , Operator form frash
'100080 , Add Arc Type Parameter

'100090 , System Update Crash
'100100 , XY Offset from CSV File
Imports System.Text
Imports System.IO.Ports
Imports ProjectCore

Public Class CTriggerBoard_30

    Implements ITriggerBoard

#Region "Definitions"

    Public Property ErrMsg As String Implements ITriggerBoard.ErrMsg

    ''' <summary>[f Command(F Command的續傳方式)之陣列大小]</summary>
    ''' <remarks></remarks>
    Private Const mFCmdTRArraySize As Integer = 740

    ''' <summary>[f Command的續傳量(F Command資料續傳-->f Command)]</summary>
    ''' <remarks></remarks>
    Private Const mTransmissionResumingOfStepCount As Integer = 20

    ''' <summary>[SerialPort]</summary>
    ''' <remarks></remarks>
    Private WithEvents mSerialPort As New SerialPort
    ''' <summary>[資料接收暫存]</summary>
    ''' <remarks></remarks>
    Private mRecievedData As StringBuilder
    ''' <summary>[Stopwatch]</summary>
    ''' <remarks></remarks>
    Private mStopWatch As Stopwatch
    ''' <summary>[傳送指令] </summary>
    ''' <remarks></remarks>
    Private mSendCmd() As Byte
    ''' <summary>[J Command 欲傳送的內容(Recipe)]</summary>
    ''' <remarks></remarks>
    Private mSendJCmdArray(1048576) As Byte       '[2^20]
    ''' <summary>[紀錄 J Command指到的陣列位置]</summary>
    ''' <remarks></remarks>
    Private mJCmdArrayIndex As Integer
    ''' <summary>[L Command欲傳送的內容(Recipe)]</summary>
    ''' <remarks></remarks>
    Private mSendVisionCmdArray(1048576) As Byte        '[2^20]
    ''' <summary>[F Command欲傳送的內容(Recipe)]</summary>
    ''' <remarks></remarks>
    Private mSendFCmdArray(1048576) As Byte             '[2^20]
    ''' <summary>[f Command欲傳送的內容(Recipe)-->F Command的續傳方式]
    ''' 此有固定大小之限制(20筆資料量)</summary>
    ''' <remarks></remarks>
    Private mSendFCmdTRArray(mFCmdTRArraySize) As Byte
    ''' <summary>[紀錄 L Command指到的陣列位置]</summary>
    ''' <remarks></remarks>
    Private mVisionCmdArrayIndex As Integer
    ''' <summary>[紀錄 F Command指到的陣列位置]</summary>
    ''' <remarks></remarks>
    Private mFCmdArrayIndex As Integer
    ''' <summary>[紀錄 f Command指到的陣列位置-->F Command的續傳方式]</summary>
    ''' <remarks></remarks>
    Private mFCmdTRArrayIndex As Integer
    ''' <summary>紀錄已經累計多少個資料內容(Byte) </summary>
    ''' <remarks></remarks>
    Private mByteCount As Integer
    ''' <summary>[字串分析]</summary>
    ''' <remarks></remarks>
    Private mAnalysisString As String
    ''' <summary>[忙碌中]</summary>
    ''' <remarks></remarks>
    Dim mIsBusy As Boolean
    Public ReadOnly Property IsBusy As Boolean Implements ITriggerBoard.IsBusy
        Get
            Return mIsBusy
        End Get
    End Property
    ''' <summary>[TimeOut(逾時)]</summary>
    ''' <remarks></remarks>
    Dim mIsTimeOut As Boolean
    Public ReadOnly Property IsTimeOut As Boolean Implements ITriggerBoard.IsTimeOut
        Get
            Try
                If (mStopWatch.ElapsedMilliseconds >= mTimeoutTimes) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mSerialPort.DiscardInBuffer()
                    mStopWatch.Stop()
                    mRecievedData.Length = 0
                End If
                Return mIsTimeOut

            Catch ex As Exception
                ErrMsg = ex.ToString()
                If (mStopWatch.ElapsedMilliseconds >= mTimeoutTimes) Then
                    mIsBusy = False
                    mIsTimeOut = True
                    mStopWatch.Stop()
                End If
                Return mIsTimeOut
            End Try
        End Get
    End Property

    ''' <summary>[設定Timeout時間]</summary>
    ''' <remarks></remarks>
    Private mTimeoutTimes As Integer
    Public Property TimeoutTimes As Integer Implements ITriggerBoard.TimeoutTimes
        Get
            Return mTimeoutTimes
        End Get
        Set(value As Integer)
            mTimeoutTimes = value
        End Set
    End Property
    ''' <summary>[是否初始化成功]</summary>
    ''' <remarks></remarks>
    Private mIsInitialOK As Boolean
    Public ReadOnly Property IsInitialOK As Boolean Implements ITriggerBoard.IsInitialOK
        Get
            Return mIsInitialOK
        End Get
    End Property

    Public ReadOnly Property TransmissionResumingOfStepCount As Integer Implements ITriggerBoard.TransmissionResumingOfStepCount
        Get
            Return mTransmissionResumingOfStepCount
        End Get
    End Property
    ''' <summary>[Is Open(通訊埠連線狀態)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PortIsOpen As Boolean Implements ITriggerBoard.PortIsOpen
        Get
            Return mSerialPort.IsOpen
        End Get
    End Property
    ''' <summary>[J Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <remarks></remarks>
    Private mJetParamRecipe As sReceiveStatus
    Public ReadOnly Property JetParamRecipe As sReceiveStatus Implements ITriggerBoard.JetParamRecipe
        Get
            Return mJetParamRecipe
        End Get
    End Property
    ''' <summary>[G Command之回傳字串(將參數丟給Trigger Board)]</summary>
    ''' <remarks></remarks>
    Private mJetParameter As sReceiveStatus
    Public ReadOnly Property JetParameter As sReceiveStatus Implements ITriggerBoard.JetParameter
        Get
            Return mJetParameter
        End Get
    End Property
    ''' <summary>[L Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <remarks></remarks>
    Private mVisionRecipe As sReceiveStatus
    Public ReadOnly Property VisionRecipe As sReceiveStatus Implements ITriggerBoard.VisionRecipe
        Get
            Return mVisionRecipe
        End Get
    End Property
    ''' <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <remarks></remarks>
    Private mJetRecipe As sReceiveStatus
    Public ReadOnly Property JetRecipe As sReceiveStatus Implements ITriggerBoard.JetRecipe
        Get
            Return mJetRecipe
        End Get
    End Property
    ''' <summary>[f Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)(F Command資料續傳-->f Command)]</summary>
    ''' <remarks></remarks>
    Private mJetRecipeUseTransmissionResuming As sReceiveStatus
    Public ReadOnly Property JetRecipeUseTransmissionResuming As sReceiveStatus Implements ITriggerBoard.JetRecipeUseTransmissionResuming
        Get
            Return mJetRecipeUseTransmissionResuming
        End Get
    End Property
    ''' <summary>[T Command之回傳字串(固定頻率打點)]</summary>
    ''' <remarks></remarks>
    Private mCycleRecipe As sReceiveStatus
    Public ReadOnly Property CycleRecipe As sReceiveStatus Implements ITriggerBoard.CycleRecipe
        Get
            Return mCycleRecipe
        End Get
    End Property
    ''' <summary>[P Command之回傳字串(固定間距打點)]</summary>
    ''' <remarks></remarks>
    Private mPitchRecipe As sReceiveStatus
    Public ReadOnly Property PitchRecipe As sReceiveStatus Implements ITriggerBoard.PitchRecipe
        Get
            Return mPitchRecipe
        End Get
    End Property
    ''' <summary>[X Command之回傳字串(Dispensing Run)]</summary>
    ''' <remarks></remarks>
    Private mDispenseRun As sReceiveStatus
    Public ReadOnly Property DispenseRun As sReceiveStatus Implements ITriggerBoard.DispenseRun
        Get
            Return mDispenseRun
        End Get
    End Property
    ''' <summary>[D Command之回傳字串(Dummy Run)]
    ''' [Note]:Recipe丟完後-->Dummy Run-->Free Type Dispensing</summary>
    ''' <remarks></remarks>
    Private mDummyRun As sReceiveStatus
    Public ReadOnly Property DummyRun As sReceiveStatus Implements ITriggerBoard.DummyRun
        Get
            Return mDummyRun
        End Get
    End Property
    ''' <summary>[S Command之回傳字串(閥體溫度、膠管壓力、閥體電源開關)]</summary>
    ''' <remarks></remarks>
    Private mParameter As sReceiveStatus
    Public ReadOnly Property Parameter As sReceiveStatus Implements ITriggerBoard.Parameter
        Get
            Return mParameter
        End Get
    End Property
    ''' <summary>[C Command之回傳字串(打點數)]
    ''' [Note]:上一次Jetting ON~OFF之間打的Dot數量</summary>
    ''' <remarks></remarks>
    Private mDispenseCounts As sReceiveStatus
    Public ReadOnly Property DispenseCounts As sReceiveStatus Implements ITriggerBoard.DispenseCounts
        Get
            Return mDispenseCounts
        End Get
    End Property
    ''' <summary>[O Command之回傳字串(打點數)]
    ''' [Note]:上一次Jetting ON~OFF之間打的Dot數量</summary>
    ''' <remarks></remarks>
    Private mVisionCounts As sReceiveStatus
    Public ReadOnly Property VisionCounts As sReceiveStatus Implements ITriggerBoard.VisionCounts
        Get
            Return mVisionCounts
        End Get
    End Property
    ''' <summary>[V Command之回傳字串(韌體版本)]</summary>
    ''' <remarks></remarks>
    Private mVersion As sReceiveStatus
    Public ReadOnly Property Version As sReceiveStatus Implements ITriggerBoard.Version
        Get
            Return mVersion
        End Get
    End Property
    ''' <summary>[B Command之回傳字串(點膠真實Cycle)]</summary>
    ''' <remarks></remarks>
    Private mCycleArray As sReceiveStatus
    Public ReadOnly Property CycleArray As sReceiveStatus Implements ITriggerBoard.CycleArray
        Get
            Return mCycleArray
        End Get
    End Property
    ''' <summary>[E Command之回傳字串(異常代號)]</summary>
    ''' <remarks></remarks>
    Private mErrorCode As sReceiveStatus
    Public ReadOnly Property ErrorCode As sReceiveStatus Implements ITriggerBoard.ErrorCode
        Get
            Return mErrorCode
        End Get
    End Property
    ''' <summary>[c Command之回傳字串(異常清除)]</summary>
    ''' <remarks></remarks>
    Private mResetAlarm As sReceiveStatus
    Public ReadOnly Property ResetAlarm As sReceiveStatus Implements ITriggerBoard.ResetAlarm
        Get
            Return mResetAlarm
        End Get
    End Property


    ''' <summary>[R Command之回傳字串(閥體溫度)]</summary>
    ''' <remarks></remarks>
    Private mTemperature As sReceiveStatus
    Public ReadOnly Property Temperature As sReceiveStatus Implements ITriggerBoard.Temperature
        Get
            Return mTemperature
        End Get
    End Property

#End Region

#Region "Properties"
    Public Sub New()
        Try
            mSerialPort = New SerialPort
            mStopWatch = New Stopwatch
            mRecievedData = New StringBuilder

        Catch ex As Exception
            ErrMsg = ex.ToString()
            mSerialPort = Nothing
            mStopWatch = Nothing
            mRecievedData = Nothing
        End Try
    End Sub

    Public Sub Dispose() Implements ITriggerBoard.Dispose
        Try
            mSerialPort.Dispose()

        Catch ex As Exception
            ErrMsg = ex.ToString()
        End Try
    End Sub

    ''' <summary>[初始化]</summary>
    ''' <param name="portName"></param>
    ''' <param name="baudRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(portName As String, baudRate As String) As Boolean Implements ITriggerBoard.Initial

        Dim mIsPortExist As Boolean                                 '[確認Com Port 是否存在]
        Try
            With mSerialPort
                .PortName = "COM" + portName                        '[連線方式]
                .BaudRate = baudRate                                '[每秒傳輸位元]
                .Parity = Parity.None                               '[同位檢查]
                .DataBits = 8                                       '[資料位元]
                .StopBits = StopBits.One                            '[停止位元]
                .Handshake = IO.Ports.Handshake.None                '[流量控制]
                .Encoding = Encoding.ASCII                          '[資料的編碼格式]
                .RtsEnable = True
                .ReceivedBytesThreshold = 1
                .NewLine = "\r\n"
            End With

            mIsBusy = False
            mIsTimeOut = False
            mIsPortExist = False
            For Each GetPortName As String In SerialPort.GetPortNames()
                If mSerialPort.PortName = GetPortName Then
                    mIsPortExist = True
                    Exit For
                End If
            Next

            If mSerialPort.IsOpen = True Then
                mSerialPort.Close()
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2016000), "Alarm_2016000", eMessageLevel.Alarm)
                gSyslog.Save("COM Port:" & mSerialPort.PortName & " BaudRate: " & mSerialPort.BaudRate, , eMessageLevel.Alarm)
                mIsInitialOK = False
                Return False
            End If

            If mIsPortExist = True Then
                mSerialPort.Open()
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015000), "INFO_6015000")
                gSyslog.Save("COM Port:" & mSerialPort.PortName & " BaudRate: " & mSerialPort.BaudRate)
                mIsInitialOK = True
                Return True
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
            gSyslog.Save("COM Port:" & mSerialPort.PortName, , eMessageLevel.Error)
            mIsInitialOK = False
            Return False

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            '[Note:少加這樣項，造成若一開始就沒這個Port，卻還不跳TimeOut
            mIsTimeOut = True
            mIsInitialOK = False
            Return False
        End Try
    End Function

    ''' <summary>[關閉通訊埠連線]</summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements ITriggerBoard.Close
        Try
            If mSerialPort.IsOpen = True Then
                mSerialPort.Close()
            End If
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mSerialPort = Nothing
        End Try
    End Sub

    Private Sub mSerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles mSerialPort.DataReceived

        Dim mSerialTemp As SerialPort
        Dim mI As Integer
        Dim mRecivedError As Boolean
        Dim mDataBuffer(1023) As Byte
        Dim mSTR As String
        Dim mStatus As Integer

        Try
            '[Note]:判斷接收資料是否為字元
            If e.EventType = SerialData.Chars Then
                mSerialTemp = sender
                System.Threading.Thread.CurrentThread.Join(20)

                mSTR = mSerialTemp.ReadExisting()
                mRecievedData.Append(mSTR)
                'Debug.Print("RecievedData: " & mRecievedData.ToString())
                If mRecievedData.ToString().EndsWith(">") = True Then
                    mAnalysisString = mRecievedData.ToString()
                    'Debug.Print("RecievedData: " & RecievedData.ToString())
                    If Not mSendCmd Is Nothing Then
                        Select Case mSendCmd(0)
                            Case 74
                                '[J] Recipe of Jet Valve
                                mJetParamRecipe.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mJetParamRecipe.STR.Replace(">", "").Replace("J", "").Trim()) Then
                                    mStatus = CInt(mJetParamRecipe.STR.Replace(">", "").Replace("J", "").Trim())
                                    If mStatus = 0 Then
                                        mJetParamRecipe.Status = True
                                    Else
                                        mJetParamRecipe.Status = False
                                    End If
                                Else
                                    mJetParamRecipe.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 73
                                '[I] Recipe of Needle-Jet Valve

                            Case 65
                                '[A] Recipe of Auger

                            Case 71
                                '[G] Parameter of Jet
                                mJetParameter.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mJetParameter.STR.Replace(">", "").Replace("G", "").Trim()) Then
                                    mStatus = CInt(mJetParameter.STR.Replace(">", "").Replace("G", "").Trim())
                                    If mStatus = 0 Then
                                        mJetParameter.Status = True
                                    Else
                                        mJetParameter.Status = False
                                    End If
                                Else
                                    mJetParameter.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 70
                                '[F] Recipe of Jet(Constant speed)
                                mJetRecipe.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mJetRecipe.STR.Replace(">", "").Replace("F", "").Trim()) Then
                                    mStatus = CInt(mJetRecipe.STR.Replace(">", "").Replace("F", "").Trim())
                                    If mStatus = 0 Then
                                        mJetRecipe.Status = True
                                    Else
                                        mJetRecipe.Status = False
                                    End If
                                Else
                                    mJetRecipe.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 76
                                '[L] Recipe of Vision trigger
                                mVisionRecipe.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mVisionRecipe.STR.Replace(">", "").Replace("L", "").Trim()) Then
                                    mStatus = CInt(mVisionRecipe.STR.Replace(">", "").Replace("L", "").Trim())
                                    If mStatus = 0 Then
                                        mVisionRecipe.Status = True
                                    Else
                                        mVisionRecipe.Status = False
                                    End If
                                Else
                                    mVisionRecipe.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 102
                                '[Note]:f Command不會有回傳資料
                                ''[f] F Command 的續傳 --> Recipe of Jet(Constant speed)
                                'mJetRecipeUseTransmissionResuming.STR = mAnalysisString
                                ''[Note]:資料拆解
                                'If IsNumeric(mJetRecipeUseTransmissionResuming.STR.Replace(">", "").Replace("f", "").Trim()) Then
                                '    mStatus = CInt(mJetRecipeUseTransmissionResuming.STR.Replace(">", "").Replace("f", "").Trim())
                                '    If mStatus = 0 Then
                                '        mJetRecipeUseTransmissionResuming.Status = True
                                '    Else
                                '        mJetRecipeUseTransmissionResuming.Status = False
                                '    End If
                                'Else
                                '    mJetRecipeUseTransmissionResuming.Status = False
                                'End If
                                'mSerialTemp.DiscardInBuffer()
                                'mIsBusy = False
                                'mStopWatch.Stop()
                                'mRecievedData.Length = 0
                                'mRecievedData.Clear()

                            Case 88
                                '[X] Free type dispensing
                                mDispenseRun.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mDispenseRun.STR.Replace(">", "").Replace("X", "").Trim()) Then
                                    mStatus = CInt(mDispenseRun.STR.Replace(">", "").Replace("X", "").Trim())
                                    If mStatus = 0 Then
                                        mDispenseRun.Status = True
                                    Else
                                        mDispenseRun.Status = False
                                    End If
                                Else
                                    mDispenseRun.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 68
                                '[D] Dummy Run(Auto Tune)
                                mDummyRun.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mDummyRun.STR.Replace(">", "").Replace("D", "").Trim()) Then
                                    mStatus = CInt(mDummyRun.STR.Replace(">", "").Replace("D", "").Trim())
                                    If mStatus = 0 Then
                                        mDummyRun.Status = True
                                    Else
                                        mDummyRun.Status = False
                                    End If
                                Else
                                    mDummyRun.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 84
                                '[T] T type dispensing(固定Cycle觸發)
                                mCycleRecipe.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mCycleRecipe.STR.Replace(">", "").Replace("T", "").Trim()) Then
                                    mStatus = CInt(mCycleRecipe.STR.Replace(">", "").Replace("T", "").Trim())
                                    If mStatus = 0 Then
                                        mCycleRecipe.Status = True
                                    Else
                                        mCycleRecipe.Status = False
                                    End If
                                Else
                                    mCycleRecipe.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 80
                                '[P] P type dispensing(固定Pitch觸發)
                                mPitchRecipe.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mPitchRecipe.STR.Replace(">", "").Replace("P", "").Trim()) Then
                                    mStatus = CInt(mPitchRecipe.STR.Replace(">", "").Replace("P", "").Trim())
                                    If mStatus = 0 Then
                                        mPitchRecipe.Status = True
                                    Else
                                        mPitchRecipe.Status = False
                                    End If
                                Else
                                    mPitchRecipe.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 67
                                '[C] Check Dispense Counts
                                mDispenseCounts.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mDispenseCounts.STR.Replace(">", "").Replace("C", "").Trim()) Then
                                    mStatus = CInt(mDispenseCounts.STR.Replace(">", "").Replace("C", "").Trim())
                                    mDispenseCounts.Value = mStatus
                                    mDispenseCounts.Status = True
                                Else
                                    mDispenseCounts.Value = "0"
                                    mDispenseCounts.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 66
                                '[B] Check Dispensing Cycle Time
                                mCycleArray.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mCycleArray.STR.Replace(">", "").Replace("B", "").Trim()) Then
                                    mStatus = CInt(mCycleArray.STR.Replace(">", "").Replace("B", "").Trim())
                                    mCycleArray.Value = mStatus
                                    mCycleArray.Status = True
                                Else
                                    mCycleArray.Value = ""
                                    mCycleArray.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 86
                                '[V] Check Version
                                mVersion.STR = mAnalysisString
                                '[Note]:資料拆解
                                mVersion.Value = mVersion.STR.Replace(">", "").Replace("V", "").Trim()
                                mVersion.Status = True
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 69
                                '[E] Check Error Code
                                mErrorCode.STR = mAnalysisString
                                '[Note]:資料拆解
                                mErrorCode.Value = mErrorCode.STR.Replace(">", "").Replace("E", "").Trim()
                                mErrorCode.Status = True

                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 99
                                '[c] Alarm Reset
                                mResetAlarm.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mResetAlarm.STR.Replace(">", "").Replace("c", "").Trim()) Then
                                    mStatus = CInt(mResetAlarm.STR.Replace(">", "").Replace("c", "").Trim())
                                    If mStatus = 0 Then
                                        mResetAlarm.Status = True
                                    Else
                                        mResetAlarm.Status = False
                                    End If
                                Else
                                    mResetAlarm.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 83
                                '[S] Set Parameter
                                mParameter.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mParameter.STR.Replace(">", "").Replace("S", "").Trim()) Then
                                    mStatus = CInt(mParameter.STR.Replace(">", "").Replace("S", "").Trim())
                                    If mStatus = 0 Then
                                        mParameter.Status = True
                                    Else
                                        mParameter.Status = False
                                    End If
                                Else
                                    mParameter.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 82
                                '[R] Get Temperature
                                mTemperature.STR = mAnalysisString
                                '[Note]:
                                '20171010
                                If mTemperature.STR.Split(",")(0) = "R 0 " Then
                                    mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 0 ", "").Trim()
                                ElseIf mTemperature.STR.Split(",")(0) = "R 1 " Then
                                    mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 1 ", "").Trim()
                                ElseIf mTemperature.STR.Split(",")(0) = "R 2 " Then
                                    mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 2 ", "").Trim()
                                ElseIf mTemperature.STR.Split(",")(0) = "R 3 " Then
                                    mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 3 ", "").Trim()
                                ElseIf mTemperature.STR.Split(",")(0) = "R 4 " Then
                                    mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 4 ", "").Trim()
                                ElseIf mTemperature.STR.Split(",")(0) = "R 5 " Then
                                    mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 5 ", "").Trim()
                                End If

                                'mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R", "").Trim()

                                mTemperature.Status = True

                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case 79
                                '[O]:Get Vision Count
                                mVisionCounts.STR = mAnalysisString
                                '[Note]:資料拆解
                                If IsNumeric(mVisionCounts.STR.Replace(">", "").Replace("O", "").Trim()) Then
                                    mStatus = CInt(mVisionCounts.STR.Replace(">", "").Replace("O", "").Trim())
                                    mVisionCounts.Value = mStatus
                                    mVisionCounts.Status = True
                                Else
                                    mVisionCounts.Value = "0"
                                    mVisionCounts.Status = False
                                End If
                                mSerialTemp.DiscardInBuffer()
                                mIsBusy = False
                                mStopWatch.Stop()
                                mRecievedData.Length = 0
                                mRecievedData.Clear()

                            Case Else
                                mRecivedError = False
                                For mI = 0 To mSendCmd.Length - 1
                                    If mSendCmd(mI) <> mDataBuffer(mI) Then
                                        mRecivedError = True
                                    End If
                                Next
                                If mRecivedError = False Then
                                    mSerialTemp.DiscardInBuffer()

                                    mIsBusy = False
                                    mStopWatch.Stop()
                                    mRecievedData.Length = 0
                                End If
                                mRecievedData.Clear()

                        End Select
                    End If
                End If
            End If
        Catch ex As Exception
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()

        End Try
    End Sub



#End Region

#Region "Function"

    ''' <summary>[取得目前電腦的序列埠代號]</summary>
    ''' <param name="portIDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortIDs(ByRef portIDs() As String) As Boolean Implements ITriggerBoard.GetPortIDs
        Dim mPortNameList As New List(Of String)
        For Each mGetPortName As String In SerialPort.GetPortNames()
            mPortNameList.Add(mGetPortName.Substring(3))
        Next
        portIDs = mPortNameList.ToArray()
        Return True
    End Function

    ''' <summary>[傳送命令]</summary>
    ''' <param name="commandBtye"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommandToSerialPort(commandBtye() As Byte) As Boolean Implements ITriggerBoard.SendCommandToSerialPort
        Try
            mSendCmd = commandBtye
            If mSerialPort.IsOpen = True Then
                mIsBusy = True
                mIsTimeOut = False
                mSerialPort.Write(mSendCmd, 0, mSendCmd.Length)
                mStopWatch.Restart()
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            Return False
        End Try
    End Function

    ''' <summary>[J Command之命令串接]</summary>
    ''' <param name="is1stStep"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddJetParamRecipe(is1stStep As Boolean, zoneNo As Integer, patternStep As sTriggerJCmdStep, isLastStep As Boolean, Optional parameter As sTriggerJCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddJetParamRecipe
        Try
            Const mScale As Integer = 1000      '[將mm轉成um(單位轉換)]

            Dim mZoneNo(3) As Byte
            Dim mJetTime(3) As Byte
            Dim mTolerance(3) As Byte
            Dim mMeasureLength(3) As Byte
            Dim mMeasurePitch(3) As Byte
            Dim mPath(3) As Byte                '[圖樣型態(Dot、Line、Arc)]
            Dim mStartPosX(3) As Byte           '[起始點]
            Dim mStartPosY(3) As Byte
            Dim mStartPosZ(3) As Byte
            Dim mEndPosX(3) As Byte             '[結束點]
            Dim mEndPosY(3) As Byte
            Dim mEndPosZ(3) As Byte
            Dim mCenterX(3) As Byte             '[中心點]
            Dim mCenterY(3) As Byte
            Dim mCenterZ(3) As Byte
            Dim mDirection(3) As Byte           '[方向 0:CW  1:CCW]
            Dim mDotCounts(3) As Byte           '[點數]
            Dim mGluePressure(3) As Byte        '[膠管壓力]
            Dim mJetPressure(3) As Byte         '[Jet Pressure]  advjet
            Dim mPulseTime(3) As Byte           '[Pulse Time]
            Dim mFallingVelocity(3) As Byte     '[FallingVelocity(%)]
            Dim mStroke(3) As Byte              '[Stroke(%)]
            Dim mSpare(3) As Byte               '[預留]
            Dim mEnd(3) As Byte                 '[結束字元]

            '[Note]:全套
            '[Note]: "J" + ZoneNo + JetTime + Tolerance + MeasureLength + MeasurePitch + Path + StartPosX + StartPosY + StartPosZ + EndPosX + EndPosY + EndPosZ + CenterX + CenterY + CenterZ 
            '            + Direction + DotCounts + GluePressure + JetPressure + PulseTime + FallingVelocity + Stroke + Spare + Spare
            '24*4+1=97

            '[Note]:半套
            '[Note]:     + Path + StartPosX + StartPosY + StartPosZ + EndPosX + EndPosY + EndPosZ + CenterX + CenterY + CenterZ 
            '            + Direction + DotCounts + GluePressure + JetPressure + PulseTime + FallingVelocity + Stroke + Spare + Spare
            '19*4=76


            mSpare = BitConverter.GetBytes(CInt(0))
            mEnd = BitConverter.GetBytes(CInt(-1))
            mZoneNo = BitConverter.GetBytes(zoneNo)
            If is1stStep = True Then
                With parameter
                    mJetTime = BitConverter.GetBytes(CInt(.JetTime))
                    mTolerance = BitConverter.GetBytes(CInt(.Tolerance))
                    mMeasureLength = BitConverter.GetBytes(CInt(.MeasureLength))
                    mMeasurePitch = BitConverter.GetBytes(CInt(.MeasurePitch))
                End With
            End If

            With patternStep
                mPath = BitConverter.GetBytes(CInt(.Path))
                mStartPosX = BitConverter.GetBytes(CInt(.StartPosX * mScale))
                mStartPosY = BitConverter.GetBytes(CInt(.StartPosY * mScale))
                mStartPosZ = BitConverter.GetBytes(CInt(.StartPosZ * mScale))
                mEndPosX = BitConverter.GetBytes(CInt(.EndPosX * mScale))
                mEndPosY = BitConverter.GetBytes(CInt(.EndPosY * mScale))
                mEndPosZ = BitConverter.GetBytes(CInt(.EndPosZ * mScale))
                mCenterX = BitConverter.GetBytes(CInt(.CenPosX * mScale))
                mCenterY = BitConverter.GetBytes(CInt(.CenPosY * mScale))
                mCenterZ = BitConverter.GetBytes(CInt(.CenPosZ * mScale))
                mDirection = BitConverter.GetBytes(CInt(.Dir))
                mDotCounts = BitConverter.GetBytes(CInt(.DotCounts))
                mGluePressure = BitConverter.GetBytes(CInt(.GluePressure))
                mJetPressure = BitConverter.GetBytes(CInt(.JetPressure))
                mPulseTime = BitConverter.GetBytes(CInt(.PulseTime))
                mFallingVelocity = BitConverter.GetBytes(CInt(.FallingVelocity))
                mStroke = BitConverter.GetBytes(CInt(.Stroke))
            End With


            If is1stStep = True Then
                mSendJCmdArray(0) = Char.ConvertToUtf32("J", 0)
                mJCmdArrayIndex = 1
                'ZoneNo
                For mI = 0 To mZoneNo.Length - 1
                    mSendJCmdArray(mJCmdArrayIndex + mI) = mZoneNo(mI)
                Next
                mJCmdArrayIndex = mJCmdArrayIndex + mZoneNo.Length
                'JetTime
                For mI = 0 To mJetTime.Length - 1
                    mSendJCmdArray(mJCmdArrayIndex + mI) = mJetTime(mI)
                Next
                mJCmdArrayIndex = mJCmdArrayIndex + mJetTime.Length
                'Tolerance
                For mI = 0 To mTolerance.Length - 1
                    mSendJCmdArray(mJCmdArrayIndex + mI) = mTolerance(mI)
                Next
                mJCmdArrayIndex = mJCmdArrayIndex + mTolerance.Length
                'MeasureLength
                For mI = 0 To mMeasureLength.Length - 1
                    mSendJCmdArray(mJCmdArrayIndex + mI) = mMeasureLength(mI)
                Next
                mJCmdArrayIndex = mJCmdArrayIndex + mMeasureLength.Length
                'MeasurePitch
                For mI = 0 To mMeasurePitch.Length - 1
                    mSendJCmdArray(mJCmdArrayIndex + mI) = mMeasurePitch(mI)
                Next
                mJCmdArrayIndex = mJCmdArrayIndex + mMeasurePitch.Length
            End If

            'Path
            For mI = 0 To mPath.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mPath(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mPath.Length
            'StartPosX
            For mI = 0 To mStartPosX.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mStartPosX(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mStartPosX.Length
            'StartPosY
            For mI = 0 To mStartPosY.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mStartPosY(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mStartPosY.Length
            'StartPosZ
            For mI = 0 To mStartPosZ.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mStartPosZ(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mStartPosZ.Length
            'EndPosX
            For mI = 0 To mEndPosX.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mEndPosX(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mEndPosX.Length
            'EndPosY
            For mI = 0 To mEndPosY.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mEndPosY(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mEndPosY.Length
            'EndPosZ
            For mI = 0 To mEndPosZ.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mEndPosZ(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mEndPosZ.Length
            'CenterX
            For mI = 0 To mCenterX.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mCenterX(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mCenterX.Length
            'CenterY
            For mI = 0 To mCenterY.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mCenterY(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mCenterY.Length
            'CenterZ
            For mI = 0 To mCenterZ.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mCenterZ(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mCenterZ.Length
            'Direction
            For mI = 0 To mDirection.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mDirection(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mDirection.Length
            'DotCounts
            For mI = 0 To mDotCounts.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mDotCounts(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mDotCounts.Length
            'GluePressure
            For mI = 0 To mGluePressure.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mGluePressure(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mGluePressure.Length
            'JetPressure
            For mI = 0 To mJetPressure.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mJetPressure(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mJetPressure.Length
            'PulseTime
            For mI = 0 To mPulseTime.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mPulseTime(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mPulseTime.Length
            'FallingVelocity
            For mI = 0 To mFallingVelocity.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mFallingVelocity(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mFallingVelocity.Length
            'Stroke
            For mI = 0 To mStroke.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mStroke(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mStroke.Length
            'Spare
            For mI = 0 To mSpare.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mSpare(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mSpare.Length
            'Spare
            For mI = 0 To mSpare.Length - 1
                mSendJCmdArray(mJCmdArrayIndex + mI) = mSpare(mI)
            Next
            mJCmdArrayIndex = mJCmdArrayIndex + mSpare.Length

            If isLastStep = True Then
                'End
                For mI = 0 To mEnd.Length - 1
                    mSendJCmdArray(mJCmdArrayIndex + mI) = mEnd(mI)
                Next
                mJCmdArrayIndex = mJCmdArrayIndex + mEnd.Length
            End If

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[Recipe of Jet (J Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetParamRecipe(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetParamRecipe
        Try
            Dim mSendArray(mJCmdArrayIndex - 1) As Byte
            Dim mI As Integer

            For mI = 0 To mJCmdArrayIndex - 1
                mSendArray(mI) = mSendJCmdArray(mI)
            Next
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mJetParamRecipe.Status = False
                End If

                Return mJetParamRecipe.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[L Command之命令串接]</summary>
    ''' <param name="is1stStep"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddVisionRecipe(is1stStep As Boolean, patternStep As sTriggerVisionCmdStep, isLastStep As Boolean, Optional parameter As sTriggerVisionCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddVisionRecipe
        Try
            Const mScale As Integer = 1000      '[將mm轉成um(單位轉換)]
            Dim mTotalPoints(3) As Byte         '[總取像數]
            Dim mApproachPosX(3) As Byte        '[助跑位置X]
            Dim mApproachPosY(3) As Byte        '[助跑位置Y]
            Dim mPath(3) As Byte                '[圖樣型態(Dot、Line、Arc)]
            Dim mDirection(3) As Byte           '[方向 0:CW  1:CCW]
            Dim mVelocity(3) As Byte            '[Stage移動速度]
            Dim mPointCounts(3) As Byte         '[取像數]
            Dim mStartPosX(3) As Byte           '[起點]
            Dim mStartPosY(3) As Byte
            Dim mEndPosX(3) As Byte             '[終點]
            Dim mEndPosY(3) As Byte
            Dim mCenterX(3) As Byte             '[圓心點]
            Dim mCenterY(3) As Byte
            Dim mEnd(3) As Byte                 '[結束字元]
            Dim mDelayTime(3) As Byte           '[訊號延遲時間]
            Dim mWatiTime(3) As Byte            '[隔多久才會接到下一條線段]
            Dim mSpare(3) As Byte               '[空白]

            '[Note]:全套
            '[Note]: "L" + TotalPoints + ApproachPosX + ApproachPosY + DelayTime + (Velocity+Direction+Path) + PointCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
            '13*4+1=53

            '[Note]:半套
            '[Note]:                                                             + (Velocity+Direction+Path) + PointCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
            '9*4=36

            mEnd = BitConverter.GetBytes(CInt(-1))
            If is1stStep = True Then
                With parameter
                    mTotalPoints = BitConverter.GetBytes(CInt(.TotalPointCounts))
                    mApproachPosX = BitConverter.GetBytes(CInt(.ApproachPosX * mScale))
                    mApproachPosY = BitConverter.GetBytes(CInt(.ApproachPosY * mScale))
                    mDelayTime = BitConverter.GetBytes(CInt(.DelayTime * mScale))
                End With
            End If
            With patternStep
                mVelocity = BitConverter.GetBytes(CInt(.Velocity))
                mDirection = BitConverter.GetBytes(CInt(.Dir))
                mPath = BitConverter.GetBytes(CInt(.Path))
                mPointCounts = BitConverter.GetBytes(CInt(.PointCounts))
                mStartPosX = BitConverter.GetBytes(CInt(.StartPosX * mScale))
                mStartPosY = BitConverter.GetBytes(CInt(.StartPosY * mScale))
                mEndPosX = BitConverter.GetBytes(CInt(.EndPosX * mScale))
                mEndPosY = BitConverter.GetBytes(CInt(.EndPosY * mScale))
                mCenterX = BitConverter.GetBytes(CInt(.CenPosX * mScale))
                mCenterY = BitConverter.GetBytes(CInt(.CenPosY * mScale))
                mWatiTime = BitConverter.GetBytes(CInt(.PathWaitTime * mScale))
            End With

            If is1stStep = True Then
                mSendVisionCmdArray(0) = Char.ConvertToUtf32("L", 0)
                mVisionCmdArrayIndex = 1
                'TotalPoints
                For mI = 0 To mTotalPoints.Length - 1
                    mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mTotalPoints(mI)
                Next
                mVisionCmdArrayIndex = mVisionCmdArrayIndex + mTotalPoints.Length
                'ApproachPosX
                For mI = 0 To mApproachPosX.Length - 1
                    mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mApproachPosX(mI)
                Next
                mVisionCmdArrayIndex = mVisionCmdArrayIndex + mApproachPosX.Length
                'ApproachPosY
                For mI = 0 To mApproachPosY.Length - 1
                    mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mApproachPosY(mI)
                Next
                'DelayTime
                mVisionCmdArrayIndex = mVisionCmdArrayIndex + mApproachPosY.Length
                For mI = 0 To mDelayTime.Length - 1
                    mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mDelayTime(mI)
                Next
                mVisionCmdArrayIndex = mVisionCmdArrayIndex + mDelayTime.Length
            End If

            '(Velocity+Direction+Path)
            mSendVisionCmdArray(mVisionCmdArrayIndex) = mVelocity(0)
            mSendVisionCmdArray(mVisionCmdArrayIndex + 1) = mVelocity(1)
            mSendVisionCmdArray(mVisionCmdArrayIndex + 2) = mDirection(0)
            mSendVisionCmdArray(mVisionCmdArrayIndex + 3) = mPath(0)
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + 4
            'PointCounts
            For mI = 0 To mPointCounts.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mPointCounts(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mPointCounts.Length
            'StartPosX
            For mI = 0 To mStartPosX.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mStartPosX(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mStartPosX.Length
            'StartPosY
            For mI = 0 To mStartPosY.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mStartPosY(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mStartPosY.Length
            'EndPosX
            For mI = 0 To mEndPosX.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mEndPosX(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mEndPosX.Length
            'EndPosY
            For mI = 0 To mEndPosY.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mEndPosY(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mEndPosY.Length
            'CenterX
            For mI = 0 To mCenterX.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mCenterX(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mCenterX.Length
            'CenterY
            For mI = 0 To mCenterY.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mCenterY(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mCenterY.Length
            'WatiTime
            For mI = 0 To mWatiTime.Length - 1
                mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mWatiTime(mI)
            Next
            mVisionCmdArrayIndex = mVisionCmdArrayIndex + mWatiTime.Length

            If isLastStep = True Then
                'End
                For mI = 0 To mEnd.Length - 1
                    mSendVisionCmdArray(mVisionCmdArrayIndex + mI) = mEnd(mI)
                Next
                mVisionCmdArrayIndex = mVisionCmdArrayIndex + mEnd.Length
            End If

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[Recipe of Vision trigger (L Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetVisionRecipe(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetVisionRecipe
        Try
            Dim mSendArray(mVisionCmdArrayIndex - 1) As Byte
            Dim mI As Integer

            For mI = 0 To mVisionCmdArrayIndex - 1
                mSendArray(mI) = mSendVisionCmdArray(mI)
            Next
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mVisionRecipe.Status = False
                End If

                Return mVisionRecipe.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[F Command之命令串接]</summary>
    ''' <param name="is1stStep"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddJetRecipe(is1stStep As Boolean, patternStep As sTriggerFCmdStep, isLastStep As Boolean, Optional parameter As sTriggerFCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddJetRecipe
        Try
            Const mScale As Integer = 1000      '[將mm轉成um(單位轉換)]
            Dim mTotalDots(3) As Byte           '[總打點數]
            Dim mApproachPosX(3) As Byte        '[助跑位置X]
            Dim mApproachPosY(3) As Byte        '[助跑位置Y]
            Dim mPath(3) As Byte                '[圖樣型態(Dot、Line、Arc)]
            Dim mDirection(3) As Byte           '[方向 0:CW  1:CCW]
            Dim mVelocity(3) As Byte             '[Stage移動速度]
            Dim mDotCounts(3) As Byte           '[點數]
            Dim mStartPosX(3) As Byte           '[起點]
            Dim mStartPosY(3) As Byte
            Dim mEndPosX(3) As Byte             '[終點]
            Dim mEndPosY(3) As Byte
            Dim mCenterX(3) As Byte             '[圓心點]
            Dim mCenterY(3) As Byte
            Dim mEnd(3) As Byte                 '[結束字元]
            Dim mWatiTime(3) As Byte            '[隔多久才會接到下一條線段]
            Dim mSpare(3) As Byte               '[空白]

            '[Note]:全套
            '[Note]: "F" + TotalDots + ApproachPosX + ApproachPosY + Spare + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
            '13*4+1=53

            '[Note]:半套
            '[Note]:                                                       + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
            '9*4=36

            mEnd = BitConverter.GetBytes(CInt(-1))
            If is1stStep = True Then
                With parameter
                    mTotalDots = BitConverter.GetBytes(CInt(.TotalDotCounts))
                    mApproachPosX = BitConverter.GetBytes(CInt(.ApproachPosX * mScale))
                    mApproachPosY = BitConverter.GetBytes(CInt(.ApproachPosY * mScale))
                    mSpare = BitConverter.GetBytes(CInt(0))
                End With
            End If
            With patternStep
                mVelocity = BitConverter.GetBytes(CInt(.Velocity))
                mDirection = BitConverter.GetBytes(CInt(.Dir))
                mPath = BitConverter.GetBytes(CInt(.Path))
                mDotCounts = BitConverter.GetBytes(CInt(.DotCounts))
                mStartPosX = BitConverter.GetBytes(CInt(.StartPosX * mScale))
                mStartPosY = BitConverter.GetBytes(CInt(.StartPosY * mScale))
                mEndPosX = BitConverter.GetBytes(CInt(.EndPosX * mScale))
                mEndPosY = BitConverter.GetBytes(CInt(.EndPosY * mScale))
                mCenterX = BitConverter.GetBytes(CInt(.CenPosX * mScale))
                mCenterY = BitConverter.GetBytes(CInt(.CenPosY * mScale))
                mWatiTime = BitConverter.GetBytes(CInt(.PathWaitTime * mScale))
            End With

            If is1stStep = True Then
                mSendFCmdArray(0) = Char.ConvertToUtf32("F", 0)
                mFCmdArrayIndex = 1
                'TotalDots
                For mI = 0 To mTotalDots.Length - 1
                    mSendFCmdArray(mFCmdArrayIndex + mI) = mTotalDots(mI)
                Next
                mFCmdArrayIndex = mFCmdArrayIndex + mTotalDots.Length
                'ApproachPosX
                For mI = 0 To mApproachPosX.Length - 1
                    mSendFCmdArray(mFCmdArrayIndex + mI) = mApproachPosX(mI)
                Next
                mFCmdArrayIndex = mFCmdArrayIndex + mApproachPosX.Length
                'ApproachPosY
                For mI = 0 To mApproachPosY.Length - 1
                    mSendFCmdArray(mFCmdArrayIndex + mI) = mApproachPosY(mI)
                Next
                mFCmdArrayIndex = mFCmdArrayIndex + mApproachPosY.Length
                'Spare
                For mI = 0 To mSpare.Length - 1
                    mSendFCmdArray(mFCmdArrayIndex + mI) = mSpare(mI)
                Next
                mFCmdArrayIndex = mFCmdArrayIndex + mSpare.Length
            End If

            '(Velocity+Direction+Path)
            mSendFCmdArray(mFCmdArrayIndex) = mVelocity(0)
            mSendFCmdArray(mFCmdArrayIndex + 1) = mVelocity(1)
            mSendFCmdArray(mFCmdArrayIndex + 2) = mDirection(0)
            mSendFCmdArray(mFCmdArrayIndex + 3) = mPath(0)
            mFCmdArrayIndex = mFCmdArrayIndex + 4
            'DotCounts
            For mI = 0 To mDotCounts.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mDotCounts(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mDotCounts.Length
            'StartPosX
            For mI = 0 To mStartPosX.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mStartPosX(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mStartPosX.Length
            'StartPosY
            For mI = 0 To mStartPosY.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mStartPosY(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mStartPosY.Length
            'EndPosX
            For mI = 0 To mEndPosX.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mEndPosX(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mEndPosX.Length
            'EndPosY
            For mI = 0 To mEndPosY.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mEndPosY(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mEndPosY.Length
            'CenterX
            For mI = 0 To mCenterX.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mCenterX(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mCenterX.Length
            'CenterY
            For mI = 0 To mCenterY.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mCenterY(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mCenterY.Length
            'WaitTime
            For mI = 0 To mWatiTime.Length - 1
                mSendFCmdArray(mFCmdArrayIndex + mI) = mWatiTime(mI)
            Next
            mFCmdArrayIndex = mFCmdArrayIndex + mWatiTime.Length

            If isLastStep = True Then
                'End
                For mI = 0 To mEnd.Length - 1
                    mSendFCmdArray(mFCmdArrayIndex + mI) = mEnd(mI)
                Next
                mFCmdArrayIndex = mFCmdArrayIndex + mEnd.Length
            End If

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[Recipe of Jet Constant Speed (F Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetRecipe(Optional ByVal waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetRecipe
        Try
            Dim mSendArray(mFCmdArrayIndex - 1) As Byte
            Dim mI As Integer

            For mI = 0 To mFCmdArrayIndex - 1
                mSendArray(mI) = mSendFCmdArray(mI)
            Next
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mJetRecipe.Status = False
                End If

                Return mJetRecipe.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[f Command的資料串接(F Command資料續傳-->f Command)]</summary>
    ''' <param name="is1stStep"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddJetRecipeUseTransmissionResuming(is1stStep As Boolean, patternStep As sTriggerFCmdStep, isLastStep As Boolean, Optional parameter As sTriggerFCmdParam = Nothing) As Boolean Implements ITriggerBoard.AddJetRecipeUseTransmissionResuming
        Try
            Const mScale As Integer = 1000      '[將mm轉成um(單位轉換)]

            Dim mTotalDots(3) As Byte           '[總打點數]
            Dim mApproachPosX(3) As Byte        '[助跑位置X]
            Dim mApproachPosY(3) As Byte        '[助跑位置Y]
            Dim mPath(3) As Byte                '[圖樣型態(Dot、Line、Arc)]
            Dim mDirection(3) As Byte           '[方向 0:CW  1:CCW]
            Dim mVelocity(3) As Byte             '[Stage移動速度]
            Dim mDotCounts(3) As Byte           '[點數]
            Dim mStartPosX(3) As Byte           '[起點]
            Dim mStartPosY(3) As Byte
            Dim mEndPosX(3) As Byte             '[終點]
            Dim mEndPosY(3) As Byte
            Dim mCenterX(3) As Byte             '[圓心點]
            Dim mCenterY(3) As Byte
            Dim mEnd(3) As Byte                 '[結束字元]
            Dim mWatiTime(3) As Byte            '[隔多久才會接到下一條線段]
            Dim mSpare(3) As Byte               '[空白]

            '[Note]:全套
            '        "f"  +mTotalDots + ApproachPosX + ApproachPosY 
            '[Note]: "f"  +     0     +       0      +       0      +   Spare   + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line     
            '13*4+1=53

            '[Note]:半套
            '[Note]:                                                            + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
            '9*4=36

            mEnd = BitConverter.GetBytes(CInt(-1))
            If is1stStep = True Then
                With parameter
                    mTotalDots = BitConverter.GetBytes(CInt(0))
                    mApproachPosX = BitConverter.GetBytes(CInt(0))
                    mApproachPosY = BitConverter.GetBytes(CInt(0))
                    mSpare = BitConverter.GetBytes(CInt(0))
                End With
            End If
            With patternStep
                mVelocity = BitConverter.GetBytes(CInt(.Velocity))
                mDirection = BitConverter.GetBytes(CInt(.Dir))
                mPath = BitConverter.GetBytes(CInt(.Path))
                mDotCounts = BitConverter.GetBytes(CInt(.DotCounts))
                mStartPosX = BitConverter.GetBytes(CInt(.StartPosX * mScale))
                mStartPosY = BitConverter.GetBytes(CInt(.StartPosY * mScale))
                mEndPosX = BitConverter.GetBytes(CInt(.EndPosX * mScale))
                mEndPosY = BitConverter.GetBytes(CInt(.EndPosY * mScale))
                mCenterX = BitConverter.GetBytes(CInt(.CenPosX * mScale))
                mCenterY = BitConverter.GetBytes(CInt(.CenPosY * mScale))
                mWatiTime = BitConverter.GetBytes(CInt(.PathWaitTime * mScale))
            End With

            If is1stStep = True Then
                mSendFCmdTRArray(0) = Char.ConvertToUtf32("f", 0)
                mFCmdTRArrayIndex = 1
                'TotalDots
                For mI = 0 To mTotalDots.Length - 1
                    mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mTotalDots(mI)
                Next
                mFCmdTRArrayIndex = mFCmdTRArrayIndex + mTotalDots.Length
                'ApproachPosX
                For mI = 0 To mApproachPosX.Length - 1
                    mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mApproachPosX(mI)
                Next
                mFCmdTRArrayIndex = mFCmdTRArrayIndex + mApproachPosX.Length
                'ApproachPosY
                For mI = 0 To mApproachPosY.Length - 1
                    mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mApproachPosY(mI)
                Next
                mFCmdTRArrayIndex = mFCmdTRArrayIndex + mApproachPosY.Length
                'Spare
                For mI = 0 To mSpare.Length - 1
                    mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mSpare(mI)
                Next
                mFCmdTRArrayIndex = mFCmdTRArrayIndex + mSpare.Length
            End If

            '(Velocity+Direction+Path)
            mSendFCmdTRArray(mFCmdTRArrayIndex) = mVelocity(0)
            mSendFCmdTRArray(mFCmdTRArrayIndex + 1) = mVelocity(1)
            mSendFCmdTRArray(mFCmdTRArrayIndex + 2) = mDirection(0)
            mSendFCmdTRArray(mFCmdTRArrayIndex + 3) = mPath(0)
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + 4
            'DotCounts
            For mI = 0 To mDotCounts.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mDotCounts(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mDotCounts.Length
            'StartPosX
            For mI = 0 To mStartPosX.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mStartPosX(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mStartPosX.Length
            'StartPosY
            For mI = 0 To mStartPosY.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mStartPosY(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mStartPosY.Length
            'EndPosX
            For mI = 0 To mEndPosX.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mEndPosX(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mEndPosX.Length
            'EndPosY
            For mI = 0 To mEndPosY.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mEndPosY(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mEndPosY.Length
            'CenterX
            For mI = 0 To mCenterX.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mCenterX(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mCenterX.Length
            'CenterY
            For mI = 0 To mCenterY.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mCenterY(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mCenterY.Length
            'WaitTime
            For mI = 0 To mWatiTime.Length - 1
                mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mWatiTime(mI)
            Next
            mFCmdTRArrayIndex = mFCmdTRArrayIndex + mWatiTime.Length

            If isLastStep = True Then
                'End
                For mI = 0 To mEnd.Length - 1
                    mSendFCmdTRArray(mFCmdTRArrayIndex + mI) = mEnd(mI)
                Next
                mFCmdTRArrayIndex = mFCmdTRArrayIndex + mEnd.Length
            End If

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try

    End Function

    ''' <summary>[將點膠資料丟給Trigger Board(f Command-->F Command的續傳)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetRecipeByTransmissionResuming(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetRecipeByTransmissionResuming
        Try
            'Dim mSendArray(mFCmdTRArrayIndex - 1) As Byte
            Dim mSendArray(mFCmdTRArraySize) As Byte

            Dim mI As Integer
            Dim mJ As Integer

            For mJ = 0 To mFCmdTRArraySize
                If mJ <= mFCmdTRArrayIndex - 1 Then
                    For mI = 0 To mFCmdTRArrayIndex - 1
                        mSendArray(mI) = mSendFCmdTRArray(mI)
                    Next
                Else
                    mSendArray(mJ) = 0
                End If
            Next
            SendCommandToSerialPort(mSendArray)

            '[Note]:f Command不會有回傳資料
            mIsBusy = False
            mIsTimeOut = False
            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mJetRecipe.Status = False
                End If

                Return mJetRecipe.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[Parameter of Jet(只丟參數)(G Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetParameter(parameter As sTriggerGCmdParam, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetJetParameter
        Try
            Dim mI As Integer
            Dim mHeadNo(3) As Byte
            Dim mPulseTime(3) As Byte
            Dim mJetTime(3) As Byte
            Dim mStroke(3) As Byte
            Dim mGluePressure(3) As Byte
            Dim mTolerance(3) As Byte
            Dim mMeasureLength(3) As Byte
            Dim mMeasurePitch(3) As Byte
            Dim mMeasureCounts(3) As Byte
            Dim mMeasureCountsAfter(3) As Byte
            Dim mSpare(3) As Byte
            Dim mCloseVoltage(3) As Byte
            Dim mJetPressure(3) As Byte
            Dim mSendArray(52) As Byte
            Dim mArrayCounts As Integer
            Dim mOpenTime(3) As Byte
            Dim mCloseTime(3) As Byte
            Dim mCycleTime(3) As Byte

            '[Note]: "G" + HeadNo + PulseTime + JetTime + Stroke + (CloseTime/OpenTime) + GluePressure + Tolerance + MeasureLength + MeasurePitch + MeasureCounts + CloseVoltage + JetPressure + CycleTime
            '13*4+1=53

            mHeadNo = BitConverter.GetBytes(parameter.HeadNo)
            mPulseTime = BitConverter.GetBytes(parameter.PulseTime)
            mJetTime = BitConverter.GetBytes(parameter.JetTime)
            mStroke = BitConverter.GetBytes(parameter.Stroke)
            mOpenTime = BitConverter.GetBytes(parameter.OpenTime)
            mCloseTime = BitConverter.GetBytes(parameter.CloseTime)
            mGluePressure = BitConverter.GetBytes(parameter.GluePressure)
            mTolerance = BitConverter.GetBytes(parameter.Tolerance)
            mMeasureLength = BitConverter.GetBytes(parameter.MeasureLength)
            mMeasurePitch = BitConverter.GetBytes(parameter.MeasurePitch)
            mMeasureCounts = BitConverter.GetBytes(parameter.MeasureCounts)
            mCloseVoltage = BitConverter.GetBytes(parameter.CloseVoltage)
            mJetPressure = BitConverter.GetBytes(parameter.JetPressure)
            mCycleTime = BitConverter.GetBytes(parameter.CycleTime)

            mSendArray(0) = Char.ConvertToUtf32("G", 0)
            mArrayCounts = 1
            'HeadNo
            For mI = 0 To mHeadNo.Length - 1
                mSendArray(mArrayCounts + mI) = mHeadNo(mI)
            Next
            mArrayCounts = mArrayCounts + mHeadNo.Length
            'PulseTime
            For mI = 0 To mPulseTime.Length - 1
                mSendArray(mArrayCounts + mI) = mPulseTime(mI)
            Next
            mArrayCounts = mArrayCounts + mPulseTime.Length
            'JetTime
            For mI = 0 To mJetTime.Length - 1
                mSendArray(mArrayCounts + mI) = mJetTime(mI)
            Next
            mArrayCounts = mArrayCounts + mJetTime.Length
            'Stroke
            For mI = 0 To mStroke.Length - 1
                mSendArray(mArrayCounts + mI) = mStroke(mI)
            Next
            mArrayCounts = mArrayCounts + mStroke.Length
            'OpenTime/CloseTime
            mSendArray(mArrayCounts) = mCloseTime(0)
            mSendArray(mArrayCounts + 1) = mCloseTime(1)
            mSendArray(mArrayCounts + 2) = mOpenTime(0)
            mSendArray(mArrayCounts + 3) = mOpenTime(1)
            mArrayCounts = mArrayCounts + 4
            'GluePressure
            For mI = 0 To mGluePressure.Length - 1
                mSendArray(mArrayCounts + mI) = mGluePressure(mI)
            Next
            mArrayCounts = mArrayCounts + mGluePressure.Length
            'Tolerance
            For mI = 0 To mTolerance.Length - 1
                mSendArray(mArrayCounts + mI) = mTolerance(mI)
            Next
            mArrayCounts = mArrayCounts + mTolerance.Length
            'MeasureLength
            For mI = 0 To mMeasureLength.Length - 1
                mSendArray(mArrayCounts + mI) = mMeasureLength(mI)
            Next
            mArrayCounts = mArrayCounts + mMeasureLength.Length
            'MeasurePitch
            For mI = 0 To mMeasurePitch.Length - 1
                mSendArray(mArrayCounts + mI) = mMeasurePitch(mI)
            Next
            mArrayCounts = mArrayCounts + mMeasurePitch.Length
            'MeasureCounts
            For mI = 0 To mMeasureCounts.Length - 1
                mSendArray(mArrayCounts + mI) = mMeasureCounts(mI)
            Next
            mArrayCounts = mArrayCounts + mMeasureCounts.Length
            'CloseVoltage
            For mI = 0 To mCloseVoltage.Length - 1
                mSendArray(mArrayCounts + mI) = mCloseVoltage(mI)
            Next
            mArrayCounts = mArrayCounts + mCloseVoltage.Length
            'JetPressure
            For mI = 0 To mJetPressure.Length - 1
                mSendArray(mArrayCounts + mI) = mJetPressure(mI)
            Next
            mArrayCounts = mArrayCounts + mJetPressure.Length
            'CycleTime
            For mI = 0 To mCycleTime.Length - 1
                mSendArray(mArrayCounts + mI) = mCycleTime(mI)
            Next
            mArrayCounts = mArrayCounts + mCycleTime.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mJetParameter.Status = False
                End If
                Return mJetParameter.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[固定頻率打點 T Command(Cycle Time)]</summary>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCycleRecipe(parameter As sTriggerTPCmdParam, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetCycleRecipe
        Try
            Dim mI As Integer
            Dim mZoneNo(3) As Byte
            Dim mCycleTime(3) As Byte
            Dim mDotCounts(3) As Byte
            Dim mGluePressure(3) As Byte
            Dim mJetPressure(3) As Byte
            Dim mPulseTime(3) As Byte
            Dim mOpenTime(3) As Byte
            Dim mCloseTime(3) As Byte
            Dim mCloseVoltage(3) As Byte
            Dim mStroke(3) As Byte
            Dim mSendArray(32) As Byte
            Dim mArrayCounts As Integer


            '[Note]: "T" + ZoneNo + CycleTime + Dots + GluePressure + JetPressure + PulseTime + (CloseTime/OpenTime) + (CloseVoltage/Stroke)
            '8*4+1=33

            mZoneNo = BitConverter.GetBytes(CInt(0))
            mCycleTime = BitConverter.GetBytes(parameter.CycleTime)
            mDotCounts = BitConverter.GetBytes(parameter.DotCounts)
            mGluePressure = BitConverter.GetBytes(parameter.GluePressure)
            mJetPressure = BitConverter.GetBytes(parameter.JetPressure)
            mPulseTime = BitConverter.GetBytes(parameter.PulseTime)
            mOpenTime = BitConverter.GetBytes(parameter.OpenTime)
            mCloseTime = BitConverter.GetBytes(parameter.CloseTime)
            mCloseVoltage = BitConverter.GetBytes(parameter.CloseVoltage)
            mStroke = BitConverter.GetBytes(parameter.Stroke)

            mSendArray(0) = Char.ConvertToUtf32("T", 0)
            mArrayCounts = 1
            'ZoneNumber
            For mI = 0 To mZoneNo.Length - 1
                mSendArray(mArrayCounts + mI) = mZoneNo(mI)
            Next
            mArrayCounts = mArrayCounts + mZoneNo.Length
            'CycleTime
            For mI = 0 To mCycleTime.Length - 1
                mSendArray(mArrayCounts + mI) = mCycleTime(mI)
            Next
            mArrayCounts = mArrayCounts + mCycleTime.Length
            'Dots
            For mI = 0 To mDotCounts.Length - 1
                mSendArray(mArrayCounts + mI) = mDotCounts(mI)
            Next
            mArrayCounts = mArrayCounts + mDotCounts.Length
            'GluePressure
            For mI = 0 To mGluePressure.Length - 1
                mSendArray(mArrayCounts + mI) = mGluePressure(mI)
            Next
            mArrayCounts = mArrayCounts + mGluePressure.Length
            'JetPressure
            For mI = 0 To mJetPressure.Length - 1
                mSendArray(mArrayCounts + mI) = mJetPressure(mI)
            Next
            mArrayCounts = mArrayCounts + mJetPressure.Length
            'PulseTime
            For mI = 0 To mPulseTime.Length - 1
                mSendArray(mArrayCounts + mI) = mPulseTime(mI)
            Next
            mArrayCounts = mArrayCounts + mPulseTime.Length

            'OpenTime/CloseTime
            mSendArray(mArrayCounts) = mCloseTime(0)
            mSendArray(mArrayCounts + 1) = mCloseTime(1)
            mSendArray(mArrayCounts + 2) = mOpenTime(0)
            mSendArray(mArrayCounts + 3) = mOpenTime(1)
            mArrayCounts = mArrayCounts + 4

            'Stroke/CloseVoltage
            mSendArray(mArrayCounts) = mCloseVoltage(0)
            mSendArray(mArrayCounts + 1) = mCloseVoltage(1)
            mSendArray(mArrayCounts + 2) = mStroke(0)
            mSendArray(mArrayCounts + 3) = mStroke(1)
            mArrayCounts = mArrayCounts + 4

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mCycleRecipe.Status = False
                End If
                Return mCycleRecipe.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[固定間距打點 P Command(Pitch)]</summary>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPitchRecipe(parameter As sTriggerTPCmdParam, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetPitchRecipe
        Try
            Dim mI As Integer
            Dim mZoneNo(3) As Byte
            Dim mPitch(3) As Byte
            Dim mDotCounts(3) As Byte
            Dim mGluePressure(3) As Byte
            Dim mJetPressure(3) As Byte
            Dim mPulseTime(3) As Byte
            Dim mOpenTime(3) As Byte
            Dim mCloseTime(3) As Byte
            Dim mCloseVoltage(3) As Byte
            Dim mStroke(3) As Byte
            Dim mSendArray(32) As Byte
            Dim mArrayCounts As Integer

            '[Note]: "P" + ZoneNo + Pitch + Dots + GluePressure + JetPressure + PulseTime + (CloseTime/OpenTime) + (CloseVoltage/Stroke)
            '8*4+1=33

            mZoneNo = BitConverter.GetBytes(CInt(0))
            mPitch = BitConverter.GetBytes(parameter.Pitch)
            mDotCounts = BitConverter.GetBytes(parameter.DotCounts)
            mGluePressure = BitConverter.GetBytes(parameter.GluePressure)
            mJetPressure = BitConverter.GetBytes(parameter.JetPressure)
            mPulseTime = BitConverter.GetBytes(parameter.PulseTime)
            mOpenTime = BitConverter.GetBytes(parameter.OpenTime)
            mCloseTime = BitConverter.GetBytes(parameter.CloseTime)
            mCloseVoltage = BitConverter.GetBytes(parameter.CloseVoltage)
            mStroke = BitConverter.GetBytes(parameter.Stroke)

            mSendArray(0) = Char.ConvertToUtf32("P", 0)
            mArrayCounts = 1
            'ZoneNumber
            For mI = 0 To mZoneNo.Length - 1
                mSendArray(mArrayCounts + mI) = mZoneNo(mI)
            Next
            mArrayCounts = mArrayCounts + mZoneNo.Length
            'Pitch
            For mI = 0 To mPitch.Length - 1
                mSendArray(mArrayCounts + mI) = mPitch(mI)
            Next
            mArrayCounts = mArrayCounts + mPitch.Length
            'Dots
            For mI = 0 To mDotCounts.Length - 1
                mSendArray(mArrayCounts + mI) = mDotCounts(mI)
            Next
            mArrayCounts = mArrayCounts + mDotCounts.Length
            'GluePressure
            For mI = 0 To mGluePressure.Length - 1
                mSendArray(mArrayCounts + mI) = mGluePressure(mI)
            Next
            mArrayCounts = mArrayCounts + mGluePressure.Length
            'JetPressure
            For mI = 0 To mJetPressure.Length - 1
                mSendArray(mArrayCounts + mI) = mJetPressure(mI)
            Next
            mArrayCounts = mArrayCounts + mJetPressure.Length
            'PulseTime
            For mI = 0 To mPulseTime.Length - 1
                mSendArray(mArrayCounts + mI) = mPulseTime(mI)
            Next
            mArrayCounts = mArrayCounts + mPulseTime.Length

            'OpenTime/CloseTime
            mSendArray(mArrayCounts) = mCloseTime(0)
            mSendArray(mArrayCounts + 1) = mCloseTime(1)
            mSendArray(mArrayCounts + 2) = mOpenTime(0)
            mSendArray(mArrayCounts + 3) = mOpenTime(1)
            mArrayCounts = mArrayCounts + 4

            'Stroke/CloseVoltage
            mSendArray(mArrayCounts) = mCloseVoltage(0)
            mSendArray(mArrayCounts + 1) = mCloseVoltage(1)
            mSendArray(mArrayCounts + 2) = mStroke(0)
            mSendArray(mArrayCounts + 3) = mStroke(1)
            mArrayCounts = mArrayCounts + 4

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mPitchRecipe.Status = False
                End If
                Return mPitchRecipe.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[Dummy Run (Auto Tune)]</summary>
    ''' <param name="dispType"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDummyRun(dispType As enmTriggerDispType, valveNo As eValveWorkMode, zoneNo As Integer, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetDummyRun
        Try
            Dim mI As Integer
            Dim mZoneNo(3) As Byte
            Dim mHeadNo(3) As Byte
            Dim mSpare(3) As Byte
            Dim mSendArray(21) As Byte
            Dim mArrayCounts As Integer

            '[Note]: "D" + (J、F、L) + ZoneNo + HeadNo + Spare + Spare + Spare 
            '5*4+2=22
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mZoneNo = BitConverter.GetBytes(zoneNo)
            mHeadNo = BitConverter.GetBytes(CInt(valveNo))
            mSpare = BitConverter.GetBytes(CInt(0))
            mSendArray(0) = Char.ConvertToUtf32("D", 0)
            mArrayCounts = 1
            Select Case dispType
                Case enmTriggerDispType.JetParamRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("J", 0)

                Case enmTriggerDispType.JetRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("F", 0)

                Case enmTriggerDispType.VisionRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("L", 0)

                Case Else
                    '[命令錯誤]
                    Return False
            End Select
            mArrayCounts = 2

            'ZoneNumber
            For mI = 0 To mZoneNo.Length - 1
                mSendArray(mArrayCounts + mI) = mZoneNo(mI)
            Next
            mArrayCounts = mArrayCounts + mZoneNo.Length
            'HeadNo
            For mI = 0 To mHeadNo.Length - 1
                mSendArray(mArrayCounts + mI) = mHeadNo(mI)
            Next
            mArrayCounts = mArrayCounts + mHeadNo.Length
            'Spare
            For mI = 0 To mSpare.Length - 1
                mSendArray(mArrayCounts + mI) = mSpare(mI)
            Next
            mArrayCounts = mArrayCounts + mSpare.Length
            'Spare
            For mI = 0 To mSpare.Length - 1
                mSendArray(mArrayCounts + mI) = mSpare(mI)
            Next
            mArrayCounts = mArrayCounts + mSpare.Length
            'Spare
            For mI = 0 To mSpare.Length - 1
                mSendArray(mArrayCounts + mI) = mSpare(mI)
            Next
            mArrayCounts = mArrayCounts + mSpare.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mDummyRun.Status = False
                End If
                Return mDummyRun.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[Free Type Dispensing (X Command)]</summary>
    ''' <param name="dispType"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="degree"></param>
    ''' <param name="reworkDotCounts"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDispenseRun(dispType As enmTriggerDispType, valveNo As eValveWorkMode, zoneNo As Integer, degree As Decimal, ByVal reworkDotCounts As Integer, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetDispenseRun
        Try
            Dim mI As Integer
            Dim mZoneNo(3) As Byte
            Dim mHeadNo(3) As Byte
            Dim mTheta(3) As Byte
            Dim mSpare(3) As Byte
            Dim mReworkDotCounts(3) As Byte
            Dim mSendArray(21) As Byte
            Dim mArrayCounts As Integer

            '[Note]: "X" + (J、I、A、T、P、F、L、H) + ZoneNo + HeadNo + Theta + non-Rework dot count of F mode + Spare
            '5*4+2=22
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mZoneNo = BitConverter.GetBytes(zoneNo)
            mHeadNo = BitConverter.GetBytes(CInt(valveNo))
            mReworkDotCounts = BitConverter.GetBytes(reworkDotCounts)
            mSpare = BitConverter.GetBytes(CInt(0))
            mSendArray(0) = Char.ConvertToUtf32("X", 0)
            mArrayCounts = 1
            Select Case dispType
                Case enmTriggerDispType.JetParamRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("J", 0)
                Case enmTriggerDispType.NeedleJetParamRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("I", 0)
                Case enmTriggerDispType.AugerParamRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("A", 0)
                Case enmTriggerDispType.CycleRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("T", 0)
                Case enmTriggerDispType.PitchRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("P", 0)
                Case enmTriggerDispType.JetRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("F", 0)
                Case enmTriggerDispType.VisionRecipe
                    mSendArray(mArrayCounts) = Char.ConvertToUtf32("L", 0)
                Case Else
                    '[命令錯誤]
                    Return False
            End Select
            mArrayCounts = 2

            'ZoneNumber
            For mI = 0 To mZoneNo.Length - 1
                mSendArray(mArrayCounts + mI) = mZoneNo(mI)
            Next
            mArrayCounts = mArrayCounts + mZoneNo.Length
            'HeadNo
            For mI = 0 To mHeadNo.Length - 1
                mSendArray(mArrayCounts + mI) = mHeadNo(mI)
            Next
            mArrayCounts = mArrayCounts + mHeadNo.Length
            'Theta
            For mI = 0 To mTheta.Length - 1
                mSendArray(mArrayCounts + mI) = mTheta(mI)
            Next
            mArrayCounts = mArrayCounts + mTheta.Length
            'ReworkDotCounts
            For mI = 0 To mReworkDotCounts.Length - 1
                mSendArray(mArrayCounts + mI) = mReworkDotCounts(mI)
            Next
            mArrayCounts = mArrayCounts + mReworkDotCounts.Length
            'Spare
            For mI = 0 To mSpare.Length - 1
                mSendArray(mArrayCounts + mI) = mSpare(mI)
            Next
            mArrayCounts = mArrayCounts + mSpare.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mDispenseRun.Status = False
                End If
                Return mDispenseRun.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[設定膠管壓力 (S Command)]</summary>
    ''' <param name="valve"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPressure(valve As eValveWorkMode, value As Decimal, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetPressure
        Try
            Dim mI As Integer
            Dim mIndex As Integer
            Dim mValue(3) As Byte
            Dim mSendArray(6) As Byte
            Dim mArrayCounts As Integer

            '[Note]: "S" + (1+Index) + value
            '1*4+1+2=7
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mIndex = CInt(valve)
            mValue = BitConverter.GetBytes(value)

            mSendArray(0) = Char.ConvertToUtf32("S", 0)
            mArrayCounts = 1
            mSendArray(1) = 1
            mSendArray(2) = mIndex
            mArrayCounts = 3
            'ZoneNumber
            For mI = 0 To mValue.Length - 1
                mSendArray(mArrayCounts + mI) = mValue(mI)
            Next
            mArrayCounts = mArrayCounts + mValue.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mParameter.Status = False
                End If
                Return mParameter.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[設定閥體溫度 (S Command)]</summary>
    ''' <param name="valve"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTempture(valve As eValveWorkMode, value As Decimal, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetTempture
        Try
            Dim mI As Integer
            Dim mIndex As Integer
            Dim mValue(3) As Byte
            Dim mSendArray(6) As Byte
            Dim mArrayCounts As Integer

            '[Note]: "S" + (0+Index) + value
            '1*4+1+2=7
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mIndex = CInt(valve)
            mValue = BitConverter.GetBytes(value)

            mSendArray(0) = Char.ConvertToUtf32("S", 0)
            mArrayCounts = 1
            mSendArray(1) = 0
            mSendArray(2) = mIndex
            mArrayCounts = 3
            'ZoneNumber
            For mI = 0 To mValue.Length - 1
                mSendArray(mArrayCounts + mI) = mValue(mI)
            Next
            mArrayCounts = mArrayCounts + mValue.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mParameter.Status = False
                End If
                Return mParameter.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[設定Valve Power(Pandora)]</summary>
    ''' <param name="valve"></param>
    ''' <param name="powerOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValvePower(valve As eValveWorkMode, powerOn As Boolean, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetValvePower
        Try
            Dim mI As Integer
            Dim mIndex As Integer
            Dim mValue(3) As Byte
            Dim mSendArray(6) As Byte
            Dim mArrayCounts As Integer
            Dim mPowerValue As Single

            '[Note]: "S" + (2+Index) + value
            '1*4+1+2=7
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mIndex = CInt(valve)
            If powerOn = True Then
                '[Note]:給TriggerBoard必須是float<-->single
                mPowerValue = 99999
                'mValue(0) = 128
                'mValue(1) = 79
                'mValue(2) = 195
                'mValue(3) = 71
            Else
                '[Note]:給TriggerBoard必須是float<-->single
                mPowerValue = -99999
                'mValue(0) = 128
                'mValue(1) = 79
                'mValue(2) = 195
                'mValue(3) = 199
            End If
            mValue = BitConverter.GetBytes(mPowerValue)

            mSendArray(0) = Char.ConvertToUtf32("S", 0)
            mSendArray(1) = 2
            mSendArray(2) = mIndex
            mArrayCounts = 3
            'Value
            For mI = 0 To mValue.Length - 1
                mSendArray(mArrayCounts + mI) = mValue(mI)
            Next
            mArrayCounts = mArrayCounts + mValue.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mParameter.Status = False
                End If
                Return mParameter.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[設定Purge On/Off(Pandora)]</summary>
    ''' <param name="valve"></param>
    ''' <param name="purgeOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPurge(valve As eValveWorkMode, purgeOn As Boolean, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetPurge
        Try
            Dim mI As Integer
            Dim mIndex As Integer
            Dim mValue(3) As Byte
            Dim mSendArray(6) As Byte
            Dim mArrayCounts As Integer
            Dim mPowerValue As Single

            '[Note]: "S" + (9+Index) + value
            '1*4+1+2=7
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mIndex = CInt(valve)
            If purgeOn = True Then
                '[Note]:給TriggerBoard必須是float<-->single
                mPowerValue = 99999
                'mValue(0) = 128
                'mValue(1) = 79
                'mValue(2) = 195
                'mValue(3) = 71
            Else
                '[Note]:給TriggerBoard必須是float<-->single
                mPowerValue = -99999
                'mValue(0) = 128
                'mValue(1) = 79
                'mValue(2) = 195
                'mValue(3) = 199
            End If
            mValue = BitConverter.GetBytes(mPowerValue)

            mSendArray(0) = Char.ConvertToUtf32("S", 0)
            mSendArray(1) = 9
            mSendArray(2) = mIndex
            mArrayCounts = 3
            'Value
            For mI = 0 To mValue.Length - 1
                mSendArray(mArrayCounts + mI) = mValue(mI)
            Next
            mArrayCounts = mArrayCounts + mValue.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mParameter.Status = False
                End If
                Return mParameter.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function


    '20170920
    ''' <summary>[設定Tempture On/Off(Pandora)]</summary>
    ''' <param name="valve"></param>
    ''' <param name="TemptureOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTemptureOnOff(valve As eValveWorkMode, TemptureOn As Boolean, Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetTemptureOnOff
        Try
            Dim mI As Integer
            Dim mIndex As Integer
            Dim mValue(3) As Byte
            Dim mSendArray(6) As Byte
            Dim mArrayCounts As Integer
            Dim mPowerValue As Single

            '[Note]: "S" + (9+Index) + value
            '1*4+1+2=7
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mIndex = CInt(valve)
            If TemptureOn = True Then
                '[Note]:給TriggerBoard必須是float<-->single
                mPowerValue = 99999
                'mValue(0) = 128
                'mValue(1) = 79
                'mValue(2) = 195
                'mValue(3) = 71
            Else
                '[Note]:給TriggerBoard必須是float<-->single
                mPowerValue = -99999
                'mValue(0) = 128
                'mValue(1) = 79
                'mValue(2) = 195
                'mValue(3) = 199
            End If
            mValue = BitConverter.GetBytes(mPowerValue)

            mSendArray(0) = Char.ConvertToUtf32("S", 0)
            mSendArray(1) = 3
            mSendArray(2) = mIndex
            mArrayCounts = 3
            'Value
            For mI = 0 To mValue.Length - 1
                mSendArray(mArrayCounts + mI) = mValue(mI)
            Next
            mArrayCounts = mArrayCounts + mValue.Length

            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mParameter.Status = False
                End If
                Return mParameter.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[清除Alarm (c Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetResetAlarm(Optional waitReturn As Boolean = False) As Boolean Implements ITriggerBoard.SetResetAlarm
        Try
            Dim mSendArray(0) As Byte

            '[Note]: "c" 
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mSendArray(0) = Char.ConvertToUtf32("c", 0)
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mResetAlarm.Status = False
                End If
                Return mResetAlarm.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[詢問打點數]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="dotCounts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispenseCounts(Optional waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean Implements ITriggerBoard.GetDispenseCounts
        Try
            Dim mSendArray(0) As Byte

            '[Note]: "C"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mSendArray(0) = Char.ConvertToUtf32("C", 0)
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mDispenseCounts.Status = False
                End If
                If IsNumeric(mDispenseCounts.Value) Then
                    dotCounts = CLng(mDispenseCounts.Value)
                Else
                    dotCounts = 0
                End If
                Return mDispenseCounts.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function


    Public Function GetVisionCounts(Optional waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean Implements ITriggerBoard.GetVisionCounts
        Try
            Dim mSendArray(0) As Byte

            '[Note]: "O"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mSendArray(0) = Char.ConvertToUtf32("O", 0)
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mVisionCounts.Status = False
                End If
                If IsNumeric(mDispenseCounts.Value) Then
                    dotCounts = CLng(mVisionCounts.Value)
                Else
                    dotCounts = 0
                End If
                Return mDispenseCounts.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[詢問韌體版本 (V Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="version"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVersion(Optional waitReturn As Boolean = False, Optional ByRef version As String = "") As Boolean Implements ITriggerBoard.GetVersion
        Try
            Dim mSendArray(0) As Byte

            '[Note]: "V"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mSendArray(0) = Char.ConvertToUtf32("V", 0)
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mVersion.Status = False
                End If
                version = mVersion.Value
                Return mVersion.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[詢問異常代碼 (E Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="errorCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrorCode(Optional waitReturn As Boolean = False, Optional ByRef errorCode As String = "") As Boolean Implements ITriggerBoard.GetErrorCode
        Try
            Dim mSendArray(0) As Byte

            '[Note]: "E"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mSendArray(0) = Char.ConvertToUtf32("E", 0)
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mErrorCode.Status = False
                End If
                errorCode = mErrorCode.Value
                Return mErrorCode.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    ''' <summary>[詢問真實點膠之Cycle]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="cycleArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispCycle(Optional waitReturn As Boolean = False, Optional ByRef cycleArray As String = "") As Boolean Implements ITriggerBoard.GetDispCycle
        Try
            Dim mSendArray(0) As Byte

            '[Note]: "B"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            mSendArray(0) = Char.ConvertToUtf32("B", 0)
            SendCommandToSerialPort(mSendArray)

            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mCycleArray.Status = False
                End If
                cycleArray = mCycleArray.Value
                Return mCycleArray.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    '20171010
    ''' <summary>[詢問閥體溫度]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="tempArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTemp(Optional waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean Implements ITriggerBoard.GetTemperature
        Try
            Dim mSendArray(1) As Byte

            '[Note]: "R"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            'return Temperature(ASCII):R 0,Head0 heater,Head1 heater,Head0 piezo,Head1 piezo
            mSendArray(0) = Char.ConvertToUtf32("R", 0)
            mSendArray(1) = 0

            SendCommandToSerialPort(mSendArray)
            'Debug.Print("RS232 Temp Cmd")
            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mTemperature.Status = False
                End If
                tempArray = mTemperature.Value
                Return mTemperature.Status
            Else
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

    '20171010
    ''' <summary>[詢問目前開關狀態]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="tempArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSwitch(Optional waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean Implements ITriggerBoard.GetSwitch
        Try
            Dim mSendArray(1) As Byte

            '[Note]: "R"
            '1
            '暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
            'return HV#0,HV#1,Heater#0,Heater#1,purge#0,purge#1,Glue#0,Glue#1,Jet#0,Jet#1
            mSendArray(0) = Char.ConvertToUtf32("R", 0)
            mSendArray(1) = 2

            SendCommandToSerialPort(mSendArray)
            'Debug.Print("RS232 Temp Cmd")
            If waitReturn = True Then
                Do While (IsBusy = True)
                    System.Threading.Thread.CurrentThread.Join(1)
                    If IsTimeOut = True Then
                        Exit Do
                    End If
                Loop

                If IsTimeOut = True Then
                    mTemperature.Status = False
                End If
                tempArray = mTemperature.Value
                Return mTemperature.Status
            Else
                Return True
            End If


        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015004), "Error_1015004", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            ErrMsg = ex.ToString()
            mIsBusy = False
            mIsTimeOut = True
            mStopWatch.Stop()
            Return False
        End Try
    End Function

#End Region

 
End Class

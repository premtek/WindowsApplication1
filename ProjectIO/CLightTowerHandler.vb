Imports ProjectCore


''' <summary>輸出行為</summary>
''' <remarks></remarks>
Public Enum eDOBehavior
    [On] = 0
    Flash = 1
    [Off] = 2
End Enum

''' <summary>燈塔行為結構</summary>
''' <remarks></remarks>
Public Structure sBehavior
    ''' <summary>燈號名稱 </summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>輸出行為</summary>
    ''' <remarks></remarks>
    Public DOutput As eDOBehavior
    ''' <summary>閃爍週期(ms)</summary>
    ''' <remarks></remarks>
    Public FlashCycleTime As Integer
    ''' <summary>閃爍亮燈時間(ms)</summary>
    ''' <remarks></remarks>
    Public FlashPulseTime As Integer
End Structure


''' <summary>
''' 訊息參數
''' </summary>
''' <remarks></remarks>
Public Class CMessageParameter
    ''' <summary>紅燈行為</summary>
    ''' <remarks></remarks>
    Public DOutput(eIndicator.Count - 1) As sBehavior
End Class

''' <summary>三色燈燈號處理</summary>
''' <remarks></remarks>
Public Class CLightTowerHandler
    Implements IDisposable

    ''' <summary>[閃爍計時器]</summary>
    ''' <remarks></remarks>
    Dim mTimer As New System.Threading.Timer(AddressOf MessageParam_Flash, Nothing, System.Threading.Timeout.Infinite, 100)

    ''' <summary>碼表</summary>
    ''' <remarks></remarks>
    Dim mStopWatch As New Stopwatch
    ''' <summary>訊息行為參數集</summary>
    ''' <remarks></remarks>
    Public MessageParam(eMessageLevel.Count - 1) As CMessageParameter
    Sub New()
        For i As Integer = 0 To eMessageLevel.Count - 1
            MessageParam(i) = New CMessageParameter
        Next
        bCloseBuzzer = False
    End Sub

    'jimmy 20170523
    Private bCloseBuzzer As Boolean
    ''' <summary>
    ''' CloseBuzzer
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property _bCloseBuzzer() As Boolean
        Set(ByVal value As Boolean)
            bCloseBuzzer = value
        End Set
    End Property

    ''' <summary>[節取目前Buzzer狀態(是否Pass)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BuzzerStatus As Boolean
        Get
            Return bCloseBuzzer
        End Get
    End Property


    ''' <summary>[開始進行燈號間測與更新]</summary>
    ''' <remarks></remarks>
    Public Sub ThreadStart()
        mStopWatch.Start()
        mTimer.Change(0, 100)
    End Sub

    ''' <summary>訊息行為儲存</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMessageParam(ByVal strFileName As String) As Boolean
        Try
            Dim strSection As String = "Message"

            For mLvNo As Integer = 0 To eMessageLevel.Count - 1
                SaveIniString(strSection, mLvNo & "Red_DOutput", CInt(MessageParam(mLvNo).DOutput(eIndicator.Red).DOutput), strFileName)
                SaveIniString(strSection, mLvNo & "Red_FlashCycleTime", MessageParam(mLvNo).DOutput(eIndicator.Red).FlashCycleTime, strFileName)
                SaveIniString(strSection, mLvNo & "Red_FlashPulseTime", MessageParam(mLvNo).DOutput(eIndicator.Red).FlashPulseTime, strFileName)

                SaveIniString(strSection, mLvNo & "Yellow_DOutput", CInt(MessageParam(mLvNo).DOutput(eIndicator.Yellow).DOutput), strFileName)
                SaveIniString(strSection, mLvNo & "Yellow_FlashCycleTime", MessageParam(mLvNo).DOutput(eIndicator.Yellow).FlashCycleTime, strFileName)
                SaveIniString(strSection, mLvNo & "Yellow_FlashPulseTime", MessageParam(mLvNo).DOutput(eIndicator.Yellow).FlashPulseTime, strFileName)

                SaveIniString(strSection, mLvNo & "Green_DOutput", CInt(MessageParam(mLvNo).DOutput(eIndicator.Green).DOutput), strFileName)
                SaveIniString(strSection, mLvNo & "Green_FlashCycleTime", MessageParam(mLvNo).DOutput(eIndicator.Green).FlashCycleTime, strFileName)
                SaveIniString(strSection, mLvNo & "Green_FlashPulseTime", MessageParam(mLvNo).DOutput(eIndicator.Green).FlashPulseTime, strFileName)

                SaveIniString(strSection, mLvNo & "Blue_DOutput", CInt(MessageParam(mLvNo).DOutput(eIndicator.Blue).DOutput), strFileName)
                SaveIniString(strSection, mLvNo & "Blue_FlashCycleTime", MessageParam(mLvNo).DOutput(eIndicator.Blue).FlashCycleTime, strFileName)
                SaveIniString(strSection, mLvNo & "Blue_FlashPulseTime", MessageParam(mLvNo).DOutput(eIndicator.Blue).FlashPulseTime, strFileName)

                SaveIniString(strSection, mLvNo & "Buzzer_DOutput", CInt(MessageParam(mLvNo).DOutput(eIndicator.Buzzer).DOutput), strFileName)
                SaveIniString(strSection, mLvNo & "Buzzer_FlashCycleTime", MessageParam(mLvNo).DOutput(eIndicator.Buzzer).FlashCycleTime, strFileName)
                SaveIniString(strSection, mLvNo & "Buzzer_FlashPulseTime", MessageParam(mLvNo).DOutput(eIndicator.Buzzer).FlashPulseTime, strFileName)
            Next
            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002025), "Error_1002025", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try


    End Function
    ''' <summary>訊息行為讀取</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadMessageParam(ByVal strFileName As String) As Boolean
        Try

            Dim strSection As String = "Message"
            Dim defaultParam(eMessageLevel.Count - 1) As CMessageParameter

            For mLvNo As Integer = 0 To eMessageLevel.Count - 1
                defaultParam(mLvNo) = New CMessageParameter
            Next
            '--- 燈號預設值 ---
            defaultParam(eMessageLevel.Running).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Running).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Running).DOutput(eIndicator.Green).DOutput = eDOBehavior.On
            defaultParam(eMessageLevel.Running).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Running).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off

            defaultParam(eMessageLevel.Information).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Information).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Information).DOutput(eIndicator.Green).DOutput = eDOBehavior.Flash
            defaultParam(eMessageLevel.Information).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Information).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off

            defaultParam(eMessageLevel.Warning).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Warning).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
            defaultParam(eMessageLevel.Warning).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Warning).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Warning).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off

            defaultParam(eMessageLevel.Idle).DOutput(eIndicator.Red).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Idle).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.On
            defaultParam(eMessageLevel.Idle).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Idle).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Idle).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.Off

            defaultParam(eMessageLevel.Error).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
            defaultParam(eMessageLevel.Error).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Error).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Error).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Error).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On

            defaultParam(eMessageLevel.Alarm).DOutput(eIndicator.Red).DOutput = eDOBehavior.On
            defaultParam(eMessageLevel.Alarm).DOutput(eIndicator.Yellow).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Alarm).DOutput(eIndicator.Green).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Alarm).DOutput(eIndicator.Blue).DOutput = eDOBehavior.Off
            defaultParam(eMessageLevel.Alarm).DOutput(eIndicator.Buzzer).DOutput = eDOBehavior.On

            gSyslog.Save("Light Tower Config Loaded as below:")

            For mLvNo As Integer = 0 To eMessageLevel.Count - 1
                MessageParam(mLvNo).DOutput(eIndicator.Red).Name = "Red Indicator"
                MessageParam(mLvNo).DOutput(eIndicator.Red).DOutput = Val(ReadIniString(strSection, mLvNo & "Red_DOutput", strFileName, defaultParam(mLvNo).DOutput(eIndicator.Red).DOutput))
                MessageParam(mLvNo).DOutput(eIndicator.Red).FlashCycleTime = Val(ReadIniString(strSection, mLvNo & "Red_FlashCycleTime", strFileName, 1000))
                MessageParam(mLvNo).DOutput(eIndicator.Red).FlashPulseTime = Val(ReadIniString(strSection, mLvNo & "Red_FlashPulseTime", strFileName, 500))

                MessageParam(mLvNo).DOutput(eIndicator.Yellow).Name = "Yellow Indicator"
                MessageParam(mLvNo).DOutput(eIndicator.Yellow).DOutput = Val(ReadIniString(strSection, mLvNo & "Yellow_DOutput", strFileName, defaultParam(mLvNo).DOutput(eIndicator.Yellow).DOutput))
                MessageParam(mLvNo).DOutput(eIndicator.Yellow).FlashCycleTime = Val(ReadIniString(strSection, mLvNo & "Yellow_FlashCycleTime", strFileName, 1000))
                MessageParam(mLvNo).DOutput(eIndicator.Yellow).FlashPulseTime = Val(ReadIniString(strSection, mLvNo & "Yellow_FlashPulseTime", strFileName, 500))

                MessageParam(mLvNo).DOutput(eIndicator.Green).Name = "Green Indicator"
                MessageParam(mLvNo).DOutput(eIndicator.Green).DOutput = Val(ReadIniString(strSection, mLvNo & "Green_DOutput", strFileName, defaultParam(mLvNo).DOutput(eIndicator.Green).DOutput))
                MessageParam(mLvNo).DOutput(eIndicator.Green).FlashCycleTime = Val(ReadIniString(strSection, mLvNo & "Green_FlashCycleTime", strFileName, 1000))
                MessageParam(mLvNo).DOutput(eIndicator.Green).FlashPulseTime = Val(ReadIniString(strSection, mLvNo & "Green_FlashPulseTime", strFileName, 500))

                MessageParam(mLvNo).DOutput(eIndicator.Blue).Name = "Blue Indicator"
                MessageParam(mLvNo).DOutput(eIndicator.Blue).DOutput = Val(ReadIniString(strSection, mLvNo & "Blue_DOutput", strFileName, defaultParam(mLvNo).DOutput(eIndicator.Blue).DOutput))
                MessageParam(mLvNo).DOutput(eIndicator.Blue).FlashCycleTime = Val(ReadIniString(strSection, mLvNo & "Blue_FlashCycleTime", strFileName, 1000))
                MessageParam(mLvNo).DOutput(eIndicator.Blue).FlashPulseTime = Val(ReadIniString(strSection, mLvNo & "Blue_FlashPulseTime", strFileName, 500))

                MessageParam(mLvNo).DOutput(eIndicator.Buzzer).Name = "Buzzer"
                MessageParam(mLvNo).DOutput(eIndicator.Buzzer).DOutput = Val(ReadIniString(strSection, mLvNo & "Buzzer_DOutput", strFileName, defaultParam(mLvNo).DOutput(eIndicator.Buzzer).DOutput))
                MessageParam(mLvNo).DOutput(eIndicator.Buzzer).FlashCycleTime = Val(ReadIniString(strSection, mLvNo & "Buzzer_FlashCycleTime", strFileName, 1000))
                MessageParam(mLvNo).DOutput(eIndicator.Buzzer).FlashPulseTime = Val(ReadIniString(strSection, mLvNo & "Buzzer_FlashPulseTime", strFileName, 500))
                Dim logData As String = "Light Tower Config(" & mLvNo & "): "
                With MessageParam(mLvNo).DOutput(eIndicator.Red)
                    logData += " Red:   " & vbTab & CInt(.DOutput) & " PT: " & vbTab & .FlashPulseTime & vbTab & " CT: " & vbTab & .FlashCycleTime
                End With
                With MessageParam(mLvNo).DOutput(eIndicator.Yellow)
                    logData += " Yellow:" & vbTab & CInt(.DOutput) & " PT: " & vbTab & .FlashPulseTime & " CT: " & vbTab & .FlashCycleTime
                End With
                With MessageParam(mLvNo).DOutput(eIndicator.Green)
                    logData += " Green: " & vbTab & CInt(.DOutput) & " PT: " & vbTab & .FlashPulseTime & " CT: " & vbTab & .FlashCycleTime
                End With
                With MessageParam(mLvNo).DOutput(eIndicator.Blue)
                    logData += " Blue:  " & vbTab & CInt(.DOutput) & " PT: " & vbTab & .FlashPulseTime & " CT: " & vbTab & .FlashCycleTime
                End With
                With MessageParam(mLvNo).DOutput(eIndicator.Buzzer)
                    logData += " Buzzer:" & vbTab & CInt(.DOutput) & " PT: " & vbTab & .FlashPulseTime & " CT: " & vbTab & .FlashCycleTime
                End With
                logData += [Enum].Parse(GetType(eMessageLevel), mLvNo).ToString
                gSyslog.Save(logData)

            Next
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002024), "Error_1002024", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try

    End Function

    Public Event OnIndicatorChanged(ByVal sender As Object, ByVal e As IndicatorEventArgs)
    'Public Event OnIndicatorChanged(ByVal indicator As eIndicator, ByVal value As Boolean)

    ''' <summary>設定輸出</summary>
    ''' <param name="msgLevel"></param>
    ''' <param name="indicator"></param>
    ''' <param name="indicatorDO"></param>
    ''' <remarks></remarks>
    Sub SetIndicator(ByVal msgLevel As eMessageLevel, ByVal indicator As eIndicator, ByVal indicatorDO As Integer)
        If msgLevel < 0 Then
            Exit Sub
        End If
        If msgLevel > MessageParam.Count - 1 Then
            Exit Sub
        End If
        If indicator < 0 Then
            Exit Sub
        End If
        If indicator > MessageParam(msgLevel).DOutput.Count - 1 Then
            Exit Sub
        End If
        If indicatorDO < 0 Then
            Exit Sub
        End If
        If indicatorDO > gDOCollection.TotalBits - 1 Then
            Exit Sub
        End If

        Select Case MessageParam(msgLevel).DOutput(indicator).DOutput
            Case eDOBehavior.On
                '[Note]:針對蜂鳴器做特別處理，若Pass蜂鳴器，則關閉。
                If indicator = eIndicator.Buzzer Then
                    If MessageParam(msgLevel).DOutput(indicator).DOutput = eDOBehavior.On Then
                        If bCloseBuzzer = True Then
                            gDOCollection.SetState(indicatorDO, False)
                        Else
                            gDOCollection.SetState(indicatorDO, True)
                        End If
                    End If
                Else
                    gDOCollection.SetState(indicatorDO, True)
                End If

                RaiseEvent OnIndicatorChanged(Me, New IndicatorEventArgs(indicator, True))

            Case eDOBehavior.Off
                gDOCollection.SetState(indicatorDO, False)
                RaiseEvent OnIndicatorChanged(Me, New IndicatorEventArgs(indicator, False))

            Case eDOBehavior.Flash
                '剩餘時間
                If MessageParam(msgLevel).DOutput(indicator).FlashCycleTime <> 0 Then
                    Dim RemainderTime As Integer = mStopWatch.ElapsedMilliseconds Mod MessageParam(msgLevel).DOutput(indicator).FlashCycleTime
                    If RemainderTime > MessageParam(msgLevel).DOutput(indicator).FlashPulseTime Then
                        gDOCollection.SetState(indicatorDO, False)
                        RaiseEvent OnIndicatorChanged(Me, New IndicatorEventArgs(indicator, False))
                    Else
                        gDOCollection.SetState(indicatorDO, True)
                        RaiseEvent OnIndicatorChanged(Me, New IndicatorEventArgs(indicator, True))
                    End If
                End If

        End Select
    End Sub

    ''' <summary>內部計時更新</summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Sub MessageParam_Flash(state As Object)
        'Soni / 2016.09.09 燈號顯示擴充
        Select Case gEqpMsg.MsgLevel
            Case eMessageLevel.Error
                SetIndicator(eMessageLevel.Error, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Error, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Error, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Error, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Error, eIndicator.Buzzer, enmDO.Buzzer)
                Exit Sub
            Case eMessageLevel.Alarm
                SetIndicator(eMessageLevel.Alarm, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Buzzer, enmDO.Buzzer)
                Exit Sub
            Case eMessageLevel.Warning
                SetIndicator(eMessageLevel.Warning, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Buzzer, enmDO.Buzzer)
                Exit Sub
        End Select

        If eSys.OverAll < 0 Then '整機系統索引不存在
            gSyslog.Save("eSys.OverAll NOT Exists.", , eMessageLevel.Alarm)
            Exit Sub
        End If
        If gSYS(eSys.OverAll) Is Nothing Then '整機系統參數不存在
            'gSyslog.Save("gSYS(eSys.OverAll) NOT Exists.", , eMessageLevel.Alarm)
            Exit Sub
        End If
        Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
            Case enmRunStatus.None
                If gSYS(eSys.OverAll).ExecuteCommand = eSysCommand.Home Then
                    Select Case gSYS(eSys.OverAll).RunStatus
                        Case enmRunStatus.Running
                            SetIndicator(eMessageLevel.Running, eIndicator.Red, enmDO.RedIndicator)
                            SetIndicator(eMessageLevel.Running, eIndicator.Yellow, enmDO.YellowIndicator)
                            SetIndicator(eMessageLevel.Running, eIndicator.Green, enmDO.GreenIndicator)
                            SetIndicator(eMessageLevel.Running, eIndicator.Blue, enmDO.BlueIndicator)
                            SetIndicator(eMessageLevel.Running, eIndicator.Buzzer, enmDO.Buzzer)
                            Exit Sub
                    End Select
                End If

                '[說明]:等待系統初始化
                SetIndicator(eMessageLevel.Warning, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Buzzer, enmDO.Buzzer)

            Case enmRunStatus.Running
                '[說明]:系統初始化執行中
                SetIndicator(eMessageLevel.Running, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Running, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Running, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Running, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Running, eIndicator.Buzzer, enmDO.Buzzer)


            Case enmRunStatus.Stop
                '[說明]:系統初始化 Purse
                SetIndicator(eMessageLevel.Warning, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Warning, eIndicator.Buzzer, enmDO.Buzzer)

            Case enmRunStatus.Alarm
                '[說明]:系統初始化 Alarm
                SetIndicator(eMessageLevel.Alarm, eIndicator.Red, enmDO.RedIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Yellow, enmDO.YellowIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Green, enmDO.GreenIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Blue, enmDO.BlueIndicator)
                SetIndicator(eMessageLevel.Alarm, eIndicator.Buzzer, enmDO.Buzzer)

            Case enmRunStatus.Finish
                Select Case gSYS(eSys.OverAll).RunStatus
                    Case enmRunStatus.None
                        '[說明]:系統初始化 完成
                        SetIndicator(eMessageLevel.Warning, eIndicator.Red, enmDO.RedIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Yellow, enmDO.YellowIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Green, enmDO.GreenIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Blue, enmDO.BlueIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Buzzer, enmDO.Buzzer)

                    Case enmRunStatus.Running
                        '[說明]:運作中
                        SetIndicator(eMessageLevel.Running, eIndicator.Red, enmDO.RedIndicator)
                        SetIndicator(eMessageLevel.Running, eIndicator.Yellow, enmDO.YellowIndicator)
                        SetIndicator(eMessageLevel.Running, eIndicator.Green, enmDO.GreenIndicator)
                        SetIndicator(eMessageLevel.Running, eIndicator.Blue, enmDO.BlueIndicator)
                        SetIndicator(eMessageLevel.Running, eIndicator.Buzzer, enmDO.Buzzer)


                    Case enmRunStatus.Stop
                        '[說明]:停止生產
                        SetIndicator(eMessageLevel.Warning, eIndicator.Red, enmDO.RedIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Yellow, enmDO.YellowIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Green, enmDO.GreenIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Blue, enmDO.BlueIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Buzzer, enmDO.Buzzer)

                    Case enmRunStatus.Alarm
                        '[說明]:Alarm
                        SetIndicator(eMessageLevel.Alarm, eIndicator.Red, enmDO.RedIndicator)
                        SetIndicator(eMessageLevel.Alarm, eIndicator.Yellow, enmDO.YellowIndicator)
                        SetIndicator(eMessageLevel.Alarm, eIndicator.Green, enmDO.GreenIndicator)
                        SetIndicator(eMessageLevel.Alarm, eIndicator.Blue, enmDO.BlueIndicator)
                        SetIndicator(eMessageLevel.Alarm, eIndicator.Buzzer, enmDO.Buzzer)


                    Case enmRunStatus.Finish
                        '[說明]:生產完成亮黃燈，通知User
                        SetIndicator(eMessageLevel.Warning, eIndicator.Red, enmDO.RedIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Yellow, enmDO.YellowIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Green, enmDO.GreenIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Blue, enmDO.BlueIndicator)
                        SetIndicator(eMessageLevel.Warning, eIndicator.Buzzer, enmDO.Buzzer)

                End Select
        End Select
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mTimer.Dispose()
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

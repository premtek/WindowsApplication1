Imports ProjectCore
Imports ProjectRecipe
Imports ProjectMotion


#Region "全域"
''' <summary>[觸發版類型]</summary>
''' <remarks></remarks>
Public Enum eTriggerBoardType
    ''' <summary>虛擬</summary>
    ''' <remarks></remarks>
    None = 0
    ''' <summary>Trigger3.0</summary>
    ''' <remarks></remarks>
    Trigger30 = 1
End Enum
#End Region

#Region "連線參數"

''' <summary>[使用Trigger Board 3.0的序列埠]</summary>
''' <remarks></remarks>
Public Structure sTriggerBoard30Connect
    ''' <summary>[通訊連接埠號]</summary>
    ''' <remarks></remarks>
    Public PortName As String
    ''' <summary>[通訊序列傳輸速率]</summary>
    ''' <remarks></remarks>
    Public BaudRate As String
    ''' <summary>通訊逾時之時間(ms)</summary>
    ''' <remarks></remarks>
    Public TimeOutTimes As Decimal
End Structure
''' <summary>[觸發版連線參數]</summary>
''' <remarks></remarks>
Public Structure sTriggerBoardConnectionParameter
    ''' <summary>[硬體型號(類型)]</summary>
    ''' <remarks></remarks>
    Public CardType As eTriggerBoardType
    ''' <summary>[Trigger Board 3.0的通訊設定]</summary>
    ''' <remarks></remarks>
    Public TriggerBoard30 As sTriggerBoard30Connect
    ''' <summary>[硬體型號之名稱(字串表示)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CardTypeString As String
        Get
            Select Case CardType
                Case eTriggerBoardType.None
                    Return "None"
                Case eTriggerBoardType.Trigger30
                    Return "Trigger 3.0"
                Case Else
                    Return "Unknown"
            End Select
        End Get
    End Property
End Structure

#End Region

#Region "接點參數"
''' <summary>[觸發版接點參數]</summary>
''' <remarks></remarks>
Public Structure sTriggerBoardParameter
    ''' <summary>[點位名稱]</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>[硬體型號(類型)]</summary>
    ''' <remarks></remarks>
    Public CardType As eTriggerBoardType
    ''' <summary>[位置(Ethernet)]</summary>
    ''' <remarks></remarks>
    Public IP As Long
    ''' <summary>[埠號(Ethernet)]</summary>
    ''' <remarks></remarks>
    Public Port As Long
    ''' <summary>[埠號(RS232)]</summary>
    ''' <remarks></remarks>
    Public PortName As String
    ''' <summary>[通訊傳輸速率(RS232)]</summary>
    ''' <remarks></remarks>
    Public BaudRate As String
    ''' <summary>[通訊逾時之時間(ms)]</summary>
    ''' <remarks></remarks>
    Public TimerOutTimes As Decimal
    ''' <summary>[Item索引]</summary>
    ''' <remarks></remarks>
    Public ItemNo As Integer
End Structure


#End Region

''' <summary>[觸發版物件類別]</summary>
''' <remarks></remarks>
Public Class CTriggerBoardCollection

    ''' <summary>[卡片初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsCardIntialOK As Boolean
        Get
            Return mIsCardIntialOK
        End Get
    End Property
    ''' <summary>[卡片初始化狀態]</summary>
    ''' <remarks></remarks>
    Private mIsCardIntialOK As Boolean

    ''' <summary>[是否走Simulation模式]</summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property IsSimulationType As Boolean
        Set(value As Boolean)
            mIsSimulationType = value
        End Set
    End Property
    ''' <summary>[是否走Simulation模式]</summary>
    ''' <remarks></remarks>
    Private mIsSimulationType As Boolean

    ''' <summary>[連線設定參數]</summary>
    ''' <remarks></remarks>
    Public TBConnectionParameter(enmTriggerBoard.Max) As sTriggerBoardConnectionParameter

    ''' <summary>[接點參數設定]</summary>
    ''' <remarks></remarks>
    Public TBParameter(enmTriggerBoard.Max) As sTriggerBoardParameter

    ''' <summary>[SaveConnectionParameter]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Dim mSection As String
        Dim mI As Integer
        For mI = 0 To gTriggerBoard.TBConnectionParameter.Count - 1
            mSection = "TriggerBoardNo" & (mI + 1).ToString()
            With gTriggerBoard.TBConnectionParameter(mI)
                Call SaveIniString(mSection, "CardType", CInt(.CardType), fileName)
                Select Case .CardType
                    Case eTriggerBoardType.None

                    Case eTriggerBoardType.Trigger30
                        Call SaveIniString(mSection, "TriggerBoard30-PortName", .TriggerBoard30.PortName, fileName)
                        Call SaveIniString(mSection, "TriggerBoard30-BaudRate", .TriggerBoard30.BaudRate, fileName)
                        Call SaveIniString(mSection, "TriggerBoard30-TimeOutTimes", .TriggerBoard30.TimeOutTimes, fileName)

                End Select
            End With
        Next
        Return True
    End Function

    ''' <summary>[LoadTriggerConnection]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Dim mSection As String
        Dim mI As Integer
        For mI = 0 To gTriggerBoard.TBConnectionParameter.Count - 1
            mSection = "TriggerBoardNo" & (mI + 1).ToString()
            With gTriggerBoard.TBConnectionParameter(mI)
                .CardType = CInt(ReadIniString(mSection, "CardType", fileName, 0))
                Select Case .CardType
                    Case eTriggerBoardType.None

                    Case eTriggerBoardType.Trigger30
                        .TriggerBoard30.PortName = ReadIniString(mSection, "TriggerBoard30-PortName", fileName, 0)
                        .TriggerBoard30.BaudRate = ReadIniString(mSection, "TriggerBoard30-BaudRate", fileName, 115200)
                        .TriggerBoard30.TimeOutTimes = CDec(ReadIniString(mSection, "TriggerBoard30-TimeOutTimes", fileName, 500))

                End Select
            End With
        Next
        Return True
    End Function

    ''' <summary>[卡集合]</summary>
    ''' <remarks></remarks>
    Public Items As New List(Of ITriggerBoard)


#Region "Definitions"
    ''' <summary>[Error Message]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrMsg(ByVal triggerNo As Integer) As String
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return ""
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return ""
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return ""
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).ErrMsg
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return ""
            End Try
        End Get
        Set(value As String)
            With TBParameter(triggerNo)
                Items(.ItemNo).ErrMsg = value
            End With
        End Set
    End Property
    ''' <summary>[忙碌中]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy(ByVal triggerNo As Integer) As Boolean
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return False
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return False
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).IsBusy
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return False
            End Try
        End Get
    End Property
    ''' <summary>[TimeOut(逾時)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut(ByVal triggerNo As Integer) As Boolean
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return False
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return False
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).IsTimeOut
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return True
            End Try
        End Get
    End Property
    ''' <summary>[設定Timeout時間]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeoutTimes(ByVal triggerNo As Integer) As Integer
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return 0
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return 0
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return 0
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).TimeoutTimes
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return 0
            End Try
        End Get
        Set(value As Integer)
            With TBParameter(triggerNo)
                Items(.ItemNo).TimeoutTimes = value
            End With
        End Set
    End Property
    ''' <summary>[Is Port Open?]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PortIsOpen(ByVal triggerNo As Integer) As Boolean
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return False
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return False
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).PortIsOpen
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return False
            End Try
        End Get
    End Property
    ''' <summary>[是否初始化成功]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsInitialOK(ByVal triggerNo As Integer) As Boolean
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return False
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return False
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).IsInitialOK
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return False
            End Try
        End Get
    End Property
    ''' <summary>[f Command的續傳量(F Command資料續傳-->f Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransmissionResumingOfStepCount(ByVal triggerNo As Integer) As Integer
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                Return False
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                Return False
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).TransmissionResumingOfStepCount
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return False
            End Try
        End Get
    End Property
    ''' <summary>[J Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property JetParamRecipe(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).JetParamRecipe
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[G Command之回傳字串(將參數丟給Trigger Board)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property JetParameter(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).JetParameter
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property

    ''' <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property VisionRecipe(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).VisionRecipe
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property

    ''' <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property JetRecipe(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).JetRecipe
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[T Command之回傳字串(固定頻率打點)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CycleRecipe(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).CycleRecipe
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[P Command之回傳字串(固定間距打點)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PitchRecipe(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).PitchRecipe
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[X Command之回傳字串(Dispensing Run)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DispenseRun(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).DispenseRun
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[D Command之回傳字串(Dummy Run)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DummyRun(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).DummyRun
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[S Command之回傳字串(閥體溫度、膠管壓力、閥體電源)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Parameter(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).Parameter
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[C Command之回傳字串(點膠觸發數)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DispenseCounts(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = "0"
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = "0"
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = "0"
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).DispenseCounts
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = "0"
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[O Command之回傳字串(取像觸發數)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property VisionCounts(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = "0"
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = "0"
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = "0"
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).VisionCounts
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = "0"
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[V Command之回傳字串(韌體版本)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Version(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).Version
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[B Command之回傳字串(點膠真實Cycle)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CycleArray(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).CycleArray
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[E Command之回傳字串(異常代號)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ErrorCode(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).ErrorCode
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property
    ''' <summary>[c Command之回傳字串(異常清除)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResetAlarm(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).ResetAlarm
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property

    ''' <summary>[E Command之回傳字串(異常代號)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Temperature(ByVal triggerNo As Integer) As sReceiveStatus
        Get
            Dim mStatus As sReceiveStatus
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If

            '[Note]:卡不存在
            If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            '[Note]:資料對應有問題
            If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
                mStatus.STR = ""
                mStatus.Status = True
                mStatus.Value = ""
                Return mStatus
            End If
            Try
                With TBParameter(triggerNo)
                    Return Items(.ItemNo).Temperature
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Temperature")
                mStatus.STR = ""
                mStatus.Status = False
                mStatus.Value = ""
                Return mStatus
            End Try
        End Get
    End Property

#End Region

#Region "Properties"
    ''' <summary>[初始化]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cardParameters() As sTriggerBoardConnectionParameter) As Boolean
        Try
            Dim mErrMessage As String = ""
            Dim mI As Integer

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                Return True
            End If

            For mI = cardParameters.GetLowerBound(0) To cardParameters.GetUpperBound(0)
                '[Note]:依卡型號做初始化
                Select Case cardParameters(mI).CardType
                    Case eTriggerBoardType.None
                        '[Note]:虛擬觸發板
                        Items.Add(New CTriggerBoard_Virtual)
                        TBParameter(mI).ItemNo = mI

                    Case eTriggerBoardType.Trigger30
                        Items.Add(New CTriggerBoard_30)
                        TBParameter(mI).ItemNo = mI
                        If Items(Items.Count - 1).Initial(cardParameters(mI).TriggerBoard30.PortName, cardParameters(mI).TriggerBoard30.BaudRate) = False Then
                            mErrMessage += "" & cardParameters(mI).CardTypeString & "初始化失敗."
                        End If

                End Select
            Next

            If mErrMessage <> "" Then
                '[Note]:有錯誤訊息
                mIsCardIntialOK = False
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
                gSyslog.Save("Eror Message: " & mErrMessage, , eMessageLevel.Error)
                MsgBox(mErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "TriggerBoard Collection")
                Return False
            End If

            '[Note]:正常結束
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6004000))
            mIsCardIntialOK = True
            Return True

        Catch ex As Exception
            mIsCardIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox("初始化失敗" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[釋放資源]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Dispose() As Boolean
        Try
            Dim mI As Integer

            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return True
            End If
            If mIsCardIntialOK = True Then
                '[Note]:卡片關閉
                For mI = 0 To Items.Count - 1
                    Items(mI).Dispose()
                Next
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                Return True
            Else
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[關閉通訊埠連線]</summary>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Try
            Dim mI As Integer

            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return True
            End If
            If mIsCardIntialOK = True Then
                For mI = 0 To Items.Count - 1
                    If Items(mI).PortIsOpen = True Then
                        '[Note]:關閉通訊埠連線
                        Items(mI).Close()
                    End If
                Next
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                Return True
            Else
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                Return True
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try

    End Function

    ''' <summary>[Send Command]</summary>
    ''' <param name="cmdBtye"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommandToSerialPort(ByVal triggerNo As Integer, ByVal cmdBtye() As Byte) As Boolean

        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If
        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If

        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SendCommandToSerialPort(cmdBtye)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try

    End Function

#End Region

#Region "Function"
    ''' <summary>[取得目前電腦的序列埠代號]</summary>
    ''' <param name="PortIDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortIDs(ByVal triggerNo As Integer, ByRef portIDs() As String) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetPortIDs(portIDs)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[J Command的資料串接]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="is1stStep"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddJetParamRecipe(ByVal triggerNo As Integer, ByVal is1stStep As Boolean, ByVal zoneNo As Integer, ByVal patternStep As sTriggerJCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerJCmdParam = Nothing) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).AddJetParamRecipe(is1stStep, zoneNo, patternStep, isLastStep, parameter)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[將點膠資料丟給Trigger Board(J Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetParamRecipe(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetJetParamRecipe(waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[Parameter of Jet(只丟參數)(G Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetParameter(ByVal triggerNo As Integer, ByVal parameter As sTriggerGCmdParam, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetJetParameter(parameter, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[L Command的資料串接]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="is1stStep"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddVisionRecipe(ByVal triggerNo As Integer, ByVal is1stStep As Boolean, ByVal patternStep As sTriggerVisionCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerVisionCmdParam = Nothing) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).AddVisionRecipe(is1stStep, patternStep, isLastStep, parameter)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[將點膠資料丟給Trigger Board(L Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetVisionRecipe(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetVisionRecipe(waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[F Command的資料串接]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="is1stStep"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddJetRecipe(ByVal triggerNo As Integer, ByVal is1stStep As Boolean, ByVal patternStep As sTriggerFCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerFCmdParam = Nothing) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).AddJetRecipe(is1stStep, patternStep, isLastStep, parameter)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[將點膠資料丟給Trigger Board(F Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetRecipe(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetJetRecipe(waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[f Command的資料串接(F Command資料續傳-->f Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="is1stStep"></param>
    ''' <param name="patternStep"></param>
    ''' <param name="isLastStep"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddJetRecipeUseTransmissionResuming(ByVal triggerNo As Integer, ByVal is1stStep As Boolean, ByVal patternStep As sTriggerFCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerFCmdParam = Nothing) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).AddJetRecipeUseTransmissionResuming(is1stStep, patternStep, isLastStep, parameter)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@AddJetRecipeUseTransmissionResuming")
            Return False
        End Try
    End Function

    ''' <summary>[將點膠資料丟給Trigger Board(f Command-->F Command的續傳)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetJetRecipeByTransmissionResuming(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetJetRecipeByTransmissionResuming(waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@SetJetRecipeByTransmissionResuming")
            Return False
        End Try
    End Function

    ''' <summary>[Dummy Run(D Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="dispType"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDummyRun(ByVal triggerNo As Integer, ByVal dispType As enmTriggerDispType, ByVal valveNo As eValveWorkMode, ByVal zoneNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetDummyRun(dispType, valveNo, zoneNo, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[Free Type Dispensing (X Command)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="dispType"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="degree"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDispenseRun(ByVal triggerNo As Integer, ByVal dispType As enmTriggerDispType, ByVal valveNo As eValveWorkMode, ByVal zoneNo As Integer, ByVal degree As Decimal, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetDispenseRun(dispType, valveNo, zoneNo, degree, 0, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[固定頻率打點 T Command(Cycle Time)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCycleRecipe(ByVal triggerNo As Integer, ByVal parameter As sTriggerTPCmdParam, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetCycleRecipe(parameter, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[固定間距打點 P Command(Pitch)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPitchRecipe(ByVal triggerNo As Integer, ByVal parameter As sTriggerTPCmdParam, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetPitchRecipe(parameter, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[設定閥體溫度]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="valve"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTempture(ByVal triggerNo As Integer, ByVal valve As eValveWorkMode, ByVal value As Decimal, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetTempture(valve, value, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[設定膠管壓力]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="valve"></param>
    ''' <param name="value"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPressure(ByVal triggerNo As Integer, ByVal valve As eValveWorkMode, ByVal value As Decimal, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetPressure(valve, value, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[設定閥體電源開關]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="valve"></param>
    ''' <param name="powerOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValvePower(ByVal triggerNo As Integer, ByVal valve As eValveWorkMode, ByVal powerOn As Boolean, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetValvePower(valve, powerOn, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[設定Purge開關]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="valve"></param>
    ''' <param name="purgeOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPurge(ByVal triggerNo As Integer, ByVal valve As eValveWorkMode, ByVal purgeOn As Boolean, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetPurge(valve, purgeOn, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function


    '20170920
    ''' <summary>[設定SetTempture開關]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="valve"></param>
    ''' <param name="TemptureOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTemptureOnOff(ByVal triggerNo As Integer, ByVal valve As eValveWorkMode, ByVal TemptureOn As Boolean, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetTemptureOnOff(valve, TemptureOn, waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[回傳打點數]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="dotCounts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispenseCounts(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetDispenseCounts(waitReturn, dotCounts)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[回傳取像數]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="dotCounts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVisionCounts(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetVisionCounts(waitReturn, dotCounts)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[傳回韌體版本]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="version"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVersion(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef version As String = "") As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetVersion(waitReturn, version)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[傳回點膠Cycle(真實)]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="cycleArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispCycle(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef cycleArray As String = "") As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetDispCycle(waitReturn, cycleArray)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[傳回異常代號]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="errorCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrorCode(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef errorCode As String = "") As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetErrorCode(waitReturn, errorCode)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[異常清除]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetResetAlarm(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).SetResetAlarm(waitReturn)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
            Return False
        End Try
    End Function


    ''' <summary>[傳回閥體溫度]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="tempArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTemperature(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetTemperature(waitReturn, tempArray)
            End With
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@GetTemperature")
            Return False
        End Try
    End Function


    '20171010
    ''' <summary>[傳回開關狀態]</summary>
    ''' <param name="triggerNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <param name="tempArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSwitch(ByVal triggerNo As Integer, Optional ByVal waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean
        If mIsSimulationType = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
            gSyslog.Save("Simulation")
            Return True
        End If

        '[Note]:卡不存在
        If triggerNo < TBParameter.GetLowerBound(0) Or triggerNo > TBParameter.GetUpperBound(0) Then
            Return False
        End If
        '[Note]:資料對應有問題
        If TBParameter(triggerNo).ItemNo < 0 Or TBParameter(triggerNo).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With TBParameter(triggerNo)
                Return Items(.ItemNo).GetSwitch(waitReturn, tempArray)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@GetTemperature")
            Return False
        End Try
    End Function

#End Region

End Class

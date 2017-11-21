Imports ProjectCore

''' <summary>Valvecontroller組數</summary>
''' <remarks></remarks>
Public Enum enmValvecontrollerCount
    No1 = 0
    No2 = 1
    No3 = 2
    No4 = 3
    Max = 3
End Enum
''' <summary>
''' Valvecontroller類型
''' </summary>
''' <remarks></remarks>
Public Enum enmValvecontrollerType
    ''' <summary>虛擬</summary>
    ''' <remarks></remarks>
    Virtual = 0
    ''' <summary>PicoTouch</summary>
    ''' <remarks></remarks>
    PicoTouch = 1
End Enum

''' <summary>PicoTouch序列埠</summary>
''' <remarks></remarks>
Public Structure PicoTouchConncet
    ''' <summary>COM1,2,3...etc</summary>
    ''' <remarks></remarks>
    Public PortName As String
    ''' <summary>Baud Rate: 9600...etc</summary>
    ''' <remarks></remarks>
    Public BaudRate As String
End Structure

''' <summary>連線參數</summary>
''' <remarks></remarks>
Public Structure sValvecontrollerConnectionParameter
    ''' <summary>元件類型</summary>
    ''' <remarks></remarks>
    Public DeviceType As enmValvecontrollerType
    Public Function DeviceTypeToString() As String
        Select Case DeviceType
            Case enmValvecontrollerType.Virtual
                Return "Virtual"
            Case enmValvecontrollerType.PicoTouch
                Return "PicoTouch"
        End Select
        Return "Unknown"
    End Function
    ''' <summary>PicoTouch通訊設定</summary>
    ''' <remarks></remarks>
    Public PICOTouch As PicoTouchConncet
    ''' <summary>[通訊逾時之時間(ms)]</summary>
    ''' <remarks></remarks>
    Public TimeOutTimes As Integer
End Structure
''' <summary>Valvecontroller接點參數</summary>
''' <remarks></remarks>
Public Structure sValvecontrollerParameter
    ''' <summary>點位名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmValvecontrollerType
    ''' <summary>原始紀錄位置</summary>
    ''' <remarks></remarks>
    Public Address As String
    ''' <summary>位置</summary>
    ''' <remarks></remarks>
    Public IPAddress As Long
    ''' <summary>埠號</summary>
    ''' <remarks></remarks>
    Public Port As Long
    ''' <summary>點號</summary>
    ''' <remarks></remarks>
    Public AIIndex As Long
    ''' <summary>略過</summary>
    ''' <remarks></remarks>
    Public ByPass As Boolean
    ''' <summary>讀值</summary>
    ''' <remarks></remarks>
    Public Value As String
    ''' <summary>Item索引/絕對卡號</summary>
    ''' <remarks></remarks>
    Public Ticket As Integer

End Structure



''' <summary>
''' 外部以此類別來對底層操作
''' </summary>
''' <remarks></remarks>
Public Class CValveControllerCollection
    ''' <summary>[卡片初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsCardIntialOK As Boolean
        Get
            Return mIsCardIntialOK
        End Get
    End Property

    ''' <summary>連線參數設定</summary>
    ''' <remarks></remarks>
    Public ConnectionParameter(enmValvecontrollerCount.Max) As sValvecontrollerConnectionParameter

    ''' <summary>個別參數設定</summary>
    ''' <remarks></remarks>
    Public ChannelParameter(enmValvecontrollerCount.Max) As sValvecontrollerParameter

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

    ''' <summary>卡集合</summary>
    ''' <remarks></remarks>
    Public Items As New List(Of IValveControllerInterface)

    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cardParameters() As sValvecontrollerConnectionParameter) As Boolean
        Try
            Dim sTemp = ""
            Dim ErrMessage As String = ""
            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                Return True
            Else
                For i As Integer = cardParameters.GetLowerBound(0) To cardParameters.GetUpperBound(0) '對每一張卡
                    Select Case cardParameters(i).DeviceType '依卡型號做初始化
                        Case enmValvecontrollerType.Virtual '虛擬卡
                            Items.Add(New CVirtualValveController)
                            ChannelParameter(i).Ticket = i

                        Case enmValvecontrollerType.PicoTouch
                            Items.Add(New CPicoTouchcontroller)
                            ChannelParameter(i).Ticket = i
                            If Items(Items.Count - 1).Initial(cardParameters(i).PICOTouch.PortName, cardParameters(i).PICOTouch.BaudRate) = False Then
                                ErrMessage += "" & cardParameters(i).DeviceTypeToString & i.ToString & "初始化失敗." & vbCrLf
                            End If

                    End Select
                Next
                If ErrMessage <> "" Then
                    mIsCardIntialOK = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "DI Collection")
                    Return False
                Else
                    mIsCardIntialOK = True
                    Return True
                End If
            End If

        Catch ex As Exception
            mIsCardIntialOK = False
            MsgBox("初始化失敗" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Close
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Return False
    End Function

    ''' <summary>儲存連線參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveValveReaderConnection(ByVal fileName As String)
        For i As Integer = 0 To enmValvecontrollerCount.Max
            Dim strSection As String = "Valvecontroller" & (i + 1).ToString & "-Connection"
            With ConnectionParameter(i)
                SaveIniString(strSection, "PicoTouch-PortName", .PICOTouch.PortName, fileName)
                SaveIniString(strSection, "PicoTouch-BaudRate", .PICOTouch.BaudRate, fileName)
                SaveIniString(strSection, "DeviceType", CInt(.DeviceType), fileName)
                SaveIniString(strSection, "TimeOutTimes", CInt(.TimeOutTimes), fileName)
            End With
        Next
        Return True
    End Function
    ''' <summary>讀取連線參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadValveReaderConnection(ByVal fileName As String)
        For i As Integer = 0 To enmValvecontrollerCount.Max
            Dim strSection As String = "Valvecontroller" & (i + 1).ToString & "-Connection"
            With ConnectionParameter(i)
                .PICOTouch.PortName = ReadIniString(strSection, "PicoTouch-PortName", fileName, "COM1")
                .PICOTouch.BaudRate = ReadIniString(strSection, "PicoTouch-BaudRate", fileName, "115200")
                .DeviceType = Val(ReadIniString(strSection, "DeviceType", fileName, 0))
                .TimeOutTimes = CInt(ReadIniString(strSection, "TimeOutTimes", fileName, "500"))
            End With
        Next
        Return True
    End Function

    ''' <summary>
    ''' [TimeOut]
    ''' </summary>
    ''' <param name="iValvecontrolerIndex"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut(ByVal iValvecontrolerIndex As Integer) As Boolean
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            If CheckBound(iValvecontrolerIndex) = False Then
                Return enmCommandState.Failed
            End If
            Try
                With ChannelParameter(iValvecontrolerIndex)
                    If .ByPass = True Then '略過,傳回預設值
                        Return enmCommandState.Failed
                    End If
                    Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).IsTimeOut
                End With
            Catch ex As Exception
                Return enmCommandState.Failed
            End Try
        End Get
    End Property

    ''' <summary>
    ''' [忙碌中]
    ''' </summary>
    ''' <param name="iValvecontrolerIndex"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy(ByVal iValvecontrolerIndex As Integer) As Boolean
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return False
            End If

            If CheckBound(iValvecontrolerIndex) = False Then
                Return enmCommandState.Failed
            End If
            Try
                With ChannelParameter(iValvecontrolerIndex)
                    If .ByPass = True Then '略過,傳回預設值
                        Return enmCommandState.Failed
                    End If
                    Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).IsBusy
                End With
            Catch ex As Exception
                Return enmCommandState.Failed
            End Try
        End Get
    End Property

    ''' <summary>
    ''' 設定Timeout時間
    ''' </summary>
    ''' <param name="iValvecontrolerIndex"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeoutTimes(ByVal iValvecontrolerIndex As Integer) As Integer
        Get
            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return 0
            End If
            If CheckBound(iValvecontrolerIndex) = False Then
                Return enmCommandState.Failed
            End If
            Try
                With ChannelParameter(iValvecontrolerIndex)
                    Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).TimeoutTimer
                End With
            Catch ex As Exception
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
                MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CTriggerBoardCollection@Initial")
                Return 0
            End Try
        End Get

        Set(value As Integer)
            With ChannelParameter(iValvecontrolerIndex)
                Items(ChannelParameter(iValvecontrolerIndex).Ticket).TimeoutTimer = value
            End With
        End Set
    End Property


    ''' <summary>
    ''' 重置Alarm
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResetAlarm(ByVal iValvecontrolerIndex As Integer, Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ResetAlarmStatus()
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the open profile of the valve
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iSelect"> 1~6</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetOpenProfile(ByVal iValvecontrolerIndex As Integer, iSelect As Integer,
            Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetOpenProfile(iSelect, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the close profile of the valve
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器</param>
    ''' <param name="iSelect">1-6</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCloseProfile(ByVal iValvecontrolerIndex As Integer, iSelect As Integer,
     Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetCloseProfile(iSelect, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定Stroke電壓
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iStrokeValve">AAA</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetStrokeValve(ByVal iValvecontrolerIndex As Integer, iStrokeValve As Integer,
             Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetStrokeValve(iStrokeValve, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定Close電壓
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iCloseVoltage">AAAA</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCloseVoltage(ByVal iValvecontrolerIndex As Integer, iCloseVoltage As Integer,
            Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetCloseVoltage(iCloseVoltage, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定Open Time 
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iOpenTime">AAAA</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetOpenTime(ByVal iValvecontrolerIndex As Integer, iOpenTime As Integer, Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetOpenTime(iOpenTime, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve ON time(PULSE)
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iVavleOnTime"></param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValveOnTime(ByVal iValvecontrolerIndex As Integer, ByVal iVavleOnTime As Double, Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValveOnTime(iVavleOnTime, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the close (rise) time of the valve
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iCloseTime">AAAA us</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCloseTime(ByVal iValvecontrolerIndex As Integer, iCloseTime As Integer, Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetCloseTime(iCloseTime, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve OFF time(Where OFF time + ON time = Cycle)
    ''' </summary>
    ''' <param name="iVavleOffTime">
    '''  OFF time in ms
    ''' Time is entered as an ON/OFF time where ON = PULSE and Cycle = ON + OFF.ON and OFF times should be adjusted together to preserve the Cycle time setting.
    ''' </param>
    Public Function SetValveOffTime(ByVal iValvecontrolerIndex As Integer, ByVal iVavleOffTime As Double, Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValveOffTime(iVavleOffTime, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Sets the valve CYCLE time
    ''' </summary>
    ''' <param name="iCycleTime">
    '''  Cycle time in ms
    ''' Time is entered as an ON/OFF time where ON = PULSE and Cycle = ON + OFF.ON and OFF times should be adjusted together to preserve the Cycle time setting.
    ''' </param>
    Public Function SetValveCycleTime(ByVal iValvecontrolerIndex As Integer, ByVal iCycleTime As Double,
                                    Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValveCycleTime(iCycleTime, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定Cycle On/Off
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="bCycleOnOff">true:開 false:關</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCycleOnOff(ByVal iValvecontrolerIndex As Integer, bCycleOnOff As Boolean,
             Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetCycleOnOff(bCycleOnOff, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定加熱模式
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iHeatherMode"></param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetHeaterMode(ByVal iValvecontrolerIndex As Integer, iHeatherMode As Integer,
             Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetHeaterMode(iHeatherMode, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定溫度
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="dHeaterTemperature">DDD.D</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetHeaterTemperature(ByVal iValvecontrolerIndex As Integer, dHeaterTemperature As Double,
             Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetHeaterTemperature(dHeaterTemperature, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定DispenseCount
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iDispenseCount"></param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValveDispenseCount(ByVal iValvecontrolerIndex As Integer, iDispenseCount As Integer,
                                 Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValveDispenseCount(iDispenseCount, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 設定Valve Mode
    '''  </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="eModeType">1=Time 
    ''' 2=Purge
    ''' 3=Continuous
    ''' 5=Reads the current mode</param>
    ''' <param name="sResponse">回應值</param>
    ''' <param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValveMode(ByVal iValvecontrolerIndex As Integer, eModeType As enmValveModeType,
                                 Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValveMode(eModeType, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Set Plok
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="iPlokTime"></param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValvePlok(ByVal iValvecontrolerIndex As Integer, ByVal iPlokTime As Integer,
                                  Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValvePlok(iPlokTime, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' Valve Power On/Off
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="bValvePower"> true:On false:Off</param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetValvePower(ByVal iValvecontrolerIndex As Integer, ByVal bValvePower As Boolean,
                                  Optional ByRef sResponse As String = "", Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).SetValvePower(bValvePower, sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function



    ''' <summary>GetAlarmInfo</summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlarmInfo(ByVal iValvecontrolerIndex As Integer, Optional ByRef sResponse As String = "",
                                 Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).GetAlarmInfo(sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>GetDisplayInfo</summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDisplayInfo(ByVal iValvecontrolerIndex As Integer, Optional ByRef sResponse As String = "",
                                   Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).GetDisplayInfo(sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>取得Valve狀態</summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValveStatus(ByVal iValvecontrolerIndex As Integer, Optional ByRef sResponse As sPicoValveStatus = Nothing,
                                   Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).GetValveStatus(sResponse, bWaitResponse)
            End With

        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>取得加熱狀態</summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="sResponse">回應值</param>
    '''<param name="bWaitResponse">是否等回應</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHeaterStatus(ByVal iValvecontrolerIndex As Integer, Optional ByRef sResponse As sPicoValveHeaterStatus = Nothing,
                                   Optional ByVal bWaitResponse As Boolean = False) As enmCommandState
        If CheckBound(iValvecontrolerIndex) = False Then
            Return enmCommandState.Failed
        End If
        Try
            With ChannelParameter(iValvecontrolerIndex)
                If .ByPass = True Then '略過,傳回預設值
                    Return enmCommandState.Failed
                End If
                Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).GetHeaterStatus(sResponse, bWaitResponse)
            End With
        Catch ex As Exception
            Return enmCommandState.Failed
        End Try
    End Function

    ''' <summary>
    ''' 取得命令回應,AlarmInfo
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseAlarmInfo(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).AlarmInfo
    End Function

    ''' <summary>
    ''' 取得命令回應,OpenProfile
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseOpenProfile(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).OpenProfile
    End Function

    ''' <summary>
    ''' 取得命令回應,CloseProfile
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseCloseProfile(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).CloseProfile
    End Function

    ''' <summary>
    ''' 取得命令回應,CloseVoltage
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseCloseVoltage(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).CloseVoltage
    End Function

    ''' <summary>
    ''' 取得命令回應,Stroke
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseStroke(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).Stroke
    End Function

    ''' <summary>
    '''  取得命令回應,DisplayInfo
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseDisplayInfo(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).DisplayInfo
    End Function

    ''' <summary>
    ''' 取得命令回應,OpenTime
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseOpenTime(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).OpenTime
    End Function

    ''' <summary>
    ''' 取得命令回應,ValveOnTime
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValveOnTime(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValveOnTime
    End Function

    ''' <summary>
    ''' 取得命令回應,CloseTime
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseCloseTime(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).CloseTime
    End Function

    ''' <summary>
    '''  取得命令回應,ValveOffTime
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValveOffTime(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValveOffTime
    End Function

    ''' <summary>
    ''' 取得命令回應,HeaterMode
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseHeaterMode(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).HeaterMode
    End Function

    ''' <summary>
    ''' 取得命令回應,HeaterStatus
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseHeaterStatus(ByVal iValvecontrolerIndex As Integer) As sPicoValveHeaterStatus
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).HeaterStatus
    End Function

    ''' <summary>
    ''' 取得命令回應,HeaterTemperature
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseHeaterTemperature(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).HeaterTemperature
    End Function

    ''' <summary>
    ''' 取得命令回應,ResetAlarm
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseResetAlarm(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ResetAlarm
    End Function

    ''' <summary>
    ''' 取得命令回應,ValveCycleONOFFStatus
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseCycleOnOff(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValveCycleONOFFStatus
    End Function

    ''' <summary>
    ''' 取得命令回應,DispenseCount
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValveDispenseCount(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValveDispenseCount
    End Function

    ''' <summary>
    ''' 取得命令回應,ValveMode
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValveMode(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValveMode
    End Function

    ''' <summary>
    ''' 取得命令回應,ValvePlok
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValvePlok(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValvePlok
    End Function

    ''' <summary>
    ''' 取得命令回應,ValvePower
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValvePower(ByVal iValvecontrolerIndex As Integer) As sPicoValveCommandResponseData
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValvePower
    End Function

    ''' <summary>
    ''' 取得命令回應,ValveStatus
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommandResponseValveStatus(ByVal iValvecontrolerIndex As Integer) As sPicoValveStatus
        Return Items(ChannelParameter(iValvecontrolerIndex).Ticket).ValveStatus
    End Function


    ''' <summary>
    ''' CheckBound
    ''' </summary>
    ''' <param name="iIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckBound(ByVal iIndex As Integer) As Boolean
        If iIndex < ChannelParameter.GetLowerBound(0) Then
            Return False
        End If
        If iIndex > ChannelParameter.GetUpperBound(0) Then
            Return False
        End If
        If ChannelParameter(iIndex).Ticket < 0 Then
            Return False
        End If
        If ChannelParameter(iIndex).Ticket >= Items.Count Then
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' 註冊TextBox
    ''' </summary>
    ''' <param name="iValvecontrolerIndex">第幾台控制器 </param>
    ''' <param name="CommandResponse"></param>
    ''' <remarks></remarks>
    Public Sub RegCommandResponse(ByVal iValvecontrolerIndex As Integer, ByRef CommandResponse As Windows.Forms.TextBox)
        Items(ChannelParameter(iValvecontrolerIndex).Ticket).mCommandResponse = CommandResponse

    End Sub

End Class

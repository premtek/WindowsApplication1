Imports ProjectCore

''' <summary>FMCS接點參數</summary>
''' <remarks></remarks>
Public Structure sFMCSParameter
    ''' <summary>點位名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmBalanceType
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
    Public Bits As Long
    ''' <summary>略過</summary>
    ''' <remarks></remarks>
    Public ByPass As Boolean
    ''' <summary>讀值</summary>
    ''' <remarks></remarks>
    Public Value As String
    ''' <summary>Item索引</summary>
    ''' <remarks></remarks>
    Public ItemNo As Integer

End Structure

''' <summary>微量天平物件類別</summary>
''' <remarks></remarks>
Public Class CFMCSCollection
    ''' <summary>FMCS連線設定</summary>
    ''' <remarks></remarks>
    Public ConnectionParameter(enmValve.Max) As sFMCSConnectionParameter
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
    ''' <summary>卡集合</summary>
    ''' <remarks></remarks>
    Public Items As New List(Of IFMCS)
    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cardParameters() As sFMCSConnectionParameter) As Boolean
        Try
            Dim ErrMessage As String = ""

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                Return True
            End If

            For i As Integer = cardParameters.GetLowerBound(0) To cardParameters.GetUpperBound(0) '對每一張卡
                Select Case cardParameters(i).CardType '依卡型號做初始化
                    Case enmFMCSType.None '虛擬卡
                        Items.Add(New CVirtualFMCS)
                    Case enmFMCSType.FMCS, enmFMCSType.FMCS_P, enmFMCSType.FMCS_PI, enmFMCSType.FMCS_PII
                        Items.Add(New CFMCS)
                        Items(Items.Count - 1).ValveIndex = i
                        If Items(Items.Count - 1).Initial(cardParameters(i).PortName, cardParameters(i).BaudRate) = False Then
                            ErrMessage += "" & cardParameters(i).CardTypeToString & "(" & cardParameters(i).PortName & ") 初始化失敗."
                        End If

                End Select
            Next

            If ErrMessage <> "" Then '錯誤
                mIsCardIntialOK = False
                gSyslog.Save(gMsgHandler.GetMessage(Error_1017000), "Error_1017000", eMessageLevel.Error)
                gSyslog.Save("Eror Message: " & ErrMessage, , eMessageLevel.Error)
                MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "DI Collection")
                Return False
            End If

            gSyslog.Save(gMsgHandler.GetMessage(INFO_6017000)) '正常結束
            mIsCardIntialOK = True
            Return True



        Catch ex As Exception
            mIsCardIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1017000), "Error_1017000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox("初始化失敗" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
            Return False
        End Try
    End Function


    ''' <summary>資源釋放</summary>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Try
            Dim intI As Integer

            If mIsSimulationType = True Then
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                gSyslog.Save("Simulation")
                Return True
            End If

            If mIsCardIntialOK = True Then
                '[說明]:卡片關閉
                For intI = 0 To Items.Count - 1
                    Items(intI).Close()
                Next
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                Return True
            Else
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6015001), "INFO_6015001")
                'gSyslog.Save("DI Card Close OK(Initial Failed.).")
                Return True
            End If


        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015001), "Error_1015001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
            Return False
        End Try

    End Function
    Public Parameter(enmBalance.Max) As sFMCSParameter

    ''' <summary>要求讀取穩定值</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>成功: True, 失敗: False</returns>
    ''' <remarks></remarks>
    Public Function RecordStart(ByVal balanceIndex As Integer, ByVal intX As Integer, ByVal intY As Integer) As Boolean
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return False
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).RecordStart(intX, intY)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1017002), "Error_1017002", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    ''' <summary>要求讀取現在值</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>成功: True, 失敗: False</returns>
    ''' <remarks></remarks>
    Public Function RecordEnd(ByVal balanceIndex As Integer, ByVal intX As Integer, ByVal intY As Integer) As Boolean
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return False
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).RecordEnd(intX, intY)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015003), "Error_1015003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function



    '''<summary>天平是否忙碌中</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>是否忙碌 True: 是, False:否</returns>
    ''' <remarks>解除忙碌才可讀值</remarks>
    Public Function IsBusy(ByVal balanceIndex As Integer) As Boolean
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return False
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).IsBusy
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015003), "Error_1015003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    '''<summary>天平是否通訊逾時</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>是否逾時 True: 是, False:否</returns>
    ''' <remarks></remarks>
    Public Function IsTimeOut(ByVal balanceIndex As Integer) As Boolean
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return True
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return True
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return True
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return True
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).IsTimeOut
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015002), "Error_1015002", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return True
        End Try
    End Function

    '''<summary>天平是否通訊逾時</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>是否逾時 True: 是, False:否</returns>
    ''' <remarks></remarks>
    Public Function isPortOpen(ByVal balanceIndex As Integer) As Boolean
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return False
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).IsPortOpen
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015002), "Error_1015002", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    '''<summary>天平是否連線成功</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>是否連線成功 True: 是, False:否</returns>
    ''' <remarks></remarks>
    Public Function IsInitialOK(ByVal balanceIndex As Integer) As Boolean
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return False
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return False
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return False
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).IsInitialOK
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015002), "Error_1015002", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function

    ''' <summary>輸出平均流量</summary>
    ''' <remarks></remarks>
    Public Property OutputAvgFlow(ByVal fmcsIndex As Integer) As Double
        Get
            If fmcsIndex < Parameter.GetLowerBound(0) Then '參數項次OutOfRange
                Return Double.NaN
            End If
            If fmcsIndex > Parameter.GetUpperBound(0) Then '參數項次OutOfRange
                Return Double.NaN
            End If
            If Parameter(fmcsIndex).ItemNo < 0 Then '參數項次OutOfRange
                Return Double.NaN
            End If
            If Parameter(fmcsIndex).ItemNo > Items.Count - 1 Then '參數項次OutOfRange
                Return Double.NaN
            End If
            With Parameter(fmcsIndex)
                Return Items(Parameter(fmcsIndex).ItemNo).OutputAvgFlow
            End With
        End Get
        Set(value As Double)
            If fmcsIndex < Parameter.GetLowerBound(0) Then
                Return
            End If
            If fmcsIndex > Parameter.GetUpperBound(0) Then
                Return
            End If
            If Parameter(fmcsIndex).ItemNo < 0 Then
                Return
            End If
            If Parameter(fmcsIndex).ItemNo >= Items.Count Then
                Return
            End If
            With Parameter(fmcsIndex)
                Items(Parameter(fmcsIndex).ItemNo).OutputAvgFlow = value
            End With
        End Set
    End Property

    ''' <summary>輸出體積</summary>
    ''' <remarks></remarks>    
    Public Property OutputVolume(ByVal fmcsIndex As Integer) As Double
        Get
            If fmcsIndex < Parameter.GetLowerBound(0) Then '參數項次OutOfRange
                Return Double.NaN
            End If
            If fmcsIndex > Parameter.GetUpperBound(0) Then '參數項次OutOfRange
                Return Double.NaN
            End If
            If Parameter(fmcsIndex).ItemNo < 0 Then '參數項次OutOfRange
                Return Double.NaN
            End If
            If Parameter(fmcsIndex).ItemNo >= Items.Count Then '參數項次OutOfRange
                Return Double.NaN
            End If
            With Parameter(fmcsIndex)
                Return Items(Parameter(fmcsIndex).ItemNo).OutputVolume
            End With
        End Get
        Set(value As Double)
            If fmcsIndex < Parameter.GetLowerBound(0) Then
                Return
            End If
            If fmcsIndex > Parameter.GetUpperBound(0) Then
                Return
            End If
            If Parameter(fmcsIndex).ItemNo < 0 Then
                Return
            End If
            If Parameter(fmcsIndex).ItemNo >= Items.Count Then
                Return
            End If
            With Parameter(fmcsIndex)
                Items(Parameter(fmcsIndex).ItemNo).OutputVolume = value
            End With
        End Set
    End Property

    ''' <summary>儲存FMCS連線參數</summary>
    ''' <remarks></remarks>
    Sub SaveFMCSConnectionParameter(ByVal fileName As String)
        'Dim strFileName As String
        Dim strSection As String
        
        For mChNo As Integer = 0 To ConnectionParameter.Count - 1
            If ConnectionParameter.Count = 1 Then
                strSection = "FMCS"
            Else
                strSection = "FMCS" & (mChNo + 1).ToString()
            End If
            With ConnectionParameter(mChNo)
                Call SaveIniString(strSection, "FMCSType", .strFMCSType, fileName)
                Call SaveIniString(strSection, "FMCSPortName", .PortName, fileName)
            End With
        Next
    End Sub
    ''' <summary>讀取FMCS連線參數</summary>
    ''' <remarks></remarks>
    Sub LoadFMCSConnectionParameter(ByVal fileName As String)
        'Dim strFileName As String
        Dim strSection As String
        
        For mChNo As Integer = 0 To ConnectionParameter.Count - 1
            If ConnectionParameter.Count = 1 Then
                strSection = "FMCS"
            Else
                strSection = "FMCS" & (mChNo + 1).ToString()
            End If

            With ConnectionParameter(mChNo)
                .strFMCSType = ReadIniString(strSection, "FMCSType", fileName, "FMCS-P")
                .PortName = ReadIniString(strSection, "FMCSPortName", fileName, "COM2")
            End With
        Next
    End Sub

End Class

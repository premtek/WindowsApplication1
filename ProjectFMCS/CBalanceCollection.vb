Imports ProjectCore

''' <summary>天平接點參數</summary>
''' <remarks></remarks>
Public Structure sBalanceParameter
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
    Public Function Save(ByVal enmLaserNo As Integer, ByVal fileName As String) As Boolean
        Dim mSection As String = "Channel" & (enmLaserNo + 1).ToString
        SaveIniString(mSection, "Name", Name, fileName)
        SaveIniString(mSection, "CardType", CInt(CardType), fileName)
        SaveIniString(mSection, "Address", Address, fileName)
        SaveIniString(mSection, "IPAddress", IPAddress, fileName)
        SaveIniString(mSection, "Port", Port, fileName)
        SaveIniString(mSection, "ByPass", ByPass, fileName)
        SaveIniString(mSection, "Value", Value, fileName)
        SaveIniString(mSection, "ItemNo", ItemNo, fileName)
        Return True
    End Function
    Public Function Load(ByVal enmLaserNo As Integer, ByVal fileName As String) As Boolean
        Dim mSection As String = "Channel" & (enmLaserNo + 1).ToString
        Name = ReadIniString(mSection, "Name", fileName)
        CardType = Val(ReadIniString(mSection, "CardType", fileName))
        Address = ReadIniString(mSection, "Address", fileName)
        IPAddress = Val(ReadIniString(mSection, "IPAddress", fileName))
        Port = Val(ReadIniString(mSection, "Port", fileName))
        ByPass = Val(ReadIniString(mSection, "ByPass", fileName))
        Value = ReadIniString(mSection, "Value", fileName)
        ItemNo = Val(ReadIniString(mSection, "ItemNo", fileName))
        Return True
    End Function
End Structure

''' <summary>微量天平物件類別</summary>
''' <remarks></remarks>
Public Class CBalanceCollection
    ''' <summary>微量天平連線設定 (0):第一組天平 (1):第二組天平</summary>
    ''' <remarks></remarks>
    Public WeighingUnit(enmBalance.Max) As sBalanceConnectionParameter '[微量天平設定值]
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
    Public Items As New List(Of IBalance)
    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cardParameters() As sBalanceConnectionParameter) As Boolean
        Try
            Dim ErrMessage As String = ""

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                Return True
            End If

            For mCardNo As Integer = cardParameters.GetLowerBound(0) To cardParameters.GetUpperBound(0) '對每一張卡
                Select Case cardParameters(mCardNo).CardType '依卡型號做初始化
                    Case enmBalanceType.None '虛擬卡
                        Items.Add(New CVirtualBalance)
                    Case enmBalanceType.AD4212A100 'AND天平
                        Items.Add(New Cbalance)
                        If Items(Items.Count - 1).Initial(cardParameters(mCardNo).PortName, cardParameters(mCardNo).BaudRate, cardParameters(mCardNo).TimeoutTimer) = False Then
                            ErrMessage += "" & sBalanceConnectionParameter.CardTypeToString(cardParameters(mCardNo).CardType) & "(" & cardParameters(mCardNo).PortName & ") 初始化失敗."
                        End If
                    Case enmBalanceType.WZA215_LC 'Sartorius天平
                        Items.Add(New CBalanceSartorius)
                        If Items(Items.Count - 1).Initial(cardParameters(mCardNo).PortName, cardParameters(mCardNo).BaudRate, cardParameters(mCardNo).TimeoutTimer) = False Then
                            ErrMessage += "" & sBalanceConnectionParameter.CardTypeToString(cardParameters(mCardNo).CardType) & "(" & cardParameters(mCardNo).PortName & ") 初始化失敗."
                        End If
                End Select
            Next

            If ErrMessage <> "" Then '錯誤
                mIsCardIntialOK = False
                gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
                gSyslog.Save("Eror Message: " & ErrMessage, , eMessageLevel.Error)
                MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Balance Collection")
                Return False
            End If

            gSyslog.Save(gMsgHandler.GetMessage(INFO_6004000)) '正常結束
            mIsCardIntialOK = True
            Return True



        Catch ex As Exception
            mIsCardIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015000), "Error_1015000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox("初始化失敗" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CBalanceCollection@Initial")
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
    Public Parameter(enmBalance.Max) As sBalanceParameter
    ''' <summary>要求讀取穩定值</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>成功: True, 失敗: False</returns>
    ''' <remarks></remarks>
    Public Function RequestStableValue(ByVal balanceIndex As Integer, ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean
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
                Return Items(Parameter(balanceIndex).ItemNo).RequestStableValue(value, waitReturn)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015003), "Error_1015003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function

    ''' <summary>要求讀取現在值</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>成功: True, 失敗: False</returns>
    ''' <remarks></remarks>
    Public Function RequestCurrentValue(ByVal balanceIndex As Integer, ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean
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

                Return Items(Parameter(balanceIndex).ItemNo).RequestCurrentValue(value, waitReturn)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015003), "Error_1015003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    ''' <summary>天平歸零</summary>
    ''' <param name="balanceIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Rezero(ByVal balanceIndex As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
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
                Return Items(Parameter(balanceIndex).ItemNo).Rezero(waitReturn)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1015003), "Error_1015003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function

    ''' <summary>主機天平量測值歸0</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>主機天平量測值歸0</returns>
    ''' <remarks></remarks>
    Public Function ResetValue(ByVal balanceIndex As Integer) As Double
        Items(Parameter(balanceIndex).ItemNo).Reset()

        Return True

    End Function

    ''' <summary>取得天平量測值</summary>
    ''' <param name="balanceIndex">enmBalance索引</param>
    ''' <returns>天平量測值</returns>
    ''' <remarks></remarks>
    Public Function GetValue(ByVal balanceIndex As Integer) As Double
        If balanceIndex < Parameter.GetLowerBound(0) Then
            Return Double.NaN
        End If
        If balanceIndex > Parameter.GetUpperBound(0) Then
            Return Double.NaN
        End If
        If Parameter(balanceIndex).ItemNo < 0 Then
            Return Double.NaN
        End If
        If Parameter(balanceIndex).ItemNo >= Items.Count Then
            Return Double.NaN
        End If
        Try
            With Parameter(balanceIndex)
                Return Items(Parameter(balanceIndex).ItemNo).Result.Value
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014003), "Error_1014003", eMessageLevel.Error)
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
                Return Items(Parameter(balanceIndex).ItemNo).PortIsOpen
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

    ''' <summary>儲存天平參數</summary>
    ''' <remarks></remarks>
    Public Function SaveBalanceConnectionParameter(ByVal fileName As String) As Boolean
        Dim strSection As String

        For mBalanceNo As Integer = 0 To enmBalance.Max

            strSection = "ProductionWeighingUnit" & (mBalanceNo + 1).ToString()

            With WeighingUnit(mBalanceNo)
                Call SaveIniString(strSection, "PortName", .PortName, fileName)
                Call SaveIniString(strSection, "BaudRate", .BaudRate, fileName)
                Call SaveIniString(strSection, "CardType", .CardType, fileName)
                Call SaveIniString(strSection, "TimeoutTimer", .TimeoutTimer, fileName)

            End With
        Next
        Return True
    End Function
    ''' <summary>讀取天平參數</summary>
    ''' <remarks></remarks>
    Public Function LoadBalanceConnectionParameter(ByVal fileName As String) As Boolean
        Dim strSection As String

        '--- Soni + 2014.10.22 微量天平設定值 ---

        For mBalanceNo As Integer = 0 To enmBalance.Max

            strSection = "ProductionWeighingUnit" & (mBalanceNo + 1).ToString()

            With WeighingUnit(mBalanceNo)
                .PortName = ReadIniString(strSection, "PortName", fileName, 2)
                .BaudRate = ReadIniString(strSection, "BaudRate", fileName, 2400)
                .CardType = ReadIniString(strSection, "CardType", fileName, 0)
                .TimeoutTimer = ReadIniString(strSection, "TimeoutTimer", fileName, 5000) 'Jeffadd Timer
            End With
        Next

        '--- Soni + 2014.10.22 微量天平設定值 ---
        Return True
    End Function

End Class

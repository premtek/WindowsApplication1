Imports ProjectCore

''' <summary>AO卡型號</summary>
''' <remarks></remarks>
Public Enum enmAOCardType
    None =0
    ''' <summary>8點類比輸出</summary>
    ''' <remarks></remarks>
    PCI_1723 = 1
End Enum
''' <summary>AO卡參數</summary>
''' <remarks></remarks>
Public Structure sAOCardParameter
    ''' <summary>卡型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmAOCardType
    ''' <summary>敘述</summary>
    ''' <remarks></remarks>
    Public DeviceDescreiption As String
    ''' <summary>卡號</summary>
    ''' <remarks></remarks>
    Public CardID As Integer
    Public Function Load(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        CardID = Val(ReadIniString(strSection, "AOCard-" & cardNo & "-CardID", fileName, ""))
        CardType = Val(ReadIniString(strSection, "AOCard-" & cardNo & "-CardType", fileName, enmAOCardType.PCI_1723))
        DeviceDescreiption = ReadIniString(strSection, "AOCard-" & cardNo & "-Description", fileName, "")
        gSyslog.Save("AO-Card" & cardNo & ":" & vbTab & "CardID: " & CardID & " CardType: " & CardType.ToString() & " Desc: " & DeviceDescreiption)
        Return True
    End Function
    Public Function Save(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        Call SaveIniString(strSection, "AOCard-" & cardNo & "-CardID", CardID, fileName)
        Call SaveIniString(strSection, "AOCard-" & cardNo & "-CardType", CardType, fileName)
        Call SaveIniString(strSection, "AOCard-" & cardNo & "-Description", DeviceDescreiption, fileName)
        Return True
    End Function
End Structure
''' <summary>AO埠參數</summary>
''' <remarks></remarks>
Public Structure sAOParameter
    ''' <summary>點位名稱(所有語系)</summary>
    ''' <remarks></remarks>
    Public FullName As String
    ''' <summary>點位名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmAOCardType
    ''' <summary>原始紀錄位置</summary>
    ''' <remarks></remarks>
    Public Address As String
    ''' <summary>IP位置</summary>
    ''' <remarks></remarks>
    Public IPAddress As Long
    ''' <summary>埠號</summary>
    ''' <remarks></remarks>
    Public Port As Long
    ''' <summary>略過</summary>
    ''' <remarks></remarks>
    Public ByPass As Boolean
    ''' <summary>讀值</summary>
    ''' <remarks></remarks>
    Public Value As Boolean
    ''' <summary>最大顯示值</summary>
    ''' <remarks></remarks>
    Public MaxValue As Double
    ''' <summary>最小顯示值</summary>
    ''' <remarks></remarks>
    Public MinValue As Double
    ''' <summary>最大電壓值</summary>
    ''' <remarks></remarks>
    Public MaxVoltage As Double
    ''' <summary>最小電壓值</summary>
    ''' <remarks></remarks>
    Public MinVoltage As Double
    ''' <summary>顯示單位</summary>
    ''' <remarks></remarks>
    Public UserUnit As String
End Structure

''' <summary>對流程固定AO介面</summary>
''' <remarks></remarks>
Public Class CAOCollection
    ''' <summary>AO卡連線設定集合</summary>
    ''' <remarks></remarks>
    Public Cards As New CAOCards
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
    Public Items As New List(Of IAOInterface)

    ''' <summary>總點數</summary>
    ''' <remarks></remarks>
    Dim TotalBits As Integer = 31 '預設值

    ''' <summary>[AO_人機設定值]</summary>
    ''' <remarks></remarks>
    Public Value(100) As Double

    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cards As List(Of sAOCardParameter)) As Boolean
        Try
            Dim ErrMessage As String = ""

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6008000), "INFO_6008000")
                Return True
            Else
                TotalBits = 0
                For mCardNo As Integer = 0 To cards.Count - 1 '對每一張卡
                    Select Case cards(mCardNo).CardType '依卡型號做初始化
                        Case enmAOCardType.None '虛擬卡
                            Items.Add(New CAOVirtual)
                        Case enmAOCardType.PCI_1723
                            Items.Add(New CAO_PCI_1723)
                            If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
                                ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
                            End If
                    End Select
                    TotalBits += Items(Items.Count - 1).PortPerCard  '計算總埠數
                Next

                If ErrMessage <> "" Then
                    mIsCardIntialOK = False
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1008000), "Error_1008000", eMessageLevel.Error)
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAOCollection@Initial")
                    Return False
                Else
                    mIsCardIntialOK = True
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6008000), "INFO_6008000")
                    Return True
                End If
            End If

        Catch ex As Exception
            mIsCardIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1008000), "Error_1008000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1008000) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAOCollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>[資源釋放]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Try
            Dim intI As Integer

            If mIsSimulationType = True Then
                Return True
            Else
                If mIsCardIntialOK = True Then
                    '[說明]:卡片關閉
                    For intI = 0 To Items.Count - 1
                        Items(intI).Close()
                    Next
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6008001), "INFO_6008001")
                    Return True
                Else
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6008001), "INFO_6008001")
                    Return True
                End If
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1008001), "Error_1008001", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1008001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAOCollection@Initial")
            Return False
        End Try

    End Function

    ''' <summary>AO接點參數</summary>
    ''' <remarks></remarks>
    Public AOParameter(100) As sAOParameter

    'Public gdblAO(glngDigitalIP, glngAIAnalogyPort) As Double
    ''' <summary>[AO_電壓輸出 0~10V給卡片]</summary>
    ''' <remarks></remarks>
    Public gdblAOVoltageOutput(100) As Double
    ''' <summary>
    ''' AO_SetVoltage
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AO_SetVoltage() As Boolean

        Dim intI As Integer

        If mIsSimulationType = True Then
            Return True
        End If

        If mIsCardIntialOK <> True Then
            Return True
        End If
        If Items.Count = 0 Then
            Return True
        End If
        For intCard As Integer = 0 To Items.Count - 1

            '[說明]:AO斜率運算
            For intI = 0 To 7
                'gdblAOVoltageOutput(intI) = ((AOParameter(intI).MinVoltage * AOParameter(intI).MaxValue) - (AOParameter(intI).MinVoltage * gdblAOHMISET(intI)) + (AOParameter(intI).MaxVoltage * gdblAOHMISET(intI)) - (AOParameter(intI).MaxVoltage * AOParameter(intI).MinValue)) / (AOParameter(intI).MaxValue - AOParameter(intI).MinValue) '斜率運算
                'VOut =(Vmin*Umax - Vmin*Uin+Vmax*Uin-Vmax*Umin)/(Umax-Umin)

                With AOParameter(intI)
                    'Vout =Vmin+(Vmax-Vmin) * (Uin-Umin)/(Umax-Umin)
                    gdblAOVoltageOutput(intI) = .MinVoltage + (.MaxVoltage - .MinVoltage) * (Value(intI) - .MinValue) / (.MaxValue - .MinValue) 'Soni / 2015.06.25 原斜率公式錯誤修正
                End With


            Next
        Next

        For intcard As Integer = 0 To Items.Count - 1
            Items(intcard).Write(0, Items(intcard).PortPerCard - 1, gdblAOVoltageOutput)
        Next
        Return True



    End Function



    Public Function Save(ByVal strFileName As String) As Boolean
        Dim intI As Long
        Dim strSection = "CONFIGURATION"
        For intI = 0 To 7 '紀錄
            '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
            Call SaveIniString(strSection, "AO-" & intI & "-Name", AOParameter(intI).FullName, strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-CardType", CInt(AOParameter(intI).CardType), strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-Address", AOParameter(intI).Address, strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-ByPass", CInt(AOParameter(intI).ByPass), strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-UserUnit", AOParameter(intI).UserUnit, strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-Umin", AOParameter(intI).MinValue, strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-Umax", AOParameter(intI).MaxValue, strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-Vmin", AOParameter(intI).MinVoltage, strFileName)
            Call SaveIniString(strSection, "AO-" & intI & "-Vmax", AOParameter(intI).MaxVoltage, strFileName)
            gSyslog.Save("AO-" & Format(intI, "000") & " Address: " & AOParameter(intI).Address & " CardType: " & AOParameter(intI).CardType.ToString() & " ByPass: " & AOParameter(intI).ByPass & " Unit: " & AOParameter(intI).UserUnit & " MinValue: " & AOParameter(intI).MinValue & " MaxValue: " & AOParameter(intI).MaxValue & " MinVoltage: " & AOParameter(intI).MinVoltage & " MaxVoltage: " & AOParameter(intI).MaxVoltage & " Name: " & AOParameter(intI).Name)
        Next
        Return True

    End Function

    Public Function Load(ByVal strFileName As String) As Boolean
        Try
            Dim strData As String
            Dim strBuffer As String
            Dim strAOAddressBuffer() As String
            strData = 1000
            strBuffer = 6
            Dim strSection = "CONFIGURATION"
            For intI = 0 To 7
                With AOParameter(intI)
                    '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                    AOParameter(intI).Name = ReadIniString(strSection, "AO-" & intI & "-Name", strFileName, "")
                    AOParameter(intI).FullName = ReadIniString(strSection, "AO-" & intI & "-Name", strFileName, "")
                    AOParameter(intI).CardType = Val(ReadIniString(strSection, "AO-" & intI & "-CardType", strFileName, ""))
                    AOParameter(intI).ByPass = Val(ReadIniString(strSection, "AO-" & intI & "-ByPass", strFileName, ""))
                    AOParameter(intI).UserUnit = ReadIniString(strSection, "AO-" & intI & "-UserUnit", strFileName, "")
                    AOParameter(intI).MinValue = Val(ReadIniString(strSection, "AO-" & intI & "-Umin", strFileName, ""))
                    AOParameter(intI).MaxValue = Val(ReadIniString(strSection, "AO-" & intI & "-Umax", strFileName, ""))
                    AOParameter(intI).MinVoltage = Val(ReadIniString(strSection, "AO-" & intI & "-Vmin", strFileName, ""))
                    AOParameter(intI).MaxVoltage = Val(ReadIniString(strSection, "AO-" & intI & "-Vmax", strFileName, ""))
                    AOParameter(intI).Address = ReadIniString(strSection, "AO-" & intI & "-Address", strFileName, "")

                    If AOParameter(intI).Address.Contains("-") Then
                        strAOAddressBuffer = AOParameter(intI).Address.Split("-")
                        AOParameter(intI).IPAddress = Val(strAOAddressBuffer(0))
                        AOParameter(intI).Port = Convert.ToInt16(strAOAddressBuffer(1).Substring(0, 2), 16)
                    End If
                    gSyslog.Save("AO-" & Format(intI, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & .ByPass & " Unit: " & .UserUnit & " MinValue: " & .MinValue & " MaxValue: " & .MaxValue & " MinVoltage: " & .MinVoltage & " MaxVoltage: " & .MaxVoltage & " Name: " & .Name)
                End With
            Next
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002014), "Error_1002014", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002014) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "MIO@ReadIOSET_AO")
            Return False
        End Try
    End Function
End Class
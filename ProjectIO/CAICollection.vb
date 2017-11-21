Imports ProjectCore
''' <summary>AI卡型號</summary>
''' <remarks></remarks>
Public Enum enmAICardType
    None = 0
    ''' <summary>
    ''' 16點單端或8點差動類比輸入
    ''' </summary>
    ''' <remarks></remarks>
    PCI_1710 = 1
End Enum
''' <summary>AI卡連線參數</summary>
''' <remarks></remarks>
Public Structure sAICardParameter
    ''' <summary>卡型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmAICardType
    ''' <summary>敘述</summary>
    ''' <remarks></remarks>
    Public DeviceDescreiption As String
    ''' <summary>卡號</summary>
    ''' <remarks></remarks>
    Public CardID As Integer
    Public Function Load(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        With Me
            .CardID = Val(ReadIniString(strSection, "AICard-" & cardNo & "-CardID", fileName, ""))
            .CardType = Val(ReadIniString(strSection, "AICard-" & cardNo & "-CardType", fileName, enmAICardType.PCI_1710))
            .DeviceDescreiption = ReadIniString(strSection, "AICard-" & cardNo & "-Description", fileName, "")
            gSyslog.Save("AI-Card" & cardNo & ":" & vbTab & "CardID: " & .CardID & " CardType: " & .CardType.ToString() & " Desc: " & .DeviceDescreiption)
        End With
        Return True
    End Function
    Public Function Save(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        Call SaveIniString(strSection, "AICard-" & cardNo & "-CardID", CardID, fileName)
        Call SaveIniString(strSection, "AICard-" & cardNo & "-CardType", CardType, fileName)
        Call SaveIniString(strSection, "AICard-" & cardNo & "-Description", DeviceDescreiption, fileName)
        Return True
    End Function
End Structure
''' <summary>AI埠參數</summary>
''' <remarks></remarks>
Public Structure sAIParameter
    ''' <summary>點位名稱(所有語系)</summary>
    ''' <remarks></remarks>
    Public FullName As String
    ''' <summary>點位名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>型號(無實際用途)</summary>
    ''' <remarks></remarks>
    Public CardType As enmAICardType
    ''' <summary>原始紀錄位置</summary>
    ''' <remarks></remarks>
    Public Address As String
    ''' <summary>IP位置(卡號)</summary>
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

Public Class CAICollection
    ''' <summary>
    ''' AI卡連線設定集合
    ''' </summary>
    ''' <remarks></remarks>
    Public Cards As New CAICards
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
    Public Items As New List(Of IAIInterface)

    ''' <summary>總點數</summary>
    ''' <remarks></remarks>
    Dim TotalBits As Integer = 31 '預設值

    ''' <summary>[初始化]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal cards As List(Of sAICardParameter)) As Boolean
        Try
            Dim ErrMessage As String = ""

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6007000), "INFO_6007000")
                gSyslog.Save("(Simulation)")
                Return True
            Else
                TotalBits = 0
                For mCardNo As Integer = 0 To cards.Count - 1 '對每一張卡
                    Select Case cards(mCardNo).CardType '依卡型號做初始化
                        Case enmAICardType.None '虛擬卡
                            Items.Add(New CAIVirtual)
                        Case enmAICardType.PCI_1710
                            Items.Add(New CAI_PCI_1710)
                            If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
                                ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
                            End If
                    End Select
                    TotalBits += Items(Items.Count - 1).PortPerCard  '計算總埠數

                Next
                If Items.Count > 0 Then
                    ReDim gdblAI(Items.Count - 1, Items(0).PortPerCard)
                End If


                If ErrMessage <> "" Then
                    mIsCardIntialOK = False
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1007000), "Error_1007000", eMessageLevel.Error)
                    gSyslog.Save("Error Message: " & ErrMessage, , eMessageLevel.Error)
                    MsgBox(gMsgHandler.GetMessage(Error_1007000) & ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAICollection@Initial")
                    Return False
                Else
                    mIsCardIntialOK = True
                    Return True
                End If
            End If

        Catch ex As Exception
            mIsCardIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1007000), "Error_1007000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1007000) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAICollection@Initial")
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
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6007001), "INFO_6007001")
                gSyslog.Save(" (Simulation)")
                Return True
            Else
                If mIsCardIntialOK = True Then
                    '[說明]:卡片關閉
                    For intI = 0 To Items.Count - 1
                        Items(intI).Close()
                    Next
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6007001), "INFO_6007001")
                    Return True
                Else
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6007001), "INFO_6007001")
                    Return True
                End If
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1007001), "Error_1007001", eMessageLevel.Error)
            gSyslog.Save("AIException Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1007001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAICollection@Initial")
            Return False
        End Try


    End Function

    ''' <summary>AI接點參數</summary>
    ''' <remarks></remarks>
    Public AIParameter(100) As sAIParameter
    ''' <summary>原始AI輸入</summary>
    ''' <remarks></remarks>
    Dim gdblAI(,) As Double
    ''' <summary>[AI_電壓輸入 0~10V給卡片]</summary>
    ''' <remarks></remarks>
    Dim gdblAIVoltageInput(100) As Double

    ''' <summary>[AI_人機顯示值]</summary>
    ''' <remarks></remarks>
    Public Value(100) As Double

    Public Enum AIUpdateMode
        ''' <summary>更新時讀取批次值做平均</summary>
        ''' <remarks></remarks>
        Average = 0
        ''' <summary>更新時讀取單次值作移動平均</summary>
        ''' <remarks></remarks>
        MoveAverage = 1
        ''' <summary>更新時,讀取原始資料. 不做任何處理. (最快)</summary>
        ''' <remarks></remarks>
        Origin = 2
    End Enum


    ''' <summary>[AI_GetVoltage]</summary>
    ''' <param name="blnUpdateAI">[是否要強制等待更新數值]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AI_GetVoltage(Optional ByVal blnUpdateAI As AIUpdateMode = AIUpdateMode.MoveAverage) As Boolean

        If mIsSimulationType = True Then '模擬不取值
            Return True
        End If

        If mIsCardIntialOK <> True Then '初始化失敗不取值
            Return True
        End If

        If Items.Count = 0 Then '無卡例外
            Return True
        End If

        '[說明]:一秒鐘只能更新10萬次(硬體限制)
        '       1ms-->100次

        Dim mCardNo As Long
        Dim mPortNo As Long
        Dim mRoundNo As Long

        Dim dblAIValue(0, Items(0).PortPerCard - 1) As Double
        Dim Status As Automation.BDaq.ErrorCode             '[回傳狀態]

        Dim dblAdvantech_Voltage(100) As Double

        Static intIndex As Integer = 0
        Static dblBufferValue(Items(0).PortPerCard - 1, 63) As Double

        Const intUpdateRate As Integer = 64

        For mCardNo = 0 To Items.Count - 1   '[第幾張卡]
            Select Case blnUpdateAI
                Case AIUpdateMode.Average 'If blnUpdateAI = True Then
                    'Parallel.For(0, intUpdateRate, Sub(i)
                    '                                   Status = Items(lngI).Read(0, Items(lngI).PortPerCard, dblAdvantech_Voltage)
                    '                                   If Status <> Automation.BDaq.ErrorCode.Success Then
                    '                                   Else
                    '                                       For lngJ = 0 To Items(lngI).PortPerCard - 1 '[數值累加]
                    '                                           dblAIValue(lngI, lngJ) = dblAIValue(lngI, lngJ) + dblAdvantech_Voltage(lngJ)
                    '                                           dblBufferValue(lngJ, i) = dblAdvantech_Voltage(lngJ)
                    '                                       Next
                    '                                   End If
                    '                               End Sub)

                    For mRoundNo = 0 To intUpdateRate - 1 '[更新幾次]
                        Status = Items(mCardNo).Read(0, Items(mCardNo).PortPerCard, dblAdvantech_Voltage)
                        If Status <> Automation.BDaq.ErrorCode.Success Then
                        Else
                            For mPortNo = 0 To Items(mCardNo).PortPerCard - 1 '[數值累加]
                                dblAIValue(mCardNo, mPortNo) = dblAIValue(mCardNo, mPortNo) + dblAdvantech_Voltage(mPortNo)
                                dblBufferValue(mPortNo, mRoundNo) = dblAdvantech_Voltage(mPortNo)
                            Next
                        End If
                    Next

                    For mPortNo = 0 To Items(mCardNo).PortPerCard - 1
                        '[說明]:數值平均
                        gdblAI(mCardNo, mPortNo) = dblAIValue(mCardNo, mPortNo) / intUpdateRate

                        '[說明]:轉換到數值顯示
                        gdblAIVoltageInput(mPortNo) = gdblAI(AIParameter(mPortNo).IPAddress, AIParameter(mPortNo).Port) 'AI_人機顯示值

                        '[說明]:斜率換算
                        '[說明]:取值完要經過斜率換算後才是真正用的數值
                        'gdblAIHMIOutput(lngJ) = ((AIParameter(lngJ).MinVoltage * AIParameter(lngJ).MaxValue) - (gdblAIVoltageInput(lngJ) * AIParameter(lngJ).MaxValue) - (AIParameter(lngJ).MaxVoltage * AIParameter(lngJ).MinValue) + (gdblAIVoltageInput(lngJ) * AIParameter(lngJ).MinValue)) / (AIParameter(lngJ).MinVoltage - AIParameter(lngJ).MaxVoltage) '斜率運算
                        'Uout = (Vmin*Umax -Vin*Umax -Vmax*Umin +Vin*Umin)/(Vmin-Vmax) 公式錯誤
                        With AIParameter(mPortNo)
                            'gdblAIHMIOutput(lngJ) = ((.MinVoltage * .MaxValue) - (gdblAIVoltageInput(lngJ) * .MaxValue) - (.MaxVoltage * .MinValue) + (gdblAIVoltageInput(lngJ) * .MinValue)) / (.MinVoltage - .MaxVoltage) '斜率運算
                            'Soni / 2015.06.25 公式修正
                            'Uout =Umin+ (Umax-Umin)*(Vin-Vmin)/(Vmax-Vmin)
                            Value(mPortNo) = .MinValue + (.MaxValue - .MinValue) * (gdblAIVoltageInput(mPortNo) - .MinVoltage) / (.MaxVoltage - .MinVoltage)
                        End With
                    Next
                    intIndex = 0
                Case AIUpdateMode.MoveAverage 'blnUpdateAI = False Then
                    '[說明]:數值更新
                    Status = Items(mCardNo).Read(0, Items(mCardNo).PortPerCard, dblAdvantech_Voltage)
                    If Status <> Automation.BDaq.ErrorCode.Success Then
                    Else
                        For mPortNo = 0 To Items(mCardNo).PortPerCard - 1
                            dblBufferValue(mPortNo, intIndex) = dblAdvantech_Voltage(mPortNo)
                        Next

                        If intIndex >= intUpdateRate - 1 Then
                            intIndex = 0
                        Else
                            intIndex = intIndex + 1
                        End If
                    End If

                    For mPortNo = 0 To Items(mCardNo).PortPerCard - 1
                        '[說明]:數值累加，但此作法會有數值不即使之現象，此數值為過往的數值的平均值
                        '[說明]:數值統計
                        For mRoundNo = 0 To intUpdateRate - 1
                            dblAIValue(mCardNo, mPortNo) = dblAIValue(mCardNo, mPortNo) + dblBufferValue(mPortNo, mRoundNo)
                        Next

                        '[說明]:數值平均
                        gdblAI(mCardNo, mPortNo) = dblAIValue(mCardNo, mPortNo) / intUpdateRate

                        '[說明]:轉換到數值顯示
                        gdblAIVoltageInput(mPortNo) = gdblAI(AIParameter(mPortNo).IPAddress, AIParameter(mPortNo).Port) 'AI_人機顯示值

                        '[說明]:斜率換算
                        '[說明]:取值完要經過斜率換算後才是真正用的數值
                        'gdblAIHMIOutput(lngJ) = ((AIParameter(lngJ).MinVoltage * AIParameter(lngJ).MaxValue) - (gdblAIVoltageInput(lngJ) * AIParameter(lngJ).MaxValue) - (AIParameter(lngJ).MaxVoltage * AIParameter(lngJ).MinValue) + (gdblAIVoltageInput(lngJ) * AIParameter(lngJ).MinValue)) / (AIParameter(lngJ).MinVoltage - AIParameter(lngJ).MaxVoltage) '斜率運算
                        'Uout = (Vmin*Umax -Vin*Umax -Vmax*Umin +Vin*Umin)/(Vmin-Vmax) 公式錯誤
                        With AIParameter(mPortNo)
                            'gdblAIHMIOutput(lngJ) = ((.MinVoltage * .MaxValue) - (gdblAIVoltageInput(lngJ) * .MaxValue) - (.MaxVoltage * .MinValue) + (gdblAIVoltageInput(lngJ) * .MinValue)) / (.MinVoltage - .MaxVoltage) '斜率運算
                            'Soni / 2015.06.25 公式修正
                            'Uout =Umin+ (Umax-Umin)*(Vin-Vmin)/(Vmax-Vmin)
                            Value(mPortNo) = .MinValue + (.MaxValue - .MinValue) * (gdblAIVoltageInput(mPortNo) - .MinVoltage) / (.MaxVoltage - .MinVoltage)
                        End With
                    Next
                Case AIUpdateMode.Origin
                    '[說明]:數值更新
                    Status = Items(mCardNo).Read(0, Items(mCardNo).PortPerCard, dblAdvantech_Voltage)
                    If Status <> Automation.BDaq.ErrorCode.Success Then
                    Else
                        For mPortNo = 0 To Items(mCardNo).PortPerCard - 1
                            dblBufferValue(mPortNo, 0) = dblAdvantech_Voltage(mPortNo)
                        Next
                    End If

                    For mPortNo = 0 To Items(mCardNo).PortPerCard - 1
                        '[說明]:數值累加，但此作法會有數值不即使之現象，此數值為過往的數值的平均值
                        '[說明]:數值統計
                        gdblAI(mCardNo, mPortNo) = dblBufferValue(mPortNo, 0)

                        ''[說明]:數值平均
                        'gdblAI(lngI, lngJ) = dblAIValue(lngI, lngJ) / intUpdateRate

                        '[說明]:轉換到數值顯示
                        gdblAIVoltageInput(mPortNo) = gdblAI(AIParameter(mPortNo).IPAddress, AIParameter(mPortNo).Port) 'AI_人機顯示值

                        '[說明]:斜率換算
                        '[說明]:取值完要經過斜率換算後才是真正用的數值
                        'gdblAIHMIOutput(lngJ) = ((AIParameter(lngJ).MinVoltage * AIParameter(lngJ).MaxValue) - (gdblAIVoltageInput(lngJ) * AIParameter(lngJ).MaxValue) - (AIParameter(lngJ).MaxVoltage * AIParameter(lngJ).MinValue) + (gdblAIVoltageInput(lngJ) * AIParameter(lngJ).MinValue)) / (AIParameter(lngJ).MinVoltage - AIParameter(lngJ).MaxVoltage) '斜率運算
                        'Uout = (Vmin*Umax -Vin*Umax -Vmax*Umin +Vin*Umin)/(Vmin-Vmax) 公式錯誤
                        With AIParameter(mPortNo)
                            'gdblAIHMIOutput(lngJ) = ((.MinVoltage * .MaxValue) - (gdblAIVoltageInput(lngJ) * .MaxValue) - (.MaxVoltage * .MinValue) + (gdblAIVoltageInput(lngJ) * .MinValue)) / (.MinVoltage - .MaxVoltage) '斜率運算
                            'Soni / 2015.06.25 公式修正
                            'Uout =Umin+ (Umax-Umin)*(Vin-Vmin)/(Vmax-Vmin)
                            Value(mPortNo) = .MinValue + (.MaxValue - .MinValue) * (gdblAIVoltageInput(mPortNo) - .MinVoltage) / (.MaxVoltage - .MinVoltage)
                        End With
                    Next
            End Select

        Next
        Return True


    End Function



    Public Function Load(ByVal strFileName As String) As Boolean
        Try
            Dim strData As String
            Dim strBuffer As String
            Dim strAIAddressBuffer() As String
            strData = 100
            strBuffer = 6
            Dim strSection = "CONFIGURATION"
            gSyslog.Save("AI Parameter Loaded as below:")
            For intI = 0 To 15
                With AIParameter(intI)
                    '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                    AIParameter(intI).Name = ReadIniString(strSection, "AI-" & intI & "-Name", strFileName, "")
                    AIParameter(intI).FullName = ReadIniString(strSection, "AI-" & intI & "-Name", strFileName, "")
                    AIParameter(intI).CardType = Val(ReadIniString(strSection, "AI-" & intI & "-CardType", strFileName, ""))
                    AIParameter(intI).ByPass = Val(ReadIniString(strSection, "AI-" & intI & "-ByPass", strFileName, ""))
                    AIParameter(intI).MinValue = Val(ReadIniString(strSection, "AI-" & intI & "-Umin", strFileName, ""))
                    AIParameter(intI).MaxValue = Val(ReadIniString(strSection, "AI-" & intI & "-Umax", strFileName, ""))
                    AIParameter(intI).MinVoltage = Val(ReadIniString(strSection, "AI-" & intI & "-Vmin", strFileName, ""))
                    AIParameter(intI).MaxVoltage = Val(ReadIniString(strSection, "AI-" & intI & "-Vmax", strFileName, ""))
                    AIParameter(intI).UserUnit = ReadIniString(strSection, "AI-" & intI & "-UserUnit", strFileName, "")
                    AIParameter(intI).Address = ReadIniString(strSection, "AI-" & intI & "-Address", strFileName, "")

                    If AIParameter(intI).Address.Contains("-") Then
                        strAIAddressBuffer = AIParameter(intI).Address.Split("-")
                        AIParameter(intI).IPAddress = Val(strAIAddressBuffer(0))
                        AIParameter(intI).Port = Convert.ToInt16(strAIAddressBuffer(1).Substring(0, 2), 16)
                    End If
                    gSyslog.Save("AI-" & Format(intI, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & .ByPass & " Unit: " & .UserUnit & " MinValue: " & .MinValue & " MaxValue: " & .MaxValue & " MinVoltage: " & .MinVoltage & " MaxVoltage: " & .MaxVoltage & " Name: " & .Name)
                End With
            Next
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002012), "Error_1002012", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002012) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "MIO@ReadIOSET_AI")
            Return False
        End Try

    End Function

    Public Function SaveAI(ByVal strFileName As String) As Boolean
        Dim strSection = "CONFIGURATION"
        gSyslog.Save("AI Parameter Saved as below:")
        For intI = 0 To 15
            With AIParameter(intI)
                '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                Call SaveIniString(strSection, "AI-" & intI & "-Name", AIParameter(intI).FullName, strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-CardType", CInt(AIParameter(intI).CardType), strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-Address", AIParameter(intI).Address, strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-ByPass", CInt(AIParameter(intI).ByPass), strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-Umin", AIParameter(intI).MinValue, strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-Umax", AIParameter(intI).MaxValue, strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-Vmin", AIParameter(intI).MinVoltage, strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-Vmax", AIParameter(intI).MaxVoltage, strFileName)
                Call SaveIniString(strSection, "AI-" & intI & "-UserUnit", AIParameter(intI).UserUnit, strFileName)
                gSyslog.Save("AI-" & Format(intI, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & .ByPass & " Unit: " & .UserUnit & " MinValue: " & .MinValue & " MaxValue: " & .MaxValue & " MinVoltage: " & .MinVoltage & " MaxVoltage: " & .MaxVoltage & " Name: " & .Name)
            End With
        Next
        Return True

    End Function

End Class

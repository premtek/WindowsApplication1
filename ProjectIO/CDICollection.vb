Imports ProjectCore

''' <summary>DI卡型號</summary>
''' <remarks></remarks>
Public Enum enmDICardType
    ''' <summary>無</summary>
    ''' <remarks></remarks>
    None = 0
    ''' <summary>32點數位輸入</summary>
    ''' <remarks></remarks>
    PCI_1756 = 1
    ''' <summary>16點數位輸入</summary>
    ''' <remarks></remarks>
    PCI_1710 = 2
    ''' <summary>64點數位輸入</summary>
    ''' <remarks></remarks>
    PCI_1758 = 3
End Enum
' ''' <summary>DI卡參數</summary>
' ''' <remarks></remarks>
'Public Structure sDICardParameter
'    ''' <summary>卡型號</summary>
'    ''' <remarks></remarks>
'    Public CardType As enmDICardType
'    ''' <summary>敘述</summary>
'    ''' <remarks></remarks>
'    Public DeviceDescreiption As String
'    ''' <summary>卡號</summary>
'    ''' <remarks></remarks>
'    Public CardID As Integer
'    Public Function Load(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
'        Dim strSection As String
'        strSection = "Configuration"
'        CardID = Val(ReadIniString(strSection, "DICard-" & cardNo & "-CardID", fileName, ""))
'        CardType = Val(ReadIniString(strSection, "DICard-" & cardNo & "-CardType", fileName, enmDICardType.PCI_1756))
'        DeviceDescreiption = ReadIniString(strSection, "DICard-" & cardNo & "-Description", fileName, "")
'        gSyslog.Save("DI-Card" & cardNo & ":" & vbTab & "CardID: " & CardID & " CardType: " & CardType.ToString() & " Desc: " & DeviceDescreiption)
'        Return True
'    End Function
'    Public Function Save(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
'        Dim strSection As String
'        strSection = "Configuration"
'        Call SaveIniString(strSection, "DICard-" & cardNo & "-CardID", CardID, fileName)
'        Call SaveIniString(strSection, "DICard-" & cardNo & "-CardType", CardType, fileName)
'        Call SaveIniString(strSection, "DICard-" & cardNo & "-Description", DeviceDescreiption, fileName)
'        Return True
'    End Function

'End Structure
' ''' <summary>DI點位參數</summary>
' ''' <remarks></remarks>
'Public Structure sDIParameter
'    ''' <summary>點位名稱(所有語系)</summary>
'    ''' <remarks></remarks>
'    Public FullName As String
'    ''' <summary>點位名稱</summary>
'    ''' <remarks></remarks>
'    Public Name As String
'    ''' <summary>型號</summary>
'    ''' <remarks></remarks>
'    Public CardType As enmDICardType
'    ''' <summary>原始紀錄位置</summary>
'    ''' <remarks></remarks>
'    Public Address As String
'    ''' <summary>位置</summary>
'    ''' <remarks></remarks>
'    Public IPAddress As Long
'    ''' <summary>埠號</summary>
'    ''' <remarks></remarks>
'    Public Port As Long
'    ''' <summary>點號</summary>
'    ''' <remarks></remarks>
'    Public Bits As Long
'    ''' <summary>略過</summary>
'    ''' <remarks></remarks>
'    Public ByPass As Boolean
'    ''' <summary>邏輯反向</summary>
'    ''' <remarks></remarks>
'    Public Toggle As Boolean
'    ''' <summary>讀值</summary>
'    ''' <remarks></remarks>
'    Public Value As Boolean
'End Structure

' ''' <summary>DI集合</summary>
' ''' <remarks></remarks>
'Public Class CDICollection
'    ''' <summary>
'    ''' DI卡連線設定
'    ''' </summary>
'    ''' <remarks></remarks>
'    Public Cards As New CDICards
'    ''' <summary>[卡片初始化狀態]</summary>
'    ''' <remarks></remarks>
'    Public ReadOnly Property IsCardIntialOK As Boolean
'        Get
'            Return mIsCardIntialOK
'        End Get
'    End Property


'    ''' <summary>[卡片初始化狀態]</summary>
'    ''' <remarks></remarks>
'    Private mIsCardIntialOK As Boolean

'    ''' <summary>[是否走Simulation模式]</summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    Public WriteOnly Property IsSimulationType As Boolean
'        Set(value As Boolean)
'            mIsSimulationType = value
'        End Set
'    End Property

'    ''' <summary>[是否走Simulation模式]</summary>
'    ''' <remarks></remarks>
'    Private mIsSimulationType As Boolean

'    ''' <summary>卡集合</summary>
'    ''' <remarks></remarks>
'    Public Items As New List(Of IDIInterface)

'    ''' <summary>總點數</summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property TotalBits As Integer
'        Set(value As Integer)
'            mTotalBits = value
'        End Set
'        Get
'            Return mTotalBits
'        End Get
'    End Property
'    ''' <summary>總點數</summary>
'    ''' <remarks></remarks>
'    Dim mTotalBits As Integer = 32 '預設值

'    ''' <summary>初始化</summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function Initial(ByVal cards As List(Of sDICardParameter)) As Boolean
'        Try
'            Dim ErrMessage As String = ""

'            If mIsSimulationType = True Then
'                mIsCardIntialOK = False
'                Return True
'            Else
'                mTotalBits = 0
'                For mCardNo As Integer = 0 To cards.Count - 1 '對每一張卡
'                    Select Case cards(mCardNo).CardType '依卡型號做初始化
'                        Case enmDICardType.None '虛擬卡
'                            Items.Add(New CDIVirtual)
'                        Case enmDICardType.PCI_1756
'                            Items.Add(New CDI_PCI_1756)
'                            If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
'                                ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
'                            End If
'                        Case enmDICardType.PCI_1758
'                            Items.Add(New CDI_PCI_1758)
'                            If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
'                                ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
'                            End If
'                        Case enmDICardType.PCI_1710
'                            Items.Add(New CDI_PCI_1710)
'                            If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
'                                ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
'                            End If

'                    End Select
'                    mTotalBits += Items(Items.Count - 1).PortPerCard * Items(Items.Count - 1).BitsPerPort '計算總點數
'                Next
'                ReDim gblnDI(Items.Count - 1, Items(0).PortPerCard, Items(0).BitsPerPort)
'                If mTotalBits > 0 Then
'                    ReDim gblnDIInput(mTotalBits - 1)
'                    ReDim DIParameter(mTotalBits - 1)
'                End If

'                If ErrMessage <> "" Then
'                    mIsCardIntialOK = False
'                    gSyslog.Save(gMsgHandler.GetMessage(Error_1004000), "Error_1004000", eMessageLevel.Error)
'                    gSyslog.Save("Eror Message: " & ErrMessage, , eMessageLevel.Error)
'                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "DI Collection")
'                    Return False
'                Else
'                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6004000))
'                    mIsCardIntialOK = True
'                    Return True
'                End If
'            End If

'        Catch ex As Exception
'            mIsCardIntialOK = False
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1004000), "Error_1004000", eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            MsgBox("初始化失敗" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
'            Return False
'        End Try
'    End Function


'    ''' <summary>資源釋放</summary>
'    ''' <remarks></remarks>
'    Public Function Close() As Boolean
'        Try

'            Dim mCardNo As Integer

'            If mIsSimulationType = True Then
'                gSyslog.Save(gMsgHandler.GetMessage(INFO_6004001))
'                gSyslog.Save("Simulation")
'                Return True
'            End If

'            If mIsCardIntialOK = True Then
'                '[說明]:卡片關閉
'                For mCardNo = 0 To Items.Count - 1
'                    Items(mCardNo).Close()
'                Next
'                gSyslog.Save(gMsgHandler.GetMessage(INFO_6004001))
'                Return True
'            Else
'                gSyslog.Save(gMsgHandler.GetMessage(INFO_6004001))
'                'gSyslog.Save("DI Card Close OK(Initial Failed.).")
'                Return True
'            End If


'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1004001),Error_1004001 , eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
'            Return False
'        End Try

'    End Function


'    ''' <summary>DI接點參數</summary>
'    ''' <remarks></remarks>
'    Public DIParameter(255) As sDIParameter
'    ''' <summary>DI原始資料</summary>
'    ''' <remarks></remarks>
'    Dim gblnDI(,,) As Boolean
'    '--------------------------------------------------------------------
'    ''' <summary>[DI-Input(陣列)]</summary>
'    ''' <remarks></remarks>
'    Dim gblnDIInput(255) As Boolean

'    ''' <summary>取得DI輸入值</summary>
'    ''' <param name="DIIndex"></param>
'    ''' <param name="defaultStatus"></param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function GetState(ByVal DIIndex As Integer, Optional ByVal defaultStatus As Boolean = False) As Boolean
'        If DIIndex < DIParameter.GetLowerBound(0) Then
'            Return defaultStatus
'        End If
'        If DIIndex > DIParameter.GetUpperBound(0) Then
'            Return defaultStatus
'        End If

'        Try
'            With DIParameter(DIIndex)
'                If .ByPass = True Then '略過,傳回預設值
'                    Return defaultStatus
'                End If

'                If .Toggle = True Then '反向
'                    Return Not gblnDIInput(DIIndex) '傳回反向讀取結果
'                End If

'                Return gblnDIInput(DIIndex) '傳回讀取結果
'            End With

'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1004002), "Error_1004002", eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try
'    End Function

'    Public Function RefreshDI() As Boolean

'        Dim mBitNo As Long
'        Dim mPortNo As Long
'        Dim bytDiData As Byte   '[讀取出來的數值]

'        Try
'            If mIsSimulationType = True Then
'                Return True
'            Else
'                If mIsCardIntialOK = True Then
'                    'Byte資料轉為卡埠點表
'                    For mCardNo As Integer = 0 To Items.Count - 1
'                        For mPortNo = 0 To Items(mCardNo).PortPerCard - 1
'                            Items(mCardNo).Read(mPortNo, bytDiData)
'                            For mBitNo = Items(mCardNo).BitsPerPort - 1 To 0 Step -1
'                                gblnDI(mCardNo, mPortNo, mBitNo) = IIf((bytDiData And (2 ^ mBitNo)) <> 0, True, False)
'                            Next
'                            bytDiData = Nothing
'                        Next
'                    Next

'                    '點位Mapping
'                    For mBitNo = 0 To mTotalBits - 1 '目前DI只使用32組
'                        gblnDIInput(mBitNo) = gblnDI(DIParameter(mBitNo).IPAddress, DIParameter(mBitNo).Port, DIParameter(mBitNo).Bits)
'                    Next
'                    Return True
'                Else
'                    Return True
'                End If
'            End If


'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1004003), "Error_1004003", eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try
'    End Function

'    ''' <summary>讀取DI設定檔</summary>
'    ''' <param name="strFileName"></param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function Load(ByVal strFileName As String) As Boolean
'        Try

'            Dim mBitNo As Integer
'            Dim strSection As String
'            Dim str_DIAddressBuffer() As String

'            strSection = "Configuration"
'            gSyslog.Save("DI Parameter Loaded as below:")

'            For mBitNo = 0 To mTotalBits - 1 '讀取
'                With DIParameter(mBitNo)
'                    '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
'                    Dim mName As String = ReadIniString(strSection, "DI-" & mBitNo & "-Name", strFileName, "")
'                    Dim mSplitedName() As String = mName.Split(",")
'                    Select Case gSSystemParameter.LanguageType
'                        Case enmLanguageType.eEnglish
'                            If mSplitedName.Count > 0 Then
'                                .Name = mSplitedName(0)
'                            End If
'                        Case enmLanguageType.eTraditionalChinese
'                            If mSplitedName.Count > 1 Then
'                                .Name = mSplitedName(1)
'                            End If
'                        Case enmLanguageType.eSimplifiedChinese
'                            If mSplitedName.Count > 2 Then
'                                .Name = mSplitedName(2)
'                            End If
'                    End Select

'                    .FullName = mName
'                    .CardType = CInt(ReadIniString(strSection, "DI-" & mBitNo & "-CardType", strFileName, enmDICardType.PCI_1758))
'                    .Address = ReadIniString(strSection, "DI-" & mBitNo & "-Address", strFileName, "")
'                    .ByPass = CInt(ReadIniString(strSection, "DI-" & mBitNo & "-ByPass", strFileName, 0))
'                    .Toggle = CInt(ReadIniString(strSection, "DI-" & mBitNo & "-Toggle", strFileName, 0))

'                    If DIParameter(mBitNo).Address.Contains("-") Then
'                        str_DIAddressBuffer = DIParameter(mBitNo).Address.Split("-")
'                        DIParameter(mBitNo).IPAddress = Val(str_DIAddressBuffer(0))
'                        DIParameter(mBitNo).Port = Convert.ToInt16(str_DIAddressBuffer(1).Substring(0, 2), 16) \ 8
'                        DIParameter(mBitNo).Bits = Convert.ToInt64(str_DIAddressBuffer(1).Substring(0, 2), 16) Mod 8
'                    End If
'                    gSyslog.Save("DI-" & Format(mBitNo, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & IIf(.ByPass, 1, 0) & " Toggle: " & IIf(.Toggle, 1, 0) & " Name: " & .Name)
'                End With
'            Next
'            Return True

'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1002016), "Error_1002016", eMessageLevel.Error)
'            gSyslog.Save("Excepption Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try
'    End Function
'    ''' <summary>
'    ''' 儲存DI設定檔
'    ''' </summary>
'    ''' <param name="strFileName"></param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function Save(ByVal strFileName As String) As Boolean
'        Try

'            Dim mBitNo As Long
'            Dim strSection As String

'            strSection = "Configuration"
'            gSyslog.Save("DI Parameter Saved as below:")
'            For mBitNo = 0 To mTotalBits - 1
'                With DIParameter(mBitNo)
'                    '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
'                    Call SaveIniString(strSection, "DI-" & mBitNo & "-Name", .FullName, strFileName)
'                    Call SaveIniString(strSection, "DI-" & mBitNo & "-CardType", CInt(.CardType), strFileName)
'                    Call SaveIniString(strSection, "DI-" & mBitNo & "-Address", .Address, strFileName)
'                    Call SaveIniString(strSection, "DI-" & mBitNo & "-ByPass", CInt(.ByPass), strFileName)
'                    Call SaveIniString(strSection, "DI-" & mBitNo & "-Toggle", CInt(.Toggle), strFileName)
'                    gSyslog.Save("DI-" & Format(mBitNo, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & IIf(.ByPass, 1, 0) & " Toggle: " & IIf(.Toggle, 1, 0) & " Name: " & .Name)
'                End With
'            Next
'            Return True

'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1002017), "Error_1002017", eMessageLevel.Error)
'            gSyslog.Save("Excepption Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try
'    End Function
'End Class

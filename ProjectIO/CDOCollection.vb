Imports ProjectCore

''' <summary>DO卡型號</summary>
''' <remarks></remarks>
Public Enum enmDOCardType
    ''' <summary>無</summary>
    ''' <remarks></remarks>
    None = 0
    ''' <summary>32點數位輸出</summary>
    ''' <remarks></remarks>
    PCI_1756 = 1
    ''' <summary>16點數位輸出</summary>
    ''' <remarks></remarks>
    PCI_1710 = 2
    ''' <summary>64點數位輸出</summary>
    ''' <remarks></remarks>
    PCI_1758 = 3
End Enum

' ''' <summary>DO卡參數</summary>
' ''' <remarks></remarks>
'Public Structure sDOCardParameter
'    ''' <summary>卡型號</summary>
'    ''' <remarks></remarks>
'    Public CardType As enmDOCardType
'    ''' <summary>敘述</summary>
'    ''' <remarks></remarks>
'    Public DeviceDescreiption As String
'    ''' <summary>卡號</summary>
'    ''' <remarks></remarks>
'    Public CardID As Integer

'    Public Function Load(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
'        Dim strSection As String
'        strSection = "Configuration"
'        CardID = Val(ReadIniString(strSection, "DOCard-" & cardNo & "-CardID", fileName, ""))
'        CardType = Val(ReadIniString(strSection, "DOCard-" & cardNo & "-CardType", fileName, enmDOCardType.PCI_1756))
'        DeviceDescreiption = ReadIniString(strSection, "DOCard-" & cardNo & "-Description", fileName, "")
'        gSyslog.Save("DO-Card" & cardNo & ":" & vbTab & "CardID: " & CardID & " CardType: " & CardType.ToString() & " Desc: " & DeviceDescreiption)
'        Return True
'    End Function
'    Public Function Save(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
'        Dim strSection As String
'        strSection = "Configuration"
'        Call SaveIniString(strSection, "DOCard-" & cardNo & "-CardID", CardID, fileName)
'        Call SaveIniString(strSection, "DOCard-" & cardNo & "-CardType", CardType, fileName)
'        Call SaveIniString(strSection, "DOCard-" & cardNo & "-Description", DeviceDescreiption, fileName)
'        Return True
'    End Function
'End Structure

' ''' <summary>DO點位參數</summary>
' ''' <remarks></remarks>
'Public Structure sDOParameter
'    ''' <summary>點位名稱(所有語系)</summary>
'    ''' <remarks></remarks>
'    Public FullName As String
'    ''' <summary>點位名稱</summary>
'    ''' <remarks></remarks>
'    Public Name As String
'    ''' <summary>型號</summary>
'    ''' <remarks></remarks>
'    Public CardType As enmDOCardType
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
'    ''' <summary>初始值</summary>
'    ''' <remarks></remarks>
'    Public InitialValue As Boolean
'    Public ByPassInitialValue As Boolean

'End Structure

' ''' <summary>DO集合</summary>
' ''' <remarks></remarks>
'Public Class CDOCollection
'    ''' <summary>DO卡連線設定</summary>
'    ''' <remarks></remarks>
'    Public Cards As New CDOCards
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
'    Public Items As New List(Of IDOInterface)

'    ''' <summary>總點數</summary>
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
'    Dim mTotalBits As Integer = 31 '預設值
'    ''' <summary>DO接點參數</summary>
'    ''' <remarks></remarks>
'    Public DOParameter(255) As sDOParameter
'    ''' <summary>DO原始資料,初始化後定義</summary>
'    ''' <remarks></remarks>
'    Public RawValue(,,) As Boolean
'    ''' <summary>[DO-Output(陣列)]</summary>
'    ''' <remarks></remarks>
'    Dim mblnDOOutput(255) As Boolean


'    ''' <summary>[取得設定目前DO設定狀態]</summary>
'    ''' <param name="DOIndex"></param>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property GetSetState(ByVal DOIndex As Integer) As Boolean
'        Get
'            Try
'                If DOIndex < 0 Then
'                    Return False
'                End If
'                Return mblnDOOutput(DOIndex)
'            Catch ex As Exception
'                gSyslog.Save(gMsgHandler.GetMessage(Error_1005002), "Error_1005002", eMessageLevel.Error)
'                gSyslog.Save("DO Index: " & DOIndex, , eMessageLevel.Error)
'                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'                Return False
'            End Try
'        End Get
'        Set(value As Boolean)
'            Try
'                If DOIndex < 0 Then
'                    Exit Property
'                End If
'                With DOParameter(DOIndex)
'                    If .ByPass Then
'                        Exit Property
'                    End If
'                    If .Toggle Then
'                        mblnDOOutput(DOIndex) = Not value
'                        Exit Property
'                    End If
'                    mblnDOOutput(DOIndex) = value
'                End With
'            Catch ex As Exception
'                gSyslog.Save(gMsgHandler.GetMessage(Error_1005003), "Error_1005003", eMessageLevel.Error)
'                gSyslog.Save("DO Index: " & DOIndex, , eMessageLevel.Error)
'                gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            End Try

'        End Set
'    End Property

'    ''' <summary>[狀態反向輸出]</summary>
'    ''' <param name="DOindex"></param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function ToggleOutput(ByVal DOindex As Integer) As Boolean
'        Try
'            If DOindex >= DOParameter.Count Then
'                Return False
'            End If
'            If DOindex < 0 Then
'                Return False
'            End If

'            With DOParameter(DOindex)
'                If .ByPass Then
'                    Return mblnDOOutput(DOindex)
'                End If
'                mblnDOOutput(DOindex) = Not mblnDOOutput(DOindex)
'            End With

'            Return mblnDOOutput(DOindex)
'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1005004), "Error_1005004", eMessageLevel.Error)
'            gSyslog.Save("DO Index: " & DOindex, , eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try

'    End Function


'    Public Sub New()
'        ReDim RawValue(1, 8, 8)
'    End Sub
'    ''' <summary>初始化</summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function Initial(ByVal cards As List(Of sDOCardParameter)) As Boolean
'        Try
'            Dim ErrMessage As String = ""

'            If mIsSimulationType = True Then
'                mIsCardIntialOK = False
'                Return True
'            End If

'            mTotalBits = 0
'            For mCardNo As Integer = 0 To cards.Count - 1 '對每一張卡
'                Select Case cards(mCardNo).CardType '依卡型號做初始化
'                    Case enmDOCardType.None '虛擬卡
'                        Items.Add(New CDOVirtual)
'                    Case enmDOCardType.PCI_1756
'                        Items.Add(New CDO_PCI_1756)
'                        If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
'                            ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
'                        End If
'                    Case enmDOCardType.PCI_1758
'                        Items.Add(New CDO_PCI_1758)
'                        If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
'                            ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
'                        End If
'                    Case enmDOCardType.PCI_1710
'                        Items.Add(New CDO_PCI_1710)
'                        If Items(Items.Count - 1).Initial(cards(mCardNo).DeviceDescreiption) = False Then
'                            ErrMessage += "" & cards(mCardNo).DeviceDescreiption & "初始化失敗."
'                        End If
'                End Select
'                mTotalBits += Items(Items.Count - 1).PortPerCard * Items(Items.Count - 1).BitsPerPort '計算總點數
'            Next

'            ReDim RawValue(Items.Count - 1, Items(0).PortPerCard - 1, Items(0).BitsPerPort - 1)
'            ReDim DOParameter(mTotalBits)
'            ReDim mblnDOOutput(mTotalBits)

'            If ErrMessage <> "" Then
'                mIsCardIntialOK = False
'                gSyslog.Save(gMsgHandler.GetMessage(Error_1005000) & ErrMessage, "Error_1005000", eMessageLevel.Error)
'                MsgBox(gMsgHandler.GetMessage(Error_1005000) & ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDOCollection.Initial")
'                Return False
'            Else
'                mIsCardIntialOK = True
'                Return True
'            End If


'        Catch ex As Exception
'            mIsCardIntialOK = False
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1005000) & ex.Message, "Error_1005000", eMessageLevel.Error)
'            MsgBox(gMsgHandler.GetMessage(Error_1005000) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDOCollection.Initial")
'            Return False
'        End Try
'    End Function

'    ''' <summary>資源釋放</summary>
'    ''' <remarks></remarks>
'    Public Function Close() As Boolean
'        Try
'            Dim intI As Integer

'            If mIsSimulationType = True Then
'                gSyslog.Save(gMsgHandler.GetMessage(INFO_6005001), "INFO_6005001")
'                gSyslog.Save("Simulation Mode")

'                Return True
'            End If

'            If mIsCardIntialOK = True Then
'                '[說明]:卡片關閉
'                For intI = 0 To Items.Count - 1
'                    Items(intI).Close()
'                Next
'                gSyslog.Save(gMsgHandler.GetMessage(INFO_6005001), "INFO_6005001")
'                Return True
'            Else
'                'gSyslog.Save("DO Card Close OK.(Initial Failed.)")
'                gSyslog.Save(gMsgHandler.GetMessage(INFO_6005001), "INFO_6005001")
'                Return True
'            End If

'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1005001), "Error_1005001")
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            MsgBox(gMsgHandler.GetMessage(Error_1005001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
'            Return False
'        End Try

'    End Function

'    ''' <summary>
'    ''' 寫入卡片 DO點
'    ''' </summary>
'    ''' <remarks></remarks>
'    Public Function RefreshDO() As Boolean
'        Try
'            Dim mBitNo As Long
'            Dim lngIPBuffer As Long
'            Dim lngPortBuffer As Long
'            Dim lngPointBuffer As Long
'            Dim bytDoData As Byte   '[寫入數值]

'            If mIsSimulationType = True Then
'                Return True
'            Else
'                'If mIsCardIntialOK = True Then
'                For mBitNo = 0 To mTotalBits - 1 '目前DO使用64組
'                    RawValue(DOParameter(mBitNo).IPAddress, DOParameter(mBitNo).Port, DOParameter(mBitNo).Bits) = mblnDOOutput(mBitNo)
'                Next

'                For lngIPBuffer = 0 To Items.Count - 1
'                    If Items(lngIPBuffer).IsInitialOK = True Then
'                        For lngPortBuffer = 0 To Items(lngIPBuffer).PortPerCard - 1
'                            bytDoData = 0
'                            For lngPointBuffer = 0 To Items(lngIPBuffer).BitsPerPort - 1
'                                If RawValue(lngIPBuffer, lngPortBuffer, lngPointBuffer) = True Then
'                                    bytDoData = bytDoData + CByte(2 ^ lngPointBuffer)
'                                End If
'                            Next
'                            Items(lngIPBuffer).Write(lngPortBuffer, bytDoData)
'                        Next
'                    End If

'                Next
'                Return True
'                'Else
'                '    Return True
'                'End If
'            End If
'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1005003), "Error_1005003", eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try

'    End Function

'    ''' <summary>
'    ''' 讀取DO設定檔
'    ''' </summary>
'    ''' <param name="strFileName"></param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function Load(ByVal strFileName As String) As Boolean
'        Try

'            Dim mBitNo As Integer
'            Dim strSection As String
'            Dim strDOAddressBuffer() As String

'            strSection = "Configuration"
'            gSyslog.Save("DO Parameter Loaded as below:")
'            For mBitNo = 0 To mTotalBits - 1
'                With DOParameter(mBitNo)
'                    '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
'                    Dim mName As String = ReadIniString(strSection, "DO-" & mBitNo & "-Name", strFileName)
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
'                    .CardType = Val(ReadIniString(strSection, "DO-" & mBitNo & "-CardType", strFileName, enmDOCardType.PCI_1758))
'                    .Address = ReadIniString(strSection, "DO-" & mBitNo & "-Address", strFileName, "")
'                    .ByPass = Val(ReadIniString(strSection, "DO-" & mBitNo & "-ByPass", strFileName, 0))
'                    .Toggle = Val(ReadIniString(strSection, "DO-" & mBitNo & "-Toggle", strFileName, 0))
'                    .InitialValue = CBool(ReadIniString(strSection, "DO-" & mBitNo & "-InitialValue", strFileName, 0))

'                    If .Address.Contains("-") = True Then
'                        strDOAddressBuffer = .Address.Split("-")
'                        .IPAddress = Val(strDOAddressBuffer(0))
'                        .Port = Convert.ToUInt64(strDOAddressBuffer(1).Substring(0, 2), 16) \ 8
'                        .Bits = Convert.ToUInt64(strDOAddressBuffer(1).Substring(0, 2), 16) Mod 8
'                    End If
'                End With
'            Next

'            For mBitNo = 0 To mTotalBits - 1
'                With DOParameter(mBitNo)
'                    gSyslog.Save("DO-" & Format(mBitNo, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & IIf(.ByPass, 1, 0) & " Toggle: " & IIf(.Toggle, 1, 0) & " Name: " & .Name)
'                End With
'            Next

'            Return True

'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1002018), "Error_1002018", eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
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
'            gSyslog.Save("DO Parameter Saved as below:")
'            For mBitNo = 0 To mTotalBits - 1
'                With DOParameter(mBitNo)
'                    '[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
'                    Call SaveIniString(strSection, "DO-" & mBitNo & "-Name", .FullName, strFileName)
'                    Call SaveIniString(strSection, "DO-" & mBitNo & "-CardType", CInt(.CardType), strFileName)
'                    Call SaveIniString(strSection, "DO-" & mBitNo & "-Address", .Address, strFileName)
'                    Call SaveIniString(strSection, "DO-" & mBitNo & "-ByPass", CInt(.ByPass), strFileName)
'                    Call SaveIniString(strSection, "DO-" & mBitNo & "-Toggle", CInt(.Toggle), strFileName)
'                    Call SaveIniString(strSection, "DO-" & mBitNo & "-InitialValue", CInt(.InitialValue), strFileName)
'                    gSyslog.Save("DO-" & Format(mBitNo, "000") & " Address: " & .Address & " CardType: " & .CardType.ToString() & " ByPass: " & IIf(.ByPass, 1, 0) & " Toggle: " & IIf(.Toggle, 1, 0) & " Name: " & .Name)
'                End With
'            Next
'            Return True
'        Catch ex As Exception
'            gSyslog.Save(gMsgHandler.GetMessage(Error_1002019), "Error_1002019", eMessageLevel.Error)
'            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
'            Return False
'        End Try

'    End Function

'    ''' <summary>設定DO回到初始值</summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function ReSetDO() As Boolean
'        Try
'            For mBitNo = 0 To mTotalBits - 1
'                If DOParameter(mBitNo).ByPassInitialValue = False Then
'                    mblnDOOutput(mBitNo) = DOParameter(mBitNo).InitialValue
'                End If
'            Next
'            Return True
'        Catch ex As Exception
'            Debug.Print(ex.ToString())
'            Return False
'        End Try
'    End Function

'End Class

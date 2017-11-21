Imports ProjectCore

#Region "連線參數"

''' <summary>使用DL RS1A序列埠</summary>
''' <remarks></remarks>
Public Structure DLRS1AConnect
    ''' <summary>COM1,2,3...etc</summary>
    ''' <remarks></remarks>
    Public PortName As String
    ''' <summary>Baud Rate: 9600...etc</summary>
    ''' <remarks></remarks>
    Public BaudRate As String
End Structure

''' <summary>使用Line Scan主機網路埠</summary>
''' <remarks></remarks>
Public Structure LJV7060TCPConnect
    ''' <summary>通訊位置</summary>
    ''' <remarks></remarks>
    Public IP As String
    ''' <summary>通訊埠</summary>
    ''' <remarks></remarks>
    Public Port As Integer
End Structure

''' <summary>雷射連線參數</summary>
''' <remarks></remarks>
Public Structure sLaserReaderConnectionParameter
    ''' <summary>元件類型</summary>
    ''' <remarks></remarks>
    Public CardType As enmLaserInterferometerType
    Public Function CardTypeToString() As String
        Select Case CardType
            Case enmLaserInterferometerType.None
                Return "None"
            Case enmLaserInterferometerType.KeyenceILS065Voltage
                Return "IL-S065"
            Case enmLaserInterferometerType.KeyenceLJV7060TCP
                Return "LJ-V7060 TCP"
        End Select
        Return "Unknown"
    End Function

    ''' <summary>LJ-V7060通訊設定</summary>
    ''' <remarks></remarks>
    Public LJV7060TCP As LJV7060TCPConnect

    ''' <summary>DL-RS1A通訊設定</summary>
    ''' <remarks></remarks>
    Public DLRS1A As DLRS1AConnect

#Region "DI轉接"
    ''' <summary>DI接點編號</summary>
    ''' <remarks></remarks>
    Public Bit As Integer

#End Region

    ''' <summary>
    ''' 複製
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Public Sub CopyFrom(ByRef item As sLaserReaderConnectionParameter)
        Me.CardType = item.CardType
        Me.LJV7060TCP.IP = item.LJV7060TCP.IP
        Me.LJV7060TCP.Port = item.LJV7060TCP.Port
        Me.DLRS1A = New DLRS1AConnect
        Me.DLRS1A.PortName = item.DLRS1A.PortName
        Me.DLRS1A.BaudRate = item.DLRS1A.BaudRate
    End Sub
End Structure

#End Region

''' <summary>雷射接點參數</summary>
''' <remarks></remarks>
Public Structure sLaserReaderParameter
    ''' <summary>點位名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmLaserInterferometerType
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
    Public ItemNo As Integer
    Public Function Save(ByVal enmLaserNo As Integer, ByVal fileName As String) As Boolean
        Dim mSection As String = "Channel" & (enmLaserNo + 1).ToString
        SaveIniString(mSection, "Name", Name, fileName)
        SaveIniString(mSection, "CardType", CInt(CardType), fileName)
        SaveIniString(mSection, "Address", Address, fileName)
        SaveIniString(mSection, "IPAddress", IPAddress, fileName)
        SaveIniString(mSection, "Port", Port, fileName)
        SaveIniString(mSection, "AIIndex", AIIndex, fileName)
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
        AIIndex = Val(ReadIniString(mSection, "AIIndex", fileName))
        ByPass = Val(ReadIniString(mSection, "ByPass", fileName))
        Value = ReadIniString(mSection, "Value", fileName)
        ItemNo = Val(ReadIniString(mSection, "ItemNo", fileName))
        Return True
    End Function
End Structure

''' <summary>非接觸式測高物件類別</summary>
''' <remarks></remarks>
Public Class CLaserReaderCollection
    ''' <summary>[卡片初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsCardIntialOK As Boolean
        Get
            Return mIsCardIntialOK
        End Get
    End Property
    ''' <summary>雷射讀值的極限值</summary>
    ''' <remarks></remarks>
    Public ReaderMaxValue As Decimal = 9
    ''' <summary>
    ''' 陣列複製自Array
    ''' </summary>
    ''' <param name="array"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ArrayCopyFrom(ByRef array() As sLaserReaderConnectionParameter) As Boolean
        '編輯區資料全部套用
        For mCardNo As Integer = 0 To Cards.Count - 1
            If mCardNo < array.Count Then
                'gLaserReaderCollection.Cards(mCardNo).CopyFrom(array(mCardNo))
                Cards(mCardNo) = array(mCardNo)
            End If
        Next
        Return True
    End Function
    ''' <summary>
    ''' 陣列複製到Array
    ''' </summary>
    ''' <param name="array"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ArrayCopyTo(ByRef array() As sLaserReaderConnectionParameter) As Boolean
        ReDim array(Cards.Count - 1)
        For mCardNo As Integer = 0 To Cards.Count - 1
            array(mCardNo) = Cards(mCardNo)
        Next
        Return True
    End Function

    ''' <summary>連線參數設定</summary>
    ''' <remarks></remarks>
    Public Cards As New CLaserReaderCards
    ''' <summary>個別參數設定(索引為enmLaserReader)</summary>
    ''' <remarks></remarks>
    Private Channel(enmLaserReader.Max) As sLaserReaderParameter
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
    Public Items As New List(Of ILaserReader)

    ''' <summary>
    ''' 卡片設定檔檔名(為避免設定值未被覆蓋,請使用時重新設定一次)
    ''' </summary>
    ''' <remarks></remarks>
    Public CardFileName As String = Application.StartupPath & "\System\" & MachineName & "\CardLaserReader.ini"

    Public Function Initial() As Boolean
        '=== Card等級存取 ===
        Me.Load(CardFileName)
        '=== Card等級存取 ===

        '=== Channel等級存取 ===
        'For mChNo As Integer = enmLaserReader.No1 To enmLaserReader.Max
        For mChNo As Integer = enmLaserReader.No1 To gSSystemParameter.StageCount - 1
            Channel(mChNo).Load(mChNo, Application.StartupPath & "\System\" & MachineName & "\ConfigLaserReader" & (mChNo + 1).ToString & ".ini")
        Next

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A 'enmMachineType.eGN2, enmMachineType.eGN3,
                Channel(enmLaserReader.No1).AIIndex = enmAI.LaserReader

        End Select

        'For mChNo As Integer = enmLaserReader.No1 To enmLaserReader.Max
        For mChNo As Integer = enmLaserReader.No1 To gSSystemParameter.StageCount - 1
            Channel(mChNo).Save(mChNo, Application.StartupPath & "\System\" & MachineName & "\ConfigLaserReader" & (mChNo + 1).ToString & ".ini")
        Next
        '=== Channel等級存取 ===

        If Me.Initial(Cards.Parameters) = False Then
            gSyslog.Save("Initialized Laser Reader Failed!", , eMessageLevel.Error)
            Return False
        End If

        Return True

    End Function

    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal cards As List(Of sLaserReaderConnectionParameter)) As Boolean
        Try
            Dim ErrMessage As String = ""

            If mIsSimulationType = True Then
                mIsCardIntialOK = False
                Return True
            End If

            For mCardNo As Integer = 0 To cards.Count - 1 '對每一張卡
                Select Case cards(mCardNo).CardType '依卡型號做初始化
                    Case enmLaserInterferometerType.None '虛擬卡
                    Case enmLaserInterferometerType.KeyenceILS065Voltage
                        Items.Add(New CLaserReader_KeyenceILS065Voltage)
                    Case enmLaserInterferometerType.KeyenceLJV7060TCP
                        Items.Add(New CLaserReader_KeyenceLJV7060_TCP)
                        If Items(Items.Count - 1).EthernetOpen(cards(mCardNo).LJV7060TCP.IP, cards(mCardNo).LJV7060TCP.Port) = False Then
                            ErrMessage += "" & cards(mCardNo).CardTypeToString & "(" & cards(mCardNo).LJV7060TCP.IP & "Port:" & cards(mCardNo).LJV7060TCP.Port & ") 初始化失敗."
                        End If
                    Case enmLaserInterferometerType.KeyenceIL065DLRS1A
                        Items.Add(New CLaserReader_DLRS1A)
                        If Items(Items.Count - 1).Initial(cards(mCardNo).DLRS1A.PortName, cards(mCardNo).DLRS1A.BaudRate) = False Then
                            ErrMessage += "" & cards(mCardNo).CardTypeToString & "(" & cards(mCardNo).DLRS1A.PortName & ") 初始化失敗."
                        End If
                End Select
            Next

            If ErrMessage <> "" Then
                mIsCardIntialOK = False
                gSyslog.Save(gMsgHandler.GetMessage(Error_1014000), "Error_1014000", eMessageLevel.Error)
                gSyslog.Save("Eror Message: " & ErrMessage, , eMessageLevel.Error)
                MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Laser Reader Collection")
                Return False
            Else
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6004000))
                mIsCardIntialOK = True
                Return True
            End If


        Catch ex As Exception
            mIsCardIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014000), "Error_1014000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox("初始化失敗" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
            Return False
        End Try
    End Function

    ''' <summary>儲存連線參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String)

        Dim mSection As String = "LaserReader"
       
        For mChNo As Integer = enmLaserReader.No1 To enmLaserReader.Max
            Channel(mChNo).Save(mChNo, Application.StartupPath & "\System\" & MachineName & "\ConfigLaserReader" & (mChNo + 1).ToString & ".ini")
        Next
        Return True
    End Function
    ''' <summary>讀取連線參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String)
        Dim mSection As String = "LaserReader"
        'Dim mCardCount As Integer
        Cards.Load(fileName)
        For mChNo As Integer = enmLaserReader.No1 To enmLaserReader.Max
            Channel(mChNo).Load(mChNo, Application.StartupPath & "\System\" & MachineName & "\ConfigLaserReader" & (mChNo + 1).ToString & ".ini")
        Next
        Return True
    End Function


    ''' <summary>資源釋放</summary>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Try

            If mIsSimulationType = True Then '模擬
                gSyslog.Save("Laser Close OK.")
                gSyslog.Save("Simulation")
                Return True
            End If

            If mIsCardIntialOK = False Then '未開卡成功
                gSyslog.Save("Laser Close OK.")
                Return True
            End If

            '[說明]:卡片關閉
            For mItemNo As Integer = 0 To Items.Count - 1
                Items(mItemNo).Close()
            Next
            gSyslog.Save("Laser Close OK.")
            Return True


        Catch ex As Exception
            gSyslog.Save("Laser Close Failed!", , eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1004001) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CDICollection@Initial")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 是否索引超出範圍
    ''' </summary>
    ''' <param name="laserNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsIndexOutOfRange(ByVal laserNo As Integer) As Boolean
        If laserNo < Channel.GetLowerBound(0) Then
            Return True
        End If
        If laserNo > Channel.GetUpperBound(0) Then
            Return True
        End If
        If Channel(laserNo).ItemNo < 0 Then
            Return True
        End If
        If Channel(laserNo).ItemNo >= Items.Count Then
            Return True
        End If
        Return False
    End Function
    ' ''' <summary>
    ' ''' 狀態重置
    ' ''' </summary>
    ' ''' <param name="laserNo">虛擬ID</param>
    ' ''' <remarks></remarks>
    'Public Sub ResetState(ByVal laserNo As Integer)
    '    If IsIndexOutOfRange(laserNo) Then
    '        Exit Sub
    '    End If
    '    With Channel(laserNo)
    '        Call Items(Channel(laserNo).ItemNo).ResetState()
    '    End With
    'End Sub


    ''' <summary>讀取值命令</summary>
    ''' <param name="laserNo">enmLaser索引</param>
    ''' <param name="value">預設值</param>
    ''' <returns>讀取資料,如為Line-Scan,使用|做Split後使用</returns>
    ''' <remarks>有waitReturn,value才有效</remarks>
    Public Function GetValue(ByVal Mode As String, ByVal laserNo As Integer, ByRef value As String, Optional ByVal waitReturn As Boolean = False) As Boolean
        If IsIndexOutOfRange(laserNo) Then
            Return False
        End If

        Try
            With Channel(laserNo)
                If .ByPass = True Then '略過,傳回預設值
                    Return True
                End If
                Return Items(Channel(laserNo).ItemNo).GetValue(Mode, value, Channel(laserNo).AIIndex, waitReturn) '傳回讀取結果

            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014003), "Error_1014003", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function

    ''' <summary>讀取雷射資料</summary>
    ''' <param name="laserNo">enmLaser索引</param>
    ''' <param name="value">資料值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLaserValue(ByVal laserNo As Integer, ByRef value As String) As Boolean
        With Channel(laserNo)
            If .ByPass = True Then '略過,傳回預設值
                Return value
            End If
            value = Items(Channel(laserNo).ItemNo).Result(0).Value
            Return Items(Channel(laserNo).ItemNo).Result(0).Status
        End With
    End Function

    ''' <summary>選取Recipe</summary>
    ''' <param name="laserNo">enmLaser索引</param>
    ''' <param name="ProgramID">LaserReader的RecipeName</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectRecipe(ByVal laserNo As Integer, ByVal ProgramID As Integer) As Boolean
        If IsIndexOutOfRange(laserNo) Then
            Return False
        End If

        Try
            With Channel(laserNo)
                If .ByPass = True Then '略過,傳回預設值
                    Return True
                End If

                Return Items(Channel(laserNo).ItemNo).ChangeProgram(ProgramID) '傳回讀取結果
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1014002), "Error_1014002", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    ''' <summary>是否讀取逾時</summary>
    ''' <param name="laserNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsTimeOut(ByVal laserNo As Integer) As Boolean
        Get
            If IsIndexOutOfRange(laserNo) Then
                Return False
            End If
            With Channel(laserNo)
                If .ByPass = True Then '略過,傳回預設值
                    Return False
                End If
                Return Items(Channel(laserNo).ItemNo).IsTimeOut '傳回讀取結果
            End With
        End Get
    End Property


    ''' <summary>是否讀取逾時</summary>
    ''' <param name="laserNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBusy(ByVal laserNo As Integer) As Boolean
        Get
            If IsIndexOutOfRange(laserNo) Then
                Return False
            End If
            With Channel(laserNo)
                If .ByPass = True Then '略過,傳回預設值
                    Return False
                End If

                Return Items(Channel(laserNo).ItemNo).IsBusy '傳回讀取結果
            End With
        End Get
    End Property


    ''' <summary>[設定Timeout時間]</summary>
    ''' <param name="laserNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeoutTimes(ByVal laserNo As Integer) As Integer
        Get
            If IsIndexOutOfRange(laserNo) Then
                Return False
            End If
            With Channel(laserNo)
                If .ByPass = True Then '略過,傳回預設值
                    Return False
                End If
                Return Items(Channel(laserNo).ItemNo).TimeoutTimer '傳回讀取結果
            End With
        End Get
        Set(value As Integer)
            Items(Channel(laserNo).ItemNo).TimeoutTimer = value
        End Set
    End Property

End Class

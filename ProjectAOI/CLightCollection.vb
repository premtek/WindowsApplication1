Imports ProjectIO
Imports ProjectAOI
Imports ProjectCore

''' <summary>光源Mapping</summary>
''' <remarks></remarks>
Public Structure sLightParam
    ''' <summary>實際控制器索引</summary>
    ''' <remarks></remarks>
    Public ItemNo As Integer
    ''' <summary>實際通道索引</summary>
    ''' <remarks></remarks>
    Public ChannelNo As Integer
End Structure



''' <summary>[程控光源] </summary>
''' <remarks></remarks>
Public Class CLightCollection
    ''' <summary>
    ''' for暫存，便於比對介面上的數值
    ''' </summary>
    ''' <remarks></remarks>
    Public mUILightValue1 As Integer
    Public mUILightValue2 As Integer
    Public mUILightValue3 As Integer
    Public mUILightValue4 As Integer

    ''' <summary>
    ''' [光源亮度設定]
    ''' </summary>
    ''' <param name="CCDNo">選擇CCD</param>
    ''' <param name="enmLight">選擇相應點位(虛擬點位)</param>
    ''' <param name="value">亮度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCCDLight(ByVal ccdNo As Integer, ByVal enmLight As enmLight, ByVal value As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        If enmLight < 0 Then
            Return False
        End If
        Dim mItemNo As Integer = LightParam(enmLight).ItemNo
        If mItemNo < 0 Then
            Return False
        End If
        Dim mChannelNo As Integer = LightParam(enmLight).ChannelNo
        If mChannelNo < 0 Then
            Return False
        End If

        '[Note]將正規化(0至255)轉回實際電流量
        Dim RealmA As Integer
        Select Case mChannelNo
            Case enmValveLight.No1
                mUILightValue1 = value
                RealmA = Int(value * gLightCollection.Cards.Parameters(mItemNo).ChannelScale1)
            Case enmValveLight.No2
                mUILightValue2 = value
                RealmA = Int(value * gLightCollection.Cards.Parameters(mItemNo).ChannelScale2)
            Case enmValveLight.No3
                mUILightValue3 = value
                RealmA = Int(value * gLightCollection.Cards.Parameters(mItemNo).ChannelScale3)
            Case enmValveLight.No4
                mUILightValue4 = value
                RealmA = Int(value * gLightCollection.Cards.Parameters(mItemNo).ChannelScale4)
        End Select
        '____[Note]將正規化(0至255)轉回實際電流量

        'Debug.Print("SetCCDLight Channel:" & mChannelNo & "  UILightValue:" & value & "  RealmA:" & RealmA)

        Select Case gAOICollection.GetCCDType(ccdNo) '  gSSystemParameter.enmCCDType
            Case enmCCDType.OmronFZS2MTCP
                Call Items(mItemNo).SetLightValue(mChannelNo, RealmA)
            Case enmCCDType.OmronFZS2MUDP
                Call Items(mItemNo).SetLightValue(mChannelNo, RealmA)
            Case enmCCDType.KeyenceCV200CTCP
                Call gAOICollection.Items(0).CCDSendCommand(CType(gAOICollection.Items(0), CAOIKeyenceCV200CTCP).gCVX100Generator.SetCCDLightValue(RealmA))
            Case enmCCDType.CognexVPRO
                Call Items(mItemNo).SetLightValue(mChannelNo, RealmA, waitReturn)
        End Select

        'gSyslog.Save(gMsgHandler.GetMessage(INFO_6022011, value), "INFO_6022011") '新設定CCD光源1亮度:{0}
        Return True
    End Function

    Public Function GetRealmAtoUILightValue(ByVal RealmA As Integer, ByVal ChannelScale As Decimal, ByVal UILightValue As Integer) As Integer
        If Int(ChannelScale * UILightValue) = RealmA Then
            Return UILightValue
        Else
            Return 0
        End If

    End Function


    ''' <summary>
    ''' 光源亮度讀取命令
    ''' </summary>
    ''' <param name="ccdNo"></param>
    ''' <param name="enmLight"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCCDLight(ByVal ccdNo As Integer, ByVal enmLight As enmLight, ByRef value As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean
        Dim mItemNo As Integer = LightParam(enmLight).ItemNo
        Dim mChannelNo As Integer = LightParam(enmLight).ChannelNo
        Dim RealmA As Integer
        Dim mChannelScale As Decimal
        Dim UILightValue As Integer
        Select Case gAOICollection.GetCCDType(ccdNo)
            Case enmCCDType.CognexVPRO
                Items(mItemNo).GetLightValue(mChannelNo, RealmA, waitReturn)
        End Select

        '[Note]將實際電流量轉回正規化(0至255)
        Select Case mChannelNo
            Case enmValveLight.No1
                mChannelScale = gLightCollection.Cards.Parameters(mItemNo).ChannelScale1
                UILightValue = mUILightValue1
            Case enmValveLight.No2
                mChannelScale = gLightCollection.Cards.Parameters(mItemNo).ChannelScale2
                UILightValue = mUILightValue2
            Case enmValveLight.No3
                mChannelScale = gLightCollection.Cards.Parameters(mItemNo).ChannelScale3
                UILightValue = mUILightValue3
            Case enmValveLight.No4
                mChannelScale = gLightCollection.Cards.Parameters(mItemNo).ChannelScale4
                UILightValue = mUILightValue4
        End Select
        value = GetRealmAtoUILightValue(RealmA, mChannelScale, UILightValue)
        'Debug.Print("GetCCDLight Channel:" & mChannelNo & "  UILightValue:" & value & "  RealmA:" & RealmA)
        '____[Note]將實際電流量轉回正規化(0至255)

        Return True
    End Function

    ''' <summary>
    ''' 取得光源亮度值
    ''' </summary>
    ''' <param name="ccdNo"></param>
    ''' <param name="enmLight"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCCDLightValue(ByVal ccdNo As Integer, ByVal enmLight As enmLight) As Integer
        Dim mItemNo As Integer = LightParam(enmLight).ItemNo
        Dim mChannelNo As Integer = LightParam(enmLight).ChannelNo
        Select Case gAOICollection.GetCCDType(ccdNo)
            Case enmCCDType.CognexVPRO
                Return Items(mItemNo).Result(mChannelNo).Value

        End Select
        Return 0
    End Function

    ''' <summary>[取得通訊命令之狀態]</summary>
    ''' <param name="ccdNo"></param>
    ''' <param name="eLight"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResultStatus(ByVal ccdNo As Integer, ByVal eLight As enmLight) As Boolean
        Dim mItemNo As Integer = LightParam(eLight).ItemNo
        Dim mChannelNo As Integer = LightParam(eLight).ChannelNo
        Select Case gAOICollection.GetCCDType(ccdNo)
            Case enmCCDType.CognexVPRO
                Return Items(mItemNo).Result(mChannelNo).Status

        End Select
        Return False
    End Function

    Public Function IsBusy(ByVal ccdNo As Integer, ByVal eLight As enmLight) As Boolean
        Dim mItemNo As Integer = LightParam(eLight).ItemNo
        Select Case gAOICollection.GetCCDType(ccdNo)
            Case enmCCDType.CognexVPRO
                Return Items(mItemNo).IsBusy
        End Select
        Return False
    End Function

    Public Function IsTimeOut(ByVal ccdNo As Integer, ByVal eLight As enmLight) As Boolean
        Dim mItemNo As Integer = LightParam(eLight).ItemNo
        Select Case gAOICollection.GetCCDType(ccdNo)
            Case enmCCDType.CognexVPRO
                Return Items(mItemNo).IsTimeOut
        End Select
        Return False
    End Function


    Public Function GetCCDLightMaxValue(ByVal chNo As Integer, ByVal Light As enmValveLight) As Integer
        'gLightCollection.Cards.Parameters(mCardNo).ChannelMaxValue4  
        'Dim mItemNo As Integer = LightParam(chNo).ItemNo
        'Dim mItemNo As Integer = LightParam(chNo).ChannelNo
        Dim MaxValue As Integer = 0

        If chNo < 0 Then
            Return MaxValue
        End If
        Dim mItemNo As Integer = gLightCollection.LightParam(chNo).ItemNo

        Select Case Light
            Case enmValveLight.No1
                MaxValue = gLightCollection.Cards.Parameters(mItemNo).ChannelMaxValue1
            Case enmValveLight.No2
                MaxValue = gLightCollection.Cards.Parameters(mItemNo).ChannelMaxValue2
            Case enmValveLight.No3
                MaxValue = gLightCollection.Cards.Parameters(mItemNo).ChannelMaxValue3
            Case enmValveLight.No4
                MaxValue = gLightCollection.Cards.Parameters(mItemNo).ChannelMaxValue4
        End Select

        Return MaxValue
    End Function

    ''' <summary>光控器Mapping設定檔</summary>
    ''' <remarks></remarks>
    Dim LightParam(enmLight.Max) As sLightParam
    ''' <summary>光控器連線設定 (0):第一組(1):第二組 (2):第三組 (3):第四組</summary>
    ''' <remarks></remarks>
    Public Cards As New CLightCards

    ''' <summary>光控器物件集合</summary>
    ''' <remarks></remarks>
    Public Items As New List(Of ILightInterface)

    ''' <summary>初始化光控器</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initialize_Light() As Boolean
        'Dim blnRet As Boolean
        Cards.LoadLightControlParameter(Application.StartupPath & "\System\" & MachineName & "\CardLightControl.ini") '讀取程控光源設定值
        Try
            Dim isInitialOK As Boolean = True
            For mItemNo As Integer = 0 To Cards.Count - 1
                Select Case Cards(mItemNo).CardType
                    Case enmProgramLightType.None
                        Items.Add(New CLight_None)
                        With Me.Cards(mItemNo)
                            If Items(Items.Count - 1).Initial(.PortName, .BaudRate, .DataBit) = False Then
                                gEqpMsg.AddHistoryAlarm("Error_1022000", "Initial Light", 0, gMsgHandler.GetMessage(Error_1022000), eMessageLevel.Error) '程控光源1初始化失敗!
                                gSyslog.Save("COM Port: " & Me.Cards(0).PortName)
                                isInitialOK = False
                            End If
                        End With
                    Case enmProgramLightType.GLCTD12V30W
                        Items.Add(New CLightGLC_PD)
                        With Me.Cards(mItemNo)
                            If Items(Items.Count - 1).Initial(.PortName, .BaudRate, .DataBit) = False Then
                                gEqpMsg.AddHistoryAlarm("Error_1022000", "Initial Light", 0, gMsgHandler.GetMessage(Error_1022000), eMessageLevel.Error) '程控光源1初始化失敗!
                                gSyslog.Save("COM Port: " & Me.Cards(0).PortName)
                                isInitialOK = False
                            End If
                        End With

                    Case enmProgramLightType.CTK_P_RS
                        Items.Add(New CLight_CTK_P_RS)
                        With Me.Cards(mItemNo)
                            If Items(Items.Count - 1).Initial(.PortName, .BaudRate, .DataBit) = False Then
                                gEqpMsg.AddHistoryAlarm("Error_1022000", "Initial Light", 0, gMsgHandler.GetMessage(Error_1022000), eMessageLevel.Error) '程控光源1初始化失敗!
                                gSyslog.Save("COM Port: " & Me.Cards(0).PortName)
                                isInitialOK = False
                            End If
                        End With
                    Case enmProgramLightType.OPT_DP1024_4
                        Items.Add(New CLight_OPT_DP1024_4)
                        With Me.Cards(mItemNo)
                            If Items(Items.Count - 1).Initial(.PortName, .BaudRate, .DataBit) = False Then
                                gEqpMsg.AddHistoryAlarm("Error_1022000", "Initial Light", 0, gMsgHandler.GetMessage(Error_1022000), eMessageLevel.Error) '程控光源1初始化失敗!
                                gSyslog.Save("COM Port: " & Me.Cards(0).PortName)
                                isInitialOK = False
                            End If
                        End With
                End Select
            Next
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    LightParam(enmLight.No1).ItemNo = 0
                    LightParam(enmLight.No1).ChannelNo = 0
                    LightParam(enmLight.No2).ItemNo = 0
                    LightParam(enmLight.No2).ChannelNo = 1
                    LightParam(enmLight.No3).ItemNo = 0
                    LightParam(enmLight.No3).ChannelNo = 2
                    LightParam(enmLight.No4).ItemNo = 0
                    LightParam(enmLight.No4).ChannelNo = 3
                    LightParam(enmLight.No5).ItemNo = 1
                    LightParam(enmLight.No5).ChannelNo = 0
                    LightParam(enmLight.No6).ItemNo = 1
                    LightParam(enmLight.No6).ChannelNo = 1
                    LightParam(enmLight.No7).ItemNo = 1
                    LightParam(enmLight.No7).ChannelNo = 2
                    LightParam(enmLight.No8).ItemNo = 1
                    LightParam(enmLight.No8).ChannelNo = 3

                Case enmMachineType.eDTS_2S2V
                    LightParam(enmLight.No1).ItemNo = 0
                    LightParam(enmLight.No1).ChannelNo = 0
                    LightParam(enmLight.No2).ItemNo = 0
                    LightParam(enmLight.No2).ChannelNo = 1
                    LightParam(enmLight.No3).ItemNo = 0
                    LightParam(enmLight.No3).ChannelNo = 2
                    LightParam(enmLight.No4).ItemNo = 0
                    LightParam(enmLight.No4).ChannelNo = 3

                Case Else
                    LightParam(enmLight.No1).ItemNo = 0
                    LightParam(enmLight.No1).ChannelNo = 0
                    LightParam(enmLight.No2).ItemNo = 0
                    LightParam(enmLight.No2).ChannelNo = 1
                    LightParam(enmLight.No3).ItemNo = 0
                    LightParam(enmLight.No3).ChannelNo = 2
                    LightParam(enmLight.No4).ItemNo = 0
                    LightParam(enmLight.No4).ChannelNo = 3

            End Select
            If isInitialOK = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            gEqpMsg.AddHistoryAlarm("Error_1022000", "Initial Light", 0, gMsgHandler.GetMessage(Error_1022000), eMessageLevel.Error) '程控光源1初始化失敗!
            gSyslog.Save("COM Port: " & Me.Cards(0).PortName)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            Return False
        End Try

    End Function


    Sub SetLightOnOff(ByVal light As enmLight, ByVal value As Boolean)
        Select Case light
            Case enmLight.No1
                gDOCollection.SetState(enmDO.CCDLight, value)
            Case enmLight.No2
                gDOCollection.SetState(enmDO.CCDLight2, value)
            Case enmLight.No3
                gDOCollection.SetState(enmDO.CCDLight3, value)
            Case enmLight.No4
                gDOCollection.SetState(enmDO.CCDLight4, value)
            Case enmLight.No5
                gDOCollection.SetState(enmDO.CCDLight5, value)
            Case enmLight.No6
                gDOCollection.SetState(enmDO.CCDLight6, value)
            Case enmLight.No7
                gDOCollection.SetState(enmDO.CCDLight7, value)
            Case enmLight.No8
                gDOCollection.SetState(enmDO.CCDLight8, value)
        End Select

    End Sub


    Public Function Close() As Boolean
        For mItemNo As Integer = 0 To Items.Count - 1
            Items(mItemNo).Close()
        Next
        Return True
    End Function


End Class

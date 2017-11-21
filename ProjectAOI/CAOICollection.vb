Imports ProjectMotion
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectCore
Imports Cognex
Imports Cognex.VisionPro
Imports Cognex.VisionPro.PMAlign

Imports System.Threading


''' <summary>CCD通訊參數</summary>
''' <remarks></remarks>
Public Structure sCCDConnectionParameter
    ''' <summary>
    ''' CCD控制器/函式庫類型
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDType As enmCCDType
    ''' <summary>Omron連線設定</summary>
    ''' <remarks></remarks>
    Public Omron As sOmronConnect
    ''' <summary>Keyence連線設定</summary>
    ''' <remarks></remarks>
    Public Keyence As sKeyenceConnect
    ''' <summary>Cognex連線設定</summary>
    ''' <remarks></remarks>
    Public Cognex As sCognexVProConnect

    ''' <summary>複製自</summary>
    ''' <param name="from"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyFrom(ByVal from As sCCDConnectionParameter) As Boolean
        Me.CCDType = from.CCDType
        Me.Omron.RecieveIPAddress = from.Omron.RecieveIPAddress
        Me.Omron.RecievePort = from.Omron.RecievePort
        Me.Omron.SendIPAddress = from.Omron.SendIPAddress
        Me.Omron.SendPort = from.Omron.SendPort
        Me.Keyence.SendIPAddress = from.Keyence.SendIPAddress
        Me.Keyence.SendPort = from.Keyence.SendPort
        Me.Cognex.CCDNo = from.Cognex.CCDNo
        Me.Cognex.ShortFileName = from.Cognex.ShortFileName
        Me.Cognex.TriggerType = from.Cognex.TriggerType

        Return True
    End Function
End Structure



''' <summary>AOI取像參數設定</summary>
''' <remarks></remarks>
Public Structure sAOIAcquisitionParameter
    ''' <summary>
    ''' 預設曝光時間
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDExposureTime As Decimal
    ''' <summary>實體CCD通道編號</summary>
    ''' <remarks></remarks>
    Public ChannelNo As Integer
    ''' <summary>絕對卡號/ 虛擬CCD對應實體CCD 票券</summary>
    ''' <remarks></remarks>
    Public ItemNo As Integer
    ''' <summary> 影像寬度 </summary>
    ''' <remarks></remarks>
    Public ImageWidth As Integer
    ''' <summary>影像高度</summary>
    ''' <remarks></remarks>
    Public ImageHeight As Integer
End Structure

Public Class CAOICollection

#Region "Event Wrapper"

    Public Event OnRunSuccessed(sender As Object, e As AOIEventArgs)

    ''' <summary>處理成功事件</summary>
    ''' <remarks></remarks>
    Sub Align_OnRunSuccessed(sender As CThreadCogToolBlock, ByVal e As AOIEventArgs)
        RaiseEvent OnRunSuccessed(sender, e)
    End Sub
#End Region

#Region "交握旗標"
    ''' <summary>
    ''' [資料接收中]
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsCCDReceiveBusy(ByVal ccdNo As Integer) As Boolean
        With Items(Channel(ccdNo).ItemNo)
            Return .IsCCDReceiveBusy(Channel(ccdNo).ChannelNo)
        End With
    End Function
    ''' <summary>
    ''' [CCD Handshake(接收到的資料格式正確)回應成功 為true, 失敗為false]
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsCCDReceivedDataOK(ByVal ccdNo As Integer) As Boolean
        With Items(Channel(ccdNo).ItemNo)
            Return .IsCCDReceivedDataOK(Channel(ccdNo).ChannelNo)
        End With
    End Function
    ''' <summary>
    ''' [CCD取像後是否設定下一組場景參數]
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsCCDSetNextScene(ByVal ccdNo As Integer) As Boolean
        With Items(Channel(ccdNo).ItemNo)
            Return .IsCCDSetNextScene(Channel(ccdNo).ChannelNo)
        End With
    End Function

    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsIntialOK As Boolean
        Get
            Return mIsIntialOK
        End Get
    End Property

    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Private mIsIntialOK As Boolean

#End Region

#Region "Cards Operation"
    ''' <summary>CCD配接參數(索引是卡片/IP連線設定)</summary>
    ''' <remarks></remarks>
    Dim Cards As New List(Of sCCDConnectionParameter)
    ''' <summary>取得enmCCD的CCD類型</summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCCDType(ByVal CCDNo As Integer) As enmCCDType
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return enmCCDType.None
        End If
        Dim mItemNo As Integer = Channel(CCDNo).ItemNo
        Return Cards(mItemNo).CCDType
    End Function
    ''' <summary>
    ''' 陣列複製自
    ''' </summary>
    ''' <param name="array"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ArrayCopyFrom(ByRef array() As sCCDConnectionParameter) As Boolean
        For mCardNo As Integer = 0 To Me.Cards.Count - 1
            If mCardNo < array.Count Then
                'Me.Cards(mCardNo).CopyFrom(array(mCardNo))
                Me.Cards(mCardNo) = array(mCardNo)
            End If
        Next
        Return True
    End Function
    Public Function ArrayCopyTo(ByRef array() As sCCDConnectionParameter) As Boolean
        ReDim array(Me.Cards.Count - 1)
        For mCardNo As Integer = 0 To Me.Cards.Count - 1
            array(mCardNo) = Me.Cards(mCardNo)
        Next
        Return True
    End Function


#End Region

#Region "Scene Operation"
    ''' <summary>各場景取像參數</summary>
    ''' <remarks></remarks>
    Public SceneDictionary As New Dictionary(Of String, CSceneParameter)
    ''' <summary>儲存AOI光源/曝光參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSceneParameter(ByVal sceneName As String, ByVal fileName As String) As Boolean

        Try
            'For mCCDNo As Integer = 0 To Channel.Count - 1
            Dim strSection As String '= "SceneList"
            'SaveIniString(strSection, "Count", SceneDictionary.Count, fileName)
            'For mSceneNo As Integer = 0 To SceneDictionary.Count - 1
            '    SaveIniString(strSection, "Scene" & (mSceneNo + 1).ToString, SceneDictionary.Keys(mSceneNo), fileName)
            'Next
            'For mSceneNo As Integer = 0 To SceneDictionary.Count - 1
            Dim mCCDNo As Integer = 0
            strSection = "CCD" & (mCCDNo + 1).ToString
            SceneDictionary(sceneName).Save(fileName, strSection)
            'Next
            'Next

            Return True
        Catch ex As Exception
            gSyslog.Save("SaveSceneParameter Error. Exception Message:" & ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try

    End Function

    ''' <summary>讀取AOI光源/曝光參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadSceneParameter(ByVal sceneName As String, ByVal fileName As String) As Boolean
        Try

            Dim strSection As String '= "SceneList"
            'Dim mCount As Integer = Val(ReadIniString(strSection, "Count", fileName, 0))
            'SceneDictionary.Clear()
            Dim mCCDNo As Integer = 0
            'For mCCDNo As Integer = 0 To Channel.Count - 1

            strSection = "CCD" & (mCCDNo + 1).ToString

            Dim mScene As New CSceneParameter

            mScene.Load(fileName, strSection)
            If SceneDictionary.ContainsKey(sceneName) Then
                SceneDictionary(sceneName) = mScene
            Else
                SceneDictionary.Add(sceneName, mScene)
            End If

            'Next


            Return True
        Catch ex As Exception
            gSyslog.Save("LoadSceneParameter Error. Exception Message:" & ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 取得場景清單
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSceneList(ByVal CCDNo As Integer) As String()
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return Nothing
        End If

        Dim mItemNo As Integer = Channel(CCDNo).ItemNo '取得ItemNo
        Return Items(mItemNo).GetSceneList
    End Function

#End Region
    ''' <summary>各CCD 取像參數(索引是enmCCD)</summary>
    ''' <remarks></remarks>
    Dim Channel(enmCCD.ConstMax) As sAOIAcquisitionParameter


    ''' <summary>CCD集合</summary>
    ''' <remarks></remarks>
    Public Items As New List(Of IAOIInterface)
    Public ErrMessage As String = ""


    ''' <summary>
    ''' 錯誤訊息
    ''' </summary>
    ''' <param name="CCDNo">enmCCD</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorMessage(ByVal CCDNo As Integer) As String
        Get
            If CCDNo < 0 Then
                Return ""
            End If
            If CCDNo >= Items.Count Then
                Return ""
            End If
            Return Items(CCDNo).ErrorMessage
        End Get
        Set(value As String)
            If CCDNo < 0 Then
                Return
            End If
            If CCDNo >= Items.Count Then
                Return
            End If
            Items(CCDNo).ErrorMessage = value
        End Set
    End Property

#Region "初始化/解除"
    Public Function Initial() As Boolean
        Try
            '=== Card等級存取 ===
            Me.LoadCCDConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardCCD.ini")
            Me.SaveCCDConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardCCD.ini")
            '=== Card等級存取 ===

            If Me.Initial(Cards) = False Then
                gSyslog.Save(gMsgHandler.GetMessage(Error_1012000), "Error_1012000", eMessageLevel.Error)
                gSyslog.Save("Error Message:" & ErrMessage, , eMessageLevel.Error)
                Return False
            End If

            '=== Chanel等級存取 ===
            Me.LoadChannelParameter(Application.StartupPath & "\System\" & MachineName & "\ConfigCCD.ini") '讀取CCD通道設定
            Me.SaveChannelParameter(Application.StartupPath & "\System\" & MachineName & "\ConfigCCD.ini")
            '=== Chanel等級存取 ===


            'Dim fileName As String
            'For mCCDNo As Integer = enmCCD.CCD1 To Channel.Count - 1
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\CALIB" & (mCCDNo + 1).ToString & ".vpp"
            '    If System.IO.File.Exists(fileName) Then
            '        LoadVision(mCCDNo, fileName)
            '    End If
            'Next
            'For mCCDNo As Integer = enmCCD.CCD1 To Channel.Count - 1
            '    '平台驗證
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\CALIBSVT" & (mCCDNo + 1).ToString & ".vpp"
            '    If System.IO.File.Exists(fileName) Then
            '        LoadVision(mCCDNo, fileName)
            '    End If
            '    '測量工具
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\CALIBMR" & (mCCDNo + 1).ToString & ".vpp"
            '    If System.IO.File.Exists(fileName) Then
            '        LoadVision(mCCDNo, fileName)
            '    End If
            'Next
            'For mCCDNo As Integer = enmCCD.CCD1 To Channel.Count - 1
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\CALIBScene" & (mCCDNo + 1).ToString & ".vpp"
            '    If System.IO.File.Exists(fileName) Then
            '        LoadVision(mCCDNo, fileName)
            '    End If
            '    '機構特徵點教導
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\CALIBMark" & (mCCDNo + 1).ToString & ".vpp"
            '    If System.IO.File.Exists(fileName) Then
            '        LoadVision(mCCDNo, fileName)
            '    End If
            '    '點膠閥頭特徵點教導
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\CALIBValve" & (mCCDNo + 1).ToString & ".vpp"
            '    If System.IO.File.Exists(fileName) Then
            '        LoadVision(mCCDNo, fileName)
            '    End If
            'Next
            '載入工程模式下使用的場景
            Dim fileName As String
            'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
            'Dim SysSceneList() As String = gAOICollection.GetSysSceneList
            Dim SysSceneList() As String = GetSysSceneList()
            'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
            For i As Integer = 0 To SysSceneList.Length - 1
                For mCCDNo As Integer = enmCCD.CCD1 To Channel.Count - 1
                    fileName = Application.StartupPath & "\System\" & MachineName & "\" & SysSceneList(i) & (mCCDNo + 1).ToString & ".vpp"
                    If System.IO.File.Exists(fileName) Then
                        LoadVision(mCCDNo, fileName)
                    End If
                    fileName = Application.StartupPath & "\System\" & MachineName & "\" & SysSceneList(i) & (mCCDNo + 1).ToString & ".ini"
                    If System.IO.File.Exists(fileName) Then
                        LoadSceneParameter(SysSceneList(i) & (mCCDNo + 1).ToString, fileName)
                    End If
                Next
            Next
            For mCCDNo As Integer = 0 To Channel.Count - 1
                SetAutoExposure(mCCDNo, False) '關閉自動曝光
                SetExposure(mCCDNo, Me.Channel(mCCDNo).CCDExposureTime) '[Note]根據system來決定曝光時間  / 2017.05.05

                gCCDAlignResultDict(mCCDNo) = New Dictionary(Of Integer, sAlignPos)()
            Next

            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1012000), "Error_1012000", eMessageLevel.Error)
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function


    ''' <summary>初始化</summary>
    ''' <param name="cards"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal cards As List(Of sCCDConnectionParameter)) As Boolean
        If gSSystemParameter.RunMode = enmRunMode.Simulation Then
            Return True
        End If

        For mCardNo As Integer = 0 To cards.Count - 1
            Select Case cards(mCardNo).CCDType
                Case enmCCDType.CognexVPRO
                    Items.Add(New CAOICognexVPRO)
                    If Items(Items.Count - 1).Initial(cards(mCardNo)) = False Then
                        ErrMessage += "" & GetCCDTypeString(cards(mCardNo).CCDType) & "初始化失敗."
                    End If
                    AddHandler Items(Items.Count - 1).OnRunSuccessed, AddressOf Align_OnRunSuccessed
                Case enmCCDType.KeyenceCV200CTCP
                    Items.Add(New CAOIKeyenceCV200CTCP)
                    If Items(Items.Count - 1).Initial(cards(mCardNo)) = False Then
                        ErrMessage += "" & GetCCDTypeString(cards(mCardNo).CCDType) & "初始化失敗."
                    End If
                Case enmCCDType.OmronFZS2MTCP
                    Items.Add(New CAOIOmronFZS2MTCP)
                    If Items(Items.Count - 1).Initial(cards(mCardNo)) = False Then
                        ErrMessage += "" & GetCCDTypeString(cards(mCardNo).CCDType) & "初始化失敗."
                    End If
                Case enmCCDType.OmronFZS2MUDP
                    Items.Add(New CAOIOmronFZS2MUDP)
                    If Items(Items.Count - 1).Initial(cards(mCardNo)) = False Then
                        ErrMessage += "" & GetCCDTypeString(cards(mCardNo).CCDType) & "初始化失敗."
                    End If

            End Select
        Next
        If ErrMessage <> "" Then
            mIsIntialOK = False
            gSyslog.Save(gMsgHandler.GetMessage(Error_1012000) & ErrMessage, "Error_1012000", eMessageLevel.Error) 'CCD1初始化失敗!
            MsgBox(gMsgHandler.GetMessage(Error_1012000) & ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CAOICollection@Initial") 'CCD1初始化失敗!
            Return False
        Else
            mIsIntialOK = True
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6000046), "INFO_6000046") 'CCD1初始化完成.
            Return True
        End If
    End Function


    Public Function Close() As Boolean
        Try
            For mCardNo As Integer = 0 To Items.Count - 1
                Items(mCardNo).Close()
            Next
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6000047), "INFO_6000047") 'CCD1關閉成功.
            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1012001), "Error_1012001", eMessageLevel.Error) 'CCD1關閉失敗!
            MsgBox(gMsgHandler.GetMessage(Error_1012001), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) 'CCD1關閉失敗!
            Return False
        End Try

    End Function

#End Region

#Region "判定"
    ''' <summary>是否索引超出範圍</summary>
    ''' <param name="ccdNO">enmCCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsIndexOutOfRange(ByVal CCDNo As Integer) As Boolean
        If CCDNo < 0 Then 'CCD索引不存在
            Return True
        End If
        If CCDNo >= Channel.Count Then 'CCD索引不存在
            Return True
        End If
        If Channel(CCDNo).ItemNo < 0 Then '指定Item不存在
            Return True
        End If
        If Channel(CCDNo).ItemNo >= Items.Count Then '指定Item不存在
            Return True
        End If
        Return False
    End Function



    ''' <summary>CCD計算->取像完成</summary>
    ''' <param name="CCDNo">enmCCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDReady(ByVal CCDNo As Integer) As Boolean
        If gSSystemParameter.IsBypassCCD = True Then
            Return True
        End If
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If

        With Channel(CCDNo)
            Return Items(.ItemNo).IsCCDReady(.ChannelNo)
        End With
    End Function

    ''' <summary>CCD忙碌中</summary>
    ''' <param name="CCDNo">enmCCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDCBusy(ByVal CCDNo As Integer) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).IsCCDCBusy(.ChannelNo)
        End With
    End Function

    ''' <summary>CCD結果不良</summary>
    ''' <param name="CCDNo">enmCCD</param>
    ''' <param name="ticket"></param>
    ''' <param name="defaultValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDResultNG(ByVal CCDNo As Integer, ByVal ticket As Integer, ByVal defaultValue As Boolean) As Boolean
        If gSSystemParameter.IsBypassCCD = True Then '傳回預設值
            Return defaultValue
        End If
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).IsCCDResultNG(ticket, CCDNo)
        End With
    End Function

    ''' <summary>
    ''' CCD逾時
    ''' </summary>
    ''' <param name="CCDNo">enmCCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDTimeOut(ByVal CCDNo As Integer) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).IsCCDTimeOut
        End With
    End Function

    ''' <summary>取得連線狀態</summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStatus(ByVal CCDNo As Integer) As SockStatusInfo
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return SockStatusInfo.sckDisconnect
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SockStatus
        End With
    End Function
#End Region

#Region "Scene Operation"
    ''' <summary>
    ''' CCD搭配場景是否存在
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="SceneName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsSceneExist(ByVal CCDNo As Integer, ByVal SceneName As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then
            Return False
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).IsSceneExist(SceneName)
        End With
    End Function

    ''' <summary>建立場景物件</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="SceneName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateScene(ByVal CCDNo As Integer, ByVal sceneName As String, Optional ByVal mAlignType As eAlignType = eAlignType.PMAlign, Optional mScenePath As String = "") As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).CreateScene(sceneName, mAlignType, mScenePath)
        End With
    End Function

    ''' <summary>
    ''' 移除場景物件
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="sceneName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RemoveScene(ByVal CCDNo As Integer, ByVal sceneName As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).RemoveScene(sceneName)
        End With
    End Function

    ''' <summary>
    ''' 設定場景輸入影像
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="SceneName"></param>
    ''' <param name="image"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSceneInputImage(ByVal CCDNo As Integer, ByVal SceneName As String, ByRef image As Object) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetSceneInputImage(SceneName, image)
        End With
    End Function

    ''' <summary>儲存場景輸出設定</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="sceneName"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSceneOutputParam(ByVal CCDNo As Integer, ByVal sceneName As String, ByVal fileName As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SaveSceneOutputParam(sceneName, fileName)
        End With
    End Function

    ''' <summary>設定要執行的場景</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="ScenceName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCCDScene(ByVal CCDNo As Integer, ByVal ScenceName As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        If ScenceName Is Nothing Then
            Return False
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetCCDScene(.ChannelNo, ScenceName)
        End With
    End Function
#End Region
#Region "取像動作"
    ''' <summary>
    ''' 取得取像工具
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAcqTool(ByVal CCDNo As Integer) As Object
        If IsIndexOutOfRange(CCDNo) Then
            Return Nothing
        End If
        Return Items(Channel(CCDNo).ItemNo).GetAcqTool  '取出AcqFifoTool/HFrameGrabber工具...etc
    End Function
    ''' <summary>
    ''' CCD是否存在
    ''' </summary>
    ''' <param name="CCDNo">enmCCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDExist(ByVal CCDNo As Integer) As Boolean
        If IsIndexOutOfRange(CCDNo) Then
            Return False
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).IsCCDExist
        End With
    End Function

    Public Function SetHardwareTrigger(ByVal CCDNo As Integer, ByVal state As Boolean) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If

        With Channel(CCDNo)
            Return Items(.ItemNo).SetHardwareTrigger(state)
        End With
    End Function

    Public Function SetIsStop(ByVal CCDNo As Integer, ByVal state As Boolean) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If

        With Channel(CCDNo)
            Return Items(.ItemNo).SetIsStop(state)
        End With
    End Function

    Public Function SetIsAcqImageOnly(ByVal CCDNo As Integer, ByVal state As Boolean) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If

        With Channel(CCDNo)
            Return Items(.ItemNo).SetIsAcqImageOnly(state)
        End With
    End Function

    ''' <summary>設定曝光時間</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="exposure_ms"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetExposure(ByVal CCDNo As Integer, ByVal exposure_ms As Double) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        '[Note]固定機台曝光時間(飛拍考量),依光源不同而有所不同 2017/03/07
        'Select Case gSSystemParameter.MachineType
        '    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V
        '        exposure_ms = 10
        '    Case enmMachineType.DCS_F230A
        '        exposure_ms = 10
        '    Case enmMachineType.DCS_350A
        '        exposure_ms = 0.3
        '    Case Else
        'End Select


        With Channel(CCDNo)
            Return Items(.ItemNo).SetExposure(.ChannelNo, exposure_ms)
        End With
    End Function

    Public Function GetExposure(ByVal CCDNo As Integer) As Double
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetExposure(.ChannelNo)
        End With
    End Function
    ''' <summary>
    ''' 設定自動白平衡
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="enable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAutoWhiteBalance(ByVal CCDNo As Integer, ByVal enable As Boolean) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetAutoWhiteBalance(.ChannelNo, enable)
        End With
    End Function
    Public Function GetCCDFormatsType(ByVal CCDNo As Integer) As Object
        If IsIndexOutOfRange(CCDNo) Then
            Return Nothing
        End If

        Select Case Cards(CCDNo).CCDType
            Case enmCCDType.CognexVPRO
                Return Cards(CCDNo).Cognex.VideoFormatType
            Case enmCCDType.KeyenceCV200CTCP
                Return enmVideoFormatType.Mono
            Case enmCCDType.OmronFZS2MTCP
                Return enmVideoFormatType.Mono
            Case enmCCDType.OmronFZS2MUDP
                Return enmVideoFormatType.Mono
            Case Else
                Return enmVideoFormatType.None
        End Select

    End Function
    ''' <summary>
    ''' 設定自動曝光
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="enable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAutoExposure(ByVal CCDNo As Integer, ByVal enable As Boolean) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetAutoExposure(.ChannelNo, enable)
        End With
    End Function
    ''' <summary>
    ''' 設定期盤格較準
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="Train"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalibBoardCalibration(ByVal CCDNo As Integer, ByVal InputImage As Object, ByVal Train As Boolean, ByVal TileSize As Decimal) As Object
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).CalibBoardCalibration(.ChannelNo, InputImage, Train, TileSize)
        End With
    End Function

    ''' <summary>[清空Ticket]</summary>
    ''' <param name="cCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ClearTicket(ByVal cCDNo As Integer) As Boolean
        If IsIndexOutOfRange(cCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(cCDNo)
            Return Items(.ItemNo).ClearTicket
        End With

    End Function

    Public Function SetLiveTriggerMode(ByVal CCDNo As Integer, ByVal Trigger As eTriggerType) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetLiveTriggerMode(.ChannelNo, Trigger)
        End With
    End Function

    ''' <summary>CCD觸發</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="OnOff"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCCDTrigger(ByVal CCDNo As Integer, ByVal OnOff As enmONOFF, ByVal isAcqImageOnly As Boolean, ByVal isSetNextScene As Boolean) As Integer
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        'Debug.Print("SetCCDTrigger CCD" & (CCDNo + 1).ToString & " " & OnOff & " @" & Channel(CCDNo).ItemNo)
        With Channel(CCDNo)
            Return Items(.ItemNo).SetCCDTrigger(.ChannelNo, OnOff, isAcqImageOnly, isSetNextScene)
        End With
    End Function



    Private mTaskTokenSource As CancellationTokenSource = New CancellationTokenSource()

    Private mTaskToken As CancellationToken

    Private mRunLongProcessTask As Task(Of Integer) = Nothing

    Private Async Sub AlignFunctoin(ByVal mCCDNo As Integer, ByVal mTicket As Integer, ByVal mScene As String)

        mTaskTokenSource = New CancellationTokenSource()
        mTaskToken = mTaskTokenSource.Token
        'Dim sSelectedItem As String = cmbBox.SelectedItem.ToString()
        mRunLongProcessTask = Task.Run(Function()
                                           'System.Threading.Thread.CurrentThread.Join(300) '防連按測試
                                           'gAOICollection.SetCCDScene(mCCDNo, mScene) '選擇場景
                                           'Dim ticket As Integer = 0
                                           'gAOICollection.SetCCDTrigger(mCCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                                           'System.Threading.Thread.Sleep(10)
                                           'ticket = gAOICollection.SetCCDTrigger(mCCDNo, enmONOFF.eON, False, False) '觸發拍照開
                                           'System.Threading.Thread.Sleep(10)
                                           'gAOICollection.SetCCDTrigger(mCCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                                           Dim stopWatch As New Stopwatch
                                           stopWatch.Start()
                                           Do
                                               If (mTaskToken.IsCancellationRequested) Then
                                                   Return 0
                                               End If
                                               If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                                   Return 1
                                               End If
                                               Threading.Thread.Sleep(1)
                                           Loop Until gAOICollection.IsCCDCBusy(mCCDNo) = False
                                           '-----------------------------------------------
                                           'mButton.Invoke(Sub()
                                           '                   'Live View輔助線
                                           '                   Dim AssistLinePixelX As Integer
                                           '                   Dim AssistLinePixelY As Integer
                                           '                   If gSSystemParameter.CCDScaleX2X(mCCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(mCCDNo) = 0 Then
                                           '                       AssistLinePixelX = 0
                                           '                       AssistLinePixelY = 0
                                           '                   Else
                                           '                       AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(mCCDNo)
                                           '                       AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(mCCDNo)
                                           '                   End If
                                           '                   InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sSelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體
                                           '               End Sub)
                                           '-----------------------------------------------
                                           stopWatch.Restart()
                                           Do

                                               If (mTaskToken.IsCancellationRequested) Then
                                                   Return 0
                                               End If

                                               If gCCDAlignResultDict(mCCDNo).ContainsKey(mTicket) Then
                                                   Exit Do
                                               End If
                                               If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                                   Return 2
                                               End If
                                               Threading.Thread.Sleep(1)
                                           Loop
                                           Threading.Thread.CurrentThread.Join(100) '避免立刻取的判定錯誤
                                           If gCCDAlignResultDict(mCCDNo)(mTicket).Result.Count = 0 Then '結果必須存在
                                               Return 3
                                           End If

                                           Return 0
                                       End Function, mTaskToken)
        Await mRunLongProcessTask
        Select Case mRunLongProcessTask.Result
            Case 0 'Success
            Case 1
                Select Case mCCDNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
            Case 2
                'CCD 計算TimeOut
                Select Case mCCDNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
            Case 3
                'CCD 找不到特徵點(等於0)
                Select Case mCCDNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012103))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012103), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012403))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012403), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012703))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012703), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2013003))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2013003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
        End Select


    End Sub

    Public Async Function AcquirePicture(ByVal Sys As sSysParam, ByVal mCCDNo As Integer, ByVal mScene As String) As Task(Of Boolean)
        If Not gAOICollection.IsCCDExist(mCCDNo) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000034))
            MsgBox(gMsgHandler.GetMessage(Warn_3000034), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If mScene Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If


        Await Task.Run(Sub()
                           Try
                               gAOICollection.SetCCDScene(mCCDNo, mScene)
                               'gAOICollection.SetExposure(mCCDNo, nmcExposure.Value) '設定曝光值

                               gAOICollection.SetCCDTrigger(mCCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(mCCDNo, enmONOFF.eON, True, False) '觸發拍照開  
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(mCCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保

                               Dim stopWatch As New Stopwatch
                               stopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                       'CCD 取像TimeOut
                                       Select Case mCCDNo
                                           Case 0
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 1
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 2
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 3
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       End Select
                                       Exit Sub
                                   End If
                                   '[Note] EMO時跳出
                                   If gDICollection.GetState(enmDI.EMO) = True Then
                                       Exit Sub
                                   Else
                                       If Sys.MachineNo = enmMachineStation.MachineA Then
                                           If gDICollection.GetState(enmDI.EMS) = True Then
                                               Exit Sub
                                           End If
                                       ElseIf Sys.MachineNo = enmMachineStation.MachineB Then
                                           If gDICollection.GetState(enmDI.EMS2) = True Then
                                               Exit Sub
                                           End If
                                       End If
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False

                               'If CogToolBlockEditV21.Subject Is Nothing Then
                               '    '工具不存在
                               '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                               '    MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               '    Exit Sub
                               'End If
                               'If CogToolBlockEditV21.Subject.Inputs.Count = 0 Then
                               '    '工具輸入不存在
                               '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                               '    MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               '    Exit Sub
                               'End If
                               'If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then
                               '    '工具輸入影像不存在
                               '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                               '    MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               '    Exit Sub
                               'End If
                               'CogToolBlockEditV21.Subject.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo)
                           Catch ex As Exception
                               MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                           End Try
                       End Sub)

        Return True
    End Function

    Public Function TakePicture(ByVal CCDNo As Integer, ByVal Scene As String, ByVal mButton As Button, ByRef Result As sAlignResult) As Boolean

        If IsCCDExist(CCDNo) = False Then
            MsgBox("Acquisition Object Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        Dim ticket As Integer = 0
        'Dim timeOutStopWatch As New Stopwatch '逾時計時器
        Dim mIsError As Boolean = False

        SetCCDScene(CCDNo, Scene) '選擇場景
        SetCCDTrigger(CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
        System.Threading.Thread.CurrentThread.Join(10)
        ticket = SetCCDTrigger(CCDNo, enmONOFF.eON, False, False) '觸發拍照開
        System.Threading.Thread.CurrentThread.Join(10)
        SetCCDTrigger(CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
        'timeOutStopWatch.Restart()

        AlignFunctoin(CCDNo, ticket, Scene)
        'Do
        '    Application.DoEvents()
        '    If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
        '        'CCD 取像TimeOut
        '        Select Case CCDNo
        '            Case 0
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 1
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 2
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 3
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        End Select
        '        Return False
        '    End If
        'Loop Until IsCCDCBusy(CCDNo) = False
        'Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)
        'timeOutStopWatch.Restart()

        'Do
        '    Application.DoEvents()
        '    If gCCDAlignResultDict(CCDNo).ContainsKey(ticket) Then
        '        If gCCDAlignResultDict(CCDNo)(ticket).IsRunSuccess = True Then
        '            Exit Do
        '        End If
        '    End If
        '    If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
        '        'CCD 計算TimeOut
        '        Select Case CCDNo
        '            Case 0
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 1
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 2
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 3
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        End Select
        '        Return False
        '    End If
        'Loop Until False

        If gCCDAlignResultDict(CCDNo)(ticket).Result.Count = 0 Then
            '[Note]CCD 找不到特徵點(等於0)
            Return False
        End If
        '[Note]CCD 回傳特徵點結果
        Result = gCCDAlignResultDict(CCDNo)(ticket).Result(0)

        Return True
    End Function

    ''' <summary>
    ''' 取像
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AcquireImage(ByVal CCDNo As Integer) As Boolean
        If IsIndexOutOfRange(CCDNo) Then
            Return False
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).AcquireImage
        End With
    End Function

    Public Function GetAcqOutputImage(ByVal CCDNo As Integer) As Object
        If IsIndexOutOfRange(CCDNo) Then
            Return Nothing
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetAcqOutputImage
        End With
    End Function
#End Region

#Region "計算操作"

    ''' <summary>
    ''' 取得工具包
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="sceneName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetToolBlock(ByVal CCDNo As Integer, ByVal sceneName As String) As Object
        If IsIndexOutOfRange(CCDNo) Then
            Return Nothing
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetToolBlock(sceneName)
        End With
    End Function

    ''' <summary>
    ''' 取得定位偏移
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="ticket"></param>
    ''' <param name="alignResult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignOffset(ByVal CCDNo As Integer, ByVal ticket As Integer, ByRef alignResult As List(Of sAlignResult)) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetAlignOffset(.ChannelNo, ticket, alignResult, CCDNo)
        End With
    End Function

    ''' <summary>顯示CCD定位結果</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="ticket"></param>
    ''' <param name="ucDisp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowAlignResult(ByVal CCDNo As Integer, ByVal ticket As Integer, ByVal ucDisp As ucDisplay) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).ShowAlignResult(.ChannelNo, ticket, ucDisp, CCDNo)
        End With

    End Function

#End Region
#Region "周邊操作"
    ''' <summary>取得取像影像範圍</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="sideXLength"></param>
    ''' <param name="sideYLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAcquistionSideLength(CCDNo As Integer, ByRef sideXLength As Integer, ByRef sideYLength As Integer) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetAcquistionSideLength(.ChannelNo, sideXLength, sideYLength)
        End With
    End Function

    ''' <summary>取得定位工具清單</summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignmentToolNameList(ByVal CCDNo As Integer) As String()
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return Nothing
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetAlignmentToolNameList
        End With
    End Function

    ''' <summary>取得定位場景教導範圍</summary>
    ''' <param name="CCDNo"></param>
    ''' <param name="sceneName"></param>
    ''' <param name="sideXLength"></param>
    ''' <param name="sideYLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignTrainSideLength(ByVal CCDNo As Integer, ByVal sceneName As String, ByRef sideXLength As Decimal, ByRef sideYLength As Decimal, ByRef rotation As Decimal) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).GetAlignTrainSideLength(sceneName, sideXLength, sideYLength, rotation)
        End With
    End Function

    ''' <summary>
    ''' 套用CCD影像到所有所屬工具
    ''' </summary>
    ''' <param name="CCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAlignImage(ByVal CCDNo As Integer) As Boolean
        If IsIndexOutOfRange(CCDNo) Then
            Return False
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetAlignImage(.ChannelNo)
        End With
    End Function

    ''' <summary>設定定位工具影像來源</summary>
    ''' <param name="CCDNo">CCD索引</param>
    ''' <param name="sceneName">場景索引</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAlignImage(ByVal CCDNo As Integer, ByVal sceneName As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetAlignImage(.ChannelNo, sceneName)
        End With
    End Function

    Public Function GetOutparam(ByVal cCDNo As Integer, ByVal sceneName As String) As COutputParam
        If IsIndexOutOfRange(cCDNo) Then 'CCDNo異常略過
            Return Nothing
        End If
        With Channel(cCDNo)
            Return Items(.ItemNo).OutputParam(sceneName)
        End With
    End Function
#End Region

    'TODO: Wenda 待廢棄項目確認是否可移除或以其他方式取代. 注意後續仍會使用Omron CCD.
    Public Function SetImageMode(ByVal CCDNo As Integer, ByVal ImageMode As enmCCDImageShowType, Optional ImageDisplayingNo As Integer = 0) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetImageMode(ImageMode, ImageDisplayingNo)
        End With
    End Function

    Public Function SetCCDRunType(ByVal CCDNo As Integer, ByVal enmRun As enmCCDRunType) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).SetCCDRunType(.ChannelNo, enmRun)
        End With
    End Function

    Public Function CCDSendCommand(ByVal CCDNo As Integer, ByVal CCDHandshakeData As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If
        With Channel(CCDNo)
            Return Items(.ItemNo).CCDSendCommand(CCDHandshakeData)
        End With
    End Function
    Public ReadOnly Property ReceiveString(ByVal CCDNo As Integer) As String

        Get

            If IsIndexOutOfRange(CCDNo) Then
                Return "ReceiveString " & gMsgHandler.GetMessage(Error_1000003) ' "ReceiveString is NOT Supported."
            End If

            Dim mItemNo As Integer = Channel(CCDNo).ItemNo '取得對應Item索引
            Select Case Cards(mItemNo).CCDType ' gSSystemParameter.enmCCDType
                Case enmCCDType.CognexVPRO
                    gSyslog.Save("ReceiveString " & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
                    Return "ReceiveString " & gMsgHandler.GetMessage(Error_1000003) ' "ReceiveString is NOT Supported."
                Case enmCCDType.KeyenceCV200CTCP
                    Return CType(Items(mItemNo), CAOIKeyenceCV200CTCP).gKeyenceCcdTcp.ReceiveString
                Case enmCCDType.OmronFZS2MTCP
                    gSyslog.Save("ReceiveString " & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
                    Return "ReceiveString " & gMsgHandler.GetMessage(Error_1000003) ' "ReceiveString is NOT Supported."
                Case enmCCDType.OmronFZS2MUDP
                    Return CType(Items(mItemNo), CAOIOmronFZS2MUDP).gOmronCcdUdp.ReceiveString
                Case Else
                    gSyslog.Save("ReceiveString " & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
                    Return "ReceiveString " & gMsgHandler.GetMessage(Error_1000003) ' "ReceiveString is NOT Supported."
            End Select
        End Get
    End Property

#Region "連線參數存取"
    ''' <summary>讀取AOI連線參數</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadCCDConnectionParameter(ByVal fileName As String) As Boolean
        Dim mSection As String = "CCD"
        Dim mCardCount As Integer

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS_2S2V
                mCardCount = Val(ReadIniString(mSection, "CardCount", fileName, enmCCD.CCD2 + 1))
            Case enmMachineType.DCSW_800AQ
                mCardCount = Val(ReadIniString(mSection, "CardCount", fileName, enmCCD.CCD4 + 1))
            Case Else
                mCardCount = Val(ReadIniString(mSection, "CardCount", fileName, enmCCD.CCD1 + 1))
        End Select

        For mCardNo As Integer = 0 To mCardCount - 1
            Dim strSection As String = "CCD" & (mCardNo + 1).ToString & "-Connection"
            Dim item As New sCCDConnectionParameter
            With item
                .CCDType = Val(ReadIniString(strSection, "CCDType", fileName))
                Select Case .CCDType
                    Case enmCCDType.OmronFZS2MTCP, enmCCDType.OmronFZS2MUDP
                        .Omron.SendIPAddress = ReadIniString(strSection, "SendIPAddress", fileName)
                        .Omron.SendPort = ReadIniString(strSection, "SendPort", fileName)
                        .Omron.RecieveIPAddress = ReadIniString(strSection, "RecieveIPAddress", fileName)
                        .Omron.RecievePort = ReadIniString(strSection, "RecievePort", fileName)
                    Case enmCCDType.CognexVPRO
                        .Cognex.CCDNo = Val(ReadIniString(strSection, "CCDNo", fileName, mCardNo))
                        .Cognex.ShortFileName = ReadIniString(strSection, "ShortFileName", fileName, "CCD" & (mCardNo + 1).ToString)
                        .Cognex.TriggerType = CInt(ReadIniString(strSection, "TriggerType", fileName, 0))
                        '.Cognex.DeviceType = CInt(ReadIniString(strSection, "CCDDevice", fileName, 0))
                        .Cognex.SerialNumber = ReadIniString(strSection, "SerialNumber", fileName)
                        .Cognex.VideoFormatType = CInt(ReadIniString(strSection, "VideoType", fileName, 0))

                End Select
            End With
            Cards.Add(item)
        Next
        Return True
    End Function

    ''' <summary>儲存AOI連線參數</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCCDConnectionParameter(ByVal fileName As String) As Boolean
        Dim mSection As String = "CCD"
        SaveIniString(mSection, "CardCount", Cards.Count, fileName)
        For mCardNo As Integer = 0 To Cards.Count - 1
            mSection = "CCD" & (mCardNo + 1).ToString & "-Connection"
            With Cards(mCardNo)
                SaveIniString(mSection, "CCDType", CInt(.CCDType), fileName)
                Select Case .CCDType
                    Case enmCCDType.OmronFZS2MTCP, enmCCDType.OmronFZS2MUDP
                        With .Omron
                            SaveIniString(mSection, "SendIPAddress", .SendIPAddress, fileName)
                            SaveIniString(mSection, "SendPort", .SendPort, fileName)
                            SaveIniString(mSection, "RecieveIPAddress", .RecieveIPAddress, fileName)
                            SaveIniString(mSection, "RecievePort", .RecievePort, fileName)
                        End With

                    Case enmCCDType.CognexVPRO
                        With .Cognex
                            SaveIniString(mSection, "CCDNo", .CCDNo, fileName)
                            SaveIniString(mSection, "ShortFileName", .ShortFileName, fileName)
                            SaveIniString(mSection, "TriggerType", .TriggerType, fileName)
                            'SaveIniString(mSection, "CCDDevice", .DeviceType, fileName)
                            SaveIniString(mSection, "SerialNumber", .SerialNumber, fileName)
                            SaveIniString(mSection, "VideoType", .VideoFormatType, fileName)
                            'SaveIniString(mSection, "AutoWhiteBalance", .AutoWhiteBalance, fileName)
                        End With


                End Select
            End With
        Next
        Return True
    End Function


    Public Function GetCCDParameter(ByVal mCardNo As Integer) As sAOIAcquisitionParameter
        If IsIndexOutOfRange(mCardNo) Then
            Return Nothing
        End If

        Return Channel(mCardNo)
    End Function


#End Region

#Region "取像參數存取"


    ''' <summary>
    ''' 讀取AOI通道參數設定
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadChannelParameter(ByVal fileName As String) As Boolean
        Try
            ReDim Channel(enmCCD.Max)
            For mCCDNo As Integer = 0 To Channel.Count - 1
                Dim strSection As String = "CCD" & (mCCDNo + 1).ToString
                Dim mImage As ICogImage
                Dim ImageWidth As Integer
                Dim ImageHeight As Integer
                AcquireImage(mCCDNo)
                mImage = GetAcqOutputImage(mCCDNo)
                If mImage IsNot Nothing Then
                    ImageWidth = mImage.Width
                    ImageHeight = mImage.Height
                    'Debug.Print("StartLive Width" & mImage.Width)
                    'Debug.Print("StartLive Height" & mImage.Height)
                End If

                With Channel(mCCDNo)
                    .CCDExposureTime = Val(ReadIniString(strSection, "CCDExposureTime", fileName, 0.3))
                    .ItemNo = Val(ReadIniString(strSection, "ItemNo", fileName, mCCDNo))
                    .ChannelNo = Val(ReadIniString(strSection, "ChannelNo", fileName, 0))
                    .ImageWidth = ImageWidth
                    .ImageHeight = ImageHeight 
                End With
            Next
            Return True
        Catch ex As Exception
            gSyslog.Save("LoadChannelParameter Error. Exception Message:" & ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try
    End Function
    ''' <summary>儲存AOI通道參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveChannelParameter(ByVal fileName As String) As Boolean
        Try
            For mCCDNo As Integer = 0 To Channel.Count - 1
                Dim strSection As String = "CCD" & (mCCDNo + 1).ToString
                With Channel(mCCDNo)
                    SaveIniString(strSection, "CCDExposureTime", .CCDExposureTime, fileName)
                    SaveIniString(strSection, "ItemNo", .ItemNo, fileName)
                    SaveIniString(strSection, "ChannelNo", .ChannelNo, fileName)
                    SaveIniString(strSection, "ItemNo", .ItemNo, fileName)
                    SaveIniString(strSection, "ImageWidth", .ImageWidth, fileName)
                    SaveIniString(strSection, "ImageHeight", .ImageHeight, fileName)
                End With
            Next
            Return True
        Catch ex As Exception
            gSyslog.Save("SaveChannelParameter Error. Exception Message:" & ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try

    End Function

#End Region

    ''' <summary>影像設定讀取</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadVision(ByVal CCDNo As Integer, ByVal strFileName As String) As Boolean
        If IsIndexOutOfRange(CCDNo) Then 'CCDNo異常略過
            Return True
        End If

        Return Items(Channel(CCDNo).ItemNo).LoadVision(strFileName)

    End Function

    Public Enum enmStatus
        ''' <summary>執行中</summary>
        ''' <remarks></remarks>
        Loading = 0
        ''' <summary>完成</summary>
        ''' <remarks></remarks>
        OK = 1
        ''' <summary>失敗</summary>
        ''' <remarks></remarks>
        NG = 2
    End Enum
    ''' <summary>場景讀取狀態</summary>
    ''' <remarks></remarks>
    Public LoadSceneStatus As enmStatus

    Sub Thread_LoadScene(ByVal fileName As String)
        LoadSceneStatus = enmStatus.Loading
        Dim ErrMsg As String = ""
        '不論有沒有用到 只要是在同一資料夾下就讀取
        For Each file In System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(fileName) & "\")
            If file.EndsWith(".vpp") Or file.EndsWith(".Vpp") Then 'vpp檔案存在. 視為定位檔 Soni / 2016.09.09 追加副檔名可能性
                'fileName = System.IO.Path.GetDirectoryName(.FileName) & "\" & strScene2ID & ".vpp"
                If System.IO.File.Exists(file) Then
                    For mCCDNo As Integer = enmCCD.CCD1 To enmCCD.Max
                        'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
                        If LoadVision(mCCDNo, file) = False Then
                            ErrMsg += ErrorMessage(mCCDNo) 'Soni + 2016.09.04 讓跳很多次,變成最後一次跳
                        End If
                    Next

                    'wenda
                    Dim fInfo As New System.IO.FileInfo(file)
                    Dim SceneName As String = fInfo.Name.Replace(".vpp", "").Replace(".Vpp", "") '取得不含後綴名的短檔名 'Soni / 2016.09.09 修正副檔名可能性
                    'Debug.WriteLine("---AOI---  SceneName: " & SceneName)
                    Dim mFileName = System.IO.Path.GetDirectoryName(fileName) & "\" & SceneName & ".ini" '光源設定檔路徑
                    'Debug.WriteLine("---AOI---  mFileName: " & mFileName)
                    If System.IO.File.Exists(mFileName) Then '如果設定檔存在
                        'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
                        'gAOICollection.LoadSceneParameter(SceneName, mFileName) '讀取光源,曝光值等設定
                        LoadSceneParameter(SceneName, mFileName) '讀取光源,曝光值等設定
                        'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
                    Else
                        'Debug.WriteLine(SceneName & ".ini檔案不存在")
                        Dim mErrorMessage As String = "Scene:" & SceneName & "ini   Not Exists!"
                        ErrMsg += mErrorMessage & vbCrLf
                        gSyslog.Save(mErrorMessage)
                    End If

                    'wenda 

                End If
            End If
        Next

        If ErrMsg <> "" Then '由每次跳,改為一次跳
            MsgBox(ErrMsg, vbOKOnly + MsgBoxStyle.Critical + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            LoadSceneStatus = enmStatus.NG
        Else
            LoadSceneStatus = enmStatus.OK
        End If
    End Sub


    ''' <summary>
    ''' 載入Recipe裡的場景工具
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadScene(ByVal fileName As String) As Boolean
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
        'Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Thread_LoadScene), fileName)
        Task.Run(Sub()
                     Thread_LoadScene(fileName)
                 End Sub)
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]
        Return True
    End Function


#Region "場景維護"
    'Dim SysSceneNamelist As New ArrayList
    'Public Function LoadSysSceneList() As Boolean
    '    SysSceneNamelist.Clear()
    '    'Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\SysScene.ini"
    '    'Dim section As String = "CALIBScene"
    '    Dim sysScene() As String = {"CALIB", "CALIBScene", "CALIBMark", "CALIBValve", "CALIBSVT", "CALIBMR"}
    '    For i As Integer = 0 To sysScene.Count - 1
    '        'Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\" & sysScene(i) & ".vpp"
    '        'If System.IO.File.Exists(fileName) Then
    '        'Dim fInfo As New System.IO.FileInfo(fileName)
    '        'Dim SceneName As String = fInfo.Name.Replace(".vpp", "").Replace(".Vpp", "") '取得不含後綴名的短檔名 
    '        SysSceneNamelist.Add(sysScene(i))
    '        'End If
    '    Next
    '    Return True
    'End Function


    ''' <summary>工程界面下使用場景</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSysSceneList() As String()
        Dim sysScene() As String = {"CALIB", "CALIBMark", "CALIBValve", "CALIBSVT", "CALIBMR", "CALIBScale"}
        'CALIB:預設光源場景
        'CALIBMark:機構定位點
        'CALIBValve:CCD2Valve校正
        'CALIBSVT:平台驗證
        'CALIBMR:點膠落點精度
        'CALIBScale:CCD校正Pixel/mm
        Return sysScene
    End Function

    Dim SceneNamelist As New ArrayList
    Public Function LoadSceneList(ByVal fileName As String) As Boolean
        SceneNamelist.Clear()
        For Each file In System.IO.Directory.GetFiles(Application.StartupPath & "\Scene\" & MachineName) '(System.IO.Path.GetDirectoryName(fileName) & "\")
            If file.EndsWith(".vpp") Or file.EndsWith(".Vpp") Then
                If System.IO.File.Exists(file) Then
                    Dim fInfo As New System.IO.FileInfo(file)
                    Dim SceneName As String = fInfo.Name.Replace(".vpp", "").Replace(".Vpp", "") '取得不含後綴名的短檔名 
                    SceneNamelist.Add(SceneName)
                End If
            End If
        Next
        Return True
    End Function

    Public Function GetSceneList() As String()
        Dim SceneList(SceneNamelist.Count - 1) As String
        For i As Integer = 0 To SceneNamelist.Count - 1
            SceneList(i) = SceneNamelist.Item(i).ToString
        Next
        Return SceneList
    End Function


    ''' <summary>釋放場景</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReleaseScene() As Boolean
        'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
        'Dim ToolScene() As String = gAOICollection.GetAlignmentToolNameList(0) '載入過的定位工具
        Dim ToolScene() As String = GetAlignmentToolNameList(0) '載入過的定位工具
        'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
        Dim RecipeScene() As String = gCRecipe.GetSceneNamelist() 'Recipe裡有使用的定位工具
        Dim Scene As String = ""
        Dim stopwatch As New Stopwatch

        For i As Integer = 0 To ToolScene.Length - 1
            If ToolScene(i).Length > 5 Then
                If ToolScene(i).Substring(0, 5) = "CALIB" Then '校正場景不能移除
                    Continue For
                End If
            End If

            'For j As Integer = 0 To RecipeScene.Length - 1
            '    If ToolScene(i) = RecipeScene.Contains Then
            '        Exit For
            '    End If
            'Next
            If RecipeScene.Contains(ToolScene(i)) Then '如果Recipe裡有使用則不移除
                Continue For
            End If

            'error
            For CCDNo As Integer = enmCCD.CCD1 To enmCCD.Max
                stopwatch.Restart()
                'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
                'gAOICollection.RemoveScene(CCDNo, ToolScene(i))
                RemoveScene(CCDNo, ToolScene(i))
                'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
                Debug.WriteLine(ToolScene(i) & "場景移除:" & stopwatch.ElapsedMilliseconds & "ms")
            Next

        Next
        Return True
    End Function


    ''' <summary>讀取Recipe場景</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadRecipeScene() As Boolean
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
        'Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Thread_LoadRecipeScene))
        Task.Run(Sub()
                     Thread_LoadRecipeScene()
                 End Sub)
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]
        Return True
    End Function

    Public LoadRecipeSceneStatus As enmStatus

    Sub Thread_LoadRecipeScene()
        LoadSceneStatus = enmStatus.Loading

        Dim ErrMsg As String = ""
        Dim RecipeSceneList() As String = gCRecipe.GetSceneNamelist()

        Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        If Not (System.IO.Directory.Exists(RecipeDirectoryName)) Then
            System.IO.Directory.CreateDirectory(RecipeDirectoryName)
        End If

        For Each file In System.IO.Directory.GetFiles(RecipeDirectoryName)
            If file.EndsWith(".vpp") Or file.EndsWith(".Vpp") Then 'vpp檔案存在. 視為定位檔 Soni / 2016.09.09 追加副檔名可能性
                'fileName = System.IO.Path.GetDirectoryName(.FileName) & "\" & strScene2ID & ".vpp"
                If System.IO.File.Exists(file) Then

                    Dim fileInfo As New System.IO.FileInfo(file)
                    Dim SceneName As String = fileInfo.Name.Replace(".vpp", "").Replace(".Vpp", "") '取得不含後綴名的短檔名 'Soni / 2016.09.09 修正副檔名可能性
                    For i As Integer = 0 To RecipeSceneList.Count - 1

                        If SceneName = RecipeSceneList(i) Then '在Recipe裡的場景才載入
                            For mCCDNo As Integer = enmCCD.CCD1 To enmCCD.Max
                                'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
                                If LoadVision(mCCDNo, file) = False Then
                                    ErrMsg += ErrorMessage(mCCDNo) 'Soni + 2016.09.04 讓跳很多次,變成最後一次跳
                                End If
                            Next

                            Dim mFileName = RecipeDirectoryName & "\" & SceneName & ".ini" '光源設定檔路徑
                            If System.IO.File.Exists(mFileName) Then '如果設定檔存在
                                'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
                                'gAOICollection.LoadSceneParameter(SceneName, mFileName) '讀取光源,曝光值等設定
                                LoadSceneParameter(SceneName, mFileName) '讀取光源,曝光值等設定
                                'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
                            Else
                                Dim mErrorMessage As String = "Scene:" & SceneName & "ini   Not Exists!"
                                ErrMsg += mErrorMessage & vbCrLf
                                gSyslog.Save(mErrorMessage)
                            End If

                        End If
                    Next

                End If
            End If
        Next

        If ErrMsg <> "" Then '由每次跳,改為一次跳
            MsgBox(ErrMsg, vbOKOnly + MsgBoxStyle.Critical + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            LoadSceneStatus = enmStatus.NG
        Else
            LoadSceneStatus = enmStatus.OK
        End If
    End Sub


#End Region

End Class

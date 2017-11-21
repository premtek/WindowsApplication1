Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports ProjectRecipe
Imports Cognex.VisionPro
'Imports Cognex.VisionPro.ImageFile
Imports Cognex.VisionPro.ToolBlock
Imports Cognex.VisionPro.PMAlign
Imports Cognex.VisionPro.CalibFix
Imports Cognex.VisionPro.PixelMap
Imports Cognex.VisionPro.Blob
Imports Cognex.VisionPro.Caliper

Imports System.Threading


''' <summary>觸發模式列舉</summary>
''' <remarks></remarks>
Public Enum eTriggerType
    ''' <summary>預設值(不明)</summary>
    ''' <remarks></remarks>
    [Default] = 0
    ''' <summary>軟體觸發</summary>
    ''' <remarks></remarks>
    SoftwareTrigger = 1
    ''' <summary>硬體觸發</summary>
    ''' <remarks></remarks>
    HardwareTrigger = 2
End Enum

''' <summary>Cognex連線設定檔</summary>
''' <remarks></remarks>
Public Structure sCognexVProConnect
    ''' <summary>一個連線只有一個唯一的CCD編號</summary>
    ''' <remarks></remarks>
    Public CCDNo As Integer
    ''' <summary>設定檔短檔名(不含副檔名,後綴名)</summary>
    ''' <remarks></remarks>
    Public ShortFileName As String

    Public TriggerType As eTriggerType
    ''' <summary>
    ''' CCD 相機廠牌
    ''' </summary>
    ''' <remarks></remarks>
    Public DeviceType As enmCCDDeciveType
    ''' <summary>
    ''' CCD相機序號
    ''' </summary>
    ''' <remarks></remarks>
    Public SerialNumber As String
    ''' <summary>
    ''' CCD影像格式
    ''' </summary>
    ''' <remarks></remarks>
    Public VideoFormatType As enmVideoFormatType
    ''' <summary>
    ''' 是否開啟白平衡
    ''' </summary>
    ''' <remarks></remarks>
    Public AutoWhiteBalance As enmONOFF
End Structure

''' <summary>AOI介面實作使用CognexVPRO</summary>
''' <remarks></remarks>
Public Class CAOICognexVPRO
    Implements IDisposable
    Implements IAOIInterface

    Public Function Copy() As IAOIInterface Implements IAOIInterface.Copy
        Return Me.MemberwiseClone
    End Function

    ''' <summary>運行成功時傳回事件</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event OnRunSuccessed(sender As Object, ByVal e As AOIEventArgs) Implements IAOIInterface.OnRunSuccessed

    ''' <summary>錯誤訊息</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorMessage As String Implements IAOIInterface.ErrorMessage

    ''' <summary>無效(相容性預留)</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SockStatus As SockStatusInfo Implements IAOIInterface.SockStatus
        Get
            Return SockStatusInfo.sckConnected
        End Get
    End Property

    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsIntialOK As Boolean Implements IAOIInterface.IsIntialOK
        Get
            Return mIsIntialOK
        End Get
    End Property

    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Private mIsIntialOK As Boolean
    Dim mIsCCDReceiveBusy(enmCCD.ConstMax) As Boolean
    Dim mIsCCDReceivedDataOK(enmCCD.ConstMax) As Boolean
    Dim mblnCCDSetNextScence(enmCCD.ConstMax) As Boolean '取像後是否設定下一組場景參數
    ''' <summary>
    ''' [資料接收中]
    ''' </summary>
    ''' <remarks></remarks>
    Public Property IsCCDReceiveBusy(ByVal channelNo As Integer) As Boolean Implements IAOIInterface.IsCCDReceiveBusy
        Get
            Return mIsCCDReceiveBusy(channelNo)
        End Get
        Set(value As Boolean)
            mIsCCDReceiveBusy(channelNo) = value
        End Set
    End Property
    ''' <summary>
    ''' [CCD Handshake(接收到的資料格式正確)回應成功 為true, 失敗為false]
    ''' </summary>
    ''' <remarks></remarks>
    Public Property IsCCDReceivedDataOK(ByVal channelNo As Integer) As Boolean Implements IAOIInterface.IsCCDReceivedDataOK
        Get
            Return mIsCCDReceivedDataOK(channelNo)
        End Get
        Set(value As Boolean)
            mIsCCDReceivedDataOK(channelNo) = value
        End Set
    End Property

    ''' <summary>
    ''' [CCD取像後是否設定下一組場景參數]
    ''' </summary>
    ''' <param name="channelNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsCCDSetNextScene(ByVal channelNo As Integer) As Boolean Implements IAOIInterface.IsCCDSetNextScene
        Get
            Return mblnCCDSetNextScence(channelNo)
        End Get
        Set(value As Boolean)
            mblnCCDSetNextScence(channelNo) = value
        End Set
    End Property

    ''' <summary>取像工具列表</summary>
    ''' <remarks></remarks>
    Public ImageCam As CThreadCogAcqFifoTool ' CThreadCogToolBlock
    ''' <summary>取像佇列</summary>
    ''' <remarks></remarks>
    Dim AcqImageQueue As New System.Collections.Concurrent.ConcurrentQueue(Of sAOIStepConfig)

#Region "初始化/檔案存取"

    ''' <summary>初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(ByVal ccdConnectionParameter As sCCDConnectionParameter) As Boolean Implements IAOIInterface.Initial

        Try
            'Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\" & ccdConnectionParameter.Cognex.ShortFileName & ".vpp" '改為指定名稱
            'If System.IO.File.Exists(fileName) Then '如果檔案存在
            If ccdConnectionParameter.Cognex.TriggerType = eTriggerType.HardwareTrigger Then
                ImageCam = New CThreadCogAcqFifoTool(True)
            Else
                ImageCam = New CThreadCogAcqFifoTool(False)
            End If

            ImageCam.CCDNo = ccdConnectionParameter.Cognex.CCDNo
            'ImageCam.Subject = CogSerializer.LoadObjectFromFile(fileName) '載入為取像物件

            '=== 使用設定檔建立AcqFifoTool ===
            Dim mFrameGrabbers As New CogFrameGrabbers
           
            Dim isMatched As Boolean = False
            ImageCam.Subject = New CogAcqFifoTool
            ImageCam.Subject.GarbageCollectionEnabled = True
            ImageCam.Subject.GarbageCollectionFrequency = 10

            If ccdConnectionParameter.CCDType = enmCCDType.CognexVPRO Then '希望設定SN是0-3時, 自動抓CCD.
                Dim mNo As Integer = Val(ccdConnectionParameter.Cognex.SerialNumber)
                If mFrameGrabbers.Count > mNo Then
                    ccdConnectionParameter.Cognex.SerialNumber = mFrameGrabbers(mNo).SerialNumber
                End If
            End If

            Dim VideoFormat As Integer = enmVideoFormatType.None
            For i As Integer = 0 To mFrameGrabbers.Count - 1 '對所有取像物件
                If mFrameGrabbers(i).SerialNumber = ccdConnectionParameter.Cognex.SerialNumber Then '先找序號 再指定取像格式
                    gSyslog.Save("CCD Device:" & mFrameGrabbers(i).Name)
                    gSyslog.Save("User Set CCD VideoFormatType: " & TransformVideoNumToCognexStr(ccdConnectionParameter.Cognex.VideoFormatType))
                    For j = mFrameGrabbers(i).AvailableVideoFormats.Count - 1 To 0 Step -1
                        If mFrameGrabbers(i).AvailableVideoFormats(j) = TransformVideoNumToCognexStr(ccdConnectionParameter.Cognex.VideoFormatType) Then
                            VideoFormat = TransformCognexVideoStrToNum(mFrameGrabbers(i).AvailableVideoFormats(j))
                        End If
                    Next
                    If VideoFormat = enmVideoFormatType.None Then '讀取影像格式錯誤 
                        'Sue0710
                        'CCD讀取影像格式錯誤 
                        gEqpMsg.AddHistoryAlarm("Alarm_2000026", "Initial", "", gMsgHandler.GetMessage(Alarm_2000026), eMessageLevel.Alarm)
                        MsgBox(gMsgHandler.GetMessage(Alarm_2000026), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If
                    ImageCam.Subject.Operator = mFrameGrabbers(i).CreateAcqFifo(TransformVideoNumToCognexStr(ccdConnectionParameter.Cognex.VideoFormatType), CogAcqFifoPixelFormatConstants.Format8Grey, 0, True)
                    isMatched = True

                    Exit For
                End If
            Next
            If isMatched = False Then
                'Sue0710
                'CCD序號不匹配 
                gEqpMsg.AddHistoryAlarm("Alarm_2000027", "Initial", "", gMsgHandler.GetMessage(Alarm_2000027), eMessageLevel.Alarm)
                MsgBox(gMsgHandler.GetMessage(Alarm_2000027) & ccdConnectionParameter.Cognex.SerialNumber, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
            '=== 使用設定檔建立AcqFifoTool ===

            'ImageCam.Subject.Operator = ImageCam.Subject.Operator.FrameGrabber.CreateAcqFifo("", CogAcqFifoPixelFormatConstants.Format8Grey, 0, True)
            'Dim frame = ImageCam.Subject.Operator.FrameGrabber

            'ImageCam.Subject.Operator.FrameGrabber.SerialNumber = ccdConnectionParameter.Cognex.SerialNumber
            'ImageCam.Subject.Operator
            'CogSerializer.SaveObjectToFile(ImageCam.Subject, fileName)
            gSyslog.Save("AOI ImageCam.Start")
            ImageCam.Start()
            AddHandler ImageCam.OnRunSuccess, AddressOf cogTB_ACQ0_OnRunSuccess '取像完成,接計算
            'End If

            'If mThread.ThreadState = Threading.ThreadState.Stopped Or mThread.ThreadState = Threading.ThreadState.Unstarted Then '啟動計算作業派工執行緒
            '    mThread.Start()
            'End If
            'Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
            'System.Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Dispatch))
            Task.Run(Sub()
                         Dispatch()
                     End Sub)
            'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]
            mIsIntialOK = True
            Return True
        Catch ex As Exception
            MsgBox("Exception Message@CCDConnect: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function




    ''' <summary>載入影像計算檔</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadVision(ByVal fileName As String, Optional sceneName As String = "") As Boolean Implements IAOIInterface.LoadVision
        Dim fInfo As New System.IO.FileInfo(fileName)
        Dim configName As String
        Dim isError As Boolean = False
        Dim sw As New Stopwatch
        Dim KeyName As String = fInfo.Name.Replace(".vpp", "").Replace(".Vpp", "") '取得不含後綴名的短檔名 Soni + 2016.09.13 讀檔出現Vpp的原因, 前面允許讀, 後面沒刪副檔名
        If sceneName <> "" Then '外部指定名稱
            KeyName = sceneName
        End If

        Try
            If Not System.IO.File.Exists(fileName) Then '不存在的另外建立
                If Not cogTB_AlignList.ContainsKey(KeyName) Then '清單內不存在則另建
                    cogTB_AlignList.Add(KeyName, New CThreadCogToolBlock(ImageCam.CCDNo))
                End If
                cogTB_AlignList(KeyName).Subject = New CogToolBlock()
                cogTB_AlignList(KeyName).Subject.GarbageCollectionEnabled = True

                If Not cogTB_AlignList.ContainsKey(KeyName) Then
                    cogTB_AlignList.Add(KeyName, New CThreadCogToolBlock(ImageCam.CCDNo))
                End If

                AddHandler cogTB_AlignList(KeyName).OnBeforeRun, AddressOf cogTB_AlignList_OnBeforeRun
                AddHandler cogTB_AlignList(KeyName).OnRunSuccess, AddressOf cogTB_AlignList_OnRunSuccess
            Else
                If cogTB_AlignList.ContainsKey(KeyName) = False Then '清單內不存在則另建
                    cogTB_AlignList.Add(KeyName, New CThreadCogToolBlock(ImageCam.CCDNo))
                    AddHandler cogTB_AlignList(KeyName).OnBeforeRun, AddressOf cogTB_AlignList_OnBeforeRun
                    AddHandler cogTB_AlignList(KeyName).OnRunSuccess, AddressOf cogTB_AlignList_OnRunSuccess
                End If
                sw.Restart()
                cogTB_AlignList(KeyName).Subject = CogSerializer.LoadObjectFromFile(fileName)
                Debug.Print(KeyName & "讀檔耗時(ms): " & sw.ElapsedMilliseconds)
            End If

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000004), "Alarm_2000004", eMessageLevel.Alarm)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Alarm)
            isError = True
            ErrorMessage = gMsgHandler.GetMessage(Alarm_2000004) & vbCrLf & KeyName & vbCrLf & ex.Message
            'MsgBox(gMsgHandler.GetMessage(Alarm_2000004) & vbCrLf & KeyName & vbCrLf & ex.Message, MsgBoxStyle.OkOnly, "LoadVision")
        End Try

        configName = fInfo.DirectoryName & "\" & KeyName & ".ini" '光源相關設定檔
        If Not System.IO.File.Exists(configName) Then
            cogTB_AlignList(KeyName).OutputParam.Save(configName) '儲存光源相關設定檔
        End If
        cogTB_AlignList(KeyName).OutputParam.Load(configName) '讀取光源相關設定檔


        If isError Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region


#Region "取像流程"
    Public Function SetHardwareTrigger(ByVal HardwareTrigger As Boolean) As Boolean Implements IAOIInterface.SetHardwareTrigger
        ImageCam.IsHardwareTrigger = HardwareTrigger
        'ImageCam.Run(Not HardwareTrigger, NotRevisedTicket)
        Return True
    End Function

    Public Function SetIsStop(ByVal state As Boolean) As Boolean Implements IAOIInterface.SetIsStop
        ImageCam.IsStop = state
        Return True
    End Function


    ''' <summary>設定曝光時間(快門時間)</summary>
    ''' <param name="channelNo"></param>
    ''' <param name="exposure_ms"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetExposure(ByVal channelNo As Integer, ByVal exposure_ms As Decimal) As Boolean Implements IAOIInterface.SetExposure
        If channelNo < 0 Then '索引不存在, 當作模擬處理
            Return True
        End If

        If ImageCam Is Nothing Then
            Return False
        End If
        If ImageCam.Subject Is Nothing Then
            Return False
        End If
        'If ImageCam.Subject.Tools.Count = 0 Then
        '    Return False
        'End If
        'If ImageCam.Subject.Tools(0).GetType <> GetType(CogAcqFifoTool) Then
        '    Return False
        'End If
        'Dim tool As CogAcqFifoTool = ImageCam.Subject.Tools(0)
        Dim tool As CogAcqFifoTool = ImageCam.Subject
        If tool.Operator Is Nothing Then
            Return False
        End If
        If tool.Operator.OwnedExposureParams Is Nothing Then
            Return False
        End If
        Try
            tool.Operator.OwnedExposureParams.Exposure = exposure_ms
            'Debug.Print("MinExposure: " & tool.Operator.OwnedExposureParams.MinExposure)
            Return True
        Catch ex As Exception
            gSyslog.Save("Set Exposure Exception: " & ex.Message & vbCrLf & ex.StackTrace, , eMessageLevel.Error)
            'MsgBox(ex.Message & vbCrLf & ex.StackTrace)
            Return False
        End Try

    End Function

    Public Function GetExposure(ByVal channelNo As Integer) As Double Implements IAOIInterface.GetExposure
        Dim tool As CogAcqFifoTool = ImageCam.Subject
        Return tool.Operator.OwnedExposureParams.Exposure
    End Function

    Public Function ClearTicket() As Boolean Implements IAOIInterface.ClearTicket
        ImageCam.Ticket = 0
        Return True
    End Function
    Public Function SetAutoWhiteBalance(ByVal channelNo As Integer, ByVal Enable As Boolean) As Boolean Implements IAOIInterface.SetAutoWhiteBalance
        If channelNo < 0 Then '索引不存在, 當作模擬處理
            Return True
        End If
        If ImageCam Is Nothing Then
            Return False
        End If
        If ImageCam.Subject Is Nothing Then
            Return False
        End If
        Dim tool As CogAcqFifoTool = ImageCam.Subject
        If tool.Operator Is Nothing Then
            Return False
        End If

        Try

            If tool.Operator.OutputPixelFormat = CogImagePixelFormatConstants.PlanarRGB8 Then
                If Enable Then
                    tool.Operator.FrameGrabber.OwnedGigEAccess.SetFeature("BalanceWhiteAuto", "Continuous")
                    If (tool.Operator.FrameGrabber.OwnedGigEAccess.GetFeature("BalanceWhiteAuto") = "Continuous") Then
                        'Debug.WriteLine("Auto White Balance enable")
                        gSyslog.Save("CCD Auto White Balance enable")
                    End If
                Else
                    tool.Operator.FrameGrabber.OwnedGigEAccess.SetFeature("BalanceWhiteAuto", "Off")
                    If (tool.Operator.FrameGrabber.OwnedGigEAccess.GetFeature("BalanceWhiteAuto") = "Off") Then
                        'Debug.WriteLine("Auto White Balance OFF")
                        gSyslog.Save("CCD Auto White Balance OFF")
                    End If
                End If
            Else
                'Mono無白平衡功能
                gSyslog.Save("Mono without AWB ")
            End If


            Return True
        Catch ex As Exception
            gSyslog.Save("Set AutoWhiteBalance Exception: " & ex.Message & vbCrLf & ex.StackTrace, , eMessageLevel.Error)
            Return False
        End Try

    End Function

    Public Function SetAutoExposure(ByVal channelNo As Integer, ByVal Enable As Boolean) As Boolean Implements IAOIInterface.SetAutoExposure
        If channelNo < 0 Then '索引不存在, 當作模擬處理
            Return True
        End If
        If ImageCam Is Nothing Then
            Return False
        End If
        If ImageCam.Subject Is Nothing Then
            Return False
        End If
        Dim tool As CogAcqFifoTool = ImageCam.Subject
        If tool.Operator Is Nothing Then
            Return False
        End If

        Try
            If Enable Then
                tool.Operator.FrameGrabber.OwnedGigEAccess.SetFeature("ExposureAuto", "Continuous")
                If (tool.Operator.FrameGrabber.OwnedGigEAccess.GetFeature("ExposureAuto") = "Continuous") Then
                    'Debug.WriteLine("CCD Auto Exposure enable")
                    gSyslog.Save("CCD Auto Exposure enable")
                End If
            Else
                tool.Operator.FrameGrabber.OwnedGigEAccess.SetFeature("ExposureAuto", "Off")
                If (tool.Operator.FrameGrabber.OwnedGigEAccess.GetFeature("ExposureAuto") = "Off") Then
                    'Debug.WriteLine(" Auto Exposure OFF")
                    gSyslog.Save("CCD Auto Exposure OFF")
                End If
            End If

            Return True
        Catch ex As Exception
            gSyslog.Save("Set AutoExposure Exception: " & ex.Message & vbCrLf & ex.StackTrace, , eMessageLevel.Error)
            Return False
        End Try

    End Function

    ''' <summary>不修改票券</summary>
    ''' <remarks></remarks>
    Const NotRevisedTicket As Integer = -1

    Public Function SetLiveTriggerMode(ByVal channelNo As Integer, ByVal TriggerType As eTriggerType) As Boolean Implements IAOIInterface.SetLiveTriggerMode
        If channelNo < 0 Then '索引不存在, 當作模擬處理
            Return True
        End If

        If ImageCam Is Nothing Then
            Return False
        End If
        If ImageCam.Subject Is Nothing Then
            Return False
        End If

        Dim tool As CogAcqFifoTool = ImageCam.Subject
        If tool.Operator Is Nothing Then
            Return False
        End If
        If tool.Operator.OwnedTriggerParams Is Nothing Then
            Return False
        End If
        'Debug.Print("Before SetLiveTriggerMode: " & tool.Operator.OwnedTriggerParams.TriggerModel)

        '[Note]切換Mode流程參考  \Cognex\VisionPro\samples\Programming\Acquisition\TriggerMode
        '若有更動請多做幾次測試，防止Hang住或者取像張數錯誤
        Try
            Dim state As Boolean
            '[Note]Stop

            'System.Threading.Thread.CurrentThread.Join(150)
            '[Note]切換mode
            If TriggerType = eTriggerType.HardwareTrigger Then
                state = True
                If tool.Operator.OwnedTriggerParams.TriggerModel <> CogAcqTriggerModelConstants.Auto Then
                    SetIsStop(True)

                    tool.Operator.OwnedTriggerParams.TriggerEnabled = False
                    'Debug.Print("CogAcqTriggerModelConstants.Auto: " & tool.Operator.OwnedTriggerParams.TriggerModel)
                    tool.Operator.Flush()
                    tool.Operator.OwnedTriggerParams.TriggerModel = CogAcqTriggerModelConstants.Auto
                    tool.Operator.OwnedTriggerParams.TriggerEnabled = True

                    '[Note]Run
                    'System.Threading.Thread.CurrentThread.Join(150)
                    SetIsAcqImageOnly(Not state)
                    SetIsStop(False)
                    SetHardwareTrigger(state)

                    tool.Operator.Flush()
                    'System.Threading.Thread.CurrentThread.Join(100)
                End If
            Else
                state = False
                If tool.Operator.OwnedTriggerParams.TriggerModel <> CogAcqTriggerModelConstants.Manual Then

                    SetIsStop(True)
                    tool.Operator.OwnedTriggerParams.TriggerEnabled = False
                    'Debug.Print("CogAcqTriggerModelConstants.Manual: " & tool.Operator.OwnedTriggerParams.TriggerModel)
                    tool.Operator.Flush()
                    tool.Operator.OwnedTriggerParams.TriggerModel = CogAcqTriggerModelConstants.Manual
                    tool.Operator.OwnedTriggerParams.TriggerEnabled = True

                    '[Note]Run
                    'System.Threading.Thread.CurrentThread.Join(150)
                    SetIsAcqImageOnly(Not state)
                    SetIsStop(False)
                    SetHardwareTrigger(state)

                    tool.Operator.Flush()
                    'System.Threading.Thread.CurrentThread.Join(100)
                End If

            End If

            'Debug.Print("After SetLiveTriggerMode End: " & tool.Operator.OwnedTriggerParams.TriggerModel)
            'tool.Run()
            Return True
        Catch ex As Exception
            gSyslog.Save("Set LiveTrigger Exception: " & ex.Message & vbCrLf & ex.StackTrace, , eMessageLevel.Error)
            'MsgBox(ex.Message & vbCrLf & ex.StackTrace)
            Return False
        End Try

    End Function

    ''' <summary>CCD觸發流程</summary>
    ''' <param name="channelNo"></param>
    ''' <param name="OnOff"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCCDTrigger(ByVal channelNo As Integer, OnOff As ProjectMotion.enmONOFF, ByVal isAcqImageOnly As Boolean, ByVal isSetNextScene As Boolean) As Integer Implements IAOIInterface.SetCCDTrigger
        If OnOff = enmONOFF.eON Then

            IsCCDReceivedDataOK(channelNo) = False
            IsCCDReceiveBusy(channelNo) = True
            IsCCDSetNextScene(channelNo) = isSetNextScene
            'Debug.Print("IsCCDReceiveBusy(" & channelNo & ")=True,IsCCDReceivedDataOK(" & channelNo & ")=False @SetCCDTrigger")
            If ImageCam Is Nothing Then
                ImageCam = New CThreadCogAcqFifoTool   ' New CThreadCogToolBlock(ToolBlockCommandType.Acquistion)
            End If
            ImageCam.Run(isAcqImageOnly, NotRevisedTicket)
        End If

        If ImageCam Is Nothing Then
            MsgBox("ImageCam Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return 0
        End If
        Return ImageCam.Ticket
    End Function

    ''' <summary>CCD取像成功</summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub cogTB_ACQ0_OnRunSuccess(sender As Object, ByVal e As AOIEventArgs)
        If sender Is Nothing Then
            Exit Sub
        End If

        Select Case mCognexRun_F
            Case enmCCDRunType.Fix '定位
                If e.SceneName Is Nothing Then '場景名稱不存在
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012106), "Alarm_2012106", eMessageLevel.Alarm)
                    ErrorMessage = "計算場景(" & e.SceneName & ")不存在!"
                    'Debug.Print(ErrorMessage)
                    IsCCDReceiveBusy(e.ChannelNo) = False
                    IsCCDReceivedDataOK(e.ChannelNo) = True
                    'Debug.Print("IsCCDReceiveBusy(" & e.ChannelNo & ")=False,IsCCDReceivedDataOK(" & e.ChannelNo & ")=True @cogTB_ACQ0_OnRunSuccess")
                    Exit Sub
                End If

                If Not cogTB_AlignList.ContainsKey(e.SceneName) Then '計算場景不存在
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012107), "Alarm_2012107", eMessageLevel.Alarm)
                    ErrorMessage = "計算場景(" & e.SceneName & ")不存在!"
                    'Debug.Print(ErrorMessage)
                    IsCCDReceiveBusy(e.ChannelNo) = False
                    IsCCDReceivedDataOK(e.ChannelNo) = True
                    'Debug.Print("IsCCDReceiveBusy(" & e.ChannelNo & ")=False,IsCCDReceivedDataOK(" & e.ChannelNo & ")=True @cogTB_ACQ0_OnRunSuccess")
                    Exit Sub
                End If

                If e.IsAcqImageOnly = False Then
                    Dim result As New sAOIStepConfig
                    SyncLock result
                        result.CCDNo = e.CCDNo
                        result.SceneID = e.SceneName
                        result.Ticket = e.Ticket
                        result.OutputImage = sender.OutputImage
                        IsCCDReceivedDataOK(e.ChannelNo) = False
                        'Debug.Print("CCD" & (e.CCDNo + 1).ToString & ",索引:" & e.Ticket & "推入佇列 場景" & e.SceneName)
                        AcqImageQueue.Enqueue(result)
                        mAutoWait.Set() '通知計算作業派工執行緒作業
                    End SyncLock
                Else
                    ' Debug.Print("CCD" & (e.ChannelNo + 1).ToString & ",索引:" & e.Ticket & "取像完成")
                End If

        End Select
    End Sub

#End Region

    Public Function SetIsAcqImageOnly(ByVal AcqImageOnly As Boolean) As Boolean Implements IAOIInterface.SetIsAcqImageOnly
        ImageCam.IsAcqImageOnly = AcqImageOnly
        Return True
    End Function
    ''' <summary>計算作業派工執行緒</summary>
    ''' <remarks></remarks>
    Dim mThreadStart As New System.Threading.ThreadStart(AddressOf Dispatch)
    ''' <summary>計算作業派工執行緒</summary>
    ''' <remarks></remarks>
    Dim mThread As New System.Threading.Thread(mThreadStart)

    ''' <summary>執行緒事件通知</summary>
    ''' <remarks></remarks>
    Dim mAutoWait As New System.Threading.AutoResetEvent(False)

    Dim mCCDTimeStopWatch As New Stopwatch

    ''' <summary>派工作業</summary>
    ''' <remarks></remarks>
    Sub Dispatch()
        Dim mAcqResult As New sAOIStepConfig
        Do
            Dim mIsAllIdle As Boolean
            mIsAllIdle = True
            'For mCCDNo As Integer = 0 To AcqImageQueue.Count - 1 '對每一支CCD
            Try
                If AcqImageQueue.Count > 0 Then '如果有待處理影像
                    mCCDTimeStopWatch.Restart()

                    Dim mCCDNo As Integer = AcqImageQueue(0).CCDNo
                    SyncLock AcqImageQueue
                        gSyslog.CCDSave("CAOICognexVPRO Dispatch time 1: " & mCCDTimeStopWatch.ElapsedMilliseconds & " ms", CSystemLog.eCCDMessageProcess.Add)
                        Dim mSceneID As String = AcqImageQueue(0).SceneID '取得最前面的場景ID
                        mIsAllIdle = False
                        If cogTB_AlignList(mSceneID).IsRunFinish = True Then '如果該場景可用

                            If AcqImageQueue.TryDequeue(mAcqResult) = True Then '將資料取出
                                'gSyslog.CCDSave("CCD" & (mCCDNo + 1) & "取出影像(" & mAcqResult.Ticket & ")進行計算(" & mAcqResult.SceneID & ")",CSystemLog.eCCDMessageProcess.Add)
                                cogTB_AlignList(mAcqResult.SceneID).InputImage = mAcqResult.OutputImage '影像丟入計算流程
                                cogTB_AlignList(mAcqResult.SceneID).SceneName = mAcqResult.SceneID '場景名稱
                                gSyslog.CCDSave("CAOICognexVPRO Dispatch time 2: " & mCCDTimeStopWatch.ElapsedMilliseconds & " ms", CSystemLog.eCCDMessageProcess.Add)
                                'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
                                'cogTB_AlignList(mAcqResult.SceneID).Run(False, mAcqResult.Ticket, mCCDNo, False) '計算 傳遞Ticket
                                cogTB_AlignList(mAcqResult.SceneID).Run(False, mAcqResult.Ticket, mCCDNo, True) '計算 傳遞Ticket '2017.02.17
                                'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]
                                gSyslog.CCDSave("CAOICognexVPRO Dispatch time 3: " & mCCDTimeStopWatch.ElapsedMilliseconds & " ms", CSystemLog.eCCDMessageProcess.Add)
                            End If
                        End If
                    End SyncLock

                End If
            Catch ex As Exception
                gSyslog.CCDSave("Dispatch Exception: " & ex.Message & " " & ex.StackTrace, CSystemLog.eCCDMessageProcess.Add)
                ErrorMessage = ex.Message & " " & ex.ToString
            End Try

            'Next

            If mIsAllIdle Then '四支CCD都Idle,封鎖執行緒
                mAutoWait.WaitOne()
            End If
            If mIsDisposing Then
                Exit Sub
            End If
        Loop

    End Sub
    ''' <summary>定位工具列表</summary>
    ''' <remarks></remarks>
    Dim WithEvents cogTB_AlignList As New Dictionary(Of String, CThreadCogToolBlock) '(CThreadCogToolBlock.ToolBlockCommandType.Alignment)
    ' ''' <summary>檢測工具列表</summary>
    ' ''' <remarks></remarks>
    'Public WithEvents cogTB_InspectList As New Dictionary(Of String, CThreadCogToolBlock)

    ''' <summary>取得定位工具清單</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignmentToolNameList() As String() Implements IAOIInterface.GetAlignmentToolNameList
        Dim data(cogTB_AlignList.Count - 1) As String
        For i As Integer = 0 To cogTB_AlignList.Count - 1
            data(i) = cogTB_AlignList.Keys(i)
        Next
        Return data
    End Function

    ''' <summary>Cognex判定旗標 0為定位 1為檢測</summary>
    ''' <remarks></remarks>
    Dim mCognexRun_F As enmCCDRunType

    ''' <summary>定位計算前</summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub cogTB_AlignList_OnBeforeRun(sender As CThreadCogToolBlock, ByVal e As AOIEventArgs)
        If e.SceneName = "" Then
            ErrorMessage = "SceneID Not Exists, before Alignment"
            MsgBox(ErrorMessage, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        IsCCDReceivedDataOK(e.ChannelNo) = False

    End Sub
    Dim mCCDTimeStopWatch1 As New Stopwatch
    ''' <summary>定位計算完成,取得資料輸出</summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub cogTB_AlignList_OnRunSuccess(sender As CThreadCogToolBlock, ByVal e As AOIEventArgs)
        mCCDTimeStopWatch1.Restart()
        Try
            'Debug.Print("cogTB_AlignList_OnRunSuccess@CAOICognexVPRO")
            Dim tmp As New sAlignPos
            Dim mKeyName As String
            mKeyName = cogTB_AlignList(e.SceneName).OutputParam.GetCCDDataKeyName(eData.OutputImage)
            If cogTB_AlignList(e.SceneName).Subject.Outputs.Contains(mKeyName) Then '輸出影像應唯一
                cogTB_AlignList(e.SceneName).OutputImage = cogTB_AlignList(e.SceneName).Subject.Outputs(mKeyName).Value
            End If

            If gCCDAlignResultDict(e.CCDNo).ContainsKey(e.Ticket) Then '如已有結果(ScanPos...etc)
                tmp = gCCDAlignResultDict(e.CCDNo)(e.Ticket) '將資料取出 進行資料填寫
                tmp.Result.Clear()
            End If

            ''可搜尋的最大值
            'If cogTB_AlignList(e.SceneName).Subject.Outputs.Contains(cogTB_AlignList(e.SceneName).OutputParam.Data(eData.MaxNumberToFind)) Then
            '    tmp.MaxCount = cogTB_AlignList(e.SceneName).Subject.Outputs(cogTB_AlignList(e.SceneName).OutputParam.Data(eData.MaxNumberToFind)).Value
            'Else
            '    tmp.MaxCount = 1
            'End If
            gSyslog.CCDSave("OnRunSuccess 1: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            mKeyName = cogTB_AlignList(e.SceneName).OutputParam.GetCCDDataKeyName(eData.ResultCount)
            If cogTB_AlignList(e.SceneName).Subject.Outputs.Contains(mKeyName) Then '輸出數量存在
                tmp.Count = cogTB_AlignList(e.SceneName).Subject.Outputs(mKeyName).Value
            Else
                tmp.Count = 0
            End If

            'wenda 0727 
            Dim PMAligntool As Cognex.VisionPro.PMAlign.CogPMAlignTool = Nothing
            Dim CircleTool As Cognex.VisionPro.Caliper.CogFindCircleTool = Nothing
            For i As Integer = 0 To cogTB_AlignList(e.SceneName).Subject.Tools.Count - 1
                If cogTB_AlignList(e.SceneName).Subject.Tools(i).GetType = GetType(Cognex.VisionPro.PMAlign.CogPMAlignTool) Then
                    PMAligntool = cogTB_AlignList(e.SceneName).Subject.Tools(i)
                End If
                If cogTB_AlignList(e.SceneName).Subject.Tools(i).GetType = GetType(Cognex.VisionPro.Caliper.CogFindCircleTool) Then
                    CircleTool = cogTB_AlignList(e.SceneName).Subject.Tools(i)
                End If
            Next

            '因應用上並不一定使用PMAlign
            'If tool Is Nothing Then
            '    MsgBox("Please use PMAlignTool")
            '    'RaiseEvent OnRunSuccessed(sender, e)
            '    IsCCDReceiveBusy(e.ChannelNo) = False
            '    IsCCDReceivedDataOK(e.ChannelNo) = True
            '    Exit Sub
            'End If
            gSyslog.CCDSave("OnRunSuccess 2: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            Dim mAlignResult As sAlignResult = cogTB_AlignList(e.SceneName).OutputParam.GetAlignResult(cogTB_AlignList(e.SceneName), e.SceneName, e.CCDNo) '嘗試撈資料,撈的到則加入結果

            'wenda 20160614
            If tmp.Count <> 0 Then '資料數量不為0時,才加入資料
                If Not IsNothing(mAlignResult) Then
                    tmp.Result.Add(mAlignResult)
                End If
            Else
                tmp.Result.Clear()
            End If
            gSyslog.CCDSave("OnRunSuccess 3: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            tmp.Image = cogTB_AlignList(e.SceneName).InputImage
            If Not PMAligntool Is Nothing Then
                tmp.CogPMAlignResults = PMAligntool.Results
            End If

            If Not CircleTool Is Nothing Then
                tmp.CogFindCircleResults = CircleTool.Results
            End If

            tmp.Scene = e.SceneName '設定場景名稱紀錄
            tmp.IsRunSuccess = True '運算完成
            If gCCDAlignResultDict(e.CCDNo).ContainsKey(e.Ticket) Then '如已有結果(ScanPos...etc)
                gCCDAlignResultDict(e.CCDNo)(e.Ticket) = tmp  '將資料取出 進行資料填寫
            Else
                gCCDAlignResultDict(e.CCDNo).Add(e.Ticket, tmp) '將計算完成資料傳回
            End If

            'Dim fileName As String = "D:\PIIData\AOI-CCD" & (e.CCDNo + 1).ToString & Date.Now.Year.ToString("0000") & Date.Now.Month.ToString("00") & Date.Now.Day.ToString("00") & ".log"
            'Dim sw As New System.IO.StreamWriter(fileName, True)
            'Dim data As String = "CCD" & (e.CCDNo + 1).ToString & vbTab _
            '             & " Ticket:" & e.Ticket & vbTab _
            '             & " Scene:" & tmp.Scene & vbTab _
            '             & " Count:" & tmp.Count
            'For mResultNo As Integer = 0 To tmp.Count - 1
            '    If mResultNo < tmp.Result.Count Then
            '        data &= "OffsetX(um):" & tmp.Result(mResultNo).OffsetX & vbTab _
            '        & "OffsetY(um):" & tmp.Result(mResultNo).OffsetY & vbTab _
            '        & "PixelOffsetX:" & tmp.Result(mResultNo).PixelOffsetX & vbTab _
            '        & "PixelOffsetY:" & tmp.Result(mResultNo).PixelOffsetY & vbTab _
            '        & "PixelTranslationX:" & tmp.Result(mResultNo).PixelTranslationX & vbTab _
            '        & "PixelTranslationY:" & tmp.Result(mResultNo).PixelTranslationY & vbTab _
            '    & "Result(Boolean):" & tmp.Result(mResultNo).Result & vbTab _
            '    & "Rotation(Deg):" & tmp.Result(mResultNo).Rotation & vbTab _
            '    & "Score:" & tmp.Result(mResultNo).Score & vbTab _
            '        & "SkipMark(Boolean):" & tmp.Result(mResultNo).SkipMark
            '    End If
            'Next
            gSyslog.CCDSave("OnRunSuccess 4: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            'sw.WriteLine(data)
            'sw.Close()
            'Debug.Print("結果. 最大: " & tmp.MaxCount)
            'wenda  20160614
            If gCCDAlignResultDict(e.CCDNo)(e.Ticket).Count = 0 Then '無結果
                'gCCDAlignResultDict(e.CCDNo)(e.Ticket).Result.Clear() 
                'Debug.Print("無定位結果")
                RaiseEvent OnRunSuccessed(sender, e)
                IsCCDReceiveBusy(e.ChannelNo) = False
                IsCCDReceivedDataOK(e.ChannelNo) = True
                Exit Sub
            End If
            gSyslog.CCDSave("OnRunSuccess 5: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            'Dim AcceptScore As Decimal
            ''[Note]為了讓使用者看到失敗的分數
            'If cogTB_AlignList(e.SceneName).Subject.Inputs.Contains("AcceptScore") Then
            '    AcceptScore = cogTB_AlignList(e.SceneName).Subject.Inputs("AcceptScore").Value
            'Else
            '    AcceptScore = 0.1
            'End If

            ''[Note]如果實際分數低於接受分數，將定位數量改成0(因流程接用數量做判斷)
            'If gCCDAlignResultDict(e.CCDNo)(e.Ticket).Result(0).Score < AcceptScore Then
            '    gCCDAlignResultDict(e.CCDNo)(e.Ticket).Count = 0
            'End If

            With gCCDAlignResultDict(e.CCDNo)(e.Ticket)
                gSyslog.CCDSave("CCD" & (e.CCDNo + 1).ToString & "定位場景:" & e.SceneName & "計算(" & e.Ticket & ")結果. 數量:" & .Count & ",分數: " & Math.Round(.Result(0).Score, 3) & ",pixel偏移量(" & Math.Round(.Result(0).PixelTranslationX, 3) & "," & Math.Round(.Result(0).PixelTranslationY, 3) & "),偏移量(" & Math.Round(.Result(0).AbsOffsetX, 3) & "," & Math.Round(.Result(0).AbsOffsetY, 3) & "), Deg: " & Math.Round(.Result(0).Rotation, 3), CSystemLog.eCCDMessageProcess.Add)
            End With
            gSyslog.CCDSave("OnRunSuccess 6: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            RaiseEvent OnRunSuccessed(sender, e)
            gSyslog.CCDSave("OnRunSuccess 7: " & mCCDTimeStopWatch1.ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
            IsCCDReceiveBusy(e.ChannelNo) = False
            IsCCDReceivedDataOK(e.ChannelNo) = True

        Catch ex As Exception
            MsgBox("CCD計算傳回值設定錯誤." & ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

        IsCCDReceiveBusy(e.ChannelNo) = False
        IsCCDReceivedDataOK(e.ChannelNo) = True
        'Debug.Print("HandkeShake: True Busy:False @cogTB_AlignList_OnRunSuccess")
    End Sub


    ''' <summary>[CCD StopWatch]</summary>
    ''' <remarks></remarks>
    Dim mCCDStopWatch As New Stopwatch

    ''' <summary>命令發送</summary>
    ''' <param name="CCDHandshakeData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CCDSendCommand(CCDHandshakeData As String) As Boolean Implements IAOIInterface.CCDSendCommand
        'gblnCCDHandshake = False
        'gblnCCDReceiveBusy = True
        'Debug.Print("CCDSendCommand is NOT Supported.")
        gSyslog.Save("CCDSendCommand" & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
        mCCDStopWatch.Restart()
        Return True
    End Function

    'Public Function DetermineCoverRate(CoverRateData() As String, CcdOutputResult As Boolean) As enmDetermineCCDGlueCoverRate Implements IAOIInterface.DetermineCoverRate
    '    'Debug.Print("DetermineCoverRate is NOT Supported.")
    '    gSyslog.Save("DetermineCoverRate" & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
    '    Return enmDetermineCCDGlueCoverRate.eTotoalCoverRateFail
    'End Function

    'Public Function DetermineFix(ByVal sys As sSysParam, ByVal nodeID As String, ByVal alignSceneName As Integer, ByVal alignScene2Name As Integer, ByVal inspectSceneName As Integer, CcdOutputResult As Boolean) As enmDetermineCCDFix Implements IAOIInterface.DetermineFix
    '    Select Case gCRecipe.Node(sys.StageNo)(nodeID).enmAlignType
    '        Case enmAlignType.DevicePos1
    '            '比較結果失敗
    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(alignSceneName) Then
    '                If gCCDAlignResultDict(sys.StageNo)(alignSceneName).Result(0).Result <> Cognex.VisionPro.CogToolResultConstants.Accept Then
    '                    Return enmDetermineCCDFix.eSimilarFail
    '                End If
    '                '對位分數檢查不良

    '                If gCCDAlignResultDict(sys.StageNo)(alignSceneName).Result(0).Score < gCRecipe.CcdFunction.intCcdSimilarThreshold Then
    '                    '[說明]:辨識NG
    '                    '[說明]:紀錄Scan良率
    '                    With gSSystemParameter.ProductState
    '                        .lngCCDScanNGPcs = .lngCCDScanNGPcs + 1
    '                        .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '                    End With
    '                    Return enmDetermineCCDFix.eSimilarFail
    '                End If
    '            End If

    '            '檢測分數檢查不良
    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(inspectSceneName) Then
    '                If gCCDAlignResultDict(sys.StageNo)(inspectSceneName).Result(0).Score > gCRecipe.CcdFunction.intCcdFixGlueCoverRateThreshold Then
    '                    '[說明]:已經點過膠了
    '                    '[說明]:紀錄Scan良率
    '                    With gSSystemParameter.ProductState
    '                        .lngCCDScanNGPcs = .lngCCDScanNGPcs + 1
    '                        .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '                    End With

    '                    Return enmDetermineCCDFix.eTotoalCoverRateFail
    '                End If
    '            End If

    '            '[說明]:OK
    '            '[說明]:紀錄Scan良率
    '            With gSSystemParameter.ProductState
    '                .lngCCDScanOKPcs = .lngCCDScanOKPcs + 1
    '                .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '            End With
    '            Return enmDetermineCCDFix.eOK

    '        Case enmAlignType.DevicePos2
    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(alignSceneName) Then
    '                '比較結果失敗
    '                If gCCDAlignResultDict(sys.StageNo)(alignSceneName).Result(0).Result <> Cognex.VisionPro.CogToolResultConstants.Accept Then
    '                    Return enmDetermineCCDFix.eSimilarFail
    '                End If
    '            End If

    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(alignScene2Name) Then
    '                '比較結果失敗
    '                If gCCDAlignResultDict(sys.StageNo)(alignScene2Name).Result(0).Result <> Cognex.VisionPro.CogToolResultConstants.Accept Then
    '                    Return enmDetermineCCDFix.eSimilarFail
    '                End If
    '            End If

    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(alignSceneName) Then
    '                '對位分數檢查不良
    '                If gCCDAlignResultDict(sys.StageNo)(alignSceneName).Result(0).Score < gCRecipe.CcdFunction.intCcdSimilarThreshold Then
    '                    '[說明]:辨識NG
    '                    '[說明]:紀錄Scan良率
    '                    With gSSystemParameter.ProductState
    '                        .lngCCDScanNGPcs = .lngCCDScanNGPcs + 1
    '                        .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '                    End With
    '                    Return enmDetermineCCDFix.eSimilarFail
    '                End If
    '            End If

    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(alignScene2Name) Then
    '                '對位分數檢查不良
    '                If gCCDAlignResultDict(sys.StageNo)(alignScene2Name).Result(0).Score < gCRecipe.CcdFunction.intCcdSimilarThreshold Then
    '                    '[說明]:辨識NG
    '                    '[說明]:紀錄Scan良率
    '                    With gSSystemParameter.ProductState
    '                        .lngCCDScanNGPcs = .lngCCDScanNGPcs + 1
    '                        .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '                    End With
    '                    Return enmDetermineCCDFix.eSimilarFail
    '                End If
    '            End If
    '            If gCCDAlignResultDict(sys.StageNo).ContainsKey(inspectSceneName) Then
    '                '檢測分數檢查不良
    '                If gCCDAlignResultDict(sys.StageNo)(inspectSceneName).Result(0).Score > gCRecipe.CcdFunction.intCcdFixGlueCoverRateThreshold Then
    '                    '[說明]:已經點過膠了
    '                    '[說明]:紀錄Scan良率
    '                    With gSSystemParameter.ProductState
    '                        .lngCCDScanNGPcs = .lngCCDScanNGPcs + 1
    '                        .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '                    End With

    '                    Return enmDetermineCCDFix.eTotoalCoverRateFail
    '                End If
    '            End If

    '            '[說明]:OK
    '            '[說明]:紀錄Scan良率
    '            With gSSystemParameter.ProductState
    '                .lngCCDScanOKPcs = .lngCCDScanOKPcs + 1
    '                .lngTotalCCDScanPcs = .lngTotalCCDScanPcs + 1
    '            End With
    '            Return enmDetermineCCDFix.eOK

    '    End Select
    '    Return enmDetermineCCDFix.eCCDOutputFail
    'End Function

    Public Function SetImageMode(ImageMode As ProjectCore.enmCCDImageShowType, Optional ImageDisplayingNo As Integer = 0) As Boolean Implements IAOIInterface.SetImageMode
        IsCCDReceiveBusy(0) = False
        IsCCDReceivedDataOK(0) = True
        'Debug.Print("HandkeShake: True Busy:False @SetImageMode")
        gSyslog.Save("SetImageMode" & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
        Return True
    End Function

    ''' <summary>是否取像未結束</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDCBusy(ByVal channelNo As Integer) As Boolean Implements IAOIInterface.IsCCDCBusy
        Return Not ImageCam.IsRunFinish
    End Function

    Public Function IsCCDReady(ByVal channelNo As Integer) As Boolean Implements IAOIInterface.IsCCDReady
        If ImageCam.IsRunFinish Then
            'gblnCCDReceiveBusy(realCCDNo) = False
            'Debug.Print("HandkeShake: ??? Busy:False @IsCCDReady")
            Return True
        End If
        Return False
    End Function

    ''' <summary>計算判定是否不良</summary>
    ''' <param name="ticket"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCCDResultNG(ticket As Integer, ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.IsCCDResultNG
        Select Case mCognexRun_F
            Case enmCCDRunType.Fix
                If gCCDAlignResultDict(ccdNo).ContainsKey(ticket) Then
                    If gCCDAlignResultDict(ccdNo)(ticket).Result(0).Result = enmResultConstants.Accept Then
                        Return False
                    Else
                        Return True
                    End If
                End If
                Return True
                'Case enmCCDRunType.ScanGlue
                '    If gCCDInspectResultDict.ContainsKey(ticket) Then
                '        If gCCDInspectResultDict(ticket).Result = Cognex.VisionPro.CogToolResultConstants.Accept Then
                '            Return False
                '        Else
                '            Return True
                '        End If
                '    End If
                '    Return True
            Case Else
                Return True
        End Select
    End Function

    Public ReadOnly Property IsCCDTimeOut As Boolean Implements IAOIInterface.IsCCDTimeOut
        Get
            Try
                If mCCDStopWatch.ElapsedMilliseconds >= gSSystemParameter.StableTime.CcdTimeOutTime Then
                    mCCDStopWatch.Stop()
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                mCCDStopWatch.Stop()
                Return True
            End Try
        End Get
    End Property


    ''' <summary>CCD光源 Mapping</summary>
    ''' <param name="ccd"></param>
    ''' <param name="light"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CCDLightMapping(ByVal ccd As Integer, ByVal light As enmValveLight) As enmLight

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                Select Case ccd
                    Case enmCCD.CCD1
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No1
                            Case enmValveLight.No2
                                Return enmLight.No2
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD2
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No3
                            Case enmValveLight.No2
                                Return enmLight.No4
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD3
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No5
                            Case enmValveLight.No2
                                Return enmLight.No6
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD4
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No7
                            Case enmValveLight.No2
                                Return enmLight.No8
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                End Select

            Case enmMachineType.eDTS_2S2V
                Select Case ccd
                    Case enmCCD.CCD1
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No1
                            Case enmValveLight.No2
                                Return enmLight.No2
                            Case enmValveLight.No3
                                Return enmLight.No3
                            Case enmValveLight.No4
                                Return enmLight.No4
                        End Select
                    Case enmCCD.CCD2
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No5
                            Case enmValveLight.No2
                                Return enmLight.No6
                            Case enmValveLight.No3
                                Return enmLight.No7
                            Case enmValveLight.No4
                                Return enmLight.No8
                        End Select
                End Select

            Case Else
                Select Case ccd
                    Case enmCCD.CCD1
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No1
                            Case enmValveLight.No2
                                Return enmLight.No2
                            Case enmValveLight.No3
                                Return enmLight.No3
                            Case enmValveLight.No4
                                Return enmLight.No4
                        End Select
                End Select

        End Select
        Return -1
    End Function
    Public Function SelectCCDScence(ByVal channelNo As Integer, ScenceName As String) As Boolean Implements IAOIInterface.SetCCDScene
        If ImageCam Is Nothing Then
            MsgBox("ImageCam Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        ImageCam.SceneName = ScenceName
        'If gAOICollection.SceneDictionary.ContainsKey(ScenceName) Then
        'SetExposure(channelNo, gAOICollection.SceneDictionary(ScenceName).CCDExposureTime) '曝光設定
        'End If
        'Debug.Print("cogTB_ACQ0.ScenceName =" & ImageCam.SceneName)
        Return True
    End Function


    Public Function SetCCDRunType(ByVal channelNo As Integer, enmRun As ProjectCore.enmCCDRunType) As Boolean Implements IAOIInterface.SetCCDRunType
        mCognexRun_F = enmRun
        IsCCDReceiveBusy(channelNo) = False
        IsCCDReceivedDataOK(channelNo) = True
        'Debug.Print("IsCCDReceiveBusy(" & channelNo & ")=False,IsCCDReceivedDataOK(" & channelNo & ")=True")
        Return True
    End Function


    ''' <summary>取得定位偏移量(um)</summary>
    ''' <param name="channelNo"></param>
    ''' <param name="ticket"></param>
    ''' <param name="alignResult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignOffset(ByVal channelNo As Integer, ByVal ticket As Integer, ByRef alignResult As List(Of sAlignResult), ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.GetAlignOffset
        alignResult = gCCDAlignResultDict(ccdNo)(ticket).Result
        Return True
    End Function

    ''' <summary>是否釋放資源</summary>
    ''' <remarks></remarks>
    Dim mIsDisposing As Boolean = False
    ''' <summary>關閉連線時,解除DoLoop鎖定</summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements IAOIInterface.Close
        mIsDisposing = True
        ImageCam.Subject.Operator.FrameGrabber.Disconnect(False) 'Soni + 2016.09.24 CCD斷線
        ImageCam.Subject.Dispose() 'Soni + 2016.09.24 CCD資源釋放 無效
        mAutoWait.Set()
    End Sub

    'Public Function GetInspectioResult(ccdIndex As Integer, sceneName As String, ByRef result As Dictionary(Of String, Double)) As Boolean Implements IAOIInterface.GetInspectioResult
    '    'Debug.Print("GetInspectioResult is NOT Supported.")
    '    gSyslog.Save("GetInspectioResult" & gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
    '    Return False
    'End Function

    Public Sub SaveResult()
        Dim fileName As String

        For mCCDNo As Integer = 0 To 3
            fileName = "D:\PIIData\CCD" & (mCCDNo + 1) & " .log"
            Dim sw As New System.IO.StreamWriter(fileName)
            For mTicket As Integer = 0 To gCCDAlignResultDict(mCCDNo).Count - 1
                If gCCDAlignResultDict(mCCDNo)(mTicket).Result.Count > 0 Then
                    sw.WriteLine(gCCDAlignResultDict(mCCDNo)(mTicket).Result(0).PixelTranslationX & "," & gCCDAlignResultDict(mCCDNo)(mTicket).Result(0).PixelTranslationY & "," & gCCDAlignResultDict(mCCDNo)(mTicket).Result(0).Rotation)
                Else
                    sw.WriteLine("NA,NA,NA")
                End If

            Next
            sw.Close()
        Next

    End Sub

    Private mTaskTokenSource As CancellationTokenSource = New CancellationTokenSource()

    Private mTaskToken As CancellationToken

    Private mRunLongProcessTask As Task(Of Integer) = Nothing

    ''' <summary>
    '''非同步顯示結果
    ''' </summary>
    ''' <param name="ccdIndex"></param>
    ''' <param name="ticket"></param>
    ''' <param name="ucDisp"></param>
    ''' <param name="ccdNo"></param>
    ''' <remarks></remarks>
    Async Function SubShowAlignResult(ccdIndex As Integer, ticket As Integer, ByVal ucDisp As ucDisplay, ByVal ccdNo As Integer) As Task(Of Boolean)

        mTaskTokenSource = New CancellationTokenSource()
        mTaskToken = mTaskTokenSource.Token
        'Dim sSelectedItem As String = cmbBox.SelectedItem.ToString()
        'Dim msysCCDNo As Integer = sys.CCDNo



        mRunLongProcessTask = Task.Run(Function()

                                           If Not gCCDAlignResultDict(ccdNo).ContainsKey(ticket) Then
                                               'Debug.Print("GG ticket@ShowAlignResult")
                                               gSyslog.CCDSave("GG ticket@ShowAlignResult :" & ticket, CSystemLog.eCCDMessageProcess.Add)
                                               Return 1

                                           End If
                                           If gCCDAlignResultDict(ccdNo)(ticket).Image Is Nothing Then
                                               'Debug.Print("GG Image@ShowAlignResult")
                                               gSyslog.CCDSave("GG Image@ShowAlignResult :" & ticket, CSystemLog.eCCDMessageProcess.Add)
                                               Return 1

                                           End If
                                           ucDisp.CogDisplay1.Image = gCCDAlignResultDict(ccdNo)(ticket).Image
                                           ucDisp.CogDisplay1.StaticGraphics.Clear()
                                           '=== 特徵位置標記 ===
                                           'For i As Integer = 0 To gCCDAlignResultDict(ccdNo)(ticket).Result.Count - 1
                                           '[Note]定位成功結果(只辨識出一組才正常)
                                           If gCCDAlignResultDict(ccdNo)(ticket).Result.Count = 1 Then
                                               Dim marker As New CogPointMarker()
                                               marker.X = gCCDAlignResultDict(ccdNo)(ticket).Result(0).PixelTranslationX
                                               marker.Y = gCCDAlignResultDict(ccdNo)(ticket).Result(0).PixelTranslationY
                                               marker.Color = CogColorConstants.Green
                                               ucDisp.CogDisplay1.StaticGraphics.Add(marker, "#")
                                           End If
                                           'Next
                                           '=== 特徵位置標記 ===

                                           '[Note]原應使用AlignType做分別，但Logan在產線須使用不模組化的Cognex，故沒有AlignType，只好用存在工具判斷
                                           '[Note]FindCircle > PMAlign  


                                           Dim mShownText As New Cognex.VisionPro.CogGraphicLabel '[Note]顯示結果數量
                                           If Not gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults Is Nothing Then
                                               '=== 1顯示FindCircle結果數量 ===
                                               mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") Results:" & gCCDAlignResultDict(ccdNo)(ticket).Result.Count
                                               If gCCDAlignResultDict(ccdNo)(ticket).Result.Count = 1 Then
                                                   If gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults IsNot Nothing Then

                                                       Dim mShownText2 As New Cognex.VisionPro.CogGraphicLabel
                                                       mShownText2.X = 10
                                                       mShownText2.Y = 140 '+ 60 '* i
                                                       mShownText2.Color = CogColorConstants.Green
                                                       mShownText2.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                                       mShownText2.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft

                                                       Dim mGetCircle As ICogGraphic
                                                       mGetCircle = gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults.GetCircle.CopyBase(CogCopyShapeConstants.All)
                                                       mGetCircle.Color = CogColorConstants.Green
                                                       ucDisp.CogDisplay1.StaticGraphics.Add(mGetCircle, "#")

                                                       With gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults
                                                           mShownText2.Text = .GetCircle.CenterX.ToString("0.00") & " " & .GetCircle.CenterY.ToString("0.00")
                                                       End With
                                                       ucDisp.CogDisplay1.StaticGraphics.Add(mShownText2, "#")
                                                   End If
                                               End If

                                           ElseIf Not gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults Is Nothing Then
                                               '=== 2顯示PMAlign結果數量 ===
                                               mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") Results:" & gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count

                                               If gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count <> 0 Then
                                                   For i As Integer = 0 To gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count - 1
                                                       Dim mShownText2 As New Cognex.VisionPro.CogGraphicLabel
                                                       mShownText2.X = 10
                                                       mShownText2.Y = 140 + 70 * i
                                                       mShownText2.Color = CogColorConstants.Green
                                                       mShownText2.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                                       mShownText2.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft

                                                       ucDisp.CogDisplay1.StaticGraphics.Add(gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).CreateResultGraphics(Cognex.VisionPro.PMAlign.CogPMAlignResultGraphicConstants.MatchRegion), "#")
                                                       With gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).GetPose
                                                           mShownText2.Text = Math.Round(.TranslationX, 2) & " " & Math.Round(.TranslationY, 2) & " " & gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).Score.ToString("0.00") & " " & Math.Round(.Rotation, 2)
                                                       End With
                                                       ucDisp.CogDisplay1.StaticGraphics.Add(mShownText2, "#")
                                                   Next
                                               End If

                                           Else
                                               '=== 3無結果數量 ===
                                               mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") "
                                           End If

                                           mShownText.X = 10
                                           mShownText.Y = 70
                                           mShownText.Color = CogColorConstants.Green
                                           mShownText.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                           mShownText.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft
                                           ucDisp.CogDisplay1.StaticGraphics.Add(mShownText, "#")

                                           'Return True
                                           Return 0
                                       End Function, mTaskToken)

        Await mRunLongProcessTask

        Select Case mRunLongProcessTask.Result
            Case 0 'Success'
                Return True
            Case 1
                Return False
            Case Else
                Return False
        End Select


    End Function


    Public Function ShowAlignResult(ccdIndex As Integer, ticket As Integer, ByVal ucDisp As ucDisplay, ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.ShowAlignResult

        Dim State As Boolean = True
        '[Note] 因Lambda內不能使用ByRef,先把ucDisp 改成ByVal測試
        ucDisp.BeginInvoke(Sub()

                               Try
                                   'SyncLock ucDisp 'Soni + 2016.09.09 加同步鎖, 測試是否能避免閃爍
                                   If Not gCCDAlignResultDict(ccdNo).ContainsKey(ticket) Then
                                       'Debug.Print("GG ticket@ShowAlignResult")
                                       gSyslog.CCDSave("GG ticket@ShowAlignResult :" & ticket, CSystemLog.eCCDMessageProcess.Add)
                                       'Return False
                                       State = False
                                       Exit Sub
                                   End If
                                   If gCCDAlignResultDict(ccdNo)(ticket).Image Is Nothing Then
                                       'Debug.Print("GG Image@ShowAlignResult")
                                       gSyslog.CCDSave("GG Image@ShowAlignResult :" & ticket, CSystemLog.eCCDMessageProcess.Add)
                                       'Return False
                                       State = False
                                       Exit Sub
                                   End If
                                   ucDisp.CogDisplay1.Image = gCCDAlignResultDict(ccdNo)(ticket).Image

                                   ucDisp.CogDisplay1.StaticGraphics.Clear()
                                   '=== 特徵位置標記 ===
                                   'For i As Integer = 0 To gCCDAlignResultDict(ccdNo)(ticket).Result.Count - 1
                                   '[Note]定位成功結果(只辨識出一組才正常)
                                   If gCCDAlignResultDict(ccdNo)(ticket).Result.Count = 1 Then
                                       Dim marker As New CogPointMarker()
                                       marker.X = gCCDAlignResultDict(ccdNo)(ticket).Result(0).PixelTranslationX
                                       marker.Y = gCCDAlignResultDict(ccdNo)(ticket).Result(0).PixelTranslationY
                                       marker.Color = CogColorConstants.Green
                                       ucDisp.CogDisplay1.StaticGraphics.Add(marker, "#")
                                   End If
                                   'Next
                                   '=== 特徵位置標記 ===

                                   '[Note]原應使用AlignType做分別，但Logan在產線須使用不模組化的Cognex，故沒有AlignType，只好用存在工具判斷
                                   '[Note]FindCircle > PMAlign  


                                   Dim mShownText As New Cognex.VisionPro.CogGraphicLabel '[Note]顯示結果數量
                                   If Not gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults Is Nothing Then
                                       '=== 1顯示FindCircle結果數量 ===
                                       mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") Results:" & gCCDAlignResultDict(ccdNo)(ticket).Result.Count
                                       If gCCDAlignResultDict(ccdNo)(ticket).Result.Count = 1 Then
                                           If gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults IsNot Nothing Then

                                               Dim mShownText2 As New Cognex.VisionPro.CogGraphicLabel
                                               mShownText2.X = 10
                                               mShownText2.Y = 140 '+ 60 '* i
                                               mShownText2.Color = CogColorConstants.Green
                                               mShownText2.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                               mShownText2.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft

                                               Dim mGetCircle As ICogGraphic
                                               mGetCircle = gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults.GetCircle.CopyBase(CogCopyShapeConstants.All)
                                               mGetCircle.Color = CogColorConstants.Green
                                               ucDisp.CogDisplay1.StaticGraphics.Add(mGetCircle, "#")

                                               With gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults
                                                   mShownText2.Text = .GetCircle.CenterX.ToString("0.00") & " " & .GetCircle.CenterY.ToString("0.00")
                                               End With
                                               ucDisp.CogDisplay1.StaticGraphics.Add(mShownText2, "#")
                                           End If
                                       End If

                                   ElseIf Not gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults Is Nothing Then
                                       '=== 2顯示PMAlign結果數量 ===
                                       mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") Results:" & gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count

                                       If gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count <> 0 Then
                                           For i As Integer = 0 To gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count - 1
                                               Dim mShownText2 As New Cognex.VisionPro.CogGraphicLabel
                                               mShownText2.X = 10
                                               mShownText2.Y = 140 + 70 * i
                                               mShownText2.Color = CogColorConstants.Green
                                               mShownText2.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                               mShownText2.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft

                                               ucDisp.CogDisplay1.StaticGraphics.Add(gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).CreateResultGraphics(Cognex.VisionPro.PMAlign.CogPMAlignResultGraphicConstants.MatchRegion), "#")
                                               With gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).GetPose
                                                   mShownText2.Text = Math.Round(.TranslationX, 2) & " " & Math.Round(.TranslationY, 2) & " " & gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).Score.ToString("0.00") & " " & Math.Round(.Rotation, 2)
                                               End With
                                               ucDisp.CogDisplay1.StaticGraphics.Add(mShownText2, "#")
                                           Next
                                       End If

                                   Else
                                       '=== 3無結果數量 ===
                                       mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") "
                                   End If

                                   mShownText.X = 10
                                   mShownText.Y = 70
                                   mShownText.Color = CogColorConstants.Green
                                   mShownText.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                   mShownText.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft
                                   ucDisp.CogDisplay1.StaticGraphics.Add(mShownText, "#")


                                   ''=== 顯示PMAlign結果數量 ===
                                   'If Not gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults Is Nothing Then
                                   '    mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") Results:" & gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count
                                   'Else
                                   '    mShownText.Text = "CCD" & (ccdNo + 1).ToString & " " & gCCDAlignResultDict(ccdNo)(ticket).Scene & "(" & ticket & ") "
                                   'End If
                                   'mShownText.X = 10
                                   'mShownText.Y = 60
                                   'mShownText.Color = CogColorConstants.Green
                                   'mShownText.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                   'mShownText.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft
                                   'ucDisp.CogDisplay1.StaticGraphics.Add(mShownText, "#")
                                   ''=== 顯示PMAlign結果數量 ===

                                   ''=== 顯示PMAlign結果數值 ===
                                   'If Not gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults Is Nothing Then
                                   '    If gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count <> 0 Then
                                   '        For i As Integer = 0 To gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults.Count - 1
                                   '            Dim mShownText2 As New Cognex.VisionPro.CogGraphicLabel
                                   '            mShownText2.X = 10
                                   '            mShownText2.Y = 120 + 60 * i
                                   '            mShownText2.Color = CogColorConstants.Green
                                   '            mShownText2.Font = New Font("微軟正黑體", 14, FontStyle.Bold)
                                   '            mShownText2.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft

                                   '            ucDisp.CogDisplay1.StaticGraphics.Add(gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).CreateResultGraphics(Cognex.VisionPro.PMAlign.CogPMAlignResultGraphicConstants.MatchRegion), "#")
                                   '            With gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).GetPose
                                   '                mShownText2.Text = gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults(i).Score.ToString("0.00") & " " & Math.Round(.TranslationX, 2) & " " & Math.Round(.TranslationY, 2) & " " & Math.Round(.Rotation, 2)
                                   '            End With
                                   '            ucDisp.CogDisplay1.StaticGraphics.Add(mShownText2, "#")
                                   '        Next
                                   '    End If
                                   'End If
                                   ''=== 顯示PMAlign結果數值 ===
                                   State = True
                                   'Return True
                                   'Exit Sub

                                   'End SyncLock
                               Catch ex As Exception
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012105) & ex.Message & " " & ex.StackTrace, "Alarm_2012105", eMessageLevel.Alarm)
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2012105) & ex.Message & " " & ex.StackTrace, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "ShowAlignResult")
                                   State = False
                                   'Return False
                                   'Exit Sub
                               End Try
                               ''Eason 20170228 Ticket:100035 , Free Memory [S] 不管存不存圖都要清除原本的
                               gCCDAlignResultDict(ccdNo)(ticket).Image = Nothing 'Soni + 2016.09.09 顯示後, 清除影像 '[Note]移至ShowAlignResult最後處理
                               gCCDAlignResultDict(ccdNo)(ticket).CogPMAlignResults = Nothing 'Soni + 2016.09.09 顯示後, 清除影像結果 '[Note]移至ShowAlignResult最後處理
                               gCCDAlignResultDict(ccdNo)(ticket).CogFindCircleResults = Nothing 'Soni + 2016.09.09 顯示後, 清除影像結果 '[Note]移至ShowAlignResult最後處理
                               ''Eason 20170228 Ticket:100035 , Free Memory [E]
                           End Sub
            )
        gSyslog.CCDSave("ShowAlignResult :" & State, CSystemLog.eCCDMessageProcess.Add)
        Return State
    End Function

    ''' <summary>取得教導框大小, 用於比例教導位置計算</summary>
    ''' <param name="sceneName"></param>
    ''' <param name="sideXLength"></param>
    ''' <param name="sideYLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignTrainSideLength(ByVal sceneName As String, ByRef sideXLength As Decimal, ByRef sideYLength As Decimal, ByRef rotation As Decimal) As Boolean Implements IAOIInterface.GetAlignTrainSideLength
        Try
            'Dim tool As Cognex.VisionPro.PMAlign.CogPMAlignTool = cogTB_AlignList(sceneName).Subject.Tools(0)
            Dim tool As Cognex.VisionPro.PMAlign.CogPMAlignTool = Nothing
            For i As Integer = 0 To cogTB_AlignList(sceneName).Subject.Tools.Count - 1
                If cogTB_AlignList(sceneName).Subject.Tools(i).GetType = GetType(Cognex.VisionPro.PMAlign.CogPMAlignTool) Then
                    tool = cogTB_AlignList(sceneName).Subject.Tools(i)
                End If
            Next
            If tool Is Nothing Then
                MsgBox("Please use PMAlignTool", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If

            Dim Region As ICogRegion
            Dim typ = tool.Pattern.TrainRegion.GetType()
            Region = tool.Pattern.TrainRegion
            Select Case typ.FullName
                Case "Cognex.VisionPro.CogRectangle"
                    Dim sender As CogRectangle = CType(Region, CogRectangle)
                    sideXLength = sender.Width '教導Pattern大小
                    sideYLength = sender.Height '教導Pattern大小
                    rotation = 0
                Case "Cognex.VisionPro.CogRectangleAffine"
                    Dim sender As CogRectangleAffine = CType(Region, CogRectangleAffine)
                    sideXLength = sender.SideXLength '教導Pattern大小
                    sideYLength = sender.SideYLength '教導Pattern大小
                    rotation = sender.Rotation '教導Pattern角度
                Case "Cognex.VisionPro.CogCircle"
                    Dim sender As CogCircle = CType(Region, CogCircle)
                    sideXLength = sender.Radius * 2 '教導Pattern大小
                    sideYLength = sender.Radius * 2 '教導Pattern大小
                    rotation = 0
                Case "Cognex.VisionPro.CogEllipse"
                    Dim sender As CogEllipse = CType(Region, CogEllipse)
                    sideXLength = sender.RadiusX * 2
                    sideYLength = sender.RadiusY * 2
                    rotation = sender.Rotation
                Case "Cognex.VisionPro.CogPolygon"
                    Dim sender As CogPolygon = CType(Region, CogPolygon)
                    Dim obj As New CogRectangle
                    sender.FitToBoundingBox(obj)
                    sideXLength = obj.Width '教導Pattern大小
                    sideYLength = obj.Height '教導Pattern大小
                    rotation = 0
                Case "Cognex.VisionPro.CogCircularAnnulusSection"
                    Dim sender As CogCircularAnnulusSection = CType(Region, CogCircularAnnulusSection)
                    sideXLength = sender.Radius * 2 '教導Pattern大小
                    sideYLength = sender.Radius * 2 '教導Pattern大小
                    rotation = 0
                Case "Cognex.VisionPro.CogEllipticalAnnulusSection"
                    Dim sender As CogEllipticalAnnulusSection = CType(Region, CogEllipticalAnnulusSection)
                    sideXLength = sender.RadiusX * 2
                    sideYLength = sender.RadiusY * 2
                    rotation = sender.Rotation
            End Select
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function
    ''' <summary>取得取像影像大小, 用於比例教導位置計算</summary>
    ''' <param name="ccdIndex">CCD索引</param>
    ''' <param name="sideXLength">寬</param>
    ''' <param name="sideYLength">高</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAcquistionSideLength(ccdIndex As Integer, ByRef sideXLength As Integer, ByRef sideYLength As Integer) As Boolean Implements IAOIInterface.GetAcquistionSideLength
        If ImageCam Is Nothing Then '取像物件不存在
            Return False
        End If
        If ImageCam.OutputImage Is Nothing Then '取像物件未取得影像
            Return False
        End If
        sideXLength = ImageCam.OutputImage.Width
        sideYLength = ImageCam.OutputImage.Height
        Return True
    End Function

    Public Function SetAlignImage(ByVal channelNo As Integer, ByVal sceneName As String) As Boolean Implements IAOIInterface.SetAlignImage
        If Not cogTB_AlignList.ContainsKey(sceneName) Then '無指定場景
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012107), "Alarm_2012107", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012107) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Scene does NOT Exist.")
            Return False
        End If
        If cogTB_AlignList(sceneName).Subject Is Nothing Then '定位場景應存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012108), "Alarm_2012108", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012108) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Subject does NOT Exist.")
            Return False
        End If
        If cogTB_AlignList(sceneName).Subject.Inputs Is Nothing Then '輸入物件應存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012104), "Alarm_2012104", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012104) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Subject.Inputs does NOT Exist.")
            Return False
        End If
        If cogTB_AlignList(sceneName).Subject.Inputs.Count = 0 Then '輸入物件應存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012110), "Alarm_2012110", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012110) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Subject.Inputs.Count is 0.")
            Return False
        End If

        If Not cogTB_AlignList(sceneName).Subject.Inputs.Contains("InputImage") Then '輸入應包含輸入影像
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012104), "Alarm_2012104", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012104) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Alignment InputImage Property does Not Exist.")
            Return False
        End If

        If ImageCam.Subject Is Nothing Then '取像物件不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012000), "Alarm_2012000", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012000) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Acquistion Tool does Not Exist.")
            Return False
        End If

        If ImageCam.Subject.OutputImage Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012002), "Alarm_2012002", eMessageLevel.Alarm)
            gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
            MsgBox(gMsgHandler.GetMessage(Alarm_2012002) & " SceneName:" & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("SetAlignImage Error: Acquistion OutputImage Property does Not Exist.")
            Return False
        End If
        'If ImageCam.Subject.Outputs Is Nothing Then '取像物件不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012002), "Alarm_2012002", eMessageLevel.Alarm)
        '    gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2012002) & " SceneName:" & sceneName)
        '    'MsgBox("SetAlignImage Error: Acquistion Tool does Not Exist.")
        '    Return False
        'End If

        'If ImageCam.Subject.Outputs.Count = 0 Then '取像物件不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012001), "Alarm_2012001", eMessageLevel.Alarm)
        '    gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2012001) & " SceneName:" & sceneName)
        '    'MsgBox("SetAlignImage Error: Subject.Outputs.Count is 0.")
        '    Return False
        'End If

        'If Not ImageCam.Subject.Outputs.Contains("OutputImage") Then '輸出應包含輸出影像
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012002), "Alarm_2012002", eMessageLevel.Alarm)
        '    gSyslog.Save("SceneName:" & sceneName, , eMessageLevel.Alarm)
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2012002) & " SceneName:" & sceneName)
        '    'MsgBox("SetAlignImage Error: Acquistion OutputImage Property does Not Exist.")
        '    Return False
        'End If

        'cogTB_AlignList(sceneName).Subject.Inputs("InputImage").Value = ImageCam.Subject.Outputs("OutputImage").Value '將影像丟給對位工具
        cogTB_AlignList(sceneName).Subject.Inputs("InputImage").Value = ImageCam.Subject.OutputImage '將影像丟給對位工具
        Return True
    End Function


    'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
    Public Sub Clear() Implements IAOIInterface.Clear

        For Each item In cogTB_AlignList
            item.Value.Dispose()
        Next
        cogTB_AlignList.Clear()

    End Sub
    'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mAutoWait.Dispose()
                ImageCam.Dispose()
            End If

        End If
        Me.disposedValue = True

    End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function GetSceneList() As String() Implements IAOIInterface.GetSceneList
        Dim mSceneList(cogTB_AlignList.Count - 1) As String
        For mSceneNo As Integer = 0 To cogTB_AlignList.Count - 1
            mSceneList(mSceneNo) = cogTB_AlignList.Keys(mSceneNo)
        Next
        Return mSceneList
    End Function

    Public Function GetAcqTool() As Object Implements IAOIInterface.GetAcqTool

        If ImageCam Is Nothing Then
            Return Nothing
        End If
        If ImageCam.Subject Is Nothing Then
            Return Nothing
        End If
        'If ImageCam.Subject.Tools Is Nothing Then
        '    Return Nothing
        'End If
        'If ImageCam.Subject.Tools.Count = 0 Then
        '    Return Nothing
        'End If
        'Return ImageCam.Subject.Tools(0) '取出AcqFifoTool工具
        Return ImageCam.Subject
    End Function

    Dim ImageConvet As New Cognex.VisionPro.ImageProcessing.CogImageConvertTool
    Public Function GetAcqOutputImage() As Object Implements IAOIInterface.GetAcqOutputImage
        If ImageCam.OutputImage IsNot Nothing Then
            If ImageCam.OutputImage.ToBitmap.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
                '[Note]mono 8 bit 黑白
                Return ImageCam.OutputImage
            Else
                '[Note]其他顏色
                ImageConvet.InputImage = ImageCam.OutputImage
                ImageConvet.RunParams.RunMode = ImageProcessing.CogImageConvertRunModeConstants.Intensity
                ImageConvet.Run()
                Return ImageConvet.OutputImage
            End If
        End If

        Return ImageCam.OutputImage
    End Function

    Public Function GetToolBlock(sceneName As String) As Object Implements IAOIInterface.GetToolBlock
        If Not cogTB_AlignList.ContainsKey(sceneName) Then
            Return Nothing
        End If
        Return cogTB_AlignList(sceneName).Subject
    End Function

    Public Function AcquireImage() As Boolean Implements IAOIInterface.AcquireImage
        Call ImageCam.Subject.Run()
        Return True
    End Function

    Public Function IsCCDExist() As Boolean Implements IAOIInterface.IsCCDExist

        If ImageCam Is Nothing Then
            Return False
        End If
        If ImageCam.Subject Is Nothing Then
            Return False
        End If

        'If ImageCam.Subject.Tools.Count = 0 Then
        '    Return False
        'End If
        'If ImageCam.Subject.Tools(0).GetType() <> GetType(Cognex.VisionPro.CogAcqFifoTool) Then
        '    Return False
        'End If
        'Dim acqfifo As Cognex.VisionPro.CogAcqFifoTool = ImageCam.Subject.Tools(0) '取得取像物件
        Dim acqfifo As Cognex.VisionPro.CogAcqFifoTool = ImageCam.Subject '取得取像物件
        If acqfifo.Operator Is Nothing Then
            Return False
        End If

        Return True
    End Function

    Public Function SaveSceneOutputParam(sceneName As String, fileName As String) As Boolean Implements IAOIInterface.SaveSceneOutputParam
        cogTB_AlignList(sceneName).OutputParam.Save(fileName)
        Return True
    End Function

    Public Function OutputParam(ByVal sceneName As String) As COutputParam Implements IAOIInterface.OutputParam
        Return cogTB_AlignList(sceneName).OutputParam
    End Function

    Public Function SetSceneInputImage(SceneName As String, ByRef image As Object) As Boolean Implements IAOIInterface.SetSceneInputImage

        With cogTB_AlignList
            If Not .ContainsKey(SceneName) Then
                MsgBox("Selected Align Tool Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End With
        If Not cogTB_AlignList.ContainsKey(SceneName) Then
            MsgBox("Select Tool NOT Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        Dim alignObj = cogTB_AlignList(SceneName)
        If alignObj.Subject Is Nothing Then
            MsgBox("Align Subject Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        If Not alignObj.Subject.Inputs.Contains("InputImage") Then
            MsgBox("Align Input 'InputImage' Not Exists.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        alignObj.Subject.Inputs("InputImage").Value = image
        alignObj.InputImage = image
        Return True
    End Function

    Public Function RemoveScene(sceneName As String) As Boolean Implements IAOIInterface.RemoveScene
        If Not cogTB_AlignList.ContainsKey(sceneName) Then
            Return False
        End If
        'Eason 20170217 Ticket:100032 , Memory Free Part3
        cogTB_AlignList(sceneName).Dispose()


        cogTB_AlignList.Remove(sceneName)
        Return True
    End Function

    Public Function CreateScene(sceneName As String, ByVal mAlignType As eAlignType, ByVal mScenePath As String) As Boolean Implements IAOIInterface.CreateScene
        If cogTB_AlignList.ContainsKey(sceneName) Then
            If cogTB_AlignList(sceneName).Subject Is Nothing Then
                cogTB_AlignList(sceneName).Subject = New Cognex.VisionPro.ToolBlock.CogToolBlock() '建立底層物件
            End If
            Return False
        End If
        Select Case mAlignType
            Case eAlignType.PMAlign '0
                LoadVision(Application.StartupPath & "\Library\Template.vpp", sceneName) '載入樣板作為預設值

            Case eAlignType.Corner '1
                LoadVision(Application.StartupPath & "\Library\SampleCorner.vpp", sceneName) '載入樣板作為預設值

            Case eAlignType.Circle '2
                LoadVision(Application.StartupPath & "\Library\SampleCircle.vpp", sceneName) '載入樣板作為預設值

            Case eAlignType.Lane '3
                LoadVision(Application.StartupPath & "\Library\SampleLane.vpp", sceneName) '載入樣板作為預設值

            Case eAlignType.LoadFile '4
                LoadVision(mScenePath, sceneName)

            Case eAlignType.Blob '5
                LoadVision(Application.StartupPath & "\Library\SampleBlob.vpp", sceneName) '載入樣板作為預設值

        End Select

        'cogTB_AlignList.Add(sceneName, New CThreadCogToolBlock()) '新增多緒版物件
        'cogTB_AlignList(sceneName).Subject = New Cognex.VisionPro.ToolBlock.CogToolBlock() '建立底層物件
        Return True
    End Function

    Public Function IsSceneExist(SceneName As String) As Boolean Implements IAOIInterface.IsSceneExist
        If SceneName Is Nothing Then
            Return False
        End If

        If Not cogTB_AlignList.ContainsKey(SceneName) Then
            Return False
        End If
        If cogTB_AlignList.Item(SceneName).Subject Is Nothing Then
            Return False
        End If
        Return True
    End Function

    Public Function IsCalcFinish(sceneName As String) As Boolean Implements IAOIInterface.IsCalcFinish
        If Not cogTB_AlignList.ContainsKey(sceneName) Then
            Return True
        End If
        Return cogTB_AlignList(sceneName).IsRunFinish
    End Function

    Public Function SetAlignImage(ByVal channelNo As Integer) As Boolean Implements IAOIInterface.SetAlignImage

        For mAlignToolNo As Integer = 0 To cogTB_AlignList.Count - 1
            Dim keyName As String = cogTB_AlignList.Keys(mAlignToolNo)
            SetAlignImage(channelNo, keyName)
        Next
        Return True
    End Function


    Dim CalibBoard As New CogCalibCheckerboardTool
    Public Function CalibBoardCalibration(ByVal channelNo As Integer, ByVal InputImage As Object, ByVal Train As Boolean, ByVal TileSize As Decimal) As Object Implements IAOIInterface.CalibBoardCalibration
        If channelNo < 0 Then '索引不存在, 當作模擬處理
            Return True
        End If
        CalibBoard.InputImage = InputImage
        Try
            If Train = True Then
                CalibBoard.Calibration.CalibrationImage = InputImage
                CalibBoard.Calibration.ComputationMode = CogCalibFixComputationModeConstants.Linear
                CalibBoard.Calibration.DOFsToCompute = CogCalibCheckerboardDOFConstants.ScalingAspectRotationSkewAndTranslation
                CalibBoard.Calibration.FiducialMark = CogCalibCheckerboardFiducialConstants.None
                CalibBoard.Calibration.PhysicalTileSizeX = TileSize
                CalibBoard.Calibration.PhysicalTileSizeY = TileSize
                CalibBoard.Calibration.Calibrate()
                CalibBoard.RunParams.SpaceToOutput = CogCalibSpaceToOutputConstants.UncalibratedSpace '[Note]座標軸不校正
                CalibBoard.Run()
                Return CalibBoard.OutputImage '[Note]影像已經過校正
            Else
                CalibBoard.Run()
                If CalibBoard.RunStatus.Result = CogToolResultConstants.Accept Then
                    If CalibBoard.OutputImage Is Nothing Then
                        Return InputImage 'ImageCam.OutputImage
                    Else
                        Return CalibBoard.OutputImage
                    End If
                Else
                    Return InputImage 'ImageCam.OutputImage
                End If
            End If
        Catch ex As Exception

            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return InputImage 'ImageCam.OutputImage
        End Try

    End Function
End Class


Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports ProjectRecipe

Public Class CAOIOmronFZS2MTCP
    Implements IAOIInterface

    Public Function Copy() As IAOIInterface Implements IAOIInterface.Copy
        Return Me.MemberwiseClone
    End Function

    Public Event OnRunSuccessed(sender As Object, ByVal e As AOIEventArgs) Implements IAOIInterface.OnRunSuccessed

    ''' <summary>錯誤訊息</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorMessage As String Implements IAOIInterface.ErrorMessage

    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsIntialOK As Boolean Implements IAOIInterface.IsIntialOK
        Get
            Return mIsIntialOK
        End Get
    End Property

    'TODO: Wenda Omron Pending. 因應資料分拆, gblnCCDReceiveBusy,gblnCCDHandshake須重新確認
    Dim mblnCCDReceiveBusy(3) As Boolean
    Dim mblnCCDHandshake(3) As Boolean
    Dim mblnCCDSetNextScence(3) As Boolean '取像後是否設定下一組場景參數
    ''' <summary>
    ''' [資料接收中]
    ''' </summary>
    ''' <remarks></remarks>
    Public Property gblnCCDReceiveBusy(ByVal realCCDNo As Integer) As Boolean Implements IAOIInterface.IsCCDReceiveBusy
        Get
            Return mblnCCDReceiveBusy(realCCDNo)
        End Get
        Set(value As Boolean)
            mblnCCDReceiveBusy(realCCDNo) = value
        End Set
    End Property
    ''' <summary>
    ''' [CCD Handshake(接收到的資料格式正確)回應成功 為true, 失敗為false]
    ''' </summary>
    ''' <remarks></remarks>
    Public Property gblnCCDHandshake(ByVal realCCDNo As Integer) As Boolean Implements IAOIInterface.IsCCDReceivedDataOK
        Get
            Return mblnCCDHandshake(realCCDNo)
        End Get
        Set(value As Boolean)
            mblnCCDHandshake(realCCDNo) = value
        End Set
    End Property
    ''' <summary>
    ''' [CCD取像後是否設定下一組場景參數]
    ''' </summary>
    ''' <param name="realCCDNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsCCDSetNextScene(ByVal realCCDNo As Integer) As Boolean Implements IAOIInterface.IsCCDSetNextScene
        Get
            Return mblnCCDSetNextScence(realCCDNo)
        End Get
        Set(value As Boolean)
            mblnCCDSetNextScence(realCCDNo) = value
        End Set
    End Property
    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Private mIsIntialOK As Boolean

    Public ReadOnly Property SockStatus As SockStatusInfo Implements IAOIInterface.SockStatus
        Get
            Return SockStatusInfo.sckDisconnect
        End Get
    End Property

    Public Function Initial(ByVal ccdConnectionParameter As sCCDConnectionParameter) As Boolean Implements IAOIInterface.Initial
        'Debug.Print("Initial Function is NOT Supported.")
        Return False
    End Function

    'Public Function CCDRemeasure(ByVal sceneName As String) As Integer Implements IAOIInterface.CCDRemeasure
    '    'Debug.Print("CCDConnect is NOT Supported.")
    '    Return 0
    'End Function

    Public Function CCDSendCommand(CCDHandshakeData As String) As Boolean Implements IAOIInterface.CCDSendCommand
        gblnCCDHandshake(0) = False
        gblnCCDReceiveBusy(0) = True

        Return True
    End Function

    Public Function SetImageMode(ImageMode As ProjectCore.enmCCDImageShowType, Optional ImageDisplayingNo As Integer = 0) As Boolean Implements IAOIInterface.SetImageMode
        '[說明]:將CCD取像模式切成靜態模式(Set the image mode for the specified image display wimdow)
        '       0 0  -->動態
        '       0 1  -->靜態
        Call CCDSendCommand("IDC " & ImageDisplayingNo.ToString() & " " & CInt(ImageMode).ToString())
        Return True
    End Function

    Public Function IsCCDCBusy(ByVal ccdIndex As Integer) As Boolean Implements IAOIInterface.IsCCDCBusy
        If gSSystemParameter.IsBypassCCD = True Then
            Return False
        End If

        Return gDICollection.GetState(enmDI.CcdBusy, False)
    End Function

    Public Function IsCCDReady(ByVal ccdIndex As Integer) As Boolean Implements IAOIInterface.IsCCDReady
        If gSSystemParameter.IsBypassCCD = True Then
            Return True
        End If

        Return gDICollection.GetState(enmDI.CcdReady, True)
    End Function

    Public Function IsCCDResultNG(ByVal ticket As Integer, ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.IsCCDResultNG
        Return gDICollection.GetState(enmDI.CcdOutputResult, False)
    End Function
    Dim mCCDStopWatch As New Stopwatch

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


    ''' <summary>觸發計數</summary>
    ''' <remarks></remarks>
    Dim mTicket As Integer

    Public Function SetCCDTrigger(ByVal ccdIndex As Integer, OnOff As ProjectMotion.enmONOFF, ByVal isAcqImageOnly As Boolean, ByVal isSetNextScene As Boolean) As Integer Implements IAOIInterface.SetCCDTrigger
        If OnOff = enmONOFF.eON Then
            gblnCCDHandshake(0) = False
            gblnCCDReceiveBusy(0) = True
            IsCCDSetNextScene(0) = isSetNextScene
            gDOCollection.SetState(enmDO.CcdImageTrigger, True)
            mTicket += 1
        Else
            gDOCollection.SetState(enmDO.CcdImageTrigger, False)
        End If
        Return mTicket
    End Function

#Region "不支援"
    'Public Function LoadOnFlyScene(ByVal CCDNo As Integer, ByVal ScenceList() As String) As Boolean Implements IAOIInterface.LoadOnFlyScene
    '    Return True
    'End Function
    'Public Function SetOnFlyCCDNextScene(ByVal realCCDNo As Integer, ByVal SceneCount As UInteger) As Boolean Implements IAOIInterface.SetOnFlyCCDNextScene
    '    Return True
    'End Function

    Public Function SelectCCDScence(ByVal ccdIndex As Integer, ScenceName As String) As Boolean Implements IAOIInterface.SetCCDScene
        'Call CCDSendCommand(CType(gAOICollection.Items(0), CAOIKeyenceCV200CTCP).gCVX100Generator.SetConfigDetect(ScenceName))
        'Debug.Print("SelectCCDScence Function is NOT Supported.")
        Return False
    End Function

    Public Function SetCCDRunType(ByVal realCCDNo As Integer, enmRun As ProjectCore.enmCCDRunType) As Boolean Implements IAOIInterface.SetCCDRunType
        'Debug.Print("CCDConnect Function is NOT Supported.")
        Return False
    End Function

    Public Function GetAlignOffset(ByVal ccdIndex As Integer, ByVal ticket As Integer, ByRef alignResult As List(Of sAlignResult), ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.GetAlignOffset
        'Debug.Print("GetAlignPos Function is NOT Supported.")
        Return False
    End Function

    Public Sub Close() Implements IAOIInterface.Close

    End Sub

    'Public Function GetInspectioResult(ccdIndex As Integer, sceneName As String, ByRef result As Dictionary(Of String, Double)) As Boolean Implements IAOIInterface.GetInspectioResult
    '    'Debug.Print("GetInspectioResult Function is NOT Supported.")
    '    Return False
    'End Function

    Public Function ShowAlignResult(ccdIndex As Integer, ByVal ticket As Integer, ByVal ucDisp As ucDisplay, ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.ShowAlignResult
        'Debug.Print("ShowAlignResult Function is NOT Supported.")
        Return False
    End Function
    Public Function GetAlignTrainSideLength(sceneName As String, ByRef sideXLength As Decimal, ByRef sideYLength As Decimal, ByRef rotation As Decimal) As Boolean Implements IAOIInterface.GetAlignTrainSideLength
        'Debug.Print("GetAlignTrainSideLength Function is NOT Supported.")
        Return False
    End Function
    Public Function GetAcquistionSideLength(ccdIndex As Integer, ByRef sideXLength As Integer, ByRef sideYLength As Integer) As Boolean Implements IAOIInterface.GetAcquistionSideLength
        'Debug.Print("GetAcquistionSideLength Function is NOT Supported.")
        Return False
    End Function

    Public Function LoadVision(strFileName As String, Optional sceneName As String = "") As Boolean Implements IAOIInterface.LoadVision
        'Debug.Print("LoadVision Function is NOT Supported.")
        Return False
    End Function

    Public Function SetAlignImage(ccdIndex As Integer, ByVal sceneName As String) As Boolean Implements IAOIInterface.SetAlignImage
        'Debug.Print("LoadVision Function is NOT Supported.")
        Return False
    End Function

    Public Function GetAlignmentToolNameList() As String() Implements IAOIInterface.GetAlignmentToolNameList
        'Debug.Print("GetAlignmentToolNameList Function is Not Supported.")
        Dim data As String() = {""}
        Return data
    End Function

    Public Function SetExposure(ccdIndex As Integer, exposure_ms As Decimal) As Boolean Implements IAOIInterface.SetExposure
        'Debug.Print("SetExposure  Function is NOT Supported.")
        Return True
    End Function

    Public Function GetExposure(ccdIndex As Integer) As Double Implements IAOIInterface.GetExposure
        'Debug.Print("GetExposure Function is NOT Supported.")
        Return 0
    End Function

#End Region

    Public Function GetSceneList() As String() Implements IAOIInterface.GetSceneList
        Return Nothing
    End Function

    Public Function GetAcqTool() As Object Implements IAOIInterface.GetAcqTool
        Return Nothing
    End Function

    Public Function GetAcqOutputImage() As Object Implements IAOIInterface.GetAcqOutputImage
        Return Nothing
    End Function

    Public Function GetToolBlock(sceneName As String) As Object Implements IAOIInterface.GetToolBlock
        Return Nothing
    End Function

    Public Function AcquireImage() As Boolean Implements IAOIInterface.AcquireImage
        Return True
    End Function

    Public Function IsCCDExist() As Boolean Implements IAOIInterface.IsCCDExist
        If SockStatus = SockStatusInfo.sckConnected Then
            Return True
        End If
        Return False
    End Function

    Public Function SaveSceneOutputParam(sceneName As String, fileName As String) As Boolean Implements IAOIInterface.SaveSceneOutputParam
        Return False
    End Function

    Public Function SetSceneInputImage(SceneName As String, ByRef image As Object) As Boolean Implements IAOIInterface.SetSceneInputImage
        Return False
    End Function

    Public Function RemoveScene(sceneName As String) As Boolean Implements IAOIInterface.RemoveScene
        Return False
    End Function

    Public Function CreateScene(sceneName As String, ByVal mAlignType As eAlignType, ByVal mScenePath As String) As Boolean Implements IAOIInterface.CreateScene
        Return False
    End Function

    Public Function IsSceneExist(SceneName As String) As Boolean Implements IAOIInterface.IsSceneExist
        Return False
    End Function

    Public Function IsCalcFinish(sceneName As String) As Boolean Implements IAOIInterface.IsCalcFinish
        Return False
    End Function

    Public Function SetAlignImage1(realCCDNo As Integer) As Boolean Implements IAOIInterface.SetAlignImage
        Return False
    End Function

    Public Function OutputParam(sceneName As String) As COutputParam Implements IAOIInterface.OutputParam
        Return Nothing
    End Function

    Public Function SetAutoWhiteBalance(ByVal channelNo As Integer, ByVal Enable As Boolean) As Boolean Implements IAOIInterface.SetAutoWhiteBalance
        Return False
    End Function
    Public Function SetAutoExposure(ByVal channelNo As Integer, ByVal Enable As Boolean) As Boolean Implements IAOIInterface.SetAutoExposure
        Return False
    End Function

    Public Function ClearTicket() As Boolean Implements IAOIInterface.ClearTicket
        Return True
    End Function

    Public Function SetLiveTriggerMode(channelNo As Integer, TriggerType As eTriggerType) As Boolean Implements IAOIInterface.SetLiveTriggerMode
        Return True
    End Function

    Public Function CalibBoardCalibration(channelNo As Integer, ByVal InputImage As Object, Train As Boolean, TileSize As Decimal) As Object Implements IAOIInterface.CalibBoardCalibration
        Return Nothing
    End Function

    'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
    Public Sub Clear() Implements IAOIInterface.Clear

    End Sub
    'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]

    Public Function SetHardwareTrigger(HardwareTrigger As Boolean) As Boolean Implements IAOIInterface.SetHardwareTrigger
        Return True
    End Function

    Public Function SetIsAcqImageOnly(AcqImageOnly As Boolean) As Boolean Implements IAOIInterface.SetIsAcqImageOnly
        Return True
    End Function

    Public Function SetIsStop(state As Boolean) As Boolean Implements IAOIInterface.SetIsStop
        Return True
    End Function
End Class

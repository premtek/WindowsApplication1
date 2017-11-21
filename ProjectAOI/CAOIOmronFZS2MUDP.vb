Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports ProjectRecipe

Public Structure sOmronConnect
    ''' <summary>發送IP位置</summary>
    ''' <remarks></remarks>
    Public SendIPAddress As String
    ''' <summary>發送通訊端口</summary>
    ''' <remarks></remarks>
    Public SendPort As String
    ''' <summary>接收IP位置</summary>
    ''' <remarks></remarks>
    Public RecieveIPAddress As String
    ''' <summary>接收通訊端口</summary>
    ''' <remarks></remarks>
    Public RecievePort As String
End Structure

Public Class CAOIOmronFZS2MUDP
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


    ''' <summary>[CCD初始化狀態]</summary>
    ''' <remarks></remarks>
    Private mIsIntialOK As Boolean

    Public ReadOnly Property SockStatus As SockStatusInfo Implements IAOIInterface.SockStatus
        Get
            If gOmronCcdUdp Is Nothing Then
                Return SockStatusInfo.sckError
            End If
            Return gOmronCcdUdp.SockStatus
        End Get
    End Property

    'TODO: Wenda Omron Pending. 因應資料分拆, gblnCCDReceiveBusy,gblnCCDHandshake須重新確認

    Dim mblnCCDReceiveBusy(enmCCD.ConstMax) As Boolean
    Dim mblnCCDHandshake(enmCCD.ConstMax) As Boolean
    Dim mblnCCDSetNextScence(enmCCD.ConstMax) As Boolean '取像後是否設定下一組場景參數
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

    ''' <summary>[Omron CCD 通訊物件]</summary>
    ''' <remarks></remarks>
    Public WithEvents gOmronCcdUdp As New CUDPIP

    ''' <summary>[Omron CCD 傳回值剖析]</summary>
    ''' <remarks></remarks>
    Public WithEvents gOmronCcdParser As New COMRONFZEthernetPasrser

    ''' <summary>[Omron CCD Error Message]</summary>
    ''' <remarks></remarks>
    Private Sub gOmronCcdUdp_OnErrorMessage(sender As Object, ByVal e As DataEventArgs) Handles gOmronCcdUdp.OnErrorMessage
        gEqpMsg.AddHistoryAlarm("Error_1012002", "CCD", , gMsgHandler.GetMessage(Error_1012002), eMessageLevel.Error) '[CCD通訊異常]
        gSyslog.Save(e.Data, "Error_1012002", eMessageLevel.Error)
    End Sub

    Private Sub gOmronCcdUdp_OnReceiveData(sender As Object, ByVal e As DataEventArgs) Handles gOmronCcdUdp.OnReceiveData
        Dim ReturnStatus As CVX100_EthernetN_Parser.ParserResult

        gblnCCDReceiveBusy(0) = True
        ReturnStatus = gOmronCcdParser.Parse(e.Data)
        If ReturnStatus = CVX100_EthernetN_Parser.ParserResult.Success Then
            gblnCCDHandshake(0) = True
        End If
        gblnCCDReceiveBusy(0) = False

    End Sub

    ''' <summary>初始化時記錄參數</summary>
    ''' <remarks></remarks>
    Dim mCcdConnectionParameter As sCCDConnectionParameter
    Public Function Initial(ByVal ccdConnectionParameter As sCCDConnectionParameter) As Boolean Implements IAOIInterface.Initial
        mCcdConnectionParameter = ccdConnectionParameter
        Call gOmronCcdUdp.ClientConnect(ccdConnectionParameter.Omron.RecieveIPAddress, ccdConnectionParameter.Omron.RecievePort)

        mIsIntialOK = True
        Return True
    End Function

    'Public Function CCDRemeasure(ByVal sceneName As String) As Integer Implements IAOIInterface.CCDRemeasure
    '    Call CCDSendCommand("Remeasure")
    '    Return 1
    'End Function
    Dim mCCDStopWatch As New Stopwatch
    Public Function CCDSendCommand(CCDHandshakeData As String) As Boolean Implements IAOIInterface.CCDSendCommand
        gblnCCDHandshake(0) = False
        gblnCCDReceiveBusy(0) = True

        gOmronCcdUdp.CCDSendAddress.intPort = mCcdConnectionParameter.Omron.SendPort ' gSSystemParameter.CCDSendAddress
        gOmronCcdUdp.CCDSendAddress.strIP = mCcdConnectionParameter.Omron.SendIPAddress
        Call gOmronCcdUdp.SendCommand(CCDHandshakeData)

        mCCDStopWatch.Restart()
        Return True
    End Function

    Public Function SetImageMode(ImageMode As ProjectCore.enmCCDImageShowType, Optional ImageDisplayingNo As Integer = 0) As Boolean Implements IAOIInterface.SetImageMode
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
    'Public Function LoadOnFlyScene(ByVal CCDNo As Integer, ByVal ScenceList() As String) As Boolean Implements IAOIInterface.LoadOnFlyScene
    '    Return True
    'End Function
    'Public Function SetOnFlyCCDNextScene(ByVal realCCDNo As Integer, ByVal SceneCount As UInteger) As Boolean Implements IAOIInterface.SetOnFlyCCDNextScene
    '    Return True
    'End Function

    Public Function SetCCDScene(ByVal realCCDNo As Integer, ScenceName As String) As Boolean Implements IAOIInterface.SetCCDScene
        Call CCDSendCommand("SCENE " & ScenceName.ToString())
        Return True
    End Function

    Public Function SetCCDRunType(ByVal realCCDNo As Integer, enmRun As ProjectCore.enmCCDRunType) As Boolean Implements IAOIInterface.SetCCDRunType
        Select Case enmRun
            Case enmCCDRunType.Fix
                gDOCollection.SetState(enmDO.CcdBit0, False)  '[False:為定位判斷 True:出膠判斷]

            Case enmCCDRunType.ScanGlue
                gDOCollection.SetState(enmDO.CcdBit0, True)  '[False:為定位判斷 True:出膠判斷]

        End Select
        Return True
    End Function

    Public Function SetCCDTrigger(ByVal realCCDNo As Integer, OnOff As ProjectMotion.enmONOFF, ByVal isAcqImageOnly As Boolean, ByVal isSetNextScene As Boolean) As Integer Implements IAOIInterface.SetCCDTrigger
        If OnOff = enmONOFF.eON Then
            gblnCCDHandshake(realCCDNo) = False
            gblnCCDReceiveBusy(realCCDNo) = True
            IsCCDSetNextScene(realCCDNo) = isSetNextScene
            gDOCollection.SetState(enmDO.CcdImageTrigger, True)
        Else
            gDOCollection.SetState(enmDO.CcdImageTrigger, False)
        End If
        Return True
    End Function

    ''' <summary></summary>
    ''' <param name="ccdIndex">CCD索引</param>
    ''' <param name="ticket">定位點索引</param>
    ''' <param name="alignResult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlignOffset(ByVal ccdIndex As Integer, ByVal ticket As Integer, ByRef alignResult As List(Of sAlignResult), ByVal ccdNo As Integer) As Boolean Implements IAOIInterface.GetAlignOffset

        Dim mAlignResult As New sAlignResult
        alignResult.Clear()
        Select Case gSSystemParameter.CoordType
            Case enmCoordinateRelationType.eDTS
                Select Case ticket
                    Case 0
                        mAlignResult.AbsOffsetX = CDbl(gOmronCcdParser.gstrCcdFix(enmCCDFix.eOffsetX))  'CCD 補正 X
                        mAlignResult.AbsOffsetY = -CDbl(gOmronCcdParser.gstrCcdFix(enmCCDFix.eOffsetY))  'CCD 補正 Y
                        mAlignResult.Rotation = CDbl(gOmronCcdParser.gstrCcdFix(enmCCDFix.eAngle))  'CCD 補正 角度
                        alignResult.Add(mAlignResult)
                    Case 1
                        mAlignResult.AbsOffsetX = CDbl(gOmronCcdParser.gstrCcdFix2(enmCCDFix.eOffsetX))  'CCD 補正 X
                        mAlignResult.AbsOffsetY = -CDbl(gOmronCcdParser.gstrCcdFix2(enmCCDFix.eOffsetY))  'CCD 補正 Y
                        mAlignResult.Rotation = CDbl(gOmronCcdParser.gstrCcdFix2(enmCCDFix.eAngle))  'CCD 補正 角度
                        alignResult.Add(mAlignResult)
                End Select

            Case Else
                mAlignResult.AbsOffsetX = CDbl(gOmronCcdParser.gstrCcdFix(enmCCDFix.eOffsetX))  'CCD 補正 X
                mAlignResult.AbsOffsetY = CDbl(gOmronCcdParser.gstrCcdFix(enmCCDFix.eOffsetY))  'CCD 補正 Y
                mAlignResult.Rotation = CDbl(gOmronCcdParser.gstrCcdFix(enmCCDFix.eAngle))  'CCD 補正 角度
                alignResult.Add(mAlignResult)
        End Select
        Return True
    End Function

    Public Sub Close() Implements IAOIInterface.Close
        If gOmronCcdUdp Is Nothing Then
            Exit Sub
        End If
        gOmronCcdUdp.Close()
    End Sub

#Region "不支援"

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
        'Debug.Print("SetAlignImage Function is NOT Supported.")
        Return False
    End Function

    'Public Function GetInputImage(ByVal ccdIndex As Integer) As Bitmap Implements IAOIInterface.GetInputImage
    '    'Debug.Print("GetInputImage is NOT Supported.")
    '    Return Nothing
    'End Function

    'Public Function GetImage(ByRef obj As Cognex.VisionPro.CogImageMaskEditV2, sceneName As String) As Boolean Implements IAOIInterface.GetImage
    '    'Debug.Print("GetImage is NOT Supported.")
    '    Return True
    'End Function

    Public Function GetAlignmentToolNameList() As String() Implements IAOIInterface.GetAlignmentToolNameList
        'Debug.Print("GetAlignmentToolNameList Function is NOT Supported.")
        Dim data As String() = {""}
        Return data
    End Function

    Public Function SetExposure(ccdIndex As Integer, exposure_ms As Decimal) As Boolean Implements IAOIInterface.SetExposure
        'Debug.Print("SetExposure Function is NOT Supported.")
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

    'Eason 20170217 Ticket:100032 , Memory Free Part3 [S]
    Public Sub Clear() Implements IAOIInterface.Clear

    End Sub
    'Eason 20170217 Ticket:100032 , Memory Free Part3 [E]

    Public Function CalibBoardCalibration(channelNo As Integer, ByVal InputImage As Object, Train As Boolean, TileSize As Decimal) As Object Implements IAOIInterface.CalibBoardCalibration
        Return Nothing
    End Function

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

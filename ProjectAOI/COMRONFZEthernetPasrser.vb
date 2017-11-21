Imports ProjectCore

Public Class COMRONFZEthernetPasrser

    Public Enum ParserResult
        ''' <summary>不明錯誤</summary>
        ''' <remarks></remarks>
        UnknownError = 0
        ''' <summary>成功</summary>
        ''' <remarks></remarks>
        Success = 1
        ''' <summary>命令錯誤</summary>
        ''' <remarks></remarks>
        CommandError = 2
        ''' <summary>命令運作禁止</summary>
        ''' <remarks></remarks>
        FobiddenError = 3
        ''' <summary>參數錯誤</summary>
        ''' <remarks></remarks>
        ParamError = 4
        ''' <summary>超時錯誤</summary>
        ''' <remarks></remarks>
        TimeoutError = 5
    End Enum

    ''' <summary>錯誤訊息
    ''' </summary>
    ''' <remarks></remarks>
    Private mErrorMessage As String

    Public Event ErrorOccurred(ByVal sender As Object, ByVal e As DataEventArgs)

    ''' <summary>
    ''' [CCD 2點定位 影像定位之資料內容]
    ''' </summary>
    ''' <remarks></remarks>
    Public gstrCcdFix2(4) As String


    ''' <summary>
    ''' [CCD Handshake 影像定位之資料內容]
    ''' </summary>
    ''' <remarks></remarks>
    Public gstrCcdFix(4) As String
    ''' <summary>
    ''' [CCD Handshake 影像判斷膠的覆蓋率]
    ''' </summary>
    ''' <remarks></remarks>
    Public gstrCcdGlueCover(5) As String
    ''' <summary>
    ''' [CCD Handshake Valve校正的偏移量]
    ''' </summary>
    ''' <remarks></remarks>
    Public gstrCcdCalibration(1) As String
    ''' <summary>
    ''' [CCD Handshake 四點校正座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public ForePointTeach(2) As String


    ''' <summary>
    ''' 定義CCD Calibration資料格式內容
    ''' </summary>
    ''' <remarks></remarks>
    Enum enmCCDCalibration
        eOffsetX = 0
        eOffsetY = 1
    End Enum


    Public Function Parse(ByVal msg As String) As ParserResult

        Dim ReceiveData() As String     '[接收到CCD的字串內容](分解完的資料)

        gSyslog.Save("CCD RecievedData(" & msg & ")")
        'Call WriteCCDReceiveData(msg)

        '[解說]:接收CCD的傳送的資料，利用逗點來判斷哪一種資料內容
        ReceiveData = Split(msg.Split(vbCrLf)(0), ",")

        If ReceiveData Is Nothing Then
            mErrorMessage = "dataCollection Is Nothing"
            gSyslog.Save(mErrorMessage, , eMessageLevel.Error)
            Dim e As New DataEventArgs
            e.Data = mErrorMessage
            RaiseEvent ErrorOccurred(Me, e)
            Return ParserResult.UnknownError
        End If
        If ReceiveData.GetUpperBound(0) < 0 Then
            mErrorMessage = "dataCollection.GetUpperBound(0) < 0"
            gSyslog.Save(mErrorMessage, , eMessageLevel.Error)
            Dim e As New DataEventArgs
            e.Data = mErrorMessage
            RaiseEvent ErrorOccurred(Me, e)
            Return ParserResult.UnknownError
        End If

        If ReceiveData.Length < 0 Then '資料不存在
            Return ParserResult.ParamError
        End If
        Select Case ReceiveData(0)
            Case 0 'Calibration???
                gstrCcdCalibration(enmCCDCalibration.eOffsetX) = ReceiveData(1).Trim()
                gstrCcdCalibration(enmCCDCalibration.eOffsetY) = ReceiveData(2).Trim()
                Return ParserResult.Success

            Case 1 '第一定位點
                Select Case ReceiveData.Length
                    Case 3
                        ForePointTeach(0) = ReceiveData(0).Trim()
                        ForePointTeach(1) = ReceiveData(1).Trim()
                        Return ParserResult.Success

                    Case 5
                        gstrCcdFix(enmCCDFix.eOffsetX) = ReceiveData(1).Trim()
                        gstrCcdFix(enmCCDFix.eOffsetY) = ReceiveData(2).Trim()
                        gstrCcdFix(enmCCDFix.eAngle) = ReceiveData(3).Trim()
                        gstrCcdFix(enmCCDFix.eSimilar) = ReceiveData(4).Trim()
                        Return ParserResult.Success
                    Case 6
                        gstrCcdFix(enmCCDFix.eOffsetX) = ReceiveData(1).Trim()
                        gstrCcdFix(enmCCDFix.eOffsetY) = ReceiveData(2).Trim()
                        gstrCcdFix(enmCCDFix.eAngle) = ReceiveData(3).Trim()
                        gstrCcdFix(enmCCDFix.eSimilar) = ReceiveData(4).Trim()
                        gstrCcdFix(enmCCDFix.eGlueCoverRate) = ReceiveData(5).Trim()
                        Return ParserResult.Success
                End Select
            Case 2 '點膠後檢查
                Select Case ReceiveData.Length
                    Case 3
                        gstrCcdGlueCover(enmCCDGlueCover.eSimilar) = ReceiveData(1).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eTotalCoverRate) = ReceiveData(2).Trim()
                        Return ParserResult.Success
                    Case 6
                        gstrCcdGlueCover(enmCCDGlueCover.eSimilar) = ReceiveData(1).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eTotalCoverRate) = ReceiveData(2).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo1CoverRate) = ReceiveData(3).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo2CoverRate) = ReceiveData(4).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo3CoverRate) = ReceiveData(5).Trim()
                        Return ParserResult.Success
                    Case 7
                        gstrCcdGlueCover(enmCCDGlueCover.eSimilar) = ReceiveData(1).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eTotalCoverRate) = ReceiveData(2).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo1CoverRate) = ReceiveData(3).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo2CoverRate) = ReceiveData(4).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo3CoverRate) = ReceiveData(5).Trim()
                        gstrCcdGlueCover(enmCCDGlueCover.eAreaNo4CoverRate) = ReceiveData(6).Trim()
                        Return ParserResult.Success
                End Select
            Case 3 '第二定位點
                gstrCcdFix2(enmCCDFix.eOffsetX) = ReceiveData(1).Trim()
                gstrCcdFix2(enmCCDFix.eOffsetY) = ReceiveData(2).Trim()
                gstrCcdFix2(enmCCDFix.eAngle) = ReceiveData(3).Trim()
                gstrCcdFix2(enmCCDFix.eSimilar) = ReceiveData(4).Trim()
                Return ParserResult.Success


            Case 33 '點膠前檢查
                Select Case ReceiveData.Length
                    Case 5

                        '    DetectData(DetectDataCount, 7) = ReceiveData(1).Trim()
                        '    DetectData(DetectDataCount, 8) = ReceiveData(2).Trim()
                        '    DetectData(DetectDataCount, 9) = ReceiveData(3).Trim()
                        '    DetectData(DetectDataCount, 10) = ReceiveData(4).Trim()
                        '    DetectData(DetectDataCount, 11) = ScanPos(CCDLIneCount + DetectDataCount, 0).ToString()
                        '    DetectData(DetectDataCount, 12) = ScanPos(CCDLIneCount + DetectDataCount, 1).ToString()
                        '    DetectDataCount += 1
                        '    Return ParserResult.Success
                End Select
            Case "OK"
                Return ParserResult.Success
        End Select

        Return ParserResult.ParamError
    End Function

End Class

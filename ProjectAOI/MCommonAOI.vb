Imports System.IO
Imports Cognex.VisionPro
'Imports Cognex.VisionPro.ImageFile
Imports Cognex.VisionPro.ToolBlock
Imports ProjectCore
Imports ProjectRecipe

Public Module MCommonAOI

    ''' <summary>
    ''' 定義CCD Fix資料格式內容
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDFix
        eOffsetX = 0
        eOffsetY = 1
        eAngle = 2
        eSimilar = 3
        eGlueCoverRate = 4
    End Enum


    Public Enum enmResultConstants
        Err = -1
        Accept = 0
        Warning = 1
        Reject = 2
    End Enum

    ''' <summary>
    ''' 定義CCD Glue Cover 資料格式內容
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDGlueCover
        eSimilar = 0
        eTotalCoverRate = 1
        eAreaNo1CoverRate = 2
        eAreaNo2CoverRate = 3
        eAreaNo3CoverRate = 4
        eAreaNo4CoverRate = 5
    End Enum
    ''' <summary>
    ''' 顯示於Display的對位形狀
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmDisplayShape
        None = 0
        Alignment = 1
        Circle = 2
    End Enum


    ' ''' <summary>
    ' ''' 覆蓋率判斷結果
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmDetermineCCDGlueCoverRate
    '    eOK = 0
    '    eTotoalCoverRateFail = 1
    '    eAreaNo1CoverRateFail = 2
    '    eAreaNo2CoverRateFail = 3
    '    eAreaNo3CoverRateFail = 4
    '    eAreaNo4CoverRateFail = 5
    '    eSimilarFail = 6
    '    eCCDOutputFail = 7
    '    eCCDOutputFailByPassDispenser = 8
    'End Enum

    ' ''' <summary>
    ' ''' 定位判斷結果
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmDetermineCCDFix
    '    eOK = 0
    '    eSimilarFail = 1
    '    eTotoalCoverRateFail = 2
    '    eCCDOutputFail = 3
    'End Enum


    

  


    ' ''' <summary>
    ' ''' 紀錄CCD Handshake內容
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Function WriteCCDReceiveData(ByVal strSystemState As String) As Boolean

    '    Dim strFileDirectory As String      '[儲存目錄]
    '    Dim strFileName As String           '[檔案名稱]

    '    Dim swrFileName As StreamWriter

    '    Try


    '        '[說明]:檢查目錄是否存在
    '        strFileDirectory = Application.StartupPath & "\DataLog"
    '        If Directory.Exists(strFileDirectory) = False Then
    '            '[說明]:目錄不存在就建目錄
    '            Directory.CreateDirectory(strFileDirectory)
    '        End If

    '        '[說明]:檢查年目錄是否存在
    '        strFileDirectory = Application.StartupPath & "\DataLog\" & Format(Now, "yyyy")
    '        If Directory.Exists(strFileDirectory) = False Then
    '            '[說明]:年目錄不存在就建目錄
    '            Directory.CreateDirectory(strFileDirectory)
    '        End If

    '        '[說明]:檢查月目錄是否存在
    '        strFileDirectory = Application.StartupPath & "\DataLog\" & Format(Now, "yyyy") & "\" & Format(Now, "MM")
    '        If Directory.Exists(strFileDirectory) = False Then
    '            '[說明]:月目錄不存在就建目錄
    '            Directory.CreateDirectory(strFileDirectory)
    '        End If

    '        strFileName = "\" & Format(Now, "yyyyMMdd") & "CCD Handshake.txt"

    '        '[說明]:檢查檔案是否存在
    '        If File.Exists(strFileDirectory & strFileName) Then
    '            swrFileName = File.AppendText(strFileDirectory & strFileName)
    '        Else
    '            swrFileName = File.AppendText(strFileDirectory & strFileName)
    '            swrFileName.WriteLine("Time" & "," & "Receive String")
    '        End If

    '        swrFileName.WriteLine(Now & "," & strSystemState)
    '        swrFileName.Close()

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.StackTrace)
    '    End Try

    '    Return False

    'End Function

    'Y = AX + B
    ' ''' <summary>輸入三點對應座標,求解轉換矩陣</summary>
    ' ''' <param name="Xi1">輸入CCD座標(Pixel)</param>
    ' ''' <param name="Xi2">輸入CCD座標(Pixel)</param>
    ' ''' <param name="Xi3">輸入CCD座標(Pixel)</param>
    ' ''' <param name="Yi1">輸入CCD座標(Pixel)</param>
    ' ''' <param name="Yi2">輸入CCD座標(Pixel)</param>
    ' ''' <param name="Yi3">輸入CCD座標(Pixel)</param>
    ' ''' <param name="Xo1">輸出馬達座標(mm)</param>
    ' ''' <param name="Xo2">輸出馬達座標(mm)</param>
    ' ''' <param name="Xo3">輸出馬達座標(mm)</param>
    ' ''' <param name="Yo1">輸出馬達座標(mm)</param>
    ' ''' <param name="Yo2">輸出馬達座標(mm)</param>
    ' ''' <param name="Yo3">輸出馬達座標(mm)</param>
    ' ''' <param name="A11">轉換矩陣參數</param>
    ' ''' <param name="A12">轉換矩陣參數</param>
    ' ''' <param name="A21">轉換矩陣參數</param>
    ' ''' <param name="A22">轉換矩陣參數</param>
    ' ''' <param name="B11">轉換矩陣參數</param>
    ' ''' <param name="B21">轉換矩陣參數</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function Point3CalcTranslation(ByVal Xi1 As Double, ByVal Xi2 As Double, ByVal Xi3 As Double, ByVal Yi1 As Double, ByVal Yi2 As Double, ByVal Yi3 As Double, _
    '                                      ByVal Xo1 As Double, ByVal Xo2 As Double, ByVal Xo3 As Double, ByVal Yo1 As Double, ByVal Yo2 As Double, ByVal Yo3 As Double, _
    '                                      ByRef A11 As Double, ByRef A12 As Double, ByRef A21 As Double, ByRef A22 As Double, ByRef B11 As Double, ByRef B21 As Double)
    '    '矩陣最小平方 求轉移函數
    '    'X = (A^T A)-1 A^T Y
    '    Dim ATA_a11 As Double = Xi1 ^ 2 + Xi2 ^ 2 + Xi3 ^ 2
    '    Dim ATA_a12 As Double = Xi1 * Yi1 + Xi2 * Yi2 + Xi3 * Yi3
    '    Dim ATA_a13 As Double = Xi1 + Xi2 + Xi3
    '    Dim ATA_a21 As Double = Xi1 * Yi1 + Xi2 * Yi2 + Xi3 * Yi3
    '    Dim ATA_a22 As Double = Yi1 ^ 2 + Yi2 ^ 2 + Yi3 ^ 2
    '    Dim ATA_a23 As Double = Yi1 + Yi2 + Yi3
    '    Dim ATA_a31 As Double = Xi1 + Xi2 + Xi3
    '    Dim ATA_a32 As Double = Yi1 + Yi2 + Yi3
    '    Dim ATA_a33 As Double = 3
    '    Dim detA As Double = ATA_a11 * ATA_a22 * ATA_a33 + ATA_a12 * ATA_a23 * ATA_a31 + ATA_a13 * ATA_a21 * ATA_a32 - ATA_a13 * ATA_a22 * ATA_a31 - ATA_a11 * ATA_a23 * ATA_a32 - ATA_a12 * ATA_a21 * ATA_a33

    '    If detA = 0 Then '無行列式值, 無法求解
    '        Return False
    '    End If

    '    '伴隨矩陣
    '    Dim Adj_a11 As Double = ATA_a22 * ATA_a33 - ATA_a23 * ATA_a32
    '    Dim Adj_a12 As Double = ATA_a13 * ATA_a32 - ATA_a12 * ATA_a33
    '    Dim Adj_a13 As Double = ATA_a12 * ATA_a23 - ATA_a13 * ATA_a22
    '    Dim Adj_a21 As Double = ATA_a23 * ATA_a31 - ATA_a21 * ATA_a33
    '    Dim Adj_a22 As Double = ATA_a11 * ATA_a33 - ATA_a13 * ATA_a31
    '    Dim Adj_a23 As Double = ATA_a13 * ATA_a21 - ATA_a11 * ATA_a23
    '    Dim Adj_a31 As Double = ATA_a21 * ATA_a32 - ATA_a22 * ATA_a31
    '    Dim Adj_a32 As Double = ATA_a12 * ATA_a31 - ATA_a11 * ATA_a32
    '    Dim Adj_a33 As Double = ATA_a11 * ATA_a22 - ATA_a12 * ATA_a21

    '    'ATA的反矩陣 (A^T A) -1
    '    Dim InvA_a11 As Double = Adj_a11 / detA
    '    Dim InvA_a12 As Double = Adj_a12 / detA
    '    Dim InvA_a13 As Double = Adj_a13 / detA
    '    Dim InvA_a21 As Double = Adj_a21 / detA
    '    Dim InvA_a22 As Double = Adj_a22 / detA
    '    Dim InvA_a23 As Double = Adj_a23 / detA
    '    Dim InvA_a31 As Double = Adj_a31 / detA
    '    Dim InvA_a32 As Double = Adj_a32 / detA
    '    Dim InvA_a33 As Double = Adj_a33 / detA

    '    'A^T Y
    '    Dim item1 As Double = Xi1 * Xo1 + Xi2 * Xo2 + Xi3 * Xo3
    '    Dim item2 As Double = Yi1 * Xo1 + Yi2 * Xo2 + Yi3 * Xo3
    '    Dim item3 As Double = Xo1 + Xo2 + Xo3
    '    A11 = InvA_a11 * item1 + InvA_a12 * item2 + InvA_a13 * item3
    '    A12 = InvA_a21 * item1 + InvA_a22 * item2 + InvA_a23 * item3
    '    B11 = InvA_a31 * item1 + InvA_a32 * item2 + InvA_a33 * item3

    '    A11 = Val(A11.ToString("#.00000"))
    '    A12 = Val(A12.ToString("#.00000"))
    '    B11 = Val(B11.ToString("#.00000"))

    '    'A^T Y
    '    Dim item4 As Double = Xi1 * Yo1 + Xi2 * Yo2 + Xi3 * Yo3
    '    Dim item5 As Double = Yi1 * Yo1 + Yi2 * Yo2 + Yi3 * Yo3
    '    Dim item6 As Double = Yo1 + Yo2 + Yo3
    '    A21 = InvA_a11 * item4 + InvA_a12 * item5 + InvA_a13 * item6
    '    A22 = InvA_a21 * item4 + InvA_a22 * item5 + InvA_a23 * item6
    '    B21 = InvA_a31 * item4 + InvA_a32 * item5 + InvA_a33 * item6

    '    A21 = Val(A21.ToString("#.00000"))
    '    A22 = Val(A22.ToString("#.00000"))
    '    B21 = Val(B21.ToString("#.00000"))

    '    Return True
    'End Function

    ''' <summary>定位點參數</summary>
    ''' <remarks></remarks>
    Public Class sAlignPos
        ''' <summary>場景名稱</summary>
        ''' <remarks></remarks>
        Public Scene As String
        ''' <summary>定位點結果參數</summary>
        ''' <remarks></remarks>
        Public Result As New List(Of sAlignResult)
        ''' <summary>特徵定位結果</summary>
        ''' <remarks></remarks>
        Public CogPMAlignResults As Cognex.VisionPro.PMAlign.CogPMAlignResults
        ''' <summary> 找圓結果 </summary>
        ''' <remarks></remarks>
        Public CogFindCircleResults As Cognex.VisionPro.Caliper.CogFindCircleResults
        ''' <summary>符合數量</summary>
        ''' <remarks></remarks>
        Public Count As Integer
        ''' <summary>可搜尋的最大數量</summary>
        ''' <remarks></remarks>
        Public MaxCount As Integer
        Public Image As Object
        ''' <summary>是否運算完成</summary>
        ''' <remarks></remarks>
        Public IsRunSuccess As Boolean
        ''' <summary>參考節點與索引</summary>
        ''' <remarks></remarks>
        Public Index As sLevelIndexCollection
    End Class

    ''' <summary>定位點結果</summary>
    ''' <remarks></remarks>
    Public Structure sAlignResult
        ''' <summary>特徵1畫面絕對位置X(Pixel)</summary>
        ''' <remarks></remarks>
        Public PixelTranslationX As Decimal
        ''' <summary>特徵1畫面絕對位置Y(Pixel)</summary>
        ''' <remarks></remarks>
        Public PixelTranslationY As Decimal
        ' ''' <summary>畫面相對位置X(Pixel), 相對教導特徵,目前架構不使用</summary>
        ' ''' <remarks></remarks>
        'Public PixelOffsetX As Decimal
        ' ''' <summary>畫面相對位置Y(Pixel), 相對教導特徵,目前架構不使用</summary>
        ' ''' <remarks></remarks>
        'Public PixelOffsetY As Decimal
        ' ''' <summary>X軸向相對偏移(mm),目前架構不使用</summary>
        ' ''' <remarks>仿Omron用法</remarks>
        'Public OffsetX As Decimal
        ' ''' <summary>Y軸向相對偏移(mm),目前架構不使用</summary>
        ' ''' <remarks>仿Omron用法</remarks>
        'Public OffsetY As Decimal
        ''' <summary>X軸向絕對偏移(mm)</summary>
        ''' <remarks>相對於CCD中心點偏移(mm)</remarks>
        Public AbsOffsetX As Decimal
        ''' <summary>Y軸向絕對偏移(mm)</summary>
        ''' <remarks>相對於CCD中心點偏移(mm)</remarks>
        Public AbsOffsetY As Decimal
        ''' <summary>角度偏移(Deg)</summary>
        ''' <remarks></remarks>
        Public Rotation As Decimal
        ''' <summary>相似度(分數)</summary>
        ''' <remarks></remarks>
        Public Score As Decimal
        ''' <summary>相似閥值</summary>
        ''' <remarks></remarks>
        Public AcceptThreshold As Decimal
        ''' <summary>判定結果</summary>
        ''' <remarks></remarks>
        Public Result As enmResultConstants  'wenda 20160721
        ''' <summary>不良品標記 </summary>
        ''' <remarks></remarks>
        Public SkipMark As Boolean
        ''' <summary>特徵2 X軸偏移(pixel)</summary>
        ''' <remarks></remarks>
        Public Item_1_PixelX As Decimal
        ''' <summary>特徵2 Y軸偏移(pixel)</summary>
        ''' <remarks></remarks>
        Public Item_1_PixelY As Decimal
        ''' <summary>特徵2 X軸偏移(mm)</summary>
        ''' <remarks>相對於CCD中心點偏移(mm)</remarks>
        Public Item_1_AbsOffsetX As Decimal
        ''' <summary>特徵2 Y軸偏移(mm)</summary>
        ''' <remarks>相對於CCD中心點偏移(mm)</remarks>
        Public Item_1_AbsOffsetY As Decimal
    End Structure

    ''' <summary>AOI單步參數</summary>
    ''' <remarks></remarks>
    Public Class sAOIStepConfig
        ''' <summary>CCD編號</summary>
        ''' <remarks></remarks>
        Public CCDNo As Integer
        ''' <summary>計算使用場景</summary>
        ''' <remarks></remarks>
        Public SceneID As String
        ''' <summary>索引</summary>
        ''' <remarks></remarks>
        Public Ticket As Integer
        ''' <summary>輸入影像</summary>
        ''' <remarks></remarks>
        Public InputImage As Object
        ''' <summary>輸出影像</summary>
        ''' <remarks></remarks>
        Public OutputImage As Object
        ''' <summary>輸出資料清單</summary>
        ''' <remarks></remarks>
        Public Param As New List(Of sAOIResultProperty)
    End Class

    ''' <summary>影像結果屬性</summary>
    ''' <remarks></remarks>
    Public Structure sAOIResultProperty
        ''' <summary>名稱</summary>
        ''' <remarks></remarks>
        Public Name As String
        ''' <summary>屬性實際值</summary>
        ''' <remarks></remarks>
        Public Value As Decimal
        ''' <summary>最大可接受值</summary>
        ''' <remarks></remarks>
        Public MaxValue As Decimal
        ''' <summary>最小可接受值</summary>
        ''' <remarks></remarks>
        Public MinValue As Decimal
        ''' <summary>顯示單位</summary>
        ''' <remarks></remarks>
        Public Unit As String
    End Structure


    'TODO: Wenda 每次產品生產完畢增加gCCDAlignResultDict清除流程
    ''' <summary>CCD定位結果 使用觸發計數Ticket進行查詢</summary>
    ''' <remarks></remarks>
    Public gCCDAlignResultDict(enmCCD.ConstMax) As Dictionary(Of Integer, sAlignPos)

    ''' <summary>對外AOI集合物件</summary>
    ''' <remarks></remarks>
    Public gAOICollection As New CAOICollection
    ''' <summary>程控光源物件</summary>
    ''' <remarks></remarks>
    Public gLightCollection As New CLightCollection

    ''' <summary>[通訊異常後 允取再從送幾次]</summary>
    ''' <remarks></remarks>
    Public Const gLightCmdMaxFailCounts As Integer = 3


    ' ''' <summary>定位教導設定</summary>
    ' ''' <remarks></remarks>
    'Public gfrmAlignPR01 As New frmAlignPR01
    ' ''' <summary>工程界面定位教導設定 </summary>
    ' ''' <remarks></remarks>
    'Public gfrmAlignScene As New frmAlignScene
    ' ''' <summary>校正場景教導 </summary>
    ' ''' <remarks></remarks>
    'Public gfrmCogToolBlock As New frmCalibAlignTool

    ''' <summary>委派更新UcDisplay(UserControl)</summary>
    ''' <param name="disp">UcDisplay物件</param>
    ''' <param name="tool">影像工具</param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeUcDisplay(ByVal disp As ucDisplay, ByVal tool As CAOICollection, ByVal sys As sSysParam, ByVal SceneID As String, ByVal AssistLineX As Integer, ByVal AssistLineY As Integer, ByVal Shape As enmDisplayShape)

    Private Sub UcDisplayDrawCenter(ByVal disp As ucDisplay, ByVal CenterX As Double, ByVal CenterY As Double, ByVal LineLength As Integer)
        Dim CenterXLine, CenterYLine As New Cognex.VisionPro.CogLineSegment
        CenterXLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
        CenterYLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
        CenterXLine.LineWidthInScreenPixels = 3
        CenterYLine.LineWidthInScreenPixels = 3
        CenterXLine.Color = Cognex.VisionPro.CogColorConstants.Green
        CenterYLine.Color = Cognex.VisionPro.CogColorConstants.Green
        CenterXLine.SetStartEnd(CenterX - LineLength, CenterY, CenterX + LineLength, CenterY)
        CenterYLine.SetStartEnd(CenterX, CenterY - LineLength, CenterX, CenterY + LineLength)
        disp.CogDisplay1.StaticGraphics.Add(CenterXLine, "##")
        disp.CogDisplay1.StaticGraphics.Add(CenterYLine, "##")
    End Sub
    ''' <summary></summary>
    ''' <param name="disp"></param>
    ''' <param name="tool"></param>
    ''' <remarks></remarks>
    Public Sub InvokeUcDisplay(ByVal disp As ucDisplay, ByVal tool As CAOICollection, ByVal sys As sSysParam, ByVal SceneID As String, ByVal AssistLineX As Integer, ByVal AssistLineY As Integer, ByVal Shape As enmDisplayShape)
        SyncLock disp
            'disp.BeginInvoke(Sub()
            If tool Is Nothing Then
                Exit Sub
            End If
            Select Case gAOICollection.GetCCDType(sys.CCDNo) ' gSSystemParameter.enmCCDType
                Case enmCCDType.CognexVPRO
                    If gCRecipe Is Nothing Then
                        MsgBox("gCRecipe Is Nothing", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If

                    If sys.CCDNo < 0 Then
                        MsgBox("CCDNo Error.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If

                    If sys.CCDNo > gAOICollection.Items.Count - 1 Then
                        MsgBox("CCDNo Error.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If

                    If gAOICollection.Items(sys.CCDNo).GetType() <> GetType(CAOICognexVPRO) Then
                        MsgBox("CCD Type Is Not Equaled with CAOICognexVPRO", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If

                    Try
                        Dim cogToolblock As Cognex.VisionPro.ToolBlock.CogToolBlock = Nothing
                        Dim cogPMAlign As Cognex.VisionPro.PMAlign.CogPMAlignTool = Nothing
                        Dim cogCircle As Cognex.VisionPro.Caliper.CogFindCircleTool = Nothing
                        Dim AlignType As eAlignType = gAOICollection.SceneDictionary(SceneID).AlignType
                        If Not SceneID Is Nothing Then
                            If SceneID <> "" Then
                                cogToolblock = gAOICollection.GetToolBlock(sys.CCDNo, SceneID)
                            End If
                        End If
                        'Dim HLine As New Cognex.VisionPro.CogLine
                        'Dim VLIne As New Cognex.VisionPro.CogLine
                        'HLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        'VLIne.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid

                        'HLine.LineWidthInScreenPixels = 1 '3
                        'VLIne.LineWidthInScreenPixels = 1 '3
                        'HLine.Color = Cognex.VisionPro.CogColorConstants.Green
                        'VLIne.Color = Cognex.VisionPro.CogColorConstants.Green

                        'Dim mImage As Cognex.VisionPro.ICogImage
                        'Dim ImageWidth As Double ' = 1600
                        'Dim ImageHeight As Double '= 1200
                        'If cogToolblock.Inputs.Contains("InputImage") Then
                        '    mImage = cogToolblock.Inputs("InputImage").Value
                        '    ImageWidth = mImage.Width
                        '    ImageHeight = mImage.Height
                        'Else
                        '    '[Note]預設值
                        '    ImageWidth = 1600
                        '    ImageHeight = 1200
                        'End If

                        Dim CCDParameter As sAOIAcquisitionParameter
                        CCDParameter = gAOICollection.GetCCDParameter(sys.CCDNo)
                        Dim ImageWidth As Double = CCDParameter.ImageWidth '1600
                        Dim ImageHeight As Double = CCDParameter.ImageHeight

                        Dim hw As Double = ImageWidth * 0.5
                        Dim hh As Double = ImageHeight * 0.5
                        '[Note]畫標靶
                        disp.DrawClear(sys.CCDNo)
                        disp.DrawTarget(sys.CCDNo, gSSystemParameter.CCDTargetDataList)


                        Select Case Shape
                            Case enmDisplayShape.None
                            Case enmDisplayShape.Alignment '[定位型狀]
                                For i As Integer = 0 To cogToolblock.Tools.Count - 1
                                    If cogToolblock.Tools(i).GetType = GetType(Cognex.VisionPro.PMAlign.CogPMAlignTool) Then
                                        cogPMAlign = cogToolblock.Tools(i)
                                    End If
                                    If cogToolblock.Tools(i).GetType = GetType(Cognex.VisionPro.Caliper.CogFindCircleTool) Then
                                        cogCircle = cogToolblock.Tools(i)
                                    End If
                                Next

                                SyncLock disp
                                    If gAOICollection.CalibBoardCalibration(sys.CCDNo, gAOICollection.GetAcqOutputImage(sys.CCDNo), False, 0) Is Nothing Then
                                        disp.CogDisplay1.Image = Nothing
                                        Exit Sub
                                    End If

                                    disp.CogDisplay1.Image = gAOICollection.CalibBoardCalibration(sys.CCDNo, gAOICollection.GetAcqOutputImage(sys.CCDNo), False, 0) 'cogTool.InputImage '造成Align閃爍
                                    If cogToolblock.Outputs.Count > 0 Then
                                        Select Case AlignType
                                            Case eAlignType.Blob, eAlignType.Circle
                                                If cogCircle Is Nothing Then    'Soni + 2016.09.28 撈不到工具, 
                                                    Exit Sub
                                                End If
                                                If cogCircle.Results Is Nothing Then
                                                    Exit Sub
                                                End If
                                                If cogCircle.Results.Count > 0 Then
                                                    Dim mGetCircle As ICogGraphic
                                                    mGetCircle = cogCircle.Results.GetCircle.CopyBase(CogCopyShapeConstants.All)
                                                    mGetCircle.Color = Cognex.VisionPro.CogColorConstants.Green
                                                    disp.CogDisplay1.StaticGraphics.Add(mGetCircle, "#")
                                                End If
                                            Case Else
                                                If cogPMAlign Is Nothing Then    'Soni + 2016.09.28 撈不到工具, 
                                                    Exit Sub
                                                End If
                                                If cogPMAlign.Results Is Nothing Then
                                                    Exit Sub
                                                End If
                                                If cogPMAlign.Results.Count > 0 Then
                                                    disp.CogDisplay1.StaticGraphics.Add(cogPMAlign.Results(0).CreateResultGraphics(Cognex.VisionPro.PMAlign.CogPMAlignResultGraphicConstants.MatchRegion), "#")
                                                End If
                                        End Select
                                        UcDisplayDrawCenter(disp, cogToolblock.Outputs("Results_Item_0_TranslationX").Value, cogToolblock.Outputs("Results_Item_0_TranslationY").Value, 10)
                                    End If


                                End SyncLock
                            Case enmDisplayShape.Circle
                                If AssistLineX <> 0 And AssistLineY <> 0 Then '輔助圓
                                    Dim Circle As New Cognex.VisionPro.CogCircle
                                    Circle.CenterX = hw
                                    Circle.CenterY = hh
                                    Circle.Radius = (AssistLineY * AssistLineY) ^ 0.5
                                    Circle.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                                    Circle.LineWidthInScreenPixels = 1
                                    Circle.Color = Cognex.VisionPro.CogColorConstants.Green
                                    disp.CogDisplay1.StaticGraphics.Add(Circle, "##")

                                End If
                        End Select

                    Catch ex As Exception
                        gSyslog.Save("Exception Message: " & ex.Message)
                        gSyslog.Save("StackTrace: " & ex.StackTrace)
                    End Try
            End Select
            'End Sub)
            'Exit Sub


        End SyncLock
    End Sub


End Module

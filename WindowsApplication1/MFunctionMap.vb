Imports ProjectAOI
Imports ProjectRecipe
Imports ProjectCore
Imports System.IO
Imports Cognex.VisionPro

''' <summary>資料結構處理 </summary>
''' <remarks></remarks>
Module MFunctionMap

    '#Region "中斷接續"
    '    ''' <summary>接續模式</summary>
    '    ''' <remarks></remarks>
    '    Public Enum enmContinueMode
    '        ''' <summary>接續生產/不退料</summary>
    '        ''' <remarks></remarks>
    '        ContinueRun
    '        ''' <summary>
    '        ''' 自動退料
    '        ''' </summary>
    '        ''' <remarks></remarks>
    '        AutoClean
    '        ''' <summary>
    '        ''' 手動退料
    '        ''' </summary>
    '        ''' <remarks></remarks>
    '        ManualClean

    '    End Enum

    '    Public Structure sContinue
    '        ''' <summary>是否Map生產未完成 復歸不能清掉. 該Flag只能由流程清</summary>
    '        ''' <remarks>按開始的時候跳判定訊息.</remarks>
    '        Public IsProductMapNotFinish As Boolean
    '        ''' <summary>異常發生後,是否Map已儲存</summary>
    '        ''' <remarks></remarks>
    '        Public IsMapSaved As Boolean

    '        ''' <summary>接續模式</summary>
    '        ''' <remarks></remarks>
    '        Public Mode As enmContinueMode
    '        ''' <summary>使用閥1</summary>
    '        ''' <remarks></remarks>
    '        Public UseValve1 As Boolean
    '        ''' <summary>使用閥2</summary>
    '        ''' <remarks></remarks>
    '        Public UseValve2 As Boolean
    '        ''' <summary>使用閥3</summary>
    '        ''' <remarks></remarks>
    '        Public UseValve3 As Boolean
    '        ''' <summary>使用閥4</summary>
    '        ''' <remarks></remarks>
    '        Public UseValve4 As Boolean

    '    End Structure

    '    ''' <summary>接續設定</summary>
    '    ''' <remarks></remarks>
    '    Public continueParam As sContinue

    '#End Region

#Region "定位結果填入MAP"
    Dim mResultThreadStart As New Threading.ThreadStart(AddressOf ResultThread)
    Dim mResultThread As New Threading.Thread(mResultThreadStart)
    Sub ResultThread()
    End Sub

    ' ''' <summary>設定MAP定位位置</summary>
    ' ''' <remarks></remarks>
    'Public Sub SetMapAlignPos(ByVal CCDNo As Integer, ByVal ticket As Integer, ByVal conveyorNo As Integer)
    '    If gAOICollection.IsCCDReceiveBusy(CCDNo) = True Then
    '        Exit Sub
    '    End If
    '    '[說明]:資料接收完成
    '    If gAOICollection.IsCCDReceivedDataOK(CCDNo) = False Then
    '        Exit Sub
    '    End If
    '    If Not gCCDAlignResultDict(CCDNo).ContainsKey(ticket) Then
    '        Debug.Print("SetMapAlignPos:" & ticket & " Not Exists.")
    '        Exit Sub
    '    End If
    '    Dim mStageNo As Integer = gCCDAlignResultDict(CCDNo)(ticket).Index.StageNo
    '    Dim mNodeID As String = gCCDAlignResultDict(CCDNo)(ticket).Index.NodeName
    '    Dim mIndexX As Integer = gCCDAlignResultDict(CCDNo)(ticket).Index.Xf
    '    Dim mIndexY As Integer = gCCDAlignResultDict(CCDNo)(ticket).Index.Yf

    '    Debug.Print("SetMapAlignPos" & CCDNo & " Ticket:" & ticket)
    '    '[說明]:回應成功

    '    With gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIndexX, mIndexY)
    '        Select Case gCRecipe.Node(mStageNo)(mNodeID).AlignType
    '            Case enmAlignType.DevicePos1 '單點對位
    '                .CcdOffsetX = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetX
    '                .CcdOffsetY = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetY
    '                .CcdRotationAngle = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Rotation

    '                'InvokeUcDisplay(gfrmOpStatus.UcDisplay1, gAOICollection, sys, gCRecipe.Node(mStageNo)(mNodeID).AligmentData(0).AligmentScene) '更新控制項,必要條件 frmMain必須是實體

    '                ''--- Log記錄 ---
    '                gSyslog.Save("Align CCD Score(" & gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score & ") Offset: (" & .CcdOffsetX & "," & .CcdOffsetY & ") Pixel Angle:" & .CcdRotationAngle & " Deg. ")
    '                '--- Log記錄 ---

    '                Select Case gCCDAlignResultDict(CCDNo)(ticket).Result(0).Result
    '                    Case CogToolResultConstants.Accept
    '                        '[說明]:紀錄Scan狀態(OK)

    '                        With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                            .DieState = enmDieState.OK
    '                            .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                        End With

    '                    Case CogToolResultConstants.Error, CogToolResultConstants.Reject, CogToolResultConstants.Warning
    '                        '[說明]:紀錄Scan狀態(NG) 圖形顏色寫入
    '                        With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                            .DieState = enmDieState.NG
    '                            .NeedUpdate = True
    '                            .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                        End With

    '                        '[說明]:判斷NG 就不用做測高與繣膠
    '                        With gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIndexX, mIndexY)
    '                            .IsByPassDispensingAction = True
    '                            .IsByPassLaserAction = True
    '                        End With

    '                End Select

    '            Case enmAlignType.DevicePos2 '兩點對位
    '                If .IsCCDOffsetReady = False Then '第一點資料填入
    '                    .CcdOffsetX = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetX
    '                    .CcdOffsetY = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetY
    '                    .CcdRotationAngle = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Rotation
    '                    .IsCCDOffsetReady = True
    '                    .IsCCDOffsetReady2 = False

    '                    Select Case gCCDAlignResultDict(CCDNo)(ticket).Result(0).Result
    '                        Case CogToolResultConstants.Accept
    '                            '[說明]:紀錄Scan狀態(OK)
    '                        Case CogToolResultConstants.Error, CogToolResultConstants.Reject, CogToolResultConstants.Warning
    '                            '[說明]:紀錄Scan狀態(NG) 圖形顏色寫入
    '                            With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                                .DieState = enmDieState.NG
    '                                .NeedUpdate = True
    '                                .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                            End With

    '                            '[說明]:判斷NG 就不用做測高與繣膠
    '                            With gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIndexX, mIndexY)
    '                                .IsByPassDispensingAction = True
    '                                .IsByPassLaserAction = True
    '                            End With
    '                    End Select
    '                ElseIf .IsCCDOffsetReady2 = False Then '第二點資料填入
    '                    .CCDOffsetX2 = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetX
    '                    .CCDOffsetY2 = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetY
    '                    .IsCCDOffsetReady2 = True
    '                    '拍照定位點座標(mm)
    '                    Dim alignPosX1 As Decimal = .ScanPosX
    '                    Dim alignPosY1 As Decimal = .ScanPosY
    '                    '拍照定位點座標(mm)
    '                    Dim alignPosX2 As Decimal = .ScanPosX2
    '                    Dim alignPosY2 As Decimal = .ScanPosY2

    '                    Dim OffsetX1 As Decimal = .CcdOffsetX
    '                    Dim OffsetY1 As Decimal = .CcdOffsetY
    '                    Dim OffsetX2 As Decimal = .CCDOffsetX2
    '                    Dim OffsetY2 As Decimal = .CCDOffsetY2
    '                    gSyslog.Save("Align Offset1:(" & OffsetX1.ToString("0.0000") & "," & OffsetY1.ToString("0.0000") & ") Offset2:(" & OffsetX2.ToString("0.0000") & "," & OffsetY2.ToString("0.0000") & ")")
    '                    Call TwoPointsCalibrationCalculate(alignPosX1, alignPosY1, alignPosX2, alignPosY2, OffsetX1, OffsetY1, OffsetX2, OffsetY2, .CcdOffsetX, .CcdOffsetY, .CcdRotationAngle)

    '                End If

    '                'InvokeUcDisplay(gfrmOpStatus.UcDisplay1, gAOICollection, sys,  gCRecipe.Node(mStageNo)(mNodeID).AligmentData(1).AligmentScene) '更新控制項,必要條件 frmMain必須是實體

    '                'gSyslog.Save("Align CCD Score1(" & alignResult1(0).Score.ToString("00.0") & ") Score2(" & alignResult2(0).Score.ToString("00.0") & ") Offset: (" & .dblCcdOffsetX.ToString("0.0000") & "," & .dblCcdOffsetY.ToString("0.0000") & ") Pixel Angle:" & .dblCcdRotationAngle.ToString("0.00") & " Deg. ")

    '            Case enmAlignType.DevicePos3 '三點對位
    '                If .IsCCDOffsetReady = False Then '第一點資料填入

    '                    .CcdOffsetX = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetX
    '                    .CcdOffsetY = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetY
    '                    .CcdRotationAngle = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Rotation
    '                    .IsCCDOffsetReady = True
    '                    Select Case gCCDAlignResultDict(CCDNo)(ticket).Result(0).Result
    '                        Case CogToolResultConstants.Accept
    '                            '[說明]:紀錄Scan狀態(OK)
    '                        Case CogToolResultConstants.Error, CogToolResultConstants.Reject, CogToolResultConstants.Warning '任一點失敗即失敗
    '                            '[說明]:紀錄Scan狀態(NG) 圖形顏色寫入
    '                            With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                                .DieState = enmDieState.NG
    '                                .NeedUpdate = True
    '                                .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                            End With

    '                            '[說明]:判斷NG 就不用做測高與繣膠
    '                            With gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIndexX, mIndexY)
    '                                .IsByPassDispensingAction = True
    '                                .IsByPassLaserAction = True
    '                            End With
    '                    End Select
    '                ElseIf .IsCCDOffsetReady2 = False Then '第二點資料填入
    '                    .CCDOffsetX2 = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetX
    '                    .CCDOffsetY2 = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetY
    '                    .IsCCDOffsetReady2 = True
    '                    Select Case gCCDAlignResultDict(CCDNo)(ticket).Result(0).Result
    '                        Case CogToolResultConstants.Accept
    '                            '[說明]:紀錄Scan狀態(OK)
    '                        Case CogToolResultConstants.Error, CogToolResultConstants.Reject, CogToolResultConstants.Warning '任一點失敗即失敗
    '                            '[說明]:紀錄Scan狀態(NG) 圖形顏色寫入
    '                            With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                                .DieState = enmDieState.NG
    '                                .NeedUpdate = True
    '                                .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                            End With

    '                            '[說明]:判斷NG 就不用做測高與繣膠
    '                            With gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIndexX, mIndexY)
    '                                .IsByPassDispensingAction = True
    '                                .IsByPassLaserAction = True
    '                            End With
    '                    End Select
    '                ElseIf .IsCCDOffsetReady3 = False Then '第三點資料填入
    '                    .CCDOffsetX3 = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetX
    '                    .CCDOffsetY3 = gCCDAlignResultDict(CCDNo)(ticket).Result(0).OffsetY
    '                    .IsCCDOffsetReady3 = True

    '                    '拍照定位點座標(mm)
    '                    Dim alignPosX1 As Decimal = .ScanPosX
    '                    Dim alignPosY1 As Decimal = .ScanPosY
    '                    '拍照定位點座標(mm)
    '                    Dim alignPosX2 As Decimal = .ScanPosX2
    '                    Dim alignPosY2 As Decimal = .ScanPosY2

    '                    Dim alignPosX3 As Decimal = .ScanPosX3
    '                    Dim alignPosY3 As Decimal = .ScanPosY3

    '                    Dim OffsetX1 As Decimal = .CcdOffsetX
    '                    Dim OffsetY1 As Decimal = .CcdOffsetY
    '                    Dim OffsetX2 As Decimal = .CCDOffsetX2
    '                    Dim OffsetY2 As Decimal = .CCDOffsetY2
    '                    Dim offsetX3 As Decimal = .CCDOffsetX3
    '                    Dim offsetY3 As Decimal = .CCDOffsetY3

    '                    Select Case gCCDAlignResultDict(CCDNo)(ticket).Result(0).Result
    '                        Case CogToolResultConstants.Accept
    '                            '[說明]:紀錄Scan狀態(OK)

    '                            With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                                .DieState = enmDieState.OK
    '                                .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                            End With

    '                        Case CogToolResultConstants.Error, CogToolResultConstants.Reject, CogToolResultConstants.Warning
    '                            '[說明]:紀錄Scan狀態(NG) 圖形顏色寫入
    '                            With gStageMap(mStageNo).Node(mNodeID).ChipState(mIndexX, mIndexY)
    '                                .DieState = enmDieState.NG
    '                                .NeedUpdate = True
    '                                .intFixSimilar = gCCDAlignResultDict(CCDNo)(ticket).Result(0).Score * 100
    '                            End With

    '                            '[說明]:判斷NG 就不用做測高與繣膠
    '                            With gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIndexX, mIndexY)
    '                                .IsByPassDispensingAction = True
    '                                .IsByPassLaserAction = True
    '                            End With

    '                    End Select

    '                End If
    '        End Select

    '    End With

    'End Sub

    ''' <summary>
    ''' 二點定位計算式
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TwoPointsCalibrationCalculate(ByVal Center1X As Decimal, ByVal Center1Y As Decimal, ByVal Center2X As Decimal, ByVal Center2Y As Decimal, ByVal Offset1X As Decimal, ByVal Offset1Y As Decimal, ByVal Offset2X As Decimal, ByVal Offset2Y As Decimal, ByRef OffsetX As Decimal, ByRef OffsetY As Decimal, ByRef Angle As Decimal)
        Dim tempAngle As Decimal
        Dim crossData As Decimal
        Dim dotValve As Decimal
        Dim OldVectorX As Decimal
        Dim OldVectorY As Decimal
        Dim NewVectorX As Decimal
        Dim NewVectorY As Decimal

        'Dim datapath As String
        'Dim sw As StreamWriter
        Dim str As String


        Try
            'datapath = Application.StartupPath & "\DateLog\2Calibration.txt"

            'sw = New StreamWriter(datapath, True, System.Text.Encoding.Default)

            OldVectorX = Center2X - Center1X
            OldVectorY = Center2Y - Center1Y
            NewVectorX = Center2X - Offset2X - Center1X + Offset1X
            NewVectorY = Center2Y - Offset2Y - Center1Y + Offset1Y
            crossData = OldVectorX * NewVectorY - OldVectorY * NewVectorX
            dotValve = OldVectorX * NewVectorX + OldVectorY * NewVectorY
            Dim Distance1 As Decimal = Math.Sqrt(OldVectorX * OldVectorX + OldVectorY * OldVectorY)
            Dim Distance2 As Decimal = Math.Sqrt(NewVectorX * NewVectorX + NewVectorY * NewVectorY)
            'gSyslog.Save("Old Vector(" & OldVectorX & "," & OldVectorY & ")")
            'gSyslog.Save("New Vector(" & NewVectorX & "," & NewVectorY & ")")
            'gSyslog.Save("Center1(" & Center1X & "," & Center1Y & ")")
            'gSyslog.Save("Center2(" & Center2X & "," & Center2Y & ")")
            'gSyslog.Save("Cross:" & crossData)
            'gSyslog.Save("Dot:" & dotValve)
            'gSyslog.Save("Distance1:" & Distance1)
            'gSyslog.Save("Distance2:" & Distance2)
            Dim value As Decimal = dotValve / Distance1 / Distance2
            If value > 1 Then value = 1 '小數點除數過範圍保護
            If value < -1 Then value = -1
            tempAngle = Math.Acos(value)
            If crossData >= 0 Then
                Angle = -tempAngle * 180 / Math.PI
            Else
                Angle = tempAngle * 180 / Math.PI
            End If
            OffsetX = Offset1X
            OffsetY = Offset1Y
            str = "1st X:" & Center1X.ToString("0.0000") & " Y:" & Center2X.ToString("0.0000") & " Offset X:" & Offset1X.ToString("0.0000") & " Y:" & Offset1Y.ToString("0.0000") &
                " 2rd X:" & Center2X.ToString("0.0000") & " Y:" & Center2Y.ToString("0.0000") & " Offset X:" & Offset2X.ToString("0.0000") & " Y:" & Offset2Y.ToString("0.0000") &
                " angle:" & Angle.ToString("0.0000")

            gSyslog.Save("TwoPointsCalibrationCalculate:" & str)

            'sw.WriteLine(str)
            'sw.Flush()
            'sw.Close()
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub

#End Region

#Region "客戶端Wafer Map轉PII Map"
    ''' <summary>[將客戶端Wafer Map轉成PII Map(Cary)]</summary>
    ''' <param name="ReadFileName"></param>
    ''' <param name="SavePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WaferMapConvertToPIIMap(ByVal ReadFileName As String, ByVal SavePath As String) As Boolean
        Try
            Dim objReader As New StreamReader(ReadFileName)
            Dim cLine() As Char
            Dim iRowCount As Integer = 0
            Dim iColumnCount As Integer = 0
            Dim i, j As Integer

            Dim iCycleRowCount As Integer = 0
            Dim IsRerunMap As Boolean
            Using sr As StreamReader = New StreamReader(ReadFileName)
                Dim sTitle As String = sr.ReadLine()

                If (sTitle = "[Bin Data]") Then
                    IsRerunMap = True
                Else
                    Dim arrStr() As String = sr.ReadLine().Split(" ")
                    iColumnCount = arrStr(0).Length
                    IsRerunMap = False
                End If
            End Using
            gCRecipe.IsRerunRun = IsRerunMap

            cLine = objReader.ReadLine() '[第一行]

            If IsRerunMap = True Then
                cLine = objReader.ReadLine() '[第二行]
                iColumnCount = cLine.Length
            End If


            Dim Die(65536, iColumnCount) As Char '(65536, cLine.Length) As Char
            '' calculate Row number & column number
            Do
                For j = 0 To cLine.Length - 1
                    Die(iRowCount, j) = cLine(j)
                Next
                iRowCount += 1
                cLine = objReader.ReadLine()
            Loop Until cLine.Length = 0 Or cLine = "[Round Data]"

            Dim Cycle(65536, iColumnCount) As Char '(65536, cLine.Length) As Char
            If IsRerunMap = True Then
                cLine = objReader.ReadLine() '[Round Data]後的資料
                Do
                    For j = 0 To cLine.Length - 1
                        Cycle(iCycleRowCount, j) = cLine(j) '[Note]紀錄Round資料
                    Next
                    iCycleRowCount += 1
                    cLine = objReader.ReadLine()
                Loop Until cLine.Length = 0

            End If


            objReader.Close()



            Dim file As System.IO.StreamWriter
            Dim split1(), SaveFilenNme As String

            If Not IO.Directory.Exists(SavePath) Then
                IO.Directory.CreateDirectory(SavePath)
            End If

            split1 = Split(ReadFileName, ".")
            split1 = Split(split1(0), "\")
            SaveFilenNme = split1(split1.Length - 1)
            SaveFilenNme = SavePath & SaveFilenNme & ".csv"
            file = My.Computer.FileSystem.OpenTextFileWriter(SaveFilenNme, False)

            '' build "WAFER INFORMATION" 
            file.WriteLine("[WAFER INFORMATION]")
            file.WriteLine("VERSION,Ver9.3")
            file.WriteLine("LOT ID,N/A")
            file.WriteLine("PRODUCT ID,N/A")
            file.WriteLine("DIE SIZE X,N/A")
            file.WriteLine("DIE SIZE Y,N/A")
            file.WriteLine("COLUMN," & iColumnCount)
            file.WriteLine("ROW," & iRowCount)
            file.WriteLine("CHECK IN TIME,N/A")
            file.WriteLine("CHECK OUT TIME,N/A")
            file.WriteLine("TOTAL TIME,N/A")
            file.WriteLine("RECIPE NAME,N/A")
            file.WriteLine("TYPE,N/A")
            file.WriteLine("PITCH,N/A")
            file.WriteLine("USER,N/A")
            file.WriteLine("MACHINE TYPE,N/A")
            file.WriteLine("NOTCH,180")

            '' build "WAFER MAP" 
            file.WriteLine("[WAFER MAP]")
            file.Write("COLUMN")
            For j = 0 To iColumnCount - 1
                file.Write("," & j + 1)
            Next
            file.WriteLine()
            For i = 0 To iRowCount - 1
                file.Write("ROW" & i + 1)
                For j = 0 To iColumnCount - 1
                    file.Write("," & Die(i, j))
                Next
                file.WriteLine()
            Next

            '' build "CYCLE MAP" 
            file.WriteLine("[CYCLE MAP]")
            file.Write("COLUMN")
            For j = 0 To iColumnCount - 1
                file.Write("," & j + 1)
            Next
            file.WriteLine()

            If IsRerunMap = True Then '[Note]有點膠紀錄
                For i = 0 To iRowCount - 1
                    file.Write("ROW" & i + 1)
                    For j = 0 To iColumnCount - 1
                        'If Char.IsLetterOrDigit(Cycle(i, j)) Then
                        'If (Cycle(i, j) = ".") Then '' change the value to 'Round' except '.'
                        '    file.Write("," & Cycle(i, j))
                        'Else
                        '    file.Write(",0")
                        'End If
                        'Else
                        file.Write("," & Cycle(i, j))
                        'End If
                    Next
                    file.WriteLine()
                Next
            Else '[Note]ASE紀錄
                For i = 0 To iRowCount - 1
                    file.Write("ROW" & i + 1)
                    For j = 0 To iColumnCount - 1

                        If Char.IsLetterOrDigit(Die(i, j)) Then
                            'If ((Die(i, j) = "X") Or (Die(i, j) = ".") Or (Die(i, j) = "@") Or (Die(i, j) = "=")) Then '' change the value to '0' except 'X' '.' '@'  '='
                            If (Die(i, j) = ".") Then '' change the value to '0' except '.'
                                file.Write("," & Die(i, j))
                            Else
                                file.Write(",0")
                            End If
                        Else
                            file.Write("," & Die(i, j))
                        End If
                    Next
                    file.WriteLine()
                Next

            End If


            '' build "EXPLAIN" 
            file.WriteLine("[EXPLAIN]")
            file.WriteLine(".,EMPTY")
            file.WriteLine("X,BAD DIE")
            file.WriteLine("0~FF,[WAFER MAP] BIN NUMBER")
            file.WriteLine("0~FF,[CYCLE MAP] CYCLE TIME")
            file.WriteLine("N,NOTCH")
            file.Close()

            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function
#End Region


    'jimmy add 
    Public Sub ClearGGFile(ByVal fileName As String)
        Dim mFileDirectory As String      '[檔案目錄]
        Dim mFileName As String           '[檔案名稱]



        '[說明]:檢查目錄是否存在
        mFileDirectory = "D:\PIIMappingData\"
        If Directory.Exists(mFileDirectory) = False Then
            '[說明]:目錄不存在就建目錄
            Directory.CreateDirectory(mFileDirectory)
        End If

        mFileName = "\" & fileName & ".txt"
        '[說明]:檢查檔案是否存在
        File.Delete(mFileDirectory & mFileName)


    End Sub







#Region "存讀檔案"

    ''' <summary>[儲存的資料型態]</summary>
    ''' <remarks></remarks>
    Public Enum eDataType
        ''' <summary>[非點膠的狀態]</summary>
        ''' <remarks></remarks>
        eNonDispensing = 0
        ''' <summary>[點膠的狀態]</summary>
        ''' <remarks></remarks>
        eDispensing = 1
    End Enum



    ''' <summary>[紀錄Mapping Data((NonDispensingStatus))]</summary>
    ''' <param name="fileName"></param>
    ''' <param name="dataType"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="nodeName"></param>
    ''' <param name="indexX"></param>
    ''' <param name="indexY"></param>
    ''' <param name="status"></param>
    ''' <param name="roundNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WriteDieStatusForMappingData(ByVal fileName As String, ByVal dataType As eDataType, ByVal stageNo As enmStage, ByVal nodeName As String, ByVal indexX As Integer, ByVal indexY As Integer, ByVal status As eDieStatus, Optional ByVal roundNo As Integer = 0) As Boolean

        Dim mFileDirectory As String      '[檔案目錄]
        Dim mFileName As String           '[檔案名稱]
        Dim swrFileName As StreamWriter


        '[說明]:檢查目錄是否存在
        mFileDirectory = "D:\PIIMappingData\"
        If Directory.Exists(mFileDirectory) = False Then
            '[說明]:目錄不存在就建目錄
            Directory.CreateDirectory(mFileDirectory)
        End If

        mFileName = "\" & fileName & ".txt"
        '[說明]:檢查檔案是否存在
        If File.Exists(mFileDirectory & mFileName) Then
            swrFileName = File.AppendText(mFileDirectory & mFileName)
        Else
            swrFileName = File.AppendText(mFileDirectory & mFileName)
        End If
        '[Note]:格式
        '       [資料類型]; [ StageNo ]; [ NodeName ]; [ Round ]; [ IndexX ]; [ IndexY ]; [ Status ]
        '
        '       [0]; [ StageNo ]; [ NodeName ]; [   0   ]; [ IndexX ]; [ IndexY ]; [ Status ]   <---非點膠的狀態
        '
        '       [1]; [ StageNo ]; [ NodeName ]; [ Round ]; [ IndexX ]; [ IndexY ]; [ Status ]   <---點膠的狀態
        swrFileName.WriteLine(dataType.ToString() & ";" & stageNo.ToString() & ";" & nodeName & ";" & roundNo.ToString() & ";" & indexX.ToString() & ";" & indexY.ToString() & ";" & status.ToString())
        swrFileName.Close()
        Return True

    End Function

    ''' <summary>[紀錄Mapping Data(DispensingStatus)]</summary>
    ''' <param name="fileName"></param>
    ''' <param name="dataType"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="nodeName"></param>
    ''' <param name="indexX"></param>
    ''' <param name="indexY"></param>
    ''' <param name="status"></param>
    ''' <param name="roundNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WriteDieStatusForMappingData(ByVal fileName As String, ByVal dataType As eDataType, ByVal stageNo As enmStage, ByVal nodeName As String, ByVal indexX As Integer, ByVal indexY As Integer, ByVal status As eDispensingStatus, Optional ByVal roundNo As Integer = 0) As Boolean

        Dim mFileDirectory As String      '[檔案目錄]
        Dim mFileName As String           '[檔案名稱]
        Dim swrFileName As StreamWriter


        '[說明]:檢查目錄是否存在
        mFileDirectory = "D:\PIIMappingData\"
        If Directory.Exists(mFileDirectory) = False Then
            '[說明]:目錄不存在就建目錄
            Directory.CreateDirectory(mFileDirectory)
        End If

        mFileName = "\" & fileName & ".txt"
        '[說明]:檢查檔案是否存在
        If File.Exists(mFileDirectory & mFileName) Then
            swrFileName = File.AppendText(mFileDirectory & mFileName)
        Else
            swrFileName = File.AppendText(mFileDirectory & mFileName)
        End If
        '[Note]:格式
        '       [資料類型][ StageNo ]; [ NodeName ]; [ Round ]; [ IndexX ]; [ IndexY ]; [ Status ]
        '
        '       [0]; [ StageNo ]; [ NodeName ]; [   0   ]; [ IndexX ]; [ IndexY ]; [ Status ]   <---非點膠的狀態
        '
        '       [1]; [ StageNo ]; [ NodeName ]; [ Round ]; [ IndexX ]; [ IndexY ]; [ Status ]   <---點膠的狀態
        swrFileName.WriteLine(CInt(dataType).ToString() & ";" & CInt(stageNo).ToString() & ";" & nodeName & ";" & roundNo.ToString() & ";" & indexX.ToString() & ";" & indexY.ToString() & ";" & CInt(status).ToString())
        swrFileName.Close()
        Return True

    End Function


    ''' <summary>[紀錄Mapping Data]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadDieStatusForMappingData(ByVal fileName As String) As Boolean

        Dim mFileDirectory As String      '[檔案目錄]
        Dim mFileName As String           '[檔案名稱]
        Dim mFileNum As Integer
        Dim mStageNo As Integer
        Dim mNodeName As String
        Dim mRoundNo As Integer
        Dim mIndexX As Integer
        Dim mIndexY As Integer
        Dim mDieStatus As eDieStatus
        Dim mDispensingStatus As eDispensingStatus
        Dim mTemp As String
        Dim mSpilt() As String
        Dim mI As Integer

        '[說明]:檢查目錄是否存在
        mFileDirectory = "D:\PIIMappingData\"
        If Directory.Exists(mFileDirectory) = False Then
            Return True
        End If

        mFileName = "\" & fileName & ".txt"
        '[說明]:檢查檔案是否存在
        If File.Exists(mFileDirectory & mFileName) = False Then
            Return True
        End If


        mFileNum = FreeFile()
        '[Note]:開檔
        FileOpen(mFileNum, mFileDirectory & mFileName, OpenMode.Input)

        '[Note]:格式
        '       [資料類型][ StageNo ]; [ NodeName ]; [ Round ]; [ IndexX ]; [ IndexY ]; [ Status ]
        '
        '       [0]; [ StageNo ]; [ NodeName ]; [   0   ]; [ IndexX ]; [ IndexY ]; [ Status ]       <---非點膠的狀態
        '
        '       [1]; [ StageNo ]; [ NodeName ]; [ Round ]; [ IndexX ]; [ IndexY ]; [ Status ]       <---點膠的狀態

        Do Until EOF(mFileNum)
            '[Note]:一行一行讀取資料
            mTemp = LineInput(mFileNum)
            mSpilt = Split(mTemp, ";")
            If mSpilt.Length = 7 Then
                '[Note]:不是7就是資料有問題
                mStageNo = CInt(mSpilt(1))
                mNodeName = mSpilt(2)
                mRoundNo = CInt(mSpilt(3))
                mIndexX = CInt(mSpilt(4))
                mIndexY = CInt(mSpilt(5))

                Select Case CInt(mSpilt(0))
                    Case CInt(eDataType.eNonDispensing)
                        '[Note]:非點膠的狀態
                        mDieStatus = CInt(mSpilt(6))
                        gStageMap(mStageNo).Node(mNodeName).SBinMapData(mIndexX, mIndexY).Status = mDieStatus

                    Case CInt(eDataType.eDispensing)
                        '[Note]:點膠的狀態
                        mDispensingStatus = CInt(mSpilt(6))
                        'gStageMap(mStageNo).Node(mNodeName).Round(mRoundNo).DispensingStatus(mIndexX, mIndexY) = mDispensingStatus
                        'Eason 20170302 Ticket:100090 , System Update Crash
                        gStageMap(mStageNo).Node(mNodeName).Round(mRoundNo).SetDispensingStatus(mIndexX, mIndexY, mDispensingStatus)

                End Select
            End If
        Loop

        '[Note]:關閉檔案
        FileClose(mFileNum)


        '[Note]:更新生產狀態
        '       將gStageMap內的DispensingStatus做統整，來判斷該顆產品是否已經完成生產，或已經NG
        For mI = enmStage.No1 To gSSystemParameter.StageCount - 1
            Call TransformDispensingStatus(mI)
        Next

        Return True

    End Function

    ''' <summary>[將gStageMap內的DispensingStatus做統整，來判斷該顆產品是否已經完成生產，或已經NG]</summary>
    ''' <param name="stageNo"></param>
    ''' <remarks></remarks>
    Private Sub TransformDispensingStatus(ByVal stageNo As enmStage)

        'Dim mStageNo As Integer
        Dim mNodeKey As Integer
        Dim mNodeName As String
        Dim mUBoundX As Integer
        Dim mUBoundY As Integer
        Dim mPatternName As String
        Dim mRoundNo As Integer
        Dim mRoundCount As Integer
        Dim mI As Integer
        Dim mJ As Integer
        Dim mDispensingOKCount As Integer

        '[Note]:根據各Node給定預設值
        For mNodeKey = 0 To gStageMap(stageNo).Node.Count - 1
            mNodeName = gStageMap(stageNo).Node.Keys(mNodeKey)
            Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo)(mNodeName).Array)
            mUBoundX = mMultiArrayAdapter.GetMemoryCountX() - 1
            mUBoundY = mMultiArrayAdapter.GetMemoryCountY() - 1
            '  mPatternName = gStageMap(stageNo).Node(mNodeName).PatternName
            mPatternName = gCRecipe.Node(stageNo)(mNodeName).PatternName
            mRoundCount = gCRecipe.Pattern(mPatternName).Round.Count

            '[Note]:將各個Round的點膠狀態整合起來
            For mI = 0 To mUBoundX
                For mJ = 0 To mUBoundY
                    mDispensingOKCount = 0
                    For mRoundNo = 0 To mRoundCount - 1
                        Select Case gStageMap(stageNo).Node(mNodeName).Round(mRoundNo).DispensingStatus(mI, mJ)
                            Case eDispensingStatus.None
                            Case eDispensingStatus.OK
                                mDispensingOKCount = mDispensingOKCount + 1

                            Case eDispensingStatus.Fail
                                '[Note]:只要有一個Fail，就認定該顆點膠Fail
                                gStageMap(stageNo).Node(mNodeName).SBinMapData(mI, mJ).Status = eDieStatus.DispensingFail

                        End Select
                    Next
                    '[Note]:需要每個Round都點完才算完成
                    If mDispensingOKCount = mRoundCount Then
                        gStageMap(stageNo).Node(mNodeName).SBinMapData(mI, mJ).Status = eDieStatus.Finish
                        gStageMap(stageNo).Node(mNodeName).SRecipePos(mI, mJ).IsByPassCCDScanFixAction = True
                        gStageMap(stageNo).Node(mNodeName).SRecipePos(mI, mJ).IsByPassDispensingAction = True
                        gStageMap(stageNo).Node(mNodeName).SRecipePos(mI, mJ).IsByPassLaserAction = True
                    End If
                Next
            Next
        Next

    End Sub

#End Region

End Module

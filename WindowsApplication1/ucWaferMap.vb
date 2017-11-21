Imports MapData
Imports ProjectRecipe
Imports ProjectCore 'Eason 20170302 Ticket:100090 , System Update Crash
Imports ProjectIO
Imports System.IO

Public Class ucWaferMap
    Public mEditType As eMapEditType

    Public Pitch_X, Pitch_Y As Integer  ' die size
    Public Row, Col As Integer      ' Array size
    Public NodetomapR, NodetoMapL As String
    Public RoundMapR, RoundMapL As Integer
    Public RoundMapR_Status, RoundMapL_Status As CRoundMap
    Public mScaleX As Integer '換算比率
    Public mScaleY As Integer '
    Public ShowBIN As Integer
    Public stageNO_L, stageNO_R As Integer

    Public BasicX() As Integer
    Public BasicY() As Integer
    Public first As Boolean = True

    Dim mMultiArrayAdapter As CMultiArrayAdapter
    Public map As Dictionary(Of String, CNodeInfo)


    Public Function getBasicPos(ByVal node As String) As Boolean '
        Dim posX, posY As Integer
        posX = gCRecipe.Node(stageNO_L)(node).ConveyorPos(0).BasicPositionX
        posY = gCRecipe.Node(stageNO_L)(node).ConveyorPos(0).BasicPositionY

        Return True
    End Function





    'Toby add 20160616
    Public Function DrawNodeMap(ByRef W As Integer, ByRef H As Integer) As Boolean '
        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小
        'Dim mScaleX As Integer '換算比率
        'Dim mScaleY As Integer '


        mBitmap = PictureBox1.Image


        mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))

        mDraw = Graphics.FromImage(mBitmap)

        '[說明]:畫外框(chuck大小)
        With mPen
            .Width = 3
            .DashStyle = Drawing2D.DashStyle.Solid
        End With
        '填滿
        mDraw.FillRectangle(mBrush, 0, 0, CInt(PictureBox1.Width) - 1, CInt(PictureBox1.Height) - 1)
        '畫線
        mDraw.DrawRectangle(mPen, 0, 0, CInt(PictureBox1.Width) - 1, CInt(PictureBox1.Height) - 1)


        mScaleY = PictureBox1.Height \ H     '實際比例
        mScaleX = PictureBox1.Width \ W    '實際比例

        Pitch_X = mScaleX
        Pitch_Y = mScaleY
        Row = H
        Col = W

        '設定線的寬度
        With mPen
            .Width = PictureBox1.Width \ 500
        End With

        '[說明]繪製 H*W 的Map 
        For i As Integer = 0 To H - 1
            For j As Integer = 0 To W - 1
                mDraw.DrawRectangle(mPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
            Next
        Next

        PictureBox1.Image = mBitmap

        Return True
        'Toby add 20160616
    End Function


    Public Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove '顯示ToolTip (Die 座標)
        Dim Locate As String
        If Me.Tag = "show_only" Then
            Exit Sub
        End If
        Try
            Locate = "(" + CStr(e.X \ Pitch_X + 1) + "," + CStr(e.Y \ Pitch_Y + 1) + ")"
            If (e.X \ Pitch_X + 1) <= Col And (e.Y \ Pitch_Y + 1) <= Row Then
                ToolTip1.SetToolTip(sender, Locate)
            Else
                ToolTip1.RemoveAll()
            End If

        Catch
        End Try
    End Sub
    Public Sub changeColor(x As Integer, y As Integer, Color As Color) '對單顆die換顏色 

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小       

        Dim SolidBrush = New Drawing.SolidBrush(Color)
        x = x - 1
        y = y - 1

        mBitmap = PictureBox1.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        End If
        mDraw = Graphics.FromImage(mBitmap)


        mDraw.FillRectangle(SolidBrush, Pitch_X * x, Pitch_Y * y, Pitch_X, Pitch_Y)
        mDraw.DrawRectangle(New Pen(Brushes.Black), Pitch_X * x, Pitch_Y * y, Pitch_X, Pitch_Y)


        PictureBox1.Image = mBitmap
        '20160603_Toby_End
    End Sub
    Public Sub changeColor(x As Decimal, y As Decimal, Pitch_X As Decimal, Pitch_Y As Decimal, Color As Color) '對單顆die換顏色 

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小       

        Dim SolidBrush = New Drawing.SolidBrush(Color)

        mBitmap = PictureBox1.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        End If
        If mDraw Is Nothing Then
            mDraw = Graphics.FromImage(mBitmap)
        End If

        mDraw.FillRectangle(SolidBrush, x, y, Pitch_X, Pitch_Y)

        PictureBox1.Image = mBitmap


    End Sub
    Public Sub WriteText(x As Integer, y As Integer, ByVal X_Size As Integer, ByVal y_size As Integer, Data As String) '對單顆寫點膠資訊

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小       



        mBitmap = PictureBox1.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        End If

        mDraw = Graphics.FromImage(mBitmap)
        mDraw.DrawString(Data, SystemFonts.DefaultFont, Brushes.Black, New RectangleF(x, y, 20, 20))

        PictureBox1.Image = mBitmap

    End Sub
    Public Function DrawStageMap(StageL As CStageMap, stageNo_L As Integer, StageR As CStageMap, stageNo_R As Integer) As Boolean  '根據Stage Map 顯示 2個Stage
        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim mWhite As New Drawing.SolidBrush(Color.White)
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小
        Dim mI As Integer
        Dim Max_L_X As Integer = 0
        Dim Max_L_Y As Integer = 0
        Dim Max_R_X As Integer = 0
        Dim Max_R_Y As Integer = 0

        Dim Temp_BasicX, Temp_BasicY As Integer

        Dim Min_BasicX_R As Integer = Int32.MaxValue
        Dim Max_BasicX_R As Integer = Int32.MinValue


        Dim Min_BasicX_L As Integer = Int32.MaxValue
        Dim Max_BasicX_L As Integer = Int32.MinValue

        '最小Y軸只取一個(左&右 Stage)
        Dim Min_BasicY As Integer = Int32.MaxValue

        Dim Display_startX, Display_startY As Integer

        Dim ScaleX As Decimal
        Dim Stage_center As Integer


        Try
            If stageNo_L < 2 Then
                ClearMapFile("MapA")
            Else
                ClearMapFile("MapB")
            End If


            ReDim BasicX(gCRecipe.Node(stageNo_L).Count + gCRecipe.Node(stageNo_R).Count - 1)
            ReDim BasicY(gCRecipe.Node(stageNo_L).Count + gCRecipe.Node(stageNo_R).Count - 1)
            PictureBox1.Image = Nothing
            PictureBox1.Refresh()

            mBitmap = PictureBox1.Image
            mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            mDraw = Graphics.FromImage(mBitmap)
            '[說明]:畫外框(chuck大小)
            With mPen
                .Width = 3
                .DashStyle = Drawing2D.DashStyle.Solid
            End With
            '填滿
            mDraw.FillRectangle(mBrush, 0, 0, CInt(PictureBox1.Width) - 1, CInt(PictureBox1.Height) - 1)
            '畫線
            mDraw.DrawRectangle(mPen, 0, 0, CInt(PictureBox1.Width) - 1, CInt(PictureBox1.Height) - 1)




            For mI = 0 To gCRecipe.Node(stageNo_L).Count - 1
                NodetoMapL = StageL.Node.Keys(mI)
                BasicX(mI) = gCRecipe.Node(stageNo_L)(NodetoMapL).ConveyorPos(0).BasicPositionX
                BasicY(mI) = gCRecipe.Node(stageNo_L)(NodetoMapL).ConveyorPos(0).BasicPositionY
                Temp_BasicX = BasicX(mI)
                Temp_BasicY = BasicY(mI)

                If Temp_BasicX < Min_BasicX_L Then
                    Min_BasicX_L = Temp_BasicX
                End If
                If Temp_BasicX > Max_BasicX_L Then
                    Max_BasicX_L = Temp_BasicX
                End If
                If Temp_BasicY < Min_BasicY Then
                    Min_BasicY = Temp_BasicY
                End If

            Next
            For mI = 0 To gCRecipe.Node(stageNo_R).Count - 1
                NodetomapR = StageR.Node.Keys(mI)
                BasicX(gCRecipe.Node(stageNo_L).Count + mI) = gCRecipe.Node(stageNo_R)(NodetomapR).ConveyorPos(0).BasicPositionX
                BasicY(gCRecipe.Node(stageNo_L).Count + mI) = gCRecipe.Node(stageNo_R)(NodetomapR).ConveyorPos(0).BasicPositionY
                Temp_BasicX = BasicX(gCRecipe.Node(stageNo_L).Count + mI)
                Temp_BasicY = BasicY(gCRecipe.Node(stageNo_L).Count + mI)

                If Temp_BasicX < Min_BasicX_R Then
                    Min_BasicX_R = Temp_BasicX
                End If
                If Temp_BasicX > Max_BasicX_R Then
                    Max_BasicX_R = Temp_BasicX
                End If
                If Temp_BasicY < Min_BasicY Then
                    Min_BasicY = Temp_BasicY
                End If

            Next

            ScaleX = 800 / (((Max_BasicX_L + 200) - Min_BasicX_L) + ((Max_BasicX_R + 200) - Min_BasicX_R))
            Stage_center = ScaleX * ((Max_BasicX_L + 200) - Min_BasicX_L)


            For mI = 0 To gCRecipe.Node(stageNo_L).Count - 1
                NodetoMapL = StageL.Node.Keys(mI)
                If IsNothing(NodetoMapL) Then
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapL = StageL.Node(NodetoMapL).Round.Count
                End If


                Display_startX = Stage_center - (BasicX(mI) - Min_BasicX_L)
                Display_startY = PictureBox1.Height - (BasicY(mI) - Min_BasicY)

                mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo_L)(NodetoMapL).Array)
                'Toby add
                '取最高的level算size
                mMultiArrayAdapter.GetNodeSize(mMultiArrayAdapter)

                'mMultiArrayAdapter.Draw(PictureBox1, stageNo_L, NodetoMapL, Display_startX - mMultiArrayAdapter.Size_X, Display_startX, Display_startY - mMultiArrayAdapter.Size_Y, Display_startY) '暫定Node size 200*200

            Next

            For mI = 0 To gCRecipe.Node(stageNo_R).Count - 1
                NodetomapR = StageR.Node.Keys(mI)
                If IsNothing(NodetomapR) Then
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapR = StageR.Node(NodetomapR).Round.Count
                End If
                Display_startX = Stage_center + (BasicX(mI + gCRecipe.Node(stageNo_L).Count) - Min_BasicX_R)
                Display_startY = PictureBox1.Height - (BasicY(mI + gCRecipe.Node(stageNo_L).Count) - Min_BasicY)

                mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo_R)(NodetomapR).Array)

                'Toby add
                '取最高的level算size
                mMultiArrayAdapter.GetNodeSize(mMultiArrayAdapter)

                'mMultiArrayAdapter.Draw(PictureBox1, stageNo_R, NodetomapR, Display_startX, Display_startX + mMultiArrayAdapter.Size_X, Display_startY - mMultiArrayAdapter.Size_Y, Display_startY) '暫定Node size 200*200

            Next


            Dim LocatS_X, LocatS_Y, Pitch_X, Pitch_Y As Decimal
            Dim fileName As String
            Dim strSection As String

            If stageNo_L < 2 Then
                fileName = "D:\PIIMappingData\MapA.txt"
            Else
                fileName = "D:\PIIMappingData\MapB.txt"
            End If

            For mI = 0 To gCRecipe.Node(stageNo_L).Count - 1
                NodetoMapL = StageL.Node.Keys(mI)
                If IsNothing(NodetoMapL) Then
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapL = StageL.Node(NodetoMapL).Round.Count
                End If

                For i As Integer = 0 To StageL.Node(NodetoMapL).ScanGlueArray.GetUpperBound(0)
                    For j As Integer = 0 To StageL.Node(NodetoMapL).ScanGlueArray.GetUpperBound(1)
                        strSection = stageNo_L.ToString & "|" & NodetoMapL & "|" & i.ToString & "|" + j.ToString
                        LocatS_X = CDec(ReadIniString(strSection, "StartX", fileName, -1))
                        LocatS_Y = CDec(ReadIniString(strSection, "StartY", fileName, -1))
                        Pitch_X = CDec(ReadIniString(strSection, "PitchX", fileName, -1))
                        Pitch_Y = CDec(ReadIniString(strSection, "PitchY", fileName, -1))

                        'Fail die 不點膠
                        If StageL.Node(NodetoMapL).SBinMapData(i, j).Disable = True Then
                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.LightGray)
                        Else
                            '判斷是否定位失敗/點膠失敗
                            If IsNothing(StageL.Node(NodetoMapL).ChipState) = False Then '判斷是否有資料
                                Select Case StageL.Node(NodetoMapL).SBinMapData(i, j).Status
                                    Case ProjectCore.eDieStatus.Finish
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Green)
                                    Case ProjectCore.eDieStatus.AlignFail, ProjectCore.eDieStatus.LaserFail
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Yellow)
                                        Select Case ShowBIN
                                            Case 1
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageL.Node(NodetoMapL).SBinMapData(i, j).BinName)
                                            Case 2
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, "0/" + RoundMapL.ToString)
                                        End Select
                                    Case ProjectCore.eDieStatus.DispensingFail
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Red)
                                End Select
                            End If
                            '點膠Die_判斷目前執行Round
                            If StageL.Node(NodetoMapL).Round(0).DispensingStatus(i, j) = eDispensingStatus.None Then
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageL.Node(NodetoMapL).SBinMapData(i, j).BinName)
                                    Case 2
                                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, "0/" + RoundMapL.ToString)
                                End Select
                            Else
                                For NowRound = 0 To RoundMapL - 1
                                    RoundMapL_Status = StageL.Node(NodetoMapL).Round(NowRound)
                                    If RoundMapL_Status.DispensingStatus(i, j) = eDispensingStatus.OK Then
                                        If NowRound = RoundMapR - 1 Then
                                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Green)
                                        Else
                                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.LightGreen)
                                        End If
                                        Select Case ShowBIN
                                            Case 1
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageL.Node(NodetoMapL).SBinMapData(i, j).BinName)
                                            Case 2
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, (NowRound + 1).ToString + "/" + RoundMapL.ToString)
                                        End Select
                                    End If
                                Next
                            End If
                        End If
                    Next
                Next
            Next

            For mI = 0 To gCRecipe.Node(stageNo_R).Count - 1
                NodetomapR = StageR.Node.Keys(mI)
                If IsNothing(NodetomapR) Then
                    PictureBox1.Image = mBitmap
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapR = StageR.Node(NodetomapR).Round.Count
                End If

                For i As Integer = 0 To StageR.Node(NodetomapR).ScanGlueArray.GetUpperBound(0)
                    For j As Integer = 0 To StageR.Node(NodetomapR).ScanGlueArray.GetUpperBound(1)
                        strSection = stageNo_R.ToString & "|" & NodetomapR & "|" & i.ToString & "|" + j.ToString
                        LocatS_X = CDec(ReadIniString(strSection, "StartX", fileName, -1))
                        LocatS_Y = CDec(ReadIniString(strSection, "StartY", fileName, -1))
                        Pitch_X = CDec(ReadIniString(strSection, "PitchX", fileName, -1))
                        Pitch_Y = CDec(ReadIniString(strSection, "PitchY", fileName, -1))

                        'Fail die 不點膠
                        If StageR.Node(NodetomapR).SBinMapData(i, j).Disable = True Then
                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.LightGray)
                        Else
                            '判斷是否定位失敗/點膠失敗
                            If IsNothing(StageR.Node(NodetomapR).ChipState) = False Then '判斷是否有資料
                                Select Case StageR.Node(NodetomapR).SBinMapData(i, j).Status
                                    Case ProjectCore.eDieStatus.Finish
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Green)
                                    Case ProjectCore.eDieStatus.AlignFail, ProjectCore.eDieStatus.LaserFail
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Yellow)
                                        Select Case ShowBIN
                                            Case 1
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageR.Node(NodetomapR).SBinMapData(i, j).BinName)
                                            Case 2
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, "0/" + RoundMapR.ToString)
                                        End Select
                                    Case ProjectCore.eDieStatus.DispensingFail
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Red)
                                End Select
                            End If
                            '點膠Die_判斷目前執行Round
                            If StageR.Node(NodetomapR).Round(0).DispensingStatus(i, j) = eDispensingStatus.None Then
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageR.Node(NodetomapR).SBinMapData(i, j).BinName)
                                    Case 2
                                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, "0/" + RoundMapR.ToString)
                                End Select
                            Else
                                For NowRound = 0 To RoundMapR - 1
                                    RoundMapR_Status = StageR.Node(NodetomapR).Round(NowRound)
                                    If RoundMapR_Status.DispensingStatus(i, j) = eDispensingStatus.OK Then
                                        If NowRound = RoundMapR - 1 Then
                                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Green)
                                        Else
                                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.LightGreen)
                                        End If
                                        Select Case ShowBIN
                                            Case 1
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageR.Node(NodetomapR).SBinMapData(i, j).BinName)
                                            Case 2
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, (NowRound + 1).ToString + "/" + RoundMapR.ToString)
                                        End Select
                                    End If
                                Next
                            End If
                        End If
                    Next
                Next
            Next

            PictureBox1.Image = mBitmap
        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try

        PictureBox1.Refresh()
        Return True
    End Function

    Public Function DrawStageMap(Stage As CStageMap, stageNo As Integer) As Boolean  '根據Stage Map 顯示 1個Stage

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mWhite As New Drawing.SolidBrush(Color.White)
        Dim mBrush As New Drawing.SolidBrush(Color.DarkGray)
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小
        Dim mI As Integer
        Dim Temp_BasicX, Temp_BasicY As Integer
        Dim Min_BasicX As Integer = Int32.MaxValue
        Dim Min_BasicY As Integer = Int32.MaxValue

        Dim Display_startX, Display_startY As Integer


        Try

            If stageNo < 2 Then
                ClearMapFile("MapA")
            Else
                ClearMapFile("MapB")
            End If


            ReDim BasicX(gCRecipe.Node(stageNo).Count - 1)
            ReDim BasicY(gCRecipe.Node(stageNo).Count - 1)
            PictureBox1.Image = Nothing
            PictureBox1.Refresh()

            mBitmap = PictureBox1.Image
            mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            mDraw = Graphics.FromImage(mBitmap)
            '[說明]:畫外框(chuck大小)
            With mPen
                .Width = 3
                .DashStyle = Drawing2D.DashStyle.Solid
            End With
            '填滿
            mDraw.FillRectangle(mBrush, 0, 0, CInt(PictureBox1.Width) - 1, CInt(PictureBox1.Height) - 1)
            '畫線
            mDraw.DrawRectangle(mPen, 0, 0, CInt(PictureBox1.Width) - 1, CInt(PictureBox1.Height) - 1)



            '[Note]:取出NodeName，但目前基本上只允許一層
            For mI = 0 To gCRecipe.Node(stageNo).Count - 1
                NodetoMapL = Stage.Node.Keys(mI)

                BasicX(mI) = gCRecipe.Node(stageNo)(NodetoMapL).ConveyorPos(0).BasicPositionX
                BasicY(mI) = gCRecipe.Node(stageNo)(NodetoMapL).ConveyorPos(0).BasicPositionY
                Temp_BasicX = BasicX(mI)
                Temp_BasicY = BasicY(mI)

                If Temp_BasicX < Min_BasicX Then
                    Min_BasicX = Temp_BasicX
                End If
                If Temp_BasicY < Min_BasicY Then
                    Min_BasicY = Temp_BasicY
                End If
            Next

            For mI = 0 To gCRecipe.Node(stageNo).Count - 1
                NodetoMapL = Stage.Node.Keys(mI)
                If IsNothing(NodetoMapL) Then
                    PictureBox1.Image = mBitmap
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapL = Stage.Node(NodetoMapL).Round.Count
                End If


                Display_startX = (BasicX(mI) - Min_BasicX)
                Display_startY = PictureBox1.Height - (BasicY(mI) - Min_BasicY)
                mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo)(NodetoMapL).Array)
                'Toby add
                '取最高的level算size
                mMultiArrayAdapter.GetNodeSize(mMultiArrayAdapter)
                
                'mMultiArrayAdapter.Draw(PictureBox1, stageNo, NodetoMapL, Display_startX, Display_startX + mMultiArrayAdapter.Size_X, Display_startY - mMultiArrayAdapter.Size_Y, Display_startY) '暫定Node size 200*200

            Next

            PictureBox1.Refresh()

            Dim LocatS_X, LocatS_Y, Pitch_X, Pitch_Y As Decimal
            Dim fileName As String
            Dim strSection As String

            If stageNo < 2 Then
                fileName = "D:\PIIMappingData\MapA.txt"
            Else
                fileName = "D:\PIIMappingData\MapB.txt"
            End If

            For mI = 0 To gCRecipe.Node(stageNo).Count - 1
                NodetoMapL = Stage.Node.Keys(mI)
                If IsNothing(NodetoMapL) Then
                    PictureBox1.Image = mBitmap
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapL = Stage.Node(NodetoMapL).Round.Count
                End If

                For i As Integer = 0 To Stage.Node(NodetoMapL).ScanGlueArray.GetUpperBound(0)
                    For j As Integer = 0 To Stage.Node(NodetoMapL).ScanGlueArray.GetUpperBound(1)
                        strSection = stageNo & "|" & NodetoMapL & "|" & i.ToString & "|" + j.ToString
                        LocatS_X = CDec(ReadIniString(strSection, "StartX", fileName, -1))
                        LocatS_Y = CDec(ReadIniString(strSection, "StartY", fileName, -1))
                        Pitch_X = CDec(ReadIniString(strSection, "PitchX", fileName, -1))
                        Pitch_Y = CDec(ReadIniString(strSection, "PitchY", fileName, -1))

                        'Fail die 不點膠
                        If Stage.Node(NodetoMapL).SBinMapData(i, j).Disable = True Then
                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.LightGray)
                        Else
                            '判斷是否定位失敗/點膠失敗
                            If IsNothing(Stage.Node(NodetoMapL).ChipState) = False Then '判斷是否有資料
                                Select Case Stage.Node(NodetoMapL).SBinMapData(i, j).Status
                                    Case ProjectCore.eDieStatus.Finish
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Green)
                                    Case ProjectCore.eDieStatus.AlignFail, ProjectCore.eDieStatus.LaserFail
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Yellow)
                                        Select Case ShowBIN
                                            Case 1
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Stage.Node(NodetoMapL).SBinMapData(i, j).BinName)
                                            Case 2
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, "0/" + RoundMapL.ToString)
                                        End Select
                                    Case ProjectCore.eDieStatus.DispensingFail
                                        changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Red)
                                End Select
                            End If
                            '點膠Die_判斷目前執行Round
                            If Stage.Node(NodetoMapL).Round(0).DispensingStatus(i, j) = eDispensingStatus.None Then
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Stage.Node(NodetoMapL).SBinMapData(i, j).BinName)
                                    Case 2
                                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, "0/" + RoundMapL.ToString)
                                End Select
                            Else
                                For NowRound = 0 To RoundMapL - 1
                                    RoundMapL_Status = Stage.Node(NodetoMapL).Round(NowRound)
                                    If RoundMapL_Status.DispensingStatus(i, j) = eDispensingStatus.OK Then
                                        If NowRound = RoundMapL - 1 Then
                                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.Green)
                                        Else
                                            changeColor(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Color.LightGreen)
                                        End If
                                        Select Case ShowBIN
                                            Case 1
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Stage.Node(NodetoMapL).SBinMapData(i, j).BinName)
                                            Case 2
                                                WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, (NowRound + 1).ToString + "/" + RoundMapL.ToString)
                                        End Select
                                    End If
                                Next
                            End If
                        End If
                    Next
                Next
            Next
            PictureBox1.Image = mBitmap
        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try

        PictureBox1.Refresh()
        Return True
    End Function
    Public Sub PictureBox1_Click(sender As Object, e As MouseEventArgs) Handles PictureBox1.Click
        Dim mBitmap As Bitmap
        Dim Co As Color
        'Dim Node_data As String
        'Dim fileName As String
        Dim key As String
        Dim LocatS_X, LocatS_Y, LocatE_X, LocatE_Y As Decimal
        Dim MIN_X, MIN_Y, MAX_X, MAX_Y As Decimal
        mBitmap = PictureBox1.Image

        If IsNothing(map) Then
            Exit Sub
        End If
        If Me.Tag = "show_only" Then
            Exit Sub
        End If

        Select Case mEditType
            Case eMapEditType.SingleDie

                If map.Count > 0 Then
                    Co = mBitmap.GetPixel(e.X, e.Y)
                    If Co.ToArgb = Color.LightGray.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            If (e.X > MIN_X) And (e.X < MAX_X) And (e.Y > MIN_Y) And (e.Y < MAX_Y) Then
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.White)
                            End If
                        Next

                    ElseIf Co.ToArgb = Color.White.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            If (e.X > MIN_X) And (e.X < MAX_X) And (e.Y > MIN_Y) And (e.Y < MAX_Y) Then
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.LightGray)
                            End If
                        Next
                    End If
                End If
            Case eMapEditType.Row

                If map.Count > 0 Then
                    Co = mBitmap.GetPixel(e.X, e.Y)

                    If Co.ToArgb = Color.LightGray.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            If (e.Y > MIN_Y) And (e.Y < MAX_Y) Then
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.White)
                            End If
                        Next

                    ElseIf Co.ToArgb = Color.White.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            If (e.Y > MIN_Y) And (e.Y < MAX_Y) Then
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.LightGray)
                            End If
                        Next
                    End If
                End If

            Case eMapEditType.Column
                If map.Count > 0 Then
                    Co = mBitmap.GetPixel(e.X, e.Y)

                    If Co.ToArgb = Color.LightGray.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            If (e.X > MIN_X) And (e.X < MAX_X) Then
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.White)
                            End If
                        Next

                    ElseIf Co.ToArgb = Color.White.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            If (e.X > MIN_X) And (e.X < MAX_X) Then
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.LightGray)
                            End If
                        Next
                    End If
                End If

            Case eMapEditType.All
                If map.Count > 0 Then
                    Co = mBitmap.GetPixel(e.X, e.Y)

                    If Co.ToArgb = Color.LightGray.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            'If (e.X > MIN_X) And (e.X < MAX_X) And (e.Y > MIN_Y) And (e.Y < MAX_Y) Then
                            changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.White)
                            'End If
                        Next

                    ElseIf Co.ToArgb = Color.White.ToArgb Then
                        For mI = 0 To map.Count - 1
                            key = map.Keys(mI)
                            LocatS_X = map(key).StartXPos
                            LocatS_Y = map(key).StartYPos
                            LocatE_X = map(key).EndXPos
                            LocatE_Y = map(key).EndYPos
                            MIN_X = min(LocatS_X, LocatE_X)
                            MIN_Y = min(LocatS_Y, LocatE_Y)
                            MAX_X = max(LocatS_X, LocatE_X)
                            MAX_Y = max(LocatS_Y, LocatE_Y)
                            'If (e.X > MIN_X) And (e.X < MAX_X) And (e.Y > MIN_Y) And (e.Y < MAX_Y) Then
                            changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.LightGray)
                            'End If
                        Next
                    End If
                End If


        End Select



            PictureBox1.Refresh()

    End Sub
    Public Function DrawStageBin(Stage As CStageMap, stageNo As Integer) As Boolean  '根據Stage Map 顯示 1個Stage

        Try

            Dim LocatS_X, LocatS_Y, Pitch_X, Pitch_Y As Decimal
            Dim fileName As String
            Dim strSection As String

            If stageNo < 2 Then
                fileName = "D:\PIIMappingData\MapA.txt"
            Else
                fileName = "D:\PIIMappingData\MapB.txt"
            End If


            For mI = 0 To gCRecipe.Node(stageNo).Count - 1
                NodetoMapL = Stage.Node.Keys(mI)
                For i As Integer = 0 To Stage.Node(NodetoMapL).ScanGlueArray.GetUpperBound(0)
                    For j As Integer = 0 To Stage.Node(NodetoMapL).ScanGlueArray.GetUpperBound(1)

                        strSection = stageNo & "|" & NodetoMapL & "|" & i.ToString & "|" + j.ToString
                        LocatS_X = CInt(ReadIniString(strSection, "StartX", fileName, -1))
                        LocatS_Y = CInt(ReadIniString(strSection, "StartY", fileName, -1))
                        Pitch_X = CInt(ReadIniString(strSection, "PitchX", fileName, -1))
                        Pitch_Y = CInt(ReadIniString(strSection, "PitchY", fileName, -1))

                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, Stage.Node(NodetoMapL).SBinMapData(i, j).BinName)
                    Next
                Next

            Next

        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        PictureBox1.Refresh()
        Return True
    End Function
  
    Public Function DrawStageBin(StageL As CStageMap, stageNo_L As Integer, StageR As CStageMap, stageNo_R As Integer) As Boolean  '根據Stage Map 顯示 2個Stage
        ''寫Bin 資訊
        Try


            Dim LocatS_X, LocatS_Y, Pitch_X, Pitch_Y As Decimal
            Dim fileName As String
            Dim strSection As String


            If stageNo_L < 2 Then
                fileName = "D:\PIIMappingData\MapA.txt"
            Else
                fileName = "D:\PIIMappingData\MapB.txt"
            End If


            For mI = 0 To gCRecipe.Node(stageNo_L).Count - 1
                NodetoMapL = StageL.Node.Keys(mI)
                For i As Integer = 0 To StageL.Node(NodetoMapL).ScanGlueArray.GetUpperBound(0)
                    For j As Integer = 0 To StageL.Node(NodetoMapL).ScanGlueArray.GetUpperBound(1)
                        strSection = stageNo_L & "|" & NodetoMapL & "|" & i.ToString & "|" + j.ToString
                        LocatS_X = CInt(ReadIniString(strSection, "StartX", fileName, -1))
                        LocatS_Y = CInt(ReadIniString(strSection, "StartY", fileName, -1))
                        Pitch_X = CInt(ReadIniString(strSection, "PitchX", fileName, -1))
                        Pitch_Y = CInt(ReadIniString(strSection, "PitchY", fileName, -1))

                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageL.Node(NodetoMapL).SBinMapData(i, j).BinName)
                    Next
                Next
            Next
            For mI = 0 To gCRecipe.Node(stageNo_R).Count - 1
                NodetomapR = StageR.Node.Keys(mI)
                For i As Integer = 0 To StageR.Node(NodetomapR).ScanGlueArray.GetUpperBound(0)
                    For j As Integer = 0 To StageR.Node(NodetomapR).ScanGlueArray.GetUpperBound(1)
                        strSection = stageNo_R & "|" & NodetomapR & "|" & i.ToString & "|" + j.ToString
                        LocatS_X = CInt(ReadIniString(strSection, "StartX", fileName, -1))
                        LocatS_Y = CInt(ReadIniString(strSection, "StartY", fileName, -1))
                        Pitch_X = CInt(ReadIniString(strSection, "PitchX", fileName, -1))
                        Pitch_Y = CInt(ReadIniString(strSection, "PitchY", fileName, -1))

                        WriteText(LocatS_X, LocatS_Y, Pitch_X, Pitch_Y, StageR.Node(NodetomapR).SBinMapData(i, j).BinName)
                    Next
                Next
            Next

            PictureBox1.Refresh()
        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        Return True
    End Function
    Public Function DrawStageMap(showPos As Dictionary(Of String, CNodeInfo)) As Boolean  '顯示StageMap
        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小
        Dim mI As Integer
        Dim Stage As CStageMap
        Dim key As String
        map = showPos

        Try

            mBitmap = PictureBox1.Image
            'mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            If mBitmap Is Nothing Then
                mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            End If
            mDraw = Graphics.FromImage(mBitmap)

            Dim LocatS_X, LocatS_Y, LocatE_X, LocatE_Y As Decimal
            Dim MIN_X, MIN_Y As Decimal

            ''用showPos 分拆資訊 stageNO / Nodename / col /Row
            'gSyslog.Save("----------------------------------------------------------------------------DrawMapStart-----------------------------------------------------------------------")
            For mI = 0 To map.Count - 1
                key = map.Keys(mI)
                Dim node() As String
                node = key.Split("|")
                LocatS_X = map(key).StartXPos
                LocatS_Y = map(key).StartYPos
                LocatE_X = map(key).EndXPos
                LocatE_Y = map(key).EndYPos
                MIN_X = min(LocatS_X, LocatE_X)
                MIN_Y = min(LocatS_Y, LocatE_Y)
                If node(0) = 0 Then
                    Stage = gStageMap(0)
                ElseIf node(0) = 1 Then
                    Stage = gStageMap(1)
                ElseIf node(0) = 2 Then
                    Stage = gStageMap(2)
                ElseIf node(0) = 3 Then
                    Stage = gStageMap(3)
                Else
                    Return False
                    Exit Function
                End If

                'gSyslog.Save(node(2) & "," & node(3) & ":")
                'gSyslog.Save("dieStatus" & Stage.Node(node(1)).SBinMapData(node(2), node(3)).Status)
                'gSyslog.Save("ByPassCCD:" & Stage.Node(node(1)).SRecipePos(node(2), node(3)).IsByPassCCDScanFixAction.ToString)
                'gSyslog.Save("ByPassLaser:" & Stage.Node(node(1)).SRecipePos(node(2), node(3)).IsByPassLaserAction.ToString)
                'gSyslog.Save("ByPassDispen:" & Stage.Node(node(1)).SRecipePos(node(2), node(3)).IsByPassDispensingAction.ToString)



                If Stage.Node(node(1)).SBinMapData(node(2), node(3)).Disable = True Then
                    changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.LightGray)
                Else
                    '判斷是否定位失敗/點膠失敗
                    If IsNothing(Stage.Node(node(1)).ChipState) = False Then '判斷是否有資料
                        Select Case Stage.Node(node(1)).SBinMapData(node(2), node(3)).Status
                            Case ProjectCore.eDieStatus.Finish
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.Green)
                            Case ProjectCore.eDieStatus.AlignFail, ProjectCore.eDieStatus.LaserFail
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.Yellow)
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Stage.Node(node(1)).SBinMapData(node(2), node(3)).BinName)
                                    Case 2
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), "0/" + Stage.Node(node(1)).Round.Count.ToString)
                                End Select
                            Case ProjectCore.eDieStatus.DispensingFail
                                changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.Red)
                        End Select
                    End If
                    '點膠Die_判斷目前執行Round
                    RoundMapR = Stage.Node(node(1)).Round.Count
                    If Stage.Node(node(1)).Round(0).DispensingStatus(node(2), node(3)) = eDispensingStatus.None Then
                        If Stage.Node(node(1)).SBinMapData(node(2), node(3)).Status = eDieStatus.None Then
                            changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.White)
                        End If
                        Select Case ShowBIN
                            Case 1
                                WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Stage.Node(node(1)).SBinMapData(node(2), node(3)).BinName)
                            Case 2
                                WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), "0/" + Stage.Node(node(1)).Round.Count.ToString)
                        End Select
                    Else
                        For NowRound = 0 To Stage.Node(node(1)).Round.Count - 1
                            RoundMapR_Status = Stage.Node(node(1)).Round(NowRound)
                            If RoundMapR_Status.DispensingStatus(node(2), node(3)) = eDispensingStatus.OK Then
                                If NowRound = RoundMapR - 1 Then
                                    changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.Green)
                                Else
                                    changeColor(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Color.LightGreen)
                                End If
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Stage.Node(node(1)).SBinMapData(node(2), node(3)).BinName)
                                    Case 2
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), (NowRound + 1).ToString + "/" + RoundMapR.ToString)
                                End Select
                            End If
                        Next
                    End If
                End If
            Next
            'gSyslog.Save(" - ---------------------------------------------------------------------------DrawMapEnd - ----------------------------------------------------------------------")
            PictureBox1.Image = mBitmap
            PictureBox1.Refresh()
        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        Return True
    End Function
    Public Function IniMap(showPos As Dictionary(Of String, CNodeInfo)) As Boolean  'ini 顯示的StageMap
        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim mWhite As New Drawing.SolidBrush(Color.White)
        Dim mRed As New Drawing.SolidBrush(Color.Red)
        Dim mYellow As New Drawing.SolidBrush(Color.Yellow)
        Dim mGreen As New Drawing.SolidBrush(Color.Green)
        Dim mLightGreen As New Drawing.SolidBrush(Color.LightGreen)
        Dim mPointSize As Integer = CInt(PictureBox1.Width / 44.8) '繪點大小
        Dim mI As Integer
        Dim key As String
        Dim Stage As CStageMap
        map = showPos

        Try

            mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            mDraw = Graphics.FromImage(mBitmap)

            Dim LocatS_X, LocatS_Y, LocatE_X, LocatE_Y As Decimal
            Dim MIN_X, MIN_Y As Decimal

            For mI = 0 To map.Count - 1
                key = map.Keys(mI)
                Dim node() As String
                node = key.Split("|")
                LocatS_X = map(key).StartXPos
                LocatS_Y = map(key).StartYPos
                LocatE_X = map(key).EndXPos
                LocatE_Y = map(key).EndYPos
                MIN_X = min(LocatS_X, LocatE_X)
                MIN_Y = min(LocatS_Y, LocatE_Y)
                If node(0) = 0 Then
                    Stage = gStageMap(0)
                ElseIf node(0) = 1 Then
                    Stage = gStageMap(1)
                ElseIf node(0) = 2 Then
                    Stage = gStageMap(2)
                ElseIf node(0) = 3 Then
                    Stage = gStageMap(3)
                Else
                    Return False
                    Exit Function
                End If

                If Stage.Node(node(1)).SBinMapData(node(2), node(3)).Disable = True Then
                    mDraw.FillRectangle(mBrush, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                Else
                    '判斷是否定位失敗/點膠失敗
                    If IsNothing(Stage.Node(node(1)).ChipState) = False Then '判斷是否有資料
                        Select Case Stage.Node(node(1)).SBinMapData(node(2), node(3)).Status
                            Case ProjectCore.eDieStatus.Finish
                                mDraw.FillRectangle(mGreEn, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                            Case ProjectCore.eDieStatus.AlignFail, ProjectCore.eDieStatus.LaserFail
                                mDraw.FillRectangle(mYellow, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Stage.Node(node(1)).SBinMapData(node(2), node(3)).BinName)
                                    Case 2
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), "0/" + Stage.Node(node(1)).Round.Count.ToString)
                                End Select
                            Case ProjectCore.eDieStatus.DispensingFail
                                mDraw.FillRectangle(mRed, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                        End Select
                    End If
                    '點膠Die_判斷目前執行Round
                    RoundMapR = Stage.Node(node(1)).Round.Count
                    If Stage.Node(node(1)).Round(0).DispensingStatus(node(2), node(3)) = eDispensingStatus.None Then
                        If Stage.Node(node(1)).SBinMapData(node(2), node(3)).Status = eDieStatus.None Then
                            mDraw.FillRectangle(mWhite, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                        End If
                        Select Case ShowBIN
                            Case 1
                                WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Stage.Node(node(1)).SBinMapData(node(2), node(3)).BinName)
                            Case 2
                                WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), "0/" + Stage.Node(node(1)).Round.Count.ToString)
                        End Select
                    Else
                        For NowRound = 0 To Stage.Node(node(1)).Round.Count - 1
                            RoundMapR_Status = Stage.Node(node(1)).Round(NowRound)
                            If RoundMapR_Status.DispensingStatus(node(2), node(3)) = eDispensingStatus.OK Then
                                If NowRound = RoundMapR - 1 Then
                                    mDraw.FillRectangle(mGreen, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                                Else
                                    mDraw.FillRectangle(mLightGreen, MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y))
                                End If
                                Select Case ShowBIN
                                    Case 1
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), Stage.Node(node(1)).SBinMapData(node(2), node(3)).BinName)
                                    Case 2
                                        WriteText(MIN_X, MIN_Y, Math.Abs(LocatE_X - LocatS_X), Math.Abs(LocatE_Y - LocatS_Y), (NowRound + 1).ToString + "/" + RoundMapR.ToString)
                                End Select
                            End If
                        Next
                    End If
                End If
            Next

            mBrush.Dispose()
            mBrush = Nothing
            mWhite.Dispose()
            mWhite = Nothing
            mRed.Dispose()
            mRed = Nothing
            mYellow.Dispose()
            mYellow = Nothing
            mGreen.Dispose()
            mGreen = Nothing
            mLightGreen.Dispose()
            mLightGreen = Nothing
            PictureBox1.Image = mBitmap
            PictureBox1.Refresh()
            first = False

        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        Return True
    End Function



    Public Sub ucWaferMap_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me.Tag = "show_only" Then
            'Me.ShowBIN = frmOperator.cboShowData.SelectedIndex
        End If

    End Sub

    Public Sub ClearMapFile(ByVal fileName As String)
       
        Dim mBitmap As Bitmap = Me.PictureBox1.Image
        mBitmap = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        first = True
        Me.PictureBox1.Image = mBitmap
        Me.Refresh()

        'Dim mFileDirectory As String      '[檔案目錄]
        'Dim mFileName As String           '[檔案名稱]

        ''[說明]:檢查目錄是否存在
        'mFileDirectory = "D:\PIIMappingData\"
        'If Directory.Exists(mFileDirectory) = False Then
        '    '[說明]:目錄不存在就建目錄
        '    Directory.CreateDirectory(mFileDirectory)
        'End If

        'mFileName = "\" & fileName & ".txt"
        ''[說明]:檢查檔案是否存在
        'File.Delete(mFileDirectory & mFileName)


    End Sub
End Class

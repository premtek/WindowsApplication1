Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Windows.Forms.ToolTip
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectCore
Imports ProjectIO
Imports ProjectAOI
Imports ProjectFeedback
Imports Cognex.VisionPro
Imports Cognex.VisionPro.PMAlign
Imports MapData


Public Class frmDrawGraphics

    Public Class CHostMap
        Public dblColnum As String
        Public lstData As List(Of String) = New List(Of String)
        Public Row() As String
    End Class

    Public Class COriginal
        Public dblColnum As String
        Public lstData As List(Of String) = New List(Of String)
        Public Row() As String
    End Class

    Public Class CStartOfMap
        Public dblColnum As String
        Public lstData As List(Of String) = New List(Of String)
        Public Row() As String
    End Class

    Public Class CStartOfInspectionInTape
        Public dblColnum As String
        Public lstData As List(Of String) = New List(Of String)
        Public Row() As String
    End Class

    Public Class CProcessedFrameMap
        Public dblColnum As String
        Public lstData As List(Of String) = New List(Of String)
        Public Row() As String
    End Class

    Public Class CWaferMap

        ''' <summary>[XDieSizw]</summary>
        ''' <remarks></remarks>
        Public dblXDieSizw As Double
        ''' <summary>[YDieSizw]</summary>
        ''' <remarks></remarks>
        Public dblYDieSizw As Double
        ''' <summary>[XDie]</summary>
        ''' <remarks></remarks>
        Public dblXDie As Double
        ''' <summary>[YDie]</summary>
        ''' <remarks></remarks>
        Public dblYDie As Double
        ''' <summary>[XRefDie]</summary>
        ''' <remarks></remarks>
        Public dblXRefDie As Double
        ''' <summary>[YRefDie]</summary>
        ''' <remarks></remarks>
        Public dblYRefDie As Double
        ''' <summary>[RAW MAP FLAT NOTCH]</summary>
        ''' <remarks></remarks>
        Public dblRawMapFlatNotch As Double
        ''' <summary>[LOT REEL NUMBER]</summary>
        ''' <remarks></remarks>
        Public dblLotReelNum As Double
        ''' <summary>[LOT COUNTER PER FRAME]</summary>
        ''' <remarks></remarks>
        Public dblLotCounterPerFrame As Double
        ''' <summary>[LOT IN THIS FRAME]</summary>
        ''' <remarks></remarks>
        Public dblLotInThisFrame As Double
        ''' <summary>[LotName429]</summary>
        ''' <remarks></remarks>
        Public dblLotName429 As Double

        ''' <summary>MapfileName</summary>
        ''' <remarks></remarks>
        Public strMapfileName As String
        ''' <summary>Lot Start Date429</summary>
        ''' <remarks></remarks>
        Public strStartData As String
        ''' <summary>Lot Start Time429</summary>
        ''' <remarks></remarks>
        Public strStartTime As String

        ''' <summary>HOST MAP</summary>
        ''' <remarks></remarks>
        Public HostMap As New CHostMap
        ''' <summary>ORIGINAL MAP</summary>
        ''' <remarks></remarks>
        Public Original As New COriginal
        ''' <summary>START OF MAP</summary>
        ''' <remarks></remarks>
        Public StartOfMap As New CStartOfMap
        ''' <summary>START OF INSPECTION / INTAPE</summary>
        ''' <remarks></remarks>
        Public StartOfInspectionInTape As New CStartOfInspectionInTape
        ''' <summary>START OF SKELETON</summary>
        ''' <remarks></remarks>
        Public StartOfSkeleTon As New CStartOfInspectionInTape
        ''' <summary>PROCESSED FRAME MAP</summary>
        ''' <remarks></remarks>
        Public ProcessedFrameMap As New CProcessedFrameMap



    End Class


    Public WaferMap As New CWaferMap

    Private Sub bntLoad_Click(sender As Object, e As EventArgs) Handles bntLoad.Click
        '[說明]:選取Recipe檔案

        With OFDLoadRecipe
            .InitialDirectory = "D:\"        '取得或設定檔案對話方塊所顯示的初始目錄。
            '  .Filter = "文字檔 (*.rcp)|*.rcp"  '取得或設定目前的檔名篩選字串，以決定出現在對話方塊中 [另存檔案類型] 或 [檔案類型] 方塊的選項。
            .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            .FilterIndex = 2                 '取得或設定檔案對話方塊中目前所選取之篩選條件的索引。
            .RestoreDirectory = True         '取得或設定值，指出對話方塊是否在關閉前將目錄還原至先前選取的目錄。

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            Dim sz As String = .FileName

            '  If System.IO.Directory.Exists(.FileName) Then        '檢查目錄是否存在
            If My.Computer.FileSystem.FileExists(.FileName) Then  '檢查檔案是否存在

                LoadRecipeisort(.FileName)

            End If

        End With

    End Sub

    Public Function LoadRecipeisort(ByVal FileName As String) As Boolean
        Dim strFileName As String
        Dim strSection As String
        strFileName = FileName

        strSection = "STI GENERIC WAFER MAP"
        With WaferMap
            .dblXDieSizw = Val(ReadIniString(strSection, "X DIE SIZE", strFileName, 0))
            .dblYDieSizw = Val(ReadIniString(strSection, "Y DIE SIZE", strFileName, 0))
            .dblXDie = Val(ReadIniString(strSection, "X DIES", strFileName, 0))
            .dblYDie = Val(ReadIniString(strSection, "Y DIES", strFileName, 0))
            .dblXRefDie = Val(ReadIniString(strSection, "X REF DIE", strFileName, 0))
            .dblYRefDie = Val(ReadIniString(strSection, "Y REF DIE", strFileName, 0))
            .dblRawMapFlatNotch = Val(ReadIniString(strSection, "RAW MAP FLAT NOTCH", strFileName, 0))
            .dblLotReelNum = Val(ReadIniString(strSection, "LOT REEL NUMBER", strFileName, 0))
            .dblLotInThisFrame = Val(ReadIniString(strSection, "LOT COUNTER PER FRAME", strFileName, 0))
            .dblLotCounterPerFrame = Val(ReadIniString(strSection, "LOT IN THIS FRAME", strFileName, 0))
            .dblLotName429 = Val(ReadIniString(strSection, "LotName429", strFileName, 0))
            .strMapfileName = ReadIniString(strSection, "MapfileName", strFileName, "")
            .strStartData = ReadIniString(strSection, "Lot Start Date429", strFileName, "")
            .strStartTime = ReadIniString(strSection, "Lot Start Time429", strFileName, "")

        End With

        Dim XDie As Double = WaferMap.dblXDie - 1
        Dim YDie As Double = WaferMap.dblYDie - 1
        Dim sz As String
        Dim szz As String

        '判斷是否為整數 是否大於零
        If IsNumeric(YDie) Then
            If XDie > 0 Then
                '判斷是否有宣告 無則動態宣告大小
                If WaferMap.HostMap.Row Is Nothing Then
                    ReDim WaferMap.HostMap.Row(YDie)
                End If
                If WaferMap.Original.Row Is Nothing Then
                    ReDim WaferMap.Original.Row(YDie)
                End If
                If WaferMap.StartOfMap.Row Is Nothing Then
                    ReDim WaferMap.StartOfMap.Row(YDie)
                End If
                If WaferMap.StartOfInspectionInTape.Row Is Nothing Then
                    ReDim WaferMap.StartOfInspectionInTape.Row(YDie)
                End If
                If WaferMap.StartOfSkeleTon.Row Is Nothing Then
                    ReDim WaferMap.StartOfSkeleTon.Row(YDie)
                End If
                If WaferMap.ProcessedFrameMap.Row Is Nothing Then
                    ReDim WaferMap.ProcessedFrameMap.Row(YDie)
                End If
            End If
        End If




        strSection = "HOST MAP"
        WaferMap.HostMap.dblColnum = ReadIniString(strSection, "COLNUM", strFileName, "XXX")
        For i As Integer = 0 To YDie

            sz = "ROW"
            sz = sz & (i + 1).ToString("000")
            WaferMap.HostMap.Row(i) = ReadIniString(strSection, sz, strFileName, "")
            szz = ReadIniString(strSection, sz, strFileName, "XXX")
            WaferMap.HostMap.lstData.Add(szz)                     'List 存
            '  szz = WaferMap.HostMap.lstData.Item(9)             'List 取

        Next

        strSection = "ORIGINAL MAP"
        WaferMap.Original.dblColnum = ReadIniString(strSection, "COLNUM", strFileName, "")
        For i As Integer = 0 To YDie
            sz = "ROW"
            sz = sz & (i + 1).ToString("000")
            WaferMap.Original.Row(i) = ReadIniString(strSection, sz, strFileName, "")
            szz = ReadIniString(strSection, sz, strFileName, "XXX")
            WaferMap.Original.lstData.Add(szz)                     'List 存
            '  szz = WaferMap.HostMap.lstData.Item(9)              'List 取
        Next

        strSection = "START OF MAP"
        WaferMap.StartOfMap.dblColnum = ReadIniString(strSection, "COLNUM", strFileName, "")
        For i As Integer = 0 To YDie
            sz = "ROW"
            sz = sz & (i + 1).ToString("000")
            WaferMap.StartOfMap.Row(i) = ReadIniString(strSection, sz, strFileName, "")
            szz = ReadIniString(strSection, sz, strFileName, "")
            WaferMap.StartOfMap.lstData.Add(szz)                     'List 存
            '  szz = WaferMap.HostMap.lstData.Item(9)                'List 取
        Next

        strSection = "START OF INSPECTION / INTAPE"
        WaferMap.StartOfInspectionInTape.dblColnum = ReadIniString(strSection, "COLNUM", strFileName, "")
        For i As Integer = 0 To YDie
            sz = "ROW"
            sz = sz & (i + 1).ToString("000")
            WaferMap.StartOfInspectionInTape.Row(i) = ReadIniString(strSection, sz, strFileName, "")
            szz = ReadIniString(strSection, sz, strFileName, "")
            WaferMap.StartOfInspectionInTape.lstData.Add(szz)                     'List 存
            '  szz = WaferMap.HostMap.lstData.Item(9)             'List 取
        Next

        strSection = "START OF SKELETON"
        WaferMap.StartOfSkeleTon.dblColnum = ReadIniString(strSection, "COLNUM", strFileName, "")
        For i As Integer = 0 To YDie
            sz = "ROW"
            sz = sz & (i + 1).ToString("000")
            WaferMap.StartOfSkeleTon.Row(i) = ReadIniString(strSection, sz, strFileName, "")
            szz = ReadIniString(strSection, sz, strFileName, "")
            WaferMap.StartOfSkeleTon.lstData.Add(szz)                     'List 存
            '  szz = WaferMap.HostMap.lstData.Item(9)             'List 取
        Next

        strSection = "PROCESSED FRAME MAP"
        WaferMap.ProcessedFrameMap.dblColnum = ReadIniString(strSection, "COLNUM", strFileName, "")
        For i As Integer = 0 To YDie
            sz = "ROW"
            sz = sz & (i + 1).ToString("000")
            WaferMap.ProcessedFrameMap.Row(i) = ReadIniString(strSection, sz, strFileName, "")
            szz = ReadIniString(strSection, sz, strFileName, "")
            WaferMap.ProcessedFrameMap.lstData.Add(szz)                     'List 存
            '  szz = WaferMap.HostMap.lstData.Item(9)             'List 取
        Next

        DrawGraphicsMultiDeviceTx(picDrawGraphicsRecipeGraph)

        Return True
    End Function

    Public Function DrawGraphicsMultiDeviceTx(ByRef graphicsPictureBox As PictureBox) As Boolean
        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim RedPen As New Drawing.SolidBrush(Color.Red)
        Dim WhitePen As New Drawing.SolidBrush(Color.White)
        Dim mPointSize As Integer = CInt(graphicsPictureBox.Width / 44.8) '繪點大小
        'Dim mScaleX As Double '換算比率
        'Dim mScaleY As Double '

        mBitmap = picDrawGraphicsRecipeGraph.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(picDrawGraphicsRecipeGraph.Width), CInt(picDrawGraphicsRecipeGraph.Height))
        End If

        mDraw = Graphics.FromImage(mBitmap)

        '[說明]:畫外框(chuck大小)
        With mPen
            .Width = 3
            .DashStyle = Drawing2D.DashStyle.Solid
        End With
        '填滿
        mDraw.FillRectangle(mBrush, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)
        '畫線
        mDraw.DrawRectangle(mPen, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)

        '[說明]:畫中心線
        With mPen
            .Width = graphicsPictureBox.Width / 500
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.LightGreen
        End With
        mDraw.DrawLine(mPen, 0, CInt(graphicsPictureBox.Height / 2), graphicsPictureBox.Width, CInt(graphicsPictureBox.Height / 2))
        mDraw.DrawLine(mPen, CInt(graphicsPictureBox.Width / 2), 0, CInt(graphicsPictureBox.Width / 2), graphicsPictureBox.Height)

        '[說明]畫點
        ' mDraw.FillEllipse(RedPen, CInt(100), CInt(100), mPointSize, mPointSize)


        'mScaleX = graphicsPictureBox.Height / 實際比例
        'mScaleY = graphicsPictureBox.Width  / 實際比例
        '[說明]繪製坐標系
        'DrawCoord(mDraw, graphicsPictureBox.Width, graphicsPictureBox.Height)
        '-----------------------------------------------------------------------------------------------------
        ' Create rectangle.
        Dim rect As New Rectangle(0, 0, 200, 200)
        ' Create pen.
        Dim blackPen As New Pen(Color.Black)
        Dim RredPen As New Pen(Color.Red)
        Dim XDie As Double = WaferMap.dblXDie - 1
        Dim YDie As Double = WaferMap.dblYDie - 1

        For i As Integer = 0 To YDie
            For j As Integer = 0 To XDie
                Dim sz As String = WaferMap.HostMap.Row(i).Split(" ")(j)

                If IsNumeric(WaferMap.HostMap.Row(i).Split(" ")(j)) Then

                    Dim q As Double = CInt(WaferMap.dblXDieSizw) * 4 + 1 ' = 9
                    Dim w As Double = CInt(WaferMap.dblYDieSizw) * 2 + 1 ' = 9


                    'graphicsPictureBox.Width   graphicsPictureBox.Height
                    If WaferMap.HostMap.Row(i).Split(" ")(j) = "033" Then
                        '   mDraw.FillRectangle(RedPen, (i * 9) + 20, (j * 9) + 80, (CInt(WaferMap.dblXDieSizw) * 4 + 1), (CInt(WaferMap.dblYDieSizw) * 2 + 1))
                        mDraw.FillRectangle(RedPen, graphicsPictureBox.Width - (i * 9) - 20, (j * 9) + 80, (CInt(WaferMap.dblXDieSizw) * 4 + 1), (CInt(WaferMap.dblYDieSizw) * 2 + 1))
                    End If
                    If WaferMap.HostMap.Row(i).Split(" ")(j) = "___" Then
                        '  mDraw.FillRectangle(WhitePen, (i * 9) + 20, (j * 9) + 80, (CInt(WaferMap.dblXDieSizw) * 4 + 1), (CInt(WaferMap.dblYDieSizw) * 2 + 1))
                        mDraw.FillRectangle(WhitePen, graphicsPictureBox.Width - (i * 9) - 20, (j * 9) + 80, (CInt(WaferMap.dblXDieSizw) * 4 + 1), (CInt(WaferMap.dblYDieSizw) * 2 + 1))
                    End If

                    q = CInt(WaferMap.dblXDieSizw) * 9 '18
                    w = CInt(WaferMap.dblYDieSizw) * 9 '36
                    '   mDraw.DrawRectangle(blackPen, (i * 9) + 20, (j * 9) + 80, 9, 9)
                    mDraw.DrawRectangle(blackPen, graphicsPictureBox.Width - (i * 9) - 20, (j * 9) + 80, 9, 9)


                End If
            Next
        Next

        '-----------------------------------------------------------------------------------------------------
        graphicsPictureBox.Image = mBitmap

        Return True
    End Function

    ''' <summary>繪製座標系</summary>
    ''' <param name="draw"></param>
    ''' <remarks></remarks>
    Public Sub DrawCoord(ByRef draw As Graphics, ByVal Width As Double, ByVal Height As Double)
        Dim bluePen As New Pen(Brushes.Blue, 3)
        Dim GreenPen As New Pen(Brushes.Green, 3)
        Dim RedPen As New Pen(Brushes.Red, 3)
        Select Case gSSystemParameter.CoordType
            Case 1
                '                'TH
                '                draw.DrawArc(RedPen, 0, CSng(Height) - 30, 30, 30, 0, -90)
                '                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 15, CSng(Height) - 45)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 28, 15, CSng(Height) - 35)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 32, 15, CSng(Height) - 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, 10, CSng(Height) - 10, 35, CSng(Height) - 10)
                draw.DrawLine(bluePen, 37, CSng(Height) - 12, 30, CSng(Height) - 5)
                draw.DrawLine(bluePen, 37, CSng(Height) - 8, 30, CSng(Height) - 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, 37, CSng(Height) - 28)

                '綠色箭頭向上
                draw.DrawLine(GreenPen, 10, CSng(Height) - 10, 10, CSng(Height) - 35)
                draw.DrawLine(GreenPen, 12, CSng(Height) - 37, 5, CSng(Height) - 30)
                draw.DrawLine(GreenPen, 8, CSng(Height) - 37, 15, CSng(Height) - 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, 2, CSng(Height) - 57)
            Case 2
                '                'TH
                '                draw.DrawArc(RedPen, 0, CSng(Height) - 30, 30, 30, 0, -90)
                '                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 15, CSng(Height) - 45)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 28, 15, CSng(Height) - 35)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 32, 15, CSng(Height) - 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, CSng(Width) - 10, CSng(Height) - 10, CSng(Width) - 35, CSng(Height) - 10)
                draw.DrawLine(bluePen, CSng(Width) - 37, CSng(Height) - 12, CSng(Width) - 30, CSng(Height) - 5)
                draw.DrawLine(bluePen, CSng(Width) - 37, CSng(Height) - 8, CSng(Width) - 30, CSng(Height) - 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, CSng(Width) - 49, CSng(Height) - 28)

                '綠色箭頭向上
                draw.DrawLine(GreenPen, CSng(Width) - 10, CSng(Height) - 10, CSng(Width) - 10, CSng(Height) - 35)
                draw.DrawLine(GreenPen, CSng(Width) - 12, CSng(Height) - 37, CSng(Width) - 5, CSng(Height) - 30)
                draw.DrawLine(GreenPen, CSng(Width) - 8, CSng(Height) - 37, CSng(Width) - 15, CSng(Height) - 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, CSng(Width) - 18, CSng(Height) - 57)
            Case 3
                '                'TH
                '                draw.DrawArc(RedPen, 0, CSng(Height) - 30, 30, 30, 0, -90)
                '                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 15, CSng(Height) - 45)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 28, 15, CSng(Height) - 35)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 32, 15, CSng(Height) - 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, CSng(Width) - 10, 10, CSng(Width) - 35, 10)
                draw.DrawLine(bluePen, CSng(Width) - 37, 12, CSng(Width) - 30, 5)
                draw.DrawLine(bluePen, CSng(Width) - 37, 8, CSng(Width) - 30, 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, CSng(Width) - 49, 16)

                '綠色箭頭向上
                draw.DrawLine(GreenPen, CSng(Width) - 10, 10, CSng(Width) - 10, 35)
                draw.DrawLine(GreenPen, CSng(Width) - 12, 37, CSng(Width) - 5, 30)
                draw.DrawLine(GreenPen, CSng(Width) - 8, 37, CSng(Width) - 15, 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, CSng(Width) - 18, 40)
            Case 4
                '                'TH
                '                draw.DrawArc(RedPen, 0, CSng(Height) - 30, 30, 30, 0, -90)
                '                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 15, CSng(Height) - 45)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 28, 15, CSng(Height) - 35)
                '                draw.DrawLine(RedPen, 8, CSng(Height) - 32, 15, CSng(Height) - 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, 10, 10, 35, 10)
                draw.DrawLine(bluePen, 37, 12, 30, 5)
                draw.DrawLine(bluePen, 37, 8, 30, 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, 37, 16)

                '綠色箭頭向上
                draw.DrawLine(GreenPen, 10, 10, 10, 35)
                draw.DrawLine(GreenPen, 12, 37, 5, 30)
                draw.DrawLine(GreenPen, 8, 37, 15, 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, 2, 40)
            Case enmCoordinateRelationType.eGN2
                'TH
                draw.DrawArc(RedPen, 0, 0, 30, 30, 0, 90)
                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 25, 25)
                draw.DrawLine(RedPen, 8, 28, 15, 35)
                draw.DrawLine(RedPen, 8, 32, 15, 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, 10, 10, 35, 10)
                draw.DrawLine(bluePen, 37, 12, 30, 5)
                draw.DrawLine(bluePen, 37, 8, 30, 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, 37, 2)
                '綠色箭頭向下
                draw.DrawLine(GreenPen, 10, 10, 10, 35)
                draw.DrawLine(GreenPen, 12, 37, 5, 30)
                draw.DrawLine(GreenPen, 8, 37, 15, 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, 2, 37)
            Case enmCoordinateRelationType.eDTS
                'TH
                draw.DrawArc(RedPen, 0, CSng(Height) - 30, 30, 30, 0, -90)
                draw.DrawString("Th", New Font("Arial", 12), Brushes.Red, 15, CSng(Height) - 45)
                draw.DrawLine(RedPen, 8, CSng(Height) - 28, 15, CSng(Height) - 35)
                draw.DrawLine(RedPen, 8, CSng(Height) - 32, 15, CSng(Height) - 25)

                '藍色箭頭向右
                draw.DrawLine(bluePen, 10, CSng(Height) - 10, 35, CSng(Height) - 10)
                draw.DrawLine(bluePen, 37, CSng(Height) - 12, 30, CSng(Height) - 5)
                draw.DrawLine(bluePen, 37, CSng(Height) - 8, 30, CSng(Height) - 15)
                draw.DrawString("X", New Font("Arial", 12), Brushes.Blue, 37, CSng(Height) - 28)

                '綠色箭頭向上
                draw.DrawLine(GreenPen, 10, CSng(Height) - 10, 10, CSng(Height) - 35)
                draw.DrawLine(GreenPen, 12, CSng(Height) - 37, 5, CSng(Height) - 30)
                draw.DrawLine(GreenPen, 8, CSng(Height) - 37, 15, CSng(Height) - 30)
                draw.DrawString("Y", New Font("Arial", 12), Brushes.Green, 2, CSng(Height) - 57)

        End Select

    End Sub

    Private Sub picDrawGraphicsRecipeGraph_MouseMove(sender As Object, e As MouseEventArgs) Handles picDrawGraphicsRecipeGraph.MouseMove
        Dim X As Integer
        Dim Y As Integer
        Dim mScaleY As Integer
        Dim mScaleX As Integer

        '[Note]:Nothing But Why???
        If Not IsNothing(gfrmMapData.Substrates) = True Then
            mScaleY = picDrawGraphicsRecipeGraph.Height / gfrmMapData.Substrates(0).Rows      '實際比例
            mScaleX = picDrawGraphicsRecipeGraph.Width / gfrmMapData.Substrates(0).Columns    '實際比例
            X = e.X \ mScaleX  '利用點擊位置再除以die size得到die 座標
            Y = e.Y \ mScaleY
            Mouse_DIE_locate.Text = "die:" + CStr(X + 1) + "," + CStr(Y + 1)
        End If

    End Sub


    Public Function DrawMap(ByRef graphicsPictureBox As PictureBox) As Boolean

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim RedPen As New Drawing.SolidBrush(Color.Red)
        Dim WhitePen As New Drawing.SolidBrush(Color.White)
        Dim mPointSize As Integer = CInt(graphicsPictureBox.Width / 44.8) '繪點大小
        'Dim mScaleX As Double '換算比率 = 產品尺寸
        'Dim mScaleY As Double
        'Dim CCDWidth As Double   'pixels 
        'Dim CCDHeight As Double
        'Dim mDrawIdx As sLevelIndexCollection

        'mScaleX = graphicsPictureBox.Height / 實際比例
        'mScaleY = graphicsPictureBox.Width  / 實際比例
        'Dim CCDWidth As Double 	 pixels 
        'Dim CCDHeight As Double 


        mBitmap = picDrawGraphicsRecipeGraph.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(picDrawGraphicsRecipeGraph.Width), CInt(picDrawGraphicsRecipeGraph.Height))
        End If

        mDraw = Graphics.FromImage(mBitmap)

        '[說明]:畫外框(chuck大小)
        With mPen
            .Width = 3
            .DashStyle = Drawing2D.DashStyle.Solid
        End With
        '填滿
        mDraw.FillRectangle(mBrush, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)
        '畫線
        mDraw.DrawRectangle(mPen, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)

        '[說明]:畫中心線
        With mPen
            .Width = graphicsPictureBox.Width / 500
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.LightGreen
        End With
        mDraw.DrawLine(mPen, 0, CInt(graphicsPictureBox.Height / 2), graphicsPictureBox.Width, CInt(graphicsPictureBox.Height / 2))
        mDraw.DrawLine(mPen, CInt(graphicsPictureBox.Width / 2), 0, CInt(graphicsPictureBox.Width / 2), graphicsPictureBox.Height)


        '描繪單一顆劃膠示意圖
        'Call DrawSingleGraphics(gfrmSinglePattern.picSingleGraph, sys, mDrawIdx)
        '選擇哪個線段 那個線段就變成紅色 描繪單一顆知某個RecipeNo StepNo的劃膠示意圖  
        '  Call DrawSingleStepGraphics(lstPatternID.SelectedItem, lstRoundNo.SelectedIndex, dgvStep.SelectedCells(0).RowIndex, gfrmSinglePattern.picSingleGraph, sys)
        '畫膠分布示意圖
        '  Call DrawGraphicsMultiDevice(gfrmOpStatus.picRecipeGraph, gSYS(eSys.Manual), mDrawIdx)
        mDraw.DrawArc(mPen, 500, 200, 100, 100, 90, 270)

        '[說明]繪製坐標系
        'DrawCoord(mDraw, graphicsPictureBox.Width, graphicsPictureBox.Height)

        graphicsPictureBox.Image = mBitmap
        Return True
    End Function


    Private Sub butPIIMap_Click(sender As Object, e As EventArgs) Handles butPIIMap.Click

        '20160603_Toby_Start
        gfrmMapData.OpenFile("d:\\Wafer Map try_1.csv")
        
        no_work_die.Items.Clear()
        getmapfaildie()
        '20160603_Toby_End

        DrawWafer(picDrawGraphicsRecipeGraph)


    End Sub

    Public Function DrawWafer(ByRef graphicsPictureBox As PictureBox) As Boolean
        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim mRedPen As New Drawing.SolidBrush(Color.Red)
        Dim WhitePen As New Drawing.SolidBrush(Color.White)
        Dim mDarkGrayPen As New Drawing.SolidBrush(Color.DarkGray)
        Dim mPointSize As Integer = CInt(graphicsPictureBox.Width / 44.8) '繪點大小
        Dim mScaleX As Integer '換算比率
        Dim mScaleY As Integer '

        mBitmap = graphicsPictureBox.Image

        '   picDrawGraphicsRecipeGraph.Image.Size

        '  If mBitmap Is Nothing Then
        mBitmap = New Bitmap(CInt(graphicsPictureBox.Width), CInt(graphicsPictureBox.Height))
        '        Else
        '            mBitmap.
        '            mBitmap.Height = CInt(picDrawGraphicsRecipeGraph.Height)
        '            mBitmap.Width = CInt(picDrawGraphicsRecipeGraph.Width)
        'End If


        mDraw = Graphics.FromImage(mBitmap)

        '[說明]:畫外框(chuck大小)
        With mPen
            .Width = 3
            .DashStyle = Drawing2D.DashStyle.Solid
        End With
        '填滿
        mDraw.FillRectangle(mBrush, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)
        '畫線
        mDraw.DrawRectangle(mPen, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)

        '[說明]:畫中心線
        With mPen
            .Width = graphicsPictureBox.Width / 500
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.LightGreen
        End With
        mDraw.DrawLine(mPen, 0, CInt(graphicsPictureBox.Height / 2), graphicsPictureBox.Width, CInt(graphicsPictureBox.Height / 2))
        mDraw.DrawLine(mPen, CInt(graphicsPictureBox.Width / 2), 0, CInt(graphicsPictureBox.Width / 2), graphicsPictureBox.Height)

        '[說明]畫點
        ' mDraw.FillEllipse(RedPen, CInt(100), CInt(100), mPointSize, mPointSize)


        mScaleY = graphicsPictureBox.Height / gfrmMapData.Substrates(0).Rows      '實際比例
        mScaleX = graphicsPictureBox.Width / gfrmMapData.Substrates(0).Columns    '實際比例
        '[說明]繪製坐標系
        'DrawCoord(mDraw, graphicsPictureBox.Width, graphicsPictureBox.Height)
        '-----------------------------------------------------------------------------------------------------
        ' Create rectangle.

        Dim D As Die = gfrmMapData.Substrates(0).DieArray(0, 0)
        Dim blackPen As New Pen(Color.Black)
        Dim redPen As New Pen(Color.Red)
        Dim DarkGrayPen As New Pen(Color.DarkGray)
        Dim XDie As Double = gfrmMapData.Substrates(0).Columns - 1
        Dim YDie As Double = gfrmMapData.Substrates(0).Rows - 1

        For i As Integer = 0 To YDie
            For j As Integer = 0 To XDie

                Select Case gfrmMapData.Substrates(0).DieArray(j, i).Bin
                    Case 1, 0
                        mDraw.DrawRectangle(blackPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                    Case -1

                    Case -2, "X" 'Fail bin Toby modify 20161005
                        '填滿
                        mDraw.FillRectangle(mRedPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                        '畫線
                        mDraw.DrawRectangle(blackPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                    Case -3 

                    Case "@" 'Notch Toby modify 20161005
                        '填滿
                        mDraw.FillRectangle(mDarkGrayPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                        '畫線
                        mDraw.DrawRectangle(blackPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)

                End Select
            Next
        Next
        graphicsPictureBox.Image = mBitmap

        Return True
    End Function

    Public Function DrawCycleByRef(graphicsPictureBox As PictureBox) As Boolean

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPen As New Pen(Color.Black)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        Dim mRedPen As New Drawing.SolidBrush(Color.Red)
        Dim WhitePen As New Drawing.SolidBrush(Color.White)
        Dim mPointSize As Integer = CInt(graphicsPictureBox.Width / 44.8) '繪點大小
        Dim mScaleX As Integer '換算比率
        Dim mScaleY As Integer '

        mBitmap = picDrawGraphicsRecipeGraph.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(picDrawGraphicsRecipeGraph.Width), CInt(picDrawGraphicsRecipeGraph.Height))
        End If

        mDraw = Graphics.FromImage(mBitmap)

        '[說明]:畫外框(chuck大小)
        With mPen
            .Width = 3
            .DashStyle = Drawing2D.DashStyle.Solid
        End With
        '填滿
        mDraw.FillRectangle(mBrush, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)
        '畫線
        mDraw.DrawRectangle(mPen, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)

        '[說明]:畫中心線
        With mPen
            .Width = graphicsPictureBox.Width / 500
            .DashStyle = Drawing2D.DashStyle.Solid
            .Color = Color.LightGreen
        End With
        mDraw.DrawLine(mPen, 0, CInt(graphicsPictureBox.Height / 2), graphicsPictureBox.Width, CInt(graphicsPictureBox.Height / 2))
        mDraw.DrawLine(mPen, CInt(graphicsPictureBox.Width / 2), 0, CInt(graphicsPictureBox.Width / 2), graphicsPictureBox.Height)

        '[說明]畫點
        ' mDraw.FillEllipse(RedPen, CInt(100), CInt(100), mPointSize, mPointSize)


        mScaleY = graphicsPictureBox.Height / gfrmMapData.Substrates(0).Rows      '實際比例
        mScaleX = graphicsPictureBox.Width / gfrmMapData.Substrates(0).Columns    '實際比例
        '[說明]繪製坐標系
        'DrawCoord(mDraw, graphicsPictureBox.Width, graphicsPictureBox.Height)
        '-----------------------------------------------------------------------------------------------------
        ' Create rectangle.

        Dim D As Die = gfrmMapData.Substrates(0).DieArray(0, 0)
        Dim blackPen As New Pen(Color.Black)
        Dim redPen As New Pen(Color.Red)
        Dim XDie As Double = gfrmMapData.Substrates(0).Columns - 1
        Dim YDie As Double = gfrmMapData.Substrates(0).Rows - 1

        For i As Integer = 0 To YDie
            For j As Integer = 0 To XDie

                Select Case gfrmMapData.Substrates(0).DieArray(j, i).Bin
                    Case 1
                        'graphicsPictureBox.Print("123456789")
                        mDraw.DrawRectangle(blackPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                    Case -1
                    Case -2, "X"
                        '畫線
                        mDraw.DrawRectangle(redPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                        '填滿
                        mDraw.FillRectangle(mRedPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
                    Case -3 

                End Select

                '  If IsNumeric(gfrmMapData.Substrates(0).DieArray(j, i).Bin) Then
                If gfrmMapData.Substrates(0).DieArray(j, i).Bin <> "X" Or gfrmMapData.Substrates(0).DieArray(j, i).Bin <> "@" Then
                    mDraw.DrawString(gfrmMapData.Substrates(0).DieArray(j, i).Cycle, New Font("Arial", 12), Brushes.Blue, (mScaleX * j), (mScaleY * i))
                End If

            Next
        Next
        graphicsPictureBox.Image = mBitmap

        Return True
    End Function


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CbCycle.CheckedChanged


        If CbCycle.Checked = True Then
            DrawCycleByRef(picDrawGraphicsRecipeGraph)
        ElseIf CbCycle.Checked = False Then
            DrawWafer(picDrawGraphicsRecipeGraph)
        End If

    End Sub

    '    Private _ImagePath As String
    '    Private _ImageWidth As Integer
    '    Private _ImageHeight As Integer
    '
    '    Private _StarPoint As Point = Point.Empty
    '    Private _ViewPoint As Point = Point.Empty
    '    Private bMoveImage As Boolean = False
    '    Public sImagePath As String
    '    Public Sub New()
    '        InitializeComponent()
    '        _ImagePath = sImagePath
    '        _ImageWidth = picDrawGraphicsRecipeGraph.Width
    '        _ImageHeight = picDrawGraphicsRecipeGraph.Height
    '    End Sub
    '
    '    Private Sub frmImageViewer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    '        picDrawGraphicsRecipeGraph.ImageLocation = _ImagePath
    '    End Sub
    '
    '
    '    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar1.Scroll
    '        picDrawGraphicsRecipeGraph.Width = _ImageWidth * (VScrollBar1.Value / 100)
    '        picDrawGraphicsRecipeGraph.Height = _ImageHeight * (VScrollBar1.Value / 100)
    '    End Sub


    '20160603_Toby_Start
    Public Sub picDrawGraphicsRecipeGraph_MouseClick(sender As Object, e As MouseEventArgs) Handles picDrawGraphicsRecipeGraph.MouseClick

        Dim x As Integer
        Dim y As Integer



        Dim mScaleY As Integer = picDrawGraphicsRecipeGraph.Height / gfrmMapData.Substrates(0).Rows      '實際比例
        Dim mScaleX As Integer = picDrawGraphicsRecipeGraph.Width / gfrmMapData.Substrates(0).Columns    '實際比例

        x = e.X \ mScaleX '利用點擊位置再除以die size得到die 座標
        y = e.Y \ mScaleY


        If (gfrmMapData.Substrates(0).DieArray(x, y).Bin = "1") Then
            gfrmMapData.Substrates(0).DieArray(x, y).Bin = "X"
            no_work_die.Items.Add("(" + CStr(x + 1) + "," + CStr(y + 1) + ")")

            changeColor(picDrawGraphicsRecipeGraph, x, y, "Red", mScaleX, mScaleY)
        Else
            If (gfrmMapData.Substrates(0).DieArray(x, y).Bin = "-2" Or gfrmMapData.Substrates(0).DieArray(x, y).Bin = "X") Then
                gfrmMapData.Substrates(0).DieArray(x, y).Bin = "1"

                no_work_die.Items.Remove("(" + CStr(x + 1) + "," + CStr(y + 1) + ")")
                changeColor(picDrawGraphicsRecipeGraph, x, y, "Grey", mScaleX, mScaleY)

            End If
        End If

    

        '20160603_Toby_End
    End Sub

    Private Sub picDrawGraphicsRecipeGraph_MouseUp(sender As Object, e As MouseEventArgs) Handles picDrawGraphicsRecipeGraph.MouseUp
        '        bMoveImage = False
    End Sub

    Public Sub GraphicsBigger()  '放大

        Dim w As Integer = picDrawGraphicsRecipeGraph.Width * 1.2             '每次放大 20%
        Dim h As Integer = picDrawGraphicsRecipeGraph.Height * 1.2

        picDrawGraphicsRecipeGraph.Size = Size.Ceiling(New SizeF(w, h))
    End Sub
    Public Sub GraphicsSmaller()  '縮小

        Dim w As Integer = picDrawGraphicsRecipeGraph.Width * 0.8             '每次放大 20%
        Dim h As Integer = picDrawGraphicsRecipeGraph.Height * 0.8

        picDrawGraphicsRecipeGraph.Size = Size.Ceiling(New SizeF(w, h))
    End Sub

    '     private void 縮小ToolStripMenuItem_Click(object sender, EventArgs e)
    '     {
    '     float w = this.pictureBox1.Width * 0.8f; //每次縮小 20%
    '     float h = this.pictureBox1.Height * 0.8f;
    '     this.pictureBox1.Size = Size.Ceiling(new SizeF(w, h));
    '     }
    '
    '     private void 原始大小ToolStripMenuItem_Click(object sender, EventArgs e)
    '     {
    '     this.pictureBox1.Size = this.pictureBox1.Image.Size;
    '     }


    Private Sub picDrawGraphicsRecipeGraph_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles picDrawGraphicsRecipeGraph.MouseDoubleClick
        '  GraphicsBigger()
        panWaferMap.AutoScroll = False

        gfrmMapData.OpenFile("d:\\Wafer Map try.csv")
        gfrmMapData.MatrixCut(10)
        gfrmMapData.MergeLogData("d:\\Wafer Map try.csv")
        DrawWafer(picDrawGraphicsRecipeGraph)
        panWaferMap.AutoScroll = True


    End Sub
    'Public Function MoveScroll(e As MouseEventArgs)
    '    Dim X As Integer = e.X
    '    Dim Y As Integer = e.Y
    '    Dim panSizeX As Integer = panWaferMap.Width
    '    Dim panSizeY As Integer = panWaferMap.Height
    '    Dim MoveX As Integer = e.X - (panWaferMap.Width / 2)
    '    Dim MoveY As Integer = e.Y - (panWaferMap.Height / 2)
    '    Dim Scroll As Point = panWaferMap.AutoScrollPosition
    '    Dim ScrollTest As New Point
    '    ScrollTest.X = -(e.X - (panWaferMap.Width / 2) + (e.X - (panWaferMap.Width / 2)) / panWaferMap.Width * 17)
    '    ScrollTest.Y = -(e.Y - (panWaferMap.Height / 2) + (e.X - (panWaferMap.Width / 2)) / panWaferMap.Width * 17)
    '    '    Dim w As Integer = picDrawGraphicsRecipeGraph.Width * 1.2             '每次放大 20%
    '    '   Dim h As Integer = picDrawGraphicsRecipeGraph.Height * 1.2
    '    ' panWaferMap.AutoScrollPosition = ScrollTest
    '    picDrawGraphicsRecipeGraph.Location = ScrollTest


    '    'panWaferMap.AutoScrollPosition = New Point(ScrollTest.X, ScrollTest.Y)
    '    'panWaferMap.SetBounds(ScrollTest.X, ScrollTest.Y, picDrawGraphicsRecipeGraph.Width, picDrawGraphicsRecipeGraph.Height)
    '    'panWaferMap.scro()
    '    'panWaferMap.Scro()
    '    'panWaferMap.AutoScrollOffset
    '    'panWaferMap.AutoScrollPosition
    '    'picDrawGraphicsRecipeGraph.Width
    '    'picDrawGraphicsRecipeGraph.Height


    'End Function

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        GraphicsSmaller()

        gfrmMapData.OpenFile("d:\\Wafer Map try.csv")
        gfrmMapData.MatrixCut(10)
        gfrmMapData.MergeLogData("d:\\Wafer Map try.csv")

        DrawWafer(picDrawGraphicsRecipeGraph)
    End Sub
    '20160603_Toby_Start
    '取消Timer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerDrawGraphics.Tick
        Dim Scroll As Point = panWaferMap.AutoScrollPosition
        
        '20160603_Toby_End
    End Sub

    Private Sub butBigger_Click(sender As Object, e As EventArgs) Handles butBigger.Click
        Dim w As Integer = picDrawGraphicsRecipeGraph.Width * 1.2             '每次放大 20%
        Dim h As Integer = picDrawGraphicsRecipeGraph.Height * 1.2

        picDrawGraphicsRecipeGraph.Size = Size.Ceiling(New SizeF(w, h))

        '20160603_Toby_Start

        DrawWafer(picDrawGraphicsRecipeGraph)

        If CbCycle.Checked = True Then
            DrawCycleByRef(picDrawGraphicsRecipeGraph)
        ElseIf CbCycle.Checked = False Then
            DrawWafer(picDrawGraphicsRecipeGraph)
        End If

        '20160603_Toby_End
    End Sub

    Private Sub butSmaller_Click(sender As Object, e As EventArgs) Handles butSmaller.Click
        Dim w As Integer = picDrawGraphicsRecipeGraph.Width * 0.8             '每次縮小 20%
        Dim h As Integer = picDrawGraphicsRecipeGraph.Height * 0.8

        picDrawGraphicsRecipeGraph.Size = Size.Ceiling(New SizeF(w, h))

        '20160603_Toby_Start
        
        DrawWafer(picDrawGraphicsRecipeGraph)

        If CbCycle.Checked = True Then
            DrawCycleByRef(picDrawGraphicsRecipeGraph)
        ElseIf CbCycle.Checked = False Then
            DrawWafer(picDrawGraphicsRecipeGraph)
        End If

        '20160603_Toby_End
    End Sub

    '20160603_Toby_Start
    Private Sub no_work_load_Click(sender As Object, e As EventArgs) Handles no_work_load.Click
        'Dim X, Y As Integer
        Dim readfile, dieLocate As String


        With LoadNoWorkDie
            .InitialDirectory = "D:\"        '取得或設定檔案對話方塊所顯示的初始目錄。
            .Filter = "txt files (*.txt)|*.txt" '取得或設定目前的檔名篩選字串，以決定出現在對話方塊中 [另存檔案類型] 或 [檔案類型] 方塊的選項。
            .FilterIndex = 2                 '取得或設定檔案對話方塊中目前所選取之篩選條件的索引。
            .RestoreDirectory = True         '取得或設定值，指出對話方塊是否在關閉前將目錄還原至先前選取的目錄。

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            Dim sz As String = .FileName

            '  If System.IO.Directory.Exists(.FileName) Then        '檢查目錄是否存在
            If My.Computer.FileSystem.FileExists(.FileName) Then  '檢查檔案是否存在

                Dim objReader As New System.IO.StreamReader(.FileName)

                Do While objReader.Peek() <> -1
                    readfile = objReader.ReadLine()
                    dieLocate = readfile
                    dieLocate = dieLocate.Substring(1, dieLocate.Length - 2)
                    Dim result As String() = Split(dieLocate, ",")

                    If (gfrmMapData.Substrates(0).DieArray(result(0) - 1, result(1) - 1).Bin = "1") Then
                        gfrmMapData.Substrates(0).DieArray(result(0) - 1, result(1) - 1).Bin = "X"
                        no_work_die.Items.Add(readfile)
                    End If

                Loop


            End If

        End With


        If CbCycle.Checked = True Then
            DrawCycleByRef(picDrawGraphicsRecipeGraph)
        ElseIf CbCycle.Checked = False Then
            DrawWafer(picDrawGraphicsRecipeGraph)
        End If

        '20160603_Toby_End
    End Sub
    '20160603_Toby_Start
    Public Sub getmapfaildie() '得到初始PII Map no work die

        Dim XDie As Integer = gfrmMapData.Substrates(0).Columns - 1
        Dim YDie As Integer = gfrmMapData.Substrates(0).Rows - 1

        For i As Integer = 0 To YDie
            For j As Integer = 0 To XDie
                If gfrmMapData.Substrates(0).DieArray(j, i).Bin = "X" Then
                    no_work_die.Items.Add("(" + CStr(j + 1) + "," + CStr(i + 1) + ")")
                End If

            Next
        Next
        '20160603_Toby_End
    End Sub
    '20160603_Toby_Start
    Private Sub no_work_save_Click(sender As Object, e As EventArgs) Handles no_work_save.Click
        Dim XDie As Double = gfrmMapData.Substrates(0).Columns - 1
        Dim YDie As Double = gfrmMapData.Substrates(0).Rows - 1

        Dim DieArray(1024) As String
        Dim count As Integer = 0
        Dim sw As System.IO.StreamWriter

        For i As Integer = 0 To YDie
            For j As Integer = 0 To XDie

                If gfrmMapData.Substrates(0).DieArray(j, i).Bin = "1" Then
                    If gfrmMapData.Substrates(0).DieArray(j, i).Cycle = "-2" Then
                        DieArray(count) = CStr(count + 1) + ":(" + CStr(j + 1) + "," + CStr(i + 1) + ")_1"
                    Else
                        DieArray(count) = CStr(count + 1) + ":(" + CStr(j + 1) + "," + CStr(i + 1) + ")_" + CStr(gfrmMapData.Substrates(0).DieArray(j, i).Cycle)
                    End If
                    count = count + 1
                End If

                If gfrmMapData.Substrates(0).DieArray(j, i).Bin = "-2" Then
                    gfrmMapData.Substrates(0).DieArray(j, i).Cycle = "0"
                End If


            Next
        Next

        Dim fileName As String = "d:\Premtek\PIIMap\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00") & "_" & gfrmMapData.Information.LotID & ".txt"
        Dim fileName1 As String = "d:\Premtek\PIIMap\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00") & "_" & gfrmMapData.Information.LotID & ".csv"
      
        '檔案存在
        If System.IO.File.Exists(fileName) Then
            Dim fInfo As New System.IO.FileInfo(fileName)
            sw = New IO.StreamWriter(fileName, True)
            For i = 0 To count

                sw.WriteLine(DieArray(i))

            Next

            sw.Close()

        Else
            '檔案不存在,另開新檔
            sw = New IO.StreamWriter(fileName, False)
            For i = 0 To count

                sw.WriteLine(DieArray(i))

            Next

            sw.Close()

        End If

        'output csv 格式
        gfrmMapData.OutputPiiMap(fileName1)
        '20160603_Toby_End
    End Sub

    '20160603_Toby_Start 針對單顆die修改顏色,更新Bitmap
    Public Sub changeColor(graphicsPictureBox As PictureBox, x As Integer, y As Integer, Color As String, W As Integer, H As Integer) '得到初始PII Map no work die

        Dim mBitmap As Bitmap
        Dim mDraw As Graphics
        Dim mPointSize As Integer = CInt(W / 44.8) '繪點大小       


        mBitmap = graphicsPictureBox.Image

        If mBitmap Is Nothing Then
            mBitmap = New Bitmap(CInt(graphicsPictureBox.Width), CInt(graphicsPictureBox.Height))
        End If
        mDraw = Graphics.FromImage(mBitmap)

        Select Case Color
            Case "Red"
                mDraw.FillRectangle(New Drawing.SolidBrush(Drawing.Color.Red), W * x, H * y, W, H)
                mDraw.DrawRectangle(New Pen(Brushes.Black), W * x, H * y, W, H)
            Case "Green"
                mDraw.FillRectangle(New Drawing.SolidBrush(Drawing.Color.Green), W * x, H * y, W, H)
                mDraw.DrawRectangle(New Pen(Brushes.Black), W * x, H * y, W, H)
            Case "Yellow"
                mDraw.FillRectangle(New Drawing.SolidBrush(Drawing.Color.Yellow), W * x, H * y, W, H)
                mDraw.DrawRectangle(New Pen(Brushes.Black), W * x, H * y, W, H)
            Case "White"
                mDraw.FillRectangle(New Drawing.SolidBrush(Drawing.Color.White), W * x, H * y, W, H)
                mDraw.DrawRectangle(New Pen(Brushes.Black), W * x, H * y, W, H)
            Case "Grey"
                mDraw.FillRectangle(New Drawing.SolidBrush(Drawing.Color.LightGray), W * x, H * y, W, H)
                mDraw.DrawRectangle(New Pen(Brushes.Black), W * x, H * y, W, H)
            Case Else
                mDraw.FillRectangle(New Drawing.SolidBrush(Drawing.Color.Gray), W * x, H * y, W, H)
                mDraw.DrawRectangle(New Pen(Brushes.Black), W * x, H * y, W, H)
        End Select

        graphicsPictureBox.Image = mBitmap
        '20160603_Toby_End
    End Sub

   
   
    Private Sub frmDrawGraphics_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmDrawGraphics_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
End Class
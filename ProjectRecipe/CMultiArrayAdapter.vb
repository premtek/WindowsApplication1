
''' <summary>多層陣列配接</summary>
''' <remarks></remarks>
Public Class CMultiArrayAdapter

    Dim mMapOffsetX(,) As Decimal
    Dim mMapOffsetY(,) As Decimal
    Dim mCountX As Integer = 1 '預設記憶體空間為1
    Dim mCountY As Integer = 1

    '20170606 Toby add
    Public Size_X As Decimal
    Public Size_Y As Decimal



    Sub FindMinPitchXY(ByRef pitchX As Decimal, ByRef pitchY As Decimal)
        Dim mPitchX As Decimal
        Dim mPitchY As Decimal
        Dim mMinX As Decimal = Decimal.MaxValue
        Dim mMinY As Decimal = Decimal.MaxValue

        For mCol As Integer = 0 To mCountX - 2
            For mRow As Integer = 0 To mCountY - 1
                mPitchX = Math.Abs(mMapOffsetX(mCol, mRow) - mMapOffsetX(mCol + 1, mRow)) '繪製以最小值為基準
                mPitchY = Math.Abs(mMapOffsetY(mCol, mRow) - mMapOffsetY(mCol + 1, mRow)) '繪製以最小值為基準
                If mPitchX <> 0 AndAlso mPitchX < mMinX Then mMinX = mPitchX
                If mPitchY <> 0 AndAlso mPitchY < mMinY Then mMinY = mPitchY
            Next
        Next
        For mRow As Integer = 0 To mCountY - 2
            For mCol As Integer = 0 To mCountX - 1
                mPitchX = Math.Abs(mMapOffsetX(mCol, mRow) - mMapOffsetX(mCol, mRow + 1)) '繪製以最小值為基準
                mPitchY = Math.Abs(mMapOffsetY(mCol, mRow) - mMapOffsetY(mCol, mRow + 1)) '繪製以最小值為基準
                If mPitchX <> 0 AndAlso mPitchX < mMinX Then mMinX = mPitchX
                If mPitchY <> 0 AndAlso mPitchY < mMinY Then mMinY = mPitchY
            Next
        Next
        If mMinX = Decimal.MaxValue Then
            mMinX = 0
        End If

        If mMinY = Decimal.MaxValue Then
            mMinY = 0
        End If

        pitchX = mMinX
        pitchY = mMinY
    End Sub

    ''' <summary>繪圖到PictureBox</summary>
    ''' <param name="pic"></param>
    ''' <remarks></remarks>
    Public Sub Draw(ByRef pic As PictureBox)
        Dim mBitmap As New Bitmap(CInt(pic.Width), CInt(pic.Height))
        Dim mDraw As Graphics = Graphics.FromImage(mBitmap)
        Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
        '填滿背景色
        mDraw.FillRectangle(mBrush, 0, 0, CInt(pic.Width) - 1, CInt(pic.Height) - 1)

        Dim maxX As Decimal
        Dim maxY As Decimal
        Dim minX As Decimal
        Dim minY As Decimal
        If GetMaxMinMapOffset(maxX, maxY, minX, minY) = False Then '取得數值範圍最大最小值
            pic.Image = mBitmap
            Exit Sub
        End If

        '顯示比例計算 (pixel/mm)
        Dim scaleX As Decimal
        Dim scaleY As Decimal
        Dim Boarder As Decimal = 10 '繪圖邊緣
        Dim mDrawWidth As Decimal = 10 '方塊大小(mm)
        Dim mDrawHeight As Decimal = 10 '方塊大小(mm)
        Dim mPitchX As Decimal '間距
        Dim mPitchY As Decimal
        FindMinPitchXY(mPitchX, mPitchY) '找元件間的間距 

        '=== 顯示比例計算 ===
        If maxX - minX + mPitchX = 0 Then
            scaleX = 1
        Else
            scaleX = (pic.Width - 2 * Boarder) / (maxX - minX + mPitchX)
        End If
        If Math.Round(scaleX, 3) = 0 Then scaleX = 0.001 '比例過小保護

        If maxY - minY + mPitchY = 0 Then
            scaleY = 1
        Else
            scaleY = (pic.Height - 2 * Boarder) / (maxY - minY + mPitchY)
        End If
        If Math.Round(scaleY, 3) = 0 Then scaleY = 0.001
        '=== 顯示比例計算 ===

        '=== 計算繪圖方塊大小 ===
        If mPitchX = 0 Then '該方向沒有元件, 使用預設值
            mDrawWidth = 10
        ElseIf 1 / scaleX < mPitchX * 0.1 Then '顯示比例處理
            mDrawWidth = mPitchX * 0.9
        ElseIf mPitchX > 1 / scaleX Then
            mDrawWidth = mPitchX - 1 / scaleX '過密時採用1Pixel當間距
        Else
            mDrawWidth = 1 / scaleX
        End If
        
        If mPitchY = 0 Then
            mDrawHeight = 10
        ElseIf 1 / scaleY < mPitchY * 0.1 Then
            mDrawHeight = mPitchY * 0.9
        ElseIf mPitchY > 1 / scaleY Then
            mDrawHeight = mPitchY - 1 / scaleY
        Else
            mDrawHeight = 1 / scaleY
        End If

        '=== 計算繪圖方塊大小 ===

        '=== 位置對Pixe轉換公式 ===
        mDraw.Transform = New Drawing2D.Matrix(scaleX, 0, 0, -scaleY, Boarder, mBitmap.Height - Boarder)

        Dim mDrawPosX As Decimal
        Dim mDrawPosY As Decimal
        mBrush.Dispose()
        mBrush = Nothing
        mBrush = New SolidBrush(Color.White)
        For mCol As Integer = 0 To mCountX - 1
            For mRow As Integer = 0 To mCountY - 1
                mDrawPosX = mMapOffsetX(mCol, mRow) - minX '繪製以最小值為基準
                mDrawPosY = mMapOffsetY(mCol, mRow) - minY
                mDraw.FillRectangle(mBrush, mDrawPosX, mDrawPosY, mDrawWidth, mDrawHeight)
            Next
        Next

        mBrush.Dispose()
        mBrush = Nothing
        pic.Image = mBitmap
    End Sub

    ''' <summary>繪圖到PictureBox</summary>
    ''' <param name="stagenumber"></param>
    ''' <param name="Nodename"></param>
    ''' <param name="pixel_startX"></param>
    ''' <param name="pixel_EndX"></param>
    ''' <param name="pixel_startY"></param>
    ''' <param name="pixel_EndY"></param>
    ''' <remarks></remarks>
    Public Sub Draw(ByRef MAPINFO As CMapInfo, ByVal stagenumber As Integer, ByVal Nodename As String, ByVal pixel_startX As Integer, ByVal pixel_EndX As Integer, ByVal pixel_startY As Integer, ByVal pixel_EndY As Integer)


        Dim mBitmap As Bitmap
        Dim PosInfo As CNodeInfo
        mBitmap = New Bitmap(800, 700)

        Dim maxX As Decimal
        Dim maxY As Decimal
        Dim minX As Decimal
        Dim minY As Decimal
        If GetMaxMinMapOffset(maxX, maxY, minX, minY) = False Then '取得數值範圍最大最小值
            Exit Sub
        End If

        '顯示比例計算 (pixel/mm)
        Dim scaleX As Decimal
        Dim scaleY As Decimal
        Dim Boarder As Decimal = 1 '繪圖邊緣
        Dim mDrawWidth As Decimal = 10 '方塊大小(mm)
        Dim mDrawHeight As Decimal = 10 '方塊大小(mm)
        Dim mPitchX As Decimal '間距
        Dim mPitchY As Decimal
        FindMinPitchXY(mPitchX, mPitchY) '找元件間的間距 


        '=== 顯示比例計算 ===
        If maxX - minX + mPitchX = 0 Then
            scaleX = 1
        Else
            scaleX = ((pixel_EndX - pixel_startX + 1) - 2 * Boarder) / (maxX - minX + mPitchX)
        End If
        If Math.Round(scaleX, 3) = 0 Then scaleX = 0.001 '比例過小保護

        If maxY - minY + mPitchY = 0 Then
            scaleY = 1
        Else
            scaleY = ((pixel_EndY - pixel_startY + 1) - 2 * Boarder) / (maxY - minY + mPitchY)
        End If
        If Math.Round(scaleY, 3) = 0 Then scaleY = 0.001
        '=== 顯示比例計算 ===

        '=== 計算繪圖方塊大小 ===
        If mPitchX = 0 Then '該方向沒有元件, 使用預設值
            mDrawWidth = 10
        ElseIf 1 / scaleX < mPitchX * 0.1 Then '顯示比例處理
            mDrawWidth = mPitchX * 0.9
        Else
            mDrawWidth = mPitchX - 1 / scaleX '過密時採用1Pixel當間距
        End If

        If mPitchY = 0 Then
            mDrawHeight = 10
        ElseIf 1 / scaleY < mPitchY * 0.1 Then
            mDrawHeight = mPitchY * 0.9
        Else
            mDrawHeight = mPitchY - 1 / scaleY
        End If
        '=== 計算繪圖方塊大小 ===

        '=== 位置對Pixe轉換公式 ===
        'mDraw.Transform = New Drawing2D.Matrix(scaleX, 0, 0, -scaleY, Boarder, 200 - Boarder)


        Dim mDrawPosX As Decimal
        Dim mDrawPosY As Decimal
        Dim max_mMapOffsetX As Decimal = Decimal.MinValue
        Dim max_mMapOffsetY As Decimal = Decimal.MinValue
        Dim min_mMapOffsetX As Decimal = Decimal.MaxValue
        Dim min_mMapOffsetY As Decimal = Decimal.MaxValue
        Dim temp_mMapOffsetX, temp_mMapOffsetY As Decimal

        For mCol As Integer = 0 To mCountX - 1
            For mRow As Integer = 0 To mCountY - 1
                temp_mMapOffsetX = Math.Abs(mMapOffsetX(mCol, mRow))  '繪製以最小值為基準
                temp_mMapOffsetY = Math.Abs(mMapOffsetY(mCol, mRow))
                If temp_mMapOffsetX > max_mMapOffsetX Then
                    max_mMapOffsetX = temp_mMapOffsetX
                End If
                If temp_mMapOffsetY > max_mMapOffsetY Then
                    max_mMapOffsetY = temp_mMapOffsetY
                End If
            Next
        Next

        For mCol As Integer = 0 To mCountX - 1
            For mRow As Integer = 0 To mCountY - 1
                temp_mMapOffsetX = mMapOffsetX(mCol, mRow)  '繪製以最小值為基準
                temp_mMapOffsetY = mMapOffsetY(mCol, mRow)
                If temp_mMapOffsetX < min_mMapOffsetX Then
                    min_mMapOffsetX = temp_mMapOffsetX
                End If
                If temp_mMapOffsetY < min_mMapOffsetY Then
                    min_mMapOffsetY = temp_mMapOffsetY
                End If
            Next
        Next


        If max_mMapOffsetX = 0 Then
            max_mMapOffsetX = 100
        End If
        If max_mMapOffsetY = 0 Then
            max_mMapOffsetY = 100
        End If


        Dim pitch_X As Decimal
        Dim pitch_Y As Decimal
        pitch_X = (pixel_EndX - pixel_startX + 1) / (max_mMapOffsetX + mPitchX + 1) '邊框各留0.5
        pitch_Y = (pixel_EndY - pixel_startY + 1) / (max_mMapOffsetY + mPitchY + 1) '邊框各留0.5





        If min_mMapOffsetX < 0 And min_mMapOffsetY < 0 Then

            For mCol As Integer = mCountX - 1 To 0 Step -1
                For mRow As Integer = 0 To mCountY - 1
                    mDrawPosX = ((Math.Abs(mMapOffsetX(mCol, mRow) - minX) + 0.5) * pitch_X) + pixel_startX
                    mDrawPosY = ((Math.Abs(mMapOffsetY(mCol, mRow) - maxY) + 0.5) * pitch_Y) + pixel_startY

                    If stagenumber < 2 Then

                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY - (mDrawHeight * pitch_Y), mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY) '' 座標顛倒(測試)

                        MAPINFO.gMotorPos_L.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    Else
                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY, mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY + (mDrawHeight * pitch_Y))
                        MAPINFO.gMotorPos_R.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    End If
                Next
            Next

        ElseIf min_mMapOffsetX < 0 And min_mMapOffsetY >= 0 Then
            For mCol As Integer = mCountX - 1 To 0 Step -1
                For mRow As Integer = mCountY - 1 To 0 Step -1
                    mDrawPosX = ((Math.Abs(mMapOffsetX(mCol, mRow) - minX) + 0.5) * pitch_X) + pixel_startX
                    mDrawPosY = ((Math.Abs(mMapOffsetY(mCol, mRow) - maxY) + 0.5) * pitch_Y) + pixel_startY

                    If stagenumber < 2 Then

                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY - (mDrawHeight * pitch_Y), mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY) '' 座標顛倒(測試)

                        MAPINFO.gMotorPos_L.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    Else
                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY, mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY + (mDrawHeight * pitch_Y))
                        MAPINFO.gMotorPos_R.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    End If
                Next
            Next

        ElseIf min_mMapOffsetX >= 0 And min_mMapOffsetY < 0 Then
            For mCol As Integer = 0 To mCountX - 1
                For mRow As Integer = 0 To mCountY - 1
                    mDrawPosX = ((Math.Abs(mMapOffsetX(mCol, mRow) - minX) + 0.5) * pitch_X) + pixel_startX
                    mDrawPosY = ((Math.Abs(mMapOffsetY(mCol, mRow) - maxY) + 0.5) * pitch_Y) + pixel_startY

                    If stagenumber < 2 Then

                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY - (mDrawHeight * pitch_Y), mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY) '' 座標顛倒(測試)

                        MAPINFO.gMotorPos_L.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    Else
                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY, mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY + (mDrawHeight * pitch_Y))
                        MAPINFO.gMotorPos_R.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    End If
                Next
            Next

        ElseIf min_mMapOffsetX >= 0 And min_mMapOffsetY >= 0 Then
            For mCol As Integer = 0 To mCountX - 1
                For mRow As Integer = mCountY - 1 To 0 Step -1
                    mDrawPosX = ((Math.Abs(mMapOffsetX(mCol, mRow) - minX) + 0.5) * pitch_X) + pixel_startX
                    mDrawPosY = ((Math.Abs(mMapOffsetY(mCol, mRow) - maxY) + 0.5) * pitch_Y) + pixel_startY

                    If stagenumber < 2 Then

                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY - (mDrawHeight * pitch_Y), mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY) '' 座標顛倒(測試)

                        MAPINFO.gMotorPos_L.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    Else
                        PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY, mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY + (mDrawHeight * pitch_Y))
                        MAPINFO.gMotorPos_R.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

                    End If
                Next
            Next

        End If

        'For mCol As Integer = 0 To mCountX - 1
        '    For mRow As Integer = 0 To mCountY - 1
        '        mDrawPosX = ((Math.Abs(mMapOffsetX(mCol, mRow)) - minX + 0.5) * pitch_X) + pixel_startX
        '        mDrawPosY = ((Math.Abs(mMapOffsetY(mCol, mRow)) - minY + 0.5) * pitch_Y) + pixel_startY

        '        If stagenumber < 2 Then

        '            PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY - (mDrawHeight * pitch_Y), mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY) '' 座標顛倒(測試)

        '            MAPINFO.gMotorPos_L.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

        '        Else
        '            PosInfo = New CNodeInfo(mDrawPosX, mDrawPosY, mDrawPosX + (mDrawWidth * pitch_X), mDrawPosY + (mDrawHeight * pitch_Y))
        '            MAPINFO.gMotorPos_R.Add(stagenumber.ToString & "|" & Nodename & "|" & mCol.ToString & "|" & mRow.ToString, PosInfo)

        '        End If
        '    Next
        'Next

    End Sub

    ''' <summary>
    ''' 外部多層資料接收
    ''' </summary>
    ''' <remarks></remarks>
    Dim mArray As List(Of CRecipeNodeLevel)

    Public Sub New(ByRef array As List(Of CRecipeNodeLevel))

        mArray = array
        For mLevel As Integer = 0 To array.Count - 1
            Select Case array(mLevel).LevelType
                Case eLevelType.Array
                    If array(mLevel).Array.CountX = 0 Then array(mLevel).Array.CountX = 1
                    mCountX = mCountX * array(mLevel).Array.CountX
                    If array(mLevel).Array.CountY = 0 Then array(mLevel).Array.CountY = 1
                    mCountY = mCountY * array(mLevel).Array.CountY '計算總數
                Case eLevelType.NoneArray
                    mCountX = mCountX * array(mLevel).NonArray.Count
                    mCountY = mCountY 'Y方向不累增
            End Select

        Next

        ReDim mMapOffsetX(mCountX - 1, mCountY - 1)
        ReDim mMapOffsetY(mCountX - 1, mCountY - 1)
       
        Dim Xo As Integer = 0
        Dim Yo As Integer = 0
       
        For mCol As Integer = 0 To mCountX - 1
            For mRow As Integer = 0 To mCountY - 1
                '參考日 時 分 秒 的計算方式
                Dim mXi(array.Count - 1) As Integer '各層索引X
                Dim mYi(array.Count - 1) As Integer '各層索引Y
                Dim mResultX(array.Count - 1) As Integer '餘數X
                Dim mResultY(array.Count - 1) As Integer '餘數Y
                For mLevel As Integer = array.Count - 1 To 0 Step -1 '從最大區塊開始
                    If mLevel = array.Count - 1 Then '如果是最大區塊
                        If mLevel = 0 Then
                            If array.Count = 1 Then '如果只有一層, 則陣列索引與記憶體索引相同
                                mXi(mLevel) = mCol
                                mYi(mLevel) = mRow
                            Else '如果不止一層, 則最內部一層的索引為　前一層的餘數
                                mXi(mLevel) = mResultX(mLevel + 1)
                                mYi(mLevel) = mResultY(mLevel + 1)
                            End If
                            
                        Else
                            mXi(mLevel) = Math.DivRem(mCol, GetLevelCountX(mLevel - 1), mResultX(mLevel)) '用索引直接除
                            mYi(mLevel) = Math.DivRem(mRow, GetLevelCountY(mLevel - 1), mResultY(mLevel))
                        End If
                        
                    Else '如果不是最大區塊, 用大區塊的餘數來除
                        If mLevel = 0 Then
                            If array.Count = 1 Then '如果只有一層, 則陣列索引與記憶體索引相同
                                mXi(mLevel) = mCol
                                mYi(mLevel) = mRow
                            Else '如果不止一層, 則最內部一層的索引為　前一層的餘數
                                mXi(mLevel) = mResultX(mLevel + 1)
                                mYi(mLevel) = mResultY(mLevel + 1)
                            End If
                        Else
                            mXi(mLevel) = Math.DivRem(mResultX(mLevel + 1), GetLevelCountX(mLevel - 1), mResultX(mLevel))
                            mYi(mLevel) = Math.DivRem(mResultY(mLevel + 1), GetLevelCountY(mLevel - 1), mResultY(mLevel))
                        End If
                        
                    End If
                Next
                Dim Xp As Decimal = 0
                Dim Yp As Decimal = 0
                If GetRelPos(mXi, mYi, Xp, Yp) Then '取得位置成功 
                    mMapOffsetX(mCol, mRow) = Xp '套用
                    mMapOffsetY(mCol, mRow) = Yp
                End If

            Next
        Next

    End Sub

    ''' <summary>取得MapOffset的最大最小值</summary>
    ''' <param name="maxX"></param>
    ''' <param name="maxY"></param>
    ''' <param name="minX"></param>
    ''' <param name="minY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMaxMinMapOffset(ByRef maxX As Decimal, ByRef maxY As Decimal, ByRef minX As Decimal, ByRef minY As Decimal) As Boolean
        Dim mMaxX As Decimal = Decimal.MinValue
        Dim mMaxY As Decimal = Decimal.MinValue
        Dim mMinX As Decimal = Decimal.MaxValue
        Dim mMinY As Decimal = Decimal.MaxValue
        For mCol As Integer = 0 To mCountX - 1
            For mRow As Integer = 0 To mCountY - 1
                If mMapOffsetX(mCol, mRow) > mMaxX Then
                    mMaxX = mMapOffsetX(mCol, mRow)
                End If
                If mMapOffsetX(mCol, mRow) < mMinX Then
                    mMinX = mMapOffsetX(mCol, mRow)
                End If
                If mMapOffsetY(mCol, mRow) > mMaxY Then
                    mMaxY = mMapOffsetY(mCol, mRow)
                End If
                If mMapOffsetY(mCol, mRow) < mMinY Then
                    mMinY = mMapOffsetY(mCol, mRow)
                End If
            Next
        Next
        maxX = mMaxX
        maxY = mMaxY
        minX = mMinX
        minY = mMinY

        '--- Soni + 2017.01.24 X值找不到參考例外 [S] ---
        If mMaxX = Decimal.MinValue Then
            Return False
        End If
        If mMinX = Decimal.MaxValue Then
            Return False
        End If
        '--- Soni + 2017.01.24 X值找不到參考例外 [E] ---

        '--- Soni + 2017.01.24 Y值找不到參考例外 [S] ---
        If mMaxY = Decimal.MinValue Then
            Return False
        End If
        If mMinY = Decimal.MaxValue Then
            Return False
        End If
        '--- Soni + 2017.01.24 Y值找不到參考例外 [E] ---
        Return True
    End Function
    ''' <summary>
    ''' 給定各層索引, 取得相對位置
    ''' </summary>
    ''' <param name="Xi">各層索引X</param>
    ''' <param name="Yi">各層索引Y</param>
    ''' <param name="Xp">相對位置X</param>
    ''' <param name="Yp">相對位置Y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRelPos(ByVal Xi() As Integer, Yi() As Integer, ByRef Xp As Decimal, ByRef Yp As Decimal) As Boolean
        Xp = 0
        Yp = 0
        For mLevel As Integer = 0 To Xi.Count - 1 '將每一層相對位置累加
            Select Case mArray(mLevel).LevelType
                Case eLevelType.Array
                    Dim mPitchAX As Decimal
                    Dim mPitchAY As Decimal
                    Dim mPitchBX As Decimal
                    Dim mPitchBY As Decimal
                    ProjectCore.CMath.Rotation(mArray(mLevel).Array.PitchX, 0, mArray(mLevel).Array.Theta, mPitchAX, mPitchAY)
                    ProjectCore.CMath.Rotation(0, mArray(mLevel).Array.PitchY, mArray(mLevel).Array.Theta, mPitchBX, mPitchBY)
                    Xp += Xi(mLevel) * mPitchAX + Yi(mLevel) * mPitchBX
                    Yp += Xi(mLevel) * mPitchAY + Yi(mLevel) * mPitchBY
                    'Xp += Xi(mLevel) * mArray(mLevel).Array.PitchX
                    'Yp += Yi(mLevel) * mArray(mLevel).Array.PitchY
                Case eLevelType.NoneArray
                    Xp += mArray(mLevel).NonArray(Xi(mLevel)).RelPosX
                    Yp += mArray(mLevel).NonArray(Xi(mLevel)).RelPosY
            End Select
        Next
        Return True
    End Function

    ''' <summary>
    ''' 取得該層除數X
    ''' </summary>
    ''' <param name="level"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLevelCountX(ByVal level As Integer) As Integer
        Select Case mArray(level).LevelType
            Case eLevelType.Array
                If level = 0 Then '最小區塊
                    Return mArray(level).Array.CountX
                Else
                    Return mArray(level).Array.CountX * GetLevelCountX(level - 1)
                End If
            Case eLevelType.NoneArray
                If level = 0 Then
                    Return mArray(level).NonArray.Count
                Else
                    Return mArray(level).NonArray.Count * GetLevelCountX(level - 1)
                End If
        End Select
        Return 1
    End Function

    ''' <summary>取得該層除數Y</summary>
    ''' <param name="level"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLevelCountY(ByVal level As Integer) As Integer
        Select Case mArray(level).LevelType
            Case eLevelType.Array
                If level = 0 Then '最小區塊
                    Return mArray(level).Array.CountY
                Else
                    Return mArray(level).Array.CountY * GetLevelCountY(level - 1)
                End If
            Case eLevelType.NoneArray
                If level = 0 Then
                    Return 1
                Else
                    Return GetLevelCountY(level - 1)
                End If
        End Select
        Return 1
    End Function
    ''' <summary>取得level Size</summary>
    ''' <param name="array"></param>
    ''' <remarks></remarks>
    Public Sub GetNodeSize(ByVal array As CMultiArrayAdapter)

        Dim minPosX As Decimal = Decimal.MaxValue
        Dim maxPosX As Decimal = Decimal.MinValue
        Dim minPosY As Decimal = Decimal.MaxValue
        Dim maxPosY As Decimal = Decimal.MinValue
        Dim tempPosX, tempPosY As Decimal
        Dim MaxSizeX As Decimal = Decimal.MinValue
        Dim MaxSizeY As Decimal = Decimal.MinValue
        Dim tempSizeX, tempSizeY As Decimal


        For i = 0 To array.mArray.Count - 1
            Select Case mArray(i).LevelType

                Case eLevelType.Array
                    'Array 的話 最大size 為 設定pitch *array 數量
                    tempSizeX = mArray(i).Array.CountX * Math.Abs(mArray(i).Array.PitchX)
                    tempSizeY = mArray(i).Array.CountY * Math.Abs(mArray(i).Array.PitchY)

                Case eLevelType.NoneArray
                    'NonArray 的話 最大size 為 設定pitch *array 數量
                
                    For j = 0 To mArray(i).NonArray.Count - 1
                        tempPosX = mArray(i).NonArray.Item(j).RelPosX
                        tempPosY = mArray(i).NonArray.Item(j).RelPosY
                        If tempPosX < minPosX Then
                            minPosX = tempPosX
                        End If
                        If tempPosX > maxPosX Then
                            maxPosX = tempPosX
                        End If
                        If tempPosY < minPosY Then
                            minPosY = tempPosY
                        End If
                        If tempPosY > maxPosY Then
                            maxPosY = tempPosY
                        End If
                    Next
                    tempSizeX = maxPosX - minPosX
                    tempSizeY = maxPosY - minPosY

            End Select

            If tempSizeX > MaxSizeX Then
                MaxSizeX = tempSizeX
            End If

            If tempSizeY > MaxSizeY Then
                MaxSizeY = tempSizeY
            End If
        Next

        Size_X = MaxSizeX
        Size_Y = MaxSizeY

    End Sub
    ''' <summary>
    ''' 取得記憶體空間尺寸X方向
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetMemoryCountX() As Integer
        Get
            Return mCountX
        End Get
    End Property

    ''' <summary>
    ''' 取得記憶體空間尺寸Y方向
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetMemoryCountY() As Integer
        Get
            Return mCountY
        End Get

    End Property

    ''' <summary>
    ''' 給定記憶體空間位置索引XY,取得相對於基準點位置X
    ''' </summary>
    ''' <param name="absIdxX">記憶體空間位置索引X</param>
    ''' <param name="absIdxY">記憶體空間位置索引Y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMemoryPosX(ByVal absIdxX As Integer, ByVal absIdxY As Integer) As Decimal
        Return mMapOffsetX(absIdxX, absIdxY)
    End Function
    ''' <summary>
    ''' 給定記憶體空間位置索引XY,取得相對於基準點位置Y
    ''' </summary>
    ''' <param name="absIdxX">記憶體空間位置索引X</param>
    ''' <param name="absIdxY">記憶體空間位置索引Y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMemoryPosY(ByVal absIdxX As Integer, ByVal absIdxY As Integer) As Decimal
        Return mMapOffsetY(absIdxX, absIdxY)
    End Function
    ''' <summary>
    ''' 給定兩組記憶體空間位置索引XY, 取得兩點相對位置X
    ''' </summary>
    ''' <param name="absIdxX"></param>
    ''' <param name="absIdxY"></param>
    ''' <param name="prevIdxX"></param>
    ''' <param name="prevIdxY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMemoryOffsetX(ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByVal prevIdxX As Integer, ByVal prevIdxY As Integer) As Decimal
        Return mMapOffsetX(absIdxX, absIdxY) - mMapOffsetX(prevIdxX, prevIdxY)
    End Function
    ''' <summary>
    ''' 給定兩組記憶體空間位置索引XY, 取得兩點相對位置Y
    ''' </summary>
    ''' <param name="absIdxX"></param>
    ''' <param name="absIdxY"></param>
    ''' <param name="prevIdxX"></param>
    ''' <param name="prevIdxY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMemoryOffsetY(ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByVal prevIdxX As Integer, ByVal prevIdxY As Integer) As Decimal
        Return mMapOffsetY(absIdxX, absIdxY) - mMapOffsetY(prevIdxX, prevIdxY)
    End Function

    Public Sub ManualDispose()
        Erase mMapOffsetX
        Erase mMapOffsetY
    End Sub
  

End Class

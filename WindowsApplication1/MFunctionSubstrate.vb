Imports ProjectRecipe
Imports ProjectCore
Imports ProjectMotion
Imports ProjectConveyor

''' <summary>物料相關功能轉接</summary>
''' <remarks></remarks>
Module MFunctionSubstrate
    ' ''' <summary>
    ' ''' [掃描時回傳狀態]
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmScanStatus
    '    ''' <summary>
    '    ''' [正在執行]
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Running = 0
    '    ''' <summary>
    '    ''' [當層結束]
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    LevelEnd = 1
    '    ''' <summary>
    '    ''' [全部結束]
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Finish = 2
    'End Enum




  
    '20161102
    'Function FindPatternMaxMinTest(ByRef minX As Double, ByRef minY As Double, ByRef maxX As Double, ByRef maxY As Double, ByVal sys As sSysParam, ByVal index As sLevelIndexCollection) As Boolean

    '    If gCRecipe Is Nothing Then
    '        minX = 0
    '        minY = 0
    '        maxX = 0
    '        maxY = 0
    '        Return False
    '    End If
    '    If gCRecipe.Pattern Is Nothing Then
    '        minX = 0
    '        minY = 0
    '        maxX = 0
    '        maxY = 0
    '        Return False
    '    End If
    '    If gCRecipe.strFileName = "" Or index.PatternName = "" Then
    '        Return False
    '    End If

    '    Dim bXz As Double
    '    Dim bYz As Double
    '    Dim mXz As Double
    '    Dim mYz As Double

    '    '20161010
    '    PatternMaxMinOnce = True


    '    Dim mNodeID As String
    '    With gCRecipe

    '        '[說明]:單Stage內有幾個Pattern
    '        For j As Integer = 0 To gCRecipe.Node(index.StageNo).Keys.Count - 1
    '            mNodeID = gCRecipe.Node(index.StageNo).Keys(j)
    '            Dim Name As String = gCRecipe.Node(index.StageNo)(mNodeID).PatternName
    '            Dim NodeBasicX As Decimal = gCRecipe.Node(index.StageNo)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionX
    '            Dim NodeBasicY As Decimal = gCRecipe.Node(index.StageNo)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionY
    '            '[說明]:每個Pattern內有幾個Round
    '            For mRoundNo = 0 To .Pattern(Name).Round.Count - 1
    '                Dim num As Integer = .Pattern(Name).Round.Count
    '                With .Pattern(Name).Round(mRoundNo)
    '                    If .CStep.Count > 0 Then 'Soni / 2016.08.20 StepCount改為CStep.Count
    '                        num = .CStep.Count 'Soni / 2016.08.20 StepCount改為CStep.Count
    '                        For mStepNo = 0 To .CStep.Count - 1 'Soni / 2016.08.20 StepCount改為CStep.Count
    '                            With .CStep(mStepNo)
    '                                Select Case .StepType
    '                                    Case eStepFunctionType.Circle2D
    '                                        '20170215
    '                                        Dim EndPos(1) As Decimal
    '                                        Dim Center(1) As Decimal
    '                                        Center(0) = NodeBasicX + .Circle2D.CenterPosX
    '                                        Center(1) = NodeBasicY + .Circle2D.CenterPosY
    '                                        EndPos(0) = NodeBasicX + .Circle2D.Middle2PosX
    '                                        EndPos(1) = NodeBasicY + .Circle2D.Middle2PosY
    '                                        Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))


    '                                        bXz = max(NodeBasicX + .Circle2D.CenterPosX + mRadius, NodeBasicX + .Circle2D.EndPosX + mRadius)
    '                                        mXz = min(NodeBasicX + .Circle2D.CenterPosX - mRadius, NodeBasicX + .Circle2D.EndPosX - mRadius)
    '                                        bYz = max(NodeBasicY + .Circle2D.CenterPosY - mRadius, NodeBasicY + .Circle2D.EndPosY + mRadius)
    '                                        mYz = min(NodeBasicY + .Circle2D.CenterPosY + mRadius, NodeBasicY + .Circle2D.EndPosY - mRadius)
    '                                        'bXz = max(NodeBasicX + .Circle2D.CenterPosX, NodeBasicX + .Circle2D.EndPosX)
    '                                        'mXz = min(NodeBasicX + .Circle2D.CenterPosX, NodeBasicX + .Circle2D.EndPosX)
    '                                        'bYz = max(NodeBasicY + .Circle2D.CenterPosY, NodeBasicY + .Circle2D.EndPosY)
    '                                        'mYz = min(NodeBasicY + .Circle2D.CenterPosY, NodeBasicY + .Circle2D.EndPosY)


    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If

    '                                    Case eStepFunctionType.Arc2D
    '                                        bXz = max(NodeBasicX + .Arc2D.CenterPosX, NodeBasicX + .Arc2D.EndPosX)
    '                                        mXz = min(NodeBasicX + .Arc2D.CenterPosX, NodeBasicX + .Arc2D.EndPosX)
    '                                        bYz = max(NodeBasicY + .Arc2D.CenterPosY, NodeBasicY + .Arc2D.EndPosY)
    '                                        mYz = min(NodeBasicY + .Arc2D.CenterPosY, NodeBasicY + .Arc2D.EndPosY)

    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If

    '                                    Case eStepFunctionType.Dots3D
    '                                        bXz = max(NodeBasicX + .Dots3D.PosX, NodeBasicX + .Dots3D.PosX)
    '                                        mXz = min(NodeBasicX + .Dots3D.PosX, NodeBasicX + .Dots3D.PosX)
    '                                        bYz = max(NodeBasicY + .Dots3D.PosY, NodeBasicY + .Dots3D.PosY)
    '                                        mYz = min(NodeBasicY + .Dots3D.PosY, NodeBasicY + .Dots3D.PosY)

    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If
    '                                    Case eStepFunctionType.Line3D
    '                                        bXz = max(NodeBasicX + .Line3D.StartPosX, NodeBasicX + .Line3D.EndPosX)
    '                                        mXz = min(NodeBasicX + .Line3D.StartPosX, NodeBasicX + .Line3D.EndPosX)
    '                                        bYz = max(NodeBasicY + .Line3D.StartPosY, NodeBasicY + .Line3D.EndPosY)
    '                                        mYz = min(NodeBasicY + .Line3D.StartPosY, NodeBasicY + .Line3D.EndPosY)

    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If
    '                                    Case eStepFunctionType.Circle3D
    '                                        bXz = max(NodeBasicX + .Circle3D.CenterPosX, NodeBasicX + .Circle3D.EndPosX)
    '                                        mXz = min(NodeBasicX + .Circle3D.CenterPosX, NodeBasicX + .Circle3D.EndPosX)
    '                                        bYz = max(NodeBasicY + .Circle3D.CenterPosY, NodeBasicY + .Circle3D.EndPosY)
    '                                        mYz = min(NodeBasicY + .Circle3D.CenterPosY, NodeBasicY + .Circle3D.EndPosY)

    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If
    '                                    Case eStepFunctionType.Arc3D
    '                                        bXz = max(NodeBasicX + .Arc3D.CenterPosX, NodeBasicX + .Arc3D.EndPosX)
    '                                        mXz = min(NodeBasicX + .Arc3D.CenterPosX, NodeBasicX + .Arc3D.EndPosX)
    '                                        bYz = max(NodeBasicY + .Arc3D.CenterPosY, NodeBasicY + .Arc3D.EndPosY)
    '                                        mYz = min(NodeBasicY + .Arc3D.CenterPosY, NodeBasicY + .Arc3D.EndPosY)

    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If
    '                                    Case eStepFunctionType.Move3D
    '                                        bXz = max(.Move3D.EndPosX, .Move3D.EndPosX)
    '                                        mXz = min(.Move3D.EndPosX, .Move3D.EndPosX)
    '                                        bYz = max(.Move3D.EndPosY, .Move3D.EndPosY)
    '                                        mYz = min(.Move3D.EndPosY, .Move3D.EndPosY)

    '                                        '20161010
    '                                        If PatternMaxMinOnce = True Then
    '                                            maxX = bXz
    '                                            minX = mXz
    '                                            maxY = bYz
    '                                            minY = mYz
    '                                            PatternMaxMinOnce = False
    '                                        ElseIf PatternMaxMinOnce = False Then
    '                                            maxX = max(maxX, bXz)
    '                                            minX = min(minX, mXz)
    '                                            maxY = max(maxY, bYz)
    '                                            minY = min(minY, mYz)
    '                                        End If
    '                                End Select
    '                            End With
    '                        Next
    '                    End If
    '                End With
    '            Next
    '        Next
    '    End With

    '    Return True
    'End Function


    Function max(value01 As Double, value02 As Double) As Double
        Dim maxx As Double
        If value01 >= value02 Then
            maxx = value01
        Else
            maxx = value02
        End If
        Return maxx
    End Function

    Function min(value01 As Double, value02 As Double) As Double
        Dim minn As Double
        If value01 >= value02 Then
            minn = value02
        Else
            minn = value01
        End If
        Return minn
    End Function

    'Public Function DrawSingleStepGraphicsTest(ByVal patternID As String, ByVal RoundNo As Integer, ByVal StepNo As Integer, ByVal GraphicsPictureBox As PictureBox, ByVal sys As sSysParam, ByVal index As sLevelIndexCollection) As Boolean
    '    Dim mRoundNo As Integer
    '    Dim mStepNo As Integer
    '    Dim mDraw As Graphics
    '    Dim mPen As New Pen(Color.Black)
    '    Dim mBrush As New Drawing.SolidBrush(Color.Black)
    '    Dim mBitmap As Bitmap
    '    Dim mScaleHeight As Decimal                              '轉換成畫布大小的Scale
    '    Dim mScaleWidth As Decimal                               '轉換成畫布大小的Scale
    '    Dim mGraphicsStartX As Decimal                           '轉換成畫布大小StartX
    '    Dim mGraphicsStartY As Decimal                           '轉換成畫布大小StartY
    '    Dim mGraphicsEndX As Decimal                             '轉換成畫布大小EndX
    '    Dim mGraphicsEndY As Decimal                             '轉換成畫布大小EndY
    '    Dim mGraphicsWidth As Decimal                            '轉換成畫布大小Width
    '    Dim mGraphicsHeight As Decimal                           '轉換成畫布大小Height
    '    Dim mGraphicsRadius As Decimal                           '轉換成畫布大小Radius
    '    Dim mGraphicsAngle As Decimal                            '轉換成畫布大小Angle
    '    Dim mShiftX As Decimal
    '    Dim mShiftY As Decimal
    '    Dim mPointSize As Integer                                 '單點的大小


    '    mBitmap = GraphicsPictureBox.Image

    '    If mBitmap Is Nothing Then
    '        mBitmap = New Bitmap(CInt(GraphicsPictureBox.Width), CInt(GraphicsPictureBox.Height))
    '    End If

    '    mDraw = Graphics.FromImage(mBitmap)

    '    'mShiftX = 0
    '    'mShiftY = 0
    '    'mScaleWidth = GraphicsPictureBox.Width / 16
    '    'mScaleHeight = GraphicsPictureBox.Height / 16
    '    'mPointSize = CInt(GraphicsPictureBox.Width / 100) '繪點大小

    '    '--- Soni + 2014.10.30 圖型過大時自動調整比例 ---
    '    Dim maxPosX As Decimal, maxPosY As Decimal, minPosX As Decimal, minPosY As Decimal
    '    'FindPatternMaxMinOffset(minPosX, minPosY, maxPosX, maxPosY, sys) '找最大最小點
    '    FindPatternMaxMinTest(minPosX, minPosY, maxPosX, maxPosY, sys, index) '找最大最小點   20161102

    '    '--- 增加邊界空白 ---
    '    maxPosX += 3
    '    maxPosY += 3
    '    minPosX -= 3
    '    minPosY -= 3
    '    '--- 增加邊界空白 ---


    '    '[說明]:換算比例大小
    '    GetDrawShiftScale(GraphicsPictureBox, maxPosX, minPosX, maxPosY, minPosY, mShiftX, mShiftY, mScaleWidth, mScaleHeight, mPointSize) 'Soni / 2016.12.07
    '    'mShiftX = 6.5
    '    'mShiftY = 6.5
    '    'mScaleWidth = GraphicsPictureBox.Width / 13
    '    'mScaleHeight = GraphicsPictureBox.Height / 13
    '    'mPointSize = CInt(GraphicsPictureBox.Width / 100) '繪點大小

    '    'If maxPosX * mScaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
    '    '    mScaleWidth = 0.25 * GraphicsPictureBox.Width / maxPosX
    '    '    mScaleHeight = mScaleWidth
    '    '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
    '    '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
    '    'End If
    '    'If -minPosX * mScaleWidth > GraphicsPictureBox.Width / 4 Then '任一邊過界則調整顯示比例
    '    '    mScaleWidth = -0.25 * GraphicsPictureBox.Width / minPosX
    '    '    mScaleHeight = mScaleWidth
    '    '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
    '    '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
    '    'End If
    '    'If maxPosY * mScaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
    '    '    mScaleHeight = 0.25 * GraphicsPictureBox.Height / maxPosY
    '    '    mScaleWidth = mScaleHeight
    '    '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
    '    '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
    '    'End If
    '    'If -minPosY * mScaleHeight > GraphicsPictureBox.Height / 4 Then '任一邊過界則調整顯示比例
    '    '    mScaleHeight = -0.25 * GraphicsPictureBox.Height / minPosY
    '    '    mScaleWidth = mScaleHeight
    '    '    mShiftX = GraphicsPictureBox.Width * 0.5 / mScaleWidth
    '    '    mShiftY = GraphicsPictureBox.Height * 0.5 / mScaleHeight
    '    'End If
    '    '--- Soni + 2014.10.30 圖型過大時自動調整比例 ---

    '    If index.PatternName = "" Then 'Pattern名稱不存在
    '        DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
    '        GraphicsPictureBox.Image = mBitmap
    '        Return True
    '    End If

    '    '=== Soni + 2016.09.09 Copy from DrawSingleGraphics ===
    '    If Not gCRecipe.Pattern.ContainsKey(index.PatternName) Then 'Pattern清單內不存在指定名稱
    '        DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
    '        GraphicsPictureBox.Image = mBitmap
    '        Return True
    '    End If
    '    If gCRecipe.Pattern(index.PatternName).Round.Count = 0 Then '數量為零不繪製
    '        DrawCoord(mDraw, GraphicsPictureBox.Width, GraphicsPictureBox.Height)
    '        GraphicsPictureBox.Image = mBitmap
    '        Return True
    '    End If
    '    '=== Soni + 2016.09.09 Copy from DrawSingleGraphics ===

    '    '[說明]:畫Recipe圖形  先轉換成畫布Size再描畫圖形
    '    With mPen
    '        .Width = GraphicsPictureBox.Width / 500
    '        .DashStyle = Drawing2D.DashStyle.Solid
    '        .Color = Color.Red
    '    End With

    '    mBrush.Color = Color.Red
    '    Dim mNodeID As String

    '    For i As Integer = index.StageNo To index.StageNo
    '        For j As Integer = 0 To gCRecipe.Node(i).Keys.Count - 1
    '            mNodeID = gCRecipe.Node(i).Keys(j)
    '            Dim Name As String = gCRecipe.Node(i)(mNodeID).PatternName
    '            Dim NodeBasicX As Decimal = gCRecipe.Node(i)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionX
    '            Dim NodeBasicY As Decimal = gCRecipe.Node(i)(mNodeID).ConveyorPos(sys.ConveyorNo).BasicPositionY

    '            With gCRecipe
    '                For mRoundNo = 0 To .Pattern(index.PatternName).Round.Count - 1
    '                    Dim num As Integer = .Pattern(index.PatternName).Round.Count
    '                    With .Pattern(index.PatternName).Round(mRoundNo)
    '                        If .StepCount > 0 Then
    '                            For mStepNo = 0 To .StepCount - 1   '20160907
    '                                If mStepNo = StepNo Then
    '                                    With .CStep(mStepNo)
    '                                        Select Case .StepType
    '                                            Case eStepFunctionType.Dots3D '點
    '                                                Select Case gSSystemParameter.CoordType
    '                                                    Case enmCoordinateRelationType.eGN2
    '                                                        mGraphicsStartX = (NodeBasicX + .Dots3D.PosX + mShiftX) * mScaleWidth - mPointSize * 0.5
    '                                                        mGraphicsStartY = (NodeBasicY + .Dots3D.PosY + mShiftY) * mScaleHeight - mPointSize * 0.5
    '                                                    Case enmCoordinateRelationType.eDTS
    '                                                        mGraphicsEndX = (NodeBasicX + .Dots3D.PosX + mShiftX) * mScaleWidth - mPointSize * 0.5
    '                                                        mGraphicsEndY = (NodeBasicY + .Dots3D.PosY + mShiftY) * mScaleHeight - mPointSize * 0.5
    '                                                End Select

    '                                                mDraw.FillEllipse(mBrush, CInt(mGraphicsEndX), CInt(mGraphicsEndY), mPointSize, mPointSize)
    '                                            Case eStepFunctionType.Move3D
    '                                                Select Case gSSystemParameter.CoordType
    '                                                    Case enmCoordinateRelationType.eGN2
    '                                                        mGraphicsEndX = (NodeBasicX + .Move3D.EndPosX + mShiftX) * mScaleWidth - mPointSize * 0.5
    '                                                        mGraphicsEndY = (NodeBasicY + .Move3D.EndPosY + mShiftY) * mScaleHeight - mPointSize * 0.5
    '                                                    Case enmCoordinateRelationType.eDTS
    '                                                        mGraphicsEndX = (NodeBasicX + .Move3D.EndPosX + mShiftX) * mScaleWidth - mPointSize * 0.5
    '                                                        mGraphicsEndY = (NodeBasicY + .Move3D.EndPosY + mShiftY) * mScaleHeight - mPointSize * 0.5
    '                                                End Select

    '                                                mDraw.FillEllipse(mBrush, CInt(mGraphicsEndX), CInt(mGraphicsEndY), mPointSize, mPointSize)

    '                                            Case eStepFunctionType.Line3D
    '                                                Select Case gSSystemParameter.CoordType
    '                                                    Case enmCoordinateRelationType.eGN2
    '                                                        mGraphicsStartX = (NodeBasicX + .Line3D.StartPosX + mShiftX) * mScaleWidth
    '                                                        mGraphicsStartY = (NodeBasicY + .Line3D.StartPosY + mShiftY) * mScaleHeight
    '                                                        mGraphicsEndX = (NodeBasicX + .Line3D.EndPosX + mShiftX) * mScaleWidth
    '                                                        mGraphicsEndY = (NodeBasicY + .Line3D.EndPosY + mShiftY) * mScaleHeight
    '                                                    Case enmCoordinateRelationType.eDTS
    '                                                        mGraphicsStartX = (NodeBasicX + .Line3D.StartPosX + mShiftX) * mScaleWidth
    '                                                        mGraphicsStartY = (NodeBasicY + .Line3D.StartPosY + mShiftY) * mScaleHeight
    '                                                        mGraphicsEndX = (NodeBasicX + .Line3D.EndPosX + mShiftX) * mScaleWidth
    '                                                        mGraphicsEndY = (NodeBasicY + .Line3D.EndPosY + mShiftY) * mScaleHeight
    '                                                End Select

    '                                                If mGraphicsStartY = mGraphicsEndY And mGraphicsStartX = mGraphicsEndX Then '共點
    '                                                    'Jeffadd
    '                                                    mDraw.FillEllipse(mBrush, CInt(mGraphicsStartX - mPointSize / 2), CInt(mGraphicsStartY - mPointSize / 2), mPointSize, mPointSize)
    '                                                Else
    '                                                    mDraw.DrawLine(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsEndX), CInt(mGraphicsEndY))
    '                                                End If
    '                                                '20160805
    '                                            Case eStepFunctionType.Circle2D '圓
    '                                                Dim EndPos(1) As Decimal
    '                                                Dim Center(1) As Decimal
    '                                                Center(0) = NodeBasicX + .Circle2D.CenterPosX
    '                                                Center(1) = NodeBasicY + .Circle2D.CenterPosY
    '                                                EndPos(0) = NodeBasicX + .Circle2D.Middle2PosX
    '                                                EndPos(1) = NodeBasicY + .Circle2D.Middle2PosY
    '                                                If Center(0) = EndPos(0) And Center(1) = EndPos(1) Then
    '                                                Else
    '                                                    Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))
    '                                                    Select Case gSSystemParameter.CoordType
    '                                                        Case enmCoordinateRelationType.eGN2
    '                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
    '                                                        Case enmCoordinateRelationType.eDTS
    '                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
    '                                                    End Select
    '                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
    '                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth
    '                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), 0, 360)
    '                                                End If

    '                                                'DrawArc
    '                                                'Pen()
    '                                                'Pen，決定弧形的色彩、寬度和樣式。
    '                                                'rect()
    '                                                'RectangleF 結構，定義橢圓形的邊界。
    '                                                'startAngle()
    '                                                '以度為單位，依順時針方向測量之從 X 軸到弧形開始點的角度。
    '                                                'sweepAngle()
    '                                                '以度為單位，依順時針方向測量之從 startAngle 參數到弧形結束點的角度。
    '                                                '20160805
    '                                            Case eStepFunctionType.Arc2D
    '                                                Dim AngleX As Double = .Arc2D.Angle
    '                                                Dim StartPos(1) As Decimal
    '                                                Dim EndPos(1) As Decimal
    '                                                Dim Center(1) As Decimal
    '                                                Dim CenterF(1) As Decimal

    '                                                StartPos(0) = NodeBasicX + .Arc2D.StartPosX
    '                                                StartPos(1) = NodeBasicY + .Arc2D.StartPosY
    '                                                EndPos(0) = NodeBasicX + .Arc2D.EndPosX
    '                                                EndPos(1) = NodeBasicY + .Arc2D.EndPosY

    '                                                Dim Circle As Circle
    '                                                Dim x, y, z As CPoint
    '                                                'Jeffadd 20160615
    '                                                x = New CPoint(Val(NodeBasicX + .Arc2D.StartPosX), Val(NodeBasicY + .Arc2D.StartPosY))
    '                                                y = New CPoint(Val(NodeBasicX + .Arc2D.MiddlePosX), Val(NodeBasicY + .Arc2D.MiddlePosY))
    '                                                z = New CPoint(Val(NodeBasicX + .Arc2D.EndPosX), Val(NodeBasicY + .Arc2D.EndPosY))

    '                                                '[說明]:計算Arc圓心座標
    '                                                Circle = CMath.GetCircleby3Point(x, y, z)

    '                                                Center(0) = Circle.PointX
    '                                                Center(1) = Circle.PointY

    '                                                Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))

    '                                                mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight

    '                                                mGraphicsHeight = mRadius * 2 * mScaleHeight
    '                                                mGraphicsWidth = mRadius * 2 * mScaleWidth

    '                                                '起始值
    '                                                '  Dim StartAngle As Integer = CInt(GetAngleJeffTest(StartPos(0), StartPos(1), Center(0), Center(1)))

    '                                                Dim StartAngle As Integer = CInt(GetAngleJeffTest(Center(0), Center(1), StartPos(0), StartPos(1)))
    '                                                Dim StartAngleTest As Integer
    '                                                Dim EndAngleTest As Integer
    '                                                If StartAngle > 0 Then
    '                                                    StartAngleTest = 180 - StartAngle
    '                                                Else
    '                                                    StartAngleTest = -(StartAngle) + 180
    '                                                End If

    '                                                Circle.clockwise = Not Circle.clockwise
    '                                                If Circle.clockwise = True Then
    '                                                    EndAngleTest = Circle.Angle
    '                                                ElseIf Circle.clockwise = False Then
    '                                                    Dim Middle As Integer
    '                                                    EndAngleTest = StartAngleTest - Circle.Angle
    '                                                    Middle = EndAngleTest
    '                                                    StartAngleTest = Middle

    '                                                    EndAngleTest = Circle.Angle
    '                                                End If


    '                                                '結束值
    '                                                mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), StartAngleTest, EndAngleTest)
    '                                                '  mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), CInt(StartAngle), CInt(Circle.Angle))
    '                                            Case eStepFunctionType.Circle3D
    '                                                Dim EndPos(1) As Decimal
    '                                                Dim Center(1) As Decimal
    '                                                Center(0) = NodeBasicX + .Circle3D.CenterPosX
    '                                                Center(1) = NodeBasicY + .Circle3D.CenterPosY
    '                                                EndPos(0) = NodeBasicX + .Circle3D.EndPosX
    '                                                EndPos(1) = NodeBasicY + .Circle3D.EndPosY
    '                                                If Center(0) = EndPos(0) And Center(1) = EndPos(1) Then
    '                                                Else
    '                                                    Dim mRadius As Decimal = GetDistance(EndPos(0), EndPos(1), Center(0), Center(1))
    '                                                    Select Case gSSystemParameter.CoordType
    '                                                        Case enmCoordinateRelationType.eGN2
    '                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
    '                                                        Case enmCoordinateRelationType.eDTS
    '                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                            mGraphicsStartY = (-Center(1) + mShiftY - mRadius) * mScaleHeight
    '                                                    End Select
    '                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
    '                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth
    '                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsWidth), CInt(mGraphicsHeight), 0, 360)
    '                                                End If

    '                                            Case eStepFunctionType.Arc3D

    '                                                '[說明]:配合劃膠之角度轉換 實際劃膠起始點為180(-1,0)順時針 
    '                                                '描繪的圖形起始點為0(1,0)順時針 起始點為矩形的為左上角之座標
    '                                                '(StartX,StartY)定義為圓心之座標
    '                                                If .Arc3D.Angle >= 180 Then
    '                                                    mGraphicsAngle = (.Arc3D.Angle + 180) Mod 360 + 180
    '                                                Else
    '                                                    mGraphicsAngle = (.Arc3D.Angle + 180) Mod 360 - 180
    '                                                End If

    '                                                Dim StartPos(1) As Decimal
    '                                                Dim EndPos(1) As Decimal
    '                                                Dim Center(1) As Decimal

    '                                                StartPos(0) = NodeBasicX + .Arc3D.StartPosX
    '                                                StartPos(1) = NodeBasicY + .Arc3D.StartPosY
    '                                                EndPos(0) = NodeBasicX + .Arc3D.EndPosX
    '                                                EndPos(1) = NodeBasicY + .Arc3D.EndPosY
    '                                                If StartPos(0) = EndPos(0) And StartPos(1) = EndPos(1) Then
    '                                                Else
    '                                                    '計算原點座標(圓心) 正向和反向 利用向量計算
    '                                                    If gCRecipe.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle >= 0 Then
    '                                                        CenterCalculate(StartPos, EndPos, Math.Abs(gCRecipe.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 0)
    '                                                    Else
    '                                                        CenterCalculate(StartPos, EndPos, Math.Abs(gCRecipe.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle), Center, 1)
    '                                                    End If

    '                                                    Dim mRadius As Decimal = GetDistance(StartPos(0), StartPos(1), Center(0), Center(1))
    '                                                    Select Case gSSystemParameter.CoordType
    '                                                        Case enmCoordinateRelationType.eGN2
    '                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                            mGraphicsStartY = (Center(1) + mShiftY - mRadius) * mScaleHeight
    '                                                        Case enmCoordinateRelationType.eDTS
    '                                                            mGraphicsStartX = (Center(0) + mShiftX - mRadius) * mScaleWidth
    '                                                            mGraphicsStartY = (-Center(1) + mShiftY - mRadius) * mScaleHeight
    '                                                    End Select

    '                                                    mGraphicsEndX = (NodeBasicX + .Arc3D.EndPosX + mShiftX) * mScaleWidth
    '                                                    mGraphicsEndY = (NodeBasicY + .Arc3D.EndPosY + mShiftY) * mScaleHeight

    '                                                    mGraphicsHeight = mRadius * 2 * mScaleHeight
    '                                                    mGraphicsWidth = mRadius * 2 * mScaleWidth
    '                                                    '起始值
    '                                                    Dim StartAngle As Integer = CInt(GetAngle(StartPos(0), StartPos(1), Center(0), Center(1)))
    '                                                    '結束值
    '                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX), CInt(mGraphicsStartY), CInt(mGraphicsHeight), CInt(mGraphicsHeight), StartAngle, CInt(mGraphicsAngle))

    '                                                    '判斷正反轉
    '                                                    If gCRecipe.Pattern(index.PatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.Angle > 0 Then
    '                                                        '正轉
    '                                                        If mGraphicsAngle > StartAngle Then
    '                                                            mGraphicsAngle -= StartAngle
    '                                                        Else
    '                                                            mGraphicsAngle += (360 - StartAngle)
    '                                                        End If

    '                                                    Else
    '                                                        '反轉
    '                                                        If (mGraphicsAngle - StartAngle) > 0 Then
    '                                                            mGraphicsAngle -= (360 + StartAngle)
    '                                                        Else
    '                                                            mGraphicsAngle -= StartAngle
    '                                                        End If


    '                                                    End If
    '                                                    mDraw.DrawArc(mPen, CInt(mGraphicsStartX - mGraphicsRadius), CInt(mGraphicsStartY - mGraphicsRadius), CInt(mGraphicsRadius) * 2, CInt(mGraphicsRadius) * 2, 180, CInt(mGraphicsAngle))
    '                                                End If

    '                                        End Select
    '                                    End With


    '                                End If
    '                            Next
    '                        End If
    '                    End With
    '                Next
    '            End With
    '        Next
    '    Next


    '    GraphicsPictureBox.Image = mBitmap

    '    Return True

    'End Function



    'End Function
    '*******************Fenix+ 2016/01/09****************

    ' ''' <summary>
    ' ''' [回傳下一個Laser測高點資訊]
    ' ''' </summary>
    ' ''' <param name="sys"></param>
    ' ''' <param name="LaserIndex"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function NextLaser(ByVal sys As sSysParam, ByRef LaserIndex As sLevelIndexCollection, ByRef nodeLevel As Integer) As enmScanStatus
    '    If gNextScan(sys.StageNo).NowIndex >= gNextScan(sys.StageNo).List.Count Then
    '        '[Note]:沒有就直接離開(Count=0)
    '        If gNextScan(sys.StageNo).List.Count > 0 Then
    '            nodeLevel = gNextScan(sys.StageNo).List(gNextScan(sys.StageNo).NowIndex - 1).LevelNo
    '        End If
    '        Return enmScanStatus.Finish
    '    Else
    '        nodeLevel = gNextScan(sys.StageNo).List(gNextScan(sys.StageNo).NowIndex).LevelNo
    '        With gNextScan(sys.StageNo).List(gNextScan(sys.StageNo).NowIndex)
    '            If .IsLevelEnd = False Then
    '                LaserIndex = gNextScan(sys.StageNo).List(gNextScan(sys.StageNo).NowIndex)
    '                gNextScan(sys.StageNo).NowIndex += 1 '指向下一點
    '                Return enmScanStatus.Running
    '            Else
    '                gNextScan(sys.StageNo).NowIndex += 1 '指向下一點
    '                Return enmScanStatus.LevelEnd
    '            End If
    '        End With
    '    End If
    'End Function

    
End Module

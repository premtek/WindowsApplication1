Imports System.IO

#Region "[Eason] Stage Offset from SCV file , "

Public Class CStageCalibrationOffsetFromSCV

    Public Enum eResult
        Success = 0
        FailLoadFile = 1
        FailAnalysis = 2
        UnknowError = 3
    End Enum

    Public Enum eFixMode
        None = 0
        ''' <summary>
        ''' 軟體補償
        ''' </summary>
        ''' <remarks></remarks>
        SWMode = 1
        ''' <summary>
        ''' 軸卡補償 
        ''' </summary>
        ''' <remarks></remarks>
        HWMode = 2
    End Enum

#Region "Private struct"
    Private Structure MePoint
        Public X As Decimal
        Public Y As Decimal
    End Structure
#End Region

#Region "Private Variable"

    ''' <summary>
    ''' Future function : Mapping Index support shuft ( no use now ) 
    ''' </summary>
    ''' <remarks></remarks>
    Private dicOffsetMapData As Dictionary(Of Integer, Dictionary(Of Integer, MePoint)) = New Dictionary(Of Integer, Dictionary(Of Integer, MePoint))()

    Private StartPoint As MePoint

    Private PitchLength As MePoint

    Private Direction As MePoint

    Private Count As Point

    Private STR_TYPE As String = "TYPE="

    Private STR_XCOUNT As String = "XCOUNT="
    Private STR_YCOUNT As String = "YCOUNT="

    Private STR_XPITCH As String = "XPITCH="
    Private STR_YPITCH As String = "YPITCH="

    Private STR_XSTART As String = "XSTART="
    Private STR_YSTART As String = "YSTART="

    Private STR_DATA_TYPE0 As String = "DATA:"
    Private STR_DATA_TYPE1 As String = "[STAGE VERIFICATION INFORMATION]"


    Private STR_XDirection As String = "OFFSETMMXDIRECTION="
    Private STR_YDirection As String = "OFFSETMMYDIRECTION="

    Private LoadFileXMax As Integer = 0
    Private LoadFileYMax As Integer = 0

    Private mExceptionString As String = ""

    Private mEnable As Boolean = False

    Private mLoaded As Boolean = False

    Private mFixMode As eFixMode = eFixMode.None

#End Region

#Region "Public Function"

    ''' <summary>
    ''' Manual Control Enable/Disable this function
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enable As Boolean
        Set(value As Boolean)
            mEnable = value
        End Set
        Get
            Return mEnable
        End Get
    End Property

    ''' <summary>
    ''' [NOTE] : After Exception , Can Read the Exception string
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExceptionString As String
        Get
            Return mExceptionString
        End Get
    End Property

    ''' <summary>
    ''' [NOTE] : After Load File , If Data Success analysis Return True else Retur False
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsDataCheckOK As Boolean
        Get
            Dim Result As Boolean = True

            mExceptionString = ""

            If (Count.X <= 0) Then
                mExceptionString = mExceptionString & " X Count = 0 ,"
                Result = False
            End If
            If (Count.Y <= 0) Then
                mExceptionString = mExceptionString & " Y Count = 0 ,"
                Result = False
            End If
            If (Count.X <> LoadFileXMax) Then
                mExceptionString = mExceptionString & " Count.X <> LoadFileXMax ,"
                Result = False
            End If
            If (Count.Y <> LoadFileYMax) Then
                mExceptionString = mExceptionString & " Count.Y <> LoadFileYMax ,"
                Result = False
            End If
            If (PitchLength.X < 0) Then
                mExceptionString = mExceptionString & " PitchLength.X < 0 ,"
                Result = False
            End If
            If (PitchLength.Y < 0) Then
                mExceptionString = mExceptionString & " PitchLength.Y < 0 ,"
                Result = False
            End If

            If (Direction.X = 0) Then
                mExceptionString = mExceptionString & " Direction.X = 0 ,"
                Result = False
            End If

            If (Direction.Y = 0) Then
                mExceptionString = mExceptionString & " Direction.Y = 0 ,"
                Result = False
            End If

            Return Result
        End Get
    End Property

    Public ReadOnly Property StartPositionX As Decimal
        Get
            Return StartPoint.X
        End Get
    End Property

    Public ReadOnly Property StartPositionY As Decimal
        Get
            Return StartPoint.Y
        End Get
    End Property

    Public ReadOnly Property PitchX As Decimal
        Get
            Return PitchLength.X
        End Get
    End Property

    Public ReadOnly Property PitchY As Decimal
        Get
            Return PitchLength.Y
        End Get
    End Property

    Public ReadOnly Property OffsetX As Decimal(,)
        Get
            Dim reOffset(,) As Decimal = New Decimal(Count.X - 1, Count.Y - 1) {}
            For index0 = 0 To Count.X - 1
                For index1 = 0 To Count.Y - 1
                    reOffset(index0, index1) = 0
                Next
            Next

            For indexKey = 0 To dicOffsetMapData.Keys.Count - 1
                For indexValue = 0 To dicOffsetMapData(dicOffsetMapData.Keys(indexKey)).Keys.Count - 1
                    Dim first As Integer = dicOffsetMapData.Keys(indexKey)
                    Dim Second As Integer = dicOffsetMapData(dicOffsetMapData.Keys(indexKey)).Keys(indexValue)
                    If (first <= Count.X - 1 AndAlso Second <= Count.Y - 1) Then
                        reOffset(indexKey, indexValue) = dicOffsetMapData(first)(Second).X
                    End If
                Next
            Next
            Return reOffset
        End Get
    End Property

    Public ReadOnly Property OffsetY As Decimal(,)
        Get
            Dim reOffset(,) As Decimal = New Decimal(Count.X - 1, Count.Y - 1) {}
            For index0 = 0 To Count.X - 1
                For index1 = 0 To Count.Y - 1
                    reOffset(index0, index1) = 0
                Next
            Next

            For indexKey = 0 To dicOffsetMapData.Keys.Count - 1
                For indexValue = 0 To dicOffsetMapData(dicOffsetMapData.Keys(indexKey)).Keys.Count - 1
                    Dim first As Integer = dicOffsetMapData.Keys(indexKey)
                    Dim Second As Integer = dicOffsetMapData(dicOffsetMapData.Keys(indexKey)).Keys(indexValue)
                    If (first <= Count.X - 1 AndAlso Second <= Count.Y - 1) Then
                        reOffset(indexKey, indexValue) = dicOffsetMapData(first)(Second).Y
                    End If
                Next
            Next
            Return reOffset
        End Get
    End Property
    ''' <summary>
    '''  [NOTE] : OpenFileDialog Function
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OpenFile() As String

        Dim strFileName As String = ""

        Using OpenFileDialogCSV As OpenFileDialog = New OpenFileDialog
            OpenFileDialogCSV.CheckFileExists = True
            OpenFileDialogCSV.Filter = "csv files (*.csv)|*.csv"
            OpenFileDialogCSV.Multiselect = False
            OpenFileDialogCSV.InitialDirectory = Application.StartupPath
            OpenFileDialogCSV.ShowDialog()
            strFileName = OpenFileDialogCSV.FileName
        End Using

        Return strFileName

    End Function

    Public Sub Clear()
        mLoaded = False
        dicOffsetMapData.Clear()
    End Sub

    ''' <summary>
    ''' [NOTE] : Load CSV File and Analysis Data
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadFile(FileName As String, Mode As eFixMode) As eResult

        mFixMode = Mode
        mLoaded = False
        dicOffsetMapData.Clear()
        Dim lsFileData As List(Of String) = New List(Of String)()
        Try
            Dim sr As StreamReader = New StreamReader(FileName)
            Do While sr.Peek() >= 0
                Dim GetLineString As String = sr.ReadLine()
                If (GetLineString <> "") Then
                    lsFileData.Add(GetLineString)
                End If
            Loop
            sr.Close()

            If (lsFileData.Count = 0) Then
                Return eResult.FailLoadFile
            End If

        Catch ex As Exception
            mExceptionString = ex.ToString()
            Return eResult.FailLoadFile
        End Try

        '確認檔案TYPE------------------------------------------------------------------------
        Dim iType As Integer = 0

        If (lsFileData(0).ToUpper.IndexOf(STR_TYPE) >= 0) Then
            Dim mType As Integer
            If (Integer.TryParse(lsFileData(0).ToUpper.Replace(STR_TYPE, ""), mType)) Then
                iType = mType
            End If
        End If
        '-------------------------------------------------------------------------------------
        Try

            Select Case iType

                Case 1
                    fnAnalysisType1CSV(lsFileData)
                    Exit Select

                Case Else
                    fnAnalysisType0CSV(lsFileData)
            End Select

            mLoaded = True

        Catch ex As Exception

            mExceptionString = ex.ToString()
            dicOffsetMapData.Clear()
            Return eResult.FailAnalysis
        End Try

        Return eResult.Success

    End Function

    ''' <summary>
    ''' [Note] : Key in the IndexX and IndexY return the offsetX
    ''' Only SW Mode
    ''' </summary>
    ''' <param name="IndexX"></param>
    ''' <param name="IndexY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOffsetfromIndexX(IndexX As Integer, IndexY As Integer) As Decimal

        If (mLoaded = False) Then
            Return 0
        End If

        If (mEnable = False) Then
            Return 0
        End If

        If (mFixMode <> eFixMode.SWMode) Then
            Return 0
        End If

        Dim Result As Decimal = 0

        If dicOffsetMapData.ContainsKey(IndexX) AndAlso dicOffsetMapData(IndexX).ContainsKey(IndexY) Then
            Return dicOffsetMapData(IndexX)(IndexY).X * Direction.X
        End If
        Return Result

    End Function

    ''' <summary>
    ''' [Note] : Key in the IndexY and IndexY return the offsetY
    ''' Only SW Mode
    ''' </summary>
    ''' <param name="IndexX"></param>
    ''' <param name="IndexY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOffsetfromIndexY(IndexX As Integer, IndexY As Integer) As Decimal

        If (mLoaded = False) Then
            Return 0
        End If

        If (mEnable = False) Then
            Return 0
        End If

        If (mFixMode <> eFixMode.SWMode) Then
            Return 0
        End If

        Dim Result As Decimal = 0
        If dicOffsetMapData.ContainsKey(IndexX) AndAlso dicOffsetMapData(IndexX).ContainsKey(IndexY) Then
            Return dicOffsetMapData(IndexX)(IndexY).Y * Direction.Y
        End If
        Return Result

    End Function
    ''' <summary>
    ''' Only SW Mode
    ''' </summary>
    ''' <param name="PositionX"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexfromPosX(PositionX As Decimal) As Integer

        If (mFixMode <> eFixMode.SWMode) Then
            Return 0
        End If

        Dim Result As Integer = 0

        Dim arr As Decimal = PositionX - StartPoint.X

        If (PitchLength.X > 0) Then
            Result = CType(Math.Abs(PositionX - StartPoint.X) / PitchLength.X, Integer)
        Else
            Result = Integer.MinValue
        End If

        Return Result

    End Function
    ''' <summary>
    ''' Only SW Mode
    ''' </summary>
    ''' <param name="PositionY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexfromPosY(PositionY As Decimal) As Integer

        If (mFixMode <> eFixMode.SWMode) Then
            Return 0
        End If

        Dim Result As Integer = 0

        Dim arr As Decimal = PositionY - StartPoint.Y

        If (PitchLength.X > 0) Then
            Result = CType(Math.Abs(PositionY - StartPoint.Y) / PitchLength.Y, Integer)
        Else
            Result = Integer.MinValue
        End If

        Return Result

    End Function

    Private Sub fnAnalysisBasicParameter(str As String)

        Dim GetLineString As String = str
        ' XCount -------------------------------------------------------------------------- 
        If (GetLineString.ToUpper.IndexOf(STR_XCOUNT) >= 0) Then
            Dim Xout As Integer
            If (Integer.TryParse(GetLineString.ToUpper.Replace(STR_XCOUNT, ""), Xout)) Then
                Count.X = Xout
            End If
            ' YCount -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_YCOUNT) >= 0) Then
            Dim Yout As Integer
            If (Integer.TryParse(GetLineString.ToUpper.Replace(STR_YCOUNT, ""), Yout)) Then
                Count.Y = Yout
            End If
            ' XPitch -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_XPITCH) >= 0) Then
            Dim Xout As Decimal
            If (Decimal.TryParse(GetLineString.ToUpper.Replace(STR_XPITCH, ""), Xout)) Then
                'PitchLength.X = Xout
                PitchLength.X = Xout * 1000
            End If
            ' YPitch -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_YPITCH) >= 0) Then
            Dim Yout As Decimal
            If (Decimal.TryParse(GetLineString.ToUpper.Replace(STR_YPITCH, ""), Yout)) Then
                PitchLength.Y = Yout * 1000
                'PitchLength.Y = Yout
            End If
            ' XStart -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_XSTART) >= 0) Then
            Dim Xout As Decimal
            If (Decimal.TryParse(GetLineString.ToUpper.Replace(STR_XSTART, ""), Xout)) Then
                StartPoint.X = Xout * 1000
                ' StartPoint.X = Xout
            End If
            ' YStart -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_YSTART) >= 0) Then
            Dim Yout As Decimal
            If (Decimal.TryParse(GetLineString.ToUpper.Replace(STR_YSTART, ""), Yout)) Then
                StartPoint.Y = Yout * 1000
                'StartPoint.Y = Yout
            End If
            ' XDic -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_XDirection) >= 0) Then
            Dim Xdic As Decimal
            If (Decimal.TryParse(GetLineString.ToUpper.Replace(STR_XDirection, ""), Xdic)) Then
                Direction.X = Xdic
            End If
            ' YDic -------------------------------------------------------------------------- 
        ElseIf (GetLineString.ToUpper.IndexOf(STR_YDirection) >= 0) Then
            Dim Ydic As Decimal
            If (Decimal.TryParse(GetLineString.ToUpper.Replace(STR_YDirection, ""), Ydic)) Then
                Direction.Y = Ydic
            End If

        End If
    End Sub

    ''' <summary>
    ''' Origin
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fnAnalysisType0CSV(ByVal lstData As List(Of String))

        Dim StartData As Boolean = False
        Dim lstCount As Integer = lstData.Count
        Dim ListdicOffsetMapData As List(Of List(Of MePoint)) = New List(Of List(Of MePoint))()
        ListdicOffsetMapData.Clear()

        For index = 0 To lstCount - 1

            Dim GetLineString As String = lstData(index)

            If (StartData) Then

                Dim inListbuffer As List(Of MePoint) = New List(Of MePoint)()
                Dim SplitString As String() = GetLineString.Split(vbTab)
                Dim FilterString As String() = SplitString.Select(Function(str As String) str.Replace(Chr(34), "")).ToArray()

                For Each item In FilterString

                    Dim SplitSubString As String() = item.Split(",")

                    If (SplitSubString.Length = 2) Then

                        Dim Xout As Decimal
                        Dim Yout As Decimal
                        If (Decimal.TryParse(SplitSubString(0), Xout) And Decimal.TryParse(SplitSubString(1), Yout)) Then
                            inListbuffer.Add(New MePoint() With {.X = Xout, .Y = Yout})
                        End If
                    End If
                Next
                ListdicOffsetMapData.Add(inListbuffer)
            Else
                If (GetLineString.ToUpper.IndexOf(STR_DATA_TYPE0) >= 0) Then
                    StartData = True
                Else
                    fnAnalysisBasicParameter(GetLineString)
                End If
            End If
        Next
        '轉回 dictionary
        Dim iCountY As Integer = ListdicOffsetMapData.Count

        For indexY = 0 To iCountY - 1
            Dim iCountX As Integer = ListdicOffsetMapData(indexY).Count
            For indexX = 0 To iCountX - 1

                If (Not dicOffsetMapData.ContainsKey(indexX)) Then
                    dicOffsetMapData(indexX) = New Dictionary(Of Integer, MePoint)()
                End If
                If (Not dicOffsetMapData(indexX).ContainsKey(indexY)) Then
                    dicOffsetMapData(indexX)(indexY) = New MePoint()
                End If
                dicOffsetMapData(indexX)(indexY) = New MePoint() With {.X = ListdicOffsetMapData(indexY)(indexX).X, .Y = ListdicOffsetMapData(indexY)(indexX).Y}
            Next
        Next

        For Each item In dicOffsetMapData
            If (item.Value.Count > LoadFileXMax) Then
                LoadFileYMax = item.Value.Count
            End If
        Next
        LoadFileXMax = dicOffsetMapData.Count
    End Sub
    ''' <summary>
    ''' eat CCD Result
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fnAnalysisType1CSV(ByVal lstData As List(Of String))

        dicOffsetMapData = New Dictionary(Of Integer, Dictionary(Of Integer, MePoint))()
        Dim StartData As Boolean = False
        Dim lstCount As Integer = lstData.Count

        For index = 0 To lstCount - 1

            Dim GetLineString As String = lstData(index)

            If (StartData) Then

                Dim mAnalysisData As String = GetLineString
                mAnalysisData = mAnalysisData.Replace("OffsetmmX:,", "")
                mAnalysisData = mAnalysisData.Replace("OffsetmmY:,", "")
                mAnalysisData = mAnalysisData.Replace("OffsetmmX1:,", "") 'Soni / 2017.06.23
                mAnalysisData = mAnalysisData.Replace("OffsetmmY1:,", "")
                mAnalysisData = mAnalysisData.Replace("X:,", "")
                mAnalysisData = mAnalysisData.Replace("Y:,", "")

                Dim SplitSubString As String() = mAnalysisData.Split(",")

                If (SplitSubString.Length >= 4) Then
                    Dim mXn As Integer = 0
                    Dim mYn As Integer = 0
                    Dim mOffsetX As Decimal = 0
                    Dim mOffsetY As Decimal = 0

                    If (Integer.TryParse(SplitSubString(0), mXn) AndAlso Integer.TryParse(SplitSubString(1), mYn) AndAlso Decimal.TryParse(SplitSubString(2), mOffsetX) AndAlso Decimal.TryParse(SplitSubString(3), mOffsetY)) Then

                        If (Not dicOffsetMapData.ContainsKey(mXn)) Then
                            dicOffsetMapData(mXn) = New Dictionary(Of Integer, MePoint)()
                        End If
                        If (Not dicOffsetMapData(mXn).ContainsKey(mYn)) Then
                            dicOffsetMapData(mXn)(mYn) = New MePoint()
                        End If
                        dicOffsetMapData(mXn)(mYn) = New MePoint() With {.X = -mOffsetX, .Y = -mOffsetY}
                    End If

                End If
            Else
                If (GetLineString.ToUpper.IndexOf(STR_DATA_TYPE1) >= 0) Then
                    StartData = True
                Else
                    fnAnalysisBasicParameter(GetLineString)
                End If
            End If
        Next
        For Each item In dicOffsetMapData
            If (item.Value.Count > LoadFileXMax) Then
                LoadFileYMax = item.Value.Count
            End If
        Next
        LoadFileXMax = dicOffsetMapData.Count
    End Sub

#End Region

End Class

#End Region

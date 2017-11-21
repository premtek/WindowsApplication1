Imports ProjectCore

''' <summary>資料項目列舉</summary>
''' <remarks></remarks>
Public Enum eData
    ''' <summary>輸出影像</summary>
    ''' <remarks></remarks>
    OutputImage = 0
    ''' <summary>結果數量
    ''' </summary>
    ''' <remarks></remarks>
    ResultCount = 1
    ''' <summary>特徵1平移X(畫面絕對Pixel)</summary>
    ''' <remarks></remarks>
    TranslationX = 2
    ''' <summary>特徵1平移Y(畫面絕對Pixel)</summary>
    ''' <remarks></remarks>
    TranslationY = 3
    ''' <summary>旋轉</summary>
    ''' <remarks></remarks>
    Rotation = 4
    ''' <summary>分數</summary>
    ''' <remarks></remarks>
    Score = 5
    ''' <summary>結果是否建議使用</summary>
    ''' <remarks></remarks>
    RunStatus_Result = 6
    '''' <summary>相似閥值-目前架構不使用</summary>
    '''' <remarks></remarks>
    'AcceptThreshold = 7
    '''' <summary>相對偏移量X-目前架構不使用</summary>
    '''' <remarks></remarks>
    'PixelOffsetX = 8
    ' ''' <summary>相對偏移量Y-目前架構不使用</summary>
    ' ''' <remarks></remarks>
    'PixelOffsetY = 9
    ' ''' <summary>可辨識的最大數量-目前架構不使用</summary>
    ' ''' <remarks></remarks>
    'MaxNumberToFind = 10
    ''' <summary>
    ''' 特徵2平移X(畫面絕對Pixel)
    ''' </summary>
    ''' <remarks></remarks>
    Item_1_PixelX = 11
    ''' <summary>
    ''' 特徵2平移X(畫面絕對Pixel)
    ''' </summary>
    ''' <remarks></remarks>
    Item_1_PixelY = 12

    ''' <summary>
    ''' 索引用
    ''' </summary>
    ''' <remarks></remarks>
    Max
End Enum


''' <summary>輸出資料結構</summary>
''' <remarks></remarks>
Public Class COutputParam

    ''' <summary>CCD影像寬</summary>
    ''' <remarks></remarks>
    Dim CCDImageWidth As Integer = 1600
    ''' <summary>CCD影像高</summary>
    ''' <remarks></remarks>
    Dim CCDImageHeight As Integer = 1200
    ''' <summary>傳回資料</summary>
    ''' <remarks></remarks>
    Public Data As New List(Of String)

    ''' <summary>讀取設定檔</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean

        Data.Clear()
        Dim mTmpData As String = ""
        mTmpData = ReadIniString("OutputParam", "Data0", fileName, "OutputImage")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data1", fileName, "Results_Count")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data2", fileName, "Results_Item_0_TranslationX")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data3", fileName, "Results_Item_0_TranslationY")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data4", fileName, "Results_Item_0_Rotation")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data5", fileName, "Results_Item_0_Score")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If

        mTmpData = ReadIniString("OutputParam", "Data6", fileName, "RunParams_AcceptThreshold")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If

        mTmpData = ReadIniString("OutputParam", "Data7", fileName, "RunStatus_Result")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data8", fileName, "PixelOffsetX")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data9", fileName, "PixelOffsetY")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data10", fileName, "MaxNumberToFind")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data11", fileName, "Item_1_PixelX")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        mTmpData = ReadIniString("OutputParam", "Data12", fileName, "Item_1_PixelY")
        If mTmpData <> "" Then
            Data.Add(mTmpData)
        End If
        For mDataNo As Integer = eData.Max To 30
            mTmpData = ReadIniString("OutputParam", "Data" & mDataNo, fileName, "")
            If mTmpData <> "" Then
                Data.Add(mTmpData)
            End If
        Next

        Return True
    End Function

    ''' <summary>儲存設定檔</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        For mDataNo As Integer = 0 To Data.Count - 1
            SaveIniString("OutputParam", "Data" & mDataNo, Data(mDataNo), fileName)
        Next
        Return True
    End Function

    Public Function GetCCDDataKeyName(ByVal mData As eData) As String
        Select Case mData
            Case eData.OutputImage '0
                Return "OutputImage"
            Case eData.ResultCount '1
                Return "Results_Count"
            Case eData.TranslationX '2
                Return "Results_Item_0_TranslationX"
            Case eData.TranslationY '3 
                Return "Results_Item_0_TranslationY"
            Case eData.Rotation '4
                Return "Results_Item_0_Rotation"
            Case eData.Score '5
                Return "Results_Item_0_Score"
            Case eData.RunStatus_Result
                Return "RunStatus_Result"
            Case eData.Item_1_PixelX
                Return "Item_1_PixelX"
            Case eData.Item_1_PixelY
                Return "Item_1_PixelY"
        End Select
        Return Nothing
    End Function


    Public Function GetAlignResult(ByRef obj As CThreadCogToolBlock, ByVal sceneName As String, ByVal ccdNo As Integer) As sAlignResult
        Dim mAlignResult As New sAlignResult
        Dim mKeyName As String
        Dim mToolID As String = ""
        Dim mIsToolExist As Boolean = False '工具是否存在 任一參數存在則存在
        '[Note]定位只輸出小數點後六位
        mKeyName = GetCCDDataKeyName(eData.TranslationX) 'Data(eData.TranslationX)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.PixelTranslationX = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
            mIsToolExist = True
        Else
            mAlignResult.PixelTranslationX = Math.Round(CCDImageWidth * 0.5, 6) '預設為畫面中心
        End If
        mKeyName = GetCCDDataKeyName(eData.TranslationY) 'Data(eData.TranslationY)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.PixelTranslationY = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
            mIsToolExist = True
        Else
            mAlignResult.PixelTranslationY = Math.Round(CCDImageHeight * 0.5, 6) '預設為畫面中心
        End If
        

        mKeyName = GetCCDDataKeyName(eData.Rotation) 'Data(eData.Rotation)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.Rotation = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6) '旋轉角度
            mIsToolExist = True
        Else
            mAlignResult.Rotation = 0
        End If

        mKeyName = GetCCDDataKeyName(eData.Score) 'Data(eData.Score)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.Score = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6) '分數
            mIsToolExist = True
        Else
            mAlignResult.Score = -1
        End If

        mKeyName = GetCCDDataKeyName(eData.RunStatus_Result) 'Data(eData.RunStatus_Result)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.Result = obj.Subject.Outputs(mKeyName).Value
        Else
            mAlignResult.Result = enmResultConstants.Warning
        End If

        'mKeyName = Data(eData.AcceptThreshold)
        'If obj.Subject.Outputs.Contains(mKeyName) Then
        '    mAlignResult.AcceptThreshold = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
        'Else
        '    mAlignResult.AcceptThreshold = 0
        'End If

        'mKeyName = Data(eData.PixelOffsetX)
        'If obj.Subject.Outputs.Contains(mKeyName) Then
        '    mAlignResult.PixelOffsetX = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
        'Else
        '    mAlignResult.PixelOffsetX = 0
        'End If

        'mKeyName = Data(eData.PixelOffsetY)
        'If obj.Subject.Outputs.Contains(mKeyName) Then
        '    mAlignResult.PixelOffsetY = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
        'Else
        '    mAlignResult.PixelOffsetY = 0
        'End If
        'mAlignResult.OffsetX = Math.Round(gSSystemParameter.CCDScaleX2X(ccdNo) * mAlignResult.PixelOffsetX + gSSystemParameter.CCDScaleY2X(ccdNo) * mAlignResult.PixelOffsetY, 6) '雙軸向比例校正
        'mAlignResult.OffsetY = Math.Round(gSSystemParameter.CCDScaleX2Y(ccdNo) * mAlignResult.PixelOffsetX + gSSystemParameter.CCDScaleY2Y(ccdNo) * mAlignResult.PixelOffsetY, 6) '雙軸向比例校正


        mAlignResult.AbsOffsetX = Math.Round(gSSystemParameter.CCDScaleX2X(ccdNo) * (mAlignResult.PixelTranslationX - CCDImageWidth * 0.5) + gSSystemParameter.CCDScaleY2X(ccdNo) * (mAlignResult.PixelTranslationY - CCDImageHeight * 0.5), 6)
        mAlignResult.AbsOffsetY = Math.Round(gSSystemParameter.CCDScaleX2Y(ccdNo) * (mAlignResult.PixelTranslationX - CCDImageWidth * 0.5) + gSSystemParameter.CCDScaleY2Y(ccdNo) * (mAlignResult.PixelTranslationY - CCDImageHeight * 0.5), 6)

        'If mAlignResult.Score > mAlignResult.AcceptThreshold And mAlignResult.AcceptThreshold > 0 Then
        '    mAlignResult.Result = enmResultConstants.Accept
        'ElseIf mAlignResult.AcceptThreshold = 0 Then
        '    mAlignResult.Result = enmResultConstants.Accept
        'Else
        '    mAlignResult.Result = enmResultConstants.Reject
        'End If

        'wenda for 精準度測試 20161031
        mKeyName = GetCCDDataKeyName(eData.Item_1_PixelX) ' Data(eData.Item_1_PixelX)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.Item_1_PixelX = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
        Else
            mAlignResult.Item_1_PixelX = 0
        End If
        mKeyName = GetCCDDataKeyName(eData.Item_1_PixelY) ' Data(eData.Item_1_PixelY)
        If obj.Subject.Outputs.Contains(mKeyName) Then
            mAlignResult.Item_1_PixelY = Math.Round(obj.Subject.Outputs(mKeyName).Value, 6)
        Else
            mAlignResult.Item_1_PixelY = 0
        End If
        mAlignResult.Item_1_AbsOffsetX = Math.Round(gSSystemParameter.CCDScaleX2X(ccdNo) * mAlignResult.Item_1_PixelX + gSSystemParameter.CCDScaleY2X(ccdNo) * mAlignResult.Item_1_PixelY, 6) '雙軸向比例校正
        mAlignResult.Item_1_AbsOffsetY = Math.Round(gSSystemParameter.CCDScaleX2Y(ccdNo) * mAlignResult.Item_1_PixelX + gSSystemParameter.CCDScaleY2Y(ccdNo) * mAlignResult.Item_1_PixelY, 6) '雙軸向比例校正


        'wenda 20160614
        'For mDataNo As Integer = eData.Max To Data.Count - 1
        '    mKeyName = Data(mDataNo)
        '    If obj.Subject.Outputs.Contains(Data(mDataNo)) Then
        '    mAlignResult.PixelOffsetX = obj.Subject.Outputs(mKeyName).Value - CCDImageWidth * 0.5 '對CCD中心偏移量
        '        mIsToolExist = True
        '    End If
        'Next

        If mIsToolExist Then ' 任一參數存在則存在
            Return mAlignResult
        Else
            Return Nothing
        End If

    End Function

End Class
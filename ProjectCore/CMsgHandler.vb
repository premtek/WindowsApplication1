Imports System.Text.RegularExpressions
Imports System.Data.OleDb

''' <summary>
''' 訊息結構
''' </summary>
''' <remarks></remarks>
Public Structure MessageStructure
    ''' <summary>
    ''' 程式內部名稱
    ''' </summary>
    ''' <remarks></remarks>
    Public ProgramID As String
    ''' <summary>
    ''' 錯誤代碼
    ''' </summary>
    ''' <remarks></remarks>
    Public ALID As String
    ''' <summary>
    ''' ITRI參數???
    ''' </summary>
    ''' <remarks></remarks>
    Public CD As String
    ''' <summary>
    ''' 是否使用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enabled As String
    ''' <summary>語系訊息1</summary>
    ''' <remarks></remarks>
    Public Msg1 As String
    ''' <summary>語系訊息2</summary>
    ''' <remarks></remarks>
    Public Msg2 As String
    ''' <summary>語系訊息3</summary>
    ''' <remarks></remarks>
    Public Msg3 As String
    ' ''' <summary>
    ' ''' 警報層級
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Level As String
End Structure

''' <summary>多語系處理</summary>
''' <remarks></remarks>
Public Class CMsgHandler
    Implements IDisposable

    ''' <summary>文字檔讀取工具</summary>
    ''' <remarks></remarks>
    Dim mStreamReader As System.IO.StreamReader
    ''' <summary>
    ''' 統整訊息字典
    ''' </summary>
    ''' <remarks></remarks>
    Public MsgDictionary As New Dictionary(Of Integer, MessageStructure)

    ''' <summary>選擇語系(預設英文)</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedLanguage As enmLanguageType = enmLanguageType.eEnglish

    ' ''' <summary>
    ' ''' 讀取訊息層級設定
    ' ''' </summary>
    ' ''' <param name="fileName"></param>
    ' ''' <remarks></remarks>
    'Public Function LoadLevel(ByVal fileName As String) As Boolean
    '    Try
    '        If Not System.IO.File.Exists(fileName) Then
    '            MsgBox("Msg Level File Lost...")
    '            Return False
    '        End If
    '        Dim mStreamReader As New System.IO.StreamReader(fileName, System.Text.Encoding.Unicode)
    '        Dim mData As String = ""
    '        Do
    '            mData = mStreamReader.ReadLine
    '            If mData Is Nothing Then
    '                MsgBox("Msg Level File is Empty...")
    '                Return False
    '            End If
    '            If Not mData.StartsWith("//") Then '不是標頭
    '                Dim pattern2 As String = "(?:^|,)(?=[^""]|("")?)""?((?(1)[^""]*|[^,""]*))""?(?=,|$)"
    '                Dim mRegEx As New Regex(pattern2) 'Compiled速度會更慢..., System.Text.RegularExpressions.RegexOptions.Compiled + RegexOptions.Singleline + RegexOptions.IgnoreCase)
    '                Dim matches As MatchCollection = mRegEx.Matches(mData)
    '                If matches.Count > 1 Then
    '                    Dim msgID As String = matches(0).Value.TrimStart(",")
    '                    Dim level As String = matches(1).Value.TrimStart(",")
    '                    If MsgDictionary.ContainsKey(msgID) Then
    '                        Dim tmp As MessageStructure = MsgDictionary(msgID)
    '                        tmp.Level = level
    '                        MsgDictionary(msgID) = tmp
    '                    End If

    '                End If
    '            End If
    '        Loop Until mStreamReader.EndOfStream
    '        mStreamReader.Close()
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message, vbOKOnly)
    '        Return False
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 寫入訊息層級設定
    ' ''' </summary>
    ' ''' <param name="fileName"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function SaveLevel(ByVal fileName As String) As Boolean
    '    Try
    '        Dim mStreamWriter As New System.IO.StreamWriter(fileName, False, System.Text.Encoding.Unicode)
    '        Dim mData As String = ""
    '        For mRow As Integer = 0 To MsgDictionary.Keys.Count - 1
    '            Dim ALID As String = MsgDictionary.Keys(mRow)
    '            Dim level As String = ""
    '            Select Case MsgDictionary(ALID).Level
    '                Case "Heavy", "Alarm"
    '                    level = "Alarm"
    '                Case "Light", "Warn"
    '                    level = "Warn"
    '            End Select
    '            mData += ALID & "," & MsgDictionary(ALID).Level & vbCrLf
    '        Next
    '        mStreamWriter.WriteLine(mData) '整批寫入
    '        mStreamWriter.Close()
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message, vbOKOnly)
    '        Return False
    '    End Try
    'End Function

    ''' <summary>讀取訊息語系設定</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Dim data As String = ""

        Try
            If Not System.IO.File.Exists(fileName) Then
                MsgBox("Multi-Language File Lost...", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
            'CSV檔案開啟只支援ANSI,Big5格式
            'StreamReader開ANSI會變亂碼.
            mStreamReader = New System.IO.StreamReader(fileName, System.Text.Encoding.Unicode)

            Do

                data = mStreamReader.ReadLine
                If data = Nothing Then
                ElseIf Not data.StartsWith("//") Then '不是標頭
                    Dim pattern As String = "[^"",]+|""(?:[^""]|"")*"
                    Dim pattern2 As String = "(?:^|,)(?=[^""]|("")?)""?((?(1)[^""]*|[^,""]*))""?(?=,|$)"
                    Dim mRegEx As New Regex(pattern2) 'Compiled速度會更慢..., System.Text.RegularExpressions.RegexOptions.Compiled + RegexOptions.Singleline + RegexOptions.IgnoreCase)
                    Dim matches As MatchCollection = mRegEx.Matches(data)
                    'Dim splitedData() As String = data.Split(",")
                    Dim progID As String = matches(0).Value.TrimStart(",")
                    Dim msgID As String = matches(1).Value.TrimStart(",")
                    'CD
                    Dim enabled As String = matches(3).Value.TrimStart(",")
                    Dim msg1 As String = ""
                    Dim msg2 As String = ""
                    Dim msg3 As String = ""
                    If matches.Count > 4 Then
                        msg1 = matches(4).Value.TrimStart(",")
                    End If
                    If matches.Count > 5 Then
                        msg2 = matches(5).Value.TrimStart(",")
                    End If
                    If matches.Count > 6 Then
                        msg3 = matches(6).Value.TrimStart(",")
                    End If
                    If MsgDictionary.ContainsKey(msgID) Then
                        Dim tmp As MessageStructure = MsgDictionary(msgID)
                        tmp.ProgramID = progID
                        tmp.ALID = msgID
                        tmp.Enabled = enabled
                        tmp.Msg1 = msg1
                        tmp.Msg2 = msg2
                        tmp.Msg3 = msg3
                        MsgDictionary(msgID) = tmp
                    Else
                        Dim tmp As New MessageStructure
                        tmp.ProgramID = progID
                        tmp.ALID = msgID
                        tmp.Enabled = enabled
                        tmp.Msg1 = msg1
                        tmp.Msg2 = msg2
                        tmp.Msg3 = msg3
                        MsgDictionary.Add(msgID, tmp)
                    End If

                End If

            Loop Until mStreamReader.EndOfStream
            'For i As Integer = 0 To MsgDictionary.Count - 1
            '    Debug.Print("語系:" & MsgDictionary.Keys(i) & vbTab & MsgDictionary(MsgDictionary.Keys(i)).Msg1 & vbTab & MsgDictionary(MsgDictionary.Keys(i)).Msg2 & vbTab & MsgDictionary(MsgDictionary.Keys(i)).Msg3)
            'Next
            mStreamReader.Close()
            Return True
        Catch ex As Exception
            MsgBox(data & vbCrLf & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("CMsgHandler. Exception Message:" & ex.Message, , eMessageLevel.Alarm)
            Return False
        End Try

    End Function

    ''' <summary>儲存語系設定檔</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Try
            Dim mStreamWriter As New System.IO.StreamWriter(fileName, False, System.Text.Encoding.Unicode)
            Dim mData As String = "// Name,ID,CD,Enabled,Text,Cht,," '& vbCrLf
            mStreamWriter.WriteLine(mData) '寫入
            For mRow As Integer = 0 To MsgDictionary.Keys.Count - 1
                Dim ALID As String = MsgDictionary.Keys(mRow)
                mData = MsgDictionary(ALID).ProgramID & "," & ALID & ",," & MsgDictionary(ALID).Enabled & "," & MsgDictionary(ALID).Msg1 & "," & MsgDictionary(ALID).Msg2 & "," & MsgDictionary(ALID).Msg3 & ","
                mStreamWriter.WriteLine(mData) '寫入
            Next

            mStreamWriter.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function

    ''' <summary>取得訊息</summary>
    ''' <param name="msgId">訊息代碼</param>
    ''' <param name="arg">參數, 如果有的話</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMessage(ByVal msgId As Integer, ByVal ParamArray arg() As String) As String
        Select Case SelectedLanguage
            Case enmLanguageType.eEnglish
                If MsgDictionary.ContainsKey(msgId) Then
                    Return String.Format(MsgDictionary(msgId).Msg1, arg)
                Else
                    Return ""
                End If

            Case enmLanguageType.eTraditionalChinese
                If MsgDictionary.ContainsKey(msgId) Then
                    Return String.Format(MsgDictionary(msgId).Msg2, arg)
                Else
                    Return ""
                End If

            Case enmLanguageType.eSimplifiedChinese
                If MsgDictionary.ContainsKey(msgId) Then
                    Return String.Format(MsgDictionary(msgId).Msg3, arg)
                Else
                    Return ""
                End If
        End Select
        Return ""
    End Function

    'Eason 20170228
    Sub New()
        System.Threading.ThreadPool.SetMinThreads(150, 150) 'Eason 20170228
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mStreamReader.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

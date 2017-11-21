Imports System.Drawing
Imports System.IO
Imports System.Globalization

Public Class clsMapData
    Dim Path As String
    Public Information As New Information
    Public Substrates() As Substrate
    Public Direction As enmDirection = enmDirection.Bottom
    Private IsCut As Boolean

    Enum enmDirection
        Bottom = 0
        Right = 1
        Top = 2
        Left = 3
    End Enum

    Enum enmMark
        WaferMap
        CycleMap
    End Enum

    Sub New()

    End Sub

    ''' <summary>
    ''' 開啟Map data
    ''' </summary>
    Public Function OpenFile(ByVal path As String) As Boolean
        Me.Path = path
        If (MapFileAnalyze(Me.Path, Substrates)) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定Notch方向, 使陣列旋轉
    ''' </summary>
    ''' <param name="direction">方向</param>
    Public Sub ResetMapNotch(ByVal direction As enmDirection)
        Dim index As Integer = Me.Direction - direction
        If index = -3 Then
            index = 1
        ElseIf index = -2 Then
            index = 2
        ElseIf index = -1 Then
            index = 3
        End If

        If (index <> 0) Then
            For i = 0 To index - 1
                For k = 0 To Substrates.Length - 1
                    Substrates(k).DieArray = RotateMatrixCounterClockwise(Substrates(k).DieArray)
                Next
            Next
            Me.Direction = direction
        End If
    End Sub

    ''' <summary>
    ''' 陣列切割
    ''' </summary>
    ''' <returns>例 : 5x3陣列, column = 2 則分為 2x3 與 3x3 陣列</returns>
    Public Function MatrixCut(ByVal column As Integer) As Boolean
        Try
            If (Substrates(0).DieArray.GetUpperBound(0) >= column - 1) Then
                Dim stage1 As New Substrate(Substrates(0))
                Dim stage2 As New Substrate(Substrates(0))

                ReDim stage1.DieArray(column - 1, Substrates(0).DieArray.GetUpperBound(1))
                ReDim stage2.DieArray(Substrates(0).DieArray.GetUpperBound(0) - column, Substrates(0).DieArray.GetUpperBound(1))

                Array.Copy(Substrates(0).DieArray, stage1.DieArray, stage1.DieArray.Length)
                Array.Copy(Substrates(0).DieArray, stage1.DieArray.Length, stage2.DieArray, 0, Substrates(0).DieArray.Length - stage1.DieArray.Length)

                ReDim Substrates(1)
                Substrates(0) = stage1
                Substrates(1) = stage2

                IsCut = True
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 陣列合併
    ''' </summary>
    ''' <remarks>使用MatrixCut函式切割後之陣列後才能使用此函式合併 : 合併Substrates(0)與Substrates(1)陣列</remarks>
    Public Function MatrixMerge() As Boolean
        If IsCut Then
            Dim s1 As Substrate = New Substrate(Substrates(0))
            Dim s2 As Substrate = New Substrate(Substrates(1))
            Dim s3 As Substrate = New Substrate(Substrates(0))
            ReDim Substrates(0)
            Substrates(0) = s3
            ReDim Substrates(0).DieArray(s1.Columns + s2.Columns - 1, s1.DieArray.GetUpperBound(1))
            Array.Copy(s1.DieArray, Substrates(0).DieArray, s1.DieArray.Length)
            Array.Copy(s2.DieArray, 0, Substrates(0).DieArray, s1.DieArray.Length, s2.DieArray.Length)
            IsCut = False
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 合併Log Data
    ''' </summary>
    Public Function MergeLogData(ByVal logPath As String) As Boolean

        Return False
    End Function

    ''' <summary>
    ''' 匯出PII格式之Map data
    ''' </summary>
    ''' <remarks>檔案不存在則自動建立</remarks>
    Public Function OutputPiiMap(ByVal path As String) As Boolean
        Try
            If File.Exists(path) Then
                File.Delete(path)
            End If

            Using fs As FileStream = File.Create(path)
            End Using

            '合併stage1, stage2
            If IsCut Then
                MatrixMerge()
            End If

            Using sw As StreamWriter = New StreamWriter(path)
                sw.WriteLine("[WAFER INFORMATION]")
                sw.WriteLine("VERSION," & Information.Version)
                sw.WriteLine("LOT ID," & Information.LotID)
                sw.WriteLine("PRODUCT ID," & Information.ProductID)
                sw.WriteLine("DIE SIZE X," & Information.DieSizeX)
                sw.WriteLine("DIE SIZE Y," & Information.DieSizeY)
                sw.WriteLine("COLUMN," & Information.Column)
                sw.WriteLine("ROW," & Information.Row)
                sw.WriteLine("CHECK IN TIME," & Information.CheckInTime.ToString("yyyy/MM/dd HH:mm"))
                sw.WriteLine("CHECK OUT TIME," & Information.CheckOutTime.ToString("yyyy/MM/dd HH:mm"))
                sw.WriteLine("TOTAL TIME," & Information.TotalTime.ToString("yyyy/MM/dd HH:mm"))
                sw.WriteLine("RECIPE NAME," & Information.RecipeName)
                sw.WriteLine("TYPE," & Information.Type)
                sw.WriteLine("PITCH," & Information.Pitch)
                sw.WriteLine("USER," & Information.User)
                sw.WriteLine("MACHINE TYPE," & Information.MachineType)
                If (Direction = enmDirection.Top) Then
                    Information.Notch = 0
                ElseIf (Direction = enmDirection.Right) Then
                    Information.Notch = 90
                ElseIf (Direction = enmDirection.Bottom) Then
                    Information.Notch = 180
                ElseIf (Direction = enmDirection.Left) Then
                    Information.Notch = 270
                End If
                sw.WriteLine("NOTCH," & Information.Notch)

                WriteMap(Substrates(0), sw)

                sw.WriteLine("[EXPLAIN]")
                sw.WriteLine(".,EMPTY")
                sw.WriteLine("X,BAD DIE")
                sw.WriteLine("0~FF,[WAFER MAP] BIN NUMBER")
                sw.WriteLine("0~FF,[CYCLE MAP] CYCLE TIME")
                sw.WriteLine("@,NOTCH")
                sw.Close()
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 匯出ASE格式之Map data
    ''' </summary>
    ''' <remarks>檔案不存在則自動建立</remarks>
    Public Function OutputAseMap(ByVal path As String) As Boolean
        Try
            If File.Exists(path) Then
                File.Delete(path)
            End If

            Using fs As FileStream = File.Create(path)
            End Using

            Dim data As String
            Using sw As StreamWriter = New StreamWriter(path)
                For row = 0 To Substrates(0).Rows - 1
                    data = ""
                    For column = 0 To Substrates(0).Columns - 1
                        data += Substrates(0).DieArray(column, row).Bin
                    Next
                    sw.WriteLine(data)
                Next
                sw.Close()
                Return True
            End Using
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' PII格式轉ASE格式
    ''' </summary>
    ''' <param name="source">PII map file 路徑</param>
    ''' <param name="export">ASE map file 路徑</param>
    ''' <remarks>ASE map file 不存在則自動建立</remarks>
    Public Function PiiMap2AseMap(ByVal source As String, ByVal export As String) As Boolean
        Try
            Dim mySubstrates() As Substrate = {}
            If (MapFileAnalyze(source, mySubstrates)) Then
                If File.Exists(export) Then
                    File.Delete(export)
                End If

                Using fs As FileStream = File.Create(export)
                End Using

                Dim data As String
                Using sw As StreamWriter = New StreamWriter(export)
                    For row = 0 To mySubstrates(0).Rows - 1
                        data = ""
                        For column = 0 To mySubstrates(0).Columns - 1
                            data += mySubstrates(0).DieArray(column, row).Bin
                        Next
                        sw.WriteLine(data)
                    Next
                    sw.Close()
                    Return True
                End Using
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 匯出ASE格式之Cycle data
    ''' </summary>
    ''' <remarks>檔案不存在則自動建立</remarks>
    Public Function OutputCycleMap(ByVal path As String) As Boolean
        Try
            If File.Exists(path) Then
                File.Delete(path)
            End If

            Using fs As FileStream = File.Create(path)
            End Using

            Dim data As String
            Using sw As StreamWriter = New StreamWriter(path)
                For row = 0 To Substrates(0).Rows - 1
                    data = ""
                    For column = 0 To Substrates(0).Columns - 1
                        data += Substrates(0).DieArray(column, row).Cycle
                    Next
                    sw.WriteLine(data)
                Next
                sw.Close()
                Return True
            End Using
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 匯出Rerun Map, 包含Bin 與 Cycle 資料 
    ''' </summary>
    ''' <remarks>含標頭[Bin Data], [Round Data]</remarks>
    Public Function OutputdRerunMap(ByVal path As String) As Boolean
        Try
            If File.Exists(path) Then
                File.Delete(path)
            End If

            Using fs As FileStream = File.Create(path)
            End Using

            Dim data As String
            Using sw As StreamWriter = New StreamWriter(path)
                'Bin 資料
                sw.WriteLine("[Bin Data]")
                For row = 0 To Substrates(0).Rows - 1
                    data = ""
                    For column = 0 To Substrates(0).Columns - 1
                        data += Substrates(0).DieArray(column, row).Bin
                    Next
                    sw.WriteLine(data)
                Next

                'Cycle 資料
                sw.WriteLine("[Round Data]")
                For row = 0 To Substrates(0).Rows - 1
                    data = ""
                    For column = 0 To Substrates(0).Columns - 1
                        data += Substrates(0).DieArray(column, row).Cycle
                    Next
                    sw.WriteLine(data)
                Next

                sw.Close()
                Return True
            End Using
            Return False
        Catch ex As Exception
            MsgBox("OutputdRerunMap error: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ASE格式轉PII格式
    ''' </summary>
    ''' <param name="source">ASE map file 路徑</param>
    ''' <param name="export">PII map file 路徑</param>
    ''' <remarks>PII map file 不存在則自動建立</remarks>
    Private Function AseMap2PiiMap(ByVal source As String, ByVal export As String) As Boolean
        Try
            If File.Exists(export) Then
                File.Delete(export)
            End If

            Using fs As FileStream = File.Create(export)
            End Using

            Using sw As StreamWriter = New StreamWriter(export)
                Dim aseData(10000) As String
                Dim column As Integer
                Dim row As Integer
                Dim line As String

                Using sr As StreamReader = New StreamReader(source)
                    While True
                        line = sr.ReadLine
                        If line IsNot Nothing Then
                            If (row = 0) Then
                                Dim arrStr() As String = sr.ReadLine().Split(",")
                                column = arrStr.Length
                            End If
                            aseData(row) = line
                            row += 1
                        Else
                            row += 1
                            Exit While
                        End If
                    End While
                End Using

                ' build "WAFER INFORMATION" 
                sw.WriteLine("[WAFER INFORMATION]")
                sw.WriteLine("VERSION,Ver9.3")
                sw.WriteLine("LOT ID,N/A")
                sw.WriteLine("PRODUCT ID,N/A")
                sw.WriteLine("DIE SIZE X,N/A")
                sw.WriteLine("DIE SIZE Y,N/A")
                sw.WriteLine("COLUMN," & column)
                sw.WriteLine("ROW," & row)
                sw.WriteLine("CHECK IN TIME,N/A")
                sw.WriteLine("CHECK OUT TIME,N/A")
                sw.WriteLine("TOTAL TIME,N/A")
                sw.WriteLine("RECIPE NAME,N/A")
                sw.WriteLine("TYPE,N/A")
                sw.WriteLine("PITCH,N/A")
                sw.WriteLine("USER,N/A")
                sw.WriteLine("MACHINE TYPE,N/A")
                sw.WriteLine("NOTCH,180")

                ' build "WAFER MAP" 
                Dim str As String
                sw.WriteLine("[WAFER MAP]")
                For c = 0 To column - 1
                    str = "COLUMN" & "," & (c + 1).ToString()
                    sw.WriteLine(str)
                Next
                For Each myData As String In aseData
                    If (myData IsNot Nothing) Then
                        sw.WriteLine(myData)
                    Else
                        Exit For
                    End If
                Next

                ' build "CYCLE MAP" 
                sw.WriteLine("[CYCLE MAP]")
                For c = 0 To column - 1
                    str = "COLUMN" & "," & (c + 1).ToString()
                    sw.WriteLine(str)
                Next
                For Each myData As String In aseData
                    If (myData IsNot Nothing) Then
                        Dim myStr() As String = myData.Split(",")
                        myData = "ROW"
                        For g = 0 To column - 1
                            If (myStr(g) = ".") Then
                                myData += ",."
                            Else
                                myData += ",0"
                            End If
                        Next
                        sw.WriteLine(myData)
                    Else
                        Exit For
                    End If
                Next

                '' build "EXPLAIN" 
                sw.WriteLine("[EXPLAIN]")
                sw.WriteLine(".,EMPTY")
                sw.WriteLine("X,BAD DIE")
                sw.WriteLine("0~FF,[WAFER MAP] BIN NUMBER")
                sw.WriteLine("0~FF,[CYCLE MAP] CYCLE TIME")
                sw.WriteLine("N,NOTCH")
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 產生一個PII Base Map Data
    ''' </summary>
    ''' <param name="column">Column</param>
    ''' <param name="row">Row</param>
    ''' <param name="path">檔案產生路徑</param>
    Public Function CreateBaseMapData(ByVal column As Integer, ByVal row As Integer, ByVal path As String) As Boolean
        Try
            If File.Exists(path) Then
                File.Delete(path)
            End If

            Using fs As FileStream = File.Create(path)
            End Using

            Dim inf As New Information
            Dim base(0) As Substrate
            base(0) = New Substrate
            ReDim base(0).DieArray(column - 1, row - 1)
            For c As Integer = 0 To column - 1
                For r As Integer = 0 To row - 1
                    base(0).DieArray(c, r) = New Die
                Next
            Next

            Using sw As StreamWriter = New StreamWriter(path)
                sw.WriteLine("[WAFER INFORMATION]")
                sw.WriteLine("VERSION," & inf.Version)
                sw.WriteLine("LOT ID," & inf.LotID)
                sw.WriteLine("PRODUCT ID," & inf.ProductID)
                sw.WriteLine("DIE SIZE X," & inf.DieSizeX)
                sw.WriteLine("DIE SIZE Y," & inf.DieSizeY)
                sw.WriteLine("COLUMN," & column)
                sw.WriteLine("ROW," & row)
                sw.WriteLine("CHECK IN TIME," & inf.CheckInTime.ToString("yyyy/MM/dd HH:mm"))
                sw.WriteLine("CHECK OUT TIME," & inf.CheckOutTime.ToString("yyyy/MM/dd HH:mm"))
                sw.WriteLine("TOTAL TIME," & inf.TotalTime.ToString("yyyy/MM/dd HH:mm"))
                sw.WriteLine("RECIPE NAME," & inf.RecipeName)
                sw.WriteLine("TYPE," & inf.Type)
                sw.WriteLine("PITCH," & inf.Pitch)
                sw.WriteLine("USER," & inf.User)
                sw.WriteLine("MACHINE TYPE," & inf.MachineType)
                sw.WriteLine("NOTCH," & inf.Notch)

                WriteMap(base(0), sw)

                sw.WriteLine("[EXPLAIN]")
                sw.WriteLine(".,EMPTY")
                sw.WriteLine("X,BAD DIE")
                sw.WriteLine("0~FF,[WAFER MAP] BIN NUMBER")
                sw.WriteLine("0~FF,[CYCLE MAP] CYCLE TIME")
                sw.WriteLine("@,NOTCH")

                sw.Close()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>[將客戶端Wafer Map轉成PII Map(Cray)]</summary>
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

            Using sr As StreamReader = New StreamReader(ReadFileName)
                Dim arrStr() As String = sr.ReadLine().Split(" ")
                iColumnCount = arrStr(0).Length
            End Using

            cLine = objReader.ReadLine()
            Dim Die(65536, cLine.Length) As Char

            '' calculate Row number & column number
            Do
                For j = 0 To cLine.Length - 1
                    Die(iRowCount, j) = cLine(j)
                Next
                iRowCount += 1
                cLine = objReader.ReadLine()
            Loop Until cLine.Length = 0
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
            Return False
        End Try
    End Function


    Private Sub AddMap(ByVal mark As enmMark, ByVal sw As StreamWriter)
        Dim column As Integer = Substrates(0).DieArray.GetUpperBound(0)
        Dim row As Integer = Substrates(0).DieArray.GetUpperBound(1)
        Dim line As String

        Select Case mark
            Case enmMark.WaferMap
                sw.WriteLine("[WAFER MAP]")
            Case enmMark.CycleMap
                sw.WriteLine("[CYCLE MAP]")
        End Select


        line = "COLUMN,"
        For i = 0 To column
            line = line & (i + 1).ToString() & ","
        Next
        line = line.Remove(line.Length - 1, 1)
        sw.WriteLine(line)

        line = ""
        For r = 0 To row
            line = "ROW" & (r + 1).ToString() & ","
            For c = 0 To column
                Dim temp As String = ""
                Select Case mark
                    Case enmMark.WaferMap
                        temp = Substrates(0).DieArray(c, r).Bin
                    Case enmMark.CycleMap
                        temp = Substrates(0).DieArray(c, r).Cycle
                End Select
                line = line & temp & ","
            Next
            line = line.Remove(line.Length - 1, 1)
            sw.WriteLine(line)
        Next
        sw.Dispose()
    End Sub

    Private Sub WriteMap(ByVal substrate As Substrate, ByVal sw As StreamWriter)
        Dim column As Integer = substrate.DieArray.GetUpperBound(0)
        Dim row As Integer = substrate.DieArray.GetUpperBound(1)
        Dim line As String
        Dim arrMark() As enmMark = {enmMark.WaferMap, enmMark.CycleMap}

        For Each mark As enmMark In arrMark
            Select Case mark
                Case enmMark.WaferMap
                    sw.WriteLine("[WAFER MAP]")
                Case enmMark.CycleMap
                    sw.WriteLine("[CYCLE MAP]")
            End Select

            line = "COLUMN,"
            For i = 0 To column
                line = line & (i + 1).ToString() & ","
            Next
            line = line.Remove(line.Length - 1, 1)
            sw.WriteLine(line)

            line = ""
            For r = 0 To row
                line = "ROW" & (r + 1).ToString() & ","
                For c = 0 To column
                    Dim temp As String = ""
                    Select Case mark
                        Case enmMark.WaferMap
                            temp = substrate.DieArray(c, r).Bin
                        Case enmMark.CycleMap
                            temp = substrate.DieArray(c, r).Cycle
                    End Select
                    line = line & temp & ","
                Next
                line = line.Remove(line.Length - 1, 1)
                sw.WriteLine(line)
            Next
        Next
        sw.Dispose()
    End Sub

    ''' <summary>
    ''' 常數轉換為Map data代號
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IntToMark(ByVal i As Integer) As String
        If (i = -1) Then
            Return "."
        ElseIf (i = -2) Then
            Return "X"
        ElseIf (i = -3) Then
            Return "@"
        Else
            Return i.ToString()
        End If
    End Function

    ''' <summary>
    ''' 矩陣順時鐘旋轉90度
    ''' </summary>
    Private Function RotateMatrixCounterClockwise(oldMatrix As String(,)) As String(,)
        Dim newMatrix As String(,) = New String(oldMatrix.GetLength(1) - 1, oldMatrix.GetLength(0) - 1) {}
        Dim newColumn As String, newRow As String = 0
        For oldColumn As Integer = oldMatrix.GetLength(1) - 1 To 0 Step -1
            newColumn = 0
            For oldRow As Integer = 0 To oldMatrix.GetLength(0) - 1
                newMatrix(newRow, newColumn) = oldMatrix(oldRow, oldColumn)
                newColumn += 1
            Next
            newRow += 1
        Next
        Return newMatrix
    End Function

    ''' <summary>
    ''' 矩陣順時鐘旋轉90度
    ''' </summary>
    Private Function RotateMatrixCounterClockwise(oldMatrix As Die(,)) As Die(,)
        Dim newMatrix As Die(,) = New Die(oldMatrix.GetLength(1) - 1, oldMatrix.GetLength(0) - 1) {}
        Dim newColumn As String, newRow As String = 0
        For oldColumn As Integer = oldMatrix.GetLength(1) - 1 To 0 Step -1
            newColumn = 0
            For oldRow As Integer = 0 To oldMatrix.GetLength(0) - 1
                newMatrix(newRow, newColumn) = oldMatrix(oldRow, oldColumn)
                newColumn += 1
            Next
            newRow += 1
        Next
        Return newMatrix
    End Function

    'Dim substrate As New Substrate
    'Dim die As New Die
    'Dim die2 As New Die
    'Substrates = New Substrate(5) {}
    'Substrates(0) = substrate
    'Substrates(0).ID = "d1-0"
    'Substrates(0).DieArray = New Die(4, 3) {}
    'Substrates(0).DieArray(2, 2) = die
    'Substrates(0).DieArray(2, 2).ID = "d2-22"
    'Substrates(0).DieArray(2, 2).DieArray = New Die(1, 1) {}
    'Substrates(0).DieArray(2, 2).DieArray(0, 0) = die2
    'Substrates(0).DieArray(2, 2).DieArray(0, 0).ID = "d3-00"

    ''' <summary>
    ''' Map file 解析
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="substrates"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MapFileAnalyze(ByVal path As String, ByRef substrates() As Substrate) As Boolean
        Try
            'Dim checkFile As Boolean
            Dim waferMapList As New List(Of String())()
            Dim waferCycleList As New List(Of String())()

            Dim data As String
            Dim index As Integer
            Using sReader As StreamReader = New StreamReader(path)
                While True
                    data = sReader.ReadLine
                    If data IsNot Nothing Then
                        Dim arrayStr As String() = data.Split(",")

                        'If (checkFile = False) Then
                        '    '判斷檔案是否為PII Data
                        '    If (arrayStr(0) <> "[WAFER INFORMATION]") Then
                        '        Return False
                        '    End If
                        '    checkFile = True
                        'End If

                        If (arrayStr(0) = "[WAFER INFORMATION]") Then
                            index = 0
                        ElseIf (arrayStr(0) = "[WAFER MAP]") Then
                            index = 1
                        ElseIf (arrayStr(0) = "[CYCLE MAP]") Then
                            index = 2
                        ElseIf (arrayStr(0) = "[EXPLAIN]") Then
                            index = 99
                        End If

                        Select Case index
                            Case 0
                                Select Case arrayStr(0)
                                    Case "VERSION"
                                        Information.Version = arrayStr(1)
                                    Case "LOT ID"
                                        Information.LotID = arrayStr(1)
                                    Case "PRODUCT ID"
                                        Information.ProductID = arrayStr(1)
                                    Case "DIE SIZE X"
                                        If (IsNumeric(arrayStr(1))) Then
                                            Information.DieSizeX = Convert.ToInt32(arrayStr(1))
                                        Else
                                            Information.DieSizeX = 0
                                        End If

                                    Case "DIE SIZE Y"
                                        If (IsNumeric(arrayStr(1))) Then
                                            Information.DieSizeY = Convert.ToInt32(arrayStr(1))
                                        Else
                                            Information.DieSizeY = 0
                                        End If

                                    Case "COLUMN"
                                        Information.Column = Convert.ToInt32(arrayStr(1))
                                    Case "ROW"
                                        Information.Row = Convert.ToInt32(arrayStr(1))
                                    Case "CHECK IN TIME"
                                        If (IsDate(arrayStr(1))) Then
                                            Information.CheckInTime = Convert.ToDateTime(arrayStr(1))
                                        Else
                                            Information.CheckInTime = Nothing
                                        End If

                                    Case "CHECK OUT TIME"
                                        If (IsDate(arrayStr(1))) Then
                                            Information.CheckOutTime = Convert.ToDateTime(arrayStr(1))
                                        Else
                                            Information.CheckOutTime = Nothing
                                        End If

                                    Case "TOTAL TIME"
                                        If (IsDate(arrayStr(1))) Then
                                            Information.TotalTime = Convert.ToDateTime(arrayStr(1))
                                        Else
                                            Information.TotalTime = Nothing
                                        End If

                                    Case "RECIPE NAME"
                                        Information.RecipeName = arrayStr(1)
                                    Case "TYPE"
                                        Information.Type = arrayStr(1)
                                    Case "PITCH"
                                        If (IsNumeric(arrayStr(1))) Then
                                            Information.Pitch = Convert.ToInt32(arrayStr(1))
                                        Else
                                            Information.Pitch = 0
                                        End If

                                    Case "USER"
                                        Information.User = arrayStr(1)
                                    Case "MACHINE TYPE"
                                        Information.MachineType = arrayStr(1)
                                    Case "NOTCH"
                                        If (IsNumeric(arrayStr(1))) Then
                                            Information.Notch = Convert.ToInt32(arrayStr(1))
                                            If (Information.Notch = 0) Then
                                                Direction = enmDirection.Top
                                            ElseIf (Information.Notch = 90) Then
                                                Direction = enmDirection.Right
                                            ElseIf (Information.Notch = 180) Then
                                                Direction = enmDirection.Bottom
                                            ElseIf (Information.Notch = 270) Then
                                                Direction = enmDirection.Left
                                            Else
                                                Direction = enmDirection.Bottom
                                            End If
                                        Else
                                            Direction = enmDirection.Bottom
                                        End If

                                End Select
                            Case 1
                                waferMapList.Add(arrayStr)
                            Case 2
                                waferCycleList.Add(arrayStr)
                        End Select
                    Else
                        Exit While
                    End If
                End While
            End Using

            ReDim substrates(0)
            substrates(0) = New Substrate
            ReDim substrates(0).DieArray(Information.Column - 1, Information.Row - 1)
            For c As Integer = 0 To Information.Column - 1
                For r As Integer = 0 To Information.Row - 1
                    substrates(0).DieArray(c, r) = New Die
                Next
            Next

            If ListToArrayBin(substrates(0).DieArray, waferMapList) AndAlso ListToArrayCycle(substrates(0).DieArray, waferCycleList) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ListToArray(ByRef array As String(,), ByVal list As List(Of String())) As Boolean
        Try
            Dim colnums = array.GetUpperBound(0)
            Dim rows = array.GetUpperBound(1)

            If (colnums = list(1).Count - 2) Then
                For r = 0 To rows
                    For c = 0 To colnums
                        array(c, r) = list(r + 2).GetValue(c + 1)
                    Next
                Next
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ListToArrayBin(ByRef array As Die(,), ByVal list As List(Of String())) As Boolean
        Try
            Dim colnums = array.GetUpperBound(0)
            Dim rows = array.GetUpperBound(1)

            If (colnums = list(1).Count - 2) Then
                For r = 0 To rows
                    For c = 0 To colnums
                        'If IsNumeric(list(r + 2).GetValue(c + 1)) Then
                        '    array(c, r).Bin = list(r + 2).GetValue(c + 1).ToString()
                        'Else
                        '    Dim str As String = list(r + 2).GetValue(c + 1)
                        '    If (str = ".") Then
                        '        array(c, r).Bin = (-1).ToString()
                        '    ElseIf (str = "X") Then
                        '        array(c, r).Bin = (-2).ToString()
                        '    ElseIf (str = "@") Then
                        '        array(c, r).Bin = (-3).ToString()
                        '    End If
                        'End If
                        array(c, r).Bin = list(r + 2).GetValue(c + 1).ToString()
                    Next
                Next
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ListToArrayCycle(ByRef array As Die(,), ByVal list As List(Of String())) As Boolean
        Try
            Dim colnums = array.GetUpperBound(0)
            Dim rows = array.GetUpperBound(1)

            If (colnums = list(1).Count - 2) Then
                For r = 0 To rows
                    For c = 0 To colnums
                        'If IsNumeric(list(r + 2).GetValue(c + 1)) Then
                        '    array(c, r).Cycle = list(r + 2).GetValue(c + 1)
                        'Else
                        '    Dim str As String = list(r + 2).GetValue(c + 1)
                        '    If (str = ".") Then
                        '        array(c, r).Cycle = -1
                        '    ElseIf (str = "X") Then
                        '        array(c, r).Cycle = -2
                        '    ElseIf (str = "@") Then
                        '        array(c, r).Cycle = -3
                        '    End If
                        'End If
                        array(c, r).Cycle = list(r + 2).GetValue(c + 1)
                    Next
                Next
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class

Public Class Substrate

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal Name As Substrate)
        MyBase.New()
        DieArray = Name.DieArray
        DieCount = Name.DieCount
        DieSize = Name.DieSize
        Size = Name.Size
        ID = Name.ID
        Number = Name.Number
    End Sub

    ''' <summary>
    ''' 產品陣列
    ''' </summary>
    Public DieArray(,) As Die

    Public ReadOnly Property Columns As Integer
        Get
            Return DieArray.GetUpperBound(0) + 1
        End Get
    End Property

    Public ReadOnly Property Rows As Integer
        Get
            Return DieArray.GetUpperBound(1) + 1
        End Get
    End Property

    Dim _dieCount As Integer
    ''' <summary>
    ''' 產品數量
    ''' </summary>
    Public Property DieCount As Integer
        Get
            Return _dieCount
        End Get
        Set(value As Integer)
            _dieCount = value
        End Set
    End Property

    Dim _dieSize As Size
    ''' <summary>
    ''' 產品尺寸
    ''' </summary>
    Public Property DieSize As Size
        Get
            Return _dieSize
        End Get
        Set(value As Size)
            _dieSize = value
        End Set
    End Property

    ''' <summary>
    ''' 基材尺寸
    ''' </summary>
    Public Size As Size

    ''' <summary>
    ''' 基材ID
    ''' </summary>
    Public ID As String

    ''' <summary>
    ''' 編號
    ''' </summary>
    Public Number As Integer
End Class

Public Enum enmClient
    NULL
    TSMC
    ASE
End Enum

Public Class Die
    ''' <summary>
    ''' 是否可以執行製程
    ''' </summary>
    Public IsReady As Boolean

    ''' <summary>
    ''' 是否已完成製程
    ''' </summary>
    Public IsComplete As Boolean

    ''' <summary>
    ''' 0~9, A~Z
    ''' </summary>
    ''' <remarks> -1 = "." , -2 = "X", -3 = "@" </remarks>
    Public Bin As String = "0"

    ''' <summary>
    ''' 已執行完成的階段
    ''' </summary>
    ''' <remarks> -1 = "." , -2 = "X", -3 = "@" </remarks>
    Public Cycle As String = "0"

    ''' <summary>
    ''' 產品尺寸
    ''' </summary>
    Public Size As Size

    ''' <summary>
    ''' 製程模式
    ''' </summary>
    Public Pattern As String

    ''' <summary>
    ''' 產品編號
    ''' </summary>
    Public Number As Integer

    ''' <summary>
    ''' 產品ID
    ''' </summary>
    Public ID As String

    ''' <summary>
    ''' 產品邊緣間隔距離
    ''' </summary>
    Public Margin As Margin

    Public NodeID As String

    Public DieArray(,) As Die

    Public ReadOnly Property Columns As Integer
        Get
            Return DieArray.GetUpperBound(0) + 1
        End Get
    End Property

    Public ReadOnly Property Rows As Integer
        Get
            Return DieArray.GetUpperBound(1) + 1
        End Get
    End Property
End Class

Public Class Information
    Public Version As String = "N/A"
    Public LotID As String = "N/A"
    Public ProductID As String = "N/A"
    Public DieSizeX As Integer
    Public DieSizeY As Integer
    Public Column As Integer
    Public Row As Integer
    Public CheckInTime As DateTime
    Public CheckOutTime As DateTime
    Public TotalTime As DateTime
    Public RecipeName As String = "N/A"
    Public Type As String = "N/A"
    Public Pitch As Integer
    Public User As String = "N/A"
    Public MachineType As String = "N/A"
    Public Notch As Integer = 180
End Class

Public Structure Margin
    Dim Top As Integer
    Dim Bottom As Integer
    Dim Left As Integer
    Dim Right As Integer
End Structure

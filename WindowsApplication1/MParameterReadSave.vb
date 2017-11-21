Imports ProjectCore
Imports ProjectMotion
Imports ProjectConveyor
Imports ProjectRecipe
Imports ProjectTriggerBoard
Imports ProjectFeedback
Imports System.IO

Module MParameterReadSave



#Region "Motor Parameter"

    ''' <summary>
    ''' ReadMotorParameter 讀取馬達參數
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadMotionParameter() As Boolean

        Dim strFileName As String

        'On Error GoTo ErrorHandler   ' Enable error-handling routine.
        Try
            For mAxisNo As Integer = 0 To enmAxis.Max
                strFileName = Application.StartupPath & "\System\" & MachineName & "\ConfigAxis" & mAxisNo & ".ini"
                gCMotion.AxisParameter(mAxisNo).Load(strFileName)
                gSyslog.Save(gCMotion.AxisParameter(mAxisNo).AxisName & "(" & mAxisNo & ") Parameter Loaded.")
            Next

            Return True

        Catch ex As Exception
            gSyslog.Save("Axis Parameter Load Error:" & ex.Message, , eMessageLevel.Error)
            MsgBox("Axis Parameter Load Error:" & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
        'ErrorHandler:  ' Error-handling routine.

        '        MsgBox(Err.GetException.ToString())
        '        Err.Clear()
        Return False
    End Function

    ''' <summary>
    ''' SaveMotorParamter 存取馬達參數
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMotionParamter() As Boolean

        Dim strFileName As String
        Dim strSection As String
        Dim intI As Integer
        Dim intJ As Integer
        Dim strString As String

        On Error GoTo ErrorHandler   ' Enable error-handling routine.

        '[說明]:檢查資料夾是否存在
        If System.IO.Directory.Exists(Application.StartupPath & "\System") = False Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\System")
        End If
        If System.IO.Directory.Exists(Application.StartupPath & "\System\" & MachineName) = False Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\System\" & MachineName)
        End If

        For mAxisNo As Integer = 0 To enmAxis.Max
            strFileName = Application.StartupPath & "\System\" & MachineName & "\ConfigAxis" & mAxisNo & ".ini"
            gCMotion.AxisParameter(mAxisNo).Save(strFileName)
        Next

        Return True

ErrorHandler:  ' Error-handling routine.
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
        Return False
    End Function

#End Region

#Region "CustomerToPIIMap"
    Public Sub WaferMapConvert(ByVal ReadFileName As String, Optional ByVal SavePath As String = "D:\PIIData\MappingData\SourceData\")

        Dim objReader As New StreamReader(ReadFileName)
        Dim cLine() As Char
        Dim iRowCount As Integer = 0
        Dim iColumnCount As Integer = 0
        Dim i, j As Integer

        Try
            cLine = objReader.ReadLine()
            Dim Die(cLine.Length * 2, cLine.Length * 2) As Char

            Do
                If cLine.Length > iColumnCount Then
                    iColumnCount = cLine.Length
                End If
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

            file.WriteLine("[WAFER INFORMATION]")
            file.WriteLine("VERSION,Ver9.2")
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
            file.WriteLine("[CYCLE MAP]")
            For i = 0 To iRowCount - 1
                file.Write("ROW" & i + 1)
                For j = 0 To iColumnCount - 1

                    If Char.IsLetterOrDigit(Die(i, j)) Then
                        If ((Die(i, j) = "X") Or (Die(i, j) = ".") Or (Die(i, j) = "N") Or (Die(i, j) = "=")) Then
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
            file.WriteLine("[EXPLAIN]")
            file.WriteLine(".,EMPTY")
            file.WriteLine("X,BAD DIE")
            file.WriteLine("0~FF,[WAFER MAP] BIN NUMBER")
            file.WriteLine("0~FF,[CYCLE MAP] CYCLE TIME")
            file.WriteLine("N,NOTCH")
            file.Close()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "WaferMap Convert")
        End Try
    End Sub
#End Region


End Module

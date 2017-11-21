Imports ProjectCore

Public Class CVX100_EthernetN_Parser
    Public Enum ParserResult
        ''' <summary>不明錯誤</summary>
        ''' <remarks></remarks>
        UnknownError = 0
        ''' <summary>成功</summary>
        ''' <remarks></remarks>
        Success = 1
        ''' <summary>命令錯誤</summary>
        ''' <remarks></remarks>
        CommandError = 2
        ''' <summary>命令運作禁止</summary>
        ''' <remarks></remarks>
        FobiddenError = 3
        ''' <summary>參數錯誤</summary>
        ''' <remarks></remarks>
        ParamError = 4
        ''' <summary>超時錯誤</summary>
        ''' <remarks></remarks>
        TimeoutError = 5
    End Enum

  
    ''' <summary>讀取時間設定值
    ''' </summary>
    ''' <remarks></remarks>
    Public TimeData As TimeFormat


    ''' <summary>檢測設定</summary>
    ''' <remarks></remarks>
    Public DetectionConfig As ConfigOfDetection

    ''' <summary>控制器狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public ControllerStatus As ControllerMode
    ''' <summary>OCR判定字串
    ''' </summary>
    ''' <remarks></remarks>
    Public OCRString As String

    ''' <summary>讀取已指定之判定條件的上限值與下限值
    ''' </summary>
    ''' <remarks></remarks>
    Public DRValue As Integer
    ''' <summary>讀取損傷工具等級</summary>
    ''' <remarks></remarks>
    Public SLRValue As Integer
    ''' <summary>讀取ECHO字串
    ''' </summary>
    ''' <remarks></remarks>
    Public EchoString As String

    ''' <summary>錯誤訊息
    ''' </summary>
    ''' <remarks></remarks>
    Public ErrorMessage As String
    ''' <summary>機型
    ''' </summary>
    ''' <remarks></remarks>
    Public MachineType As String
    ''' <summary>ROM版本
    ''' </summary>
    ''' <remarks></remarks>
    Public ROMVersion As String

    
    ''' <summary>讀取模式
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ModeLoaded(sender As Object, ByVal e As ControllerModeEventArgs)
    Public Event DetectionConfigLoaded(sender As Object, ByVal e As ConfigEventArgs)
    Public Event OCRStringLoaded(sender As Object, ByVal e As DataEventArgs)
    Public Event DRValueLoaded(sender As Object, ByVal e As DataEventArgs)
    Public Event SLRValueLodaed(sender As Object, ByVal e As DataEventArgs)
    Public Event EchoStringLodaed(sender As Object, ByVal e As DataEventArgs)
    Public Event TimerLoaded(sender As Object, ByVal e As TimeEventArgs)
    Public Event VersionLoaded(sender As Object, ByVal e As DataEventArgs)
    Public Event ErrorOccurred(sender As Object, ByVal e As DataEventArgs)
    Public Event ReturnValueLoaded(sender As Object, ByVal e As DataArrayEventArgs)

    Public Function Parse(ByVal msg As String) As ParserResult

        Dim dataCollection() As String
        gSyslog.Save("CCD Recieved Data(" & msg & ")")
        'Call WriteCCDReceiveData(msg)
        dataCollection = Split(msg.Split(vbCrLf)(0), ",")

        If dataCollection Is Nothing Then
            ErrorMessage = "dataCollection Is Nothing"
            gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
            Return ParserResult.UnknownError
        End If
        If dataCollection.GetUpperBound(0) < 0 Then
            ErrorMessage = "dataCollection.GetUpperBound(0) < 0"
            gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
            Return ParserResult.UnknownError
        End If

        Select Case dataCollection(0)
            Case "ER" '命令受理中發生錯誤時
                'dataCollection(1): 造成錯誤原因的接收命令
                'dataCollection(2): 2位的錯誤代碼
                Select Case dataCollection(2)
                    Case "02"
                        ErrorMessage = "@Cmd:" & dataCollection(1) & " Command is Not Existed."
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.CommandError
                    Case "03"
                        ErrorMessage = "@Cmd:" & dataCollection(1) & " Command Forbidden"
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.FobiddenError
                    Case "05"
                        ErrorMessage = "@Cmd:" & dataCollection(1) & " Command Ack. Error"
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.UnknownError
                    Case "22"
                        ErrorMessage = "@Cmd:" & dataCollection(1) & " Parameter Error"
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.ParamError
                    Case "91"
                        ErrorMessage = "@Cmd:" & dataCollection(1) & " Time Out"
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.TimeoutError
                    Case Else
                        ErrorMessage = "@Cmd:" & dataCollection(1) & " Unknown Error"
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.UnknownError
                End Select
            Case "RET" '傳回值
                RaiseEvent ReturnValueLoaded(Me, New DataArrayEventArgs(dataCollection))

            Case "T1", "T2", "T3", "T4", "TA", "R0", "S0", "RS", "RB", "SS", "CE", "VW", "RE", "PW", "CSH", "CSE", "CTD", "CLV", "BS", "EXW", "CW", "DW", "SLW", "TE", "OE", "TC", "TS", "HC", "HS", "BC", "OW", "TW"
                If dataCollection.GetUpperBound(0) = 0 Then
                    ErrorMessage = ""
                    Return ParserResult.Success
                End If

            Case "RM"
                If dataCollection.GetUpperBound(0) < 1 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                Select Case dataCollection(1)
                    Case "0"
                        ControllerStatus = ControllerMode.Setup
                        ErrorMessage = ""
                        RaiseEvent ModeLoaded(Me, New ControllerModeEventArgs(ControllerStatus))
                        Return ParserResult.Success
                    Case "1"
                        ControllerStatus = ControllerMode.Run
                        ErrorMessage = ""
                        RaiseEvent ModeLoaded(Me, New ControllerModeEventArgs(ControllerStatus))
                        Return ParserResult.Success
                End Select
            Case "PR"
                If dataCollection.GetUpperBound(0) < 2 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                Select Case dataCollection(1) '記憶卡編號
                    Case "0", "1"
                        DetectionConfig.CardID = CInt(dataCollection(1))
                    Case Else
                        ErrorMessage = "@Cmd:" & dataCollection(0) & " Card ID Error"
                        gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                        RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                        Return ParserResult.UnknownError
                End Select

                If Not IsNumeric(dataCollection(2)) Then 'dataCollection(2) 檢測設定
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Item ID Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                DetectionConfig.ItemID = CInt(dataCollection(2))
                RaiseEvent DetectionConfigLoaded(Me, New ConfigEventArgs(DetectionConfig))
                Return ParserResult.Success

            Case "CR"
                If dataCollection.GetUpperBound(0) < 1 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                OCRString = dataCollection(1)
                ErrorMessage = ""
                RaiseEvent OCRStringLoaded(Me, New DataEventArgs(OCRString))
                Return ParserResult.Success

            Case "DR"
                If dataCollection.GetUpperBound(0) < 1 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                DRValue = dataCollection(1)
                ErrorMessage = ""
                RaiseEvent DRValueLoaded(Me, New DataEventArgs(DRValue))
                Return ParserResult.Success

            Case "SLR"
                If dataCollection.GetUpperBound(0) < 1 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                SLRValue = dataCollection(1)
                ErrorMessage = ""
                RaiseEvent SLRValueLodaed(Me, New DataEventArgs(SLRValue))
                Return ParserResult.Success

            Case "EC"
                If dataCollection.GetUpperBound(0) < 1 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                EchoString = dataCollection(1)
                ErrorMessage = ""
                RaiseEvent EchoStringLodaed(Me, New DataEventArgs(EchoString))
                Return ParserResult.Success

            Case "TR"
                If dataCollection.GetUpperBound(0) < 6 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                TimeData.Yy = dataCollection(1)
                TimeData.Mo = dataCollection(2)
                TimeData.Dd = dataCollection(3)
                TimeData.Hh = dataCollection(4)
                TimeData.Mi = dataCollection(5)
                TimeData.Ss = dataCollection(6)
                ErrorMessage = ""
                RaiseEvent TimerLoaded(Me, New TimeEventArgs(TimeData))
                Return ParserResult.Success

            Case "VI"
                If dataCollection.GetUpperBound(0) < 2 Then
                    ErrorMessage = "@Cmd:" & dataCollection(0) & " Unknown Error"
                    gSyslog.Save(ErrorMessage, , eMessageLevel.Error)
                    RaiseEvent ErrorOccurred(Me, New DataEventArgs(ErrorMessage))
                    Return ParserResult.UnknownError
                End If

                MachineType = dataCollection(1)
                ROMVersion = dataCollection(2)
                RaiseEvent VersionLoaded(Me, New DataEventArgs(MachineType, ROMVersion))
                Return ParserResult.Success

        End Select
        Return ParserResult.CommandError
    End Function


End Class

Imports ProjectFeedback
'Imports ProjectMotion
Imports ProjectIO
Imports ProjectCore

''' <summary>FMCS設定值</summary>
''' <remarks></remarks>
Public Module MFunctionFMCS

    ' ''' <summary>FMCS通訊物件</summary>
    ' ''' <remarks></remarks>
    'Public gFMCS(3) As CFMCS
    Public gFMCSCollection As New CFMCSCollection
    ''' <summary>體積控制</summary>
    ''' <remarks></remarks>
    Public gVolumneControl As New CFMCSVolumeControl
    ''' <summary>FMCS記錄打點數</summary>
    ''' <remarks></remarks>
    Public gJettingCount(enmValve.Max) As Integer
    ''' <summary>FMCS初始化</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial_FMCS() As Boolean
        If gFMCSCollection.Initial(gFMCSCollection.ConnectionParameter) = False Then
            'AddHistoryAlarm("Error_1017000", "FMCS1初始化失敗!" & gSSystemParameter.sFMCS(i).strPortName, , gMsgHandler.GetMessage(Error_1017000))
            Return False
        End If
        For i As Integer = 0 To gFMCSCollection.ConnectionParameter.Count - 1
            AddHandler gFMCSCollection.Items(i).OnRecievedData, AddressOf mFMCS_OnRecievedData
        Next
        'Dim blnRet As Boolean
        'For i As Integer = 0 To gSSystemParameter.sFMCS.Count - 1
        '    gFMCS(i) = New CFMCS()
        '    blnRet = gFMCS(i).Initial(gSSystemParameter.sFMCS(i).strPortName)

        '    If blnRet = False Then
        '        gEqpMsg.AddHistoryAlarm("Error_1017000", "FMCS1初始化失敗!" & gSSystemParameter.sFMCS(i).strPortName, , gMsgHandler.GetMessage(Error_1017000))
        '    Else
        '        AddHandler gFMCS(i).OnRecievedData, AddressOf mFMCS_OnRecievedData '掛載事件
        '    End If
        'Next

        'Return blnRet
        Return True
    End Function

    Private Sub mFMCS_OnRecievedData(sender As Object, ByVal e As FMCSEventArgs) 'Handles gFMCS.OnRecievedData
        'TODO: Jeff 取消壓力控制方案, 改為打點修正方案.
        '壓力控制 
        If gVolumneControl.Enabled Then
            Select Case gVolumneControl.ControlType
                Case enmControlType.FlowToAir
                    gVolumneControl.AirControl(e.Data.avgFlow, gAOCollection.Value(enmAO.DispenserNo1EPRegulator))
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6017004, gAOCollection.Value(enmAO.DispenserNo1EPRegulator)), "INFO_6017004") 'FMCS1流量對氣壓控制: {0}
                Case enmControlType.FlowToPoints
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6017005, gVolumneControl.PointsControl(e.Data.avgFlow)), "INFO_6017005") 'FMCS1流量對點數控制: {0}
                Case enmControlType.VolumeToAir
                    gVolumneControl.AirControl(e.Data.volume, gAOCollection.Value(enmAO.DispenserNo1EPRegulator))
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6017006, gAOCollection.Value(enmAO.DispenserNo1EPRegulator)), "INFO_6017006") 'FMCS1體積對氣壓控制: {0}
                Case enmControlType.VolumeToPoints
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6017007, gVolumneControl.PointsControl(e.Data.volume)), "INFO_6017007") 'FMCS1體積對點數控制: {0}
            End Select
        End If

        'If gAct(eAct.WeightUnit).RunStatus <> enmRunStatus.Running Then

        'Dim fileName As String
        'Dim folderName As String
        'With DateTime.Now
        '    folderName = Application.StartupPath & "\DataLog\" & .Year.ToString("0000") & "\" & .Month.ToString("00") & "\"
        '    If Not System.IO.Directory.Exists(folderName) Then '確保資料夾存在
        '        System.IO.Directory.CreateDirectory(folderName)
        '    End If
        '    fileName = folderName & .Year.ToString("0000") & .Month.ToString("00") & .Day.ToString("00") & "FMCS.csv"

        '    If gFMCS.IsOpen Then
        '        If Not System.IO.File.Exists(fileName) Then
        '            Dim sw As New System.IO.StreamWriter(fileName, True, System.Text.Encoding.Unicode)
        '            sw.WriteLine("Date" & vbTab & "Time" & vbTab & "打點數" & vbTab & "校正後顯示值" & vbTab & "微量天平量測值" & vbTab & "FMCS測得體積" & vbTab & "FMCS平均流量" & vbTab & "測試用氣壓1" & vbTab & "ST時間" & vbTab & "下次命令")
        '            sw.WriteLine(.Year.ToString("0000") & .Month.ToString("00") & .Day.ToString("00") & vbTab & .Hour.ToString("00") & .Minute.ToString("00") & .Second.ToString("00") & vbTab & gJettingCount & vbTab & weightCorrectedValue & vbTab & " " & vbTab & gFMCS.OutputVolume & vbTab & gFMCS.OutputAvgFlow & vbTab & RecordAP1 & vbTab & MeasureTimeInSec & vbTab & gVolumneControl.output_0)
        '            sw.Close()
        '        Else
        '            Dim sw As New System.IO.StreamWriter(fileName, True, System.Text.Encoding.Unicode)
        '            sw.WriteLine(.Year.ToString("0000") & .Month.ToString("00") & .Day.ToString("00") & vbTab & .Hour.ToString("00") & .Minute.ToString("00") & .Second.ToString("00") & vbTab & gJettingCount & vbTab & weightCorrectedValue & vbTab & " " & vbTab & gFMCS.OutputVolume & vbTab & gFMCS.OutputAvgFlow & vbTab & RecordAP1 & vbTab & MeasureTimeInSec & vbTab & gVolumneControl.output_0)
        '            sw.Close()
        '        End If
        '    Else
        '        If Not System.IO.File.Exists(fileName) Then
        '            Dim sw As New System.IO.StreamWriter(fileName, True, System.Text.Encoding.Unicode)
        '            sw.WriteLine("Date" & vbTab & "Time" & vbTab & "打點數" & vbTab & "校正後顯示值" & vbTab & "微量天平量測值")
        '            sw.WriteLine(.Year.ToString("0000") & .Month.ToString("00") & .Day.ToString("00") & vbTab & .Hour.ToString("00") & .Minute.ToString("00") & .Second.ToString("00") & vbTab & gJettingCount & vbTab & weightCorrectedValue & vbTab & " ")
        '            sw.Close()
        '        Else
        '            Dim sw As New System.IO.StreamWriter(fileName, True, System.Text.Encoding.Unicode)
        '            sw.WriteLine(.Year.ToString("0000") & .Month.ToString("00") & .Day.ToString("00") & vbTab & .Hour.ToString("00") & .Minute.ToString("00") & .Second.ToString("00") & vbTab & gJettingCount & vbTab & weightCorrectedValue & vbTab & " ")
        '            sw.Close()
        '        End If
        '    End If


        'End With

        'End If
    End Sub
End Module

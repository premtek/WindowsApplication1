Imports ProjectRecipe
Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion

''' <summary>流道Conveyor設定
''' </summary>
''' <remarks></remarks>
Public Module MFunctionConveyor
    



#Region "PLC Conveyor"

    ''' <summary>PLC Conveor</summary>
    ''' <remarks></remarks>
    Public WithEvents gPLC As New CPLC_RS232

    ''' <summary>接收資料X000~X031</summary>
    ''' <remarks></remarks>
    Dim dataX000_X031 As String
    ''' <summary>接收資料X032~X063</summary>
    ''' <remarks></remarks>
    Dim dataX032_X063 As String
    ''' <summary>接收資料Y000~Y031</summary>
    ''' <remarks></remarks>
    Dim dataY000_Y031 As String
    ''' <summary>接收資料Y032~Y063</summary>
    ''' <remarks></remarks>
    Dim dataY032_Y063 As String

    'Dere + 2015/04/22---------------------------------

    ''' <summary>宣告Conveyor名稱</summary>    
    Public ConveyorA As New CConveyorFlow
    ''' <summary>[紀錄Conveyor的狀態]</summary>
    Public genmConveyorStatus As enmRunStatus
    ' ''' <summary>[紀錄Conveyor Function的狀態]</summary>
    'Public glngConveyorState As enmRunStatus
    ''' <summary>資料暫存器 </summary>
    Public gCRegister As New ConveyorRegister

    ' ''' <summary>
    ' ''' 使用 Mobus 通訊與加熱器交握
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public gMobusHeater As New CMotion_ModBus
    'Dere + 2015/04/22---------------------------------

    ''' <summary>PLC接點值X</summary>
    ''' <remarks></remarks>
    Public PLCX(100) As Boolean
    ''' <summary>PLC接點值Y</summary>
    ''' <remarks></remarks>
    Public PLCY(100) As Boolean
    'Public PLCM(6000) As Boolean
    Public PLCM(6000) As Boolean
    ''' <summary>PLC逾時計時</summary>
    ''' <remarks></remarks>
    Public PLC_TimeOut_StopWatch As New Stopwatch

    ''' <summary>PLC逾時判定條件</summary>
    ''' <remarks></remarks>
    Public PLC_TimeOut_in_ms As Double = 1000

    ''' <summary>PLCY資料串接字串</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPLCYString() As String
        GetPLCYString = ""
        For i = 0 To 39
            If PLCY(i) Then
                GetPLCYString += "1"
            Else
                GetPLCYString += "0"
            End If
        Next
        Return GetPLCYString
    End Function

    Public Function IsPLCOpen() As Boolean
        Return gPLC.IsOpen
    End Function

    ''' <summary>頂出下降</summary>
    ''' <remarks></remarks>
    Public Sub ConveyerPinDown()
        PLCY(enmPLCY.AStopSOL1) = True
        PLCY(enmPLCY.AStopSOL2) = True
        'gPLC.WriteBits("Y0020", 2, "11")
    End Sub

    Public plc_sysNum As Integer

    ''' <summary>PLC Scan動作</summary>
    ''' <param name="sys_num"></param>
    ''' <remarks></remarks>
    Public Sub PLC_ScanAction(ByRef sys_num As Integer)
        If gPLC.IsOpen = False Then '未連線無法辨識
            Exit Sub
        End If
        Static sys_Timer As New Stopwatch
        Select Case sys_num
            Case 0, 1000
                gPLC.ReadBits("X0000", 40)
                sys_Timer.Restart()
                sys_num = 1100
            Case 1100
                If sys_Timer.ElapsedMilliseconds > 50 Then
                    gPLC.WriteBits("Y0000", 40, GetPLCYString)
                    sys_Timer.Restart()
                    sys_num = 1200
                End If

            Case 1200
                If sys_Timer.ElapsedMilliseconds > 50 Then
                    gPLC.ReadBits("M5000", 35)
                    sys_Timer.Restart()
                    sys_num = 1300
                End If

            Case 1300
                If sys_Timer.ElapsedMilliseconds > 50 Then
                    gPLC.ReadBits("M1611", 1)
                    sys_Timer.Restart()
                    sys_num = 1400
                End If

            Case 1400
                If sys_Timer.ElapsedMilliseconds > 50 Then
                    sys_num = 1000
                End If
        End Select
    End Sub

    Public Function IsConveyorPinDown() As Boolean
        If gPLC.IsOpen = False Or True Then '未連線無法辨識
            Return True
        End If
        If PLCX(enmPLCX.AStopBot1) = False Then
            Return False
        End If
        If PLCX(enmPLCX.AStopBot2) = False Then
            Return False
        End If
        Return True
    End Function

    Public Function Initial_PLC() As Boolean
        If gSSystemParameter.MachineType <> enmMachineType.eDTS300A Then
            Return True
        End If
        gPLC.Open(gSSystemParameter.sConveyor(0).PLCPortName)
        If gPLC.IsOpen Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6023000), "INFO_6023000")
            gSyslog.Save("COM Port:" & gSSystemParameter.sConveyor(0).PLCPortName)
            Return True
        Else
            gEqpMsg.AddHistoryAlarm("Error_1023000", "Initial_PLC", , gMsgHandler.GetMessage(Error_1023000), eMessageLevel.Error)
            gSyslog.Save("COM Port:" & gSSystemParameter.sConveyor(0).PLCPortName, , eMessageLevel.Error)
            Return False
        End If
    End Function


    Private Sub gPLC_ReadBitsDaataRecieved(sender As Object, e As DataEventArgs) Handles gPLC.ReadBitsDaataRecieved
        Dim startBit As Integer
        startBit = CInt(gPLC.ReadBitDevice.Substring(1))
        Select Case gPLC.ReadBitDevice.Substring(0, 1)
            Case "X"
                For i As Integer = 0 To e.Data.Length - 1
                    If e.Data(i) = "1" Then
                        PLCX(i + startBit) = True
                    Else
                        PLCX(i + startBit) = False
                    End If
                Next
            Case "Y"
                For i As Integer = 0 To e.Data.Length - 1
                    If e.Data(i) = "1" Then
                        PLCY(i + startBit) = True
                    Else
                        PLCY(i + startBit) = False
                    End If
                Next
            Case "M"
                For i As Integer = 0 To e.Data.Length - 1
                    If e.Data(i) = "1" Then
                        PLCM(i + startBit) = True
                    Else
                        PLCM(i + startBit) = False
                    End If
                Next
        End Select

    End Sub


    Function GetChuckStatus(ByVal intXf As Integer, ByVal intYf As Integer, ByVal sys As sSysParam) As Boolean
        Dim intSeq As Integer
        If Not gCRecipe.Node(sys.StageNo).ContainsKey(gCRecipe.StageNodeID(sys.StageNo)) Then
            Return True
        End If
        Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(sys.StageNo)(gCRecipe.StageNodeID(sys.StageNo)).Array)
        Select Case gCRecipe.ChuckMapType
            Case enmSearchType.Y_Snake
                If intXf Mod 2 = 0 Then
                    '[正]
                    intSeq = 5000 + intYf + intXf * mMultiArrayAdapter.GetMemoryCountY() ' .MultiDevice.Level(enmRecipeLevel.Area).ArrayNumY
                Else
                    '[逆]
                    intSeq = 5000 + intXf * mMultiArrayAdapter.GetMemoryCountY() + mMultiArrayAdapter.GetMemoryCountY() - intYf
                End If
            Case enmSearchType.X_Snake
                If intYf Mod 2 = 0 Then
                    '[正]
                    intSeq = 5000 + intXf + intYf * mMultiArrayAdapter.GetMemoryCountX()
                Else
                    '[逆]
                    intSeq = 5000 + intYf * mMultiArrayAdapter.GetMemoryCountX() + mMultiArrayAdapter.GetMemoryCountX() - intXf - 1
                End If

        End Select
        Return PLCM(intSeq)
    End Function

    '對於每一個位置,取得相應Status,
    Public Sub UpdateConveyorVacuum(ByVal sys As sSysParam)
        Dim intXf As Integer
        Dim intYf As Integer
        Dim status As Boolean

        For i As Integer = 0 To gStageMap(sys.StageNo).Node.Count - 1
            Dim mNodeID As String = gStageMap(sys.StageNo).Node.Keys(i)
            Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(sys.StageNo)(mNodeID).Array)
            Dim maxX As Integer = mMultiArrayAdapter.GetMemoryCountX() - 1
            Dim maxY As Integer = mMultiArrayAdapter.GetMemoryCountY() - 1
            For intXf = 0 To maxX
                For intYf = 0 To maxY
                    status = Not GetChuckStatus(intXf, intYf, sys)
                    gStageMap(sys.StageNo).Node(mNodeID).SRecipePos(intXf, intYf).IsByPassCCDScanFixAction = status
                    gStageMap(sys.StageNo).Node(mNodeID).SRecipePos(intXf, intYf).IsByPassLaserAction = status
                    gStageMap(sys.StageNo).Node(mNodeID).SRecipePos(intXf, intYf).IsByPassDispensingAction = status
                    gStageMap(sys.StageNo).Node(mNodeID).SRecipePos(intXf, intYf).IsByPassCCDScanGlueAction = status

                Next intYf
            Next intXf
        Next


    End Sub

    ''' <summary>儲存Conveyor連線參數</summary>
    ''' <remarks></remarks>
    Sub SaveConveyorConnectionParameter(ByVal fileName As String)
        'Dim strFileName As String
        Dim strSection As String
        'strFileName = Application.StartupPath & "\system\SystemParamter.ini"
        For i As Integer = 0 To gSSystemParameter.sConveyor.Count - 1
            If gSSystemParameter.sConveyor.Count = 1 Then
                strSection = "Conveyor"
            Else
                strSection = "Conveyor" & (i + 1).ToString()
            End If
            With gSSystemParameter.sConveyor(i)
                Call SaveIniString(strSection, "PLCPortName", .PLCPortName, fileName)
                Call SaveIniString(strSection, "ConveyorType", .ConveyorType, fileName)
                Call SaveIniString(strSection, "PLCType", .PLCType, fileName)
            End With
        Next
    End Sub
    ''' <summary>讀取Conveyor連線參數</summary>
    ''' <remarks></remarks>
    Sub LoadConveyorConnectionParameter(ByVal fileName As String)
        'Dim strFileName As String
        Dim strSection As String
        'strFileName = Application.StartupPath & "\system\SystemParamter.ini"
        For i As Integer = 0 To gSSystemParameter.sConveyor.Count - 1
            If gSSystemParameter.sConveyor.Count = 1 Then
                strSection = "Conveyor"
            Else
                strSection = "Conveyor" & (i + 1).ToString()
            End If
            With gSSystemParameter.sConveyor(i)
                .PLCPortName = ReadIniString(strSection, "PLCPortName", fileName, "COM1")
                .ConveyorType = ReadIniString(strSection, "ConveyorType", fileName, "SINGLE_RAIL")
                .PLCType = ReadIniString(strSection, "PLCType", fileName, "FX3U")
            End With
        Next
    End Sub


#End Region


End Module

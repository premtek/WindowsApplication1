Imports ProjectCore

Public Class clsULoader
    Dim WithEvents PLC_FX3U As New clsPLC_FX3U
    Dim SwTimeOut As New Stopwatch

    Enum enmStatus
        [Stop] = 0
        Run = 1
        Alarm = 2
        Idle = 3


        Null
    End Enum

    Enum enmTask
        GetPassModel
        GetMachineStatus
        GetAlarmCode
        GetTemperatures
        GetProductCount
        GetSlotStatus
        GetCaseteBarCode
        GetProductNum
        Read
    End Enum

    Enum enmMotion
        Start
        [End]
        IsRecipeChange
        SendCmd
        GetReceive
    End Enum

    Dim eFunc As enmTask
    Public PlcStep As enmMotion

    Dim DeadTime As Integer = gSSystemParameter.TimeOut4
    Public IsBusy As Boolean

    Dim _isRecieveBusy As Boolean
    ''' <summary>
    ''' 是否忙碌中(執行資料接收中)
    ''' </summary>
    Public ReadOnly Property IsRecieveBusy As Boolean
        Get
            Return Not PLC_FX3U.IsDataRecieved
        End Get
    End Property

    Public Pass As Boolean
    Public AlarmCodes(4) As String
    Public HpTemperatures(11) As Integer
    Public ProductCount As Integer
    Public SlotStatus(9) As Integer
    Public ProductNum As Integer
    Public CaseteBarCode As String
    Public PlcData() As String

    Dim _machineStatus As enmStatus = enmStatus.Null
    ''' <summary>
    ''' 設備狀態
    ''' </summary>
    Public ReadOnly Property MachineStatus As enmStatus
        Get
            Return _machineStatus
        End Get
    End Property

    Dim ID_Pass As String = "D7010"
    Dim ID_SetHpTempreature As String = "D7020"
    Dim ID_ProductType As String = "D7050"
    Dim ID_MachineStatus As String = "D7500"
    Dim ID_AlarmCode() As String = {"D7501", "D7502", "D7503", "D7504", "D7505"}
    Dim ID_HpTempreatures() As String = {"D7520", "D7521", "D7522", "D7523", "D7524", "D7525", "D7530", "D7531", "D7532", "D7533", "D7534", "D7535"}
    Dim ID_ProductCount As String = "D7550"
    Dim ID_SlotsStatus() As String = {"D7551", "D7552", "D7553", "D7554", "D7555", "D7556", "D7557", "D7558", "D7559", "D7560"}
    Dim ID_CaseteBarCode() As String = {"D7600", "D7601", "D7602", "D7603", "D7604", "D7605", "D7606", "D7607", "D7608", "D7609"}
    Dim ID_GetProductNum As String = "D7620"
    Dim ID_SetProductNum As String = "D7120"

    Sub New()

    End Sub

    ''' <summary>
    ''' 是否已開啟
    ''' </summary>
    Public ReadOnly Property IsOpen As Boolean
        Get
            Return PLC_FX3U.IsOpen
        End Get
    End Property

    ''' <summary>
    ''' 開啟設備
    ''' </summary>
    Public Function Open(portName As String, baudRate As Integer, parity As System.IO.Ports.Parity, dataBits As Integer, stopBits As System.IO.Ports.StopBits) As Boolean
        If PLC_FX3U.Open(portName, baudRate, parity, dataBits, stopBits) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 取得設備狀態,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetMachineStatus() As enmMotionStatus
        Try
            Select Case PlcStep
                Case enmMotion.Start
                    IsBusy = True
                    SwTimeOut.Start()
                    PlcStep = enmMotion.SendCmd

                Case enmMotion.SendCmd
                    If (TimeOutCheck(DeadTime)) Then
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            PLC_FX3U.ReadWords(ID_MachineStatus, 1)
                            eFunc = enmTask.GetMachineStatus
                            SwTimeOut.Restart()
                            PlcStep = enmMotion.GetReceive
                        End If
                    End If

                Case enmMotion.GetReceive
                    If (TimeOutCheck(DeadTime)) Then
                        Return enmMotionStatus.Alarm
                    Else
                        If (IsRecieveBusy = False) Then
                            SwTimeOut.Restart()
                            PlcStep = enmMotion.End
                        End If
                    End If

                Case enmMotion.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' 讀取Pass模式,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetPassModel() As Boolean
        If IsRecieveBusy = False Then
            PLC_FX3U.ReadWords(ID_Pass, 1)
            eFunc = enmTask.GetPassModel
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定Pass模式
    ''' </summary>
    Public Function SetPassModel(ByVal enable As Boolean) As Boolean
        Dim value As Integer = IIf(enable, 1, 0)
        If (IsRecieveBusy = False) Then
            PLC_FX3U.WriteWords(ID_Pass, 1, value.ToString("X4"))
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取錯誤訊息命令,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetAlarmCode() As Boolean
        If IsRecieveBusy = False Then
            PLC_FX3U.ReadWords(ID_AlarmCode(0), 5)
            eFunc = enmTask.GetAlarmCode
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定產品類別
    ''' </summary>
    Public Function SetProductType(ByVal type As Integer) As Boolean
        If (IsRecieveBusy = False) Then
            PLC_FX3U.WriteWords(ID_ProductType, 1, type.ToString("X4"))
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取Hot plate溫度,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetHpTemperatures() As Boolean
        If IsRecieveBusy = False Then
            PLC_FX3U.ReadWords(ID_HpTempreatures(0), 12)
            eFunc = enmTask.GetTemperatures
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 設定Hot plate溫度
    ''' </summary>
    Public Function SetHpTempreature(ByVal temperature As Integer) As Boolean
        If (IsRecieveBusy = False) Then
            PLC_FX3U.WriteWords(ID_SetHpTempreature, 1, temperature.ToString("X4"))
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取Cassette內料片個數,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetProductCount() As Boolean
        If IsRecieveBusy = False Then
            PLC_FX3U.ReadWords(ID_ProductCount, 1)
            eFunc = enmTask.GetProductCount
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取Cassette slot狀態,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetSlotsStatus() As Boolean
        If IsRecieveBusy = False Then
            PLC_FX3U.ReadWords(ID_SlotsStatus(0), 10)
            eFunc = enmTask.GetSlotStatus
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 取得輸送帶上產品資訊,接收完成IsRecieveBusy = false
    ''' </summary>
    Public Function GetProductNum() As Boolean
        If IsRecieveBusy = False Then
            PLC_FX3U.ReadWords(ID_GetProductNum, 1)
            eFunc = enmTask.GetProductNum
            Return True
        End If
        Return False
    End Function

    Public Function SetProductNum(ByVal num As Integer) As Boolean
        If (IsRecieveBusy = False) Then
            PLC_FX3U.WriteWords(ID_SetProductNum, 1, num.ToString("X4"))
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 讀取Bar-Code資料,接收完成IsRecieveBusy = false
    ''' </summary>
    ''' <returns>資料是否抓取成功</returns>
    ''' <remarks></remarks>
    Public Function GetCaseteBarCode() As Boolean

        Return False
    End Function

    ''' <summary>
    ''' 設定Bar Code資料
    ''' </summary>
    Public Function SetCaseteBarCode(ByVal barCode As String) As Boolean
        If (IsRecieveBusy = False) Then
            Dim values() As Char = barCode.ToCharArray()
            Dim hex As String = ""
            For Each value In values
                Dim data As Integer = Convert.ToInt32(value)
                hex = hex + String.Format("{0:X}", data)
            Next
            Dim length As Integer = barCode.Length / 2
            PLC_FX3U.WriteWords(ID_CaseteBarCode(0), length, hex)

            Return True
        End If
        Return False
    End Function

    'string input = "Hello World!";
    'char[] values = input.ToCharArray();
    'foreach (char letter in values)
    '{
    '    // Get the integral value of the character.
    '    int value = Convert.ToInt32(letter);
    '    // Convert the decimal value to a hexadecimal value in string form.
    '    string hexOutput = String.Format("{0:X}", value);
    '    Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
    '}

    Private Sub PLC_DataReceived(sender As Object, e As DataEventArgs) Handles PLC_FX3U.ReadBitsDaataRecieved
        Dim value As Integer
        Dim count As Integer = Convert.ToInt32(e.Data2)

        Select Case eFunc
            Case enmTask.Read
                ReDim PlcData(count - 1)
                For i = 0 To count - 1
                    Dim data As String = e.Data.Substring(4 * i, 4)
                    value = Convert.ToInt32(data, 16)
                    PlcData(i) = value
                Next

            Case enmTask.GetMachineStatus
                Dim data As Integer = Convert.ToInt32(e.Data, 16)
                _machineStatus = data

            Case enmTask.GetAlarmCode
                ReDim AlarmCodes(count - 1)
                For i = 0 To count - 1
                    Dim data As String = e.Data.Substring(4 * i, 4)
                    value = Convert.ToInt32(data, 16)
                    AlarmCodes(i) = value
                Next

            Case enmTask.GetCaseteBarCode
                CaseteBarCode = AsciiToString(e.Data, count)

            Case enmTask.GetPassModel
                Pass = IIf(Convert.ToInt32(e.Data, 16) = 1, True, False)

            Case enmTask.GetProductCount
                ProductCount = Convert.ToInt32(e.Data, 16)

            Case enmTask.GetProductNum
                ProductNum = Convert.ToInt32(e.Data, 16)

            Case enmTask.GetSlotStatus
                ReDim SlotStatus(count - 1)
                For i = 0 To count - 1
                    Dim data As String = e.Data.Substring(4 * i, 4)
                    value = Convert.ToInt32(data, 16)
                    SlotStatus(i) = value
                Next

            Case enmTask.GetTemperatures
                ReDim HpTemperatures(count - 1)
                For i = 0 To count - 1
                    Dim data As String = e.Data.Substring(4 * i, 4)
                    value = Convert.ToInt32(data, 16)
                    HpTemperatures(i) = value
                Next

        End Select
    End Sub

    Private Sub PLC_CommandSuccess(sender As Object, e As DataEventArgs) Handles PLC_FX3U.CommandSuccess

    End Sub

    Private Sub PLC_ErrorOccurred(sender As Object, e As DataEventArgs) Handles PLC_FX3U.ErrorOccurred

    End Sub

    Private Function AsciiToString(ByVal ascii As String, ByVal length As Integer) As String
        Dim myByte As Byte
        Dim bytes(length - 1) As Byte
        For i = 0 To length - 1
            Dim d As String = ascii.Substring(4 * i, 4)
            Byte.TryParse(d, myByte)
            bytes(i) = myByte
        Next

        Dim str As String = System.Text.Encoding.Default.GetString(bytes)
        Return str
    End Function

    ''' <summary>
    ''' 判斷動作是否逾時
    ''' </summary>
    ''' <param name="time">毫秒</param>
    Private Function TimeOutCheck(ByVal time As Integer) As Boolean
        If (SwTimeOut.ElapsedMilliseconds > time) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 工作重置
    ''' </summary>
    ''' <remarks>發生異常後須執行重製動作</remarks>
    Public Sub Reset()
        SwTimeOut.Reset()
        PlcStep = enmMotion.Start
        IsBusy = False
    End Sub


End Class

Imports System.IO.Ports

Public Class clsPidData
    Public Comport As String

    ''' <summary>
    ''' 溫度:發生錯誤則回傳Nothing,可用is Nothing判斷
    ''' </summary>
    Public PV As Double?

    ''' <summary>
    ''' 目標溫度:發生錯誤則回傳Nothing,可用is Nothing判斷
    ''' </summary>
    Public SV As Double?

    ''' <summary>
    ''' 溫度補償值:發生錯誤則回傳Nothing,可用is Nothing判斷
    ''' </summary>
    Public PVOS As Double?

    Public IsOpen As Boolean
End Class

Public Class clsSetValue
    Public SvValue As Integer
    Public PvosValue As Integer
    Public Task As enmCtrlerTask = enmCtrlerTask.Null
End Class

Public Enum enmCtrlerTask
    Null
    GetPV
    GetSV
    SetSV
    GetPVOS
    SetPVOS
End Enum

Public Class clsTemperatureController
    Public Enum enmPidController
        A1 = 0
        A2 = 1
        A3 = 2
        A4 = 3
        A5 = 4
        A6 = 5
        B1 = 6
        B2 = 7
        B3 = 8
        B4 = 9
        B5 = 10
        B6 = 11
    End Enum

    Dim arrValue() As clsSetValue = {New clsSetValue, New clsSetValue, New clsSetValue, New clsSetValue, New clsSetValue, New clsSetValue,
                                            New clsSetValue, New clsSetValue, New clsSetValue, New clsSetValue, New clsSetValue, New clsSetValue}

    Dim timerTC As New System.Timers.Timer
    Dim thread As Threading.Thread

    ''' <summary>
    ''' 是否忙碌中
    ''' </summary>
    Dim arrCtrllerBusy() As Boolean = {False, False, False, False, False, False, False, False, False, False, False, False}

    Public ErrorMessage As String
    Private WaitRecieveTime As Integer = 100

    Dim A1_Modbus As New clsPidController
    Dim A2_Modbus As New clsPidController
    Dim A3_Modbus As New clsPidController
    Dim A4_Modbus As New clsPidController
    Dim A5_Modbus As New clsPidController
    Dim A6_Modbus As New clsPidController

    Dim B1_Modbus As New clsPidController
    Dim B2_Modbus As New clsPidController
    Dim B3_Modbus As New clsPidController
    Dim B4_Modbus As New clsPidController
    Dim B5_Modbus As New clsPidController
    Dim B6_Modbus As New clsPidController

    Dim arrModbus() As clsPidController = {A1_Modbus, A2_Modbus, A3_Modbus, A4_Modbus, A5_Modbus, A6_Modbus,
                                           B1_Modbus, B2_Modbus, B3_Modbus, B4_Modbus, B5_Modbus, B6_Modbus}

    Public A1_PidController As clsPidData = A1_Modbus.Data
    Public A2_PidController As clsPidData = A2_Modbus.Data
    Public A3_PidController As clsPidData = A3_Modbus.Data
    Public A4_PidController As clsPidData = A4_Modbus.Data
    Public A5_PidController As clsPidData = A5_Modbus.Data
    Public A6_PidController As clsPidData = A6_Modbus.Data

    Public B1_PidController As clsPidData = B1_Modbus.Data
    Public B2_PidController As clsPidData = B2_Modbus.Data
    Public B3_PidController As clsPidData = B3_Modbus.Data
    Public B4_PidController As clsPidData = B4_Modbus.Data
    Public B5_PidController As clsPidData = B5_Modbus.Data
    Public B6_PidController As clsPidData = B6_Modbus.Data

    Public arrPidController() As clsPidData = {A1_PidController, A2_PidController, A3_PidController, A4_PidController, A5_PidController, A6_PidController,
                                                  B1_PidController, B2_PidController, B3_PidController, B4_PidController, B5_PidController, B6_PidController}

    Dim cmdGetPV As String = "0103008A0001"

    ''' <summary>
    ''' 目標溫度控制
    ''' </summary>
    Dim cmdGetSV As String = "010300000001"
    Dim cmdSetSV As String = "01060000"

    ''' <summary>
    ''' PV補償控制
    ''' </summary>
    Dim cmdGetPVOS As String = "010300650001"
    Dim cmdSetPVOS As String = "01060065"

    Dim BuadRade As Integer = 38400

    Sub New()
        '2017/11/8 測試Mark
        timerTC.Interval = 500
        AddHandler timerTC.Elapsed, AddressOf OnTimedEvent
    End Sub

    Public Function OpenAll() As Boolean
        A1_Modbus.Open(A1_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        A2_Modbus.Open(A2_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        A3_Modbus.Open(A3_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        A4_Modbus.Open(A4_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        A5_Modbus.Open(A5_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        A6_Modbus.Open(A6_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)

        B1_Modbus.Open(B1_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        B2_Modbus.Open(B2_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        B3_Modbus.Open(B3_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        B4_Modbus.Open(B4_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        B5_Modbus.Open(B5_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)
        B6_Modbus.Open(B6_PidController.Comport, BuadRade, Parity.Even, 8, StopBits.One)

        For Each value In arrValue
            value.Task = enmCtrlerTask.GetPV
        Next

        Dim msg As String = ""
        If (A1_Modbus.IsOpen <> True AndAlso A1_PidController.Comport <> "") Then
            msg = msg & "A1,"
        End If

        If (A2_Modbus.IsOpen <> True AndAlso A2_PidController.Comport <> "") Then
            msg = msg & "A2,"
        End If

        If (A3_Modbus.IsOpen <> True AndAlso A3_PidController.Comport <> "") Then
            msg = msg & "A3,"
        End If

        If (A4_Modbus.IsOpen <> True AndAlso A4_PidController.Comport <> "") Then
            msg = msg & "A4,"
        End If

        If (A5_Modbus.IsOpen <> True AndAlso A5_PidController.Comport <> "") Then
            msg = msg & "A5,"
        End If

        If (A6_Modbus.IsOpen <> True AndAlso A6_PidController.Comport <> "") Then
            msg = msg & "A6,"
        End If

        If (B1_Modbus.IsOpen <> True AndAlso B1_PidController.Comport <> "") Then
            msg = msg & "B1,"
        End If

        If (B2_Modbus.IsOpen <> True AndAlso B2_PidController.Comport <> "") Then
            msg = msg & "B2,"
        End If

        If (B3_Modbus.IsOpen <> True AndAlso B3_PidController.Comport <> "") Then
            msg = msg & "B3,"
        End If

        If (B4_Modbus.IsOpen <> True AndAlso B4_PidController.Comport <> "") Then
            msg = msg & "B4,"
        End If

        If (B5_Modbus.IsOpen <> True AndAlso B5_PidController.Comport <> "") Then
            msg = msg & "B5,"
        End If

        If (B6_Modbus.IsOpen <> True AndAlso B6_PidController.Comport <> "") Then
            msg = msg & "B6,"
        End If

        If msg <> "" Then
            ErrorMessage = "Hot plate not open : " & msg
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 關閉所有PID控制器
    ''' </summary>
    Public Sub Close()
        For Each ctrller As clsPidController In arrModbus
            ctrller.Close()
        Next
    End Sub

    ''' <summary>
    ''' 開啟溫度監控
    ''' </summary>
    Public Sub StartWatcher()
        GetPVOS(enmPidController.A1)
        GetPVOS(enmPidController.A2)
        GetPVOS(enmPidController.A3)
        GetPVOS(enmPidController.A4)
        GetPVOS(enmPidController.A5)
        GetPVOS(enmPidController.A6)
        GetPVOS(enmPidController.B1)
        GetPVOS(enmPidController.B2)
        GetPVOS(enmPidController.B3)
        GetPVOS(enmPidController.B4)
        GetPVOS(enmPidController.B5)
        GetPVOS(enmPidController.B6)

        timerTC.Start()
    End Sub

    ''' <summary>
    ''' 關閉溫度監控
    ''' </summary>
    Public Sub StopWatcher()
        timerTC.Stop()
    End Sub

    Private Sub OnTimedEvent(source As Object, e As Timers.ElapsedEventArgs)
        'Dim index As Integer
        'For Each ctrller In arrPidController
        For index = 0 To arrPidController.Length - 1
            If (arrPidController(index).IsOpen) Then
                Dim task As enmCtrlerTask = arrValue(index).Task
                Select Case task
                    Case enmCtrlerTask.GetPV
                        If (arrModbus(index).GetPV()) Then

                        End If
                    Case enmCtrlerTask.GetSV
                        If (arrModbus(index).GetSV()) Then
                            If (arrValue(index).Task = enmCtrlerTask.GetSV) Then
                                arrValue(index).Task = enmCtrlerTask.GetPV
                            End If
                            arrCtrllerBusy(index) = False
                        End If
                    Case enmCtrlerTask.SetSV
                        If (arrModbus(index).SetSV(arrValue(index).SvValue)) Then
                            If (arrValue(index).Task = enmCtrlerTask.SetSV) Then
                                arrValue(index).Task = enmCtrlerTask.GetSV
                            End If
                            arrCtrllerBusy(index) = False
                        End If
                    Case enmCtrlerTask.GetPVOS
                        If (arrModbus(index).GetPVOS()) Then
                            If (arrValue(index).Task = enmCtrlerTask.GetPVOS) Then
                                arrValue(index).Task = enmCtrlerTask.GetPV
                            End If
                            arrCtrllerBusy(index) = False
                        End If
                    Case enmCtrlerTask.SetPVOS
                        If (arrModbus(index).SetPVOS(arrValue(index).PvosValue)) Then
                            If (arrValue(index).Task = enmCtrlerTask.SetPVOS) Then
                                arrValue(index).Task = enmCtrlerTask.GetPV
                            End If
                            arrCtrllerBusy(index) = False
                        End If
                End Select
                'index += 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' 取得當前溫度
    ''' </summary>
    Private Function GetPV(ByVal controller As enmPidController) As Boolean
        Try
            arrValue(controller).Task = enmCtrlerTask.GetPV
            Return True
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    '20161102
    ''' <summary>
    ''' 單位轉換
    ''' </summary>
    Public Function GetPidController(ByVal i As Integer) As Double

        '20161122
        If IsNothing(WetcoConveyor.Unit.TempController.arrPidController(i).PV) Then
            Return 0
        Else
            Return (WetcoConveyor.Unit.TempController.arrPidController(i).PV)
        End If


    End Function

    ''' <summary>設定溫度補償值</summary>
    ''' <param name="controller"></param>
    ''' <param name="temperature">補償1.5度,則輸入15</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPVOS(ByVal controller As enmPidController, ByVal temperature As Short) As Boolean
        Try
            If (arrCtrllerBusy(controller) = False) Then
                arrValue(controller).PvosValue = temperature
                arrValue(controller).Task = enmCtrlerTask.SetPVOS
                arrCtrllerBusy(controller) = True
                Return True
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 取得溫度補償值
    ''' </summary>
    Public Function GetPVOS(ByVal controller As enmPidController) As Boolean
        Try
            If (arrCtrllerBusy(controller) = False) Then
                arrValue(controller).Task = enmCtrlerTask.GetPVOS
                arrCtrllerBusy(controller) = True
                Return True
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 取得控制器目標溫度
    ''' </summary>
    Public Function GetSV(ByVal controller As enmPidController) As Boolean
        Try
            If (arrCtrllerBusy(controller) = False) Then
                arrValue(controller).Task = enmCtrlerTask.GetSV
                arrCtrllerBusy(controller) = True
                Return True
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 設定控制器的目標溫度
    ''' </summary>
    ''' <param name="temperature">目標溫度</param>
    ''' <remarks>溫度20.5度,則輸入205</remarks>
    Public Function SetSV(ByVal controller As enmPidController, ByVal temperature As UShort) As Boolean
        Try
            If (arrCtrllerBusy(controller) = False) Then
                arrValue(controller).SvValue = temperature
                arrValue(controller).Task = enmCtrlerTask.SetSV
                arrCtrllerBusy(controller) = True
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function





    ''' <summary>
    ''' PID回傳值轉為溫度值
    ''' </summary>
    Private Function DataToTemperature(ByVal data As Byte(), ByRef value As Double) As Boolean
        If (data IsNot Nothing) Then
            If data(0).ToString("X2") = "01" AndAlso _
               data(1).ToString("X2") = "03" AndAlso _
               data(2).ToString("X2") = "02" Then
                Dim myHex As String = data(3).ToString("X2") & data(4).ToString("X2")
                value = Convert.ToInt32(myHex, 16)
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' PID回傳值轉為Hex CMD
    ''' </summary>
    Private Function DataToCmd(ByVal data() As Byte) As String
        Dim cmd As String = ""
        For Each bt As Byte In data
            cmd += bt.ToString("X2")
        Next
        Return cmd
    End Function
End Class

''' <summary>
''' 溫度監控類別
''' </summary>
Public Class clsPidWatcher
    Dim timerTC As New System.Timers.Timer

    Public ErrorMessage As String

    Dim _interval As Integer = 500
    ''' <summary>
    ''' 取得或設定監控時間間隔
    ''' </summary>
    Public Property Interval As UInteger
        Get
            Return _interval
        End Get
        Set(value As UInteger)
            _interval = value
            timerTC.Interval = value
        End Set
    End Property

    Dim PID As List(Of clsPidController)
    Public PidData As List(Of clsPidData)
    Dim crtlValue As SortedList(Of Integer, clsSetValue)

    Sub New()
        timerTC.Interval = Interval
        AddHandler timerTC.Elapsed, AddressOf OnTimedEvent
    End Sub

    ''' <summary>
    ''' 加入溫控器
    ''' </summary>
    Public Sub Add(ByVal Controller As clsPidController)
        PID.Add(Controller)
        PidData.Add(Controller.Data)

        crtlValue.Add(PID.Count - 1, New clsSetValue)
        crtlValue(PID.Count - 1).Task = enmCtrlerTask.GetPV
    End Sub

    ''' <summary>
    ''' 移除溫控器
    ''' </summary>
    Public Function Remove(ByVal Controller As clsPidController) As Boolean
        If (PID.Remove(Controller) AndAlso PidData.Remove(Controller.Data) AndAlso crtlValue.Remove(PID.IndexOf(Controller))) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 開啟溫度監控
    ''' </summary>
    Public Sub StartWatcher()
        For index = 0 To PID.Count - 1
            GetPVOS(index)
        Next

        timerTC.Start()
    End Sub

    ''' <summary>
    ''' 關閉溫度監控
    ''' </summary>
    Public Sub StopWatcher()
        timerTC.Stop()
    End Sub

    Private Sub OnTimedEvent(source As Object, e As Timers.ElapsedEventArgs)
        For index = 0 To PidData.Count - 1
            If (PidData(index).IsOpen) Then
                Dim task As enmCtrlerTask = crtlValue(index).Task
                Select Case task
                    Case enmCtrlerTask.GetPV
                        If (PID(index).GetPV()) Then

                        End If

                    Case enmCtrlerTask.GetSV
                        If (PID(index).GetSV()) Then
                            If (crtlValue(index).Task = enmCtrlerTask.GetSV) Then
                                crtlValue(index).Task = enmCtrlerTask.GetPV
                            End If
                        End If

                    Case enmCtrlerTask.SetSV
                        If (PID(index).SetSV(crtlValue(index).SvValue)) Then
                            If (crtlValue(index).Task = enmCtrlerTask.SetSV) Then
                                crtlValue(index).Task = enmCtrlerTask.GetPV
                            End If
                        End If

                    Case enmCtrlerTask.GetPVOS
                        If (PID(index).GetPVOS()) Then
                            If (crtlValue(index).Task = enmCtrlerTask.GetPVOS) Then
                                crtlValue(index).Task = enmCtrlerTask.GetPV
                            End If
                        End If

                    Case enmCtrlerTask.SetPVOS
                        If (PID(index).SetPVOS(crtlValue(index).PvosValue)) Then
                            If (crtlValue(index).Task = enmCtrlerTask.SetPVOS) Then
                                crtlValue(index).Task = enmCtrlerTask.GetPV
                            End If
                        End If
                End Select
            End If
        Next
    End Sub

    ''' <summary>
    ''' 取得當前溫度
    ''' </summary>
    Private Function GetPV(ByVal index As Integer) As Boolean
        Try
            crtlValue(index - 1).Task = enmCtrlerTask.GetPV
            Return True
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>設定溫度補償值</summary>
    ''' <param name="index"></param>
    ''' <param name="temperature">補償1.5度,則輸入15</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPVOS(ByVal index As Integer, ByVal temperature As Short) As Boolean
        Try
            If (PID.Count >= index) Then
                If (PID(index - 1).IsBusy = False) Then
                    crtlValue(index - 1).PvosValue = temperature
                    crtlValue(index - 1).Task = enmCtrlerTask.SetPVOS
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 取得溫度補償值
    ''' </summary>
    Public Function GetPVOS(ByVal index As Integer) As Boolean
        Try
            If (PID.Count >= index) Then
                If (PID(index - 1).IsBusy = False) Then
                    crtlValue(index - 1).Task = enmCtrlerTask.GetPVOS
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 取得控制器目標溫度
    ''' </summary>
    Public Function GetSV(ByVal index As Integer) As Boolean
        Try
            If (PID.Count >= index) Then
                If (PID(index - 1).IsBusy = False) Then
                    crtlValue(index - 1).Task = enmCtrlerTask.GetSV
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 設定控制器的目標溫度
    ''' </summary>
    ''' <param name="temperature">目標溫度</param>
    ''' <remarks>溫度20.5度,則輸入205</remarks>
    Public Function SetSV(ByVal index As Integer, ByVal temperature As UShort) As Boolean
        Try
            If (PID.Count >= index) Then
                If (PID(index - 1).IsBusy = False) Then
                    crtlValue(index - 1).SvValue = temperature
                    crtlValue(index - 1).Task = enmCtrlerTask.SetSV
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            ErrorMessage = ex.ToString()
            Return False
        End Try
    End Function


    ''' <summary>
    ''' PID回傳值轉為溫度值
    ''' </summary>
    Private Function DataToTemperature(ByVal data As Byte(), ByRef value As Double) As Boolean
        If (data IsNot Nothing) Then
            If data(0).ToString("X2") = "01" AndAlso _
               data(1).ToString("X2") = "03" AndAlso _
               data(2).ToString("X2") = "02" Then
                Dim myHex As String = data(3).ToString("X2") & data(4).ToString("X2")
                value = Convert.ToInt32(myHex, 16)
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' PID回傳值轉為Hex CMD
    ''' </summary>
    Private Function DataToCmd(ByVal data() As Byte) As String
        Dim cmd As String = ""
        For Each bt As Byte In data
            cmd += bt.ToString("X2")
        Next
        Return cmd
    End Function

End Class

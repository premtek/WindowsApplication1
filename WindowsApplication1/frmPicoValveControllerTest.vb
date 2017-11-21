Imports ProjectCore
Imports ProjectValveController

''' <summary>
''' 連線設定介面
''' </summary>
''' <remarks></remarks>
Public Class frmPicoValveControllerTest
    ''' <summary>連線參數(編輯用)</summary>
    ''' <remarks></remarks>
    Dim EditParameter(enmValvecontrollerCount.Max) As sValvecontrollerConnectionParameter


    Public gfrmPicoController As frmPicoController

    Private Sub frmPicoValveControllerTest_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub





    Private Sub frmPicoValveControllerTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '資料讀取至編輯區
        For i As Integer = 0 To gValvecontrollerCollection.ConnectionParameter.Count - 1
            EditParameter(i) = gValvecontrollerCollection.ConnectionParameter(i)
        Next
        cbxValvecontrolerType.Items.Clear()
        cbxValvecontrolerType.Items.Add(enmValvecontrollerType.Virtual & " Virtual")
        cbxValvecontrolerType.Items.Add(enmValvecontrollerType.PicoTouch & " PicoTouch")
        cbxValvecontrolerType.SelectedIndex = 1

        cmbItem.Items.Clear()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cmbItem.Items.Add("Controller1")
                cmbItem.Items.Add("Controller2")
                cmbItem.Items.Add("Controller3")
                cmbItem.Items.Add("Controller4")
                lblSelectItem.Visible = True
                cmbItem.Visible = True
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                cmbItem.Items.Add("Controller1")
                cmbItem.Items.Add("Controller2")
                lblSelectItem.Visible = True
                cmbItem.Visible = True
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cmbItem.Items.Add("Controller1")
                        lblSelectItem.Visible = False
                        cmbItem.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        cmbItem.Items.Add("Controller1")
                        cmbItem.Items.Add("Controller2")
                        lblSelectItem.Visible = True
                        cmbItem.Visible = True
                End Select
        End Select

        cmbCOMPort.Items.Clear()
        cmbCOMPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames())

        With cmbBaudRate.Items
            .Clear()
            .Add(CInt(enmBaudRate.e9600))
            .Add(CInt(enmBaudRate.e14400))
            .Add(CInt(enmBaudRate.e19200))
            .Add(CInt(enmBaudRate.e38400))
            .Add(CInt(enmBaudRate.e57600))
            .Add(CInt(enmBaudRate.e115200))
        End With

        If cmbItem.Items.Count > 0 Then
            cmbItem.SelectedIndex = 0
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub cmbItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItem.SelectedIndexChanged
        If cmbItem.SelectedIndex < 0 Then
            Exit Sub
        End If

        With gValvecontrollerCollection.ConnectionParameter(cmbItem.SelectedIndex)
            If .DeviceType < cbxValvecontrolerType.Items.Count Then
                cbxValvecontrolerType.SelectedIndex = CInt(.DeviceType)
            End If

            If cmbCOMPort.Items.Contains("COM" & .PICOTouch.PortName) Then
                cmbCOMPort.SelectedItem = "COM" & .PICOTouch.PortName
            End If

            '[說明]:配接Baud Rate
            Select Case CInt(.PICOTouch.BaudRate)
                Case enmBaudRate.e9600 '.ToString()
                    cmbBaudRate.SelectedIndex = 0
                Case enmBaudRate.e14400 '.ToString()
                    cmbBaudRate.SelectedIndex = 1
                Case enmBaudRate.e19200 '.ToString()
                    cmbBaudRate.SelectedIndex = 2
                Case enmBaudRate.e38400 '.ToString()
                    cmbBaudRate.SelectedIndex = 3
                Case enmBaudRate.e57600 '.ToString()
                    cmbBaudRate.SelectedIndex = 4
                Case enmBaudRate.e115200 '.ToString()
                    cmbBaudRate.SelectedIndex = 5
            End Select
        End With
    End Sub


    Private Sub cbxValvecontrolerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxValvecontrolerType.SelectedIndexChanged
        If cbxValvecontrolerType.SelectedIndex < 0 Then '未選擇
            Exit Sub
        End If
        If cmbItem.SelectedIndex < 0 Then
            Exit Sub
        End If
        EditParameter(cmbItem.SelectedIndex).DeviceType = cbxValvecontrolerType.SelectedIndex
    End Sub
    Private Sub txtPortName_TextChanged(sender As Object, e As EventArgs)
        If cmbItem.SelectedIndex < 0 Then
            Exit Sub
        End If
        EditParameter(cmbItem.SelectedIndex).PICOTouch.PortName = cmbCOMPort.SelectedItem '將操作變更暫存至編輯區
    End Sub

    Private Sub txtBaudRate_TextChanged(sender As Object, e As EventArgs)
        If cmbItem.SelectedIndex < 0 Then
            Exit Sub
        End If

        EditParameter(cmbItem.SelectedIndex).PICOTouch.BaudRate = cmbBaudRate.SelectedItem '將操作變更暫存至編輯區
    End Sub


    Private Sub btnShowcontrolerForm_Click(sender As Object, e As EventArgs) Handles btnShowcontrolerForm.Click
        'Sue20170627
        gSyslog.Save("[frmPicoValveControllerTest]" & vbTab & "[btnShowcontrolerForm]" & vbTab & "Click")
        Select Case cbxValvecontrolerType.SelectedIndex
            Case 0

            Case 1
                If gfrmPicoController Is Nothing Then
                    gfrmPicoController = New frmPicoController
                ElseIf gfrmPicoController.IsDisposed Then
                    gfrmPicoController = New frmPicoController
                End If
                gfrmPicoController._mSelectValve = cmbItem.SelectedIndex
                gfrmPicoController.ShowDialog()
            Case 2


            Case Else

        End Select

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmPicoValveControllerTest]" & vbTab & "[btnOK]" & vbTab & "Click")
        '編輯區資料全部套用
        'For i As Integer = 0 To gVavlecontrollerCollection.ConnectionParameter.Count - 1
        '    With gVavlecontrollerCollection.ConnectionParameter(i)
        '        .DeviceType = EditParameter(i).DeviceType
        '        .PICOTouch = New PicoTouchConncet
        '        .PICOTouch.PortName = EditParameter(i).PICOTouch.PortName
        '        .PICOTouch.BaudRate = EditParameter(i).PICOTouch.BaudRate
        '    End With
        'Next
        '20161010
        With gValvecontrollerCollection.ConnectionParameter(cmbItem.SelectedIndex)
            Dim mPicoToTouch As New PicoTouchConncet
            If cmbCOMPort.SelectedIndex <> Nothing Then
                mPicoToTouch.PortName = cmbCOMPort.SelectedIndex
            End If

            '[說明]:配接Baud Rate
            Select Case cmbBaudRate.SelectedIndex
                Case 0
                    mPicoToTouch.BaudRate = enmBaudRate.e9600
                Case 1
                    mPicoToTouch.BaudRate = enmBaudRate.e14400
                Case 2
                    mPicoToTouch.BaudRate = enmBaudRate.e19200
                Case 3
                    mPicoToTouch.BaudRate = enmBaudRate.e38400
                Case 4
                    mPicoToTouch.BaudRate = enmBaudRate.e57600
                Case 5
                    mPicoToTouch.BaudRate = enmBaudRate.e115200
            End Select

            If cbxValvecontrolerType.SelectedIndex <> Nothing Then
                .DeviceType = cbxValvecontrolerType.SelectedIndex
            End If
            .PICOTouch = mPicoToTouch
        End With

        gValvecontrollerCollection.SaveValveReaderConnection(Application.StartupPath & "\System\" & MachineName & "\ValvecontrollerParam.ini")
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        '  '20161010
        'Me.Close()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmPicoValveControllerTest]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub


    Private Sub frmPicoValveControllerTest_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If cmbItem.Items.Count > 0 Then
            If cmbItem.SelectedIndex < 0 Then cmbItem.SelectedIndex = 0
        End If
    End Sub
End Class

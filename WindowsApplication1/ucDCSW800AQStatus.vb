Imports ProjectCore
Imports WetcoConveyor.mGlobalPool
Imports ProjectRecipe
Imports System.Reflection

Public Class ucDCSW800AQStatus
    Dim TipType As New ToolTip()

    Private Sub ucDCSW800AQStatus_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Me.Dispose(True)
    End Sub

    Private Sub ucDCSW800AQStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateDataGridView()
        If (gAutoMapPath) Then
            cbManualMapData.Checked = False
            btnMacAOpenMap.Enabled = False
            btnMacBOpenMap.Enabled = False
        Else
            cbManualMapData.Checked = True
            btnMacAOpenMap.Enabled = True
            btnMacBOpenMap.Enabled = True
        End If

        Dim str As String() = Split(MFunctionModule.gMapDataPathA, "\")
        tbMapDataA.Text = str(str.Length - 1)
        str = Split(MFunctionModule.gMapDataPathB, "\")
        tbMapDataB.Text = str(str.Length - 1)

        'Eason 20170214 Ticket:100070 : Operator form frash [S]
        Dim Info As Reflection.PropertyInfo = Me.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        Info.SetValue(dgv_LCassetteA, True, Nothing)
        Info.SetValue(dgv_LCassetteB, True, Nothing)
        Info.SetValue(dgv_UnLCassetteA, True, Nothing)
        Info.SetValue(dgv_UnLCassetteB, True, Nothing)
        'Eason 20170214 Ticket:100070 : Operator form frash [E]
    End Sub

    ''' <summary>建立LUL Cassette Slot顯示</summary>
    ''' <remarks></remarks>
    Public Sub CreateDataGridView()
        dgv_LCassetteA.ColumnHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_LCassetteA.DefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_LCassetteB.ColumnHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_LCassetteB.DefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_UnLCassetteA.ColumnHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_UnLCassetteA.DefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_UnLCassetteB.ColumnHeadersDefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)
        dgv_UnLCassetteB.DefaultCellStyle.Font = New Font("微軟正黑體", 12, FontStyle.Regular)

        For i = 0 To 19
            dgv_LCassetteA.Rows.Add()
            dgv_LCassetteA.Rows(i).Cells(0).Value = (i + 1).ToString()
            dgv_LCassetteB.Rows.Add()
            dgv_LCassetteB.Rows(i).Cells(0).Value = (i + 1).ToString()
            dgv_UnLCassetteA.Rows.Add()
            dgv_UnLCassetteA.Rows(i).Cells(0).Value = (i + 1).ToString()
            dgv_UnLCassetteB.Rows.Add()
            dgv_UnLCassetteB.Rows(i).Cells(0).Value = (i + 1).ToString()
        Next

        Dim dgvbtnOpenFileA As New DataGridViewButtonColumn
        dgvbtnOpenFileA.Name = "..."
        dgvbtnOpenFileA.UseColumnTextForButtonValue = True
        dgvbtnOpenFileA.Text = "..."
        dgvbtnOpenFileA.Width = 30
        dgv_LCassetteA.Columns.Add(dgvbtnOpenFileA)

        Dim dgvbtnOpenFileB As New DataGridViewButtonColumn
        dgvbtnOpenFileB.Name = "..."
        dgvbtnOpenFileB.UseColumnTextForButtonValue = True
        dgvbtnOpenFileB.Text = "..."
        dgvbtnOpenFileB.Width = 30
        dgv_LCassetteB.Columns.Add(dgvbtnOpenFileB)

        dgv_LCassetteA.Columns(2).ReadOnly = False
        dgv_LCassetteA.AllowUserToResizeRows = False

        dgv_LCassetteB.Columns(2).ReadOnly = False
        dgv_LCassetteB.AllowUserToResizeRows = False

        'dgv_UnLCassetteA.Columns(2).ReadOnly = False
        dgv_UnLCassetteA.AllowUserToResizeRows = False

        'dgv_UnLCassetteB.Columns(2).ReadOnly = False
        dgv_UnLCassetteB.AllowUserToResizeRows = False

        '停用表頭排序功能
        For i = 0 To dgv_LCassetteA.ColumnCount - 1
            dgv_LCassetteA.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 0 To dgv_LCassetteB.ColumnCount - 1
            dgv_LCassetteB.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 0 To dgv_UnLCassetteA.ColumnCount - 1
            dgv_UnLCassetteA.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 0 To dgv_UnLCassetteB.ColumnCount - 1
            dgv_UnLCassetteB.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        Dim str As String()
        For i = 0 To 19
            If (gCaseteAMapDataList(i) IsNot Nothing) Then
                str = Split(gCaseteAMapDataList(i), "\")
                dgv_LCassetteA.Rows(i).Cells(2).Value() = str(str.Length - 1)
            End If

            If (gCaseteBMapDataList(i) IsNot Nothing) Then
                str = Split(gCaseteBMapDataList(i), "\")
                dgv_LCassetteB.Rows(i).Cells(2).Value() = str(str.Length - 1)
            End If
        Next
    End Sub

    Public Sub ShowMachineStatus()
        If gSYS(eSys.OverAll).ExecuteCommand = eSysCommand.Home Then
            If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home Then
                Select Case gSYS(eSys.MachineA).RunStatus
                    Case enmRunStatus.Finish
                        lblMachineAStatus.Text = "Home Finish"
                    Case enmRunStatus.Running
                        lblMachineAStatus.Text = "Homing"
                    Case enmRunStatus.Alarm
                        lblMachineAStatus.Text = "Alarm"
                    Case enmRunStatus.Stop
                        lblMachineAStatus.Text = "Stop"
                End Select
            End If
            If gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.Home Then
                Select Case gSYS(eSys.MachineB).RunStatus
                    Case enmRunStatus.Finish
                        lblMachineBStatus.Text = "Home Finish"
                    Case enmRunStatus.Running
                        lblMachineBStatus.Text = "Homing"
                    Case enmRunStatus.Alarm
                        lblMachineBStatus.Text = "Alarm"
                    Case enmRunStatus.Stop
                        lblMachineBStatus.Text = "Stop"
                End Select
            End If
        End If
        If gSYS(eSys.OverAll).ExecuteCommand = eSysCommand.AutoRun Then
            Select Case gSYS(eSys.OverAll).RunStatus
                Case enmRunStatus.Stop
                    lblMachineAStatus.Text = "Stop"
                    lblMachineBStatus.Text = "Stop"
                Case enmRunStatus.Alarm
                    lblMachineAStatus.Text = "Alarm"
                    lblMachineBStatus.Text = "Alarm"
                Case enmRunStatus.Running
                    If gSYS(eSys.OverAll).SysNum <= 3100 Then
                        lblMachineAStatus.Text = "Loading"
                        lblMachineBStatus.Text = "Loading"
                    ElseIf gSYS(eSys.OverAll).SysNum >= 4100 Then
                        lblMachineAStatus.Text = "Unloading"
                        lblMachineBStatus.Text = "Unloading"
                    Else
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AutoRun Then
                            Select Case gSYS(eSys.MachineA).SysNum
                                Case 2000, 2100
                                    lblMachineAStatus.Text = "Loading"
                                Case 3000, 3100
                                    lblMachineAStatus.Text = "Alignment"
                                Case 4000, 4100
                                    lblMachineAStatus.Text = "Find Z Height"
                                Case 6000, 6100
                                    lblMachineAStatus.Text = "Dispensing"
                                Case 9000
                                    lblMachineAStatus.Text = "Finish"
                            End Select
                        End If
                        If gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.AutoRun Then
                            Select Case gSYS(eSys.MachineB).SysNum
                                Case 2000, 2100
                                    lblMachineBStatus.Text = "Loading"
                                Case 3000, 3100
                                    lblMachineBStatus.Text = "Alignment"
                                Case 4000, 4100
                                    lblMachineBStatus.Text = "Find Z Height"
                                Case 6000, 6100
                                    lblMachineBStatus.Text = "Dispensing"
                                Case 9000
                                    lblMachineBStatus.Text = "Finish"
                            End Select
                        End If
                    End If
            End Select


        End If

        lblMachineAWafer.Text = cls800AQ_LUL.A_ProductNum
        lblMachineBWafer.Text = cls800AQ_LUL.B_ProductNum
    End Sub

    Public Sub LoaderCaseteData()

        For i = 0 To cls800AQ_LUL.LoaderData.SlotStatusA.Length - 1

            'Eason 20170214 Ticket:100070 : Operator form frash [S]
            If (IsNothing(dgv_LCassetteA.Rows(i).Cells(1).Value()) OrElse _
                dgv_LCassetteA.Rows(i).Cells(1).Tag.ToString() <> cls800AQ_LUL.LoaderData.SlotStatusA(i).ToString()) Then
                dgv_LCassetteA.Rows(i).Cells(1).Tag = cls800AQ_LUL.LoaderData.SlotStatusA(i).ToString()
                dgv_LCassetteA.Rows(i).Cells(1).Value() = ImageList1.Images(cls800AQ_LUL.LoaderData.SlotStatusA(i))
            End If
            If (IsNothing(dgv_LCassetteB.Rows(i).Cells(1).Value()) OrElse _
                dgv_LCassetteB.Rows(i).Cells(1).Tag.ToString() <> cls800AQ_LUL.LoaderData.SlotStatusB(i).ToString()) Then
                dgv_LCassetteB.Rows(i).Cells(1).Tag = cls800AQ_LUL.LoaderData.SlotStatusB(i).ToString()
                dgv_LCassetteB.Rows(i).Cells(1).Value() = ImageList1.Images(cls800AQ_LUL.LoaderData.SlotStatusB(i))
            End If
            'dgv_LCassetteA.Rows(i).Cells(1).Value() = ImageList1.Images(Conveyor.LoaderData.SlotStatusA(i))
            'dgv_LCassetteB.Rows(i).Cells(1).Value() = ImageList1.Images(Conveyor.LoaderData.SlotStatusB(i))
            'Eason 20170214 Ticket:100070 : Operator form frash [E]
        Next
    End Sub


    Public Sub UnloaderCaseteData()
        For i = 0 To cls800AQ_LUL.UnloaderData.SlotStatusA.Length - 1

            'Eason 20170214 Ticket:100070 : Operator form frash [S]
            If (IsNothing(dgv_UnLCassetteA.Rows(i).Cells(1).Value()) OrElse _
                dgv_UnLCassetteA.Rows(i).Cells(1).Tag.ToString() <> cls800AQ_LUL.UnloaderData.SlotStatusA(i).ToString()) Then
                dgv_UnLCassetteA.Rows(i).Cells(1).Tag = cls800AQ_LUL.UnloaderData.SlotStatusA(i).ToString()
                dgv_UnLCassetteA.Rows(i).Cells(1).Value() = ImageList1.Images(cls800AQ_LUL.UnloaderData.SlotStatusA(i))
            End If

            If (IsNothing(dgv_UnLCassetteB.Rows(i).Cells(1).Value()) OrElse _
                dgv_UnLCassetteB.Rows(i).Cells(1).Tag.ToString() <> cls800AQ_LUL.UnloaderData.SlotStatusB(i).ToString()) Then
                dgv_UnLCassetteB.Rows(i).Cells(1).Tag = cls800AQ_LUL.UnloaderData.SlotStatusB(i).ToString()
                dgv_UnLCassetteB.Rows(i).Cells(1).Value() = ImageList1.Images(cls800AQ_LUL.UnloaderData.SlotStatusB(i))
            End If
            'dgv_UnLCassetteA.Rows(i).Cells(1).Value() = ImageList1.Images(Conveyor.UnloaderData.SlotStatusA(i))
            'dgv_UnLCassetteB.Rows(i).Cells(1).Value() = ImageList1.Images(Conveyor.UnloaderData.SlotStatusB(i))
            'Eason 20170214 Ticket:100070 : Operator form frash [E]
        Next

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        If (CoverMapData(enmMachineStation.MachineA, MFunctionModule.gMapDataPathA) = MapErrorCode.Success) Then
            If (DataOutputAseMap(enmMachineStation.MachineA, "D:\\MapDataTest\\ASEmA.txt") = False) Then
                'ERROR A : DataOutputAseMap
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2081106))
                MsgBox(gMsgHandler.GetMessage(Alarm_2081106), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            'ERROR A : CoverMapData
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2081107))
            MsgBox(gMsgHandler.GetMessage(Alarm_2081107), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

        If (CoverMapData(enmMachineStation.MachineB, MFunctionModule.gMapDataPathB) = MapErrorCode.Success) Then
            If (DataOutputAseMap(enmMachineStation.MachineB, "D:\\MapDataTest\\ASEmB.txt") = False) Then
                'ERROR B : DataOutputAseMap
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2082106))
                MsgBox(gMsgHandler.GetMessage(Alarm_2082106), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            'ERROR B : CoverMapData
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2082107))
            MsgBox(gMsgHandler.GetMessage(Alarm_2082107), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

        ' ''Toby test
        'Dim ChangeMapA As frmModifyDie = New frmModifyDie(MCommonRecipe.gStageMap(0), MCommonRecipe.gStageMap(1))
        'ChangeMapA.ShowDialog()

        ' ''Toby test

    End Sub

    Private Sub btnMacAOpenMap_Click(sender As Object, e As EventArgs) Handles btnMacAOpenMap.Click
        Dim openDialog As New OpenFileDialog
        If (openDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then
            MFunctionModule.gMapDataPathA = openDialog.FileName
            tbMapDataA.Text = openDialog.SafeFileName
        End If
    End Sub

    Private Sub btnMacBOpenMap_Click(sender As Object, e As EventArgs) Handles btnMacBOpenMap.Click
        Dim openDialog As New OpenFileDialog
        If (openDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then
            MFunctionModule.gMapDataPathB = openDialog.FileName
            tbMapDataB.Text = openDialog.SafeFileName
        End If
    End Sub

    Private Sub dgv_LCassetteA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_LCassetteA.CellClick
        If (e.RowIndex > -1) AndAlso (e.ColumnIndex = 3) Then '判斷是否為button
            Dim openDialog As New OpenFileDialog
            openDialog.Filter = "Cursor Files|*.csv"
            If (openDialog.ShowDialog = DialogResult.OK) Then
                gCaseteAMapDataList(e.RowIndex) = openDialog.FileName
                Dim str As String() = Split(openDialog.FileName, "\")
                dgv_LCassetteA.Rows(e.RowIndex).Cells(2).Value = str(str.Length - 1)
            End If
        End If
    End Sub

    Private Sub dgv_LCassetteB_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_LCassetteB.CellClick
        If (e.RowIndex > -1) AndAlso (e.ColumnIndex = 3) Then '判斷是否為button
            Dim openDialog As New OpenFileDialog
            openDialog.Filter = "Cursor Files|*.csv"
            If (openDialog.ShowDialog = DialogResult.OK) Then
                gCaseteBMapDataList(e.RowIndex) = openDialog.FileName
                Dim str As String() = Split(openDialog.FileName, "\")
                dgv_LCassetteB.Rows(e.RowIndex).Cells(2).Value = str(str.Length - 1)
            End If
        End If
    End Sub

    Private Sub cbManualMapData_CheckedChanged(sender As Object, e As EventArgs) Handles cbManualMapData.CheckedChanged
        If (cbManualMapData.Checked) Then
            btnMacAOpenMap.Enabled = True
            btnMacBOpenMap.Enabled = True
            gAutoMapPath = False
        Else
            btnMacAOpenMap.Enabled = False
            btnMacBOpenMap.Enabled = False
            gAutoMapPath = True
        End If
    End Sub

    Private Sub dgv_LCassetteA_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_LCassetteA.CellEndEdit
        If (dgv_LCassetteA.Rows(e.RowIndex).Cells(2).Value = "") Then
            gCaseteAMapDataList(e.RowIndex) = ""
        End If
    End Sub

    Private Sub dgv_LCassetteB_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_LCassetteB.CellEndEdit
        If (dgv_LCassetteB.Rows(e.RowIndex).Cells(2).Value = "") Then
            gCaseteBMapDataList(e.RowIndex) = ""
        End If
    End Sub

    Private Sub btnCleanAWaferId_Click(sender As Object, e As EventArgs) Handles btnCleanAWaferId.Click
        cls800AQ_LUL.A_ProductNum = 0
    End Sub

    Private Sub btnCleanBWaferId_Click(sender As Object, e As EventArgs) Handles btnCleanBWaferId.Click
        cls800AQ_LUL.B_ProductNum = 0
    End Sub
End Class

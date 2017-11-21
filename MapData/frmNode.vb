Imports ProjectRecipe
Imports ProjectCore
Imports System.Windows.Forms

Public Class frmNode
    Enum enmDirection
        Top
        Bottom
        Left
        Right
    End Enum
    Public RecipeEdit As ProjectRecipe.CRecipe

    Dim DrawMapA As clsRectangleDraw
    Dim DrawMapB As clsRectangleDraw
    Dim Map1Column, Map1Row As Integer
    Dim Map2Column, Map2Row As Integer
    Dim DieSize1 As Integer = 10
    Dim DieSize2 As Integer = 10
    Dim WaferADirection As enmDirection = enmDirection.Bottom
    Dim WaferBDirection As enmDirection = enmDirection.Bottom

    'Map data 陣列大小 
    Dim Data1Column As Integer
    Dim Data1Row As Integer
    Dim Data2Column As Integer
    Dim Data2Row As Integer

    Dim maxColumn As Integer = 1000
    Dim maxRow As Integer = 1000
    Dim trayArray1(maxColumn, maxRow) As Integer
    Dim trayArray2(maxColumn, maxRow) As Integer

    Private Sub frmNode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DrawMapA = New clsRectangleDraw(picANode1)
        DrawMapB = New clsRectangleDraw(picBNode1)

        If (RecipeEdit.Node(enmStage.No1) IsNot Nothing) Then
            For Each Str As String In RecipeEdit.Node(enmStage.No1).Keys()
                Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No1)(Str).Array)
                Dim nodeData As String = Str & "[(" & mMultiArrayAdapter.GetMemoryCountX & "x" & mMultiArrayAdapter.GetMemoryCountY & "); (" & RecipeEdit.Node(enmStage.No1)(Str).NodeStartingX & "," & RecipeEdit.Node(enmStage.No1)(Str).NodeStartingY & ")]"
                chklbStage1WorkNode.Items.Add(nodeData)

                For Each node In RecipeEdit.NodeToMap(enmStage.No1)
                    If (node = Str) Then
                        chklbStage1WorkNode.SetItemChecked(chklbStage1WorkNode.Items.Count - 1, True)
                    End If
                Next
            Next
        End If

        If (RecipeEdit.Node(enmStage.No2) IsNot Nothing) Then
            For Each Str As String In RecipeEdit.Node(enmStage.No2).Keys()
                Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No2)(Str).Array)
                Dim nodeData As String = Str & "[(" & mMultiArrayAdapter.GetMemoryCountX & "x" & mMultiArrayAdapter.GetMemoryCountY & "); (" & RecipeEdit.Node(enmStage.No2)(Str).NodeStartingX & "," & RecipeEdit.Node(enmStage.No2)(Str).NodeStartingY & ")]"
                chklbStage2WorkNode.Items.Add(nodeData)

                For Each node In RecipeEdit.NodeToMap(enmStage.No2)
                    If (node = Str) Then
                        chklbStage2WorkNode.SetItemChecked(chklbStage2WorkNode.Items.Count - 1, True)
                    End If
                Next
            Next
        End If

        If (RecipeEdit.Node(enmStage.No3) IsNot Nothing) Then
            For Each Str As String In RecipeEdit.Node(enmStage.No3).Keys()
                Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No3)(Str).Array)
                Dim nodeData As String = Str & "[(" & mMultiArrayAdapter.GetMemoryCountX & "x" & mMultiArrayAdapter.GetMemoryCountY & "); (" & RecipeEdit.Node(enmStage.No3)(Str).NodeStartingX & "," & RecipeEdit.Node(enmStage.No3)(Str).NodeStartingY & ")]"
                chklbStage3WorkNode.Items.Add(nodeData)

                For Each node In RecipeEdit.NodeToMap(enmStage.No3)
                    If (node = Str) Then
                        chklbStage3WorkNode.SetItemChecked(chklbStage3WorkNode.Items.Count - 1, True)
                    End If
                Next
            Next
        End If

        If (RecipeEdit.Node(enmStage.No4) IsNot Nothing) Then
            For Each Str As String In RecipeEdit.Node(enmStage.No4).Keys()
                Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No4)(Str).Array)
                Dim nodeData As String = Str & "[(" & mMultiArrayAdapter.GetMemoryCountX & "x" & mMultiArrayAdapter.GetMemoryCountY & "); (" & RecipeEdit.Node(enmStage.No4)(Str).NodeStartingX & "," & RecipeEdit.Node(enmStage.No4)(Str).NodeStartingY & ")]"
                chklbStage4WorkNode.Items.Add(nodeData)

                For Each node In RecipeEdit.NodeToMap(enmStage.No4)
                    If (node = Str) Then
                        chklbStage4WorkNode.SetItemChecked(chklbStage4WorkNode.Items.Count - 1, True)
                    End If
                Next
            Next
        End If


        For i = 0 To chklbBin.Items.Count - 1
            Dim bin As String = chklbBin.Items.Item(i).ToString().Split(" ")(1)
            chklbBin.SetItemChecked(i, RecipeEdit.sBinData.Item(bin).Disable)
        Next
        For i = 0 To chklbBin2.Items.Count - 1
            Dim bin As String = chklbBin2.Items.Item(i).ToString().Split(" ")(1)
            chklbBin2.SetItemChecked(i, RecipeEdit.sBinData.Item(bin).Disable)
        Next

        chklbBin2.SetItemChecked(23, True)

        rbNotchTop.Checked = False
        rbNotchRight.Checked = False
        rbNotchBottom.Checked = False
        rbNotchLeft.Checked = False

        If (RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Top) Then
            rbNotchTop.Checked = True
        ElseIf (RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Right) Then
            rbNotchRight.Checked = True
        ElseIf (RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Bottom) Then
            rbNotchBottom.Checked = True
        ElseIf (RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Left) Then
            rbNotchLeft.Checked = True
        Else
            rbNotchBottom.Checked = True
        End If


        '20161114
        '[說明]:關閉目前不使用的項目
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
            Case enmMachineType.DCS_500AD
                TabControl1.Controls.Remove(tb_B)
                TabControl1.TabPages(0).Text = "Machine"
                gboxStage2.Visible = True
            Case Else
                TabControl1.Controls.Remove(tb_B)
                TabControl1.TabPages(0).Text = "Machine"
                gboxStage2.Visible = False
        End Select

    End Sub

#Region "A機"
    ''' <summary>
    ''' 比對Stage1, Stage2 陣列大小來決定Die Size,並繪圖
    ''' </summary>
    Private Sub DrawAPic()
        'Dim minWidth As Integer
        'Dim minHeight As Integer

        'If Stage1NodeColumn > Stage2NodeColumn Then
        '    minWidth = IIf(Stage1NodeColumn = 0, 0, (picANode1.Size.Width - 60) / Stage1NodeColumn)
        'Else
        '    minWidth = IIf(Stage2NodeColumn = 0, 0, (picANode1.Size.Width - 60) / Stage2NodeColumn)
        'End If

        'If Stage1NodeRow > Stage2NodeRow Then
        '    minHeight = IIf(Stage1NodeRow = 0, 0, (picANode1.Size.Height - 60) / Stage1NodeRow)
        'Else
        '    minHeight = IIf(Stage2NodeRow = 0, 0, (picANode1.Size.Height - 60) / Stage2NodeRow)
        'End If

        'rdANode1.SetDieSize(minWidth, minHeight)
        'rdANode1.DrawRectangleArray(Stage1NodeColumn, Stage1NodeRow, False)
        'rdANode2.SetDieSize(minWidth, minHeight)
        'rdANode2.DrawRectangleArray(Stage2NodeColumn, Stage2NodeRow, False)
    End Sub

    Private Sub rbANotchTop_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotchTop.CheckedChanged
        WaferADirection = enmDirection.Top
    End Sub

    Private Sub rbANotchBottom_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotchBottom.CheckedChanged
        WaferADirection = enmDirection.Bottom
    End Sub

    Private Sub rbANotchLeft_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotchLeft.CheckedChanged
        WaferADirection = enmDirection.Left
    End Sub

    Private Sub rbANotchRight_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotchRight.CheckedChanged
        WaferADirection = enmDirection.Right
    End Sub
#End Region

#Region "B機"
    ''' <summary>
    ''' 比對Stage3, Stage4陣列大小來決定Die Size,並繪圖
    ''' </summary>
    Private Sub DrawBPic()
        'Dim minWidth As Integer
        'Dim minHeight As Integer

        'If Stage3Column > Stage4Column Then
        '    minWidth = IIf(Map2Column = 0, 0, (picBNode1.Size.Width - 60) / Map2Column)
        'Else
        '    minWidth = IIf(Stage4Column = 0, 0, (picBNode1.Size.Width - 60) / Stage4Column)
        'End If

        'If Stage3Row > Stage4Row Then
        '    minHeight = IIf(Map2Row = 0, 0, (picBNode1.Size.Height - 60) / Map2Row)
        'Else
        '    minHeight = IIf(Stage4Row = 0, 0, (picBNode1.Size.Height - 60) / Stage4Row)
        'End If

        'DrawMapB.SetDieSize(minWidth, minHeight)
        'DrawMapB.DrawRectangleArray(Map2Column, Map2Row, False)
        'DrawStage4.SetDieSize(minWidth, minHeight)
        'DrawStage4.DrawRectangleArray(Stage4Column, Stage4Row, False)
    End Sub

    Private Sub rbBNotchTop_CheckedChanged(sender As Object, e As EventArgs)
        WaferBDirection = enmDirection.Top
    End Sub

    Private Sub rbBNotchBottom_CheckedChanged(sender As Object, e As EventArgs)
        WaferBDirection = enmDirection.Bottom
    End Sub

    Private Sub rbBNotchLeft_CheckedChanged(sender As Object, e As EventArgs)
        WaferBDirection = enmDirection.Left
    End Sub

    Private Sub rbBNotchRight_CheckedChanged(sender As Object, e As EventArgs)
        WaferBDirection = enmDirection.Right
    End Sub
#End Region

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Sue20170627
        gSyslog.Save("[frmNode]" & vbTab & "[btnSave]" & vbTab & "Click")
        MachineA_NodeListUpdate()
        MachineB_NodeListUpdate()

        If (chklbStage1WorkNode.CheckedIndices.Count = 0 AndAlso chklbStage2WorkNode.CheckedIndices.Count = 0 AndAlso chklbStage3WorkNode.CheckedIndices.Count = 0 AndAlso chklbStage4WorkNode.CheckedIndices.Count = 0) Then
            'sue0428
            'Node不存在，請先選擇
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        'Map File與Node陣列大小比對
        If (CheckMapSize(1) = False Or CheckMapSize(2) = False) Then
            If MsgBox("Node size is not match. Still want to save it?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "WARNING") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        ''目前只能設定同一份MAPPINGDATA只能選相同的PATTERN
        Dim PatternID As String = ""
        For intStage As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            For Each NodeStr As String In RecipeEdit.Node(intStage).Keys()
                RecipeEdit.Node(intStage)(NodeStr).IsMapping = False
            Next
        Next

        '設定Stage1那些Node需要點膠
        If (RecipeEdit.NodeToMap(enmStage.No1) IsNot Nothing) Then
            RecipeEdit.NodeToMap(enmStage.No1).Clear()
            For i = 0 To chklbStage1WorkNode.Items.Count - 1
                Dim str As String = chklbStage1WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)
                RecipeEdit.Node(enmStage.No1)(node).Enable = chklbStage1WorkNode.GetItemChecked(i)
                If (chklbStage1WorkNode.GetItemChecked(i)) Then
                    RecipeEdit.NodeToMap(enmStage.No1).Add(node)
                    RecipeEdit.Node(enmStage.No1)(node).IsMapping = True
                    PatternID = RecipeEdit.Node(enmStage.No1)(node).PatternName
                End If
            Next
        End If

        '設定Stage2那些Node需要點膠
        If (RecipeEdit.NodeToMap(enmStage.No2) IsNot Nothing) Then
            RecipeEdit.NodeToMap(enmStage.No2).Clear()
            For i = 0 To chklbStage2WorkNode.Items.Count - 1
                Dim str As String = chklbStage2WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)
                RecipeEdit.Node(enmStage.No2)(node).Enable = chklbStage2WorkNode.GetItemChecked(i)
                If (chklbStage2WorkNode.GetItemChecked(i)) Then
                    RecipeEdit.NodeToMap(enmStage.No2).Add(node)
                    RecipeEdit.Node(enmStage.No2)(node).IsMapping = True
                    PatternID = RecipeEdit.Node(enmStage.No2)(node).PatternName
                End If
            Next
        End If

        '設定Stage3那些Node需要點膠
        If (RecipeEdit.NodeToMap(enmStage.No3) IsNot Nothing) Then
            RecipeEdit.NodeToMap(enmStage.No3).Clear()
            For i = 0 To chklbStage3WorkNode.Items.Count - 1
                Dim str As String = chklbStage3WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)
                RecipeEdit.Node(enmStage.No3)(node).Enable = chklbStage3WorkNode.GetItemChecked(i)
                If (chklbStage3WorkNode.GetItemChecked(i)) Then
                    RecipeEdit.NodeToMap(enmStage.No3).Add(node)
                    RecipeEdit.Node(enmStage.No3)(node).IsMapping = True
                    PatternID = RecipeEdit.Node(enmStage.No3)(node).PatternName
                End If
            Next
        End If

        '設定Stage4那些Node需要點膠
        If (RecipeEdit.NodeToMap(enmStage.No4) IsNot Nothing) Then
            RecipeEdit.NodeToMap(enmStage.No4).Clear()
            For i = 0 To chklbStage4WorkNode.Items.Count - 1
                Dim str As String = chklbStage4WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)
                RecipeEdit.Node(enmStage.No4)(node).Enable = chklbStage4WorkNode.GetItemChecked(i)
                If (chklbStage4WorkNode.GetItemChecked(i)) Then
                    RecipeEdit.NodeToMap(enmStage.No4).Add(node)
                    RecipeEdit.Node(enmStage.No4)(node).IsMapping = True
                    PatternID = RecipeEdit.Node(enmStage.No4)(node).PatternName
                End If
            Next
        End If


        '設定Bin 0~9要打或不打
        For i = 0 To chklbBin.Items.Count - 1
            Dim tempBin As New BinMappingData
            If CBool(chklbBin.GetItemChecked(i)) Then
                tempBin.Disable = True
                tempBin.PatternName = ""
            Else
                tempBin.Disable = False
                tempBin.PatternName = PatternID
            End If
            RecipeEdit.sBinData(chklbBin.Items.Item(i).ToString().Split(" ")(1)) = tempBin
            tempBin = Nothing
        Next

        '設定Bin A~Z要打或不打
        For i = 0 To chklbBin2.Items.Count - 1
            Dim tempBin As New BinMappingData
            If CBool(chklbBin2.GetItemChecked(i)) Then
                tempBin.Disable = True
                tempBin.PatternName = ""
            Else
                tempBin.Disable = False
                tempBin.PatternName = PatternID
            End If
            RecipeEdit.sBinData(chklbBin2.Items.Item(i).ToString().Split(" ")(1)) = tempBin
            tempBin = Nothing
        Next

        '設定Notch方向
        If (rbNotchTop.Checked) Then
            RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Top
            RecipeEdit.NotchDir(1) = clsMapData.enmDirection.Top
        ElseIf (rbNotchRight.Checked) Then
            RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Right
            RecipeEdit.NotchDir(1) = clsMapData.enmDirection.Right
        ElseIf (rbNotchBottom.Checked) Then
            RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Bottom
            RecipeEdit.NotchDir(1) = clsMapData.enmDirection.Bottom
        ElseIf (rbNotchLeft.Checked) Then
            RecipeEdit.NotchDir(0) = clsMapData.enmDirection.Left
            RecipeEdit.NotchDir(1) = clsMapData.enmDirection.Left
        End If

        'Sue20170627
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[frmNode]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOpenMapA_Click(sender As Object, e As EventArgs) Handles btnOpenMapA.Click
        'Dim openDialog As New OpenFileDialog
        'openDialog.Filter = "Cursor Files|*.csv| Txt Files|*.txt"
        'If (openDialog.ShowDialog = DialogResult.OK) Then
        '    Dim mapData As New clsMapData
        '    If (mapData.OpenFile(openDialog.FileName)) Then
        '       Data1Column = mapData.Information.Column
        '       Data1Row = mapData.Information.Row
        '       labMapPathA.Text = openDialog.FileName
        '    Else
        '        MsgBox("Failed to open file")
        '    End If
        'End If

        Try
            Dim openDialog As New OpenFileDialog
            openDialog.Filter = "Cursor Files|*.csv| Txt Files|*.txt"
            If (openDialog.ShowDialog = DialogResult.OK) Then

                Dim path As String = openDialog.FileName
                Dim arrStr() As String = path.Split("\\")
                Dim fileName() As String = arrStr(arrStr.Length - 1).Split(".")
                Dim savePath As String = "D:\\PIIData\\MappingData\\Source\\"
                Dim filePath As String = "D:\\PIIData\\MappingData\\Source\\" & fileName(0) & ".csv"

                Dim mapData As New clsMapData
                If (mapData.OpenFile(path)) Then
                    Data1Column = mapData.Information.Column
                    Data1Row = mapData.Information.Row
                    labMapPathA.Text = openDialog.FileName

                ElseIf (mapData.WaferMapConvertToPIIMap(path, savePath)) Then
                    If mapData.OpenFile(filePath) Then
                        Data1Column = mapData.Information.Column
                        Data1Row = mapData.Information.Row
                        labMapPathA.Text = openDialog.FileName
                    End If
                Else
                    'sue0428
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000033))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000033), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
            End If
        Catch ex As Exception
            'Sue20170512
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000033))
            MsgBox(gMsgHandler.GetMessage(Warn_3000033), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

    End Sub

    Private Sub btnOpenMapB_Click(sender As Object, e As EventArgs) Handles btnOpenMapB.Click
        'Dim openDialog As New OpenFileDialog
        'openDialog.Filter = "Cursor Files|*.csv| Txt Files|*.txt"   
        'If (openDialog.ShowDialog = DialogResult.OK) Then
        '    Dim mapData As New clsMapData
        '    If (mapData.OpenFile(openDialog.FileName)) Then
        '        Data2Column = mapData.Information.Column
        '        Data2Row = mapData.Information.Row
        '        labMapPathB.Text = openDialog.FileName
        '    Else
        '        MsgBox("Failed to open file")
        '    End If
        'End If

        Try
            Dim openDialog As New OpenFileDialog
            openDialog.Filter = "Cursor Files|*.csv| Txt Files|*.txt"
            If (openDialog.ShowDialog = DialogResult.OK) Then

                Dim path As String = openDialog.FileName
                Dim arrStr() As String = path.Split("\\")
                Dim fileName() As String = arrStr(arrStr.Length - 1).Split(".")
                Dim savePath As String = "D:\\PIIData\\MappingData\\Source\\"
                Dim filePath As String = "D:\\PIIData\\MappingData\\Source\\" & fileName(0) & ".csv"

                Dim mapData As New clsMapData
                If (mapData.OpenFile(path)) Then
                    Data2Column = mapData.Information.Column
                    Data2Row = mapData.Information.Row
                    labMapPathB.Text = openDialog.FileName

                ElseIf (mapData.WaferMapConvertToPIIMap(path, savePath)) Then
                    If mapData.OpenFile(filePath) Then
                        Data2Column = mapData.Information.Column
                        Data2Row = mapData.Information.Row
                        labMapPathB.Text = openDialog.FileName
                    End If
                Else
                    'sue0428
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000033))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000033), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
            End If
        Catch ex As Exception
            'sue0428
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000033))
            MsgBox(gMsgHandler.GetMessage(Warn_3000033), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    Private Sub chklbStage1WorkNode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chklbStage1WorkNode.SelectedIndexChanged, chklbStage2WorkNode.SelectedIndexChanged
        MachineA_NodeListUpdate()
    End Sub

    Private Sub chklbStage3WorkNode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chklbStage3WorkNode.SelectedIndexChanged, chklbStage4WorkNode.SelectedIndexChanged
        MachineB_NodeListUpdate()
    End Sub

    Private Function CheckMapSize(ByVal machineNo As Integer) As Boolean
        Dim mapFile As String
        Dim dataColumn As Integer
        Dim dataRow As Integer
        Dim trayArray(,) As Integer
        Dim nodeColumn As Integer
        Dim nodeRow As Integer

        If (machineNo = 1) Then
            mapFile = labMapPathA.Text
            dataColumn = Data1Column
            dataRow = Data1Row
            trayArray = trayArray1
            nodeColumn = Map1Column
            nodeRow = Map1Row
        Else
            mapFile = labMapPathB.Text
            dataColumn = Data2Column
            dataRow = Data2Row
            trayArray = trayArray2
            nodeColumn = Map2Column
            nodeRow = Map2Row
        End If

        '[Note]:比對Map與Node List陣列大小
        If (mapFile <> "...") Then
            If ((dataColumn <> nodeColumn) Or (dataRow <> nodeRow)) Then
                Return False
            End If
        End If

        '[Note]:檢查Node陣列是否為一個完整的矩形陣列,並排除重複(value > 1)或遺漏(value = 0)的地方
        For c = 0 To nodeColumn - 1
            For r = 0 To nodeRow - 1
                If (trayArray(c, r) <> 1) Then
                    Return False
                End If
            Next
        Next

        Return True
    End Function

    Private Sub MachineA_NodeListUpdate()
        Try
            Map1Column = 0
            Map1Row = 0

            DrawMapA.Clean()
            DrawMapA.SetDieSize(DieSize1, DieSize1)

            For x = 0 To trayArray1.GetUpperBound(0) - 1
                For y = 0 To trayArray1.GetUpperBound(1) - 1
                    trayArray1(x, y) = 0
                Next
            Next

            For i = 0 To chklbStage1WorkNode.Items.Count - 1
                Dim str As String = chklbStage1WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)

                If (chklbStage1WorkNode.GetItemChecked(i)) Then
                    Dim x As Integer = RecipeEdit.Node(enmStage.No1)(node).NodeStartingX - 1
                    Dim y As Integer = RecipeEdit.Node(enmStage.No1)(node).NodeStartingY - 1
                    Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No1)(node).Array)
                    Dim column As Integer = mMultiArrayAdapter.GetMemoryCountX
                    Dim row As Integer = mMultiArrayAdapter.GetMemoryCountY
                    For c = 0 To column - 1
                        For r = 0 To row - 1
                            trayArray1(x + c, y + r) += 1
                        Next
                    Next

                    DrawMapA.DrawRectangleArray(x, y, column, row)

                    If (Map1Column < column + x) Then
                        Map1Column = column + x
                    End If
                    If (Map1Row < row + y) Then
                        Map1Row = row + y
                    End If
                End If
            Next

            For i = 0 To chklbStage2WorkNode.Items.Count - 1
                Dim str As String = chklbStage2WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)

                If (chklbStage2WorkNode.GetItemChecked(i)) Then
                    Dim x As Integer = RecipeEdit.Node(enmStage.No2)(node).NodeStartingX - 1
                    Dim y As Integer = RecipeEdit.Node(enmStage.No2)(node).NodeStartingY - 1
                    Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No2)(node).Array)
                    Dim column As Integer = mMultiArrayAdapter.GetMemoryCountX
                    Dim row As Integer = mMultiArrayAdapter.GetMemoryCountY
                    For c = 0 To column - 1
                        For r = 0 To row - 1
                            trayArray1(x + c, y + r) += 1
                        Next
                    Next

                    DrawMapA.DrawRectangleArray(x, y, column, row)

                    If (Map1Column < column + x) Then
                        Map1Column = column + x
                    End If
                    If (Map1Row < row + y) Then
                        Map1Row = row + y
                    End If
                End If
            Next

            DrawMapA.SetDieSize(DieSize1 * Map1Column, DieSize1 * Map1Row)
            DrawMapA.DrawRectangleArray(0, 0, 1, 1)
            gbANode1.Text = "X : " & Map1Column.ToString() & ", Y : " & Map1Row.ToString()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    Private Sub MachineB_NodeListUpdate()
        Try
            Map2Column = 0
            Map2Row = 0

            DrawMapB.Clean()
            DrawMapB.SetDieSize(DieSize2, DieSize2)

            For x = 0 To trayArray2.GetUpperBound(0) - 1
                For y = 0 To trayArray2.GetUpperBound(1) - 1
                    trayArray2(x, y) = 0
                Next
            Next

            For i = 0 To chklbStage3WorkNode.Items.Count - 1
                Dim str As String = chklbStage3WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)

                If (chklbStage3WorkNode.GetItemChecked(i)) Then
                    Dim x As Integer = RecipeEdit.Node(enmStage.No3)(node).NodeStartingX - 1
                    Dim y As Integer = RecipeEdit.Node(enmStage.No3)(node).NodeStartingY - 1
                    Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No3)(node).Array)
                    Dim column As Integer = mMultiArrayAdapter.GetMemoryCountX
                    Dim row As Integer = mMultiArrayAdapter.GetMemoryCountY
                    For c = 0 To column - 1
                        For r = 0 To row - 1
                            trayArray2(x + c, y + r) += 1
                        Next
                    Next

                    DrawMapB.DrawRectangleArray(x, y, column, row)

                    If (Map2Column < column + x) Then
                        Map2Column = column + x
                    End If
                    If (Map2Row < row + y) Then
                        Map2Row = row + y
                    End If
                End If
            Next

            For i = 0 To chklbStage4WorkNode.Items.Count - 1
                Dim str As String = chklbStage4WorkNode.Items.Item(i).ToString()
                Dim node As String = str.Split("[")(0)

                If (chklbStage4WorkNode.GetItemChecked(i)) Then
                    Dim x As Integer = RecipeEdit.Node(enmStage.No4)(node).NodeStartingX - 1
                    Dim y As Integer = RecipeEdit.Node(enmStage.No4)(node).NodeStartingY - 1
                    Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(enmStage.No4)(node).Array)
                    Dim column As Integer = mMultiArrayAdapter.GetMemoryCountX
                    Dim row As Integer = mMultiArrayAdapter.GetMemoryCountY
                    For c = 0 To column - 1
                        For r = 0 To row - 1
                            trayArray2(x + c, y + r) += 1
                        Next
                    Next

                    DrawMapB.DrawRectangleArray(x, y, column, row)

                    If (Map2Column < column + x) Then
                        Map2Column = column + x
                    End If
                    If (Map2Row < row + y) Then
                        Map2Row = row + y
                    End If
                End If
            Next

            DrawMapB.SetDieSize(DieSize2 * Map2Column, DieSize2 * Map2Row)
            DrawMapB.DrawRectangleArray(0, 0, 1, 1)
            gbBNode1.Text = "X : " & Map2Column.ToString() & ", Y : " & Map2Row.ToString()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
End Class
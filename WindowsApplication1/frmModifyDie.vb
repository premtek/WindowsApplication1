Imports MapData
Imports ProjectRecipe
Imports ProjectCore


Public Class frmModifyDie

    Public Stage1, Stage2 As ProjectRecipe.CStageMap
    Public spiltX As Integer = -1
    Public Stage1Patten, Stage2Patten As ProjectRecipe.CPatternMap
    Public StageNo_L As Integer = -1
    Public StageNo_R As Integer = -1
    Dim Map As Dictionary(Of String, CNodeInfo)

    Public Sub btnDieModifyOK_Click(sender As Object, e As EventArgs) Handles btnDieModifyOK.Click
        gSyslog.Save("[frmModifyDie]" & vbTab & "[btnDieModifyOK]" & vbTab & "Click")
        Dim mybinmap As Bitmap = UcWaferMap1.PictureBox1.Image
        Dim X As Integer = UcWaferMap1.Pitch_X
        Dim Y As Integer = UcWaferMap1.Pitch_Y
        Dim co As Color
        'Dim NodetoMap As String
        'Dim fileName, strSection As String
        Dim LocatS_X, LocatS_Y, LocatE_X, LocatE_Y As Decimal
        Dim Stage As CStageMap
        Dim key As String

        Try

            For mI = 0 To Map.Count - 1
                key = Map.Keys(mI)
                Dim node() As String
                node = key.Split("|")
                LocatS_X = Map(key).StartXPos
                LocatS_Y = Map(key).StartYPos
                LocatE_X = Map(key).EndXPos
                LocatE_Y = Map(key).EndYPos

                If node(0) = 0 Then
                    Stage = gStageMap(0)
                ElseIf node(0) = 1 Then
                    Stage = gStageMap(1)
                ElseIf node(0) = 2 Then
                    Stage = gStageMap(2)
                ElseIf node(0) = 3 Then
                    Stage = gStageMap(3)
                Else
                    Exit Sub
                End If


                co = mybinmap.GetPixel((LocatS_X + LocatE_X) / 2, (LocatS_Y + LocatE_Y) / 2)

                If co.ToArgb = Color.LightGray.ToArgb Then  '判斷是否為灰色
                    Stage.Node(node(1)).SBinMapData(CInt(node(2).ToString), CInt(node(3).ToString)).Disable = True
                    Stage.Node(node(1)).SRecipePos(CInt(node(2).ToString), CInt(node(3).ToString)).IsByPassCCDScanFixAction = True
                    Stage.Node(node(1)).SRecipePos(CInt(node(2).ToString), CInt(node(3).ToString)).IsByPassDispensingAction = True
                    Stage.Node(node(1)).SRecipePos(CInt(node(2).ToString), CInt(node(3).ToString)).IsByPassLaserAction = True

                Else
                    Stage.Node(node(1)).SBinMapData(CInt(node(2).ToString), CInt(node(3).ToString)).Disable = False
                    Stage.Node(node(1)).SRecipePos(CInt(node(2).ToString), CInt(node(3).ToString)).IsByPassCCDScanFixAction = False
                    Stage.Node(node(1)).SRecipePos(CInt(node(2).ToString), CInt(node(3).ToString)).IsByPassDispensingAction = False
                    Stage.Node(node(1)).SRecipePos(CInt(node(2).ToString), CInt(node(3).ToString)).IsByPassLaserAction = False
                End If
                Stage.Node(node(1)).mmIsMapDataChange = True
            Next
            'Sue0829
            '待補AlarmCode
            MsgBox("設定完成", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        Catch ex As Exception





        End Try


        'If spiltX = 0 Then '只傳入一個stageMap

        '    For mI = 0 To Stage1.Node.Count - 1
        '        NodetoMap = Stage1.Node.Keys(mI)


        '        Stage1Patten = Stage1.Node(NodetoMap)

        '        For i As Integer = 0 To Stage1.Node(NodetoMap).ScanGlueArray.GetUpperBound(0)
        '            For j As Integer = 0 To Stage1.Node(NodetoMap).ScanGlueArray.GetUpperBound(1)

        '                strSection = StageNo_L.ToString & "|" & NodetoMap & "|" & i.ToString & "|" + j.ToString
        '                LocatS_X = CDec(ReadIniString(strSection, "StartX", fileName, -1))
        '                LocatS_Y = CDec(ReadIniString(strSection, "StartY", fileName, -1))
        '                Pitch_X = CDec(ReadIniString(strSection, "PitchX", fileName, -1))
        '                Pitch_Y = CDec(ReadIniString(strSection, "PitchY", fileName, -1))

        '                co = mybinmap.GetPixel((LocatS_X) + (Pitch_X / 2), (LocatS_Y) + (Pitch_Y / 2))
        '                If co.ToArgb = Color.LightGray.ToArgb Then  '判斷是否為灰色
        '                    Stage1Patten.SBinMapData(i, j).Disable = True
        '                    Stage1Patten.SRecipePos(i, j).IsByPassCCDScanFixAction = True
        '                    Stage1Patten.SRecipePos(i, j).IsByPassDispensingAction = True
        '                    Stage1Patten.SRecipePos(i, j).IsByPassLaserAction = True

        '                Else
        '                    Stage1Patten.SBinMapData(i, j).Disable = False
        '                    Stage1Patten.SRecipePos(i, j).IsByPassCCDScanFixAction = False
        '                    Stage1Patten.SRecipePos(i, j).IsByPassDispensingAction = False
        '                    Stage1Patten.SRecipePos(i, j).IsByPassLaserAction = False
        '                End If

        '            Next
        '        Next
        '    Next
        'Else ' 有2個Stage 傳入 增加判斷

        '    For mI = 0 To Stage1.Node.Count - 1
        '        NodetoMap = Stage1.Node.Keys(mI)
        '        Stage1Patten = Stage1.Node(NodetoMap)

        '        For i As Integer = 0 To Stage1.Node(NodetoMap).ScanGlueArray.GetUpperBound(0)
        '            For j As Integer = 0 To Stage1.Node(NodetoMap).ScanGlueArray.GetUpperBound(1)

        '                strSection = StageNo_L.ToString & "|" & NodetoMap & "|" & i.ToString & "|" + j.ToString
        '                LocatS_X = CDec(ReadIniString(strSection, "StartX", fileName, -1))
        '                LocatS_Y = CDec(ReadIniString(strSection, "StartY", fileName, -1))
        '                Pitch_X = CDec(ReadIniString(strSection, "PitchX", fileName, -1))
        '                Pitch_Y = CDec(ReadIniString(strSection, "PitchY", fileName, -1))

        '                co = mybinmap.GetPixel((LocatS_X) + (Pitch_X / 2), (LocatS_Y) + (Pitch_Y / 2))
        '                If co.ToArgb = Color.LightGray.ToArgb Then  '判斷是否為灰色
        '                    Stage1Patten.SBinMapData(i, j).Disable = True
        '                    Stage1Patten.SRecipePos(i, j).IsByPassCCDScanFixAction = True
        '                    Stage1Patten.SRecipePos(i, j).IsByPassDispensingAction = True
        '                    Stage1Patten.SRecipePos(i, j).IsByPassLaserAction = True

        '                Else
        '                    Stage1Patten.SBinMapData(i, j).Disable = False
        '                    Stage1Patten.SRecipePos(i, j).IsByPassCCDScanFixAction = False
        '                    Stage1Patten.SRecipePos(i, j).IsByPassDispensingAction = False
        '                    Stage1Patten.SRecipePos(i, j).IsByPassLaserAction = False
        '                End If

        '            Next
        '        Next
        '    Next

        '    For mI = 0 To Stage2.Node.Count - 1
        '        NodetoMap = Stage2.Node.Keys(mI)
        '        Stage2Patten = Stage2.Node(NodetoMap)

        '        For i As Integer = 0 To Stage2.Node(NodetoMap).ScanGlueArray.GetUpperBound(0)
        '            For j As Integer = 0 To Stage2.Node(NodetoMap).ScanGlueArray.GetUpperBound(1)

        '                strSection = StageNo_R.ToString & "|" & NodetoMap & "|" & i.ToString & "|" + j.ToString
        '                LocatS_X = CDec(ReadIniString(strSection, "StartX", fileName, -1))
        '                LocatS_Y = CDec(ReadIniString(strSection, "StartY", fileName, -1))
        '                Pitch_X = CDec(ReadIniString(strSection, "PitchX", fileName, -1))
        '                Pitch_Y = CDec(ReadIniString(strSection, "PitchY", fileName, -1))

        '                co = mybinmap.GetPixel((LocatS_X) + (Pitch_X / 2), (LocatS_Y) + (Pitch_Y / 2))
        '                If co.ToArgb = Color.LightGray.ToArgb Then  '判斷是否為灰色
        '                    Stage2Patten.SBinMapData(i, j).Disable = True
        '                    Stage2Patten.SRecipePos(i, j).IsByPassCCDScanFixAction = True
        '                    Stage2Patten.SRecipePos(i, j).IsByPassDispensingAction = True
        '                    Stage2Patten.SRecipePos(i, j).IsByPassLaserAction = True

        '                Else
        '                    Stage2Patten.SBinMapData(i, j).Disable = False
        '                    Stage2Patten.SRecipePos(i, j).IsByPassCCDScanFixAction = False
        '                    Stage2Patten.SRecipePos(i, j).IsByPassDispensingAction = False
        '                    Stage2Patten.SRecipePos(i, j).IsByPassLaserAction = False
        '                End If

        '            Next
        '        Next
        '    Next

        'End If
        'Me.Dispose()
    End Sub
    Public Sub New(ByVal StageL As ProjectRecipe.CStageMap, ByVal stageNo As Integer)
        Stage1 = StageL
        StageNo_L = stageNo
        spiltX = 0
        InitializeComponent()
    End Sub
    Public Sub New(ByVal StageL As ProjectRecipe.CStageMap, ByVal StageN_L As Integer, ByVal StageR As ProjectRecipe.CStageMap, ByVal StageN_R As Integer)
        Stage1 = StageL
        Stage2 = StageR
        StageNo_L = StageN_L
        StageNo_R = StageN_R
        InitializeComponent()
    End Sub
    Public Sub New(showPos As Dictionary(Of String, CNodeInfo))
        Map = showPos
        InitializeComponent()
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Sue20170627
        gSyslog.Save("[frmModifyDie]" & vbTab & "[Button1]" & vbTab & "Click")
        Me.Dispose()
    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        btnDieModifyOK.Enabled = True

        UcWaferMap1.IniMap(Map)


        'UcWaferMap1.DrawStageMap(Map)
        'If spiltX = -1 Then
        '    UcWaferMap1.DrawStageMap(Map)
        '    UcWaferMap1.DrawStageBin(Stage1, StageNo_L, Stage2, StageNo_R)
        '    'spiltX = Stage1.Node(UcWaferMap1.NodetoMapL).ScanGlueArray.GetUpperBound(0)
        '    'Stage1Patten = Stage1.Node(UcWaferMap1.NodetoMapL)
        '    'Stage2Patten = Stage2.Node(UcWaferMap1.NodetomapR)
        'Else
        '    UcWaferMap1.DrawStageMap(Map)
        '    UcWaferMap1.DrawStageBin(Stage1, StageNo_L)
        '    'Stage1Patten = Stage1.Node(UcWaferMap1.NodetoMapL)
        'End If

    End Sub

    Public Sub frmModifyDie_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Button2_Click(sender, e)
            cmbEditType.SelectedIndex = 0
            UcWaferMap1.mEditType = eMapEditType.SingleDie
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1025004))
            MsgBox(gMsgHandler.GetMessage(Error_1025004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Can't load Map, Please click Load Map Manual")
            Button2.Enabled = True
        End Try
        'Button2_Click(sender, e)
    End Sub

    Private Sub cmbEditType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEditType.SelectedIndexChanged
        UcWaferMap1.mEditType = cmbEditType.SelectedIndex
    End Sub
End Class
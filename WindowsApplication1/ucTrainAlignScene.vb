Imports ProjectAOI
Imports ProjectCore
Imports ProjectIO
Imports ProjectRecipe

Public Class ucTrainAlignScene
    Public CCDNo As Integer

    Public SceneName As String = ""

    Public IsRecipeScene As Boolean

    Public sys As sSysParam
    ''' <summary>外部傳入NodeID</summary>
    ''' <remarks></remarks>
    Public NodeID As String
    ''' <summary>外部傳入PatternID</summary>
    ''' <remarks></remarks>
    Public PatternID As String

    Private Sub btnTrainScene_Click(sender As Object, e As EventArgs) Handles btnTrainScene.Click

        If IsRecipeScene Then
            '[Note]定位頁面

            ''[Note]模組化測試中----------
            'Dim mfrmAlignPR01 As frmAlignModule
            'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            'If btnTrainScene.Enabled = False Then
            '    Exit Sub
            'End If

            'btnTrainScene.Enabled = False
            'If Not gCRecipe.Editable Then
            '    BtnReadOnlyBehavior(sender)
            '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            '    btnTrainScene.Enabled = True
            '    Exit Sub
            'End If
            'If NodeID = "" Then
            '    '請先選擇Node
            '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            '    MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '    btnTrainScene.Enabled = True
            '    Exit Sub
            'End If

            'If gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '    '定位資料不存在
            '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '    btnTrainScene.Enabled = True
            '    Exit Sub
            'End If


            'Dim mScene As String = ""
            'If gAOICollection.SetCCDScene(sys.CCDNo, gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene) = False Then
            '    MsgBox("Scene (" & gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene & ") Not Exists!")
            'Else
            '    mScene = gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
            'End If

            'Try
            '    mfrmAlignPR01 = New frmAlignModule
            '    With mfrmAlignPR01
            '        .Sys = sys
            '        .SceneName = mScene 'gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
            '        .StartPosition = FormStartPosition.Manual
            '        .Location = New Point(0, 0)
            '        .ShowDialog()
            '        '[Note]:回復原始設定
            '        cmbSceneID.Items.Clear()
            '        cmbSceneID.Items.AddRange(gAOICollection.GetSceneList)
            '        'cmbSceneID.SelectedItem = .lstScene.SelectedItem
            '        If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
            '            cmbSceneID.SelectedItem = .RecipeSceneName
            '        End If
            '    End With
            'Catch ex As Exception
            '    MsgBox("Exception Message: " & ex.Message)
            'End Try
            'btnTrainScene.Enabled = True
            ''[Note]模組化測試中----------







        Else
            '[Note]校正頁面









        End If



    End Sub
End Class

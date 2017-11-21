Imports ProjectRecipe
Imports ProjectCore 'Eason 20170302 Ticket:100090 , System Update Crash
Imports ProjectIO
Imports System.IO

Public Class CMapInfo

    Public gMotorPos_L As New Dictionary(Of String, CNodeInfo)
    Public gDrewMapPos_L As New Dictionary(Of String, CNodeInfo)
    Public gMotorPos_R As New Dictionary(Of String, CNodeInfo)
    Public gDrewMapPos_R As New Dictionary(Of String, CNodeInfo)


    Dim Pitch_X, Pitch_Y As Integer  ' die size
    Dim Row, Col As Integer      ' Array size
    Dim NodetomapR, NodetoMapL As String
    Dim RoundMapR, RoundMapL As Integer
    Dim RoundMapR_Status, RoundMapL_Status As CRoundMap
    Dim mScaleX As Integer '換算比率
    Dim mScaleY As Integer '
    Dim ShowBIN As Integer
    Dim stageNO_L, stageNO_R As Integer
    Dim StageL, StageR As CStageMap
    Dim BasicX() As Integer
    Dim BasicY() As Integer

    Dim mMultiArrayAdapter As CMultiArrayAdapter

    Public Sub New()


        Select Case gSSystemParameter.MachineType

            Case enmMachineType.DCSW_800AQ
                '產生map 資訊_馬達座標(實際尺寸)
                'A機

                If gCRecipe.Node(0).Count > 0 Or gCRecipe.Node(1).Count > 0 Then
                    If gCRecipe.Node(1).Count = 0 Then '判斷stage 1 有沒有node
                        CreateMapDtat(gStageMap(0), 0)
                    ElseIf gCRecipe.Node(0).Count = 0 Then '判斷stage 0 有沒有node
                        CreateMapDtat(gStageMap(1), 1)
                    Else ' 2個stage都有node
                        CreateMapDtat(gStageMap(0), 0, gStageMap(1), 1)
                    End If
                End If

                'B機
                If gCRecipe.Node(2).Count > 0 Or gCRecipe.Node(3).Count > 0 Then

                    If gCRecipe.Node(3).Count = 0 Then '判斷stage 3 有沒有node
                        CreateMapDtat(gStageMap(2), 2)
                    ElseIf gCRecipe.Node(2).Count = 0 Then '判斷stage 2 有沒有node
                        CreateMapDtat(gStageMap(3), 3)
                    Else ' 2個stage都有node
                        CreateMapDtat(gStageMap(2), 2, gStageMap(3), 3)
                    End If
                End If
            Case enmMachineType.DCS_500AD
                '產生map 資訊_馬達座標(實際尺寸)
                'A機

                If gCRecipe.Node(0).Count > 0 Or gCRecipe.Node(1).Count > 0 Then
                    If gCRecipe.Node(1).Count = 0 Then '判斷stage 1 有沒有node
                        CreateMapDtat(gStageMap(0), 0)
                    ElseIf gCRecipe.Node(0).Count = 0 Then '判斷stage 0 有沒有node
                        CreateMapDtat(gStageMap(1), 1)
                    Else ' 2個stage都有node
                        CreateMapDtat(gStageMap(0), 0, gStageMap(1), 1)
                    End If
                End If

            Case Else
                '產生map 資訊_馬達座標(實際尺寸)

                If gCRecipe.Node(0).Count > 0 Then '判斷stage 0 有沒有node
                    CreateMapDtat(gStageMap(0), 0)
                End If


        End Select

        '產生map 資訊_顯示座標(經過縮放)
        'A機
        If gMotorPos_L.Count > 0 Then
            Resize(gMotorPos_L, gDrewMapPos_L)
        End If
        'B機
        If gMotorPos_R.Count > 0 Then
            Resize(gMotorPos_R, gDrewMapPos_R)
        End If


    End Sub



    Public Function CreateMapDtat(StageL As CStageMap, stageNo_L As Integer, StageR As CStageMap, stageNo_R As Integer) As Boolean  '根據Stage Map 顯示 2個Stage
        Dim mI As Integer
        Dim Max_L_X As Integer = 0
        Dim Max_L_Y As Integer = 0
        Dim Max_R_X As Integer = 0
        Dim Max_R_Y As Integer = 0
        Dim Temp_BasicX, Temp_BasicY As Integer
        Dim Min_BasicX_R As Integer = Int32.MaxValue
        Dim Max_BasicX_R As Integer = Int32.MinValue
        Dim Min_BasicX_L As Integer = Int32.MaxValue
        Dim Max_BasicX_L As Integer = Int32.MinValue
        '最小Y軸只取一個(左&右 Stage)
        Dim Min_BasicY As Integer = Int32.MaxValue
        Dim Display_startX, Display_startY As Integer
        Dim ScaleX As Decimal
        Dim Stage_center As Integer

        Try

            ReDim BasicX(gCRecipe.Node(stageNo_L).Count + gCRecipe.Node(stageNo_R).Count - 1)
            ReDim BasicY(gCRecipe.Node(stageNo_L).Count + gCRecipe.Node(stageNo_R).Count - 1)


            For mI = 0 To gCRecipe.Node(stageNo_L).Count - 1
                NodetoMapL = StageL.Node.Keys(mI)
                BasicX(mI) = gCRecipe.Node(stageNo_L)(NodetoMapL).ConveyorPos(0).BasicPositionX
                BasicY(mI) = gCRecipe.Node(stageNo_L)(NodetoMapL).ConveyorPos(0).BasicPositionY
                Temp_BasicX = BasicX(mI)
                Temp_BasicY = BasicY(mI)

                If Temp_BasicX < Min_BasicX_L Then
                    Min_BasicX_L = Temp_BasicX
                End If
                If Temp_BasicX > Max_BasicX_L Then
                    Max_BasicX_L = Temp_BasicX
                End If
                If Temp_BasicY < Min_BasicY Then
                    Min_BasicY = Temp_BasicY
                End If

            Next
            For mI = 0 To gCRecipe.Node(stageNo_R).Count - 1
                NodetomapR = StageR.Node.Keys(mI)
                BasicX(gCRecipe.Node(stageNo_L).Count + mI) = gCRecipe.Node(stageNo_R)(NodetomapR).ConveyorPos(0).BasicPositionX
                BasicY(gCRecipe.Node(stageNo_L).Count + mI) = gCRecipe.Node(stageNo_R)(NodetomapR).ConveyorPos(0).BasicPositionY
                Temp_BasicX = BasicX(gCRecipe.Node(stageNo_L).Count + mI)
                Temp_BasicY = BasicY(gCRecipe.Node(stageNo_L).Count + mI)

                If Temp_BasicX < Min_BasicX_R Then
                    Min_BasicX_R = Temp_BasicX
                End If
                If Temp_BasicX > Max_BasicX_R Then
                    Max_BasicX_R = Temp_BasicX
                End If
                If Temp_BasicY < Min_BasicY Then
                    Min_BasicY = Temp_BasicY
                End If

            Next

            ScaleX = 800 / (((Max_BasicX_L + 200) - Min_BasicX_L) + ((Max_BasicX_R + 200) - Min_BasicX_R))
            Stage_center = ScaleX * ((Max_BasicX_L + 200) - Min_BasicX_L)


            For mI = 0 To gCRecipe.Node(stageNo_L).Count - 1
                NodetoMapL = StageL.Node.Keys(mI)
                If IsNothing(NodetoMapL) Then
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapL = StageL.Node(NodetoMapL).Round.Count
                End If


                Display_startX = Stage_center - (BasicX(mI) - Min_BasicX_L)
                Display_startY = (BasicY(mI) - Min_BasicY)

                mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo_L)(NodetoMapL).Array)
                'Toby add
                '取最高的level算size
                mMultiArrayAdapter.GetNodeSize(mMultiArrayAdapter)

                mMultiArrayAdapter.Draw(Me, stageNo_L, NodetoMapL, Display_startX - mMultiArrayAdapter.Size_X, Display_startX, Display_startY - mMultiArrayAdapter.Size_Y, Display_startY) '暫定Node size 200*200

            Next

            For mI = 0 To gCRecipe.Node(stageNo_R).Count - 1
                NodetomapR = StageR.Node.Keys(mI)
                If IsNothing(NodetomapR) Then
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapR = StageR.Node(NodetomapR).Round.Count
                End If
                Display_startX = Stage_center + (BasicX(mI + gCRecipe.Node(stageNo_L).Count) - Min_BasicX_R)
                Display_startY = (BasicY(mI + gCRecipe.Node(stageNo_L).Count) - Min_BasicY)

                mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo_R)(NodetomapR).Array)

                'Toby add
                '取最高的level算size
                mMultiArrayAdapter.GetNodeSize(mMultiArrayAdapter)

                mMultiArrayAdapter.Draw(Me, stageNo_R, NodetomapR, Display_startX, Display_startX + mMultiArrayAdapter.Size_X, Display_startY - mMultiArrayAdapter.Size_Y, Display_startY) '暫定Node size 200*200

            Next

        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        Return True
    End Function
    Public Function CreateMapDtat(Stage As CStageMap, stageNo As Integer) As Boolean  '根據Stage Map 顯示 1個Stage
        Dim mI As Integer
        Dim Temp_BasicX, Temp_BasicY As Integer
        Dim Min_BasicX As Integer = Int32.MaxValue
        Dim Min_BasicY As Integer = Int32.MaxValue

        Dim Display_startX, Display_startY As Integer

        Try

            ReDim BasicX(gCRecipe.Node(stageNo).Count - 1)
            ReDim BasicY(gCRecipe.Node(stageNo).Count - 1)


            '[Note]:取出NodeName，但目前基本上只允許一層
            For mI = 0 To gCRecipe.Node(stageNo).Count - 1
                NodetoMapL = Stage.Node.Keys(mI)

                BasicX(mI) = gCRecipe.Node(stageNo)(NodetoMapL).ConveyorPos(0).BasicPositionX
                BasicY(mI) = gCRecipe.Node(stageNo)(NodetoMapL).ConveyorPos(0).BasicPositionY

                If gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchX < 0 Then
                    Temp_BasicX = BasicX(mI) + (gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.CountX * gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchX)
                Else
                    Temp_BasicX = BasicX(mI)
                End If
                If gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchY < 0 Then
                    Temp_BasicY = BasicY(mI) + (gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.CountY * gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchY)
                Else
                    Temp_BasicY = BasicY(mI)
                End If
                'Temp_BasicX = BasicX(mI)
                'Temp_BasicY = BasicY(mI)

                If Temp_BasicX < Min_BasicX Then
                    Min_BasicX = Temp_BasicX
                End If
                If Temp_BasicY < Min_BasicY Then
                    Min_BasicY = Temp_BasicY
                End If
            Next

            For mI = 0 To gCRecipe.Node(stageNo).Count - 1
                NodetoMapL = Stage.Node.Keys(mI)
                If IsNothing(NodetoMapL) Then
                    'Recipe錯誤,請重新建立檔案
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("請確認配方是否建置正確")
                    Return False
                    Exit Function
                Else
                    RoundMapL = Stage.Node(NodetoMapL).Round.Count
                End If


                If gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchX < 0 Then
                    Display_startX = (BasicX(mI) + (gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.CountX * gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchX)) - Min_BasicX
                Else
                    Display_startX = BasicX(mI) - Min_BasicX
                End If
                If gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchY < 0 Then
                    Display_startY = 700 - (BasicY(mI) + (gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.CountY * gCRecipe.Node(stageNo)(NodetoMapL).Array(0).Array.PitchY) - Min_BasicY)
                Else
                    Display_startY = 700 - (BasicY(mI) - Min_BasicY)
                End If



                'Display_startX = (BasicX(mI) - Min_BasicX)
                'Display_startY = 700 - (BasicY(mI) - Min_BasicY)
                mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo)(NodetoMapL).Array)
                'Toby add
                '取最高的level算size
                mMultiArrayAdapter.GetNodeSize(mMultiArrayAdapter)

                mMultiArrayAdapter.Draw(Me, stageNo, NodetoMapL, Display_startX, Display_startX + mMultiArrayAdapter.Size_X, Display_startY - mMultiArrayAdapter.Size_Y, Display_startY) '暫定Node size 200*200

            Next
        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        Return True
    End Function
    Public Function Resize(MotorPos As Dictionary(Of String, CNodeInfo), showPos As Dictionary(Of String, CNodeInfo)) As Boolean  '根據Stage Map 顯示 1個Stage

        Dim Key As String
        Dim max_XPos As Decimal = Decimal.MinValue
        Dim min_XPos As Decimal = Decimal.MaxValue
        Dim min_YPos As Decimal = Decimal.MaxValue
        Dim max_YPos As Decimal = Decimal.MinValue
        Dim Temp_XPos_S As Decimal
        Dim Temp_YPos_S As Decimal
        Dim Temp_XPos_E As Decimal
        Dim Temp_YPos_E As Decimal
        Dim Scale_X As Decimal
        Dim Scale_Y As Decimal

        Dim scale As Decimal

        Try
            For i = 0 To MotorPos.Count - 1

                Key = MotorPos.Keys(i)
                Temp_XPos_S = MotorPos(Key).StartXPos
                Temp_YPos_S = MotorPos(Key).StartYPos
                Temp_XPos_E = MotorPos(Key).EndXPos
                Temp_YPos_E = MotorPos(Key).EndYPos
                '抓最小X&Y
                If Temp_XPos_S < min_XPos Then
                    min_XPos = Temp_XPos_S
                End If
                If Temp_YPos_S < min_YPos Then
                    min_YPos = Temp_YPos_S
                End If
                If Temp_XPos_E < min_XPos Then
                    min_XPos = Temp_XPos_E
                End If
                If Temp_YPos_E < min_YPos Then
                    min_YPos = Temp_YPos_E
                End If
              
                '抓最大X& Y
                If Temp_XPos_S > max_XPos Then
                    max_XPos = Temp_XPos_S
                End If
                If Temp_YPos_S > max_YPos Then
                    max_YPos = Temp_YPos_S
                End If
                If Temp_XPos_E > max_XPos Then
                    max_XPos = Temp_XPos_E
                End If
                If Temp_YPos_E > max_YPos Then
                    max_YPos = Temp_YPos_E
                End If
            Next

            Scale_X = 800 / (max_XPos - min_XPos)
            Scale_Y = 700 / (max_YPos - min_YPos)
            If Scale_X > Scale_Y Then
                scale = Scale_Y
            Else
                scale = Scale_X
            End If

            For j = 0 To MotorPos.Count - 1
                Key = MotorPos.Keys(j)
                Dim startPosX As Decimal
                Dim endPosX As Decimal
                Dim startPosY As Decimal
                Dim endPosY As Decimal

                startPosX = Math.Abs((MotorPos(Key).StartXPos - min_XPos) * scale)
                startPosY = Math.Abs(((MotorPos(Key).StartYPos - min_YPos) * scale))
                endPosX = Math.Abs((MotorPos(Key).EndXPos - min_XPos) * scale)
                endPosY = Math.Abs(((MotorPos(Key).EndYPos - min_YPos) * scale))

                Dim PosInfo As CNodeInfo = New CNodeInfo(startPosX, startPosY, endPosX, endPosY)
                showPos.Add(Key, PosInfo)
            Next


        Catch ex As Exception
            'Eason 20170302 Ticket:100090 , System Update Crash
            gMeEventLog.Log("Draw Exception:" & ex.ToString())
        End Try
        Return True
    End Function

    
End Class

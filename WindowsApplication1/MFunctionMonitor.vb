
Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectTriggerBoard
Imports ProjectValveController
Imports System.Math

Module MFunctionMonitor
#Region "區域變數"
    ''' <summary>[隔多久更新一次]</summary>
    ''' <remarks></remarks>
    Const mUpdateCycle As Integer = 10000

#End Region

#Region "Trigger Board相關I/O"

    ''' <summary>[確認Trigger Board Ready]</summary>
    ''' <param name="stageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTriggerBoardReady(ByVal stageNo As enmStage) As Boolean
        '[Note]:基本上一組Stage配一塊Trigger Board
        Select Case stageNo
            Case enmStage.No1
                Return gDICollection.GetState(enmDI.TriggerBoardReady1, True)

            Case enmStage.No2
                Return gDICollection.GetState(enmDI.TriggerBoardReady2, True)

            Case enmStage.No3
                Return gDICollection.GetState(enmDI.TriggerBoardReady3, True)

            Case enmStage.No4
                Return gDICollection.GetState(enmDI.TriggerBoardReady4, True)

        End Select

        Return True
    End Function

    ''' <summary>[點膠觸發訊號狀態]</summary>
    ''' <param name="stageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DispensingTriggerStatus(ByVal stageNo As enmStage) As Boolean

        Select Case stageNo
            Case enmStage.No1
                Return gDOCollection.GetState(enmDO.DispensingTrigger1)

            Case enmStage.No2
                Return gDOCollection.GetState(enmDO.DispensingTrigger2)

            Case enmStage.No3
                Return gDOCollection.GetState(enmDO.DispensingTrigger3)

            Case enmStage.No4
                Return gDOCollection.GetState(enmDO.DispensingTrigger4)

        End Select

        Return False


    End Function

#End Region


    ''' <summary>[監控動作流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub MonitorDisp_MonitorAction(ByRef sys As sSysParam)

        Static mStopWatch(enmStage.Max) As Stopwatch
        Static mTriggerTemperature(enmStage.Max) As String
        Static mValveTemperature(enmStage.Max) As sPicoValveHeaterStatus

        Dim mI(enmStage.Max) As Integer
        Dim mSplit(enmStage.Max)() As String

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                If IsNothing(mStopWatch(sys.StageNo)) = True Then
                    mStopWatch(sys.StageNo) = New Stopwatch
                End If
                sys.SysNum = 2000

                '**********************************************************************************************************
                '*********************************************Trigger Board************************************************
                '**********************************************************************************************************
            Case 2000
                '[Note]:Send R Cmd  
                '            Check trigger board is ready before send command
                '20171010 R命令須要修改 暫時移除
                'If DispensingTriggerStatus(sys.StageNo) = False Then
                '    If IsTriggerBoardReady(sys.StageNo) = True Then
                '        If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                '            If gTriggerBoard.GetTemperature(sys.StageNo, False) = True Then
                sys.SysNum = 2100
                '            End If
                '        End If
                '    End If
                'End If

            Case 2100
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                    '[Note]:還在接收傳送資料中
                    If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                        '[Note]:超過時間還沒處裡完-->放棄更新溫度資訊(溫度監控若此次詢問失敗就算了，反正會定時更新)
                        Debug.Print("Temperature(R Cmd): MonitorTriggerBoardTimeOut")
                        sys.SysNum = 3000
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.Temperature(sys.StageNo).Status = True Then
                        mTriggerTemperature(sys.StageNo) = gTriggerBoard.Temperature(sys.StageNo).Value
                        sys.SysNum = 2200
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("Temperature(R Cmd): " & gTriggerBoard.Temperature(sys.StageNo).STR)
                        sys.SysNum = 3000
                    End If
                End If

            Case 2200
                '[Note]:Update Temperature
                mSplit(sys.StageNo) = mTriggerTemperature(sys.StageNo).Split(",")
                If mSplit(sys.StageNo).Length = 4 Then
                    If IsNumeric(mSplit(sys.StageNo)(0)) Then
                        gCRecipe.StageParts(sys.StageNo).NozzleTemperature(eValveWorkMode.Valve1) = mSplit(sys.StageNo)(0)
                    End If
                    If IsNumeric(mSplit(sys.StageNo)(1)) Then
                        gCRecipe.StageParts(sys.StageNo).NozzleTemperature(eValveWorkMode.Valve2) = mSplit(sys.StageNo)(1)
                    End If
                    If IsNumeric(mSplit(sys.StageNo)(2)) Then
                        gCRecipe.StageParts(sys.StageNo).PiezoTemperature(eValveWorkMode.Valve1) = mSplit(sys.StageNo)(2)
                    End If
                    If IsNumeric(mSplit(sys.StageNo)(3)) Then
                        gCRecipe.StageParts(sys.StageNo).PiezoTemperature(eValveWorkMode.Valve2) = mSplit(sys.StageNo)(3)
                    End If
                End If
                sys.SysNum = 3000


                '**********************************************************************************************************
                '*********************************************Valve Controll***********************************************
                '**********************************************************************************************************

            Case 3000
                '[Note]:Valve Temperature
                '       Check trigger board is ready before send command
                If gValvecontrollerCollection.ConnectionParameter(sys.ValveControllerNo1).DeviceType = enmValvecontrollerType.Virtual Then
                    sys.SysNum = 3300
                Else
                    If gValvecontrollerCollection.IsBusy(sys.ValveControllerNo1) = False Then
                        If gValvecontrollerCollection.GetHeaterStatus(sys.ValveControllerNo1, , False) = enmCommandState.Success Then
                            sys.SysNum = 3100
                        End If
                    End If
                End If

            Case 3100
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gValvecontrollerCollection.IsBusy(sys.ValveControllerNo1) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gValvecontrollerCollection.IsTimeOut(sys.ValveControllerNo1) = True Then
                        '[Note]:超過時間還沒處裡完-->放棄更新溫度資訊(溫度監控若此次詢問失敗就算了，反正會定時更新)
                        Debug.Print("Valve Temperature Command TimeOut")
                        sys.SysNum = 3300
                    End If
                Else
                    '[Note]:檢查接收資料
                    mValveTemperature(sys.ValveControllerNo1) = gValvecontrollerCollection.GetCommandResponseHeaterStatus(sys.ValveControllerNo1)
                    If mValveTemperature(sys.ValveControllerNo1).Status = True Then
                        sys.SysNum = 3200
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("Valve Controll TimeOut: " & mValveTemperature(sys.ValveControllerNo1).sACT)
                        sys.SysNum = 3300
                    End If
                End If

            Case 3200
                If IsNumeric(mValveTemperature(sys.ValveControllerNo1).sACT) = True Then
                    gCRecipe.StageParts(sys.StageNo).NozzleTemperature(eValveWorkMode.Valve1) = CDec(mValveTemperature(sys.ValveControllerNo1).sACT)
                End If
                sys.SysNum = 3300

            Case 3300
                '[Note]:Valve Temperature
                '       Check trigger board is ready before send command
                If gValvecontrollerCollection.ConnectionParameter(sys.ValveControllerNo2).DeviceType = enmValvecontrollerType.Virtual Then
                    sys.SysNum = 4000
                Else
                    If gValvecontrollerCollection.IsBusy(sys.ValveControllerNo2) = False Then
                        If gValvecontrollerCollection.GetHeaterStatus(sys.ValveControllerNo2, , False) = enmCommandState.Success Then
                            sys.SysNum = 3400
                        End If
                    End If
                End If


            Case 3400
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gValvecontrollerCollection.IsBusy(sys.ValveControllerNo2) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gValvecontrollerCollection.IsTimeOut(sys.ValveControllerNo2) = True Then
                        '[Note]:超過時間還沒處裡完-->放棄更新溫度資訊(溫度監控若此次詢問失敗就算了，反正會定時更新)
                        Debug.Print("Valve Temperature Command TimeOut")
                        sys.SysNum = 4000
                    End If
                Else
                    '[Note]:檢查接收資料
                    mValveTemperature(sys.ValveControllerNo2) = gValvecontrollerCollection.GetCommandResponseHeaterStatus(sys.ValveControllerNo2)
                    If mValveTemperature(sys.ValveControllerNo2).Status = True Then
                        sys.SysNum = 3500
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("Valve Controll TimeOut: " & mValveTemperature(sys.ValveControllerNo2).sACT)
                        sys.SysNum = 4000
                    End If
                End If

            Case 3500
                If IsNumeric(mValveTemperature(sys.ValveControllerNo2).sACT) = True Then
                    gCRecipe.StageParts(sys.StageNo).NozzleTemperature(eValveWorkMode.Valve2) = CDec(mValveTemperature(sys.ValveControllerNo2).sACT)
                End If
                sys.SysNum = 4000

            Case 4000
                mStopWatch(sys.StageNo).Restart()
                sys.SysNum = 4100

            Case 4100
                '[Note]:每五秒更新一次
                If mStopWatch(sys.StageNo).ElapsedMilliseconds > mUpdateCycle Then
                    mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 2000
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub


End Module

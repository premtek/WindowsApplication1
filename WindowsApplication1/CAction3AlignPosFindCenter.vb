Imports ProjectCore
Imports ProjectMotion
Imports ProjectAOI
Imports ProjectIO
''' <summary>
'''AOI辨識三個特徵點計算中心位置
''' </summary>
''' <remarks></remarks>
Public Class CAction3AlignPosFindCenter
    Dim ClassName As String = "CAction3AlignPosFindCenter"

    Public Function CheckGoPos(ByVal sys As sSysParam, ByVal PosX As Decimal, ByVal PosY As Decimal, ByVal PosZ As Decimal) As Boolean
        If PosX < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or PosX > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           PosY < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or PosY > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           PosZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or PosZ > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function Run(ByVal sys As sSysParam, ByVal sceneID As String, ByVal AlignPos1 As CPoint3D, ByVal AlignPos2 As CPoint3D, ByVal AlignPos3 As CPoint3D, ByRef centerPosX As Decimal, ByRef centerPosY As Decimal) As Boolean
        '位置保護
        If CheckGoPos(sys, AlignPos1.PointX, AlignPos1.PointY, AlignPos1.PointZ) = False Then
            gSyslog.Save(ClassName & " AlignPos1 Pos is out of Range ")
            Return False
        End If
        If CheckGoPos(sys, AlignPos2.PointX, AlignPos2.PointY, AlignPos2.PointZ) = False Then
            gSyslog.Save(ClassName & " AlignPos2 Pos is out of Range ")
            Return False
        End If
        If CheckGoPos(sys, AlignPos3.PointX, AlignPos3.PointY, AlignPos3.PointZ) = False Then
            gSyslog.Save(ClassName & " AlignPos3 Pos is out of Range ")
            Return False
        End If


        Dim AlignCount As Integer = 3
        Dim mCCDAlignPosX(2) As Decimal
        Dim mCCDAlignPosY(2) As Decimal
        Dim mCCDAlignPosZ(2) As Decimal
        mCCDAlignPosX(0) = AlignPos1.PointX
        mCCDAlignPosX(1) = AlignPos2.PointX
        mCCDAlignPosX(2) = AlignPos3.PointX
        mCCDAlignPosY(0) = AlignPos1.PointY
        mCCDAlignPosY(1) = AlignPos2.PointY
        mCCDAlignPosY(2) = AlignPos3.PointY
        mCCDAlignPosZ(0) = AlignPos1.PointZ
        mCCDAlignPosZ(1) = AlignPos2.PointZ
        mCCDAlignPosZ(2) = AlignPos3.PointZ

        Dim mRealPosX(2) As Decimal
        Dim mRealPosY(2) As Decimal

        Dim ticket As Integer = 0
        For i As Integer = 0 To AlignCount - 1
            Dim mAxisNo(4) As Integer
            Dim mTargetPos(4) As Decimal
            mAxisNo(0) = sys.AxisX
            mAxisNo(1) = sys.AxisY
            mAxisNo(2) = sys.AxisZ
            mAxisNo(3) = sys.AxisB
            mAxisNo(4) = sys.AxisC
            mTargetPos(0) = mCCDAlignPosX(i)
            mTargetPos(1) = mCCDAlignPosY(i)
            mTargetPos(2) = mCCDAlignPosZ(i)
            mTargetPos(3) = 0
            mTargetPos(4) = 0

            ButtonSafeMovePos(Nothing, mAxisNo, mTargetPos, sys) '移動到拍照位置
            gAOICollection.SetCCDScene(sys.CCDNo, sceneID) '選擇場景
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
            System.Threading.Thread.CurrentThread.Join(10)
            ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
            System.Threading.Thread.CurrentThread.Join(10)
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

            Dim timeOutStopWatch As New Stopwatch '逾時計時器
            timeOutStopWatch.Restart()
            Do
                System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 
                If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                    'gSyslog.Save(ClassName & " Acquisition is Timeout!")
                    'Sue AlarmCode 20170602
                    'CCD 取像TimeOut
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(ClassName & gMsgHandler.GetMessage(Alarm_2012003))
                        Case 1
                            gSyslog.Save(ClassName & gMsgHandler.GetMessage(Alarm_2012303))
                        Case 2
                            gSyslog.Save(ClassName & gMsgHandler.GetMessage(Alarm_2012603))
                        Case 3
                            gSyslog.Save(ClassName & gMsgHandler.GetMessage(Alarm_2012903))
                    End Select
                    Return False
                End If

                If gDICollection.GetState(enmDI.EMO) = True Then
                    Return False
                Else
                    If sys.MachineNo = enmMachineStation.MachineA Then
                        If gDICollection.GetState(enmDI.EMS) = True Then
                            Return False
                        End If
                    ElseIf sys.MachineNo = enmMachineStation.MachineB Then
                        If gDICollection.GetState(enmDI.EMS2) = True Then
                            Return False
                        End If
                    End If
                End If
            Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
            Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)

            'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneID) '更新控制項,必要條件 frmMain必須是實體

            timeOutStopWatch.Restart()
            Do
                If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                    Exit Do
                End If
                System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 
                If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                    gSyslog.Save(ClassName & " Calculation is Timeout!")
                    'Sue AlarmCode 20170602
                    MsgBox("Calculation is Timeout!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
                If gDICollection.GetState(enmDI.EMO) = True Or gDICollection.GetState(enmDI.EMS) = True Or gDICollection.GetState(enmDI.EMS2) = True Then
                    Return False
                End If
            Loop

            If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                gSyslog.Save(ClassName & " Pattern Not Found.")
                'Sue AlarmCode 20170602
                MsgBox("Pattern Not Found.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If

            If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
                gSyslog.Save(ClassName & " Wrong Pattern!!")
                'Sue AlarmCode 20170602
                MsgBox("Wrong Pattern!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
            '=== 取得拍照結果 ===
            Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
            Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
            'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
            '=== 取得拍照結果 ===

            mRealPosX(i) = mCCDAlignPosX(i) - offsetX
            mRealPosY(i) = mCCDAlignPosY(i) - offsetY

        Next

        '=== 因公式限定Integer, 乘以1000倍計算 mm->um ===
        Dim mPos(2) As ProjectCore.CPoint
        For i As Integer = 0 To AlignCount - 1
            mPos(i) = New CPoint
            mPos(i).PointX = CInt(mRealPosX(i) * 1000)
            mPos(i).PointY = CInt(mRealPosY(i) * 1000)
        Next
        Dim mCenter As ProjectCore.CPoint = CMath.GetCenterby3Point(mPos(0), mPos(1), mPos(2))
        '=== 因公式限定Integer, 乘以1000倍計算 mm->um ===

        centerPosX = mCenter.PointX / 1000 '計算完轉為mm
        centerPosY = mCenter.PointY / 1000
        centerPosX = Math.Round(centerPosX, 3)
        centerPosY = Math.Round(centerPosY, 3)
        gSyslog.Save(ClassName & " centerPosX:" & centerPosX & " centerPosY:" & centerPosY)
        Debug.WriteLine(ClassName & " centerPosX:" & centerPosX & " centerPosY:" & centerPosY)

        '算出的圓心位置超出範圍
        If centerPosX < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or centerPosX > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
            centerPosY < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or centerPosY > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Then
            gSyslog.Save(ClassName & " centerPosX:" & centerPosX & " centerPosY:" & centerPosY & "is Out of Range ")
            Return False
        End If



        Return True
    End Function


End Class

Imports ProjectCore
Imports ProjectMotion
Imports ProjectAOI

'TODO: 刪除此Class
''' <summary>
''' 三點求圓心 
''' </summary>
''' <remarks></remarks>
Public Class CAction3PointFindCenter

    ''' <summary>半徑(mm)</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Radius As Decimal
    ''' <summary>基準角度(度)</summary>
    ''' <remarks></remarks>
    Public Angle As Decimal = 90

    ''' <summary>以當前位置往外移動半徑,以角度為基準取等分三點做為拍照點. 拍照後計算特徵位置座標. 並以三點求圓心計算中心點位置.</summary>
    ''' <param name="sys"></param>
    ''' <param name="sceneID"></param>
    ''' <param name="centerPosX"></param>
    ''' <param name="centerPosY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Run(ByVal sys As sSysParam, ByVal sceneID As String, ByRef centerPosX As Decimal, ByRef centerPosY As Decimal) As Boolean

        '以目前位置為基準,向外找三點拍照
        Dim mOriginPosX As Decimal
        Dim mOriginPosY As Decimal
        Dim mOriginPosZ As Decimal
        mOriginPosX = gCMotion.GetPositionValue(sys.AxisX)
        mOriginPosY = gCMotion.GetPositionValue(sys.AxisY)
        mOriginPosZ = gCMotion.GetPositionValue(sys.AxisZ)

        '=== 對半徑進行旋轉,取得投影量 ===
        Dim mCCDPosX(2) As Decimal
        Dim mCCDPosY(2) As Decimal
        Dim mCCDPosZ(2) As Decimal
        CMath.Rotation(Radius, 0, Angle, mCCDPosX(0), mCCDPosY(0))
        CMath.Rotation(Radius, 0, Angle + 120, mCCDPosX(0), mCCDPosY(0))
        CMath.Rotation(Radius, 0, Angle - 120, mCCDPosX(0), mCCDPosY(0))
        '=== 對半徑進行旋轉,取得投影量 ===

        '=== 投影量加上基準座標,成為拍照座標 ===
        For i As Integer = 0 To 2
            mCCDPosX(i) += mOriginPosX
            mCCDPosY(i) += mOriginPosY
            mCCDPosZ(i) = mOriginPosZ
        Next
        '=== 投影量加上基準座標,成為拍照座標 ===

        Dim mRealPosX(2) As Decimal
        Dim mRealPosY(2) As Decimal

        For i As Integer = 0 To 2
            Dim mAxisNo(4) As Integer
            Dim mTargetPos(4) As Decimal
            mAxisNo(0) = sys.AxisX
            mAxisNo(1) = sys.AxisY
            mAxisNo(2) = sys.AxisZ
            mAxisNo(3) = sys.AxisB
            mAxisNo(4) = sys.AxisC
            mTargetPos(0) = mCCDPosX(i)
            mTargetPos(1) = mCCDPosY(i)
            mTargetPos(2) = mCCDPosZ(i)
            mTargetPos(3) = 0
            mTargetPos(4) = 0
            gAOICollection.SetCCDScene(sys.CCDNo, sceneID) '選擇場景
            ButtonSafeMovePos(Nothing, mAxisNo, mTargetPos, sys) '移動到拍照位置

            Dim ticket As Integer = 0
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
            System.Threading.Thread.CurrentThread.Join(10)
            ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
            System.Threading.Thread.CurrentThread.Join(10)
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

            Dim timeOutStopWatch As New Stopwatch '逾時計時器
            timeOutStopWatch.Restart()
            Do
                Application.DoEvents()
                If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                    'Sue AlarmCode 20170602
                    'CCD 取像TimeOut
                    Select Case sys.StageNo
                        Case 0
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 1
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    Return False
                End If
            Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
            Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)

            'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneID) '更新控制項,必要條件 frmMain必須是實體

            timeOutStopWatch.Restart()
            Do
                If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                    Exit Do
                End If
                Application.DoEvents()
                If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                    MsgBox("Calculation is Timeout!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
            Loop

            timeOutStopWatch.Restart()
            Do
                Application.DoEvents()
                If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                    'Sue AlarmCode 20170602
                    MsgBox("Calculation is Timeout!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
            Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False

            If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                'Sue AlarmCode 20170602
                MsgBox("Pattern Not Found.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If

            If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
                'Sue AlarmCode 20170602
                MsgBox("Wrong Pattern!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
            '=== 取得拍照結果 ===
            Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
            Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
            'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
            '=== 取得拍照結果 ===

            '注意: 計算基準固定為CCD中心. 才能得知特徵相對於CCD中心多少距離
            mRealPosX(i) = mCCDPosX(i) + offsetX
            mRealPosY(i) = mCCDPosY(i) + offsetY

        Next
        '=== 因公式限定Integer, 乘以1000倍計算 mm->um ===
        Dim mPos(2) As ProjectCore.CPoint
        For i As Integer = 0 To 2
            mPos(i).PointX = CInt(mRealPosX(i) * 1000)
            mPos(i).PointY = CInt(mRealPosY(i) * 1000)
        Next
        Dim mCenter As ProjectCore.CPoint = CMath.GetCenterby3Point(mPos(0), mPos(1), mPos(2))
        '=== 因公式限定Integer, 乘以1000倍計算 mm->um ===

        centerPosX = mCenter.PointX / 1000 '計算完轉為mm
        centerPosY = mCenter.PointY / 1000
        Return True
    End Function

    Public Class CPoint
        Public PointX As Double
        Public PointY As Double
        Public Sub New()
            PointX = 0
            PointY = 0
        End Sub
        Public Sub New(ByVal x As Double, ByVal y As Double)
            PointX = x
            PointY = y
        End Sub
    End Class
End Class

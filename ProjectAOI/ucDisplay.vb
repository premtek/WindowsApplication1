Imports ProjectMotion
Imports ProjectCore
Imports ProjectRecipe

Public Class ucDisplay
    Public Enum DisplayType
        ''' <summary>未定義預設</summary>
        ''' <remarks></remarks>
        Picture = 0
        ''' <summary>CogDisplay</summary>
        ''' <remarks></remarks>
        Cognex = 1
        ''' <summary>FZ控制項</summary>
        ''' <remarks></remarks>
        Omronx64 = 2
    End Enum
    ''' <summary>內定顯示方式</summary>
    ''' <remarks></remarks>
    Dim mDisplayType As DisplayType = DisplayType.Picture
    
    Dim mCCDNo As Integer
    ''' <summary>Halcon是否Live</summary>
    ''' <remarks></remarks>
    Dim mIsHalconLive As Boolean

    Dim mImage As Cognex.VisionPro.ICogImage

    Public Sub ShowDisplay(ByVal index As DisplayType)
        mDisplayType = index
        Select Case index
            Case DisplayType.Cognex
                If Not CogDisplay1 Is Nothing Then
                    CogDisplay1.Dock = DockStyle.Fill
                    CogDisplay1.BringToFront()
                End If
                If Not CogDisplayStatusBarV21 Is Nothing Then
                    CogDisplayStatusBarV21.Display = CogDisplay1
                    CogDisplayStatusBarV21.Visible = True
                    CogDisplayStatusBarV21.BringToFront()
                End If
            Case DisplayType.Omronx64
                picDisplay.Dock = DockStyle.Fill
                picDisplay.BringToFront()
                CogDisplayStatusBarV21.Visible = False
            Case Else
                picDisplay.Dock = DockStyle.Fill
                picDisplay.BringToFront()
                CogDisplayStatusBarV21.Visible = False
        End Select
    End Sub

    ''' <summary>執行緒事件通知</summary>
    ''' <remarks></remarks>
    Dim mAutoWait As New System.Threading.AutoResetEvent(False)
    Dim mThread As Threading.Thread
    Public Function StartLive(ByVal ccdNo As Integer) As Boolean
        'Debug.Print("StartLive" & ccdNo)
        Try
            mCCDNo = ccdNo
            Select Case mDisplayType
                Case DisplayType.Cognex
                    If CogDisplay1.IsDisposed Then
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000020)) 'Soni + 2016.09.22 Copy from EndLive
                        'MsgBox(gMsgHandler.GetMessage(Alarm_2000020))

                        'gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000020), "Alarm_2000020", eMessageLevel.Alarm) 'Soni + 2016.09.22 Copy from EndLive
                        Return False
                    End If
                    If CogDisplay1.LiveDisplayRunning = True Then '重複叫用保護
                        'gSyslog.Save(gMsgHandler.GetMessage(Warn_3012001), "Warn_3012001", eMessageLevel.Warning) 'CCD1重複Start Live.
                        'Return False
                        'Return True
                        CogDisplay1.StopLiveDisplay()

                    End If

                    If gAOICollection.Items.Count = 0 Then '物件不存在
                        'sue0428
                        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000034))
                        'MsgBox(gMsgHandler.GetMessage(Warn_3000034))
                        'gSyslog.Save("StartLive. gAOICollection.Items.Count = 0", , eMessageLevel.Alarm)
                        Return False
                    End If

                    If Not gAOICollection.IsCCDExist(ccdNo) Then
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012000))
                        'MsgBox(gMsgHandler.GetMessage(Alarm_2012000))
                        'gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012000), "Alarm_2012000", eMessageLevel.Error)
                        Return False
                    End If
                    Dim acqTool As Cognex.VisionPro.CogAcqFifoTool
                    acqTool = gAOICollection.GetAcqTool(ccdNo) '取出AcqFifoTool工具

                    If acqTool Is Nothing Then 'Soni + 2016.09.22 嘗試攔截物件不存在
                        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000034))
                        'MsgBox(gMsgHandler.GetMessage(Warn_3000034))
                        'gSyslog.Save("StartLive. acqTool is Nothing", , eMessageLevel.Alarm)
                        Return False
                    End If
                    If acqTool.Operator Is Nothing Then 'Soni + 2016.09.22 嘗試攔截物件不存在
                        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000034))
                        'MsgBox(gMsgHandler.GetMessage(Warn_3000034))
                        'gSyslog.Save("A board might be missing or not be functioning properly.", , eMessageLevel.Alarm)
                        Return False
                    End If
                    CogDisplay1.StartLiveDisplay(acqTool.[Operator]) '對工具進行LIve操作
                    'gSyslog.Save(gMsgHandler.GetMessage(Warn_3012001), "Warn_3012001", eMessageLevel.Warning) 'CCD1重複Start Live.
                    CogDisplay1.StaticGraphics.Clear()
                    DrawTarget(ccdNo, gSSystemParameter.CCDTargetDataList)
                    Return True
                Case DisplayType.Picture
                    Return True
            End Select

        Catch ex As Exception
            Debug.Print("Exception Message:" & ex.Message)
            gSyslog.Save("Exception Message@StartLive:" & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
        Return False
    End Function

    Public Sub EndLive(Optional ByVal isDisposeDisplay As Boolean = True)
        Try
            Select Case mDisplayType
                Case DisplayType.Cognex
                    If CogDisplay1.IsDisposed Then
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000020))
                        'MsgBox(gMsgHandler.GetMessage(Alarm_2000020))
                        'gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000020), "Alarm_2000020", eMessageLevel.Alarm)
                        Exit Sub
                    End If
                    If CogDisplay1.LiveDisplayRunning = False Then '重複叫用保護
                        gSyslog.Save(gMsgHandler.GetMessage(Warn_3012002))
                        'MsgBox(gMsgHandler.GetMessage(Warn_3012002))

                        'gSyslog.Save(gMsgHandler.GetMessage(Warn_3012002), "Warn_3012002", eMessageLevel.Warning)
                        Exit Sub
                    End If
                    CogDisplay1.StopLiveDisplay() '停止Live
                    If isDisposeDisplay = True Then
                        CogDisplay1.Dispose()
                    End If

                    '第一個可能發生的例外狀況類型() 'Cognex.VisionPro.Exceptions.CogDisplayStartStopNotReEntrantException' 發生於 Cognex.VisionPro.Display.Controls.dll
                    'System.Transactions Critical: 0 : <TraceRecord xmlns="http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord" Severity="Critical"><TraceIdentifier>http://msdn.microsoft.com/TraceCodes/System/ActivityTracing/2004/07/Reliability/Exception/Unhandled</TraceIdentifier><Description>未處理的例外狀況</Description><AppDomain>Dispensing_Premtek.vshost.exe</AppDomain><Exception><ExceptionType>Cognex.VisionPro.Exceptions.CogDisplayStartStopNotReEntrantException, Cognex.VisionPro.Display.Controls, Version=53.2.0.0, Culture=neutral, PublicKeyToken=ef0f902af9dee505</ExceptionType><Message>StartLiveDisplay and StopLiveDisplay are not reentrant.</Message><StackTrace>   於 Cognex.VisionPro.Display.CogDisplay.StopLiveDisplay()
                    '於 ProjectAOI.ucDisplay.EndLive() 於 C:\Users\MyUser\Desktop\DTS_20150810v22\WindowsApplication1\ProjectAOI\ucDisplay.vb: 行 76
                    '於 WindowsApplication1.frmCalibrationCCD2FindHeight.frmCalibrationCCD2FindHeight_Deactivate(Object sender, EventArgs e) 於 C:\Users\MyUser\Desktop\DTS_20150810v22\WindowsApplication1\WindowsApplication1\frmCalibrationCCD2FindHeight.vb: 行 76
                    '於 System.Windows.Forms.Form.WmActivate(Message&amp;amp; m)
                    '於 System.Windows.Forms.Form.WndProc(Message&amp;amp; m)
                    '於 System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)</StackTrace><ExceptionString>Cognex.VisionPro.Exceptions.CogDisplayStartStopNotReEntrantException: StartLiveDisplay and StopLiveDisplay are not reentrant.
                    '             於 Cognex.VisionPro.Display.CogDisplay.StopLiveDisplay()
                    '於 ProjectAOI.ucDisplay.EndLive() 於 C:\Users\MyUser\Desktop\DTS_20150810v22\WindowsApplication1\ProjectAOI\ucDisplay.vb: 行 76
                    '於 WindowsApplication1.frmCalibrationCCD2FindHeight.frmCalibrationCCD2FindHeight_Deactivate(Object sender, EventArgs e) 於 C:\Users\MyUser\Desktop\DTS_20150810v22\WindowsApplication1\WindowsApplication1\frmCalibrationCCD2FindHeight.vb: 行 76
                    '於 System.Windows.Forms.Form.WmActivate(Message&amp;amp; m)
                    '於 System.Windows.Forms.Form.WndProc(Message&amp;amp; m)
                    '於 System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)</ExceptionString></Exception></TraceRecord>

            End Select
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

    End Sub


    Public Function GetCognexColorType(ByVal color As enmCCDTargetColor) As Cognex.VisionPro.CogColorConstants
        Select Case color
            Case enmCCDTargetColor.Black
                Return Cognex.VisionPro.CogColorConstants.Black
            Case enmCCDTargetColor.Blue
                Return Cognex.VisionPro.CogColorConstants.Blue
            Case enmCCDTargetColor.Red
                Return Cognex.VisionPro.CogColorConstants.Red
            Case enmCCDTargetColor.Yellow
                Return Cognex.VisionPro.CogColorConstants.Yellow
            Case Else
                Return Cognex.VisionPro.CogColorConstants.Black
        End Select
    End Function

    Public Sub DrawClear(ByVal ccdNo As Integer)
        CogDisplay1.StaticGraphics.Clear()
    End Sub

    Public Sub DrawTarget(ByVal ccdNo As Integer, ByVal DrawList As List(Of sCCDTargetData))
        Dim CCDParameter As sAOIAcquisitionParameter
        CCDParameter = gAOICollection.GetCCDParameter(ccdNo)
        Dim Width As Double = CCDParameter.ImageWidth '1600
        Dim Height As Double = CCDParameter.ImageHeight
        Dim hWidth As Double = Width * 0.5
        Dim hHeight As Double = Height * 0.5
        Dim mTargetType As enmCCDTargetType
        Dim mTargetColor As enmCCDTargetColor
        CogDisplay1.SuspendLayout()

        Dim HLine As New Cognex.VisionPro.CogLine
        Dim VLIne As New Cognex.VisionPro.CogLine
        Dim Line1 As New Cognex.VisionPro.CogLine
        Dim LIne2 As New Cognex.VisionPro.CogLine

        Dim VTickMark1 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim VTickMark2 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim HTickMark1 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim HTickMark2 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim TickMarkX1 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim TickMarkX2 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim TickMarkX3 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim TickMarkX4 As New List(Of Cognex.VisionPro.CogLineSegment)
        Dim Circle As New Cognex.VisionPro.CogCircle
        Dim Rectangle As New Cognex.VisionPro.CogRectangle
        Dim myGraphics As New Cognex.VisionPro.CogGraphicCollection


        For i As Integer = 0 To DrawList.Count - 1 ''gSSystemParameter.CCDTargetDataList.Count - 1
            mTargetType = DrawList(i).CCDTargetType
            mTargetColor = DrawList(i).CCDTargetColor
            Dim mTargetRadius As Double = DrawList(i).Radius
            Dim mTargetWidth As Double = DrawList(i).Width
            Dim mTargetHeight As Double = DrawList(i).Height
            Select Case mTargetType
                Case enmCCDTargetType.Cross  '十字
                    HLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                    VLIne.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                    HLine.LineWidthInScreenPixels = 1 '3
                    VLIne.LineWidthInScreenPixels = 1 '3
                    HLine.Color = GetCognexColorType(mTargetColor) 'Cognex.VisionPro.CogColorConstants.Green
                    VLIne.Color = GetCognexColorType(mTargetColor) 'Cognex.VisionPro.CogColorConstants.Green
                    HLine.SetFromStartXYEndXY(0, hHeight, Width, hHeight)
                    VLIne.SetFromStartXYEndXY(hWidth, 0, hWidth, Height)
                    'CogDisplay1.StaticGraphics.Add(HLine, "##")
                    'CogDisplay1.StaticGraphics.Add(VLIne, "##")
                    myGraphics.Add(HLine)
                    myGraphics.Add(VLIne)

                Case enmCCDTargetType.CrossX  'X
                    Line1.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                    LIne2.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                    Line1.LineWidthInScreenPixels = 1 '3
                    LIne2.LineWidthInScreenPixels = 1 '3
                    Line1.Color = GetCognexColorType(mTargetColor) 'Cognex.VisionPro.CogColorConstants.Green
                    LIne2.Color = GetCognexColorType(mTargetColor) 'Cognex.VisionPro.CogColorConstants.Green
                    Line1.SetFromStartXYEndXY(hWidth + 10, hHeight + 10, hWidth - 10, hHeight - 10)
                    LIne2.SetFromStartXYEndXY(hWidth + 10, hHeight - 10, hWidth - 10, hHeight + 10)
                    'CogDisplay1.StaticGraphics.Add(Line1, "##")
                    'CogDisplay1.StaticGraphics.Add(LIne2, "##")
                    myGraphics.Add(Line1)
                    myGraphics.Add(LIne2)

                Case enmCCDTargetType.TickMark  '刻度尺
                    If mTargetRadius <> 0 And gSSystemParameter.CCDScaleX2X(ccdNo) <> 0 And gSSystemParameter.CCDScaleY2Y(ccdNo) <> 0 Then
                        Dim FovWidth As Double = Width * gSSystemParameter.CCDScaleX2X(ccdNo)
                        Dim FovHight As Double = Height * gSSystemParameter.CCDScaleY2Y(ccdNo)
                        Dim WidthCount As Integer = FovWidth / mTargetRadius
                        Dim HightCount As Integer = FovHight / mTargetRadius
                        Dim PitchW As Double = mTargetRadius / gSSystemParameter.CCDScaleX2X(ccdNo)
                        Dim PitchH As Double = mTargetRadius / gSSystemParameter.CCDScaleY2Y(ccdNo)
                        Dim Count As Integer


                        For Count = 0 To (Math.Abs(WidthCount / 2)) - 1
                            Dim mtemp1 As New Cognex.VisionPro.CogLineSegment
                            mtemp1.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp1.LineWidthInScreenPixels = 1
                            mtemp1.Color = GetCognexColorType(mTargetColor)

                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp1.SetStartEnd(hWidth - (PitchW * (Count + 1)), hHeight - 25, hWidth - (PitchW * (Count + 1)), hHeight + 25)
                            Else
                                mtemp1.SetStartEnd(hWidth - (PitchW * (Count + 1)), hHeight - 15, hWidth - (PitchW * (Count + 1)), hHeight + 15)
                            End If
                            'CogDisplay1.StaticGraphics.Add(VTickMark1, "##")
                            VTickMark1.Add(mtemp1)
                            myGraphics.Add(VTickMark1(Count))

                            Dim mtemp2 As New Cognex.VisionPro.CogLineSegment
                            mtemp2.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp2.LineWidthInScreenPixels = 1
                            mtemp2.Color = GetCognexColorType(mTargetColor)
                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp2.SetStartEnd(hWidth + (PitchW * (Count + 1)), hHeight - 25, hWidth + (PitchW * (Count + 1)), hHeight + 25)
                            Else
                                mtemp2.SetStartEnd(hWidth + (PitchW * (Count + 1)), hHeight - 15, hWidth + (PitchW * (Count + 1)), hHeight + 15)
                            End If

                            'CogDisplay1.StaticGraphics.Add(VTickMark2, "##")
                            VTickMark2.Add(mtemp2)
                            myGraphics.Add(VTickMark2(Count))
                        Next

                        For Count = 0 To (Math.Abs(WidthCount / 2)) - 1
                            Dim mtemp3 As New Cognex.VisionPro.CogLineSegment
                            mtemp3.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp3.LineWidthInScreenPixels = 1
                            mtemp3.Color = GetCognexColorType(mTargetColor)
                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp3.SetStartEnd(hWidth - 25, hHeight - (PitchH * (Count + 1)), hWidth + 25, hHeight - (PitchH * (Count + 1)))
                            Else
                                mtemp3.SetStartEnd(hWidth - 15, hHeight - (PitchH * (Count + 1)), hWidth + 15, hHeight - (PitchH * (Count + 1)))
                            End If
                            'CogDisplay1.StaticGraphics.Add(HTickMark1, "##")
                            HTickMark1.Add(mtemp3)
                            myGraphics.Add(HTickMark1(Count))

                            Dim mtemp4 As New Cognex.VisionPro.CogLineSegment
                            mtemp4.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp4.LineWidthInScreenPixels = 1
                            mtemp4.Color = GetCognexColorType(mTargetColor)
                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp4.SetStartEnd(hWidth - 25, hHeight + (PitchH * (Count + 1)), hWidth + 25, hHeight + (PitchH * (Count + 1)))
                            Else
                                mtemp4.SetStartEnd(hWidth - 15, hHeight + (PitchH * (Count + 1)), hWidth + 15, hHeight + (PitchH * (Count + 1)))
                            End If
                            'CogDisplay1.StaticGraphics.Add(HTickMark2, "##")
                            HTickMark2.Add(mtemp4)
                            myGraphics.Add(HTickMark2(Count))
                        Next
                    End If

                Case enmCCDTargetType.TickMarkX
                    If mTargetRadius <> 0 And gSSystemParameter.CCDScaleX2X(ccdNo) <> 0 And gSSystemParameter.CCDScaleY2Y(ccdNo) <> 0 Then
                        Dim FovWidth As Double = Width * gSSystemParameter.CCDScaleX2X(ccdNo)
                        Dim FovHight As Double = Height * gSSystemParameter.CCDScaleY2Y(ccdNo)
                        Dim WidthCount As Integer = FovWidth / mTargetRadius
                        Dim HightCount As Integer = FovHight / mTargetRadius
                        Dim PitchP As Double = mTargetRadius / gSSystemParameter.CCDScaleX2X(ccdNo)
                        Dim PitchN As Double = mTargetRadius / gSSystemParameter.CCDScaleY2Y(ccdNo)
                        Dim Count As Integer


                        For Count = 0 To (Math.Abs(WidthCount) * 2 / 3 - 1)
                            Dim mtemp1 As New Cognex.VisionPro.CogLineSegment
                            mtemp1.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp1.LineWidthInScreenPixels = 1
                            mtemp1.Color = GetCognexColorType(mTargetColor)
                            '[Note]轉45度
                            Dim mtmpX As Double = PitchP * (Count + 1)
                            mtmpX = mtmpX * Math.Cos(45 * Math.PI / 180)
                            Dim mtmpY As Double = PitchP * (Count + 1)
                            mtmpY = mtmpY * Math.Sin(45 * Math.PI / 180)

                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp1.SetStartEnd(hWidth - mtmpX - 25, hHeight - mtmpY + 25, hWidth - mtmpX + 25, hHeight - mtmpY - 25)
                            Else
                                mtemp1.SetStartEnd(hWidth - mtmpX - 15, hHeight - mtmpY + 15, hWidth - mtmpX + 15, hHeight - mtmpY - 15)
                            End If
                            'CogDisplay1.StaticGraphics.Add(TickMarkX1, "##")
                            TickMarkX1.Add(mtemp1)
                            myGraphics.Add(TickMarkX1(Count))


                            Dim mtemp2 As New Cognex.VisionPro.CogLineSegment
                            mtemp2.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp2.LineWidthInScreenPixels = 1
                            mtemp2.Color = GetCognexColorType(mTargetColor)
                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp2.SetStartEnd(hWidth + mtmpX - 25, hHeight + mtmpY + 25, hWidth + mtmpX + 25, hHeight + mtmpY - 25)
                            Else
                                mtemp2.SetStartEnd(hWidth + mtmpX - 15, hHeight + mtmpY + 15, hWidth + mtmpX + 15, hHeight + mtmpY - 15)
                            End If
                            'CogDisplay1.StaticGraphics.Add(TickMarkX2, "##")
                            TickMarkX2.Add(mtemp2)
                            myGraphics.Add(TickMarkX2(Count))

                        Next

                        For Count = 0 To (Math.Abs(WidthCount) * 2 / 3 - 1)
                            Dim mtemp3 As New Cognex.VisionPro.CogLineSegment
                            mtemp3.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp3.LineWidthInScreenPixels = 1
                            mtemp3.Color = GetCognexColorType(mTargetColor)
                            '[Note]轉45度
                            Dim mtmpX As Double = PitchP * (Count + 1)
                            mtmpX = mtmpX * Math.Cos(-45 * Math.PI / 180)
                            Dim mtmpY As Double = PitchP * (Count + 1)
                            mtmpY = mtmpY * Math.Sin(-45 * Math.PI / 180)

                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp3.SetStartEnd(hWidth - mtmpX + 25, hHeight - mtmpY + 25, hWidth - mtmpX - 25, hHeight - mtmpY - 25)
                            Else
                                mtemp3.SetStartEnd(hWidth - mtmpX + 15, hHeight - mtmpY + 15, hWidth - mtmpX - 15, hHeight - mtmpY - 15)
                            End If
                            'CogDisplay1.StaticGraphics.Add(TickMarkX3, "##")
                            TickMarkX3.Add(mtemp3)
                            myGraphics.Add(TickMarkX3(Count))


                            Dim mtemp4 As New Cognex.VisionPro.CogLineSegment
                            mtemp4.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            mtemp4.LineWidthInScreenPixels = 1
                            mtemp4.Color = GetCognexColorType(mTargetColor)
                            If ((Count + 1) Mod 5) = 0 Then
                                mtemp4.SetStartEnd(hWidth + mtmpX + 25, hHeight + mtmpY + 25, hWidth + mtmpX - 25, hHeight + mtmpY - 25)
                            Else
                                mtemp4.SetStartEnd(hWidth + mtmpX + 15, hHeight + mtmpY + 15, hWidth + mtmpX - 15, hHeight + mtmpY - 15)
                            End If
                            'CogDisplay1.StaticGraphics.Add(TickMarkX4, "##")
                            TickMarkX4.Add(mtemp4)
                            myGraphics.Add(TickMarkX4(Count))
                        Next
                    End If

                Case enmCCDTargetType.Circle '圓
                    If mTargetRadius <> 0 And gSSystemParameter.CCDScaleX2X(ccdNo) <> 0 Then

                        Circle.CenterX = hWidth
                        Circle.CenterY = hHeight
                        Circle.Radius = (mTargetRadius / Math.Abs(gSSystemParameter.CCDScaleX2X(ccdNo)))
                        Circle.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        Circle.LineWidthInScreenPixels = 1
                        Circle.Color = GetCognexColorType(mTargetColor)
                        'CogDisplay1.StaticGraphics.Add(Circle, "##")
                        myGraphics.Add(Circle)
                    End If
                Case enmCCDTargetType.Rectangle '矩形
                    If mTargetHeight <> 0 And mTargetWidth <> 0 And gSSystemParameter.CCDScaleX2X(ccdNo) <> 0 And gSSystemParameter.CCDScaleY2Y(ccdNo) <> 0 Then
                        Rectangle.Color = GetCognexColorType(mTargetColor)
                        Rectangle.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        Rectangle.LineWidthInScreenPixels = 1
                        Rectangle.X = hWidth - (mTargetWidth / Math.Abs(gSSystemParameter.CCDScaleX2X(ccdNo)) / 2)
                        Rectangle.Y = hHeight - (mTargetHeight / Math.Abs(gSSystemParameter.CCDScaleX2X(ccdNo)) / 2)
                        Rectangle.Width = mTargetWidth / Math.Abs(gSSystemParameter.CCDScaleX2X(ccdNo))
                        Rectangle.Height = mTargetHeight / Math.Abs(gSSystemParameter.CCDScaleY2Y(ccdNo))
                        'CogDisplay1.StaticGraphics.Add(Rectangle, "##")
                        myGraphics.Add(Rectangle)
                    End If

                Case Else

            End Select

            CogDisplay1.StaticGraphics.AddList(myGraphics, "myGraphics")

        Next
        CogDisplay1.ResumeLayout()
    End Sub


    'Eason 20170120 Ticket:100030 , Memory Freed [S]
    Public Sub ManualDispose()
        'CogDisplay1.StaticGraphics.Clear()
        'CogDisplay1.StaticGraphics.Dispose()
        If picDisplay IsNot Nothing Then
            picDisplay.Dispose()
        End If
        If CogDisplay1 IsNot Nothing Then
            CogDisplay1.Dispose()
        End If
        If CogDisplayStatusBarV21 IsNot Nothing Then
            CogDisplayStatusBarV21.Dispose()
        End If

        picDisplay = Nothing
        CogDisplay1 = Nothing
        CogDisplayStatusBarV21 = Nothing
    End Sub
    'Eason 20170120 Ticket:100030 , Memory Freed [E]
End Class

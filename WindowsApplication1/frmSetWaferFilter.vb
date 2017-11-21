Imports MapData
Imports ProjectMotion
Imports ProjectCore
Imports Premtek
Imports ProjectRecipe
Imports ProjectAOI

Public Class frmSetWaferFilter

    ''' <summary>外部傳入Recipe
    ''' </summary>
    ''' <remarks></remarks>
    Public RecipeEdit As ProjectRecipe.CRecipe
    ''' <summary>外部傳入NodeID</summary>
    ''' <remarks></remarks>
    Public NodeID As String
    ''' <summary>NodeMap對應位置
    ''' </summary>
    ''' <remarks>注意從1開始, FirstDieIndex從0開始</remarks>
    Public NodeStartingX As Integer
    ''' <summary>NodeMap對應位置
    ''' </summary>
    ''' <remarks>注意從1開始, FirstDieIndex從0開始</remarks>
    Public NodeStartingY As Integer
    Dim conveyorNo As eConveyor = eConveyor.ConveyorNo1
    Dim mPageNo As Integer = 0

    'AutoPitch 依特徵往旁邊吋步移動找下一個特徵.
    '三點求圓心 
    '自動三點:依Pitch 往左到底, 往右到底 往下到底.
    '圓心求半徑
    '顆數 = 半徑/Pitch
    '依照定位點位置

    ''' <summary>Wafer邊緣第一點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosX1 As Decimal
    ''' <summary>Wafer邊緣第一點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosY1 As Decimal
    ''' <summary>Wafer邊緣第一點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosZ1 As Decimal
    ''' <summary>Wafer邊緣第二點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosX2 As Decimal
    ''' <summary>Wafer邊緣第二點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosY2 As Decimal
    ''' <summary>Wafer邊緣第二點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosZ2 As Decimal
    ''' <summary>Wafer邊緣第三點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosX3 As Decimal
    ''' <summary>Wafer邊緣第三點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosY3 As Decimal
    ''' <summary>Wafer邊緣第三點
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosZ3 As Decimal

    ''' <summary>Wafer圓心
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCenterX As Decimal
    ''' <summary>Wafer圓心
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCenterY As Decimal

    ''' <summary>元件左極限角落座標
    ''' </summary>
    ''' <remarks>左右極限元件需在同一列</remarks>
    Dim mLeftDiePosX As Decimal
    ''' <summary>元件左極限角落座標
    ''' </summary>
    ''' <remarks>左右極限元件需在同一列</remarks>
    Dim mLeftDiePosY As Decimal
    ''' <summary>元件左極限角落座標
    ''' </summary>
    ''' <remarks>左右極限元件需在同一列</remarks>
    Dim mLeftDiePosZ As Decimal
    ''' <summary>元件右極限角落座標
    ''' </summary>
    ''' <remarks>左右極限元件需在同一列</remarks>
    Dim mRightDiePosX As Decimal
    ''' <summary>元件右極限角落座標
    ''' </summary>
    ''' <remarks>左右極限元件需在同一列</remarks>
    Dim mRightDiePosY As Decimal
    ''' <summary>元件右極限角落座標
    ''' </summary>
    ''' <remarks>左右極限元件需在同一列</remarks>
    Dim mRightDiePosZ As Decimal

    Dim mTopDiePosX As Decimal
    Dim mTopDiePosY As Decimal
    Dim mTopDiePosZ As Decimal

    Dim mBottomDiePosX As Decimal
    Dim mBottomDiePosY As Decimal
    Dim mBottomDiePosZ As Decimal

    ''' <summary>Wafer邊界寬度
    ''' </summary>
    ''' <remarks></remarks>
    Dim mBoundary As Decimal

    ''' <summary>元件基準角落座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOriginCornerX As Decimal
    ''' <summary>元件基準角落座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOriginCornerY As Decimal
    ''' <summary>元件基準角落座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOriginCornerZ As Decimal
    ''' <summary>元件對角角落座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOppositeCornerX As Decimal
    ''' <summary>元件對角角落座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOppositeCornerY As Decimal
    ''' <summary>元件對角角落座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOppositeCornerZ As Decimal
    ''' <summary>A方向間距座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mADirectionX As Decimal
    ''' <summary>A方向間距座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mADirectionY As Decimal
    ''' <summary>A方向間距座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mADirectionZ As Decimal
    ''' <summary>B方向間距座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mBDirectionX As Decimal
    ''' <summary>B方向間距座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mBDirectionY As Decimal
    ''' <summary>B方向間距座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mBDirectionZ As Decimal

    ''' <summary>X軸方向數量
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCountX As Integer
    ''' <summary>Y軸方向數量
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCountY As Integer

    ''' <summary>外部配接系統
    ''' </summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    Dim mFileName As String = Application.StartupPath & "\System\" & MachineName & "\SetWaferMapFilter.ini"
    ''' <summary>Wafer半徑, 從圓心到邊緣
    ''' </summary>
    ''' <remarks></remarks>
    Dim mWaferRadius As Decimal
    ''' <summary>Wafer有效半徑, 從圓心到有效邊緣
    ''' </summary>
    ''' <remarks></remarks>
    Dim mWaferEffectiveRadius As Decimal
    ''' <summary>Wafer放置傾斜角度(Deg)
    ''' </summary>
    ''' <remarks></remarks>
    Dim mWaferAngle As Decimal

    ''' <summary>第一顆位置X
    ''' </summary>
    ''' <remarks></remarks>
    Dim mFirstDiePosX As Decimal
    ''' <summary>第一顆位置Y
    ''' </summary>
    ''' <remarks></remarks>
    Dim mFirstDiePosY As Decimal

    Dim mapData As New clsMapData

    Dim mIsUpdating As Boolean = False

    ''' <summary>左上角(0,0)座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOriginDiePosX As Decimal
    ''' <summary>左上角(0,0)座標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mOriginDiePosY As Decimal

    Private Sub frmSetWaferFilter_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (sys IsNot Nothing) Then
            ucJoyStick1.AxisX = sys.AxisX
            ucJoyStick1.AxisY = sys.AxisY
            ucJoyStick1.AxisZ = sys.AxisZ
            ucJoyStick1.AXisA = sys.AxisA
            ucJoyStick1.AXisB = sys.AxisB
            ucJoyStick1.AXisC = sys.AxisC
            ucJoyStick1.SetSpeedType(SpeedType.Slow)
            ucJoyStick1.RefreshPosition()

            If (gSSystemParameter.StageCount > 1) Then
                If (gSSystemParameter.MachineSafeData.Count > 0) Then
                    ucJoyStick1.InverseAxisX.SafeDistance = gSSystemParameter.MachineSafeData(sys.MachineNo).SafeDistanceX
                    ucJoyStick1.InverseAxisX.Spread = gSSystemParameter.MachineSafeData(sys.MachineNo).SpreadX

                    If (sys.StageNo = enmStage.No1) Then
                        ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage2).AxisX    '對立軸
                        ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                    ElseIf (sys.StageNo = enmStage.No2) Then
                        ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage1).AxisX    '對立軸
                        ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                    ElseIf (sys.StageNo = enmStage.No3) Then
                        ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage4).AxisX    '對立軸
                        ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                    ElseIf (sys.StageNo = enmStage.No4) Then
                        ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage3).AxisX    '對立軸
                        ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                    End If
                End If
            End If

            Select Case gAOICollection.GetCCDType(sys.CCDNo)
                Case enmCCDType.CognexVPRO
                    UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                Case enmCCDType.OmronFZS2MUDP
                    UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                Case Else
                    UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
            End Select

            '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---
            Dim mSceneName As String = "CALIB" & (sys.CCDNo + 1).ToString '預設CALIB1校正場景
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > 0 Then 'Soni 2017.02.09 雙軌資料結構
                If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene <> "" Then '如果定位點已設定可使用,則採用第一定位點
                    If gAOICollection.SceneDictionary.ContainsKey(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene) Then
                        mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                    End If
                End If
            End If

            UcLightControl1.CCDNo = sys.CCDNo
            If gAOICollection.SceneDictionary.ContainsKey(mSceneName) Then
                '[Note]:使用定位點場景光源，若沒有則使用CALIB1校正場景
                UcLightControl1.SceneName = mSceneName
                UcLightControl1.ShowUI()
                'SelectScene(mSceneName) '場景開光
            End If
            If gAOICollection.IsSceneExist(sys.CCDNo, mSceneName) Then
                gAOICollection.SetCCDScene(sys.CCDNo, mSceneName) '曝光,亮度
            End If

            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
            System.Threading.Thread.CurrentThread.Join(10)
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
            System.Threading.Thread.CurrentThread.Join(10)
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
            '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---

            If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
                'CCD 取像TimeOut
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select

            End If


            If RecipeEdit IsNot Nothing AndAlso RecipeEdit.Node.Count > sys.StageNo AndAlso RecipeEdit.Node(sys.StageNo).ContainsKey(NodeID) AndAlso RecipeEdit.Node(sys.StageNo)(NodeID).Array IsNot Nothing AndAlso RecipeEdit.Node(sys.StageNo)(NodeID).Array.Count > 0 Then
                Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(sys.StageNo)(NodeID).Array)
                UcWaferMapAdjust1.IsUpdating = True
                UcWaferMapAdjust1.CountX = mMultiArrayAdapter.GetMemoryCountX()
                UcWaferMapAdjust1.CountY = mMultiArrayAdapter.GetMemoryCountY()
                UcWaferMapAdjust1.IsUpdating = False
            End If

        End If
        LoadConfig(mFileName)

        btnWaferCenter_Click(sender, e)

        RefreshUI()
    End Sub

    Sub SaveConfig(ByVal fileName As String)
        Dim mSection As String = "WaferMapFilter"
        If sys IsNot Nothing Then
            If sys.StageNo <> 0 Then '第一組Stage以外命名
                mSection += (sys.StageNo + 1).ToString()
            End If
        End If
        mPosX1 = UcWaferMapCenter1.PosX1
        mPosY1 = UcWaferMapCenter1.PosY1
        mPosZ1 = UcWaferMapCenter1.PosZ1
        mPosX2 = UcWaferMapCenter1.PosX2
        mPosY2 = UcWaferMapCenter1.PosY2
        mPosZ2 = UcWaferMapCenter1.PosZ2
        mPosX3 = UcWaferMapCenter1.PosX3
        mPosY3 = UcWaferMapCenter1.PosY3
        mPosZ3 = UcWaferMapCenter1.PosZ3
        mBoundary = UcWaferMapCenter1.Boundary

        mLeftDiePosX = UcWaferMapAngle1.PosX1
        mLeftDiePosY = UcWaferMapAngle1.PosY1
        mLeftDiePosZ = UcWaferMapAngle1.PosZ1

        mRightDiePosX = UcWaferMapAngle1.PosX2
        mRightDiePosY = UcWaferMapAngle1.PosY2
        mRightDiePosZ = UcWaferMapAngle1.PosZ2

        mTopDiePosX = UcWaferMapAngle1.PosX3
        mTopDiePosY = UcWaferMapAngle1.PosY3
        mTopDiePosZ = UcWaferMapAngle1.PosZ3

        mBottomDiePosX = UcWaferMapAngle1.PosX4
        mBottomDiePosY = UcWaferMapAngle1.PosY4
        mBottomDiePosZ = UcWaferMapAngle1.PosZ4

        mCountX = UcWaferMapAngle1.CountX
        mCountY = UcWaferMapAngle1.CountY

        mOriginCornerX = UcWaferMapPitchSize1.PosX1
        mOriginCornerY = UcWaferMapPitchSize1.PosY1
        mOriginCornerZ = UcWaferMapPitchSize1.PosZ1

        mOppositeCornerX = UcWaferMapPitchSize1.PosX2
        mOppositeCornerY = UcWaferMapPitchSize1.PosY2
        mOppositeCornerZ = UcWaferMapPitchSize1.PosZ2

        mADirectionX = UcWaferMapPitchSize1.ADirectionX
        mADirectionY = UcWaferMapPitchSize1.ADirectionY
        mADirectionZ = UcWaferMapPitchSize1.ADirectionZ

        mBDirectionX = UcWaferMapPitchSize1.BDirectionX
        mBDirectionY = UcWaferMapPitchSize1.BDirectionY
        mBDirectionZ = UcWaferMapPitchSize1.BDirectionZ

        'mCountX = UcWaferMapAdjust1.CountX
        'mCountY = UcWaferMapAdjust1.CountY

        CIni.SaveIniString(mSection, "PosX1", mPosX1.ToString(), fileName)
        CIni.SaveIniString(mSection, "PosY1", mPosY1.ToString(), fileName)
        CIni.SaveIniString(mSection, "PosZ1", mPosZ1.ToString(), fileName)

        CIni.SaveIniString(mSection, "PosX2", mPosX2.ToString(), fileName)
        CIni.SaveIniString(mSection, "PosY2", mPosY2.ToString(), fileName)
        CIni.SaveIniString(mSection, "PosZ2", mPosZ2.ToString(), fileName)

        CIni.SaveIniString(mSection, "PosX3", mPosX3.ToString(), fileName)
        CIni.SaveIniString(mSection, "PosY3", mPosY3.ToString(), fileName)
        CIni.SaveIniString(mSection, "PosZ3", mPosZ3.ToString(), fileName)

        CIni.SaveIniString(mSection, "Boundary", mBoundary.ToString(), fileName)

        CIni.SaveIniString(mSection, "LeftDiePosX", mLeftDiePosX.ToString(), fileName)
        CIni.SaveIniString(mSection, "LeftDiePosY", mLeftDiePosY.ToString(), fileName)
        CIni.SaveIniString(mSection, "LeftDiePosZ", mLeftDiePosZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "RightDiePosX", mRightDiePosX.ToString(), fileName)
        CIni.SaveIniString(mSection, "RightDiePosY", mRightDiePosY.ToString(), fileName)
        CIni.SaveIniString(mSection, "RightDiePosZ", mRightDiePosZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "TopDiePosX", mTopDiePosX.ToString(), fileName)
        CIni.SaveIniString(mSection, "TopDiePosY", mTopDiePosY.ToString(), fileName)
        CIni.SaveIniString(mSection, "TopDiePosZ", mTopDiePosZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "BottomDiePosX", mBottomDiePosX.ToString(), fileName)
        CIni.SaveIniString(mSection, "BottomDiePosY", mBottomDiePosY.ToString(), fileName)
        CIni.SaveIniString(mSection, "BottomDiePosZ", mBottomDiePosZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "ADirectionX", mADirectionX.ToString(), fileName)
        CIni.SaveIniString(mSection, "ADirectionY", mADirectionY.ToString(), fileName)
        CIni.SaveIniString(mSection, "ADirectionZ", mADirectionZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "BDirectionX", mBDirectionX.ToString(), fileName)
        CIni.SaveIniString(mSection, "BDirectionY", mBDirectionY.ToString(), fileName)
        CIni.SaveIniString(mSection, "BDirectionZ", mBDirectionZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "OriginCornerX", mOriginCornerX.ToString(), fileName)
        CIni.SaveIniString(mSection, "OriginCornerY", mOriginCornerY.ToString(), fileName)
        CIni.SaveIniString(mSection, "OriginCornerZ", mOriginCornerZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "OppositeCornerX", mOppositeCornerX.ToString(), fileName)
        CIni.SaveIniString(mSection, "OppositeCornerY", mOppositeCornerY.ToString(), fileName)
        CIni.SaveIniString(mSection, "OppositeCornerZ", mOppositeCornerZ.ToString(), fileName)

        CIni.SaveIniString(mSection, "CountX", mCountX.ToString(), fileName)
        CIni.SaveIniString(mSection, "CountY", mCountY.ToString(), fileName)
    End Sub

    Sub LoadConfig(ByVal fileName As String)
        Dim mSection As String = "WaferMapFilter"
        If sys IsNot Nothing Then
            If sys.StageNo <> 0 Then '第一組Stage以外命名
                mSection += (sys.StageNo + 1).ToString()
            End If
        End If
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosX1", fileName, 0), mPosX1)
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosY1", fileName, 0), mPosY1)
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosZ1", fileName, 0), mPosZ1)

        Decimal.TryParse(CIni.ReadIniString(mSection, "PosX2", fileName, 0), mPosX2)
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosY2", fileName, 0), mPosY2)
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosZ2", fileName, 0), mPosZ2)

        Decimal.TryParse(CIni.ReadIniString(mSection, "PosX3", fileName, 0), mPosX3)
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosY3", fileName, 0), mPosY3)
        Decimal.TryParse(CIni.ReadIniString(mSection, "PosZ3", fileName, 0), mPosZ3)

        Decimal.TryParse(CIni.ReadIniString(mSection, "Boundary", fileName, 0), mBoundary)

        Decimal.TryParse(CIni.ReadIniString(mSection, "LeftDiePosX", fileName, 0), mLeftDiePosX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "LeftDiePosY", fileName, 0), mLeftDiePosY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "LeftDiePosZ", fileName, 0), mLeftDiePosZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "RightDiePosX", fileName, 0), mRightDiePosX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "RightDiePosY", fileName, 0), mRightDiePosY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "RightDiePosZ", fileName, 0), mRightDiePosZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "TopDiePosX", fileName, 0), mTopDiePosX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "TopDiePosY", fileName, 0), mTopDiePosY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "TopDiePosZ", fileName, 0), mTopDiePosZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "BottomDiePosX", fileName, 0), mBottomDiePosX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "BottomDiePosY", fileName, 0), mBottomDiePosY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "BottomDiePosZ", fileName, 0), mBottomDiePosZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "ADirectionX", fileName, 0), mADirectionX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "ADirectionY", fileName, 0), mADirectionY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "ADirectionZ", fileName, 0), mADirectionZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "BDirectionX", fileName, 0), mBDirectionX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "BDirectionY", fileName, 0), mBDirectionY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "BDirectionZ", fileName, 0), mBDirectionZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "OriginCornerX", fileName, 0), mOriginCornerX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "OriginCornerY", fileName, 0), mOriginCornerY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "OriginCornerZ", fileName, 0), mOriginCornerZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "OppositeCornerX", fileName, 0), mOppositeCornerX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "OppositeCornerY", fileName, 0), mOppositeCornerY)
        Decimal.TryParse(CIni.ReadIniString(mSection, "OppositeCornerZ", fileName, 0), mOppositeCornerZ)

        Decimal.TryParse(CIni.ReadIniString(mSection, "CountX", fileName, 1), mCountX)
        Decimal.TryParse(CIni.ReadIniString(mSection, "CountY", fileName, 1), mCountY)

        UcWaferMapCenter1.PosX1 = mPosX1
        UcWaferMapCenter1.PosY1 = mPosY1
        UcWaferMapCenter1.PosZ1 = mPosZ1
        UcWaferMapCenter1.PosX2 = mPosX2
        UcWaferMapCenter1.PosY2 = mPosY2
        UcWaferMapCenter1.PosZ2 = mPosZ2
        UcWaferMapCenter1.PosX3 = mPosX3
        UcWaferMapCenter1.PosY3 = mPosY3
        UcWaferMapCenter1.PosZ3 = mPosZ3
        UcWaferMapCenter1.Boundary = mBoundary
        UcWaferMapCenter1.ShowProperty()
        UcWaferMapCenter1.UpdateUI()
        AddHandler UcWaferMapCenter1.MoveStart, AddressOf MoveStart
        AddHandler UcWaferMapCenter1.PosChanged, AddressOf MoveEnd

        AddHandler UcWaferMapAngle1.MoveStart, AddressOf MoveStart
        AddHandler UcWaferMapAngle1.PosChanged, AddressOf MoveEnd

        AddHandler UcWaferMapPitchSize1.MoveStart, AddressOf MoveStart
        AddHandler UcWaferMapPitchSize1.PosChanged, AddressOf MoveEnd

        AddHandler UcWaferMapAdjust1.MoveStart, AddressOf MoveStart
        AddHandler UcWaferMapAdjust1.PosChanged, AddressOf MoveEnd

        AddHandler UcIndexer1.MoveStart, AddressOf MoveStart
        AddHandler UcIndexer1.PosChanged, AddressOf MoveEnd
        UcWaferMapAngle1.WaferCenterX = UcWaferMapCenter1.CenterX '極左
        UcWaferMapAngle1.WaferCenterY = UcWaferMapCenter1.CenterY
        UcWaferMapAngle1.WaferCenterY = mPosZ1
        UcWaferMapAngle1.WaferEffectiveRadius = UcWaferMapCenter1.WaferEffectiveRadius '有效半徑

        UcWaferMapAngle1.PosX1 = mLeftDiePosX '左Die
        UcWaferMapAngle1.PosY1 = mLeftDiePosY
        UcWaferMapAngle1.PosZ1 = mLeftDiePosZ
        UcWaferMapAngle1.PosX2 = mRightDiePosX '右Die
        UcWaferMapAngle1.PosY2 = mRightDiePosY
        UcWaferMapAngle1.PosZ2 = mRightDiePosZ
        UcWaferMapAngle1.PosX3 = mTopDiePosX '上Die
        UcWaferMapAngle1.PosY3 = mTopDiePosY
        UcWaferMapAngle1.PosZ3 = mTopDiePosZ
        UcWaferMapAngle1.PosX4 = mBottomDiePosX '下Die
        UcWaferMapAngle1.PosY4 = mBottomDiePosY
        UcWaferMapAngle1.PosZ4 = mBottomDiePosZ
        UcWaferMapAngle1.CountX = mCountX
        UcWaferMapAngle1.CountY = mCountY
        UcWaferMapAngle1.ShowProperty()
        UcWaferMapAngle1.UpdateUI()
        UcWaferMapPitchSize1.WaferAngle = UcWaferMapAngle1.WaferAngle

        UcWaferMapPitchSize1.PosX1 = mOriginCornerX
        UcWaferMapPitchSize1.PosY1 = mOriginCornerY
        UcWaferMapPitchSize1.PosZ1 = mOriginCornerZ

        UcWaferMapPitchSize1.PosX2 = mOppositeCornerX
        UcWaferMapPitchSize1.PosY2 = mOppositeCornerY
        UcWaferMapPitchSize1.PosZ2 = mOppositeCornerZ

        UcWaferMapPitchSize1.ADirectionX = mADirectionX
        UcWaferMapPitchSize1.ADirectionY = mADirectionY
        UcWaferMapPitchSize1.ADirectionZ = mADirectionZ

        UcWaferMapPitchSize1.BDirectionX = mBDirectionX
        UcWaferMapPitchSize1.BDirectionY = mBDirectionY
        UcWaferMapPitchSize1.BDirectionZ = mBDirectionZ
        UcWaferMapPitchSize1.ShowProperty()
        UcWaferMapPitchSize1.UpdateUI()

        UcWaferMapAdjust1.FirstDieIndexX = RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX
        UcWaferMapAdjust1.FirstDieIndexY = RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY
        UcWaferMapAdjust1.FirstDiePosX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).TeachPosX
        UcWaferMapAdjust1.FirstDiePosY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).TeachPosY
        UcWaferMapAdjust1.FirstDiePosZ = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).TeachPosZ
        UcWaferMapAdjust1.CountX = mCountX
        UcWaferMapAdjust1.CountY = mCountY
        UcWaferMapAdjust1.PitchX = UcWaferMapPitchSize1.PitchX
        UcWaferMapAdjust1.PitchY = UcWaferMapPitchSize1.PitchY
        UcWaferMapAdjust1.WaferAngle = UcWaferMapAngle1.WaferAngle
        UcWaferMapAdjust1.WaferAngleY = UcWaferMapAdjust1.WaferAngle
        UcWaferMapAdjust1.WaferCenterX = UcWaferMapCenter1.CenterX
        UcWaferMapAdjust1.WaferCenterY = UcWaferMapCenter1.CenterY
        UcWaferMapAdjust1.WaferEffectiveRadius = UcWaferMapCenter1.WaferEffectiveRadius
        UcWaferMapAdjust1.UpdateUI()

    End Sub

    Sub MoveStart(sender As Object, e As EventArgs)
        ucJoyStick1.SetSpeedType(SpeedType.Slow)
        btnCancel.BeginInvoke(Sub()
                                  btnCancel.Enabled = False
                                  btnOK.Enabled = False
                                  btnNext.Enabled = False
                                  btnPrev.Enabled = False
                                  ucJoyStick1.Enabled = False
                              End Sub)
    End Sub

    Sub MoveEnd(sender As Object, e As EventArgs)
        btnCancel.BeginInvoke(Sub()
                                  btnCancel.Enabled = True
                                  btnOK.Enabled = True
                                  btnNext.Enabled = True
                                  btnPrev.Enabled = True
                                  ucJoyStick1.Enabled = True
                              End Sub)
        ucJoyStick1.RefreshPosition()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SaveConfig(mFileName)
        MsgBox("Save OK")
        If MsgBox("Overwritte Array Config?", MsgBoxStyle.OkCancel + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Ok Then
            RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX = UcWaferMapAdjust1.FirstDieIndexX
            RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY = UcWaferMapAdjust1.FirstDieIndexY
            RecipeEdit.Node(sys.StageNo)(NodeID).Array(0).LevelType = eLevelType.Array
            RecipeEdit.Node(sys.StageNo)(NodeID).Array(0).Array.CountX = mCountX
            RecipeEdit.Node(sys.StageNo)(NodeID).Array(0).Array.CountY = mCountY
            RecipeEdit.Node(sys.StageNo)(NodeID).Array(0).Array.Theta = mWaferAngle
            RecipeEdit.Node(sys.StageNo)(NodeID).Array(0).Array.PitchX = UcWaferMapAngle1.PitchX
            RecipeEdit.Node(sys.StageNo)(NodeID).Array(0).Array.PitchY = -UcWaferMapAngle1.PitchY '方向相反
        End If

    End Sub

    Private Sub btnWaferCenter_Click(sender As Object, e As EventArgs) Handles btnWaferCenter.Click
        UcWaferMapCenter1.Sys = Me.sys
        UcWaferMapCenter1.Motion = gCMotion
        UcWaferMapCenter1.Width = palWafer.Width
        UcWaferMapCenter1.Height = palWafer.Height
        UcWaferMapCenter1.Location = New Point(0, 0)
        UcWaferMapCenter1.Visible = True
        UcWaferMapCenter1.BringToFront()

        UcWaferMapCenter1.ShowProperty()
        UcWaferMapCenter1.UpdateUI()
        UcIndexer1.Visible = False
        mPageNo = 0
        btnPrev.Visible = False
        btnNext.Visible = True
    End Sub

    Private Sub btnWaferAngle_Click(sender As Object, e As EventArgs) Handles btnWaferAngle.Click
        UcWaferMapAngle1.Sys = Me.sys
        UcWaferMapAngle1.Motion = gCMotion
        UcWaferMapAngle1.Width = palWafer.Width
        UcWaferMapAngle1.Height = palWafer.Height
        UcWaferMapAngle1.Location = New Point(0, 0)
        UcWaferMapAngle1.Visible = True
        UcWaferMapAngle1.BringToFront()
        UcWaferMapAngle1.WaferCenterX = UcWaferMapCenter1.CenterX
        UcWaferMapAngle1.WaferCenterY = UcWaferMapCenter1.CenterY
        UcWaferMapAngle1.WaferCenterZ = UcWaferMapCenter1.PosZ1
        UcWaferMapAngle1.WaferEffectiveRadius = UcWaferMapCenter1.WaferEffectiveRadius
        UcWaferMapAngle1.ShowProperty()
        UcWaferMapAngle1.UpdateUI()
        UcIndexer1.Visible = False
        mPageNo = 1
        btnPrev.Visible = True
        btnNext.Visible = True
    End Sub

    Private Sub btnDieSizePitch_Click(sender As Object, e As EventArgs) Handles btnDieSizePitch.Click

        UcWaferMapPitchSize1.Sys = Me.sys
        UcWaferMapPitchSize1.Motion = gCMotion
        UcWaferMapPitchSize1.Width = palWafer.Width
        UcWaferMapPitchSize1.Height = palWafer.Height
        UcWaferMapPitchSize1.Location = New Point(0, 0)
        UcWaferMapPitchSize1.Visible = True
        UcWaferMapPitchSize1.BringToFront()

        UcWaferMapPitchSize1.WaferAngle = UcWaferMapAngle1.WaferAngle
        UcWaferMapPitchSize1.WaferCenterX = UcWaferMapCenter1.CenterX
        UcWaferMapPitchSize1.WaferCenterY = UcWaferMapCenter1.CenterY
        UcWaferMapPitchSize1.WaferCenterZ = UcWaferMapCenter1.PosZ1
        UcWaferMapPitchSize1.LeftDiePosX = UcWaferMapAngle1.PosX1
        UcWaferMapPitchSize1.LeftDiePosY = UcWaferMapAngle1.PosY1
        UcWaferMapPitchSize1.LeftDiePosZ = UcWaferMapAngle1.PosZ1
        UcWaferMapPitchSize1.RightDiePosX = UcWaferMapAngle1.PosX2
        UcWaferMapPitchSize1.RightDiePosY = UcWaferMapAngle1.PosY2
        UcWaferMapPitchSize1.RightDiePosZ = UcWaferMapAngle1.PosZ2
        UcWaferMapPitchSize1.TopDiePosX = UcWaferMapAngle1.PosX3
        UcWaferMapPitchSize1.TopDiePosY = UcWaferMapAngle1.PosY3
        UcWaferMapPitchSize1.TopDiePosZ = UcWaferMapAngle1.PosZ3
        UcWaferMapPitchSize1.BottomDiePosX = UcWaferMapAngle1.PosX4
        UcWaferMapPitchSize1.BottomDiePosY = UcWaferMapAngle1.PosY4
        UcWaferMapPitchSize1.BottomDiePosZ = UcWaferMapAngle1.PosZ4
        UcWaferMapPitchSize1.ShowProperty()
        UcWaferMapPitchSize1.UpdateUI()
        UcIndexer1.Visible = False
        mPageNo = 2
        btnPrev.Visible = True
        btnNext.Visible = True
    End Sub

    Private Sub btnAdjustMap_Click(sender As Object, e As EventArgs) Handles btnAdjustMap.Click
        UcWaferMapAdjust1.Sys = Me.sys
        UcWaferMapAdjust1.Motion = gCMotion
        UcWaferMapAdjust1.Width = palWafer.Width
        UcWaferMapAdjust1.Height = palWafer.Height
        UcWaferMapAdjust1.Location = New Point(0, 0)
        UcWaferMapAdjust1.Visible = True
        UcWaferMapAdjust1.BringToFront()
       
        UcWaferMapAdjust1.DieLeftDownCornerX = UcWaferMapPitchSize1.PosX1
        UcWaferMapAdjust1.DieLeftDownCornerY = UcWaferMapPitchSize1.PosY1
        UcWaferMapAdjust1.DieSizeX = UcWaferMapPitchSize1.DieSizeX
        UcWaferMapAdjust1.DieSizeY = UcWaferMapPitchSize1.DieSizeY
        UcWaferMapAdjust1.CountX = UcWaferMapAngle1.CountX
        UcWaferMapAdjust1.CountY = UcWaferMapAngle1.CountY
        UcWaferMapAdjust1.PitchX = UcWaferMapAngle1.PitchX
        UcWaferMapAdjust1.PitchY = UcWaferMapAngle1.PitchY
        UcWaferMapAdjust1.WaferAngle = UcWaferMapAngle1.WaferAngle + gSSystemParameter.OrthgonalAngleDiff(sys.StageNo) '計算的角度+ Stage正交誤差角度
        UcWaferMapAdjust1.WaferAngleY = UcWaferMapAngle1.WaferAngle + gSSystemParameter.OrthgonalAngleDiff(sys.StageNo) '計算的角度+ Stage正交誤差角度
        'UcWaferMapAdjust1.WaferCenterX = UcWaferMapCenter1.CenterX
        'UcWaferMapAdjust1.WaferCenterY = UcWaferMapCenter1.CenterY
        UcWaferMapAdjust1.WaferCenterX = UcWaferMapPitchSize1.WaferCenterX2
        UcWaferMapAdjust1.WaferCenterY = UcWaferMapPitchSize1.WaferCenterY2
        UcWaferMapAdjust1.WaferEffectiveRadius = UcWaferMapCenter1.WaferEffectiveRadius
        UcWaferMapAdjust1.ShowProperty()
        UcWaferMapAdjust1.UpdateUI()
        UcIndexer1.DieSizeX = UcWaferMapPitchSize1.DieSizeX
        UcIndexer1.DieSizeY = UcWaferMapPitchSize1.DieSizeY
        UcIndexer1.LeftUpperPosX = UcWaferMapAdjust1.TranslatedLeftUpperPosX
        UcIndexer1.LeftUpperPosY = UcWaferMapAdjust1.TranslatedLeftUpperPosY
        UcIndexer1.PitchX = UcWaferMapAngle1.PitchX
        UcIndexer1.PitchY = UcWaferMapAngle1.PitchY
        UcIndexer1.WaferAngle = UcWaferMapAdjust1.WaferAngle
        UcIndexer1.WaferAngleY = UcWaferMapAdjust1.WaferAngle
        UcIndexer1.WaferCenterX = UcWaferMapPitchSize1.WaferCenterX2
        UcIndexer1.WaferCenterY = UcWaferMapPitchSize1.WaferCenterY2
        UcIndexer1.WaferCenterZ = UcWaferMapPitchSize1.WaferCenterZ2
        UcIndexer1.Sys = sys
        UcIndexer1.ShowProperty()
        UcIndexer1.Visible = False
        mPageNo = 3
        btnPrev.Visible = True
        btnNext.Visible = False
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case mPageNo
            Case 0
                btnWaferAngle_Click(sender, e)
            Case 1
                btnDieSizePitch_Click(sender, e)
            Case 2
                btnAdjustMap_Click(sender, e)

        End Select
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Select Case mPageNo
            Case 1
                btnWaferCenter_Click(sender, e)
            Case 2
                btnWaferAngle_Click(sender, e)
            Case 3
                btnDieSizePitch_Click(sender, e)
        End Select
    End Sub

    Private Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            ucJoyStick1.Enabled = False
            UcWaferMapAdjust1.Enabled = False
            UcWaferMapAngle1.Enabled = False
            UcWaferMapCenter1.Enabled = False
            UcWaferMapPitchSize1.Enabled = False
        End If
    End Sub
End Class
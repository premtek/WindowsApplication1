﻿Imports ProjectCore

Public Module MCommonRecipe

#Region "列舉"
    ''' <summary>步驟功能</summary>
    ''' <remarks></remarks>
    Public Enum eStepFunctionType
        ''' <summary>矩形</summary>
        ''' <remarks></remarks>
        Rectangle = 2
        ''' <summary>畫圓</summary>
        ''' <remarks></remarks>
        Circle2D = 3
        ''' <summary>畫弧</summary>
        ''' <remarks></remarks>
        Arc2D = 4
        ''' <summary>起始線段</summary>
        ''' <remarks></remarks>
        FirstLine = 6
        ''' <summary>結束線段</summary>
        ''' <remarks></remarks>
        EndLine = 7
        ''' <summary>選擇閥組</summary>
        ''' <remarks></remarks>
        SelectValve = 8
        ''' <summary>連續動作開始</summary>
        ''' <remarks></remarks>
        ContiStart = 9
        ''' <summary>連續動作結束</summary>
        ''' <remarks></remarks>
        ContiEnd = 10
        ''' <summary>等待</summary>
        ''' <remarks></remarks>
        Wait = 11
        ''' <summary>前檢</summary>
        ''' <remarks></remarks>
        Picture = 12
        ''' <summary>上層定位到本層定位銜接</summary>
        ''' <remarks></remarks>
        CCDStart = 13
        ''' <summary>本層定位到本層定位銜接</summary>
        ''' <remarks></remarks>
        CCDLine = 14
        ''' <summary>本層定位到上層定位銜接</summary>
        ''' <remarks></remarks>
        CCDEnd = 15
        ''' <summary>作業後檢測</summary>
        ''' <remarks></remarks>
        Inspect = 16
        ''' <summary>畫點3D</summary>
        ''' <remarks></remarks>
        Dots3D = 17
        ''' <summary>畫線3D</summary>
        ''' <remarks></remarks>
        Line3D = 18
        ''' <summary>畫弧3D</summary>
        ''' <remarks></remarks>
        Arc3D = 19
        ''' <summary>移動3D</summary>
        ''' <remarks></remarks>
        Move3D = 20
        ''' <summary>畫圓3D</summary>
        ''' <remarks></remarks>
        Circle3D = 21
        ''' <summary>延伸開始</summary>
        ''' <remarks></remarks>
        ExtendOn = 22
        ''' <summary>延伸結束</summary>
        ''' <remarks></remarks>
        ExtendOff = 23
        ''' <summary>叫用陣列Pattern</summary>
        ''' <remarks></remarks>
        Array = 24
        ''' <summary>叫用單一Pattern</summary>
        ''' <remarks></remarks>
        SubPattern = 25
    End Enum


    ''' <summary>[PathMode]</summary>
    ''' <remarks></remarks>
    Public Enum ePathMode
        ''' <summary>[路徑串接的起始段]</summary>
        ''' <remarks></remarks>
        ContiStart = 0
        ''' <summary>[路徑串接的結束段]</summary>
        ''' <remarks></remarks>
        ContiEnd = 1
    End Enum

    ''' <summary>[點膠狀態]</summary>
    ''' <remarks></remarks>
    Public Enum eDispensingStatus
        ''' <summary>[尚未處理]</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>[點膠失敗(異常)]</summary>
        ''' <remarks></remarks>
        Fail = 1
        ''' <summary>[點膠完成]</summary>
        ''' <remarks></remarks>
        OK = 2
    End Enum

#End Region

    ''' <summary>平台MAP</summary>
    ''' <remarks></remarks>
    Public Class CStageMap
        ''' <summary>依照節點區分, 節點索引參考搜尋結果, 使用Dictionary是為了確保在不同搜尋邏輯下的參考一致</summary>
        ''' <remarks></remarks>
        Public Node As New Dictionary(Of String, CPatternMap)
        ''' <summary>
        ''' [暫停時，復原位置紀錄]
        ''' </summary>
        ''' <remarks></remarks>
        Public ReBackPos As AxisPos

        'Eason 20170302 Ticket:100090 , System Update Crash [S]
        Public mobjectLock As Object = New Object() '資料更新用的互斥鎖
        ''' <summary>
        ''' [Deep Copy]
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Clone() As CStageMap
            Dim Buffer As CStageMap = New CStageMap()
            'Deep Copy need one by one copy
            SyncLock (mobjectLock)
                Buffer.ReBackPos = Me.ReBackPos
                Buffer.Node.Clear()
                For Each item In Me.Node
                    Buffer.Node.Add(item.Key, item.Value.Clone())
                Next
            End SyncLock
            Return Buffer
        End Function
        Public Function IsPatternMapDataChange() As Boolean
            Dim Result As Boolean = False
            SyncLock (mobjectLock)
                For Each item In Node
                    If (item.Value.IsMapDataChange(True)) Then
                        Result = True
                    End If
                Next
            End SyncLock
            Return Result
        End Function
        'Eason 20170302 Ticket:100090 , System Update Crash [E]

    End Class


    ''' <summary>[Round Map]</summary>
    ''' <remarks></remarks>
    Public Class CRoundMap
        ''' <summary>[紀錄每個Round的點膠狀態]</summary>
        ''' <remarks></remarks>
        Public DispensingStatus(,) As eDispensingStatus
        'Eason 20170302 Ticket:100090 , System Update Crash [S]
        Private IsDispensingStatusChangeLock As Object = New Object()
        Private mIsDispensingStatusChange As Boolean = False
        Public Property IsDispensingStatusChange As Boolean
            Set(value As Boolean)
                SyncLock (IsDispensingStatusChangeLock)
                    mIsDispensingStatusChange = value
                End SyncLock
            End Set
            Get
                Return mIsDispensingStatusChange
            End Get
        End Property
        Public Function SetDispensingStatus(x As Integer, y As Integer, value As eDispensingStatus)
            If (x > DispensingStatus.GetUpperBound(0) OrElse y > DispensingStatus.GetUpperBound(1)) Then
                Return False
            End If
            If (DispensingStatus(x, y) <> value) Then
                DispensingStatus(x, y) = value
                IsDispensingStatusChange = True
            End If
            Return True
        End Function
        Public Function Clone() As CRoundMap
            Dim Buffer As CRoundMap = New CRoundMap()
            ReDim Buffer.DispensingStatus(Me.DispensingStatus.GetUpperBound(0), Me.DispensingStatus.GetUpperBound(1))
            Array.Copy(Me.DispensingStatus, Buffer.DispensingStatus, Me.DispensingStatus.Length)
            Return Buffer
        End Function
        'Eason 20170302 Ticket:100090 , System Update Crash [E]
    End Class

    ''' <summary>Conveyor相關參數</summary>
    ''' <remarks></remarks>
    Public Class CRecipeConveyorPos
       
        ''' [定位點資料結構，數量等同結構數]
        ''' <summary>
        ''' </summary>
        ''' <remarks></remarks>
        Public AlignmentData As New List(Of AlignmentStructure)
      
        ''' <summary>
        ''' [SkipMark資料結構，數量等同結構數]
        ''' </summary>
        ''' <remarks></remarks>
        Public SkipMarkData As New List(Of AlignmentStructure)
        
        ''' <summary>
        ''' [雷射測高資料結構，數量等同結構數]
        ''' </summary>
        ''' <remarks></remarks>
        Public LaserData As New List(Of LaserStructure)

        ''' <summary>介面教導點膠基準點
        ''' </summary>
        ''' <remarks></remarks>
        Public TeachBasicPosX As Decimal
        ''' <summary>介面教導點膠基準點
        ''' </summary>
        ''' <remarks></remarks>
        Public TeachBasicPosY As Decimal
        ''' <summary>介面教導點膠基準點
        ''' </summary>
        ''' <remarks></remarks>
        Public TeachBasicPosZ As Decimal

        ''' <summary>
        ''' [教導][X基準座標，不設定同Aligment點]
        ''' </summary>
        ''' <remarks>陣列索引(0,0)</remarks>
        Public BasicPositionX As Decimal
        ''' <summary>
        ''' [教導][Y基準座標，不設定同Aligment點]
        ''' </summary>
        ''' <remarks>陣列索引(0,0)</remarks>
        Public BasicPositionY As Decimal
        ''' <summary>
        ''' [教導][Z基準座標，不設定同Aligment點]
        ''' </summary>
        ''' <remarks>陣列索引(0,0)</remarks>
        Public BasicPositionZ As Decimal

        ''' <summary>X軸上層定位點-本層點膠基準點偏移量</summary>
        ''' <remarks></remarks>
        Public ParentAlignBasicOffsetX As Decimal
        ''' <summary>Y軸上層定位點-本層點膠基準點偏移量</summary>
        ''' <remarks></remarks>
        Public ParentAlignBasicOffsetY As Decimal
        ''' <summary>Z軸上層定位點-本層點膠基準點偏移量</summary>
        ''' <remarks></remarks>
        Public ParentAlignBasicOffsetZ As Decimal

        ''' <summary>X軸本層定位點-本層點膠基準點偏移量</summary>
        ''' <remarks></remarks>
        Public AlignBasicOffsetX As Decimal
        ''' <summary>Y軸本層定位點-本層點膠基準點偏移量</summary>
        ''' <remarks></remarks>
        Public AlignBasicOffsetY As Decimal
        ''' <summary>Z軸本層定位點-本層點膠基準點偏移量</summary>
        ''' <remarks></remarks>
        Public AlignBasicOffsetZ As Decimal


        ''' <summary>X軸上層定位點-本層定位點</summary>
        ''' <remarks></remarks>
        Public ParentAlignAlignOffsetX As Decimal
        ''' <summary>Y軸上層定位點-本層定位點</summary>
        ''' <remarks></remarks>
        Public ParentAlignAlignOffsetY As Decimal
        ''' <summary>Z軸上層定位點-本層定位點</summary>
        ''' <remarks></remarks>
        Public ParentAlignAlignOffsetZ As Decimal

        ''' <summary>[生產]真正確認後基準點X</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosX As Decimal
        ''' <summary>[生產]真正確認後基準點Y</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosY As Decimal
        ''' <summary>[生產]真正確認後基準點Z</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosZ As Decimal
        ''' <summary>[生產]真正確認後基準點(角度)</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosC As Decimal

        ''' <summary>
        ''' 複製
        ''' </summary>
        Public Function Clone()
            Dim copyRCP As New CRecipeConveyorPos

            For Each temp In AlignmentData
                copyRCP.AlignmentData.Add(temp.Clone())
            Next

            For Each temp In SkipMarkData
                copyRCP.SkipMarkData.Add(temp.Clone())
            Next

            For Each temp In LaserData
                copyRCP.LaserData.Add(temp)
            Next

            copyRCP.TeachBasicPosX = TeachBasicPosX
            copyRCP.TeachBasicPosY = TeachBasicPosY
            copyRCP.TeachBasicPosZ = TeachBasicPosZ
            copyRCP.BasicPositionX = BasicPositionX
            copyRCP.BasicPositionY = BasicPositionY
            copyRCP.BasicPositionZ = BasicPositionZ
            copyRCP.ParentAlignBasicOffsetX = ParentAlignBasicOffsetX
            copyRCP.ParentAlignBasicOffsetY = ParentAlignBasicOffsetY
            copyRCP.ParentAlignBasicOffsetZ = ParentAlignBasicOffsetZ
            copyRCP.AlignBasicOffsetX = AlignBasicOffsetX
            copyRCP.AlignBasicOffsetY = AlignBasicOffsetY
            copyRCP.AlignBasicOffsetZ = AlignBasicOffsetZ
            copyRCP.ParentAlignAlignOffsetX = ParentAlignAlignOffsetX
            copyRCP.ParentAlignAlignOffsetY = ParentAlignAlignOffsetY
            copyRCP.ParentAlignAlignOffsetZ = ParentAlignAlignOffsetZ
            copyRCP.RealBasicPosX = RealBasicPosX
            copyRCP.RealBasicPosY = RealBasicPosY
            copyRCP.RealBasicPosZ = RealBasicPosZ
            copyRCP.RealBasicPosC = RealBasicPosC
            Return copyRCP
        End Function

        Public Function Load(ByVal fileName As String, ByVal section As String) As Boolean
            Dim strFileName As String = fileName
            Dim strSection As String = section

            
            Dim tempNumber As Integer
            'tempNumber = 3  'CInt(ReadIniString(strSection, "AligmentNumber", strFileName, 0))  目前固定為三個定位點

            tempNumber = 3 'CInt(ReadIniString(strSection, "AligmentNumber", strFileName, 3))
            'tempNumber = CInt(ReadIniString(strSection, "AligmentNumber", strFileName, 3))

            For mAlignID As Integer = 0 To tempNumber - 1
                Dim tempAligmentData As New AlignmentStructure 'Soni 2017.02.09 雙軌結構擴充
                For mLaneNo As Integer = 0 To 1
                    tempAligmentData.TeachPosX = Val(ReadIniString(strSection, "TeachAlignPosX" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.TeachPosY = Val(ReadIniString(strSection, "TeachAlignPosY" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.TeachPosZ = Val(ReadIniString(strSection, "TeachAlignPosZ" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.AlignPosX = Val(ReadIniString(strSection, "AligmentPosX" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.AlignPosY = Val(ReadIniString(strSection, "AligmentPosY" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.AlignPosZ = Val(ReadIniString(strSection, "AligmentPosZ" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.AlignScene = ReadIniString(strSection, "AligmentScene" & (mAlignID + 1).ToString(), strFileName, "")
                    tempAligmentData.AlignOffsetX = Val(ReadIniString(strSection, "AlignOffsetX" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.AlignOffsetY = Val(ReadIniString(strSection, "AlignOffsetY" & (mAlignID + 1).ToString(), strFileName, 0))
                    tempAligmentData.AlignRoation = Val(ReadIniString(strSection, "AlignRoation" & (mAlignID + 1).ToString(), strFileName, 0))
                    '[Note]:先把舊的Recipe的場景由"0"改成""
                    If tempAligmentData.AlignScene = "0" Then
                        tempAligmentData.AlignScene = ""
                    End If
                    'AddSceneNamelist(tempAligmentData.AlignScene) '儲存Recipe中所有有使用的場景
                Next

                AlignmentData.Add(tempAligmentData)
            Next
            'SkipMark

            Dim tmpSkipMarkData As New AlignmentStructure
            tmpSkipMarkData.TeachPosX = Val(ReadIniString(strSection, "TeachSkipMarkPosX", strFileName, 0))
            tmpSkipMarkData.TeachPosY = Val(ReadIniString(strSection, "TeachSkipMarkPosY", strFileName, 0))
            tmpSkipMarkData.TeachPosZ = Val(ReadIniString(strSection, "TeachSkipMarkPosZ", strFileName, 0))
            tmpSkipMarkData.AlignPosX = Val(ReadIniString(strSection, "SkipMarkPosX", strFileName, 0))
            tmpSkipMarkData.AlignPosY = Val(ReadIniString(strSection, "SkipMarkPosY", strFileName, 0))
            tmpSkipMarkData.AlignPosZ = Val(ReadIniString(strSection, "SkipMarkPosZ", strFileName, 0))
            tmpSkipMarkData.AlignScene = ReadIniString(strSection, "SkipMarkScene", strFileName, "")
            tmpSkipMarkData.AlignOffsetX = Val(ReadIniString(strSection, "SkipMarkOffsetX", strFileName, 0))
            tmpSkipMarkData.AlignOffsetY = Val(ReadIniString(strSection, "SkipMarkOffsetY", strFileName, 0))
            tmpSkipMarkData.AlignRoation = Val(ReadIniString(strSection, "SkipMarkRoation", strFileName, 0))
            SkipMarkData.Add(tmpSkipMarkData)


            tempNumber = CInt(ReadIniString(strSection, "LaserNumber", strFileName, 1))
            If tempNumber < 1 Then tempNumber = 1
            LaserData.Clear()
            For mLaserID As Integer = 0 To tempNumber - 1
                Dim tempLaserData As New LaserStructure
                tempLaserData.TeachPosX = Val(ReadIniString(strSection, "TeachLaserPosX" & (mLaserID + 1).ToString(), strFileName, 0))
                tempLaserData.TeachPosY = Val(ReadIniString(strSection, "TeachLaserPosY" & (mLaserID + 1).ToString(), strFileName, 0))
                tempLaserData.TeachPosZ = Val(ReadIniString(strSection, "TeachLaserPosZ" & (mLaserID + 1).ToString(), strFileName, 0))
                tempLaserData.LaserPositionX = Val(ReadIniString(strSection, "LaserPosX" & (mLaserID + 1).ToString(), strFileName, 0))
                tempLaserData.LaserPositionY = Val(ReadIniString(strSection, "LaserPosY" & (mLaserID + 1).ToString(), strFileName, 0))
                tempLaserData.LaserPositionZ = Val(ReadIniString(strSection, "LaserPosZ" & (mLaserID + 1).ToString(), strFileName, 0))
                LaserData.Add(tempLaserData)
            Next

            TeachBasicPosX = Val(ReadIniString(strSection, "TeachBasicPosX", strFileName, 0))
            TeachBasicPosY = Val(ReadIniString(strSection, "TeachBasicPosY", strFileName, 0))
            TeachBasicPosZ = Val(ReadIniString(strSection, "TeachBasicPosZ", strFileName, 0))
            BasicPositionX = Val(ReadIniString(strSection, "BasicPositionX", strFileName, 0))
            BasicPositionY = Val(ReadIniString(strSection, "BasicPositionY", strFileName, 0))
            BasicPositionZ = Val(ReadIniString(strSection, "BasicPositionZ", strFileName, 0))

            Return True
        End Function

        Public Function Save(ByVal fileName As String, ByVal section As String) As Boolean
            Dim strFileName As String = fileName
            Dim strSection As String = section
            
            'Call SaveIniString(strSection, "AligmentNumber", 3, strFileName) 'AlignmentData.Count.ToString(), strFileName)    目前固定三個定位點
            Call SaveIniString(strSection, "AligmentNumber", AlignmentData.Count.ToString(), strFileName) 'AlignmentData.Count.ToString(), strFileName)    目前固定三個定位點
            For mAlignID As Integer = 0 To AlignmentData.Count - 1
                Call SaveIniString(strSection, "TeachAlignPosX" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).TeachPosX.ToString(), strFileName)
                Call SaveIniString(strSection, "TeachAlignPosY" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).TeachPosY.ToString(), strFileName)
                Call SaveIniString(strSection, "TeachAlignPosZ" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).TeachPosZ.ToString(), strFileName)
                Call SaveIniString(strSection, "AligmentPosX" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignPosX.ToString(), strFileName)
                Call SaveIniString(strSection, "AligmentPosY" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignPosY.ToString(), strFileName)
                Call SaveIniString(strSection, "AligmentPosZ" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignPosZ.ToString(), strFileName)
                Call SaveIniString(strSection, "AlignOffsetX" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignOffsetX.ToString(), strFileName)
                Call SaveIniString(strSection, "AlignOffsetY" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignOffsetY.ToString(), strFileName)
                Call SaveIniString(strSection, "AlignRoation" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignRoation.ToString(), strFileName)
                Call SaveIniString(strSection, "AligmentScene" & (mAlignID + 1).ToString(), AlignmentData(mAlignID).AlignScene, strFileName)
                'AddSceneNamelist(AlignmentData(mAlignID).AlignScene)
            Next

            'SkipMark
            Call SaveIniString(strSection, "TeachSkipMarkPosX", SkipMarkData(0).TeachPosX.ToString(), strFileName)
            Call SaveIniString(strSection, "TeachSkipMarkPosY", SkipMarkData(0).TeachPosY.ToString(), strFileName)
            Call SaveIniString(strSection, "TeachSkipMarkPosZ", SkipMarkData(0).TeachPosZ.ToString(), strFileName)
            Call SaveIniString(strSection, "SkipMarkPosX", SkipMarkData(0).AlignPosX.ToString(), strFileName)
            Call SaveIniString(strSection, "SkipMarkPosY", SkipMarkData(0).AlignPosY.ToString(), strFileName)
            Call SaveIniString(strSection, "SkipMarkPosZ", SkipMarkData(0).AlignPosZ.ToString(), strFileName)
            Call SaveIniString(strSection, "SkipMarkScene", SkipMarkData(0).AlignScene, strFileName)
            Call SaveIniString(strSection, "SkipMarkOffsetX", SkipMarkData(0).AlignOffsetX.ToString(), strFileName)
            Call SaveIniString(strSection, "SkipMarkOffsetY", SkipMarkData(0).AlignOffsetY.ToString(), strFileName)
            Call SaveIniString(strSection, "SkipMarkRoation", SkipMarkData(0).AlignRoation.ToString(), strFileName)

            Call SaveIniString(strSection, "LaserNumber", LaserData.Count.ToString(), strFileName)
            For mLaserID As Integer = 0 To LaserData.Count - 1
                Call SaveIniString(strSection, "TeachLaserPosX" & (mLaserID + 1).ToString(), LaserData(mLaserID).TeachPosX.ToString(), strFileName)
                Call SaveIniString(strSection, "TeachLaserPosY" & (mLaserID + 1).ToString(), LaserData(mLaserID).TeachPosY.ToString(), strFileName)
                Call SaveIniString(strSection, "TeachLaserPosZ" & (mLaserID + 1).ToString(), LaserData(mLaserID).TeachPosZ.ToString(), strFileName)
                Call SaveIniString(strSection, "LaserPosX" & (mLaserID + 1).ToString(), LaserData(mLaserID).LaserPositionX.ToString(), strFileName)
                Call SaveIniString(strSection, "LaserPosY" & (mLaserID + 1).ToString(), LaserData(mLaserID).LaserPositionY.ToString(), strFileName)
                Call SaveIniString(strSection, "LaserPosZ" & (mLaserID + 1).ToString(), LaserData(mLaserID).LaserPositionZ.ToString(), strFileName)
            Next

            Call SaveIniString(strSection, "TeachBasicPosX", TeachBasicPosX.ToString(), strFileName)
            Call SaveIniString(strSection, "TeachBasicPosY", TeachBasicPosY.ToString(), strFileName)
            Call SaveIniString(strSection, "TeachBasicPosZ", TeachBasicPosZ.ToString(), strFileName)
            Call SaveIniString(strSection, "BasicPositionX", BasicPositionX.ToString(), strFileName)
            Call SaveIniString(strSection, "BasicPositionY", BasicPositionY.ToString(), strFileName)
            Call SaveIniString(strSection, "BasicPositionZ", BasicPositionZ.ToString(), strFileName)
            
            Return True
        End Function
       
    End Class

    ''' <summary>[Stage下Valve周邊的東西]</summary>
    ''' <remarks></remarks>
    Public Class CStageParts
        Public Function Clone() As CStageParts
            Return Me.MemberwiseClone()
        End Function
        ''' <summary>閥體名稱</summary>
        ''' <remarks></remarks>
        Public ValveName(enmValve.Max) As String
        ''' <summary>膠材名稱</summary>
        ''' <remarks></remarks>
        Public PasteName(enmValve.Max) As String
        ''' <summary>[秤重名稱]</summary>
        ''' <remarks></remarks>
        Public FlowRateName(enmValve.Max) As String
        ''' <summary>[Purge名稱]</summary>
        ''' <remarks></remarks>
        Public PurgeName(enmValve.Max) As String
        ''' <summary>每點平均重量(限噴射閥類型)</summary>
        ''' <remarks></remarks>
        Public AverageWeightPerDot(enmValve.Max) As Decimal
        ''' <summary>[出膠偏移Z]</summary>
        ''' <remarks></remarks>
        Public ValveShiftZ(enmValve.Max) As Decimal
        ''' <summary>[出膠偏移X] </summary>
        ''' <remarks></remarks>
        Public ValveShiftX(enmValve.Max) As Decimal
        ''' <summary>[出膠偏移Y] </summary>
        ''' <remarks></remarks>
        Public ValveShiftY(enmValve.Max) As Decimal
        ''' <summary>[膠管壓力]</summary>
        ''' <remarks></remarks>
        Public SyringePressure(enmValve.Max) As Decimal
        ''' <summary>[閥體壓力]</summary>
        ''' <remarks></remarks>
        Public ValvePressure(enmValve.Max) As Decimal
        ''' <summary>[閥體溫度(Heater)]</summary>
        ''' <remarks></remarks>
        Public NozzleTemperature(enmValve.Max) As Decimal
        ''' <summary>[閥體溫度(Piezo)]</summary>
        ''' <remarks></remarks>
        Public PiezoTemperature(enmStage.Max) As Decimal
    End Class



    ''' <summary>單一Pattern的Map,先採行原設計</summary>
    ''' <remarks></remarks>
    Public Class CRecipeNode
        ''' <summary>
        ''' [節點路徑，在一份Map中，各節點路徑是單獨且唯一的，不會重複]
        ''' </summary>
        ''' <remarks></remarks>
        Public NodePath As String
        ''' <summary>多層陣列/非陣列</summary>
        ''' <remarks></remarks>
        Public Array As New List(Of CRecipeNodeLevel) 'Soni / 2016.12.28 多層陣列設定
        ''' <summary>
        ''' [親代節點名稱]
        ''' </summary>
        ''' <remarks></remarks>
        Public ParentNode As String
        ''' <summary>子節點名稱</summary>
        ''' <remarks></remarks>
        Public ChildNodes As New List(Of String)
        ''' <summary>Pattern名稱</summary>
        ''' <remarks>擴充參數, 必要時可依此從gCRecipe.Pattern取得相關資料</remarks>
        Public PatternName As String
        ''' <summary>節點串接旗標</summary>
        ''' <remarks></remarks>
        Public IsNodeConnect As Boolean

        ''' <summary>對位方法</summary>
        ''' <remarks></remarks>
        Public AlignType As enmAlignType
        ''' <summary>
        ''' [定位啟用旗標]
        ''' </summary>
        ''' <remarks></remarks>
        Public AlignmentEnable As Boolean
        ''' <summary>
        ''' [SkipMark啟用旗標]
        ''' </summary>
        ''' <remarks></remarks>
        Public SkipMarkEnable As Boolean
        ''' <summary>
        ''' [雷射測高啟用旗標]
        ''' </summary>
        ''' <remarks></remarks>
        Public LaserEnable As Boolean

        ''' <summary>定位點教導,指定基準點,拍照點的索引X</summary>
        ''' <remarks>給索引(0,0)在陣列範圍外,或是(0,0)無法教導時使用</remarks>
        Public TeachIndexX As Integer = 0
        ''' <summary>定位點教導,指定基準點,拍照點的索引Y</summary>
        ''' <remarks></remarks>
        Public TeachIndexY As Integer = 0
        ''' <summary>
        ''' [雷射測高數量]
        ''' </summary>
        ''' <remarks></remarks>
        Public LaserNumber As Integer


        ''' <summary>雙軌位置參數</summary>
        ''' <remarks></remarks>
        Public ConveyorPos(1) As CRecipeConveyorPos




        ' ''' <summary>X軸基準偏移量(相對於上一層)</summary>
        ' ''' <remarks></remarks>
        'Public BasicOffsetX As Decimal
        ' ''' <summary>Y軸基準偏移量(相對於上一層)</summary>
        ' ''' <remarks></remarks>
        'Public BasicOffsetY As Decimal
        ' ''' <summary>Z軸基準偏移量(相對於上一層)</summary>
        ' ''' <remarks></remarks>
        'Public BasicOffsetZ As Decimal

        ''' <summary>
        ''' [確認是否與MappingData連結]
        ''' </summary>
        ''' <remarks></remarks>
        Public IsMapping As Boolean

        ''' <summary>
        ''' [在 Map 中 X 方向的起始位置]
        ''' </summary>
        ''' <remarks></remarks>
        Public NodeStartingX As Integer = 1
        ''' <summary>
        ''' [在 Map 中 Y 方向的起始位置]
        ''' </summary>
        ''' <remarks></remarks>
        Public NodeStartingY As Integer = 1
        ''' <summary>
        ''' [是否執行作業,預設為true]
        ''' </summary>
        ''' <remarks></remarks>
        Public Enable As Boolean = True

        ''' <summary>
        ''' [複製]
        ''' </summary>
        Public Function Clone() As CRecipeNode
            Dim nodeCopy As New CRecipeNode
            nodeCopy.NodePath = NodePath

            For Each ary In Array
                nodeCopy.Array.Add(ary.Clone())
            Next

            nodeCopy.ParentNode = ParentNode

            For Each temp In ChildNodes
                nodeCopy.ChildNodes.Add(temp)
            Next

            nodeCopy.PatternName = PatternName
            nodeCopy.IsNodeConnect = IsNodeConnect
            nodeCopy.AlignType = AlignType
            nodeCopy.AlignmentEnable = AlignmentEnable
            nodeCopy.SkipMarkEnable = SkipMarkEnable
            nodeCopy.LaserEnable = LaserEnable
            nodeCopy.TeachIndexX = TeachIndexX
            nodeCopy.TeachIndexY = TeachIndexY
            nodeCopy.LaserNumber = LaserNumber

            For i = 0 To ConveyorPos.Count - 1
                nodeCopy.ConveyorPos(i) = ConveyorPos(i).Clone()
            Next

            nodeCopy.IsMapping = IsMapping
            nodeCopy.NodeStartingX = NodeStartingX
            nodeCopy.NodeStartingY = NodeStartingY
            nodeCopy.Enable = Enable
            Return nodeCopy
        End Function

        ''' <summary>Constructor</summary>
        ''' <remarks></remarks>
        Public Sub New()
            For i As Integer = 0 To ConveyorPos.Count - 1
                ConveyorPos(i) = New CRecipeConveyorPos
            Next
        End Sub

    End Class

    ''' <summary>
    ''' 膠量補償
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SDispenseGlueAutoTuning
        ''' <summary>[出膠壓力]</summary>
        ''' <remarks></remarks>
        Public dblDispenserNo1PressureOffset As Decimal
        ''' <summary>[出膠壓力]</summary>
        ''' <remarks></remarks>
        Public dblDispenserNo2PressureOffset As Decimal
    End Structure
    ''' <summary>
    ''' 紀錄各站的處理狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SState
        ''' <summary>[點膠前影像辨識 and Laser Reader狀態]</summary>
        ''' <remarks></remarks>
        Public DieState As enmDieState
        ''' <summary>[點膠處理狀態]</summary>
        ''' <remarks></remarks>
        Public enmDispenserState As enmDispenserState
        ''' <summary>[點膠後的檢測狀態]</summary>
        ''' <remarks></remarks>
        Public enmDieDetectState As enmDieDetectState
        ''' <summary>[畫布須更新]</summary>
        ''' <remarks></remarks>
        Public NeedUpdate As Boolean
        ''' <summary>[晶粒處理完之狀態]</summary>
        ''' <remarks></remarks>
        Public enmResultState As enmResultState
        ''' <summary>[膠量覆蓋率]</summary>
        ''' <remarks></remarks>
        Public intGlueCoverRate As Integer
        ''' <summary>[AreaNo1膠量覆蓋率]</summary>
        ''' <remarks></remarks>
        Public intAreaNo1GlueCoverRate As Integer
        ''' <summary>[AreaNo2膠量覆蓋率]</summary>
        ''' <remarks></remarks>
        Public intAreaNo2GlueCoverRate As Integer
        ''' <summary>[AreaNo3膠量覆蓋率]</summary>
        ''' <remarks></remarks>
        Public intAreaNo3GlueCoverRate As Integer
        ''' <summary>[AreaNo4膠量覆蓋率]</summary>
        ''' <remarks></remarks>
        Public intAreaNo4GlueCoverRate As Integer
        ''' <summary>[影像定位時的相似度]</summary>
        ''' <remarks></remarks>
        Public intFixSimilar As Integer
        ''' <summary>[膠量檢測時的相似度]</summary>
        ''' <remarks></remarks>
        Public intGlueCheckSimilar As Integer
    End Structure

    ''' <summary>節點MAP</summary>
    ''' <remarks></remarks>
    Public Class CPatternMap
        ''' <summary>Pattern名稱</summary>
        ''' <remarks>擴充參數, 必要時可依此從gCRecipe.Pattern取得相關資料</remarks>
        Public PatternName As String

        ''' <summary>[紀錄Ccd補償量]</summary>
        ''' <remarks></remarks>
        Public SRecipePos(,) As SCcdOffsetPos
        ''' <summary>[紀錄Laser數值]</summary>
        ''' <remarks></remarks>
        Public SLaserValue(,) As SLaser
        ''' <summary>[記錄每一輪點膠狀態]</summary>
        ''' <remarks></remarks>
        Public Round As New List(Of CRoundMap)
        ''' <summary>[紀錄Scan Glue跑到第幾個(膠量檢查)]</summary>
        ''' <remarks></remarks>
        Public ScanGlueArray(,) As Boolean

        ''' <summary>[紀錄Chip在各站的狀態]</summary>
        ''' <remarks></remarks>
        Public ChipState(,) As SState
        ''' <summary>[膠量補償]</summary>
        ''' <remarks></remarks>
        Public SDispenseGlue(,) As SDispenseGlueAutoTuning
        ''' <summary>
        ''' [紀錄Die的MappingData]
        ''' </summary>
        ''' <remarks></remarks>
        Public SBinMapData(,) As BinMappingData

        ''' <summary>[對位方法]</summary>
        ''' <remarks></remarks>
        Public AlignType As enmAlignType

        'Eason 20170302 Ticket:100090 , System Update Crash [S]
        Public mmIsMapDataChange As Boolean = True 'False  'Toby Modify_20170513
        Private Property mIsMapDataChange As Boolean
            Set(value As Boolean)

                If (mmIsMapDataChange = value) Then
                    Exit Property
                End If

                SyncLock (mIsMapDataChangeSafeLock)
                    mmIsMapDataChange = value
                End SyncLock
            End Set
            Get
                Return mmIsMapDataChange
            End Get
        End Property
        Private mIsMapDataChangeSafeLock As Object = New Object()

        Public Function IsMapDataChange(Optional Refresh As Boolean = True) As Boolean
            Dim Result As Boolean = mIsMapDataChange

            For Each item In Round
                If (item.IsDispensingStatusChange = True) Then
                    If (Refresh) Then
                        item.IsDispensingStatusChange = False
                    End If
                    Result = True
                End If
            Next

            If (mIsMapDataChange = True) Then
                If (Refresh) Then
                    mIsMapDataChange = False
                End If
            End If

            Return Result
        End Function
        'Eason 20170302 Ticket:100090 , System Update Crash [E]

        '20170405_Add Toby test _Start
        Public Function SetCCDLaserStatus(x As Integer, y As Integer, value As eDieStatus)
            If (x > SBinMapData.GetUpperBound(0) OrElse y > SBinMapData.GetUpperBound(1)) Then
                Return False
            End If
            If (SBinMapData(x, y).Status <> value) Then
                SBinMapData(x, y).Status = value
                mIsMapDataChange = True
            End If
            Return True
        End Function
        '20170405_Add Toby test _End



        'Eason 20170302 Ticket:100090 , System Update Crash [S]
        Public Function Clone()
            Dim Buffer As CPatternMap = New CPatternMap()
            '[AlignType]
            Buffer.AlignType = Me.AlignType
            '[PatternName]
            Buffer.PatternName = Me.PatternName
            '[SRecipePos]
            If (Not IsNothing(Me.SRecipePos)) Then
                ReDim Buffer.SRecipePos(Me.SRecipePos.GetUpperBound(0), Me.SRecipePos.GetUpperBound(1))
                Array.Copy(Me.SRecipePos, Buffer.SRecipePos, Me.SRecipePos.Length)
            End If
            '[SLaserValue]
            If (Not IsNothing(Me.SLaserValue)) Then
                ReDim Buffer.SLaserValue(Me.SLaserValue.GetUpperBound(0), Me.SLaserValue.GetUpperBound(1))
                Array.Copy(Me.SLaserValue, Buffer.SLaserValue, Me.SLaserValue.Length)
            End If
            '[ChipState]
            If (Not IsNothing(Me.ChipState)) Then
                ReDim Buffer.ChipState(Me.ChipState.GetUpperBound(0), Me.ChipState.GetUpperBound(1))
                Array.Copy(Me.ChipState, Buffer.ChipState, Me.ChipState.Length)
            End If
            '[SBinMapData]
            If (Not IsNothing(Me.SBinMapData)) Then
                ReDim Buffer.SBinMapData(Me.SBinMapData.GetUpperBound(0), Me.SBinMapData.GetUpperBound(1))
                Array.Copy(Me.SBinMapData, Buffer.SBinMapData, Me.SBinMapData.Length)
            End If
            '[ScanGlueArray]
            If (Not IsNothing(Me.ScanGlueArray)) Then
                ReDim Buffer.ScanGlueArray(Me.ScanGlueArray.GetUpperBound(0), Me.ScanGlueArray.GetUpperBound(1))
                Array.Copy(Me.ScanGlueArray, Buffer.ScanGlueArray, Me.ScanGlueArray.Length)
            End If
            '[SDispenseGlue]
            If (Not IsNothing(Me.SDispenseGlue)) Then
                ReDim Buffer.SDispenseGlue(Me.SDispenseGlue.GetUpperBound(0), Me.SDispenseGlue.GetUpperBound(1))
                Array.Copy(Me.SDispenseGlue, Buffer.SDispenseGlue, Me.SDispenseGlue.Length)
            End If
            '[Round]
            Buffer.Round.Clear()
            For Each item In Me.Round
                Buffer.Round.Add(item.Clone())
            Next

            Return Buffer

        End Function
        'Eason 20170302 Ticket:100090 , System Update Crash [E]

    End Class

#Region "掃描排序類別"        'Fenix+ 2016/01/09
    Public Class ScanList
        ''' <summary>
        ''' [取像排序 (同層Node先完成後，才會進入下一層)]
        ''' </summary>
        ''' <remarks></remarks>
        Public List As New List(Of sLevelIndexCollection)
        ''' <summary>
        ''' [紀錄掃描點位排序目前執行順序]
        ''' </summary>
        ''' <remarks></remarks>
        Public NowIndex As Integer

        Public Sub Clear()
            List.Clear()
            NowIndex = 0
        End Sub
    End Class
#End Region

    ''' <summary>運行用ID</summary>
    ''' <remarks></remarks>
    Public Class RunID
        ''' <summary>Pattern ID</summary>
        ''' <remarks></remarks>
        Public PatternID As String
        ''' <summary>節點ID</summary>
        ''' <remarks></remarks>
        Public NodeID As String
        Public Sub New(nodeID As String, ByVal patternID As String)
            Me.PatternID = patternID
            Me.NodeID = nodeID
        End Sub
    End Class

    ' ''' <summary>節點運行清單</summary>
    ' ''' <remarks></remarks>
    'Public NodeIDRunList(enmStage.Max) As List(Of String)
    ''' <summary>各平台Map資料結構</summary>
    ''' <remarks></remarks>
    Public gStageMap(enmStage.Max) As CStageMap

    ''' <summary>掃描順序清單</summary>
    ''' <remarks></remarks>
    Public gNextScan(enmStage.Max) As ScanList  'Fenix+ 2016/01/09
    ''' <summary>生產用Recipe</summary>
    ''' <remarks></remarks>
    Public gCRecipe As CRecipe
    ''' <summary>編輯用Recipe
    ''' </summary>
    ''' <remarks></remarks>
    Public gRecipeEdit As CRecipe

    ''' <summary>膠材資料庫</summary>
    ''' <remarks></remarks>
    Public gPasteDataBase As New Dictionary(Of String, CPasteParameter)
    ''' <summary>噴射閥資料庫</summary>
    ''' <remarks></remarks>
    Public gJetValveDB As New Dictionary(Of String, CJetValveParameter)
    ''' <summary>螺桿閥資料庫</summary>
    ''' <remarks></remarks>
    Public gAugerValveDB As New Dictionary(Of String, CAugerValveParameter)
    ''' <summary>[Purge資料庫]</summary>
    ''' <remarks></remarks>
    Public gPurgeDB As New Dictionary(Of String, CPurgeParameter)
    ''' <summary>[秤重資料庫]</summary>
    ''' <remarks></remarks>
    Public gFlowRateDB As New Dictionary(Of String, CFlowRateParameter)
    ''' <summary>溫度資料庫</summary>
    ''' <remarks></remarks>
    Public gTempDB As New Dictionary(Of String, CTempParameter)

    ''' <summary>Dot 資料庫</summary>  'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
    ''' <remarks></remarks>
    Public gDotValueDB As New Dictionary(Of String, CDotTypeParameter)

    ''' <summary>Line 資料庫</summary>  'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
    ''' <remarks></remarks>
    Public gLineValueDB As New Dictionary(Of String, CLineTypeParameter)

    ''' <summary>Arc 資料庫</summary>  'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
    ''' <remarks></remarks>
    ''' Note: 目前規劃 Arc , Circle , Arc3D , Circle3D 都用歸納在弧
    Public gArcValueDB As New Dictionary(Of String, CArcTypeParameter)

    ''' <summary>初始化資料結構</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial_StageMap() As Boolean
        'Dim mStageMax As Integer = GetStageMax()
        For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gStageMap(i) = New CStageMap
            gNextScan(i) = New ScanList     'Fenix+ 2016/01/09
        Next

        Return True
    End Function

   

    ''' <summary>資料庫初始化</summary>
    ''' <remarks></remarks>
    Public Sub InitDataBase() 'Soni / 2017.04.26
        Dim folderPath As String
        folderPath = Application.StartupPath & "\Database\" '資料夾確保
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        '=== 膠材資料 ===
        folderPath = Application.StartupPath & "\Database\Paste\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gPasteDataBase.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("膠材資料載入: " & fileName)
            Dim temp As New CPasteParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gPasteDataBase.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gPasteDataBase.Add(temp.Name, temp)
            End If
        Next
        If Not gPasteDataBase.ContainsKey("Default") Then
            gPasteDataBase.Add("Default", New CPasteParameter("Default"))
            gPasteDataBase("Default").Save(folderPath & "Default.pst")
        End If
        '=== 膠材資料 ===


        '=== 噴射閥資料 ===
        'folderPath = Application.StartupPath & "\Database\JetValve\"
        'If Not System.IO.Directory.Exists(folderPath) Then
        '    System.IO.Directory.CreateDirectory(folderPath)
        'End If
        'gJetValveDB.Clear()
        'For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
        '    Debug.Print("噴射閥資料載入: " & fileName)
        '    Dim temp As New CJetValveParameter
        '    temp.Load(fileName)
        '    gJetValveDB.Add(temp.Name, temp)
        'Next
        '=========================================================================================
        gJetValveDB.Clear()
        folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If

        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("PicoTouch資料載入: " & fileName)
            Dim temp As New CJetValveParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gJetValveDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gJetValveDB.Add(temp.Name, temp)
            End If
           
        Next

        folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("Advanjet資料載入: " & fileName)
            Dim temp As New CJetValveParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gJetValveDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gJetValveDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gJetValveDB.ContainsKey("Default") Then
            gJetValveDB.Add("Default", New CJetValveParameter("Default"))
            gJetValveDB("Default").Save(folderPath & "Default.pst")
        End If
        '=== 噴射閥資料 ===

        '=== 螺桿閥資料 ===
        folderPath = Application.StartupPath & "\Database\AugerValve\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gAugerValveDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("螺桿閥資料載入: " & fileName)
            Dim temp As New CAugerValveParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gAugerValveDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gAugerValveDB.Add(temp.Name, temp)
            End If
           
        Next

        folderPath = Application.StartupPath & "\Database\Purge\" 'Purge資料
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gPurgeDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("Purge資料載入: " & fileName)
            Dim temp As New CPurgeParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gPurgeDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gPurgeDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gPurgeDB.ContainsKey("Default") Then
            gPurgeDB.Add("Default", New CPurgeParameter("Default"))
            gPurgeDB("Default").Save(folderPath & "Default.pdb")
        End If

        '=== 溫控資料 ====
        folderPath = Application.StartupPath & "\Database\Temperature\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gTempDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("溫控資料載入: " & fileName)
            Dim temp As New CTempParameter(System.IO.Path.GetFileNameWithoutExtension(fileName)) 'Soni / 2017.04.26 下拉
            If Not gTempDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gTempDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gTempDB.ContainsKey("Default") Then
            gTempDB.Add("Default", New CTempParameter("Default"))
            gTempDB("Default").Save(folderPath & "Default.tdb")
        End If
        '=== 溫控資料 ====


        '=== 秤重資料 ===
        folderPath = Application.StartupPath & "\Database\Weight\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gFlowRateDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("秤重資料載入: " & fileName)
            Dim temp As New CFlowRateParameter("Default")
            If Not gFlowRateDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gFlowRateDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gFlowRateDB.ContainsKey("Default") Then
            gFlowRateDB.Add("Default", New CFlowRateParameter("Default"))
            gFlowRateDB("Default").Save(folderPath & "Default.wdb")
        End If
        '=== 秤重資料 ===

        '=== DOT資料 ==== 
        'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
        folderPath = Application.StartupPath & "\Database\DotValue\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gDotValueDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("DOT資料載入: " & fileName)
            Dim temp As New CDotTypeParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gDotValueDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gDotValueDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gDotValueDB.ContainsKey("Default") Then
            gDotValueDB.Add("Default", New CDotTypeParameter("Default"))
            gDotValueDB("Default").Save(folderPath & "Default.ddb")
        End If
        '=== DOT資料 ====
        '=== LINE資料 ====
        folderPath = Application.StartupPath & "\Database\LineValue\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gLineValueDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("LINE資料載入: " & fileName)
            Dim temp As New CLineTypeParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gLineValueDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gLineValueDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gLineValueDB.ContainsKey("Default") Then
            gLineValueDB.Add("Default", New CLineTypeParameter("Default"))
            gLineValueDB("Default").Save(folderPath & "Default.ldb")
        End If
        'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]

        'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
        '=== Arc資料 ====
        folderPath = Application.StartupPath & "\Database\ArcValue\"
        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If
        gArcValueDB.Clear()
        For Each fileName As String In System.IO.Directory.GetFiles(folderPath)
            Debug.Print("Arc資料載入: " & fileName)
            Dim temp As New CArcTypeParameter(System.IO.Path.GetFileNameWithoutExtension(fileName))
            If Not gArcValueDB.ContainsKey(temp.Name) Then 'Soni + 2017.04.26 避免有人丟錯同短名,副檔名不同的檔案進去噴掉
                temp.Load(fileName)
                gArcValueDB.Add(temp.Name, temp)
            End If
            
        Next
        If Not gArcValueDB.ContainsKey("Default") Then
            gArcValueDB.Add("Default", New CArcTypeParameter("Default"))
            gArcValueDB("Default").Save(folderPath & "Default.ldb")
        End If
        'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]

    End Sub


End Module

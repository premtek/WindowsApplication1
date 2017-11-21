''' <summary>
''' "CCD閥 XY校正"
''' </summary>
''' <remarks></remarks>
Public Structure SCCDTiltValveCalibration
    ''' <summary>
    ''' [平台內校正第n組閥的CCD拍照定位位置]
    ''' </summary> 
    ''' <remarks></remarks>
    Public CCDCalibPosX As Decimal
    ''' <summary>
    ''' [平台內校正第n組閥的CCD拍照定位位置]
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDCalibPosY As Decimal
    ''' <summary>
    ''' [平台內校正第n組閥的CCD拍照高度]
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDCalibPosZ As Decimal
    ''' <summary>
    ''' [平台內校正第n組閥的點膠實際位置X]
    ''' </summary>
    ''' <remarks></remarks>
    Public ValveCalibPosX As Decimal
    ''' <summary>
    ''' [平台內校正第n組閥的點膠實際位置Y]
    ''' </summary>
    ''' <remarks></remarks>
    Public ValveCalibPosY As Decimal
    ''' <summary>
    ''' [平台內校正第n組閥的點膠實際高度Z]
    ''' </summary>
    ''' <remarks></remarks>
    Public ValveCalibPosZ As Decimal

    ''' <summary>
    ''' [點膠閥至最低點上移多少至點膠位置]
    ''' </summary>
    ''' <remarks></remarks>
    Public ValvePinZHight As Decimal

    ' ''' <summary>是否以CCD跑點膠/CCD對閥修正量為0</summary>
    ' ''' <remarks></remarks>
    'Public IsVideoRun As Boolean 'Soni + 2016.09.20 VideoRun計算條件

    ''' <summary>自動校正影像計算結果X</summary>
    ''' <remarks></remarks>
    Public AutoCalibResultX As Decimal
    ''' <summary>自動校正影像計算結果Y</summary>
    ''' <remarks></remarks>
    Public AutoCalibResultY As Decimal


    ''' <summary>[CCD校正場景名稱]</summary>
    ''' <remarks></remarks>
    Public CCDValveCalibrationSceneName As String
    ''' <summary>[CCD Valve 校正的次數]</summary>
    ''' <remarks></remarks>
    Public CCDValveCalibrationCount As Integer
    ''' <summary>[CCD Valve 校正後之容許誤差]</summary>
    ''' <remarks></remarks>
    Public CCDValveCalibrationThreshold As Decimal
    ''' <summary>自動打點間隔</summary>
    ''' <remarks></remarks>
    Public Pitch As Decimal

    ''' <summary>氣壓量</summary>
    ''' <remarks></remarks>
    Public AirPressure As Decimal

    '20170520
    ''' <summary>CCD穩定時間</summary>
    ''' <remarks></remarks>
    Public CCDStableTime As Decimal


End Structure
Public Class CCCDTiltValveCalibration
    Private mDicCCDTiltValveCalib(enmValve.Max) As Dictionary(Of Decimal, SCCDTiltValveCalibration)
    Public Property DicCCDTiltValveCalib(enmValve As enmValve) As Dictionary(Of Decimal, SCCDTiltValveCalibration)
        Get
            Return mDicCCDTiltValveCalib(enmValve)
        End Get
        Set(ByVal value As Dictionary(Of Decimal, SCCDTiltValveCalibration))
            mDicCCDTiltValveCalib(enmValve.Max) = value
        End Set
    End Property
    Private bViedoRun As Boolean
    Public Property IsVideoRun() As String
        Get
            Return bViedoRun
        End Get
        Set(ByVal value As String)
            bViedoRun = value
        End Set
    End Property


    ' ''' <summary>
    ' ''' CCD 與 Valve
    ' ''' </summary>
    ' ''' <remarks></remarks>
    '  Public DicCCDTiltValveCalib(enmValve.Max) As Dictionary(Of Decimal, SCCDTiltValveCalibration)
    Public Sub New()
        For index = 0 To enmValve.Max
            mDicCCDTiltValveCalib(index) = New Dictionary(Of Decimal, SCCDTiltValveCalibration)
        Next
    End Sub
    ''' <summary>
    ''' 加入新項目
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <param name="angle"></param>
    ''' <param name="sCCDTiltValve"></param>
    ''' <remarks></remarks>
    Public Sub AddCCDTiltCCDValve(eValve As enmValve, angle As Decimal, sCCDTiltValve As SCCDTiltValveCalibration)
        If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) = False Then
            mDicCCDTiltValveCalib(eValve).Add(angle, sCCDTiltValve)
        Else
            mDicCCDTiltValveCalib(eValve).Item(angle) = sCCDTiltValve
        End If
    End Sub
    ''' <summary>
    ''' 移除項目
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <param name="angle"></param>
    ''' <remarks></remarks>
    Public Sub ReMoveCCDTiltValve(eValve As enmValve, angle As Decimal) ', sLaserTiltvalve As SCCDTiltValveCalibration)
        If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) = True Then
            mDicCCDTiltValveCalib(eValve).Remove(angle)
        End If
    End Sub
    ''' <summary>
    ''' 取得對應閥裡的所有key值
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllKeyName(ByVal eValve As enmValve) As List(Of Decimal)
        Dim lstKeyName As List(Of Decimal) = New List(Of Decimal)
        For Each pair As KeyValuePair(Of Decimal, SCCDTiltValveCalibration) In mDicCCDTiltValveCalib(eValve)
            lstKeyName.Add(pair.Key)
        Next
        Return lstKeyName
    End Function

#Region "CCD 與 Valve Offset"
    ''' <summary>
    ''' [平台內CCD與閥偏移量X]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CCDTiltValveOffsetX(eValve As enmValve, angle As Decimal)
        Get
            If IsVideoRun = True Then 'Soni + 2016.09.20 VideoRun判定條件
                Return 0
            End If
            Dim TiltTemp As New SCCDTiltValveCalibration
            TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Return TiltTemp.CCDCalibPosX - TiltTemp.ValveCalibPosX
        End Get
    End Property
    ''' <summary>
    ''' [平台內CCD與閥偏移量Y]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CCDTiltValveOffsetY(eValve As enmValve, angle As Decimal)
        Get
            If IsVideoRun = True Then 'Soni + 2016.09.20 VideoRun判定條件
                Return 0
            End If
            Dim TiltTemp As New SCCDTiltValveCalibration
            TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Return TiltTemp.CCDCalibPosY - TiltTemp.ValveCalibPosY
        End Get
    End Property
    ''' <summary>
    ''' [平台內CCD與閥偏移量Z]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CCDTiltValveOffsetZ(eValve As enmValve, angle As Decimal)
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Return TiltTemp.CCDCalibPosZ - TiltTemp.ValveCalibPosZ
        End Get
    End Property
#End Region

    Public Property ValveX(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.ValveCalibPosX
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.ValveCalibPosX = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property ValveY(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If

            Return TiltTemp.ValveCalibPosY
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.ValveCalibPosY = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property ValveZ(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If

            Return TiltTemp.ValveCalibPosZ
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.ValveCalibPosZ = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set

    End Property
    Public Property CCDX(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If

            Return TiltTemp.CCDCalibPosX
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDCalibPosX = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set

    End Property
    Public Property CCDY(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If

            Return TiltTemp.CCDCalibPosY
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDCalibPosY = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set

    End Property
    Public Property CCDZ(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.CCDCalibPosZ
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDCalibPosZ = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property ValvePinZHight(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.ValvePinZHight
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.ValvePinZHight = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property

    'Public Property IsVideoRun(eValve As enmValve, angle As Decimal) As Boolean
    '    Get
    '        Dim TiltTemp As New SCCDTiltValveCalibration
    '        If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
    '            TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
    '        Else
    '            Return 0
    '        End If
    '        Return TiltTemp.IsVideoRun
    '    End Get
    '    Set(value As Boolean)
    '        Dim TiltTemp As New SCCDTiltValveCalibration
    '        If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
    '            TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
    '            TiltTemp.IsVideoRun = value
    '            ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
    '        End If
    '    End Set
    'End Property

    Public Property AutoResultX(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.AutoCalibResultX
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.AutoCalibResultX = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property AutoResultY(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.AutoCalibResultY
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.AutoCalibResultY = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property decCCDValveCalibrationSceneName(eValve As enmValve, angle As Decimal) As String
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.CCDValveCalibrationSceneName
        End Get
        Set(value As String)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDValveCalibrationSceneName = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property iCCDValveCount(eValve As enmValve, angle As Decimal) As Integer
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.CCDValveCalibrationCount
        End Get
        Set(value As Integer)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDValveCalibrationCount = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property
    Public Property dCCDValveThreadshold(eValve As enmValve, angle As Decimal) As String
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.CCDValveCalibrationThreshold
        End Get
        Set(value As String)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDValveCalibrationThreshold = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property


    'AirPressure
    '20161122
    Public Property decAirPressure(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.AirPressure
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.AirPressure = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property



    Public Property decPitch(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.Pitch
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.Pitch = value
                ADDCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property

    '20170520
    'CCDStableTime
    Public Property decCCDStableTime(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.CCDStableTime
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SCCDTiltValveCalibration
            If mDicCCDTiltValveCalib(eValve).ContainsKey(angle) Then
                TiltTemp = mDicCCDTiltValveCalib(eValve).Item(angle)
                TiltTemp.CCDStableTime = value
                AddCCDTiltCCDValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property



    ''' <summary>儲存校正檔</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub SaveCalibrationValve(ByVal fileName As String)
        Try
            Dim strSection As String
            Dim iCount As Integer = 0
            'Dim sCountSection As String = "AngleCount"

            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                For Each pair As KeyValuePair(Of Decimal, SCCDTiltValveCalibration) In mDicCCDTiltValveCalib(mValveNo)
                    strSection = "CCDValve" & (mValveNo + 1).ToString & "_TiltWorkAngle_" & pair.Key.ToString
                    Dim sTemp1 As SCCDTiltValveCalibration = pair.Value
                    Call SaveIniString(strSection, "ValveCalibPosX_" & iCount.ToString, sTemp1.ValveCalibPosX, fileName)
                    Call SaveIniString(strSection, "ValveCalibPosY_" & iCount.ToString, sTemp1.ValveCalibPosY, fileName)
                    Call SaveIniString(strSection, "ValveCalibPosZ_" & iCount.ToString, sTemp1.ValveCalibPosZ, fileName)
                    Call SaveIniString(strSection, "CCDCalibPosX_" & iCount.ToString, sTemp1.CCDCalibPosX, fileName)
                    Call SaveIniString(strSection, "CCDCalibPosY_" & iCount.ToString, sTemp1.CCDCalibPosY, fileName)
                    Call SaveIniString(strSection, "CCDCalibPosZ_" & iCount.ToString, sTemp1.CCDCalibPosZ, fileName)
                    Call SaveIniString(strSection, "CCDValveCalibrationSceneName_" & iCount.ToString, sTemp1.CCDValveCalibrationSceneName, fileName)
                    Call SaveIniString(strSection, "CCDValveCalibrationCount_" & iCount.ToString, sTemp1.CCDValveCalibrationCount, fileName)
                    Call SaveIniString(strSection, "CCDValveCalibrationThreshold_" & iCount.ToString, sTemp1.CCDValveCalibrationThreshold, fileName)
                    Call SaveIniString(strSection, "AirPressure_" & iCount.ToString, sTemp1.AirPressure, fileName)
                    Call SaveIniString(strSection, "Pitch_" & iCount.ToString, sTemp1.Pitch, fileName)

                    '20170520
                    Call SaveIniString(strSection, "CCDStableTime_" & iCount.ToString, sTemp1.CCDStableTime, fileName)

                    Call SaveIniString("CCDValve" & (mValveNo + 1).ToString & "_Key", "KeyName" & iCount.ToString, pair.Key.ToString, fileName)
                    iCount = iCount + 1
                Next
                Call SaveIniString("CCDValve" & (mValveNo + 1).ToString & "_Key", "KeyCount", iCount, fileName)
                iCount = 0
            Next


        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002029), "Error_1002029", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002029) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    ''' <summary>讀取校正檔-平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Load(ByVal fileName As String)

        For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
            mDicCCDTiltValveCalib(mValveNo).Clear()
        Next

        Try
            Dim sTemp1 As SCCDTiltValveCalibration = New SCCDTiltValveCalibration
            Dim kTemp As String = ""
            Dim iTemp As Integer

            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                sTemp1 = New SCCDTiltValveCalibration
                iTemp = ReadIniString("CCDValve" & mValveNo + 1 & "_Key", "KeyCount", fileName, 1)
                For index = 0 To iTemp - 1
                    kTemp = ReadIniString("CCDValve" & (mValveNo + 1).ToString & "_Key", "KeyName" & index.ToString, fileName, 0)
                    sTemp1.ValveCalibPosX = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValveCalibPosX_" & index.ToString, fileName, 0)
                    sTemp1.ValveCalibPosY = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValveCalibPosY_" & index.ToString, fileName, 0)
                    sTemp1.ValveCalibPosZ = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValveCalibPosZ_" & index.ToString, fileName, 0)
                    sTemp1.CCDCalibPosX = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDCalibPosX_" & index.ToString, fileName, 0)
                    sTemp1.CCDCalibPosY = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDCalibPosY_" & index.ToString, fileName, 0)
                    sTemp1.CCDCalibPosZ = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDCalibPosZ_" & index.ToString, fileName, 0)
                    sTemp1.CCDValveCalibrationSceneName = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDValveCalibrationSceneName_" & index.ToString, fileName, 0)
                    sTemp1.CCDValveCalibrationCount = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDValveCalibrationCount_" & index.ToString, fileName, 0)
                    sTemp1.CCDValveCalibrationThreshold = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDValveCalibrationThreshold_" & index.ToString, fileName, 5)
                    sTemp1.AirPressure = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "AirPressure_" & index.ToString, fileName, 0)
                    sTemp1.Pitch = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "Pitch_" & index.ToString, fileName, 0)

                    '20170520
                    sTemp1.CCDStableTime = ReadIniString("CCDValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "CCDStableTime_" & index.ToString, fileName, 1200)

                    mDicCCDTiltValveCalib(mValveNo).Add(Convert.ToDecimal(kTemp), sTemp1)
                Next
            Next

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
End Class

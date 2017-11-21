''' <summary>
''' Tilt Z Height高度校正
''' </summary>
''' <remarks></remarks>
Public Structure SLaserTiltValveCalibration

    ''' <summary>[閥頭測Pin高位置X]</summary>
    ''' <remarks></remarks>
    Public ValvePinPosX As Decimal
    ''' <summary>[閥頭測Pin高位置Y]</summary>
    ''' <remarks></remarks>
    Public ValvePinPosY As Decimal
    ''' <summary>[閥頭測Pin高位置Z]</summary>
    ''' <remarks></remarks>
    Public ValvePinPosZ As Decimal
    ''' <summary>[第一支膠針高度(相對於Latch)之測高找尋上限值]</summary>
    ''' <remarks></remarks>
    Public ValvePinLimitZ As Decimal
    ''' <summary>雷射測Pin高位置X</summary>
    ''' <remarks></remarks>
    Public LaserPinPosX As Decimal
    ''' <summary>雷射測Pin高位置Y</summary>
    ''' <remarks></remarks>
    Public LaserPinPosY As Decimal
    ''' <summary>雷射測Pin高位置Z</summary>
    ''' <remarks></remarks>
    Public LaserPinPosZ As Decimal
    ''' <summary>雷射測Pin高讀值</summary>
    ''' <remarks></remarks>
    Public LaserPinValue As Decimal

End Structure
Public Class CLaserTiltValveCalibration
    Private mDicLaserTiltValve(enmValve.Max) As Dictionary(Of Decimal, SLaserTiltValveCalibration)
    Public ReadOnly Property DicLaserTiltValve(enmValve As enmValve) As Dictionary(Of Decimal, SLaserTiltValveCalibration)
        Get
            Return mDicLaserTiltValve(enmValve)
        End Get
        'Set(ByVal value As Dictionary(Of Decimal, SLaserTiltValveCalibration))
        '    mDicLaserTiltValve = value
        'End Set
    End Property

    'Public DicLaserTiltValve(enmValve.Max) As Dictionary(Of Decimal, SLaserTiltValveCalibration)
    Public Sub New()
        For index = 0 To enmValve.Max
            mDicLaserTiltValve(index) = New Dictionary(Of Decimal, SLaserTiltValveCalibration)

        Next
    End Sub
    ''' <summary>
    ''' 加入新項目
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <param name="angle"></param>
    ''' <param name="sLaserTiltvalve"></param>
    ''' <remarks></remarks>
    Public Sub AddLaserTiltValve(eValve As enmValve, angle As Decimal, sLaserTiltvalve As SLaserTiltValveCalibration)
        If mDicLaserTiltValve(eValve).ContainsKey(angle) = False Then
            mDicLaserTiltValve(eValve).Add(angle, sLaserTiltvalve)
        Else
            mDicLaserTiltValve(eValve).Item(angle) = sLaserTiltvalve
        End If
    End Sub
    ''' <summary>
    ''' 移除項目
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <param name="angle"></param>
    ''' <remarks></remarks>
    Public Sub ReMoveLaserTiltValve(eValve As enmValve, angle As Decimal) ', sLaserTiltvalve As SLaserTiltValveCalibration)
        If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
            mDicLaserTiltValve(eValve).Remove(angle)
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
        For Each pair As KeyValuePair(Of Decimal, SLaserTiltValveCalibration) In mDicLaserTiltValve(eValve)
            lstKeyName.Add(pair.Key)
        Next
        Return lstKeyName
    End Function
#Region "Laser 與 Vavle Offset"
    ''' <summary> 
    ''' [平台內Laser與閥頭修正量Z:(LaserPinPosZ-LaserPinValue)-ValvePinPosZ]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LaserTiltValveOffsetZ(eValve As enmValve, angle As Decimal)
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            'Return TiltTemp.LaserPinPosZ - TiltTemp.ValvePinLimitZ

            '[Note]:(LaserPinPosZ-LaserPinValue)-ValvePinPosZ
            Return (TiltTemp.LaserPinPosZ - TiltTemp.LaserPinValue) - TiltTemp.ValvePinPosZ


        End Get
    End Property
#End Region




    Public Property ValvePinX(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.ValvePinPosX
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosY = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)
            End If

        End Set
    End Property

    Public Property ValvePinY(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.ValvePinPosY
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosY = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)
            End If

        End Set
    End Property

    Public Property ValvePinZ(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.ValvePinPosZ
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosZ = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)

            End If

        End Set
    End Property

    Public Property LaserPinX(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.LaserPinPosX
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosZ = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)
            End If
        End Set
    End Property

    Public Property LaserPinY(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.LaserPinPosY
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosZ = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)
            End If
        End Set

    End Property

    Public Property LaserPinZ(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.LaserPinPosZ
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosZ = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)
            End If
        End Set

    End Property

    Public Property LaserPinLimitZ(eValve As enmValve, angle As Decimal) As Decimal
        Get
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
            Else
                Return 0
            End If
            Return TiltTemp.ValvePinLimitZ
        End Get
        Set(value As Decimal)
            Dim TiltTemp As New SLaserTiltValveCalibration
            If mDicLaserTiltValve(eValve).ContainsKey(angle) = True Then
                TiltTemp = mDicLaserTiltValve(eValve).Item(angle)
                TiltTemp.ValvePinPosZ = value
                ADDLaserTiltValve(eValve, angle, TiltTemp)
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

            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                For Each pair As KeyValuePair(Of Decimal, SLaserTiltValveCalibration) In mDicLaserTiltValve(mValveNo)
                    strSection = "LaserValve" & (mValveNo + 1).ToString & "_TiltWorkAngle_" & pair.Key.ToString
                    Dim sTemp1 As SLaserTiltValveCalibration = pair.Value
                    Call SaveIniString(strSection, "ValvePinPosX_" & iCount.ToString, sTemp1.ValvePinPosX, fileName)
                    Call SaveIniString(strSection, "ValvePinPosY_" & iCount.ToString, sTemp1.ValvePinPosY, fileName)
                    Call SaveIniString(strSection, "ValvePinPosZ_" & iCount.ToString, sTemp1.ValvePinPosZ, fileName)
                    Call SaveIniString(strSection, "ValvePinLimitZ_" & iCount.ToString, sTemp1.ValvePinLimitZ, fileName)
                    Call SaveIniString(strSection, "LaserPinPosX_" & iCount.ToString, sTemp1.LaserPinPosX, fileName)
                    Call SaveIniString(strSection, "LaserPinPosY_" & iCount.ToString, sTemp1.LaserPinPosY, fileName)
                    Call SaveIniString(strSection, "LaserPinPosZ_" & iCount.ToString, sTemp1.LaserPinPosZ, fileName)
                    Call SaveIniString(strSection, "LaserPinValue_" & iCount.ToString, sTemp1.LaserPinValue, fileName)
                    Call SaveIniString("LaserValve" & (mValveNo + 1).ToString & "_Key", "KeyName" & iCount.ToString, pair.Key.ToString, fileName)
                    iCount = iCount + 1
                Next
                Call SaveIniString("LaserValve" & (mValveNo + 1).ToString & "_Key", "KeyCount", iCount, fileName)
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
            mDicLaserTiltValve(mValveNo).Clear()
        Next

        Try
            Dim sTemp1 As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
            Dim kTemp As String = ""
            Dim iTemp As Integer

            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                sTemp1 = New SLaserTiltValveCalibration
                iTemp = ReadIniString("LaserValve" & mValveNo + 1 & "_Key", "KeyCount", fileName, 1)
                For index = 0 To iTemp - 1
                    kTemp = ReadIniString("LaserValve" & (mValveNo + 1).ToString & "_Key", "KeyName" & index.ToString, fileName, 0)
                    sTemp1.ValvePinPosX = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValvePinPosX_" & index.ToString, fileName, 0)
                    sTemp1.ValvePinPosY = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValvePinPosY_" & index.ToString, fileName, 0)
                    sTemp1.ValvePinPosZ = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValvePinPosZ_" & index.ToString, fileName, 0)
                    sTemp1.ValvePinLimitZ = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "ValvePinLimitZ_" & index.ToString, fileName, 0)
                    sTemp1.LaserPinPosX = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "LaserPinPosX_" & index.ToString, fileName, 0)
                    sTemp1.LaserPinPosY = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "LaserPinPosY_" & index.ToString, fileName, 0)
                    sTemp1.LaserPinPosZ = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "LaserPinPosZ_" & index.ToString, fileName, 0)
                    sTemp1.LaserPinValue = ReadIniString("LaserValve" & mValveNo + 1 & "_TiltWorkAngle_" & kTemp, "LaserPinValue_" & index.ToString, fileName, 0)
                    mDicLaserTiltValve(mValveNo).Add(Convert.ToDecimal(kTemp), sTemp1)
                Next
            Next

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub


End Class

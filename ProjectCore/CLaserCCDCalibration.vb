''' <summary>
''' "Laser CCD 校正"
''' </summary>
''' <remarks></remarks>
Public Structure SLserCCDCalibration
    ''' <summary>CCD測平台校正X(for Laser)</summary>
    ''' <remarks></remarks>
    Public CCDCalibPosX As Decimal
    ''' <summary>CCD測平台校正Y(for Laser)</summary>
    ''' <remarks></remarks>
    Public CCDCalibPosY As Decimal
    ''' <summary>CCD測平台校正Z(for Laser)</summary>
    ''' <remarks></remarks>
    Public CCDCalibPosZ As Decimal
    ''' <summary>[雷射測平台X]</summary>
    ''' <remarks></remarks>
    Public LaserCalibPosX As Decimal
    ''' <summary>[雷射測平台Y]</summary>
    ''' <remarks></remarks>
    Public LaserCalibPosY As Decimal
    ''' <summary>[雷射測平台Z]</summary>
    ''' <remarks></remarks>
    Public LaserCalibPosZ As Decimal
End Structure

Public Class CLaserCCDCalibration
    Private mDicLaserCCD(enmValve.Max) As Dictionary(Of Decimal, SLserCCDCalibration)
    Public ReadOnly Property DicLaserCCD(enmValve As enmValve) As Dictionary(Of Decimal, SLserCCDCalibration)
        Get
            Return mDicLaserCCD(enmValve)
        End Get
        'Set(ByVal value)
        '    mDicLaserCCD(enmValve.Max) = value
        'End Set
    End Property

    'Public DicLaserCCD(enmValve.Max) As Dictionary(Of Decimal, SLserCCDCalibration)

    ''' <summary>
    ''' [平台內CCD與 Laser修正量X:(CCDCalibPosY-LaserCalibPosY]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LaserCCDOffsetX(eValve As enmValve, angle As Decimal)
        Get
            Dim TiltTemp As New SLserCCDCalibration
            TiltTemp = mDicLaserCCD(eValve).Item(angle)
            Return TiltTemp.CCDCalibPosX - TiltTemp.LaserCalibPosX
        End Get
    End Property

    ''' <summary>
    ''' [平台內CCD與 Laser修正量Y:(CCDCalibPosY-LaserCalibPosY]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LaserCCDOffsetY(eValve As enmValve, angle As Decimal)
        Get
            Dim TiltTemp As New SLserCCDCalibration
            TiltTemp = mDicLaserCCD(eValve).Item(angle)
            Return TiltTemp.CCDCalibPosY - TiltTemp.LaserCalibPosY
        End Get
    End Property


    ''' <summary>
    ''' [平台內CCD與 Laser修正量Z:(CCDCalibPosZ-LaserCalibPosZ)-ValvePinPosZ] 重點是在新竹可能
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LaserCCDOffsetZ(eValve As enmValve, angle As Decimal)
        Get
            Dim TiltTemp As New SLserCCDCalibration
            TiltTemp = mDicLaserCCD(eValve).Item(angle)
            Return TiltTemp.CCDCalibPosZ - TiltTemp.LaserCalibPosZ
        End Get
    End Property
    Public Sub New()
        For index = 0 To enmValve.Max
            mDicLaserCCD(index) = New Dictionary(Of Decimal, SLserCCDCalibration)
        Next
    End Sub
    ''' <summary>
    ''' 加入新項目
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <param name="angle"></param>
    ''' <param name="sLaserCCD"></param>
    ''' <remarks></remarks>
    Public Sub ADDLaserCCD(eValve As enmValve, angle As Decimal, sLaserCCD As SLserCCDCalibration)
        If mDicLaserCCD(eValve).ContainsKey(angle) = False Then
            mDicLaserCCD(eValve).Add(angle, sLaserCCD)
        Else
            mDicLaserCCD(eValve).Item(angle) = sLaserCCD
        End If
    End Sub

    ''' <summary>
    ''' 移除項目
    ''' </summary>
    ''' <param name="eValve"></param>
    ''' <param name="angle"></param>
    ''' <param name="sLaserTiltvalve"></param>
    ''' <remarks></remarks>
    Public Sub ReMoveLaserCCD(eValve As enmValve, angle As Decimal, sLaserTiltvalve As SLserCCDCalibration)
        If mDicLaserCCD(eValve).ContainsKey(angle) = True Then
            mDicLaserCCD(eValve).Remove(angle)
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
        For Each pair As KeyValuePair(Of Decimal, SLserCCDCalibration) In mDicLaserCCD(eValve)
            lstKeyName.Add(pair.Key)
        Next
        Return lstKeyName
    End Function

    ''' <summary>儲存校正檔</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            Dim strSection As String
            Dim iCount As Integer = 0

            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                For Each pair As KeyValuePair(Of Decimal, SLserCCDCalibration) In mDicLaserCCD(mValveNo)
                    strSection = "Valve" & (mValveNo + 1).ToString & "_LaserCCD_" & pair.Key.ToString
                    Dim sTemp1 As SLserCCDCalibration = pair.Value
                    Call SaveIniString(strSection, "CCDCalibPosX_" & iCount.ToString, sTemp1.CCDCalibPosX, fileName)
                    Call SaveIniString(strSection, "CCDCalibPosY_" & iCount.ToString, sTemp1.CCDCalibPosY, fileName)
                    Call SaveIniString(strSection, "CCDCalibPosZ_" & iCount.ToString, sTemp1.CCDCalibPosZ, fileName)
                    Call SaveIniString(strSection, "LaserCalibPosX_" & iCount.ToString, sTemp1.LaserCalibPosX, fileName)
                    Call SaveIniString(strSection, "LaserCalibPosY_" & iCount.ToString, sTemp1.LaserCalibPosY, fileName)
                    Call SaveIniString(strSection, "LaserCalibPosZ_" & iCount.ToString, sTemp1.LaserCalibPosZ, fileName)
                    Call SaveIniString("Valve" & mValveNo.ToString + 1 & "_Key", "KeyName" & iCount.ToString,pair.Key.ToString, fileName)
                    iCount = iCount + 1
                Next
                Call SaveIniString("Valve" & mValveNo.ToString + 1 & "_Key", "KeyCount", iCount, fileName)
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
            mDicLaserCCD(mValveNo).Clear()
        Next

        Try
            Dim sTemp1 As SLserCCDCalibration = New SLserCCDCalibration
            Dim kTemp As String = ""
            Dim iTemp As Integer

            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                sTemp1 = New SLserCCDCalibration
                iTemp = ReadIniString("Valve" & mValveNo + 1 & "_Key", "KeyCount", fileName, 0)

                For index = 0 To iTemp - 1
                    kTemp = ReadIniString("Valve1_Key", "KeyName" & index.ToString, fileName, 0)
                    sTemp1.CCDCalibPosX = ReadIniString("Valve" & mValveNo + 1 & "_LaserCCD_" & kTemp, "CCDCalibPosX" & index.ToString, fileName, 0)
                    sTemp1.CCDCalibPosY = ReadIniString("Valve" & mValveNo + 1 & "_LaserCCD_" & kTemp, "CCDCalibPosY" & index.ToString, fileName, 0)
                    sTemp1.CCDCalibPosZ = ReadIniString("Valve" & mValveNo + 1 & "_LaserCCD_" & kTemp, "CCDCalibPosZ" & index.ToString, fileName, 0)
                    sTemp1.LaserCalibPosX = ReadIniString("Valve" & mValveNo + 1 & "_LaserCCD_" & kTemp, "LaserCalibPosX" & index.ToString, fileName, 0)
                    sTemp1.LaserCalibPosY = ReadIniString("Valve" & mValveNo + 1 & "_LaserCCD_" & kTemp, "LaserCalibPosY" & index.ToString, fileName, 0)
                    sTemp1.LaserCalibPosZ = ReadIniString("Valve" & mValveNo + 1 & "_LaserCCD_" & kTemp, "LaserCalibPosZ" & index.ToString, fileName, 0)
                    mDicLaserCCD(mValveNo).Add(Convert.ToDecimal(kTemp), sTemp1)
                Next
            Next

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End Try
    End Sub
End Class

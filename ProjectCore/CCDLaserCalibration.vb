''' <summary>CCD對雷射校正</summary>
''' <remarks></remarks>
Public Class CCDLaserCalibration

#Region "CCD Laser XY校正"
    ''' <summary>CCD測平台校正X(for Laser)</summary>
    ''' <remarks></remarks>
    Public CCDCalibPosX(enmValve.Max) As Decimal
    ''' <summary>CCD測平台校正Y(for Laser)</summary>
    ''' <remarks></remarks>
    Public CCDCalibPosY(enmValve.Max) As Decimal
    ''' <summary>CCD測平台校正Z(for Laser)</summary>
    ''' <remarks></remarks>
    Public CCDCalibPosZ(enmValve.Max) As Decimal

    ''' <summary>[雷射測平台X]</summary>
    ''' <remarks></remarks>
    Public LaserCalibPosX(enmValve.Max) As Decimal
    ''' <summary>[雷射測平台Y]</summary>
    ''' <remarks></remarks>
    Public LaserCalibPosY(enmValve.Max) As Decimal
    ''' <summary>[雷射測平台Z]</summary>
    ''' <remarks></remarks>
    Public LaserCalibPosZ(enmValve.Max) As Decimal

    ''' <summary>
    ''' [平台內CCD與雷射偏移量X]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CCDLaserOffsetX(ByVal valveNo As enmValve) As Decimal
        Get
            Return CCDCalibPosX(valveNo) - LaserCalibPosX(valveNo)
        End Get
    End Property

    ''' <summary>
    ''' [平台內CCD與雷射偏移量Y]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CCDLaserOffsetY(ByVal valveNo As enmValve) As Decimal
        Get
            Return CCDCalibPosY(valveNo) - LaserCalibPosY(valveNo)
        End Get
    End Property

    ''' <summary>
    ''' [平台內CCD與雷射偏移量Y]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CCDLaserOffsetZ(ByVal valveNo As enmValve) As Decimal
        Get
            Return CCDCalibPosZ(valveNo) - LaserCalibPosZ(valveNo)
        End Get
    End Property

#End Region

    ''' <summary>儲存校正檔-平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            With Me
                Dim strSection As String
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "LaserCalibPos" & (mValveNo + 1).ToString & "X", .LaserCalibPosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "LaserCalibPos" & (mValveNo + 1).ToString & "Y", .LaserCalibPosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "LaserCalibPos" & (mValveNo + 1).ToString & "Z", .LaserCalibPosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "CCDLaserCalibPos" & (mValveNo + 1).ToString & "X", .CCDCalibPosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "CCDLaserCalibPos" & (mValveNo + 1).ToString & "Y", .CCDCalibPosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "CCDLaserCalibPos" & (mValveNo + 1).ToString & "Z", .CCDCalibPosZ(mValveNo), fileName)
                Next
            End With

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
        Try
            Dim strSection As String

            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    .LaserCalibPosX(mValveNo) = Val(ReadIniString(strSection, "LaserCalibPos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .LaserCalibPosY(mValveNo) = Val(ReadIniString(strSection, "LaserCalibPos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .LaserCalibPosZ(mValveNo) = Val(ReadIniString(strSection, "LaserCalibPos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDCalibPosX(mValveNo) = Val(ReadIniString(strSection, "CCDLaserCalibPos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDCalibPosY(mValveNo) = Val(ReadIniString(strSection, "CCDLaserCalibPos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDCalibPosZ(mValveNo) = Val(ReadIniString(strSection, "CCDLaserCalibPos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

End Class

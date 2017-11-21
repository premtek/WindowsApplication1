''' <summary>[Purge位置校正]</summary>
''' <remarks></remarks>
Public Class CPurgeCalibration
    ''' <summary>CCD在Purge位置</summary>
    ''' <remarks></remarks>
    Public CCDPosX(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge位置</summary>
    ''' <remarks></remarks>
    Public CCDPosY(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge位置</summary>
    ''' <remarks></remarks>
    Public CCDPosZ(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點1位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign1PosX(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點1位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign1PosY(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點1位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign1PosZ(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點2位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign2PosX(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點2位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign2PosY(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點2位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign2PosZ(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點3位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign3PosX(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點3位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign3PosY(enmValve.Max) As Decimal
    ''' <summary>CCD在Purge特徵定位點3位置</summary>
    ''' <remarks></remarks>
    Public CCDAlign3PosZ(enmValve.Max) As Decimal
    ''' <summary>[閥件Purge位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosX(enmValve.Max) As Decimal
    ''' <summary>[閥件Purge位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosY(enmValve.Max) As Decimal
    ''' <summary>[閥件Purge位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosZ(enmValve.Max) As Decimal
    ''' <summary>[閥件Purge位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosB(enmValve.Max) As Decimal
    ''' <summary>[閥件Purge位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosC(enmValve.Max) As Decimal

    ''' <summary>儲存校正檔-平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            With Me
                Dim strSection As String
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "PurgeCalibration_CCDPos" & (mValveNo + 1).ToString & "X", .CCDPosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDPos" & (mValveNo + 1).ToString & "Y", .CCDPosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDPos" & (mValveNo + 1).ToString & "Z", .CCDPosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "X", .CCDAlign1PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Y", .CCDAlign1PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Z", .CCDAlign1PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "X", .CCDAlign2PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Y", .CCDAlign2PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Z", .CCDAlign2PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "X", .CCDAlign3PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Y", .CCDAlign3PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Z", .CCDAlign3PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "X", .ValvePosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "Y", .ValvePosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "Z", .ValvePosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "B", .ValvePosB(mValveNo), fileName)
                    Call SaveIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "C", .ValvePosC(mValveNo), fileName)
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
                    .CCDPosX(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDPos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDPosY(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDPos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDPosZ(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDPos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDAlign1PosX(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDAlign1PosY(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDAlign1PosZ(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDAlign2PosX(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDAlign2PosY(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDAlign2PosZ(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDAlign3PosX(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDAlign3PosY(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDAlign3PosZ(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .ValvePosX(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .ValvePosY(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .ValvePosZ(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .ValvePosB(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "B", fileName, 0))
                    .ValvePosC(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "C", fileName, 0))
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
End Class

''' <summary>[秤重位置校正]</summary>
''' <remarks></remarks>
Public Class CWeightCalibration
    ''' <summary>[CCD在秤重位置]</summary>
    ''' <remarks></remarks>
    Public CCDPosX(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重位置]</summary>
    ''' <remarks></remarks>
    Public CCDPosY(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重位置]</summary>
    ''' <remarks></remarks>
    Public CCDPosZ(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點1位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign1PosX(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點1位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign1PosY(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點1位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign1PosZ(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點2位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign2PosX(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點2位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign2PosY(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點2位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign2PosZ(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點3位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign3PosX(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點3位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign3PosY(enmValve.Max) As Decimal
    ''' <summary>[CCD在秤重特徵定位點3位置]</summary>
    ''' <remarks></remarks>
    Public CCDAlign3PosZ(enmValve.Max) As Decimal
    ''' <summary>[閥秤重位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosX(enmValve.Max) As Decimal
    ''' <summary>[閥秤重位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosY(enmValve.Max) As Decimal
    ''' <summary>[閥秤重位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosZ(enmValve.Max) As Decimal
    ''' <summary>[閥秤重位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosB(enmValve.Max) As Decimal
    ''' <summary>[閥秤重位置]</summary>
    ''' <remarks></remarks>
    Public ValvePosC(enmValve.Max) As Decimal
    ''' <summary>[秤重的安全位置]</summary>
    ''' <remarks></remarks>
    Public SafePosX(enmValve.Max) As Double
    ''' <summary>[秤重的安全位置]</summary>
    ''' <remarks></remarks>
    Public SafePosY(enmValve.Max) As Double
    ''' <summary>[秤重的安全位置]</summary>
    ''' <remarks></remarks>
    Public SafePosZ(enmValve.Max) As Double
    ''' <summary>[秤重的安全位置]</summary>
    ''' <remarks></remarks>
    Public SafePosB(enmValve.Max) As Double
    ''' <summary>[秤重的安全位置]</summary>
    ''' <remarks></remarks>
    Public SafePosC(enmValve.Max) As Double

    ''' <summary>儲存校正檔-平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            Dim strSection As String
            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "WeightCalibration_CCDPos" & (mValveNo + 1).ToString & "X", .CCDPosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDPos" & (mValveNo + 1).ToString & "Y", .CCDPosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDPos" & (mValveNo + 1).ToString & "Z", .CCDPosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "X", .CCDAlign1PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Y", .CCDAlign1PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Z", .CCDAlign1PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "X", .CCDAlign2PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Y", .CCDAlign2PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Z", .CCDAlign2PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "X", .CCDAlign3PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Y", .CCDAlign3PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Z", .CCDAlign3PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "X", .ValvePosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "Y", .ValvePosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "Z", .ValvePosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "B", .ValvePosB(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "C", .ValvePosC(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "X", .SafePosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "Y", .SafePosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "Z", .SafePosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "B", .SafePosB(mValveNo), fileName)
                    Call SaveIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "C", .SafePosC(mValveNo), fileName)
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
                    .CCDPosX(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDPos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDPosY(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDPos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDPosZ(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDPos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDAlign1PosX(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDAlign1PosY(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDAlign1PosZ(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign1Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDAlign2PosX(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDAlign2PosY(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDAlign2PosZ(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign2Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .CCDAlign3PosX(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .CCDAlign3PosY(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .CCDAlign3PosZ(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_CCDAlign3Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .ValvePosX(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .ValvePosY(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .ValvePosZ(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .ValvePosB(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "B", fileName, 0))
                    .ValvePosC(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_ValvePos" & (mValveNo + 1).ToString & "C", fileName, 0))
                    .SafePosX(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .SafePosY(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .SafePosZ(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .SafePosB(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "B", fileName, 0))
                    .SafePosC(mValveNo) = Val(ReadIniString(strSection, "WeightCalibration_SafePos" & (mValveNo + 1).ToString & "C", fileName, 0))
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
End Class

''' <summary>[閃避安全區位置]</summary>
''' <remarks></remarks>
Public Class CSafeRegion
    ''' <summary>[X Axis Pos]</summary>
    ''' <remarks></remarks>
    Public PosX(enmValve.Max) As Decimal
    ''' <summary>[Y Axis Pos]</summary>
    ''' <remarks></remarks>
    Public PosY(enmValve.Max) As Decimal
    ''' <summary>[Z Axis Pos]</summary>
    ''' <remarks></remarks>
    Public PosZ(enmValve.Max) As Decimal
    ''' <summary>[B Axis Pos]</summary>
    ''' <remarks></remarks>
    Public PosB(enmValve.Max) As Decimal
    ''' <summary>[C Axis Pos]</summary>
    ''' <remarks></remarks>
    Public PosC(enmValve.Max) As Decimal

    ''' <summary>[儲存安全區座標]</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            Dim strSection As String
            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "X", .PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "Y", .PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "Z", .PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "B", .PosB(mValveNo), fileName)
                    Call SaveIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "C", .PosC(mValveNo), fileName)
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002029), "Error_1002029", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002029) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
    ''' <summary>[儲存安全區座標]</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Load(ByVal fileName As String)
        Try
            Dim strSection As String
            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    .PosX(mValveNo) = Val(ReadIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .PosY(mValveNo) = Val(ReadIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .PosZ(mValveNo) = Val(ReadIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .PosB(mValveNo) = Val(ReadIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "B", fileName, 0))
                    .PosC(mValveNo) = Val(ReadIniString(strSection, "SafeRegion_Pos" & (mValveNo + 1).ToString & "C", fileName, 0))
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
End Class

''' <summary>[換膠管位置校正]</summary>
''' <remarks></remarks>
Public Class CChangePotCalibration
    ''' <summary>[換膠的位置] </summary>
    ''' <remarks></remarks>
    Public PosX(enmValve.Max) As Decimal
    ''' <summary>[換膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosY(enmValve.Max) As Decimal
    ''' <summary>[換膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosZ(enmValve.Max) As Decimal
    ''' <summary>[換膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosB(enmValve.Max) As Decimal
    ''' <summary>[換膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosC(enmValve.Max) As Decimal

    ''' <summary>儲存校正檔-平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            Dim strSection As String
            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "X", .PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Y", .PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Z", .PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "B", .PosB(mValveNo), fileName)
                    Call SaveIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "C", .PosC(mValveNo), fileName)
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
                'For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                '    strSection = "Valve" & (mValveNo + 1).ToString
                '    .PosX(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                '    .PosY(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                '    .PosZ(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                '    .PosB(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                '    .PosC(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                'Next
                'jimmy add 20170426

                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    .PosX(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .PosY(mValveNo) = Val(ReadIniString(strSection, "PurgeCalibration_ValvePos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .PosZ(mValveNo) = 0
                    .PosB(mValveNo) = 0
                    .PosC(mValveNo) = 0
                Next

            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
    'Public Sub Load(ByVal fileName As String)
    '    Try
    '        Dim strSection As String
    '        With Me
    '            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
    '                strSection = "Valve" & (mValveNo + 1).ToString
    '                .PosX(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
    '                .PosY(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
    '                .PosZ(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
    '                .PosB(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
    '                .PosC(mValveNo) = Val(ReadIniString(strSection, "ChangePotCalibration_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
    '            Next
    '        End With

    '    Catch ex As Exception
    '        gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
    '        gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
    '        MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message)
    '    End Try
    'End Sub
End Class

''' <summary>[擦拭閥頭位置]</summary>
''' <remarks></remarks>
Public Class CCleanValveCalibration
    ''' <summary>[閥擦拭清膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosX(enmValve.Max) As Decimal
    ''' <summary>[閥擦拭清膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosY(enmValve.Max) As Decimal
    ''' <summary>[閥擦拭清膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosZ(enmValve.Max) As Decimal
    ''' <summary>[閥擦拭清膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosB(enmValve.Max) As Decimal
    ''' <summary>[閥擦拭清膠的位置]</summary>
    ''' <remarks></remarks>
    Public PosC(enmValve.Max) As Decimal

    ''' <summary>儲存校正檔-平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            Dim strSection As String
            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "X", .PosX(mValveNo), fileName)
                    Call SaveIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "Y", .PosY(mValveNo), fileName)
                    Call SaveIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "Z", .PosZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "B", .PosB(mValveNo), fileName)
                    Call SaveIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "C", .PosC(mValveNo), fileName)
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
                    .PosX(mValveNo) = Val(ReadIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .PosY(mValveNo) = Val(ReadIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                    .PosZ(mValveNo) = Val(ReadIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "Z", fileName, 0))
                    .PosB(mValveNo) = Val(ReadIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "X", fileName, 0))
                    .PosC(mValveNo) = Val(ReadIniString(strSection, "CleanValveCalibration_Pos" & (mValveNo + 1).ToString & "Y", fileName, 0))
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
End Class
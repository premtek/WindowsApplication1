''' <summary>
''' 平台精準度驗證
''' </summary>
''' <remarks></remarks>
Public Class CStageVerification
#Region "平台精準度驗證 三點位置紀錄"
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照起始位置X]
    ''' </summary> 
    ''' <remarks></remarks>
    Public StageStartPosX As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照起始位置Y]
    ''' </summary> 
    ''' <remarks></remarks>
    Public StageStartPosY As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照起始位置Z]
    ''' </summary> 
    ''' <remarks></remarks>
    Public StageStartPosZ As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照轉角位置X]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageCornerPosX As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照轉角位置Y]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageCornerPosY As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照轉角位置Z]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageCornerPosZ As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照結束位置X]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageEndPosX As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照結束位置Y]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageEndPosY As Decimal
    ''' <summary>
    ''' [平台驗證第n組的CCD拍照結束位置Z]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageEndPosZ As Decimal
    ''' <summary>
    ''' [平台驗補償檔案名稱]
    ''' </summary>
    ''' <remarks></remarks>
    Public StageVerificationData As String
#End Region


#Region "平台精準度驗證 陣列數量及角度"
    ''' <summary>
    ''' [平台精準度驗證的陣列X數量]
    ''' </summary> 
    ''' <remarks></remarks>
    Public ArrayXCount As Decimal
    ''' <summary>
    ''' [平台精準度驗證的陣列Y數量]
    ''' </summary> 
    ''' <remarks></remarks>
    Public ArrayYCount As Decimal
    ''' <summary>
    ''' [平台精準度驗證水平方向的X偏移量]
    ''' </summary> 
    ''' <remarks></remarks>
    Public PitchHX As Decimal
    ''' <summary>
    ''' [平台精準度驗證水平方向的Y偏移量]
    ''' </summary> 
    ''' <remarks></remarks>
    Public PitchHY As Decimal
    ''' <summary>
    ''' [平台精準度驗證垂直方向的X偏移量]
    ''' </summary> 
    ''' <remarks></remarks>
    Public PitchVX As Decimal
    ''' <summary>
    ''' [平台精準度驗證垂直方向的Y偏移量]
    ''' </summary> 
    ''' <remarks></remarks>
    Public PitchVY As Decimal
    ''' <summary>
    ''' [平台精準度驗證的旋轉角度]
    ''' </summary> 
    ''' <remarks></remarks>
    Public StageVerificationAngle As Decimal
    ''' <summary>
    ''' 整定時間
    ''' </summary>
    ''' <remarks></remarks>
    Public SteadyTime As Integer
#End Region


    ''' <summary>儲存驗證檔-平台內精準度驗證</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub SaveParameter(ByVal fileName As String)
        Try
            With Me
                Dim strSection As String
                'For i As Integer = 0 To enmValve.Max
                strSection = "StageVerification"
                Call SaveIniString(strSection, "StageStartPosX", .StageStartPosX, fileName)
                Call SaveIniString(strSection, "StageStartPosY", .StageStartPosY, fileName)
                Call SaveIniString(strSection, "StageStartPosZ", .StageStartPosZ, fileName)

                Call SaveIniString(strSection, "StageCornerPosX", .StageCornerPosX, fileName)
                Call SaveIniString(strSection, "StageCornerPosY", .StageCornerPosY, fileName)
                Call SaveIniString(strSection, "StageCornerPosZ", .StageCornerPosZ, fileName)

                Call SaveIniString(strSection, "StageEndPosX", .StageEndPosX, fileName)
                Call SaveIniString(strSection, "StageEndPosY", .StageEndPosY, fileName)
                Call SaveIniString(strSection, "StageEndPosZ", .StageEndPosZ, fileName)
                'Next
                Call SaveIniString(strSection, "ArrayXCount", .ArrayXCount, fileName)
                Call SaveIniString(strSection, "ArrayYCount", .ArrayYCount, fileName)
                Call SaveIniString(strSection, "PitchHX", .PitchHX, fileName)
                Call SaveIniString(strSection, "PitchHY", .PitchHY, fileName)
                Call SaveIniString(strSection, "PitchVX", .PitchVX, fileName)
                Call SaveIniString(strSection, "PitchVY", .PitchVY, fileName)
                Call SaveIniString(strSection, "StageVerificationAngle", .StageVerificationAngle, fileName)
                Call SaveIniString(strSection, "SteadyTime", .SteadyTime, fileName)
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002029), "Error_1002029", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002029) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    ''' <summary>讀取驗證檔-平台內精準度驗證</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Load(ByVal fileName As String)
        Try
            Dim strSection As String

            With Me
                'For i As Integer = 0 to gSSystemParameter.StageCount
                strSection = "StageVerification"
                '.CCDValveCalibrationSceneName(i) = ReadIniString(strSection, "Scene" & (i + 1).ToString & "Name", fileName, 0)

                .StageStartPosX = Val(ReadIniString(strSection, "StageStartPosX", fileName, 0))  '驗機起始位置
                .StageStartPosY = Val(ReadIniString(strSection, "StageStartPosY", fileName, 0))
                .StageStartPosZ = Val(ReadIniString(strSection, "StageStartPosZ", fileName, 0))

                .StageCornerPosX = Val(ReadIniString(strSection, "StageCornerPosX", fileName, 0)) '驗機轉角位置
                .StageCornerPosY = Val(ReadIniString(strSection, "StageCornerPosY", fileName, 0))
                .StageCornerPosZ = Val(ReadIniString(strSection, "StageCornerPosZ", fileName, 0))

                .StageEndPosX = Val(ReadIniString(strSection, "StageEndPosX", fileName, 0)) '驗機轉角位置
                .StageEndPosY = Val(ReadIniString(strSection, "StageEndPosY", fileName, 0))
                .StageEndPosZ = Val(ReadIniString(strSection, "StageEndPosZ", fileName, 0))

                .ArrayXCount = Val(ReadIniString(strSection, "ArrayXCount", fileName, 1)) '陣列紀錄
                .ArrayYCount = Val(ReadIniString(strSection, "ArrayYCount", fileName, 1))
                .PitchHX = Val(ReadIniString(strSection, "PitchHX", fileName, 0))
                .PitchHY = Val(ReadIniString(strSection, "PitchHY", fileName, 0))
                .PitchVX = Val(ReadIniString(strSection, "PitchVX", fileName, 0))
                .PitchVY = Val(ReadIniString(strSection, "PitchVY", fileName, 0))
                .StageVerificationAngle = Val(ReadIniString(strSection, "StageVerificationAngle", fileName, 0))
                .SteadyTime = Val(ReadIniString(strSection, "SteadyTime", fileName, 100))
                'Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub









End Class

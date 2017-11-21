Imports ProjectCore

Public Class CLightCards
    Public ReadOnly Property Count As Integer
        Get
            Return Parameters.Count
        End Get
    End Property
    Default Public Property Items(ByVal index As Integer) As sLightConnectionParameter
        Get
            Return Parameters(index)
        End Get
        Set(value As sLightConnectionParameter)
            Parameters(index) = value
        End Set
    End Property
    ''' <summary>光控器連線設定 (0):第一組(1):第二組 (2):第三組 (3):第四組</summary>
    ''' <remarks></remarks>
    Public Parameters(enmCCD.ConstMax) As sLightConnectionParameter
    Public Function SaveLightControlParameter(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            Dim mCardCount As Integer = Parameters.Count
            strSection = "LightControl"
            SaveIniString(strSection, "CardCount", mCardCount, fileName) '數量

            For mChNo As Integer = 0 To Parameters.Count - 1
                strSection = "LightControl" & (mChNo + 1).ToString
                With Parameters(mChNo)
                    SaveIniString(strSection, "PortName", .PortName, fileName)
                    SaveIniString(strSection, "BaudRate", .BaudRate, fileName)
                    SaveIniString(strSection, "DataBits", .DataBit, fileName)
                    SaveIniString(strSection, "ProgramLightType", CInt(.CardType), fileName)
                    SaveIniString(strSection, "Ch1MaxValue", CInt(.ChannelMaxValue1), fileName) '20160625
                    SaveIniString(strSection, "Ch2MaxValue", CInt(.ChannelMaxValue2), fileName)
                    SaveIniString(strSection, "Ch3MaxValue", CInt(.ChannelMaxValue3), fileName)
                    SaveIniString(strSection, "Ch4MaxValue", CInt(.ChannelMaxValue4), fileName)
                    SaveIniString(strSection, "Unit", CInt(.Unit), fileName)
                    SaveIniString(strSection, "ChannelScale1", CDecimal(.ChannelScale1), fileName)
                    SaveIniString(strSection, "ChannelScale2", CDecimal(.ChannelScale2), fileName)
                    SaveIniString(strSection, "ChannelScale3", CDecimal(.ChannelScale3), fileName)
                    SaveIniString(strSection, "ChannelScale4", CDecimal(.ChannelScale4), fileName)
                End With
            Next
            Return True
        Catch ex As Exception
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            Return False
        End Try

    End Function

    Public Function LoadLightControlParameter(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "LightControl"
            Dim mCardCount As Integer = Val(ReadIniString(strSection, "CardCount", fileName, Parameters.Count))

            For mChNo As Integer = 0 To mCardCount - 1
                strSection = "LightControl" & (mChNo + 1).ToString
                With Parameters(mChNo)
                    .PortName = ReadIniString(strSection, "PortName", fileName, "COM1")
                    .BaudRate = CInt(ReadIniString(strSection, "BaudRate", fileName, "19200"))
                    .DataBit = ReadIniString(strSection, "DataBits", fileName, "8")
                    .CardType = CInt(ReadIniString(strSection, "ProgramLightType", fileName, 0))    '[0:沒有]

                    .ChannelMaxValue1 = CInt(ReadIniString(strSection, "Ch1MaxValue", fileName, 0)) '[0:沒有]
                    .ChannelMaxValue2 = CInt(ReadIniString(strSection, "Ch2MaxValue", fileName, 0)) '[0:沒有]
                    .ChannelMaxValue3 = CInt(ReadIniString(strSection, "Ch3MaxValue", fileName, 0)) '[0:沒有]
                    .ChannelMaxValue4 = CInt(ReadIniString(strSection, "Ch4MaxValue", fileName, 0)) '[0:沒有]
                    .Unit = 255
                    .ChannelScale1 = Math.Round(.ChannelMaxValue1 / .Unit, 3) 'CInt(ReadIniString(strSection, "ChannelScale1", fileName, 1))
                    .ChannelScale2 = Math.Round(.ChannelMaxValue2 / .Unit, 3) 'CInt(ReadIniString(strSection, "ChannelScale2", fileName, 1))
                    .ChannelScale3 = Math.Round(.ChannelMaxValue3 / .Unit, 3) 'CInt(ReadIniString(strSection, "ChannelScale3", fileName, 1))
                    .ChannelScale4 = Math.Round(.ChannelMaxValue4 / .Unit, 3) 'CInt(ReadIniString(strSection, "ChannelScale4", fileName, 1))
                End With
            Next

            Return True
        Catch ex As Exception
            gSyslog.Save("Exception Message:" & ex.Message, , eMessageLevel.Error)
            Return False
        End Try

    End Function
End Class

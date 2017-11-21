Imports ProjectCore

''' <summary>雷射干涉儀控制器連線設定</summary>
''' <remarks></remarks>
Public Class CLaserReaderCards
    Public ReadOnly Property Count As Integer
        Get
            Return Parameters.Count
        End Get
    End Property
    Public Parameters As New List(Of sLaserReaderConnectionParameter)
    Default Public Property Items(ByVal index As Integer) As sLaserReaderConnectionParameter
        Get
            Return Parameters(index)
        End Get
        Set(value As sLaserReaderConnectionParameter)
            Parameters(index) = value
        End Set
    End Property

    ''' <summary>儲存連線參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String)
        Dim CardCount As Integer = Parameters.Count
        Dim mSection As String = "LaserReader"
        SaveIniString(mSection, "CardCount", CardCount, fileName) '卡片數量儲存

        For mCardNo As Integer = 0 To CardCount - 1
            mSection = "Laser" & (mCardNo + 1).ToString & "-Connection"
            With Parameters(mCardNo)
                SaveIniString(mSection, "CardType", CInt(.CardType), fileName)

                SaveIniString(mSection, "DL-RS1A-PortName", .DLRS1A.PortName, fileName)
                SaveIniString(mSection, "DL-RS1A-BaudRate", .DLRS1A.BaudRate, fileName)

                SaveIniString(mSection, "IPAddress", .LJV7060TCP.IP, fileName)
                SaveIniString(mSection, "Port", .LJV7060TCP.Port, fileName)

            End With
        Next

        Return True
    End Function
    ''' <summary>讀取連線參數設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String)
        Dim mSection As String = "LaserReader"
        Dim mCardCount As Integer
        mCardCount = Val(ReadIniString(mSection, "CardCount", fileName, enmLaserReader.Max + 1))
        Parameters.Clear()

        For mLaserNo As Integer = 0 To mCardCount - 1
            mSection = "Laser" & (mLaserNo + 1).ToString & "-Connection"
            Dim mItem As New sLaserReaderConnectionParameter
            With mItem
                .CardType = Val(ReadIniString(mSection, "CardType", fileName, 0))

                .DLRS1A.PortName = ReadIniString(mSection, "DL-RS1A-PortName", fileName, "COM1")
                .DLRS1A.BaudRate = ReadIniString(mSection, "DL-RS1A-BaudRate", fileName, "9600")

                .LJV7060TCP.IP = ReadIniString(mSection, "IPAddress", fileName)
                .LJV7060TCP.Port = Val(ReadIniString(mSection, "Port", fileName))

            End With
            Parameters.Add(mItem)
        Next
        Return True
    End Function

End Class

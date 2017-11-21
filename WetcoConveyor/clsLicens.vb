Imports System.Security.Cryptography
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Windows.Forms

''' <summary>
''' 設定或比對授權保護
''' </summary>
''' <remarks> Key 與 SetLicensFilePath(ByVal path As String) 為必要設定值</remarks>
Public Class clsLicens
    Dim md5 As MD5
    Dim LicensPath As String = Environment.SystemDirectory & "\STD\Premtek.ini"
    Dim LogPath As String = Application.StartupPath & "\PiiLibrary"

    Private _key As String = GetMacAdress()
    ''' <summary>
    ''' 設定或取得授權字串
    ''' </summary>
    Public Property Key As String
        Get
            Return _key
        End Get
        Set(value As String)
            _key = value
        End Set
    End Property

    Dim _deadline As DateTime = New DateTime(1900, 1, 1)
    ''' <summary>
    ''' 金鑰期限
    ''' </summary>
    Public ReadOnly Property Deadline As DateTime
        Get
            _deadline = GetDeadline()
            Return _deadline
        End Get
    End Property

    ''' <summary>
    ''' 金鑰權限
    ''' </summary>
    Public ReadOnly Property Licens As enmVersion
        Get
            Return GetLicensVersion()
        End Get
    End Property

    Private Enum ErrorCode
        Null
        LicensError
    End Enum

    Public Enum enmVersion
        Null
        Golden
        Trial
    End Enum

    Sub New()
        CreateLogFile()
    End Sub

    ''' <summary>
    ''' 設定金鑰檔案路徑
    ''' </summary>
    ''' <param name="path"></param>
    Public Sub SetLicensFilePath(ByVal path As String)
        '[Note]:預設路徑 : Environment.SystemDirectory & "\STD\Premtek.ini"
        LicensPath = path
    End Sub

    ''' <summary>
    '''產生永久權限金鑰
    ''' </summary>
    Public Function CreateLicens() As String
        Try
            '1988/8/23
            Dim year As Integer = 1988
            Dim month As Integer = 8
            Dim day As Integer = 23
            Dim md5 As String = StringToMD5(year.ToString())
            Dim key As String = md5
            md5 = StringToMD5(month.ToString())
            key += md5
            md5 = StringToMD5(day.ToString())
            key += md5
            md5 = StringToMD5(Me.Key)
            key += md5

            Return key
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' 產生有使用期限之金鑰
    ''' </summary>
    ''' <param name="ddate">有效期限</param>
    Public Function CreateLicens(ByVal ddate As DateTime) As String
        Try
            Dim year As Integer = ddate.Year
            Dim month As Integer = ddate.Month
            Dim day As Integer = ddate.Day
            Dim md5 As String = StringToMD5(year.ToString())
            Dim key As String = md5
            md5 = StringToMD5(month.ToString())
            key += md5
            md5 = StringToMD5(day.ToString())
            key += md5
            md5 = StringToMD5(Me.Key)
            key += md5

            Return key
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' 檢查Licens是否過期
    ''' </summary>
    Public Function CheckLicens() As Boolean
        Dim directoryInfo As New DirectoryInfo(LogPath)
        Dim tNow As Date = Date.Now.ToShortDateString()

        If (Licens = clsLicens.enmVersion.Golden) Then
            Return True
        ElseIf (Licens = enmVersion.Trial) Then
            If DateTime.Compare(Deadline.AddDays(1), Date.Now) > 0 Then '金鑰日期尚未過期
                For Each logFile In directoryInfo.GetFiles("*.dll")
                    Dim path As String = logFile.FullName
                    Dim createTime As Date = File.GetCreationTime(path).ToShortDateString()

                    If DateTime.Compare(createTime, tNow) > 0 Then
                        Return False 'Log檔建立日期大於電腦時間, 表示電腦日期遭修改
                    End If
                Next
                Return True
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' 取得許可權限
    ''' </summary>
    Private Function GetLicensVersion() As enmVersion
        If File.Exists(LicensPath) Then
            Dim licens As String
            Using sr As New StreamReader(LicensPath)
                licens = sr.ReadLine()
                sr.Close()
            End Using

            Dim year As String = licens.Substring(0, 24)
            Dim month As String = licens.Substring(24, 24)
            Dim day As String = licens.Substring(48, 24)
            Dim mac As String = licens.Substring(72, 24)
            Dim goldenKey As String = CreateLicens()
            If (licens = goldenKey) Then
                Return enmVersion.Golden
            ElseIf (mac = StringToMD5(Me.Key)) Then
                Return enmVersion.Trial
            Else
                Return enmVersion.Null
            End If
        End If
        Return enmVersion.Null
    End Function

    ''' <summary>
    ''' 取得使用期限
    ''' </summary>
    Private Function GetDeadline() As DateTime
        If File.Exists(LicensPath) Then
            Dim licens As String
            Using sr As New StreamReader(LicensPath)
                licens = sr.ReadLine()
                sr.Close()
            End Using

            Dim year As String = licens.Substring(0, 24)
            Dim month As String = licens.Substring(24, 24)
            Dim day As String = licens.Substring(48, 24)
            Dim mac As String = licens.Substring(72, 24)
            Dim goldenKey As String = CreateLicens()
            If (licens = goldenKey) Then
                Return New DateTime(2999, 1, 1)
            ElseIf (mac = StringToMD5(Me.Key)) Then
                Dim y, m, d As Integer
                For i = 2016 To Date.Now.Year + 20
                    If (year = StringToMD5(i.ToString())) Then
                        y = i
                        Exit For
                    End If
                Next
                For i = 1 To 12
                    If (month = StringToMD5(i.ToString())) Then
                        m = i
                        Exit For
                    End If
                Next
                For i = 0 To 30
                    If (day = StringToMD5(i.ToString())) Then
                        d = i
                        Exit For
                    End If
                Next
                Dim time As DateTime = New DateTime(y, m, d)
                Return time
            Else
                Return New DateTime(1900, 1, 1)
            End If
        Else
            Return New DateTime(1900, 1, 1)
        End If
    End Function

    ''' <summary>
    ''' 產生日期紀錄檔
    ''' </summary>
    Private Sub CreateLogFile()
        If Not Directory.Exists(LogPath) Then
            Directory.CreateDirectory(LogPath)
        End If

        Dim directoryInfo As New DirectoryInfo(LogPath)
        'Dim dateList As New List(Of Date)
        Dim create As Boolean = True
        Dim tNow As Date = Date.Now.ToShortDateString()

        For Each logFile In directoryInfo.GetFiles("*.dll")
            Dim path As String = logFile.FullName
            Dim createTime As Date = File.GetCreationTime(path).ToShortDateString()

            If DateTime.Compare(createTime, tNow) >= 0 Then
                create = False
                Exit For
            End If
        Next

        If (create) Then
            File.Create(LogPath & "\" & tNow.Year.ToString() & tNow.Month.ToString() & tNow.Day.ToString() & ".dll")
        End If
    End Sub

    ''' <summary>
    ''' 取得網卡Mac Address
    ''' </summary>
    Private Function GetMacAdress() As String
        Dim nics As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        For Each nic As NetworkInterface In nics
            ' 因為電腦中可能有很多的網卡(包含虛擬的網卡)，
            ' 只需要 Ethernet 網卡的 MAC
            If nic.NetworkInterfaceType = NetworkInterfaceType.Ethernet Then
                Return nic.GetPhysicalAddress().ToString()
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' MD5 編碼轉換
    ''' </summary>
    Private Function StringToMD5(ByVal str As String) As String
        Try
            Dim Original As Byte() = System.Text.Encoding.[Default].GetBytes(str.ToUpper())
            Dim st As MD5 = md5.Create()
            Dim PWD As Byte() = md5.Create().ComputeHash(Original)
            Return Convert.ToBase64String(PWD)
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

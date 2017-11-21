Imports ProjectIO
Imports ProjectCore

Public Class frmPLC
    Dim timerIndex As Integer

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case TabControl1.SelectedIndex
            Case 0

                UpdateLabel(palX000, PLCX(0))
                UpdateLabel(palX001, PLCX(1))
                UpdateLabel(palX002, PLCX(2))
                UpdateLabel(palX003, PLCX(3))
                UpdateLabel(palX004, PLCX(4))
                UpdateLabel(palX005, PLCX(5))
                UpdateLabel(palX006, PLCX(6))
                UpdateLabel(palX007, PLCX(7))
                'UpdatePanel(palX008, PLCX(8))
                'pdatePanel(palX009, PLCX(9))
                UpdateLabel(palX010, PLCX(8))
                UpdateLabel(palX011, PLCX(9))
                UpdateLabel(palX012, PLCX(10))
                UpdateLabel(palX013, PLCX(11))
                UpdateLabel(palX014, PLCX(12))
                UpdateLabel(palX015, PLCX(13))
                UpdateLabel(palX016, PLCX(14))
                UpdateLabel(palX017, PLCX(15))
                'UpdatePanel(palX018, PLCX(18))
                'UpdatePanel(palX019, PLCX(19))
                UpdateLabel(palX020, PLCX(16))
                UpdateLabel(palX021, PLCX(17))
                UpdateLabel(palX022, PLCX(18))
                UpdateLabel(palX023, PLCX(19))
                UpdateLabel(palX024, PLCX(20))
                UpdateLabel(palX025, PLCX(21))
                UpdateLabel(palX026, PLCX(22))
                UpdateLabel(palX027, PLCX(23))
                'UpdatePanel(palX028, PLCX(28))
                'UpdatePanel(palX029, PLCX(29))
                UpdateLabel(palX030, PLCX(24))
                UpdateLabel(palX031, PLCX(25))
                UpdateLabel(palX032, PLCX(26))
                UpdateLabel(palX033, PLCX(27))
                UpdateLabel(palX034, PLCX(28))
                UpdateLabel(palX035, PLCX(29))
                UpdateLabel(palX036, PLCX(30))
                UpdateLabel(palX037, PLCX(31))
                'UpdatePanel(palX038, PLCX(38))
                'UpdatePanel(palX039, PLCX(39))
                UpdateLabel(palX040, PLCX(32))
                UpdateLabel(palX041, PLCX(33))
                UpdateLabel(palX042, PLCX(34))
                UpdateLabel(palX043, PLCX(35))
                UpdateLabel(palX044, PLCX(36))
                UpdateLabel(palX045, PLCX(37))
                UpdateLabel(palX046, PLCX(38))
                UpdateLabel(palX047, PLCX(39))
                gPLC.ReadBits("X0000", 36)
                timerIndex = 0

            Case 1

                UpdateLabel(palY000, PLCY(0))
                UpdateLabel(palY001, PLCY(1))
                UpdateLabel(palY002, PLCY(2))
                UpdateLabel(palY003, PLCY(3))
                UpdateLabel(palY004, PLCY(4))
                UpdateLabel(palY005, PLCY(5))
                UpdateLabel(palY006, PLCY(6))
                UpdateLabel(palY007, PLCY(7))
                'UpdatePanel(palY008, PLCY(8))
                'UpdatePanel(palY009, PLCY(9))
                UpdateLabel(palY010, PLCY(8))
                UpdateLabel(palY011, PLCY(9))
                UpdateLabel(palY012, PLCY(10))
                UpdateLabel(palY013, PLCY(11))
                UpdateLabel(palY014, PLCY(12))
                UpdateLabel(palY015, PLCY(13))
                UpdateLabel(palY016, PLCY(14))
                UpdateLabel(palY017, PLCY(15))
                'UpdatePanel(palY018, PLCY(18))
                'UpdatePanel(palY019, PLCY(19))
                UpdateLabel(palY020, PLCY(16))
                UpdateLabel(palY021, PLCY(17))
                UpdateLabel(palY022, PLCY(18))
                UpdateLabel(palY023, PLCY(19))
                UpdateLabel(palY024, PLCY(20))
                UpdateLabel(palY025, PLCY(21))
                UpdateLabel(palY026, PLCY(22))
                UpdateLabel(palY027, PLCY(23))
                'UpdatePanel(palY028, PLCY(28))
                'UpdatePanel(palY029, PLCY(29))
                UpdateLabel(palY030, PLCY(24))
                UpdateLabel(palY031, PLCY(25))
                UpdateLabel(palY032, PLCY(26))
                UpdateLabel(palY033, PLCY(27))
                UpdateLabel(palY034, PLCY(28))
                UpdateLabel(palY035, PLCY(29))
                UpdateLabel(palY036, PLCY(30))
                UpdateLabel(palY037, PLCY(31))
                'UpdatePanel(palY038, PLCY(38))
                'UpdatePanel(palY039, PLCY(39))
                UpdateLabel(palY040, PLCY(32))
                UpdateLabel(palY041, PLCY(33))
                UpdateLabel(palY042, PLCY(34))
                UpdateLabel(palY043, PLCY(35))
                UpdateLabel(palY044, PLCY(36))
                UpdateLabel(palY045, PLCY(37))
                UpdateLabel(palY046, PLCY(38))
                UpdateLabel(palY047, PLCY(39))
                gPLC.ReadBits("Y0000", 36)
                timerIndex = 0

            Case 2
                Select Case timerIndex
                    Case 0
                        UpdateLabel(palM0202, PLCM(enmPLCM.DispenserBusy))
                        UpdateLabel(palM0206, PLCM(enmPLCM.DispenserFinish))
                        gPLC.ReadBits("M1416", 256)
                        timerIndex = 1
                    Case 1
                        UpdateLabel(palM1416, PLCM(enmPLCM.Station2Up))
                        UpdateLabel(palM1428, PLCM(enmPLCM.Station2Down))
                        UpdateLabel(palM1610, PLCM(enmPLCM.PCAlarmOutput))
                        UpdateLabel(palM1611, PLCM(enmPLCM.PLCAlarmInput))
                        gPLC.ReadBits("M5000", 36)
                        timerIndex = 2
                    Case 2
                        UpdateLabel(palM5000, PLCM(5000))
                        UpdateLabel(palM5001, PLCM(5001))
                        UpdateLabel(palM5002, PLCM(5002))
                        UpdateLabel(palM5003, PLCM(5003))
                        UpdateLabel(palM5004, PLCM(5004))
                        UpdateLabel(palM5005, PLCM(5005))
                        UpdateLabel(palM5006, PLCM(5006))
                        UpdateLabel(palM5007, PLCM(5007))
                        UpdateLabel(palM5008, PLCM(5008))
                        UpdateLabel(palM5009, PLCM(5009))
                        UpdateLabel(palM5010, PLCM(5010))
                        UpdateLabel(palM5011, PLCM(5011))
                        UpdateLabel(palM5012, PLCM(5012))
                        UpdateLabel(palM5013, PLCM(5013))
                        UpdateLabel(palM5014, PLCM(5014))
                        UpdateLabel(palM5015, PLCM(5015))
                        UpdateLabel(palM5016, PLCM(5016))
                        UpdateLabel(palM5017, PLCM(5017))
                        UpdateLabel(palM5018, PLCM(5018))
                        UpdateLabel(palM5019, PLCM(5019))
                        UpdateLabel(palM5020, PLCM(5020))
                        UpdateLabel(palM5021, PLCM(5021))
                        UpdateLabel(palM5022, PLCM(5022))
                        UpdateLabel(palM5023, PLCM(5023))
                        UpdateLabel(palM5024, PLCM(5024))
                        UpdateLabel(palM5025, PLCM(5025))
                        UpdateLabel(palM5026, PLCM(5026))
                        UpdateLabel(palM5027, PLCM(5027))
                        UpdateLabel(palM5028, PLCM(5028))
                        UpdateLabel(palM5029, PLCM(5029))
                        UpdateLabel(palM5030, PLCM(5030))
                        UpdateLabel(palM5031, PLCM(5031))
                        UpdateLabel(palM5032, PLCM(5032))
                        UpdateLabel(palM5033, PLCM(5033))
                        UpdateLabel(palM5034, PLCM(5034))
                        UpdateLabel(palM5035, PLCM(5035))
                        gPLC.ReadBits("M0200", 6)
                        timerIndex = 0

                End Select

        End Select


    End Sub

    Sub UpdateLabel(ByRef panel As Label, ByVal isOn As Boolean)
        If isOn Then
            panel.BackColor = Color.Red
        Else
            panel.BackColor = Color.Transparent

        End If
    End Sub

    Private Sub frmPLC_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub

    Sub ToggleBit(ByVal device As String, ByRef BitValue As Boolean)
        If BitValue = False Then
            gPLC.WriteBits(device, 1, "1")
            BitValue = True
        Else
            gPLC.WriteBits(device, 1, "0")
            BitValue = False
        End If
    End Sub
    Private Sub palY000_Click(sender As Object, e As EventArgs) Handles palY000.Click
        ToggleBit("Y0000", PLCY(0))
    End Sub

    Private Sub palY001_Click(sender As Object, e As EventArgs) Handles palY001.Click
        ToggleBit("Y0001", PLCY(1))
    End Sub

    Private Sub palY002_Click(sender As Object, e As EventArgs) Handles palY002.Click
        ToggleBit("Y0002", PLCY(2))
    End Sub

    Private Sub palY003_Click(sender As Object, e As EventArgs) Handles palY003.Click
        ToggleBit("Y0003", PLCY(3))
    End Sub

    Private Sub palY004_Click(sender As Object, e As EventArgs) Handles palY004.Click
        ToggleBit("Y0004", PLCY(4))
    End Sub

    Private Sub palY005_Click(sender As Object, e As EventArgs) Handles palY005.Click
        ToggleBit("Y0005", PLCY(5))
    End Sub

    Private Sub palY006_Click(sender As Object, e As EventArgs) Handles palY006.Click
        ToggleBit("Y0006", PLCY(6))
    End Sub

    Private Sub palY007_Click(sender As Object, e As EventArgs) Handles palY007.Click
        ToggleBit("Y0007", PLCY(7))
    End Sub

    Private Sub palY010_Click(sender As Object, e As EventArgs) Handles palY010.Click
        ToggleBit("Y0010", PLCY(8))
    End Sub

    Private Sub palY011_Click(sender As Object, e As EventArgs) Handles palY011.Click
        ToggleBit("Y0011", PLCY(9))
    End Sub

    Private Sub palY012_Click(sender As Object, e As EventArgs) Handles palY012.Click
        ToggleBit("Y0012", PLCY(10))
    End Sub

    Private Sub palY013_Click(sender As Object, e As EventArgs) Handles palY013.Click
        ToggleBit("Y0013", PLCY(11))
    End Sub

    Private Sub palY014_Click(sender As Object, e As EventArgs) Handles palY014.Click
        ToggleBit("Y0014", PLCY(12))
    End Sub

    Private Sub palY015_Click(sender As Object, e As EventArgs) Handles palY015.Click
        ToggleBit("Y0015", PLCY(13))
    End Sub

    Private Sub palY016_Click(sender As Object, e As EventArgs) Handles palY016.Click
        ToggleBit("Y0016", PLCY(14))
    End Sub

    Private Sub palY017_Click(sender As Object, e As EventArgs) Handles palY017.Click
        ToggleBit("Y0017", PLCY(15))
    End Sub

    Private Sub palY020_Click(sender As Object, e As EventArgs) Handles palY020.Click
        ToggleBit("Y0020", PLCY(16))
    End Sub

    Private Sub palY021_Click(sender As Object, e As EventArgs) Handles palY021.Click
        ToggleBit("Y0021", PLCY(17))
    End Sub

    Private Sub palY022_Click(sender As Object, e As EventArgs) Handles palY022.Click
        ToggleBit("Y0022", PLCY(18))
    End Sub

    Private Sub palY023_Click(sender As Object, e As EventArgs) Handles palY023.Click
        ToggleBit("Y0023", PLCY(19))
    End Sub

    Private Sub palY024_Click(sender As Object, e As EventArgs) Handles palY024.Click
        ToggleBit("Y0024", PLCY(20))
    End Sub

    Private Sub palY025_Click(sender As Object, e As EventArgs) Handles palY025.Click
        ToggleBit("Y0025", PLCY(21))
    End Sub

    Private Sub palY026_Click(sender As Object, e As EventArgs) Handles palY026.Click
        ToggleBit("Y0026", PLCY(22))
    End Sub

    Private Sub palY027_Click(sender As Object, e As EventArgs) Handles palY027.Click
        ToggleBit("Y0027", PLCY(23))
    End Sub

    Private Sub palY030_Click(sender As Object, e As EventArgs) Handles palY030.Click
        ToggleBit("Y0030", PLCY(24))
    End Sub

    Private Sub palY031_Click(sender As Object, e As EventArgs) Handles palY031.Click
        ToggleBit("Y0031", PLCY(25))
    End Sub

    Private Sub palY032_Click(sender As Object, e As EventArgs) Handles palY032.Click
        ToggleBit("Y0032", PLCY(26))
    End Sub

    Private Sub palY033_Click(sender As Object, e As EventArgs) Handles palY033.Click
        ToggleBit("Y0033", PLCY(27))
    End Sub

    Private Sub palY034_Click(sender As Object, e As EventArgs) Handles palY034.Click
        ToggleBit("Y0034", PLCY(28))
    End Sub

    Private Sub palY035_Click(sender As Object, e As EventArgs) Handles palY035.Click
        ToggleBit("Y0035", PLCY(29))
    End Sub

    Private Sub palY036_Click(sender As Object, e As EventArgs) Handles palY036.Click
        ToggleBit("Y0036", PLCY(30))
    End Sub

    Private Sub palY037_Click(sender As Object, e As EventArgs) Handles palY037.Click
        ToggleBit("Y0037", PLCY(31))
    End Sub

    Private Sub palY040_Click(sender As Object, e As EventArgs) Handles palY040.Click
        ToggleBit("Y0040", PLCY(32))
    End Sub

    Private Sub palY041_Click(sender As Object, e As EventArgs) Handles palY041.Click
        ToggleBit("Y0041", PLCY(33))
    End Sub

    Private Sub palY042_Click(sender As Object, e As EventArgs) Handles palY042.Click
        ToggleBit("Y0042", PLCY(34))
    End Sub

    Private Sub palY043_Click(sender As Object, e As EventArgs) Handles palY043.Click
        ToggleBit("Y0043", PLCY(35))
    End Sub

    Private Sub palY044_Click(sender As Object, e As EventArgs) Handles palY044.Click
        ToggleBit("Y0044", PLCY(36))
    End Sub

    Private Sub palY045_Click(sender As Object, e As EventArgs) Handles palY045.Click
        ToggleBit("Y0045", PLCY(37))
    End Sub

    Private Sub palY046_Click(sender As Object, e As EventArgs) Handles palY046.Click
        ToggleBit("Y0046", PLCY(38))
    End Sub

    Private Sub palY047_Click(sender As Object, e As EventArgs) Handles palY047.Click
        ToggleBit("Y0047", PLCY(39))
    End Sub

    Private Sub palM1416_Click(sender As Object, e As EventArgs) Handles palM1416.Click
        ToggleBit("M1416", PLCM(1416))
    End Sub

    Private Sub palM1428_Click(sender As Object, e As EventArgs) Handles palM1428.Click
        ToggleBit("M1428", PLCM(1428))
    End Sub

    Private Sub palM1610_Click(sender As Object, e As EventArgs) Handles palM1610.Click
        ToggleBit("M1610", PLCM(1610))
    End Sub

    Private Sub btnPLCOpen_Click(sender As Object, e As EventArgs) Handles btnPLCOpen.Click
        Try
            If gPLC.IsOpen Then
                gPLC.Close()
                btnPLCOpen.Text = "Open"
                btnPLCOpen.BackColor = SystemColors.Control
            Else
                gPLC.Open(cmbPLCPort.SelectedText)
                btnPLCOpen.Text = "Close"
                btnPLCOpen.BackColor = Color.Yellow
            End If
        Catch ex As Exception
            gEqpMsg.AddHistoryAlarm("Error_1010000", "frmPLC", , gMsgHandler.GetMessage(Error_1010000), eMessageLevel.Error)
        End Try

    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected
        Select Case TabControl1.SelectedIndex
            Case 3 'Config
                cmbPLCPort.Items.Clear()
                cmbPLCPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames())
                cmbPLCPort.SelectedText = gSSystemParameter.sConveyor(0).PLCPortName

                If gPLC.IsOpen Then
                    btnPLCOpen.Text = "Close"
                    btnPLCOpen.BackColor = Color.Yellow
                Else
                    btnPLCOpen.Text = "Open"
                    btnPLCOpen.BackColor = SystemColors.Control
                End If
        End Select
    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnPreviousPage.Click
        Me.OnDeactivate(e)
        Me.Hide()
    End Sub
End Class
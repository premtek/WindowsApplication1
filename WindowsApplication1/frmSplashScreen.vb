Imports ProjectCore

Public Class frmSplashScreen
    Public Sub AddProgress(ByVal value As Integer)
        If ProgressBar1.Value + value < ProgressBar1.Minimum Then
            ProgressBar1.Value = ProgressBar1.Minimum
        ElseIf ProgressBar1.Value + value > ProgressBar1.Maximum Then
            ProgressBar1.Value = ProgressBar1.Maximum
        Else
            ProgressBar1.Value += value
        End If
        ProgressBar1.Refresh()

    End Sub

    Private Sub frmSplashScreen_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        tmrLife.Stop()
    End Sub

    Private Sub frmSplashScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        tmrLife.Interval = 300
        ProgressBar1.Value = 0

        lblMachineType.Text = GetMachineTypeFromFileToString()
        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\logo.jpg"
        If System.IO.File.Exists(fileName) Then
            Panel1.BackgroundImage = Image.FromFile(fileName)
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ '客製化不顯示版權宣告
                Label1.Text = ""

            Case enmMachineType.DCS_F230A
                Label1.Text = "Copyright (C) 2017. All Rights Reserved."

            Case Else
                Label1.Text = "Copyright (C) 2017 Premtek  International Inc. All Rights Reserved."

        End Select

        lblVersion.Text = "Ver." & System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString()
        tmrLife.Start()
    End Sub

    'Private Sub tmrLife_Tick(sender As Object, e As EventArgs) Handles tmrLife.Tick
    '    Application.DoEvents()
    'End Sub
End Class
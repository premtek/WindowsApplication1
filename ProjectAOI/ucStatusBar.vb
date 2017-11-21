Imports ProjectIO
Imports ProjectCore

Public Class ucStatusBar




    Public Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim indexS As Integer
        Dim data As String

        If gEqpMsg.AlarmList.Count > 0 Then
            indexS = gEqpMsg.AlarmList.Count
            data = gEqpMsg.AlarmList(indexS - 1).ALID & " " & gMsgHandler.GetMessage(gEqpMsg.AlarmList(indexS - 1).ALID)
            lblStatus.Text = data

            If data.StartsWith("1") Or data.StartsWith("2") Or data.StartsWith("A") Or data.StartsWith("E") Then
                lblStatus.BackColor = Color.Red
            ElseIf data.StartsWith("3") Or data.StartsWith("W") Then
                lblStatus.BackColor = Color.Yellow
            Else
                lblStatus.BackColor = Color.Green
            End If
        ElseIf gEqpMsg.AlarmList.Count = 0 Then
            lblStatus.Text = "Home Finish"
            lblStatus.BackColor = Color.Green
        End If

    End Sub



 
    Private Sub ucStatusBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()
        Timer1.Enabled = True
        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub

    'Eason 20170120 Ticket:100030 , Memory Freed 
    'Public Sub MyDispose()
    Public Sub ManualDispose()
        Me.Dispose(True)
    End Sub

End Class

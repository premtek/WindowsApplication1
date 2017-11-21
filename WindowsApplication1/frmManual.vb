Imports ProjectRecipe.MCommonRecipe
Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectIO
Imports ProjectCore

Public Class frmManual

    ''' <summary>按鍵依序排列</summary>
    ''' <param name="btnList"></param>
    ''' <remarks></remarks>
    Sub AppendButton(ByRef btnList As List(Of Button))
        Dim basicPosX As Integer
        Dim basicPosY As Integer
        Dim height As Integer
        If btnList.count > 0 Then
            basicPosX = btnList(0).Location.X
            basicPosY = btnList(0).Location.Y
            height = btnList(0).Height
        End If
        Dim visibleBtnY As Integer = 0
        Dim visibleBtnX As Integer = 0
        For i As Integer = 0 To btnList.Count - 1
            If btnList(i).Visible Then
                btnList(i).Location = New Point(basicPosX + visibleBtnX * 230, basicPosY + visibleBtnY * height)
                visibleBtnY += 1
                If visibleBtnY = 5 Then
                    visibleBtnY = 0
                    visibleBtnX += 1
                End If
            End If
        Next

    End Sub
    ''' <summary>離開此頁面時自動關閉</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Manual_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Dispose(True) 'Soni / 2017.01.10 記憶體累增
    End Sub

    Private Sub frmManual_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    ''' <summary>載入時配接軸名稱</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Manual_Load(sender As Object, e As EventArgs) Handles Me.Load
        '=== 介面按鍵顯示更新 ===
        For i As Integer = 0 To gCMotion.AxisParameter.Count - 1
            Dim mControlName As String = "btnSetupAxis" & i.ToString()
            If tabMotor.Controls.ContainsKey(mControlName) Then

                tabMotor.Controls(mControlName).Text = gCMotion.AxisParameter(i).AxisName
                tabMotor.Controls(mControlName).Visible = True
                If gCMotion.AxisParameter(i).AxisName <> "" Then
                    tabMotor.Controls(mControlName).Visible = True
                    tabMotor.Controls(mControlName).Enabled = True
                Else
                    tabMotor.Controls(mControlName).Text = "(Reserved)"
                    tabMotor.Controls(mControlName).Enabled = False
                    tabMotor.Controls(mControlName).Visible = False
                End If

            End If
        Next
        '=== 介面按鍵顯示更新 ===
        Dim btnList As New List(Of Button)
        btnList.Add(btnSetupAxis0)
        btnList.Add(btnSetupAxis1)
        btnList.Add(btnSetupAxis2)
        btnList.Add(btnSetupAxis3)
        btnList.Add(btnSetupAxis4)
        btnList.Add(btnSetupAxis5)
        btnList.Add(btnSetupAxis6)
        btnList.Add(btnSetupAxis7)
        btnList.Add(btnSetupAxis8)
        btnList.Add(btnSetupAxis9)
        btnList.Add(btnSetupAxis10)
        btnList.Add(btnSetupAxis11)
        btnList.Add(btnSetupAxis12)
        btnList.Add(btnSetupAxis13)
        btnList.Add(btnSetupAxis14)
        btnList.Add(btnSetupAxis15)
        btnList.Add(btnSetupAxis16)
        btnList.Add(btnSetupAxis17)
        btnList.Add(btnSetupAxis18)
        btnList.Add(btnSetupAxis19)
        btnList.Add(btnSetupAxis20)
        btnList.Add(btnSetupAxis21)
        btnList.Add(btnSetupAxis22)
        btnList.Add(btnSetupAxis23)
        btnList.Add(btnSetupAxis24)
        AppendButton(btnList)


        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    ' ''' <summary>單開用顯示介面</summary>
    ' ''' <remarks></remarks>
    Dim frmMotionOperation As frmMotionOp

    ''' <summary>
    ''' 顯示FrmMotionOp
    ''' </summary>
    ''' <param name="axisNo"></param>
    ''' <remarks>廠內權限可以多開, 客戶權限只能單開</remarks>
    Sub ShowfrmMotionOperation(ByVal axisNo As Integer, ByRef btn As Button)
        If btn.Enabled = False Then
            Exit Sub
        End If
        btn.Enabled = False
        Select Case gUserLevel
            Case enmUserLevel.eSoftwareMaker, enmUserLevel.eAdministrator '最高權限允許多開介面
                Dim frmMotionOp As New frmMotionOp
                frmMotionOp.Location = New Point(0, 0)
                frmMotionOp.StartPosition = FormStartPosition.Manual
                frmMotionOp.AxisNo = axisNo
                frmMotionOp.TopMost = True
                frmMotionOp.Show()

            Case Else '一般權限, 只能單開介面
                Dim pos As Point = PointToScreen(btn.Parent.Parent.Location + btn.Parent.Location + btn.Location)
                frmMotionOperation = New frmMotionOp
                With btn
                    frmMotionOperation.Location = New Point(pos.X + .Width, pos.Y)
                End With
                frmMotionOperation.AxisNo = axisNo
                frmMotionOperation.ShowDialog()
        End Select
        btn.Enabled = True
    End Sub

#Region "軸按鍵操作"
    Private Sub btnSetupAxis0_Click(sender As Object, e As EventArgs) Handles btnSetupAxis0.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis0]" & vbTab & "Click")
        ShowfrmMotionOperation(0, sender)
    End Sub

    Private Sub btnSetupAxis1_Click(sender As Object, e As EventArgs) Handles btnSetupAxis1.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis1]" & vbTab & "Click")
        ShowfrmMotionOperation(1, sender)
    End Sub

    Private Sub btnSetupAxis2_Click(sender As Object, e As EventArgs) Handles btnSetupAxis2.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis2]" & vbTab & "Click")
        ShowfrmMotionOperation(2, sender)
    End Sub

    Private Sub btnSetupAxis3_Click(sender As Object, e As EventArgs) Handles btnSetupAxis3.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis3]" & vbTab & "Click")
        ShowfrmMotionOperation(3, sender)
    End Sub

    Private Sub btnSetupAxis4_Click(sender As Object, e As EventArgs) Handles btnSetupAxis4.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis4]" & vbTab & "Click")
        ShowfrmMotionOperation(4, sender)
    End Sub

    Private Sub btnSetupAxis5_Click(sender As Object, e As EventArgs) Handles btnSetupAxis5.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis5]" & vbTab & "Click")
        ShowfrmMotionOperation(5, sender)
    End Sub

    Private Sub btnSetupAxis6_Click(sender As Object, e As EventArgs) Handles btnSetupAxis6.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis6]" & vbTab & "Click")
        ShowfrmMotionOperation(6, sender)
    End Sub

    Private Sub btnSetupAxis7_Click(sender As Object, e As EventArgs) Handles btnSetupAxis7.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis7]" & vbTab & "Click")
        ShowfrmMotionOperation(7, sender)
    End Sub

    Private Sub btnSetupAxis8_Click(sender As Object, e As EventArgs) Handles btnSetupAxis8.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis8]" & vbTab & "Click")
        ShowfrmMotionOperation(8, sender)
    End Sub

    Private Sub btnSetupAxis9_Click(sender As Object, e As EventArgs) Handles btnSetupAxis9.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis9]" & vbTab & "Click")
        ShowfrmMotionOperation(9, sender)
    End Sub

    Private Sub btnSetupAxis10_Click(sender As Object, e As EventArgs) Handles btnSetupAxis10.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis10]" & vbTab & "Click")
        ShowfrmMotionOperation(10, sender)
    End Sub

    Private Sub btnSetupAxis11_Click(sender As Object, e As EventArgs) Handles btnSetupAxis11.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis11]" & vbTab & "Click")
        ShowfrmMotionOperation(11, sender)
    End Sub

    Private Sub btnSetupAxis12_Click(sender As Object, e As EventArgs) Handles btnSetupAxis12.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis12]" & vbTab & "Click")
        ShowfrmMotionOperation(12, sender)
    End Sub

    Private Sub btnSetupAxis13_Click(sender As Object, e As EventArgs) Handles btnSetupAxis13.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis13]" & vbTab & "Click")
        ShowfrmMotionOperation(13, sender)
    End Sub

    Private Sub btnSetupAxis14_Click(sender As Object, e As EventArgs) Handles btnSetupAxis14.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis14]" & vbTab & "Click")
        ShowfrmMotionOperation(14, sender)
    End Sub

    Private Sub btnSetupAxis15_Click(sender As Object, e As EventArgs) Handles btnSetupAxis15.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis15]" & vbTab & "Click")
        ShowfrmMotionOperation(15, sender)
    End Sub

    Private Sub btnSetupAxis16_Click(sender As Object, e As EventArgs) Handles btnSetupAxis16.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis16]" & vbTab & "Click")
        ShowfrmMotionOperation(16, sender)
    End Sub

    Private Sub btnSetupAxis17_Click(sender As Object, e As EventArgs) Handles btnSetupAxis17.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis17]" & vbTab & "Click")
        ShowfrmMotionOperation(17, sender)
    End Sub

    Private Sub btnSetupAxis18_Click(sender As Object, e As EventArgs) Handles btnSetupAxis18.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis18]" & vbTab & "Click")
        ShowfrmMotionOperation(18, sender)
    End Sub

    Private Sub btnSetupAxis19_Click(sender As Object, e As EventArgs) Handles btnSetupAxis19.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis19]" & vbTab & "Click")
        ShowfrmMotionOperation(19, sender)
    End Sub

    Private Sub btnSetupAxis20_Click(sender As Object, e As EventArgs) Handles btnSetupAxis20.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis20]" & vbTab & "Click")
        ShowfrmMotionOperation(20, sender)
    End Sub

    Private Sub btnSetupAxis21_Click(sender As Object, e As EventArgs) Handles btnSetupAxis21.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis21]" & vbTab & "Click")
        ShowfrmMotionOperation(21, sender)
    End Sub

    Private Sub btnSetupAxis22_Click(sender As Object, e As EventArgs) Handles btnSetupAxis22.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis22]" & vbTab & "Click")
        ShowfrmMotionOperation(22, sender)
    End Sub

    Private Sub btnSetupAxis23_Click(sender As Object, e As EventArgs) Handles btnSetupAxis23.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis23]" & vbTab & "Click")
        ShowfrmMotionOperation(23, sender)
    End Sub

    Private Sub btnSetupAxis24_Click(sender As Object, e As EventArgs) Handles btnSetupAxis24.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnSetupAxis24]" & vbTab & "Click")
        ShowfrmMotionOperation(24, sender)
    End Sub
#End Region

    ''' <summary>介面確認,關閉</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmManual]" & vbTab & "[btnOK]" & vbTab & "Click")
        Me.Dispose(True) 'Soni / 2017.01.10 記憶體累增
    End Sub

End Class
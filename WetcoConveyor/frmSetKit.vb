Imports ProjectCore
Imports ProjectIO
Imports System.Windows.Forms

Public Class frmSetKit
    Dim folderName As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName
    Dim path As String = folderName & "\Conveyor.ini"

    Private Sub frmSetTemperture_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For i = 0 To mGlobalPool.Unit.A_Vacuum.Length - 1
            chklbVacuumA.SetItemChecked(i, mGlobalPool.Unit.A_Vacuum(i))
        Next

        For i = 0 To mGlobalPool.Unit.B_Vacuum.Length - 1
            chklbVacuumB.SetItemChecked(i, mGlobalPool.Unit.A_Vacuum(i))
        Next
        chklockANo1.Checked = True
        chklockANo2.Checked = True
        chklockANo3.Checked = True
        chklockANo4.Checked = True
        chklockANo5.Checked = True
        chklockANo6.Checked = True

        chklockBNo1.Checked = True
        chklockBNo2.Checked = True
        chklockBNo3.Checked = True
        chklockBNo4.Checked = True
        chklockBNo5.Checked = True
        chklockBNo6.Checked = True

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                GroupBox4.Visible = True
            Case Else
                GroupBox4.Visible = False
        End Select
        timer_CheckStatus.Start()
    End Sub

    ''' <summary>
    ''' 檢查並更新汽缸狀態
    ''' </summary>
    Private Sub timer_CheckStatus_Tick(sender As Object, e As EventArgs) Handles timer_CheckStatus.Tick
        'A機Cylinder上升狀態
        picCylinderUpA1.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady), 0, 1))
        picCylinderUpA2.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater2CylinderUpReady), 0, 1))
        picCylinderUpA3.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater3CylinderUpReady), 0, 1))
        picCylinderUpA4.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater4CylinderUpReady), 0, 1))
        picCylinderUpA5.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater5CylinderUpReady), 0, 1))
        picCylinderUpA6.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater6CylinderUpReady), 0, 1))

        'A機Cylinder下降狀態
        picCylinderDownA1.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady), 0, 1))
        picCylinderDownA2.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater2CylinderDownReady), 0, 1))
        picCylinderDownA3.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater3CylinderDownReady), 0, 1))
        picCylinderDownA4.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater4CylinderDownReady), 0, 1))
        picCylinderDownA5.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater5CylinderDownReady), 0, 1))
        picCylinderDownA6.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station2Heater6CylinderDownReady), 0, 1))

        'B機Cylinder上升狀態
        picCylinderUpB1.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady), 0, 1))
        picCylinderUpB2.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater2CylinderUpReady), 0, 1))
        picCylinderUpB3.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater3CylinderUpReady), 0, 1))
        picCylinderUpB4.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater4CylinderUpReady), 0, 1))
        picCylinderUpB5.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater5CylinderUpReady), 0, 1))
        picCylinderUpB6.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater6CylinderUpReady), 0, 1))

        'B機Cylinder下降狀態
        picCylinderDownB1.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady), 0, 1))
        picCylinderDownB2.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater2CylinderDownReady), 0, 1))
        picCylinderDownB3.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater3CylinderDownReady), 0, 1))
        picCylinderDownB4.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater4CylinderDownReady), 0, 1))
        picCylinderDownB5.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater5CylinderDownReady), 0, 1))
        picCylinderDownB6.Image = ImageList1.Images(If(gDICollection.GetState(enmDI.Station3Heater6CylinderDownReady), 0, 1))

        For Each Ctl As Control In Me.Controls
            If TypeOf Ctl Is PictureBox Then
                Ctl.Invalidate()
            End If
        Next
    End Sub

    Private Sub btnSetVacuumA_Click(sender As Object, e As EventArgs) Handles btnSetVacuumA.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnSetVacuumA]" & vbTab & "Click")
        Dim vacuum(5) As Boolean
        For i = 0 To chklbVacuumA.Items.Count - 1
            If chklbVacuumA.GetItemChecked(i) Then
                vacuum(i) = True
            Else
                vacuum(i) = False
            End If
        Next
        Unit.A_SetVacuum(vacuum)

        Call SaveIniString("Vacuum", "A1", IIf(vacuum(0), 1, 0), path)
        Call SaveIniString("Vacuum", "A2", IIf(vacuum(1), 1, 0), path)
        Call SaveIniString("Vacuum", "A3", IIf(vacuum(2), 1, 0), path)
        Call SaveIniString("Vacuum", "A4", IIf(vacuum(3), 1, 0), path)
        Call SaveIniString("Vacuum", "A5", IIf(vacuum(4), 1, 0), path)
        Call SaveIniString("Vacuum", "A6", IIf(vacuum(5), 1, 0), path)

        'Sue20170627
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

    End Sub

    Private Sub btnSetVacuumB_Click(sender As Object, e As EventArgs) Handles btnSetVacuumB.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnSetVacuumB]" & vbTab & "Click")
        Dim vacuum(5) As Boolean
        For i = 0 To chklbVacuumB.Items.Count - 1
            If chklbVacuumB.GetItemChecked(i) Then
                vacuum(i) = True
            Else
                vacuum(i) = False
            End If
        Next
        Unit.B_SetVacuum(vacuum)

        Call SaveIniString("Vacuum", "B1", IIf(vacuum(0), 1, 0), path)
        Call SaveIniString("Vacuum", "B2", IIf(vacuum(1), 1, 0), path)
        Call SaveIniString("Vacuum", "B3", IIf(vacuum(2), 1, 0), path)
        Call SaveIniString("Vacuum", "B4", IIf(vacuum(3), 1, 0), path)
        Call SaveIniString("Vacuum", "B5", IIf(vacuum(4), 1, 0), path)
        Call SaveIniString("Vacuum", "B6", IIf(vacuum(5), 1, 0), path)

        'Sue20170627
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

    End Sub

#Region "Machine A 汽缸控制"
    Private Sub btnCylinderUpA1_Click(sender As Object, e As EventArgs) Handles btnCylinderUpA1.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpA1]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder1, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpA2_Click(sender As Object, e As EventArgs) Handles btnCylinderUpA2.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpA2]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder2, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpA3_Click(sender As Object, e As EventArgs) Handles btnCylinderUpA3.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpA3]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder3, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpA4_Click(sender As Object, e As EventArgs) Handles btnCylinderUpA4.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpA4]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder4, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpA5_Click(sender As Object, e As EventArgs) Handles btnCylinderUpA5.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpA5]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder5, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpA6_Click(sender As Object, e As EventArgs) Handles btnCylinderUpA6.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpA6]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder6, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderDownA1_Click(sender As Object, e As EventArgs) Handles btnCylinderDownA1.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownA1]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder1, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownA2_Click(sender As Object, e As EventArgs) Handles btnCylinderDownA2.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownA2]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder2, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownA3_Click(sender As Object, e As EventArgs) Handles btnCylinderDownA3.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownA3]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder3, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownA4_Click(sender As Object, e As EventArgs) Handles btnCylinderDownA4.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownA4]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder4, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownA5_Click(sender As Object, e As EventArgs) Handles btnCylinderDownA5.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownA5]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder5, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownA6_Click(sender As Object, e As EventArgs) Handles btnCylinderDownA6.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownA6]" & vbTab & "Click")
        Unit.A_Cylinder(clsUnit.enmCylinder.Cylinder6, clsUnit.enmDirection.Down)
    End Sub
#End Region

#Region "Machine B 汽缸控制"
    Private Sub btnCylinderUpB1_Click(sender As Object, e As EventArgs) Handles btnCylinderUpB1.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpB1]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder1, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpB2_Click(sender As Object, e As EventArgs) Handles btnCylinderUpB2.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpB2]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder2, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpB3_Click(sender As Object, e As EventArgs) Handles btnCylinderUpB3.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpB3]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder3, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpB4_Click(sender As Object, e As EventArgs) Handles btnCylinderUpB4.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpB4]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder4, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpB5_Click(sender As Object, e As EventArgs) Handles btnCylinderUpB5.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpB5]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder5, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderUpB6_Click(sender As Object, e As EventArgs) Handles btnCylinderUpB6.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderUpB6]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder6, clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnCylinderDownB1_Click(sender As Object, e As EventArgs) Handles btnCylinderDownB1.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownB1]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder1, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownB2_Click(sender As Object, e As EventArgs) Handles btnCylinderDownB2.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownB2]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder2, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownB3_Click(sender As Object, e As EventArgs) Handles btnCylinderDownB3.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownB3]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder3, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownB4_Click(sender As Object, e As EventArgs) Handles btnCylinderDownB4.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownB4]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder4, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownB5_Click(sender As Object, e As EventArgs) Handles btnCylinderDownB5.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownB5]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder5, clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnCylinderDownB6_Click(sender As Object, e As EventArgs) Handles btnCylinderDownB6.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnCylinderDownB6]" & vbTab & "Click")
        Unit.B_Cylinder(clsUnit.enmCylinder.Cylinder6, clsUnit.enmDirection.Down)
    End Sub
#End Region

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[frmSetKit]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub chklocAkNO6_CheckedChanged(sender As Object, e As EventArgs) Handles chklockANo6.CheckedChanged
        If chklockANo6.Checked = True Then
            GroupBox27.Enabled = False
        Else
            GroupBox27.Enabled = True
        End If
    End Sub

    Private Sub chklockANo5_CheckedChanged(sender As Object, e As EventArgs) Handles chklockANo5.CheckedChanged
        If chklockANo5.Checked = True Then
            GroupBox21.Enabled = False
        Else
            GroupBox21.Enabled = True
        End If
    End Sub

    Private Sub chklockANo4_CheckedChanged(sender As Object, e As EventArgs) Handles chklockANo4.CheckedChanged
        If chklockANo4.Checked = True Then
            GroupBox25.Enabled = False
        Else
            GroupBox25.Enabled = True
        End If

    End Sub

    Private Sub chklockANo3_CheckedChanged(sender As Object, e As EventArgs) Handles chklockANo3.CheckedChanged
        If chklockANo3.Checked = True Then
            GroupBox19.Enabled = False
        Else
            GroupBox19.Enabled = True
        End If

    End Sub

    Private Sub chklockANo2_CheckedChanged(sender As Object, e As EventArgs) Handles chklockANo2.CheckedChanged
        If chklockANo2.Checked = True Then
            GroupBox23.Enabled = False
        Else
            GroupBox23.Enabled = True
        End If

    End Sub

    Private Sub chklockANo1_CheckedChanged(sender As Object, e As EventArgs) Handles chklockANo1.CheckedChanged
        If chklockANo1.Checked = True Then
            GroupBox5.Enabled = False
        Else
            GroupBox5.Enabled = True
        End If

    End Sub

    Private Sub chklockBNo6_CheckedChanged(sender As Object, e As EventArgs) Handles chklockBNo6.CheckedChanged
        If chklockBNo6.Checked = True Then
            GroupBox7.Enabled = False
        Else
            GroupBox7.Enabled = True
        End If

    End Sub

    Private Sub chklockBNo5_CheckedChanged(sender As Object, e As EventArgs) Handles chklockBNo5.CheckedChanged
        If chklockBNo5.Checked = True Then
            GroupBox17.Enabled = False
        Else
            GroupBox17.Enabled = True
        End If

    End Sub

    Private Sub chklockBNo4_CheckedChanged(sender As Object, e As EventArgs) Handles chklockBNo4.CheckedChanged
        If chklockBNo4.Checked = True Then
            GroupBox9.Enabled = False
        Else
            GroupBox9.Enabled = True
        End If

    End Sub

    Private Sub chklockBNo3_CheckedChanged(sender As Object, e As EventArgs) Handles chklockBNo3.CheckedChanged
        If chklockBNo3.Checked = True Then
            GroupBox15.Enabled = False
        Else
            GroupBox15.Enabled = True
        End If


    End Sub

    Private Sub chklockBNo2_CheckedChanged(sender As Object, e As EventArgs) Handles chklockBNo2.CheckedChanged
        If chklockBNo2.Checked = True Then
            GroupBox11.Enabled = False
        Else
            GroupBox11.Enabled = True
        End If

    End Sub

    Private Sub chklockBNo1_CheckedChanged(sender As Object, e As EventArgs) Handles chklockBNo1.CheckedChanged
        If chklockBNo1.Checked = True Then
            GroupBox13.Enabled = False
        Else
            GroupBox13.Enabled = True
        End If

    End Sub
End Class
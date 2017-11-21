Imports MapData
Imports ProjectCore


Public Class frmMapping
    Dim ArraySize As String
    Dim array(2) As String
    Public Sub btnNewMapping_Click(sender As Object, e As EventArgs) Handles btnNewMapping.Click
        'Sue20170627
        gSyslog.Save("[frmMapping]" & vbTab & "[btnNewMapping]" & vbTab & "Click")
        ArraySize = InputBox("請輸入 Colune& Row  ex  C,R", "Input Array Size", "")
        If ArraySize Is "" Or Not ArraySize.Contains(",") Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("請確認輸入格式是否正確")
            'ArraySize = "1,1"
        Else
            array = Split(ArraySize, ",")
            gfrmMapData.CreateBaseMapData(CInt(array(0)), CInt(array(1)), "d:\\Wafer_new.csv")
            gfrmMapData.OpenFile("d:\\Wafer_new.csv")
            loadAllData()
            frmDrawGraphics.DrawWafer(piboxWaferMap)
        End If

    End Sub



    Public Sub piboxWaferMap_MouseClick(sender As Object, e As MouseEventArgs) Handles piboxWaferMap.MouseClick
        Dim x As Integer
        Dim y As Integer
        frmDrawGraphics.DrawWafer(piboxWaferMap)

        Dim mScaleY As Integer = piboxWaferMap.Height / gfrmMapData.Substrates(0).Rows      '實際比例
        Dim mScaleX As Integer = piboxWaferMap.Width / gfrmMapData.Substrates(0).Columns    '實際比例


        x = e.X \ mScaleX '利用點擊位置再除以die size得到die 座標
        y = e.Y \ mScaleY




        GroupBox1.Text = CStr(x + 1) + "," + CStr(y + 1)
        frmDrawGraphics.changeColor(piboxWaferMap, x, y, "Yellow", mScaleX, mScaleY)
        txtBin.Text = gfrmMapData.Substrates(0).DieArray(x, y).Bin
        txtCycle.Text = gfrmMapData.Substrates(0).DieArray(x, y).Cycle



    End Sub

    Private Sub dieDataSave_Click(sender As Object, e As EventArgs) Handles dieDataSave.Click
        'Sue20170627
        gSyslog.Save("[frmMapping]" & vbTab & "[dieDataSave]" & vbTab & "Click")
        Dim DIEARRAY(2) As String
        DIEARRAY = Split(GroupBox1.Text, ",")
        Dim X, Y As Integer
        X = CInt(DIEARRAY(0)) - 1
        Y = CInt(DIEARRAY(1)) - 1
        If Len(txtBin.Text) > 1 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("請確認輸入BIN號是否正確")
        Else
            gfrmMapData.Substrates(0).DieArray(X, Y).Bin = txtBin.Text
            gfrmMapData.Substrates(0).DieArray(X, Y).Cycle = txtCycle.Text
        End If

    End Sub

    Public Sub btnSavePIIMap_Click(sender As Object, e As EventArgs) Handles btnSavePIIMap.Click
        'Sue20170627
        gSyslog.Save("[frmMapping]" & vbTab & "[btnSavePIIMap]" & vbTab & "Click")
        gfrmMapData.Information.Version = txtVersion.Text
        gfrmMapData.Information.LotID = txtLotID.Text
        gfrmMapData.Information.ProductID = txtProduct.Text
        gfrmMapData.Information.DieSizeX = txtDieSizeX.Text
        gfrmMapData.Information.DieSizeY = txtDieSizeY.Text
        gfrmMapData.Information.RecipeName = txtRecipe.Text
        gfrmMapData.Information.Type = txtType.Text
        gfrmMapData.Information.Pitch = txtPitch.Text
        gfrmMapData.Information.User = txtUser.Text
        gfrmMapData.Information.MachineType = txtMachineType.Text

        Select Case CblNotch.SelectedIndex
            Case 0
                gfrmMapData.Direction = clsMapData.enmDirection.Top
                gfrmMapData.Information.Notch = 0
            Case 1
                gfrmMapData.Direction = clsMapData.enmDirection.Left
                gfrmMapData.Information.Notch = 90
            Case 2
                gfrmMapData.Direction = clsMapData.enmDirection.Bottom
                gfrmMapData.Information.Notch = 180
            Case 3
                gfrmMapData.Direction = clsMapData.enmDirection.Left
                gfrmMapData.Information.Notch = 270
        End Select

        If MessageBox.Show("修改完成並儲存成Map?", "Save", MessageBoxButtons.OKCancel, _
           Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Dim sFileDialog As New SaveFileDialog

            sFileDialog.Filter = "EXCEL檔 (*.CSV)|*.CSV" ' 只能寫入CSV檔
            sFileDialog.FilterIndex = 1
            sFileDialog.RestoreDirectory = True
            If sFileDialog.ShowDialog() = DialogResult.OK Then
                Dim fileName As String = sFileDialog.FileName
                gfrmMapData.OutputPiiMap(sFileDialog.FileName)
            End If
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'System.Console.WriteLine("OK Clicked")
        Else
            'System.Console.WriteLine("Cancel Clicked")
        End If
        'Me.Close()
    End Sub

    Public Sub loadAllData()
        txtVersion.Text = gfrmMapData.Information.Version
        txtLotID.Text = gfrmMapData.Information.LotID
        txtProduct.Text = gfrmMapData.Information.ProductID
        txtDieSizeX.Text = gfrmMapData.Information.DieSizeX
        txtDieSizeY.Text = gfrmMapData.Information.DieSizeY
        txtRecipe.Text = gfrmMapData.Information.RecipeName
        txtType.Text = gfrmMapData.Information.Type
        txtPitch.Text = gfrmMapData.Information.Pitch
        txtUser.Text = gfrmMapData.Information.User
        txtMachineType.Text = gfrmMapData.Information.MachineType
        Dim notch = gfrmMapData.Information.Notch

        Select Case notch
            Case 0
                CblNotch.SelectedIndex = 0
            Case 90
                CblNotch.SelectedIndex = 1
            Case 180
                CblNotch.SelectedIndex = 2
            Case 270
                CblNotch.SelectedIndex = 3
        End Select
    End Sub

    Private Sub btnOpenMapping_Click(sender As Object, e As EventArgs) Handles btnOpenMapping.Click
        'Sue20170627
        gSyslog.Save("[frmMapping]" & vbTab & "[btnOpenMapping]" & vbTab & "Click")
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.Filter = "(*.*)|*.*"

        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
         
            If gfrmMapData.OpenFile(openFileDialog1.FileName) Then
                loadAllData()
                frmDrawGraphics.DrawWafer(piboxWaferMap)
            ElseIf MFunctionMap.WaferMapConvertToPIIMap(openFileDialog1.FileName, "D:\PIIData\WaferTemp.csv") Then
                gfrmMapData.OpenFile("D:\PIIData\WaferTemp.csv")
                loadAllData()
                frmDrawGraphics.DrawWafer(piboxWaferMap)
            Else
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
                MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("檔案格式錯誤")
            End If


        End If

    End Sub

    Private Sub txtBin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBin.KeyPress
        If Char.IsControl(e.KeyChar) Or Char.IsDigit(e.KeyChar) Or Char.IsLetter(e.KeyChar) Then
            If Char.IsLower(e.KeyChar) Then
                e.KeyChar = Char.ToUpper(e.KeyChar)
            End If
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCycle_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCycle.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub frmMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        'Sue20170627
        gSyslog.Save("[frmMapping]" & vbTab & "[btn_Cancel]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
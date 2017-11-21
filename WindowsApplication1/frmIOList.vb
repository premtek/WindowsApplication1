Imports ProjectIO
Imports ProjectCore
Imports System.Drawing

Public Class frmIOList

    Private Sub frmIOList_Load(sender As Object, e As EventArgs) Handles Me.Load

        chk_DoAddress_0.Tag = 0
        chk_DoAddress_1.Tag = 1
        chk_DoAddress_2.Tag = 2
        chk_DoAddress_3.Tag = 3
        chk_DoAddress_4.Tag = 4
        chk_DoAddress_5.Tag = 5
        chk_DoAddress_6.Tag = 6
        chk_DoAddress_7.Tag = 7
        chk_DoAddress_8.Tag = 8
        chk_DoAddress_9.Tag = 9
        chk_DoAddress_10.Tag = 10
        chk_DoAddress_11.Tag = 11
        chk_DoAddress_12.Tag = 12
        chk_DoAddress_13.Tag = 13
        chk_DoAddress_14.Tag = 14
        chk_DoAddress_15.Tag = 15
        chk_DoAddress_16.Tag = 16
        chk_DoAddress_17.Tag = 17
        chk_DoAddress_18.Tag = 18
        chk_DoAddress_19.Tag = 19
        chk_DoAddress_20.Tag = 20
        chk_DoAddress_21.Tag = 21
        chk_DoAddress_22.Tag = 22
        chk_DoAddress_23.Tag = 23
        chk_DoAddress_24.Tag = 24
        chk_DoAddress_25.Tag = 25
        chk_DoAddress_26.Tag = 26
        chk_DoAddress_27.Tag = 27
        chk_DoAddress_28.Tag = 28
        chk_DoAddress_29.Tag = 29
        chk_DoAddress_30.Tag = 30
        chk_DoAddress_31.Tag = 31

        lblDI0.Tag = 0
        lblDI1.Tag = 1
        lblDI2.Tag = 2
        lblDI3.Tag = 3
        lblDI4.Tag = 4
        lblDI5.Tag = 5
        lblDI6.Tag = 6
        lblDI7.Tag = 7
        lblDI8.Tag = 8
        lblDI9.Tag = 9
        lblDI10.Tag = 10
        lblDI11.Tag = 11
        lblDI12.Tag = 12
        lblDI13.Tag = 13
        lblDI14.Tag = 14
        lblDI15.Tag = 15
        lblDI16.Tag = 16
        lblDI17.Tag = 17
        lblDI18.Tag = 18
        lblDI19.Tag = 19
        lblDI20.Tag = 20
        lblDI21.Tag = 21
        lblDI22.Tag = 22
        lblDI23.Tag = 23
        lblDI24.Tag = 24
        lblDI25.Tag = 25
        lblDI26.Tag = 26
        lblDI27.Tag = 27
        lblDI28.Tag = 28
        lblDI29.Tag = 29
        lblDI30.Tag = 30
        lblDI31.Tag = 31

        '[說明]:讀取設定檔
        gDICollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDI.ini") '路徑改向至System下
        gDOCollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDO.ini") '路徑改向至System下
        gAICollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAI.ini") '路徑改向至System下
        gAOCollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAO.ini") '路徑改向至System下

        For mChNo As Integer = 0 To 7
            Me.tabAI.Controls("lblAI" & mChNo).Tag = mChNo
            Me.tabAI.Controls("lblAI" & mChNo).Text = gAICollection.AIParameter(mChNo).Name
            Me.tabAO.Controls("lblAO" & mChNo).Tag = mChNo
            Me.tabAO.Controls("lblAO" & mChNo).Text = gAOCollection.AOParameter(mChNo).Name
        Next

        If gAICollection.Cards.AICardCount = 0 Then
            tabIOTest.Controls.Remove(tabAI)
        End If
        If gAOCollection.Cards.AOCardParameter.Count = 0 Then
            tabIOTest.Controls.Remove(tabAO)
        End If

        If gDICollection.Cards.DICardParameter.Count = 0 Then
            tabIOTest.Controls.Remove(tabDI)
        Else
            Select Case gDICollection.TotalBits
                Case 32
                    rdbDIPart1.Visible = True
                    rdbDIPart2.Visible = False
                    rdbDIPart3.Visible = False
                    rdbDIPart4.Visible = False
                    rdbDIPart5.Visible = False
                    rdbDIPart6.Visible = False
                Case 64
                    rdbDIPart1.Visible = True
                    rdbDIPart2.Visible = True
                    rdbDIPart3.Visible = False
                    rdbDIPart4.Visible = False
                    rdbDIPart5.Visible = False
                    rdbDIPart6.Visible = False
                Case 96
                    rdbDIPart1.Visible = True
                    rdbDIPart2.Visible = True
                    rdbDIPart3.Visible = True
                    rdbDIPart4.Visible = False
                    rdbDIPart5.Visible = False
                    rdbDIPart6.Visible = False
                Case 128
                    rdbDIPart1.Visible = True
                    rdbDIPart2.Visible = True
                    rdbDIPart3.Visible = True
                    rdbDIPart4.Visible = True
                    rdbDIPart5.Visible = False
                    rdbDIPart6.Visible = False
                Case 160
                    rdbDIPart1.Visible = True
                    rdbDIPart2.Visible = True
                    rdbDIPart3.Visible = True
                    rdbDIPart4.Visible = True
                    rdbDIPart5.Visible = True
                    rdbDIPart6.Visible = False
                Case 192
                    rdbDIPart1.Visible = True
                    rdbDIPart2.Visible = True
                    rdbDIPart3.Visible = True
                    rdbDIPart4.Visible = True
                    rdbDIPart5.Visible = True
                    rdbDIPart6.Visible = True
            End Select
        End If
        If gDOCollection.Cards.DOCardParameter.Count = 0 Then
            tabIOTest.Controls.Remove(tabDO)
        Else
            Select Case gDOCollection.TotalBits
                Case 32
                    rdbDOPart1.Visible = True
                    rdbDOPart2.Visible = False
                    rdbDOPart3.Visible = False
                    rdbDOPart4.Visible = False
                    rdbDOPart5.Visible = False
                    rdbDOPart6.Visible = False
                Case 64
                    rdbDOPart1.Visible = True
                    rdbDOPart2.Visible = True
                    rdbDOPart3.Visible = False
                    rdbDOPart4.Visible = False
                    rdbDOPart5.Visible = False
                    rdbDOPart6.Visible = False
                Case 96
                    rdbDOPart1.Visible = True
                    rdbDOPart2.Visible = True
                    rdbDOPart3.Visible = True
                    rdbDOPart4.Visible = False
                    rdbDOPart5.Visible = False
                    rdbDOPart6.Visible = False
                Case 128
                    rdbDOPart1.Visible = True
                    rdbDOPart2.Visible = True
                    rdbDOPart3.Visible = True
                    rdbDOPart4.Visible = True
                    rdbDOPart5.Visible = False
                    rdbDOPart6.Visible = False
                Case 160
                    rdbDOPart1.Visible = True
                    rdbDOPart2.Visible = True
                    rdbDOPart3.Visible = True
                    rdbDOPart4.Visible = True
                    rdbDOPart5.Visible = True
                    rdbDOPart6.Visible = False
                Case 192
                    rdbDOPart1.Visible = True
                    rdbDOPart2.Visible = True
                    rdbDOPart3.Visible = True
                    rdbDOPart4.Visible = True
                    rdbDOPart5.Visible = True
                    rdbDOPart6.Visible = True
            End Select
        End If
        rdbDIPart1.Checked = True
        rdbDOPart1.Checked = True
        IOListTimer.Enabled = True
    End Sub

    Private Sub IOListTimer_Tick(sender As Object, e As EventArgs) Handles IOListTimer.Tick
        ShowDIStatus()
        ShowDOStatus()
        ShowAIStatus()
    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnOK.Click, btnCancel.Click, btnExit.Click, Button1.Click
        gSyslog.Save("[frmIOList]" & vbTab & "[btnPreviousPage]" & vbTab & "Click")
        Me.Dispose(True)
    End Sub

#Region "DI介面相關"
    ''' <summary>DI基準點位 </summary>
    ''' <remarks></remarks>
    Dim rdbDIPart As Integer

    ''' <summary>跳出DI設定介面</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ShowFrmDIConfig(sender As Object, e As EventArgs) Handles _
        lblDI0.Click, lblDI1.Click, lblDI2.Click, lblDI3.Click, lblDI4.Click, lblDI5.Click, lblDI6.Click, lblDI7.Click, lblDI8.Click, lblDI9.Click, _
        lblDI10.Click, lblDI11.Click, lblDI12.Click, lblDI13.Click, lblDI14.Click, lblDI15.Click, lblDI16.Click, lblDI17.Click, lblDI18.Click, lblDI19.Click, _
        lblDI20.Click, lblDI21.Click, lblDI22.Click, lblDI23.Click, lblDI24.Click, lblDI25.Click, lblDI26.Click, lblDI27.Click, lblDI28.Click, lblDI29.Click, _
        lblDI30.Click, lblDI31.Click

        If gUserLevel > enmUserLevel.eAdministrator Then
            Exit Sub
        End If

        Dim lbl As System.Windows.Forms.Label = CType(sender, System.Windows.Forms.Label)
        frmDICfg = New frmDIConfig
        With frmDICfg
            .DIIndex = lbl.Tag ' + rdbDIPart

            '預期顯示左上角位置
            Dim scrPos As New Point(MousePosition.X - .btnCancel.Location.X - .btnCancel.Width * 0.5, MousePosition.Y - .btnCancel.Location.Y - .btnCancel.Height * 0.5)
            '預期顯示右下角位置
            Dim RefPosX2 As Double = scrPos.X + .Size.Width
            '預期顯示右下角位置
            Dim RefPosY2 As Double = scrPos.Y + .Size.Height
            '螢幕邊界
            Dim BoundPosX As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
            '螢幕邊界
            Dim BoundPosY As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
            '超出範圍時, 界定右下角仍在顯示範圍內.
            If RefPosX2 > BoundPosX Then scrPos.X = BoundPosX - .Size.Width
            If RefPosY2 > BoundPosY Then scrPos.Y = BoundPosY - .Size.Height
            If scrPos.X < 0 Then scrPos.X = 0
            If scrPos.Y < 0 Then scrPos.Y = 0
            .StartPosition = Windows.Forms.FormStartPosition.Manual
            .Location = New Point(scrPos.X, scrPos.Y)

            .ShowDialog()
        End With

    End Sub

    ''' <summary>更新DI接點狀態</summary>
    ''' <remarks></remarks>
    Public Sub ShowDIStatus()

        Dim mChNo As Long

        Dim mUpperBound As Integer = 127
        If gDICollection.TotalBits > 0 Then
            mUpperBound = gDICollection.TotalBits - 1
        End If
        Dim mChannelValue(mUpperBound) As Boolean
        For mChNo = 0 To gDICollection.TotalBits - 1 '目前DI只使用32組
            mChannelValue(mChNo) = gDICollection.GetState(mChNo, False)
        Next

        UpdateShapeFillColor(chk_DIAddress_0, mChannelValue(rdbDIPart + 0))
        UpdateShapeFillColor(chk_DIAddress_1, mChannelValue(rdbDIPart + 1))
        UpdateShapeFillColor(chk_DIAddress_2, mChannelValue(rdbDIPart + 2))
        UpdateShapeFillColor(chk_DIAddress_3, mChannelValue(rdbDIPart + 3))
        UpdateShapeFillColor(chk_DIAddress_4, mChannelValue(rdbDIPart + 4))
        UpdateShapeFillColor(chk_DIAddress_5, mChannelValue(rdbDIPart + 5))
        UpdateShapeFillColor(chk_DIAddress_6, mChannelValue(rdbDIPart + 6))
        UpdateShapeFillColor(chk_DIAddress_7, mChannelValue(rdbDIPart + 7))
        UpdateShapeFillColor(chk_DIAddress_8, mChannelValue(rdbDIPart + 8))
        UpdateShapeFillColor(chk_DIAddress_9, mChannelValue(rdbDIPart + 9))
        UpdateShapeFillColor(chk_DIAddress_10, mChannelValue(rdbDIPart + 10))
        UpdateShapeFillColor(chk_DIAddress_11, mChannelValue(rdbDIPart + 11))
        UpdateShapeFillColor(chk_DIAddress_12, mChannelValue(rdbDIPart + 12))
        UpdateShapeFillColor(chk_DIAddress_13, mChannelValue(rdbDIPart + 13))
        UpdateShapeFillColor(chk_DIAddress_14, mChannelValue(rdbDIPart + 14))
        UpdateShapeFillColor(chk_DIAddress_15, mChannelValue(rdbDIPart + 15))
        UpdateShapeFillColor(chk_DIAddress_16, mChannelValue(rdbDIPart + 16))
        UpdateShapeFillColor(chk_DIAddress_17, mChannelValue(rdbDIPart + 17))
        UpdateShapeFillColor(chk_DIAddress_18, mChannelValue(rdbDIPart + 18))
        UpdateShapeFillColor(chk_DIAddress_19, mChannelValue(rdbDIPart + 19))
        UpdateShapeFillColor(chk_DIAddress_20, mChannelValue(rdbDIPart + 20))
        UpdateShapeFillColor(chk_DIAddress_21, mChannelValue(rdbDIPart + 21))
        UpdateShapeFillColor(chk_DIAddress_22, mChannelValue(rdbDIPart + 22))
        UpdateShapeFillColor(chk_DIAddress_23, mChannelValue(rdbDIPart + 23))
        UpdateShapeFillColor(chk_DIAddress_24, mChannelValue(rdbDIPart + 24))
        UpdateShapeFillColor(chk_DIAddress_25, mChannelValue(rdbDIPart + 25))
        UpdateShapeFillColor(chk_DIAddress_26, mChannelValue(rdbDIPart + 26))
        UpdateShapeFillColor(chk_DIAddress_27, mChannelValue(rdbDIPart + 27))
        UpdateShapeFillColor(chk_DIAddress_28, mChannelValue(rdbDIPart + 28))
        UpdateShapeFillColor(chk_DIAddress_29, mChannelValue(rdbDIPart + 29))
        UpdateShapeFillColor(chk_DIAddress_30, mChannelValue(rdbDIPart + 30))
        UpdateShapeFillColor(chk_DIAddress_31, mChannelValue(rdbDIPart + 31))

    End Sub

    ''' <summary>
    ''' 更新按鍵控制項的背景圖
    ''' </summary>
    ''' <param name="shape"></param>
    ''' <param name="bool"></param>
    ''' <remarks></remarks>
    Sub UpdateShapeFillColor(ByRef shape As System.Windows.Forms.Button, ByVal bool As Boolean)
        If bool Then
            shape.BackgroundImage = My.Resources.Green '.circle_green.ToBitmap
        Else
            shape.BackgroundImage = My.Resources.Grey
        End If
    End Sub


    ''' <summary>更新介面顯示</summary>
    ''' <param name="part">顯示基準點位</param>
    ''' <remarks></remarks>
    Sub SetDIText(ByVal part As Integer)
        For mControlNo As Integer = 0 To 31
            If Me.tabDI.Controls.ContainsKey("lblDI" & mControlNo) Then
                Dim mDINo As Integer = part + mControlNo
                Me.tabDI.Controls("lblDI" & mControlNo).Tag = mDINo
                If mDINo < gDICollection.DIParameter.Count Then
                    Me.tabDI.Controls("lblDI" & mControlNo).Text = gDICollection.DIParameter(mDINo).Name
                Else
                    Me.tabDI.Controls("lblDI" & mControlNo).Text = "(Spare)"
                End If
            End If
        Next
    End Sub
    Private Sub rdbDIPart1_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDIPart1.CheckedChanged
        rdbDIPart = 0
        SetDIText(rdbDIPart)
    End Sub

    Private Sub rdbDIPart2_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDIPart2.CheckedChanged
        rdbDIPart = 32
        SetDIText(rdbDIPart)
    End Sub

    Private Sub rdbDIPart3_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDIPart3.CheckedChanged
        rdbDIPart = 64
        SetDIText(rdbDIPart)
    End Sub

    Private Sub rdbDIPart4_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDIPart4.CheckedChanged
        rdbDIPart = 96
        SetDIText(rdbDIPart)
    End Sub
    Private Sub rdbDIPart5_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDIPart5.CheckedChanged
        rdbDIPart = 128
        SetDIText(rdbDIPart)
    End Sub

    Private Sub rdbDIPart6_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDIPart6.CheckedChanged
        rdbDIPart = 160
        SetDIText(rdbDIPart)
    End Sub

#End Region

#Region "DO介面相關"
    ''' <summary>DO基準點位</summary>
    ''' <remarks></remarks>
    Dim rdbDOPart As Integer

    ''' <summary>
    ''' 更新DO接點狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowDOStatus()

        Dim mDoNo As Long

        Dim mChannelValue(gDOCollection.TotalBits) As Boolean
        Dim tempBitCount As Integer
        If gDOCollection.TotalBits > 32 Then
            tempBitCount = 32
        Else
            tempBitCount = gDOCollection.TotalBits
        End If

        For mDoNo = 0 To gDOCollection.TotalBits - 1
            mChannelValue(mDoNo) = gDOCollection.RawValue(gDOCollection.DOParameter(mDoNo).IPAddress, gDOCollection.DOParameter(mDoNo).Port, gDOCollection.DOParameter(mDoNo).Bits)
        Next

        For mDoNo = 0 To 31
            If Me.tabDO.Controls.ContainsKey("chk_DoAddress_" & mDoNo) Then
                Dim mDoId As Integer = mDoNo + rdbDOPart
                If mDoId <= mChannelValue.GetUpperBound(0) Then
                    If mChannelValue(mDoNo + rdbDOPart) = True Then
                        Me.tabDO.Controls("chk_DoAddress_" & mDoNo).BackColor = Color.Lime
                    Else
                        Me.tabDO.Controls("chk_DoAddress_" & mDoNo).BackColor = SystemColors.Control
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub ShowFrmDOConfig(sender As Object, e As EventArgs) Handles _
       lblDO0.Click, lblDO1.Click, lblDO2.Click, lblDO3.Click, lblDO4.Click, lblDO5.Click, lblDO6.Click, lblDO7.Click, lblDO8.Click, lblDO9.Click, _
       lblDO10.Click, lblDO11.Click, lblDO12.Click, lblDO13.Click, lblDO14.Click, lblDO15.Click, lblDO16.Click, lblDO17.Click, lblDO18.Click, lblDO19.Click, _
       lblDO20.Click, lblDO21.Click, lblDO22.Click, lblDO23.Click, lblDO24.Click, lblDO25.Click, lblDO26.Click, lblDO27.Click, lblDO28.Click, lblDO29.Click, _
       lblDO30.Click, lblDO31.Click


        If gUserLevel > enmUserLevel.eAdministrator Then
            Exit Sub
        End If
        Dim lbl As System.Windows.Forms.Label = CType(sender, System.Windows.Forms.Label)

        frmDOCfg = New frmDOConfig
        With frmDOCfg
            .DOIndex = lbl.Tag '+ rdbDOPart

            '預期顯示左上角位置
            Dim scrPos As New Point(MousePosition.X - .btnCancel.Location.X - .btnCancel.Width * 0.5, MousePosition.Y - .btnCancel.Location.Y - .btnCancel.Height * 0.5)
            '預期顯示右下角位置
            Dim RefPosX2 As Double = scrPos.X + .Size.Width
            '預期顯示右下角位置
            Dim RefPosY2 As Double = scrPos.Y + .Size.Height
            '螢幕邊界
            Dim BoundPosX As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
            '螢幕邊界
            Dim BoundPosY As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
            '超出範圍時, 界定右下角仍在顯示範圍內.
            If RefPosX2 > BoundPosX Then scrPos.X = BoundPosX - .Size.Width
            If RefPosY2 > BoundPosY Then scrPos.Y = BoundPosY - .Size.Height
            If scrPos.X < 0 Then scrPos.X = 0
            If scrPos.Y < 0 Then scrPos.Y = 0
            .StartPosition = Windows.Forms.FormStartPosition.Manual
            .Location = New Point(scrPos.X, scrPos.Y)

            .ShowDialog()
        End With

    End Sub

    ''' <summary>反轉輸出</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToggleOutput(sender As Object, e As EventArgs) Handles _
        chk_DoAddress_0.Click, chk_DoAddress_1.Click, chk_DoAddress_2.Click, chk_DoAddress_3.Click, chk_DoAddress_4.Click, _
        chk_DoAddress_5.Click, chk_DoAddress_6.Click, chk_DoAddress_7.Click, chk_DoAddress_8.Click, chk_DoAddress_9.Click, _
        chk_DoAddress_10.Click, chk_DoAddress_11.Click, chk_DoAddress_12.Click, chk_DoAddress_13.Click, chk_DoAddress_14.Click, _
        chk_DoAddress_15.Click, chk_DoAddress_16.Click, chk_DoAddress_17.Click, chk_DoAddress_18.Click, chk_DoAddress_19.Click, _
        chk_DoAddress_20.Click, chk_DoAddress_21.Click, chk_DoAddress_22.Click, chk_DoAddress_23.Click, chk_DoAddress_24.Click, _
        chk_DoAddress_25.Click, chk_DoAddress_26.Click, chk_DoAddress_27.Click, chk_DoAddress_28.Click, chk_DoAddress_29.Click, _
        chk_DoAddress_30.Click, chk_DoAddress_31.Click

        gDOCollection.ToggleOutput(CType(sender, System.Windows.Forms.Button).Tag + rdbDOPart)
    End Sub


    ''' <summary>更新介面顯示</summary>
    ''' <param name="part">顯示基準點位</param>
    ''' <remarks></remarks>
    Sub SetDOText(ByVal part As Integer)
        For mControlNo As Integer = 0 To 31
            If Me.tabDO.Controls.ContainsKey("lblDO" & mControlNo) Then
                Dim mDONo As Integer = part + mControlNo
                Me.tabDO.Controls("lblDO" & mControlNo).Tag = mDONo
                If mDONo < gDOCollection.DOParameter.Count Then
                    Me.tabDO.Controls("lblDO" & mControlNo).Text = gDOCollection.DOParameter(mDONo).Name
                Else
                    Me.tabDO.Controls("lblDO" & mControlNo).Text = "(Spare)"
                End If
            End If
        Next
    End Sub

    Private Sub rdbDOPart1_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDOPart1.CheckedChanged
        rdbDOPart = 0
        SetDOText(rdbDOPart)
    End Sub

    Private Sub rdbDOPart2_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDOPart2.CheckedChanged
        rdbDOPart = 32
        SetDOText(rdbDOPart)
    End Sub

    Private Sub rdbDOPart3_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDOPart3.CheckedChanged
        rdbDOPart = 64
        SetDOText(rdbDOPart)
    End Sub

    Private Sub rdbDOPart4_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDOPart4.CheckedChanged
        rdbDOPart = 96

        SetDOText(rdbDOPart)
    End Sub

    Private Sub rdbDOPart5_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDOPart5.CheckedChanged
        rdbDOPart = 128

        SetDOText(rdbDOPart)
    End Sub

    Private Sub rdbDOPart6_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDOPart6.CheckedChanged
        rdbDOPart = 160
        SetDOText(rdbDOPart)
    End Sub

#End Region

#Region "AI介面相關"
    Private Sub ShowFrmAIConfig(sender As Object, e As EventArgs) Handles lblAI0.Click, lblAI1.Click, lblAI2.Click, lblAI3.Click, lblAI4.Click, lblAI5.Click, lblAI6.Click, lblAI7.Click
        If gUserLevel = enmUserLevel.eOperator Then
            Exit Sub
        End If

        Dim lbl As System.Windows.Forms.Label = CType(sender, System.Windows.Forms.Label)
        With frmAICfg
            frmAICfg = New frmAIConfig
            .AIIndex = lbl.Tag
            '預期顯示左上角位置
            Dim scrPos As New Point(MousePosition.X - .btnCancel.Location.X - .btnCancel.Width * 0.5, MousePosition.Y - .btnCancel.Location.Y - .btnCancel.Height * 0.5)
            '預期顯示右下角位置
            Dim RefPosX2 As Double = scrPos.X + .Size.Width
            '預期顯示右下角位置
            Dim RefPosY2 As Double = scrPos.Y + .Size.Height
            '螢幕邊界
            Dim BoundPosX As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
            '螢幕邊界
            Dim BoundPosY As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
            '超出範圍時, 界定右下角仍在顯示範圍內.
            If RefPosX2 > BoundPosX Then scrPos.X = BoundPosX - .Size.Width
            If RefPosY2 > BoundPosY Then scrPos.Y = BoundPosY - .Size.Height
            If scrPos.X < 0 Then scrPos.X = 0
            If scrPos.Y < 0 Then scrPos.Y = 0
            .StartPosition = Windows.Forms.FormStartPosition.Manual
            .Location = New Point(scrPos.X, scrPos.Y)

            .ShowDialog()
        End With

    End Sub

    ''' <summary>
    ''' 顯示AI接點狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowAIStatus()
        lblAIU0.Text = NaNConverter(gAICollection.Value(0))
        lblAIU1.Text = NaNConverter(gAICollection.Value(1))
        lblAIU2.Text = NaNConverter(gAICollection.Value(2))
        lblAIU3.Text = NaNConverter(gAICollection.Value(3))
        lblAIU4.Text = NaNConverter(gAICollection.Value(4))
        lblAIU5.Text = NaNConverter(gAICollection.Value(5))
        lblAIU6.Text = NaNConverter(gAICollection.Value(6))
        lblAIU7.Text = NaNConverter(gAICollection.Value(7))

    End Sub
    ''' <summary>
    ''' 非數值轉字串
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function NaNConverter(ByVal value As Double) As String
        If Double.IsNaN(value) Then
            Return "NaN"
        End If
        Return value.ToString("#0.00")
    End Function


#End Region

#Region "AO介面相關"
    Private Sub ShowFrmAOConfig(sender As Object, e As EventArgs) Handles lblAO0.Click, lblAO1.Click, lblAO2.Click, lblAO3.Click, lblAO4.Click, lblAO5.Click, lblAO6.Click, lblAO7.Click
        If gUserLevel = enmUserLevel.eOperator Then
            Exit Sub
        End If

        Dim lbl As System.Windows.Forms.Label = CType(sender, System.Windows.Forms.Label)
        frmAOCfg = New frmAOConfig
        With frmAOCfg
            .AOIndex = lbl.Tag
            '預期顯示左上角位置
            Dim scrPos As New Point(MousePosition.X - .btnCancel.Location.X - .btnCancel.Width * 0.5, MousePosition.Y - .btnCancel.Location.Y - .btnCancel.Height * 0.5)
            '預期顯示右下角位置
            Dim RefPosX2 As Double = scrPos.X + .Size.Width
            '預期顯示右下角位置
            Dim RefPosY2 As Double = scrPos.Y + .Size.Height
            '螢幕邊界
            Dim BoundPosX As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
            '螢幕邊界
            Dim BoundPosY As Double = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
            '超出範圍時, 界定右下角仍在顯示範圍內.
            If RefPosX2 > BoundPosX Then scrPos.X = BoundPosX - .Size.Width
            If RefPosY2 > BoundPosY Then scrPos.Y = BoundPosY - .Size.Height
            If scrPos.X < 0 Then scrPos.X = 0
            If scrPos.Y < 0 Then scrPos.Y = 0
            .StartPosition = Windows.Forms.FormStartPosition.Manual
            .Location = New Point(scrPos.X, scrPos.Y)

            .ShowDialog()
        End With

    End Sub

    Private Sub btnSetAO0_Click(sender As Object, e As EventArgs) Handles btnSetAO0.Click, btnSetAO1.Click, btnSetAO2.Click, btnSetAO3.Click, btnSetAO4.Click, btnSetAO5.Click, btnSetAO6.Click, btnSetAO7.Click
        Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)
        Dim AONo As Integer = btn.Tag
        If AONo < 0 Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Data Not Support!")
            Exit Sub
        End If
        If AONo > gAOCollection.Value.Count - 1 Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'MsgBox("Data Not Support!")
            Exit Sub
        End If

        Select Case AONo
            Case 0
                gAOCollection.Value(0) = Val(txtAOU0.Text)
            Case 1
                gAOCollection.Value(1) = Val(txtAOU1.Text)
            Case 2
                gAOCollection.Value(2) = Val(txtAOU2.Text)
            Case 3
                gAOCollection.Value(3) = Val(txtAOU3.Text)
            Case 4
                gAOCollection.Value(4) = Val(txtAOU4.Text)
            Case 5
                gAOCollection.Value(5) = Val(txtAOU5.Text)
            Case 10
                gAOCollection.Value(6) = Val(txtAOU6.Text)
            Case 7
                gAOCollection.Value(7) = Val(txtAOU7.Text)
        End Select

    End Sub

#End Region

End Class
Imports System
Imports ProjectCore
Imports ProjectAOI
Imports ProjectIO

Public Class ucCCDTargerEdit
    Public sys As sSysParam



    Public Sub Initial()

        cmbTargetType.SelectedIndex = 0
        cmbTargetColor.SelectedIndex = 0
        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        If gSSystemParameter.CCDTargetDataList.Count > 0 Then

            Dim mStep As DataGridViewRowCollection
            mStep = dgvStep.Rows
            For i As Integer = 0 To gSSystemParameter.CCDTargetDataList.Count - 1
                'Dim tempData As sCCDTargetData
                Dim CCDTargetType As String = GetCCDTargetTypeString(gSSystemParameter.CCDTargetDataList(i).CCDTargetType)
                Dim CCDTargetColor As String = GetCCDTargetColorString(gSSystemParameter.CCDTargetDataList(i).CCDTargetColor)
                Dim Radius As Decimal = gSSystemParameter.CCDTargetDataList(i).Radius
                Dim Height As Decimal = gSSystemParameter.CCDTargetDataList(i).Height
                Dim Width As Decimal = gSSystemParameter.CCDTargetDataList(i).Width
                mStep.Add(CCDTargetType, CCDTargetColor, Radius, Height, Width)
            Next

        End If

        If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
        End If
    End Sub

    Public Sub ManualDispose()
        UcDisplay1.EndLive(sys.CCDNo)
        Me.Dispose()
    End Sub

    Private Sub BtnControl(ByVal Status As Boolean)

        btnAdd.Enabled = Status
        btnDelete.Enabled = Status
        btnSave.Enabled = Status

    End Sub

    Private Sub SetRadiusEnable(ByVal Status As Boolean)
        If Status = False Then
            nmcRadius.Value = 0
        End If
        lblRadiusUnit.Enabled = Status
        lblRadius.Enabled = Status
        nmcRadius.Enabled = Status
    End Sub

    Private Sub SetWHEnable(ByVal Status As Boolean)
        If Status = False Then
            nmcHeight.Value = 0
            nmcWidth.Value = 0
        End If
        lblHeight.Enabled = Status
        lblHeightUnit.Enabled = Status
        nmcHeight.Enabled = Status

        lblWidth.Enabled = Status
        lblWidthUnit.Enabled = Status
        nmcWidth.Enabled = Status
    End Sub


    Private Function GetCCDTargetTypeString(ByVal stringType As enmCCDTargetType) As String
        Select Case stringType
            Case enmCCDTargetType.Cross
                Return "Cross"
            Case enmCCDTargetType.CrossX
                Return "CrossX"
            Case enmCCDTargetType.TickMark
                Return "TickMark"
            Case enmCCDTargetType.TickMarkX
                Return "TickMarkX"
            Case enmCCDTargetType.Circle
                Return "Circle"
            Case enmCCDTargetType.Rectangle
                Return "Rectangle"
            Case Else
                Return "None"
        End Select
    End Function


    Private Function GetCCDTargetType(ByVal stringType As String) As enmCCDTargetType
        Select Case stringType
            Case "Cross"
                Return enmCCDTargetType.Cross
            Case "CrossX"
                Return enmCCDTargetType.CrossX
            Case "TickMark"
                Return enmCCDTargetType.TickMark
            Case "TickMarkX"
                Return enmCCDTargetType.TickMarkX
            Case "Circle"
                Return enmCCDTargetType.Circle
            Case "Rectangle"
                Return enmCCDTargetType.Rectangle
            Case Else
                Return enmCCDTargetType.None
        End Select
    End Function

    Public Function GetCCDTargetColorString(ByVal stringType As enmCCDTargetColor) As String
        Select Case stringType
            Case enmCCDTargetColor.Black
                Return "Black"
            Case enmCCDTargetColor.Blue
                Return "Blue"
            Case enmCCDTargetColor.Red
                Return "Red"
            Case enmCCDTargetColor.Yellow
                Return "Yellow"
            Case Else
                Return "None"
        End Select
    End Function

    Public Function GetCCDTargetColor(ByVal stringType As String) As enmCCDTargetColor
        Select Case stringType
            Case "Black"
                Return enmCCDTargetColor.Black
            Case "Blue"
                Return enmCCDTargetColor.Blue
            Case "Red"
                Return enmCCDTargetColor.Red
            Case "Yellow"
                Return enmCCDTargetColor.Yellow
            Case Else
                Return enmCCDTargetColor.None
        End Select
    End Function


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        BtnControl(False)

        Dim tmpType As enmCCDTargetType
        tmpType = cmbTargetType.SelectedIndex
        Select Case tmpType
            Case enmCCDTargetType.Circle
                If nmcRadius.Value = 0 Then
                    MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    BtnControl(True)
                    Exit Sub
                End If
            Case enmCCDTargetType.TickMark
                If nmcRadius.Value = 0 Then
                    MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    BtnControl(True)
                    Exit Sub
                End If
            Case enmCCDTargetType.Rectangle
                If nmcHeight.Value = 0 Or nmcWidth.Value = 0 Then
                    MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    BtnControl(True)
                    Exit Sub
                End If
        End Select


        ''dgvStep.c()
        Dim mStep As DataGridViewRowCollection
        mStep = dgvStep.Rows
        mStep.Add(cmbTargetType.SelectedItem.ToString, cmbTargetColor.SelectedItem.ToString, nmcRadius.Value, nmcWidth.Value, nmcHeight.Value)
        Dim mTargetData As sCCDTargetData
        mTargetData.CCDTargetType = GetCCDTargetType(cmbTargetType.SelectedItem.ToString)
        mTargetData.CCDTargetColor = GetCCDTargetColor(cmbTargetColor.SelectedItem.ToString)
        mTargetData.Radius = nmcRadius.Value
        mTargetData.Width = nmcWidth.Value
        mTargetData.Height = nmcHeight.Value
        Dim mTargetDataList As List(Of sCCDTargetData) = New List(Of sCCDTargetData)
        mTargetDataList.Add(mTargetData)
        UcDisplay1.DrawTarget(sys.CCDNo, mTargetDataList)

        BtnControl(True)
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        BtnControl(False)

        If dgvStep.SelectedCells(0).RowIndex < 0 Then '未選Step
            If dgvStep.Rows.Count > 0 Then
                dgvStep.Rows(0).Selected = True
            End If
            '請先選取Pattern Step
            MsgBox(gMsgHandler.GetMessage(Warn_3000014), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            BtnControl(True)
            Exit Sub
        End If
        '[Note]先刪除暫存資料
        Dim mStepNo As Integer = dgvStep.SelectedCells(0).RowIndex
        dgvStep.Rows.Remove(dgvStep.Rows(mStepNo))
        UcDisplay1.DrawClear(sys.CCDNo)
        '[Note重新讀取資料]
        Dim mtemp As sCCDTargetData
        Dim mtempList As List(Of sCCDTargetData) = New List(Of sCCDTargetData)
        For i As Integer = 0 To dgvStep.Rows.Count - 1
            mtemp.CCDTargetType = GetCCDTargetType(dgvStep.Rows.Item(i).Cells(0).Value)
            mtemp.CCDTargetColor = GetCCDTargetColor(dgvStep.Rows.Item(i).Cells(1).Value)
            mtemp.Radius = dgvStep.Rows.Item(i).Cells(2).Value
            mtemp.Width = dgvStep.Rows.Item(i).Cells(3).Value
            mtemp.Height = dgvStep.Rows.Item(i).Cells(4).Value
            mtempList.Add(mtemp)
        Next
        UcDisplay1.DrawTarget(sys.CCDNo, mtempList)

        BtnControl(True)
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        BtnControl(False)

        gSSystemParameter.CCDTargetDataList.Clear()
        Dim mtemp As sCCDTargetData
        For i As Integer = 0 To dgvStep.Rows.Count - 1
            mtemp.CCDTargetType = GetCCDTargetType(dgvStep.Rows.Item(i).Cells(0).Value)
            mtemp.CCDTargetColor = GetCCDTargetColor(dgvStep.Rows.Item(i).Cells(1).Value)
            mtemp.Radius = dgvStep.Rows.Item(i).Cells(2).Value
            mtemp.Width = dgvStep.Rows.Item(i).Cells(3).Value
            mtemp.Height = dgvStep.Rows.Item(i).Cells(4).Value
            gSSystemParameter.CCDTargetDataList.Add(mtemp)
        Next

        gSSystemParameter.SaveCCDTargetData(Application.StartupPath & "\system\" & MachineName & "\SysCCDTarget.ini")

        BtnControl(True)
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")
    End Sub

    Private Sub cmbTargetType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTargetType.SelectedIndexChanged

        If cmbTargetType.SelectedIndex < 0 Then
            Exit Sub
        End If

        Dim tempType As enmCCDTargetType
        tempType = cmbTargetType.SelectedIndex
        Select Case tempType
            Case enmCCDTargetType.Cross
                SetRadiusEnable(False)
                SetWHEnable(False)
            Case enmCCDTargetType.CrossX
                SetRadiusEnable(False)
                SetWHEnable(False)
            Case enmCCDTargetType.TickMark
                SetRadiusEnable(True)
                SetWHEnable(False)
            Case enmCCDTargetType.TickMarkX
                SetRadiusEnable(True)
                SetWHEnable(False)
            Case enmCCDTargetType.Circle
                SetRadiusEnable(True)
                SetWHEnable(False)
            Case enmCCDTargetType.Rectangle
                SetRadiusEnable(False)
                SetWHEnable(True)
        End Select

    End Sub
End Class

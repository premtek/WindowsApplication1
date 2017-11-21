Imports ProjectCore

Public Class frmMessageConfig

    Sub SetColumn(ByVal colName As String, ByVal isVisible As Boolean, ByVal isReadonly As Boolean)
        If Not DataGridView1.Columns.Contains(colName) Then
            Exit Sub
        End If
        With DataGridView1.Columns(colName)
            .Visible = isVisible
            .ReadOnly = isReadonly
        End With
    End Sub

    Private Sub frmMessageConfig_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Select Case gUserLevel
            Case enmUserLevel.eOperator 'OP只能看到ALID與訊息對照,無法編輯
                SetColumn("colAlid", True, True)
                SetColumn("colEnabled", False, True)
                'SetColumn("colLevel", False, True)
                SetColumn("colMsg1", True, True)
                SetColumn("colMsg2", True, True)
                SetColumn("colMsg3", True, True)

            Case enmUserLevel.eEngineer '工程師只能看到ALID,層級,訊息對照,無法編輯
                SetColumn("colAlid", True, True)
                SetColumn("colEnabled", False, True)
                'SetColumn("colLevel", True, True)
                SetColumn("colMsg1", True, True)
                SetColumn("colMsg2", True, True)
                SetColumn("colMsg3", True, True)

            Case enmUserLevel.eManager '管理者只能看到ALID,層級,訊息對照,,可編輯層級,訊息
                SetColumn("colAlid", True, True)
                SetColumn("colEnabled", False, True)
                ' SetColumn("colLevel", True, False)
                SetColumn("colMsg1", True, False)
                SetColumn("colMsg2", True, False)
                SetColumn("colMsg3", True, False)

            Case enmUserLevel.eAdministrator '客服只能看到ALID,層級,訊息對照,,可編輯層級,訊息
                SetColumn("colAlid", True, True)
                SetColumn("colEnabled", False, True)
                ' SetColumn("colLevel", True, False)
                SetColumn("colMsg1", True, False)
                SetColumn("colMsg2", True, False)
                SetColumn("colMsg3", True, False)

            Case enmUserLevel.eSoftwareMaker '軟體工程師,全可見可編輯
                SetColumn("colAlid", True, False)
                SetColumn("colEnabled", True, False)
                'SetColumn("colLevel", True, False)
                SetColumn("colMsg1", True, False)
                SetColumn("colMsg2", True, False)
                SetColumn("colMsg3", True, False)

        End Select
    End Sub
    'Dim mDataTable As New DataTable("Message")
    Private Sub frmMessageConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            DataGridView1.Columns.Clear()
            DataGridView1.Columns.Add("colAlid", "ALID")

            Dim colEnabled As New DataGridViewComboBoxColumn
            colEnabled.Name = "colEnabled"
            colEnabled.HeaderText = "Enabled"
            colEnabled.Items.Add("On")
            colEnabled.Items.Add("Off")
            DataGridView1.Columns.Add(colEnabled)
            'Dim colLevel As New DataGridViewComboBoxColumn
            'colLevel.Name = "colLevel"
            'colLevel.HeaderText = "Level"
            'colLevel.Items.Add("Alarm")
            'colLevel.Items.Add("Warn")
            'DataGridView1.Columns.Add(colLevel)
            DataGridView1.Columns.Add("colMsg1", "語系1")
            DataGridView1.Columns.Add("colMsg2", "語系2")
            DataGridView1.Columns.Add("colMsg3", "語系3")

            DataGridView1.Columns("colAlid").Frozen = True
            DataGridView1.Columns("colEnabled").Frozen = False
            'DataGridView1.Columns("colLevel").Frozen = False
            DataGridView1.Columns("colMsg1").Frozen = False
            DataGridView1.Columns("colMsg2").Frozen = False
            DataGridView1.Columns("colMsg3").Frozen = False

            For mRow As Integer = 0 To gMsgHandler.MsgDictionary.Keys.Count - 1
                Dim ALID As String = gMsgHandler.MsgDictionary.Keys(mRow)
                Dim Message1 As String = ""
                Dim Message2 As String = ""
                Dim Message3 As String = ""
                Dim Enable As String = "On"
                ' Dim level As String = "Heavy"
                If gMsgHandler.MsgDictionary.ContainsKey(ALID) Then
                    Message1 = gMsgHandler.MsgDictionary(ALID).Msg1
                    Message2 = gMsgHandler.MsgDictionary(ALID).Msg2
                    Message3 = gMsgHandler.MsgDictionary(ALID).Msg3
                    Enable = IIf(gMsgHandler.MsgDictionary(ALID).Enabled = "1", "On", "Off")
                    'Select Case gMsgHandler.MsgDictionary(ALID).Level
                    '    Case "Heavy", "Alarm"
                    '        level = "Alarm"
                    '    Case "Light", "Warn"
                    '        level = "Warn"
                    'End Select

                End If

                'DataGridView1.Rows.Add(ALID, Enable, level, Message1, Message2, Message3)
                DataGridView1.Rows.Add(ALID, Enable, Message1, Message2, Message3)
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Sue20170627
        gSyslog.Save("[frmMessageConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'Sue20170627
        gSyslog.Save("[frmMessageConfig]" & vbTab & "[btnOK]" & vbTab & "Click")
        For mRow As Integer = 0 To gMsgHandler.MsgDictionary.Keys.Count - 1
            Dim tmp As MessageStructure = gMsgHandler.MsgDictionary(gMsgHandler.MsgDictionary.Keys(mRow))
            With tmp
                Dim row As DataGridViewRow = DataGridView1.Rows(mRow)
                .ALID = row.Cells.Item(0).Value
                .Enabled = IIf(row.Cells.Item(1).Value = "On", "1", "0")
                '.Level = row.Cells.Item(2).Value
                .Msg1 = row.Cells.Item(2).Value
                .Msg2 = row.Cells.Item(3).Value
                .Msg3 = row.Cells.Item(4).Value
                gMsgHandler.MsgDictionary(.ALID) = tmp
            End With

        Next
        'gMsgHandler.SaveLevel(Application.StartupPath & "\EqpInitData\EqpAlarm2.csv")
        gMsgHandler.Save(Application.StartupPath & "\EqpInitData\EqpAlarm.csv")
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
    End Sub
End Class
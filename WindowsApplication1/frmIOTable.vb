Imports ProjectCore
Imports ProjectIO

Public Class frmIOTable
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIOTable))
    Private Sub frmIOTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rowData(4) As Object
        '--- Soni + 2015.03.20 DI一覽表 ---
        dgvDITable.Rows.Clear()

        For mDINo As Integer = 0 To gDICollection.DIParameter.Count - 1 'enmDI.Max
            If mDINo < gDICollection.DIParameter.Count Then
                rowData(0) = gDICollection.DIParameter(mDINo).Name
                rowData(1) = gDICollection.DIParameter(mDINo).Address
                rowData(2) = IIf(gDICollection.GetState(mDINo, False), myResource.GetString("On"), myResource.GetString("Off"))
                rowData(3) = gDICollection.DIParameter(mDINo).CardType
                rowData(4) = IIf(gDICollection.DIParameter(mDINo).ByPass, myResource.GetString("Yes"), myResource.GetString("No"))
                dgvDITable.Rows.Add(rowData)
            End If
        Next
        '--- Soni + 2015.03.20 DI一覽表 ---

        '--- Soni + 2015.03.20 DO一覽表 ---
        dgvDOTable.Rows.Clear()

        For mDONo As Integer = 0 To gDOCollection.DOParameter.Count - 1 'enmDO.Max
            If mDONo < gDOCollection.DOParameter.Count Then
                rowData(0) = gDOCollection.DOParameter(mDONo).Name
                rowData(1) = gDOCollection.DOParameter(mDONo).Address
                rowData(2) = IIf(gDOCollection.GetState(mDONo), myResource.GetString("On"), myResource.GetString("Off"))
                rowData(3) = gDOCollection.DOParameter(mDONo).CardType
                rowData(4) = IIf(gDOCollection.DOParameter(mDONo).ByPass, myResource.GetString("Yes"), myResource.GetString("No"))
                dgvDOTable.Rows.Add(rowData)
            End If

        Next
        '--- Soni + 2015.03.20 DO一覽表 ---

        '--- Soni + 2015.03.20 AI一覽表 ---
        dgvAITable.Rows.Clear()

        For mAINo As Integer = 0 To gAICollection.AIParameter.Count - 1 ' enmAI.Max
            If mAINo < gAICollection.AIParameter.Count Then
                rowData(0) = gAICollection.AIParameter(mAINo).Name
                rowData(1) = gAICollection.AIParameter(mAINo).Address
                rowData(2) = gAICollection.Value(mAINo).ToString("##0.###") '忽略小數後三位
                rowData(3) = gAICollection.AIParameter(mAINo).CardType
                rowData(4) = IIf(gAICollection.AIParameter(mAINo).ByPass, myResource.GetString("Yes"), myResource.GetString("No"))
                dgvAITable.Rows.Add(rowData)
            End If

        Next
        '--- Soni + 2015.03.20 AI一覽表 ---

        '--- Soni + 2015.03.20 AO一覽表 ---
        dgvAOTable.Rows.Clear()

        For mAONo As Integer = 0 To gAOCollection.AOParameter.Count - 1 'enmAO.Max
            If mAONo < gAOCollection.AOParameter.Count Then
                rowData(0) = gAOCollection.AOParameter(mAONo).Name
                rowData(1) = gAOCollection.AOParameter(mAONo).Address
                rowData(2) = gAOCollection.Value(mAONo).ToString("##0.###") '忽略小數後三位
                rowData(3) = gAOCollection.AOParameter(mAONo).CardType
                rowData(4) = IIf(gAOCollection.AOParameter(mAONo).ByPass, myResource.GetString("Yes"), myResource.GetString("No"))
                dgvAOTable.Rows.Add(rowData)
            End If

        Next
        '--- Soni + 2015.03.20 AO一覽表 ---
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_F230A, enmMachineType.DCS_350A, enmMachineType.DCS_500AD
                TabControl1.Controls.Remove(tabAITable)
                TabControl1.Controls.Remove(tabAOTable)

            Case Else

        End Select
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        gSyslog.Save("[frmIOTable]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Dispose(True)
    End Sub

End Class
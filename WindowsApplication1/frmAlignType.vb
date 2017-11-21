Imports ProjectCore
Imports ProjectAOI

Public Class frmAlignType
    Public Sys As sSysParam
    ''' <summary>CCD動態顯示頁籤</summary>

    Public mAlignType As Integer

    Public mScenePath As String

    Private Sub btn_Control(ByVal state As Boolean)
        btnAlign.Enabled = state
        btnCorner.Enabled = state
        btnCircle.Enabled = state
        btnLane.Enabled = state
        btnLoadFile.Enabled = state
        btnBlob.Enabled = state
    End Sub

    Private Sub frmAlignType_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmAlignType_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'e.Cancel = True
    End Sub

    Private Sub frmAlignType_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose(True)
    End Sub

    Private Sub btnAlign_Click(sender As Object, e As EventArgs) Handles btnAlign.Click
        If btnAlign.Enabled = False Then
            Exit Sub
        End If
        btn_Control(False)
        mAlignType = eAlignType.PMAlign
        btn_Control(True)
        Me.Close()
    End Sub

    Private Sub btnCorner_Click(sender As Object, e As EventArgs) Handles btnCorner.Click
        If btnCorner.Enabled = False Then
            Exit Sub
        End If
        btn_Control(False)
        mAlignType = eAlignType.Corner
        btn_Control(True)
        Me.Close()
    End Sub

    Private Sub btnCircle_Click(sender As Object, e As EventArgs) Handles btnCircle.Click
        If btnCircle.Enabled = False Then
            Exit Sub
        End If
        btn_Control(False)
        mAlignType = eAlignType.Circle
        btn_Control(True)
        Me.Close()
    End Sub

    Private Sub btnLane_Click(sender As Object, e As EventArgs) Handles btnLane.Click
        If btnLane.Enabled = False Then
            Exit Sub
        End If
        btn_Control(False)
        mAlignType = eAlignType.Lane
        btn_Control(True)
        Me.Close()
    End Sub

    Private Sub btnLoadFile_Click(sender As Object, e As EventArgs) Handles btnLoadFile.Click
        If btnLoadFile.Enabled = False Then
            Exit Sub
        End If
        btn_Control(False)
        mAlignType = eAlignType.LoadFile

        Dim DefaultDirectory As String
        DefaultDirectory = Application.StartupPath & "\Recipe\"


        '[Note]載入vpp
        With OFDLoadScene
            .InitialDirectory = DefaultDirectory
            .Filter = "(*.vpp)|*.vpp"
            .FilterIndex = 2
            .RestoreDirectory = True
            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                btnLoadFile.Enabled = True
                btn_Control(True)
                Exit Sub
            End If
            mScenePath = .FileName
        End With


        btn_Control(True)
        Me.Close()
    End Sub

    Private Sub btnBlob_Click(sender As Object, e As EventArgs) Handles btnBlob.Click
        If btnBlob.Enabled = False Then
            Exit Sub
        End If
        btn_Control(False)
        mAlignType = eAlignType.Blob
        btn_Control(True)
        Me.Close()
    End Sub
End Class
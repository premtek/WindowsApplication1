Public Class frmTiltSelect
    Dim fTilt As frmTilt
    Private Sub btnTiltA_Click(sender As Object, e As EventArgs) Handles btnTiltA.Click
        If btnTiltA.Enabled = False Then
            Exit Sub
        End If
        btnTiltA.Enabled = False
        fTilt._TiltSelect = enmTiltIndex.TiltA
        btnTiltA.Enabled = True
    End Sub

    Private Sub btnTiltB_Click(sender As Object, e As EventArgs) Handles btnTiltB.Click
        If btnTiltB.Enabled = False Then
            Exit Sub
        End If
        btnTiltB.Enabled = False
        fTilt._TiltSelect = enmTiltIndex.TiltB
        btnTiltB.Enabled = True
    End Sub

    Private Sub btnTiltC_Click(sender As Object, e As EventArgs) Handles btnTiltC.Click
        If btnTiltC.Enabled = False Then
            Exit Sub
        End If
        btnTiltC.Enabled = False
        fTilt._TiltSelect = enmTiltIndex.TiltC
        btnTiltC.Enabled = True
    End Sub

    Private Sub btnTiltD_Click(sender As Object, e As EventArgs) Handles btnTiltD.Click
        If btnTiltD.Enabled = False Then
            Exit Sub
        End If
        btnTiltD.Enabled = False
        fTilt._TiltSelect = enmTiltIndex.TiltD
        btnTiltD.Enabled = True
    End Sub

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()
        fTilt = New frmTilt
        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub
End Class
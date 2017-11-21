<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSinglePattern
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.picSingleGraph = New System.Windows.Forms.PictureBox()
        CType(Me.picSingleGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picSingleGraph
        '
        Me.picSingleGraph.BackColor = System.Drawing.SystemColors.ControlLight
        Me.picSingleGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSingleGraph.Location = New System.Drawing.Point(3, 3)
        Me.picSingleGraph.Name = "picSingleGraph"
        Me.picSingleGraph.Size = New System.Drawing.Size(718, 718)
        Me.picSingleGraph.TabIndex = 377
        Me.picSingleGraph.TabStop = False
        '
        'frmSinglePattern
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(721, 721)
        Me.ControlBox = False
        Me.Controls.Add(Me.picSingleGraph)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSinglePattern"
        Me.Text = "SinglePattern"
        CType(Me.picSingleGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picSingleGraph As System.Windows.Forms.PictureBox
End Class

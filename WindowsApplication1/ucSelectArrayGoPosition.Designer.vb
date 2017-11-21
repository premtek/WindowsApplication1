<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucSelectArrayGoPosition
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
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
        Me.grpLevel = New System.Windows.Forms.GroupBox()
        Me.cmbYa = New System.Windows.Forms.ComboBox()
        Me.cmbXa = New System.Windows.Forms.ComboBox()
        Me.lblYa = New System.Windows.Forms.Label()
        Me.lblXa = New System.Windows.Forms.Label()
        Me.grpLevel.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLevel
        '
        Me.grpLevel.Controls.Add(Me.cmbYa)
        Me.grpLevel.Controls.Add(Me.cmbXa)
        Me.grpLevel.Controls.Add(Me.lblYa)
        Me.grpLevel.Controls.Add(Me.lblXa)
        Me.grpLevel.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.grpLevel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpLevel.Location = New System.Drawing.Point(8, 10)
        Me.grpLevel.Name = "grpLevel"
        Me.grpLevel.Size = New System.Drawing.Size(248, 65)
        Me.grpLevel.TabIndex = 379
        Me.grpLevel.TabStop = False
        Me.grpLevel.Text = "Level"
        '
        'cmbYa
        '
        Me.cmbYa.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.cmbYa.FormattingEnabled = True
        Me.cmbYa.Location = New System.Drawing.Point(145, 26)
        Me.cmbYa.Name = "cmbYa"
        Me.cmbYa.Size = New System.Drawing.Size(65, 32)
        Me.cmbYa.TabIndex = 19
        Me.cmbYa.Text = "000"
        '
        'cmbXa
        '
        Me.cmbXa.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.cmbXa.FormattingEnabled = True
        Me.cmbXa.Location = New System.Drawing.Point(46, 26)
        Me.cmbXa.Name = "cmbXa"
        Me.cmbXa.Size = New System.Drawing.Size(65, 32)
        Me.cmbXa.TabIndex = 18
        Me.cmbXa.Text = "000"
        '
        'lblYa
        '
        Me.lblYa.AutoSize = True
        Me.lblYa.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblYa.Location = New System.Drawing.Point(117, 29)
        Me.lblYa.Name = "lblYa"
        Me.lblYa.Size = New System.Drawing.Size(22, 24)
        Me.lblYa.TabIndex = 5
        Me.lblYa.Text = "Y"
        '
        'lblXa
        '
        Me.lblXa.AutoSize = True
        Me.lblXa.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblXa.Location = New System.Drawing.Point(17, 29)
        Me.lblXa.Name = "lblXa"
        Me.lblXa.Size = New System.Drawing.Size(23, 24)
        Me.lblXa.TabIndex = 2
        Me.lblXa.Text = "X"
        '
        'ucSelectArrayGoPosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpLevel)
        Me.Name = "ucSelectArrayGoPosition"
        Me.Size = New System.Drawing.Size(263, 80)
        Me.grpLevel.ResumeLayout(False)
        Me.grpLevel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpLevel As System.Windows.Forms.GroupBox
    Friend WithEvents cmbYa As System.Windows.Forms.ComboBox
    Friend WithEvents cmbXa As System.Windows.Forms.ComboBox
    Friend WithEvents lblYa As System.Windows.Forms.Label
    Friend WithEvents lblXa As System.Windows.Forms.Label

End Class

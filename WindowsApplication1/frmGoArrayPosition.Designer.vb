<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGoArrayPosition
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboB = New System.Windows.Forms.ComboBox()
        Me.btnGoDispenseBasePos = New System.Windows.Forms.Button()
        Me.btnGoCCDAlignPos = New System.Windows.Forms.Button()
        Me.btnGoFindHeightPos = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(264, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 24)
        Me.Label7.TabIndex = 509
        Me.Label7.Text = "Angle(°)"
        '
        'cboB
        '
        Me.cboB.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.cboB.FormattingEnabled = True
        Me.cboB.Location = New System.Drawing.Point(356, 12)
        Me.cboB.Name = "cboB"
        Me.cboB.Size = New System.Drawing.Size(122, 32)
        Me.cboB.TabIndex = 508
        '
        'btnGoDispenseBasePos
        '
        Me.btnGoDispenseBasePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SelectValve
        Me.btnGoDispenseBasePos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnGoDispenseBasePos.FlatAppearance.BorderSize = 0
        Me.btnGoDispenseBasePos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGoDispenseBasePos.Location = New System.Drawing.Point(408, 66)
        Me.btnGoDispenseBasePos.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGoDispenseBasePos.Name = "btnGoDispenseBasePos"
        Me.btnGoDispenseBasePos.Size = New System.Drawing.Size(70, 70)
        Me.btnGoDispenseBasePos.TabIndex = 507
        Me.btnGoDispenseBasePos.UseVisualStyleBackColor = True
        '
        'btnGoCCDAlignPos
        '
        Me.btnGoCCDAlignPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.FIDs
        Me.btnGoCCDAlignPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnGoCCDAlignPos.FlatAppearance.BorderSize = 0
        Me.btnGoCCDAlignPos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGoCCDAlignPos.Location = New System.Drawing.Point(268, 66)
        Me.btnGoCCDAlignPos.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGoCCDAlignPos.Name = "btnGoCCDAlignPos"
        Me.btnGoCCDAlignPos.Size = New System.Drawing.Size(70, 70)
        Me.btnGoCCDAlignPos.TabIndex = 506
        Me.btnGoCCDAlignPos.UseVisualStyleBackColor = True
        '
        'btnGoFindHeightPos
        '
        Me.btnGoFindHeightPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.LaserReader
        Me.btnGoFindHeightPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnGoFindHeightPos.FlatAppearance.BorderSize = 0
        Me.btnGoFindHeightPos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGoFindHeightPos.Location = New System.Drawing.Point(338, 66)
        Me.btnGoFindHeightPos.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGoFindHeightPos.Name = "btnGoFindHeightPos"
        Me.btnGoFindHeightPos.Size = New System.Drawing.Size(70, 70)
        Me.btnGoFindHeightPos.TabIndex = 505
        Me.btnGoFindHeightPos.UseVisualStyleBackColor = True
        '
        'frmGoArrayPosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(514, 335)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboB)
        Me.Controls.Add(Me.btnGoDispenseBasePos)
        Me.Controls.Add(Me.btnGoCCDAlignPos)
        Me.Controls.Add(Me.btnGoFindHeightPos)
        Me.Name = "frmGoArrayPosition"
        Me.Text = "GoArrayPosition"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboB As System.Windows.Forms.ComboBox
    Friend WithEvents btnGoDispenseBasePos As System.Windows.Forms.Button
    Friend WithEvents btnGoCCDAlignPos As System.Windows.Forms.Button
    Friend WithEvents btnGoFindHeightPos As System.Windows.Forms.Button
End Class

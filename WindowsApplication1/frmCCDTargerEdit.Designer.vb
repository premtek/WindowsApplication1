<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDTargerEdit
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
        Me.UcCCDTargerEdit1 = New WindowsApplication1.ucCCDTargerEdit()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'UcCCDTargerEdit1
        '
        Me.UcCCDTargerEdit1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcCCDTargerEdit1.Location = New System.Drawing.Point(12, 12)
        Me.UcCCDTargerEdit1.Name = "UcCCDTargerEdit1"
        Me.UcCCDTargerEdit1.Size = New System.Drawing.Size(1030, 619)
        Me.UcCCDTargerEdit1.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(1052, 549)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(150, 70)
        Me.btnCancel.TabIndex = 496
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmCCDTargerEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1214, 641)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.UcCCDTargerEdit1)
        Me.Name = "frmCCDTargerEdit"
        Me.Text = "CCDTargerEdit"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcCCDTargerEdit1 As WindowsApplication1.ucCCDTargerEdit
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucHeavyLight
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucHeavyLight))
        Me.rdbHeavy1 = New System.Windows.Forms.RadioButton()
        Me.rdbLight1 = New System.Windows.Forms.RadioButton()
        Me.rdbNone1 = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'rdbHeavy1
        '
        resources.ApplyResources(Me.rdbHeavy1, "rdbHeavy1")
        Me.rdbHeavy1.Name = "rdbHeavy1"
        Me.rdbHeavy1.TabStop = True
        Me.rdbHeavy1.UseVisualStyleBackColor = True
        '
        'rdbLight1
        '
        resources.ApplyResources(Me.rdbLight1, "rdbLight1")
        Me.rdbLight1.Name = "rdbLight1"
        Me.rdbLight1.TabStop = True
        Me.rdbLight1.UseVisualStyleBackColor = True
        '
        'rdbNone1
        '
        resources.ApplyResources(Me.rdbNone1, "rdbNone1")
        Me.rdbNone1.Name = "rdbNone1"
        Me.rdbNone1.TabStop = True
        Me.rdbNone1.UseVisualStyleBackColor = True
        '
        'ucHeavyLight
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.rdbNone1)
        Me.Controls.Add(Me.rdbHeavy1)
        Me.Controls.Add(Me.rdbLight1)
        Me.Name = "ucHeavyLight"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdbHeavy1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLight1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNone1 As System.Windows.Forms.RadioButton

End Class

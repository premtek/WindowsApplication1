<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucLightControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucLightControl))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkLight4 = New System.Windows.Forms.CheckBox()
        Me.chkLight3 = New System.Windows.Forms.CheckBox()
        Me.chkLight2 = New System.Windows.Forms.CheckBox()
        Me.chkLight1 = New System.Windows.Forms.CheckBox()
        Me.nmcLight4 = New System.Windows.Forms.NumericUpDown()
        Me.nmcLight3 = New System.Windows.Forms.NumericUpDown()
        Me.nmcLight2 = New System.Windows.Forms.NumericUpDown()
        Me.nmcLight1 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcLight4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkLight4)
        Me.GroupBox1.Controls.Add(Me.chkLight3)
        Me.GroupBox1.Controls.Add(Me.chkLight2)
        Me.GroupBox1.Controls.Add(Me.chkLight1)
        Me.GroupBox1.Controls.Add(Me.nmcLight4)
        Me.GroupBox1.Controls.Add(Me.nmcLight3)
        Me.GroupBox1.Controls.Add(Me.nmcLight2)
        Me.GroupBox1.Controls.Add(Me.nmcLight1)
        Me.GroupBox1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'chkLight4
        '
        resources.ApplyResources(Me.chkLight4, "chkLight4")
        Me.chkLight4.Name = "chkLight4"
        Me.chkLight4.UseVisualStyleBackColor = True
        '
        'chkLight3
        '
        resources.ApplyResources(Me.chkLight3, "chkLight3")
        Me.chkLight3.Name = "chkLight3"
        Me.chkLight3.UseVisualStyleBackColor = True
        '
        'chkLight2
        '
        resources.ApplyResources(Me.chkLight2, "chkLight2")
        Me.chkLight2.Name = "chkLight2"
        Me.chkLight2.UseVisualStyleBackColor = True
        '
        'chkLight1
        '
        resources.ApplyResources(Me.chkLight1, "chkLight1")
        Me.chkLight1.Name = "chkLight1"
        Me.chkLight1.UseVisualStyleBackColor = True
        '
        'nmcLight4
        '
        resources.ApplyResources(Me.nmcLight4, "nmcLight4")
        Me.nmcLight4.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcLight4.Name = "nmcLight4"
        '
        'nmcLight3
        '
        resources.ApplyResources(Me.nmcLight3, "nmcLight3")
        Me.nmcLight3.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcLight3.Name = "nmcLight3"
        '
        'nmcLight2
        '
        resources.ApplyResources(Me.nmcLight2, "nmcLight2")
        Me.nmcLight2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcLight2.Name = "nmcLight2"
        '
        'nmcLight1
        '
        resources.ApplyResources(Me.nmcLight1, "nmcLight1")
        Me.nmcLight1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcLight1.Name = "nmcLight1"
        '
        'ucLightControl
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ucLightControl"
        resources.ApplyResources(Me, "$this")
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcLight4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkLight4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLight3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLight2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLight1 As System.Windows.Forms.CheckBox
    Friend WithEvents nmcLight4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcLight3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcLight2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcLight1 As System.Windows.Forms.NumericUpDown

End Class

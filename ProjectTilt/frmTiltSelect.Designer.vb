<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTiltSelect
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTiltA = New System.Windows.Forms.Button()
        Me.btnTiltB = New System.Windows.Forms.Button()
        Me.btnTiltC = New System.Windows.Forms.Button()
        Me.btnTiltD = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnTiltA, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnTiltB, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnTiltC, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnTiltD, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(284, 262)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'btnTiltA
        '
        Me.btnTiltA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTiltA.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnTiltA.Location = New System.Drawing.Point(3, 3)
        Me.btnTiltA.Name = "btnTiltA"
        Me.btnTiltA.Size = New System.Drawing.Size(136, 125)
        Me.btnTiltA.TabIndex = 0
        Me.btnTiltA.Text = "TiltA"
        Me.btnTiltA.UseVisualStyleBackColor = True
        '
        'btnTiltB
        '
        Me.btnTiltB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTiltB.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnTiltB.Location = New System.Drawing.Point(145, 3)
        Me.btnTiltB.Name = "btnTiltB"
        Me.btnTiltB.Size = New System.Drawing.Size(136, 125)
        Me.btnTiltB.TabIndex = 1
        Me.btnTiltB.Text = "TiltB"
        Me.btnTiltB.UseVisualStyleBackColor = True
        '
        'btnTiltC
        '
        Me.btnTiltC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTiltC.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnTiltC.Location = New System.Drawing.Point(3, 134)
        Me.btnTiltC.Name = "btnTiltC"
        Me.btnTiltC.Size = New System.Drawing.Size(136, 125)
        Me.btnTiltC.TabIndex = 2
        Me.btnTiltC.Text = "TiltC"
        Me.btnTiltC.UseVisualStyleBackColor = True
        '
        'btnTiltD
        '
        Me.btnTiltD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTiltD.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnTiltD.Location = New System.Drawing.Point(145, 134)
        Me.btnTiltD.Name = "btnTiltD"
        Me.btnTiltD.Size = New System.Drawing.Size(136, 125)
        Me.btnTiltD.TabIndex = 3
        Me.btnTiltD.Text = "TiltD"
        Me.btnTiltD.UseVisualStyleBackColor = True
        '
        'frmTiltSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmTiltSelect"
        Me.Text = "frmTiltSelect"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnTiltA As System.Windows.Forms.Button
    Friend WithEvents btnTiltB As System.Windows.Forms.Button
    Friend WithEvents btnTiltC As System.Windows.Forms.Button
    Friend WithEvents btnTiltD As System.Windows.Forms.Button
End Class

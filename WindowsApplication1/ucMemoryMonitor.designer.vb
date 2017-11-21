<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucMemoryMonitor
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
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lstRecord = New System.Windows.Forms.ListBox()
        Me.tlpTool = New System.Windows.Forms.TableLayoutPanel()
        Me.cbEnable = New System.Windows.Forms.CheckBox()
        Me.cbListNoToEnd = New System.Windows.Forms.CheckBox()
        Me.tlpMain.SuspendLayout()
        Me.tlpTool.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lstRecord, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpTool, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 1
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Size = New System.Drawing.Size(407, 158)
        Me.tlpMain.TabIndex = 0
        '
        'lstRecord
        '
        Me.lstRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRecord.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstRecord.FormattingEnabled = True
        Me.lstRecord.ItemHeight = 15
        Me.lstRecord.Location = New System.Drawing.Point(1, 71)
        Me.lstRecord.Margin = New System.Windows.Forms.Padding(1)
        Me.lstRecord.Name = "lstRecord"
        Me.lstRecord.Size = New System.Drawing.Size(405, 86)
        Me.lstRecord.TabIndex = 5
        '
        'tlpTool
        '
        Me.tlpTool.ColumnCount = 2
        Me.tlpTool.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTool.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTool.Controls.Add(Me.cbEnable, 0, 0)
        Me.tlpTool.Controls.Add(Me.cbListNoToEnd, 0, 1)
        Me.tlpTool.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTool.Location = New System.Drawing.Point(3, 4)
        Me.tlpTool.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tlpTool.Name = "tlpTool"
        Me.tlpTool.RowCount = 2
        Me.tlpTool.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTool.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTool.Size = New System.Drawing.Size(401, 62)
        Me.tlpTool.TabIndex = 6
        '
        'cbEnable
        '
        Me.cbEnable.AutoSize = True
        Me.cbEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbEnable.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cbEnable.Location = New System.Drawing.Point(3, 4)
        Me.cbEnable.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbEnable.Name = "cbEnable"
        Me.cbEnable.Size = New System.Drawing.Size(194, 23)
        Me.cbEnable.TabIndex = 0
        Me.cbEnable.Text = "Enable"
        Me.cbEnable.UseVisualStyleBackColor = True
        '
        'cbListNoToEnd
        '
        Me.cbListNoToEnd.AutoSize = True
        Me.cbListNoToEnd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbListNoToEnd.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cbListNoToEnd.Location = New System.Drawing.Point(3, 35)
        Me.cbListNoToEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbListNoToEnd.Name = "cbListNoToEnd"
        Me.cbListNoToEnd.Size = New System.Drawing.Size(194, 23)
        Me.cbListNoToEnd.TabIndex = 1
        Me.cbListNoToEnd.Text = "Stop Scroll Bar"
        Me.cbListNoToEnd.UseVisualStyleBackColor = True
        '
        'ucMemoryMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.tlpMain)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "ucMemoryMonitor"
        Me.Size = New System.Drawing.Size(407, 158)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpTool.ResumeLayout(False)
        Me.tlpTool.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Private WithEvents lstRecord As System.Windows.Forms.ListBox
    Friend WithEvents tlpTool As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbEnable As System.Windows.Forms.CheckBox
    Friend WithEvents cbListNoToEnd As System.Windows.Forms.CheckBox

End Class

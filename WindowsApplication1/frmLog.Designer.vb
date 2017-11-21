<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLog))
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.chkInfo = New System.Windows.Forms.CheckBox()
        Me.chkWarn = New System.Windows.Forms.CheckBox()
        Me.chkAlarm = New System.Windows.Forms.CheckBox()
        Me.chkError = New System.Windows.Forms.CheckBox()
        Me.chkOperator = New System.Windows.Forms.CheckBox()
        Me.chkEngineer = New System.Windows.Forms.CheckBox()
        Me.chkManager = New System.Windows.Forms.CheckBox()
        Me.chkAdministrator = New System.Windows.Forms.CheckBox()
        Me.chkManufacturer = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.grpUser = New System.Windows.Forms.GroupBox()
        Me.grpLevel = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUser.SuspendLayout()
        Me.grpLevel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpStartDate
        '
        resources.ApplyResources(Me.dtpStartDate, "dtpStartDate")
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Name = "dtpStartDate"
        '
        'dtpEndDate
        '
        resources.ApplyResources(Me.dtpEndDate, "dtpEndDate")
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Name = "dtpEndDate"
        '
        'lblStartDate
        '
        resources.ApplyResources(Me.lblStartDate, "lblStartDate")
        Me.lblStartDate.Name = "lblStartDate"
        '
        'lblEndDate
        '
        resources.ApplyResources(Me.lblEndDate, "lblEndDate")
        Me.lblEndDate.Name = "lblEndDate"
        '
        'chkInfo
        '
        resources.ApplyResources(Me.chkInfo, "chkInfo")
        Me.chkInfo.Name = "chkInfo"
        Me.chkInfo.UseVisualStyleBackColor = True
        '
        'chkWarn
        '
        resources.ApplyResources(Me.chkWarn, "chkWarn")
        Me.chkWarn.Name = "chkWarn"
        Me.chkWarn.UseVisualStyleBackColor = True
        '
        'chkAlarm
        '
        resources.ApplyResources(Me.chkAlarm, "chkAlarm")
        Me.chkAlarm.Name = "chkAlarm"
        Me.chkAlarm.UseVisualStyleBackColor = True
        '
        'chkError
        '
        resources.ApplyResources(Me.chkError, "chkError")
        Me.chkError.Name = "chkError"
        Me.chkError.UseVisualStyleBackColor = True
        '
        'chkOperator
        '
        resources.ApplyResources(Me.chkOperator, "chkOperator")
        Me.chkOperator.Name = "chkOperator"
        Me.chkOperator.UseVisualStyleBackColor = True
        '
        'chkEngineer
        '
        resources.ApplyResources(Me.chkEngineer, "chkEngineer")
        Me.chkEngineer.Name = "chkEngineer"
        Me.chkEngineer.UseVisualStyleBackColor = True
        '
        'chkManager
        '
        resources.ApplyResources(Me.chkManager, "chkManager")
        Me.chkManager.Name = "chkManager"
        Me.chkManager.UseVisualStyleBackColor = True
        '
        'chkAdministrator
        '
        resources.ApplyResources(Me.chkAdministrator, "chkAdministrator")
        Me.chkAdministrator.Name = "chkAdministrator"
        Me.chkAdministrator.UseVisualStyleBackColor = True
        '
        'chkManufacturer
        '
        resources.ApplyResources(Me.chkManufacturer, "chkManufacturer")
        Me.chkManufacturer.Name = "chkManufacturer"
        Me.chkManufacturer.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8})
        resources.ApplyResources(Me.DataGridView1, "DataGridView1")
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        '
        'Column1
        '
        resources.ApplyResources(Me.Column1, "Column1")
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        resources.ApplyResources(Me.Column2, "Column2")
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        resources.ApplyResources(Me.Column3, "Column3")
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        resources.ApplyResources(Me.Column4, "Column4")
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        resources.ApplyResources(Me.Column5, "Column5")
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        resources.ApplyResources(Me.Column6, "Column6")
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        resources.ApplyResources(Me.Column7, "Column7")
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        resources.ApplyResources(Me.Column8, "Column8")
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'btnRefresh
        '
        resources.ApplyResources(Me.btnRefresh, "btnRefresh")
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'grpUser
        '
        Me.grpUser.Controls.Add(Me.chkOperator)
        Me.grpUser.Controls.Add(Me.chkEngineer)
        Me.grpUser.Controls.Add(Me.chkManager)
        Me.grpUser.Controls.Add(Me.chkManufacturer)
        Me.grpUser.Controls.Add(Me.chkAdministrator)
        resources.ApplyResources(Me.grpUser, "grpUser")
        Me.grpUser.Name = "grpUser"
        Me.grpUser.TabStop = False
        '
        'grpLevel
        '
        Me.grpLevel.Controls.Add(Me.chkInfo)
        Me.grpLevel.Controls.Add(Me.chkWarn)
        Me.grpLevel.Controls.Add(Me.chkAlarm)
        Me.grpLevel.Controls.Add(Me.chkError)
        resources.ApplyResources(Me.grpLevel, "grpLevel")
        Me.grpLevel.Name = "grpLevel"
        Me.grpLevel.TabStop = False
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.lblStartDate)
        Me.Panel1.Controls.Add(Me.dtpStartDate)
        Me.Panel1.Controls.Add(Me.grpLevel)
        Me.Panel1.Controls.Add(Me.dtpEndDate)
        Me.Panel1.Controls.Add(Me.grpUser)
        Me.Panel1.Controls.Add(Me.lblEndDate)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'ProgressBar1
        '
        resources.ApplyResources(Me.ProgressBar1, "ProgressBar1")
        Me.ProgressBar1.Name = "ProgressBar1"
        '
        'frmLog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLog"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUser.ResumeLayout(False)
        Me.grpLevel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents chkInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkWarn As System.Windows.Forms.CheckBox
    Friend WithEvents chkAlarm As System.Windows.Forms.CheckBox
    Friend WithEvents chkError As System.Windows.Forms.CheckBox
    Friend WithEvents chkOperator As System.Windows.Forms.CheckBox
    Friend WithEvents chkEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents chkManager As System.Windows.Forms.CheckBox
    Friend WithEvents chkAdministrator As System.Windows.Forms.CheckBox
    Friend WithEvents chkManufacturer As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents grpUser As System.Windows.Forms.GroupBox
    Friend WithEvents grpLevel As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class

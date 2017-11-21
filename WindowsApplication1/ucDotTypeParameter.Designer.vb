<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucDotTypeParameter
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucDotTypeParameter))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpPreDispense = New System.Windows.Forms.GroupBox()
        Me.tlpPreDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.textDotDownSpeed = New System.Windows.Forms.TextBox()
        Me.textDotDispenseGap = New System.Windows.Forms.TextBox()
        Me.lbDotDownSpeed = New System.Windows.Forms.Label()
        Me.lbDotDispenseGap = New System.Windows.Forms.Label()
        Me.lbDotDispenseGapUnit = New System.Windows.Forms.Label()
        Me.textDotSettlingTime = New System.Windows.Forms.TextBox()
        Me.lbDotSettlingTime = New System.Windows.Forms.Label()
        Me.lbDotSettlingTimeUnit = New System.Windows.Forms.Label()
        Me.lbDotDownSpeedUnit = New System.Windows.Forms.Label()
        Me.textDotDownAcc = New System.Windows.Forms.TextBox()
        Me.lbDotDownAcc = New System.Windows.Forms.Label()
        Me.lbDotDownAccUnit = New System.Windows.Forms.Label()
        Me.grpDuringDispense = New System.Windows.Forms.GroupBox()
        Me.tlpDuringDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.lbDotMultiShotDelta = New System.Windows.Forms.Label()
        Me.textDotValveOnTime = New System.Windows.Forms.TextBox()
        Me.lbDotValveOnTime = New System.Windows.Forms.Label()
        Me.lbDotValveOnTimeUnit = New System.Windows.Forms.Label()
        Me.textDotNumberOfShots = New System.Windows.Forms.TextBox()
        Me.lbDotNumberOfShots = New System.Windows.Forms.Label()
        Me.lbDotNumberOfShotsUnit = New System.Windows.Forms.Label()
        Me.lbDotMultiShotDeltaUnit = New System.Windows.Forms.Label()
        Me.textDotMultiShotDelta = New System.Windows.Forms.TextBox()
        Me.grpPostDispense = New System.Windows.Forms.GroupBox()
        Me.tlpPostDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.lbDotSuckBack = New System.Windows.Forms.Label()
        Me.textDotRetractDistance = New System.Windows.Forms.TextBox()
        Me.lbDotRetractDistance = New System.Windows.Forms.Label()
        Me.textDotDwellTime = New System.Windows.Forms.TextBox()
        Me.lbDotDwellTime = New System.Windows.Forms.Label()
        Me.lbDotDwellTimeUnit = New System.Windows.Forms.Label()
        Me.lbDotRetractDistanceUnit = New System.Windows.Forms.Label()
        Me.textDotRetractSpeed = New System.Windows.Forms.TextBox()
        Me.lbDotRetractSpeed = New System.Windows.Forms.Label()
        Me.lbDotRetractSpeedUnit = New System.Windows.Forms.Label()
        Me.lbDotRetractAcc = New System.Windows.Forms.Label()
        Me.textDotRetractAcc = New System.Windows.Forms.TextBox()
        Me.textDotSuckBack = New System.Windows.Forms.TextBox()
        Me.lbDotRetractAccUnit = New System.Windows.Forms.Label()
        Me.lbDotSuckBackUnit = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnDotDBDel = New System.Windows.Forms.Button()
        Me.btnDotDBUpdate = New System.Windows.Forms.Button()
        Me.btnDotDBAdd = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lstDotDB = New System.Windows.Forms.ListBox()
        Me.txtDotDB = New System.Windows.Forms.TextBox()
        Me.tlpTool = New System.Windows.Forms.TableLayoutPanel()
        Me.btSetDefaultValue = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grpPreDispense.SuspendLayout()
        Me.tlpPreDispense.SuspendLayout()
        Me.grpDuringDispense.SuspendLayout()
        Me.tlpDuringDispense.SuspendLayout()
        Me.grpPostDispense.SuspendLayout()
        Me.tlpPostDispense.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tlpTool.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.grpPreDispense, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpDuringDispense, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.grpPostDispense, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.tlpTool, 2, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'grpPreDispense
        '
        Me.grpPreDispense.Controls.Add(Me.tlpPreDispense)
        resources.ApplyResources(Me.grpPreDispense, "grpPreDispense")
        Me.grpPreDispense.Name = "grpPreDispense"
        Me.grpPreDispense.TabStop = False
        '
        'tlpPreDispense
        '
        resources.ApplyResources(Me.tlpPreDispense, "tlpPreDispense")
        Me.tlpPreDispense.Controls.Add(Me.textDotDownSpeed, 1, 1)
        Me.tlpPreDispense.Controls.Add(Me.textDotDispenseGap, 1, 3)
        Me.tlpPreDispense.Controls.Add(Me.lbDotDownSpeed, 0, 1)
        Me.tlpPreDispense.Controls.Add(Me.lbDotDispenseGap, 0, 3)
        Me.tlpPreDispense.Controls.Add(Me.lbDotDispenseGapUnit, 2, 3)
        Me.tlpPreDispense.Controls.Add(Me.textDotSettlingTime, 1, 0)
        Me.tlpPreDispense.Controls.Add(Me.lbDotSettlingTime, 0, 0)
        Me.tlpPreDispense.Controls.Add(Me.lbDotSettlingTimeUnit, 2, 0)
        Me.tlpPreDispense.Controls.Add(Me.lbDotDownSpeedUnit, 2, 1)
        Me.tlpPreDispense.Controls.Add(Me.textDotDownAcc, 1, 2)
        Me.tlpPreDispense.Controls.Add(Me.lbDotDownAcc, 0, 2)
        Me.tlpPreDispense.Controls.Add(Me.lbDotDownAccUnit, 2, 2)
        Me.tlpPreDispense.Name = "tlpPreDispense"
        '
        'textDotDownSpeed
        '
        Me.textDotDownSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotDownSpeed, "textDotDownSpeed")
        Me.textDotDownSpeed.Name = "textDotDownSpeed"
        '
        'textDotDispenseGap
        '
        Me.textDotDispenseGap.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotDispenseGap, "textDotDispenseGap")
        Me.textDotDispenseGap.Name = "textDotDispenseGap"
        '
        'lbDotDownSpeed
        '
        Me.lbDotDownSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDownSpeed, "lbDotDownSpeed")
        Me.lbDotDownSpeed.Name = "lbDotDownSpeed"
        '
        'lbDotDispenseGap
        '
        Me.lbDotDispenseGap.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDispenseGap, "lbDotDispenseGap")
        Me.lbDotDispenseGap.Name = "lbDotDispenseGap"
        '
        'lbDotDispenseGapUnit
        '
        Me.lbDotDispenseGapUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDispenseGapUnit, "lbDotDispenseGapUnit")
        Me.lbDotDispenseGapUnit.Name = "lbDotDispenseGapUnit"
        '
        'textDotSettlingTime
        '
        Me.textDotSettlingTime.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotSettlingTime, "textDotSettlingTime")
        Me.textDotSettlingTime.Name = "textDotSettlingTime"
        '
        'lbDotSettlingTime
        '
        Me.lbDotSettlingTime.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotSettlingTime, "lbDotSettlingTime")
        Me.lbDotSettlingTime.Name = "lbDotSettlingTime"
        '
        'lbDotSettlingTimeUnit
        '
        Me.lbDotSettlingTimeUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotSettlingTimeUnit, "lbDotSettlingTimeUnit")
        Me.lbDotSettlingTimeUnit.Name = "lbDotSettlingTimeUnit"
        '
        'lbDotDownSpeedUnit
        '
        Me.lbDotDownSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDownSpeedUnit, "lbDotDownSpeedUnit")
        Me.lbDotDownSpeedUnit.Name = "lbDotDownSpeedUnit"
        '
        'textDotDownAcc
        '
        Me.textDotDownAcc.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotDownAcc, "textDotDownAcc")
        Me.textDotDownAcc.Name = "textDotDownAcc"
        '
        'lbDotDownAcc
        '
        Me.lbDotDownAcc.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDownAcc, "lbDotDownAcc")
        Me.lbDotDownAcc.Name = "lbDotDownAcc"
        '
        'lbDotDownAccUnit
        '
        Me.lbDotDownAccUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDownAccUnit, "lbDotDownAccUnit")
        Me.lbDotDownAccUnit.Name = "lbDotDownAccUnit"
        '
        'grpDuringDispense
        '
        Me.grpDuringDispense.Controls.Add(Me.tlpDuringDispense)
        resources.ApplyResources(Me.grpDuringDispense, "grpDuringDispense")
        Me.grpDuringDispense.Name = "grpDuringDispense"
        Me.grpDuringDispense.TabStop = False
        '
        'tlpDuringDispense
        '
        resources.ApplyResources(Me.tlpDuringDispense, "tlpDuringDispense")
        Me.tlpDuringDispense.Controls.Add(Me.lbDotMultiShotDelta, 0, 2)
        Me.tlpDuringDispense.Controls.Add(Me.textDotValveOnTime, 1, 0)
        Me.tlpDuringDispense.Controls.Add(Me.lbDotValveOnTime, 0, 0)
        Me.tlpDuringDispense.Controls.Add(Me.lbDotValveOnTimeUnit, 2, 0)
        Me.tlpDuringDispense.Controls.Add(Me.textDotNumberOfShots, 1, 1)
        Me.tlpDuringDispense.Controls.Add(Me.lbDotNumberOfShots, 0, 1)
        Me.tlpDuringDispense.Controls.Add(Me.lbDotNumberOfShotsUnit, 2, 1)
        Me.tlpDuringDispense.Controls.Add(Me.lbDotMultiShotDeltaUnit, 2, 2)
        Me.tlpDuringDispense.Controls.Add(Me.textDotMultiShotDelta, 1, 2)
        Me.tlpDuringDispense.Name = "tlpDuringDispense"
        '
        'lbDotMultiShotDelta
        '
        Me.lbDotMultiShotDelta.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotMultiShotDelta, "lbDotMultiShotDelta")
        Me.lbDotMultiShotDelta.Name = "lbDotMultiShotDelta"
        '
        'textDotValveOnTime
        '
        Me.textDotValveOnTime.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotValveOnTime, "textDotValveOnTime")
        Me.textDotValveOnTime.Name = "textDotValveOnTime"
        '
        'lbDotValveOnTime
        '
        Me.lbDotValveOnTime.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotValveOnTime, "lbDotValveOnTime")
        Me.lbDotValveOnTime.Name = "lbDotValveOnTime"
        '
        'lbDotValveOnTimeUnit
        '
        Me.lbDotValveOnTimeUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotValveOnTimeUnit, "lbDotValveOnTimeUnit")
        Me.lbDotValveOnTimeUnit.Name = "lbDotValveOnTimeUnit"
        '
        'textDotNumberOfShots
        '
        Me.textDotNumberOfShots.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotNumberOfShots, "textDotNumberOfShots")
        Me.textDotNumberOfShots.Name = "textDotNumberOfShots"
        '
        'lbDotNumberOfShots
        '
        Me.lbDotNumberOfShots.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotNumberOfShots, "lbDotNumberOfShots")
        Me.lbDotNumberOfShots.Name = "lbDotNumberOfShots"
        '
        'lbDotNumberOfShotsUnit
        '
        Me.lbDotNumberOfShotsUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotNumberOfShotsUnit, "lbDotNumberOfShotsUnit")
        Me.lbDotNumberOfShotsUnit.Name = "lbDotNumberOfShotsUnit"
        '
        'lbDotMultiShotDeltaUnit
        '
        Me.lbDotMultiShotDeltaUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotMultiShotDeltaUnit, "lbDotMultiShotDeltaUnit")
        Me.lbDotMultiShotDeltaUnit.Name = "lbDotMultiShotDeltaUnit"
        '
        'textDotMultiShotDelta
        '
        Me.textDotMultiShotDelta.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotMultiShotDelta, "textDotMultiShotDelta")
        Me.textDotMultiShotDelta.Name = "textDotMultiShotDelta"
        '
        'grpPostDispense
        '
        Me.grpPostDispense.Controls.Add(Me.tlpPostDispense)
        resources.ApplyResources(Me.grpPostDispense, "grpPostDispense")
        Me.grpPostDispense.Name = "grpPostDispense"
        Me.grpPostDispense.TabStop = False
        '
        'tlpPostDispense
        '
        resources.ApplyResources(Me.tlpPostDispense, "tlpPostDispense")
        Me.tlpPostDispense.Controls.Add(Me.lbDotSuckBack, 0, 4)
        Me.tlpPostDispense.Controls.Add(Me.textDotRetractDistance, 1, 1)
        Me.tlpPostDispense.Controls.Add(Me.lbDotRetractDistance, 0, 1)
        Me.tlpPostDispense.Controls.Add(Me.textDotDwellTime, 1, 0)
        Me.tlpPostDispense.Controls.Add(Me.lbDotDwellTime, 0, 0)
        Me.tlpPostDispense.Controls.Add(Me.lbDotDwellTimeUnit, 2, 0)
        Me.tlpPostDispense.Controls.Add(Me.lbDotRetractDistanceUnit, 2, 1)
        Me.tlpPostDispense.Controls.Add(Me.textDotRetractSpeed, 1, 2)
        Me.tlpPostDispense.Controls.Add(Me.lbDotRetractSpeed, 0, 2)
        Me.tlpPostDispense.Controls.Add(Me.lbDotRetractSpeedUnit, 2, 2)
        Me.tlpPostDispense.Controls.Add(Me.lbDotRetractAcc, 0, 3)
        Me.tlpPostDispense.Controls.Add(Me.textDotRetractAcc, 1, 3)
        Me.tlpPostDispense.Controls.Add(Me.textDotSuckBack, 1, 4)
        Me.tlpPostDispense.Controls.Add(Me.lbDotRetractAccUnit, 2, 3)
        Me.tlpPostDispense.Controls.Add(Me.lbDotSuckBackUnit, 2, 4)
        Me.tlpPostDispense.Name = "tlpPostDispense"
        '
        'lbDotSuckBack
        '
        Me.lbDotSuckBack.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotSuckBack, "lbDotSuckBack")
        Me.lbDotSuckBack.Name = "lbDotSuckBack"
        '
        'textDotRetractDistance
        '
        Me.textDotRetractDistance.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotRetractDistance, "textDotRetractDistance")
        Me.textDotRetractDistance.Name = "textDotRetractDistance"
        '
        'lbDotRetractDistance
        '
        Me.lbDotRetractDistance.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotRetractDistance, "lbDotRetractDistance")
        Me.lbDotRetractDistance.Name = "lbDotRetractDistance"
        '
        'textDotDwellTime
        '
        Me.textDotDwellTime.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotDwellTime, "textDotDwellTime")
        Me.textDotDwellTime.Name = "textDotDwellTime"
        '
        'lbDotDwellTime
        '
        Me.lbDotDwellTime.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDwellTime, "lbDotDwellTime")
        Me.lbDotDwellTime.Name = "lbDotDwellTime"
        '
        'lbDotDwellTimeUnit
        '
        Me.lbDotDwellTimeUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotDwellTimeUnit, "lbDotDwellTimeUnit")
        Me.lbDotDwellTimeUnit.Name = "lbDotDwellTimeUnit"
        '
        'lbDotRetractDistanceUnit
        '
        Me.lbDotRetractDistanceUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotRetractDistanceUnit, "lbDotRetractDistanceUnit")
        Me.lbDotRetractDistanceUnit.Name = "lbDotRetractDistanceUnit"
        '
        'textDotRetractSpeed
        '
        Me.textDotRetractSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotRetractSpeed, "textDotRetractSpeed")
        Me.textDotRetractSpeed.Name = "textDotRetractSpeed"
        '
        'lbDotRetractSpeed
        '
        Me.lbDotRetractSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotRetractSpeed, "lbDotRetractSpeed")
        Me.lbDotRetractSpeed.Name = "lbDotRetractSpeed"
        '
        'lbDotRetractSpeedUnit
        '
        Me.lbDotRetractSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotRetractSpeedUnit, "lbDotRetractSpeedUnit")
        Me.lbDotRetractSpeedUnit.Name = "lbDotRetractSpeedUnit"
        '
        'lbDotRetractAcc
        '
        Me.lbDotRetractAcc.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotRetractAcc, "lbDotRetractAcc")
        Me.lbDotRetractAcc.Name = "lbDotRetractAcc"
        '
        'textDotRetractAcc
        '
        Me.textDotRetractAcc.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotRetractAcc, "textDotRetractAcc")
        Me.textDotRetractAcc.Name = "textDotRetractAcc"
        '
        'textDotSuckBack
        '
        Me.textDotSuckBack.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textDotSuckBack, "textDotSuckBack")
        Me.textDotSuckBack.Name = "textDotSuckBack"
        '
        'lbDotRetractAccUnit
        '
        Me.lbDotRetractAccUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotRetractAccUnit, "lbDotRetractAccUnit")
        Me.lbDotRetractAccUnit.Name = "lbDotRetractAccUnit"
        '
        'lbDotSuckBackUnit
        '
        Me.lbDotSuckBackUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbDotSuckBackUnit, "lbDotSuckBackUnit")
        Me.lbDotSuckBackUnit.Name = "lbDotSuckBackUnit"
        '
        'TableLayoutPanel2
        '
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel1.SetRowSpan(Me.TableLayoutPanel2, 3)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnDotDBDel)
        Me.Panel1.Controls.Add(Me.btnDotDBUpdate)
        Me.Panel1.Controls.Add(Me.btnDotDBAdd)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'btnDotDBDel
        '
        Me.btnDotDBDel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        resources.ApplyResources(Me.btnDotDBDel, "btnDotDBDel")
        Me.btnDotDBDel.Name = "btnDotDBDel"
        Me.ToolTip1.SetToolTip(Me.btnDotDBDel, resources.GetString("btnDotDBDel.ToolTip"))
        Me.btnDotDBDel.UseVisualStyleBackColor = True
        '
        'btnDotDBUpdate
        '
        Me.btnDotDBUpdate.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Save1
        resources.ApplyResources(Me.btnDotDBUpdate, "btnDotDBUpdate")
        Me.btnDotDBUpdate.Name = "btnDotDBUpdate"
        Me.ToolTip1.SetToolTip(Me.btnDotDBUpdate, resources.GetString("btnDotDBUpdate.ToolTip"))
        Me.btnDotDBUpdate.UseVisualStyleBackColor = True
        '
        'btnDotDBAdd
        '
        Me.btnDotDBAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        resources.ApplyResources(Me.btnDotDBAdd, "btnDotDBAdd")
        Me.btnDotDBAdd.Name = "btnDotDBAdd"
        Me.ToolTip1.SetToolTip(Me.btnDotDBAdd, resources.GetString("btnDotDBAdd.ToolTip"))
        Me.btnDotDBAdd.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lstDotDB)
        Me.Panel2.Controls.Add(Me.txtDotDB)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        Me.TableLayoutPanel1.SetRowSpan(Me.Panel2, 2)
        '
        'lstDotDB
        '
        resources.ApplyResources(Me.lstDotDB, "lstDotDB")
        Me.lstDotDB.FormattingEnabled = True
        Me.lstDotDB.Name = "lstDotDB"
        '
        'txtDotDB
        '
        resources.ApplyResources(Me.txtDotDB, "txtDotDB")
        Me.txtDotDB.Name = "txtDotDB"
        '
        'tlpTool
        '
        resources.ApplyResources(Me.tlpTool, "tlpTool")
        Me.tlpTool.Controls.Add(Me.btSetDefaultValue, 1, 0)
        Me.tlpTool.Name = "tlpTool"
        '
        'btSetDefaultValue
        '
        resources.ApplyResources(Me.btSetDefaultValue, "btSetDefaultValue")
        Me.btSetDefaultValue.Name = "btSetDefaultValue"
        Me.btSetDefaultValue.UseVisualStyleBackColor = True
        '
        'ucDotTypeParameter
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ucDotTypeParameter"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.grpPreDispense.ResumeLayout(False)
        Me.tlpPreDispense.ResumeLayout(False)
        Me.tlpPreDispense.PerformLayout()
        Me.grpDuringDispense.ResumeLayout(False)
        Me.tlpDuringDispense.ResumeLayout(False)
        Me.tlpDuringDispense.PerformLayout()
        Me.grpPostDispense.ResumeLayout(False)
        Me.tlpPostDispense.ResumeLayout(False)
        Me.tlpPostDispense.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tlpTool.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpPreDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPreDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents textDotDownSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbDotDownSpeed As System.Windows.Forms.Label
    Friend WithEvents textDotSettlingTime As System.Windows.Forms.TextBox
    Friend WithEvents lbDotSettlingTime As System.Windows.Forms.Label
    Friend WithEvents lbDotSettlingTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbDotDownSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents textDotDownAcc As System.Windows.Forms.TextBox
    Friend WithEvents lbDotDownAcc As System.Windows.Forms.Label
    Friend WithEvents lbDotDownAccUnit As System.Windows.Forms.Label
    Friend WithEvents grpDuringDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpDuringDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbDotMultiShotDelta As System.Windows.Forms.Label
    Friend WithEvents textDotDispenseGap As System.Windows.Forms.TextBox
    Friend WithEvents lbDotDispenseGap As System.Windows.Forms.Label
    Friend WithEvents textDotValveOnTime As System.Windows.Forms.TextBox
    Friend WithEvents lbDotValveOnTime As System.Windows.Forms.Label
    Friend WithEvents lbDotValveOnTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbDotDispenseGapUnit As System.Windows.Forms.Label
    Friend WithEvents textDotNumberOfShots As System.Windows.Forms.TextBox
    Friend WithEvents lbDotNumberOfShots As System.Windows.Forms.Label
    Friend WithEvents lbDotNumberOfShotsUnit As System.Windows.Forms.Label
    Friend WithEvents lbDotMultiShotDeltaUnit As System.Windows.Forms.Label
    Friend WithEvents textDotMultiShotDelta As System.Windows.Forms.TextBox
    Friend WithEvents grpPostDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPostDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbDotSuckBack As System.Windows.Forms.Label
    Friend WithEvents textDotRetractDistance As System.Windows.Forms.TextBox
    Friend WithEvents lbDotRetractDistance As System.Windows.Forms.Label
    Friend WithEvents textDotDwellTime As System.Windows.Forms.TextBox
    Friend WithEvents lbDotDwellTime As System.Windows.Forms.Label
    Friend WithEvents lbDotDwellTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbDotRetractDistanceUnit As System.Windows.Forms.Label
    Friend WithEvents textDotRetractSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbDotRetractSpeed As System.Windows.Forms.Label
    Friend WithEvents lbDotRetractSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents lbDotRetractAcc As System.Windows.Forms.Label
    Friend WithEvents textDotRetractAcc As System.Windows.Forms.TextBox
    Friend WithEvents textDotSuckBack As System.Windows.Forms.TextBox
    Friend WithEvents lbDotRetractAccUnit As System.Windows.Forms.Label
    Friend WithEvents lbDotSuckBackUnit As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDotDBAdd As System.Windows.Forms.Button
    Friend WithEvents btnDotDBDel As System.Windows.Forms.Button
    Friend WithEvents btnDotDBUpdate As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtDotDB As System.Windows.Forms.TextBox
    Friend WithEvents lstDotDB As System.Windows.Forms.ListBox
    Friend WithEvents tlpTool As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btSetDefaultValue As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

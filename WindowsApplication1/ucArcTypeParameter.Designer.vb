<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucArcTypeParameter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucArcTypeParameter))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnArcDBDel = New System.Windows.Forms.Button()
        Me.btnArcDBUpdate = New System.Windows.Forms.Button()
        Me.btnArcDBAdd = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lstArcDB = New System.Windows.Forms.ListBox()
        Me.txtArcDB = New System.Windows.Forms.TextBox()
        Me.grpPreArcDispense = New System.Windows.Forms.GroupBox()
        Me.tlpPreArcDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.textArcPreDownSpeed = New System.Windows.Forms.TextBox()
        Me.lbArcPreDownSpeed = New System.Windows.Forms.Label()
        Me.textArcPreMoveDelayFactor = New System.Windows.Forms.TextBox()
        Me.lbArcPreMoveDelayFactor = New System.Windows.Forms.Label()
        Me.lbArcPreDispenseGapUnit = New System.Windows.Forms.Label()
        Me.textArcPreDispenseGap = New System.Windows.Forms.TextBox()
        Me.lbArcPreMoveDelayFactorUnit = New System.Windows.Forms.Label()
        Me.lbArcPreDispenseGap = New System.Windows.Forms.Label()
        Me.lbArcPreDownSpeedUnit = New System.Windows.Forms.Label()
        Me.textArcPreDownAcc = New System.Windows.Forms.TextBox()
        Me.lbArcPreDownAcc = New System.Windows.Forms.Label()
        Me.lbArcPreDownAccUnit = New System.Windows.Forms.Label()
        Me.grpDuringArcDispense = New System.Windows.Forms.GroupBox()
        Me.tlpDuringArcDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.textArcDuringSpeed = New System.Windows.Forms.TextBox()
        Me.lbArcDuringSpeed = New System.Windows.Forms.Label()
        Me.lbArcDuringSpeedUnit = New System.Windows.Forms.Label()
        Me.textArcDuringShutOffDistance = New System.Windows.Forms.TextBox()
        Me.lbArcDuringShutOffDistance = New System.Windows.Forms.Label()
        Me.lbArcDuringShutOffDistanceUnit = New System.Windows.Forms.Label()
        Me.grpPostArcDispense = New System.Windows.Forms.GroupBox()
        Me.tlpPostArcDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.lbArcPostSuckBack = New System.Windows.Forms.Label()
        Me.lbArcPostDwell = New System.Windows.Forms.Label()
        Me.lbArcPostRetractAcc = New System.Windows.Forms.Label()
        Me.lbArcPostRetractSpeed = New System.Windows.Forms.Label()
        Me.textArcPostBacktrackLength = New System.Windows.Forms.TextBox()
        Me.lbArcPostBacktrackLength = New System.Windows.Forms.Label()
        Me.textArcPostBacktrackGap = New System.Windows.Forms.TextBox()
        Me.lbArcPostBacktrackGap = New System.Windows.Forms.Label()
        Me.textArcPostSuckBack = New System.Windows.Forms.TextBox()
        Me.lbArcPostSuckBackUnit = New System.Windows.Forms.Label()
        Me.lbArcPostBacktrackGapUnit = New System.Windows.Forms.Label()
        Me.lbArcPostBacktrackLengthUnit = New System.Windows.Forms.Label()
        Me.lbArcPostDwellUnit = New System.Windows.Forms.Label()
        Me.textArcPostBacktrackSpeed = New System.Windows.Forms.TextBox()
        Me.textArcPostDwell = New System.Windows.Forms.TextBox()
        Me.lbArcPostBacktrackSpeed = New System.Windows.Forms.Label()
        Me.lbArcPostBacktrackSpeedUnit = New System.Windows.Forms.Label()
        Me.lbArcPostRetractDistance = New System.Windows.Forms.Label()
        Me.textArcPostRetractDistance = New System.Windows.Forms.TextBox()
        Me.textArcPostRetractSpeed = New System.Windows.Forms.TextBox()
        Me.lbArcPostRetractDistanceUnit = New System.Windows.Forms.Label()
        Me.lbArcPostRetractSpeedUnit = New System.Windows.Forms.Label()
        Me.lbArcPostRetractAccUnit = New System.Windows.Forms.Label()
        Me.textArcPostRetractAcc = New System.Windows.Forms.TextBox()
        Me.tlpTool = New System.Windows.Forms.TableLayoutPanel()
        Me.btSetDefaultValue = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.grpPreArcDispense.SuspendLayout()
        Me.tlpPreArcDispense.SuspendLayout()
        Me.grpDuringArcDispense.SuspendLayout()
        Me.tlpDuringArcDispense.SuspendLayout()
        Me.grpPostArcDispense.SuspendLayout()
        Me.tlpPostArcDispense.SuspendLayout()
        Me.tlpTool.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpPreArcDispense, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpDuringArcDispense, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.grpPostArcDispense, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tlpTool, 2, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
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
        Me.Panel1.Controls.Add(Me.btnArcDBDel)
        Me.Panel1.Controls.Add(Me.btnArcDBUpdate)
        Me.Panel1.Controls.Add(Me.btnArcDBAdd)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'btnArcDBDel
        '
        Me.btnArcDBDel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        resources.ApplyResources(Me.btnArcDBDel, "btnArcDBDel")
        Me.btnArcDBDel.Name = "btnArcDBDel"
        Me.ToolTip1.SetToolTip(Me.btnArcDBDel, resources.GetString("btnArcDBDel.ToolTip"))
        Me.btnArcDBDel.UseVisualStyleBackColor = True
        '
        'btnArcDBUpdate
        '
        Me.btnArcDBUpdate.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Save1
        resources.ApplyResources(Me.btnArcDBUpdate, "btnArcDBUpdate")
        Me.btnArcDBUpdate.Name = "btnArcDBUpdate"
        Me.ToolTip1.SetToolTip(Me.btnArcDBUpdate, resources.GetString("btnArcDBUpdate.ToolTip"))
        Me.btnArcDBUpdate.UseVisualStyleBackColor = True
        '
        'btnArcDBAdd
        '
        Me.btnArcDBAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        resources.ApplyResources(Me.btnArcDBAdd, "btnArcDBAdd")
        Me.btnArcDBAdd.Name = "btnArcDBAdd"
        Me.ToolTip1.SetToolTip(Me.btnArcDBAdd, resources.GetString("btnArcDBAdd.ToolTip"))
        Me.btnArcDBAdd.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lstArcDB)
        Me.Panel2.Controls.Add(Me.txtArcDB)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        Me.TableLayoutPanel1.SetRowSpan(Me.Panel2, 2)
        '
        'lstArcDB
        '
        resources.ApplyResources(Me.lstArcDB, "lstArcDB")
        Me.lstArcDB.FormattingEnabled = True
        Me.lstArcDB.Name = "lstArcDB"
        '
        'txtArcDB
        '
        resources.ApplyResources(Me.txtArcDB, "txtArcDB")
        Me.txtArcDB.Name = "txtArcDB"
        '
        'grpPreArcDispense
        '
        Me.grpPreArcDispense.Controls.Add(Me.tlpPreArcDispense)
        resources.ApplyResources(Me.grpPreArcDispense, "grpPreArcDispense")
        Me.grpPreArcDispense.Name = "grpPreArcDispense"
        Me.grpPreArcDispense.TabStop = False
        '
        'tlpPreArcDispense
        '
        resources.ApplyResources(Me.tlpPreArcDispense, "tlpPreArcDispense")
        Me.tlpPreArcDispense.Controls.Add(Me.textArcPreDownSpeed, 1, 1)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreDownSpeed, 0, 1)
        Me.tlpPreArcDispense.Controls.Add(Me.textArcPreMoveDelayFactor, 1, 0)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreMoveDelayFactor, 0, 0)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreDispenseGapUnit, 2, 3)
        Me.tlpPreArcDispense.Controls.Add(Me.textArcPreDispenseGap, 1, 3)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreMoveDelayFactorUnit, 2, 0)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreDispenseGap, 0, 3)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreDownSpeedUnit, 2, 1)
        Me.tlpPreArcDispense.Controls.Add(Me.textArcPreDownAcc, 1, 2)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreDownAcc, 0, 2)
        Me.tlpPreArcDispense.Controls.Add(Me.lbArcPreDownAccUnit, 2, 2)
        Me.tlpPreArcDispense.Name = "tlpPreArcDispense"
        '
        'textArcPreDownSpeed
        '
        Me.textArcPreDownSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPreDownSpeed, "textArcPreDownSpeed")
        Me.textArcPreDownSpeed.Name = "textArcPreDownSpeed"
        '
        'lbArcPreDownSpeed
        '
        Me.lbArcPreDownSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreDownSpeed, "lbArcPreDownSpeed")
        Me.lbArcPreDownSpeed.Name = "lbArcPreDownSpeed"
        '
        'textArcPreMoveDelayFactor
        '
        Me.textArcPreMoveDelayFactor.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPreMoveDelayFactor, "textArcPreMoveDelayFactor")
        Me.textArcPreMoveDelayFactor.Name = "textArcPreMoveDelayFactor"
        '
        'lbArcPreMoveDelayFactor
        '
        Me.lbArcPreMoveDelayFactor.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreMoveDelayFactor, "lbArcPreMoveDelayFactor")
        Me.lbArcPreMoveDelayFactor.Name = "lbArcPreMoveDelayFactor"
        '
        'lbArcPreDispenseGapUnit
        '
        Me.lbArcPreDispenseGapUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreDispenseGapUnit, "lbArcPreDispenseGapUnit")
        Me.lbArcPreDispenseGapUnit.Name = "lbArcPreDispenseGapUnit"
        '
        'textArcPreDispenseGap
        '
        Me.textArcPreDispenseGap.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPreDispenseGap, "textArcPreDispenseGap")
        Me.textArcPreDispenseGap.Name = "textArcPreDispenseGap"
        '
        'lbArcPreMoveDelayFactorUnit
        '
        Me.lbArcPreMoveDelayFactorUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreMoveDelayFactorUnit, "lbArcPreMoveDelayFactorUnit")
        Me.lbArcPreMoveDelayFactorUnit.Name = "lbArcPreMoveDelayFactorUnit"
        '
        'lbArcPreDispenseGap
        '
        Me.lbArcPreDispenseGap.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreDispenseGap, "lbArcPreDispenseGap")
        Me.lbArcPreDispenseGap.Name = "lbArcPreDispenseGap"
        '
        'lbArcPreDownSpeedUnit
        '
        Me.lbArcPreDownSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreDownSpeedUnit, "lbArcPreDownSpeedUnit")
        Me.lbArcPreDownSpeedUnit.Name = "lbArcPreDownSpeedUnit"
        '
        'textArcPreDownAcc
        '
        Me.textArcPreDownAcc.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPreDownAcc, "textArcPreDownAcc")
        Me.textArcPreDownAcc.Name = "textArcPreDownAcc"
        '
        'lbArcPreDownAcc
        '
        Me.lbArcPreDownAcc.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreDownAcc, "lbArcPreDownAcc")
        Me.lbArcPreDownAcc.Name = "lbArcPreDownAcc"
        '
        'lbArcPreDownAccUnit
        '
        Me.lbArcPreDownAccUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPreDownAccUnit, "lbArcPreDownAccUnit")
        Me.lbArcPreDownAccUnit.Name = "lbArcPreDownAccUnit"
        '
        'grpDuringArcDispense
        '
        Me.grpDuringArcDispense.Controls.Add(Me.tlpDuringArcDispense)
        resources.ApplyResources(Me.grpDuringArcDispense, "grpDuringArcDispense")
        Me.grpDuringArcDispense.Name = "grpDuringArcDispense"
        Me.grpDuringArcDispense.TabStop = False
        '
        'tlpDuringArcDispense
        '
        resources.ApplyResources(Me.tlpDuringArcDispense, "tlpDuringArcDispense")
        Me.tlpDuringArcDispense.Controls.Add(Me.textArcDuringSpeed, 1, 0)
        Me.tlpDuringArcDispense.Controls.Add(Me.lbArcDuringSpeed, 0, 0)
        Me.tlpDuringArcDispense.Controls.Add(Me.lbArcDuringSpeedUnit, 2, 0)
        Me.tlpDuringArcDispense.Controls.Add(Me.textArcDuringShutOffDistance, 1, 1)
        Me.tlpDuringArcDispense.Controls.Add(Me.lbArcDuringShutOffDistance, 0, 1)
        Me.tlpDuringArcDispense.Controls.Add(Me.lbArcDuringShutOffDistanceUnit, 2, 1)
        Me.tlpDuringArcDispense.Name = "tlpDuringArcDispense"
        '
        'textArcDuringSpeed
        '
        Me.textArcDuringSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcDuringSpeed, "textArcDuringSpeed")
        Me.textArcDuringSpeed.Name = "textArcDuringSpeed"
        '
        'lbArcDuringSpeed
        '
        Me.lbArcDuringSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcDuringSpeed, "lbArcDuringSpeed")
        Me.lbArcDuringSpeed.Name = "lbArcDuringSpeed"
        '
        'lbArcDuringSpeedUnit
        '
        Me.lbArcDuringSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcDuringSpeedUnit, "lbArcDuringSpeedUnit")
        Me.lbArcDuringSpeedUnit.Name = "lbArcDuringSpeedUnit"
        '
        'textArcDuringShutOffDistance
        '
        Me.textArcDuringShutOffDistance.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcDuringShutOffDistance, "textArcDuringShutOffDistance")
        Me.textArcDuringShutOffDistance.Name = "textArcDuringShutOffDistance"
        '
        'lbArcDuringShutOffDistance
        '
        Me.lbArcDuringShutOffDistance.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcDuringShutOffDistance, "lbArcDuringShutOffDistance")
        Me.lbArcDuringShutOffDistance.Name = "lbArcDuringShutOffDistance"
        '
        'lbArcDuringShutOffDistanceUnit
        '
        Me.lbArcDuringShutOffDistanceUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcDuringShutOffDistanceUnit, "lbArcDuringShutOffDistanceUnit")
        Me.lbArcDuringShutOffDistanceUnit.Name = "lbArcDuringShutOffDistanceUnit"
        '
        'grpPostArcDispense
        '
        Me.grpPostArcDispense.Controls.Add(Me.tlpPostArcDispense)
        resources.ApplyResources(Me.grpPostArcDispense, "grpPostArcDispense")
        Me.grpPostArcDispense.Name = "grpPostArcDispense"
        Me.grpPostArcDispense.TabStop = False
        '
        'tlpPostArcDispense
        '
        resources.ApplyResources(Me.tlpPostArcDispense, "tlpPostArcDispense")
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostSuckBack, 0, 7)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostDwell, 0, 0)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostRetractAcc, 0, 6)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostRetractSpeed, 0, 5)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostBacktrackLength, 1, 2)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostBacktrackLength, 0, 2)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostBacktrackGap, 1, 1)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostBacktrackGap, 0, 1)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostSuckBack, 1, 7)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostSuckBackUnit, 2, 7)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostBacktrackGapUnit, 2, 1)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostBacktrackLengthUnit, 2, 2)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostDwellUnit, 2, 0)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostBacktrackSpeed, 1, 3)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostDwell, 1, 0)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostBacktrackSpeed, 0, 3)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostBacktrackSpeedUnit, 2, 3)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostRetractDistance, 0, 4)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostRetractDistance, 1, 4)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostRetractSpeed, 1, 5)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostRetractDistanceUnit, 2, 4)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostRetractSpeedUnit, 2, 5)
        Me.tlpPostArcDispense.Controls.Add(Me.lbArcPostRetractAccUnit, 2, 6)
        Me.tlpPostArcDispense.Controls.Add(Me.textArcPostRetractAcc, 1, 6)
        Me.tlpPostArcDispense.Name = "tlpPostArcDispense"
        '
        'lbArcPostSuckBack
        '
        Me.lbArcPostSuckBack.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostSuckBack, "lbArcPostSuckBack")
        Me.lbArcPostSuckBack.Name = "lbArcPostSuckBack"
        '
        'lbArcPostDwell
        '
        Me.lbArcPostDwell.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostDwell, "lbArcPostDwell")
        Me.lbArcPostDwell.Name = "lbArcPostDwell"
        '
        'lbArcPostRetractAcc
        '
        Me.lbArcPostRetractAcc.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostRetractAcc, "lbArcPostRetractAcc")
        Me.lbArcPostRetractAcc.Name = "lbArcPostRetractAcc"
        '
        'lbArcPostRetractSpeed
        '
        Me.lbArcPostRetractSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostRetractSpeed, "lbArcPostRetractSpeed")
        Me.lbArcPostRetractSpeed.Name = "lbArcPostRetractSpeed"
        '
        'textArcPostBacktrackLength
        '
        Me.textArcPostBacktrackLength.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostBacktrackLength, "textArcPostBacktrackLength")
        Me.textArcPostBacktrackLength.Name = "textArcPostBacktrackLength"
        '
        'lbArcPostBacktrackLength
        '
        Me.lbArcPostBacktrackLength.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostBacktrackLength, "lbArcPostBacktrackLength")
        Me.lbArcPostBacktrackLength.Name = "lbArcPostBacktrackLength"
        '
        'textArcPostBacktrackGap
        '
        Me.textArcPostBacktrackGap.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostBacktrackGap, "textArcPostBacktrackGap")
        Me.textArcPostBacktrackGap.Name = "textArcPostBacktrackGap"
        '
        'lbArcPostBacktrackGap
        '
        Me.lbArcPostBacktrackGap.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostBacktrackGap, "lbArcPostBacktrackGap")
        Me.lbArcPostBacktrackGap.Name = "lbArcPostBacktrackGap"
        '
        'textArcPostSuckBack
        '
        Me.textArcPostSuckBack.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostSuckBack, "textArcPostSuckBack")
        Me.textArcPostSuckBack.Name = "textArcPostSuckBack"
        '
        'lbArcPostSuckBackUnit
        '
        Me.lbArcPostSuckBackUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostSuckBackUnit, "lbArcPostSuckBackUnit")
        Me.lbArcPostSuckBackUnit.Name = "lbArcPostSuckBackUnit"
        '
        'lbArcPostBacktrackGapUnit
        '
        Me.lbArcPostBacktrackGapUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostBacktrackGapUnit, "lbArcPostBacktrackGapUnit")
        Me.lbArcPostBacktrackGapUnit.Name = "lbArcPostBacktrackGapUnit"
        '
        'lbArcPostBacktrackLengthUnit
        '
        Me.lbArcPostBacktrackLengthUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostBacktrackLengthUnit, "lbArcPostBacktrackLengthUnit")
        Me.lbArcPostBacktrackLengthUnit.Name = "lbArcPostBacktrackLengthUnit"
        '
        'lbArcPostDwellUnit
        '
        Me.lbArcPostDwellUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostDwellUnit, "lbArcPostDwellUnit")
        Me.lbArcPostDwellUnit.Name = "lbArcPostDwellUnit"
        '
        'textArcPostBacktrackSpeed
        '
        Me.textArcPostBacktrackSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostBacktrackSpeed, "textArcPostBacktrackSpeed")
        Me.textArcPostBacktrackSpeed.Name = "textArcPostBacktrackSpeed"
        '
        'textArcPostDwell
        '
        Me.textArcPostDwell.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostDwell, "textArcPostDwell")
        Me.textArcPostDwell.Name = "textArcPostDwell"
        '
        'lbArcPostBacktrackSpeed
        '
        Me.lbArcPostBacktrackSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostBacktrackSpeed, "lbArcPostBacktrackSpeed")
        Me.lbArcPostBacktrackSpeed.Name = "lbArcPostBacktrackSpeed"
        '
        'lbArcPostBacktrackSpeedUnit
        '
        Me.lbArcPostBacktrackSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostBacktrackSpeedUnit, "lbArcPostBacktrackSpeedUnit")
        Me.lbArcPostBacktrackSpeedUnit.Name = "lbArcPostBacktrackSpeedUnit"
        '
        'lbArcPostRetractDistance
        '
        Me.lbArcPostRetractDistance.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostRetractDistance, "lbArcPostRetractDistance")
        Me.lbArcPostRetractDistance.Name = "lbArcPostRetractDistance"
        '
        'textArcPostRetractDistance
        '
        Me.textArcPostRetractDistance.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostRetractDistance, "textArcPostRetractDistance")
        Me.textArcPostRetractDistance.Name = "textArcPostRetractDistance"
        '
        'textArcPostRetractSpeed
        '
        Me.textArcPostRetractSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostRetractSpeed, "textArcPostRetractSpeed")
        Me.textArcPostRetractSpeed.Name = "textArcPostRetractSpeed"
        '
        'lbArcPostRetractDistanceUnit
        '
        Me.lbArcPostRetractDistanceUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostRetractDistanceUnit, "lbArcPostRetractDistanceUnit")
        Me.lbArcPostRetractDistanceUnit.Name = "lbArcPostRetractDistanceUnit"
        '
        'lbArcPostRetractSpeedUnit
        '
        Me.lbArcPostRetractSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostRetractSpeedUnit, "lbArcPostRetractSpeedUnit")
        Me.lbArcPostRetractSpeedUnit.Name = "lbArcPostRetractSpeedUnit"
        '
        'lbArcPostRetractAccUnit
        '
        Me.lbArcPostRetractAccUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbArcPostRetractAccUnit, "lbArcPostRetractAccUnit")
        Me.lbArcPostRetractAccUnit.Name = "lbArcPostRetractAccUnit"
        '
        'textArcPostRetractAcc
        '
        Me.textArcPostRetractAcc.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textArcPostRetractAcc, "textArcPostRetractAcc")
        Me.textArcPostRetractAcc.Name = "textArcPostRetractAcc"
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
        'ucArcTypeParameter
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ucArcTypeParameter"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.grpPreArcDispense.ResumeLayout(False)
        Me.tlpPreArcDispense.ResumeLayout(False)
        Me.tlpPreArcDispense.PerformLayout()
        Me.grpDuringArcDispense.ResumeLayout(False)
        Me.tlpDuringArcDispense.ResumeLayout(False)
        Me.tlpDuringArcDispense.PerformLayout()
        Me.grpPostArcDispense.ResumeLayout(False)
        Me.tlpPostArcDispense.ResumeLayout(False)
        Me.tlpPostArcDispense.PerformLayout()
        Me.tlpTool.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnArcDBDel As System.Windows.Forms.Button
    Friend WithEvents btnArcDBUpdate As System.Windows.Forms.Button
    Friend WithEvents btnArcDBAdd As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lstArcDB As System.Windows.Forms.ListBox
    Friend WithEvents txtArcDB As System.Windows.Forms.TextBox
    Friend WithEvents grpPreArcDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPreArcDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents textArcPreDownSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPreDownSpeed As System.Windows.Forms.Label
    Friend WithEvents textArcPreMoveDelayFactor As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPreMoveDelayFactor As System.Windows.Forms.Label
    Friend WithEvents lbArcPreMoveDelayFactorUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcPreDownSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents textArcPreDownAcc As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPreDownAcc As System.Windows.Forms.Label
    Friend WithEvents lbArcPreDownAccUnit As System.Windows.Forms.Label
    Friend WithEvents grpDuringArcDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpDuringArcDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbArcPostDwell As System.Windows.Forms.Label
    Friend WithEvents lbArcPostSuckBack As System.Windows.Forms.Label
    Friend WithEvents textArcDuringSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbArcDuringSpeed As System.Windows.Forms.Label
    Friend WithEvents textArcPreDispenseGap As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPreDispenseGap As System.Windows.Forms.Label
    Friend WithEvents lbArcPreDispenseGapUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcDuringSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents textArcDuringShutOffDistance As System.Windows.Forms.TextBox
    Friend WithEvents lbArcDuringShutOffDistance As System.Windows.Forms.Label
    Friend WithEvents lbArcDuringShutOffDistanceUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcPostSuckBackUnit As System.Windows.Forms.Label
    Friend WithEvents textArcPostSuckBack As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPostDwellUnit As System.Windows.Forms.Label
    Friend WithEvents textArcPostDwell As System.Windows.Forms.TextBox
    Friend WithEvents grpPostArcDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPostArcDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbArcPostRetractAcc As System.Windows.Forms.Label
    Friend WithEvents lbArcPostRetractSpeed As System.Windows.Forms.Label
    Friend WithEvents textArcPostBacktrackLength As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPostBacktrackLength As System.Windows.Forms.Label
    Friend WithEvents textArcPostBacktrackGap As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPostBacktrackGap As System.Windows.Forms.Label
    Friend WithEvents lbArcPostBacktrackGapUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcPostBacktrackLengthUnit As System.Windows.Forms.Label
    Friend WithEvents textArcPostBacktrackSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPostBacktrackSpeed As System.Windows.Forms.Label
    Friend WithEvents lbArcPostBacktrackSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcPostRetractDistance As System.Windows.Forms.Label
    Friend WithEvents textArcPostRetractDistance As System.Windows.Forms.TextBox
    Friend WithEvents textArcPostRetractSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbArcPostRetractDistanceUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcPostRetractSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents lbArcPostRetractAccUnit As System.Windows.Forms.Label
    Friend WithEvents textArcPostRetractAcc As System.Windows.Forms.TextBox
    Friend WithEvents tlpTool As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btSetDefaultValue As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

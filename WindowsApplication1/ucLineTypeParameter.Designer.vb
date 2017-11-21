<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucLineTypeParameter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucLineTypeParameter))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnLineDBDel = New System.Windows.Forms.Button()
        Me.btnLineDBUpdate = New System.Windows.Forms.Button()
        Me.btnLineDBAdd = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lstLineDB = New System.Windows.Forms.ListBox()
        Me.txtLineDB = New System.Windows.Forms.TextBox()
        Me.grpPreLineDispense = New System.Windows.Forms.GroupBox()
        Me.tlpPreLineDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.textLinePreDownSpeed = New System.Windows.Forms.TextBox()
        Me.lbLinePreDownSpeed = New System.Windows.Forms.Label()
        Me.textLinePreMoveDelayFactor = New System.Windows.Forms.TextBox()
        Me.lbLinePreMoveDelayFactor = New System.Windows.Forms.Label()
        Me.lbLinePreDispenseGapUnit = New System.Windows.Forms.Label()
        Me.textLinePreDispenseGap = New System.Windows.Forms.TextBox()
        Me.lbLinePreMoveDelayFactorUnit = New System.Windows.Forms.Label()
        Me.lbLinePreDispenseGap = New System.Windows.Forms.Label()
        Me.lbLinePreDownSpeedUnit = New System.Windows.Forms.Label()
        Me.textLinePreDownAcc = New System.Windows.Forms.TextBox()
        Me.lbLinePreDownAcc = New System.Windows.Forms.Label()
        Me.lbLinePreDownAccUnit = New System.Windows.Forms.Label()
        Me.grpDuringLineDispense = New System.Windows.Forms.GroupBox()
        Me.tlpDuringLineDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.textLineDuringSpeed = New System.Windows.Forms.TextBox()
        Me.lbLineDuringSpeed = New System.Windows.Forms.Label()
        Me.lbLineDuringSpeedUnit = New System.Windows.Forms.Label()
        Me.textLineDuringShutOffDistance = New System.Windows.Forms.TextBox()
        Me.lbLineDuringShutOffDistance = New System.Windows.Forms.Label()
        Me.lbLineDuringShutOffDistanceUnit = New System.Windows.Forms.Label()
        Me.grpPostLineDispense = New System.Windows.Forms.GroupBox()
        Me.tlpPostLineDispense = New System.Windows.Forms.TableLayoutPanel()
        Me.lbLinePostSuckBack = New System.Windows.Forms.Label()
        Me.lbLinePostDwell = New System.Windows.Forms.Label()
        Me.lbLinePostRetractAcc = New System.Windows.Forms.Label()
        Me.lbLinePostRetractSpeed = New System.Windows.Forms.Label()
        Me.textLinePostBacktrackLength = New System.Windows.Forms.TextBox()
        Me.lbLinePostBacktrackLength = New System.Windows.Forms.Label()
        Me.textLinePostBacktrackGap = New System.Windows.Forms.TextBox()
        Me.lbLinePostBacktrackGap = New System.Windows.Forms.Label()
        Me.textLinePostSuckBack = New System.Windows.Forms.TextBox()
        Me.lbLinePostSuckBackUnit = New System.Windows.Forms.Label()
        Me.lbLinePostBacktrackGapUnit = New System.Windows.Forms.Label()
        Me.lbLinePostBacktrackLengthUnit = New System.Windows.Forms.Label()
        Me.lbLinePostDwellUnit = New System.Windows.Forms.Label()
        Me.textLinePostBacktrackSpeed = New System.Windows.Forms.TextBox()
        Me.textLinePostDwell = New System.Windows.Forms.TextBox()
        Me.lbLinePostBacktrackSpeed = New System.Windows.Forms.Label()
        Me.lbLinePostBacktrackSpeedUnit = New System.Windows.Forms.Label()
        Me.lbLinePostRetractDistance = New System.Windows.Forms.Label()
        Me.textLinePostRetractDistance = New System.Windows.Forms.TextBox()
        Me.textLinePostRetractSpeed = New System.Windows.Forms.TextBox()
        Me.lbLinePostRetractDistanceUnit = New System.Windows.Forms.Label()
        Me.lbLinePostRetractSpeedUnit = New System.Windows.Forms.Label()
        Me.lbLinePostRetractAccUnit = New System.Windows.Forms.Label()
        Me.textLinePostRetractAcc = New System.Windows.Forms.TextBox()
        Me.tlpTool = New System.Windows.Forms.TableLayoutPanel()
        Me.btSetDefaultValue = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.grpPreLineDispense.SuspendLayout()
        Me.tlpPreLineDispense.SuspendLayout()
        Me.grpDuringLineDispense.SuspendLayout()
        Me.tlpDuringLineDispense.SuspendLayout()
        Me.grpPostLineDispense.SuspendLayout()
        Me.tlpPostLineDispense.SuspendLayout()
        Me.tlpTool.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpPreLineDispense, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpDuringLineDispense, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.grpPostLineDispense, 2, 2)
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
        Me.Panel1.Controls.Add(Me.btnLineDBDel)
        Me.Panel1.Controls.Add(Me.btnLineDBUpdate)
        Me.Panel1.Controls.Add(Me.btnLineDBAdd)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'btnLineDBDel
        '
        Me.btnLineDBDel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        resources.ApplyResources(Me.btnLineDBDel, "btnLineDBDel")
        Me.btnLineDBDel.Name = "btnLineDBDel"
        Me.ToolTip1.SetToolTip(Me.btnLineDBDel, resources.GetString("btnLineDBDel.ToolTip"))
        Me.btnLineDBDel.UseVisualStyleBackColor = True
        '
        'btnLineDBUpdate
        '
        Me.btnLineDBUpdate.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Save1
        resources.ApplyResources(Me.btnLineDBUpdate, "btnLineDBUpdate")
        Me.btnLineDBUpdate.Name = "btnLineDBUpdate"
        Me.ToolTip1.SetToolTip(Me.btnLineDBUpdate, resources.GetString("btnLineDBUpdate.ToolTip"))
        Me.btnLineDBUpdate.UseVisualStyleBackColor = True
        '
        'btnLineDBAdd
        '
        Me.btnLineDBAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        resources.ApplyResources(Me.btnLineDBAdd, "btnLineDBAdd")
        Me.btnLineDBAdd.Name = "btnLineDBAdd"
        Me.ToolTip1.SetToolTip(Me.btnLineDBAdd, resources.GetString("btnLineDBAdd.ToolTip"))
        Me.btnLineDBAdd.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lstLineDB)
        Me.Panel2.Controls.Add(Me.txtLineDB)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        Me.TableLayoutPanel1.SetRowSpan(Me.Panel2, 2)
        '
        'lstLineDB
        '
        resources.ApplyResources(Me.lstLineDB, "lstLineDB")
        Me.lstLineDB.FormattingEnabled = True
        Me.lstLineDB.Name = "lstLineDB"
        '
        'txtLineDB
        '
        resources.ApplyResources(Me.txtLineDB, "txtLineDB")
        Me.txtLineDB.Name = "txtLineDB"
        '
        'grpPreLineDispense
        '
        Me.grpPreLineDispense.Controls.Add(Me.tlpPreLineDispense)
        resources.ApplyResources(Me.grpPreLineDispense, "grpPreLineDispense")
        Me.grpPreLineDispense.Name = "grpPreLineDispense"
        Me.grpPreLineDispense.TabStop = False
        '
        'tlpPreLineDispense
        '
        resources.ApplyResources(Me.tlpPreLineDispense, "tlpPreLineDispense")
        Me.tlpPreLineDispense.Controls.Add(Me.textLinePreDownSpeed, 1, 1)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreDownSpeed, 0, 1)
        Me.tlpPreLineDispense.Controls.Add(Me.textLinePreMoveDelayFactor, 1, 0)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreMoveDelayFactor, 0, 0)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreDispenseGapUnit, 2, 3)
        Me.tlpPreLineDispense.Controls.Add(Me.textLinePreDispenseGap, 1, 3)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreMoveDelayFactorUnit, 2, 0)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreDispenseGap, 0, 3)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreDownSpeedUnit, 2, 1)
        Me.tlpPreLineDispense.Controls.Add(Me.textLinePreDownAcc, 1, 2)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreDownAcc, 0, 2)
        Me.tlpPreLineDispense.Controls.Add(Me.lbLinePreDownAccUnit, 2, 2)
        Me.tlpPreLineDispense.Name = "tlpPreLineDispense"
        '
        'textLinePreDownSpeed
        '
        Me.textLinePreDownSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePreDownSpeed, "textLinePreDownSpeed")
        Me.textLinePreDownSpeed.Name = "textLinePreDownSpeed"
        '
        'lbLinePreDownSpeed
        '
        Me.lbLinePreDownSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreDownSpeed, "lbLinePreDownSpeed")
        Me.lbLinePreDownSpeed.Name = "lbLinePreDownSpeed"
        '
        'textLinePreMoveDelayFactor
        '
        Me.textLinePreMoveDelayFactor.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePreMoveDelayFactor, "textLinePreMoveDelayFactor")
        Me.textLinePreMoveDelayFactor.Name = "textLinePreMoveDelayFactor"
        '
        'lbLinePreMoveDelayFactor
        '
        Me.lbLinePreMoveDelayFactor.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreMoveDelayFactor, "lbLinePreMoveDelayFactor")
        Me.lbLinePreMoveDelayFactor.Name = "lbLinePreMoveDelayFactor"
        '
        'lbLinePreDispenseGapUnit
        '
        Me.lbLinePreDispenseGapUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreDispenseGapUnit, "lbLinePreDispenseGapUnit")
        Me.lbLinePreDispenseGapUnit.Name = "lbLinePreDispenseGapUnit"
        '
        'textLinePreDispenseGap
        '
        Me.textLinePreDispenseGap.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePreDispenseGap, "textLinePreDispenseGap")
        Me.textLinePreDispenseGap.Name = "textLinePreDispenseGap"
        '
        'lbLinePreMoveDelayFactorUnit
        '
        Me.lbLinePreMoveDelayFactorUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreMoveDelayFactorUnit, "lbLinePreMoveDelayFactorUnit")
        Me.lbLinePreMoveDelayFactorUnit.Name = "lbLinePreMoveDelayFactorUnit"
        '
        'lbLinePreDispenseGap
        '
        Me.lbLinePreDispenseGap.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreDispenseGap, "lbLinePreDispenseGap")
        Me.lbLinePreDispenseGap.Name = "lbLinePreDispenseGap"
        '
        'lbLinePreDownSpeedUnit
        '
        Me.lbLinePreDownSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreDownSpeedUnit, "lbLinePreDownSpeedUnit")
        Me.lbLinePreDownSpeedUnit.Name = "lbLinePreDownSpeedUnit"
        '
        'textLinePreDownAcc
        '
        Me.textLinePreDownAcc.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePreDownAcc, "textLinePreDownAcc")
        Me.textLinePreDownAcc.Name = "textLinePreDownAcc"
        '
        'lbLinePreDownAcc
        '
        Me.lbLinePreDownAcc.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreDownAcc, "lbLinePreDownAcc")
        Me.lbLinePreDownAcc.Name = "lbLinePreDownAcc"
        '
        'lbLinePreDownAccUnit
        '
        Me.lbLinePreDownAccUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePreDownAccUnit, "lbLinePreDownAccUnit")
        Me.lbLinePreDownAccUnit.Name = "lbLinePreDownAccUnit"
        '
        'grpDuringLineDispense
        '
        Me.grpDuringLineDispense.Controls.Add(Me.tlpDuringLineDispense)
        resources.ApplyResources(Me.grpDuringLineDispense, "grpDuringLineDispense")
        Me.grpDuringLineDispense.Name = "grpDuringLineDispense"
        Me.grpDuringLineDispense.TabStop = False
        '
        'tlpDuringLineDispense
        '
        resources.ApplyResources(Me.tlpDuringLineDispense, "tlpDuringLineDispense")
        Me.tlpDuringLineDispense.Controls.Add(Me.textLineDuringSpeed, 1, 0)
        Me.tlpDuringLineDispense.Controls.Add(Me.lbLineDuringSpeed, 0, 0)
        Me.tlpDuringLineDispense.Controls.Add(Me.lbLineDuringSpeedUnit, 2, 0)
        Me.tlpDuringLineDispense.Controls.Add(Me.textLineDuringShutOffDistance, 1, 1)
        Me.tlpDuringLineDispense.Controls.Add(Me.lbLineDuringShutOffDistance, 0, 1)
        Me.tlpDuringLineDispense.Controls.Add(Me.lbLineDuringShutOffDistanceUnit, 2, 1)
        Me.tlpDuringLineDispense.Name = "tlpDuringLineDispense"
        '
        'textLineDuringSpeed
        '
        Me.textLineDuringSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLineDuringSpeed, "textLineDuringSpeed")
        Me.textLineDuringSpeed.Name = "textLineDuringSpeed"
        '
        'lbLineDuringSpeed
        '
        Me.lbLineDuringSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLineDuringSpeed, "lbLineDuringSpeed")
        Me.lbLineDuringSpeed.Name = "lbLineDuringSpeed"
        '
        'lbLineDuringSpeedUnit
        '
        Me.lbLineDuringSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLineDuringSpeedUnit, "lbLineDuringSpeedUnit")
        Me.lbLineDuringSpeedUnit.Name = "lbLineDuringSpeedUnit"
        '
        'textLineDuringShutOffDistance
        '
        Me.textLineDuringShutOffDistance.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLineDuringShutOffDistance, "textLineDuringShutOffDistance")
        Me.textLineDuringShutOffDistance.Name = "textLineDuringShutOffDistance"
        '
        'lbLineDuringShutOffDistance
        '
        Me.lbLineDuringShutOffDistance.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLineDuringShutOffDistance, "lbLineDuringShutOffDistance")
        Me.lbLineDuringShutOffDistance.Name = "lbLineDuringShutOffDistance"
        '
        'lbLineDuringShutOffDistanceUnit
        '
        Me.lbLineDuringShutOffDistanceUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLineDuringShutOffDistanceUnit, "lbLineDuringShutOffDistanceUnit")
        Me.lbLineDuringShutOffDistanceUnit.Name = "lbLineDuringShutOffDistanceUnit"
        '
        'grpPostLineDispense
        '
        Me.grpPostLineDispense.Controls.Add(Me.tlpPostLineDispense)
        resources.ApplyResources(Me.grpPostLineDispense, "grpPostLineDispense")
        Me.grpPostLineDispense.Name = "grpPostLineDispense"
        Me.grpPostLineDispense.TabStop = False
        '
        'tlpPostLineDispense
        '
        resources.ApplyResources(Me.tlpPostLineDispense, "tlpPostLineDispense")
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostSuckBack, 0, 7)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostDwell, 0, 0)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostRetractAcc, 0, 6)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostRetractSpeed, 0, 5)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostBacktrackLength, 1, 2)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostBacktrackLength, 0, 2)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostBacktrackGap, 1, 1)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostBacktrackGap, 0, 1)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostSuckBack, 1, 7)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostSuckBackUnit, 2, 7)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostBacktrackGapUnit, 2, 1)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostBacktrackLengthUnit, 2, 2)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostDwellUnit, 2, 0)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostBacktrackSpeed, 1, 3)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostDwell, 1, 0)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostBacktrackSpeed, 0, 3)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostBacktrackSpeedUnit, 2, 3)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostRetractDistance, 0, 4)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostRetractDistance, 1, 4)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostRetractSpeed, 1, 5)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostRetractDistanceUnit, 2, 4)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostRetractSpeedUnit, 2, 5)
        Me.tlpPostLineDispense.Controls.Add(Me.lbLinePostRetractAccUnit, 2, 6)
        Me.tlpPostLineDispense.Controls.Add(Me.textLinePostRetractAcc, 1, 6)
        Me.tlpPostLineDispense.Name = "tlpPostLineDispense"
        '
        'lbLinePostSuckBack
        '
        Me.lbLinePostSuckBack.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostSuckBack, "lbLinePostSuckBack")
        Me.lbLinePostSuckBack.Name = "lbLinePostSuckBack"
        '
        'lbLinePostDwell
        '
        Me.lbLinePostDwell.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostDwell, "lbLinePostDwell")
        Me.lbLinePostDwell.Name = "lbLinePostDwell"
        '
        'lbLinePostRetractAcc
        '
        Me.lbLinePostRetractAcc.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostRetractAcc, "lbLinePostRetractAcc")
        Me.lbLinePostRetractAcc.Name = "lbLinePostRetractAcc"
        '
        'lbLinePostRetractSpeed
        '
        Me.lbLinePostRetractSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostRetractSpeed, "lbLinePostRetractSpeed")
        Me.lbLinePostRetractSpeed.Name = "lbLinePostRetractSpeed"
        '
        'textLinePostBacktrackLength
        '
        Me.textLinePostBacktrackLength.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostBacktrackLength, "textLinePostBacktrackLength")
        Me.textLinePostBacktrackLength.Name = "textLinePostBacktrackLength"
        '
        'lbLinePostBacktrackLength
        '
        Me.lbLinePostBacktrackLength.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostBacktrackLength, "lbLinePostBacktrackLength")
        Me.lbLinePostBacktrackLength.Name = "lbLinePostBacktrackLength"
        '
        'textLinePostBacktrackGap
        '
        Me.textLinePostBacktrackGap.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostBacktrackGap, "textLinePostBacktrackGap")
        Me.textLinePostBacktrackGap.Name = "textLinePostBacktrackGap"
        '
        'lbLinePostBacktrackGap
        '
        Me.lbLinePostBacktrackGap.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostBacktrackGap, "lbLinePostBacktrackGap")
        Me.lbLinePostBacktrackGap.Name = "lbLinePostBacktrackGap"
        '
        'textLinePostSuckBack
        '
        Me.textLinePostSuckBack.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostSuckBack, "textLinePostSuckBack")
        Me.textLinePostSuckBack.Name = "textLinePostSuckBack"
        '
        'lbLinePostSuckBackUnit
        '
        Me.lbLinePostSuckBackUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostSuckBackUnit, "lbLinePostSuckBackUnit")
        Me.lbLinePostSuckBackUnit.Name = "lbLinePostSuckBackUnit"
        '
        'lbLinePostBacktrackGapUnit
        '
        Me.lbLinePostBacktrackGapUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostBacktrackGapUnit, "lbLinePostBacktrackGapUnit")
        Me.lbLinePostBacktrackGapUnit.Name = "lbLinePostBacktrackGapUnit"
        '
        'lbLinePostBacktrackLengthUnit
        '
        Me.lbLinePostBacktrackLengthUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostBacktrackLengthUnit, "lbLinePostBacktrackLengthUnit")
        Me.lbLinePostBacktrackLengthUnit.Name = "lbLinePostBacktrackLengthUnit"
        '
        'lbLinePostDwellUnit
        '
        Me.lbLinePostDwellUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostDwellUnit, "lbLinePostDwellUnit")
        Me.lbLinePostDwellUnit.Name = "lbLinePostDwellUnit"
        '
        'textLinePostBacktrackSpeed
        '
        Me.textLinePostBacktrackSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostBacktrackSpeed, "textLinePostBacktrackSpeed")
        Me.textLinePostBacktrackSpeed.Name = "textLinePostBacktrackSpeed"
        '
        'textLinePostDwell
        '
        Me.textLinePostDwell.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostDwell, "textLinePostDwell")
        Me.textLinePostDwell.Name = "textLinePostDwell"
        '
        'lbLinePostBacktrackSpeed
        '
        Me.lbLinePostBacktrackSpeed.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostBacktrackSpeed, "lbLinePostBacktrackSpeed")
        Me.lbLinePostBacktrackSpeed.Name = "lbLinePostBacktrackSpeed"
        '
        'lbLinePostBacktrackSpeedUnit
        '
        Me.lbLinePostBacktrackSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostBacktrackSpeedUnit, "lbLinePostBacktrackSpeedUnit")
        Me.lbLinePostBacktrackSpeedUnit.Name = "lbLinePostBacktrackSpeedUnit"
        '
        'lbLinePostRetractDistance
        '
        Me.lbLinePostRetractDistance.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostRetractDistance, "lbLinePostRetractDistance")
        Me.lbLinePostRetractDistance.Name = "lbLinePostRetractDistance"
        '
        'textLinePostRetractDistance
        '
        Me.textLinePostRetractDistance.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostRetractDistance, "textLinePostRetractDistance")
        Me.textLinePostRetractDistance.Name = "textLinePostRetractDistance"
        '
        'textLinePostRetractSpeed
        '
        Me.textLinePostRetractSpeed.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostRetractSpeed, "textLinePostRetractSpeed")
        Me.textLinePostRetractSpeed.Name = "textLinePostRetractSpeed"
        '
        'lbLinePostRetractDistanceUnit
        '
        Me.lbLinePostRetractDistanceUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostRetractDistanceUnit, "lbLinePostRetractDistanceUnit")
        Me.lbLinePostRetractDistanceUnit.Name = "lbLinePostRetractDistanceUnit"
        '
        'lbLinePostRetractSpeedUnit
        '
        Me.lbLinePostRetractSpeedUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostRetractSpeedUnit, "lbLinePostRetractSpeedUnit")
        Me.lbLinePostRetractSpeedUnit.Name = "lbLinePostRetractSpeedUnit"
        '
        'lbLinePostRetractAccUnit
        '
        Me.lbLinePostRetractAccUnit.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lbLinePostRetractAccUnit, "lbLinePostRetractAccUnit")
        Me.lbLinePostRetractAccUnit.Name = "lbLinePostRetractAccUnit"
        '
        'textLinePostRetractAcc
        '
        Me.textLinePostRetractAcc.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textLinePostRetractAcc, "textLinePostRetractAcc")
        Me.textLinePostRetractAcc.Name = "textLinePostRetractAcc"
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
        'ucLineTypeParameter
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ucLineTypeParameter"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.grpPreLineDispense.ResumeLayout(False)
        Me.tlpPreLineDispense.ResumeLayout(False)
        Me.tlpPreLineDispense.PerformLayout()
        Me.grpDuringLineDispense.ResumeLayout(False)
        Me.tlpDuringLineDispense.ResumeLayout(False)
        Me.tlpDuringLineDispense.PerformLayout()
        Me.grpPostLineDispense.ResumeLayout(False)
        Me.tlpPostLineDispense.ResumeLayout(False)
        Me.tlpPostLineDispense.PerformLayout()
        Me.tlpTool.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnLineDBDel As System.Windows.Forms.Button
    Friend WithEvents btnLineDBUpdate As System.Windows.Forms.Button
    Friend WithEvents btnLineDBAdd As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lstLineDB As System.Windows.Forms.ListBox
    Friend WithEvents txtLineDB As System.Windows.Forms.TextBox
    Friend WithEvents grpPreLineDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPreLineDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents textLinePreDownSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePreDownSpeed As System.Windows.Forms.Label
    Friend WithEvents textLinePreMoveDelayFactor As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePreMoveDelayFactor As System.Windows.Forms.Label
    Friend WithEvents lbLinePreMoveDelayFactorUnit As System.Windows.Forms.Label
    Friend WithEvents lbLinePreDownSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents textLinePreDownAcc As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePreDownAcc As System.Windows.Forms.Label
    Friend WithEvents lbLinePreDownAccUnit As System.Windows.Forms.Label
    Friend WithEvents grpDuringLineDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpDuringLineDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbLinePostDwell As System.Windows.Forms.Label
    Friend WithEvents lbLinePostSuckBack As System.Windows.Forms.Label
    Friend WithEvents textLineDuringSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbLineDuringSpeed As System.Windows.Forms.Label
    Friend WithEvents textLinePreDispenseGap As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePreDispenseGap As System.Windows.Forms.Label
    Friend WithEvents lbLinePreDispenseGapUnit As System.Windows.Forms.Label
    Friend WithEvents lbLineDuringSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents textLineDuringShutOffDistance As System.Windows.Forms.TextBox
    Friend WithEvents lbLineDuringShutOffDistance As System.Windows.Forms.Label
    Friend WithEvents lbLineDuringShutOffDistanceUnit As System.Windows.Forms.Label
    Friend WithEvents lbLinePostSuckBackUnit As System.Windows.Forms.Label
    Friend WithEvents textLinePostSuckBack As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePostDwellUnit As System.Windows.Forms.Label
    Friend WithEvents textLinePostDwell As System.Windows.Forms.TextBox
    Friend WithEvents grpPostLineDispense As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPostLineDispense As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbLinePostRetractAcc As System.Windows.Forms.Label
    Friend WithEvents lbLinePostRetractSpeed As System.Windows.Forms.Label
    Friend WithEvents textLinePostBacktrackLength As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePostBacktrackLength As System.Windows.Forms.Label
    Friend WithEvents textLinePostBacktrackGap As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePostBacktrackGap As System.Windows.Forms.Label
    Friend WithEvents lbLinePostBacktrackGapUnit As System.Windows.Forms.Label
    Friend WithEvents lbLinePostBacktrackLengthUnit As System.Windows.Forms.Label
    Friend WithEvents textLinePostBacktrackSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePostBacktrackSpeed As System.Windows.Forms.Label
    Friend WithEvents lbLinePostBacktrackSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents lbLinePostRetractDistance As System.Windows.Forms.Label
    Friend WithEvents textLinePostRetractDistance As System.Windows.Forms.TextBox
    Friend WithEvents textLinePostRetractSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lbLinePostRetractDistanceUnit As System.Windows.Forms.Label
    Friend WithEvents lbLinePostRetractSpeedUnit As System.Windows.Forms.Label
    Friend WithEvents lbLinePostRetractAccUnit As System.Windows.Forms.Label
    Friend WithEvents textLinePostRetractAcc As System.Windows.Forms.TextBox
    Friend WithEvents tlpTool As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btSetDefaultValue As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

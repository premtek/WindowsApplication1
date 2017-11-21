<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipe04Step
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe04Step))
        Me.tabStep = New System.Windows.Forms.TabControl()
        Me.tabSelectValve = New System.Windows.Forms.TabPage()
        Me.grpSelectValve = New System.Windows.Forms.GroupBox()
        Me.lbConvertTiltAngle = New System.Windows.Forms.Label()
        Me.cmbPosB = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSelectValveDone = New System.Windows.Forms.Button()
        Me.btnSelectValveCancel = New System.Windows.Forms.Button()
        Me.lblSelect = New System.Windows.Forms.Label()
        Me.cmbValve = New System.Windows.Forms.ComboBox()
        Me.TabContiStart = New System.Windows.Forms.TabPage()
        Me.grpCOntiStart = New System.Windows.Forms.GroupBox()
        Me.btnContiStartCancel = New System.Windows.Forms.Button()
        Me.btnContiStartDone = New System.Windows.Forms.Button()
        Me.TabContiEnd = New System.Windows.Forms.TabPage()
        Me.grpContiEnd = New System.Windows.Forms.GroupBox()
        Me.btnContiEndCancel = New System.Windows.Forms.Button()
        Me.btnContiEndDone = New System.Windows.Forms.Button()
        Me.tabMove3D = New System.Windows.Forms.TabPage()
        Me.grpMove3D = New System.Windows.Forms.GroupBox()
        Me.btnMove3DCancel = New System.Windows.Forms.Button()
        Me.btnMove3DGo = New System.Windows.Forms.Button()
        Me.btnMove3DSet = New System.Windows.Forms.Button()
        Me.lblMove3DEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblMove3DEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblMove3DEndPosXUnit = New System.Windows.Forms.Label()
        Me.txtMove3DEndPosZ = New System.Windows.Forms.TextBox()
        Me.txtMove3DEndPosY = New System.Windows.Forms.TextBox()
        Me.txtMove3DEndPosX = New System.Windows.Forms.TextBox()
        Me.lblMove3DEndPosZ = New System.Windows.Forms.Label()
        Me.lblMove3DEndPosY = New System.Windows.Forms.Label()
        Me.lblMove3DEndPosX = New System.Windows.Forms.Label()
        Me.btnMove3DDone = New System.Windows.Forms.Button()
        Me.tabDots3D = New System.Windows.Forms.TabPage()
        Me.grpDot3D = New System.Windows.Forms.GroupBox()
        Me.txtDot3DWeight = New System.Windows.Forms.TextBox()
        Me.lblDot3DWeightUnit = New System.Windows.Forms.Label()
        Me.lblDot3DWeight = New System.Windows.Forms.Label()
        Me.txtDots3DDot = New System.Windows.Forms.TextBox()
        Me.lblDots3DDot = New System.Windows.Forms.Label()
        Me.lblDots3DDotWeight = New System.Windows.Forms.Label()
        Me.txtDots3DDotWeight = New System.Windows.Forms.TextBox()
        Me.lblDots3DDotWeightUnit = New System.Windows.Forms.Label()
        Me.btnDot3DGetPos = New System.Windows.Forms.Button()
        Me.tlpDotValueUc = New System.Windows.Forms.TableLayoutPanel()
        Me.btDotTypeSelect = New System.Windows.Forms.Button()
        Me.cbDotTypeSelect = New System.Windows.Forms.ComboBox()
        Me.lbDotTypeSelect = New System.Windows.Forms.Label()
        Me.lblDots3DVelocityUnit = New System.Windows.Forms.Label()
        Me.txtDots3DVelocity = New System.Windows.Forms.TextBox()
        Me.lblDots3DVelocity = New System.Windows.Forms.Label()
        Me.btnDot3DCancel = New System.Windows.Forms.Button()
        Me.btnDots3DGo = New System.Windows.Forms.Button()
        Me.btnDots3DSet = New System.Windows.Forms.Button()
        Me.lblDots3DEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblDots3DEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblDots3DEndPosXUnit = New System.Windows.Forms.Label()
        Me.txtDots3DPosZ = New System.Windows.Forms.TextBox()
        Me.txtDots3DPosY = New System.Windows.Forms.TextBox()
        Me.txtDots3DPosX = New System.Windows.Forms.TextBox()
        Me.lblDots3DEndPosZ = New System.Windows.Forms.Label()
        Me.lblDots3DEndPosY = New System.Windows.Forms.Label()
        Me.lblDots3DEndPosX = New System.Windows.Forms.Label()
        Me.btnDots3DDone = New System.Windows.Forms.Button()
        Me.tabLine3D = New System.Windows.Forms.TabPage()
        Me.grpLine3D = New System.Windows.Forms.GroupBox()
        Me.nmuLine3DStartVelocity = New System.Windows.Forms.NumericUpDown()
        Me.lblLine3DStartVelocityUnit = New System.Windows.Forms.Label()
        Me.lblLine3DStartVelocity = New System.Windows.Forms.Label()
        Me.nmuLine3DWeight = New System.Windows.Forms.NumericUpDown()
        Me.nmuLine3DVelocity = New System.Windows.Forms.NumericUpDown()
        Me.nmuLine3DDot = New System.Windows.Forms.NumericUpDown()
        Me.lblLine3DVelocityUnit = New System.Windows.Forms.Label()
        Me.lblLine3DVelocity = New System.Windows.Forms.Label()
        Me.lblLine3DDotWeight = New System.Windows.Forms.Label()
        Me.txtLine3DDotWeight = New System.Windows.Forms.TextBox()
        Me.lblLine3DDotWeightUnit = New System.Windows.Forms.Label()
        Me.btnLine3DGetPos = New System.Windows.Forms.Button()
        Me.tlpLineValueUc = New System.Windows.Forms.TableLayoutPanel()
        Me.btLineTypeSelect = New System.Windows.Forms.Button()
        Me.cbLineTypeSelect = New System.Windows.Forms.ComboBox()
        Me.lbLineTypeSelect = New System.Windows.Forms.Label()
        Me.btnLine3DRefresh = New System.Windows.Forms.Button()
        Me.txtLine3DComment = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnLine3DCancel = New System.Windows.Forms.Button()
        Me.btnLine3DEndMove = New System.Windows.Forms.Button()
        Me.btnLine3DStartMove = New System.Windows.Forms.Button()
        Me.txtLine3DPitch = New System.Windows.Forms.TextBox()
        Me.lblLine3DPitch = New System.Windows.Forms.Label()
        Me.lblLine3DPitchUnit = New System.Windows.Forms.Label()
        Me.lblLine3DWeightUnit = New System.Windows.Forms.Label()
        Me.lblLine3DDot = New System.Windows.Forms.Label()
        Me.lblLine3DWeight = New System.Windows.Forms.Label()
        Me.btnLine3DEndSet = New System.Windows.Forms.Button()
        Me.btnLine3DStartSet = New System.Windows.Forms.Button()
        Me.lblLine3DStartPosZUnit = New System.Windows.Forms.Label()
        Me.txtLine3DStartPosZ = New System.Windows.Forms.TextBox()
        Me.lblLine3DStartPosZ = New System.Windows.Forms.Label()
        Me.lblLine3DEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblLine3DEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblLine3DEndPosXUnit = New System.Windows.Forms.Label()
        Me.lblLine3DStartPosYUnit = New System.Windows.Forms.Label()
        Me.lblLine3DStartPosXUnit = New System.Windows.Forms.Label()
        Me.txtLine3DEndPosZ = New System.Windows.Forms.TextBox()
        Me.txtLine3DEndPosY = New System.Windows.Forms.TextBox()
        Me.txtLine3DEndPosX = New System.Windows.Forms.TextBox()
        Me.txtLine3DStartPosY = New System.Windows.Forms.TextBox()
        Me.txtLine3DStartPosX = New System.Windows.Forms.TextBox()
        Me.lblLine3DStartPosY = New System.Windows.Forms.Label()
        Me.lblLine3DStartPosX = New System.Windows.Forms.Label()
        Me.lblLine3DEndPosZ = New System.Windows.Forms.Label()
        Me.lblLine3DEndPosY = New System.Windows.Forms.Label()
        Me.lblLine3DEndPosX = New System.Windows.Forms.Label()
        Me.btnLine3DDone = New System.Windows.Forms.Button()
        Me.tabArc = New System.Windows.Forms.TabPage()
        Me.grpArc = New System.Windows.Forms.GroupBox()
        Me.nmuArcStartVelocity = New System.Windows.Forms.NumericUpDown()
        Me.lblArcStartVelocityUnit = New System.Windows.Forms.Label()
        Me.lblArcStartVelocity = New System.Windows.Forms.Label()
        Me.nmuArcVelocity = New System.Windows.Forms.NumericUpDown()
        Me.nmuArcDot = New System.Windows.Forms.NumericUpDown()
        Me.nmuArcWeight = New System.Windows.Forms.NumericUpDown()
        Me.lblArcVelocityUnit = New System.Windows.Forms.Label()
        Me.lblArcVelocity = New System.Windows.Forms.Label()
        Me.lblArcDotWeight = New System.Windows.Forms.Label()
        Me.txtArcDotWeight = New System.Windows.Forms.TextBox()
        Me.lblArcDotWeightUnit = New System.Windows.Forms.Label()
        Me.btnArcGetPos = New System.Windows.Forms.Button()
        Me.tlpArcValueUc = New System.Windows.Forms.TableLayoutPanel()
        Me.btArcTypeSelect = New System.Windows.Forms.Button()
        Me.cbArcTypeSelect = New System.Windows.Forms.ComboBox()
        Me.lbArcTypeSelect = New System.Windows.Forms.Label()
        Me.btnArcRefresh = New System.Windows.Forms.Button()
        Me.txtArcComment = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnArcCancel = New System.Windows.Forms.Button()
        Me.txtArcPitch = New System.Windows.Forms.TextBox()
        Me.lblArcPitch = New System.Windows.Forms.Label()
        Me.lblArcPitchUnit = New System.Windows.Forms.Label()
        Me.lblArcWeightUnit = New System.Windows.Forms.Label()
        Me.lblArcDot = New System.Windows.Forms.Label()
        Me.lblArcWeight = New System.Windows.Forms.Label()
        Me.txtArcMidPosZ = New System.Windows.Forms.TextBox()
        Me.txtArcMidPosY = New System.Windows.Forms.TextBox()
        Me.txtArcMidPosX = New System.Windows.Forms.TextBox()
        Me.btnArcMidMove = New System.Windows.Forms.Button()
        Me.btnArcEndMove = New System.Windows.Forms.Button()
        Me.btnArcStartMove = New System.Windows.Forms.Button()
        Me.btnArcMidSet = New System.Windows.Forms.Button()
        Me.btnArcEndSet = New System.Windows.Forms.Button()
        Me.btnArcStartSet = New System.Windows.Forms.Button()
        Me.lblArcAngleUnit = New System.Windows.Forms.Label()
        Me.txtArcAngle = New System.Windows.Forms.TextBox()
        Me.lblArcAngle = New System.Windows.Forms.Label()
        Me.lblArcMidPosZUnit = New System.Windows.Forms.Label()
        Me.txtArcCenterPosZ = New System.Windows.Forms.TextBox()
        Me.lblArcMidPosYUnit = New System.Windows.Forms.Label()
        Me.lblArcMidPosXUnit = New System.Windows.Forms.Label()
        Me.txtArcCenterPosY = New System.Windows.Forms.TextBox()
        Me.txtArcCenterPosX = New System.Windows.Forms.TextBox()
        Me.lblArcStartPosZUnit = New System.Windows.Forms.Label()
        Me.txtArcStartPosZ = New System.Windows.Forms.TextBox()
        Me.lblArcStartPosZ = New System.Windows.Forms.Label()
        Me.lblArcEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblArcEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblArcEndPosXUnit = New System.Windows.Forms.Label()
        Me.lblArcStartPosYUnit = New System.Windows.Forms.Label()
        Me.lblArcStartPosXUnit = New System.Windows.Forms.Label()
        Me.txtArcEndPosZ = New System.Windows.Forms.TextBox()
        Me.txtArcEndPosY = New System.Windows.Forms.TextBox()
        Me.txtArcEndPosX = New System.Windows.Forms.TextBox()
        Me.txtArcStartPosY = New System.Windows.Forms.TextBox()
        Me.txtArcStartPosX = New System.Windows.Forms.TextBox()
        Me.lblArcStartPosY = New System.Windows.Forms.Label()
        Me.lblArcStartPosX = New System.Windows.Forms.Label()
        Me.lblArcMidPosZ = New System.Windows.Forms.Label()
        Me.lblArcMidPosY = New System.Windows.Forms.Label()
        Me.lblArcMidPosX = New System.Windows.Forms.Label()
        Me.lblArcEndPosZ = New System.Windows.Forms.Label()
        Me.lblArcEndPosY = New System.Windows.Forms.Label()
        Me.lblArcEndPosX = New System.Windows.Forms.Label()
        Me.btnArcDone = New System.Windows.Forms.Button()
        Me.tabCircle = New System.Windows.Forms.TabPage()
        Me.grpCircle = New System.Windows.Forms.GroupBox()
        Me.nmuCircleStartVelocity = New System.Windows.Forms.NumericUpDown()
        Me.lblCircleStartVelocityUnit = New System.Windows.Forms.Label()
        Me.lblCircleStartVelocity = New System.Windows.Forms.Label()
        Me.nmuCircleVelocity = New System.Windows.Forms.NumericUpDown()
        Me.nmuCircleDot = New System.Windows.Forms.NumericUpDown()
        Me.nmuCircleWeight = New System.Windows.Forms.NumericUpDown()
        Me.lblCircleVelocityUnit = New System.Windows.Forms.Label()
        Me.lblCircleVelocity = New System.Windows.Forms.Label()
        Me.lblCircleDotWeight = New System.Windows.Forms.Label()
        Me.txtCircleDotWeight = New System.Windows.Forms.TextBox()
        Me.lblCircleDotWeightUnit = New System.Windows.Forms.Label()
        Me.tlpCircleValueUc = New System.Windows.Forms.TableLayoutPanel()
        Me.btCircleTypeSelect = New System.Windows.Forms.Button()
        Me.cbCircleTypeSelect = New System.Windows.Forms.ComboBox()
        Me.lbCircleTypeSelect = New System.Windows.Forms.Label()
        Me.btnCircleRefresh = New System.Windows.Forms.Button()
        Me.txtCircleComment = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCircleCancel = New System.Windows.Forms.Button()
        Me.txtCirclePitch = New System.Windows.Forms.TextBox()
        Me.lblCirclePitch = New System.Windows.Forms.Label()
        Me.lblCirclePitchUnit = New System.Windows.Forms.Label()
        Me.lblCircleWeightUnit = New System.Windows.Forms.Label()
        Me.lblCircleDot = New System.Windows.Forms.Label()
        Me.lblCircleWeight = New System.Windows.Forms.Label()
        Me.txtCircleMidPosZ = New System.Windows.Forms.TextBox()
        Me.txtCircleMidPosY = New System.Windows.Forms.TextBox()
        Me.txtCircleMidPosX = New System.Windows.Forms.TextBox()
        Me.btnCircleStartMove = New System.Windows.Forms.Button()
        Me.btnCircleStartSet = New System.Windows.Forms.Button()
        Me.txtCircleStartPosZ = New System.Windows.Forms.TextBox()
        Me.txtCircleStartPosY = New System.Windows.Forms.TextBox()
        Me.txtCircleStartPosX = New System.Windows.Forms.TextBox()
        Me.lblCircleStartPosZUnit = New System.Windows.Forms.Label()
        Me.lblCircleStartPosYUnit = New System.Windows.Forms.Label()
        Me.lblCircleStartPosXUnit = New System.Windows.Forms.Label()
        Me.lblCircleStartPosZ = New System.Windows.Forms.Label()
        Me.lblCircleStartPosY = New System.Windows.Forms.Label()
        Me.lblCircleStartPosX = New System.Windows.Forms.Label()
        Me.btnCircleMidMove = New System.Windows.Forms.Button()
        Me.btnCircleEndMove = New System.Windows.Forms.Button()
        Me.btnCircleMidSet = New System.Windows.Forms.Button()
        Me.btnCircleEndSet = New System.Windows.Forms.Button()
        Me.lblCircleMidPosZUnit = New System.Windows.Forms.Label()
        Me.txtCircleCenterPosZ = New System.Windows.Forms.TextBox()
        Me.lblCircleMidPosYUnit = New System.Windows.Forms.Label()
        Me.lblCircleMidPosXUnit = New System.Windows.Forms.Label()
        Me.txtCircleCenterPosY = New System.Windows.Forms.TextBox()
        Me.txtCircleCenterPosX = New System.Windows.Forms.TextBox()
        Me.lblCircleEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblCircleEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblCircleEndPosXUnit = New System.Windows.Forms.Label()
        Me.txtCircleMid2PosZ = New System.Windows.Forms.TextBox()
        Me.txtCircleMid2PosY = New System.Windows.Forms.TextBox()
        Me.txtCircleMid2PosX = New System.Windows.Forms.TextBox()
        Me.lblCircleMidPosZ = New System.Windows.Forms.Label()
        Me.lblCircleMidPosY = New System.Windows.Forms.Label()
        Me.lblCircleMidPosX = New System.Windows.Forms.Label()
        Me.lblCircleEndPosZ = New System.Windows.Forms.Label()
        Me.lblCircleEndPosY = New System.Windows.Forms.Label()
        Me.lblCircleEndPosX = New System.Windows.Forms.Label()
        Me.btnCircleDone = New System.Windows.Forms.Button()
        Me.tabWait = New System.Windows.Forms.TabPage()
        Me.grpWait = New System.Windows.Forms.GroupBox()
        Me.btnWaitCancel = New System.Windows.Forms.Button()
        Me.btnWaitDone = New System.Windows.Forms.Button()
        Me.lblWaitDwellTimeUnit = New System.Windows.Forms.Label()
        Me.txtWaitDwellTime = New System.Windows.Forms.TextBox()
        Me.lblWaitDwellTime = New System.Windows.Forms.Label()
        Me.tabCircle3D = New System.Windows.Forms.TabPage()
        Me.grpCircle3D = New System.Windows.Forms.GroupBox()
        Me.tlpCircle3DValueUc = New System.Windows.Forms.TableLayoutPanel()
        Me.btCircle3DTypeSelect = New System.Windows.Forms.Button()
        Me.cbCircle3DTypeSelect = New System.Windows.Forms.ComboBox()
        Me.lbCircle3DTypeSelect = New System.Windows.Forms.Label()
        Me.btnCircle3DCancel = New System.Windows.Forms.Button()
        Me.btnCircle3DDone = New System.Windows.Forms.Button()
        Me.btnCircle3DCenterMove = New System.Windows.Forms.Button()
        Me.btnCircle3DEndMove = New System.Windows.Forms.Button()
        Me.btnCircleCenterSet = New System.Windows.Forms.Button()
        Me.btnCircle3DEndSet = New System.Windows.Forms.Button()
        Me.lblCircle3DCenterPosZUnit = New System.Windows.Forms.Label()
        Me.txtCircle3DCenterPosZ = New System.Windows.Forms.TextBox()
        Me.lblCircle3DCenterPosYUnit = New System.Windows.Forms.Label()
        Me.lblCircle3DCenterPosXUnit = New System.Windows.Forms.Label()
        Me.txtCircle3DCenterPosY = New System.Windows.Forms.TextBox()
        Me.txtCircle3DCenterPosX = New System.Windows.Forms.TextBox()
        Me.lblCircle3DEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblCircle3DEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblCircle3DEndPosXUnit = New System.Windows.Forms.Label()
        Me.txtCircle3DEndPosZ = New System.Windows.Forms.TextBox()
        Me.txtCircle3DEndPosY = New System.Windows.Forms.TextBox()
        Me.txtCircle3DEndPosX = New System.Windows.Forms.TextBox()
        Me.lblCircle3DCenterPosZ = New System.Windows.Forms.Label()
        Me.lblCircle3DCenterPosY = New System.Windows.Forms.Label()
        Me.lblCircle3DCenterPosX = New System.Windows.Forms.Label()
        Me.lblCircle3DEndPosZ = New System.Windows.Forms.Label()
        Me.lblCircle3DEndPosY = New System.Windows.Forms.Label()
        Me.lblCircle3DEndPosX = New System.Windows.Forms.Label()
        Me.tabArc3D = New System.Windows.Forms.TabPage()
        Me.grpArc3D = New System.Windows.Forms.GroupBox()
        Me.tlpArc3DValueUc = New System.Windows.Forms.TableLayoutPanel()
        Me.btArc3DTypeSelect = New System.Windows.Forms.Button()
        Me.cbArc3DTypeSelect = New System.Windows.Forms.ComboBox()
        Me.lbArc3DTypeSelect = New System.Windows.Forms.Label()
        Me.btnArc3DCancel = New System.Windows.Forms.Button()
        Me.btnArc3DDone = New System.Windows.Forms.Button()
        Me.btnArc3DCenterMove = New System.Windows.Forms.Button()
        Me.btnArc3DEndMove = New System.Windows.Forms.Button()
        Me.btnArc3DStartrMove = New System.Windows.Forms.Button()
        Me.btnArc3DCenterSet = New System.Windows.Forms.Button()
        Me.btnArc3DEndSet = New System.Windows.Forms.Button()
        Me.btnArc3DStartSet = New System.Windows.Forms.Button()
        Me.lblArc3DAngleUnit = New System.Windows.Forms.Label()
        Me.txtArc3DAngle = New System.Windows.Forms.TextBox()
        Me.lblArc3DAngle = New System.Windows.Forms.Label()
        Me.lblArc3DCenterPosZUnit = New System.Windows.Forms.Label()
        Me.txtArc3DCenterPosZ = New System.Windows.Forms.TextBox()
        Me.lblArc3DCenterPosYUnit = New System.Windows.Forms.Label()
        Me.lblArc3DCenterPosXUnit = New System.Windows.Forms.Label()
        Me.txtArc3DCenterPosY = New System.Windows.Forms.TextBox()
        Me.txtArc3DCenterPosX = New System.Windows.Forms.TextBox()
        Me.lblArc3DStartPosZUnit = New System.Windows.Forms.Label()
        Me.txtArc3DStartPosZ = New System.Windows.Forms.TextBox()
        Me.lblArc3DStartPosZ = New System.Windows.Forms.Label()
        Me.lblArc3DEndPosZUnit = New System.Windows.Forms.Label()
        Me.lblArc3DEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblArc3DEndPosXUnit = New System.Windows.Forms.Label()
        Me.lblArc3DStartPosYUnit = New System.Windows.Forms.Label()
        Me.lblArc3DStartPosXUnit = New System.Windows.Forms.Label()
        Me.txtArc3DEndPosZ = New System.Windows.Forms.TextBox()
        Me.txtArc3DEndPosY = New System.Windows.Forms.TextBox()
        Me.txtArc3DEndPosX = New System.Windows.Forms.TextBox()
        Me.txtArc3DStartPosY = New System.Windows.Forms.TextBox()
        Me.txtArc3DStartPosX = New System.Windows.Forms.TextBox()
        Me.lblArc3DStartPosY = New System.Windows.Forms.Label()
        Me.lblArc3DStartPosX = New System.Windows.Forms.Label()
        Me.lblArc3DCenterPosZ = New System.Windows.Forms.Label()
        Me.lblArc3DCenterPosY = New System.Windows.Forms.Label()
        Me.lblArc3DCenterPosX = New System.Windows.Forms.Label()
        Me.lblArc3DEndPosZ = New System.Windows.Forms.Label()
        Me.lblArc3DEndPosY = New System.Windows.Forms.Label()
        Me.lblArc3DEndPosX = New System.Windows.Forms.Label()
        Me.tabExtendOn = New System.Windows.Forms.TabPage()
        Me.grpExtendOn = New System.Windows.Forms.GroupBox()
        Me.btnExtendOnCancel = New System.Windows.Forms.Button()
        Me.btnExtendOnDone = New System.Windows.Forms.Button()
        Me.tabExtendOff = New System.Windows.Forms.TabPage()
        Me.grpExtendOff = New System.Windows.Forms.GroupBox()
        Me.btnExtendOffCancel = New System.Windows.Forms.Button()
        Me.btnExtendOffDone = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.tabStep.SuspendLayout()
        Me.tabSelectValve.SuspendLayout()
        Me.grpSelectValve.SuspendLayout()
        Me.TabContiStart.SuspendLayout()
        Me.grpCOntiStart.SuspendLayout()
        Me.TabContiEnd.SuspendLayout()
        Me.grpContiEnd.SuspendLayout()
        Me.tabMove3D.SuspendLayout()
        Me.grpMove3D.SuspendLayout()
        Me.tabDots3D.SuspendLayout()
        Me.grpDot3D.SuspendLayout()
        Me.tlpDotValueUc.SuspendLayout()
        Me.tabLine3D.SuspendLayout()
        Me.grpLine3D.SuspendLayout()
        CType(Me.nmuLine3DStartVelocity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuLine3DWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuLine3DVelocity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuLine3DDot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpLineValueUc.SuspendLayout()
        Me.tabArc.SuspendLayout()
        Me.grpArc.SuspendLayout()
        CType(Me.nmuArcStartVelocity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuArcVelocity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuArcDot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuArcWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpArcValueUc.SuspendLayout()
        Me.tabCircle.SuspendLayout()
        Me.grpCircle.SuspendLayout()
        CType(Me.nmuCircleStartVelocity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuCircleVelocity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuCircleDot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuCircleWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpCircleValueUc.SuspendLayout()
        Me.tabWait.SuspendLayout()
        Me.grpWait.SuspendLayout()
        Me.tabCircle3D.SuspendLayout()
        Me.grpCircle3D.SuspendLayout()
        Me.tlpCircle3DValueUc.SuspendLayout()
        Me.tabArc3D.SuspendLayout()
        Me.grpArc3D.SuspendLayout()
        Me.tlpArc3DValueUc.SuspendLayout()
        Me.tabExtendOn.SuspendLayout()
        Me.grpExtendOn.SuspendLayout()
        Me.tabExtendOff.SuspendLayout()
        Me.grpExtendOff.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabStep
        '
        Me.tabStep.Controls.Add(Me.tabSelectValve)
        Me.tabStep.Controls.Add(Me.TabContiStart)
        Me.tabStep.Controls.Add(Me.TabContiEnd)
        Me.tabStep.Controls.Add(Me.tabMove3D)
        Me.tabStep.Controls.Add(Me.tabDots3D)
        Me.tabStep.Controls.Add(Me.tabLine3D)
        Me.tabStep.Controls.Add(Me.tabArc)
        Me.tabStep.Controls.Add(Me.tabCircle)
        Me.tabStep.Controls.Add(Me.tabWait)
        Me.tabStep.Controls.Add(Me.tabCircle3D)
        Me.tabStep.Controls.Add(Me.tabArc3D)
        Me.tabStep.Controls.Add(Me.tabExtendOn)
        Me.tabStep.Controls.Add(Me.tabExtendOff)
        resources.ApplyResources(Me.tabStep, "tabStep")
        Me.tabStep.Name = "tabStep"
        Me.tabStep.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.tabStep, resources.GetString("tabStep.ToolTip"))
        '
        'tabSelectValve
        '
        Me.tabSelectValve.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabSelectValve.Controls.Add(Me.grpSelectValve)
        resources.ApplyResources(Me.tabSelectValve, "tabSelectValve")
        Me.tabSelectValve.Name = "tabSelectValve"
        '
        'grpSelectValve
        '
        Me.grpSelectValve.Controls.Add(Me.lbConvertTiltAngle)
        Me.grpSelectValve.Controls.Add(Me.cmbPosB)
        Me.grpSelectValve.Controls.Add(Me.Label1)
        Me.grpSelectValve.Controls.Add(Me.btnSelectValveDone)
        Me.grpSelectValve.Controls.Add(Me.btnSelectValveCancel)
        Me.grpSelectValve.Controls.Add(Me.lblSelect)
        Me.grpSelectValve.Controls.Add(Me.cmbValve)
        resources.ApplyResources(Me.grpSelectValve, "grpSelectValve")
        Me.grpSelectValve.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpSelectValve.Name = "grpSelectValve"
        Me.grpSelectValve.TabStop = False
        '
        'lbConvertTiltAngle
        '
        resources.ApplyResources(Me.lbConvertTiltAngle, "lbConvertTiltAngle")
        Me.lbConvertTiltAngle.Name = "lbConvertTiltAngle"
        '
        'cmbPosB
        '
        Me.cmbPosB.BackColor = System.Drawing.Color.White
        Me.cmbPosB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbPosB, "cmbPosB")
        Me.cmbPosB.FormattingEnabled = True
        Me.cmbPosB.Name = "cmbPosB"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'btnSelectValveDone
        '
        resources.ApplyResources(Me.btnSelectValveDone, "btnSelectValveDone")
        Me.btnSelectValveDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnSelectValveDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnSelectValveDone.FlatAppearance.BorderSize = 0
        Me.btnSelectValveDone.Name = "btnSelectValveDone"
        Me.ToolTip1.SetToolTip(Me.btnSelectValveDone, resources.GetString("btnSelectValveDone.ToolTip"))
        Me.btnSelectValveDone.UseVisualStyleBackColor = True
        '
        'btnSelectValveCancel
        '
        resources.ApplyResources(Me.btnSelectValveCancel, "btnSelectValveCancel")
        Me.btnSelectValveCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnSelectValveCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnSelectValveCancel.FlatAppearance.BorderSize = 0
        Me.btnSelectValveCancel.Name = "btnSelectValveCancel"
        Me.ToolTip1.SetToolTip(Me.btnSelectValveCancel, resources.GetString("btnSelectValveCancel.ToolTip"))
        Me.btnSelectValveCancel.UseVisualStyleBackColor = True
        '
        'lblSelect
        '
        resources.ApplyResources(Me.lblSelect, "lblSelect")
        Me.lblSelect.Name = "lblSelect"
        '
        'cmbValve
        '
        Me.cmbValve.BackColor = System.Drawing.Color.White
        Me.cmbValve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValve, "cmbValve")
        Me.cmbValve.FormattingEnabled = True
        Me.cmbValve.Name = "cmbValve"
        '
        'TabContiStart
        '
        Me.TabContiStart.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabContiStart.Controls.Add(Me.grpCOntiStart)
        resources.ApplyResources(Me.TabContiStart, "TabContiStart")
        Me.TabContiStart.Name = "TabContiStart"
        '
        'grpCOntiStart
        '
        Me.grpCOntiStart.Controls.Add(Me.btnContiStartCancel)
        Me.grpCOntiStart.Controls.Add(Me.btnContiStartDone)
        resources.ApplyResources(Me.grpCOntiStart, "grpCOntiStart")
        Me.grpCOntiStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCOntiStart.Name = "grpCOntiStart"
        Me.grpCOntiStart.TabStop = False
        '
        'btnContiStartCancel
        '
        resources.ApplyResources(Me.btnContiStartCancel, "btnContiStartCancel")
        Me.btnContiStartCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnContiStartCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnContiStartCancel.FlatAppearance.BorderSize = 0
        Me.btnContiStartCancel.Name = "btnContiStartCancel"
        Me.ToolTip1.SetToolTip(Me.btnContiStartCancel, resources.GetString("btnContiStartCancel.ToolTip"))
        Me.btnContiStartCancel.UseVisualStyleBackColor = True
        '
        'btnContiStartDone
        '
        resources.ApplyResources(Me.btnContiStartDone, "btnContiStartDone")
        Me.btnContiStartDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnContiStartDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnContiStartDone.FlatAppearance.BorderSize = 0
        Me.btnContiStartDone.Name = "btnContiStartDone"
        Me.ToolTip1.SetToolTip(Me.btnContiStartDone, resources.GetString("btnContiStartDone.ToolTip"))
        Me.btnContiStartDone.UseVisualStyleBackColor = True
        '
        'TabContiEnd
        '
        Me.TabContiEnd.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabContiEnd.Controls.Add(Me.grpContiEnd)
        resources.ApplyResources(Me.TabContiEnd, "TabContiEnd")
        Me.TabContiEnd.Name = "TabContiEnd"
        '
        'grpContiEnd
        '
        Me.grpContiEnd.Controls.Add(Me.btnContiEndCancel)
        Me.grpContiEnd.Controls.Add(Me.btnContiEndDone)
        resources.ApplyResources(Me.grpContiEnd, "grpContiEnd")
        Me.grpContiEnd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpContiEnd.Name = "grpContiEnd"
        Me.grpContiEnd.TabStop = False
        '
        'btnContiEndCancel
        '
        resources.ApplyResources(Me.btnContiEndCancel, "btnContiEndCancel")
        Me.btnContiEndCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnContiEndCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnContiEndCancel.FlatAppearance.BorderSize = 0
        Me.btnContiEndCancel.Name = "btnContiEndCancel"
        Me.ToolTip1.SetToolTip(Me.btnContiEndCancel, resources.GetString("btnContiEndCancel.ToolTip"))
        Me.btnContiEndCancel.UseVisualStyleBackColor = True
        '
        'btnContiEndDone
        '
        resources.ApplyResources(Me.btnContiEndDone, "btnContiEndDone")
        Me.btnContiEndDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnContiEndDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnContiEndDone.FlatAppearance.BorderSize = 0
        Me.btnContiEndDone.Name = "btnContiEndDone"
        Me.ToolTip1.SetToolTip(Me.btnContiEndDone, resources.GetString("btnContiEndDone.ToolTip"))
        Me.btnContiEndDone.UseVisualStyleBackColor = True
        '
        'tabMove3D
        '
        Me.tabMove3D.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabMove3D.Controls.Add(Me.grpMove3D)
        resources.ApplyResources(Me.tabMove3D, "tabMove3D")
        Me.tabMove3D.Name = "tabMove3D"
        '
        'grpMove3D
        '
        Me.grpMove3D.Controls.Add(Me.btnMove3DCancel)
        Me.grpMove3D.Controls.Add(Me.btnMove3DGo)
        Me.grpMove3D.Controls.Add(Me.btnMove3DSet)
        Me.grpMove3D.Controls.Add(Me.lblMove3DEndPosZUnit)
        Me.grpMove3D.Controls.Add(Me.lblMove3DEndPosYUnit)
        Me.grpMove3D.Controls.Add(Me.lblMove3DEndPosXUnit)
        Me.grpMove3D.Controls.Add(Me.txtMove3DEndPosZ)
        Me.grpMove3D.Controls.Add(Me.txtMove3DEndPosY)
        Me.grpMove3D.Controls.Add(Me.txtMove3DEndPosX)
        Me.grpMove3D.Controls.Add(Me.lblMove3DEndPosZ)
        Me.grpMove3D.Controls.Add(Me.lblMove3DEndPosY)
        Me.grpMove3D.Controls.Add(Me.lblMove3DEndPosX)
        Me.grpMove3D.Controls.Add(Me.btnMove3DDone)
        resources.ApplyResources(Me.grpMove3D, "grpMove3D")
        Me.grpMove3D.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpMove3D.Name = "grpMove3D"
        Me.grpMove3D.TabStop = False
        '
        'btnMove3DCancel
        '
        resources.ApplyResources(Me.btnMove3DCancel, "btnMove3DCancel")
        Me.btnMove3DCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnMove3DCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnMove3DCancel.FlatAppearance.BorderSize = 0
        Me.btnMove3DCancel.Name = "btnMove3DCancel"
        Me.ToolTip1.SetToolTip(Me.btnMove3DCancel, resources.GetString("btnMove3DCancel.ToolTip"))
        Me.btnMove3DCancel.UseVisualStyleBackColor = True
        '
        'btnMove3DGo
        '
        Me.btnMove3DGo.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnMove3DGo, "btnMove3DGo")
        Me.btnMove3DGo.FlatAppearance.BorderSize = 0
        Me.btnMove3DGo.Name = "btnMove3DGo"
        Me.ToolTip1.SetToolTip(Me.btnMove3DGo, resources.GetString("btnMove3DGo.ToolTip"))
        Me.btnMove3DGo.UseVisualStyleBackColor = True
        '
        'btnMove3DSet
        '
        Me.btnMove3DSet.BackColor = System.Drawing.SystemColors.Control
        Me.btnMove3DSet.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnMove3DSet, "btnMove3DSet")
        Me.btnMove3DSet.FlatAppearance.BorderSize = 0
        Me.btnMove3DSet.Name = "btnMove3DSet"
        Me.ToolTip1.SetToolTip(Me.btnMove3DSet, resources.GetString("btnMove3DSet.ToolTip"))
        Me.btnMove3DSet.UseVisualStyleBackColor = True
        '
        'lblMove3DEndPosZUnit
        '
        resources.ApplyResources(Me.lblMove3DEndPosZUnit, "lblMove3DEndPosZUnit")
        Me.lblMove3DEndPosZUnit.Name = "lblMove3DEndPosZUnit"
        '
        'lblMove3DEndPosYUnit
        '
        resources.ApplyResources(Me.lblMove3DEndPosYUnit, "lblMove3DEndPosYUnit")
        Me.lblMove3DEndPosYUnit.Name = "lblMove3DEndPosYUnit"
        '
        'lblMove3DEndPosXUnit
        '
        resources.ApplyResources(Me.lblMove3DEndPosXUnit, "lblMove3DEndPosXUnit")
        Me.lblMove3DEndPosXUnit.Name = "lblMove3DEndPosXUnit"
        '
        'txtMove3DEndPosZ
        '
        resources.ApplyResources(Me.txtMove3DEndPosZ, "txtMove3DEndPosZ")
        Me.txtMove3DEndPosZ.Name = "txtMove3DEndPosZ"
        '
        'txtMove3DEndPosY
        '
        resources.ApplyResources(Me.txtMove3DEndPosY, "txtMove3DEndPosY")
        Me.txtMove3DEndPosY.Name = "txtMove3DEndPosY"
        '
        'txtMove3DEndPosX
        '
        resources.ApplyResources(Me.txtMove3DEndPosX, "txtMove3DEndPosX")
        Me.txtMove3DEndPosX.Name = "txtMove3DEndPosX"
        '
        'lblMove3DEndPosZ
        '
        resources.ApplyResources(Me.lblMove3DEndPosZ, "lblMove3DEndPosZ")
        Me.lblMove3DEndPosZ.Name = "lblMove3DEndPosZ"
        '
        'lblMove3DEndPosY
        '
        resources.ApplyResources(Me.lblMove3DEndPosY, "lblMove3DEndPosY")
        Me.lblMove3DEndPosY.Name = "lblMove3DEndPosY"
        '
        'lblMove3DEndPosX
        '
        resources.ApplyResources(Me.lblMove3DEndPosX, "lblMove3DEndPosX")
        Me.lblMove3DEndPosX.Name = "lblMove3DEndPosX"
        '
        'btnMove3DDone
        '
        resources.ApplyResources(Me.btnMove3DDone, "btnMove3DDone")
        Me.btnMove3DDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnMove3DDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnMove3DDone.FlatAppearance.BorderSize = 0
        Me.btnMove3DDone.Name = "btnMove3DDone"
        Me.ToolTip1.SetToolTip(Me.btnMove3DDone, resources.GetString("btnMove3DDone.ToolTip"))
        Me.btnMove3DDone.UseVisualStyleBackColor = True
        '
        'tabDots3D
        '
        Me.tabDots3D.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabDots3D.Controls.Add(Me.grpDot3D)
        resources.ApplyResources(Me.tabDots3D, "tabDots3D")
        Me.tabDots3D.Name = "tabDots3D"
        '
        'grpDot3D
        '
        Me.grpDot3D.Controls.Add(Me.txtDot3DWeight)
        Me.grpDot3D.Controls.Add(Me.lblDot3DWeightUnit)
        Me.grpDot3D.Controls.Add(Me.lblDot3DWeight)
        Me.grpDot3D.Controls.Add(Me.txtDots3DDot)
        Me.grpDot3D.Controls.Add(Me.lblDots3DDot)
        Me.grpDot3D.Controls.Add(Me.lblDots3DDotWeight)
        Me.grpDot3D.Controls.Add(Me.txtDots3DDotWeight)
        Me.grpDot3D.Controls.Add(Me.lblDots3DDotWeightUnit)
        Me.grpDot3D.Controls.Add(Me.btnDot3DGetPos)
        Me.grpDot3D.Controls.Add(Me.tlpDotValueUc)
        Me.grpDot3D.Controls.Add(Me.lblDots3DVelocityUnit)
        Me.grpDot3D.Controls.Add(Me.txtDots3DVelocity)
        Me.grpDot3D.Controls.Add(Me.lblDots3DVelocity)
        Me.grpDot3D.Controls.Add(Me.btnDot3DCancel)
        Me.grpDot3D.Controls.Add(Me.btnDots3DGo)
        Me.grpDot3D.Controls.Add(Me.btnDots3DSet)
        Me.grpDot3D.Controls.Add(Me.lblDots3DEndPosZUnit)
        Me.grpDot3D.Controls.Add(Me.lblDots3DEndPosYUnit)
        Me.grpDot3D.Controls.Add(Me.lblDots3DEndPosXUnit)
        Me.grpDot3D.Controls.Add(Me.txtDots3DPosZ)
        Me.grpDot3D.Controls.Add(Me.txtDots3DPosY)
        Me.grpDot3D.Controls.Add(Me.txtDots3DPosX)
        Me.grpDot3D.Controls.Add(Me.lblDots3DEndPosZ)
        Me.grpDot3D.Controls.Add(Me.lblDots3DEndPosY)
        Me.grpDot3D.Controls.Add(Me.lblDots3DEndPosX)
        Me.grpDot3D.Controls.Add(Me.btnDots3DDone)
        resources.ApplyResources(Me.grpDot3D, "grpDot3D")
        Me.grpDot3D.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpDot3D.Name = "grpDot3D"
        Me.grpDot3D.TabStop = False
        '
        'txtDot3DWeight
        '
        resources.ApplyResources(Me.txtDot3DWeight, "txtDot3DWeight")
        Me.txtDot3DWeight.Name = "txtDot3DWeight"
        '
        'lblDot3DWeightUnit
        '
        resources.ApplyResources(Me.lblDot3DWeightUnit, "lblDot3DWeightUnit")
        Me.lblDot3DWeightUnit.Name = "lblDot3DWeightUnit"
        '
        'lblDot3DWeight
        '
        resources.ApplyResources(Me.lblDot3DWeight, "lblDot3DWeight")
        Me.lblDot3DWeight.Name = "lblDot3DWeight"
        '
        'txtDots3DDot
        '
        resources.ApplyResources(Me.txtDots3DDot, "txtDots3DDot")
        Me.txtDots3DDot.Name = "txtDots3DDot"
        '
        'lblDots3DDot
        '
        resources.ApplyResources(Me.lblDots3DDot, "lblDots3DDot")
        Me.lblDots3DDot.Name = "lblDots3DDot"
        '
        'lblDots3DDotWeight
        '
        resources.ApplyResources(Me.lblDots3DDotWeight, "lblDots3DDotWeight")
        Me.lblDots3DDotWeight.Name = "lblDots3DDotWeight"
        '
        'txtDots3DDotWeight
        '
        resources.ApplyResources(Me.txtDots3DDotWeight, "txtDots3DDotWeight")
        Me.txtDots3DDotWeight.Name = "txtDots3DDotWeight"
        Me.txtDots3DDotWeight.ReadOnly = True
        '
        'lblDots3DDotWeightUnit
        '
        resources.ApplyResources(Me.lblDots3DDotWeightUnit, "lblDots3DDotWeightUnit")
        Me.lblDots3DDotWeightUnit.Name = "lblDots3DDotWeightUnit"
        '
        'btnDot3DGetPos
        '
        resources.ApplyResources(Me.btnDot3DGetPos, "btnDot3DGetPos")
        Me.btnDot3DGetPos.FlatAppearance.BorderSize = 0
        Me.btnDot3DGetPos.Name = "btnDot3DGetPos"
        Me.btnDot3DGetPos.UseVisualStyleBackColor = True
        '
        'tlpDotValueUc
        '
        resources.ApplyResources(Me.tlpDotValueUc, "tlpDotValueUc")
        Me.tlpDotValueUc.Controls.Add(Me.btDotTypeSelect, 1, 0)
        Me.tlpDotValueUc.Controls.Add(Me.cbDotTypeSelect, 0, 1)
        Me.tlpDotValueUc.Controls.Add(Me.lbDotTypeSelect, 0, 0)
        Me.tlpDotValueUc.Name = "tlpDotValueUc"
        '
        'btDotTypeSelect
        '
        Me.btDotTypeSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btDotTypeSelect, "btDotTypeSelect")
        Me.btDotTypeSelect.FlatAppearance.BorderSize = 0
        Me.btDotTypeSelect.Name = "btDotTypeSelect"
        Me.tlpDotValueUc.SetRowSpan(Me.btDotTypeSelect, 2)
        Me.ToolTip1.SetToolTip(Me.btDotTypeSelect, resources.GetString("btDotTypeSelect.ToolTip"))
        Me.btDotTypeSelect.UseVisualStyleBackColor = True
        '
        'cbDotTypeSelect
        '
        Me.cbDotTypeSelect.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.cbDotTypeSelect, "cbDotTypeSelect")
        Me.cbDotTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDotTypeSelect.FormattingEnabled = True
        Me.cbDotTypeSelect.Name = "cbDotTypeSelect"
        '
        'lbDotTypeSelect
        '
        resources.ApplyResources(Me.lbDotTypeSelect, "lbDotTypeSelect")
        Me.lbDotTypeSelect.Name = "lbDotTypeSelect"
        '
        'lblDots3DVelocityUnit
        '
        resources.ApplyResources(Me.lblDots3DVelocityUnit, "lblDots3DVelocityUnit")
        Me.lblDots3DVelocityUnit.Name = "lblDots3DVelocityUnit"
        '
        'txtDots3DVelocity
        '
        resources.ApplyResources(Me.txtDots3DVelocity, "txtDots3DVelocity")
        Me.txtDots3DVelocity.Name = "txtDots3DVelocity"
        '
        'lblDots3DVelocity
        '
        resources.ApplyResources(Me.lblDots3DVelocity, "lblDots3DVelocity")
        Me.lblDots3DVelocity.Name = "lblDots3DVelocity"
        '
        'btnDot3DCancel
        '
        resources.ApplyResources(Me.btnDot3DCancel, "btnDot3DCancel")
        Me.btnDot3DCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnDot3DCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnDot3DCancel.FlatAppearance.BorderSize = 0
        Me.btnDot3DCancel.Name = "btnDot3DCancel"
        Me.ToolTip1.SetToolTip(Me.btnDot3DCancel, resources.GetString("btnDot3DCancel.ToolTip"))
        Me.btnDot3DCancel.UseVisualStyleBackColor = True
        '
        'btnDots3DGo
        '
        Me.btnDots3DGo.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnDots3DGo, "btnDots3DGo")
        Me.btnDots3DGo.FlatAppearance.BorderSize = 0
        Me.btnDots3DGo.Name = "btnDots3DGo"
        Me.ToolTip1.SetToolTip(Me.btnDots3DGo, resources.GetString("btnDots3DGo.ToolTip"))
        Me.btnDots3DGo.UseVisualStyleBackColor = True
        '
        'btnDots3DSet
        '
        Me.btnDots3DSet.BackColor = System.Drawing.SystemColors.Control
        Me.btnDots3DSet.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnDots3DSet, "btnDots3DSet")
        Me.btnDots3DSet.FlatAppearance.BorderSize = 0
        Me.btnDots3DSet.Name = "btnDots3DSet"
        Me.ToolTip1.SetToolTip(Me.btnDots3DSet, resources.GetString("btnDots3DSet.ToolTip"))
        Me.btnDots3DSet.UseVisualStyleBackColor = True
        '
        'lblDots3DEndPosZUnit
        '
        resources.ApplyResources(Me.lblDots3DEndPosZUnit, "lblDots3DEndPosZUnit")
        Me.lblDots3DEndPosZUnit.Name = "lblDots3DEndPosZUnit"
        '
        'lblDots3DEndPosYUnit
        '
        resources.ApplyResources(Me.lblDots3DEndPosYUnit, "lblDots3DEndPosYUnit")
        Me.lblDots3DEndPosYUnit.Name = "lblDots3DEndPosYUnit"
        '
        'lblDots3DEndPosXUnit
        '
        resources.ApplyResources(Me.lblDots3DEndPosXUnit, "lblDots3DEndPosXUnit")
        Me.lblDots3DEndPosXUnit.Name = "lblDots3DEndPosXUnit"
        '
        'txtDots3DPosZ
        '
        resources.ApplyResources(Me.txtDots3DPosZ, "txtDots3DPosZ")
        Me.txtDots3DPosZ.Name = "txtDots3DPosZ"
        '
        'txtDots3DPosY
        '
        resources.ApplyResources(Me.txtDots3DPosY, "txtDots3DPosY")
        Me.txtDots3DPosY.Name = "txtDots3DPosY"
        '
        'txtDots3DPosX
        '
        resources.ApplyResources(Me.txtDots3DPosX, "txtDots3DPosX")
        Me.txtDots3DPosX.Name = "txtDots3DPosX"
        '
        'lblDots3DEndPosZ
        '
        resources.ApplyResources(Me.lblDots3DEndPosZ, "lblDots3DEndPosZ")
        Me.lblDots3DEndPosZ.Name = "lblDots3DEndPosZ"
        '
        'lblDots3DEndPosY
        '
        resources.ApplyResources(Me.lblDots3DEndPosY, "lblDots3DEndPosY")
        Me.lblDots3DEndPosY.Name = "lblDots3DEndPosY"
        '
        'lblDots3DEndPosX
        '
        resources.ApplyResources(Me.lblDots3DEndPosX, "lblDots3DEndPosX")
        Me.lblDots3DEndPosX.Name = "lblDots3DEndPosX"
        '
        'btnDots3DDone
        '
        resources.ApplyResources(Me.btnDots3DDone, "btnDots3DDone")
        Me.btnDots3DDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnDots3DDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnDots3DDone.FlatAppearance.BorderSize = 0
        Me.btnDots3DDone.Name = "btnDots3DDone"
        Me.ToolTip1.SetToolTip(Me.btnDots3DDone, resources.GetString("btnDots3DDone.ToolTip"))
        Me.btnDots3DDone.UseVisualStyleBackColor = True
        '
        'tabLine3D
        '
        Me.tabLine3D.BackColor = System.Drawing.SystemColors.Control
        Me.tabLine3D.Controls.Add(Me.grpLine3D)
        resources.ApplyResources(Me.tabLine3D, "tabLine3D")
        Me.tabLine3D.Name = "tabLine3D"
        '
        'grpLine3D
        '
        Me.grpLine3D.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grpLine3D.Controls.Add(Me.nmuLine3DStartVelocity)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartVelocityUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartVelocity)
        Me.grpLine3D.Controls.Add(Me.nmuLine3DWeight)
        Me.grpLine3D.Controls.Add(Me.nmuLine3DVelocity)
        Me.grpLine3D.Controls.Add(Me.nmuLine3DDot)
        Me.grpLine3D.Controls.Add(Me.lblLine3DVelocityUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DVelocity)
        Me.grpLine3D.Controls.Add(Me.lblLine3DDotWeight)
        Me.grpLine3D.Controls.Add(Me.txtLine3DDotWeight)
        Me.grpLine3D.Controls.Add(Me.lblLine3DDotWeightUnit)
        Me.grpLine3D.Controls.Add(Me.btnLine3DGetPos)
        Me.grpLine3D.Controls.Add(Me.tlpLineValueUc)
        Me.grpLine3D.Controls.Add(Me.btnLine3DRefresh)
        Me.grpLine3D.Controls.Add(Me.txtLine3DComment)
        Me.grpLine3D.Controls.Add(Me.Label4)
        Me.grpLine3D.Controls.Add(Me.btnLine3DCancel)
        Me.grpLine3D.Controls.Add(Me.btnLine3DEndMove)
        Me.grpLine3D.Controls.Add(Me.btnLine3DStartMove)
        Me.grpLine3D.Controls.Add(Me.txtLine3DPitch)
        Me.grpLine3D.Controls.Add(Me.lblLine3DPitch)
        Me.grpLine3D.Controls.Add(Me.lblLine3DPitchUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DWeightUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DDot)
        Me.grpLine3D.Controls.Add(Me.lblLine3DWeight)
        Me.grpLine3D.Controls.Add(Me.btnLine3DEndSet)
        Me.grpLine3D.Controls.Add(Me.btnLine3DStartSet)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartPosZUnit)
        Me.grpLine3D.Controls.Add(Me.txtLine3DStartPosZ)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartPosZ)
        Me.grpLine3D.Controls.Add(Me.lblLine3DEndPosZUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DEndPosYUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DEndPosXUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartPosYUnit)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartPosXUnit)
        Me.grpLine3D.Controls.Add(Me.txtLine3DEndPosZ)
        Me.grpLine3D.Controls.Add(Me.txtLine3DEndPosY)
        Me.grpLine3D.Controls.Add(Me.txtLine3DEndPosX)
        Me.grpLine3D.Controls.Add(Me.txtLine3DStartPosY)
        Me.grpLine3D.Controls.Add(Me.txtLine3DStartPosX)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartPosY)
        Me.grpLine3D.Controls.Add(Me.lblLine3DStartPosX)
        Me.grpLine3D.Controls.Add(Me.lblLine3DEndPosZ)
        Me.grpLine3D.Controls.Add(Me.lblLine3DEndPosY)
        Me.grpLine3D.Controls.Add(Me.lblLine3DEndPosX)
        Me.grpLine3D.Controls.Add(Me.btnLine3DDone)
        resources.ApplyResources(Me.grpLine3D, "grpLine3D")
        Me.grpLine3D.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpLine3D.Name = "grpLine3D"
        Me.grpLine3D.TabStop = False
        '
        'nmuLine3DStartVelocity
        '
        resources.ApplyResources(Me.nmuLine3DStartVelocity, "nmuLine3DStartVelocity")
        Me.nmuLine3DStartVelocity.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmuLine3DStartVelocity.Name = "nmuLine3DStartVelocity"
        Me.nmuLine3DStartVelocity.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblLine3DStartVelocityUnit
        '
        resources.ApplyResources(Me.lblLine3DStartVelocityUnit, "lblLine3DStartVelocityUnit")
        Me.lblLine3DStartVelocityUnit.Name = "lblLine3DStartVelocityUnit"
        '
        'lblLine3DStartVelocity
        '
        resources.ApplyResources(Me.lblLine3DStartVelocity, "lblLine3DStartVelocity")
        Me.lblLine3DStartVelocity.Name = "lblLine3DStartVelocity"
        '
        'nmuLine3DWeight
        '
        Me.nmuLine3DWeight.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuLine3DWeight, "nmuLine3DWeight")
        Me.nmuLine3DWeight.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuLine3DWeight.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuLine3DWeight.Name = "nmuLine3DWeight"
        Me.nmuLine3DWeight.Value = New Decimal(New Integer() {1, 0, 0, 196608})
        '
        'nmuLine3DVelocity
        '
        resources.ApplyResources(Me.nmuLine3DVelocity, "nmuLine3DVelocity")
        Me.nmuLine3DVelocity.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmuLine3DVelocity.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nmuLine3DVelocity.Name = "nmuLine3DVelocity"
        Me.nmuLine3DVelocity.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'nmuLine3DDot
        '
        resources.ApplyResources(Me.nmuLine3DDot, "nmuLine3DDot")
        Me.nmuLine3DDot.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuLine3DDot.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nmuLine3DDot.Name = "nmuLine3DDot"
        Me.nmuLine3DDot.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblLine3DVelocityUnit
        '
        resources.ApplyResources(Me.lblLine3DVelocityUnit, "lblLine3DVelocityUnit")
        Me.lblLine3DVelocityUnit.Name = "lblLine3DVelocityUnit"
        '
        'lblLine3DVelocity
        '
        resources.ApplyResources(Me.lblLine3DVelocity, "lblLine3DVelocity")
        Me.lblLine3DVelocity.Name = "lblLine3DVelocity"
        '
        'lblLine3DDotWeight
        '
        resources.ApplyResources(Me.lblLine3DDotWeight, "lblLine3DDotWeight")
        Me.lblLine3DDotWeight.Name = "lblLine3DDotWeight"
        '
        'txtLine3DDotWeight
        '
        resources.ApplyResources(Me.txtLine3DDotWeight, "txtLine3DDotWeight")
        Me.txtLine3DDotWeight.Name = "txtLine3DDotWeight"
        Me.txtLine3DDotWeight.ReadOnly = True
        '
        'lblLine3DDotWeightUnit
        '
        resources.ApplyResources(Me.lblLine3DDotWeightUnit, "lblLine3DDotWeightUnit")
        Me.lblLine3DDotWeightUnit.Name = "lblLine3DDotWeightUnit"
        '
        'btnLine3DGetPos
        '
        resources.ApplyResources(Me.btnLine3DGetPos, "btnLine3DGetPos")
        Me.btnLine3DGetPos.FlatAppearance.BorderSize = 0
        Me.btnLine3DGetPos.Name = "btnLine3DGetPos"
        Me.btnLine3DGetPos.UseVisualStyleBackColor = True
        '
        'tlpLineValueUc
        '
        resources.ApplyResources(Me.tlpLineValueUc, "tlpLineValueUc")
        Me.tlpLineValueUc.Controls.Add(Me.btLineTypeSelect, 1, 0)
        Me.tlpLineValueUc.Controls.Add(Me.cbLineTypeSelect, 0, 1)
        Me.tlpLineValueUc.Controls.Add(Me.lbLineTypeSelect, 0, 0)
        Me.tlpLineValueUc.Name = "tlpLineValueUc"
        '
        'btLineTypeSelect
        '
        Me.btLineTypeSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btLineTypeSelect, "btLineTypeSelect")
        Me.btLineTypeSelect.FlatAppearance.BorderSize = 0
        Me.btLineTypeSelect.Name = "btLineTypeSelect"
        Me.tlpLineValueUc.SetRowSpan(Me.btLineTypeSelect, 2)
        Me.ToolTip1.SetToolTip(Me.btLineTypeSelect, resources.GetString("btLineTypeSelect.ToolTip"))
        Me.btLineTypeSelect.UseVisualStyleBackColor = True
        '
        'cbLineTypeSelect
        '
        Me.cbLineTypeSelect.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.cbLineTypeSelect, "cbLineTypeSelect")
        Me.cbLineTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLineTypeSelect.FormattingEnabled = True
        Me.cbLineTypeSelect.Name = "cbLineTypeSelect"
        '
        'lbLineTypeSelect
        '
        resources.ApplyResources(Me.lbLineTypeSelect, "lbLineTypeSelect")
        Me.lbLineTypeSelect.Name = "lbLineTypeSelect"
        '
        'btnLine3DRefresh
        '
        resources.ApplyResources(Me.btnLine3DRefresh, "btnLine3DRefresh")
        Me.btnLine3DRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.btnLine3DRefresh.FlatAppearance.BorderSize = 0
        Me.btnLine3DRefresh.Name = "btnLine3DRefresh"
        Me.btnLine3DRefresh.UseVisualStyleBackColor = True
        '
        'txtLine3DComment
        '
        resources.ApplyResources(Me.txtLine3DComment, "txtLine3DComment")
        Me.txtLine3DComment.Name = "txtLine3DComment"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'btnLine3DCancel
        '
        resources.ApplyResources(Me.btnLine3DCancel, "btnLine3DCancel")
        Me.btnLine3DCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnLine3DCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnLine3DCancel.FlatAppearance.BorderSize = 0
        Me.btnLine3DCancel.Name = "btnLine3DCancel"
        Me.ToolTip1.SetToolTip(Me.btnLine3DCancel, resources.GetString("btnLine3DCancel.ToolTip"))
        Me.btnLine3DCancel.UseVisualStyleBackColor = True
        '
        'btnLine3DEndMove
        '
        Me.btnLine3DEndMove.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnLine3DEndMove, "btnLine3DEndMove")
        Me.btnLine3DEndMove.FlatAppearance.BorderSize = 0
        Me.btnLine3DEndMove.Name = "btnLine3DEndMove"
        Me.ToolTip1.SetToolTip(Me.btnLine3DEndMove, resources.GetString("btnLine3DEndMove.ToolTip"))
        Me.btnLine3DEndMove.UseVisualStyleBackColor = True
        '
        'btnLine3DStartMove
        '
        Me.btnLine3DStartMove.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnLine3DStartMove, "btnLine3DStartMove")
        Me.btnLine3DStartMove.FlatAppearance.BorderSize = 0
        Me.btnLine3DStartMove.Name = "btnLine3DStartMove"
        Me.ToolTip1.SetToolTip(Me.btnLine3DStartMove, resources.GetString("btnLine3DStartMove.ToolTip"))
        Me.btnLine3DStartMove.UseVisualStyleBackColor = True
        '
        'txtLine3DPitch
        '
        resources.ApplyResources(Me.txtLine3DPitch, "txtLine3DPitch")
        Me.txtLine3DPitch.Name = "txtLine3DPitch"
        Me.txtLine3DPitch.ReadOnly = True
        '
        'lblLine3DPitch
        '
        Me.lblLine3DPitch.AutoEllipsis = True
        resources.ApplyResources(Me.lblLine3DPitch, "lblLine3DPitch")
        Me.lblLine3DPitch.Name = "lblLine3DPitch"
        '
        'lblLine3DPitchUnit
        '
        resources.ApplyResources(Me.lblLine3DPitchUnit, "lblLine3DPitchUnit")
        Me.lblLine3DPitchUnit.Name = "lblLine3DPitchUnit"
        '
        'lblLine3DWeightUnit
        '
        resources.ApplyResources(Me.lblLine3DWeightUnit, "lblLine3DWeightUnit")
        Me.lblLine3DWeightUnit.Name = "lblLine3DWeightUnit"
        '
        'lblLine3DDot
        '
        resources.ApplyResources(Me.lblLine3DDot, "lblLine3DDot")
        Me.lblLine3DDot.Name = "lblLine3DDot"
        '
        'lblLine3DWeight
        '
        resources.ApplyResources(Me.lblLine3DWeight, "lblLine3DWeight")
        Me.lblLine3DWeight.Name = "lblLine3DWeight"
        '
        'btnLine3DEndSet
        '
        Me.btnLine3DEndSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnLine3DEndSet, "btnLine3DEndSet")
        Me.btnLine3DEndSet.FlatAppearance.BorderSize = 0
        Me.btnLine3DEndSet.Name = "btnLine3DEndSet"
        Me.ToolTip1.SetToolTip(Me.btnLine3DEndSet, resources.GetString("btnLine3DEndSet.ToolTip"))
        Me.btnLine3DEndSet.UseVisualStyleBackColor = True
        '
        'btnLine3DStartSet
        '
        Me.btnLine3DStartSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnLine3DStartSet, "btnLine3DStartSet")
        Me.btnLine3DStartSet.FlatAppearance.BorderSize = 0
        Me.btnLine3DStartSet.Name = "btnLine3DStartSet"
        Me.ToolTip1.SetToolTip(Me.btnLine3DStartSet, resources.GetString("btnLine3DStartSet.ToolTip"))
        Me.btnLine3DStartSet.UseVisualStyleBackColor = True
        '
        'lblLine3DStartPosZUnit
        '
        resources.ApplyResources(Me.lblLine3DStartPosZUnit, "lblLine3DStartPosZUnit")
        Me.lblLine3DStartPosZUnit.Name = "lblLine3DStartPosZUnit"
        '
        'txtLine3DStartPosZ
        '
        resources.ApplyResources(Me.txtLine3DStartPosZ, "txtLine3DStartPosZ")
        Me.txtLine3DStartPosZ.Name = "txtLine3DStartPosZ"
        '
        'lblLine3DStartPosZ
        '
        resources.ApplyResources(Me.lblLine3DStartPosZ, "lblLine3DStartPosZ")
        Me.lblLine3DStartPosZ.Name = "lblLine3DStartPosZ"
        '
        'lblLine3DEndPosZUnit
        '
        resources.ApplyResources(Me.lblLine3DEndPosZUnit, "lblLine3DEndPosZUnit")
        Me.lblLine3DEndPosZUnit.Name = "lblLine3DEndPosZUnit"
        '
        'lblLine3DEndPosYUnit
        '
        resources.ApplyResources(Me.lblLine3DEndPosYUnit, "lblLine3DEndPosYUnit")
        Me.lblLine3DEndPosYUnit.Name = "lblLine3DEndPosYUnit"
        '
        'lblLine3DEndPosXUnit
        '
        resources.ApplyResources(Me.lblLine3DEndPosXUnit, "lblLine3DEndPosXUnit")
        Me.lblLine3DEndPosXUnit.Name = "lblLine3DEndPosXUnit"
        '
        'lblLine3DStartPosYUnit
        '
        resources.ApplyResources(Me.lblLine3DStartPosYUnit, "lblLine3DStartPosYUnit")
        Me.lblLine3DStartPosYUnit.Name = "lblLine3DStartPosYUnit"
        '
        'lblLine3DStartPosXUnit
        '
        resources.ApplyResources(Me.lblLine3DStartPosXUnit, "lblLine3DStartPosXUnit")
        Me.lblLine3DStartPosXUnit.Name = "lblLine3DStartPosXUnit"
        '
        'txtLine3DEndPosZ
        '
        resources.ApplyResources(Me.txtLine3DEndPosZ, "txtLine3DEndPosZ")
        Me.txtLine3DEndPosZ.Name = "txtLine3DEndPosZ"
        '
        'txtLine3DEndPosY
        '
        resources.ApplyResources(Me.txtLine3DEndPosY, "txtLine3DEndPosY")
        Me.txtLine3DEndPosY.Name = "txtLine3DEndPosY"
        '
        'txtLine3DEndPosX
        '
        resources.ApplyResources(Me.txtLine3DEndPosX, "txtLine3DEndPosX")
        Me.txtLine3DEndPosX.Name = "txtLine3DEndPosX"
        '
        'txtLine3DStartPosY
        '
        resources.ApplyResources(Me.txtLine3DStartPosY, "txtLine3DStartPosY")
        Me.txtLine3DStartPosY.Name = "txtLine3DStartPosY"
        '
        'txtLine3DStartPosX
        '
        resources.ApplyResources(Me.txtLine3DStartPosX, "txtLine3DStartPosX")
        Me.txtLine3DStartPosX.Name = "txtLine3DStartPosX"
        '
        'lblLine3DStartPosY
        '
        resources.ApplyResources(Me.lblLine3DStartPosY, "lblLine3DStartPosY")
        Me.lblLine3DStartPosY.Name = "lblLine3DStartPosY"
        '
        'lblLine3DStartPosX
        '
        resources.ApplyResources(Me.lblLine3DStartPosX, "lblLine3DStartPosX")
        Me.lblLine3DStartPosX.Name = "lblLine3DStartPosX"
        '
        'lblLine3DEndPosZ
        '
        resources.ApplyResources(Me.lblLine3DEndPosZ, "lblLine3DEndPosZ")
        Me.lblLine3DEndPosZ.Name = "lblLine3DEndPosZ"
        '
        'lblLine3DEndPosY
        '
        resources.ApplyResources(Me.lblLine3DEndPosY, "lblLine3DEndPosY")
        Me.lblLine3DEndPosY.Name = "lblLine3DEndPosY"
        '
        'lblLine3DEndPosX
        '
        resources.ApplyResources(Me.lblLine3DEndPosX, "lblLine3DEndPosX")
        Me.lblLine3DEndPosX.Name = "lblLine3DEndPosX"
        '
        'btnLine3DDone
        '
        resources.ApplyResources(Me.btnLine3DDone, "btnLine3DDone")
        Me.btnLine3DDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnLine3DDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnLine3DDone.FlatAppearance.BorderSize = 0
        Me.btnLine3DDone.Name = "btnLine3DDone"
        Me.ToolTip1.SetToolTip(Me.btnLine3DDone, resources.GetString("btnLine3DDone.ToolTip"))
        Me.btnLine3DDone.UseVisualStyleBackColor = True
        '
        'tabArc
        '
        Me.tabArc.BackColor = System.Drawing.SystemColors.Control
        Me.tabArc.Controls.Add(Me.grpArc)
        resources.ApplyResources(Me.tabArc, "tabArc")
        Me.tabArc.Name = "tabArc"
        '
        'grpArc
        '
        Me.grpArc.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grpArc.Controls.Add(Me.nmuArcStartVelocity)
        Me.grpArc.Controls.Add(Me.lblArcStartVelocityUnit)
        Me.grpArc.Controls.Add(Me.lblArcStartVelocity)
        Me.grpArc.Controls.Add(Me.nmuArcVelocity)
        Me.grpArc.Controls.Add(Me.nmuArcDot)
        Me.grpArc.Controls.Add(Me.nmuArcWeight)
        Me.grpArc.Controls.Add(Me.lblArcVelocityUnit)
        Me.grpArc.Controls.Add(Me.lblArcVelocity)
        Me.grpArc.Controls.Add(Me.lblArcDotWeight)
        Me.grpArc.Controls.Add(Me.txtArcDotWeight)
        Me.grpArc.Controls.Add(Me.lblArcDotWeightUnit)
        Me.grpArc.Controls.Add(Me.btnArcGetPos)
        Me.grpArc.Controls.Add(Me.tlpArcValueUc)
        Me.grpArc.Controls.Add(Me.btnArcRefresh)
        Me.grpArc.Controls.Add(Me.txtArcComment)
        Me.grpArc.Controls.Add(Me.Label3)
        Me.grpArc.Controls.Add(Me.btnArcCancel)
        Me.grpArc.Controls.Add(Me.txtArcPitch)
        Me.grpArc.Controls.Add(Me.lblArcPitch)
        Me.grpArc.Controls.Add(Me.lblArcPitchUnit)
        Me.grpArc.Controls.Add(Me.lblArcWeightUnit)
        Me.grpArc.Controls.Add(Me.lblArcDot)
        Me.grpArc.Controls.Add(Me.lblArcWeight)
        Me.grpArc.Controls.Add(Me.txtArcMidPosZ)
        Me.grpArc.Controls.Add(Me.txtArcMidPosY)
        Me.grpArc.Controls.Add(Me.txtArcMidPosX)
        Me.grpArc.Controls.Add(Me.btnArcMidMove)
        Me.grpArc.Controls.Add(Me.btnArcEndMove)
        Me.grpArc.Controls.Add(Me.btnArcStartMove)
        Me.grpArc.Controls.Add(Me.btnArcMidSet)
        Me.grpArc.Controls.Add(Me.btnArcEndSet)
        Me.grpArc.Controls.Add(Me.btnArcStartSet)
        Me.grpArc.Controls.Add(Me.lblArcAngleUnit)
        Me.grpArc.Controls.Add(Me.txtArcAngle)
        Me.grpArc.Controls.Add(Me.lblArcAngle)
        Me.grpArc.Controls.Add(Me.lblArcMidPosZUnit)
        Me.grpArc.Controls.Add(Me.txtArcCenterPosZ)
        Me.grpArc.Controls.Add(Me.lblArcMidPosYUnit)
        Me.grpArc.Controls.Add(Me.lblArcMidPosXUnit)
        Me.grpArc.Controls.Add(Me.txtArcCenterPosY)
        Me.grpArc.Controls.Add(Me.txtArcCenterPosX)
        Me.grpArc.Controls.Add(Me.lblArcStartPosZUnit)
        Me.grpArc.Controls.Add(Me.txtArcStartPosZ)
        Me.grpArc.Controls.Add(Me.lblArcStartPosZ)
        Me.grpArc.Controls.Add(Me.lblArcEndPosZUnit)
        Me.grpArc.Controls.Add(Me.lblArcEndPosYUnit)
        Me.grpArc.Controls.Add(Me.lblArcEndPosXUnit)
        Me.grpArc.Controls.Add(Me.lblArcStartPosYUnit)
        Me.grpArc.Controls.Add(Me.lblArcStartPosXUnit)
        Me.grpArc.Controls.Add(Me.txtArcEndPosZ)
        Me.grpArc.Controls.Add(Me.txtArcEndPosY)
        Me.grpArc.Controls.Add(Me.txtArcEndPosX)
        Me.grpArc.Controls.Add(Me.txtArcStartPosY)
        Me.grpArc.Controls.Add(Me.txtArcStartPosX)
        Me.grpArc.Controls.Add(Me.lblArcStartPosY)
        Me.grpArc.Controls.Add(Me.lblArcStartPosX)
        Me.grpArc.Controls.Add(Me.lblArcMidPosZ)
        Me.grpArc.Controls.Add(Me.lblArcMidPosY)
        Me.grpArc.Controls.Add(Me.lblArcMidPosX)
        Me.grpArc.Controls.Add(Me.lblArcEndPosZ)
        Me.grpArc.Controls.Add(Me.lblArcEndPosY)
        Me.grpArc.Controls.Add(Me.lblArcEndPosX)
        Me.grpArc.Controls.Add(Me.btnArcDone)
        resources.ApplyResources(Me.grpArc, "grpArc")
        Me.grpArc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpArc.Name = "grpArc"
        Me.grpArc.TabStop = False
        '
        'nmuArcStartVelocity
        '
        resources.ApplyResources(Me.nmuArcStartVelocity, "nmuArcStartVelocity")
        Me.nmuArcStartVelocity.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuArcStartVelocity.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuArcStartVelocity.Name = "nmuArcStartVelocity"
        Me.nmuArcStartVelocity.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblArcStartVelocityUnit
        '
        resources.ApplyResources(Me.lblArcStartVelocityUnit, "lblArcStartVelocityUnit")
        Me.lblArcStartVelocityUnit.Name = "lblArcStartVelocityUnit"
        '
        'lblArcStartVelocity
        '
        resources.ApplyResources(Me.lblArcStartVelocity, "lblArcStartVelocity")
        Me.lblArcStartVelocity.Name = "lblArcStartVelocity"
        '
        'nmuArcVelocity
        '
        resources.ApplyResources(Me.nmuArcVelocity, "nmuArcVelocity")
        Me.nmuArcVelocity.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuArcVelocity.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuArcVelocity.Name = "nmuArcVelocity"
        Me.nmuArcVelocity.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nmuArcDot
        '
        resources.ApplyResources(Me.nmuArcDot, "nmuArcDot")
        Me.nmuArcDot.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuArcDot.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuArcDot.Name = "nmuArcDot"
        Me.nmuArcDot.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nmuArcWeight
        '
        Me.nmuArcWeight.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuArcWeight, "nmuArcWeight")
        Me.nmuArcWeight.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuArcWeight.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuArcWeight.Name = "nmuArcWeight"
        Me.nmuArcWeight.Value = New Decimal(New Integer() {1, 0, 0, 196608})
        '
        'lblArcVelocityUnit
        '
        resources.ApplyResources(Me.lblArcVelocityUnit, "lblArcVelocityUnit")
        Me.lblArcVelocityUnit.Name = "lblArcVelocityUnit"
        '
        'lblArcVelocity
        '
        resources.ApplyResources(Me.lblArcVelocity, "lblArcVelocity")
        Me.lblArcVelocity.Name = "lblArcVelocity"
        '
        'lblArcDotWeight
        '
        resources.ApplyResources(Me.lblArcDotWeight, "lblArcDotWeight")
        Me.lblArcDotWeight.Name = "lblArcDotWeight"
        '
        'txtArcDotWeight
        '
        resources.ApplyResources(Me.txtArcDotWeight, "txtArcDotWeight")
        Me.txtArcDotWeight.Name = "txtArcDotWeight"
        Me.txtArcDotWeight.ReadOnly = True
        '
        'lblArcDotWeightUnit
        '
        resources.ApplyResources(Me.lblArcDotWeightUnit, "lblArcDotWeightUnit")
        Me.lblArcDotWeightUnit.Name = "lblArcDotWeightUnit"
        '
        'btnArcGetPos
        '
        resources.ApplyResources(Me.btnArcGetPos, "btnArcGetPos")
        Me.btnArcGetPos.FlatAppearance.BorderSize = 0
        Me.btnArcGetPos.Name = "btnArcGetPos"
        Me.btnArcGetPos.UseVisualStyleBackColor = True
        '
        'tlpArcValueUc
        '
        resources.ApplyResources(Me.tlpArcValueUc, "tlpArcValueUc")
        Me.tlpArcValueUc.Controls.Add(Me.btArcTypeSelect, 1, 0)
        Me.tlpArcValueUc.Controls.Add(Me.cbArcTypeSelect, 0, 1)
        Me.tlpArcValueUc.Controls.Add(Me.lbArcTypeSelect, 0, 0)
        Me.tlpArcValueUc.Name = "tlpArcValueUc"
        '
        'btArcTypeSelect
        '
        Me.btArcTypeSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btArcTypeSelect, "btArcTypeSelect")
        Me.btArcTypeSelect.FlatAppearance.BorderSize = 0
        Me.btArcTypeSelect.Name = "btArcTypeSelect"
        Me.tlpArcValueUc.SetRowSpan(Me.btArcTypeSelect, 2)
        Me.ToolTip1.SetToolTip(Me.btArcTypeSelect, resources.GetString("btArcTypeSelect.ToolTip"))
        Me.btArcTypeSelect.UseVisualStyleBackColor = True
        '
        'cbArcTypeSelect
        '
        Me.cbArcTypeSelect.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.cbArcTypeSelect, "cbArcTypeSelect")
        Me.cbArcTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbArcTypeSelect.FormattingEnabled = True
        Me.cbArcTypeSelect.Name = "cbArcTypeSelect"
        '
        'lbArcTypeSelect
        '
        resources.ApplyResources(Me.lbArcTypeSelect, "lbArcTypeSelect")
        Me.lbArcTypeSelect.Name = "lbArcTypeSelect"
        '
        'btnArcRefresh
        '
        resources.ApplyResources(Me.btnArcRefresh, "btnArcRefresh")
        Me.btnArcRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.btnArcRefresh.FlatAppearance.BorderSize = 0
        Me.btnArcRefresh.Name = "btnArcRefresh"
        Me.btnArcRefresh.UseVisualStyleBackColor = True
        '
        'txtArcComment
        '
        resources.ApplyResources(Me.txtArcComment, "txtArcComment")
        Me.txtArcComment.Name = "txtArcComment"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'btnArcCancel
        '
        resources.ApplyResources(Me.btnArcCancel, "btnArcCancel")
        Me.btnArcCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnArcCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnArcCancel.FlatAppearance.BorderSize = 0
        Me.btnArcCancel.Name = "btnArcCancel"
        Me.ToolTip1.SetToolTip(Me.btnArcCancel, resources.GetString("btnArcCancel.ToolTip"))
        Me.btnArcCancel.UseVisualStyleBackColor = True
        '
        'txtArcPitch
        '
        resources.ApplyResources(Me.txtArcPitch, "txtArcPitch")
        Me.txtArcPitch.Name = "txtArcPitch"
        Me.txtArcPitch.ReadOnly = True
        '
        'lblArcPitch
        '
        Me.lblArcPitch.AutoEllipsis = True
        resources.ApplyResources(Me.lblArcPitch, "lblArcPitch")
        Me.lblArcPitch.Name = "lblArcPitch"
        '
        'lblArcPitchUnit
        '
        resources.ApplyResources(Me.lblArcPitchUnit, "lblArcPitchUnit")
        Me.lblArcPitchUnit.Name = "lblArcPitchUnit"
        '
        'lblArcWeightUnit
        '
        resources.ApplyResources(Me.lblArcWeightUnit, "lblArcWeightUnit")
        Me.lblArcWeightUnit.Name = "lblArcWeightUnit"
        '
        'lblArcDot
        '
        resources.ApplyResources(Me.lblArcDot, "lblArcDot")
        Me.lblArcDot.Name = "lblArcDot"
        '
        'lblArcWeight
        '
        resources.ApplyResources(Me.lblArcWeight, "lblArcWeight")
        Me.lblArcWeight.Name = "lblArcWeight"
        '
        'txtArcMidPosZ
        '
        resources.ApplyResources(Me.txtArcMidPosZ, "txtArcMidPosZ")
        Me.txtArcMidPosZ.Name = "txtArcMidPosZ"
        '
        'txtArcMidPosY
        '
        resources.ApplyResources(Me.txtArcMidPosY, "txtArcMidPosY")
        Me.txtArcMidPosY.Name = "txtArcMidPosY"
        '
        'txtArcMidPosX
        '
        resources.ApplyResources(Me.txtArcMidPosX, "txtArcMidPosX")
        Me.txtArcMidPosX.Name = "txtArcMidPosX"
        '
        'btnArcMidMove
        '
        resources.ApplyResources(Me.btnArcMidMove, "btnArcMidMove")
        Me.btnArcMidMove.FlatAppearance.BorderSize = 0
        Me.btnArcMidMove.Name = "btnArcMidMove"
        Me.ToolTip1.SetToolTip(Me.btnArcMidMove, resources.GetString("btnArcMidMove.ToolTip"))
        Me.btnArcMidMove.UseVisualStyleBackColor = True
        '
        'btnArcEndMove
        '
        resources.ApplyResources(Me.btnArcEndMove, "btnArcEndMove")
        Me.btnArcEndMove.FlatAppearance.BorderSize = 0
        Me.btnArcEndMove.Name = "btnArcEndMove"
        Me.ToolTip1.SetToolTip(Me.btnArcEndMove, resources.GetString("btnArcEndMove.ToolTip"))
        Me.btnArcEndMove.UseVisualStyleBackColor = True
        '
        'btnArcStartMove
        '
        resources.ApplyResources(Me.btnArcStartMove, "btnArcStartMove")
        Me.btnArcStartMove.FlatAppearance.BorderSize = 0
        Me.btnArcStartMove.Name = "btnArcStartMove"
        Me.ToolTip1.SetToolTip(Me.btnArcStartMove, resources.GetString("btnArcStartMove.ToolTip"))
        Me.btnArcStartMove.UseVisualStyleBackColor = True
        '
        'btnArcMidSet
        '
        Me.btnArcMidSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnArcMidSet, "btnArcMidSet")
        Me.btnArcMidSet.FlatAppearance.BorderSize = 0
        Me.btnArcMidSet.Name = "btnArcMidSet"
        Me.ToolTip1.SetToolTip(Me.btnArcMidSet, resources.GetString("btnArcMidSet.ToolTip"))
        Me.btnArcMidSet.UseVisualStyleBackColor = True
        '
        'btnArcEndSet
        '
        Me.btnArcEndSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnArcEndSet, "btnArcEndSet")
        Me.btnArcEndSet.FlatAppearance.BorderSize = 0
        Me.btnArcEndSet.Name = "btnArcEndSet"
        Me.ToolTip1.SetToolTip(Me.btnArcEndSet, resources.GetString("btnArcEndSet.ToolTip"))
        Me.btnArcEndSet.UseVisualStyleBackColor = True
        '
        'btnArcStartSet
        '
        Me.btnArcStartSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnArcStartSet, "btnArcStartSet")
        Me.btnArcStartSet.FlatAppearance.BorderSize = 0
        Me.btnArcStartSet.Name = "btnArcStartSet"
        Me.ToolTip1.SetToolTip(Me.btnArcStartSet, resources.GetString("btnArcStartSet.ToolTip"))
        Me.btnArcStartSet.UseVisualStyleBackColor = True
        '
        'lblArcAngleUnit
        '
        resources.ApplyResources(Me.lblArcAngleUnit, "lblArcAngleUnit")
        Me.lblArcAngleUnit.Name = "lblArcAngleUnit"
        '
        'txtArcAngle
        '
        resources.ApplyResources(Me.txtArcAngle, "txtArcAngle")
        Me.txtArcAngle.Name = "txtArcAngle"
        '
        'lblArcAngle
        '
        resources.ApplyResources(Me.lblArcAngle, "lblArcAngle")
        Me.lblArcAngle.Name = "lblArcAngle"
        '
        'lblArcMidPosZUnit
        '
        resources.ApplyResources(Me.lblArcMidPosZUnit, "lblArcMidPosZUnit")
        Me.lblArcMidPosZUnit.Name = "lblArcMidPosZUnit"
        '
        'txtArcCenterPosZ
        '
        resources.ApplyResources(Me.txtArcCenterPosZ, "txtArcCenterPosZ")
        Me.txtArcCenterPosZ.Name = "txtArcCenterPosZ"
        '
        'lblArcMidPosYUnit
        '
        resources.ApplyResources(Me.lblArcMidPosYUnit, "lblArcMidPosYUnit")
        Me.lblArcMidPosYUnit.Name = "lblArcMidPosYUnit"
        '
        'lblArcMidPosXUnit
        '
        resources.ApplyResources(Me.lblArcMidPosXUnit, "lblArcMidPosXUnit")
        Me.lblArcMidPosXUnit.Name = "lblArcMidPosXUnit"
        '
        'txtArcCenterPosY
        '
        resources.ApplyResources(Me.txtArcCenterPosY, "txtArcCenterPosY")
        Me.txtArcCenterPosY.Name = "txtArcCenterPosY"
        '
        'txtArcCenterPosX
        '
        resources.ApplyResources(Me.txtArcCenterPosX, "txtArcCenterPosX")
        Me.txtArcCenterPosX.Name = "txtArcCenterPosX"
        '
        'lblArcStartPosZUnit
        '
        resources.ApplyResources(Me.lblArcStartPosZUnit, "lblArcStartPosZUnit")
        Me.lblArcStartPosZUnit.Name = "lblArcStartPosZUnit"
        '
        'txtArcStartPosZ
        '
        resources.ApplyResources(Me.txtArcStartPosZ, "txtArcStartPosZ")
        Me.txtArcStartPosZ.Name = "txtArcStartPosZ"
        '
        'lblArcStartPosZ
        '
        resources.ApplyResources(Me.lblArcStartPosZ, "lblArcStartPosZ")
        Me.lblArcStartPosZ.Name = "lblArcStartPosZ"
        '
        'lblArcEndPosZUnit
        '
        resources.ApplyResources(Me.lblArcEndPosZUnit, "lblArcEndPosZUnit")
        Me.lblArcEndPosZUnit.Name = "lblArcEndPosZUnit"
        '
        'lblArcEndPosYUnit
        '
        resources.ApplyResources(Me.lblArcEndPosYUnit, "lblArcEndPosYUnit")
        Me.lblArcEndPosYUnit.Name = "lblArcEndPosYUnit"
        '
        'lblArcEndPosXUnit
        '
        resources.ApplyResources(Me.lblArcEndPosXUnit, "lblArcEndPosXUnit")
        Me.lblArcEndPosXUnit.Name = "lblArcEndPosXUnit"
        '
        'lblArcStartPosYUnit
        '
        resources.ApplyResources(Me.lblArcStartPosYUnit, "lblArcStartPosYUnit")
        Me.lblArcStartPosYUnit.Name = "lblArcStartPosYUnit"
        '
        'lblArcStartPosXUnit
        '
        resources.ApplyResources(Me.lblArcStartPosXUnit, "lblArcStartPosXUnit")
        Me.lblArcStartPosXUnit.Name = "lblArcStartPosXUnit"
        '
        'txtArcEndPosZ
        '
        resources.ApplyResources(Me.txtArcEndPosZ, "txtArcEndPosZ")
        Me.txtArcEndPosZ.Name = "txtArcEndPosZ"
        '
        'txtArcEndPosY
        '
        resources.ApplyResources(Me.txtArcEndPosY, "txtArcEndPosY")
        Me.txtArcEndPosY.Name = "txtArcEndPosY"
        '
        'txtArcEndPosX
        '
        resources.ApplyResources(Me.txtArcEndPosX, "txtArcEndPosX")
        Me.txtArcEndPosX.Name = "txtArcEndPosX"
        '
        'txtArcStartPosY
        '
        resources.ApplyResources(Me.txtArcStartPosY, "txtArcStartPosY")
        Me.txtArcStartPosY.Name = "txtArcStartPosY"
        '
        'txtArcStartPosX
        '
        resources.ApplyResources(Me.txtArcStartPosX, "txtArcStartPosX")
        Me.txtArcStartPosX.Name = "txtArcStartPosX"
        '
        'lblArcStartPosY
        '
        resources.ApplyResources(Me.lblArcStartPosY, "lblArcStartPosY")
        Me.lblArcStartPosY.Name = "lblArcStartPosY"
        '
        'lblArcStartPosX
        '
        resources.ApplyResources(Me.lblArcStartPosX, "lblArcStartPosX")
        Me.lblArcStartPosX.Name = "lblArcStartPosX"
        '
        'lblArcMidPosZ
        '
        resources.ApplyResources(Me.lblArcMidPosZ, "lblArcMidPosZ")
        Me.lblArcMidPosZ.Name = "lblArcMidPosZ"
        '
        'lblArcMidPosY
        '
        resources.ApplyResources(Me.lblArcMidPosY, "lblArcMidPosY")
        Me.lblArcMidPosY.Name = "lblArcMidPosY"
        '
        'lblArcMidPosX
        '
        resources.ApplyResources(Me.lblArcMidPosX, "lblArcMidPosX")
        Me.lblArcMidPosX.Name = "lblArcMidPosX"
        '
        'lblArcEndPosZ
        '
        resources.ApplyResources(Me.lblArcEndPosZ, "lblArcEndPosZ")
        Me.lblArcEndPosZ.Name = "lblArcEndPosZ"
        '
        'lblArcEndPosY
        '
        resources.ApplyResources(Me.lblArcEndPosY, "lblArcEndPosY")
        Me.lblArcEndPosY.Name = "lblArcEndPosY"
        '
        'lblArcEndPosX
        '
        resources.ApplyResources(Me.lblArcEndPosX, "lblArcEndPosX")
        Me.lblArcEndPosX.Name = "lblArcEndPosX"
        '
        'btnArcDone
        '
        resources.ApplyResources(Me.btnArcDone, "btnArcDone")
        Me.btnArcDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnArcDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnArcDone.FlatAppearance.BorderSize = 0
        Me.btnArcDone.Name = "btnArcDone"
        Me.ToolTip1.SetToolTip(Me.btnArcDone, resources.GetString("btnArcDone.ToolTip"))
        Me.btnArcDone.UseVisualStyleBackColor = True
        '
        'tabCircle
        '
        Me.tabCircle.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabCircle.Controls.Add(Me.grpCircle)
        resources.ApplyResources(Me.tabCircle, "tabCircle")
        Me.tabCircle.Name = "tabCircle"
        '
        'grpCircle
        '
        Me.grpCircle.Controls.Add(Me.nmuCircleStartVelocity)
        Me.grpCircle.Controls.Add(Me.lblCircleStartVelocityUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleStartVelocity)
        Me.grpCircle.Controls.Add(Me.nmuCircleVelocity)
        Me.grpCircle.Controls.Add(Me.nmuCircleDot)
        Me.grpCircle.Controls.Add(Me.nmuCircleWeight)
        Me.grpCircle.Controls.Add(Me.lblCircleVelocityUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleVelocity)
        Me.grpCircle.Controls.Add(Me.lblCircleDotWeight)
        Me.grpCircle.Controls.Add(Me.txtCircleDotWeight)
        Me.grpCircle.Controls.Add(Me.lblCircleDotWeightUnit)
        Me.grpCircle.Controls.Add(Me.tlpCircleValueUc)
        Me.grpCircle.Controls.Add(Me.btnCircleRefresh)
        Me.grpCircle.Controls.Add(Me.txtCircleComment)
        Me.grpCircle.Controls.Add(Me.Label2)
        Me.grpCircle.Controls.Add(Me.btnCircleCancel)
        Me.grpCircle.Controls.Add(Me.txtCirclePitch)
        Me.grpCircle.Controls.Add(Me.lblCirclePitch)
        Me.grpCircle.Controls.Add(Me.lblCirclePitchUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleWeightUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleDot)
        Me.grpCircle.Controls.Add(Me.lblCircleWeight)
        Me.grpCircle.Controls.Add(Me.txtCircleMidPosZ)
        Me.grpCircle.Controls.Add(Me.txtCircleMidPosY)
        Me.grpCircle.Controls.Add(Me.txtCircleMidPosX)
        Me.grpCircle.Controls.Add(Me.btnCircleStartMove)
        Me.grpCircle.Controls.Add(Me.btnCircleStartSet)
        Me.grpCircle.Controls.Add(Me.txtCircleStartPosZ)
        Me.grpCircle.Controls.Add(Me.txtCircleStartPosY)
        Me.grpCircle.Controls.Add(Me.txtCircleStartPosX)
        Me.grpCircle.Controls.Add(Me.lblCircleStartPosZUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleStartPosYUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleStartPosXUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleStartPosZ)
        Me.grpCircle.Controls.Add(Me.lblCircleStartPosY)
        Me.grpCircle.Controls.Add(Me.lblCircleStartPosX)
        Me.grpCircle.Controls.Add(Me.btnCircleMidMove)
        Me.grpCircle.Controls.Add(Me.btnCircleEndMove)
        Me.grpCircle.Controls.Add(Me.btnCircleMidSet)
        Me.grpCircle.Controls.Add(Me.btnCircleEndSet)
        Me.grpCircle.Controls.Add(Me.lblCircleMidPosZUnit)
        Me.grpCircle.Controls.Add(Me.txtCircleCenterPosZ)
        Me.grpCircle.Controls.Add(Me.lblCircleMidPosYUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleMidPosXUnit)
        Me.grpCircle.Controls.Add(Me.txtCircleCenterPosY)
        Me.grpCircle.Controls.Add(Me.txtCircleCenterPosX)
        Me.grpCircle.Controls.Add(Me.lblCircleEndPosZUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleEndPosYUnit)
        Me.grpCircle.Controls.Add(Me.lblCircleEndPosXUnit)
        Me.grpCircle.Controls.Add(Me.txtCircleMid2PosZ)
        Me.grpCircle.Controls.Add(Me.txtCircleMid2PosY)
        Me.grpCircle.Controls.Add(Me.txtCircleMid2PosX)
        Me.grpCircle.Controls.Add(Me.lblCircleMidPosZ)
        Me.grpCircle.Controls.Add(Me.lblCircleMidPosY)
        Me.grpCircle.Controls.Add(Me.lblCircleMidPosX)
        Me.grpCircle.Controls.Add(Me.lblCircleEndPosZ)
        Me.grpCircle.Controls.Add(Me.lblCircleEndPosY)
        Me.grpCircle.Controls.Add(Me.lblCircleEndPosX)
        Me.grpCircle.Controls.Add(Me.btnCircleDone)
        resources.ApplyResources(Me.grpCircle, "grpCircle")
        Me.grpCircle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCircle.Name = "grpCircle"
        Me.grpCircle.TabStop = False
        '
        'nmuCircleStartVelocity
        '
        resources.ApplyResources(Me.nmuCircleStartVelocity, "nmuCircleStartVelocity")
        Me.nmuCircleStartVelocity.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuCircleStartVelocity.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuCircleStartVelocity.Name = "nmuCircleStartVelocity"
        Me.nmuCircleStartVelocity.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblCircleStartVelocityUnit
        '
        resources.ApplyResources(Me.lblCircleStartVelocityUnit, "lblCircleStartVelocityUnit")
        Me.lblCircleStartVelocityUnit.Name = "lblCircleStartVelocityUnit"
        '
        'lblCircleStartVelocity
        '
        resources.ApplyResources(Me.lblCircleStartVelocity, "lblCircleStartVelocity")
        Me.lblCircleStartVelocity.Name = "lblCircleStartVelocity"
        '
        'nmuCircleVelocity
        '
        resources.ApplyResources(Me.nmuCircleVelocity, "nmuCircleVelocity")
        Me.nmuCircleVelocity.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuCircleVelocity.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuCircleVelocity.Name = "nmuCircleVelocity"
        Me.nmuCircleVelocity.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nmuCircleDot
        '
        resources.ApplyResources(Me.nmuCircleDot, "nmuCircleDot")
        Me.nmuCircleDot.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuCircleDot.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuCircleDot.Name = "nmuCircleDot"
        Me.nmuCircleDot.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nmuCircleWeight
        '
        Me.nmuCircleWeight.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuCircleWeight, "nmuCircleWeight")
        Me.nmuCircleWeight.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuCircleWeight.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuCircleWeight.Name = "nmuCircleWeight"
        Me.nmuCircleWeight.Value = New Decimal(New Integer() {1, 0, 0, 196608})
        '
        'lblCircleVelocityUnit
        '
        resources.ApplyResources(Me.lblCircleVelocityUnit, "lblCircleVelocityUnit")
        Me.lblCircleVelocityUnit.Name = "lblCircleVelocityUnit"
        '
        'lblCircleVelocity
        '
        resources.ApplyResources(Me.lblCircleVelocity, "lblCircleVelocity")
        Me.lblCircleVelocity.Name = "lblCircleVelocity"
        '
        'lblCircleDotWeight
        '
        resources.ApplyResources(Me.lblCircleDotWeight, "lblCircleDotWeight")
        Me.lblCircleDotWeight.Name = "lblCircleDotWeight"
        '
        'txtCircleDotWeight
        '
        resources.ApplyResources(Me.txtCircleDotWeight, "txtCircleDotWeight")
        Me.txtCircleDotWeight.Name = "txtCircleDotWeight"
        Me.txtCircleDotWeight.ReadOnly = True
        '
        'lblCircleDotWeightUnit
        '
        resources.ApplyResources(Me.lblCircleDotWeightUnit, "lblCircleDotWeightUnit")
        Me.lblCircleDotWeightUnit.Name = "lblCircleDotWeightUnit"
        '
        'tlpCircleValueUc
        '
        resources.ApplyResources(Me.tlpCircleValueUc, "tlpCircleValueUc")
        Me.tlpCircleValueUc.Controls.Add(Me.btCircleTypeSelect, 1, 0)
        Me.tlpCircleValueUc.Controls.Add(Me.cbCircleTypeSelect, 0, 1)
        Me.tlpCircleValueUc.Controls.Add(Me.lbCircleTypeSelect, 0, 0)
        Me.tlpCircleValueUc.Name = "tlpCircleValueUc"
        '
        'btCircleTypeSelect
        '
        Me.btCircleTypeSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btCircleTypeSelect, "btCircleTypeSelect")
        Me.btCircleTypeSelect.FlatAppearance.BorderSize = 0
        Me.btCircleTypeSelect.Name = "btCircleTypeSelect"
        Me.tlpCircleValueUc.SetRowSpan(Me.btCircleTypeSelect, 2)
        Me.ToolTip1.SetToolTip(Me.btCircleTypeSelect, resources.GetString("btCircleTypeSelect.ToolTip"))
        Me.btCircleTypeSelect.UseVisualStyleBackColor = True
        '
        'cbCircleTypeSelect
        '
        Me.cbCircleTypeSelect.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.cbCircleTypeSelect, "cbCircleTypeSelect")
        Me.cbCircleTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCircleTypeSelect.FormattingEnabled = True
        Me.cbCircleTypeSelect.Name = "cbCircleTypeSelect"
        '
        'lbCircleTypeSelect
        '
        resources.ApplyResources(Me.lbCircleTypeSelect, "lbCircleTypeSelect")
        Me.lbCircleTypeSelect.Name = "lbCircleTypeSelect"
        '
        'btnCircleRefresh
        '
        resources.ApplyResources(Me.btnCircleRefresh, "btnCircleRefresh")
        Me.btnCircleRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.btnCircleRefresh.FlatAppearance.BorderSize = 0
        Me.btnCircleRefresh.Name = "btnCircleRefresh"
        Me.btnCircleRefresh.UseVisualStyleBackColor = True
        '
        'txtCircleComment
        '
        resources.ApplyResources(Me.txtCircleComment, "txtCircleComment")
        Me.txtCircleComment.Name = "txtCircleComment"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'btnCircleCancel
        '
        resources.ApplyResources(Me.btnCircleCancel, "btnCircleCancel")
        Me.btnCircleCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCircleCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCircleCancel.FlatAppearance.BorderSize = 0
        Me.btnCircleCancel.Name = "btnCircleCancel"
        Me.ToolTip1.SetToolTip(Me.btnCircleCancel, resources.GetString("btnCircleCancel.ToolTip"))
        Me.btnCircleCancel.UseVisualStyleBackColor = True
        '
        'txtCirclePitch
        '
        resources.ApplyResources(Me.txtCirclePitch, "txtCirclePitch")
        Me.txtCirclePitch.Name = "txtCirclePitch"
        Me.txtCirclePitch.ReadOnly = True
        '
        'lblCirclePitch
        '
        Me.lblCirclePitch.AutoEllipsis = True
        resources.ApplyResources(Me.lblCirclePitch, "lblCirclePitch")
        Me.lblCirclePitch.Name = "lblCirclePitch"
        '
        'lblCirclePitchUnit
        '
        resources.ApplyResources(Me.lblCirclePitchUnit, "lblCirclePitchUnit")
        Me.lblCirclePitchUnit.Name = "lblCirclePitchUnit"
        '
        'lblCircleWeightUnit
        '
        resources.ApplyResources(Me.lblCircleWeightUnit, "lblCircleWeightUnit")
        Me.lblCircleWeightUnit.Name = "lblCircleWeightUnit"
        '
        'lblCircleDot
        '
        resources.ApplyResources(Me.lblCircleDot, "lblCircleDot")
        Me.lblCircleDot.Name = "lblCircleDot"
        '
        'lblCircleWeight
        '
        resources.ApplyResources(Me.lblCircleWeight, "lblCircleWeight")
        Me.lblCircleWeight.Name = "lblCircleWeight"
        '
        'txtCircleMidPosZ
        '
        resources.ApplyResources(Me.txtCircleMidPosZ, "txtCircleMidPosZ")
        Me.txtCircleMidPosZ.Name = "txtCircleMidPosZ"
        '
        'txtCircleMidPosY
        '
        resources.ApplyResources(Me.txtCircleMidPosY, "txtCircleMidPosY")
        Me.txtCircleMidPosY.Name = "txtCircleMidPosY"
        '
        'txtCircleMidPosX
        '
        resources.ApplyResources(Me.txtCircleMidPosX, "txtCircleMidPosX")
        Me.txtCircleMidPosX.Name = "txtCircleMidPosX"
        '
        'btnCircleStartMove
        '
        resources.ApplyResources(Me.btnCircleStartMove, "btnCircleStartMove")
        Me.btnCircleStartMove.FlatAppearance.BorderSize = 0
        Me.btnCircleStartMove.Name = "btnCircleStartMove"
        Me.ToolTip1.SetToolTip(Me.btnCircleStartMove, resources.GetString("btnCircleStartMove.ToolTip"))
        Me.btnCircleStartMove.UseVisualStyleBackColor = True
        '
        'btnCircleStartSet
        '
        Me.btnCircleStartSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCircleStartSet, "btnCircleStartSet")
        Me.btnCircleStartSet.FlatAppearance.BorderSize = 0
        Me.btnCircleStartSet.Name = "btnCircleStartSet"
        Me.ToolTip1.SetToolTip(Me.btnCircleStartSet, resources.GetString("btnCircleStartSet.ToolTip"))
        Me.btnCircleStartSet.UseVisualStyleBackColor = True
        '
        'txtCircleStartPosZ
        '
        resources.ApplyResources(Me.txtCircleStartPosZ, "txtCircleStartPosZ")
        Me.txtCircleStartPosZ.Name = "txtCircleStartPosZ"
        '
        'txtCircleStartPosY
        '
        resources.ApplyResources(Me.txtCircleStartPosY, "txtCircleStartPosY")
        Me.txtCircleStartPosY.Name = "txtCircleStartPosY"
        '
        'txtCircleStartPosX
        '
        resources.ApplyResources(Me.txtCircleStartPosX, "txtCircleStartPosX")
        Me.txtCircleStartPosX.Name = "txtCircleStartPosX"
        '
        'lblCircleStartPosZUnit
        '
        resources.ApplyResources(Me.lblCircleStartPosZUnit, "lblCircleStartPosZUnit")
        Me.lblCircleStartPosZUnit.Name = "lblCircleStartPosZUnit"
        '
        'lblCircleStartPosYUnit
        '
        resources.ApplyResources(Me.lblCircleStartPosYUnit, "lblCircleStartPosYUnit")
        Me.lblCircleStartPosYUnit.Name = "lblCircleStartPosYUnit"
        '
        'lblCircleStartPosXUnit
        '
        resources.ApplyResources(Me.lblCircleStartPosXUnit, "lblCircleStartPosXUnit")
        Me.lblCircleStartPosXUnit.Name = "lblCircleStartPosXUnit"
        '
        'lblCircleStartPosZ
        '
        resources.ApplyResources(Me.lblCircleStartPosZ, "lblCircleStartPosZ")
        Me.lblCircleStartPosZ.Name = "lblCircleStartPosZ"
        '
        'lblCircleStartPosY
        '
        resources.ApplyResources(Me.lblCircleStartPosY, "lblCircleStartPosY")
        Me.lblCircleStartPosY.Name = "lblCircleStartPosY"
        '
        'lblCircleStartPosX
        '
        resources.ApplyResources(Me.lblCircleStartPosX, "lblCircleStartPosX")
        Me.lblCircleStartPosX.Name = "lblCircleStartPosX"
        '
        'btnCircleMidMove
        '
        resources.ApplyResources(Me.btnCircleMidMove, "btnCircleMidMove")
        Me.btnCircleMidMove.FlatAppearance.BorderSize = 0
        Me.btnCircleMidMove.Name = "btnCircleMidMove"
        Me.ToolTip1.SetToolTip(Me.btnCircleMidMove, resources.GetString("btnCircleMidMove.ToolTip"))
        Me.btnCircleMidMove.UseVisualStyleBackColor = True
        '
        'btnCircleEndMove
        '
        resources.ApplyResources(Me.btnCircleEndMove, "btnCircleEndMove")
        Me.btnCircleEndMove.FlatAppearance.BorderSize = 0
        Me.btnCircleEndMove.Name = "btnCircleEndMove"
        Me.ToolTip1.SetToolTip(Me.btnCircleEndMove, resources.GetString("btnCircleEndMove.ToolTip"))
        Me.btnCircleEndMove.UseVisualStyleBackColor = True
        '
        'btnCircleMidSet
        '
        Me.btnCircleMidSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCircleMidSet, "btnCircleMidSet")
        Me.btnCircleMidSet.FlatAppearance.BorderSize = 0
        Me.btnCircleMidSet.Name = "btnCircleMidSet"
        Me.ToolTip1.SetToolTip(Me.btnCircleMidSet, resources.GetString("btnCircleMidSet.ToolTip"))
        Me.btnCircleMidSet.UseVisualStyleBackColor = True
        '
        'btnCircleEndSet
        '
        Me.btnCircleEndSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCircleEndSet, "btnCircleEndSet")
        Me.btnCircleEndSet.FlatAppearance.BorderSize = 0
        Me.btnCircleEndSet.Name = "btnCircleEndSet"
        Me.ToolTip1.SetToolTip(Me.btnCircleEndSet, resources.GetString("btnCircleEndSet.ToolTip"))
        Me.btnCircleEndSet.UseVisualStyleBackColor = True
        '
        'lblCircleMidPosZUnit
        '
        resources.ApplyResources(Me.lblCircleMidPosZUnit, "lblCircleMidPosZUnit")
        Me.lblCircleMidPosZUnit.Name = "lblCircleMidPosZUnit"
        '
        'txtCircleCenterPosZ
        '
        resources.ApplyResources(Me.txtCircleCenterPosZ, "txtCircleCenterPosZ")
        Me.txtCircleCenterPosZ.Name = "txtCircleCenterPosZ"
        '
        'lblCircleMidPosYUnit
        '
        resources.ApplyResources(Me.lblCircleMidPosYUnit, "lblCircleMidPosYUnit")
        Me.lblCircleMidPosYUnit.Name = "lblCircleMidPosYUnit"
        '
        'lblCircleMidPosXUnit
        '
        resources.ApplyResources(Me.lblCircleMidPosXUnit, "lblCircleMidPosXUnit")
        Me.lblCircleMidPosXUnit.Name = "lblCircleMidPosXUnit"
        '
        'txtCircleCenterPosY
        '
        resources.ApplyResources(Me.txtCircleCenterPosY, "txtCircleCenterPosY")
        Me.txtCircleCenterPosY.Name = "txtCircleCenterPosY"
        '
        'txtCircleCenterPosX
        '
        resources.ApplyResources(Me.txtCircleCenterPosX, "txtCircleCenterPosX")
        Me.txtCircleCenterPosX.Name = "txtCircleCenterPosX"
        '
        'lblCircleEndPosZUnit
        '
        resources.ApplyResources(Me.lblCircleEndPosZUnit, "lblCircleEndPosZUnit")
        Me.lblCircleEndPosZUnit.Name = "lblCircleEndPosZUnit"
        '
        'lblCircleEndPosYUnit
        '
        resources.ApplyResources(Me.lblCircleEndPosYUnit, "lblCircleEndPosYUnit")
        Me.lblCircleEndPosYUnit.Name = "lblCircleEndPosYUnit"
        '
        'lblCircleEndPosXUnit
        '
        resources.ApplyResources(Me.lblCircleEndPosXUnit, "lblCircleEndPosXUnit")
        Me.lblCircleEndPosXUnit.Name = "lblCircleEndPosXUnit"
        '
        'txtCircleMid2PosZ
        '
        resources.ApplyResources(Me.txtCircleMid2PosZ, "txtCircleMid2PosZ")
        Me.txtCircleMid2PosZ.Name = "txtCircleMid2PosZ"
        '
        'txtCircleMid2PosY
        '
        resources.ApplyResources(Me.txtCircleMid2PosY, "txtCircleMid2PosY")
        Me.txtCircleMid2PosY.Name = "txtCircleMid2PosY"
        '
        'txtCircleMid2PosX
        '
        resources.ApplyResources(Me.txtCircleMid2PosX, "txtCircleMid2PosX")
        Me.txtCircleMid2PosX.Name = "txtCircleMid2PosX"
        '
        'lblCircleMidPosZ
        '
        resources.ApplyResources(Me.lblCircleMidPosZ, "lblCircleMidPosZ")
        Me.lblCircleMidPosZ.Name = "lblCircleMidPosZ"
        '
        'lblCircleMidPosY
        '
        resources.ApplyResources(Me.lblCircleMidPosY, "lblCircleMidPosY")
        Me.lblCircleMidPosY.Name = "lblCircleMidPosY"
        '
        'lblCircleMidPosX
        '
        resources.ApplyResources(Me.lblCircleMidPosX, "lblCircleMidPosX")
        Me.lblCircleMidPosX.Name = "lblCircleMidPosX"
        '
        'lblCircleEndPosZ
        '
        resources.ApplyResources(Me.lblCircleEndPosZ, "lblCircleEndPosZ")
        Me.lblCircleEndPosZ.Name = "lblCircleEndPosZ"
        '
        'lblCircleEndPosY
        '
        resources.ApplyResources(Me.lblCircleEndPosY, "lblCircleEndPosY")
        Me.lblCircleEndPosY.Name = "lblCircleEndPosY"
        '
        'lblCircleEndPosX
        '
        resources.ApplyResources(Me.lblCircleEndPosX, "lblCircleEndPosX")
        Me.lblCircleEndPosX.Name = "lblCircleEndPosX"
        '
        'btnCircleDone
        '
        resources.ApplyResources(Me.btnCircleDone, "btnCircleDone")
        Me.btnCircleDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnCircleDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnCircleDone.FlatAppearance.BorderSize = 0
        Me.btnCircleDone.Name = "btnCircleDone"
        Me.ToolTip1.SetToolTip(Me.btnCircleDone, resources.GetString("btnCircleDone.ToolTip"))
        Me.btnCircleDone.UseVisualStyleBackColor = True
        '
        'tabWait
        '
        Me.tabWait.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabWait.Controls.Add(Me.grpWait)
        resources.ApplyResources(Me.tabWait, "tabWait")
        Me.tabWait.Name = "tabWait"
        '
        'grpWait
        '
        Me.grpWait.Controls.Add(Me.btnWaitCancel)
        Me.grpWait.Controls.Add(Me.btnWaitDone)
        Me.grpWait.Controls.Add(Me.lblWaitDwellTimeUnit)
        Me.grpWait.Controls.Add(Me.txtWaitDwellTime)
        Me.grpWait.Controls.Add(Me.lblWaitDwellTime)
        resources.ApplyResources(Me.grpWait, "grpWait")
        Me.grpWait.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpWait.Name = "grpWait"
        Me.grpWait.TabStop = False
        '
        'btnWaitCancel
        '
        resources.ApplyResources(Me.btnWaitCancel, "btnWaitCancel")
        Me.btnWaitCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnWaitCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnWaitCancel.FlatAppearance.BorderSize = 0
        Me.btnWaitCancel.Name = "btnWaitCancel"
        Me.ToolTip1.SetToolTip(Me.btnWaitCancel, resources.GetString("btnWaitCancel.ToolTip"))
        Me.btnWaitCancel.UseVisualStyleBackColor = True
        '
        'btnWaitDone
        '
        resources.ApplyResources(Me.btnWaitDone, "btnWaitDone")
        Me.btnWaitDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnWaitDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnWaitDone.FlatAppearance.BorderSize = 0
        Me.btnWaitDone.Name = "btnWaitDone"
        Me.ToolTip1.SetToolTip(Me.btnWaitDone, resources.GetString("btnWaitDone.ToolTip"))
        Me.btnWaitDone.UseVisualStyleBackColor = True
        '
        'lblWaitDwellTimeUnit
        '
        resources.ApplyResources(Me.lblWaitDwellTimeUnit, "lblWaitDwellTimeUnit")
        Me.lblWaitDwellTimeUnit.Name = "lblWaitDwellTimeUnit"
        '
        'txtWaitDwellTime
        '
        resources.ApplyResources(Me.txtWaitDwellTime, "txtWaitDwellTime")
        Me.txtWaitDwellTime.Name = "txtWaitDwellTime"
        '
        'lblWaitDwellTime
        '
        resources.ApplyResources(Me.lblWaitDwellTime, "lblWaitDwellTime")
        Me.lblWaitDwellTime.Name = "lblWaitDwellTime"
        '
        'tabCircle3D
        '
        Me.tabCircle3D.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabCircle3D.Controls.Add(Me.grpCircle3D)
        resources.ApplyResources(Me.tabCircle3D, "tabCircle3D")
        Me.tabCircle3D.Name = "tabCircle3D"
        '
        'grpCircle3D
        '
        Me.grpCircle3D.Controls.Add(Me.tlpCircle3DValueUc)
        Me.grpCircle3D.Controls.Add(Me.btnCircle3DCancel)
        Me.grpCircle3D.Controls.Add(Me.btnCircle3DDone)
        Me.grpCircle3D.Controls.Add(Me.btnCircle3DCenterMove)
        Me.grpCircle3D.Controls.Add(Me.btnCircle3DEndMove)
        Me.grpCircle3D.Controls.Add(Me.btnCircleCenterSet)
        Me.grpCircle3D.Controls.Add(Me.btnCircle3DEndSet)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DCenterPosZUnit)
        Me.grpCircle3D.Controls.Add(Me.txtCircle3DCenterPosZ)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DCenterPosYUnit)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DCenterPosXUnit)
        Me.grpCircle3D.Controls.Add(Me.txtCircle3DCenterPosY)
        Me.grpCircle3D.Controls.Add(Me.txtCircle3DCenterPosX)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DEndPosZUnit)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DEndPosYUnit)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DEndPosXUnit)
        Me.grpCircle3D.Controls.Add(Me.txtCircle3DEndPosZ)
        Me.grpCircle3D.Controls.Add(Me.txtCircle3DEndPosY)
        Me.grpCircle3D.Controls.Add(Me.txtCircle3DEndPosX)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DCenterPosZ)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DCenterPosY)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DCenterPosX)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DEndPosZ)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DEndPosY)
        Me.grpCircle3D.Controls.Add(Me.lblCircle3DEndPosX)
        resources.ApplyResources(Me.grpCircle3D, "grpCircle3D")
        Me.grpCircle3D.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCircle3D.Name = "grpCircle3D"
        Me.grpCircle3D.TabStop = False
        '
        'tlpCircle3DValueUc
        '
        resources.ApplyResources(Me.tlpCircle3DValueUc, "tlpCircle3DValueUc")
        Me.tlpCircle3DValueUc.Controls.Add(Me.btCircle3DTypeSelect, 1, 0)
        Me.tlpCircle3DValueUc.Controls.Add(Me.cbCircle3DTypeSelect, 0, 1)
        Me.tlpCircle3DValueUc.Controls.Add(Me.lbCircle3DTypeSelect, 0, 0)
        Me.tlpCircle3DValueUc.Name = "tlpCircle3DValueUc"
        '
        'btCircle3DTypeSelect
        '
        Me.btCircle3DTypeSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btCircle3DTypeSelect, "btCircle3DTypeSelect")
        Me.btCircle3DTypeSelect.FlatAppearance.BorderSize = 0
        Me.btCircle3DTypeSelect.Name = "btCircle3DTypeSelect"
        Me.tlpCircle3DValueUc.SetRowSpan(Me.btCircle3DTypeSelect, 2)
        Me.ToolTip1.SetToolTip(Me.btCircle3DTypeSelect, resources.GetString("btCircle3DTypeSelect.ToolTip"))
        Me.btCircle3DTypeSelect.UseVisualStyleBackColor = True
        '
        'cbCircle3DTypeSelect
        '
        Me.cbCircle3DTypeSelect.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.cbCircle3DTypeSelect, "cbCircle3DTypeSelect")
        Me.cbCircle3DTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCircle3DTypeSelect.FormattingEnabled = True
        Me.cbCircle3DTypeSelect.Name = "cbCircle3DTypeSelect"
        '
        'lbCircle3DTypeSelect
        '
        resources.ApplyResources(Me.lbCircle3DTypeSelect, "lbCircle3DTypeSelect")
        Me.lbCircle3DTypeSelect.Name = "lbCircle3DTypeSelect"
        '
        'btnCircle3DCancel
        '
        resources.ApplyResources(Me.btnCircle3DCancel, "btnCircle3DCancel")
        Me.btnCircle3DCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCircle3DCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCircle3DCancel.FlatAppearance.BorderSize = 0
        Me.btnCircle3DCancel.Name = "btnCircle3DCancel"
        Me.ToolTip1.SetToolTip(Me.btnCircle3DCancel, resources.GetString("btnCircle3DCancel.ToolTip"))
        Me.btnCircle3DCancel.UseVisualStyleBackColor = True
        '
        'btnCircle3DDone
        '
        resources.ApplyResources(Me.btnCircle3DDone, "btnCircle3DDone")
        Me.btnCircle3DDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnCircle3DDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnCircle3DDone.FlatAppearance.BorderSize = 0
        Me.btnCircle3DDone.Name = "btnCircle3DDone"
        Me.ToolTip1.SetToolTip(Me.btnCircle3DDone, resources.GetString("btnCircle3DDone.ToolTip"))
        Me.btnCircle3DDone.UseVisualStyleBackColor = True
        '
        'btnCircle3DCenterMove
        '
        resources.ApplyResources(Me.btnCircle3DCenterMove, "btnCircle3DCenterMove")
        Me.btnCircle3DCenterMove.FlatAppearance.BorderSize = 0
        Me.btnCircle3DCenterMove.Name = "btnCircle3DCenterMove"
        Me.ToolTip1.SetToolTip(Me.btnCircle3DCenterMove, resources.GetString("btnCircle3DCenterMove.ToolTip"))
        Me.btnCircle3DCenterMove.UseVisualStyleBackColor = True
        '
        'btnCircle3DEndMove
        '
        resources.ApplyResources(Me.btnCircle3DEndMove, "btnCircle3DEndMove")
        Me.btnCircle3DEndMove.FlatAppearance.BorderSize = 0
        Me.btnCircle3DEndMove.Name = "btnCircle3DEndMove"
        Me.ToolTip1.SetToolTip(Me.btnCircle3DEndMove, resources.GetString("btnCircle3DEndMove.ToolTip"))
        Me.btnCircle3DEndMove.UseVisualStyleBackColor = True
        '
        'btnCircleCenterSet
        '
        Me.btnCircleCenterSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCircleCenterSet, "btnCircleCenterSet")
        Me.btnCircleCenterSet.FlatAppearance.BorderSize = 0
        Me.btnCircleCenterSet.Name = "btnCircleCenterSet"
        Me.ToolTip1.SetToolTip(Me.btnCircleCenterSet, resources.GetString("btnCircleCenterSet.ToolTip"))
        Me.btnCircleCenterSet.UseVisualStyleBackColor = True
        '
        'btnCircle3DEndSet
        '
        Me.btnCircle3DEndSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCircle3DEndSet, "btnCircle3DEndSet")
        Me.btnCircle3DEndSet.FlatAppearance.BorderSize = 0
        Me.btnCircle3DEndSet.Name = "btnCircle3DEndSet"
        Me.ToolTip1.SetToolTip(Me.btnCircle3DEndSet, resources.GetString("btnCircle3DEndSet.ToolTip"))
        Me.btnCircle3DEndSet.UseVisualStyleBackColor = True
        '
        'lblCircle3DCenterPosZUnit
        '
        resources.ApplyResources(Me.lblCircle3DCenterPosZUnit, "lblCircle3DCenterPosZUnit")
        Me.lblCircle3DCenterPosZUnit.Name = "lblCircle3DCenterPosZUnit"
        '
        'txtCircle3DCenterPosZ
        '
        resources.ApplyResources(Me.txtCircle3DCenterPosZ, "txtCircle3DCenterPosZ")
        Me.txtCircle3DCenterPosZ.Name = "txtCircle3DCenterPosZ"
        '
        'lblCircle3DCenterPosYUnit
        '
        resources.ApplyResources(Me.lblCircle3DCenterPosYUnit, "lblCircle3DCenterPosYUnit")
        Me.lblCircle3DCenterPosYUnit.Name = "lblCircle3DCenterPosYUnit"
        '
        'lblCircle3DCenterPosXUnit
        '
        resources.ApplyResources(Me.lblCircle3DCenterPosXUnit, "lblCircle3DCenterPosXUnit")
        Me.lblCircle3DCenterPosXUnit.Name = "lblCircle3DCenterPosXUnit"
        '
        'txtCircle3DCenterPosY
        '
        resources.ApplyResources(Me.txtCircle3DCenterPosY, "txtCircle3DCenterPosY")
        Me.txtCircle3DCenterPosY.Name = "txtCircle3DCenterPosY"
        '
        'txtCircle3DCenterPosX
        '
        resources.ApplyResources(Me.txtCircle3DCenterPosX, "txtCircle3DCenterPosX")
        Me.txtCircle3DCenterPosX.Name = "txtCircle3DCenterPosX"
        '
        'lblCircle3DEndPosZUnit
        '
        resources.ApplyResources(Me.lblCircle3DEndPosZUnit, "lblCircle3DEndPosZUnit")
        Me.lblCircle3DEndPosZUnit.Name = "lblCircle3DEndPosZUnit"
        '
        'lblCircle3DEndPosYUnit
        '
        resources.ApplyResources(Me.lblCircle3DEndPosYUnit, "lblCircle3DEndPosYUnit")
        Me.lblCircle3DEndPosYUnit.Name = "lblCircle3DEndPosYUnit"
        '
        'lblCircle3DEndPosXUnit
        '
        resources.ApplyResources(Me.lblCircle3DEndPosXUnit, "lblCircle3DEndPosXUnit")
        Me.lblCircle3DEndPosXUnit.Name = "lblCircle3DEndPosXUnit"
        '
        'txtCircle3DEndPosZ
        '
        resources.ApplyResources(Me.txtCircle3DEndPosZ, "txtCircle3DEndPosZ")
        Me.txtCircle3DEndPosZ.Name = "txtCircle3DEndPosZ"
        '
        'txtCircle3DEndPosY
        '
        resources.ApplyResources(Me.txtCircle3DEndPosY, "txtCircle3DEndPosY")
        Me.txtCircle3DEndPosY.Name = "txtCircle3DEndPosY"
        '
        'txtCircle3DEndPosX
        '
        resources.ApplyResources(Me.txtCircle3DEndPosX, "txtCircle3DEndPosX")
        Me.txtCircle3DEndPosX.Name = "txtCircle3DEndPosX"
        '
        'lblCircle3DCenterPosZ
        '
        resources.ApplyResources(Me.lblCircle3DCenterPosZ, "lblCircle3DCenterPosZ")
        Me.lblCircle3DCenterPosZ.Name = "lblCircle3DCenterPosZ"
        '
        'lblCircle3DCenterPosY
        '
        resources.ApplyResources(Me.lblCircle3DCenterPosY, "lblCircle3DCenterPosY")
        Me.lblCircle3DCenterPosY.Name = "lblCircle3DCenterPosY"
        '
        'lblCircle3DCenterPosX
        '
        resources.ApplyResources(Me.lblCircle3DCenterPosX, "lblCircle3DCenterPosX")
        Me.lblCircle3DCenterPosX.Name = "lblCircle3DCenterPosX"
        '
        'lblCircle3DEndPosZ
        '
        resources.ApplyResources(Me.lblCircle3DEndPosZ, "lblCircle3DEndPosZ")
        Me.lblCircle3DEndPosZ.Name = "lblCircle3DEndPosZ"
        '
        'lblCircle3DEndPosY
        '
        resources.ApplyResources(Me.lblCircle3DEndPosY, "lblCircle3DEndPosY")
        Me.lblCircle3DEndPosY.Name = "lblCircle3DEndPosY"
        '
        'lblCircle3DEndPosX
        '
        resources.ApplyResources(Me.lblCircle3DEndPosX, "lblCircle3DEndPosX")
        Me.lblCircle3DEndPosX.Name = "lblCircle3DEndPosX"
        '
        'tabArc3D
        '
        Me.tabArc3D.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabArc3D.Controls.Add(Me.grpArc3D)
        resources.ApplyResources(Me.tabArc3D, "tabArc3D")
        Me.tabArc3D.Name = "tabArc3D"
        '
        'grpArc3D
        '
        Me.grpArc3D.Controls.Add(Me.tlpArc3DValueUc)
        Me.grpArc3D.Controls.Add(Me.btnArc3DCancel)
        Me.grpArc3D.Controls.Add(Me.btnArc3DDone)
        Me.grpArc3D.Controls.Add(Me.btnArc3DCenterMove)
        Me.grpArc3D.Controls.Add(Me.btnArc3DEndMove)
        Me.grpArc3D.Controls.Add(Me.btnArc3DStartrMove)
        Me.grpArc3D.Controls.Add(Me.btnArc3DCenterSet)
        Me.grpArc3D.Controls.Add(Me.btnArc3DEndSet)
        Me.grpArc3D.Controls.Add(Me.btnArc3DStartSet)
        Me.grpArc3D.Controls.Add(Me.lblArc3DAngleUnit)
        Me.grpArc3D.Controls.Add(Me.txtArc3DAngle)
        Me.grpArc3D.Controls.Add(Me.lblArc3DAngle)
        Me.grpArc3D.Controls.Add(Me.lblArc3DCenterPosZUnit)
        Me.grpArc3D.Controls.Add(Me.txtArc3DCenterPosZ)
        Me.grpArc3D.Controls.Add(Me.lblArc3DCenterPosYUnit)
        Me.grpArc3D.Controls.Add(Me.lblArc3DCenterPosXUnit)
        Me.grpArc3D.Controls.Add(Me.txtArc3DCenterPosY)
        Me.grpArc3D.Controls.Add(Me.txtArc3DCenterPosX)
        Me.grpArc3D.Controls.Add(Me.lblArc3DStartPosZUnit)
        Me.grpArc3D.Controls.Add(Me.txtArc3DStartPosZ)
        Me.grpArc3D.Controls.Add(Me.lblArc3DStartPosZ)
        Me.grpArc3D.Controls.Add(Me.lblArc3DEndPosZUnit)
        Me.grpArc3D.Controls.Add(Me.lblArc3DEndPosYUnit)
        Me.grpArc3D.Controls.Add(Me.lblArc3DEndPosXUnit)
        Me.grpArc3D.Controls.Add(Me.lblArc3DStartPosYUnit)
        Me.grpArc3D.Controls.Add(Me.lblArc3DStartPosXUnit)
        Me.grpArc3D.Controls.Add(Me.txtArc3DEndPosZ)
        Me.grpArc3D.Controls.Add(Me.txtArc3DEndPosY)
        Me.grpArc3D.Controls.Add(Me.txtArc3DEndPosX)
        Me.grpArc3D.Controls.Add(Me.txtArc3DStartPosY)
        Me.grpArc3D.Controls.Add(Me.txtArc3DStartPosX)
        Me.grpArc3D.Controls.Add(Me.lblArc3DStartPosY)
        Me.grpArc3D.Controls.Add(Me.lblArc3DStartPosX)
        Me.grpArc3D.Controls.Add(Me.lblArc3DCenterPosZ)
        Me.grpArc3D.Controls.Add(Me.lblArc3DCenterPosY)
        Me.grpArc3D.Controls.Add(Me.lblArc3DCenterPosX)
        Me.grpArc3D.Controls.Add(Me.lblArc3DEndPosZ)
        Me.grpArc3D.Controls.Add(Me.lblArc3DEndPosY)
        Me.grpArc3D.Controls.Add(Me.lblArc3DEndPosX)
        resources.ApplyResources(Me.grpArc3D, "grpArc3D")
        Me.grpArc3D.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpArc3D.Name = "grpArc3D"
        Me.grpArc3D.TabStop = False
        '
        'tlpArc3DValueUc
        '
        resources.ApplyResources(Me.tlpArc3DValueUc, "tlpArc3DValueUc")
        Me.tlpArc3DValueUc.Controls.Add(Me.btArc3DTypeSelect, 1, 0)
        Me.tlpArc3DValueUc.Controls.Add(Me.cbArc3DTypeSelect, 0, 1)
        Me.tlpArc3DValueUc.Controls.Add(Me.lbArc3DTypeSelect, 0, 0)
        Me.tlpArc3DValueUc.Name = "tlpArc3DValueUc"
        '
        'btArc3DTypeSelect
        '
        Me.btArc3DTypeSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btArc3DTypeSelect, "btArc3DTypeSelect")
        Me.btArc3DTypeSelect.FlatAppearance.BorderSize = 0
        Me.btArc3DTypeSelect.Name = "btArc3DTypeSelect"
        Me.tlpArc3DValueUc.SetRowSpan(Me.btArc3DTypeSelect, 2)
        Me.ToolTip1.SetToolTip(Me.btArc3DTypeSelect, resources.GetString("btArc3DTypeSelect.ToolTip"))
        Me.btArc3DTypeSelect.UseVisualStyleBackColor = True
        '
        'cbArc3DTypeSelect
        '
        Me.cbArc3DTypeSelect.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.cbArc3DTypeSelect, "cbArc3DTypeSelect")
        Me.cbArc3DTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbArc3DTypeSelect.FormattingEnabled = True
        Me.cbArc3DTypeSelect.Name = "cbArc3DTypeSelect"
        '
        'lbArc3DTypeSelect
        '
        resources.ApplyResources(Me.lbArc3DTypeSelect, "lbArc3DTypeSelect")
        Me.lbArc3DTypeSelect.Name = "lbArc3DTypeSelect"
        '
        'btnArc3DCancel
        '
        resources.ApplyResources(Me.btnArc3DCancel, "btnArc3DCancel")
        Me.btnArc3DCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnArc3DCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnArc3DCancel.FlatAppearance.BorderSize = 0
        Me.btnArc3DCancel.Name = "btnArc3DCancel"
        Me.ToolTip1.SetToolTip(Me.btnArc3DCancel, resources.GetString("btnArc3DCancel.ToolTip"))
        Me.btnArc3DCancel.UseVisualStyleBackColor = True
        '
        'btnArc3DDone
        '
        resources.ApplyResources(Me.btnArc3DDone, "btnArc3DDone")
        Me.btnArc3DDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnArc3DDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnArc3DDone.FlatAppearance.BorderSize = 0
        Me.btnArc3DDone.Name = "btnArc3DDone"
        Me.ToolTip1.SetToolTip(Me.btnArc3DDone, resources.GetString("btnArc3DDone.ToolTip"))
        Me.btnArc3DDone.UseVisualStyleBackColor = True
        '
        'btnArc3DCenterMove
        '
        resources.ApplyResources(Me.btnArc3DCenterMove, "btnArc3DCenterMove")
        Me.btnArc3DCenterMove.FlatAppearance.BorderSize = 0
        Me.btnArc3DCenterMove.Name = "btnArc3DCenterMove"
        Me.ToolTip1.SetToolTip(Me.btnArc3DCenterMove, resources.GetString("btnArc3DCenterMove.ToolTip"))
        Me.btnArc3DCenterMove.UseVisualStyleBackColor = True
        '
        'btnArc3DEndMove
        '
        resources.ApplyResources(Me.btnArc3DEndMove, "btnArc3DEndMove")
        Me.btnArc3DEndMove.FlatAppearance.BorderSize = 0
        Me.btnArc3DEndMove.Name = "btnArc3DEndMove"
        Me.ToolTip1.SetToolTip(Me.btnArc3DEndMove, resources.GetString("btnArc3DEndMove.ToolTip"))
        Me.btnArc3DEndMove.UseVisualStyleBackColor = True
        '
        'btnArc3DStartrMove
        '
        resources.ApplyResources(Me.btnArc3DStartrMove, "btnArc3DStartrMove")
        Me.btnArc3DStartrMove.FlatAppearance.BorderSize = 0
        Me.btnArc3DStartrMove.Name = "btnArc3DStartrMove"
        Me.ToolTip1.SetToolTip(Me.btnArc3DStartrMove, resources.GetString("btnArc3DStartrMove.ToolTip"))
        Me.btnArc3DStartrMove.UseVisualStyleBackColor = True
        '
        'btnArc3DCenterSet
        '
        Me.btnArc3DCenterSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnArc3DCenterSet, "btnArc3DCenterSet")
        Me.btnArc3DCenterSet.FlatAppearance.BorderSize = 0
        Me.btnArc3DCenterSet.Name = "btnArc3DCenterSet"
        Me.ToolTip1.SetToolTip(Me.btnArc3DCenterSet, resources.GetString("btnArc3DCenterSet.ToolTip"))
        Me.btnArc3DCenterSet.UseVisualStyleBackColor = True
        '
        'btnArc3DEndSet
        '
        Me.btnArc3DEndSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnArc3DEndSet, "btnArc3DEndSet")
        Me.btnArc3DEndSet.FlatAppearance.BorderSize = 0
        Me.btnArc3DEndSet.Name = "btnArc3DEndSet"
        Me.ToolTip1.SetToolTip(Me.btnArc3DEndSet, resources.GetString("btnArc3DEndSet.ToolTip"))
        Me.btnArc3DEndSet.UseVisualStyleBackColor = True
        '
        'btnArc3DStartSet
        '
        Me.btnArc3DStartSet.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnArc3DStartSet, "btnArc3DStartSet")
        Me.btnArc3DStartSet.FlatAppearance.BorderSize = 0
        Me.btnArc3DStartSet.Name = "btnArc3DStartSet"
        Me.ToolTip1.SetToolTip(Me.btnArc3DStartSet, resources.GetString("btnArc3DStartSet.ToolTip"))
        Me.btnArc3DStartSet.UseVisualStyleBackColor = True
        '
        'lblArc3DAngleUnit
        '
        resources.ApplyResources(Me.lblArc3DAngleUnit, "lblArc3DAngleUnit")
        Me.lblArc3DAngleUnit.Name = "lblArc3DAngleUnit"
        '
        'txtArc3DAngle
        '
        resources.ApplyResources(Me.txtArc3DAngle, "txtArc3DAngle")
        Me.txtArc3DAngle.Name = "txtArc3DAngle"
        '
        'lblArc3DAngle
        '
        resources.ApplyResources(Me.lblArc3DAngle, "lblArc3DAngle")
        Me.lblArc3DAngle.Name = "lblArc3DAngle"
        '
        'lblArc3DCenterPosZUnit
        '
        resources.ApplyResources(Me.lblArc3DCenterPosZUnit, "lblArc3DCenterPosZUnit")
        Me.lblArc3DCenterPosZUnit.Name = "lblArc3DCenterPosZUnit"
        '
        'txtArc3DCenterPosZ
        '
        resources.ApplyResources(Me.txtArc3DCenterPosZ, "txtArc3DCenterPosZ")
        Me.txtArc3DCenterPosZ.Name = "txtArc3DCenterPosZ"
        '
        'lblArc3DCenterPosYUnit
        '
        resources.ApplyResources(Me.lblArc3DCenterPosYUnit, "lblArc3DCenterPosYUnit")
        Me.lblArc3DCenterPosYUnit.Name = "lblArc3DCenterPosYUnit"
        '
        'lblArc3DCenterPosXUnit
        '
        resources.ApplyResources(Me.lblArc3DCenterPosXUnit, "lblArc3DCenterPosXUnit")
        Me.lblArc3DCenterPosXUnit.Name = "lblArc3DCenterPosXUnit"
        '
        'txtArc3DCenterPosY
        '
        resources.ApplyResources(Me.txtArc3DCenterPosY, "txtArc3DCenterPosY")
        Me.txtArc3DCenterPosY.Name = "txtArc3DCenterPosY"
        '
        'txtArc3DCenterPosX
        '
        resources.ApplyResources(Me.txtArc3DCenterPosX, "txtArc3DCenterPosX")
        Me.txtArc3DCenterPosX.Name = "txtArc3DCenterPosX"
        '
        'lblArc3DStartPosZUnit
        '
        resources.ApplyResources(Me.lblArc3DStartPosZUnit, "lblArc3DStartPosZUnit")
        Me.lblArc3DStartPosZUnit.Name = "lblArc3DStartPosZUnit"
        '
        'txtArc3DStartPosZ
        '
        resources.ApplyResources(Me.txtArc3DStartPosZ, "txtArc3DStartPosZ")
        Me.txtArc3DStartPosZ.Name = "txtArc3DStartPosZ"
        '
        'lblArc3DStartPosZ
        '
        resources.ApplyResources(Me.lblArc3DStartPosZ, "lblArc3DStartPosZ")
        Me.lblArc3DStartPosZ.Name = "lblArc3DStartPosZ"
        '
        'lblArc3DEndPosZUnit
        '
        resources.ApplyResources(Me.lblArc3DEndPosZUnit, "lblArc3DEndPosZUnit")
        Me.lblArc3DEndPosZUnit.Name = "lblArc3DEndPosZUnit"
        '
        'lblArc3DEndPosYUnit
        '
        resources.ApplyResources(Me.lblArc3DEndPosYUnit, "lblArc3DEndPosYUnit")
        Me.lblArc3DEndPosYUnit.Name = "lblArc3DEndPosYUnit"
        '
        'lblArc3DEndPosXUnit
        '
        resources.ApplyResources(Me.lblArc3DEndPosXUnit, "lblArc3DEndPosXUnit")
        Me.lblArc3DEndPosXUnit.Name = "lblArc3DEndPosXUnit"
        '
        'lblArc3DStartPosYUnit
        '
        resources.ApplyResources(Me.lblArc3DStartPosYUnit, "lblArc3DStartPosYUnit")
        Me.lblArc3DStartPosYUnit.Name = "lblArc3DStartPosYUnit"
        '
        'lblArc3DStartPosXUnit
        '
        resources.ApplyResources(Me.lblArc3DStartPosXUnit, "lblArc3DStartPosXUnit")
        Me.lblArc3DStartPosXUnit.Name = "lblArc3DStartPosXUnit"
        '
        'txtArc3DEndPosZ
        '
        resources.ApplyResources(Me.txtArc3DEndPosZ, "txtArc3DEndPosZ")
        Me.txtArc3DEndPosZ.Name = "txtArc3DEndPosZ"
        '
        'txtArc3DEndPosY
        '
        resources.ApplyResources(Me.txtArc3DEndPosY, "txtArc3DEndPosY")
        Me.txtArc3DEndPosY.Name = "txtArc3DEndPosY"
        '
        'txtArc3DEndPosX
        '
        resources.ApplyResources(Me.txtArc3DEndPosX, "txtArc3DEndPosX")
        Me.txtArc3DEndPosX.Name = "txtArc3DEndPosX"
        '
        'txtArc3DStartPosY
        '
        resources.ApplyResources(Me.txtArc3DStartPosY, "txtArc3DStartPosY")
        Me.txtArc3DStartPosY.Name = "txtArc3DStartPosY"
        '
        'txtArc3DStartPosX
        '
        resources.ApplyResources(Me.txtArc3DStartPosX, "txtArc3DStartPosX")
        Me.txtArc3DStartPosX.Name = "txtArc3DStartPosX"
        '
        'lblArc3DStartPosY
        '
        resources.ApplyResources(Me.lblArc3DStartPosY, "lblArc3DStartPosY")
        Me.lblArc3DStartPosY.Name = "lblArc3DStartPosY"
        '
        'lblArc3DStartPosX
        '
        resources.ApplyResources(Me.lblArc3DStartPosX, "lblArc3DStartPosX")
        Me.lblArc3DStartPosX.Name = "lblArc3DStartPosX"
        '
        'lblArc3DCenterPosZ
        '
        resources.ApplyResources(Me.lblArc3DCenterPosZ, "lblArc3DCenterPosZ")
        Me.lblArc3DCenterPosZ.Name = "lblArc3DCenterPosZ"
        '
        'lblArc3DCenterPosY
        '
        resources.ApplyResources(Me.lblArc3DCenterPosY, "lblArc3DCenterPosY")
        Me.lblArc3DCenterPosY.Name = "lblArc3DCenterPosY"
        '
        'lblArc3DCenterPosX
        '
        resources.ApplyResources(Me.lblArc3DCenterPosX, "lblArc3DCenterPosX")
        Me.lblArc3DCenterPosX.Name = "lblArc3DCenterPosX"
        '
        'lblArc3DEndPosZ
        '
        resources.ApplyResources(Me.lblArc3DEndPosZ, "lblArc3DEndPosZ")
        Me.lblArc3DEndPosZ.Name = "lblArc3DEndPosZ"
        '
        'lblArc3DEndPosY
        '
        resources.ApplyResources(Me.lblArc3DEndPosY, "lblArc3DEndPosY")
        Me.lblArc3DEndPosY.Name = "lblArc3DEndPosY"
        '
        'lblArc3DEndPosX
        '
        resources.ApplyResources(Me.lblArc3DEndPosX, "lblArc3DEndPosX")
        Me.lblArc3DEndPosX.Name = "lblArc3DEndPosX"
        '
        'tabExtendOn
        '
        Me.tabExtendOn.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabExtendOn.Controls.Add(Me.grpExtendOn)
        resources.ApplyResources(Me.tabExtendOn, "tabExtendOn")
        Me.tabExtendOn.Name = "tabExtendOn"
        '
        'grpExtendOn
        '
        Me.grpExtendOn.Controls.Add(Me.btnExtendOnCancel)
        Me.grpExtendOn.Controls.Add(Me.btnExtendOnDone)
        resources.ApplyResources(Me.grpExtendOn, "grpExtendOn")
        Me.grpExtendOn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpExtendOn.Name = "grpExtendOn"
        Me.grpExtendOn.TabStop = False
        '
        'btnExtendOnCancel
        '
        resources.ApplyResources(Me.btnExtendOnCancel, "btnExtendOnCancel")
        Me.btnExtendOnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtendOnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnExtendOnCancel.FlatAppearance.BorderSize = 0
        Me.btnExtendOnCancel.Name = "btnExtendOnCancel"
        Me.ToolTip1.SetToolTip(Me.btnExtendOnCancel, resources.GetString("btnExtendOnCancel.ToolTip"))
        Me.btnExtendOnCancel.UseVisualStyleBackColor = True
        '
        'btnExtendOnDone
        '
        resources.ApplyResources(Me.btnExtendOnDone, "btnExtendOnDone")
        Me.btnExtendOnDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtendOnDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnExtendOnDone.FlatAppearance.BorderSize = 0
        Me.btnExtendOnDone.Name = "btnExtendOnDone"
        Me.ToolTip1.SetToolTip(Me.btnExtendOnDone, resources.GetString("btnExtendOnDone.ToolTip"))
        Me.btnExtendOnDone.UseVisualStyleBackColor = True
        '
        'tabExtendOff
        '
        Me.tabExtendOff.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabExtendOff.Controls.Add(Me.grpExtendOff)
        resources.ApplyResources(Me.tabExtendOff, "tabExtendOff")
        Me.tabExtendOff.Name = "tabExtendOff"
        '
        'grpExtendOff
        '
        Me.grpExtendOff.Controls.Add(Me.btnExtendOffCancel)
        Me.grpExtendOff.Controls.Add(Me.btnExtendOffDone)
        resources.ApplyResources(Me.grpExtendOff, "grpExtendOff")
        Me.grpExtendOff.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpExtendOff.Name = "grpExtendOff"
        Me.grpExtendOff.TabStop = False
        '
        'btnExtendOffCancel
        '
        resources.ApplyResources(Me.btnExtendOffCancel, "btnExtendOffCancel")
        Me.btnExtendOffCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtendOffCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnExtendOffCancel.FlatAppearance.BorderSize = 0
        Me.btnExtendOffCancel.Name = "btnExtendOffCancel"
        Me.ToolTip1.SetToolTip(Me.btnExtendOffCancel, resources.GetString("btnExtendOffCancel.ToolTip"))
        Me.btnExtendOffCancel.UseVisualStyleBackColor = True
        '
        'btnExtendOffDone
        '
        resources.ApplyResources(Me.btnExtendOffDone, "btnExtendOffDone")
        Me.btnExtendOffDone.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtendOffDone.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnExtendOffDone.FlatAppearance.BorderSize = 0
        Me.btnExtendOffDone.Name = "btnExtendOffDone"
        Me.ToolTip1.SetToolTip(Me.btnExtendOffDone, resources.GetString("btnExtendOffDone.ToolTip"))
        Me.btnExtendOffDone.UseVisualStyleBackColor = True
        '
        'UcJoyStick1
        '
        Me.UcJoyStick1.AXisA = 0
        Me.UcJoyStick1.AXisB = 0
        Me.UcJoyStick1.AXisC = 0
        Me.UcJoyStick1.AxisX = 0
        Me.UcJoyStick1.AxisY = 0
        Me.UcJoyStick1.AxisZ = 0
        Me.UcJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcJoyStick1, "UcJoyStick1")
        Me.UcJoyStick1.Name = "UcJoyStick1"
        '
        'UcLightControl1
        '
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.Name = "UcLightControl1"
        '
        'frmRecipe04Step
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.tabStep)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRecipe04Step"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.tabStep.ResumeLayout(False)
        Me.tabSelectValve.ResumeLayout(False)
        Me.grpSelectValve.ResumeLayout(False)
        Me.grpSelectValve.PerformLayout()
        Me.TabContiStart.ResumeLayout(False)
        Me.grpCOntiStart.ResumeLayout(False)
        Me.TabContiEnd.ResumeLayout(False)
        Me.grpContiEnd.ResumeLayout(False)
        Me.tabMove3D.ResumeLayout(False)
        Me.grpMove3D.ResumeLayout(False)
        Me.grpMove3D.PerformLayout()
        Me.tabDots3D.ResumeLayout(False)
        Me.grpDot3D.ResumeLayout(False)
        Me.grpDot3D.PerformLayout()
        Me.tlpDotValueUc.ResumeLayout(False)
        Me.tabLine3D.ResumeLayout(False)
        Me.grpLine3D.ResumeLayout(False)
        Me.grpLine3D.PerformLayout()
        CType(Me.nmuLine3DStartVelocity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuLine3DWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuLine3DVelocity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuLine3DDot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpLineValueUc.ResumeLayout(False)
        Me.tabArc.ResumeLayout(False)
        Me.grpArc.ResumeLayout(False)
        Me.grpArc.PerformLayout()
        CType(Me.nmuArcStartVelocity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuArcVelocity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuArcDot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuArcWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpArcValueUc.ResumeLayout(False)
        Me.tabCircle.ResumeLayout(False)
        Me.grpCircle.ResumeLayout(False)
        Me.grpCircle.PerformLayout()
        CType(Me.nmuCircleStartVelocity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuCircleVelocity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuCircleDot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuCircleWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpCircleValueUc.ResumeLayout(False)
        Me.tabWait.ResumeLayout(False)
        Me.grpWait.ResumeLayout(False)
        Me.grpWait.PerformLayout()
        Me.tabCircle3D.ResumeLayout(False)
        Me.grpCircle3D.ResumeLayout(False)
        Me.grpCircle3D.PerformLayout()
        Me.tlpCircle3DValueUc.ResumeLayout(False)
        Me.tabArc3D.ResumeLayout(False)
        Me.grpArc3D.ResumeLayout(False)
        Me.grpArc3D.PerformLayout()
        Me.tlpArc3DValueUc.ResumeLayout(False)
        Me.tabExtendOn.ResumeLayout(False)
        Me.grpExtendOn.ResumeLayout(False)
        Me.tabExtendOff.ResumeLayout(False)
        Me.grpExtendOff.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents tabStep As System.Windows.Forms.TabControl
    Friend WithEvents tabSelectValve As System.Windows.Forms.TabPage
    Friend WithEvents grpSelectValve As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectValveDone As System.Windows.Forms.Button
    Friend WithEvents lblSelect As System.Windows.Forms.Label
    Friend WithEvents cmbValve As System.Windows.Forms.ComboBox
    Friend WithEvents TabContiStart As System.Windows.Forms.TabPage
    Friend WithEvents grpCOntiStart As System.Windows.Forms.GroupBox
    Friend WithEvents btnContiStartDone As System.Windows.Forms.Button
    Friend WithEvents TabContiEnd As System.Windows.Forms.TabPage
    Friend WithEvents grpContiEnd As System.Windows.Forms.GroupBox
    Friend WithEvents btnContiEndDone As System.Windows.Forms.Button
    Friend WithEvents tabMove3D As System.Windows.Forms.TabPage
    Friend WithEvents grpMove3D As System.Windows.Forms.GroupBox
    Friend WithEvents btnMove3DGo As System.Windows.Forms.Button
    Friend WithEvents btnMove3DSet As System.Windows.Forms.Button
    Friend WithEvents lblMove3DEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblMove3DEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblMove3DEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtMove3DEndPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtMove3DEndPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtMove3DEndPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblMove3DEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblMove3DEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblMove3DEndPosX As System.Windows.Forms.Label
    Friend WithEvents btnMove3DDone As System.Windows.Forms.Button
    Friend WithEvents tabDots3D As System.Windows.Forms.TabPage
    Friend WithEvents grpDot3D As System.Windows.Forms.GroupBox
    Friend WithEvents btnDots3DGo As System.Windows.Forms.Button
    Friend WithEvents btnDots3DSet As System.Windows.Forms.Button
    Friend WithEvents lblDots3DEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblDots3DEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblDots3DEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtDots3DPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtDots3DPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtDots3DPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblDots3DEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblDots3DEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblDots3DEndPosX As System.Windows.Forms.Label
    Friend WithEvents btnDots3DDone As System.Windows.Forms.Button
    Friend WithEvents tabLine3D As System.Windows.Forms.TabPage
    Friend WithEvents grpLine3D As System.Windows.Forms.GroupBox
    Friend WithEvents btnLine3DEndMove As System.Windows.Forms.Button
    Friend WithEvents btnLine3DStartMove As System.Windows.Forms.Button
    Friend WithEvents txtLine3DPitch As System.Windows.Forms.TextBox
    Friend WithEvents lblLine3DPitch As System.Windows.Forms.Label
    Friend WithEvents lblLine3DPitchUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DDot As System.Windows.Forms.Label
    Friend WithEvents lblLine3DWeight As System.Windows.Forms.Label
    Friend WithEvents btnLine3DEndSet As System.Windows.Forms.Button
    Friend WithEvents btnLine3DStartSet As System.Windows.Forms.Button
    Friend WithEvents lblLine3DStartPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtLine3DStartPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblLine3DStartPosZ As System.Windows.Forms.Label
    Friend WithEvents lblLine3DEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DStartPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DStartPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtLine3DEndPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtLine3DEndPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtLine3DEndPosX As System.Windows.Forms.TextBox
    Friend WithEvents txtLine3DStartPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtLine3DStartPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblLine3DStartPosY As System.Windows.Forms.Label
    Friend WithEvents lblLine3DStartPosX As System.Windows.Forms.Label
    Friend WithEvents lblLine3DEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblLine3DEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblLine3DEndPosX As System.Windows.Forms.Label
    Friend WithEvents btnLine3DDone As System.Windows.Forms.Button
    Friend WithEvents tabArc As System.Windows.Forms.TabPage
    Friend WithEvents grpArc As System.Windows.Forms.GroupBox
    Friend WithEvents txtArcPitch As System.Windows.Forms.TextBox
    Friend WithEvents lblArcPitch As System.Windows.Forms.Label
    Friend WithEvents lblArcPitchUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcDot As System.Windows.Forms.Label
    Friend WithEvents lblArcWeight As System.Windows.Forms.Label
    Friend WithEvents txtArcMidPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtArcMidPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArcMidPosX As System.Windows.Forms.TextBox
    Friend WithEvents btnArcMidMove As System.Windows.Forms.Button
    Friend WithEvents btnArcEndMove As System.Windows.Forms.Button
    Friend WithEvents btnArcStartMove As System.Windows.Forms.Button
    Friend WithEvents btnArcMidSet As System.Windows.Forms.Button
    Friend WithEvents btnArcEndSet As System.Windows.Forms.Button
    Friend WithEvents btnArcStartSet As System.Windows.Forms.Button
    Friend WithEvents lblArcAngleUnit As System.Windows.Forms.Label
    Friend WithEvents txtArcAngle As System.Windows.Forms.TextBox
    Friend WithEvents lblArcAngle As System.Windows.Forms.Label
    Friend WithEvents lblArcMidPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtArcCenterPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblArcMidPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcMidPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtArcCenterPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArcCenterPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblArcStartPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtArcStartPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblArcStartPosZ As System.Windows.Forms.Label
    Friend WithEvents lblArcEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcStartPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcStartPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtArcEndPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtArcEndPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArcEndPosX As System.Windows.Forms.TextBox
    Friend WithEvents txtArcStartPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArcStartPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblArcStartPosY As System.Windows.Forms.Label
    Friend WithEvents lblArcStartPosX As System.Windows.Forms.Label
    Friend WithEvents lblArcMidPosZ As System.Windows.Forms.Label
    Friend WithEvents lblArcMidPosY As System.Windows.Forms.Label
    Friend WithEvents lblArcMidPosX As System.Windows.Forms.Label
    Friend WithEvents lblArcEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblArcEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblArcEndPosX As System.Windows.Forms.Label
    Friend WithEvents btnArcDone As System.Windows.Forms.Button
    Friend WithEvents tabCircle As System.Windows.Forms.TabPage
    Friend WithEvents grpCircle As System.Windows.Forms.GroupBox
    Friend WithEvents txtCirclePitch As System.Windows.Forms.TextBox
    Friend WithEvents lblCirclePitch As System.Windows.Forms.Label
    Friend WithEvents lblCirclePitchUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleDot As System.Windows.Forms.Label
    Friend WithEvents lblCircleWeight As System.Windows.Forms.Label
    Friend WithEvents txtCircleMidPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleMidPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleMidPosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCircleStartMove As System.Windows.Forms.Button
    Friend WithEvents btnCircleStartSet As System.Windows.Forms.Button
    Friend WithEvents txtCircleStartPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleStartPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleStartPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCircleStartPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleStartPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleStartPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleStartPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCircleStartPosY As System.Windows.Forms.Label
    Friend WithEvents lblCircleStartPosX As System.Windows.Forms.Label
    Friend WithEvents btnCircleMidMove As System.Windows.Forms.Button
    Friend WithEvents btnCircleEndMove As System.Windows.Forms.Button
    Friend WithEvents btnCircleMidSet As System.Windows.Forms.Button
    Friend WithEvents btnCircleEndSet As System.Windows.Forms.Button
    Friend WithEvents lblCircleMidPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtCircleCenterPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblCircleMidPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleMidPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtCircleCenterPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleCenterPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCircleEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtCircleMid2PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleMid2PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCircleMid2PosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCircleMidPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCircleMidPosY As System.Windows.Forms.Label
    Friend WithEvents lblCircleMidPosX As System.Windows.Forms.Label
    Friend WithEvents lblCircleEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCircleEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblCircleEndPosX As System.Windows.Forms.Label
    Friend WithEvents btnCircleDone As System.Windows.Forms.Button
    Friend WithEvents tabWait As System.Windows.Forms.TabPage
    Friend WithEvents grpWait As System.Windows.Forms.GroupBox
    Friend WithEvents btnWaitDone As System.Windows.Forms.Button
    Friend WithEvents lblWaitDwellTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtWaitDwellTime As System.Windows.Forms.TextBox
    Friend WithEvents lblWaitDwellTime As System.Windows.Forms.Label
    Friend WithEvents tabCircle3D As System.Windows.Forms.TabPage
    Friend WithEvents grpCircle3D As System.Windows.Forms.GroupBox
    Friend WithEvents btnCircle3DDone As System.Windows.Forms.Button
    Friend WithEvents btnCircle3DCenterMove As System.Windows.Forms.Button
    Friend WithEvents btnCircle3DEndMove As System.Windows.Forms.Button
    Friend WithEvents btnCircleCenterSet As System.Windows.Forms.Button
    Friend WithEvents btnCircle3DEndSet As System.Windows.Forms.Button
    Friend WithEvents lblCircle3DCenterPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtCircle3DCenterPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblCircle3DCenterPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DCenterPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtCircle3DCenterPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCircle3DCenterPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCircle3DEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtCircle3DEndPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCircle3DEndPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCircle3DEndPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCircle3DCenterPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DCenterPosY As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DCenterPosX As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblCircle3DEndPosX As System.Windows.Forms.Label
    Friend WithEvents tabArc3D As System.Windows.Forms.TabPage
    Friend WithEvents grpArc3D As System.Windows.Forms.GroupBox
    Friend WithEvents btnArc3DDone As System.Windows.Forms.Button
    Friend WithEvents btnArc3DCenterMove As System.Windows.Forms.Button
    Friend WithEvents btnArc3DEndMove As System.Windows.Forms.Button
    Friend WithEvents btnArc3DStartrMove As System.Windows.Forms.Button
    Friend WithEvents btnArc3DCenterSet As System.Windows.Forms.Button
    Friend WithEvents btnArc3DEndSet As System.Windows.Forms.Button
    Friend WithEvents btnArc3DStartSet As System.Windows.Forms.Button
    Friend WithEvents lblArc3DAngleUnit As System.Windows.Forms.Label
    Friend WithEvents txtArc3DAngle As System.Windows.Forms.TextBox
    Friend WithEvents lblArc3DAngle As System.Windows.Forms.Label
    Friend WithEvents lblArc3DCenterPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtArc3DCenterPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblArc3DCenterPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArc3DCenterPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtArc3DCenterPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArc3DCenterPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblArc3DStartPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtArc3DStartPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblArc3DStartPosZ As System.Windows.Forms.Label
    Friend WithEvents lblArc3DEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblArc3DEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArc3DEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblArc3DStartPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArc3DStartPosXUnit As System.Windows.Forms.Label
    Friend WithEvents txtArc3DEndPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtArc3DEndPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArc3DEndPosX As System.Windows.Forms.TextBox
    Friend WithEvents txtArc3DStartPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtArc3DStartPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblArc3DStartPosY As System.Windows.Forms.Label
    Friend WithEvents lblArc3DStartPosX As System.Windows.Forms.Label
    Friend WithEvents lblArc3DCenterPosZ As System.Windows.Forms.Label
    Friend WithEvents lblArc3DCenterPosY As System.Windows.Forms.Label
    Friend WithEvents lblArc3DCenterPosX As System.Windows.Forms.Label
    Friend WithEvents lblArc3DEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblArc3DEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblArc3DEndPosX As System.Windows.Forms.Label
    Friend WithEvents tabExtendOn As System.Windows.Forms.TabPage
    Friend WithEvents grpExtendOn As System.Windows.Forms.GroupBox
    Friend WithEvents btnExtendOnDone As System.Windows.Forms.Button
    Friend WithEvents tabExtendOff As System.Windows.Forms.TabPage
    Friend WithEvents grpExtendOff As System.Windows.Forms.GroupBox
    Friend WithEvents btnExtendOffDone As System.Windows.Forms.Button
    Friend WithEvents btnSelectValveCancel As System.Windows.Forms.Button
    Friend WithEvents btnContiStartCancel As System.Windows.Forms.Button
    Friend WithEvents btnContiEndCancel As System.Windows.Forms.Button
    Friend WithEvents btnMove3DCancel As System.Windows.Forms.Button
    Friend WithEvents btnDot3DCancel As System.Windows.Forms.Button
    Friend WithEvents btnLine3DCancel As System.Windows.Forms.Button
    Friend WithEvents btnArcCancel As System.Windows.Forms.Button
    Friend WithEvents btnCircleCancel As System.Windows.Forms.Button
    Friend WithEvents btnWaitCancel As System.Windows.Forms.Button
    Friend WithEvents btnCircle3DCancel As System.Windows.Forms.Button
    Friend WithEvents btnArc3DCancel As System.Windows.Forms.Button
    Friend WithEvents btnExtendOnCancel As System.Windows.Forms.Button
    Friend WithEvents btnExtendOffCancel As System.Windows.Forms.Button
    Friend WithEvents cmbPosB As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDots3DVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents txtDots3DVelocity As System.Windows.Forms.TextBox
    Friend WithEvents lblDots3DVelocity As System.Windows.Forms.Label
    Friend WithEvents txtLine3DComment As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtArcComment As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCircleComment As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnLine3DRefresh As System.Windows.Forms.Button
    Friend WithEvents btnArcRefresh As System.Windows.Forms.Button
    Friend WithEvents btnCircleRefresh As System.Windows.Forms.Button
    Friend WithEvents tlpDotValueUc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btDotTypeSelect As System.Windows.Forms.Button
    Friend WithEvents cbDotTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lbDotTypeSelect As System.Windows.Forms.Label
    Friend WithEvents tlpLineValueUc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btLineTypeSelect As System.Windows.Forms.Button
    Friend WithEvents cbLineTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lbLineTypeSelect As System.Windows.Forms.Label
    Friend WithEvents tlpArcValueUc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btArcTypeSelect As System.Windows.Forms.Button
    Friend WithEvents cbArcTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lbArcTypeSelect As System.Windows.Forms.Label
    Friend WithEvents tlpCircleValueUc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btCircleTypeSelect As System.Windows.Forms.Button
    Friend WithEvents cbCircleTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lbCircleTypeSelect As System.Windows.Forms.Label
    Friend WithEvents tlpCircle3DValueUc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btCircle3DTypeSelect As System.Windows.Forms.Button
    Friend WithEvents cbCircle3DTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lbCircle3DTypeSelect As System.Windows.Forms.Label
    Friend WithEvents tlpArc3DValueUc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btArc3DTypeSelect As System.Windows.Forms.Button
    Friend WithEvents cbArc3DTypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents lbArc3DTypeSelect As System.Windows.Forms.Label
    Friend WithEvents btnDot3DGetPos As System.Windows.Forms.Button
    Friend WithEvents btnLine3DGetPos As System.Windows.Forms.Button
    Friend WithEvents btnArcGetPos As System.Windows.Forms.Button
    Friend WithEvents lblDots3DDotWeight As System.Windows.Forms.Label
    Friend WithEvents txtDots3DDotWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblDots3DDotWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DDotWeight As System.Windows.Forms.Label
    Friend WithEvents txtLine3DDotWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblLine3DDotWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcDotWeight As System.Windows.Forms.Label
    Friend WithEvents txtArcDotWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblArcDotWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleDotWeight As System.Windows.Forms.Label
    Friend WithEvents txtCircleDotWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblCircleDotWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DVelocity As System.Windows.Forms.Label
    Friend WithEvents lblArcVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcVelocity As System.Windows.Forms.Label
    Friend WithEvents lblCircleVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleVelocity As System.Windows.Forms.Label
    Friend WithEvents lbConvertTiltAngle As System.Windows.Forms.Label
    Friend WithEvents txtDots3DDot As System.Windows.Forms.TextBox
    Friend WithEvents lblDots3DDot As System.Windows.Forms.Label
    Friend WithEvents txtDot3DWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblDot3DWeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblDot3DWeight As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents nmuLine3DDot As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuLine3DVelocity As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuLine3DWeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuArcWeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuArcDot As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuArcVelocity As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuCircleVelocity As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuCircleDot As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuCircleWeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuLine3DStartVelocity As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblLine3DStartVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblLine3DStartVelocity As System.Windows.Forms.Label
    Friend WithEvents nmuArcStartVelocity As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblArcStartVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblArcStartVelocity As System.Windows.Forms.Label
    Friend WithEvents nmuCircleStartVelocity As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCircleStartVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblCircleStartVelocity As System.Windows.Forms.Label
    Friend WithEvents UcLightControl1 As WindowsApplication1.ucLightControl
End Class

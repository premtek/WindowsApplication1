<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTest))
        Me.btnAUnload = New System.Windows.Forms.Button()
        Me.btnALoad = New System.Windows.Forms.Button()
        Me.btnBUnload = New System.Windows.Forms.Button()
        Me.btnBLoad = New System.Windows.Forms.Button()
        Me.timerCheckIO = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnAuto = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnAInitial = New System.Windows.Forms.Button()
        Me.btnBInitial = New System.Windows.Forms.Button()
        Me.chkbAuto = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnLdON6 = New System.Windows.Forms.Button()
        Me.btnLdOFF6 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnLdON5 = New System.Windows.Forms.Button()
        Me.btnLdOFF5 = New System.Windows.Forms.Button()
        Me.btnLdON4 = New System.Windows.Forms.Button()
        Me.btnLdOFF4 = New System.Windows.Forms.Button()
        Me.btnLdON3 = New System.Windows.Forms.Button()
        Me.btnLdOFF3 = New System.Windows.Forms.Button()
        Me.btnLdON2 = New System.Windows.Forms.Button()
        Me.btnLdOFF2 = New System.Windows.Forms.Button()
        Me.btnLdON1 = New System.Windows.Forms.Button()
        Me.btnLdOFF1 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.btnUlON5 = New System.Windows.Forms.Button()
        Me.btnUlOFF5 = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.btnUlON4 = New System.Windows.Forms.Button()
        Me.btnUlOFF4 = New System.Windows.Forms.Button()
        Me.btnUlON3 = New System.Windows.Forms.Button()
        Me.btnUlOFF3 = New System.Windows.Forms.Button()
        Me.btnUlON2 = New System.Windows.Forms.Button()
        Me.btnUlOFF2 = New System.Windows.Forms.Button()
        Me.btnUlON1 = New System.Windows.Forms.Button()
        Me.btnUlOFF1 = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.tbLDSlotStatus1 = New System.Windows.Forms.TextBox()
        Me.tbLDSlotStatus2 = New System.Windows.Forms.TextBox()
        Me.tbLDProductNumB = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tbLDTargetTemp = New System.Windows.Forms.TextBox()
        Me.tbLDPass = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tbLDCaseteBarCode = New System.Windows.Forms.TextBox()
        Me.tbLDProductCount = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.tbLDProductType = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tbLDProductNumA = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tbLDStatus = New System.Windows.Forms.TextBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.tbLDAlarmCode1 = New System.Windows.Forms.TextBox()
        Me.tbLDAlarmCode2 = New System.Windows.Forms.TextBox()
        Me.tbLDAlarmCode3 = New System.Windows.Forms.TextBox()
        Me.tbLDAlarmCode4 = New System.Windows.Forms.TextBox()
        Me.tbLDAlarmCode5 = New System.Windows.Forms.TextBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.tbLDTemp1 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp2 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp7 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp8 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp3 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp4 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp9 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp10 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp12 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp5 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp6 = New System.Windows.Forms.TextBox()
        Me.tbLDTemp11 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.tbULSlotStatus1 = New System.Windows.Forms.TextBox()
        Me.tbULSlotStatus2 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.tbULTargetTemp = New System.Windows.Forms.TextBox()
        Me.tbULPass = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.tbULCaseteBarCode = New System.Windows.Forms.TextBox()
        Me.tbULProductCount = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.tbULProductType = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.tbULProductNum = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.tbULStatus = New System.Windows.Forms.TextBox()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.tbULAlarmCode1 = New System.Windows.Forms.TextBox()
        Me.tbULAlarmCode2 = New System.Windows.Forms.TextBox()
        Me.tbULAlarmCode3 = New System.Windows.Forms.TextBox()
        Me.tbULAlarmCode4 = New System.Windows.Forms.TextBox()
        Me.tbULAlarmCode5 = New System.Windows.Forms.TextBox()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.tbULTemp1 = New System.Windows.Forms.TextBox()
        Me.tbULTemp2 = New System.Windows.Forms.TextBox()
        Me.tbULTemp7 = New System.Windows.Forms.TextBox()
        Me.tbULTemp8 = New System.Windows.Forms.TextBox()
        Me.tbULTemp3 = New System.Windows.Forms.TextBox()
        Me.tbULTemp4 = New System.Windows.Forms.TextBox()
        Me.tbULTemp9 = New System.Windows.Forms.TextBox()
        Me.tbULTemp10 = New System.Windows.Forms.TextBox()
        Me.tbULTemp12 = New System.Windows.Forms.TextBox()
        Me.tbULTemp5 = New System.Windows.Forms.TextBox()
        Me.tbULTemp6 = New System.Windows.Forms.TextBox()
        Me.tbULTemp11 = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.btnSetLastNum = New System.Windows.Forms.Button()
        Me.tbSetProductNum = New System.Windows.Forms.TextBox()
        Me.btnGetAlarmCode = New System.Windows.Forms.Button()
        Me.btnCassetteAbort = New System.Windows.Forms.Button()
        Me.btnSetProductNum = New System.Windows.Forms.Button()
        Me.tbSetTargetTemp = New System.Windows.Forms.TextBox()
        Me.tbSetProductType = New System.Windows.Forms.TextBox()
        Me.chkbPass = New System.Windows.Forms.CheckBox()
        Me.btnGetProductNum = New System.Windows.Forms.Button()
        Me.btnGetCstBarCode = New System.Windows.Forms.Button()
        Me.btnGetCastetData = New System.Windows.Forms.Button()
        Me.btnSetTargetTemp = New System.Windows.Forms.Button()
        Me.btnSetProductType = New System.Windows.Forms.Button()
        Me.btnSetPassModel = New System.Windows.Forms.Button()
        Me.rbUnloader = New System.Windows.Forms.RadioButton()
        Me.rbLoader = New System.Windows.Forms.RadioButton()
        Me.imgListItems = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.picLD12 = New System.Windows.Forms.PictureBox()
        Me.picLD11 = New System.Windows.Forms.PictureBox()
        Me.picLD10 = New System.Windows.Forms.PictureBox()
        Me.picLD9 = New System.Windows.Forms.PictureBox()
        Me.picLD8 = New System.Windows.Forms.PictureBox()
        Me.picLD7 = New System.Windows.Forms.PictureBox()
        Me.picLD6 = New System.Windows.Forms.PictureBox()
        Me.picLD5 = New System.Windows.Forms.PictureBox()
        Me.picLD4 = New System.Windows.Forms.PictureBox()
        Me.picLD3 = New System.Windows.Forms.PictureBox()
        Me.picLD2 = New System.Windows.Forms.PictureBox()
        Me.picLD1 = New System.Windows.Forms.PictureBox()
        Me.picUL10 = New System.Windows.Forms.PictureBox()
        Me.picUL9 = New System.Windows.Forms.PictureBox()
        Me.picUL8 = New System.Windows.Forms.PictureBox()
        Me.picUL7 = New System.Windows.Forms.PictureBox()
        Me.picUL6 = New System.Windows.Forms.PictureBox()
        Me.picUL5 = New System.Windows.Forms.PictureBox()
        Me.picUL4 = New System.Windows.Forms.PictureBox()
        Me.picUL3 = New System.Windows.Forms.PictureBox()
        Me.picUL2 = New System.Windows.Forms.PictureBox()
        Me.picUL1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        CType(Me.picLD12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLD1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUL1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAUnload
        '
        resources.ApplyResources(Me.btnAUnload, "btnAUnload")
        Me.btnAUnload.Name = "btnAUnload"
        Me.ToolTip1.SetToolTip(Me.btnAUnload, resources.GetString("btnAUnload.ToolTip"))
        Me.btnAUnload.UseVisualStyleBackColor = True
        '
        'btnALoad
        '
        resources.ApplyResources(Me.btnALoad, "btnALoad")
        Me.btnALoad.Name = "btnALoad"
        Me.ToolTip1.SetToolTip(Me.btnALoad, resources.GetString("btnALoad.ToolTip"))
        Me.btnALoad.UseVisualStyleBackColor = True
        '
        'btnBUnload
        '
        resources.ApplyResources(Me.btnBUnload, "btnBUnload")
        Me.btnBUnload.Name = "btnBUnload"
        Me.ToolTip1.SetToolTip(Me.btnBUnload, resources.GetString("btnBUnload.ToolTip"))
        Me.btnBUnload.UseVisualStyleBackColor = True
        '
        'btnBLoad
        '
        resources.ApplyResources(Me.btnBLoad, "btnBLoad")
        Me.btnBLoad.Name = "btnBLoad"
        Me.ToolTip1.SetToolTip(Me.btnBLoad, resources.GetString("btnBLoad.ToolTip"))
        Me.btnBLoad.UseVisualStyleBackColor = True
        '
        'timerCheckIO
        '
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'GroupBox6
        '
        resources.ApplyResources(Me.GroupBox6, "GroupBox6")
        Me.GroupBox6.Controls.Add(Me.btnReset)
        Me.GroupBox6.Controls.Add(Me.btnAuto)
        Me.GroupBox6.Controls.Add(Me.btnStop)
        Me.GroupBox6.Controls.Add(Me.btnAInitial)
        Me.GroupBox6.Controls.Add(Me.btnBInitial)
        Me.GroupBox6.Controls.Add(Me.chkbAuto)
        Me.GroupBox6.Controls.Add(Me.btnALoad)
        Me.GroupBox6.Controls.Add(Me.btnBLoad)
        Me.GroupBox6.Controls.Add(Me.btnAUnload)
        Me.GroupBox6.Controls.Add(Me.btnBUnload)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox6, resources.GetString("GroupBox6.ToolTip"))
        '
        'btnReset
        '
        resources.ApplyResources(Me.btnReset, "btnReset")
        Me.btnReset.Name = "btnReset"
        Me.ToolTip1.SetToolTip(Me.btnReset, resources.GetString("btnReset.ToolTip"))
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnAuto
        '
        resources.ApplyResources(Me.btnAuto, "btnAuto")
        Me.btnAuto.Name = "btnAuto"
        Me.ToolTip1.SetToolTip(Me.btnAuto, resources.GetString("btnAuto.ToolTip"))
        Me.btnAuto.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        resources.ApplyResources(Me.btnStop, "btnStop")
        Me.btnStop.Name = "btnStop"
        Me.ToolTip1.SetToolTip(Me.btnStop, resources.GetString("btnStop.ToolTip"))
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnAInitial
        '
        resources.ApplyResources(Me.btnAInitial, "btnAInitial")
        Me.btnAInitial.Name = "btnAInitial"
        Me.ToolTip1.SetToolTip(Me.btnAInitial, resources.GetString("btnAInitial.ToolTip"))
        Me.btnAInitial.UseVisualStyleBackColor = True
        '
        'btnBInitial
        '
        resources.ApplyResources(Me.btnBInitial, "btnBInitial")
        Me.btnBInitial.Name = "btnBInitial"
        Me.ToolTip1.SetToolTip(Me.btnBInitial, resources.GetString("btnBInitial.ToolTip"))
        Me.btnBInitial.UseVisualStyleBackColor = True
        '
        'chkbAuto
        '
        resources.ApplyResources(Me.chkbAuto, "chkbAuto")
        Me.chkbAuto.Checked = True
        Me.chkbAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbAuto.Name = "chkbAuto"
        Me.ToolTip1.SetToolTip(Me.chkbAuto, resources.GetString("chkbAuto.ToolTip"))
        Me.chkbAuto.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Controls.Add(Me.btnLdON6)
        Me.GroupBox5.Controls.Add(Me.btnLdOFF6)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.picLD12)
        Me.GroupBox5.Controls.Add(Me.btnLdON5)
        Me.GroupBox5.Controls.Add(Me.btnLdOFF5)
        Me.GroupBox5.Controls.Add(Me.btnLdON4)
        Me.GroupBox5.Controls.Add(Me.btnLdOFF4)
        Me.GroupBox5.Controls.Add(Me.btnLdON3)
        Me.GroupBox5.Controls.Add(Me.btnLdOFF3)
        Me.GroupBox5.Controls.Add(Me.btnLdON2)
        Me.GroupBox5.Controls.Add(Me.btnLdOFF2)
        Me.GroupBox5.Controls.Add(Me.btnLdON1)
        Me.GroupBox5.Controls.Add(Me.btnLdOFF1)
        Me.GroupBox5.Controls.Add(Me.picLD11)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.picLD10)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.picLD9)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.picLD8)
        Me.GroupBox5.Controls.Add(Me.picLD7)
        Me.GroupBox5.Controls.Add(Me.picLD6)
        Me.GroupBox5.Controls.Add(Me.picLD5)
        Me.GroupBox5.Controls.Add(Me.picLD4)
        Me.GroupBox5.Controls.Add(Me.picLD3)
        Me.GroupBox5.Controls.Add(Me.picLD2)
        Me.GroupBox5.Controls.Add(Me.picLD1)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip"))
        '
        'btnLdON6
        '
        resources.ApplyResources(Me.btnLdON6, "btnLdON6")
        Me.btnLdON6.Name = "btnLdON6"
        Me.ToolTip1.SetToolTip(Me.btnLdON6, resources.GetString("btnLdON6.ToolTip"))
        Me.btnLdON6.UseVisualStyleBackColor = True
        '
        'btnLdOFF6
        '
        resources.ApplyResources(Me.btnLdOFF6, "btnLdOFF6")
        Me.btnLdOFF6.Name = "btnLdOFF6"
        Me.ToolTip1.SetToolTip(Me.btnLdOFF6, resources.GetString("btnLdOFF6.ToolTip"))
        Me.btnLdOFF6.UseVisualStyleBackColor = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        Me.ToolTip1.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip"))
        '
        'btnLdON5
        '
        resources.ApplyResources(Me.btnLdON5, "btnLdON5")
        Me.btnLdON5.Name = "btnLdON5"
        Me.ToolTip1.SetToolTip(Me.btnLdON5, resources.GetString("btnLdON5.ToolTip"))
        Me.btnLdON5.UseVisualStyleBackColor = True
        '
        'btnLdOFF5
        '
        resources.ApplyResources(Me.btnLdOFF5, "btnLdOFF5")
        Me.btnLdOFF5.Name = "btnLdOFF5"
        Me.ToolTip1.SetToolTip(Me.btnLdOFF5, resources.GetString("btnLdOFF5.ToolTip"))
        Me.btnLdOFF5.UseVisualStyleBackColor = True
        '
        'btnLdON4
        '
        resources.ApplyResources(Me.btnLdON4, "btnLdON4")
        Me.btnLdON4.Name = "btnLdON4"
        Me.ToolTip1.SetToolTip(Me.btnLdON4, resources.GetString("btnLdON4.ToolTip"))
        Me.btnLdON4.UseVisualStyleBackColor = True
        '
        'btnLdOFF4
        '
        resources.ApplyResources(Me.btnLdOFF4, "btnLdOFF4")
        Me.btnLdOFF4.Name = "btnLdOFF4"
        Me.ToolTip1.SetToolTip(Me.btnLdOFF4, resources.GetString("btnLdOFF4.ToolTip"))
        Me.btnLdOFF4.UseVisualStyleBackColor = True
        '
        'btnLdON3
        '
        resources.ApplyResources(Me.btnLdON3, "btnLdON3")
        Me.btnLdON3.Name = "btnLdON3"
        Me.ToolTip1.SetToolTip(Me.btnLdON3, resources.GetString("btnLdON3.ToolTip"))
        Me.btnLdON3.UseVisualStyleBackColor = True
        '
        'btnLdOFF3
        '
        resources.ApplyResources(Me.btnLdOFF3, "btnLdOFF3")
        Me.btnLdOFF3.Name = "btnLdOFF3"
        Me.ToolTip1.SetToolTip(Me.btnLdOFF3, resources.GetString("btnLdOFF3.ToolTip"))
        Me.btnLdOFF3.UseVisualStyleBackColor = True
        '
        'btnLdON2
        '
        resources.ApplyResources(Me.btnLdON2, "btnLdON2")
        Me.btnLdON2.Name = "btnLdON2"
        Me.ToolTip1.SetToolTip(Me.btnLdON2, resources.GetString("btnLdON2.ToolTip"))
        Me.btnLdON2.UseVisualStyleBackColor = True
        '
        'btnLdOFF2
        '
        resources.ApplyResources(Me.btnLdOFF2, "btnLdOFF2")
        Me.btnLdOFF2.Name = "btnLdOFF2"
        Me.ToolTip1.SetToolTip(Me.btnLdOFF2, resources.GetString("btnLdOFF2.ToolTip"))
        Me.btnLdOFF2.UseVisualStyleBackColor = True
        '
        'btnLdON1
        '
        resources.ApplyResources(Me.btnLdON1, "btnLdON1")
        Me.btnLdON1.Name = "btnLdON1"
        Me.ToolTip1.SetToolTip(Me.btnLdON1, resources.GetString("btnLdON1.ToolTip"))
        Me.btnLdON1.UseVisualStyleBackColor = True
        '
        'btnLdOFF1
        '
        resources.ApplyResources(Me.btnLdOFF1, "btnLdOFF1")
        Me.btnLdOFF1.Name = "btnLdOFF1"
        Me.ToolTip1.SetToolTip(Me.btnLdOFF1, resources.GetString("btnLdOFF1.ToolTip"))
        Me.btnLdOFF1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "ledHigh.png")
        Me.ImageList1.Images.SetKeyName(1, "ledLow.png")
        '
        'GroupBox7
        '
        resources.ApplyResources(Me.GroupBox7, "GroupBox7")
        Me.GroupBox7.Controls.Add(Me.btnUlON5)
        Me.GroupBox7.Controls.Add(Me.btnUlOFF5)
        Me.GroupBox7.Controls.Add(Me.Label28)
        Me.GroupBox7.Controls.Add(Me.picUL10)
        Me.GroupBox7.Controls.Add(Me.btnUlON4)
        Me.GroupBox7.Controls.Add(Me.btnUlOFF4)
        Me.GroupBox7.Controls.Add(Me.btnUlON3)
        Me.GroupBox7.Controls.Add(Me.btnUlOFF3)
        Me.GroupBox7.Controls.Add(Me.btnUlON2)
        Me.GroupBox7.Controls.Add(Me.btnUlOFF2)
        Me.GroupBox7.Controls.Add(Me.btnUlON1)
        Me.GroupBox7.Controls.Add(Me.btnUlOFF1)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.Controls.Add(Me.picUL9)
        Me.GroupBox7.Controls.Add(Me.Label23)
        Me.GroupBox7.Controls.Add(Me.Label24)
        Me.GroupBox7.Controls.Add(Me.Label25)
        Me.GroupBox7.Controls.Add(Me.Label26)
        Me.GroupBox7.Controls.Add(Me.Label27)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Controls.Add(Me.Label30)
        Me.GroupBox7.Controls.Add(Me.picUL8)
        Me.GroupBox7.Controls.Add(Me.picUL7)
        Me.GroupBox7.Controls.Add(Me.picUL6)
        Me.GroupBox7.Controls.Add(Me.picUL5)
        Me.GroupBox7.Controls.Add(Me.picUL4)
        Me.GroupBox7.Controls.Add(Me.picUL3)
        Me.GroupBox7.Controls.Add(Me.picUL2)
        Me.GroupBox7.Controls.Add(Me.picUL1)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox7, resources.GetString("GroupBox7.ToolTip"))
        '
        'btnUlON5
        '
        resources.ApplyResources(Me.btnUlON5, "btnUlON5")
        Me.btnUlON5.Name = "btnUlON5"
        Me.ToolTip1.SetToolTip(Me.btnUlON5, resources.GetString("btnUlON5.ToolTip"))
        Me.btnUlON5.UseVisualStyleBackColor = True
        '
        'btnUlOFF5
        '
        resources.ApplyResources(Me.btnUlOFF5, "btnUlOFF5")
        Me.btnUlOFF5.Name = "btnUlOFF5"
        Me.ToolTip1.SetToolTip(Me.btnUlOFF5, resources.GetString("btnUlOFF5.ToolTip"))
        Me.btnUlOFF5.UseVisualStyleBackColor = True
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.Name = "Label28"
        Me.ToolTip1.SetToolTip(Me.Label28, resources.GetString("Label28.ToolTip"))
        '
        'btnUlON4
        '
        resources.ApplyResources(Me.btnUlON4, "btnUlON4")
        Me.btnUlON4.Name = "btnUlON4"
        Me.ToolTip1.SetToolTip(Me.btnUlON4, resources.GetString("btnUlON4.ToolTip"))
        Me.btnUlON4.UseVisualStyleBackColor = True
        '
        'btnUlOFF4
        '
        resources.ApplyResources(Me.btnUlOFF4, "btnUlOFF4")
        Me.btnUlOFF4.Name = "btnUlOFF4"
        Me.ToolTip1.SetToolTip(Me.btnUlOFF4, resources.GetString("btnUlOFF4.ToolTip"))
        Me.btnUlOFF4.UseVisualStyleBackColor = True
        '
        'btnUlON3
        '
        resources.ApplyResources(Me.btnUlON3, "btnUlON3")
        Me.btnUlON3.Name = "btnUlON3"
        Me.ToolTip1.SetToolTip(Me.btnUlON3, resources.GetString("btnUlON3.ToolTip"))
        Me.btnUlON3.UseVisualStyleBackColor = True
        '
        'btnUlOFF3
        '
        resources.ApplyResources(Me.btnUlOFF3, "btnUlOFF3")
        Me.btnUlOFF3.Name = "btnUlOFF3"
        Me.ToolTip1.SetToolTip(Me.btnUlOFF3, resources.GetString("btnUlOFF3.ToolTip"))
        Me.btnUlOFF3.UseVisualStyleBackColor = True
        '
        'btnUlON2
        '
        resources.ApplyResources(Me.btnUlON2, "btnUlON2")
        Me.btnUlON2.Name = "btnUlON2"
        Me.ToolTip1.SetToolTip(Me.btnUlON2, resources.GetString("btnUlON2.ToolTip"))
        Me.btnUlON2.UseVisualStyleBackColor = True
        '
        'btnUlOFF2
        '
        resources.ApplyResources(Me.btnUlOFF2, "btnUlOFF2")
        Me.btnUlOFF2.Name = "btnUlOFF2"
        Me.ToolTip1.SetToolTip(Me.btnUlOFF2, resources.GetString("btnUlOFF2.ToolTip"))
        Me.btnUlOFF2.UseVisualStyleBackColor = True
        '
        'btnUlON1
        '
        resources.ApplyResources(Me.btnUlON1, "btnUlON1")
        Me.btnUlON1.Name = "btnUlON1"
        Me.ToolTip1.SetToolTip(Me.btnUlON1, resources.GetString("btnUlON1.ToolTip"))
        Me.btnUlON1.UseVisualStyleBackColor = True
        '
        'btnUlOFF1
        '
        resources.ApplyResources(Me.btnUlOFF1, "btnUlOFF1")
        Me.btnUlOFF1.Name = "btnUlOFF1"
        Me.ToolTip1.SetToolTip(Me.btnUlOFF1, resources.GetString("btnUlOFF1.ToolTip"))
        Me.btnUlOFF1.UseVisualStyleBackColor = True
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        Me.ToolTip1.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip"))
        '
        'Label22
        '
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.Name = "Label22"
        Me.ToolTip1.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip"))
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        Me.ToolTip1.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip"))
        '
        'Label24
        '
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.Name = "Label24"
        Me.ToolTip1.SetToolTip(Me.Label24, resources.GetString("Label24.ToolTip"))
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.Name = "Label25"
        Me.ToolTip1.SetToolTip(Me.Label25, resources.GetString("Label25.ToolTip"))
        '
        'Label26
        '
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.Name = "Label26"
        Me.ToolTip1.SetToolTip(Me.Label26, resources.GetString("Label26.ToolTip"))
        '
        'Label27
        '
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.Name = "Label27"
        Me.ToolTip1.SetToolTip(Me.Label27, resources.GetString("Label27.ToolTip"))
        '
        'Label29
        '
        resources.ApplyResources(Me.Label29, "Label29")
        Me.Label29.Name = "Label29"
        Me.ToolTip1.SetToolTip(Me.Label29, resources.GetString("Label29.ToolTip"))
        '
        'Label30
        '
        resources.ApplyResources(Me.Label30, "Label30")
        Me.Label30.Name = "Label30"
        Me.ToolTip1.SetToolTip(Me.Label30, resources.GetString("Label30.ToolTip"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        '
        'TabPage1
        '
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Controls.Add(Me.GroupBox8)
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Name = "TabPage1"
        Me.ToolTip1.SetToolTip(Me.TabPage1, resources.GetString("TabPage1.ToolTip"))
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        resources.ApplyResources(Me.GroupBox8, "GroupBox8")
        Me.GroupBox8.Controls.Add(Me.Label37)
        Me.GroupBox8.Controls.Add(Me.tbLDSlotStatus1)
        Me.GroupBox8.Controls.Add(Me.tbLDSlotStatus2)
        Me.GroupBox8.Controls.Add(Me.tbLDProductNumB)
        Me.GroupBox8.Controls.Add(Me.Label18)
        Me.GroupBox8.Controls.Add(Me.tbLDTargetTemp)
        Me.GroupBox8.Controls.Add(Me.tbLDPass)
        Me.GroupBox8.Controls.Add(Me.Label20)
        Me.GroupBox8.Controls.Add(Me.Label17)
        Me.GroupBox8.Controls.Add(Me.tbLDCaseteBarCode)
        Me.GroupBox8.Controls.Add(Me.tbLDProductCount)
        Me.GroupBox8.Controls.Add(Me.Label19)
        Me.GroupBox8.Controls.Add(Me.tbLDProductType)
        Me.GroupBox8.Controls.Add(Me.Label16)
        Me.GroupBox8.Controls.Add(Me.tbLDProductNumA)
        Me.GroupBox8.Controls.Add(Me.Label14)
        Me.GroupBox8.Controls.Add(Me.tbLDStatus)
        Me.GroupBox8.Controls.Add(Me.GroupBox10)
        Me.GroupBox8.Controls.Add(Me.GroupBox9)
        Me.GroupBox8.Controls.Add(Me.Label15)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox8, resources.GetString("GroupBox8.ToolTip"))
        '
        'Label37
        '
        resources.ApplyResources(Me.Label37, "Label37")
        Me.Label37.Name = "Label37"
        Me.ToolTip1.SetToolTip(Me.Label37, resources.GetString("Label37.ToolTip"))
        '
        'tbLDSlotStatus1
        '
        resources.ApplyResources(Me.tbLDSlotStatus1, "tbLDSlotStatus1")
        Me.tbLDSlotStatus1.Name = "tbLDSlotStatus1"
        Me.ToolTip1.SetToolTip(Me.tbLDSlotStatus1, resources.GetString("tbLDSlotStatus1.ToolTip"))
        '
        'tbLDSlotStatus2
        '
        resources.ApplyResources(Me.tbLDSlotStatus2, "tbLDSlotStatus2")
        Me.tbLDSlotStatus2.Name = "tbLDSlotStatus2"
        Me.ToolTip1.SetToolTip(Me.tbLDSlotStatus2, resources.GetString("tbLDSlotStatus2.ToolTip"))
        '
        'tbLDProductNumB
        '
        resources.ApplyResources(Me.tbLDProductNumB, "tbLDProductNumB")
        Me.tbLDProductNumB.Name = "tbLDProductNumB"
        Me.ToolTip1.SetToolTip(Me.tbLDProductNumB, resources.GetString("tbLDProductNumB.ToolTip"))
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        Me.ToolTip1.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip"))
        '
        'tbLDTargetTemp
        '
        resources.ApplyResources(Me.tbLDTargetTemp, "tbLDTargetTemp")
        Me.tbLDTargetTemp.Name = "tbLDTargetTemp"
        Me.ToolTip1.SetToolTip(Me.tbLDTargetTemp, resources.GetString("tbLDTargetTemp.ToolTip"))
        '
        'tbLDPass
        '
        resources.ApplyResources(Me.tbLDPass, "tbLDPass")
        Me.tbLDPass.Name = "tbLDPass"
        Me.ToolTip1.SetToolTip(Me.tbLDPass, resources.GetString("tbLDPass.ToolTip"))
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        Me.ToolTip1.SetToolTip(Me.Label20, resources.GetString("Label20.ToolTip"))
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        Me.ToolTip1.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip"))
        '
        'tbLDCaseteBarCode
        '
        resources.ApplyResources(Me.tbLDCaseteBarCode, "tbLDCaseteBarCode")
        Me.tbLDCaseteBarCode.Name = "tbLDCaseteBarCode"
        Me.ToolTip1.SetToolTip(Me.tbLDCaseteBarCode, resources.GetString("tbLDCaseteBarCode.ToolTip"))
        '
        'tbLDProductCount
        '
        resources.ApplyResources(Me.tbLDProductCount, "tbLDProductCount")
        Me.tbLDProductCount.Name = "tbLDProductCount"
        Me.ToolTip1.SetToolTip(Me.tbLDProductCount, resources.GetString("tbLDProductCount.ToolTip"))
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        Me.ToolTip1.SetToolTip(Me.Label19, resources.GetString("Label19.ToolTip"))
        '
        'tbLDProductType
        '
        resources.ApplyResources(Me.tbLDProductType, "tbLDProductType")
        Me.tbLDProductType.Name = "tbLDProductType"
        Me.ToolTip1.SetToolTip(Me.tbLDProductType, resources.GetString("tbLDProductType.ToolTip"))
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        Me.ToolTip1.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip"))
        '
        'tbLDProductNumA
        '
        resources.ApplyResources(Me.tbLDProductNumA, "tbLDProductNumA")
        Me.tbLDProductNumA.Name = "tbLDProductNumA"
        Me.ToolTip1.SetToolTip(Me.tbLDProductNumA, resources.GetString("tbLDProductNumA.ToolTip"))
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        '
        'tbLDStatus
        '
        resources.ApplyResources(Me.tbLDStatus, "tbLDStatus")
        Me.tbLDStatus.Name = "tbLDStatus"
        Me.ToolTip1.SetToolTip(Me.tbLDStatus, resources.GetString("tbLDStatus.ToolTip"))
        '
        'GroupBox10
        '
        resources.ApplyResources(Me.GroupBox10, "GroupBox10")
        Me.GroupBox10.Controls.Add(Me.tbLDAlarmCode1)
        Me.GroupBox10.Controls.Add(Me.tbLDAlarmCode2)
        Me.GroupBox10.Controls.Add(Me.tbLDAlarmCode3)
        Me.GroupBox10.Controls.Add(Me.tbLDAlarmCode4)
        Me.GroupBox10.Controls.Add(Me.tbLDAlarmCode5)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox10, resources.GetString("GroupBox10.ToolTip"))
        '
        'tbLDAlarmCode1
        '
        resources.ApplyResources(Me.tbLDAlarmCode1, "tbLDAlarmCode1")
        Me.tbLDAlarmCode1.Name = "tbLDAlarmCode1"
        Me.ToolTip1.SetToolTip(Me.tbLDAlarmCode1, resources.GetString("tbLDAlarmCode1.ToolTip"))
        '
        'tbLDAlarmCode2
        '
        resources.ApplyResources(Me.tbLDAlarmCode2, "tbLDAlarmCode2")
        Me.tbLDAlarmCode2.Name = "tbLDAlarmCode2"
        Me.ToolTip1.SetToolTip(Me.tbLDAlarmCode2, resources.GetString("tbLDAlarmCode2.ToolTip"))
        '
        'tbLDAlarmCode3
        '
        resources.ApplyResources(Me.tbLDAlarmCode3, "tbLDAlarmCode3")
        Me.tbLDAlarmCode3.Name = "tbLDAlarmCode3"
        Me.ToolTip1.SetToolTip(Me.tbLDAlarmCode3, resources.GetString("tbLDAlarmCode3.ToolTip"))
        '
        'tbLDAlarmCode4
        '
        resources.ApplyResources(Me.tbLDAlarmCode4, "tbLDAlarmCode4")
        Me.tbLDAlarmCode4.Name = "tbLDAlarmCode4"
        Me.ToolTip1.SetToolTip(Me.tbLDAlarmCode4, resources.GetString("tbLDAlarmCode4.ToolTip"))
        '
        'tbLDAlarmCode5
        '
        resources.ApplyResources(Me.tbLDAlarmCode5, "tbLDAlarmCode5")
        Me.tbLDAlarmCode5.Name = "tbLDAlarmCode5"
        Me.ToolTip1.SetToolTip(Me.tbLDAlarmCode5, resources.GetString("tbLDAlarmCode5.ToolTip"))
        '
        'GroupBox9
        '
        resources.ApplyResources(Me.GroupBox9, "GroupBox9")
        Me.GroupBox9.Controls.Add(Me.tbLDTemp1)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp2)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp7)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp8)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp3)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp4)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp9)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp10)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp12)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp5)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp6)
        Me.GroupBox9.Controls.Add(Me.tbLDTemp11)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox9, resources.GetString("GroupBox9.ToolTip"))
        '
        'tbLDTemp1
        '
        resources.ApplyResources(Me.tbLDTemp1, "tbLDTemp1")
        Me.tbLDTemp1.Name = "tbLDTemp1"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp1, resources.GetString("tbLDTemp1.ToolTip"))
        '
        'tbLDTemp2
        '
        resources.ApplyResources(Me.tbLDTemp2, "tbLDTemp2")
        Me.tbLDTemp2.Name = "tbLDTemp2"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp2, resources.GetString("tbLDTemp2.ToolTip"))
        '
        'tbLDTemp7
        '
        resources.ApplyResources(Me.tbLDTemp7, "tbLDTemp7")
        Me.tbLDTemp7.Name = "tbLDTemp7"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp7, resources.GetString("tbLDTemp7.ToolTip"))
        '
        'tbLDTemp8
        '
        resources.ApplyResources(Me.tbLDTemp8, "tbLDTemp8")
        Me.tbLDTemp8.Name = "tbLDTemp8"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp8, resources.GetString("tbLDTemp8.ToolTip"))
        '
        'tbLDTemp3
        '
        resources.ApplyResources(Me.tbLDTemp3, "tbLDTemp3")
        Me.tbLDTemp3.Name = "tbLDTemp3"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp3, resources.GetString("tbLDTemp3.ToolTip"))
        '
        'tbLDTemp4
        '
        resources.ApplyResources(Me.tbLDTemp4, "tbLDTemp4")
        Me.tbLDTemp4.Name = "tbLDTemp4"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp4, resources.GetString("tbLDTemp4.ToolTip"))
        '
        'tbLDTemp9
        '
        resources.ApplyResources(Me.tbLDTemp9, "tbLDTemp9")
        Me.tbLDTemp9.Name = "tbLDTemp9"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp9, resources.GetString("tbLDTemp9.ToolTip"))
        '
        'tbLDTemp10
        '
        resources.ApplyResources(Me.tbLDTemp10, "tbLDTemp10")
        Me.tbLDTemp10.Name = "tbLDTemp10"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp10, resources.GetString("tbLDTemp10.ToolTip"))
        '
        'tbLDTemp12
        '
        resources.ApplyResources(Me.tbLDTemp12, "tbLDTemp12")
        Me.tbLDTemp12.Name = "tbLDTemp12"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp12, resources.GetString("tbLDTemp12.ToolTip"))
        '
        'tbLDTemp5
        '
        resources.ApplyResources(Me.tbLDTemp5, "tbLDTemp5")
        Me.tbLDTemp5.Name = "tbLDTemp5"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp5, resources.GetString("tbLDTemp5.ToolTip"))
        '
        'tbLDTemp6
        '
        resources.ApplyResources(Me.tbLDTemp6, "tbLDTemp6")
        Me.tbLDTemp6.Name = "tbLDTemp6"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp6, resources.GetString("tbLDTemp6.ToolTip"))
        '
        'tbLDTemp11
        '
        resources.ApplyResources(Me.tbLDTemp11, "tbLDTemp11")
        Me.tbLDTemp11.Name = "tbLDTemp11"
        Me.ToolTip1.SetToolTip(Me.tbLDTemp11, resources.GetString("tbLDTemp11.ToolTip"))
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        Me.ToolTip1.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip"))
        '
        'TabPage2
        '
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Controls.Add(Me.GroupBox12)
        Me.TabPage2.Controls.Add(Me.GroupBox7)
        Me.TabPage2.Name = "TabPage2"
        Me.ToolTip1.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip"))
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        resources.ApplyResources(Me.GroupBox12, "GroupBox12")
        Me.GroupBox12.Controls.Add(Me.tbULSlotStatus1)
        Me.GroupBox12.Controls.Add(Me.tbULSlotStatus2)
        Me.GroupBox12.Controls.Add(Me.Label21)
        Me.GroupBox12.Controls.Add(Me.tbULTargetTemp)
        Me.GroupBox12.Controls.Add(Me.tbULPass)
        Me.GroupBox12.Controls.Add(Me.Label31)
        Me.GroupBox12.Controls.Add(Me.Label32)
        Me.GroupBox12.Controls.Add(Me.tbULCaseteBarCode)
        Me.GroupBox12.Controls.Add(Me.tbULProductCount)
        Me.GroupBox12.Controls.Add(Me.Label33)
        Me.GroupBox12.Controls.Add(Me.tbULProductType)
        Me.GroupBox12.Controls.Add(Me.Label34)
        Me.GroupBox12.Controls.Add(Me.tbULProductNum)
        Me.GroupBox12.Controls.Add(Me.Label35)
        Me.GroupBox12.Controls.Add(Me.tbULStatus)
        Me.GroupBox12.Controls.Add(Me.GroupBox14)
        Me.GroupBox12.Controls.Add(Me.GroupBox15)
        Me.GroupBox12.Controls.Add(Me.Label36)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox12, resources.GetString("GroupBox12.ToolTip"))
        '
        'tbULSlotStatus1
        '
        resources.ApplyResources(Me.tbULSlotStatus1, "tbULSlotStatus1")
        Me.tbULSlotStatus1.Name = "tbULSlotStatus1"
        Me.ToolTip1.SetToolTip(Me.tbULSlotStatus1, resources.GetString("tbULSlotStatus1.ToolTip"))
        '
        'tbULSlotStatus2
        '
        resources.ApplyResources(Me.tbULSlotStatus2, "tbULSlotStatus2")
        Me.tbULSlotStatus2.Name = "tbULSlotStatus2"
        Me.ToolTip1.SetToolTip(Me.tbULSlotStatus2, resources.GetString("tbULSlotStatus2.ToolTip"))
        '
        'Label21
        '
        resources.ApplyResources(Me.Label21, "Label21")
        Me.Label21.Name = "Label21"
        Me.ToolTip1.SetToolTip(Me.Label21, resources.GetString("Label21.ToolTip"))
        '
        'tbULTargetTemp
        '
        resources.ApplyResources(Me.tbULTargetTemp, "tbULTargetTemp")
        Me.tbULTargetTemp.Name = "tbULTargetTemp"
        Me.ToolTip1.SetToolTip(Me.tbULTargetTemp, resources.GetString("tbULTargetTemp.ToolTip"))
        '
        'tbULPass
        '
        resources.ApplyResources(Me.tbULPass, "tbULPass")
        Me.tbULPass.Name = "tbULPass"
        Me.ToolTip1.SetToolTip(Me.tbULPass, resources.GetString("tbULPass.ToolTip"))
        '
        'Label31
        '
        resources.ApplyResources(Me.Label31, "Label31")
        Me.Label31.Name = "Label31"
        Me.ToolTip1.SetToolTip(Me.Label31, resources.GetString("Label31.ToolTip"))
        '
        'Label32
        '
        resources.ApplyResources(Me.Label32, "Label32")
        Me.Label32.Name = "Label32"
        Me.ToolTip1.SetToolTip(Me.Label32, resources.GetString("Label32.ToolTip"))
        '
        'tbULCaseteBarCode
        '
        resources.ApplyResources(Me.tbULCaseteBarCode, "tbULCaseteBarCode")
        Me.tbULCaseteBarCode.Name = "tbULCaseteBarCode"
        Me.ToolTip1.SetToolTip(Me.tbULCaseteBarCode, resources.GetString("tbULCaseteBarCode.ToolTip"))
        '
        'tbULProductCount
        '
        resources.ApplyResources(Me.tbULProductCount, "tbULProductCount")
        Me.tbULProductCount.Name = "tbULProductCount"
        Me.ToolTip1.SetToolTip(Me.tbULProductCount, resources.GetString("tbULProductCount.ToolTip"))
        '
        'Label33
        '
        resources.ApplyResources(Me.Label33, "Label33")
        Me.Label33.Name = "Label33"
        Me.ToolTip1.SetToolTip(Me.Label33, resources.GetString("Label33.ToolTip"))
        '
        'tbULProductType
        '
        resources.ApplyResources(Me.tbULProductType, "tbULProductType")
        Me.tbULProductType.Name = "tbULProductType"
        Me.ToolTip1.SetToolTip(Me.tbULProductType, resources.GetString("tbULProductType.ToolTip"))
        '
        'Label34
        '
        resources.ApplyResources(Me.Label34, "Label34")
        Me.Label34.Name = "Label34"
        Me.ToolTip1.SetToolTip(Me.Label34, resources.GetString("Label34.ToolTip"))
        '
        'tbULProductNum
        '
        resources.ApplyResources(Me.tbULProductNum, "tbULProductNum")
        Me.tbULProductNum.Name = "tbULProductNum"
        Me.ToolTip1.SetToolTip(Me.tbULProductNum, resources.GetString("tbULProductNum.ToolTip"))
        '
        'Label35
        '
        resources.ApplyResources(Me.Label35, "Label35")
        Me.Label35.Name = "Label35"
        Me.ToolTip1.SetToolTip(Me.Label35, resources.GetString("Label35.ToolTip"))
        '
        'tbULStatus
        '
        resources.ApplyResources(Me.tbULStatus, "tbULStatus")
        Me.tbULStatus.Name = "tbULStatus"
        Me.ToolTip1.SetToolTip(Me.tbULStatus, resources.GetString("tbULStatus.ToolTip"))
        '
        'GroupBox14
        '
        resources.ApplyResources(Me.GroupBox14, "GroupBox14")
        Me.GroupBox14.Controls.Add(Me.tbULAlarmCode1)
        Me.GroupBox14.Controls.Add(Me.tbULAlarmCode2)
        Me.GroupBox14.Controls.Add(Me.tbULAlarmCode3)
        Me.GroupBox14.Controls.Add(Me.tbULAlarmCode4)
        Me.GroupBox14.Controls.Add(Me.tbULAlarmCode5)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox14, resources.GetString("GroupBox14.ToolTip"))
        '
        'tbULAlarmCode1
        '
        resources.ApplyResources(Me.tbULAlarmCode1, "tbULAlarmCode1")
        Me.tbULAlarmCode1.Name = "tbULAlarmCode1"
        Me.ToolTip1.SetToolTip(Me.tbULAlarmCode1, resources.GetString("tbULAlarmCode1.ToolTip"))
        '
        'tbULAlarmCode2
        '
        resources.ApplyResources(Me.tbULAlarmCode2, "tbULAlarmCode2")
        Me.tbULAlarmCode2.Name = "tbULAlarmCode2"
        Me.ToolTip1.SetToolTip(Me.tbULAlarmCode2, resources.GetString("tbULAlarmCode2.ToolTip"))
        '
        'tbULAlarmCode3
        '
        resources.ApplyResources(Me.tbULAlarmCode3, "tbULAlarmCode3")
        Me.tbULAlarmCode3.Name = "tbULAlarmCode3"
        Me.ToolTip1.SetToolTip(Me.tbULAlarmCode3, resources.GetString("tbULAlarmCode3.ToolTip"))
        '
        'tbULAlarmCode4
        '
        resources.ApplyResources(Me.tbULAlarmCode4, "tbULAlarmCode4")
        Me.tbULAlarmCode4.Name = "tbULAlarmCode4"
        Me.ToolTip1.SetToolTip(Me.tbULAlarmCode4, resources.GetString("tbULAlarmCode4.ToolTip"))
        '
        'tbULAlarmCode5
        '
        resources.ApplyResources(Me.tbULAlarmCode5, "tbULAlarmCode5")
        Me.tbULAlarmCode5.Name = "tbULAlarmCode5"
        Me.ToolTip1.SetToolTip(Me.tbULAlarmCode5, resources.GetString("tbULAlarmCode5.ToolTip"))
        '
        'GroupBox15
        '
        resources.ApplyResources(Me.GroupBox15, "GroupBox15")
        Me.GroupBox15.Controls.Add(Me.tbULTemp1)
        Me.GroupBox15.Controls.Add(Me.tbULTemp2)
        Me.GroupBox15.Controls.Add(Me.tbULTemp7)
        Me.GroupBox15.Controls.Add(Me.tbULTemp8)
        Me.GroupBox15.Controls.Add(Me.tbULTemp3)
        Me.GroupBox15.Controls.Add(Me.tbULTemp4)
        Me.GroupBox15.Controls.Add(Me.tbULTemp9)
        Me.GroupBox15.Controls.Add(Me.tbULTemp10)
        Me.GroupBox15.Controls.Add(Me.tbULTemp12)
        Me.GroupBox15.Controls.Add(Me.tbULTemp5)
        Me.GroupBox15.Controls.Add(Me.tbULTemp6)
        Me.GroupBox15.Controls.Add(Me.tbULTemp11)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox15, resources.GetString("GroupBox15.ToolTip"))
        '
        'tbULTemp1
        '
        resources.ApplyResources(Me.tbULTemp1, "tbULTemp1")
        Me.tbULTemp1.Name = "tbULTemp1"
        Me.ToolTip1.SetToolTip(Me.tbULTemp1, resources.GetString("tbULTemp1.ToolTip"))
        '
        'tbULTemp2
        '
        resources.ApplyResources(Me.tbULTemp2, "tbULTemp2")
        Me.tbULTemp2.Name = "tbULTemp2"
        Me.ToolTip1.SetToolTip(Me.tbULTemp2, resources.GetString("tbULTemp2.ToolTip"))
        '
        'tbULTemp7
        '
        resources.ApplyResources(Me.tbULTemp7, "tbULTemp7")
        Me.tbULTemp7.Name = "tbULTemp7"
        Me.ToolTip1.SetToolTip(Me.tbULTemp7, resources.GetString("tbULTemp7.ToolTip"))
        '
        'tbULTemp8
        '
        resources.ApplyResources(Me.tbULTemp8, "tbULTemp8")
        Me.tbULTemp8.Name = "tbULTemp8"
        Me.ToolTip1.SetToolTip(Me.tbULTemp8, resources.GetString("tbULTemp8.ToolTip"))
        '
        'tbULTemp3
        '
        resources.ApplyResources(Me.tbULTemp3, "tbULTemp3")
        Me.tbULTemp3.Name = "tbULTemp3"
        Me.ToolTip1.SetToolTip(Me.tbULTemp3, resources.GetString("tbULTemp3.ToolTip"))
        '
        'tbULTemp4
        '
        resources.ApplyResources(Me.tbULTemp4, "tbULTemp4")
        Me.tbULTemp4.Name = "tbULTemp4"
        Me.ToolTip1.SetToolTip(Me.tbULTemp4, resources.GetString("tbULTemp4.ToolTip"))
        '
        'tbULTemp9
        '
        resources.ApplyResources(Me.tbULTemp9, "tbULTemp9")
        Me.tbULTemp9.Name = "tbULTemp9"
        Me.ToolTip1.SetToolTip(Me.tbULTemp9, resources.GetString("tbULTemp9.ToolTip"))
        '
        'tbULTemp10
        '
        resources.ApplyResources(Me.tbULTemp10, "tbULTemp10")
        Me.tbULTemp10.Name = "tbULTemp10"
        Me.ToolTip1.SetToolTip(Me.tbULTemp10, resources.GetString("tbULTemp10.ToolTip"))
        '
        'tbULTemp12
        '
        resources.ApplyResources(Me.tbULTemp12, "tbULTemp12")
        Me.tbULTemp12.Name = "tbULTemp12"
        Me.ToolTip1.SetToolTip(Me.tbULTemp12, resources.GetString("tbULTemp12.ToolTip"))
        '
        'tbULTemp5
        '
        resources.ApplyResources(Me.tbULTemp5, "tbULTemp5")
        Me.tbULTemp5.Name = "tbULTemp5"
        Me.ToolTip1.SetToolTip(Me.tbULTemp5, resources.GetString("tbULTemp5.ToolTip"))
        '
        'tbULTemp6
        '
        resources.ApplyResources(Me.tbULTemp6, "tbULTemp6")
        Me.tbULTemp6.Name = "tbULTemp6"
        Me.ToolTip1.SetToolTip(Me.tbULTemp6, resources.GetString("tbULTemp6.ToolTip"))
        '
        'tbULTemp11
        '
        resources.ApplyResources(Me.tbULTemp11, "tbULTemp11")
        Me.tbULTemp11.Name = "tbULTemp11"
        Me.ToolTip1.SetToolTip(Me.tbULTemp11, resources.GetString("tbULTemp11.ToolTip"))
        '
        'Label36
        '
        resources.ApplyResources(Me.Label36, "Label36")
        Me.Label36.Name = "Label36"
        Me.ToolTip1.SetToolTip(Me.Label36, resources.GetString("Label36.ToolTip"))
        '
        'GroupBox16
        '
        resources.ApplyResources(Me.GroupBox16, "GroupBox16")
        Me.GroupBox16.Controls.Add(Me.btnSetLastNum)
        Me.GroupBox16.Controls.Add(Me.tbSetProductNum)
        Me.GroupBox16.Controls.Add(Me.btnGetAlarmCode)
        Me.GroupBox16.Controls.Add(Me.btnCassetteAbort)
        Me.GroupBox16.Controls.Add(Me.btnSetProductNum)
        Me.GroupBox16.Controls.Add(Me.tbSetTargetTemp)
        Me.GroupBox16.Controls.Add(Me.tbSetProductType)
        Me.GroupBox16.Controls.Add(Me.chkbPass)
        Me.GroupBox16.Controls.Add(Me.btnGetProductNum)
        Me.GroupBox16.Controls.Add(Me.btnGetCstBarCode)
        Me.GroupBox16.Controls.Add(Me.btnGetCastetData)
        Me.GroupBox16.Controls.Add(Me.btnSetTargetTemp)
        Me.GroupBox16.Controls.Add(Me.btnSetProductType)
        Me.GroupBox16.Controls.Add(Me.btnSetPassModel)
        Me.GroupBox16.Controls.Add(Me.rbUnloader)
        Me.GroupBox16.Controls.Add(Me.rbLoader)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox16, resources.GetString("GroupBox16.ToolTip"))
        '
        'btnSetLastNum
        '
        resources.ApplyResources(Me.btnSetLastNum, "btnSetLastNum")
        Me.btnSetLastNum.Name = "btnSetLastNum"
        Me.ToolTip1.SetToolTip(Me.btnSetLastNum, resources.GetString("btnSetLastNum.ToolTip"))
        Me.btnSetLastNum.UseVisualStyleBackColor = True
        '
        'tbSetProductNum
        '
        resources.ApplyResources(Me.tbSetProductNum, "tbSetProductNum")
        Me.tbSetProductNum.Name = "tbSetProductNum"
        Me.ToolTip1.SetToolTip(Me.tbSetProductNum, resources.GetString("tbSetProductNum.ToolTip"))
        '
        'btnGetAlarmCode
        '
        resources.ApplyResources(Me.btnGetAlarmCode, "btnGetAlarmCode")
        Me.btnGetAlarmCode.Name = "btnGetAlarmCode"
        Me.ToolTip1.SetToolTip(Me.btnGetAlarmCode, resources.GetString("btnGetAlarmCode.ToolTip"))
        Me.btnGetAlarmCode.UseVisualStyleBackColor = True
        '
        'btnCassetteAbort
        '
        resources.ApplyResources(Me.btnCassetteAbort, "btnCassetteAbort")
        Me.btnCassetteAbort.Name = "btnCassetteAbort"
        Me.ToolTip1.SetToolTip(Me.btnCassetteAbort, resources.GetString("btnCassetteAbort.ToolTip"))
        Me.btnCassetteAbort.UseVisualStyleBackColor = True
        '
        'btnSetProductNum
        '
        resources.ApplyResources(Me.btnSetProductNum, "btnSetProductNum")
        Me.btnSetProductNum.Name = "btnSetProductNum"
        Me.ToolTip1.SetToolTip(Me.btnSetProductNum, resources.GetString("btnSetProductNum.ToolTip"))
        Me.btnSetProductNum.UseVisualStyleBackColor = True
        '
        'tbSetTargetTemp
        '
        resources.ApplyResources(Me.tbSetTargetTemp, "tbSetTargetTemp")
        Me.tbSetTargetTemp.Name = "tbSetTargetTemp"
        Me.ToolTip1.SetToolTip(Me.tbSetTargetTemp, resources.GetString("tbSetTargetTemp.ToolTip"))
        '
        'tbSetProductType
        '
        resources.ApplyResources(Me.tbSetProductType, "tbSetProductType")
        Me.tbSetProductType.Name = "tbSetProductType"
        Me.ToolTip1.SetToolTip(Me.tbSetProductType, resources.GetString("tbSetProductType.ToolTip"))
        '
        'chkbPass
        '
        resources.ApplyResources(Me.chkbPass, "chkbPass")
        Me.chkbPass.Name = "chkbPass"
        Me.ToolTip1.SetToolTip(Me.chkbPass, resources.GetString("chkbPass.ToolTip"))
        Me.chkbPass.UseVisualStyleBackColor = True
        '
        'btnGetProductNum
        '
        resources.ApplyResources(Me.btnGetProductNum, "btnGetProductNum")
        Me.btnGetProductNum.Name = "btnGetProductNum"
        Me.ToolTip1.SetToolTip(Me.btnGetProductNum, resources.GetString("btnGetProductNum.ToolTip"))
        Me.btnGetProductNum.UseVisualStyleBackColor = True
        '
        'btnGetCstBarCode
        '
        resources.ApplyResources(Me.btnGetCstBarCode, "btnGetCstBarCode")
        Me.btnGetCstBarCode.Name = "btnGetCstBarCode"
        Me.ToolTip1.SetToolTip(Me.btnGetCstBarCode, resources.GetString("btnGetCstBarCode.ToolTip"))
        Me.btnGetCstBarCode.UseVisualStyleBackColor = True
        '
        'btnGetCastetData
        '
        resources.ApplyResources(Me.btnGetCastetData, "btnGetCastetData")
        Me.btnGetCastetData.Name = "btnGetCastetData"
        Me.ToolTip1.SetToolTip(Me.btnGetCastetData, resources.GetString("btnGetCastetData.ToolTip"))
        Me.btnGetCastetData.UseVisualStyleBackColor = True
        '
        'btnSetTargetTemp
        '
        resources.ApplyResources(Me.btnSetTargetTemp, "btnSetTargetTemp")
        Me.btnSetTargetTemp.Name = "btnSetTargetTemp"
        Me.ToolTip1.SetToolTip(Me.btnSetTargetTemp, resources.GetString("btnSetTargetTemp.ToolTip"))
        Me.btnSetTargetTemp.UseVisualStyleBackColor = True
        '
        'btnSetProductType
        '
        resources.ApplyResources(Me.btnSetProductType, "btnSetProductType")
        Me.btnSetProductType.Name = "btnSetProductType"
        Me.ToolTip1.SetToolTip(Me.btnSetProductType, resources.GetString("btnSetProductType.ToolTip"))
        Me.btnSetProductType.UseVisualStyleBackColor = True
        '
        'btnSetPassModel
        '
        resources.ApplyResources(Me.btnSetPassModel, "btnSetPassModel")
        Me.btnSetPassModel.Name = "btnSetPassModel"
        Me.ToolTip1.SetToolTip(Me.btnSetPassModel, resources.GetString("btnSetPassModel.ToolTip"))
        Me.btnSetPassModel.UseVisualStyleBackColor = True
        '
        'rbUnloader
        '
        resources.ApplyResources(Me.rbUnloader, "rbUnloader")
        Me.rbUnloader.Name = "rbUnloader"
        Me.ToolTip1.SetToolTip(Me.rbUnloader, resources.GetString("rbUnloader.ToolTip"))
        Me.rbUnloader.UseVisualStyleBackColor = True
        '
        'rbLoader
        '
        resources.ApplyResources(Me.rbLoader, "rbLoader")
        Me.rbLoader.Checked = True
        Me.rbLoader.Name = "rbLoader"
        Me.rbLoader.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbLoader, resources.GetString("rbLoader.ToolTip"))
        Me.rbLoader.UseVisualStyleBackColor = True
        '
        'imgListItems
        '
        Me.imgListItems.ImageStream = CType(resources.GetObject("imgListItems.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgListItems.TransparentColor = System.Drawing.Color.Transparent
        Me.imgListItems.Images.SetKeyName(0, "Light-Green01.png")
        Me.imgListItems.Images.SetKeyName(1, "Light-Black01.png")
        Me.imgListItems.Images.SetKeyName(2, "Light-Red01.png")
        '
        'btnExit
        '
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.BackgroundImage = Global.WetcoConveyor.My.Resources.Resources.CancelExit
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'picLD12
        '
        resources.ApplyResources(Me.picLD12, "picLD12")
        Me.picLD12.Name = "picLD12"
        Me.picLD12.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD12, resources.GetString("picLD12.ToolTip"))
        '
        'picLD11
        '
        resources.ApplyResources(Me.picLD11, "picLD11")
        Me.picLD11.Name = "picLD11"
        Me.picLD11.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD11, resources.GetString("picLD11.ToolTip"))
        '
        'picLD10
        '
        resources.ApplyResources(Me.picLD10, "picLD10")
        Me.picLD10.Name = "picLD10"
        Me.picLD10.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD10, resources.GetString("picLD10.ToolTip"))
        '
        'picLD9
        '
        resources.ApplyResources(Me.picLD9, "picLD9")
        Me.picLD9.Name = "picLD9"
        Me.picLD9.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD9, resources.GetString("picLD9.ToolTip"))
        '
        'picLD8
        '
        resources.ApplyResources(Me.picLD8, "picLD8")
        Me.picLD8.Name = "picLD8"
        Me.picLD8.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD8, resources.GetString("picLD8.ToolTip"))
        '
        'picLD7
        '
        resources.ApplyResources(Me.picLD7, "picLD7")
        Me.picLD7.Name = "picLD7"
        Me.picLD7.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD7, resources.GetString("picLD7.ToolTip"))
        '
        'picLD6
        '
        resources.ApplyResources(Me.picLD6, "picLD6")
        Me.picLD6.Name = "picLD6"
        Me.picLD6.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD6, resources.GetString("picLD6.ToolTip"))
        '
        'picLD5
        '
        resources.ApplyResources(Me.picLD5, "picLD5")
        Me.picLD5.Name = "picLD5"
        Me.picLD5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD5, resources.GetString("picLD5.ToolTip"))
        '
        'picLD4
        '
        resources.ApplyResources(Me.picLD4, "picLD4")
        Me.picLD4.Name = "picLD4"
        Me.picLD4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD4, resources.GetString("picLD4.ToolTip"))
        '
        'picLD3
        '
        resources.ApplyResources(Me.picLD3, "picLD3")
        Me.picLD3.Name = "picLD3"
        Me.picLD3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD3, resources.GetString("picLD3.ToolTip"))
        '
        'picLD2
        '
        resources.ApplyResources(Me.picLD2, "picLD2")
        Me.picLD2.Name = "picLD2"
        Me.picLD2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD2, resources.GetString("picLD2.ToolTip"))
        '
        'picLD1
        '
        resources.ApplyResources(Me.picLD1, "picLD1")
        Me.picLD1.Name = "picLD1"
        Me.picLD1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLD1, resources.GetString("picLD1.ToolTip"))
        '
        'picUL10
        '
        resources.ApplyResources(Me.picUL10, "picUL10")
        Me.picUL10.Name = "picUL10"
        Me.picUL10.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL10, resources.GetString("picUL10.ToolTip"))
        '
        'picUL9
        '
        resources.ApplyResources(Me.picUL9, "picUL9")
        Me.picUL9.Name = "picUL9"
        Me.picUL9.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL9, resources.GetString("picUL9.ToolTip"))
        '
        'picUL8
        '
        resources.ApplyResources(Me.picUL8, "picUL8")
        Me.picUL8.Name = "picUL8"
        Me.picUL8.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL8, resources.GetString("picUL8.ToolTip"))
        '
        'picUL7
        '
        resources.ApplyResources(Me.picUL7, "picUL7")
        Me.picUL7.Name = "picUL7"
        Me.picUL7.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL7, resources.GetString("picUL7.ToolTip"))
        '
        'picUL6
        '
        resources.ApplyResources(Me.picUL6, "picUL6")
        Me.picUL6.Name = "picUL6"
        Me.picUL6.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL6, resources.GetString("picUL6.ToolTip"))
        '
        'picUL5
        '
        resources.ApplyResources(Me.picUL5, "picUL5")
        Me.picUL5.Name = "picUL5"
        Me.picUL5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL5, resources.GetString("picUL5.ToolTip"))
        '
        'picUL4
        '
        resources.ApplyResources(Me.picUL4, "picUL4")
        Me.picUL4.Name = "picUL4"
        Me.picUL4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL4, resources.GetString("picUL4.ToolTip"))
        '
        'picUL3
        '
        resources.ApplyResources(Me.picUL3, "picUL3")
        Me.picUL3.Name = "picUL3"
        Me.picUL3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL3, resources.GetString("picUL3.ToolTip"))
        '
        'picUL2
        '
        resources.ApplyResources(Me.picUL2, "picUL2")
        Me.picUL2.Name = "picUL2"
        Me.picUL2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL2, resources.GetString("picUL2.ToolTip"))
        '
        'picUL1
        '
        resources.ApplyResources(Me.picUL1, "picUL1")
        Me.picUL1.Name = "picUL1"
        Me.picUL1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picUL1, resources.GetString("picUL1.ToolTip"))
        '
        'frmTest
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox16)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTest"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        CType(Me.picLD12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLD1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUL1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBUnload As System.Windows.Forms.Button
    Friend WithEvents btnBLoad As System.Windows.Forms.Button
    Friend WithEvents btnAUnload As System.Windows.Forms.Button
    Friend WithEvents btnALoad As System.Windows.Forms.Button
    Friend WithEvents timerCheckIO As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Private WithEvents picLD8 As System.Windows.Forms.PictureBox
    Private WithEvents picLD7 As System.Windows.Forms.PictureBox
    Private WithEvents picLD6 As System.Windows.Forms.PictureBox
    Private WithEvents picLD5 As System.Windows.Forms.PictureBox
    Private WithEvents picLD4 As System.Windows.Forms.PictureBox
    Private WithEvents picLD3 As System.Windows.Forms.PictureBox
    Private WithEvents picLD2 As System.Windows.Forms.PictureBox
    Private WithEvents picLD1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents picLD9 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents picUL9 As System.Windows.Forms.PictureBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents picUL8 As System.Windows.Forms.PictureBox
    Private WithEvents picUL7 As System.Windows.Forms.PictureBox
    Private WithEvents picUL6 As System.Windows.Forms.PictureBox
    Private WithEvents picUL5 As System.Windows.Forms.PictureBox
    Private WithEvents picUL4 As System.Windows.Forms.PictureBox
    Private WithEvents picUL3 As System.Windows.Forms.PictureBox
    Private WithEvents picUL2 As System.Windows.Forms.PictureBox
    Private WithEvents picUL1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents picLD10 As System.Windows.Forms.PictureBox
    Private WithEvents picLD11 As System.Windows.Forms.PictureBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tbLDTemp11 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp5 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp10 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp9 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp4 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp3 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp8 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp7 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp2 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp1 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents tbLDAlarmCode1 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDAlarmCode2 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDAlarmCode3 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDAlarmCode4 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDAlarmCode5 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents tbLDTemp12 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDTemp6 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDSlotStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDSlotStatus2 As System.Windows.Forms.TextBox
    Friend WithEvents tbLDCaseteBarCode As System.Windows.Forms.TextBox
    Friend WithEvents tbLDProductCount As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents tbLDProductType As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tbLDProductNumA As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbLDStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents tbLDTargetTemp As System.Windows.Forms.TextBox
    Friend WithEvents tbLDPass As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents tbULTargetTemp As System.Windows.Forms.TextBox
    Friend WithEvents tbULPass As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents tbULCaseteBarCode As System.Windows.Forms.TextBox
    Friend WithEvents tbULProductCount As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents tbULProductType As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents tbULProductNum As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents tbULStatus As System.Windows.Forms.TextBox
    Friend WithEvents tbULSlotStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents tbULSlotStatus2 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents tbULAlarmCode1 As System.Windows.Forms.TextBox
    Friend WithEvents tbULAlarmCode2 As System.Windows.Forms.TextBox
    Friend WithEvents tbULAlarmCode3 As System.Windows.Forms.TextBox
    Friend WithEvents tbULAlarmCode4 As System.Windows.Forms.TextBox
    Friend WithEvents tbULAlarmCode5 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents tbULTemp1 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp2 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp7 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp8 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp3 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp4 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp9 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp10 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp12 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp5 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp6 As System.Windows.Forms.TextBox
    Friend WithEvents tbULTemp11 As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents chkbAuto As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents rbUnloader As System.Windows.Forms.RadioButton
    Friend WithEvents rbLoader As System.Windows.Forms.RadioButton
    Friend WithEvents btnGetProductNum As System.Windows.Forms.Button
    Friend WithEvents btnGetCstBarCode As System.Windows.Forms.Button
    Friend WithEvents btnGetCastetData As System.Windows.Forms.Button
    Friend WithEvents btnSetTargetTemp As System.Windows.Forms.Button
    Friend WithEvents btnSetProductType As System.Windows.Forms.Button
    Friend WithEvents btnSetPassModel As System.Windows.Forms.Button
    Friend WithEvents chkbPass As System.Windows.Forms.CheckBox
    Friend WithEvents tbSetProductType As System.Windows.Forms.TextBox
    Friend WithEvents tbSetTargetTemp As System.Windows.Forms.TextBox
    Friend WithEvents btnGetAlarmCode As System.Windows.Forms.Button
    Friend WithEvents btnCassetteAbort As System.Windows.Forms.Button
    Friend WithEvents btnSetProductNum As System.Windows.Forms.Button
    Friend WithEvents tbSetProductNum As System.Windows.Forms.TextBox
    Friend WithEvents btnLdON5 As System.Windows.Forms.Button
    Friend WithEvents btnLdOFF5 As System.Windows.Forms.Button
    Friend WithEvents btnLdON4 As System.Windows.Forms.Button
    Friend WithEvents btnLdOFF4 As System.Windows.Forms.Button
    Friend WithEvents btnLdON3 As System.Windows.Forms.Button
    Friend WithEvents btnLdOFF3 As System.Windows.Forms.Button
    Friend WithEvents btnLdON2 As System.Windows.Forms.Button
    Friend WithEvents btnLdOFF2 As System.Windows.Forms.Button
    Friend WithEvents btnLdON1 As System.Windows.Forms.Button
    Friend WithEvents btnLdOFF1 As System.Windows.Forms.Button
    Friend WithEvents btnUlON4 As System.Windows.Forms.Button
    Friend WithEvents btnUlOFF4 As System.Windows.Forms.Button
    Friend WithEvents btnUlON3 As System.Windows.Forms.Button
    Friend WithEvents btnUlOFF3 As System.Windows.Forms.Button
    Friend WithEvents btnUlON2 As System.Windows.Forms.Button
    Friend WithEvents btnUlOFF2 As System.Windows.Forms.Button
    Friend WithEvents btnUlON1 As System.Windows.Forms.Button
    Friend WithEvents btnUlOFF1 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents picLD12 As System.Windows.Forms.PictureBox
    Friend WithEvents btnLdON6 As System.Windows.Forms.Button
    Friend WithEvents btnLdOFF6 As System.Windows.Forms.Button
    Friend WithEvents btnUlON5 As System.Windows.Forms.Button
    Friend WithEvents btnUlOFF5 As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents picUL10 As System.Windows.Forms.PictureBox
    Friend WithEvents btnAInitial As System.Windows.Forms.Button
    Friend WithEvents btnBInitial As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents imgListItems As System.Windows.Forms.ImageList
    Friend WithEvents tbLDProductNumB As System.Windows.Forms.TextBox
    Friend WithEvents btnSetLastNum As System.Windows.Forms.Button
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnAuto As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

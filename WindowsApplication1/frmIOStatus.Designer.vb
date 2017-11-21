<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIOStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIOStatus))
        Me.grpIOStatus = New System.Windows.Forms.GroupBox()
        Me.palSD = New System.Windows.Forms.Panel()
        Me.palERC = New System.Windows.Forms.Panel()
        Me.palDIR = New System.Windows.Forms.Panel()
        Me.palEZ = New System.Windows.Forms.Panel()
        Me.palTRIG = New System.Windows.Forms.Panel()
        Me.palNSEL = New System.Windows.Forms.Panel()
        Me.palLTC = New System.Windows.Forms.Panel()
        Me.palALRM = New System.Windows.Forms.Panel()
        Me.palPEL = New System.Windows.Forms.Panel()
        Me.palPSEL = New System.Windows.Forms.Panel()
        Me.palCLR = New System.Windows.Forms.Panel()
        Me.palPCS = New System.Windows.Forms.Panel()
        Me.palORG = New System.Windows.Forms.Panel()
        Me.palSVON = New System.Windows.Forms.Panel()
        Me.palALM = New System.Windows.Forms.Panel()
        Me.palEMG = New System.Windows.Forms.Panel()
        Me.palNEL = New System.Windows.Forms.Panel()
        Me.palINP = New System.Windows.Forms.Panel()
        Me.palRDY = New System.Windows.Forms.Panel()
        Me.lblTRIG = New System.Windows.Forms.Label()
        Me.lblNSEL = New System.Windows.Forms.Label()
        Me.lblPSEL = New System.Windows.Forms.Label()
        Me.lblALRM = New System.Windows.Forms.Label()
        Me.lblSVON = New System.Windows.Forms.Label()
        Me.lblINP = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblSD = New System.Windows.Forms.Label()
        Me.lblLTC = New System.Windows.Forms.Label()
        Me.lblCLR = New System.Windows.Forms.Label()
        Me.lblEZ = New System.Windows.Forms.Label()
        Me.lblERC = New System.Windows.Forms.Label()
        Me.lblPCS = New System.Windows.Forms.Label()
        Me.lblEMG = New System.Windows.Forms.Label()
        Me.lblDIR = New System.Windows.Forms.Label()
        Me.lblORG = New System.Windows.Forms.Label()
        Me.lblNEL = New System.Windows.Forms.Label()
        Me.lblPEL = New System.Windows.Forms.Label()
        Me.lblALM = New System.Windows.Forms.Label()
        Me.lblRDY = New System.Windows.Forms.Label()
        Me.tmrOption = New System.Windows.Forms.Timer(Me.components)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpIOStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpIOStatus
        '
        resources.ApplyResources(Me.grpIOStatus, "grpIOStatus")
        Me.grpIOStatus.Controls.Add(Me.palSD)
        Me.grpIOStatus.Controls.Add(Me.palERC)
        Me.grpIOStatus.Controls.Add(Me.palDIR)
        Me.grpIOStatus.Controls.Add(Me.palEZ)
        Me.grpIOStatus.Controls.Add(Me.palTRIG)
        Me.grpIOStatus.Controls.Add(Me.palNSEL)
        Me.grpIOStatus.Controls.Add(Me.palLTC)
        Me.grpIOStatus.Controls.Add(Me.palALRM)
        Me.grpIOStatus.Controls.Add(Me.palPEL)
        Me.grpIOStatus.Controls.Add(Me.palPSEL)
        Me.grpIOStatus.Controls.Add(Me.palCLR)
        Me.grpIOStatus.Controls.Add(Me.palPCS)
        Me.grpIOStatus.Controls.Add(Me.palORG)
        Me.grpIOStatus.Controls.Add(Me.palSVON)
        Me.grpIOStatus.Controls.Add(Me.palALM)
        Me.grpIOStatus.Controls.Add(Me.palEMG)
        Me.grpIOStatus.Controls.Add(Me.palNEL)
        Me.grpIOStatus.Controls.Add(Me.palINP)
        Me.grpIOStatus.Controls.Add(Me.palRDY)
        Me.grpIOStatus.Controls.Add(Me.lblTRIG)
        Me.grpIOStatus.Controls.Add(Me.lblNSEL)
        Me.grpIOStatus.Controls.Add(Me.lblPSEL)
        Me.grpIOStatus.Controls.Add(Me.lblALRM)
        Me.grpIOStatus.Controls.Add(Me.lblSVON)
        Me.grpIOStatus.Controls.Add(Me.lblINP)
        Me.grpIOStatus.Controls.Add(Me.Label14)
        Me.grpIOStatus.Controls.Add(Me.lblSD)
        Me.grpIOStatus.Controls.Add(Me.lblLTC)
        Me.grpIOStatus.Controls.Add(Me.lblCLR)
        Me.grpIOStatus.Controls.Add(Me.lblEZ)
        Me.grpIOStatus.Controls.Add(Me.lblERC)
        Me.grpIOStatus.Controls.Add(Me.lblPCS)
        Me.grpIOStatus.Controls.Add(Me.lblEMG)
        Me.grpIOStatus.Controls.Add(Me.lblDIR)
        Me.grpIOStatus.Controls.Add(Me.lblORG)
        Me.grpIOStatus.Controls.Add(Me.lblNEL)
        Me.grpIOStatus.Controls.Add(Me.lblPEL)
        Me.grpIOStatus.Controls.Add(Me.lblALM)
        Me.grpIOStatus.Controls.Add(Me.lblRDY)
        Me.grpIOStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpIOStatus.Name = "grpIOStatus"
        Me.grpIOStatus.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpIOStatus, resources.GetString("grpIOStatus.ToolTip"))
        '
        'palSD
        '
        resources.ApplyResources(Me.palSD, "palSD")
        Me.palSD.Name = "palSD"
        Me.ToolTip1.SetToolTip(Me.palSD, resources.GetString("palSD.ToolTip"))
        '
        'palERC
        '
        resources.ApplyResources(Me.palERC, "palERC")
        Me.palERC.Name = "palERC"
        Me.ToolTip1.SetToolTip(Me.palERC, resources.GetString("palERC.ToolTip"))
        '
        'palDIR
        '
        resources.ApplyResources(Me.palDIR, "palDIR")
        Me.palDIR.Name = "palDIR"
        Me.ToolTip1.SetToolTip(Me.palDIR, resources.GetString("palDIR.ToolTip"))
        '
        'palEZ
        '
        resources.ApplyResources(Me.palEZ, "palEZ")
        Me.palEZ.Name = "palEZ"
        Me.ToolTip1.SetToolTip(Me.palEZ, resources.GetString("palEZ.ToolTip"))
        '
        'palTRIG
        '
        resources.ApplyResources(Me.palTRIG, "palTRIG")
        Me.palTRIG.Name = "palTRIG"
        Me.ToolTip1.SetToolTip(Me.palTRIG, resources.GetString("palTRIG.ToolTip"))
        '
        'palNSEL
        '
        resources.ApplyResources(Me.palNSEL, "palNSEL")
        Me.palNSEL.Name = "palNSEL"
        Me.ToolTip1.SetToolTip(Me.palNSEL, resources.GetString("palNSEL.ToolTip"))
        '
        'palLTC
        '
        resources.ApplyResources(Me.palLTC, "palLTC")
        Me.palLTC.Name = "palLTC"
        Me.ToolTip1.SetToolTip(Me.palLTC, resources.GetString("palLTC.ToolTip"))
        '
        'palALRM
        '
        resources.ApplyResources(Me.palALRM, "palALRM")
        Me.palALRM.Name = "palALRM"
        Me.ToolTip1.SetToolTip(Me.palALRM, resources.GetString("palALRM.ToolTip"))
        '
        'palPEL
        '
        resources.ApplyResources(Me.palPEL, "palPEL")
        Me.palPEL.Name = "palPEL"
        Me.ToolTip1.SetToolTip(Me.palPEL, resources.GetString("palPEL.ToolTip"))
        '
        'palPSEL
        '
        resources.ApplyResources(Me.palPSEL, "palPSEL")
        Me.palPSEL.Name = "palPSEL"
        Me.ToolTip1.SetToolTip(Me.palPSEL, resources.GetString("palPSEL.ToolTip"))
        '
        'palCLR
        '
        resources.ApplyResources(Me.palCLR, "palCLR")
        Me.palCLR.Name = "palCLR"
        Me.ToolTip1.SetToolTip(Me.palCLR, resources.GetString("palCLR.ToolTip"))
        '
        'palPCS
        '
        resources.ApplyResources(Me.palPCS, "palPCS")
        Me.palPCS.Name = "palPCS"
        Me.ToolTip1.SetToolTip(Me.palPCS, resources.GetString("palPCS.ToolTip"))
        '
        'palORG
        '
        resources.ApplyResources(Me.palORG, "palORG")
        Me.palORG.Name = "palORG"
        Me.ToolTip1.SetToolTip(Me.palORG, resources.GetString("palORG.ToolTip"))
        '
        'palSVON
        '
        resources.ApplyResources(Me.palSVON, "palSVON")
        Me.palSVON.Name = "palSVON"
        Me.ToolTip1.SetToolTip(Me.palSVON, resources.GetString("palSVON.ToolTip"))
        '
        'palALM
        '
        resources.ApplyResources(Me.palALM, "palALM")
        Me.palALM.Name = "palALM"
        Me.ToolTip1.SetToolTip(Me.palALM, resources.GetString("palALM.ToolTip"))
        '
        'palEMG
        '
        resources.ApplyResources(Me.palEMG, "palEMG")
        Me.palEMG.Name = "palEMG"
        Me.ToolTip1.SetToolTip(Me.palEMG, resources.GetString("palEMG.ToolTip"))
        '
        'palNEL
        '
        resources.ApplyResources(Me.palNEL, "palNEL")
        Me.palNEL.Name = "palNEL"
        Me.ToolTip1.SetToolTip(Me.palNEL, resources.GetString("palNEL.ToolTip"))
        '
        'palINP
        '
        resources.ApplyResources(Me.palINP, "palINP")
        Me.palINP.Name = "palINP"
        Me.ToolTip1.SetToolTip(Me.palINP, resources.GetString("palINP.ToolTip"))
        '
        'palRDY
        '
        resources.ApplyResources(Me.palRDY, "palRDY")
        Me.palRDY.Name = "palRDY"
        Me.ToolTip1.SetToolTip(Me.palRDY, resources.GetString("palRDY.ToolTip"))
        '
        'lblTRIG
        '
        resources.ApplyResources(Me.lblTRIG, "lblTRIG")
        Me.lblTRIG.BackColor = System.Drawing.Color.Transparent
        Me.lblTRIG.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTRIG.Name = "lblTRIG"
        Me.ToolTip1.SetToolTip(Me.lblTRIG, resources.GetString("lblTRIG.ToolTip"))
        '
        'lblNSEL
        '
        resources.ApplyResources(Me.lblNSEL, "lblNSEL")
        Me.lblNSEL.BackColor = System.Drawing.Color.Transparent
        Me.lblNSEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNSEL.Name = "lblNSEL"
        Me.ToolTip1.SetToolTip(Me.lblNSEL, resources.GetString("lblNSEL.ToolTip"))
        '
        'lblPSEL
        '
        resources.ApplyResources(Me.lblPSEL, "lblPSEL")
        Me.lblPSEL.BackColor = System.Drawing.Color.Transparent
        Me.lblPSEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPSEL.Name = "lblPSEL"
        Me.ToolTip1.SetToolTip(Me.lblPSEL, resources.GetString("lblPSEL.ToolTip"))
        '
        'lblALRM
        '
        resources.ApplyResources(Me.lblALRM, "lblALRM")
        Me.lblALRM.BackColor = System.Drawing.Color.Transparent
        Me.lblALRM.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblALRM.Name = "lblALRM"
        Me.ToolTip1.SetToolTip(Me.lblALRM, resources.GetString("lblALRM.ToolTip"))
        '
        'lblSVON
        '
        resources.ApplyResources(Me.lblSVON, "lblSVON")
        Me.lblSVON.BackColor = System.Drawing.Color.Transparent
        Me.lblSVON.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSVON.Name = "lblSVON"
        Me.ToolTip1.SetToolTip(Me.lblSVON, resources.GetString("lblSVON.ToolTip"))
        '
        'lblINP
        '
        resources.ApplyResources(Me.lblINP, "lblINP")
        Me.lblINP.BackColor = System.Drawing.Color.Transparent
        Me.lblINP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblINP.Name = "lblINP"
        Me.ToolTip1.SetToolTip(Me.lblINP, resources.GetString("lblINP.ToolTip"))
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        '
        'lblSD
        '
        resources.ApplyResources(Me.lblSD, "lblSD")
        Me.lblSD.BackColor = System.Drawing.Color.Transparent
        Me.lblSD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSD.Name = "lblSD"
        Me.ToolTip1.SetToolTip(Me.lblSD, resources.GetString("lblSD.ToolTip"))
        '
        'lblLTC
        '
        resources.ApplyResources(Me.lblLTC, "lblLTC")
        Me.lblLTC.BackColor = System.Drawing.Color.Transparent
        Me.lblLTC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLTC.Name = "lblLTC"
        Me.ToolTip1.SetToolTip(Me.lblLTC, resources.GetString("lblLTC.ToolTip"))
        '
        'lblCLR
        '
        resources.ApplyResources(Me.lblCLR, "lblCLR")
        Me.lblCLR.BackColor = System.Drawing.Color.Transparent
        Me.lblCLR.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCLR.Name = "lblCLR"
        Me.ToolTip1.SetToolTip(Me.lblCLR, resources.GetString("lblCLR.ToolTip"))
        '
        'lblEZ
        '
        resources.ApplyResources(Me.lblEZ, "lblEZ")
        Me.lblEZ.BackColor = System.Drawing.Color.Transparent
        Me.lblEZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblEZ.Name = "lblEZ"
        Me.ToolTip1.SetToolTip(Me.lblEZ, resources.GetString("lblEZ.ToolTip"))
        '
        'lblERC
        '
        resources.ApplyResources(Me.lblERC, "lblERC")
        Me.lblERC.BackColor = System.Drawing.Color.Transparent
        Me.lblERC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblERC.Name = "lblERC"
        Me.ToolTip1.SetToolTip(Me.lblERC, resources.GetString("lblERC.ToolTip"))
        '
        'lblPCS
        '
        resources.ApplyResources(Me.lblPCS, "lblPCS")
        Me.lblPCS.BackColor = System.Drawing.Color.Transparent
        Me.lblPCS.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPCS.Name = "lblPCS"
        Me.ToolTip1.SetToolTip(Me.lblPCS, resources.GetString("lblPCS.ToolTip"))
        '
        'lblEMG
        '
        resources.ApplyResources(Me.lblEMG, "lblEMG")
        Me.lblEMG.BackColor = System.Drawing.Color.Transparent
        Me.lblEMG.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblEMG.Name = "lblEMG"
        Me.ToolTip1.SetToolTip(Me.lblEMG, resources.GetString("lblEMG.ToolTip"))
        '
        'lblDIR
        '
        resources.ApplyResources(Me.lblDIR, "lblDIR")
        Me.lblDIR.BackColor = System.Drawing.Color.Transparent
        Me.lblDIR.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblDIR.Name = "lblDIR"
        Me.ToolTip1.SetToolTip(Me.lblDIR, resources.GetString("lblDIR.ToolTip"))
        '
        'lblORG
        '
        resources.ApplyResources(Me.lblORG, "lblORG")
        Me.lblORG.BackColor = System.Drawing.Color.Transparent
        Me.lblORG.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblORG.Name = "lblORG"
        Me.ToolTip1.SetToolTip(Me.lblORG, resources.GetString("lblORG.ToolTip"))
        '
        'lblNEL
        '
        resources.ApplyResources(Me.lblNEL, "lblNEL")
        Me.lblNEL.BackColor = System.Drawing.Color.Transparent
        Me.lblNEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNEL.Name = "lblNEL"
        Me.ToolTip1.SetToolTip(Me.lblNEL, resources.GetString("lblNEL.ToolTip"))
        '
        'lblPEL
        '
        resources.ApplyResources(Me.lblPEL, "lblPEL")
        Me.lblPEL.BackColor = System.Drawing.Color.Transparent
        Me.lblPEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPEL.Name = "lblPEL"
        Me.ToolTip1.SetToolTip(Me.lblPEL, resources.GetString("lblPEL.ToolTip"))
        '
        'lblALM
        '
        resources.ApplyResources(Me.lblALM, "lblALM")
        Me.lblALM.BackColor = System.Drawing.Color.Transparent
        Me.lblALM.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblALM.Name = "lblALM"
        Me.ToolTip1.SetToolTip(Me.lblALM, resources.GetString("lblALM.ToolTip"))
        '
        'lblRDY
        '
        resources.ApplyResources(Me.lblRDY, "lblRDY")
        Me.lblRDY.BackColor = System.Drawing.Color.Transparent
        Me.lblRDY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblRDY.Name = "lblRDY"
        Me.ToolTip1.SetToolTip(Me.lblRDY, resources.GetString("lblRDY.ToolTip"))
        '
        'tmrOption
        '
        '
        'btnExit
        '
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmIOStatus
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grpIOStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIOStatus"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpIOStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpIOStatus As System.Windows.Forms.GroupBox
    Friend WithEvents palSD As System.Windows.Forms.Panel
    Friend WithEvents palERC As System.Windows.Forms.Panel
    Friend WithEvents palDIR As System.Windows.Forms.Panel
    Friend WithEvents palEZ As System.Windows.Forms.Panel
    Friend WithEvents palTRIG As System.Windows.Forms.Panel
    Friend WithEvents palNSEL As System.Windows.Forms.Panel
    Friend WithEvents palLTC As System.Windows.Forms.Panel
    Friend WithEvents palALRM As System.Windows.Forms.Panel
    Friend WithEvents palPEL As System.Windows.Forms.Panel
    Friend WithEvents palPSEL As System.Windows.Forms.Panel
    Friend WithEvents palCLR As System.Windows.Forms.Panel
    Friend WithEvents palPCS As System.Windows.Forms.Panel
    Friend WithEvents palORG As System.Windows.Forms.Panel
    Friend WithEvents palSVON As System.Windows.Forms.Panel
    Friend WithEvents palALM As System.Windows.Forms.Panel
    Friend WithEvents palEMG As System.Windows.Forms.Panel
    Friend WithEvents palNEL As System.Windows.Forms.Panel
    Friend WithEvents palINP As System.Windows.Forms.Panel
    Friend WithEvents palRDY As System.Windows.Forms.Panel
    Friend WithEvents lblTRIG As System.Windows.Forms.Label
    Friend WithEvents lblNSEL As System.Windows.Forms.Label
    Friend WithEvents lblPSEL As System.Windows.Forms.Label
    Friend WithEvents lblALRM As System.Windows.Forms.Label
    Friend WithEvents lblSVON As System.Windows.Forms.Label
    Friend WithEvents lblINP As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblSD As System.Windows.Forms.Label
    Friend WithEvents lblLTC As System.Windows.Forms.Label
    Friend WithEvents lblCLR As System.Windows.Forms.Label
    Friend WithEvents lblEZ As System.Windows.Forms.Label
    Friend WithEvents lblERC As System.Windows.Forms.Label
    Friend WithEvents lblPCS As System.Windows.Forms.Label
    Friend WithEvents lblEMG As System.Windows.Forms.Label
    Friend WithEvents lblDIR As System.Windows.Forms.Label
    Friend WithEvents lblORG As System.Windows.Forms.Label
    Friend WithEvents lblNEL As System.Windows.Forms.Label
    Friend WithEvents lblPEL As System.Windows.Forms.Label
    Friend WithEvents lblALM As System.Windows.Forms.Label
    Friend WithEvents lblRDY As System.Windows.Forms.Label
    Friend WithEvents tmrOption As System.Windows.Forms.Timer
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

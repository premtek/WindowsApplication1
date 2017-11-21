<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDHandshake
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDHandshake))
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pl_CCDHandshakeListDO = New System.Windows.Forms.Panel()
        Me.txtCCDConnectState = New System.Windows.Forms.TextBox()
        Me.lblCCDConnectState = New System.Windows.Forms.Label()
        Me.btnConnectStatus = New System.Windows.Forms.Button()
        Me.btnCCDSendData = New System.Windows.Forms.Button()
        Me.txtCCDSendData = New System.Windows.Forms.TextBox()
        Me.lblCCDSendData = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.btnCcdBit1 = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.btnCcdBit0 = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.btnCcdImageTrigger = New System.Windows.Forms.Button()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pl_CCDHandshakeListDI = New System.Windows.Forms.Panel()
        Me.btnCcdGate = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCCDReceiveData = New System.Windows.Forms.TextBox()
        Me.lblCCDReceiveData = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.btnCcdOutputResult = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCcdReady = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCcdBusy = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tmrCcdHandshake = New System.Windows.Forms.Timer(Me.components)
        Me.pl_CCDHandshakeListDO.SuspendLayout()
        Me.pl_CCDHandshakeListDI.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label25.Name = "Label25"
        '
        'pl_CCDHandshakeListDO
        '
        Me.pl_CCDHandshakeListDO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.txtCCDConnectState)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.lblCCDConnectState)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.btnConnectStatus)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.btnCCDSendData)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.txtCCDSendData)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.lblCCDSendData)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.Label38)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.btnCcdBit1)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.Label41)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.btnCcdBit0)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.Label44)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.btnCcdImageTrigger)
        Me.pl_CCDHandshakeListDO.Controls.Add(Me.Label47)
        resources.ApplyResources(Me.pl_CCDHandshakeListDO, "pl_CCDHandshakeListDO")
        Me.pl_CCDHandshakeListDO.Name = "pl_CCDHandshakeListDO"
        '
        'txtCCDConnectState
        '
        Me.txtCCDConnectState.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtCCDConnectState, "txtCCDConnectState")
        Me.txtCCDConnectState.Name = "txtCCDConnectState"
        '
        'lblCCDConnectState
        '
        resources.ApplyResources(Me.lblCCDConnectState, "lblCCDConnectState")
        Me.lblCCDConnectState.Name = "lblCCDConnectState"
        '
        'btnConnectStatus
        '
        Me.btnConnectStatus.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnConnectStatus, "btnConnectStatus")
        Me.btnConnectStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnConnectStatus.Name = "btnConnectStatus"
        Me.btnConnectStatus.UseVisualStyleBackColor = True
        '
        'btnCCDSendData
        '
        Me.btnCCDSendData.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCCDSendData, "btnCCDSendData")
        Me.btnCCDSendData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCCDSendData.Name = "btnCCDSendData"
        Me.btnCCDSendData.UseVisualStyleBackColor = True
        '
        'txtCCDSendData
        '
        Me.txtCCDSendData.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtCCDSendData, "txtCCDSendData")
        Me.txtCCDSendData.Name = "txtCCDSendData"
        '
        'lblCCDSendData
        '
        resources.ApplyResources(Me.lblCCDSendData, "lblCCDSendData")
        Me.lblCCDSendData.Name = "lblCCDSendData"
        '
        'Label38
        '
        resources.ApplyResources(Me.Label38, "Label38")
        Me.Label38.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label38.Name = "Label38"
        '
        'btnCcdBit1
        '
        Me.btnCcdBit1.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdBit1, "btnCcdBit1")
        Me.btnCcdBit1.Name = "btnCcdBit1"
        Me.btnCcdBit1.UseVisualStyleBackColor = True
        '
        'Label41
        '
        resources.ApplyResources(Me.Label41, "Label41")
        Me.Label41.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label41.Name = "Label41"
        '
        'btnCcdBit0
        '
        Me.btnCcdBit0.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdBit0, "btnCcdBit0")
        Me.btnCcdBit0.Name = "btnCcdBit0"
        Me.btnCcdBit0.UseVisualStyleBackColor = True
        '
        'Label44
        '
        resources.ApplyResources(Me.Label44, "Label44")
        Me.Label44.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label44.Name = "Label44"
        '
        'btnCcdImageTrigger
        '
        Me.btnCcdImageTrigger.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdImageTrigger, "btnCcdImageTrigger")
        Me.btnCcdImageTrigger.Name = "btnCcdImageTrigger"
        Me.btnCcdImageTrigger.UseVisualStyleBackColor = True
        '
        'Label47
        '
        resources.ApplyResources(Me.Label47, "Label47")
        Me.Label47.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label47.Name = "Label47"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label10.Name = "Label10"
        '
        'pl_CCDHandshakeListDI
        '
        Me.pl_CCDHandshakeListDI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.btnCcdGate)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.Label12)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.txtCCDReceiveData)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.lblCCDReceiveData)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.Label52)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.btnCcdOutputResult)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.Label4)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.btnCcdReady)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.Label6)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.btnCcdBusy)
        Me.pl_CCDHandshakeListDI.Controls.Add(Me.Label1)
        resources.ApplyResources(Me.pl_CCDHandshakeListDI, "pl_CCDHandshakeListDI")
        Me.pl_CCDHandshakeListDI.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.pl_CCDHandshakeListDI.Name = "pl_CCDHandshakeListDI"
        '
        'btnCcdGate
        '
        Me.btnCcdGate.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdGate, "btnCcdGate")
        Me.btnCcdGate.Name = "btnCcdGate"
        Me.btnCcdGate.UseVisualStyleBackColor = True
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label12.Name = "Label12"
        '
        'txtCCDReceiveData
        '
        Me.txtCCDReceiveData.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtCCDReceiveData, "txtCCDReceiveData")
        Me.txtCCDReceiveData.Name = "txtCCDReceiveData"
        '
        'lblCCDReceiveData
        '
        resources.ApplyResources(Me.lblCCDReceiveData, "lblCCDReceiveData")
        Me.lblCCDReceiveData.Name = "lblCCDReceiveData"
        '
        'Label52
        '
        resources.ApplyResources(Me.Label52, "Label52")
        Me.Label52.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label52.Name = "Label52"
        '
        'btnCcdOutputResult
        '
        Me.btnCcdOutputResult.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdOutputResult, "btnCcdOutputResult")
        Me.btnCcdOutputResult.Name = "btnCcdOutputResult"
        Me.btnCcdOutputResult.UseVisualStyleBackColor = True
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Name = "Label4"
        '
        'btnCcdReady
        '
        Me.btnCcdReady.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdReady, "btnCcdReady")
        Me.btnCcdReady.Name = "btnCcdReady"
        Me.btnCcdReady.UseVisualStyleBackColor = True
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Name = "Label6"
        '
        'btnCcdBusy
        '
        Me.btnCcdBusy.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCcdBusy, "btnCcdBusy")
        Me.btnCcdBusy.Name = "btnCcdBusy"
        Me.btnCcdBusy.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Name = "Label1"
        '
        'tmrCcdHandshake
        '
        '
        'frmCCDHandshake
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.pl_CCDHandshakeListDO)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.pl_CCDHandshakeListDI)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCCDHandshake"
        Me.pl_CCDHandshakeListDO.ResumeLayout(False)
        Me.pl_CCDHandshakeListDO.PerformLayout()
        Me.pl_CCDHandshakeListDI.ResumeLayout(False)
        Me.pl_CCDHandshakeListDI.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pl_CCDHandshakeListDO As System.Windows.Forms.Panel
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnCcdBit1 As System.Windows.Forms.Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents btnCcdBit0 As System.Windows.Forms.Button
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents btnCcdImageTrigger As System.Windows.Forms.Button
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pl_CCDHandshakeListDI As System.Windows.Forms.Panel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnCcdOutputResult As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnCcdReady As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnCcdBusy As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmrCcdHandshake As System.Windows.Forms.Timer
    Friend WithEvents txtCCDSendData As System.Windows.Forms.TextBox
    Friend WithEvents lblCCDSendData As System.Windows.Forms.Label
    Friend WithEvents btnCCDSendData As System.Windows.Forms.Button
    Friend WithEvents lblCCDReceiveData As System.Windows.Forms.Label
    Friend WithEvents txtCCDReceiveData As System.Windows.Forms.TextBox
    Friend WithEvents btnCcdGate As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnConnectStatus As System.Windows.Forms.Button
    Friend WithEvents txtCCDConnectState As System.Windows.Forms.TextBox
    Friend WithEvents lblCCDConnectState As System.Windows.Forms.Label
End Class

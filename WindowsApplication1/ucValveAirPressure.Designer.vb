<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucValveAirPressure
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
        Me.grpValveAir = New System.Windows.Forms.GroupBox()
        Me.btnSetValveOnOff = New System.Windows.Forms.Button()
        Me.lblValveAPMax1 = New System.Windows.Forms.Label()
        Me.lblValveAPUnit1 = New System.Windows.Forms.Label()
        Me.btnSetSyringeOnOff = New System.Windows.Forms.Button()
        Me.lblValveAPNow1 = New System.Windows.Forms.Label()
        Me.txtSyringeAPSet = New System.Windows.Forms.TextBox()
        Me.btnSetValveAP = New System.Windows.Forms.Button()
        Me.lblSyringeAP1 = New System.Windows.Forms.Label()
        Me.lblSyringeAPNow1 = New System.Windows.Forms.Label()
        Me.txtValveAPSet = New System.Windows.Forms.TextBox()
        Me.btnSetSyringeAP = New System.Windows.Forms.Button()
        Me.lbValveApMax = New System.Windows.Forms.Label()
        Me.lbValveApNow = New System.Windows.Forms.Label()
        Me.lblValveAPSetUnit = New System.Windows.Forms.Label()
        Me.lblValveAPSet1 = New System.Windows.Forms.Label()
        Me.lblSyringeAPMax1 = New System.Windows.Forms.Label()
        Me.lblSyringeAPUnit1 = New System.Windows.Forms.Label()
        Me.lblValveAPSet = New System.Windows.Forms.Label()
        Me.grpValveAir.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpValveAir
        '
        Me.grpValveAir.Controls.Add(Me.btnSetValveOnOff)
        Me.grpValveAir.Controls.Add(Me.lblValveAPMax1)
        Me.grpValveAir.Controls.Add(Me.lblValveAPUnit1)
        Me.grpValveAir.Controls.Add(Me.btnSetSyringeOnOff)
        Me.grpValveAir.Controls.Add(Me.lblValveAPNow1)
        Me.grpValveAir.Controls.Add(Me.txtSyringeAPSet)
        Me.grpValveAir.Controls.Add(Me.btnSetValveAP)
        Me.grpValveAir.Controls.Add(Me.lblSyringeAP1)
        Me.grpValveAir.Controls.Add(Me.lblSyringeAPNow1)
        Me.grpValveAir.Controls.Add(Me.txtValveAPSet)
        Me.grpValveAir.Controls.Add(Me.btnSetSyringeAP)
        Me.grpValveAir.Controls.Add(Me.lbValveApMax)
        Me.grpValveAir.Controls.Add(Me.lbValveApNow)
        Me.grpValveAir.Controls.Add(Me.lblValveAPSetUnit)
        Me.grpValveAir.Controls.Add(Me.lblValveAPSet1)
        Me.grpValveAir.Controls.Add(Me.lblSyringeAPMax1)
        Me.grpValveAir.Controls.Add(Me.lblSyringeAPUnit1)
        Me.grpValveAir.Controls.Add(Me.lblValveAPSet)
        Me.grpValveAir.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.grpValveAir.Location = New System.Drawing.Point(3, 3)
        Me.grpValveAir.Name = "grpValveAir"
        Me.grpValveAir.Size = New System.Drawing.Size(732, 186)
        Me.grpValveAir.TabIndex = 91
        Me.grpValveAir.TabStop = False
        Me.grpValveAir.Text = "Valve Air Pressure "
        '
        'btnSetValveOnOff
        '
        Me.btnSetValveOnOff.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnSetValveOnOff.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSetValveOnOff.Location = New System.Drawing.Point(661, 137)
        Me.btnSetValveOnOff.Name = "btnSetValveOnOff"
        Me.btnSetValveOnOff.Size = New System.Drawing.Size(60, 30)
        Me.btnSetValveOnOff.TabIndex = 92
        Me.btnSetValveOnOff.Text = "On"
        Me.btnSetValveOnOff.UseVisualStyleBackColor = True
        '
        'lblValveAPMax1
        '
        Me.lblValveAPMax1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblValveAPMax1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblValveAPMax1.Location = New System.Drawing.Point(193, 38)
        Me.lblValveAPMax1.Name = "lblValveAPMax1"
        Me.lblValveAPMax1.Size = New System.Drawing.Size(76, 50)
        Me.lblValveAPMax1.TabIndex = 17
        Me.lblValveAPMax1.Text = "Max"
        Me.lblValveAPMax1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblValveAPUnit1
        '
        Me.lblValveAPUnit1.AutoEllipsis = True
        Me.lblValveAPUnit1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblValveAPUnit1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblValveAPUnit1.Location = New System.Drawing.Point(523, 38)
        Me.lblValveAPUnit1.Name = "lblValveAPUnit1"
        Me.lblValveAPUnit1.Size = New System.Drawing.Size(100, 50)
        Me.lblValveAPUnit1.TabIndex = 15
        Me.lblValveAPUnit1.Text = "Unit"
        Me.lblValveAPUnit1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSetSyringeOnOff
        '
        Me.btnSetSyringeOnOff.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnSetSyringeOnOff.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSetSyringeOnOff.Location = New System.Drawing.Point(661, 88)
        Me.btnSetSyringeOnOff.Name = "btnSetSyringeOnOff"
        Me.btnSetSyringeOnOff.Size = New System.Drawing.Size(60, 30)
        Me.btnSetSyringeOnOff.TabIndex = 90
        Me.btnSetSyringeOnOff.Text = "On"
        Me.btnSetSyringeOnOff.UseVisualStyleBackColor = True
        '
        'lblValveAPNow1
        '
        Me.lblValveAPNow1.AutoEllipsis = True
        Me.lblValveAPNow1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblValveAPNow1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblValveAPNow1.Location = New System.Drawing.Point(313, 38)
        Me.lblValveAPNow1.Name = "lblValveAPNow1"
        Me.lblValveAPNow1.Size = New System.Drawing.Size(76, 50)
        Me.lblValveAPNow1.TabIndex = 16
        Me.lblValveAPNow1.Text = "Now"
        Me.lblValveAPNow1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSyringeAPSet
        '
        Me.txtSyringeAPSet.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtSyringeAPSet.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSyringeAPSet.Location = New System.Drawing.Point(433, 88)
        Me.txtSyringeAPSet.Name = "txtSyringeAPSet"
        Me.txtSyringeAPSet.Size = New System.Drawing.Size(80, 33)
        Me.txtSyringeAPSet.TabIndex = 19
        Me.txtSyringeAPSet.Text = "0.00"
        Me.txtSyringeAPSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSetValveAP
        '
        Me.btnSetValveAP.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnSetValveAP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSetValveAP.Location = New System.Drawing.Point(591, 137)
        Me.btnSetValveAP.Name = "btnSetValveAP"
        Me.btnSetValveAP.Size = New System.Drawing.Size(60, 30)
        Me.btnSetValveAP.TabIndex = 89
        Me.btnSetValveAP.Text = "Set"
        Me.btnSetValveAP.UseVisualStyleBackColor = True
        '
        'lblSyringeAP1
        '
        Me.lblSyringeAP1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblSyringeAP1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSyringeAP1.Location = New System.Drawing.Point(23, 88)
        Me.lblSyringeAP1.Name = "lblSyringeAP1"
        Me.lblSyringeAP1.Size = New System.Drawing.Size(140, 33)
        Me.lblSyringeAP1.TabIndex = 21
        Me.lblSyringeAP1.Text = "Syringe"
        Me.lblSyringeAP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyringeAPNow1
        '
        Me.lblSyringeAPNow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyringeAPNow1.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.lblSyringeAPNow1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSyringeAPNow1.Location = New System.Drawing.Point(313, 88)
        Me.lblSyringeAPNow1.Name = "lblSyringeAPNow1"
        Me.lblSyringeAPNow1.Size = New System.Drawing.Size(80, 33)
        Me.lblSyringeAPNow1.TabIndex = 24
        Me.lblSyringeAPNow1.Text = "0.00"
        Me.lblSyringeAPNow1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtValveAPSet
        '
        Me.txtValveAPSet.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValveAPSet.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtValveAPSet.Location = New System.Drawing.Point(433, 138)
        Me.txtValveAPSet.Name = "txtValveAPSet"
        Me.txtValveAPSet.Size = New System.Drawing.Size(80, 33)
        Me.txtValveAPSet.TabIndex = 86
        Me.txtValveAPSet.Text = "0.00"
        Me.txtValveAPSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSetSyringeAP
        '
        Me.btnSetSyringeAP.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnSetSyringeAP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSetSyringeAP.Location = New System.Drawing.Point(591, 88)
        Me.btnSetSyringeAP.Name = "btnSetSyringeAP"
        Me.btnSetSyringeAP.Size = New System.Drawing.Size(60, 30)
        Me.btnSetSyringeAP.TabIndex = 89
        Me.btnSetSyringeAP.Text = "Set"
        Me.btnSetSyringeAP.UseVisualStyleBackColor = True
        '
        'lbValveApMax
        '
        Me.lbValveApMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbValveApMax.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.lbValveApMax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbValveApMax.Location = New System.Drawing.Point(193, 138)
        Me.lbValveApMax.Name = "lbValveApMax"
        Me.lbValveApMax.Size = New System.Drawing.Size(80, 33)
        Me.lbValveApMax.TabIndex = 24
        Me.lbValveApMax.Text = "0.00"
        Me.lbValveApMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbValveApNow
        '
        Me.lbValveApNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbValveApNow.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.lbValveApNow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbValveApNow.Location = New System.Drawing.Point(313, 138)
        Me.lbValveApNow.Name = "lbValveApNow"
        Me.lbValveApNow.Size = New System.Drawing.Size(80, 33)
        Me.lbValveApNow.TabIndex = 24
        Me.lbValveApNow.Text = "0.00"
        Me.lbValveApNow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblValveAPSetUnit
        '
        Me.lblValveAPSetUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblValveAPSetUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblValveAPSetUnit.Location = New System.Drawing.Point(523, 138)
        Me.lblValveAPSetUnit.Name = "lblValveAPSetUnit"
        Me.lblValveAPSetUnit.Size = New System.Drawing.Size(80, 30)
        Me.lblValveAPSetUnit.TabIndex = 87
        Me.lblValveAPSetUnit.Text = "MPa"
        Me.lblValveAPSetUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblValveAPSet1
        '
        Me.lblValveAPSet1.AutoEllipsis = True
        Me.lblValveAPSet1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblValveAPSet1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblValveAPSet1.Location = New System.Drawing.Point(433, 38)
        Me.lblValveAPSet1.Name = "lblValveAPSet1"
        Me.lblValveAPSet1.Size = New System.Drawing.Size(80, 50)
        Me.lblValveAPSet1.TabIndex = 15
        Me.lblValveAPSet1.Text = "Set"
        Me.lblValveAPSet1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyringeAPMax1
        '
        Me.lblSyringeAPMax1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyringeAPMax1.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.lblSyringeAPMax1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSyringeAPMax1.Location = New System.Drawing.Point(193, 88)
        Me.lblSyringeAPMax1.Name = "lblSyringeAPMax1"
        Me.lblSyringeAPMax1.Size = New System.Drawing.Size(80, 33)
        Me.lblSyringeAPMax1.TabIndex = 24
        Me.lblSyringeAPMax1.Text = "0.00"
        Me.lblSyringeAPMax1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyringeAPUnit1
        '
        Me.lblSyringeAPUnit1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblSyringeAPUnit1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSyringeAPUnit1.Location = New System.Drawing.Point(523, 88)
        Me.lblSyringeAPUnit1.Name = "lblSyringeAPUnit1"
        Me.lblSyringeAPUnit1.Size = New System.Drawing.Size(80, 30)
        Me.lblSyringeAPUnit1.TabIndex = 21
        Me.lblSyringeAPUnit1.Text = "MPa"
        Me.lblSyringeAPUnit1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblValveAPSet
        '
        Me.lblValveAPSet.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblValveAPSet.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblValveAPSet.Location = New System.Drawing.Point(23, 138)
        Me.lblValveAPSet.Name = "lblValveAPSet"
        Me.lblValveAPSet.Size = New System.Drawing.Size(140, 33)
        Me.lblValveAPSet.TabIndex = 88
        Me.lblValveAPSet.Text = "Valve AP"
        Me.lblValveAPSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ucValveAirPressure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.grpValveAir)
        Me.Name = "ucValveAirPressure"
        Me.Size = New System.Drawing.Size(738, 194)
        Me.grpValveAir.ResumeLayout(False)
        Me.grpValveAir.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpValveAir As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetValveOnOff As System.Windows.Forms.Button
    Friend WithEvents btnSetSyringeOnOff As System.Windows.Forms.Button
    Friend WithEvents btnSetValveAP As System.Windows.Forms.Button
    Friend WithEvents btnSetSyringeAP As System.Windows.Forms.Button
    Friend WithEvents lblValveAPSet1 As System.Windows.Forms.Label
    Friend WithEvents lblValveAPSet As System.Windows.Forms.Label
    Friend WithEvents lblValveAPSetUnit As System.Windows.Forms.Label
    Friend WithEvents txtValveAPSet As System.Windows.Forms.TextBox
    Friend WithEvents lblValveAPMax1 As System.Windows.Forms.Label
    Friend WithEvents lblValveAPNow1 As System.Windows.Forms.Label
    Friend WithEvents lblValveAPUnit1 As System.Windows.Forms.Label
    Friend WithEvents txtSyringeAPSet As System.Windows.Forms.TextBox
    Friend WithEvents lblSyringeAP1 As System.Windows.Forms.Label
    Friend WithEvents lbValveApMax As System.Windows.Forms.Label
    Friend WithEvents lblSyringeAPMax1 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeAPUnit1 As System.Windows.Forms.Label
    Friend WithEvents lbValveApNow As System.Windows.Forms.Label
    Friend WithEvents lblSyringeAPNow1 As System.Windows.Forms.Label

End Class

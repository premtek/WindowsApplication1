<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetWaferFilter
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.palWafer = New System.Windows.Forms.Panel()
        Me.btnWaferCenter = New System.Windows.Forms.Button()
        Me.btnWaferAngle = New System.Windows.Forms.Button()
        Me.btnDieSizePitch = New System.Windows.Forms.Button()
        Me.btnAdjustMap = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.UcWaferMapAdjust1 = New Premtek.ucWaferMapAdjust()
        Me.UcWaferMapCenter1 = New Premtek.ucWaferMapCenter()
        Me.UcWaferMapPitchSize1 = New Premtek.ucWaferMapPitchSize()
        Me.UcWaferMapAngle1 = New Premtek.ucWaferMapAngle()
        Me.ucJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.UcIndexer1 = New Premtek.ucIndexer()
        Me.palWafer.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(1742, 920)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(150, 70)
        Me.btnCancel.TabIndex = 492
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnOK.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOK.Location = New System.Drawing.Point(1586, 920)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(150, 70)
        Me.btnOK.TabIndex = 491
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'palWafer
        '
        Me.palWafer.Controls.Add(Me.UcWaferMapAdjust1)
        Me.palWafer.Controls.Add(Me.UcWaferMapCenter1)
        Me.palWafer.Controls.Add(Me.UcWaferMapPitchSize1)
        Me.palWafer.Controls.Add(Me.UcWaferMapAngle1)
        Me.palWafer.Location = New System.Drawing.Point(1041, 9)
        Me.palWafer.Name = "palWafer"
        Me.palWafer.Size = New System.Drawing.Size(800, 600)
        Me.palWafer.TabIndex = 519
        '
        'btnWaferCenter
        '
        Me.btnWaferCenter.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnWaferCenter.Location = New System.Drawing.Point(1742, 625)
        Me.btnWaferCenter.Name = "btnWaferCenter"
        Me.btnWaferCenter.Size = New System.Drawing.Size(150, 50)
        Me.btnWaferCenter.TabIndex = 520
        Me.btnWaferCenter.Text = "Wafer Center"
        Me.btnWaferCenter.UseVisualStyleBackColor = True
        Me.btnWaferCenter.Visible = False
        '
        'btnWaferAngle
        '
        Me.btnWaferAngle.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnWaferAngle.Location = New System.Drawing.Point(1742, 681)
        Me.btnWaferAngle.Name = "btnWaferAngle"
        Me.btnWaferAngle.Size = New System.Drawing.Size(150, 50)
        Me.btnWaferAngle.TabIndex = 521
        Me.btnWaferAngle.Text = "Wafer Angle"
        Me.btnWaferAngle.UseVisualStyleBackColor = True
        Me.btnWaferAngle.Visible = False
        '
        'btnDieSizePitch
        '
        Me.btnDieSizePitch.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnDieSizePitch.Location = New System.Drawing.Point(1742, 737)
        Me.btnDieSizePitch.Name = "btnDieSizePitch"
        Me.btnDieSizePitch.Size = New System.Drawing.Size(150, 60)
        Me.btnDieSizePitch.TabIndex = 522
        Me.btnDieSizePitch.Text = "Die Size && Pitch"
        Me.btnDieSizePitch.UseVisualStyleBackColor = True
        Me.btnDieSizePitch.Visible = False
        '
        'btnAdjustMap
        '
        Me.btnAdjustMap.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnAdjustMap.Location = New System.Drawing.Point(1742, 803)
        Me.btnAdjustMap.Name = "btnAdjustMap"
        Me.btnAdjustMap.Size = New System.Drawing.Size(150, 50)
        Me.btnAdjustMap.TabIndex = 523
        Me.btnAdjustMap.Text = "Adjust Map"
        Me.btnAdjustMap.UseVisualStyleBackColor = True
        Me.btnAdjustMap.Visible = False
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnNext.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnNext.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnNext.Location = New System.Drawing.Point(1430, 920)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(150, 70)
        Me.btnNext.TabIndex = 537
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnPrev.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPrev.Location = New System.Drawing.Point(1274, 920)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(150, 70)
        Me.btnPrev.TabIndex = 536
        Me.btnPrev.Text = "Prev."
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'UcLightControl1
        '
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcLightControl1.Location = New System.Drawing.Point(6, 791)
        Me.UcLightControl1.Name = "UcLightControl1"
        Me.UcLightControl1.Size = New System.Drawing.Size(239, 199)
        Me.UcLightControl1.TabIndex = 524
        '
        'UcWaferMapAdjust1
        '
        Me.UcWaferMapAdjust1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcWaferMapAdjust1.CountX = 1
        Me.UcWaferMapAdjust1.CountY = 1
        Me.UcWaferMapAdjust1.DieLeftDownCornerX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.DieLeftDownCornerY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.DieSizeX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.DieSizeY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.FirstDieIndexX = 0
        Me.UcWaferMapAdjust1.FirstDieIndexY = 0
        Me.UcWaferMapAdjust1.FirstDiePosX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.FirstDiePosY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.FirstDiePosZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.Location = New System.Drawing.Point(472, 383)
        Me.UcWaferMapAdjust1.Name = "UcWaferMapAdjust1"
        Me.UcWaferMapAdjust1.OriginDiePosX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.OriginDiePosY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.PitchX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.PitchY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.Size = New System.Drawing.Size(800, 600)
        Me.UcWaferMapAdjust1.TabIndex = 3
        Me.UcWaferMapAdjust1.Visible = False
        Me.UcWaferMapAdjust1.WaferAngle = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.WaferAngleY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.WaferCenterX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.WaferCenterY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAdjust1.WaferEffectiveRadius = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'UcWaferMapCenter1
        '
        Me.UcWaferMapCenter1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcWaferMapCenter1.Boundary = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.UcWaferMapCenter1.Location = New System.Drawing.Point(-121, 260)
        Me.UcWaferMapCenter1.Name = "UcWaferMapCenter1"
        Me.UcWaferMapCenter1.PosX1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosX2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosX3 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosY1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosY2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosY3 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosZ1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosZ2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.PosZ3 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapCenter1.Size = New System.Drawing.Size(800, 600)
        Me.UcWaferMapCenter1.TabIndex = 0
        Me.UcWaferMapCenter1.Visible = False
        '
        'UcWaferMapPitchSize1
        '
        Me.UcWaferMapPitchSize1.ADirectionX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.ADirectionY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.ADirectionZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcWaferMapPitchSize1.BDirectionX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.BDirectionY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.BDirectionZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.BottomDiePosX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.BottomDiePosY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.BottomDiePosZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.DieSizeX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.DieSizeY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.LeftDiePosX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.LeftDiePosY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.LeftDiePosZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.Location = New System.Drawing.Point(180, 153)
        Me.UcWaferMapPitchSize1.Name = "UcWaferMapPitchSize1"
        Me.UcWaferMapPitchSize1.PosX1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.PosX2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.PosY1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.PosY2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.PosZ1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.PosZ2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.RightDiePosX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.RightDiePosY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.RightDiePosZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.Size = New System.Drawing.Size(800, 600)
        Me.UcWaferMapPitchSize1.TabIndex = 2
        Me.UcWaferMapPitchSize1.TopDiePosX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.TopDiePosY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.TopDiePosZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.Visible = False
        Me.UcWaferMapPitchSize1.WaferAngle = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.WaferCenterX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.WaferCenterX2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.WaferCenterY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.WaferCenterY2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.WaferCenterZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapPitchSize1.WaferCenterZ2 = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'UcWaferMapAngle1
        '
        Me.UcWaferMapAngle1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcWaferMapAngle1.CountX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.CountY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.Location = New System.Drawing.Point(310, 19)
        Me.UcWaferMapAngle1.Name = "UcWaferMapAngle1"
        Me.UcWaferMapAngle1.PitchX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PitchY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosX1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosX2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosX3 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosX4 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosY1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosY2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosY3 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosY4 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosZ1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosZ2 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosZ3 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.PosZ4 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.Size = New System.Drawing.Size(800, 600)
        Me.UcWaferMapAngle1.TabIndex = 1
        Me.UcWaferMapAngle1.Visible = False
        Me.UcWaferMapAngle1.WaferCenterX = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.WaferCenterY = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.WaferCenterZ = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcWaferMapAngle1.WaferEffectiveRadius = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'ucJoyStick1
        '
        Me.ucJoyStick1.AXisA = 0
        Me.ucJoyStick1.AXisB = 0
        Me.ucJoyStick1.AXisC = 0
        Me.ucJoyStick1.AxisX = 0
        Me.ucJoyStick1.AxisY = 0
        Me.ucJoyStick1.AxisZ = 0
        Me.ucJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ucJoyStick1.Location = New System.Drawing.Point(978, 615)
        Me.ucJoyStick1.Name = "ucJoyStick1"
        Me.ucJoyStick1.Size = New System.Drawing.Size(290, 380)
        Me.ucJoyStick1.TabIndex = 493
        '
        'UcDisplay1
        '
        Me.UcDisplay1.AutoScroll = True
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.UcDisplay1.Location = New System.Drawing.Point(6, 9)
        Me.UcDisplay1.Margin = New System.Windows.Forms.Padding(4)
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.UcDisplay1.Size = New System.Drawing.Size(965, 775)
        Me.UcDisplay1.TabIndex = 1
        '
        'UcIndexer1
        '
        Me.UcIndexer1.Location = New System.Drawing.Point(1274, 615)
        Me.UcIndexer1.Name = "UcIndexer1"
        Me.UcIndexer1.Size = New System.Drawing.Size(203, 284)
        Me.UcIndexer1.TabIndex = 538
        '
        'frmSetWaferFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1904, 1002)
        Me.Controls.Add(Me.UcIndexer1)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.btnAdjustMap)
        Me.Controls.Add(Me.btnDieSizePitch)
        Me.Controls.Add(Me.btnWaferAngle)
        Me.Controls.Add(Me.btnWaferCenter)
        Me.Controls.Add(Me.palWafer)
        Me.Controls.Add(Me.ucJoyStick1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.UcDisplay1)
        Me.Name = "frmSetWaferFilter"
        Me.Text = "frmSetWaferFilter"
        Me.palWafer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ucJoyStick1 As WindowsApplication1.ucJoyStick
    Friend WithEvents palWafer As System.Windows.Forms.Panel
    Friend WithEvents btnWaferCenter As System.Windows.Forms.Button
    Friend WithEvents btnWaferAngle As System.Windows.Forms.Button
    Friend WithEvents btnDieSizePitch As System.Windows.Forms.Button
    Friend WithEvents btnAdjustMap As System.Windows.Forms.Button
    Friend WithEvents UcWaferMapCenter1 As Premtek.ucWaferMapCenter
    Friend WithEvents UcWaferMapPitchSize1 As Premtek.ucWaferMapPitchSize
    Friend WithEvents UcWaferMapAngle1 As Premtek.ucWaferMapAngle
    Friend WithEvents UcWaferMapAdjust1 As Premtek.ucWaferMapAdjust
    Friend WithEvents UcLightControl1 As WindowsApplication1.ucLightControl
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents UcIndexer1 As Premtek.ucIndexer
End Class

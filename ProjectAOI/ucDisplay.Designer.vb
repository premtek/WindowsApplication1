<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucDisplay
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            mIsHalconLive = False
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                mAutoWait.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucDisplay))
        Me.picDisplay = New System.Windows.Forms.PictureBox()
        Me.CogDisplay1 = New Cognex.VisionPro.Display.CogDisplay()
        Me.CogDisplayStatusBarV21 = New Cognex.VisionPro.CogDisplayStatusBarV2()
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picDisplay
        '
        Me.picDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.picDisplay.Location = New System.Drawing.Point(0, 0)
        Me.picDisplay.Name = "picDisplay"
        Me.picDisplay.Size = New System.Drawing.Size(188, 166)
        Me.picDisplay.TabIndex = 0
        Me.picDisplay.TabStop = False
        '
        'CogDisplay1
        '
        Me.CogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapLowerRoiLimit = 0.0R
        Me.CogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None
        Me.CogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapUpperRoiLimit = 1.0R
        Me.CogDisplay1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CogDisplay1.DoubleTapZoomCycleLength = 2
        Me.CogDisplay1.DoubleTapZoomSensitivity = 2.5R
        Me.CogDisplay1.Location = New System.Drawing.Point(0, 0)
        Me.CogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1
        Me.CogDisplay1.MouseWheelSensitivity = 1.0R
        Me.CogDisplay1.Name = "CogDisplay1"
        Me.CogDisplay1.OcxState = CType(resources.GetObject("CogDisplay1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.CogDisplay1.Size = New System.Drawing.Size(420, 310)
        Me.CogDisplay1.TabIndex = 1
        '
        'CogDisplayStatusBarV21
        '
        Me.CogDisplayStatusBarV21.CoordinateSpaceName = "*\#"
        Me.CogDisplayStatusBarV21.CoordinateSpaceName3D = "*\#"
        Me.CogDisplayStatusBarV21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CogDisplayStatusBarV21.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.CogDisplayStatusBarV21.Location = New System.Drawing.Point(0, 288)
        Me.CogDisplayStatusBarV21.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CogDisplayStatusBarV21.Name = "CogDisplayStatusBarV21"
        Me.CogDisplayStatusBarV21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CogDisplayStatusBarV21.Size = New System.Drawing.Size(420, 22)
        Me.CogDisplayStatusBarV21.TabIndex = 2
        Me.CogDisplayStatusBarV21.Use3DCoordinateSpaceTree = False
        '
        'ucDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.CogDisplayStatusBarV21)
        Me.Controls.Add(Me.picDisplay)
        Me.Controls.Add(Me.CogDisplay1)
        Me.Name = "ucDisplay"
        Me.Size = New System.Drawing.Size(420, 310)
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents picDisplay As System.Windows.Forms.PictureBox
    Public WithEvents CogDisplay1 As Cognex.VisionPro.Display.CogDisplay
    Friend WithEvents CogDisplayStatusBarV21 As Cognex.VisionPro.CogDisplayStatusBarV2

End Class

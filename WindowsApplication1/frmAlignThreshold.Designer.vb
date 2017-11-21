<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlignThreshold
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAlignThreshold))
        Me.lblResult = New System.Windows.Forms.Label()
        Me.lblParams = New System.Windows.Forms.Label()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.CogRecordDisplay2 = New Cognex.VisionPro.CogRecordDisplay()
        Me.CogRecordDisplay1 = New Cognex.VisionPro.CogRecordDisplay()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.CogRecordDisplay2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CogRecordDisplay1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblResult
        '
        resources.ApplyResources(Me.lblResult, "lblResult")
        Me.lblResult.Name = "lblResult"
        Me.ToolTip1.SetToolTip(Me.lblResult, resources.GetString("lblResult.ToolTip"))
        '
        'lblParams
        '
        resources.ApplyResources(Me.lblParams, "lblParams")
        Me.lblParams.Name = "lblParams"
        Me.ToolTip1.SetToolTip(Me.lblParams, resources.GetString("lblParams.ToolTip"))
        '
        'btnRun
        '
        resources.ApplyResources(Me.btnRun, "btnRun")
        Me.btnRun.Name = "btnRun"
        Me.ToolTip1.SetToolTip(Me.btnRun, resources.GetString("btnRun.ToolTip"))
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'CogRecordDisplay2
        '
        resources.ApplyResources(Me.CogRecordDisplay2, "CogRecordDisplay2")
        Me.CogRecordDisplay2.ColorMapLowerClipColor = System.Drawing.Color.Black
        Me.CogRecordDisplay2.ColorMapLowerRoiLimit = 0.0R
        Me.CogRecordDisplay2.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None
        Me.CogRecordDisplay2.ColorMapUpperClipColor = System.Drawing.Color.Black
        Me.CogRecordDisplay2.ColorMapUpperRoiLimit = 1.0R
        Me.CogRecordDisplay2.DoubleTapZoomCycleLength = 2
        Me.CogRecordDisplay2.DoubleTapZoomSensitivity = 2.5R
        Me.CogRecordDisplay2.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1
        Me.CogRecordDisplay2.MouseWheelSensitivity = 1.0R
        Me.CogRecordDisplay2.Name = "CogRecordDisplay2"
        Me.CogRecordDisplay2.OcxState = CType(resources.GetObject("CogRecordDisplay2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ToolTip1.SetToolTip(Me.CogRecordDisplay2, resources.GetString("CogRecordDisplay2.ToolTip"))
        '
        'CogRecordDisplay1
        '
        resources.ApplyResources(Me.CogRecordDisplay1, "CogRecordDisplay1")
        Me.CogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black
        Me.CogRecordDisplay1.ColorMapLowerRoiLimit = 0.0R
        Me.CogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None
        Me.CogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black
        Me.CogRecordDisplay1.ColorMapUpperRoiLimit = 1.0R
        Me.CogRecordDisplay1.DoubleTapZoomCycleLength = 2
        Me.CogRecordDisplay1.DoubleTapZoomSensitivity = 2.5R
        Me.CogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1
        Me.CogRecordDisplay1.MouseWheelSensitivity = 1.0R
        Me.CogRecordDisplay1.Name = "CogRecordDisplay1"
        Me.CogRecordDisplay1.OcxState = CType(resources.GetObject("CogRecordDisplay1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ToolTip1.SetToolTip(Me.CogRecordDisplay1, resources.GetString("CogRecordDisplay1.ToolTip"))
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, resources.GetString("btnCancel.ToolTip"))
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmAlignThreshold
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.lblParams)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.CogRecordDisplay2)
        Me.Controls.Add(Me.CogRecordDisplay1)
        Me.MaximizeBox = False
        Me.Name = "frmAlignThreshold"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.CogRecordDisplay2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CogRecordDisplay1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents lblParams As System.Windows.Forms.Label
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents CogRecordDisplay2 As Cognex.VisionPro.CogRecordDisplay
    Friend WithEvents CogRecordDisplay1 As Cognex.VisionPro.CogRecordDisplay
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

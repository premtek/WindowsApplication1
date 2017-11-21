<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibAlignTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibAlignTool))
        Me.CogToolBlockEditV21 = New Cognex.VisionPro.ToolBlock.CogToolBlockEditV2()
        Me.lblScene = New System.Windows.Forms.Label()
        Me.lblSetLight = New System.Windows.Forms.Label()
        Me.lblSceneSet = New System.Windows.Forms.Label()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTime = New System.Windows.Forms.Label()
        Me.chkLight4 = New System.Windows.Forms.CheckBox()
        Me.chkLight3 = New System.Windows.Forms.CheckBox()
        Me.chkLight2 = New System.Windows.Forms.CheckBox()
        Me.chkLight1 = New System.Windows.Forms.CheckBox()
        Me.lightBar4 = New System.Windows.Forms.TrackBar()
        Me.lightBar3 = New System.Windows.Forms.TrackBar()
        Me.lightBar2 = New System.Windows.Forms.TrackBar()
        Me.lightBar1 = New System.Windows.Forms.TrackBar()
        Me.btnSetLight4 = New System.Windows.Forms.Button()
        Me.btnSetLight3 = New System.Windows.Forms.Button()
        Me.btnSetLight2 = New System.Windows.Forms.Button()
        Me.btnSetLight1 = New System.Windows.Forms.Button()
        Me.nmcLight4 = New System.Windows.Forms.NumericUpDown()
        Me.nmcLight3 = New System.Windows.Forms.NumericUpDown()
        Me.nmcLight2 = New System.Windows.Forms.NumericUpDown()
        Me.nmcLight1 = New System.Windows.Forms.NumericUpDown()
        Me.lblImageSource = New System.Windows.Forms.Label()
        Me.btnACQ = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTrainTool = New System.Windows.Forms.Label()
        Me.lblRepeatability = New System.Windows.Forms.Label()
        Me.btnRepeatRun = New System.Windows.Forms.Button()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.CogToolBlockEditV21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lightBar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lightBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lightBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lightBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLight1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CogToolBlockEditV21
        '
        resources.ApplyResources(Me.CogToolBlockEditV21, "CogToolBlockEditV21")
        Me.CogToolBlockEditV21.AllowDrop = True
        Me.CogToolBlockEditV21.ContextMenuCustomizer = Nothing
        Me.CogToolBlockEditV21.Name = "CogToolBlockEditV21"
        Me.CogToolBlockEditV21.ShowNodeToolTips = True
        Me.CogToolBlockEditV21.SuspendElectricRuns = False
        Me.ToolTip1.SetToolTip(Me.CogToolBlockEditV21, resources.GetString("CogToolBlockEditV21.ToolTip"))
        '
        'lblScene
        '
        resources.ApplyResources(Me.lblScene, "lblScene")
        Me.lblScene.Name = "lblScene"
        Me.ToolTip1.SetToolTip(Me.lblScene, resources.GetString("lblScene.ToolTip"))
        '
        'lblSetLight
        '
        resources.ApplyResources(Me.lblSetLight, "lblSetLight")
        Me.lblSetLight.Name = "lblSetLight"
        Me.ToolTip1.SetToolTip(Me.lblSetLight, resources.GetString("lblSetLight.ToolTip"))
        '
        'lblSceneSet
        '
        resources.ApplyResources(Me.lblSceneSet, "lblSceneSet")
        Me.lblSceneSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSceneSet.Name = "lblSceneSet"
        Me.ToolTip1.SetToolTip(Me.lblSceneSet, resources.GetString("lblSceneSet.ToolTip"))
        '
        'lblExposureTimeUnit
        '
        resources.ApplyResources(Me.lblExposureTimeUnit, "lblExposureTimeUnit")
        Me.lblExposureTimeUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblExposureTimeUnit.Name = "lblExposureTimeUnit"
        Me.ToolTip1.SetToolTip(Me.lblExposureTimeUnit, resources.GetString("lblExposureTimeUnit.ToolTip"))
        '
        'nmcExposure
        '
        resources.ApplyResources(Me.nmcExposure, "nmcExposure")
        Me.nmcExposure.DecimalPlaces = 1
        Me.nmcExposure.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcExposure.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nmcExposure.Name = "nmcExposure"
        Me.ToolTip1.SetToolTip(Me.nmcExposure, resources.GetString("nmcExposure.ToolTip"))
        Me.nmcExposure.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblExposureTime
        '
        resources.ApplyResources(Me.lblExposureTime, "lblExposureTime")
        Me.lblExposureTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblExposureTime.Name = "lblExposureTime"
        Me.ToolTip1.SetToolTip(Me.lblExposureTime, resources.GetString("lblExposureTime.ToolTip"))
        '
        'chkLight4
        '
        resources.ApplyResources(Me.chkLight4, "chkLight4")
        Me.chkLight4.Name = "chkLight4"
        Me.ToolTip1.SetToolTip(Me.chkLight4, resources.GetString("chkLight4.ToolTip"))
        Me.chkLight4.UseVisualStyleBackColor = True
        '
        'chkLight3
        '
        resources.ApplyResources(Me.chkLight3, "chkLight3")
        Me.chkLight3.Name = "chkLight3"
        Me.ToolTip1.SetToolTip(Me.chkLight3, resources.GetString("chkLight3.ToolTip"))
        Me.chkLight3.UseVisualStyleBackColor = True
        '
        'chkLight2
        '
        resources.ApplyResources(Me.chkLight2, "chkLight2")
        Me.chkLight2.Name = "chkLight2"
        Me.ToolTip1.SetToolTip(Me.chkLight2, resources.GetString("chkLight2.ToolTip"))
        Me.chkLight2.UseVisualStyleBackColor = True
        '
        'chkLight1
        '
        resources.ApplyResources(Me.chkLight1, "chkLight1")
        Me.chkLight1.Name = "chkLight1"
        Me.ToolTip1.SetToolTip(Me.chkLight1, resources.GetString("chkLight1.ToolTip"))
        Me.chkLight1.UseVisualStyleBackColor = True
        '
        'lightBar4
        '
        resources.ApplyResources(Me.lightBar4, "lightBar4")
        Me.lightBar4.LargeChange = 10
        Me.lightBar4.Maximum = 255
        Me.lightBar4.Name = "lightBar4"
        Me.lightBar4.TickFrequency = 10
        Me.ToolTip1.SetToolTip(Me.lightBar4, resources.GetString("lightBar4.ToolTip"))
        '
        'lightBar3
        '
        resources.ApplyResources(Me.lightBar3, "lightBar3")
        Me.lightBar3.LargeChange = 10
        Me.lightBar3.Maximum = 255
        Me.lightBar3.Name = "lightBar3"
        Me.lightBar3.TickFrequency = 10
        Me.ToolTip1.SetToolTip(Me.lightBar3, resources.GetString("lightBar3.ToolTip"))
        '
        'lightBar2
        '
        resources.ApplyResources(Me.lightBar2, "lightBar2")
        Me.lightBar2.LargeChange = 10
        Me.lightBar2.Maximum = 255
        Me.lightBar2.Name = "lightBar2"
        Me.lightBar2.TickFrequency = 10
        Me.ToolTip1.SetToolTip(Me.lightBar2, resources.GetString("lightBar2.ToolTip"))
        '
        'lightBar1
        '
        resources.ApplyResources(Me.lightBar1, "lightBar1")
        Me.lightBar1.LargeChange = 10
        Me.lightBar1.Maximum = 255
        Me.lightBar1.Name = "lightBar1"
        Me.lightBar1.TickFrequency = 10
        Me.ToolTip1.SetToolTip(Me.lightBar1, resources.GetString("lightBar1.ToolTip"))
        '
        'btnSetLight4
        '
        resources.ApplyResources(Me.btnSetLight4, "btnSetLight4")
        Me.btnSetLight4.Name = "btnSetLight4"
        Me.ToolTip1.SetToolTip(Me.btnSetLight4, resources.GetString("btnSetLight4.ToolTip"))
        Me.btnSetLight4.UseVisualStyleBackColor = True
        '
        'btnSetLight3
        '
        resources.ApplyResources(Me.btnSetLight3, "btnSetLight3")
        Me.btnSetLight3.Name = "btnSetLight3"
        Me.ToolTip1.SetToolTip(Me.btnSetLight3, resources.GetString("btnSetLight3.ToolTip"))
        Me.btnSetLight3.UseVisualStyleBackColor = True
        '
        'btnSetLight2
        '
        resources.ApplyResources(Me.btnSetLight2, "btnSetLight2")
        Me.btnSetLight2.Name = "btnSetLight2"
        Me.ToolTip1.SetToolTip(Me.btnSetLight2, resources.GetString("btnSetLight2.ToolTip"))
        Me.btnSetLight2.UseVisualStyleBackColor = True
        '
        'btnSetLight1
        '
        resources.ApplyResources(Me.btnSetLight1, "btnSetLight1")
        Me.btnSetLight1.Name = "btnSetLight1"
        Me.ToolTip1.SetToolTip(Me.btnSetLight1, resources.GetString("btnSetLight1.ToolTip"))
        Me.btnSetLight1.UseVisualStyleBackColor = True
        '
        'nmcLight4
        '
        resources.ApplyResources(Me.nmcLight4, "nmcLight4")
        Me.nmcLight4.Maximum = New Decimal(New Integer() {700, 0, 0, 0})
        Me.nmcLight4.Name = "nmcLight4"
        Me.ToolTip1.SetToolTip(Me.nmcLight4, resources.GetString("nmcLight4.ToolTip"))
        '
        'nmcLight3
        '
        resources.ApplyResources(Me.nmcLight3, "nmcLight3")
        Me.nmcLight3.Maximum = New Decimal(New Integer() {700, 0, 0, 0})
        Me.nmcLight3.Name = "nmcLight3"
        Me.ToolTip1.SetToolTip(Me.nmcLight3, resources.GetString("nmcLight3.ToolTip"))
        '
        'nmcLight2
        '
        resources.ApplyResources(Me.nmcLight2, "nmcLight2")
        Me.nmcLight2.Maximum = New Decimal(New Integer() {700, 0, 0, 0})
        Me.nmcLight2.Name = "nmcLight2"
        Me.ToolTip1.SetToolTip(Me.nmcLight2, resources.GetString("nmcLight2.ToolTip"))
        '
        'nmcLight1
        '
        resources.ApplyResources(Me.nmcLight1, "nmcLight1")
        Me.nmcLight1.Maximum = New Decimal(New Integer() {700, 0, 0, 0})
        Me.nmcLight1.Name = "nmcLight1"
        Me.ToolTip1.SetToolTip(Me.nmcLight1, resources.GetString("nmcLight1.ToolTip"))
        '
        'lblImageSource
        '
        resources.ApplyResources(Me.lblImageSource, "lblImageSource")
        Me.lblImageSource.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblImageSource.Name = "lblImageSource"
        Me.ToolTip1.SetToolTip(Me.lblImageSource, resources.GetString("lblImageSource.ToolTip"))
        '
        'btnACQ
        '
        resources.ApplyResources(Me.btnACQ, "btnACQ")
        Me.btnACQ.Name = "btnACQ"
        Me.ToolTip1.SetToolTip(Me.btnACQ, resources.GetString("btnACQ.ToolTip"))
        Me.btnACQ.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        resources.ApplyResources(Me.btnReset, "btnReset")
        Me.btnReset.FlatAppearance.BorderSize = 0
        Me.btnReset.Name = "btnReset"
        Me.ToolTip1.SetToolTip(Me.btnReset, resources.GetString("btnReset.ToolTip"))
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'lblTrainTool
        '
        resources.ApplyResources(Me.lblTrainTool, "lblTrainTool")
        Me.lblTrainTool.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTrainTool.Name = "lblTrainTool"
        Me.ToolTip1.SetToolTip(Me.lblTrainTool, resources.GetString("lblTrainTool.ToolTip"))
        '
        'lblRepeatability
        '
        resources.ApplyResources(Me.lblRepeatability, "lblRepeatability")
        Me.lblRepeatability.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRepeatability.Name = "lblRepeatability"
        Me.ToolTip1.SetToolTip(Me.lblRepeatability, resources.GetString("lblRepeatability.ToolTip"))
        '
        'btnRepeatRun
        '
        resources.ApplyResources(Me.btnRepeatRun, "btnRepeatRun")
        Me.btnRepeatRun.FlatAppearance.BorderSize = 0
        Me.btnRepeatRun.Name = "btnRepeatRun"
        Me.ToolTip1.SetToolTip(Me.btnRepeatRun, resources.GetString("btnRepeatRun.ToolTip"))
        Me.btnRepeatRun.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        resources.ApplyResources(Me.btnRun, "btnRun")
        Me.btnRun.FlatAppearance.BorderSize = 0
        Me.btnRun.Name = "btnRun"
        Me.ToolTip1.SetToolTip(Me.btnRun, resources.GetString("btnRun.ToolTip"))
        Me.btnRun.UseVisualStyleBackColor = True
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
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.Button1.Name = "Button1"
        Me.ToolTip1.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Name = "Button2"
        Me.ToolTip1.SetToolTip(Me.Button2, resources.GetString("Button2.ToolTip"))
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmCalibAlignTool
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnRepeatRun)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblRepeatability)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTrainTool)
        Me.Controls.Add(Me.lblImageSource)
        Me.Controls.Add(Me.btnACQ)
        Me.Controls.Add(Me.chkLight4)
        Me.Controls.Add(Me.chkLight3)
        Me.Controls.Add(Me.chkLight2)
        Me.Controls.Add(Me.chkLight1)
        Me.Controls.Add(Me.lightBar4)
        Me.Controls.Add(Me.lightBar3)
        Me.Controls.Add(Me.lightBar2)
        Me.Controls.Add(Me.lightBar1)
        Me.Controls.Add(Me.btnSetLight4)
        Me.Controls.Add(Me.btnSetLight3)
        Me.Controls.Add(Me.btnSetLight2)
        Me.Controls.Add(Me.btnSetLight1)
        Me.Controls.Add(Me.nmcLight4)
        Me.Controls.Add(Me.nmcLight3)
        Me.Controls.Add(Me.nmcLight2)
        Me.Controls.Add(Me.nmcLight1)
        Me.Controls.Add(Me.lblExposureTimeUnit)
        Me.Controls.Add(Me.nmcExposure)
        Me.Controls.Add(Me.lblExposureTime)
        Me.Controls.Add(Me.lblSceneSet)
        Me.Controls.Add(Me.lblSetLight)
        Me.Controls.Add(Me.lblScene)
        Me.Controls.Add(Me.CogToolBlockEditV21)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibAlignTool"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.CogToolBlockEditV21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lightBar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lightBar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lightBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lightBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLight1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CogToolBlockEditV21 As Cognex.VisionPro.ToolBlock.CogToolBlockEditV2
    Friend WithEvents lblScene As System.Windows.Forms.Label
    Friend WithEvents lblSetLight As System.Windows.Forms.Label
    Friend WithEvents lblSceneSet As System.Windows.Forms.Label
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTime As System.Windows.Forms.Label
    Friend WithEvents chkLight4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLight3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLight2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkLight1 As System.Windows.Forms.CheckBox
    Friend WithEvents lightBar4 As System.Windows.Forms.TrackBar
    Friend WithEvents lightBar3 As System.Windows.Forms.TrackBar
    Friend WithEvents lightBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents lightBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents btnSetLight4 As System.Windows.Forms.Button
    Friend WithEvents btnSetLight3 As System.Windows.Forms.Button
    Friend WithEvents btnSetLight2 As System.Windows.Forms.Button
    Friend WithEvents btnSetLight1 As System.Windows.Forms.Button
    Friend WithEvents nmcLight4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcLight3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcLight2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcLight1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblImageSource As System.Windows.Forms.Label
    Friend WithEvents btnACQ As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTrainTool As System.Windows.Forms.Label
    Friend WithEvents lblRepeatability As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnRepeatRun As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterlockConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInterlockConfig))
        Me.grpHWInterlock = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.UcHeavyLight11 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight10 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight9 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight8 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight7 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight6 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight5 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight4 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight3 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight2 = New WindowsApplication1.ucHeavyLight()
        Me.UcHeavyLight1 = New WindowsApplication1.ucHeavyLight()
        Me.lblItem11 = New System.Windows.Forms.Label()
        Me.lblItem10 = New System.Windows.Forms.Label()
        Me.lblItem9 = New System.Windows.Forms.Label()
        Me.lblItem8 = New System.Windows.Forms.Label()
        Me.lblItem7 = New System.Windows.Forms.Label()
        Me.lblItem6 = New System.Windows.Forms.Label()
        Me.lblItem5 = New System.Windows.Forms.Label()
        Me.lblItem4 = New System.Windows.Forms.Label()
        Me.lblItem3 = New System.Windows.Forms.Label()
        Me.lblItem2 = New System.Windows.Forms.Label()
        Me.lblItem1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpHWInterlock.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpHWInterlock
        '
        resources.ApplyResources(Me.grpHWInterlock, "grpHWInterlock")
        Me.grpHWInterlock.Controls.Add(Me.btnCancel)
        Me.grpHWInterlock.Controls.Add(Me.btnOK)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight11)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight10)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight9)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight8)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight7)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight6)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight5)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight4)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight3)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight2)
        Me.grpHWInterlock.Controls.Add(Me.UcHeavyLight1)
        Me.grpHWInterlock.Controls.Add(Me.lblItem11)
        Me.grpHWInterlock.Controls.Add(Me.lblItem10)
        Me.grpHWInterlock.Controls.Add(Me.lblItem9)
        Me.grpHWInterlock.Controls.Add(Me.lblItem8)
        Me.grpHWInterlock.Controls.Add(Me.lblItem7)
        Me.grpHWInterlock.Controls.Add(Me.lblItem6)
        Me.grpHWInterlock.Controls.Add(Me.lblItem5)
        Me.grpHWInterlock.Controls.Add(Me.lblItem4)
        Me.grpHWInterlock.Controls.Add(Me.lblItem3)
        Me.grpHWInterlock.Controls.Add(Me.lblItem2)
        Me.grpHWInterlock.Controls.Add(Me.lblItem1)
        Me.grpHWInterlock.Name = "grpHWInterlock"
        Me.grpHWInterlock.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpHWInterlock, resources.GetString("grpHWInterlock.ToolTip"))
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
        'UcHeavyLight11
        '
        resources.ApplyResources(Me.UcHeavyLight11, "UcHeavyLight11")
        Me.UcHeavyLight11.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight11.Name = "UcHeavyLight11"
        Me.UcHeavyLight11.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight11, resources.GetString("UcHeavyLight11.ToolTip"))
        '
        'UcHeavyLight10
        '
        resources.ApplyResources(Me.UcHeavyLight10, "UcHeavyLight10")
        Me.UcHeavyLight10.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight10.Name = "UcHeavyLight10"
        Me.UcHeavyLight10.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight10, resources.GetString("UcHeavyLight10.ToolTip"))
        '
        'UcHeavyLight9
        '
        resources.ApplyResources(Me.UcHeavyLight9, "UcHeavyLight9")
        Me.UcHeavyLight9.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight9.Name = "UcHeavyLight9"
        Me.UcHeavyLight9.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight9, resources.GetString("UcHeavyLight9.ToolTip"))
        '
        'UcHeavyLight8
        '
        resources.ApplyResources(Me.UcHeavyLight8, "UcHeavyLight8")
        Me.UcHeavyLight8.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight8.Name = "UcHeavyLight8"
        Me.UcHeavyLight8.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight8, resources.GetString("UcHeavyLight8.ToolTip"))
        '
        'UcHeavyLight7
        '
        resources.ApplyResources(Me.UcHeavyLight7, "UcHeavyLight7")
        Me.UcHeavyLight7.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight7.Name = "UcHeavyLight7"
        Me.UcHeavyLight7.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight7, resources.GetString("UcHeavyLight7.ToolTip"))
        '
        'UcHeavyLight6
        '
        resources.ApplyResources(Me.UcHeavyLight6, "UcHeavyLight6")
        Me.UcHeavyLight6.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight6.Name = "UcHeavyLight6"
        Me.UcHeavyLight6.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight6, resources.GetString("UcHeavyLight6.ToolTip"))
        '
        'UcHeavyLight5
        '
        resources.ApplyResources(Me.UcHeavyLight5, "UcHeavyLight5")
        Me.UcHeavyLight5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight5.Name = "UcHeavyLight5"
        Me.UcHeavyLight5.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight5, resources.GetString("UcHeavyLight5.ToolTip"))
        '
        'UcHeavyLight4
        '
        resources.ApplyResources(Me.UcHeavyLight4, "UcHeavyLight4")
        Me.UcHeavyLight4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight4.Name = "UcHeavyLight4"
        Me.UcHeavyLight4.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight4, resources.GetString("UcHeavyLight4.ToolTip"))
        '
        'UcHeavyLight3
        '
        resources.ApplyResources(Me.UcHeavyLight3, "UcHeavyLight3")
        Me.UcHeavyLight3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight3.Name = "UcHeavyLight3"
        Me.UcHeavyLight3.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight3, resources.GetString("UcHeavyLight3.ToolTip"))
        '
        'UcHeavyLight2
        '
        resources.ApplyResources(Me.UcHeavyLight2, "UcHeavyLight2")
        Me.UcHeavyLight2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight2.Name = "UcHeavyLight2"
        Me.UcHeavyLight2.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight2, resources.GetString("UcHeavyLight2.ToolTip"))
        '
        'UcHeavyLight1
        '
        resources.ApplyResources(Me.UcHeavyLight1, "UcHeavyLight1")
        Me.UcHeavyLight1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcHeavyLight1.Name = "UcHeavyLight1"
        Me.UcHeavyLight1.Status = ProjectIO.eInterlock.None
        Me.ToolTip1.SetToolTip(Me.UcHeavyLight1, resources.GetString("UcHeavyLight1.ToolTip"))
        '
        'lblItem11
        '
        resources.ApplyResources(Me.lblItem11, "lblItem11")
        Me.lblItem11.Name = "lblItem11"
        Me.ToolTip1.SetToolTip(Me.lblItem11, resources.GetString("lblItem11.ToolTip"))
        '
        'lblItem10
        '
        resources.ApplyResources(Me.lblItem10, "lblItem10")
        Me.lblItem10.Name = "lblItem10"
        Me.ToolTip1.SetToolTip(Me.lblItem10, resources.GetString("lblItem10.ToolTip"))
        '
        'lblItem9
        '
        resources.ApplyResources(Me.lblItem9, "lblItem9")
        Me.lblItem9.Name = "lblItem9"
        Me.ToolTip1.SetToolTip(Me.lblItem9, resources.GetString("lblItem9.ToolTip"))
        '
        'lblItem8
        '
        resources.ApplyResources(Me.lblItem8, "lblItem8")
        Me.lblItem8.Name = "lblItem8"
        Me.ToolTip1.SetToolTip(Me.lblItem8, resources.GetString("lblItem8.ToolTip"))
        '
        'lblItem7
        '
        resources.ApplyResources(Me.lblItem7, "lblItem7")
        Me.lblItem7.Name = "lblItem7"
        Me.ToolTip1.SetToolTip(Me.lblItem7, resources.GetString("lblItem7.ToolTip"))
        '
        'lblItem6
        '
        resources.ApplyResources(Me.lblItem6, "lblItem6")
        Me.lblItem6.Name = "lblItem6"
        Me.ToolTip1.SetToolTip(Me.lblItem6, resources.GetString("lblItem6.ToolTip"))
        '
        'lblItem5
        '
        resources.ApplyResources(Me.lblItem5, "lblItem5")
        Me.lblItem5.Name = "lblItem5"
        Me.ToolTip1.SetToolTip(Me.lblItem5, resources.GetString("lblItem5.ToolTip"))
        '
        'lblItem4
        '
        resources.ApplyResources(Me.lblItem4, "lblItem4")
        Me.lblItem4.Name = "lblItem4"
        Me.ToolTip1.SetToolTip(Me.lblItem4, resources.GetString("lblItem4.ToolTip"))
        '
        'lblItem3
        '
        resources.ApplyResources(Me.lblItem3, "lblItem3")
        Me.lblItem3.Name = "lblItem3"
        Me.ToolTip1.SetToolTip(Me.lblItem3, resources.GetString("lblItem3.ToolTip"))
        '
        'lblItem2
        '
        resources.ApplyResources(Me.lblItem2, "lblItem2")
        Me.lblItem2.Name = "lblItem2"
        Me.ToolTip1.SetToolTip(Me.lblItem2, resources.GetString("lblItem2.ToolTip"))
        '
        'lblItem1
        '
        resources.ApplyResources(Me.lblItem1, "lblItem1")
        Me.lblItem1.Name = "lblItem1"
        Me.ToolTip1.SetToolTip(Me.lblItem1, resources.GetString("lblItem1.ToolTip"))
        '
        'frmInterlockConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpHWInterlock)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInterlockConfig"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpHWInterlock.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpHWInterlock As System.Windows.Forms.GroupBox
    Friend WithEvents lblItem11 As System.Windows.Forms.Label
    Friend WithEvents lblItem10 As System.Windows.Forms.Label
    Friend WithEvents lblItem9 As System.Windows.Forms.Label
    Friend WithEvents lblItem8 As System.Windows.Forms.Label
    Friend WithEvents lblItem7 As System.Windows.Forms.Label
    Friend WithEvents lblItem6 As System.Windows.Forms.Label
    Friend WithEvents lblItem5 As System.Windows.Forms.Label
    Friend WithEvents lblItem4 As System.Windows.Forms.Label
    Friend WithEvents lblItem3 As System.Windows.Forms.Label
    Friend WithEvents lblItem2 As System.Windows.Forms.Label
    Friend WithEvents lblItem1 As System.Windows.Forms.Label
    Friend WithEvents UcHeavyLight11 As ucHeavyLight
    Friend WithEvents UcHeavyLight10 As ucHeavyLight
    Friend WithEvents UcHeavyLight9 As ucHeavyLight
    Friend WithEvents UcHeavyLight8 As ucHeavyLight
    Friend WithEvents UcHeavyLight7 As ucHeavyLight
    Friend WithEvents UcHeavyLight6 As ucHeavyLight
    Friend WithEvents UcHeavyLight5 As ucHeavyLight
    Friend WithEvents UcHeavyLight4 As ucHeavyLight
    Friend WithEvents UcHeavyLight3 As ucHeavyLight
    Friend WithEvents UcHeavyLight2 As ucHeavyLight
    Friend WithEvents UcHeavyLight1 As ucHeavyLight
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

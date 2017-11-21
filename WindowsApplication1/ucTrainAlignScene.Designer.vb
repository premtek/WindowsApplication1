<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTrainAlignScene
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucTrainAlignScene))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnTrainScene = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnTrainScene
        '
        resources.ApplyResources(Me.btnTrainScene, "btnTrainScene")
        Me.btnTrainScene.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.setup1
        Me.btnTrainScene.FlatAppearance.BorderSize = 0
        Me.btnTrainScene.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnTrainScene.Name = "btnTrainScene"
        Me.ToolTip1.SetToolTip(Me.btnTrainScene, resources.GetString("btnTrainScene.ToolTip"))
        Me.btnTrainScene.UseVisualStyleBackColor = True
        '
        'ucTrainAlignScene
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnTrainScene)
        Me.Name = "ucTrainAlignScene"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTrainScene As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

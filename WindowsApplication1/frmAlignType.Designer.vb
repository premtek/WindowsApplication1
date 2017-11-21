<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlignType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAlignType))
        Me.btnAlign = New System.Windows.Forms.Button()
        Me.btnCorner = New System.Windows.Forms.Button()
        Me.btnCircle = New System.Windows.Forms.Button()
        Me.btnLane = New System.Windows.Forms.Button()
        Me.btnLoadFile = New System.Windows.Forms.Button()
        Me.OFDLoadScene = New System.Windows.Forms.OpenFileDialog()
        Me.btnBlob = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAlign
        '
        resources.ApplyResources(Me.btnAlign, "btnAlign")
        Me.btnAlign.Name = "btnAlign"
        Me.btnAlign.UseVisualStyleBackColor = True
        '
        'btnCorner
        '
        resources.ApplyResources(Me.btnCorner, "btnCorner")
        Me.btnCorner.Name = "btnCorner"
        Me.btnCorner.UseVisualStyleBackColor = True
        '
        'btnCircle
        '
        resources.ApplyResources(Me.btnCircle, "btnCircle")
        Me.btnCircle.Name = "btnCircle"
        Me.btnCircle.UseVisualStyleBackColor = True
        '
        'btnLane
        '
        resources.ApplyResources(Me.btnLane, "btnLane")
        Me.btnLane.Name = "btnLane"
        Me.btnLane.UseVisualStyleBackColor = True
        '
        'btnLoadFile
        '
        resources.ApplyResources(Me.btnLoadFile, "btnLoadFile")
        Me.btnLoadFile.Name = "btnLoadFile"
        Me.btnLoadFile.UseVisualStyleBackColor = True
        '
        'OFDLoadScene
        '
        Me.OFDLoadScene.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OFDLoadScene, "OFDLoadScene")
        '
        'btnBlob
        '
        resources.ApplyResources(Me.btnBlob, "btnBlob")
        Me.btnBlob.Name = "btnBlob"
        Me.btnBlob.UseVisualStyleBackColor = True
        '
        'frmAlignType
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnBlob)
        Me.Controls.Add(Me.btnLoadFile)
        Me.Controls.Add(Me.btnLane)
        Me.Controls.Add(Me.btnCircle)
        Me.Controls.Add(Me.btnCorner)
        Me.Controls.Add(Me.btnAlign)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmAlignType"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAlign As System.Windows.Forms.Button
    Friend WithEvents btnCorner As System.Windows.Forms.Button
    Friend WithEvents btnCircle As System.Windows.Forms.Button
    Friend WithEvents btnLane As System.Windows.Forms.Button
    Friend WithEvents btnLoadFile As System.Windows.Forms.Button
    Friend WithEvents OFDLoadScene As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnBlob As System.Windows.Forms.Button
End Class

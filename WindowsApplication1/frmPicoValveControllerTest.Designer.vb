<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPicoValveControllerTest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPicoValveControllerTest))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCOMPort = New System.Windows.Forms.ComboBox()
        Me.lblSelectItem = New System.Windows.Forms.Label()
        Me.cbxValvecontrolerType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnShowcontrolerForm = New System.Windows.Forms.Button()
        Me.cmbItem = New System.Windows.Forms.ComboBox()
        Me.grpValveController = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grpValveController.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.cmbBaudRate, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbCOMPort, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSelectItem, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbxValvecontrolerType, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnShowcontrolerForm, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbItem, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'cmbBaudRate
        '
        resources.ApplyResources(Me.cmbBaudRate, "cmbBaudRate")
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Name = "cmbBaudRate"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'cmbCOMPort
        '
        resources.ApplyResources(Me.cmbCOMPort, "cmbCOMPort")
        Me.cmbCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCOMPort.FormattingEnabled = True
        Me.cmbCOMPort.Name = "cmbCOMPort"
        '
        'lblSelectItem
        '
        resources.ApplyResources(Me.lblSelectItem, "lblSelectItem")
        Me.lblSelectItem.Name = "lblSelectItem"
        '
        'cbxValvecontrolerType
        '
        resources.ApplyResources(Me.cbxValvecontrolerType, "cbxValvecontrolerType")
        Me.cbxValvecontrolerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxValvecontrolerType.FormattingEnabled = True
        Me.cbxValvecontrolerType.Name = "cbxValvecontrolerType"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'btnShowcontrolerForm
        '
        resources.ApplyResources(Me.btnShowcontrolerForm, "btnShowcontrolerForm")
        Me.btnShowcontrolerForm.Name = "btnShowcontrolerForm"
        Me.btnShowcontrolerForm.UseVisualStyleBackColor = True
        '
        'cmbItem
        '
        resources.ApplyResources(Me.cmbItem, "cmbItem")
        Me.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItem.FormattingEnabled = True
        Me.cmbItem.Name = "cmbItem"
        '
        'grpValveController
        '
        Me.grpValveController.Controls.Add(Me.TableLayoutPanel1)
        Me.grpValveController.Controls.Add(Me.btnCancel)
        Me.grpValveController.Controls.Add(Me.btnOK)
        resources.ApplyResources(Me.grpValveController, "grpValveController")
        Me.grpValveController.Name = "grpValveController"
        Me.grpValveController.TabStop = False
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
        'frmPicoValveControllerTest
        '
        Me.AcceptButton = Me.btnOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CancelButton = Me.btnCancel
        Me.ControlBox = False
        Me.Controls.Add(Me.grpValveController)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPicoValveControllerTest"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.grpValveController.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSelectItem As System.Windows.Forms.Label
    Friend WithEvents cbxValvecontrolerType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnShowcontrolerForm As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents cmbItem As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCOMPort As System.Windows.Forms.ComboBox
    Friend WithEvents grpValveController As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

Imports ProjectCore

Public Class frmSysDebug

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblOverallCommand.Text = gSYS(eSys.OverAll).ExecuteCommand.ToString
        lblOverallStatus.Text = gSYS(eSys.OverAll).RunStatus.ToString
        lblOverallSysNum.Text = gSYS(eSys.OverAll).SysNum
        lblMachineACommand.Text = gSYS(eSys.MachineA).ExecuteCommand.ToString
        lblMachineAStatus.Text = gSYS(eSys.MachineA).RunStatus.ToString
        lblMachineASysNum.Text = gSYS(eSys.MachineA).SysNum
        lblMachineBCommand.Text = gSYS(eSys.MachineB).ExecuteCommand.ToString
        lblMachineBStatus.Text = gSYS(eSys.MachineB).RunStatus.ToString
        lblMachineBSysNum.Text = gSYS(eSys.MachineB).SysNum
        lblDispStage1Command.Text = gSYS(eSys.DispStage1).ExecuteCommand.ToString
        lblDispStage1Status.Text = gSYS(eSys.DispStage1).RunStatus.ToString
        lblDispStage1SysNum.Text = gSYS(eSys.DispStage1).SysNum
        lblDispStage2Command.Text = gSYS(eSys.DispStage2).ExecuteCommand.ToString
        lblDispStage2Status.Text = gSYS(eSys.DispStage2).RunStatus.ToString
        lblDispStage2SysNum.Text = gSYS(eSys.DispStage2).SysNum
        lblDispStage3Command.Text = gSYS(eSys.DispStage3).ExecuteCommand.ToString
        lblDispStage3Status.Text = gSYS(eSys.DispStage3).RunStatus.ToString
        lblDispStage3SysNum.Text = gSYS(eSys.DispStage3).SysNum
        lblDispStage4Command.Text = gSYS(eSys.DispStage4).ExecuteCommand.ToString
        lblDispStage4Status.Text = gSYS(eSys.DispStage4).RunStatus.ToString
        lblDispStage4SysNum.Text = gSYS(eSys.DispStage4).SysNum
    End Sub
End Class
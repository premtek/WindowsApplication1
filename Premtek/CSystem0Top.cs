using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectCore;
using WetcoConveyor;

namespace Premtek
{
    public enum eConveyorStatus
    {
        /// <summary>
        /// [A機作動/流道A作動]
        /// </summary>
        Station_A = 0,
        /// <summary>
        /// [A、B機作動/流道A、B作動]
        /// </summary>
        Station_AB = 1,
        /// <summary>
        /// [B機作動/流道B作動]
        /// </summary>
        Station_B = 2,
        /// <summary>
        /// [不用動]
        /// </summary>
        Station_None = 3
    }

    /// <summary>最上層系統
    /// </summary>
    public class CSystem0Top
    {
        /// <summary>軌道數
        /// </summary>
        public int ConveyorCount
        {
            get
            {
                return _ConveyorCount;
            }
            set
            {
                _ConveyorCount = value;
            }
        }
        /// <summary>軌道數
        /// </summary>
        int _ConveyorCount = 1;
        /// <summary>機台數
        /// </summary>
        public int MachineCount
        {
            get
            {
                return _MachineCount;
            }
            set
            {
                _MachineCount = value;
            }
        }
        /// <summary>機台數
        /// </summary>
        int _MachineCount = 1;
        /// <summary>外部配接機台型號
        /// </summary>
        public MCommonDefine.enmMachineType MachineType { get; set; }
        /// <summary>外部配接DI集合
        /// </summary>
        public CDICollection DICollection { get; set; }
        /// <summary>外部配接DO集合
        /// </summary>
        public CDOCollection DOCollection { get; set; }
        /// <summary>外部配接設備訊息處理
        /// </summary>
        public Premtek.Base.CEqpMsgHandler EqpMsg { get; set; }
        public CMsgHandler MsgHandler { get; set; }
        public cls800AQLul cls800AQ_LUL { get; set; }
        /// <summary>外部引入, 預處理後的Recipe(MAP)
        /// </summary>
        public CRecipe Recipe { get; set; }
        /// <summary>機型名稱
        /// </summary>
        public string MachineName { get; set; }
        
        /// <summary>單次運行
        /// </summary>
        public bool IsJustOneRun { get; set; }
        /// <summary>忽略Conveyor
        /// </summary>
        public bool IsBypassConveyor { get; set; }
        /// <summary>外部配接加熱開關設定
        /// </summary>
        public bool[] HeaterOn { get; set; }

        /// <summary>傳送帶狀態
        /// </summary>
        eConveyorStatus[] mConveyorStatus = new eConveyorStatus[(int)ProjectCore.MCommonDefine.eConveyor.Max];
        /// <summary>軌A代號(可能是第一軌或第二軌)
        /// </summary>
        ProjectCore.MCommonDefine.eConveyor mConveyorNo1;
        /// <summary>軌B代號(可能是第一軌或第二軌)
        /// </summary>
        ProjectCore.MCommonDefine.eConveyor mConveyorNo2;
        /// <summary>傳送帶系統代號A
        /// </summary>
        int mSysConveyorNo1;
        /// <summary>傳送帶系統代號B
        /// </summary>
        int mSysConveyorNo2;

        List<CSystem1Machine> _Machine;


        public CSystem0Top(int MachineCount, int MachineStageCount, Base.CMotionCollection motion, ProjectAOI.CAOICollection aoi, Base.CEqpMsgHandler eqpMsg, CDICollection diCollection, CDOCollection doCollection)
        {
            _Machine = new List<CSystem1Machine>();
            for (int machineNo = 0; machineNo < MachineCount; machineNo++)
            {
                _Machine.Add(new CSystem1Machine(machineNo, MachineStageCount, motion, aoi, eqpMsg, diCollection, doCollection));
            }
        }

        /// <summary>[主控層(LevelNo1端:控制 MachineA、MachineB、 Conveyor)]</summary>
        /// <param name="sys"></param>
        /// <remarks></remarks>
        public void System_OverAll(ref MSystemParameter.sSysParam sys)
        {
            //[Note]:判斷系統狀態是否為可接收命令
            if (sys.RunStatus == MSystemParameter.enmRunStatus.None | sys.RunStatus == MSystemParameter.enmRunStatus.Finish)
            {
                //[Note]:有下一筆命令
                if (sys.Command != MCommonDefine.eSysCommand.None)
                {
                    sys.ExecuteCommand = sys.Command;
                    //接收命令至執行命令
                    sys.Command = MCommonDefine.eSysCommand.None;
                    //接收暫存器清空 避免Finish又進來
                    sys.SysNum = MSystemParameter.sSysParam.SysLoopStart;
                    //系統起始索引
                    sys.RunStatus = MSystemParameter.enmRunStatus.Running;
                    //系統狀態改為運行中
                }
            }

            //[Note]:外部暫停，且系統可暫停時，不執行動作流程
            if (sys.ExternalPause == true & sys.IsCanPause == true)
            {
                return;
            }

            if (sys.RunStatus == MSystemParameter.enmRunStatus.Running)
            {
                switch (sys.ExecuteCommand)
                {
                    case MCommonDefine.eSysCommand.Home:
                        //整機復歸
                        Overall_HomeAction(ref sys);
                        break;
                    case MCommonDefine.eSysCommand.AutoRun:
                        //自動生產
                        if (this.ConveyorCount == 2)//雙軌
                        {
                            Overall_MulitConveyorAutoRunAction(ref sys);
                        }
                        else//單軌
                        {
                            Overall_AutoRunAction(ref sys);
                        }
                        break;

                }
            }
            for (int i = 0; i < _Machine.Count; i++)
            {
                _Machine[i].System_Machine(ref _Machine[i].Sys, i);
            }

        }

        /// <summary>復歸是否維持加熱
        /// </summary>
        public bool EnableInitialHotPlate;

        int[] _HeaterDO = { MCommonDefineDO.enmDO.HeaterOn1, MCommonDefineDO.enmDO.HeaterOn2, MCommonDefineDO.enmDO.HeaterOn3, MCommonDefineDO.enmDO.HeaterOn4, MCommonDefineDO.enmDO.HeaterOn5, MCommonDefineDO.enmDO.HeaterOn6, MCommonDefineDO.enmDO.HeaterOn7, MCommonDefineDO.enmDO.HeaterOn8, MCommonDefineDO.enmDO.HeaterOn9, MCommonDefineDO.enmDO.HeaterOn10, MCommonDefineDO.enmDO.HeaterOn11, MCommonDefineDO.enmDO.HeaterOn12 };

        void SetHotPlate()
        {
            if (EnableInitialHotPlate != true)
            {
                return;
            }
            if (HeaterOn == null)
            {
                return;
            }
            for (int i = 0; i < HeaterOn.Count(); i++)
            {
                DOCollection.SetState(_HeaterDO[i], true);
            }

        }

        /// <summary>復歸時Conveyor狀態
        /// </summary>
        eConveyorStatus mHomeConveyorStatus;

        /// <summary>[整機復歸流程]</summary>
        /// <param name="sys"></param>
        /// <remarks></remarks>
        void Overall_HomeAction(ref MSystemParameter.sSysParam sys)
        {

            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    //[Note]:先將狀態清除
                    MSystemParameter.gSYS[MCommonDefine.eSys.OverAll].Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.None;
                    MSystemParameter.gSYS[MCommonDefine.eSys.OverAll].Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.None;
                    for (int mMachineNo = MCommonDefine.eSys.MachineA; mMachineNo <= MSystemParameter.gSSystemParameter.MachineMax; mMachineNo++)
                    {
                        MSystemParameter.gSYS[mMachineNo].RunStatus = MSystemParameter.enmRunStatus.None;
                    }

                    sys.SysNum = 1500;

                    break;
                case 1500:
                    //[Note]:對各機組下復歸之命令
                    if (MachineCount == 2)
                    {
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.Home;
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineB].Command = MCommonDefine.eSysCommand.Home;

                    }
                    else
                    {
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.Home;
                    }
                    sys.SysNum = 2000;

                    break;
                //'[Note]:I/O Reset(TODO:一邊作業一邊復歸會掛)
                //If gDOCollection.ReSetDO() Then
                //    sys.SysNum = 1500
                //Else
                //    gEqpMsg.AddHistoryAlarm("Alarm_2080002", "Overall_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2080002))
                //    sys.RunStatus = enmRunStatus.Alarm
                //    Exit Sub
                //End If

                case 2000:
                    //[Note]:I/O Reset()
                    if (DOCollection.ReSetDO() == false)
                    {
                        if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Alarm_2080002", "Overall_HomeAction", sys.SysNum.ToString(), MsgHandler.GetMessage(EqpID.Alarm_2080002));
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    //[說明]:初始化後Heater紀錄開啟 For 800A   20161129
                    switch (MachineType)
                    {
                        case MCommonDefine.enmMachineType.DCSW_800AQ:
                            //   If gfrmUIViewer.Heater = True Then   20161206
                            SetHotPlate();
                            break;
                    }

                    switch (MachineType)
                    {
                        case MCommonDefine.enmMachineType.DCSW_800AQ:
                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo1) == true)
                            {
                                if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == true)
                                {
                                    //[Note]:把A機Conveyor原本要動的I/O省略掉
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station2ChuckVacuum2, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station2StopperUp, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station2StopperDown, false);
                                    //[Note]:把B機Conveyor原本要動的I/O省略掉
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station3ChuckVacuum2, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station3StopperUp, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station3StopperDown, false);
                                }
                                else
                                {
                                    //[Note]:把A機Conveyor原本要動的I/O省略掉
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station2ChuckVacuum2, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station2StopperUp, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station2StopperDown, false);
                                }
                            }
                            else
                            {
                                if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == true)
                                {
                                    //[Note]:把B機Conveyor原本要動的I/O省略掉
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station3ChuckVacuum2, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station3StopperUp, true);
                                    DOCollection.SetState(MCommonDefineDO.enmDO.Station3StopperDown, false);
                                }
                            }

                            break;
                        default:
                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo1) == true)
                            {
                                //[Note]:把A機Conveyor原本要動的I/O省略掉
                                DOCollection.SetState(MCommonDefineDO.enmDO.Station2ChuckVacuum2, true);
                                DOCollection.SetState(MCommonDefineDO.enmDO.Station2StopperUp, true);
                                DOCollection.SetState(MCommonDefineDO.enmDO.Station2StopperDown, false);
                            }

                            break;
                    }
                    sys.SysNum = 2100;

                    break;
                case 2100:
                    //[Note]:流道誰復歸
                    switch (MachineType)
                    {
                        case MCommonDefine.enmMachineType.DCSW_800AQ:
                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo1) == true)
                            {
                                if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == true)
                                {
                                    mHomeConveyorStatus = Premtek.eConveyorStatus.Station_None;
                                }
                                else
                                {
                                    mHomeConveyorStatus = Premtek.eConveyorStatus.Station_B;
                                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus = MSystemParameter.enmRunStatus.None;
                                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.HomeB;
                                    //B機復歸
                                }
                            }
                            else
                            {
                                if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == true)
                                {
                                    mHomeConveyorStatus = Premtek.eConveyorStatus.Station_A;
                                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus = MSystemParameter.enmRunStatus.None;
                                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.HomeA;
                                    //A機復歸
                                }
                                else
                                {
                                    mHomeConveyorStatus = Premtek.eConveyorStatus.Station_AB;
                                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus = MSystemParameter.enmRunStatus.None;
                                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.Home;
                                    //AB機復歸
                                }

                            }

                            if ((cls800AQ_LUL.Loader.IsOpen == false))
                            {
                                cls800AQ_LUL.Loader.Open(cls800AQ_LUL.LoaderPort.PortName, cls800AQ_LUL.LoaderPort.BuadRade, cls800AQ_LUL.LoaderPort.Parity, cls800AQ_LUL.LoaderPort.DataBits, cls800AQ_LUL.LoaderPort.StopBits);
                            }

                            if ((cls800AQ_LUL.Unloader.IsOpen == false))
                            {
                                cls800AQ_LUL.Unloader.Open(cls800AQ_LUL.UnloaderPort.PortName, cls800AQ_LUL.UnloaderPort.BuadRade, cls800AQ_LUL.UnloaderPort.Parity, cls800AQ_LUL.UnloaderPort.DataBits, cls800AQ_LUL.UnloaderPort.StopBits);
                            }

                            break;
                        case MCommonDefine.enmMachineType.DCS_350A:
                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo1) == false)
                            {
                                mHomeConveyorStatus = Premtek.eConveyorStatus.Station_A;
                                MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus = MSystemParameter.enmRunStatus.None;
                                MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.HomeA;
                                //A機復歸
                            }

                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == false)
                            {
                                mHomeConveyorStatus = Premtek.eConveyorStatus.Station_B;
                                MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor2].RunStatus = MSystemParameter.enmRunStatus.None;
                                MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor2].Command = MCommonDefine.eSysCommand.HomeA;
                                //B機復歸
                            }

                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo1) == false && IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == false)
                            {
                                mHomeConveyorStatus = Premtek.eConveyorStatus.Station_AB;
                            }

                            break;
                        default:
                            mHomeConveyorStatus = Premtek.eConveyorStatus.Station_None;
                            if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo1) == true)
                            {
                                mHomeConveyorStatus = Premtek.eConveyorStatus.Station_None;
                            }
                            else
                            {
                                MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus = MSystemParameter.enmRunStatus.None;
                                MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.Home;
                                //A機復歸
                            }

                            break;
                    }
                    sys.SysNum = 2500;

                    break;
                case 2500:
                    //[Note]:確認復歸完成
                    if (MachineCount == 2)
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                        {
                            if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineB].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[MCommonDefine.eSys.MachineB].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                            {
                                sys.SysNum = 3000;
                                //Home Error
                            }
                            else if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineB].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                            {
                                sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                                sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                                return;
                            }
                            //Home Error
                        }
                        else if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                        {
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            return;
                        }

                    }
                    else
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                        {
                            sys.SysNum = 3000;
                            //Home Error
                        }
                        else if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                        {
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            return;
                        }

                    }
                   
                    break;
                case 3000:
                    //[Note]:確認復歸完成
                    switch (mHomeConveyorStatus)
                    {
                        case Premtek.eConveyorStatus.Station_A:
                            if (MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.HomeA && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                            {
                                sys.SysNum = 9000;
                            }

                            break;
                        case Premtek.eConveyorStatus.Station_AB:
                            if ((MachineType == MCommonDefine.enmMachineType.DCS_350A))
                            {
                                if (MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.HomeA && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor2].ExecuteCommand == MCommonDefine.eSysCommand.HomeA && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                {
                                    sys.SysNum = 9000;
                                }
                            }
                            else
                            {
                                if (MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.Home && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                {
                                    sys.SysNum = 9000;
                                }
                            }

                            break;
                        case Premtek.eConveyorStatus.Station_B:
                            if ((MachineType == MCommonDefine.enmMachineType.DCS_350A))
                            {
                                if (MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor2].ExecuteCommand == MCommonDefine.eSysCommand.HomeA && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                {
                                    sys.SysNum = 9000;
                                }
                            }
                            else
                            {
                                if (MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.HomeB && MSystemParameter.gSYS[MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                {
                                    sys.SysNum = 9000;
                                }
                            }

                            break;
                        case Premtek.eConveyorStatus.Station_None:
                            sys.SysNum = 9000;

                            break;
                    }

                    break;
                case 9000:
                    //[Note]:完成整機復歸
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Finish;
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    Debug.Print("OverAll_HomeAction: " + sys.Act[MCommonDefine.eAct.Home].RunStatus);
                    return;

            }
        }

        /// <summary>[整機生產流程(Auto Run)(雙流道)]</summary>
        /// <param name="sys"></param>
        /// <remarks></remarks>
        void Overall_MulitConveyorAutoRunAction(ref MSystemParameter.sSysParam sys)
        {
            //[Note]:雙流道版本，基本上只適用在單機上不適用在雙機上

            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:

                    //[Note]:使用流道的配置
                    switch (MSystemParameter.gSSystemParameter.ConveyorModel)
                    {
                        case MCommonDefine.eConveyorModel.eConveyorNo1:
                            mConveyorNo1 = MCommonDefine.eConveyor.ConveyorNo1;
                            mSysConveyorNo1 = MCommonDefine.eSys.Conveyor1;
                            break;
                        case MCommonDefine.eConveyorModel.eConveyorNo2:
                            mConveyorNo1 = MCommonDefine.eConveyor.ConveyorNo2;
                            mSysConveyorNo1 = MCommonDefine.eSys.Conveyor2;

                            break;
                        case MCommonDefine.eConveyorModel.eConveyorNo1No2:
                            mConveyorNo1 = MCommonDefine.eConveyor.ConveyorNo1;
                            mConveyorNo2 = MCommonDefine.eConveyor.ConveyorNo2;
                            mSysConveyorNo1 = MCommonDefine.eSys.Conveyor1;
                            mSysConveyorNo2 = MCommonDefine.eSys.Conveyor2;
                            break;
                    }

                    sys.SysNum = 1500;

                    break;
                case 1500:
                    //[Note]:判斷本次作業會使用會使用到哪幾個Valve
                    //for (int mStageNo = MCommonDefine.eSys.DispStage1; mStageNo <= MSystemParameter.gSSystemParameter.StageMax; mStageNo++)
                    //{
                    //    WhichValveIsUsed(gCRecipe, MSystemParameter.gSYS[mStageNo].StageNo, gIsUseValveNo1(MSystemParameter.gSYS[mStageNo].StageNo), gIsUseValveNo2(MSystemParameter.gSYS[mStageNo].StageNo));
                    //}

                    sys.SysNum = 2000;
                    break;

                case 2000:
                    //[Note]:呼叫機台作點膠前動作(秤重含Purge)
                    //[Note]:第一次進入流程時就先檢查需不需要點膠前動作(秤重含Purge)
                    MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;
                    sys.SysNum = 2100;
                    break;

                case 2100:
                    //[Note]:流道A Load
                    if (IsChuckVacuumReady(mConveyorNo1) == true | IsBypassConveyor == true)
                    {
                        //[Note]:上面有料片不用再進料
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_None;
                        //[Note]:從入料完成後開始計時
                        _Machine[(int)mConveyorNo1].PriorHeatTimer.Restart();
                        sys.SysNum = 2300;
                    }
                    else
                    {
                        //[Note]:先給可入料之訊號
                        MSystemParameter.gSYS[mSysConveyorNo1].Command = MCommonDefine.eSysCommand.LoadA;
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_A;
                        sys.SysNum = 2200;
                    }

                    break;
                case 2200:
                    //[Note]:流道A Load Finish
                    if (MSystemParameter.gSYS[mSysConveyorNo1].ExecuteCommand == MCommonDefine.eSysCommand.LoadA && MSystemParameter.gSYS[mSysConveyorNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        //[Note]:從入料完成後開始計時
                        _Machine[(int)mConveyorNo1].PriorHeatTimer.Restart();
                        sys.SysNum = 2300;
                    }

                    break;
                case 2300:
                    //[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                    if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.PrevDispense)
                    {
                        sys.SysNum = 3000;
                    }

                    break;
                case 3000:
                    //[Note]:流道A  AutoRun
                    if (MSystemParameter.gSSystemParameter.IsContinueLastRun == true)
                    {
                        //[Note]:接續流程
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.ContinueLastRun;
                    }
                    else
                    {
                        //[Note]:正常流程
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.AutoRun;
                    }
                    MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ConveyorNo = mConveyorNo1;
                    sys.SysNum = 3100;

                    break;
                case 3100:
                    //[Note]:確認是否會有雙流道生產
                    if (MSystemParameter.gSSystemParameter.ConveyorModel == MCommonDefine.eConveyorModel.eConveyorNo1No2)
                    {
                        //[Note]:流道B Load
                        if (IsChuckVacuumReady(mConveyorNo2) == true | IsBypassConveyor == true)
                        {
                            //[Note]:上面有料片不用再進料
                            mConveyorStatus[(int)mConveyorNo2] = eConveyorStatus.Station_None;
                            sys.SysNum = 3300;
                        }
                        else
                        {
                            //[Note]:先給可入料之訊號
                            MSystemParameter.gSYS[mSysConveyorNo2].Command = MCommonDefine.eSysCommand.LoadA;
                            mConveyorStatus[(int)mConveyorNo2] = eConveyorStatus.Station_B;
                            sys.SysNum = 3200;
                        }
                    }
                    else
                    {
                        sys.SysNum = 3200;
                    }

                    break;
                case 3200:
                    //[Note]:流道A AutoRun Finish
                    if (MSystemParameter.gSSystemParameter.IsContinueLastRun == true)
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.ContinueLastRun)
                        {
                            sys.SysNum = 3300;
                        }
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.AutoRun)
                        {
                            sys.SysNum = 3300;
                        }
                    }

                    break;
                case 3300:
                    //[Note]:呼叫機台作點膠前動作(秤重含Purge)、流道A開始退料
                    //Soni / 2017.04.27 跑單次時, 退料不做Purge秤重
                    if (this.IsJustOneRun == false)
                    {
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;
                        //Soni + 2017.04.27 JustOneRun時, 雙閥仍做
                    }
                    else if (MSystemParameter.gSSystemParameter.ConveyorModel == MCommonDefine.eConveyorModel.eConveyorNo1No2)
                    {
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;
                    }


                    //[Note]:流道A Unload
                    if (IsBypassConveyor == false)
                    {
                        MSystemParameter.gSYS[mSysConveyorNo1].Command = MCommonDefine.eSysCommand.UnloadA;
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_A;
                    }
                    else
                    {
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_None;
                    }
                    sys.SysNum = 3400;

                    break;
                case 3400:
                    //[Note]:確認是否會有雙流道生產
                    if (MSystemParameter.gSSystemParameter.ConveyorModel == MCommonDefine.eConveyorModel.eConveyorNo1No2)
                    {
                        //[Note]:流道B Load Finish
                        if (mConveyorStatus[(int)mConveyorNo2] == eConveyorStatus.Station_B)
                        {
                            if (MSystemParameter.gSYS[mSysConveyorNo2].ExecuteCommand == MCommonDefine.eSysCommand.LoadA && MSystemParameter.gSYS[mSysConveyorNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                            {
                                //[Note]:從入料完成後開始計時
                                _Machine[(int)mConveyorNo2].PriorHeatTimer.Restart();
                                sys.SysNum = 3500;
                            }
                        }
                        else
                        {
                            //[Note]:從入料完成後開始計時
                            _Machine[(int)mConveyorNo2].PriorHeatTimer.Restart();
                            sys.SysNum = 3500;
                        }
                    }
                    else
                    {
                        sys.SysNum = 3600;
                    }

                    break;
                case 3500:
                    //[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                    if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.PrevDispense)
                    {
                        sys.SysNum = 4000;
                    }

                    break;
                case 3600:
                    //[Note]:確認流道A Unload Finish
                    if (mConveyorStatus[(int)mConveyorNo1] == eConveyorStatus.Station_None)
                    {
                        //Soni / 2017.04.27 由原流程移植
                        if (this.IsJustOneRun == true)
                        {
                            //[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                            sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.Finish;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                            return;
                        }
                        else
                        {
                            sys.SysNum = 2100;
                        }

                    }
                    else
                    {
                        if (MSystemParameter.gSYS[mSysConveyorNo1].ExecuteCommand == MCommonDefine.eSysCommand.UnloadA && MSystemParameter.gSYS[mSysConveyorNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                        {
                            //Soni / 2017.04.27 由原流程移植
                            if (this.IsJustOneRun == true)
                            {
                                //[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                                sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.Finish;
                                sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                                return;
                            }
                            else
                            {
                                sys.SysNum = 2100;
                            }

                        }
                    }

                    break;
                case 4000:
                    //[Note]:流道B AutoRun 
                    if (MSystemParameter.gSSystemParameter.IsContinueLastRun == true)
                    {
                        //[Note]:接續流程
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.ContinueLastRun;
                    }
                    else
                    {
                        //[Note]:正常流程
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.AutoRun;
                    }
                    MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ConveyorNo = mConveyorNo2;
                    sys.SysNum = 4100;

                    break;
                case 4100:
                    //[Note]:確認流道A Unload Finish
                    if (mConveyorStatus[(int)mConveyorNo1] == eConveyorStatus.Station_None)
                    {
                        sys.SysNum = 4200;
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[mSysConveyorNo1].ExecuteCommand == MCommonDefine.eSysCommand.UnloadA && MSystemParameter.gSYS[mSysConveyorNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                        {
                            sys.SysNum = 4200;
                        }
                    }

                    break;
                case 4200:
                    //[Note]:流道A Load
                    if (IsChuckVacuumReady(mConveyorNo1) == true | IsBypassConveyor == true)
                    {
                        //[Note]:上面有料片不用再進料
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_None;
                        sys.SysNum = 4300;
                    }
                    else
                    {
                        //[Note]:先給可入料之訊號
                        MSystemParameter.gSYS[mSysConveyorNo1].Command = MCommonDefine.eSysCommand.LoadA;
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_A;
                        sys.SysNum = 4300;
                    }

                    break;
                case 4300:
                    //[Note]:流道B AutoRun Finish
                    if (MSystemParameter.gSSystemParameter.IsContinueLastRun == true)
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.ContinueLastRun)
                        {
                            sys.SysNum = 4400;
                        }
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.AutoRun)
                        {
                            sys.SysNum = 4400;
                        }
                    }

                    break;
                case 4400:
                    if (this.IsJustOneRun == false)
                    {
                        //[Note]:呼叫機台作點膠前動作(秤重含Purge)、流道A開始退料
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;
                    }
                    //[Note]:流道B Unload
                    if (IsBypassConveyor == false)
                    {
                        MSystemParameter.gSYS[mSysConveyorNo2].Command = MCommonDefine.eSysCommand.UnloadA;
                        mConveyorStatus[(int)mConveyorNo2] = eConveyorStatus.Station_A;
                    }
                    else
                    {
                        mConveyorStatus[(int)mConveyorNo2] = eConveyorStatus.Station_None;
                    }
                    if (this.IsJustOneRun == false)
                    {
                        sys.SysNum = 4500;
                        //跑單次流程結束
                    }
                    else
                    {
                        sys.SysNum = 4420;
                    }

                    break;
                case 4420:
                    //[Note]:流道A Unload Finish
                    if (mConveyorStatus[(int)mConveyorNo2] == eConveyorStatus.Station_A)
                    {
                        if (MSystemParameter.gSYS[(int)mConveyorNo2].ExecuteCommand == MCommonDefine.eSysCommand.UnloadA && MSystemParameter.gSYS[mSysConveyorNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                        {
                            sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.Finish;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                            return;
                        }
                    }
                    else
                    {
                        sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.Finish;
                        sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                        return;
                    }

                    break;

                case 4500:
                    //[Note]:流道A Load Finish
                    if (mConveyorStatus[(int)mConveyorNo1] == eConveyorStatus.Station_A)
                    {
                        if (MSystemParameter.gSYS[mSysConveyorNo1].ExecuteCommand == MCommonDefine.eSysCommand.LoadA && MSystemParameter.gSYS[mSysConveyorNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                        {
                            //[Note]:從入料完成後開始計時
                            _Machine[(int)mConveyorNo1].PriorHeatTimer.Restart();
                            sys.SysNum = 4600;
                        }
                    }
                    else
                    {
                        //[Note]:從入料完成後開始計時
                        _Machine[(int)mConveyorNo1].PriorHeatTimer.Restart();
                        sys.SysNum = 4600;
                    }

                    break;
                case 4600:
                    //[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                    if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.PrevDispense)
                    {
                        sys.SysNum = 5000;
                    }

                    break;
                case 5000:
                    //[Note]:流道A AutoRun 
                    if (MSystemParameter.gSSystemParameter.IsContinueLastRun == true)
                    {
                        //[Note]:接續流程
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.ContinueLastRun;
                    }
                    else
                    {
                        //[Note]:正常流程
                        MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.AutoRun;
                    }
                    MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ConveyorNo = mConveyorNo1;
                    sys.SysNum = 5100;

                    break;
                case 5100:
                    //[Note]:確認流道B Unload Finish
                    if (mConveyorStatus[(int)mConveyorNo2] == eConveyorStatus.Station_None)
                    {
                        sys.SysNum = 5200;
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[mSysConveyorNo2].ExecuteCommand == MCommonDefine.eSysCommand.UnloadA && MSystemParameter.gSYS[mSysConveyorNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                        {
                            sys.SysNum = 5200;
                        }
                    }

                    break;
                case 5200:
                    //[Note]:流道B Load
                    if (IsChuckVacuumReady(MCommonDefine.eConveyor.ConveyorNo2) == true | IsBypassConveyor == true)
                    {
                        //[Note]:上面有料片不用再進料
                        mConveyorStatus[(int)mConveyorNo2] = eConveyorStatus.Station_None;
                        sys.SysNum = 5300;
                    }
                    else
                    {
                        //[Note]:先給可入料之訊號
                        MSystemParameter.gSYS[mSysConveyorNo2].Command = MCommonDefine.eSysCommand.LoadA;
                        mConveyorStatus[(int)mConveyorNo2] = eConveyorStatus.Station_A;
                        sys.SysNum = 5300;
                    }

                    break;
                case 5300:
                    //[Note]:流道A AutoRun Finish
                    if (MSystemParameter.gSSystemParameter.IsContinueLastRun == true)
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.ContinueLastRun)
                        {
                            sys.SysNum = 5400;
                        }
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.AutoRun)
                        {
                            sys.SysNum = 5400;
                        }
                    }

                    break;
                case 5400:
                    //[Note]:呼叫機台作點膠前動作(秤重含Purge)、流道A開始退料
                    MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;

                    //[Note]:流道A Unload
                    if (IsBypassConveyor == false)
                    {
                        MSystemParameter.gSYS[mSysConveyorNo1].Command = MCommonDefine.eSysCommand.UnloadA;
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_A;
                    }
                    else
                    {
                        mConveyorStatus[(int)mConveyorNo1] = eConveyorStatus.Station_None;
                    }
                    sys.SysNum = 5500;

                    break;
                case 5500:
                    //[Note]:流道B Load Finish
                    if (mConveyorStatus[(int)mConveyorNo2] == eConveyorStatus.Station_B)
                    {
                        if (MSystemParameter.gSYS[mSysConveyorNo2].ExecuteCommand == MCommonDefine.eSysCommand.LoadA && MSystemParameter.gSYS[mSysConveyorNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                        {
                            //[Note]:從入料完成後開始計時
                            _Machine[(int)mConveyorNo2].PriorHeatTimer.Restart();
                            sys.SysNum = 5600;
                        }
                    }
                    else
                    {
                        //[Note]:從入料完成後開始計時
                        _Machine[(int)mConveyorNo2].PriorHeatTimer.Restart();
                        sys.SysNum = 5600;
                    }

                    break;
                case 5600:
                    //[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                    if (MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.PrevDispense)
                    {
                        sys.SysNum = 4000;
                    }

                    break;

            }
        }



        /// <summary>生產時Conveyor狀態
        /// </summary>
        eConveyorStatus static_Overall_AutoRunAction_mConveyorStatus;

        /// <summary>接續流程
        /// </summary>
        bool IsContinueLastRun { get; set; }

        /// <summary>[抓取那些機台需作業]</summary>
        /// <param name="isBypassMachineA"></param>
        /// <param name="isBypassMachineB"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool GetMachineBypassState(ref bool isBypassMachineA, ref bool isBypassMachineB)
        {

            bool mIsBypassMachineA = false;
            bool mIsBypassMachineB = false;

            mIsBypassMachineA = true;
            mIsBypassMachineB = true;

            if (Recipe.Pattern[Recipe.Machine[0].Main].Step.Count == 0)
            {

            }
            for (int mStageNo = 0; mStageNo <= Recipe.Machine.Count() - 1; mStageNo++)
            {
                //[Note]:TODO:CCDFix改版需要修改
                if (Recipe.Pattern[Recipe.Machine[mStageNo].Main].Step.Count != 0)
                {
                    switch ((MCommonDefine.enmStage)mStageNo)
                    {
                        case MCommonDefine.enmStage.No1:
                        case MCommonDefine.enmStage.No2:
                            mIsBypassMachineA = false;

                            break;
                        case MCommonDefine.enmStage.No3:
                        case MCommonDefine.enmStage.No4:
                            mIsBypassMachineB = false;
                            break;
                    }
                }
            }

            isBypassMachineA = mIsBypassMachineA;
            isBypassMachineB = mIsBypassMachineB;
            return true;
        }


        /// <summary>[A機] 1~6 Vacuum 於Load/Unload動作時是否運作
        /// </summary>
        /// <returns></returns>
        bool[] A_Vacuum = { false, false, false, false, false, false };
        /// <summary>[B機] 1~6 Vacuum 於Load/Unload動作時是否運作
        /// </summary>
        /// <returns></returns>
        bool[] B_Vacuum = { false, false, false, false, false, false };

        /// <summary>
        /// [A機] 是否為真空狀態
        /// </summary>
        bool A_IsVacuum(bool defaultValue)
        {
            if ((DICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady, defaultValue) | A_Vacuum[0] == false))
            {
                if ((DICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady2, defaultValue) | A_Vacuum[1] == false))
                {
                    if ((DICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady3, defaultValue) | A_Vacuum[2] == false))
                    {
                        if ((DICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady4, defaultValue) | A_Vacuum[3] == false))
                        {
                            if ((DICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady5, defaultValue) | A_Vacuum[4] == false))
                            {
                                if ((DICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady6, defaultValue) | A_Vacuum[5] == false))
                                {
                                    //防止全部Vacuum都未開的情況下回復true
                                    foreach (bool @bool in A_Vacuum)
                                    {
                                        if ((@bool))
                                        {
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// [B機] 是否為真空狀態
        /// </summary>
        bool B_IsVacuum(bool defaultValue)
        {
            if ((DICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady, defaultValue) | B_Vacuum[0] == false))
            {
                if ((DICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady2, defaultValue) | B_Vacuum[1] == false))
                {
                    if ((DICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady3, defaultValue) | B_Vacuum[2] == false))
                    {
                        if ((DICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady4, defaultValue) | B_Vacuum[3] == false))
                        {
                            if ((DICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady5, defaultValue) | B_Vacuum[4] == false))
                            {
                                if ((DICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady6, defaultValue) | B_Vacuum[5] == false))
                                {
                                    //防止全部Vacuum都未開的情況下回復true
                                    foreach (bool @bool in B_Vacuum)
                                    {
                                        if ((@bool))
                                        {
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        bool mIsBypassMachineA;
        bool mIsBypassMachineB;
        void Overall_AutoRunAction(ref MSystemParameter.sSysParam sys)
        {
            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_None;

                    GetMachineBypassState(ref mIsBypassMachineA, ref  mIsBypassMachineB);

                    //[Note]:第一次進入流程時就先檢查需不需要點膠前動作(秤重含Purge)
                    sys.SysNum = 1100;

                    break;
                case 1100:
                    sys.SysNum = 1500;
                    break;

                case 1500:
                    //[Note]:呼叫A機作點膠前動作(秤重含Purge)
                    if (mIsBypassMachineA == false)
                    {
                        //[Note]:判斷要不要做
                        static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_A;
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;
                    }
                    //[Note]:呼叫B機作點膠前動作(秤重含Purge)
                    if (mIsBypassMachineB == false)
                    {
                        //[Note]:判斷要不要做
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].Command = MCommonDefine.eSysCommand.PrevDispense;
                        if (mIsBypassMachineA == false)
                        {
                            static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_AB;
                        }
                        else
                        {
                            static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_B;
                        }
                    }
                    sys.SysNum = 1600;

                    break;
                case 1600:
                    //[Note]:若AB機都Bypass就GG了-->不應該成立，即便成立了也是在此迴圈繞
                    if (mIsBypassMachineB == false)
                    {
                        //[Note]:B機先進料
                        sys.SysNum = 2000;
                    }
                    else
                    {
                        if (mIsBypassMachineA == false)
                        {
                            //[Note]:A機進料
                            sys.SysNum = 3000;
                        }
                    }

                    break;
                case 2000:
                    //[Note]:B機進料
                    if (B_IsVacuum(false) == true | IsBypassConveyor == true)
                    {
                        //[Note]:上面有料片不用再進料
                        sys.SysNum = 2200;
                    }
                    else
                    {
                        //[Note]:先給可入料之訊號
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.LoadB;
                        sys.SysNum = 2100;
                    }

                    break;
                case 2100:
                    //[Note]:B機進料完成
                    if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.LoadB && MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        //Debug.Print("B_LoadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        sys.SysNum = 2200;
                    }

                    break;
                case 2200:
                    //[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                    //[Note]:從入料完成後開始計時
                    _Machine[(int)MCommonDefine.enmMachineStation.MachineB].PriorHeatTimer.Restart();
                    if (static_Overall_AutoRunAction_mConveyorStatus == Premtek.eConveyorStatus.Station_AB | static_Overall_AutoRunAction_mConveyorStatus == Premtek.eConveyorStatus.Station_B)
                    {
                        if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].ExecuteCommand == MCommonDefine.eSysCommand.PrevDispense)
                        {
                            sys.SysNum = 2300;
                        }
                    }
                    else
                    {
                        sys.SysNum = 2300;
                    }

                    break;
                case 2300:
                    //[Note]:B機開始生產
                    if (IsContinueLastRun == true)
                    {
                        //[Note]:接續流程
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].Command = MCommonDefine.eSysCommand.ContinueLastRun;
                    }
                    else
                    {
                        //[Note]:正常流程
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].Command = MCommonDefine.eSysCommand.AutoRun;
                    }

                    if (mIsBypassMachineA == false)
                    {
                        sys.SysNum = 3000;
                    }
                    else
                    {
                        //[Note]:跳過A機檢查
                        sys.SysNum = 4000;
                    }

                    break;
                case 3000:
                    //[Note]:A機進料
                    if (A_IsVacuum(false) == true | IsBypassConveyor == true)
                    {
                        //[Note]:上面有料片不用再進料
                        sys.SysNum = 3200;
                    }
                    else
                    {
                        //[Note]:先給可入料之訊號
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.LoadA;
                        sys.SysNum = 3100;
                    }

                    break;
                case 3100:
                    //[Note]:A機進料完成
                    if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.LoadA && MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        //Debug.Print("A_LoadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        sys.SysNum = 3200;
                    }

                    break;
                case 3200:
                    //[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                    //[Note]:從入料完成後開始計時
                    _Machine[(int)MCommonDefine.enmMachineStation.MachineA].PriorHeatTimer.Restart();
                    if (static_Overall_AutoRunAction_mConveyorStatus == Premtek.eConveyorStatus.Station_AB | static_Overall_AutoRunAction_mConveyorStatus == Premtek.eConveyorStatus.Station_A)
                    {
                        if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.PrevDispense)
                        {
                            sys.SysNum = 3300;
                        }
                    }
                    else
                    {
                        sys.SysNum = 3300;
                    }

                    break;
                case 3300:
                    //[Note]:A機開始生產
                    if (IsContinueLastRun == true)
                    {
                        //[Note]:接續流程
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.ContinueLastRun;
                    }
                    else
                    {
                        //[Note]:正常流程
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.AutoRun;
                    }
                    if (mIsBypassMachineB == false)
                    {
                        sys.SysNum = 4000;
                    }
                    else
                    {
                        //[Note]:跳過B機檢查
                        sys.SysNum = 5000;
                    }

                    break;
                case 4000:
                    //[Note]:B機生產完成
                    if (IsContinueLastRun == true)
                    {
                        if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].ExecuteCommand == MCommonDefine.eSysCommand.ContinueLastRun)
                        {
                            //Debug.Print("B_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                            sys.SysNum = 4100;
                        }
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].ExecuteCommand == MCommonDefine.eSysCommand.AutoRun)
                        {
                            //Debug.Print("B_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                            sys.SysNum = 4100;
                        }
                    }

                    break;
                case 4100:
                    //[Note]:呼叫B機作點膠前動作(秤重含Purge)(TODO:後續要不要A機一起秤，看數值會不會跳再決定要不要做)
                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineB].Command = MCommonDefine.eSysCommand.PrevDispense;

                    if (IsBypassConveyor == false)
                    {
                        //[Note]:B機退料
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.UnloadB;
                        sys.SysNum = 4200;
                    }
                    else
                    {
                        //[Note]:B機是優先退料，所以前面不會有東西肯定為(Station_None)
                        static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_B;
                        if (mIsBypassMachineA == false)
                        {
                            sys.SysNum = 5000;
                        }
                        else
                        {
                            //[Note]:跳過A機檢查
                            sys.SysNum = 6000;
                        }
                    }

                    break;

                case 4200:
                    //[Note]:B機退料完成
                    if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.UnloadB && MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        //Debug.Print("B_UnloadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        //[Note]:B機是優先退料，所以前面不會有東西肯定為(Station_None)
                        static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_B;
                        if (mIsBypassMachineA == false)
                        {
                            sys.SysNum = 5000;
                        }
                        else
                        {
                            //[Note]:跳過A機檢查
                            sys.SysNum = 6000;
                        }
                    }

                    break;
                case 5000:
                    //[Note]:A機生產完成
                    if (IsContinueLastRun == true)
                    {
                        if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.ContinueLastRun)
                        {
                            //Debug.Print("A_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                            sys.SysNum = 5100;
                        }
                    }
                    else
                    {
                        if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].ExecuteCommand == MCommonDefine.eSysCommand.AutoRun)
                        {
                            //Debug.Print("A_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                            sys.SysNum = 5100;
                        }
                    }

                    break;

                case 5100:
                    //[Note]:呼叫A機作點膠前動作(秤重含Purge)
                    MSystemParameter.gSYS[(int)MCommonDefine.eSys.MachineA].Command = MCommonDefine.eSysCommand.PrevDispense;

                    if (IsBypassConveyor == false)
                    {
                        //[Note]:A機退料
                        MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].Command = MCommonDefine.eSysCommand.UnloadA;
                        sys.SysNum = 5200;
                    }
                    else
                    {
                        //[Note]:前面只有進B機料片，所以狀態只有可能是(Station_B or Station_None)
                        switch (static_Overall_AutoRunAction_mConveyorStatus)
                        {
                            case Premtek.eConveyorStatus.Station_B:
                                static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_AB;

                                break;
                            case Premtek.eConveyorStatus.Station_None:
                                static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_A;
                                break;
                        }
                        sys.SysNum = 6000;
                    }

                    break;

                case 5200:
                    //[Note]:A機退料完成
                    //[Note]:點膠前動作(秤重含Purge)完成再下一個迴圈做確認
                    if (MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].ExecuteCommand == MCommonDefine.eSysCommand.UnloadA && MSystemParameter.gSYS[(int)MCommonDefine.eSys.Conveyor1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        //Debug.Print("A_UnloadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        //[Note]:前面只有進B機料片，所以狀態只有可能是(Station_B or Station_None)
                        switch (static_Overall_AutoRunAction_mConveyorStatus)
                        {
                            case Premtek.eConveyorStatus.Station_B:
                                static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_AB;

                                break;
                            case Premtek.eConveyorStatus.Station_None:
                                static_Overall_AutoRunAction_mConveyorStatus = Premtek.eConveyorStatus.Station_A;
                                break;
                        }
                        sys.SysNum = 6000;
                    }

                    break;
                case 6000:
                    //[Note]:完成-->從頭再來吧(無窮迴圈)
                    //jimmy add 20170424
                    ClearGGFile("GG");
                    if (IsContinueLastRun == true)
                    {
                        IsContinueLastRun = false;

                        SaveContinueLastRun(System.Windows.Forms.Application.StartupPath + "\\System\\" + MachineName + "\\SysParam.ini");
                        //[Note]:整個流程跑一次就結束
                        sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.Finish;
                        sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                        return;
                    }
                    else
                    {
                        if (IsJustOneRun == true)
                        {
                            //[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                            sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.Finish;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                            return;
                        }
                        else
                        {
                            sys.SysNum = 1500;
                        }
                    }

                    break;
            }

        }

        void SaveContinueLastRun(string fileName)
        {
            CIni.SaveIniString("WorkSize", "ContinueLastRun", Convert.ToInt32(this.IsContinueLastRun), fileName);
        }

        void ClearGGFile(string fileName)
        {
            string mFileDirectory = null;
            //[檔案目錄]
            string mFileName = null;
            //[檔案名稱]



            //[說明]:檢查目錄是否存在
            mFileDirectory = "D:\\PIIMappingData\\";
            if (System.IO.Directory.Exists(mFileDirectory) == false)
            {
                //[說明]:目錄不存在就建目錄
                System.IO.Directory.CreateDirectory(mFileDirectory);
            }

            mFileName = "\\" + fileName + ".txt";
            //[說明]:檢查檔案是否存在
            System.IO.File.Delete(mFileDirectory + mFileName);


        }


        /// <summary>[確認真空建立(用來檢查有無料件)]</summary>
        /// <param name="conveyor"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool IsChuckVacuumReady(MCommonDefine.eConveyor conveyor)
        {
            switch (conveyor)
            {
                case MCommonDefine.eConveyor.ConveyorNo1:

                    //[Note]:確認真空建立
                    if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady, true) == true)
                    {
                        if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady2, true) == true)
                        {
                            if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady3, true) == true)
                            {
                                if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady4, true) == true)
                                {
                                    if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady5, true) == true)
                                    {
                                        if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station2ChuckVacuumReady6, true) == true)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return false;
                case MCommonDefine.eConveyor.ConveyorNo2:
                    //[Note]:確認真空建立
                    if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady, true) == true)
                    {
                        if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady2, true) == true)
                        {
                            if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady3, true) == true)
                            {
                                if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady4, true) == true)
                                {
                                    if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady5, true) == true)
                                    {
                                        if (ProjectIO.MCommonIO.gDICollection.GetState(MCommonDefineDI.enmDI.Station3ChuckVacuumReady6, true) == true)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return false;
            }

            return false;
        }
    }


}

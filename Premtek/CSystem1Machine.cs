using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;
using ProjectIO;
using WetcoConveyor;

namespace Premtek
{
    /// <summary>Machine生產物件 A,B機分為兩個物件處理.
    /// </summary>
    public class CSystem1Machine
    {
        public delegate void MachineEvent(int MachineNo);
        public event MachineEvent MachineStart;
        public event MachineEvent MachinePause;

        /// <summary>外部配接設備訊息處理
        /// </summary>
        public Premtek.Base.CEqpMsgHandler EqpMsg { get; set; }
        /// <summary>閥2氣缸
        /// </summary>
        public List<CCylinder> _CylCollection;

        public CDICollection DICollection;
        public CDOCollection DOCollection;

        public Dictionary<string, ProjectRecipe.CPurgeParameter> gPurgeDB { get; set; }
        public Dictionary<string, ProjectRecipe.CFlowRateParameter> gFlowRateDB { get; set; }
        public CSystemParameter gSSystemParameter { get; set; }
        /// <summary>機台索引 預設A機
        /// </summary>
        private int _machineNo = 0;
        /// <summary>本機有幾個stage
        /// </summary>
        int _MachineStageCount;
        public MSystemParameter.sSysParam Sys;
        /// <summary>硬體物件配接
        /// </summary>
        /// <param name="MachineStageCount">單機Stage數</param>
        /// <param name="motion">軸卡</param>
        /// <param name="aoi">影像</param>
        /// <param name="eqpMsg">訊息</param>
        public CSystem1Machine(int MachineNo, int MachineStageCount, Premtek.Base.CMotionCollection motion, ProjectAOI.CAOICollection aoi, Premtek.Base.CEqpMsgHandler eqpMsg, CDICollection diCollection, CDOCollection doCollection)
        {
            this.EqpMsg = eqpMsg;
            this._machineNo = MachineNo;
            Sys = new MSystemParameter.sSysParam();
            PriorHeatTimer = new System.Diagnostics.Stopwatch();
            _CylCollection = new List<CCylinder>();
            _CylCollection.Clear();
            FIDs.Clear();
            DICollection = diCollection;
            DOCollection = doCollection;
            _MachineStageCount = MachineStageCount;
            for (int i = 0; i < MachineStageCount; i++)
            {
                CCylinder _Cyl = new CCylinder();
                _Cyl.DICollection = diCollection;
                _Cyl.DOCollection = doCollection;
                _CylCollection.Add(_Cyl);
                FIDs.Add(new CSystem2Stage(motion, aoi, eqpMsg, _Cyl));
            }
        }

        /// <summary>
        /// [預熱等待時間之計時器(入料完成到開始點膠的這段時間)]
        /// </summary>
        public System.Diagnostics.Stopwatch PriorHeatTimer;

        int mDispValveNo1;
        int mDispValveNo2;
        bool[] mbUseStageNo = new bool[(int)MCommonDefine.enmStage.Max];

        /// <summary>[R側是否可以開始作業(在同一層的條件下，R側優先權永遠大於L側)]
        /// </summary>
        bool gIsLSideWorking = false;

        /// <summary>外部配接使用的Recipe
        /// </summary>
        public CRecipe Recipe { get; set; }


        /// <summary>使用自動(true)/手動模式(false) Map data位址
        /// </summary>
        public bool gAutoMapPath { get; set; }

        /// <summary>[點膠是否有使用ValveNo1]</summary>
        /// <remarks></remarks>
        public bool[] gIsUseValveNo1 = new bool[(int)MCommonDefine.enmStage.Max + 1];
        /// <summary>[點膠是否有使用ValveNo2]</summary>
        /// <remarks></remarks>
        public bool[] gIsUseValveNo2 = new bool[(int)MCommonDefine.enmStage.Max + 1];

        /// <summary>[協助記錄是否途中有執行Purge的動作]</summary>
        /// <remarks></remarks>
        public bool[] gIsOnPurge = new bool[(int)MCommonDefine.enmStage.Max + 1];

        /// <summary>[L側呼叫R側閃開(退至安全區)]</summary>
        /// <remarks></remarks>
        public bool gIsRSideNeedGoToSafePos;
        /// <summary>[R側呼叫L側閃開(退至安全區)]</summary>
        /// <remarks></remarks>
        public bool gIsLSideNeedGoToSafePos;
        /// <summary>Loader Cassette A 的 Map data 位址陣列 : 自動模式下使用
        /// </summary>
        public string[] gCaseteAMapDataList = new string[30];
        /// <summary>Loader Cassette B 的 Map data 位址陣列 : 自動模式下使用
        /// </summary>
        public string[] gCaseteBMapDataList = new string[30];

        /// <summary>A機Map data位址 : 手動模式下使用
        /// </summary>
        public string gMapDataPathA;
        /// <summary>B機Map data位址 : 手動模式下使用
        /// </summary>
        public string gMapDataPathB;

        public ProjectRecipe.CMapInfo gMapInfo;   //20170612 Toby_add

        /// <summary>[料片預熱時間(入料後，需等待料片加熱一段時間後才能進行點膠)(ms)]
        /// </summary>
        public decimal PriorHeatTime;


        /// <summary>生產前 套用Recipe資料
        /// </summary>
        /// <param name="recipe"></param>
        public void SetRecipe(CRecipe recipe, MCommonDefine.enmMachineStation machineNo)
        {
            string[] _entry = new string[] { "Main", "Main2", "Main3", "Main4" };//進入點
            for (int _StageNo = 0; _StageNo <= _entry.GetUpperBound(0); _StageNo++)//每個Stage一個進入點
            {
                mbUseStageNo[_StageNo] = false;
                string _main = _entry[_StageNo];//進入點名稱
                if (recipe.Pattern.ContainsKey(_main))//進入點存在
                {
                    if (recipe.Pattern[_main].Step.Count > 0)//步驟存在
                    {
                        for (int _stepNo = 0; _stepNo < recipe.Pattern[_main].Step.Count; _stepNo++)
                        {
                            switch (recipe.Pattern[_main].Step[_stepNo].WorkType)
                            {
                                case eStepWorkType.Arc:
                                case eStepWorkType.Circle:
                                case eStepWorkType.Line:
                                case eStepWorkType.Dot:
                                    mbUseStageNo[_StageNo] = true;//要點膠    
                                    string _type = recipe.Pattern[_main].Step[_stepNo].Type;
                                    gIsUseValveNo1[_StageNo] = recipe.StepGroup[_type].Valve1.UseValve;
                                    gIsUseValveNo2[_StageNo] = recipe.StepGroup[_type].Valve2.UseValve;
                                    break;
                            }
                        }
                    }
                }


            }

            this._machineNo = (int)machineNo;
            //TODO: Recipe決定哪些Stage要動
            switch (machineNo)
            {
                case MCommonDefine.enmMachineStation.MachineA:
                    //[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                    switch (mbUseStageNo[0])
                    {
                        case true:
                            switch (mbUseStageNo[1])
                            {
                                case true:
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage1;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage2;
                                    break;
                                case false:
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage1;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage1;
                                    break;
                            }
                            break;
                        case false:
                            switch (mbUseStageNo[1])
                            {
                                case true:
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage2;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage2;
                                    break;
                                case false:
                                    //[Note]:不正常喔，不正常就配置第一組
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage1;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage1;
                                    break;
                            }
                            break;
                    }
                    break;
                case MCommonDefine.enmMachineStation.MachineB:
                    switch (mbUseStageNo[2])
                    {
                        case true:
                            switch (mbUseStageNo[3])
                            {
                                case true:
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage3;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage4;
                                    break;
                                case false:
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage3;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage3;
                                    break;
                            }
                            break;
                        case false:
                            switch (mbUseStageNo[3])
                            {
                                case true:
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage4;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage4;

                                    break;
                                case false:
                                    //[Note]:不正常喔，不正常就配置第一組
                                    mDispValveNo1 = MCommonDefine.eSys.DispStage3;
                                    mDispValveNo2 = MCommonDefine.eSys.DispStage3;
                                    break;
                            }
                            break;
                    }
                    break;
            }
            //防撞保護開啟
            switch (machineNo)
            {
                case MCommonDefine.enmMachineStation.MachineA:
                    switch (MSystemParameter.gSSystemParameter.MachineType)
                    {
                        case MCommonDefine.enmMachineType.DCSW_800AQ:
                        case MCommonDefine.enmMachineType.eDTS_2S2V:

                            //如果只有左側作業時
                            if ((mbUseStageNo[0] == true) && (mbUseStageNo[1] == false))
                            {
                                gIsLSideWorking = true;
                            }
                            else
                            {
                                gIsLSideWorking = false;
                            }

                            break;

                        default:
                            gIsLSideWorking = true;

                            break;
                    }

                    break;
                case MCommonDefine.enmMachineStation.MachineB:
                    //如果只有左側作業時
                    if ((mbUseStageNo[2] == true) && (mbUseStageNo[3] == false))
                    {
                        gIsLSideWorking = true;
                    }
                    else
                    {
                        gIsLSideWorking = false;
                    }

                    break;

            }

        }

        /// <summary>定位流程
        /// </summary>
        List<CSystem2Stage> FIDs;

        /// <summary>定位用Map
        /// </summary>
        List<CRecipe> _FIDsMap;
        /// <summary>測高用Map
        /// </summary>
        List<CRecipe> _FHsMap;
        /// <summary>點膠用Map
        /// </summary>
        List<CRecipe> _DispMap;

        /// <summary>[機台端生產流程(LevelNo2)]</summary>
        /// <param name="sys"></param>
        /// <remarks></remarks>
        public void Machine_AutoRun(ref MSystemParameter.sSysParam sys)
        {
            try
            {
                switch (sys.SysNum)
                {
                    case MSystemParameter.sSysParam.SysLoopStart:

                        //jimmy add 20170424
                        //for (index = enmStage.No1; index <= MSystemParameter. gSSystemParameter.StageCount - 1; index++) {
                        //    if (gCRecipe.Node(index).Count > 0) {
                        //        mbUseStageNo[index] = true;
                        //    } else {
                        //        mbUseStageNo[index] = false;
                        //    }
                        //}

                        sys.SysNum = 1500;

                        break;

                    case 1500:
                        //[Note]:沒有MapData
                        //[Note]:ReDim
                        switch (sys.MachineNo)
                        {
                            case MCommonDefine.enmMachineStation.MachineA:
                                if (MSystemParameter.gSSystemParameter.StageCount == 1)
                                {

                                    //gCRecipe.Initial_StageMap(enmStage.No1, MSystemParameter.gSSystemParameter.IsBypassCCD, sys.ConveyorNo);
                                }
                                else if (MSystemParameter.gSSystemParameter.StageCount >= 2)
                                {
                                    //gCRecipe.Initial_StageMap(enmStage.No1, MSystemParameter.gSSystemParameter.IsBypassCCD, sys.ConveyorNo);
                                    //gCRecipe.Initial_StageMap(enmStage.No2, MSystemParameter.gSSystemParameter.IsBypassCCD, sys.ConveyorNo);
                                }

                                break;
                            case MCommonDefine.enmMachineStation.MachineB:
                                //gCRecipe.Initial_StageMap(enmStage.No3, MSystemParameter.gSSystemParameter.IsBypassCCD, sys.ConveyorNo);
                                //gCRecipe.Initial_StageMap(enmStage.No4, MSystemParameter.gSSystemParameter.IsBypassCCD, sys.ConveyorNo);
                                break;
                        }

                        //[Note]:Map Data處理

                        if (MSystemParameter.gSSystemParameter.IsCompareWithMapData != 0)
                        {
                            string path = "";
                            if ((sys.MachineNo == MCommonDefine.enmMachineStation.MachineA))
                            {
                                if ((gAutoMapPath))
                                {
                                    if ((200 > mGlobalPool.cls800AQ_LUL.A_ProductNum) && (mGlobalPool.cls800AQ_LUL.A_ProductNum > 99))
                                    {
                                        path = gCaseteAMapDataList[(mGlobalPool.cls800AQ_LUL.A_ProductNum % 100) - 1];
                                    }
                                    else
                                    {
                                        path = gCaseteBMapDataList[(mGlobalPool.cls800AQ_LUL.A_ProductNum % 100) - 1];
                                    }
                                }
                                else
                                {
                                    path = gMapDataPathA;
                                }
                            }
                            else if ((sys.MachineNo == MCommonDefine.enmMachineStation.MachineB))
                            {
                                if ((gAutoMapPath))
                                {
                                    if ((200 > mGlobalPool.cls800AQ_LUL.B_ProductNum) && (mGlobalPool.cls800AQ_LUL.B_ProductNum > 99))
                                    {
                                        path = gCaseteAMapDataList[(mGlobalPool.cls800AQ_LUL.B_ProductNum % 100) - 1];
                                    }
                                    else
                                    {
                                        path = gCaseteBMapDataList[(mGlobalPool.cls800AQ_LUL.B_ProductNum % 100) - 1];
                                    }
                                }
                                else
                                {
                                    path = gMapDataPathB;
                                }
                            }

                            if (CoverMapData(sys.MachineNo, path) == true)
                            {
                                sys.SysNum = 1900;
                                //Toby_Modify_20170103
                            }
                            else
                            {
                                sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                                if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1025002", "Machine_AutoRun", sys.SysNum.ToString(), MDateLog.gMsgHandler.GetMessage(EqpID.Error_1025002), eMessageLevel.Warning);
                                return;
                            }

                        }
                        else
                        {
                            sys.SysNum = 1900;
                        }

                        break;

                    case 1900:
                        // 手動設定Map 
                        //Toby add for test_Start
                        if (MSystemParameter.gSSystemParameter.IsManualMap == false)
                        {
                            sys.SysNum = 2000;
                        }
                        else
                        {
                            //20170612_Toby
                            gMapInfo = new ProjectRecipe.CMapInfo();

                            switch (MSystemParameter.gSSystemParameter.MachineType)
                            {
                                case MCommonDefine.enmMachineType.DCSW_800AQ:
                                    switch (sys.MachineNo)
                                    {
                                        case MCommonDefine.enmMachineStation.MachineA:
                                            //節點Pattern有數量
                                            //if (gStageMap(0).Node.Count > 0 | gStageMap(1).Node.Count > 0)
                                            //{
                                            //    //判斷Recipe是否有stage Node資料
                                            //    //Machine A
                                            //    frmModifyDie ChangeMap = new frmModifyDie(gMapInfo.gDrewMapPos_L);
                                            //    ChangeMap.Text = "Machine A";
                                            //    ChangeMap.ShowDialog();
                                            //}
                                            sys.SysNum = 2000;
                                            break;
                                        case MCommonDefine.enmMachineStation.MachineB:
                                            //節點Pattern有數量
                                            //if (gStageMap(2).Node.Count > 0 | gStageMap(3).Node.Count > 0)
                                            //{
                                            //    frmModifyDie ChangeMap = new frmModifyDie(gMapInfo.gDrewMapPos_R);
                                            //    ChangeMap.Text = "Machine B";
                                            //    ChangeMap.ShowDialog();
                                            //}
                                            sys.SysNum = 2000;
                                            break;
                                    }

                                    break;
                                default:
                                    //節點Pattern有數量
                                    //if (gStageMap(0).Node.Count > 0)
                                    //{
                                    //    frmModifyDie ChangeMap = new frmModifyDie(gMapInfo.gDrewMapPos_L);
                                    //    ChangeMap.Text = "Map";
                                    //    ChangeMap.ShowDialog();
                                    sys.SysNum = 2000;
                                    //}
                                    break;
                            }
                        }
                        break;
                    //Toby add for test_end

                    case 2000:
                        //[Note]:ProductLoading流程
                        for (int mStageMax = mDispValveNo1; mStageMax <= mDispValveNo2; mStageMax++)
                        {
                            MSystemParameter.gSYS[mStageMax].Command = MCommonDefine.eSysCommand.ProductLoading;
                        }

                        sys.SysNum = 2100;

                        break;
                    case 2100:
                        //[Note]:判斷ProductLoading完成
                        if ((MSystemParameter.gSYS[mDispValveNo1].RunStatus == MSystemParameter.enmRunStatus.Finish) && (MSystemParameter.gSYS[mDispValveNo1].ExecuteCommand == MCommonDefine.eSysCommand.ProductLoading))
                        {
                            if ((MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish) && (MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.ProductLoading))
                            {
                                sys.SysNum = 2200;
                            }
                        }
                        break;

                    case 2200:
                        //[Note]:防撞保護再次開啟(Scan)

                        gIsRSideNeedGoToSafePos = false;
                        gIsLSideNeedGoToSafePos = false;
                        sys.SysNum = 3000;

                        break;
                    case 3000:
                        //[Note]:Scan流程
                        for (int mStageMax = mDispValveNo1; mStageMax <= mDispValveNo2; mStageMax++)
                        {
                            FIDs[(int)sys.StageNo].Input = _FIDsMap[(int)sys.StageNo];
                            FIDs[(int)sys.StageNo].Output = _FHsMap[(int)sys.StageNo];
                            MSystemParameter.gSYS[mStageMax].Command = MCommonDefine.eSysCommand.CCDFix;

                        }

                        sys.SysNum = 3100;

                        break;
                    case 3100:

                        //[Note]:判斷Scan完成
                        if (MSystemParameter.gSYS[mDispValveNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispValveNo1].ExecuteCommand == MCommonDefine.eSysCommand.CCDFix)
                        {
                            if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.CCDFix)
                            {
                                sys.SysNum = 3200;
                            }
                        }

                        break;
                    case 3200:
                        //[Note]:防撞保護再次開啟(Laser)

                        gIsRSideNeedGoToSafePos = false;
                        gIsLSideNeedGoToSafePos = false;
                        sys.SysNum = 4000;

                        break;
                    case 4000:
                        //[Note]:Laser Reader測高流程
                        for (int mStageMax = mDispValveNo1; mStageMax <= mDispValveNo2; mStageMax++)
                        {
                            MSystemParameter.gSYS[mStageMax].Command = MCommonDefine.eSysCommand.LaserReader;
                        }
                        sys.SysNum = 4100;
                        break;

                    case 4100:
                        //[Note]:判斷Laser Reader完成
                        if (MSystemParameter.gSYS[mDispValveNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispValveNo1].ExecuteCommand == MCommonDefine.eSysCommand.LaserReader)
                        {
                            if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.LaserReader)
                            {
                                sys.SysNum = 4200;
                            }
                        }
                        break;

                    case 4200:
                        //[Note]:防撞保護再次開啟(Laser)
                        switch (sys.MachineNo)
                        {
                            case MCommonDefine.enmMachineStation.MachineA:
                                switch (MSystemParameter.gSSystemParameter.MachineType)
                                {
                                    case MCommonDefine.enmMachineType.DCSW_800AQ:
                                    case MCommonDefine.enmMachineType.eDTS_2S2V:

                                        //如果只有左側作業時
                                        if (mbUseStageNo[0] == true & mbUseStageNo[1] == false)
                                        {
                                            gIsLSideWorking = true;
                                        }
                                        else
                                        {
                                            gIsLSideWorking = false;
                                        }
                                        break;

                                    default:
                                        gIsLSideWorking = true;
                                        break;
                                }
                                break;

                            case MCommonDefine.enmMachineStation.MachineB:

                                //如果只有左側作業時
                                if (mbUseStageNo[2] == true & mbUseStageNo[3] == false)
                                {
                                    gIsLSideWorking = true;
                                }
                                else
                                {
                                    gIsLSideWorking = false;
                                }

                                break;
                        }
                        gIsRSideNeedGoToSafePos = false;
                        gIsLSideNeedGoToSafePos = false;
                        sys.SysNum = 4300;

                        break;
                    case 4300:
                        //[Note]:判斷有無使用第二組閥
                        if (gIsUseValveNo2[(int)MSystemParameter.gSYS[mDispValveNo1].StageNo] == true | gIsUseValveNo2[(int)MSystemParameter.gSYS[mDispValveNo2].StageNo] == true)
                        {
                            if (gIsUseValveNo2[(int)MSystemParameter.gSYS[mDispValveNo1].StageNo] == true)
                            {
                                MSystemParameter.gSYS[mDispValveNo1].SelectValve = MCommonDefine.eValveWorkMode.Valve2;
                            }
                            if (gIsUseValveNo2[(int)MSystemParameter.gSYS[mDispValveNo2].StageNo] == true)
                            {
                                MSystemParameter.gSYS[mDispValveNo2].SelectValve = MCommonDefine.eValveWorkMode.Valve2;
                            }
                            sys.SysNum = 5000;
                        }
                        else
                        {
                            sys.SysNum = 5300;
                        }

                        break;
                    case 5000:
                        //[Note]:Purge流程
                        //       開啟Pre-Dispense Purge
                        //for (int mDispStage = mDispValveNo1; mDispStage <= mDispValveNo2; mDispStage++) {
                        //    if (gPurgeDB.ContainsKey(gCRecipe.StageParts(sys.StageNo).PurgeName[(int)MSystemParameter.gSYS[mDispStage].SelectValve]) == true) {
                        //        if (gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName[(int)MSystemParameter.gSYS[mDispStage].SelectValve]).IsPreDispenePurge == true) {
                        //            MSystemParameter.gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                        //            MSystemParameter.gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = Now;
                        //            MSystemParameter.gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName[(int)MSystemParameter.gSYS[mDispStage].SelectValve]).OnRuns;
                        //            MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                        //            //[Note]:協助記錄中途是否做過Purge
                        //            switch (sys.MachineNo) {
                        //                case MCommonDefine.enmMachineStation.MachineA:
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage1) {
                        //                        gIsOnPurge[(int)MCommonDefine.enmStage.No1] = true;
                        //                    }
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage2) {
                        //                        gIsOnPurge[(int)MCommonDefine.enmStage.No2] = true;
                        //                    }
                        //                    break;
                        //                case MCommonDefine.enmMachineStation.MachineB:
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage1) {
                        //                        gIsOnPurge[(int)MCommonDefine.enmStage.No3] = true;
                        //                    }
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage2) {
                        //                        gIsOnPurge[(int)MCommonDefine.enmStage.No4]= true;
                        //                    }
                        //                    break;
                        //            }
                        //        }
                        //    }
                        //}

                        sys.SysNum = 5100;

                        break;
                    case 5100:
                        //[Note]:判斷Purge完成
                        if (MSystemParameter.gSYS[mDispValveNo1].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                        {
                            if (MSystemParameter.gSYS[mDispValveNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                            {
                                if (MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                                {
                                    if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                    {
                                        sys.SysNum = 5200;
                                    }
                                }
                                else
                                {
                                    sys.SysNum = 5200;
                                }
                            }
                        }
                        else
                        {
                            if (MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                            {
                                if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                {
                                    sys.SysNum = 5200;
                                }
                            }
                            else
                            {
                                sys.SysNum = 5200;
                            }
                        }

                        break;
                    case 5200:
                        //[Note]:判斷有無使用第一組閥
                        if (gIsUseValveNo1[(int)MSystemParameter.gSYS[mDispValveNo1].StageNo] == true | gIsUseValveNo1[(int)MSystemParameter.gSYS[mDispValveNo2].StageNo] == true)
                        {
                            if (gIsUseValveNo1[(int)MSystemParameter.gSYS[mDispValveNo1].StageNo] == true)
                            {
                                MSystemParameter.gSYS[mDispValveNo1].SelectValve = MCommonDefine.eValveWorkMode.Valve1;
                            }
                            if (gIsUseValveNo1[(int)MSystemParameter.gSYS[mDispValveNo2].StageNo] == true)
                            {
                                MSystemParameter.gSYS[mDispValveNo2].SelectValve = MCommonDefine.eValveWorkMode.Valve1;
                            }
                            sys.SysNum = 5300;
                        }
                        else
                        {
                            sys.SysNum = 5500;
                        }

                        break;
                    case 5300:
                        //[Note]:Purge流程
                        //       開啟Pre-Dispense Purge
                        //for (int mDispStage = mDispValveNo1; mDispStage <= mDispValveNo2; mDispStage++) {
                        //    if (gPurgeDB.ContainsKey(gCRecipe.StageParts(sys.StageNo).PurgeName[(int)MSystemParameter.gSYS[mDispStage].SelectValve]) == true) {
                        //        if (gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName[(int)MSystemParameter.gSYS[mDispStage].SelectValve]).IsPreDispenePurge == true) {
                        //            MSystemParameter.gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                        //            MSystemParameter.gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = Now;
                        //            MSystemParameter.gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName[(int)MSystemParameter.gSYS[mDispStage].SelectValve]).OnRuns;
                        //            MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                        //            //[Note]:協助記錄中途是否做過Purge
                        //            switch (sys.MachineNo) {
                        //                case MCommonDefine.enmMachineStation.MachineA:
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage1) {
                        //                        gIsOnPurge(enmStage.No1) = true;
                        //                    }
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage2) {
                        //                        gIsOnPurge(enmStage.No2) = true;
                        //                    }
                        //                    break;
                        //                case MCommonDefine.enmMachineStation.MachineB:
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage1) {
                        //                        gIsOnPurge(enmStage.No3) = true;
                        //                    }
                        //                    if (mDispStage == MCommonDefine.eSys.DispStage2) {
                        //                        gIsOnPurge(enmStage.No4) = true;
                        //                    }
                        //                    break;
                        //            }
                        //        }
                        //    }
                        //}

                        sys.SysNum = 5400;

                        break;
                    case 5400:
                        //[Note]:判斷Purge完成
                        if (MSystemParameter.gSYS[mDispValveNo1].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                        {
                            if (MSystemParameter.gSYS[mDispValveNo1].RunStatus == MSystemParameter.enmRunStatus.Finish)
                            {
                                if (MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                                {
                                    if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                    {
                                        sys.SysNum = 5500;
                                    }
                                }
                                else
                                {
                                    sys.SysNum = 5500;
                                }
                            }
                        }
                        else
                        {
                            if (MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                            {
                                if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish)
                                {
                                    sys.SysNum = 5500;
                                }
                            }
                            else
                            {
                                sys.SysNum = 5500;
                            }
                        }

                        break;
                    case 5500:
                        //[Note]:等待料片預熱時間，時間到了才可以進入點膠流程   

                        if (IsWaitPriorHeat(sys.MachineNo, sys.ConveyorNo, PriorHeatTimer, this.PriorHeatTime) == true)
                        {
                            sys.SysNum = 6000;
                        }

                        break;
                    case 6000:
                        //[Note]:Dispensing流程
                        for (int mStageMax = mDispValveNo1; mStageMax <= mDispValveNo2; mStageMax++)
                        {
                            MSystemParameter.gSYS[mStageMax].Command = MCommonDefine.eSysCommand.Dispensing;
                        }

                        sys.SysNum = 6100;

                        break;
                    case 6100:
                        //[Note]:判斷Dispensing完成
                        if (MSystemParameter.gSYS[mDispValveNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispValveNo1].ExecuteCommand == MCommonDefine.eSysCommand.Dispensing)
                        {
                            if (MSystemParameter.gSYS[mDispValveNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispValveNo2].ExecuteCommand == MCommonDefine.eSysCommand.Dispensing)
                            {
                                sys.SysNum = 9000;
                            }
                        }

                        break;
                    case 9000:
                        //[Note]:完成生產
                        sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                        return;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                MDateLog.gSyslog.Save(ex.Message + " " + ex.StackTrace, "", eMessageLevel.Error);
                sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                return;
            }
        }

        /// <summary>是否預熱等待時間到
        /// </summary>
        /// <param name="machineNo"></param>
        /// <param name="conveyor"></param>
        /// <param name="stopwatch"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        bool IsWaitPriorHeat(MCommonDefine.enmMachineStation machineNo, MCommonDefine.eConveyor conveyor, System.Diagnostics.Stopwatch stopwatch, decimal time)
        {

            if (stopwatch.ElapsedMilliseconds > Convert.ToInt64(time))
            {
                stopwatch.Stop();
                return true;
            }
            else
            {
                System.Diagnostics.Debug.Print("Wait Time: " + stopwatch.ElapsedMilliseconds);
                return false;
            }

        }

        /// <summary>
        /// [Mapping Data轉換處理]
        /// </summary>
        /// <param name="machineNo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool CoverMapData(MCommonDefine.enmMachineStation machineNo, string path)
        {
            //MCommonDefine.enmStage mStageNo1 = default(MCommonDefine.enmStage);
            //MCommonDefine.enmStage mStageNo2 = default(MCommonDefine.enmStage);

            try
            {
                //    switch (machineNo) {
                //        case MCommonDefine.enmMachineStation.MachineA:
                //            mStageNo1 = MCommonDefine.enmStage.No1;
                //            mStageNo2 = MCommonDefine.enmStage.No2;

                //            break;
                //        case MCommonDefine.enmMachineStation.MachineB:
                //            mStageNo1 = MCommonDefine.enmStage.No3;
                //            mStageNo2 = MCommonDefine.enmStage.No4;
                //            break;
                //    }

                //    //[Note]:根據進料的資訊、取出Wafer Map的檔案路徑、名稱
                //    if ((MapToData(machineNo, path) == false)) {
                //        return false;
                //    }

                //    //'[Note]:取得Notch方向
                //    gMapData(machineNo).ResetMapNotch(gCRecipe.NotchDir(machineNo));

                //    //[Note]:比對Mapping Data是否相符, 無此資訊則不比對
                //    if (gMapData(machineNo).Information.Type != "N/A" & !string.IsNullOrEmpty(gMapData(machineNo).Information.Type)) {
                //        if (gMapData(machineNo).Information.Type != gCRecipe.ProductType) {
                //            return false;
                //        }
                //    }

                //    //[Note]:合併NodeToMap裡的Node, 取得總陣列大小
                //    int maxColumn = 1000;
                //    int maxRow = 1000;
                //    int[,] trayArray = new int[maxColumn + 1, maxRow + 1];
                //    if ((gCRecipe.NodeToMap(mStageNo1) != null)) {
                //        foreach (void node2Map_loopVariable in gCRecipe.NodeToMap(mStageNo1)) {
                //            node2Map = node2Map_loopVariable;
                //            if (gCRecipe.Node(mStageNo1).ContainsKey(node2Map)) {
                //                int x = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingX - 1;
                //                int y = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingY - 1;
                //                CMultiArrayAdapter nodeArray = new CMultiArrayAdapter(gCRecipe.Node(mStageNo1)(node2Map).Array);
                //                int column = nodeArray.GetMemoryCountX;
                //                int row = nodeArray.GetMemoryCountY;
                //                for (c = 0; c <= column - 1; c++) {
                //                    for (r = 0; r <= row - 1; r++) {
                //                        trayArray(x + c, y + r) += 1;
                //                    }
                //                }
                //            }
                //        }
                //    }

                //    if ((gCRecipe.NodeToMap(mStageNo2) != null)) {
                //        foreach (void node2Map_loopVariable in gCRecipe.NodeToMap(mStageNo2)) {
                //            node2Map = node2Map_loopVariable;
                //            if (gCRecipe.Node(mStageNo2).ContainsKey(node2Map)) {
                //                int x = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingX - 1;
                //                int y = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingY - 1;
                //                CMultiArrayAdapter nodeArray = new CMultiArrayAdapter(gCRecipe.Node(mStageNo2)(node2Map).Array);
                //                int column = nodeArray.GetMemoryCountX;
                //                int row = nodeArray.GetMemoryCountY;
                //                for (c = 0; c <= column - 1; c++) {
                //                    for (r = 0; r <= row - 1; r++) {
                //                        trayArray(x + c, y + r) += 1;
                //                    }
                //                }
                //            }
                //        }
                //    }


                //    int nodeColumn = 0;
                //    for (i = 0; i <= maxColumn; i++) {
                //        if ((trayArray(i, 0) != 1)) {
                //            nodeColumn = i;
                //            break; // TODO: might not be correct. Was : Exit For
                //        }
                //    }

                //    int nodeRow = 0;
                //    for (i = 0; i <= maxRow; i++) {
                //        if ((trayArray(0, i) != 1)) {
                //            nodeRow = i;
                //            break; // TODO: might not be correct. Was : Exit For
                //        }
                //    }

                //    //[Note]:比對Map與Node List陣列大小
                //    if (((gMapData(machineNo).Substrates(0).Columns != nodeColumn) | (gMapData(machineNo).Substrates(0).Rows != nodeRow))) {
                //        return false;
                //    }

                //    //[Note]:檢查Node陣列是否為一個完整的矩形陣列,並排除重複(value > 1)或遺漏(value = 0)的地方
                //    for (c = 0; c <= nodeColumn - 1; c++) {
                //        for (r = 0; r <= nodeRow - 1; r++) {
                //            if ((trayArray(c, r) != 1)) {
                //                return false;
                //            }
                //        }
                //    }

                //    //[Note]:將MappingData丟入StageMap
                //    if ((gCRecipe.NodeToMap(mStageNo1) != null)) {
                //        foreach (void node_loopVariable in gCRecipe.NodeToMap(mStageNo1)) {
                //            node = node_loopVariable;
                //            if (MapDataCoverToStageMap(gMapData(machineNo), gStageMap(mStageNo1).Node(node), gCRecipe.Node(mStageNo1)(node)) == false) {
                //                return false;
                //            }
                //        }
                //    }

                //    //[Note]:將MappingData丟入StageMap
                //    if ((gCRecipe.NodeToMap(mStageNo2) != null)) {
                //        foreach (void node_loopVariable in gCRecipe.NodeToMap(mStageNo2)) {
                //            node = node_loopVariable;
                //            if (MapDataCoverToStageMap(gMapData(machineNo), gStageMap(mStageNo2).Node(node), gCRecipe.Node(mStageNo2)(node)) == false) {
                //                return false;
                //            }
                //        }
                //    }

                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>DCS-W800AQ機台復歸流程</summary>
        /// <param name="sys"></param>
        /// <remarks>雙機, 每機雙Stage</remarks>
        public void Home_800AQ(MSystemParameter.sSysParam sys)
        {
            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    //gMachineIsManual = False
                    switch (sys.MachineNo)
                    {
                        case MCommonDefine.enmMachineStation.MachineA:
                            //[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                            mDispStageNo1 = MCommonDefine.eSys.DispStage1;
                            mDispStageNo2 = MCommonDefine.eSys.DispStage2;
                            break;
                        case MCommonDefine.enmMachineStation.MachineB:
                            mDispStageNo1 = MCommonDefine.eSys.DispStage3;
                            mDispStageNo2 = MCommonDefine.eSys.DispStage4;

                            break;
                    }

                    //[Note]:先清除狀態
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.None;
                    sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.None;
                    for (int mStageNo = mDispStageNo1; mStageNo <= mDispStageNo2; mStageNo++)
                    {
                        MSystemParameter.gSYS[mStageNo].RunStatus = MSystemParameter.enmRunStatus.None;
                    }


                    sys.SysNum = 1200;

                    break;

                case 1200:
                    //[Note]:復歸
                    for (int mStageNo = mDispStageNo1; mStageNo <= mDispStageNo2; mStageNo++)
                    {
                        MSystemParameter.gSYS[mStageNo].Command = MCommonDefine.eSysCommand.Home;
                    }

                    sys.SysNum = 1300;

                    break;
                case 1300:
                    //[Note]:判斷復歸完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                    {
                        if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                        {
                            sys.SysNum = 9000;
                            //Home Error
                        }
                        else if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                        {
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        }
                        //Home Error
                    }
                    else if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                    {
                        sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                    }

                    break;
                case 9000:
                    //[Note]:完成復歸
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Finish;
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    return;
            }
        }

        /// <summary>DTS-400A機台復歸流程
        /// </summary>
        /// <param name="sys"></param>
        /// <remarks>單機, 每機雙Stage</remarks>
        public void Home_2S2V(MSystemParameter.sSysParam sys)
        {
            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    //gMachineIsManual = False
                    switch (sys.MachineNo)
                    {
                        case MCommonDefine.enmMachineStation.MachineA:
                            mDispStageNo1 = MCommonDefine.eSys.DispStage1;
                            mDispStageNo2 = MCommonDefine.eSys.DispStage2;
                            break;
                        case MCommonDefine.enmMachineStation.MachineB:
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Finish;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                            return;
                    }

                    //[Note]:先清除狀態
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.None;
                    sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.None;
                    for (int mStageNo = mDispStageNo1; mStageNo <= mDispStageNo2; mStageNo++)
                    {
                        MSystemParameter.gSYS[mStageNo].RunStatus = MSystemParameter.enmRunStatus.None;
                    }

                    sys.SysNum = 1200;

                    break;

                case 1200:
                    //[Note]:復歸
                    for (int mStageNo = mDispStageNo1; mStageNo <= mDispStageNo2; mStageNo++)
                    {
                        MSystemParameter.gSYS[mStageNo].Command = MCommonDefine.eSysCommand.Home;
                    }

                    sys.SysNum = 1300;

                    break;
                case 1300:
                    //[Note]:判斷復歸完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                    {
                        if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                        {
                            sys.SysNum = 9000;
                            //Home Error
                        }
                        else if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                        {
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        }
                        //Home Error
                    }
                    else if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                    {
                        sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                    }

                    break;
                case 9000:
                    //[Note]:完成復歸
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Finish;
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    return;


            }
        }

        /// <summary>DCS-F230A機台復歸流程
        /// </summary>
        /// <param name="sys"></param>
        /// <remarks>單機,每機單Stage</remarks>
        public void Machine_Home(ref MSystemParameter.sSysParam sys)
        {

            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    //gMachineIsManual = False
                    switch (sys.MachineNo)
                    {
                        case MCommonDefine.enmMachineStation.MachineA:

                            mDispStageNo1 = MCommonDefine.eSys.DispStage1;
                            mDispStageNo2 = MCommonDefine.eSys.DispStage1;

                            break;
                        case MCommonDefine.enmMachineStation.MachineB:
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Finish;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                            return;
                    }

                    //[Note]:先清除狀態
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.None;
                    sys.Act[MCommonDefine.eAct.AutoRun].RunStatus = MSystemParameter.enmRunStatus.None;
                    for (int mStageNo = mDispStageNo1; mStageNo <= mDispStageNo2; mStageNo++)
                    {
                        MSystemParameter.gSYS[mStageNo].RunStatus = MSystemParameter.enmRunStatus.None;
                    }

                    sys.SysNum = 1200;

                    break;

                case 1200:
                    //[Note]:復歸
                    for (int mStageNo = mDispStageNo1; mStageNo <= mDispStageNo2; mStageNo++)
                    {
                        MSystemParameter.gSYS[mStageNo].Command = MCommonDefine.eSysCommand.Home;
                    }

                    sys.SysNum = 1300;

                    break;
                case 1300:
                    //[Note]:判斷復歸完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish && MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                    {
                        if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Home)
                        {
                            sys.SysNum = 9000;
                            //Home Error
                        }
                        else if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                        {
                            sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        }
                        //Home Error
                    }
                    else if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Alarm)
                    {
                        sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                    }

                    break;
                case 9000:
                    //[Note]:完成復歸
                    sys.Act[MCommonDefine.eAct.Home].RunStatus = MSystemParameter.enmRunStatus.Finish;
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    return;

            }

        }

        /// <summary>[紀錄左側Stage對應的esys.DispStage]
        /// </summary>
        int mDispStageNo1;
        /// <summary>[紀錄右側Stage對應的esys.DispStage]
        /// </summary>
        int mDispStageNo2;
        /// <summary>[紀錄左側Stage是否需要做Purge]
        /// </summary>
        bool mIsPurgeDispStageNo1;
        /// <summary>[紀錄右側Stage是否需要做Purge]
        /// </summary>
        bool mIsPurgeDispStageNo2;
        /// <summary>[紀錄需不需要做秤重]
        /// </summary>
        bool mIsNeedFlowRate;

        void Machine_PrevDispense(ref MSystemParameter.sSysParam sys)
        {
            bool _IsStage1UseValve1;
            bool _IsStage2UseValve1;
            bool _IsStage1UseValve2;
            bool _IsStage2UseValve2;

            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:

                    for (int index = 0; index < this._MachineStageCount; index++)
                    {
                        //if Recipe.Stage[index].Main
                        if (Recipe.Pattern[Recipe.Machine[index].Main].Step.Count > 0)
                        {
                            mbUseStageNo[index] = true;
                        }
                        else
                        {
                            mbUseStageNo[index] = false;
                        }
                    }

                    switch (sys.MachineNo)
                    {
                        case MCommonDefine.enmMachineStation.MachineA:
                            //[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                            switch (mbUseStageNo[0])
                            {
                                case true:
                                    switch (mbUseStageNo[1])
                                    {
                                        case true:
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage1;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage2;

                                            break;
                                        case false:
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage1;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage1;

                                            break;
                                    }

                                    break;
                                case false:
                                    switch (mbUseStageNo[1])
                                    {
                                        case true:
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage2;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage2;

                                            break;
                                        case false:
                                            //[Note]:不正常喔，不正常就配置第一組
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage1;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage1;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case MCommonDefine.enmMachineStation.MachineB:
                            switch (mbUseStageNo[2])
                            {
                                case true:
                                    switch (mbUseStageNo[3])
                                    {
                                        case true:
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage3;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage4;

                                            break;
                                        case false:
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage3;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage3;

                                            break;
                                    }

                                    break;
                                case false:
                                    switch (mbUseStageNo[3])
                                    {
                                        case true:
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage4;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage4;

                                            break;
                                        case false:
                                            //[Note]:不正常喔，不正常就配置第一組
                                            mDispStageNo1 = MCommonDefine.eSys.DispStage3;
                                            mDispStageNo2 = MCommonDefine.eSys.DispStage3;
                                            break;
                                    }

                                    break;
                            }

                            break;
                    }
                    mIsNeedFlowRate = false;
                    sys.SysNum = 2000;

                    break;
                case 2000:
                    //[Note]:判斷有無使用第二組閥
                    //[Note]:秤重條件&Pruge條件判斷
                    //       若要秤重，強制作Purge

                    //[Note]:雙閥同動點膠的條件下，秤重只看第一組的資料，故無需對第二支閥做秤重，但Purge需要
                    if (gSSystemParameter.MultiDispenseEnable == true)
                    {
                        sys.SysNum = 4000;
                    }
                    else
                    {
                        _IsStage1UseValve2 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo1].StageNo].IsUseValve2();
                        _IsStage2UseValve2 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo2].StageNo].IsUseValve2();
                        if (_IsStage1UseValve2 == true | _IsStage2UseValve2 == true)
                        {
                            if (_IsStage1UseValve2 == true)
                            {
                                MSystemParameter.gSYS[mDispStageNo1].SelectValve = MCommonDefine.eValveWorkMode.Valve2;
                            }
                            if (_IsStage2UseValve2 == true)
                            {
                                MSystemParameter.gSYS[mDispStageNo2].SelectValve = MCommonDefine.eValveWorkMode.Valve2;
                            }
                            sys.SysNum = 2100;
                        }
                        else
                        {
                            sys.SysNum = 4000;
                        }
                    }

                    break;
                case 2100:
                    //[Note]:秤重條件&Pruge條件判斷
                    //       若要秤重，強制作Purge
                    for (int mDispStage = mDispStageNo1; mDispStage <= mDispStageNo2; mDispStage++)
                    {
                        if (gFlowRateDB.ContainsKey("Default") == true)
                        {
                            switch (gFlowRateDB["Default"].BaseOn)
                            {
                                case ProjectRecipe.eInspectionType.OnTimerOrRuns:
                                    if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gFlowRateDB["Default"].OnTimer)
                                    {
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                        mIsNeedFlowRate = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                    else
                                    {
                                        if (gFlowRateDB["Default"].OnRuns != 0)
                                        {
                                            //[Note]:進入時就做一次(次數的時候)
                                            if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                            {
                                                gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                                gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                                gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                                mIsNeedFlowRate = true;
                                                break; // TODO: might not be correct. Was : Exit For
                                            }
                                        }
                                    }

                                    break;
                                case ProjectRecipe.eInspectionType.OnTimer:
                                    if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gFlowRateDB["Default"].OnTimer)
                                    {
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                        mIsNeedFlowRate = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }

                                    break;
                                case ProjectRecipe.eInspectionType.OnRuns:
                                    if (gFlowRateDB["Default"].OnRuns != 0)
                                    {
                                        //[Note]:進入時就做一次(次數的時候)
                                        if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                        {
                                            gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                            gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                            gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                            mIsNeedFlowRate = true;
                                            break; // TODO: might not be correct. Was : Exit For
                                        }
                                    }

                                    break;
                                case ProjectRecipe.eInspectionType.Noen:

                                    break;
                            }
                        }
                    }


                    if (mIsNeedFlowRate == true)
                    {
                        //[Note]:協助記錄中途是否有做過Purge的動作
                        switch (sys.MachineNo)
                        {
                            case MCommonDefine.enmMachineStation.MachineA:
                                gIsOnPurge[(int)MCommonDefine.enmStage.No1] = true;
                                gIsOnPurge[(int)MCommonDefine.enmStage.No2] = true;

                                break;
                            case MCommonDefine.enmMachineStation.MachineB:
                                gIsOnPurge[(int)MCommonDefine.enmStage.No3] = true;
                                gIsOnPurge[(int)MCommonDefine.enmStage.No4] = true;

                                break;
                        }
                        sys.SysNum = 2200;
                    }
                    else
                    {
                        sys.SysNum = 4000;
                    }

                    break;
                case 2200:
                    //[Note]:Purge流程
                    for (int mDispStage = mDispStageNo1; mDispStage <= mDispStageNo2; mDispStage++)
                    {
                        MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                    }

                    sys.SysNum = 2300;

                    break;
                case 2300:
                    //[Note]:判斷Purge完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                    {
                        if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                        {
                            sys.SysNum = 3000;
                        }
                    }

                    break;
                case 3000:
                    //[Note]:左側先秤重，完成後再換右側秤重
                    MSystemParameter.gSYS[mDispStageNo1].Command = MCommonDefine.eSysCommand.WeightUnit;
                    sys.SysNum = 3200;

                    break;
                case 3200:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.WeightUnit)
                    {
                        sys.SysNum = 3300;
                    }

                    break;
                case 3300:
                    //[Note]:移至安全位置
                    MSystemParameter.gSYS[mDispStageNo1].Command = MCommonDefine.eSysCommand.Safe;
                    sys.SysNum = 3400;

                    break;
                case 3400:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Safe)
                    {
                        sys.SysNum = 3500;
                    }

                    break;
                case 3500:
                    //[Note]:左側先秤重，完成後再換右側秤重
                    if (mDispStageNo1 == mDispStageNo2)
                    {
                        //[Note]:表示只有單邊作業，則只需秤一次即可。
                        sys.SysNum = 4000;
                    }
                    else
                    {
                        MSystemParameter.gSYS[mDispStageNo2].Command = MCommonDefine.eSysCommand.WeightUnit;
                        sys.SysNum = 3600;
                    }

                    break;
                case 3600:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.WeightUnit)
                    {
                        sys.SysNum = 3700;
                    }

                    break;
                case 3700:
                    //[Note]:移至安全位置
                    MSystemParameter.gSYS[mDispStageNo2].Command = MCommonDefine.eSysCommand.Safe;
                    sys.SysNum = 3800;

                    break;
                case 3800:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Safe)
                    {
                        sys.SysNum = 4000;
                    }

                    break;
                case 4000:
                    //[Note]:判斷有無使用第一組閥
                    //[Note]:秤重條件&Pruge條件判斷
                    //       若要秤重，強制作Purge
                    _IsStage1UseValve1 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo1].StageNo].IsUseValve1();
                    _IsStage2UseValve1 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo2].StageNo].IsUseValve1();
                    if (_IsStage1UseValve1 == true | _IsStage2UseValve1 == true)
                    {
                        if (_IsStage1UseValve1 == true)
                        {
                            MSystemParameter.gSYS[mDispStageNo1].SelectValve = MCommonDefine.eValveWorkMode.Valve1;
                        }
                        if (_IsStage2UseValve1 == true)
                        {
                            MSystemParameter.gSYS[mDispStageNo2].SelectValve = MCommonDefine.eValveWorkMode.Valve1;
                        }
                        sys.SysNum = 4100;
                    }
                    else
                    {
                        sys.SysNum = 6000;
                    }

                    break;
                case 4100:
                    //[Note]:秤重條件&Pruge條件判斷
                    //       若要秤重，強制作Purge
                    for (int mDispStage = mDispStageNo1; mDispStage <= mDispStageNo2; mDispStage++)
                    {
                        if (gFlowRateDB.ContainsKey("Default") == true)
                        {
                            switch (gFlowRateDB["Default"].BaseOn)
                            {
                                case ProjectRecipe.eInspectionType.OnTimerOrRuns:
                                    if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gFlowRateDB["Default"].OnTimer)
                                    {
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                        mIsNeedFlowRate = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                    else
                                    {
                                        if (gFlowRateDB["Default"].OnRuns != 0)
                                        {
                                            //[Note]:進入時就做一次(次數的時候)
                                            if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                            {
                                                gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                                gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                                gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                                mIsNeedFlowRate = true;
                                                break; // TODO: might not be correct. Was : Exit For
                                            }
                                        }
                                    }

                                    break;
                                case ProjectRecipe.eInspectionType.OnTimer:
                                    if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gFlowRateDB["Default"].OnTimer)
                                    {
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                        gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                        mIsNeedFlowRate = true;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }

                                    break;
                                case ProjectRecipe.eInspectionType.OnRuns:
                                    if (gFlowRateDB["Default"].OnRuns != 0)
                                    {
                                        //[Note]:進入時就做一次(次數的時候)
                                        if (gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                        {
                                            gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                            gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                            gSSystemParameter.StageParts.FlowRate[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gFlowRateDB["Default"].OnRuns;
                                            mIsNeedFlowRate = true;
                                            break; // TODO: might not be correct. Was : Exit For
                                        }
                                    }

                                    break;
                                case ProjectRecipe.eInspectionType.Noen:

                                    break;
                            }
                        }
                    }


                    if (mIsNeedFlowRate == true)
                    {
                        //[Note]:協助記錄中途是否有做過Purge的動作
                        switch (sys.MachineNo)
                        {
                            case MCommonDefine.enmMachineStation.MachineA:
                                gIsOnPurge[(int)MCommonDefine.enmStage.No1] = true;
                                gIsOnPurge[(int)MCommonDefine.enmStage.No2] = true;

                                break;
                            case MCommonDefine.enmMachineStation.MachineB:
                                gIsOnPurge[(int)MCommonDefine.enmStage.No3] = true;
                                gIsOnPurge[(int)MCommonDefine.enmStage.No4] = true;

                                break;
                        }
                        sys.SysNum = 4200;
                    }
                    else
                    {
                        sys.SysNum = 6000;
                    }

                    break;
                case 4200:
                    //[Note]:Purge流程
                    for (int mDispStage = mDispStageNo1; mDispStage <= mDispStageNo2; mDispStage++)
                    {
                        MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                    }

                    sys.SysNum = 4300;

                    break;
                case 4300:
                    //[Note]:判斷Purge完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                    {
                        if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge)
                        {
                            sys.SysNum = 5000;
                        }
                    }

                    break;
                case 5000:
                    //[Note]:左側先秤重，完成後再換右側秤重
                    MSystemParameter.gSYS[mDispStageNo1].Command = MCommonDefine.eSysCommand.WeightUnit;
                    sys.SysNum = 5200;

                    break;
                case 5200:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.WeightUnit)
                    {
                        sys.SysNum = 5300;
                    }

                    break;
                case 5300:
                    //[Note]:移至安全位置
                    MSystemParameter.gSYS[mDispStageNo1].Command = MCommonDefine.eSysCommand.Safe;
                    sys.SysNum = 5400;

                    break;
                case 5400:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Safe)
                    {
                        sys.SysNum = 5500;
                    }

                    break;
                case 5500:
                    //[Note]:左側先秤重，完成後再換右側秤重
                    if (mDispStageNo1 == mDispStageNo2)
                    {
                        //[Note]:表示只有單邊作業，則只需秤一次即可。
                        sys.SysNum = 6000;
                    }
                    else
                    {
                        MSystemParameter.gSYS[mDispStageNo2].Command = MCommonDefine.eSysCommand.WeightUnit;
                        sys.SysNum = 5600;
                    }

                    break;
                case 5600:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.WeightUnit)
                    {
                        sys.SysNum = 5700;
                    }

                    break;
                case 5700:
                    //[移至安全位置]
                    MSystemParameter.gSYS[mDispStageNo2].Command = MCommonDefine.eSysCommand.Safe;
                    sys.SysNum = 5800;

                    break;
                case 5800:
                    //[Note]:判斷秤重完成
                    if (MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Safe)
                    {
                        sys.SysNum = 6000;
                    }

                    break;
                case 6000:
                    //[Note]:判斷有無使用第二組閥
                    //[Note]:Purge流程 & 判斷是否需要做Purge
                    _IsStage1UseValve2 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo1].StageNo].IsUseValve2();
                    _IsStage2UseValve2 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo2].StageNo].IsUseValve2();
                    if (_IsStage1UseValve2 == true | _IsStage2UseValve2 == true)
                    {
                        if (_IsStage1UseValve2 == true)
                        {
                            MSystemParameter.gSYS[mDispStageNo1].SelectValve = MCommonDefine.eValveWorkMode.Valve2;
                        }
                        if (_IsStage2UseValve2 == true)
                        {
                            MSystemParameter.gSYS[mDispStageNo2].SelectValve = MCommonDefine.eValveWorkMode.Valve2;
                        }
                        sys.SysNum = 6100;
                    }
                    else
                    {
                        sys.SysNum = 7000;
                    }

                    break;
                case 6100:
                    //[Note]:Purge流程 & 判斷是否需要做Purge
                    mIsPurgeDispStageNo1 = false;
                    mIsPurgeDispStageNo2 = false;
                    for (int mDispStage = mDispStageNo1; mDispStage <= mDispStageNo2; mDispStage++)
                    {
                        if (gPurgeDB.ContainsKey("Default") == true)
                        {
                            //[Note]:若點膠前需要Purge，則定位前就不做Purge的動作。
                            if (gPurgeDB["Default"].IsPreDispenePurge == false)
                            {
                                switch (gPurgeDB["Default"].BaseOn)
                                {
                                    case ProjectRecipe.eInspectionType.OnTimerOrRuns:
                                        if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gPurgeDB["Default"].OnTimer)
                                        {
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                            MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                            if (mDispStage == mDispStageNo1)
                                            {
                                                mIsPurgeDispStageNo1 = true;
                                            }
                                            else if (mDispStage == mDispStageNo2)
                                            {
                                                mIsPurgeDispStageNo2 = true;
                                            }
                                        }
                                        else
                                        {
                                            if (gPurgeDB["Default"].OnRuns != 0)
                                            {
                                                //[Note]:進入時就做一次(次數的時候)
                                                if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                                {
                                                    gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                                    gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                                    gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                                    MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                                    if (mDispStage == mDispStageNo1)
                                                    {
                                                        mIsPurgeDispStageNo1 = true;
                                                    }
                                                    else if (mDispStage == mDispStageNo2)
                                                    {
                                                        mIsPurgeDispStageNo2 = true;
                                                    }
                                                }
                                            }
                                        }

                                        break;
                                    case ProjectRecipe.eInspectionType.OnTimer:
                                        if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gPurgeDB["Default"].OnTimer)
                                        {
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                            MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                            if (mDispStage == mDispStageNo1)
                                            {
                                                mIsPurgeDispStageNo1 = true;
                                            }
                                            else if (mDispStage == mDispStageNo2)
                                            {
                                                mIsPurgeDispStageNo2 = true;
                                            }
                                        }

                                        break;
                                    case ProjectRecipe.eInspectionType.OnRuns:
                                        if (gPurgeDB["Default"].OnRuns != 0)
                                        {
                                            //[Note]:進入時就做一次(次數的時候)
                                            if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                            {
                                                gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                                gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                                gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                                MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                                if (mDispStage == mDispStageNo1)
                                                {
                                                    mIsPurgeDispStageNo1 = true;
                                                }
                                                else if (mDispStage == mDispStageNo2)
                                                {
                                                    mIsPurgeDispStageNo2 = true;
                                                }
                                            }
                                        }

                                        break;
                                    case ProjectRecipe.eInspectionType.Noen:

                                        break;
                                }
                            }
                        }
                    }


                    //[Note]:協助記錄哪些是中途作Purge
                    switch (sys.MachineNo)
                    {
                        case MCommonDefine.enmMachineStation.MachineA:
                            if (mIsPurgeDispStageNo1 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage1)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No1] = true;
                            }
                            if (mIsPurgeDispStageNo2 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage2)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No2] = true;
                            }

                            break;
                        case MCommonDefine.enmMachineStation.MachineB:
                            if (mIsPurgeDispStageNo1 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage3)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No3] = true;
                            }
                            if (mIsPurgeDispStageNo2 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage4)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No4] = true;
                            }
                            break;
                    }

                    sys.SysNum = 6200;

                    break;
                case 6200:
                    //[Note]:判斷Purge完成
                    if ((MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Purge) | mIsPurgeDispStageNo1 == false)
                    {
                        if ((MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge) | mIsPurgeDispStageNo2 == false)
                        {
                            sys.SysNum = 7000;
                        }
                    }

                    break;
                case 7000:
                    //[Note]:判斷有無使用第一組閥
                    //[Note]:秤重條件&Pruge條件判斷
                    //       若要秤重，強制作Purge
                    _IsStage1UseValve1 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo1].StageNo].IsUseValve1();
                    _IsStage2UseValve1 = Recipe.Machine[(int)MSystemParameter.gSYS[mDispStageNo2].StageNo].IsUseValve1();
                    if (_IsStage1UseValve1 == true | _IsStage2UseValve1 == true)
                    {
                        if (_IsStage1UseValve1 == true)
                        {
                            MSystemParameter.gSYS[mDispStageNo1].SelectValve = MCommonDefine.eValveWorkMode.Valve1;
                        }
                        if (_IsStage2UseValve1 == true)
                        {
                            MSystemParameter.gSYS[mDispStageNo2].SelectValve = MCommonDefine.eValveWorkMode.Valve1;
                        }
                        sys.SysNum = 7100;
                    }
                    else
                    {
                        sys.SysNum = 9000;
                    }

                    break;
                case 7100:
                    //[Note]:Purge流程 & 判斷是否需要做Purge
                    mIsPurgeDispStageNo1 = false;
                    mIsPurgeDispStageNo2 = false;
                    for (int mDispStage = mDispStageNo1; mDispStage <= mDispStageNo2; mDispStage++)
                    {
                        if (gPurgeDB.ContainsKey("Default") == true)
                        {
                            //[Note]:若點膠前需要Purge，則定位前就不做Purge的動作。
                            if (gPurgeDB["Default"].IsPreDispenePurge == false)
                            {
                                switch (gPurgeDB["Default"].BaseOn)
                                {
                                    case ProjectRecipe.eInspectionType.OnTimerOrRuns:
                                        if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gPurgeDB["Default"].OnTimer)
                                        {
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                            MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                            if (mDispStage == mDispStageNo1)
                                            {
                                                mIsPurgeDispStageNo1 = true;
                                            }
                                            else if (mDispStage == mDispStageNo2)
                                            {
                                                mIsPurgeDispStageNo2 = true;
                                            }
                                        }
                                        else
                                        {
                                            if (gPurgeDB["Default"].OnRuns != 0)
                                            {
                                                //[Note]:進入時就做一次(次數的時候)
                                                if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                                {
                                                    gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                                    gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                                    gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                                    MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                                    if (mDispStage == mDispStageNo1)
                                                    {
                                                        mIsPurgeDispStageNo1 = true;
                                                    }
                                                    else if (mDispStage == mDispStageNo2)
                                                    {
                                                        mIsPurgeDispStageNo2 = true;
                                                    }
                                                }
                                            }
                                        }

                                        break;
                                    case ProjectRecipe.eInspectionType.OnTimer:
                                        if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].get_OnTimer(MSystemParameter.gSYS[mDispStage].SelectValve) >= gPurgeDB["Default"].OnTimer)
                                        {
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                            gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                            MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                            if (mDispStage == mDispStageNo1)
                                            {
                                                mIsPurgeDispStageNo1 = true;
                                            }
                                            else if (mDispStage == mDispStageNo2)
                                            {
                                                mIsPurgeDispStageNo2 = true;
                                            }
                                        }

                                        break;
                                    case ProjectRecipe.eInspectionType.OnRuns:
                                        if (gPurgeDB["Default"].OnRuns != 0)
                                        {
                                            //[Note]:進入時就做一次(次數的時候)
                                            if (gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] <= 0)
                                            {
                                                gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].LastTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = 0;
                                                gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].StartTime[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = DateTime.Now;
                                                gSSystemParameter.StageParts.Purge[(int)MSystemParameter.gSYS[mDispStage].StageNo].OnRuns[(int)MSystemParameter.gSYS[mDispStage].SelectValve] = gPurgeDB["Default"].OnRuns;
                                                MSystemParameter.gSYS[mDispStage].Command = MCommonDefine.eSysCommand.Purge;
                                                if (mDispStage == mDispStageNo1)
                                                {
                                                    mIsPurgeDispStageNo1 = true;
                                                }
                                                else if (mDispStage == mDispStageNo2)
                                                {
                                                    mIsPurgeDispStageNo2 = true;
                                                }
                                            }
                                        }

                                        break;
                                    case ProjectRecipe.eInspectionType.Noen:

                                        break;
                                }
                            }
                        }
                    }


                    //[Note]:協助記錄哪些是中途作Purge
                    switch (sys.MachineNo)
                    {
                        case MCommonDefine.enmMachineStation.MachineA:
                            if (mIsPurgeDispStageNo1 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage1)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No1] = true;
                            }
                            if (mIsPurgeDispStageNo2 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage2)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No2] = true;
                            }

                            break;
                        case MCommonDefine.enmMachineStation.MachineB:
                            if (mIsPurgeDispStageNo1 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage3)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No3] = true;
                            }
                            if (mIsPurgeDispStageNo2 == true & mDispStageNo1 == MCommonDefine.eSys.DispStage4)
                            {
                                gIsOnPurge[(int)MCommonDefine.enmStage.No4] = true;
                            }
                            break;
                    }
                    sys.SysNum = 7200;

                    break;
                case 7200:
                    //[Note]:判斷Purge完成
                    if ((MSystemParameter.gSYS[mDispStageNo1].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo1].ExecuteCommand == MCommonDefine.eSysCommand.Purge) | mIsPurgeDispStageNo1 == false)
                    {
                        if ((MSystemParameter.gSYS[mDispStageNo2].RunStatus == MSystemParameter.enmRunStatus.Finish & MSystemParameter.gSYS[mDispStageNo2].ExecuteCommand == MCommonDefine.eSysCommand.Purge) | mIsPurgeDispStageNo2 == false)
                        {
                            sys.SysNum = 9000;
                        }
                    }

                    break;
                case 9000:
                    //[Note]:完成
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    return;


                    break;
            }

        }
        /// <summary>
        /// A機外部操作
        /// </summary>
        /// <remarks></remarks>
        public void MachineAExternalOp()
        {
            if (MCommonDefineDI.enmDI.StartButton >= 0 && MCommonDefineDO.enmDO.StartButtonLight >= 0 && MCommonDefineDO.enmDO.PauseButtonLight >= 0)
            {
                if (DICollection.GetState(MCommonDefineDI.enmDI.StartButton) == true)
                {
                    DOCollection.SetState(MCommonDefineDO.enmDO.StartButtonLight, true);
                    DOCollection.SetState(MCommonDefineDO.enmDO.PauseButtonLight, false);
                    MachineStart((int)_machineNo);
                    //外部按鍵 A機開始
                }
            }

            if (MCommonDefineDI.enmDI.PauseButton >= 0 && MCommonDefineDO.enmDO.PauseButtonLight >= 0 && MCommonDefineDO.enmDO.StartButtonLight >= 0)
            {
                if (DICollection.GetState(MCommonDefineDI.enmDI.PauseButton) == true)
                {
                    MachinePause((int)_machineNo);
                    DOCollection.SetState(MCommonDefineDO.enmDO.StartButtonLight, false);
                    DOCollection.SetState(MCommonDefineDO.enmDO.PauseButtonLight, true);
                }
            }


        }
        /// <summary>
        /// B機外部操作
        /// </summary>
        /// <remarks></remarks>
        public void MachineBExternalOp()
        {
            if (MCommonDefineDI.enmDI.StartButton2 >= 0 && MCommonDefineDO.enmDO.StartButtonLight2 >= 0 && MCommonDefineDO.enmDO.PauseButtonLight2 >= 0)
            {
                if (DICollection.GetState(MCommonDefineDI.enmDI.StartButton2) == true)
                {
                    DOCollection.SetState(MCommonDefineDO.enmDO.StartButtonLight2, true);
                    DOCollection.SetState(MCommonDefineDO.enmDO.PauseButtonLight2, false);
                    MachineStart((int)_machineNo);
                    //外部按鍵 B機開始
                }
            }
            if (MCommonDefineDI.enmDI.PauseButton2 >= 0 && MCommonDefineDO.enmDO.PauseButtonLight2 >= 0 && MCommonDefineDO.enmDO.StartButtonLight2 >= 0)
            {
                if (DICollection.GetState(MCommonDefineDI.enmDI.PauseButton2) == true)
                {
                    MachinePause((int)_machineNo);
                    DOCollection.SetState(MCommonDefineDO.enmDO.StartButtonLight2, false);
                    DOCollection.SetState(MCommonDefineDO.enmDO.PauseButtonLight2, true);
                }
            }
        }



        /// <summary>機台控制層(LevelNo2端:控制 DispStage)</summary>
        /// <param name="sys"></param>
        /// <param name="MachineType"></param>
        /// <remarks></remarks>
        public void System_Machine(ref MSystemParameter.sSysParam sys, int MachineType)
        {
            //=== 外部IO操作 ===
            if (MachineType == MCommonDefine.eSys.MachineA)
            {
                MachineAExternalOp();
            }
            else
            {
                MachineBExternalOp();

            }
            //=== 外部IO操作 ===

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

            switch (sys.RunStatus)
            {
                case MSystemParameter.enmRunStatus.Running:
                    switch (sys.ExecuteCommand)
                    {
                        case MCommonDefine.eSysCommand.Home:
                            Machine_Home(ref sys);

                            break;
                        case MCommonDefine.eSysCommand.AutoRun:
                            //新版測試
                            Machine_AutoRun(ref sys);

                            break;
                        case MCommonDefine.eSysCommand.PrevDispense:
                            Machine_PrevDispense(ref sys);

                            break;

                    }

                    break;
            }
        }
    }
}

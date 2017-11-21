using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;
using ProjectAOI;
using ProjectMotion;
using ProjectIO;
using System.Diagnostics;

namespace Premtek
{

    /// <summary>CCD定位流程
    /// </summary>
    public class CSystem2Stage
    {
        /// <summary>定位結果清單
        /// </summary>
        public Dictionary<string, CAlignResults> AlignDictionary = new Dictionary<string, CAlignResults>();
        /// <summary>輸入Map
        /// </summary>
        public CRecipe Input { get; set; }
        /// <summary>輸出Map
        /// </summary>
        public CRecipe Output { get; set; }
        /// <summary>外部配接軸卡集合
        /// </summary>
        public Premtek.Base.CMotionCollection Motion { get; set; }
        /// <summary>外部配接AOI集合
        /// </summary>
        public ProjectAOI.CAOICollection AOI { get; set; }
        /// <summary>外部配接設備訊息處理
        /// </summary>
        public Premtek.Base.CEqpMsgHandler EqpMsg { get; set; }
        /// <summary>閥2氣缸
        /// </summary>
        public CCylinder Cylinder { get; set; }

        /// <summary>建立時, 配接硬體物件
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="aoi"></param>
        /// <param name="eqpMsg"></param>
        /// <param name="cylinder"></param>
        public CSystem2Stage(Premtek.Base.CMotionCollection motion, ProjectAOI.CAOICollection aoi, Premtek.Base.CEqpMsgHandler eqpMsg, CCylinder cylinder)
        {
            this.Motion = motion;
            this.AOI = aoi;
            this.EqpMsg = eqpMsg;
            this.Cylinder = cylinder;
        }
        /// <summary>起始點X
        /// </summary>
        decimal _StartPosX;
        /// <summary>起始點Y
        /// </summary>
        decimal _StartPosY;
        /// <summary>起始點Z
        /// </summary>
        decimal _StartPosZ;

        Stopwatch _StopWatch = new Stopwatch();
        /// <summary>做第幾顆
        /// </summary>
        int AlignNo;

        public void FIDsRun(ref MSystemParameter.sSysParam sys)
        {
            const string _FuncName = "CSystemStageFiducial.Run";//功能名稱
            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    if (Input == null)
                    {
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;//沒輸入
                        System.Diagnostics.Debug.Assert(false);
                        return;
                    }
                    if (!Input.Machine[(int)sys.StageNo].IsNeedFids((int)sys.ConveyorNo, (int)sys.StageNo))//不需定位, 流程結束
                    {
                        Output = Input.Clone();
                        sys.RunStatus = MSystemParameter.enmRunStatus.Finish;//流程結束
                        return;
                    }
                    AlignDictionary.Clear();
                    AlignNo = 0;//從第0顆開始生產
                    sys.SysNum = 1100;
                    FIDsRun(ref sys);//判定完直接進下一Case..
                    break;

                case 1100://Z軸到安全高度
                    if (Motion.AbsMove(sys.AxisZ, MSystemParameter.gSSystemParameter.Pos.SafePosZ) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                             if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1032000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1044000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1062000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1069000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            default://不存在待確認
                                System.Diagnostics.Debug.Assert(false);
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                    }
                    if (Cylinder == null)
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    Cylinder.Action(eDoubleActionCylinderAction.Unactuated);//氣缸到 安全
                    sys.SysNum = 1200;
                    break;

                case 1200://等Z軸安全高度到位
                    if (Motion.MotionDone(sys.AxisZ) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        if (Motion.IsMoveTimeOut(sys.AxisZ))
                        {
                            switch (sys.StageNo)
                            {
                                case MCommonDefine.enmStage.No1:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1032004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No2:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1044004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No3:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1062004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No4:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1069004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                default://不存在待確認
                                    System.Diagnostics.Debug.Assert(false);
                                    break;
                            }
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        }
                        return;
                    }
                    sys.SysNum = 1300;
                    FIDsRun(ref sys);//判定完直接進下一Case..
                    break;

                case 1300://Tilt轉正 轉正是因為,不會因Tilt傾斜造成WorkingArea縮減. 
                    if (sys.AxisB == -1)
                    {
                        return;
                    }

                    if (Motion.AbsMove(sys.AxisB, 0) != Premtek.Base.CommandStatus.Sucessed)//拍照與Tilt無關, 固定為0度.
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1032000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1044000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1062000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1069000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            default://不存在待確認
                                System.Diagnostics.Debug.Assert(false);
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                    }
                    sys.SysNum = 1400;
                    break;

                case 1400://Tilt轉正到位
                    if (Motion.MotionDone(sys.AxisB) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        if (Motion.IsMoveTimeOut(sys.AxisB))
                        {
                            switch (sys.StageNo)
                            {
                                case MCommonDefine.enmStage.No1:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1034004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No2:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1046004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No3:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1064004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No4:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1071004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                default://不存在待確認
                                    System.Diagnostics.Debug.Assert(false);
                                    break;
                            }
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        }
                        return;
                    }
                    sys.SysNum = 1500;
                    FIDsRun(ref sys);//判定完直接進下一Case..
                    break;

                case 1500:
                    switch (Cylinder.InPos(eDoubleActionCylinderAction.Unactuated))
                    {
                        case ErrorCode.Failed://氣缸作動逾時
                            switch (sys.StageNo)
                            {
                                case MCommonDefine.enmStage.No1:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Alarm_2004000, eMessageLevel.Alarm, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No2:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Alarm_2004000, eMessageLevel.Alarm, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No3:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Alarm_2004000, eMessageLevel.Alarm, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No4:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Alarm_2004000, eMessageLevel.Alarm, sys.SysNum.ToString());
                                    break;
                            }
                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            break;
                        case ErrorCode.Success://氣缸到位
                            sys.SysNum = 1600;
                            break;
                    }
                    break;

                case 1600://決定目標點
                    switch (Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Type)
                    {
                        case enmAlignType.DevicePos3:
                            _StartPosX = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align3.Pos.X;
                            _StartPosY = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align3.Pos.Y;
                            _StartPosZ = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align3.Pos.Z;
                            _Sys.SysNum = MSystemParameter.sSysParam.SysLoopStart;
                            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
                            sys.SysNum = 1700;
                            break;

                        case enmAlignType.DevicePos1:
                        case enmAlignType.DevicePos2:
                            _StartPosX = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align1.Pos.X;
                            _StartPosY = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align1.Pos.Y;
                            _StartPosZ = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align1.Pos.Z;
                            _Sys.SysNum = MSystemParameter.sSysParam.SysLoopStart;
                            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
                            sys.SysNum = 1800;
                            break;
                    }
                    break;

                case 1700://SkipMark
                    MoveAndAcq(ref _Sys, _StartPosX, _StartPosY, _StartPosZ, AlignNo, 2);
                    if (_Sys.RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        _StartPosX = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align1.Pos.X;
                        _StartPosY = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align1.Pos.Y;
                        _StartPosZ = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align1.Pos.Z;
                        _Sys.SysNum = MSystemParameter.sSysParam.SysLoopStart;
                        _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
                        sys.SysNum = 1800;
                    }
                    break;

                case 1800://Align1
                    MoveAndAcq(ref _Sys, _StartPosX, _StartPosY, _StartPosZ, AlignNo, 0);
                    if (_Sys.RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        switch (Input.Machine[(int)sys.StageNo].AlignList[0].Type)
                        {
                            case enmAlignType.DevicePos1:
                                sys.SysNum = 2000;
                                break;
                            case enmAlignType.DevicePos2:
                            case enmAlignType.DevicePos3:
                                _StartPosX = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align2.Pos.X;
                                _StartPosY = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align2.Pos.Y;
                                _StartPosZ = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].Align[(int)sys.ConveyorNo][(int)sys.StageNo].Align2.Pos.Z;
                                _Sys.SysNum = MSystemParameter.sSysParam.SysLoopStart;
                                _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
                                sys.SysNum = 1900;
                                break;
                            default:
                                Debug.Assert(false);
                                break;
                        }
                    }
                    break;
                case 1900://Align2
                    MoveAndAcq(ref _Sys, _StartPosX, _StartPosY, _StartPosZ, AlignNo, 1);
                    if (_Sys.RunStatus == MSystemParameter.enmRunStatus.Finish)
                    {
                        sys.SysNum = 2000;
                    }
                    break;

                case 2000://單顆拍完
                    AlignNo++;
                    if (AlignNo > Input.Machine[(int)sys.StageNo].AlignList.Count)//全部拍完
                    {
                        sys.SysNum = 9000;
                    }
                    else
                    {
                        sys.SysNum = 1600;//下一顆繼續
                    }
                    break;


                case 9000:
                    foreach (CAlignResults result in AlignDictionary.Values)
                    {
                        if (result.IsBusy != false)//沒跑完..
                        {
                            return;
                        }
                        if (result.IsBusy2 != false)//沒跑完..
                        {
                            return;
                        }
                        if (result.IsBusy3 != false)//沒跑完..
                        {
                            return;
                        }
                    }
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    break;

                default://不應該進來
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        /// <summary>內部子流程
        /// </summary>
        MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
        /// <summary>移動到位拍照
        /// </summary>
        /// <param name="sys"></param>
        /// <param name="targetPosX"></param>
        /// <param name="targetPosY"></param>
        /// <param name="targetPosZ"></param>
        /// <param name="AlignNo"></param>
        /// <param name="type"></param>
        void MoveAndAcq(ref MSystemParameter.sSysParam sys, decimal targetPosX, decimal targetPosY, decimal targetPosZ, int AlignNo, int type)
        {
            const string _FuncName = "MoveAndAcq";
            switch (sys.SysNum)
            {
                case MSystemParameter.sSysParam.SysLoopStart:
                    decimal mVelocity = (decimal)Motion.AxisParameter[sys.AxisX].Velocity.MaxVel;
                    decimal mSCurveRatio = 0.5M;

                    decimal mAcc = Motion.AxisParameter[sys.AxisX].Velocity.Acc * Motion.AxisParameter[sys.AxisX].Velocity.AccRatio * mSCurveRatio;
                    decimal mDec = Motion.AxisParameter[sys.AxisX].Velocity.Dec * Motion.AxisParameter[sys.AxisX].Velocity.DecRatio * mSCurveRatio;

                    decimal mDeltaX = default(decimal);
                    decimal mDeltaY = default(decimal);
                    mDeltaX = targetPosX - Convert.ToDecimal(Motion.GetPositionValue(sys.AxisX));
                    mDeltaY = targetPosY - Convert.ToDecimal(Motion.GetPositionValue(sys.AxisY));

                    decimal mDistance = (decimal)Math.Sqrt((double)(mDeltaX * mDeltaX + mDeltaY * mDeltaY));

                    Premtek.CDispensingMath.GetCrossVelocity(MSystemParameter.gSSystemParameter.MaxCrossDeviceVelocity, mAcc, mDec, mDistance, 0, ref mVelocity);
                    //CDispensingMath.GetCrossVelocity(gSSystemParameter.MaxCrossDeviceVelocity, mAcc(sys.StageNo), mDec(sys.StageNo), mDistance, gSSystemParameter.CrossVerticalTime, mVelocity)
                    if (mVelocity == 0)
                    {
                        mVelocity = MSystemParameter.gSSystemParameter.MaxCrossDeviceVelocity;
                    }
                    Motion.SyncParameter[(int)sys.StageNo].TargetPos[0] = targetPosX;
                    Motion.SyncParameter[(int)sys.StageNo].TargetPos[1] = targetPosY;
                    Motion.SyncParameter[(int)sys.StageNo].TargetPos[2] = Convert.ToDecimal(Motion.GetPositionValue(sys.AxisZ));
                    Motion.SyncParameter[(int)sys.StageNo].Velocity.VelLow = 0;

                    Motion.SyncParameter[(int)sys.StageNo].Velocity.VelHigh = mVelocity;

                    if (Motion.GpSetVelLow(Motion.SyncParameter[(int)sys.StageNo], 0) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1036014, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1048014, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1066014, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1073014, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    if (Motion.GpSetVelHigh(Motion.SyncParameter[(int)sys.StageNo], (double)mVelocity) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1036013, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1048013, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1066013, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1073013, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    if (Motion.GpSetAcc(Motion.SyncParameter[(int)sys.StageNo], (double)mAcc) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1036011, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1048011, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1066011, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1073011, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    if (Motion.GpSetDec(Motion.SyncParameter[(int)sys.StageNo], (double)mDec) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1036012, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1048012, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1066012, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1073012, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    if (Motion.GpSetCurve(Motion.SyncParameter[(int)sys.StageNo], Premtek.Base.eCurveMode.SCurve) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1036010, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1048010, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1066010, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1073010, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }
                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    if (Motion.GpMoveLinearAbsXYZ(Motion.SyncParameter[(int)sys.StageNo]) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1036000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1048000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1066000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1073000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }

                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }

                    sys.SysNum = 1700;
                    break;

                case 1700://到達起點
                    if (Motion.GpMoveDone(Motion.SyncParameter[(int)sys.StageNo]) == Premtek.Base.CommandStatus.Sucessed)
                    {
                        sys.SysNum = 1800;
                    }

                    break;

                case 1800://Z軸移到拍照高度
                    if (Motion.AbsMove(sys.AxisZ, targetPosZ) != Base.CommandStatus.Sucessed)
                    {
                        switch (sys.StageNo)
                        {
                            case MCommonDefine.enmStage.No1:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1032000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No2:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1044000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No3:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1062000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                            case MCommonDefine.enmStage.No4:
                                if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1069000, eMessageLevel.Error, sys.SysNum.ToString());
                                break;
                        }

                        sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                        return;
                    }
                    sys.SysNum = 1900;
                    break;

                case 1900://Z軸到達拍照高度
                    if (Motion.MotionDone(sys.AxisZ, false) != Base.CommandStatus.Sucessed)
                    {
                        if (Motion.IsMoveTimeOut(sys.AxisZ))
                        {
                            switch (sys.StageNo)
                            {
                                case MCommonDefine.enmStage.No1:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1032004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No2:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1044004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No3:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1062004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                                case MCommonDefine.enmStage.No4:
                                    if (EqpMsg !=null) EqpMsg.Add(_FuncName, EqpID.Error_1069004, eMessageLevel.Error, sys.SysNum.ToString());
                                    break;
                            }

                            sys.RunStatus = MSystemParameter.enmRunStatus.Alarm;
                            return;
                        }
                    }
                    sys.SysNum = 2000;
                    _StopWatch.Restart();
                    break;

                case 2000://設定場景
                    sys.SysNum = 2100;
                    break;

                case 2100://設定光源
                    sys.SysNum = 4000;
                    break;
                case 2200:
                    break;

                case 4000://整定時間
                    if (_StopWatch.ElapsedMilliseconds > MSystemParameter.gSSystemParameter.StableTime.CCDStableTime)
                    {
                        _StopWatch.Stop();
                        sys.SysNum = 4100;
                    }
                    break;

                case 4100://觸發Off
                    string _Info = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].ArrayInfo;
                    
                    CAlignResults _Result;
                    if (AlignDictionary.ContainsKey(_Info))
                    {
                        _Result = AlignDictionary[_Info];
                    }
                    else
                    {
                        _Result = new CAlignResults();
                    }
                    switch (type)
                    {
                        case 0://TODO:目前AOI緒不在流程內, 需於AOI緒填入資料
                            _Result.Ticket = AOI.SetCCDTrigger(sys.CCDNo, MCommonMotion.enmONOFF.eOff, false, false);
                            _Result.IsBusy = true;
                            _Result.ByPassResult = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].ByPassResult;
                            break;
                        case 1:
                            _Result.Ticket2 = AOI.SetCCDTrigger(sys.CCDNo, MCommonMotion.enmONOFF.eOff, false, false);
                            _Result.IsBusy2 = true;
                            _Result.ByPassResult = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].ByPassResult;
                            break;
                        case 2:
                            _Result.Ticket3 = AOI.SetCCDTrigger(sys.CCDNo, MCommonMotion.enmONOFF.eOff, false, false);
                            _Result.IsBusy3 = true;
                            _Result.ByPassResult = Input.Machine[(int)sys.StageNo].AlignList[AlignNo].ByPassResult;
                            break;
                    }

                    if (AlignDictionary.ContainsKey(_Info))
                    {
                        AlignDictionary[_Info] = _Result;
                    }
                    else
                    {
                        AlignDictionary.Add(_Info, _Result);
                    }

                    sys.SysNum = 4200;
                    break;

                case 4200://觸發On
                    AOI.SetCCDTrigger(sys.CCDNo, MCommonMotion.enmONOFF.eON, false, false);
                    sys.SysNum = 4300;
                    break;

                case 4300://觸發Off
                    AOI.SetCCDTrigger(sys.CCDNo, MCommonMotion.enmONOFF.eOff, false, false);
                    sys.SysNum = 4400;
                    break;

                case 4400://取像完成
                    if (AOI.IsCCDReady(sys.CCDNo) == true)
                    {
                        //sys.SysNum = 4500;
                        sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    }
                    break;
            }
        }

    }
}

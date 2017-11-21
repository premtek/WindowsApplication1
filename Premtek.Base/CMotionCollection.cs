using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectCore;


namespace Premtek.Base
{
    //定義: Boolean型別傳回值 1為正常 0為異常
    //定義: Enum,Integer型別傳回值 0為正常 >0為異常 <0為警告
    public class CMotionCollection
    {
        /// <summary>外部配接物件
        /// </summary>
        public CEqpMsgHandler gEqpMsg;
        /// <summary>運動控制卡連線參數</summary>
        /// <remarks></remarks>
        public CMotionCards Cards = new CMotionCards();
        CAlarmIDMap AlarmIDMap = new CAlarmIDMap();
        /// <summary>最大支援軸號</summary>
        /// <remarks></remarks>

        const int MaxSupportAxisNo = 31;
        /// <summary>單軸運動參數 索引是enmAxis</summary>
        /// <remarks></remarks>

        public SMotor[] AxisParameter = new SMotor[MCommonDefineAxis.enmAxis.Max + 1];
        /// <summary>連續同動參數</summary>
        /// <remarks></remarks>

        public List<CSyncParameter> SyncParameter = new List<CSyncParameter>();
        /// <summary>運動控制卡集合 對於介面未定義項目,允許直接叫用運動控制卡底層</summary>
        /// <remarks></remarks>

        public List<IMotionCard> Items = new List<IMotionCard>();

        /// <summary>[卡片初始化狀態]</summary>
        /// <remarks></remarks>
        public bool IsCardIntialOK
        {
            get { return mIsCardIntialOK; }
        }

        /// <summary>[卡片初始化狀態]</summary>
        /// <remarks></remarks>

        private bool mIsCardIntialOK;

        /// <summary>[是否走Simulation模式]</summary>
        /// <value></value>
        /// <remarks></remarks>
        public bool IsSimulationType
        {
            set { mIsSimulationType = value; }
        }

        /// <summary>[是否走Simulation模式]</summary>
        /// <remarks></remarks>

        private bool mIsSimulationType;
        /// <summary>
        /// '[說明]:二軸同動至少塞三個點位的資料(使用限制)
        /// </summary>
        /// <remarks></remarks>

        public const int PathCountLimit = 3;
        public bool SetVelAccDec(int axisNo)
        {
            if (axisNo < 0)
            {
                return true;
            }
            
            if (this.SetVelLow(axisNo, this.AxisParameter[axisNo].Velocity.VelLow) != CommandStatus.Sucessed)
            {
                return false;
            }
            if (this.SetVelHigh(axisNo, this.AxisParameter[axisNo].Velocity.VelHigh) != CommandStatus.Sucessed)
            {
                return false;
            }
            if (this.SetAcc(axisNo, this.AxisParameter[axisNo].Velocity.Acc * this.AxisParameter[axisNo].Velocity.AccRatio) != CommandStatus.Sucessed)
            {
                return false;
            }
            if (this.SetDec(axisNo, this.AxisParameter[axisNo].Velocity.Dec * this.AxisParameter[axisNo].Velocity.DecRatio) != CommandStatus.Sucessed)
            {
                return false;
            }
            return true;
        }

        /// <summary>[設定移動時的參數]</summary>
        /// <param name="axisNo"></param>
        /// <param name="velLow"></param>
        /// <param name="velHigh"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SetVelAccDec(int axisNo, decimal velLow, decimal velHigh, decimal acc, decimal dec)
        {
            if (axisNo < 0)
            {
                return true;
            }

            if (this.SetVelLow(axisNo, velLow) != CommandStatus.Sucessed)
            {
                return false;
            }
            if (this.SetVelHigh(axisNo, velHigh) != CommandStatus.Sucessed)
            {
                return false;
            }
            if (this.SetAcc(axisNo, acc) != CommandStatus.Sucessed)
            {
                return false;
            }
            if (this.SetDec(axisNo, dec) != CommandStatus.Sucessed)
            {
                return false;
            }

            return true;
        }

        /// <summary>套用復歸速度/加減速</summary>
        /// <param name="axisNo"></param>
        /// <param name="isHomeDouble"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SetHomeVelAccDec(int axisNo, bool isHomeDouble = false)
        {
            if (axisNo < 0)
            {
                return false;
            }
            if (isHomeDouble == true)
            {
             
                if (this.SetHomeVelLow(axisNo, this.AxisParameter[axisNo].Velocity.HomeVelHigh) == CommandStatus.Sucessed)
                {
                    if (this.SetHomeVelHigh(axisNo, this.AxisParameter[axisNo].Velocity.HomeVelHigh) == CommandStatus.Sucessed)
                    {
                        if (this.SetHomeAcc(axisNo, this.AxisParameter[axisNo].Velocity.HomeAcc) == CommandStatus.Sucessed)
                        {
                            if (this.SetHomeDec(axisNo, this.AxisParameter[axisNo].Velocity.HomeDec) == CommandStatus.Sucessed)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.SetHomeVelLow(axisNo, this.AxisParameter[axisNo].Velocity.HomeVelLow) == CommandStatus.Sucessed)
                {
                    if (this.SetHomeVelHigh(axisNo, this.AxisParameter[axisNo].Velocity.HomeVelHigh) == CommandStatus.Sucessed)
                    {
                        if (this.SetHomeAcc(axisNo, this.AxisParameter[axisNo].Velocity.HomeAcc) == CommandStatus.Sucessed)
                        {
                            if (this.SetHomeDec(axisNo, this.AxisParameter[axisNo].Velocity.HomeDec) == CommandStatus.Sucessed)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;

        }


        string ErrMessage = "";

        /// <summary>取得詳細錯誤訊息</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetErrorMessage()
        {
            return ErrMessage;
        }


        #region "初始化/連線/解構/中斷連線"
        /// <summary>[軸卡初始化]</summary>
        /// <param name="motionCardType"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Initial(ref List<SMotionConnectParameter> motionCardType)
        {
            try
            {
                for (int i = 0; i <= mMoveStopWatch.GetUpperBound(0); i++)
                {
                    mMoveStopWatch[i] = new Stopwatch();
                    mMoveFinishStopWatch[i] = new Stopwatch();
                }
                if (mIsSimulationType == true)
                {
                    mIsCardIntialOK = false;
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6009000), "INFO_6009000");
                    MDateLog.gSyslog.Save("Simulation");
                    return CommandStatus.Sucessed;
                }

                for (int mItemNo = 0; mItemNo <= motionCardType.Count - 1; mItemNo++)
                {
                    switch (motionCardType[mItemNo].CardType)
                    {
                        case enmMotionCardType.None:
                            Items.Add(new CMotionCardVirtual());
                            break;
                        //motionCardType(i).Virtual.ticket = Items.Count - 1 '實際卡號
                        case enmMotionCardType.PCI_1245:
                            Items.Add(new CMotion_PCI_Advantech());
                            if (Items[Items.Count - 1].Initial( motionCardType[mItemNo]) == CommandStatus.Alarm)
                            {
                                //If Items(Items.Count - 1).Initial(motionCardType(mItemNo).PCI1245.CardNo) = False Then
                                ErrMessage += "PCI-1245(" + motionCardType[mItemNo].PCI_1245.CardNo + ")初始化失敗." + Convert.ToString(motionCardType[mItemNo].PCI_1245.CardNo,16);
                            }
                            break;
                        case enmMotionCardType.PCI_1285:
                            Items.Add(new CMotion_PCI_Advantech());
                            if (Items[Items.Count - 1].Initial( motionCardType[mItemNo]) == CommandStatus.Alarm)
                            {
                                //If Items(Items.Count - 1).Initial(motionCardType[mItemNo].PCI1285.CardNo) = False Then
                                ErrMessage += "PCI-1285(" + motionCardType[mItemNo].PCI_1285.CardNo + ")初始化失敗." + Convert.ToString(motionCardType[mItemNo].PCI_1285.CardNo,16);
                            }
                            break;
                        case enmMotionCardType.ModBus:
                            Items.Add(new CMotion_ModBus());
                            //卡號 = COM PORT號

                            motionCardType[mItemNo].MODBUS.SetItemNo(Items.Count - 1);
                            //實際卡號
                            if (Items[Items.Count - 1].Initial( motionCardType[mItemNo]) != CommandStatus.Sucessed)
                            {
                                // If Items(Items.Count - 1).Initial(motionCardType[mItemNo].MODBUS.PortName) <> CommandStatus.Sucessed Then
                                ErrMessage += "MODBUS(" + motionCardType[mItemNo].MODBUS.PortName + ")初始化失敗.";
                            }
                            break;
                    }

                }

                if (!string.IsNullOrEmpty(ErrMessage))
                {
                    mIsCardIntialOK = false;
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009000), "Error_1009000", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Message: " + ErrMessage, "", eMessageLevel.Error);
                    System.Windows.Forms.MessageBox.Show(ErrMessage, "CMotion@Initial", System.Windows.Forms.MessageBoxButtons.OK);
                    return CommandStatus.Alarm;
                }
                else
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6009000), "INFO_6009000");
                    mIsCardIntialOK = true;
                    return CommandStatus.Sucessed;
                }


            }
            catch (Exception ex)
            {
                mIsCardIntialOK = false;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009000), "Error_1009000", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009000) + ex.Message, "CMotion@Initial", System.Windows.Forms.MessageBoxButtons.OK);
                return CommandStatus.Alarm;
            }


        }

        /// <summary>[運動控制卡關閉]</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Close()
        {
            try
            {
                int mItemNo = 0;

                if (mIsSimulationType == true)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6009001), "INFO_6009001");
                    MDateLog.gSyslog.Save("Simulation");
                    return CommandStatus.Sucessed;
                }
                else
                {
                    if (mIsCardIntialOK == true)
                    {
                        for (mItemNo = 0; mItemNo <= Items.Count - 1; mItemNo++)
                        {
                            Items[mItemNo].Close();
                        }
                        MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6009001), "INFO_6009001");
                        return CommandStatus.Sucessed;
                    }
                    else
                    {
                        MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6009001), "INFO_6009001");
                        return CommandStatus.Sucessed;
                    }
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009001), "Error_1009001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009001) + ex.Message, "CMotion@Close", System.Windows.Forms.MessageBoxButtons.OK);
                return CommandStatus.Alarm;
            }


        }
        #endregion

        #region "單軸操作"

        /// <summary>虛擬軸索引查找是否超出範圍</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsIndexOutofRange(int axisNo)
        {
            if (axisNo < 0)
            {
                return true;
            }
            if (axisNo > AxisParameter.GetUpperBound(0))
            {
                return true;
            }
            if (AxisParameter[axisNo].CardParameter.ItemNo < 0)
            {
                return true;
            }
            if (AxisParameter[axisNo].CardParameter.ItemNo >= Items.Count)
            {
                return true;
            }
            if (AxisParameter[axisNo].CardParameter.AxisNo < 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>馬達回原點</summary>
        /// <param name="axisNo"></param>
        /// <param name="isHomeDouble"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Home(int axisNo, bool isHomeDouble = false)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            switch (AxisParameter[axisNo].Parameter.MotorType)
            {
                case eMotorType.ServoMotor:
                case eMotorType.SteppingMotor:
                    if (isHomeDouble == true)
                    {
                        ret = Items[mItemNo].Home(mAxisNo, Convert.ToUInt32(AxisParameter[axisNo].HomeParameter.HomeDoubleMode), Convert.ToUInt32(AxisParameter[axisNo].HomeParameter.HomeDirection));
                    }
                    else
                    {
                        ret = Items[mItemNo].Home(mAxisNo, Convert.ToUInt32(AxisParameter[axisNo].HomeParameter.HomeMode), Convert.ToUInt32(AxisParameter[axisNo].HomeParameter.HomeDirection));
                    }
                    if (ret == CommandStatus.Alarm)
                    {
                        gEqpMsg.Add("Home", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030016), eMessageLevel.Error);
                    }
                    return ret;
                case eMotorType.ElectricCylinder:
                    ret = DOOutput(axisNo, 5, enmCardIOONOFF.eOFF);
                    //OUT5接電動缸ZHOME
                    ret = DOOutput(axisNo, 5, enmCardIOONOFF.eON);
                    //OUT5接電動缸ZHOME
                    if (ret == CommandStatus.Alarm)
                    {
                        gEqpMsg.Add("Home", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030016), eMessageLevel.Error);
                    }
                    return CommandStatus.Sucessed;
            }
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Axis: (" + axisNo + ")" + " MotorType Not Exists.");
            return CommandStatus.Alarm;
        }

        /// <summary>設定原點復歸偏移量</summary>
        /// <param name="axisNo">enmAxis索引</param>
        /// <param name="homeOffset">原點復歸偏移量</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeOffset(int axisNo, decimal homeOffset)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            double mHomeOffset = 0;
            mHomeOffset = Convert.ToDouble(homeOffset * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeOffset(mAxisNo, (decimal)mHomeOffset);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeOffset", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        public CommandStatus SlowStop(int axisNo, decimal dec)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal mDec = 0;
            mDec = Convert.ToDecimal(dec * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            ret = Items[mItemNo].SlowStop(mAxisNo, mDec);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SlowStop", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
                Items[mItemNo].EmgStop(mAxisNo);
                //減速失效時,緊急停止
            }
            return ret;
        }

        public CommandStatus GetCompensatePosition(int axisNo, ref double OffsetValue)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                OffsetValue = 0;
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                OffsetValue = 0;
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            return Items[mItemNo].GetCompensatePosition(mAxisNo, ref  OffsetValue);
        }
        /// <summary>
        /// 取得Courent Position數值(即Freedback數值)
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetPositionValue(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return "0";
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return "0";
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return "0.000";
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal dblPosition = decimal.Zero;
            if (AxisParameter[axisNo].Parameter.IsEncoderExist == true)
            {
                dblPosition = Convert.ToDecimal(Items[mItemNo].GetPositionValue(mAxisNo));

                return (dblPosition * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale).ToString("#0.000");
                //20161112
                // Return Format(dblPosition * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale, "#0.0000").ToString
            }
            else
            {
                decimal pos = default(decimal);
                Items[mItemNo].GetCommandValue(mAxisNo, ref pos);
                dblPosition = pos;
                // Return Format(dblPosition * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale, "#0.0000").ToString
                return (dblPosition * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale).ToString("#0.000");
                //20161112
            }
        }

        /// <summary>
        /// 取得Courent Command數值
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetCommandValue(int axisNo, ref decimal pos)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal mPosition = default(decimal);
            ret = Items[mItemNo].GetCommandValue(mAxisNo, ref mPosition);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetCommandValue", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            else
            {
                pos = Convert.ToDecimal((mPosition * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale).ToString("#0.0000"));
            }
            return ret;
        }

        /// <summary>
        /// 設定Compare
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetCompare(int axisNo, enmCompareEnable Enable, enmCompareSource Type, enmCompareMethod Method)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetCompare(mAxisNo, Enable, Type, Method);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetCompare", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }


        /// <summary>
        /// Set compare data for the specified axis
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetCampareData(int axisNo, decimal startPos, decimal endPos, int interval)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal mStartPos = 0;
            decimal mEndPos = 0;
            int mInterval = 0;
            mStartPos = Convert.ToDecimal(startPos * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            mEndPos = Convert.ToDecimal(endPos * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            mInterval = Convert.ToInt32(interval * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);

            ret = Items[mItemNo].SetCampareData(mAxisNo, mStartPos, mEndPos, mInterval);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetCampareData", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>
        /// Get current compare data in the comparator.
        /// </summary>
        /// <param name="Pos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetCompareValue(int axisNo, ref decimal pos)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal mPosition = default(decimal);
            ret = Items[mItemNo].GetCompareValue(mAxisNo, ref mPosition);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetCompareValue", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            else
            {
                pos = Convert.ToDecimal((mPosition * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale).ToString( "#0.0000"));
            }
            return ret;
        }


        /// <summary>
        /// 設定馬達狀態 IO
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus IOSet(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);

            ret = Items[mItemNo].SetALM(mAxisNo, AxisParameter[axisNo].Parameter.AlarmEnable, AxisParameter[axisNo].Parameter.AlarmLogic, AxisParameter[axisNo].Parameter.AlarmStopMode);
            ret = Items[mItemNo].SetBacklash(mAxisNo, AxisParameter[axisNo].Parameter.BacklashEnable);
            //+
            ret = Items[mItemNo].SetERC(mAxisNo, AxisParameter[axisNo].Parameter.ErcEnable, AxisParameter[axisNo].Parameter.ErcLogic);
            ret = Items[mItemNo].SetExternalDrive(mAxisNo, AxisParameter[axisNo].Parameter.ExternalDriveAxis, AxisParameter[axisNo].Parameter.ExternalDriveEnable, AxisParameter[axisNo].Parameter.ExternalDrivePulseInMode);
            ret = Items[mItemNo].SetHEL(mAxisNo, AxisParameter[axisNo].Limit.HLimitEnable, AxisParameter[axisNo].Limit.HLimitLogic, AxisParameter[axisNo].Limit.HLimitStopMode);
            ret = Items[mItemNo].SetORG(mAxisNo, AxisParameter[axisNo].Parameter.OrgLogic);
            ret = Items[mItemNo].SetEZ(mAxisNo, AxisParameter[axisNo].Parameter.EZLogic);
            ret = Items[mItemNo].SetHomeReset(mAxisNo, AxisParameter[axisNo].Parameter.HomeReset);
            ret = Items[mItemNo].SetINP(mAxisNo, AxisParameter[axisNo].Parameter.INPEnable, AxisParameter[axisNo].Parameter.INPLogic);
            ret = Items[mItemNo].SETIN1Stop(mAxisNo, AxisParameter[axisNo].Parameter.TriggerStopEnable1, AxisParameter[axisNo].Parameter.TriggerStopMode1, AxisParameter[axisNo].Parameter.TriggerStopLogic1);
            ret = Items[mItemNo].SETIN2Stop(mAxisNo, AxisParameter[axisNo].Parameter.TriggerStopEnable2, AxisParameter[axisNo].Parameter.TriggerStopMode2, AxisParameter[axisNo].Parameter.TriggerStopLogic2);
            ret = Items[mItemNo].SETIN4Stop(mAxisNo, AxisParameter[axisNo].Parameter.TriggerStopEnable4, AxisParameter[axisNo].Parameter.TriggerStopMode4, AxisParameter[axisNo].Parameter.TriggerStopLogic4);
            ret = Items[mItemNo].SETIN5Stop(mAxisNo, AxisParameter[axisNo].Parameter.TriggerStopEnable5, AxisParameter[axisNo].Parameter.TriggerStopMode5, AxisParameter[axisNo].Parameter.TriggerStopLogic5);
            ret = Items[mItemNo].SetLatch(mAxisNo, AxisParameter[axisNo].Parameter.LatchEnable, AxisParameter[axisNo].Parameter.LatchLogic);
            ret = Items[mItemNo].ResetLatch(mAxisNo);
            ret = Items[mItemNo].SetPulseIn(mAxisNo, AxisParameter[axisNo].Parameter.PulseInMode, AxisParameter[axisNo].Parameter.PulseInDirection);
            //Pulse In Logic '
            ret = Items[mItemNo].SetMaxPulseFrequency(mAxisNo, AxisParameter[axisNo].Parameter.PulseInMaxFreq);
            //Max Pulse Frequency 
            if (AxisParameter[axisNo].CardParameter.CardType == enmMotionCardType.PCI_1285)
            {
                ret = Items[mItemNo].SetPulseOutReverse(mAxisNo, AxisParameter[axisNo].Parameter.PulseOutReverse);
            }
            ret = Items[mItemNo].SetPulseOutMode(mAxisNo, AxisParameter[axisNo].Parameter.PulseOutMode);
            ret = Items[mItemNo].IOSet(mAxisNo);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("IOSet", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            ret = Items[mItemNo].AxisResetError(mAxisNo);
            return ret;
        }


        /// <summary>
        /// 設定馬達Backlash
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetBacklash(int axisNo, enmBacklashEnable Enable)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetBacklash(mAxisNo, Enable);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetBacklash", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>
        /// 設定External Drive
        /// </summary>
        /// <param name="Drive"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetExternalDrive(int axisNo, enmExternalDrive Drive, enmExternalDriveEnable Enable, enmExternalDrivePulseInMode Mode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetExternalDrive(mAxisNo, Drive, Enable, Mode);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetExternalDrive", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }


        /// <summary>
        /// 設定馬達Pulse Out Mode
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPulseOutMode(int axisNo, enmPulseOutMode Mode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetPulseOutMode(mAxisNo, Mode);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetPulseOutMode", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        public bool WaitCmdStatus(int axisNo)
        {
            do
            {
                if (GetCmdStatus(axisNo) == CommandStatus.Sucessed)
                {
                    break;
                    
                }
            } while (true);
            return true;
        }

        public CommandStatus GetCmdStatus(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            return Items[mItemNo].cmdStatus;
        }
        /// <summary>
        /// 設定馬達EMG邏輯訊號 Hi/Lo
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetEMG(enmEmgLogic Logic)
        {
            CommandStatus isSuccess = CommandStatus.Alarm;
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            CommandStatus errCode = CommandStatus.Alarm;
            foreach (IMotionCard item in Items)
            {
                errCode = item.SetEMG(Logic);
                //全部都過才會過

                isSuccess = (errCode != 0 ? errCode : isSuccess);
                //如果不是Success(0),則傳回ErrorCode, 如果是則傳回暫存值. 
                //isSuccess = isSuccess And item.SetEMG(Logic) '全部都過才會過

            }
            return isSuccess;

        }
        #endregion



        /// <summary>取得詳細錯誤訊息</summary>
        /// <param name="axisNo">軸號</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetErrorMessage(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return "";
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return "";
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return "";
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            return Items[mItemNo].GetErrorMessage(mAxisNo);

        }
        /// <summary>設定轉換比例(pulse/mm)</summary>
        /// <param name="axisNo"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetScale(int axisNo, decimal scale)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetScale(mAxisNo, scale);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetScale", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>每單位Pulse數,虛擬單位</summary>
        /// <param name="axisNo"></param>
        /// <param name="ppu"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPPU(int axisNo, decimal ppu)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetPPU(mAxisNo, ppu);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetPPU", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定最大速度(mm/s)</summary>
        /// <param name="velHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetVelHigh(int axisNo, decimal velHigh)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mVelHigh = 0;
            mVelHigh = Convert.ToDecimal(velHigh * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetVelHigh(mAxisNo, mVelHigh);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetVelHigh", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030013), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定初速度(mm/s)</summary>
        /// <param name="velLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetVelLow(int axisNo, decimal velLow)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mVelLow = 0;
            mVelLow = Convert.ToDecimal(velLow * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetVelLow(mAxisNo, mVelLow);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetVelLow", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030014), eMessageLevel.Error);
            }
            return ret;
        }



        /// <summary>設定加速度上限值(mm/s^2)</summary>
        /// <param name="maxAcc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxAcc(int axisNo, decimal maxAcc)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mMaxAcc = 0;
            mMaxAcc = Convert.ToDecimal(maxAcc * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetMaxAcc(mAxisNo, mMaxAcc);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetMaxAcc", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030014), eMessageLevel.Error);
            }
            return ret;

        }

        /// <summary>設定減速度上限值(mm/s^2)</summary>
        /// <param name="maxDec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxDec(int axisNo, decimal maxDec)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }

            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mMaxDec = 0;
            mMaxDec = Convert.ToDecimal(maxDec * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetMaxDec(mAxisNo, mMaxDec);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetMaxDec", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030014), eMessageLevel.Error);
            }
            return ret;

        }

        /// <summary>設定速度上限值(mm/s)</summary>
        /// <param name="maxVel"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxVel(int axisNo, decimal maxVel)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }

            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mMaxVel = 0;
            mMaxVel = Convert.ToDecimal(maxVel * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetMaxVel(mAxisNo, mMaxVel);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetMaxVel", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030014), eMessageLevel.Error);
            }
            return ret;

        }

        /// <summary>設定加速度(mm/s^2)</summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetAcc(int axisNo, decimal acc)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal dblAcc = 0;
            dblAcc = Convert.ToDecimal(acc * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetAcc(mAxisNo, dblAcc);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetAcc", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030011), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>[設定T S Curve]</summary>
        /// <param name="axisNo"></param>
        /// <param name="curveMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetCurve(int axisNo, eCurveMode curveMode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetCurve(mAxisNo, curveMode);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetCurve", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>設定減速度(mm/s^2)</summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetDec(int axisNo, decimal dec)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mDec = 0;
            mDec = Convert.ToDecimal(dec * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetDec(mAxisNo, mDec);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetDec", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030012), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>相對移動</summary>
        /// <param name="Dist"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus RelMove(int axisNo, decimal Dist)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }

            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            IOStatus mIOStatus = default(IOStatus);
            //馬達狀態取得失敗
            if (Items[mItemNo].CheckMotorStatus(mAxisNo, ref mIOStatus) == CommandStatus.Alarm)
            {
                return CommandStatus.Alarm;
            }
            //馬達狀態異常
            if (AxisParameter[axisNo].MotionIOStatus.blnALM == true)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009007), "Error_1009007", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            decimal mOffsetPulse = 0;
            CommandStatus ret = default(CommandStatus);

            mOffsetPulse = Convert.ToDecimal(Dist * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            //單位轉換 mm -> um
            if (AxisParameter[axisNo].Parameter.Direction == eDirection.Positive)
            {
                ret = Items[mItemNo].RelMove(mAxisNo, mOffsetPulse);
                if (ret == CommandStatus.Sucessed) TargetPos[axisNo] = Convert.ToDecimal(GetPositionValue(axisNo)) + Dist;
            }
            else
            {
                ret = Items[mItemNo].RelMove(mAxisNo, -mOffsetPulse);
                if (ret == CommandStatus.Sucessed) TargetPos[axisNo] = Convert.ToDecimal(GetPositionValue(axisNo)) - Dist;
            }
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("RelMove", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }

            return ret;
        }

        /// <summary>目標位置暫存</summary>
        /// <remarks></remarks>
        //不能直接使用enmAxis.Max,因為初始化在讀檔之前
        public decimal[] TargetPos = new decimal[MaxSupportAxisNo + 1];
        /// <summary>舊目標位置暫存</summary>
        /// <remarks></remarks>
        //不能直接使用enmAxis.Max,因為初始化在讀檔之前
        public decimal[] OldTargetPos = new decimal[MaxSupportAxisNo + 1];
        /// <summary>移動逾時計時器</summary>
        /// <remarks></remarks>
        Stopwatch[] mMoveStopWatch = new Stopwatch[MaxSupportAxisNo + 1];
        /// <summary>到位穩定計時器</summary>
        /// <remarks></remarks>
        Stopwatch[] mMoveFinishStopWatch = new Stopwatch[MaxSupportAxisNo + 1];
        /// <summary>到位計時流程切換旗標</summary>
        /// <remarks></remarks>
        bool[] mIsMoveFinishOK = new bool[MaxSupportAxisNo + 1];


        /// <summary>絕對移動</summary>
        /// <param name="axisNo">enmAxis</param>
        /// <param name="absPos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus AbsMove(int axisNo, decimal absPos)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (axisNo < 0)
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;

            decimal absPulse = 0;
            absPulse = Convert.ToInt32(absPos * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);

            if (absPos > AxisParameter[axisNo].Limit.PosivtiveLimit)
            {
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")" + AxisParameter[axisNo].AxisName, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Target Position: " + absPos, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Soft Positive Edge Limit: " + AxisParameter[axisNo].Limit.PosivtiveLimit, "", eMessageLevel.Error);
                //Debug.Print(AxisParam(axisNo).AxisName & "目標位置(" & Dist & ")超出馬達軟體正極限(" & AxisParam(axisNo).Limit.dblPosivtiveLimit & ")")
                return CommandStatus.Warning;
                //超出馬達軟體極限
            }
            if (absPos < AxisParameter[axisNo].Limit.NegativeLimit)
            {
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")" + AxisParameter[axisNo].AxisName, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Target Position: " + absPos, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Soft Negative Edge Limit: " + AxisParameter[axisNo].Limit.NegativeLimit, "", eMessageLevel.Error);
                //Debug.Print(AxisParam(axisNo).AxisName & "目標位置(" & Dist & ")超出馬達軟體負極限(" & AxisParam(axisNo).Limit.dblNegativeLimit & ")")
                return CommandStatus.Warning;
                //超出馬達軟體極限
            }

            //留給外部判斷
            //If CheckMotorStatus(axisNo) = CommandStatus.Alarm Then '馬達狀態取得失敗
            //    'Debug.Print(AxisParam(axisNo).AxisName & "馬達狀態取得失敗")
            //    Return CommandStatus.Alarm
            //End If

            //If AxisParameter[axisNo].MotionIOStatus.blnALM = True Then '馬達狀態異常
            //    'Debug.Print(AxisParam(axisNo).AxisName & "馬達狀態異常")
            //    MDateLog.gSyslog.Save(gMsgHandler.GetMessage(Error_1009007), "Error_1009007", eMessageLevel.Error)
            //    Return CommandStatus.Alarm
            //End If


            if (Math.Abs(absPos -Convert.ToDecimal( GetPositionValue(axisNo))) < 0.001M)
            {
                //gSyslog.Save("Axis: (" & axisNo & ")" & AxisParameter[axisNo].AxisName)
                //gSyslog.Save("Target & Current Position almost Equal, Command is Ignored!")
                //gSyslog.Save("Target Position:" & absPos)
                //gSyslog.Save("Current Position:" & GetPositionValue(axisNo))
                mMoveStopWatch[axisNo].Restart();
                mIsMoveFinishOK[axisNo] = false;
                return CommandStatus.Sucessed;
            }

            CommandStatus ret = default(CommandStatus);
            if (AxisParameter[axisNo].Parameter.Direction == eDirection.Positive)
            {
                ret = Items[mItemNo].AbsMove(mAxisNo, absPulse);
            }
            else
            {
                ret = Items[mItemNo].AbsMove(mAxisNo, -absPulse);
            }

            if (ret == CommandStatus.Sucessed)
            {
                OldTargetPos[axisNo] = TargetPos[axisNo];
                //舊目標位置暫存
                TargetPos[axisNo] = absPos;
                //命令發送成功, 目標位置暫存
                //Debug.Print(AxisParameter[axisNo].AxisName & " " & OldTargetPos[axisNo] & "->" & TargetPos[axisNo])
                mMoveStopWatch[axisNo].Restart();
                mIsMoveFinishOK[axisNo] = false;
            }
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("AbsMove", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;

        }
        /// <summary>取得絕對移動目標位置
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="absPos"></param>
        /// <returns></returns>
        public CommandStatus GetTargetPos(int axisNo, ref decimal absPos)
        {
            //軸不存在
            if (axisNo < 0)
            {
                return CommandStatus.Sucessed;
            }
            absPos = TargetPos[axisNo];
            return CommandStatus.Sucessed;
        }
        /// <summary>等速移動</summary>
        /// <param name="axisNo"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus VelMove(int axisNo, eDirection dir)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            if (AxisParameter[axisNo].Parameter.Direction == eDirection.Positive)
            {
                if (dir == eDirection.Positive)
                {
                    ret = Items[mItemNo].VelMove(mAxisNo, eDirection.Positive);
                }
                else
                {
                    ret = Items[mItemNo].VelMove(mAxisNo, eDirection.Negative);
                }
            }
            else
            {
                if (dir == eDirection.Positive)
                {
                    ret = Items[mItemNo].VelMove(mAxisNo, eDirection.Negative);
                }
                else
                {
                    ret = Items[mItemNo].VelMove(mAxisNo, eDirection.Positive);
                }
            }

            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("JogPlus", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>是否移動逾時</summary>>
        /// <param name="axisNo">enmAxis</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsMoveTimeOut(int axisNo)
        {

            //參數不對一定逾時
            if (AxisParameter[axisNo].Velocity.Acc <= 0)
            {
                return true;
            }
            //參數不對一定逾時
            if (AxisParameter[axisNo].Velocity.Dec <= 0)
            {
                return true;
            }
            //參數不對一定逾時
            if (AxisParameter[axisNo].Velocity.VelHigh <= 0)
            {
                return true;
            }

            decimal tacc = AxisParameter[axisNo].Velocity.VelHigh / AxisParameter[axisNo].Velocity.Acc;
            //標準加速時間
            decimal tdec = AxisParameter[axisNo].Velocity.VelHigh / AxisParameter[axisNo].Velocity.Dec;
            //標準減速時間
            decimal Pos = (AxisParameter[axisNo].Velocity.VelLow + 0.5M * AxisParameter[axisNo].Velocity.VelHigh) * (tacc + tdec);
            //三角波判定位置

            decimal dis = Math.Abs(OldTargetPos[axisNo] - TargetPos[axisNo]);
            decimal timeCondition = 0;
            //梯形
            if (dis > Pos)
            {
                decimal tv = 0;
                //最大速移動時間
                tv = (dis - 0.5M * (tacc + tdec) * AxisParameter[axisNo].Velocity.VelHigh - AxisParameter[axisNo].Velocity.VelLow * (tacc + tdec)) / (AxisParameter[axisNo].Velocity.VelLow + AxisParameter[axisNo].Velocity.VelHigh);
                timeCondition = 1000M * (tacc + tdec + tv);
                //以梯形計算 1000為Sec轉msec
                timeCondition += 30000;
                //三角
            }
            else
            {
                timeCondition = 1000 * Pos / AxisParameter[axisNo].Velocity.VelHigh;
                //以最大三角形計算 1000為Sec轉msec
                timeCondition += 30000;
                //增加逾時保護
            }

            //[Note]:雖然已經到位了，但還要等馬達整定時間，故不能算是逾時
            if (mMoveStopWatch[axisNo].IsRunning == true)
            {
                if (mMoveStopWatch[axisNo].ElapsedMilliseconds > timeCondition)
                {
                    //逾時
                    gEqpMsg.Add("IsMoveTimeOut", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030004), eMessageLevel.Error);
                    return true;
                }
            }

            return false;
        }
        /// <summary>是否移動逾時</summary>
        /// <param name="stopWatch">計時器</param>
        /// <param name="startPos">開始位置</param>
        /// <param name="EndPos">結束位置</param>
        /// <param name="param">計算用參數</param>
        /// <returns></returns>
        /// <remarks>TODO:移植到gCMotion內, 修改輸入只剩enmAxis</remarks>
        public bool IsMoveTimeOut(ref Stopwatch stopWatch, double startPos, double EndPos, ref SMotor param)
        {
            //參數不對一定逾時
            if (param.Velocity.Acc <= 0)
            {
                return true;
            }
            //參數不對一定逾時
            if (param.Velocity.Dec <= 0)
            {
                return true;
            }
            //參數不對一定逾時
            if (param.Velocity.VelHigh <= 0)
            {
                return true;
            }

            decimal tacc = param.Velocity.VelHigh / param.Velocity.Acc;
            //標準加速時間
            decimal tdec = param.Velocity.VelHigh / param.Velocity.Dec;
            //標準減速時間
            decimal Pos = (param.Velocity.VelLow + 0.5M * param.Velocity.VelHigh) * (tacc + tdec);
            //三角波判定位置
            decimal dis = (decimal)Math.Abs(startPos - EndPos);
            decimal timeCondition = 0;
            //梯形
            if (dis > Pos)
            {
                decimal tv = 0;
                //最大速移動時間
                tv = (dis - 0.5M * (tacc + tdec) * param.Velocity.VelHigh - param.Velocity.VelLow * (tacc + tdec)) / (param.Velocity.VelLow + param.Velocity.VelHigh);
                timeCondition = 1000 * (tacc + tdec + tv);
                //以梯形計算 1000為Sec轉msec
                timeCondition += 5000;
                //三角
            }
            else
            {
                timeCondition = 1000M * Pos / param.Velocity.VelHigh;
                //以最大三角形計算 1000為Sec轉msec
                timeCondition += 5000;
                //增加逾時保護
            }
            if (stopWatch.ElapsedMilliseconds > timeCondition)
            {
                return true;
            }
            return false;
        }

        /// <summary>移動完成</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus MotionDone(int axisNo, bool IsWaitStable = true)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            //去除軸狀態判斷, 留給外部操作
            //Dim motionStatus As UInteger
            //ret = Items[mItemNo].GetAxisState(mAxisNo, motionStatus) '取得軸狀態
            //If ret = CommandStatus.Alarm Then '取得軸狀態異常無法判斷
            //    Return CommandStatus.Alarm
            //End If
            //If motionStatus <> CUShort(AxisState.STA_AX_READY) Then
            //    Return CommandStatus.Alarm '馬達狀態不為可以下命令 (移動中,加速,減速,Alarm,正在下命令...etc)
            //End If

            IOStatus mIOStatus = default(IOStatus);
            ret = Items[mItemNo].CheckMotorStatus(mAxisNo, ref mIOStatus);
            //取得IO狀態
            //取得IO狀態異常無法判斷
            if (ret == CommandStatus.Alarm)
            {
                return CommandStatus.Alarm;
            }
            AxisParameter[axisNo].MotionIOStatus = mIOStatus;
            //未到位
            if (AxisParameter[axisNo].MotionIOStatus.blnINP != true)
            {
                return CommandStatus.Alarm;
            }

            //不等穩定時間
            if (IsWaitStable == false)
            {
                return ret;
            }

            //就算沒有AbsMove,也會進來 Polling到計時結束後就會傳回正確值
            if (mIsMoveFinishOK[axisNo] == false)
            {
                mMoveStopWatch[axisNo].Stop();
                //到位. 移動計時停止
                //未設定,到位完成
                if (AxisParameter[axisNo].InpositionStableTime <= 0)
                {
                    return ret;
                }
                mMoveFinishStopWatch[axisNo].Restart();
                //到位穩定開始計時
                mIsMoveFinishOK[axisNo] = true;
                //走計時流程
                //Debug.Print(AxisParameter[axisNo].AxisName & "INP出現,計時開始")
                //計時流程
            }
            else
            {
                //到位穩定
                if (mMoveFinishStopWatch[axisNo].ElapsedMilliseconds > AxisParameter[axisNo].InpositionStableTime)
                {
                    mMoveFinishStopWatch[axisNo].Stop();
                    //到位穩定,計時停止
                    //Debug.Print(AxisParameter[axisNo].AxisName & "到位穩定計時停止: " & mMoveFinishStopWatch[axisNo].ElapsedMilliseconds)
                    return ret;
                }
            }

            return CommandStatus.Sending;

            


        }
        /// <summary>取得運動接點狀態</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus CheckMotorStatus(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }

            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            IOStatus mIoStatus = default(IOStatus);
            ret = Items[mItemNo].CheckMotorStatus(mAxisNo, ref mIoStatus);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("CheckMotorStatus", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            else
            {
                AxisParameter[axisNo].MotionIOStatus = mIoStatus;
            }
            return ret;
        }
        /// <summary>取得軸狀態</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetAxisState(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }


            int mItemNo = 0;
            int mAxisNo = 0;
            mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            uint mStatus = 0;
            ret = Items[mItemNo].GetAxisState(mAxisNo,ref mStatus);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetAxisState", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            else
            {
                 Enum.TryParse<AxisState>( mStatus.ToString(), out AxisParameter[axisNo].MotionStatus);
            }

            return ret;

        }
        /// <summary>取得軸狀態</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetMotionState(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }


            int mItemNo = 0;
            int mAxisNo = 0;
            mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            int mStatus = 0;
            ret = Items[mItemNo].GetMotionState(mAxisNo, ref mStatus);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetMotionAxisState", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            else
            {
                AxisParameter[axisNo].AxisMotionStatus = mStatus;
            }

            return ret;

        }
        /// <summary>軸異常重置</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus AxisResetError(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }

            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            if (AxisParameter[axisNo].Parameter.MotorType == eMotorType.ElectricCylinder)
            {
                Items[mItemNo].DOOutput(mAxisNo, 7, enmCardIOONOFF.eOFF);
                Items[mItemNo].DOOutput(mAxisNo, 7, enmCardIOONOFF.eON);
                Items[mItemNo].DOOutput(mAxisNo, 7, enmCardIOONOFF.eOFF);
            }
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].AxisResetError(mAxisNo);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("AxisResetError", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>
        /// 設定快速回Home之速度
        /// </summary>
        /// <param name="VelHigh">速度</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeVelHigh(int axisNo, decimal velHigh)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mVelHigh = Convert.ToDecimal(velHigh * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeVelHigh(mAxisNo, mVelHigh);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeVelHigh", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定慢速回Home速度</summary>
        /// <param name="axisNo"></param>
        /// <param name="velLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeVelLow(int axisNo, decimal velLow)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mVelLow = Convert.ToDecimal(velLow * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeVelLow(mAxisNo, mVelLow);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeVelLow", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定回Home加速度</summary>
        /// <param name="axisNo"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeAcc(int axisNo, decimal acc)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mAcc = Convert.ToDecimal(acc * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeAcc(mAxisNo, mAcc);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeAcc", AlarmIDMap.GetAxisALID(axisNo,EqpID. Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定回Home減速度</summary>
        /// <param name="axisNo"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeDec(int axisNo, decimal dec)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            decimal mDec = Convert.ToDecimal(dec * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeDec(mAxisNo, mDec);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeDec", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>軸原點復歸</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Home(int axisNo, int homeMode, eDirection homeDirection)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].Home(mAxisNo, (uint)homeMode, (uint)homeDirection);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("Home", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>復歸完成</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus HomeFinish(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            switch (AxisParameter[axisNo].Parameter.MotorType)
            {
                case eMotorType.ServoMotor:
                case eMotorType.SteppingMotor:
                case eMotorType.None:
                    
                    return Items[AxisParameter[axisNo].CardParameter.ItemNo].HomeFinish(AxisParameter[axisNo].CardParameter.AxisNo);
                case eMotorType.ElectricCylinder:
                    CheckMotorStatus(axisNo);
                    //更新狀態
                    //復歸完成
                    if (AxisParameter[axisNo].MotionIOStatus.blnORG == true)
                    {
                        SetPosition(axisNo, 0);

                        return CommandStatus.Sucessed;
                    }
                    else
                    {
                        return CommandStatus.Sending;
                    }

                    
                default:
                    return CommandStatus.Alarm;
            }

        }
        /// <summary>設定當前位置座標</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPosition(int axisNo, decimal pos)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;

            CommandStatus ret = default(CommandStatus);
            decimal mPos = 0;
            mPos = Convert.ToDecimal(pos * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            ret = Items[mItemNo].SetPosition(mAxisNo, mPos);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetPosition", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>馬達激磁</summary>
        /// <param name="axisNo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Servo(int axisNo, enmONOFF state)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].Servo(mAxisNo, state);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("Servo", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary></summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus EmgStop(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].EmgStop(mAxisNo);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("EmgStop", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>取得目前速度</summary>
        /// <param name="axisNo">enmAxis</param>
        /// <param name="vel">實際速度mm/s</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetCurrentVel(int axisNo, ref decimal vel)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                vel = 0;
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                vel = 0;
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal mVelocity = default(decimal);
            ret = Items[mItemNo].GetCurrentVel(mAxisNo, ref mVelocity);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetCurrentVel", AlarmIDMap.GetAxisALID(axisNo,EqpID. Error_1030000), eMessageLevel.Error);
            }
            else
            {
                vel = Convert.ToDecimal((mVelocity * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale).ToString("#0.0000"));
            }
            return ret;
        }

        /// <summary>Error Clear</summary>
        /// <param name="axisNo"></param>
        /// <param name="enableMode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetERC(int axisNo, enmErcEnableMode enableMode, enmErcLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetERC(mAxisNo, enableMode, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetERC", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定硬體極限訊號</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHEL(int axisNo, enmLimitEnable enable, enmLimitLogic logic, enmLimitStopMode stopMode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHEL(mAxisNo, enable, logic, stopMode);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHEL", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定ORG訊號</summary>
        /// <param name="axisNo"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetORG(int axisNo, enmOrgLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetORG(mAxisNo, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetORG", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定Encoder Z相邏輯訊號</summary>
        /// <param name="axisNo"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetEZ(int axisNo, enmEZLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetEZ(mAxisNo, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetEZ", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>復歸後命令,位置歸零</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeReset(int axisNo, enmHomeReset enable)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeReset(mAxisNo, enable);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeReset", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>特殊復歸方式,停止條件</summary>
        /// <param name="axisNo"></param>
        /// <param name="homeExSwitchMode">HomeEx停止條件</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeExSwitchMode(int axisNo, enmHomeExSwitchMode homeExSwitchMode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetHomeExSwitchMode(mAxisNo, homeExSwitchMode);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeExSwitchMode", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>在Sensor範圍內時,要往外移動多遠再找Sensor</summary>
        /// <param name="axisNo"></param>
        /// <param name="homeCrossDistance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeCrossDistance(int axisNo, decimal homeCrossDistance)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal mHomeCrossDistance = 0;
            mHomeCrossDistance = Convert.ToDecimal(homeCrossDistance * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU);
            if (mHomeCrossDistance <= 0)
                mHomeCrossDistance = 100;
            //最小設定值保護
            ret = Items[mItemNo].SetHomeCrossDistance(mAxisNo, mHomeCrossDistance);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetHomeCrossDistance", AlarmIDMap.GetAxisALID(axisNo,EqpID. Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>設定警告邏輯</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetALM(int axisNo, enmAlarmEnable enable, enmAlarmLogic logic, enmAlarmStopMode stopMode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetALM(mAxisNo, enable, logic, stopMode);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetALM", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>到位致能/判定邏輯</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetINP(int axisNo, enmINPEnable enable, enmINPLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetINP(mAxisNo, enable, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetINP", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>緊停邏輯</summary>
        /// <param name="axisNo"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetEMG(int axisNo, enmEmgLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetEMG(mAxisNo, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetEMG", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>觸發停止功能1</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SETIN1Stop(int axisNo, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SETIN1Stop(mAxisNo, enable, mode, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SETIN1Stop", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>觸發停止功能2</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SETIN2Stop(int axisNo, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SETIN2Stop(mAxisNo, enable, mode, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SETIN2Stop", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>觸發停止功能4</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SETIN4Stop(int axisNo, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SETIN2Stop(mAxisNo, enable, mode, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SETIN4Stop", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>觸發停止功能5</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SETIN5Stop(int axisNo, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SETIN5Stop(mAxisNo, enable, mode, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SETIN5Stop", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>編碼器輸入方式</summary>
        /// <param name="axisNo"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPulseIn(int axisNo, enmPulseInMode mode, enmPulseInLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetPulseIn(mAxisNo, mode, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetPulseIn", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>編碼器最大輸入頻率</summary>
        /// <param name="axisNo"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxPulseFrequency(int axisNo, enmEncodePulseInFrequency frequency)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetMaxPulseFrequency(mAxisNo, frequency);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetMaxPulseFrequency", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        /// <summary>設定Latch</summary>
        /// <param name="axisNo"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetLatch(int axisNo, enmLatchEnable enable, enmLatchPLogic logic)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetLatch(mAxisNo, enable, logic);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetMaxPulseFrequency", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>取得Latch位置</summary>
        /// <param name="axisNo"></param>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetLatchPosition(int axisNo, enmPositionType type, ref decimal pos)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                pos = 0;
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            decimal position = default(decimal);
            ret = Items[mItemNo].GetLatchPosition(mAxisNo, type,ref position);
            //2016.06.24 修正讀取錯誤
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetLatchPosition", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            else
            {
                pos = Convert.ToDecimal((position * AxisParameter[axisNo].Parameter.PPU / AxisParameter[axisNo].Parameter.Scale).ToString( "#0.0000"));
            }

            return ret;
        }
        /// <summary>取得Latch旗標</summary>
        /// <param name="axisNo"></param>
        /// <param name="latch"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetLatchFlag(int axisNo, ref bool latch)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                latch = false;
                return CommandStatus.Sucessed;
            }
            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                latch = false;
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].GetLatchFlag(mAxisNo,ref latch);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("GetLatchFlag", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>清除Latch位置資料與旗標</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus ResetLatch(int axisNo)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].ResetLatch(mAxisNo);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("ResetLatch", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }
        /// <summary>雙軸同動(gantry)</summary>
        /// <param name="axisNo">主動軸索引</param>
        /// <param name="slaveAxis">從動軸索引</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetGantry(int axisNo, int slaveAxis)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //軸不存在
            if (IsIndexOutofRange(axisNo))
            {
                return CommandStatus.Sucessed;
            }
            //從動軸不存在
            if (slaveAxis < 0)
            {
                return CommandStatus.Sucessed;
            }
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
            CommandStatus ret = default(CommandStatus);
            ret = Items[mItemNo].SetGantry(mAxisNo, AxisParameter[slaveAxis].CardParameter.AxisNo);
            if (ret == CommandStatus.Alarm)
            {
                gEqpMsg.Add("SetGantry", AlarmIDMap.GetAxisALID(axisNo,EqpID. Error_1030000), eMessageLevel.Error);
            }
            return ret;
        }

        //Eason 20170313 [S]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="axisNo2"></param>
        /// <param name="originPosX"></param>
        /// <param name="originPosY"></param>
        /// <param name="pitchX"></param>
        /// <param name="pitchY"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Dev2DCompensateTable(int axisNo, int axisNo2, decimal originPosX, decimal originPosY, decimal pitchX, decimal pitchY, ref decimal[,] offsetX, ref decimal[,] offsetY)
        {

            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;
            int mItemNo2 = AxisParameter[axisNo2].CardParameter.ItemNo;
            CommandStatus ret = CommandStatus.Alarm;

            if ((mItemNo != mItemNo2))
            {
                ret = CommandStatus.Alarm;
            }
            else
            {
                int mAxisNo = AxisParameter[axisNo].CardParameter.AxisNo;
                int mAxisNo2 = AxisParameter[axisNo2].CardParameter.AxisNo;

                decimal mRateNo = AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU;
                decimal mRateNo2 = AxisParameter[axisNo2].Parameter.Scale / AxisParameter[axisNo2].Parameter.PPU;

                int mXBound0 = offsetX.GetUpperBound(0);
                int mXBound1 = offsetX.GetUpperBound(1);

                int mYBound0 = offsetY.GetUpperBound(0);
                int mYBound1 = offsetY.GetUpperBound(1);

                decimal[,] RealOffsetToMotorCardX = new decimal[mXBound0 + 1, mXBound1 + 1];
                decimal[,] RealOffsetToMotorCardY = new decimal[mYBound0 + 1, mYBound1 + 1];

                for (int index0 = 0; index0 <= mXBound0; index0++)
                {
                    for (int index1 = 0; index1 <= mXBound1; index1++)
                    {
                        RealOffsetToMotorCardX[index0, index1] = offsetX[index0, index1] * mRateNo;
                    }
                }

                for (int index0 = 0; index0 <= mYBound0; index0++)
                {
                    for (int index1 = 0; index1 <= mYBound1; index1++)
                    {
                        RealOffsetToMotorCardY[index0, index1] = offsetY[index0, index1] * mRateNo2;
                    }
                }
                //ret = Items[mItemNo].Dev2DCompensateTable(mAxisNo, mAxisNo2, originPosX, originPosY, pitchX, pitchY, offsetX, offsetY)
                ret = Items[mItemNo].Dev2DCompensateTable(mAxisNo, mAxisNo2, originPosX, originPosY, pitchX, pitchY,ref RealOffsetToMotorCardX,ref RealOffsetToMotorCardY);
                if (ret == CommandStatus.Alarm)
                {
                    gEqpMsg.Add("SetHomeVelHigh", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
                }
            }
            return ret;
        }

        public CommandStatus Dev2DCompensateTableEnable(int axisNo, int axisNo2, bool Enable)
        {
            int mItemNo = AxisParameter[axisNo].CardParameter.ItemNo;

            int mItemNo2 = AxisParameter[axisNo2].CardParameter.ItemNo;
            CommandStatus ret = default(CommandStatus);
            if ((mItemNo != mItemNo2))
            {
                ret = CommandStatus.Alarm;
            }
            else
            {
                ret = Items[mItemNo].Dev2DCompensateTableEnable(Enable);
                if (ret == CommandStatus.Alarm)
                {
                    gEqpMsg.Add("SetHomeVelHigh", AlarmIDMap.GetAxisALID(axisNo, EqpID.Error_1030000), eMessageLevel.Error);
                }
            }
            return ret;
        }
        //Eason 20170313 [E]


        #region "群組動作"

        /// <summary>記錄塞入幾筆資料</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PathCount { get; set; }

        public bool IsIndexOutofRange(CSyncParameter sync)
        {
            if (sync.CardParameter.ItemNo < 0)
            {
                return true;
            }
            if (sync.CardParameter.ItemNo > Items.Count - 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 群組add Axis模式
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddAxis(CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }


            Debug.Print("absCardNo: " + SyncParameter.CardParameter.ItemNo + " Group ");
            return Items[SyncParameter.CardParameter.ItemNo].GpAddAxis(ref SyncParameter, SyncParameter.SyncAxisNo);

        }

        public CommandStatus GpMoveLinearAbsXYZ(CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }

            //對每一插補軸
            for (int i = 0; i <= SyncParameter.TargetPos.GetUpperBound(0); i++)
            {
                //無限大
                //if (double.IsInfinity(SyncParameter.TargetPos[i]))
                //{
                //    return CommandStatus.Alarm;
                //}
                //如果正極限不為0
                if (SyncParameter.SPEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.TargetPos[i] > SyncParameter.SPEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
                //如果負極限不為0
                if (SyncParameter.SNEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.TargetPos[i] < SyncParameter.SNEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
            }

            SyncParameter.TargetPos[0] = SyncParameter.TargetPos[0] * SyncParameter.Scale[0];
            //[X]
            SyncParameter.TargetPos[1] = SyncParameter.TargetPos[1] * SyncParameter.Scale[1];
            //[Y]
            SyncParameter.TargetPos[2] = SyncParameter.TargetPos[2] * SyncParameter.Scale[2];
            //[Z]

            return Items[SyncParameter.CardParameter.ItemNo].GpMoveLinearAbsXYZ(ref SyncParameter);
        }
        /// <summary>[加入點的路徑]</summary>
        /// <param name="IsEndPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddDotPath(CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }

            //對每一插補軸
            for (int i = 0; i <= SyncParameter.TargetPos.GetUpperBound(0); i++)
            {
                //無限大
                //if (double.IsInfinity(SyncParameter.TargetPos[i]))
                //{
                //    return false;
                //}
                //如果正極限不為0
                if (SyncParameter.SPEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.TargetPos[i] > SyncParameter.SPEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
                //如果負極限不為0
                if (SyncParameter.SNEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.TargetPos[i] < SyncParameter.SNEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
            }

            SyncParameter.Velocity.VelHigh = SyncParameter.Velocity.VelHigh * SyncParameter.Scale[0];
            SyncParameter.Velocity.VelLow = SyncParameter.Velocity.VelLow * SyncParameter.Scale[0];
            SyncParameter.TargetPos[0] = SyncParameter.TargetPos[0] * SyncParameter.Scale[0];
            //[X]
            SyncParameter.TargetPos[1] = SyncParameter.TargetPos[1] * SyncParameter.Scale[1];
            //[Y]
            SyncParameter.TargetPos[2] = SyncParameter.TargetPos[2] * SyncParameter.Scale[2];
            //[Z]
            SyncParameter.TargetPos[3] = SyncParameter.TargetPos[3] * SyncParameter.Scale[3];
            //[B]
            SyncParameter.TargetPos[4] = SyncParameter.TargetPos[4] * SyncParameter.Scale[4];
            //[C]

            
            return Items[SyncParameter.CardParameter.ItemNo].GpAddDotPath(ref SyncParameter, IsEndPath);

        }

        /// <summary>[加入圓弧之路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="IsEndPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddArcPath(CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }

            //對每一插補軸
            for (int i = 0; i <= SyncParameter.TargetPos.GetUpperBound(0); i++)
            {
                //無限大
                //if (double.IsInfinity(SyncParameter.TargetPos[i]))
                //{
                //    return false;
                //}
                //如果正極限不為0
                if (SyncParameter.SPEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.TargetPos[i] > SyncParameter.SPEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
                //如果負極限不為0
                if (SyncParameter.SNEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.TargetPos[i] < SyncParameter.SNEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
                //如果正極限不為0
                if (SyncParameter.SPEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.CenterPos[i] > SyncParameter.SPEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
                //如果負極限不為0
                if (SyncParameter.SNEL[i] != 0)
                {
                    //過極限
                    if (SyncParameter.CenterPos[i] < SyncParameter.SNEL[i])
                    {
                        return CommandStatus.Alarm;
                    }
                }
            }

            SyncParameter.Velocity.VelHigh = SyncParameter.Velocity.VelHigh * SyncParameter.Scale[0];
            SyncParameter.Velocity.VelLow = SyncParameter.Velocity.VelLow * SyncParameter.Scale[0];
            SyncParameter.TargetPos[0] = SyncParameter.TargetPos[0] * SyncParameter.Scale[0];
            //[X]
            SyncParameter.TargetPos[1] = SyncParameter.TargetPos[1] * SyncParameter.Scale[1];
            //[Y]
            SyncParameter.TargetPos[2] = SyncParameter.TargetPos[2] * SyncParameter.Scale[2];
            //[Z]
            SyncParameter.TargetPos[3] = SyncParameter.TargetPos[3] * SyncParameter.Scale[3];
            //[B]
            SyncParameter.TargetPos[4] = SyncParameter.TargetPos[4] * SyncParameter.Scale[4];
            //[C]

            SyncParameter.CenterPos[0] = SyncParameter.CenterPos[0] * SyncParameter.Scale[0];
            //[X]
            SyncParameter.CenterPos[1] = SyncParameter.CenterPos[1] * SyncParameter.Scale[1];
            //[Y]
            SyncParameter.CenterPos[2] = SyncParameter.CenterPos[2] * SyncParameter.Scale[2];
            //[Z]
            SyncParameter.CenterPos[3] = SyncParameter.CenterPos[3] * SyncParameter.Scale[3];
            //[B]
            SyncParameter.CenterPos[4] = SyncParameter.CenterPos[4] * SyncParameter.Scale[4];
            //[C]

            return Items[SyncParameter.CardParameter.ItemNo].GpAddArcPath(ref SyncParameter, IsEndPath);

        }

        /// <summary>[加入等待時間之路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddDwell(ref CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpAddDwell(ref SyncParameter);

        }

        /// <summary>
        /// X、Y雙軸同時移動動作
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpMoving(CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpMovePath(ref SyncParameter);

        }

        /// <summary>
        /// 暫停移動Path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpPauseMovePath(CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpPauseMovePath(ref SyncParameter);

        }

        /// <summary>
        /// 清除移動Path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpClearMovePath(CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpClearMovePath(ref SyncParameter);

        }

        /// <summary>[設定運行模式 Buffer/Blending/Fly Mode]</summary>
        /// <param name="syncParameter"></param>
        /// <param name="runMode"></param>
        /// <param name="blendingTime"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GPSetRunMode(ref CSyncParameter syncParameter, eRunMode runMode, int blendingTime = 1024)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(syncParameter))
            {
                return CommandStatus.Alarm;
            }

            return Items[syncParameter.CardParameter.ItemNo].GPSetRunMode(ref syncParameter, runMode, blendingTime);

        }

        /// <summary>
        /// Speed Forward Function
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetSpeedForward(CSyncParameter SyncParameter, enmSFEnable Enable)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpSetSpeedForward(ref SyncParameter, Enable);

        }

        /// <summary>
        /// Blengding Time
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetBlendingTime(CSyncParameter SyncParameter, int Time)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpSetBlendingTime(ref SyncParameter, Time);

        }
        //===20170821============================================================================================================================================
        /// <summary>
        /// 設定負方向軸卡極限
        /// </summary>
        /// <param name="negativeLimit">負極限</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetSNEL(int axisNo, double negativeLimit)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            if (axisNo < 0)
            {
                return CommandStatus.Sucessed;
            }
            decimal mValue = default(decimal);

            mValue = (decimal)negativeLimit * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU;

            
            return Items[AxisParameter[axisNo].CardParameter.ItemNo].SetSNEL(AxisParameter[axisNo].CardParameter.AxisNo, mValue);

        }

        /// <summary>
        /// 設定正方向軸卡極限
        /// </summary>
        /// <param name="maxValve"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetSPEL(int axisNo, double maxValve)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            decimal mValue = default(decimal);

            mValue =(decimal) maxValve * AxisParameter[axisNo].Parameter.Scale / AxisParameter[axisNo].Parameter.PPU;


            
            return Items[AxisParameter[axisNo].CardParameter.ItemNo].SetSPEL(AxisParameter[axisNo].CardParameter.AxisNo, mValue);

        }
        //jimmy 20170823
        /// <summary>
        /// 啟用軸卡軟體極限(負)
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="SwMelEnable"></param>
        /// <returns></returns>
        public CommandStatus SetNELEnable(int axisNo, bool SwMelEnable)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            
            return Items[AxisParameter[axisNo].CardParameter.ItemNo].GpSetSwMelEnable(AxisParameter[axisNo].CardParameter.AxisNo, SwMelEnable);

        }
        /// <summary>
        /// 啟用軸卡軟體極限(正)
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="SwPelEnable"></param>
        /// <returns></returns>
        public CommandStatus SetPELEnable(int axisNo, bool SwPelEnable)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            
            return Items[AxisParameter[axisNo].CardParameter.ItemNo].GpSetSwPelEnable(AxisParameter[axisNo].CardParameter.AxisNo, SwPelEnable);

        }
        /// <summary>
        /// 設定正方向軟件限位的反應模式
        /// </summary>
        /// <param name="PelReact"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetSPELReact(int axisNo, bool PelReact)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            
            return Items[AxisParameter[axisNo].CardParameter.ItemNo].SetSPELReact(AxisParameter[axisNo].CardParameter.AxisNo, PelReact);

        }

        /// <summary>
        /// 設定負方向軸卡限位的反應模式
        /// </summary>
        /// <param name="MelReact"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetSNELReact(int axisNo, bool MelReact)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            
            return Items[AxisParameter[axisNo].CardParameter.ItemNo].SetSNELReact(AxisParameter[axisNo].CardParameter.AxisNo, MelReact);
        }
        //===20170821============================================================================================================================================
        /// <summary>
        /// 設定初速度
        /// </summary>
        /// <param name="VelLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetVelLow(CSyncParameter SyncParameter, double VelLow)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }

            decimal mVelLow = default(decimal);
            mVelLow =(decimal) VelLow * SyncParameter.Scale[0];

            return Items[SyncParameter.CardParameter.ItemNo].GpSetVelLow(ref SyncParameter, mVelLow);

        }

        /// <summary>
        /// 設定最大速度
        /// </summary>
        /// <param name="VelHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetVelHigh(CSyncParameter SyncParameter, double VelHigh)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            decimal mVelHigh = default(decimal);
            mVelHigh =(decimal) VelHigh * SyncParameter.Scale[0];
            
            return Items[SyncParameter.CardParameter.ItemNo].GpSetVelHigh(ref SyncParameter, mVelHigh);

        }

        /// <summary>
        /// 設定移動加速度Move Acc
        /// </summary>
        /// <param name="Acc"></param>
        /// <remarks></remarks>
        public CommandStatus GpSetAcc(CSyncParameter SyncParameter, double Acc)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            decimal mAcc = default(decimal);
            mAcc = (decimal)Acc * SyncParameter.Scale[0];
            
            return Items[SyncParameter.CardParameter.ItemNo].GpSetAcc(ref SyncParameter, mAcc);

        }

        /// <summary>
        /// 設定移動減速度
        /// </summary>
        /// <param name="Dec"></param>
        /// <remarks></remarks>
        public CommandStatus GpSetDec(CSyncParameter SyncParameter, double Dec)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            decimal mDec = default(decimal);
            mDec = (decimal)Dec * SyncParameter.Scale[0];
            
            return Items[SyncParameter.CardParameter.ItemNo].GpSetDec(ref SyncParameter, mDec);

        }

        /// <summary>
        /// Move 完成訊號
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpMoveDone(CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            for (int mSyncNo = 0; mSyncNo <= SyncParameter.SyncAxisNo.Count - 1; mSyncNo++)
            {
                //任一軸狀態異常為異常
                if (CheckMotorStatus(AxisParameter[SyncParameter.SyncAxisNo[mSyncNo]].CardParameter.AxisNo) != CommandStatus.Sucessed)
                {
                    return CommandStatus.Alarm;
                }
            }
            for (int mSyncNo = 0; mSyncNo <= SyncParameter.SyncAxisNo.Count - 1; mSyncNo++)
            {
                int mAxisNo = SyncParameter.SyncAxisNo[mSyncNo];
                if (AxisParameter[mAxisNo].MotionIOStatus.blnINP != true)
                {
                    //Debug.Print(AxisParameter(mAxisNo).AxisName & "等待INP")
                    return CommandStatus.Warning;
                }
            }

            return Items[SyncParameter.CardParameter.ItemNo].GpMoveDone(ref SyncParameter);

        }


        /// <summary>[取得目前路徑執行之狀態]</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpGetPathStatus(CSyncParameter SyncParameter, ref long RemainCount)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpGetPathStatus(ref SyncParameter, ref RemainCount);

        }

        /// <summary>[清除路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpResetPath(ref CSyncParameter SyncParameter)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpResetPath(ref SyncParameter);

        }

        /// <summary>[設定T、S Curve]</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetCurve(CSyncParameter SyncParameter, eCurveMode CurveMode)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }
            //超出範圍無法執行
            if (IsIndexOutofRange(SyncParameter))
            {
                return CommandStatus.Alarm;
            }
            
            return Items[SyncParameter.CardParameter.ItemNo].GpSetCurve(ref SyncParameter, CurveMode);

        }


        #endregion

        #region "Heater"
        public CommandStatus SetHeaterSV(int axis, double vel)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            return Items[AxisParameter[axis].CardParameter.ItemNo].SetHeaterSV(AxisParameter[axis].CardParameter.AxisNo, (int)vel);

        }

        public string ReadHeaterPV(int axis)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return "0";
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return "0";
            }

            
            return Items[AxisParameter[axis].CardParameter.ItemNo].ReadHeaterPV(AxisParameter[axis].CardParameter.AxisNo);

        }
        #endregion

        #region "MotionOut"
        public CommandStatus DOOutput(int Axis, ushort DOChannel, enmCardIOONOFF OnOFF = enmCardIOONOFF.eOFF)
        {
            //模擬
            if (mIsSimulationType == true)
            {
                return CommandStatus.Sucessed;
            }

            //初始化失敗略過
            if (mIsCardIntialOK != true)
            {
                return CommandStatus.Sucessed;
            }

            if (Axis < 0)
            {
                return CommandStatus.Sucessed;
            }
            return Items[AxisParameter[Axis].CardParameter.ItemNo].DOOutput(AxisParameter[Axis].CardParameter.AxisNo, DOChannel, OnOFF);
            //Return Items(.ticket).SetPulseIn(.AxisNo, mode, logic)

        }

        #endregion
    }
}

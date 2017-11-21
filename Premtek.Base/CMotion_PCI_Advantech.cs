using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;
using Advantech.Motion;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Premtek.Base
{


    #region "IO狀態"
    /// <summary>
    /// Motor IO Status
    /// </summary>
    /// <remarks></remarks>
    public struct IOStatus
    {
        /// <summary>[bit0 RDY   RDY pin input]</summary>
        /// <remarks></remarks>
        /// 
        public bool blnRDY;
        /// <summary>[bit1 ALM   Alarm Signal]</summary>
        /// <remarks></remarks>
        public bool blnALM;
        /// <summary>[bit2 PEL   Positive Limit Switch]</summary>
        /// <remarks></remarks>
        public bool blnPEL;
        /// <summary>[bit3 NEL   Negative Limit Switch]</summary>
        /// <remarks></remarks>
        public bool blnNEL;
        /// <summary>[bit4 ORG   Origin Switch]</summary>
        /// <remarks></remarks>
        public bool blnORG;
        /// <summary>[bit5 DIR   DIR output]</summary>
        /// <remarks></remarks>
        public bool blnDIR;
        /// <summary>[bit6 EMG   Emergency signal input]</summary>
        /// <remarks></remarks>
        public bool blnEMG;
        /// <summary>[bit7 PCS   signalinput(not support in PCI-1245/1245E/1265)] </summary>
        /// <remarks></remarks>
        public bool blnPCS;
        /// <summary>[bit8 ERC   Output deflection counter clear signal to a servomotor driver;(OUT7)]</summary>
        /// <remarks></remarks>
        public bool blnERC;
        /// <summary>[bit9 EZ    Encoder Z signal] </summary>
        /// <remarks></remarks>
        public bool blnEZ;
        /// <summary>[bit10 ext. input to Clear postion counter ;(not support in PCI-1245/1245V/1245E/1265]</summary>
        /// <remarks></remarks>
        public bool blnCLR;
        /// <summary>[bit11 LTC  Latch signal input] </summary>
        /// <remarks></remarks>
        public bool blnLTC;
        /// <summary>bit12 SD   Slow Down signal input(not support in PCI-1245/1245V/1245E/1265) </summary>
        /// <remarks></remarks>
        public bool blnSD;
        /// <summary>[bit13 INP  In-Position signal input]</summary>
        /// <remarks></remarks>
        public bool blnINP;
        /// <summary>[bit14 SVON Servo-ON(OUT6)] </summary>
        /// <remarks></remarks>
        public bool blnSVON;
        /// <summary>[bit15 Alarm Reset output status] </summary>
        /// <remarks></remarks>
        public bool blnALRM;
        /// <summary>[bit16 SLMT_P ---- Software Limit+] </summary>
        /// <remarks></remarks>
        public bool blnSPEL;
        /// <summary>[bit17 SLMT_N ---- Software Limit-] </summary>
        /// <remarks></remarks>
        public bool blnSNEL;
        /// <summary>[bit18 TRIG-----Compare signal(OUT5)]</summary>
        /// <remarks></remarks>
        public bool blnTRIG;
        /// <summary>[bit19 CAMDO---- position window do(OUT4)] </summary>
        /// <remarks></remarks>
        public bool blnCAMDO;
    }
    #endregion

    #region "運動狀態"
    /// <summary>
    /// 用以記錄Motion Status
    /// </summary>
    /// <remarks></remarks>
    public struct Status
    {
        /// <summary>[0]</summary>
        /// <remarks></remarks>
        public bool STA_AxDisable;
        /// <summary>[1]</summary>
        /// <remarks></remarks>
        public bool STA_AxReady;
        /// <summary>[2]</summary>
        /// <remarks></remarks>
        public bool STA_Stopping;
        /// <summary>[3]</summary>
        /// <remarks></remarks>
        public bool STA_AxErrorStop;
        /// <summary>[4]</summary>
        /// <remarks></remarks>
        public bool STA_AxHoming;
        /// <summary>[5]</summary>
        /// <remarks></remarks>
        public bool STA_AxPtpMotion;
        /// <summary>[6]</summary>
        /// <remarks></remarks>
        public bool STA_AxContiMotion;
        /// <summary>[7] </summary>
        /// <remarks></remarks>
        public bool STA_AxSyncMotion;
        /// <summary>[8] </summary>
        /// <remarks></remarks>
        public bool STA_AX_EXT_JOG;
        /// <summary>[9] </summary>
        /// <remarks></remarks>
        public bool STA_AX_EXT_MPG;
    }
    #endregion

    /// <summary>同動架構</summary>
    /// <remarks></remarks>
    struct sGantry
    {
        /// <summary>主動軸</summary>
        /// <remarks></remarks>
        public int mMasterAxis;
        /// <summary>從動軸</summary>
        /// <remarks></remarks>
        public int mSlaveAxis;
        public sGantry(int masterAxis, int slaveAxis)
        {
            mMasterAxis = masterAxis;
            mSlaveAxis = slaveAxis;
        }
    }

    /// <summary>運動控制卡 PCI-1245/PCI-1285</summary>
    /// <remarks></remarks>
    public class CMotion_PCI_Advantech : IMotionCard, IDisposable
    {
        /// <summary>外部配接
        /// </summary>
        public CEqpMsgHandler gEqpMsg;

        /// <summary>[軸Handle]</summary>
        /// <remarks></remarks>
        IntPtr[] mAxisHandle = new IntPtr[4];
        /// <summary>軸卡Handle</summary>
        /// <remarks></remarks>

        IntPtr mDeviceHandle = IntPtr.Zero;
        /// <summary>最大支援軸數</summary>
        /// <remarks></remarks>
        public int AxisCount { get; set; }
        /// <summary>命令發送狀態</summary>
        /// <remarks></remarks>
        public CommandStatus cmdStatus { get; set; }


        private bool mEnableOffsetTable;
        public bool EnableOffsetTable
        {
            get { return mEnableOffsetTable; }
        }

        /// <summary>到位逾時計時器</summary>
        /// <remarks></remarks>
        Stopwatch[] mStopWatch;
        /// <summary>安全到位計時器</summary>
        /// <remarks></remarks>
        Stopwatch[] mSafeInposition;
        /// <summary>群組安全到位計時器</summary>
        /// <remarks></remarks>
        Stopwatch[] mSyncSafeInposition;
        public CMotion_PCI_Advantech()
        {
            EndArray = new double[5];
            CenArray = new double[5];
           
        }

        void Create(uint axisCount)
        {
            mStopWatch = new Stopwatch[axisCount + 1];
            mSafeInposition = new Stopwatch[axisCount + 1];
            mSyncSafeInposition = new Stopwatch[axisCount+1];
            for (int i = 0; i <= mStopWatch.GetUpperBound(0); i++)
            {
                mStopWatch[i] = new Stopwatch();
            }
            for (int i = 0; i <= mSafeInposition.GetUpperBound(0); i++)
            {
                mSafeInposition[i] = new Stopwatch();
            }
            for (int i = 0; i <= mSyncSafeInposition.GetUpperBound(0); i++)
            {
                mSyncSafeInposition[i] = new Stopwatch();
            }
            for (int i = 0; i <= m_GpHand.GetUpperBound(0); i++)
            {
                m_GpHand[i] = new IntPtr();
            }
            cmdStatus = CommandStatus.Sucessed;
        }

        #region "Home"
        /// <summary>
        /// 設定快速回Home之速度
        /// </summary>
        /// <param name="velHigh">速度</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeVelHigh(int axisNo, decimal velHigh)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            double _velHigh = (double)velHigh;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.PAR_AxVelHigh), ref  _velHigh, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeVelHigh", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxVelHigh: " + velHigh, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定慢速回Home之速度
        /// </summary>
        /// <param name="VelLow">速度</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeVelLow(int axisNo, decimal velLow)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            double _velLow = (double)velLow;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.PAR_AxVelLow), ref _velLow, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeVelLow", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxVelLow: " + velLow, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }


        /// <summary>
        /// 設定回Home加速度
        /// </summary>
        /// <param name="Acc"></param>
        /// <remarks></remarks>
        public CommandStatus SetHomeAcc(int axisNo, decimal acc)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxAcc), CDbl(acc), CUInt(Marshal.SizeOf(GetType(Double))))
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxAcc, Convert.ToDouble(acc));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeAcc", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxAcc: " + acc, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定回Home減速度
        /// </summary>
        /// <param name="Dec"></param>
        /// <remarks></remarks>
        public CommandStatus SetHomeDec(int axisNo, decimal dec)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxDec), CDbl(dec), CUInt(Marshal.SizeOf(GetType(Double))))
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxDec, Convert.ToDouble(dec));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeDec", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxDec: " + dec, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 馬達回原點
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Home(int axisNo, uint homeMode, uint homeDirection)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);

            errCode = (ErrorCode)Motion.mAcm_AxHome(mAxisHandle[axisNo], homeMode, homeDirection);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: Home", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("HomeMode: " + homeMode, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("HomeDirection: " + homeDirection, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }



        /// <summary>
        /// 馬達Home完成訊號
        /// </summary>
        /// <returns>'Home 完成 '(回傳值ReValue = 1 成功,ReValue= 0 等待)</returns>
        /// <remarks></remarks>
        public CommandStatus HomeFinish(int axisNo)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            ushort mStatus = 0;

            //如啟用到位保護
            if (mSafeInposition[axisNo].IsRunning)
            {
                //則15ms內
                if (mSafeInposition[axisNo].ElapsedMilliseconds < 15)
                {
                    return CommandStatus.Sending;
                    //必不為到位
                }
            }

            errCode = (ErrorCode)Motion.mAcm_AxGetState(mAxisHandle[axisNo], ref mStatus);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: HomeFinish", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //[回Home中]
            if (mStatus == Convert.ToUInt16(AxisState.STA_AX_HOMING))
            {
                return CommandStatus.Warning;
            }
            else
            {
                //[回Home中]
                if (mStatus == Convert.ToUInt16(AxisState.STA_AX_READY))
                {

                    return CommandStatus.Sucessed;
                }
                else
                {
                    return CommandStatus.Warning;
                }
            }

            //MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
            //MDateLog.gSyslog.Save("Axis: (" + axisNo + ")" + " MotorType Not Exists.");
            //return CommandStatus.Alarm;

        }

        public CommandStatus SetHomeOffset(int axisNo, decimal homeOffset)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double _HomeOffset = (double)homeOffset;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxHomeOffsetDistance), ref _HomeOffset, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeDec", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxDec: " + homeOffset, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }
        #endregion

        #region "I/O Status"


        /// <summary>
        /// CheckMotorStatus:檢查Motor狀態
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus CheckMotorStatus(int axisNo, ref IOStatus IOStatus)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            uint mIOStatus = 0;
            errCode = (ErrorCode)Motion.mAcm_AxGetMotionIO(mAxisHandle[axisNo], ref mIOStatus);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: CheckMotorStatus", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            
            IOStatus.blnRDY = ((mIOStatus & ((uint)Math.Pow(2, 0))) != 0 ? true : false);
            IOStatus.blnALM = ((mIOStatus & ((uint)Math.Pow(2, 1))) != 0 ? true : false);
            IOStatus.blnPEL = ((mIOStatus & ((uint)Math.Pow(2, 2))) != 0 ? true : false);
            IOStatus.blnNEL = ((mIOStatus & ((uint)Math.Pow(2, 3))) != 0 ? true : false);
            IOStatus.blnORG = ((mIOStatus & ((uint)Math.Pow(2, 4))) != 0 ? true : false);
            IOStatus.blnDIR = ((mIOStatus & ((uint)Math.Pow(2, 5))) != 0 ? true : false);
            IOStatus.blnEMG = ((mIOStatus & ((uint)Math.Pow(2, 6))) != 0 ? true : false);
            IOStatus.blnPCS = ((mIOStatus & ((uint)Math.Pow(2, 7))) != 0 ? true : false);
            IOStatus.blnERC = ((mIOStatus & ((uint)Math.Pow(2, 8))) != 0 ? true : false);
            IOStatus.blnEZ = ((mIOStatus & ((uint)Math.Pow(2, 9))) != 0 ? true : false);
            IOStatus.blnCLR = ((mIOStatus & ((uint)Math.Pow(2, 10))) != 0 ? true : false);
            IOStatus.blnLTC = ((mIOStatus & ((uint)Math.Pow(2, 11))) != 0 ? true : false);
            IOStatus.blnSD = ((mIOStatus & ((uint)Math.Pow(2, 12))) != 0 ? true : false);
            IOStatus.blnINP = ((mIOStatus & ((uint)Math.Pow(2, 13))) != 0 ? true : false);
            IOStatus.blnSVON = ((mIOStatus & ((uint)Math.Pow(2, 14))) != 0 ? true : false);
            IOStatus.blnALRM = ((mIOStatus & ((uint)Math.Pow(2, 15))) != 0 ? true : false);
            IOStatus.blnSPEL = ((mIOStatus & ((uint)Math.Pow(2, 16))) != 0 ? true : false);
            IOStatus.blnSNEL = ((mIOStatus & ((uint)Math.Pow(2, 17))) != 0 ? true : false);
            IOStatus.blnTRIG = ((mIOStatus & ((uint)Math.Pow(2, 18))) != 0 ? true : false);
            IOStatus.blnCAMDO = ((mIOStatus & ((uint)Math.Pow(2, 19))) != 0 ? true : false);
            return CommandStatus.Sucessed;


        }


        /// <summary>
        /// Get the axis's current state
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetAxisState(int axisNo, ref uint status)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            ushort _status=0;
            errCode = (ErrorCode)Motion.mAcm_AxGetState(mAxisHandle[axisNo], ref _status);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetAxisState", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            status = (uint)_status;
            return CommandStatus.Sucessed;
        }
            ///20171013 新增 get 軸運動狀態
    /// <summary>
    /// Get Motion state
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
     public CommandStatus GetMotionState(int axisNo, ref int status) 
    {
        if(axisNo < 0) 
         {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Axis: (" + axisNo +  ")", "", eMessageLevel.Error);
            return CommandStatus.Alarm;
         }
        if (axisNo > mAxisHandle.GetUpperBound(0))
        {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Axis: (" + axisNo + ")","" , eMessageLevel.Error);
            return CommandStatus.Alarm;
        }
        ErrorCode errCode = default(ErrorCode);
        uint _status = 0;
       errCode = (ErrorCode)Motion.mAcm_AxGetMotionStatus(mAxisHandle[axisNo], ref _status);
     
        if(errCode != ErrorCode.SUCCESS)
         {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Axis: (" + axisNo + ")","" , eMessageLevel.Error);
            MDateLog.gSyslog.Save("Command: GetAxisState","" , eMessageLevel.Error);
            MDateLog.gSyslog.Save("Error Code: " +Convert.ToString((int)errCode,16) + " " + errCode.ToString(),"" , eMessageLevel.Error);
            return CommandStatus.Alarm;
         }
        status = (int)_status;
        return CommandStatus.Sucessed;    
    }

        /// <summary>
        /// Reset the axis' state
        /// If the axis is in ErrorStop state, the state will be changed to Ready after calling this function.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus AxisResetError(int axisNo)
        {

            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_AxResetError(mAxisHandle[axisNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: AxisResetError", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //If mMasterAxis = axisNo And mSlaveAxis <> -1 Then '發生異常軸為Gantry,且Slave軸不為-1
            //    Dim result As UInteger
            //    result = Motion.mAcm_AxGantryInAx(mAxisHandle(mSlaveAxis), mAxisHandle(mMasterAxis), CUShort(0), CUShort(0))
            //    If result <> CInt(ErrorCode.SUCCESS) Then
            //        'MessageBox.Show("Can Not GantryIn", "Gantry", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            //        'AddHistoryAlarm("A0208", "AxisResetError")
            //    End If
            //End If
            return CommandStatus.Sucessed;
        }


        #endregion

        #region "移動"

        /// <summary>
        /// 設定最大速度
        /// </summary>
        /// <param name="VelHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetVelHigh(int axisNo, decimal velHigh)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxVelHigh, Convert.ToDouble(velHigh));
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxVelHigh), CDbl(velHigh), CUInt(Marshal.SizeOf(GetType(Double))))
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetVelHigh", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxVelHigh:" + velHigh, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定初速度
        /// </summary>
        /// <param name="VelLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetVelLow(int axisNo, decimal velLow)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            //Debug.Print(AxisParam(axisNo).AxisName & "SetVelLow:" & VelLow)
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxVelLow, Convert.ToDouble(velLow));
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxVelLow), CDbl(velLow), CUInt(Marshal.SizeOf(GetType(Double))))
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetVelLow", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxVelLow:" + velLow, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;


        }

        /// <summary>[最大加速度]</summary>
        /// <param name="axisNo"></param>
        /// <param name="maxAcc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxAcc(int axisNo, decimal maxAcc)
        {

            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double _maxAcc = (double)maxAcc;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxMaxAcc), ref _maxAcc, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")" + axisNo, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetMaxAcc", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_GpMaxAcc:" + maxAcc, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>[最大減速度]</summary>
        /// <param name="axisNo"></param>
        /// <param name="maxDec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxDec(int axisNo, decimal maxDec)
        {

            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double _maxDec = (double)maxDec;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxMaxDec), ref _maxDec, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")" + axisNo, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetMaxDec", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_GpMaxDec:" + maxDec, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }



        /// <summary>
        /// 設定加速度
        /// </summary>
        /// <param name="Acc"></param>
        /// <remarks></remarks>
        public CommandStatus SetAcc(int axisNo, decimal Acc)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxAcc), CDbl(Acc), CUInt(Marshal.SizeOf(GetType(Double))))
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxAcc, Convert.ToDouble(Acc));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetAcc", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxAcc:" + Acc, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;


        }

        /// <summary>
        /// 設定減速度
        /// </summary>
        /// <param name="Dec"></param>
        /// <remarks></remarks>
        public CommandStatus SetDec(int axisNo, decimal Dec)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxDec), CDbl(Dec), CUInt(Marshal.SizeOf(GetType(Double))))
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxDec, Convert.ToDouble(Dec));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetDec", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxDec:" + Dec, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定馬達移動相對距離
        /// </summary>
        /// <param name="offsetPulse">移動距離</param>
        /// <remarks></remarks>
        public CommandStatus RelMove(int axisNo, decimal offsetPulse)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);

            errCode = (ErrorCode)Motion.mAcm_AxMoveRel(mAxisHandle[axisNo], Convert.ToDouble(offsetPulse));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: RelMove", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_AxMoveRel:" + offsetPulse, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定馬達移動絕對距離
        /// </summary>
        /// <param name="axisNo">本卡軸號</param>
        /// <param name="targetPulse">移動距離</param>
        /// <remarks></remarks>
        public CommandStatus AbsMove(int axisNo, decimal targetPulse)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);

            errCode = (ErrorCode)Motion.mAcm_AxMoveAbs(mAxisHandle[axisNo], Convert.ToDouble(targetPulse));
            if (errCode == ErrorCode.SUCCESS)
            {
                mSafeInposition[axisNo].Restart();
                //軸移動開始計時
                return CommandStatus.Sucessed;
            }
            else
            {
                //Debug.Print(AxisParam(axisNo).AxisName & "命令執行失敗" & Hex(errCode) & " " & errCode.ToString() & " Target:" & Dist & " Now: " & GetPositionValue(axisNo))
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: AbsMove", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_AxMoveAbs:" + targetPulse, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

        }


        /// <summary>
        /// 馬達等速移動
        /// </summary>
        /// <param name="Dir"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus VelMove(int axisNo, eDirection Dir)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //[說明]:To command axis to make a never ending movement with a specified velocity()
            //[說明]:Direction       
            //       0:Positive direction
            //       1: Negative direction
            ushort _dir = (ushort)Dir;
            errCode = (ErrorCode)Motion.mAcm_AxMoveVel(mAxisHandle[axisNo],  _dir);
            if (errCode == ErrorCode.SUCCESS)
            {
                return CommandStatus.Sucessed;
            }
            else
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: JogPlus", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
        }

        /// <summary>
        /// 馬達移動完成訊號(Motion Done)
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus MoveFinish(int axisNo)
        {
            //若停止則回傳True  
            //還在移動則回傳False
            //Dim errCode As ErrorCode
            //Dim MotionStatus As UShort
            //軸不存在
            if (axisNo < 0)
            {
                return CommandStatus.Sucessed;
            }

            //如啟用到位保護
            if (mSafeInposition[axisNo].IsRunning)
            {
                //則前15ms內
                if (mSafeInposition[axisNo].ElapsedMilliseconds < 15)
                {
                    return CommandStatus.Sending;
                    //必不為到位
                }
            }
            return CommandStatus.Sucessed;

        }

        #endregion

        #region "Other"

        /// <summary>
        /// Set Position
        /// </summary>
        /// <param name="nowPosition">要設定的位置</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPosition(int axisNo, decimal nowPosition)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);

            //[說明]:Set command position for the specified axis
            //       設定位置
            errCode = (ErrorCode)Motion.mAcm_AxSetCmdPosition(mAxisHandle[axisNo], Convert.ToDouble(nowPosition));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPosition", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("SetPosition:" + nowPosition, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //[說明]:Set actual position for the specified axis
            //       回饋回來的位置
            errCode = (ErrorCode)Motion.mAcm_AxSetActualPosition(mAxisHandle[axisNo], Convert.ToDouble(nowPosition));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPosition", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("FeedbackPosition:" + nowPosition, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定Motor Servro狀態
        /// </summary>
        /// <param name="ServoState"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Servo(int axisNo, enmONOFF ServoState)
	{
		if (axisNo < 0) {
		 MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
		 MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "" ,eMessageLevel.Error);
			return CommandStatus.Alarm;
		}
		if (axisNo > mAxisHandle.GetUpperBound(0)) {
		 MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
		 MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "" ,eMessageLevel.Error);
			return CommandStatus.Alarm;
		}

		ErrorCode errCode = default(ErrorCode);
		errCode = (ErrorCode)Motion.mAcm_AxSetSvOn(mAxisHandle[axisNo], Convert.ToUInt32(ServoState));
		if (errCode != ErrorCode.SUCCESS) {
		 MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
		 MDateLog.gSyslog.Save("Axis: (" + axisNo + ")","" , eMessageLevel.Error);
		 MDateLog.gSyslog.Save("Command: Servo", "", eMessageLevel.Error);
		 MDateLog.gSyslog.Save("ServoState:" + Convert.ToInt32(ServoState),"" , eMessageLevel.Error);
		 MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(),"" , eMessageLevel.Error);
			return CommandStatus.Alarm;
		}

		for (int i = 0; i <= mGantryList.Count - 1; i++) {
			if (axisNo == mGantryList[i].mMasterAxis) {
				errCode = (ErrorCode)Motion.mAcm_AxSetSvOn(mAxisHandle[mGantryList[i].mSlaveAxis], Convert.ToUInt32(ServoState));
				if (errCode != ErrorCode.SUCCESS) {
				 MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
				 MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
				 MDateLog.gSyslog.Save("Command: Servo(Slave)", "", eMessageLevel.Error);
				 MDateLog.gSyslog.Save("Slave ServoState:" + Convert.ToInt32(ServoState), "", eMessageLevel.Error);
				 MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
					return CommandStatus.Alarm;
				}
				return CommandStatus.Sucessed;
			}
		}


		return CommandStatus.Sucessed;


	}

        /// <summary>
        /// Stop Move(急停)
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus EmgStop(int axisNo)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_AxStopEmg(mAxisHandle[axisNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: EmgStop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        public CommandStatus SlowStop(int axisNo, decimal dec)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_AxStopDecEx(mAxisHandle[axisNo], Convert.ToDouble(dec));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SlowStop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Dec:" + dec, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }
        /// <summary>
        /// 取得Courent Position數值(即Freedback數值)
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetPositionValue(int axisNo)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return "0";
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return "0";
            }

            ErrorCode errCode = default(ErrorCode);
            //Static dblPosition As Decimal = 0
            //軸號不存在
            if (axisNo < 0)
            {
                return "0.000";
            }
            double mPosition = 0;
            errCode =(ErrorCode) Motion.mAcm_AxGetActualPosition(mAxisHandle[axisNo],ref mPosition);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetPositionValue", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                //[說明]:若命令失敗，則數值不更新
                return mPosition.ToString();
            }

            //有開才補
            if (EnableOffsetTable == true)
            {
                double offsetValue = 0;
                if (GetCompensatePosition(axisNo, ref offsetValue) != CommandStatus.Sucessed)
                {
                    return mPosition.ToString();
                }
                //Debug.Print("補償值:" & offsetValue)
                return (mPosition - offsetValue).ToString();
            }
            else
            {
                return mPosition.ToString();
            }


        }
       
        /// <summary>
        /// 取得Courent Command數值
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>

        decimal static_GetCommandValue_dblPosition;
        public CommandStatus GetCommandValue(int axisNo, ref decimal pos)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            ErrorCode errCode = default(ErrorCode);
           
            double cmdPos = 0;
            errCode = (ErrorCode)Motion.mAcm_AxGetCmdPosition(mAxisHandle[axisNo], ref cmdPos);
            if (errCode != ErrorCode.SUCCESS)
            {
                //[說明]:若命令失敗，則數值不更新
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetCommandValue", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            pos = Convert.ToDecimal(cmdPos);
            static_GetCommandValue_dblPosition = pos;
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 讀取目前速度
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetCurrentVel(int axisNo, ref decimal velocity)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double Vel = 0;
            errCode = (ErrorCode)Motion.mAcm_AxGetCmdVelocity(mAxisHandle[axisNo], ref Vel);
            if (errCode != ErrorCode.SUCCESS)
            {
                velocity = 0;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetCurrentVel", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            velocity =(decimal) Vel;
            return CommandStatus.Sucessed;
        }


        #endregion

        #region "Campare"


        /// <summary>
        /// 設定Compare
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetCompare(int axisNo, enmCompareEnable Enable, enmCompareSource Type, enmCompareMethod Method)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //In PCI-1245/1245V/1245E/1265, the default value is 0.

            //Enable/disable axis compare function
            int _Enable = (int)Enable;
            errCode =(ErrorCode) Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxCmpEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetCompare", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxCmpEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //Get/set compare source
            int _Type = (int)Type;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxCmpSrc), ref _Type, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetCompare", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxCmpSrc: " + Convert.ToInt32(Type), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //Set or get compare method
            int _Method = (int)Method;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxCmpMethod), ref _Method, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetCompare", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxCmpMethod: " + Convert.ToInt32(Method), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// Set compare data for the specified axis
        /// </summary>
        /// <param name="mStart"></param>
        /// <param name="mEnd"></param>
        /// <param name="mInterval"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetCampareData(int axisNo, decimal mStart, decimal mEnd, int mInterval)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_AxSetCmpAuto(mAxisHandle[axisNo], Convert.ToDouble(mStart), Convert.ToDouble(mEnd), Convert.ToDouble(mInterval));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetCampareData", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Start Position: " + mStart, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("End Position: " + mEnd, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Interval: " + mInterval, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// Get current compare data in the comparator.
        /// </summary>
        /// <param name="Pos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetCompareValue(int axisNo, ref decimal pos)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double mPos =0;
            errCode = (ErrorCode)Motion.mAcm_AxGetCmpData(mAxisHandle[axisNo], ref mPos);
            if (errCode != ErrorCode.SUCCESS)
            {
                pos = 0;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetCompareValue", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            pos =(decimal) mPos;
            return CommandStatus.Sucessed;
        }
        //====20170821========================================================================================================================================================================
        /// <summary>
        /// 設定負方向軸卡極限
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="negativeLimit"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetSNEL(int axisNo, decimal negativeLimit)
        {
            ErrorCode errCode = default(ErrorCode);

            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            int _NEL = (int)negativeLimit;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxSwMelValue), ref _NEL, Convert.ToUInt32(Marshal.SizeOf(typeof(Int32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: MelValue", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxSwMelValue:" + negativeLimit, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定正方向軸卡極限
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="positiveLimit"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetSPEL(int axisNo, decimal positiveLimit)
        {
            ErrorCode errCode = default(ErrorCode);

            int _PEL = (int)positiveLimit;
            errCode =(ErrorCode) Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxSwPelValue), ref _PEL, Convert.ToUInt32(Marshal.SizeOf(typeof(Int32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PelValue", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxSwPelValue:" + positiveLimit, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定正方向軟件限位的反應模式
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="PelReact"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetPelReact(int axisNo, bool PelReact)
        {
            ErrorCode errCode = default(ErrorCode);
            uint _swPelReact;
            if (PelReact == true)
            {
                 _swPelReact = 1;
            }
            else
            {
                _swPelReact = 0;
               
            }
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxSwPelReact), ref _swPelReact, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PelReact", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxSwPelReact:" + PelReact, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.SetSPELReact(int axisNo, bool PelReact)
        {
            return GpSetPelReact(axisNo, PelReact);
        }

        /// <summary>
        /// 設定負方向軸卡限位的反應模式
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="MelReact"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetMelReact(int axisNo, bool MelReact)
        {
            ErrorCode errCode = default(ErrorCode);
            uint _swNELReact;
            if (MelReact == true)
            {
                _swNELReact = 1;
            }
            else
            {
                _swNELReact = 0;
            }

            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxSwMelReact), ref _swNELReact, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: MelReact", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxSwMelReact:" + MelReact, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.SetSNELReact(int axisNo, bool MelReact)
        {
            return GpSetMelReact(axisNo, MelReact);
        }

        //============================================================================================================================================================================
        //jimmy 20170823
        public CommandStatus GpSetSwPelEnable(int axisNo, bool SWPelEnable)
        {
            ErrorCode errCode = default(ErrorCode);
            uint _swPelEnable;
            if (SWPelEnable == true)
            {
                _swPelEnable = 1;
            }
            else
            {
                _swPelEnable = 0;
            }
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxSwPelEnable), ref _swPelEnable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PelReact", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxSwPelReact:" + SWPelEnable, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }
        public CommandStatus GpSetSwMelEnable(int axisNo, bool SWMelEnable)
        {
            ErrorCode errCode = default(ErrorCode);
            uint _SwNELEnable;
            if (SWMelEnable == true)
            {
                _SwNELEnable = 1;
            }
            else 
            {
                _SwNELEnable = 0;
            }
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxSwMelEnable), ref _SwNELEnable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PelReact", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxSwPelReact:" + SWMelEnable, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }
        #endregion

        #region "Latch"

        public CommandStatus SetMaxVel(int axisNo, decimal maxVel)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //CFG_AxMaxVel
            double _maxVel = (double)maxVel;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxMaxVel), ref _maxVel, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetMaxVel", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxMaxVel: " + maxVel, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// Set Latch Logic
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetLatch(int axisNo, enmLatchEnable enable, enmLatchPLogic logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)設定Enable = True
            uint _enable = (uint)enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], (uint)PropertyID.CFG_AxLatchEnable, ref  _enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetLatch", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxLatchEnable: " + Convert.ToUInt32(enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //2)設定Logic
            uint _logic = (uint)logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], (uint)PropertyID.CFG_AxLatchLogic, ref _logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetLatch", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxLatchLogic: " + Convert.ToUInt32(logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// Get Latch Position
        /// </summary>
        /// <param name="Type">取值的型態</param>
        /// <param name="Pos">Latch位置</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetLatchPosition(int axisNo, enmPositionType type, ref decimal pos)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double Position =0;
            errCode = (ErrorCode)Motion.mAcm_AxGetLatchData(mAxisHandle[axisNo], Convert.ToUInt32(type),ref Position);
            if (errCode != ErrorCode.SUCCESS)
            {
                pos = 0;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetLatchPosition", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_AxGetLatchData: " + Convert.ToInt32(type), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            pos = (decimal)Position;
            //CDbl(Format(Position / AxisParam(axisNo).Parameter.Scale, "#0.0000"))
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// Clear the latch data and latch flag in device.
        /// </summary>
        /// <remarks></remarks>
        public CommandStatus ResetLatch(int axisNo)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_AxResetLatch(mAxisHandle[axisNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: ResetLatch", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// Get the latch flag in device if data is latched
        /// 如果數據鎖存(Flag = 1) 則無法在寫入數據
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetLatchFlag(int axisNo, ref bool latch)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            byte LatchFlag = 0;
            //0：not latched      1：Data is latched 
            errCode = (ErrorCode)Motion.mAcm_AxGetLatchFlag(mAxisHandle[axisNo],ref LatchFlag);
            if (errCode != ErrorCode.SUCCESS)
            {
                latch = false;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GetLatchFlag", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            if (Convert.ToInt32(LatchFlag) == 1)
            {
                latch = true;
            }
            else
            {
                latch = false;
            }
            return CommandStatus.Sucessed;

        }


        /// <summary>
        /// Wait Table Stop
        /// </summary>
        /// <param name="TimeOut">TimeOut上限</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus WaitTableStop(int axisNo, decimal TimeOut = 1000)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            long mStartTime = 0;
            bool Status = false;

            mStopWatch[axisNo].Start();
            mStartTime = mStopWatch[axisNo].ElapsedMilliseconds;
            Status = false;
            do
            {
                if (Math.Abs(mStartTime - mStopWatch[axisNo].ElapsedMilliseconds) > TimeOut)
                {
                    break; // TODO: might not be correct. Was : Exit Do
                }

                if (MoveFinish(axisNo) == CommandStatus.Sucessed)
                {
                    Status = true;
                    break; // TODO: might not be correct. Was : Exit Do
                }

            } while (true);
            mStopWatch[axisNo].Stop();
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Command: WaitTableStop", "", eMessageLevel.Error);
            MDateLog.gSyslog.Save("Error Message: Wait Move Finish Timeout!", "", eMessageLevel.Error);
            if (Status)
            {
                return CommandStatus.Sucessed;
            }
            else
            {
                return CommandStatus.Alarm;
            }
            
        }

        #endregion

        #region "IO 設定 & 頻率"

        /// <summary>
        /// 設定馬達狀態 IO
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus IOSet(int axisNo)
        {

            //Call SetALM(axisNo, enmAlarmEnable.Disable, enmAlarmLogic.HighActive, enmAlarmStopMode.MotorDeceleratesStop)     '設定馬達ALM邏輯訊號
            //Call SetBacklash(axisNo, enmBacklashEnable.Disable)
            //Call SetERC(axisNo, enmErcEnableMode.Disable, enmErcLogic.HighActive)                                           'ERC

            //'*************************External Drive****************************
            //Call SetExternalDrive(axisNo, enmExternalDrive.Axis_0, enmExternalDriveEnable.Disabled, enmExternalDrivePulseInMode.e4XAB)
            //'**********************************************************

            //Call SetHEL(axisNo, enmLimitEnable.Enabled, enmLimitLogic.HighActive, enmLimitStopMode.MotorImmediatelyStop)    '設定馬達極限邏輯訊號

            //'*************************Home****************************
            //Call SetORG(axisNo, enmOrgLogic.LowActive)                                                                      '設定馬達成點邏輯訊號
            //Call SetEZ(axisNo, enmEZLogic.LowActive)                                                                        '設定馬達ZL邏輯訊號  
            //Call SetHomeReset(axisNo, enmHomeReset.Enable)
            //'**********************************************************

            //Call SetINP(axisNo, enmINPEnable.Enable, enmINPLogic.LowActive)                                                 'Set INP

            //'***********************Trigger Stop***********************
            //Call SetIN1Stop(axisNo, enmTriggerStopEnable.Disabled, enmTriggerStopMode.Decelerating, enmTriggerStopLogic.LowActive)
            //Call SetIN2Stop(axisNo, enmTriggerStopEnable.Disabled, enmTriggerStopMode.Decelerating, enmTriggerStopLogic.LowActive)
            //Call SetIN4Stop(axisNo, enmTriggerStopEnable.Disabled, enmTriggerStopMode.Decelerating, enmTriggerStopLogic.LowActive)
            //Call SetIN5Stop(axisNo, enmTriggerStopEnable.Disabled, enmTriggerStopMode.Decelerating, enmTriggerStopLogic.LowActive)
            //'**********************************************************

            //'*************************Latch****************************
            //Call SetLatch(axisNo, enmLatchEnable.Enable, enmLatchPLogic.LowActive)                                          'Set Latch Logic
            //Call ResetLatch(axisNo)                                                                                       'Reset Latch
            //'**********************************************************

            //'*************************PulseIn****************************
            //Call SetPulseIn(axisNo, enmPulseInMode.e4XAB, enmPulseInLogic.NotInverseDirection)                              'Pulse In Logic '
            //Call SetMaxPulseFrequency(axisNo, enmEncodePulseInFrequency.e1M)                                                'Max Pulse Frequency 
            //'**********************************************************

            //Call SetPulseOutMode(axisNo, enmPulseOutMode.CW_CCW)                                                            'Pulse Out Mode
            SetEMG(enmEmgLogic.HighActive);
            //設定馬達EMG邏輯訊號

            //SetMaxVel(axisNo, 5000000)

            //[說明]:軟體極限還沒補
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定分割數
        /// </summary>
        /// <param name="PPU"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPPU(int axisNo, decimal PPU)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //[說明]:Pulse per unit (PPU), a virtual unit.
            //       This property value must be greater than 0.
            //       This property value's change will affect CFG_AxMaxVel, 
            //       CFG_AxMaxAcc, CFG_AxMaxDec, PAR_AxVelHigh, 
            //       PAR_AxVelLow, PAR_AxAcc, PAR_AxDec, PAR_GpVelHigh, 
            //       PAR_GpVelLow, PAR_GpAcc, 
            //       PAR_GpDec,PAR_HomeCrossDistance.
            double mPPU = Convert.ToDouble(PPU);
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxPPU),ref mPPU, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPPU", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxPPU: " + PPU, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定馬達ALM邏輯訊號
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetALM(int axisNo, enmAlarmEnable Enable, enmAlarmLogic Logic, enmAlarmStopMode StopMode)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //[說明]:Please modify "CFG_AxAlmReact" and "CFG_AxAlmLogic" before modifying the value of "CFG_AxAlmEnable".

            //1)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxAlmLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetALM", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxAlmLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //2)Set stop modes
            int _StopMode = (int)StopMode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxAlmReact), ref _StopMode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetALM", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxAlmReact: " + Convert.ToInt32(StopMode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //3)開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxAlmEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetALM", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxAlmEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定馬達Backlash
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetBacklash(int axisNo, enmBacklashEnable Enable)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //In PCI-1245/1245V/1245E/1265, the default value is 10
            int backlashPulse = 10;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxBacklashPulses), ref backlashPulse, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetBacklash", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxBacklashPulses: " + Convert.ToInt32(10), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //In PCI-1245/1245V/1245E/1265, the default value is 1000
            int backlashVel = 1000;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxBacklashVel), ref backlashVel, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetBacklash", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxBacklashVel: " + Convert.ToInt32(1000), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxBacklashEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetBacklash", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxBacklashEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定馬達ERC邏輯訊號
        /// </summary>
        /// <param name="EnableMode"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetERC(int axisNo, enmErcEnableMode EnableMode, enmErcLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxErcLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetERC", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxErcLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            int _EnableMode = (int)EnableMode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxErcEnableMode), ref _EnableMode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetERC", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxErcEnableMode: " + Convert.ToInt32(EnableMode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;


        }

        /// <summary>
        /// 設定External Drive
        /// </summary>
        /// <param name="Drive"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetExternalDrive(int axisNo, enmExternalDrive Drive, enmExternalDriveEnable Enable, enmExternalDrivePulseInMode Mode)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Drive = (int)Drive;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxExtMasterSrc), ref _Drive, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetExternalDrive", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxExtMasterSrc: " + Convert.ToInt32(Drive), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxExtSelEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS & errCode == ErrorCode.AbsMotionNotSupport)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetExternalDrive", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxExtSelEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxExtPulseInMode), ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetExternalDrive", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxExtPulseInMode: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //In this PCI-1245/1245V/1245E/1265, the default value is 1. This value must be larger than zero.
            int _AxExtPulseNum=1;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxExtPulseNum), ref _AxExtPulseNum, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetExternalDrive", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxExtPulseNum: " + Convert.ToInt32(1), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //In PCI-1245/1245E/1265, the default value is 1.This value must lager than zero.
            int _AxExtPresetNum = 1;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxExtPresetNum), ref _AxExtPresetNum, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetExternalDrive", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxExtPresetNum: " + Convert.ToInt32(1), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }


        /// <summary>
        /// 設定硬體極限邏輯訊號
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHEL(int axisNo, enmLimitEnable Enable, enmLimitLogic Logic, enmLimitStopMode StopMode)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)Set stop modes
            int _StopMode = (int)StopMode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxElReact), ref _StopMode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHEL", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxElReact: " + Convert.ToInt32(StopMode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //2)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxElLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHEL", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxElLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //3)Set 開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxElEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHEL", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxElEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }


        /// <summary>
        /// 設定馬達原點邏輯訊號
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetORG(int axisNo, enmOrgLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxOrgLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetORG", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxOrgLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定馬達ZL邏輯訊號
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetEZ(int axisNo, enmEZLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxEzLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetEZ", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxEzLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定SetHomeReset
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeReset(int axisNo, enmHomeReset Enable)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxHomeResetEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeReset", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxHomeResetEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }


        /// <summary>
        /// 設定HomeExSwitchMode
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeExSwitchMode(int axisNo, enmHomeExSwitchMode HomeExSwitchMode)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _HomeExSwitchMode = (int)HomeExSwitchMode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.PAR_AxHomeExSwitchMode), ref _HomeExSwitchMode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeReset", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxHomeExSwitchMode: " + Convert.ToInt32(HomeExSwitchMode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定HomeCrossDistance 
        /// Set the home cross distance (Unit: PPU). 
        /// This property must be greater than 0. The default value is 10000.
        /// </summary>
        /// <param name="HomeCrossDistance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeCrossDistance(int axisNo, decimal HomeCrossDistance)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            double mHomeCrossDistance = 0;
            mHomeCrossDistance = Convert.ToDouble(HomeCrossDistance);
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.PAR_AxHomeCrossDistance),ref mHomeCrossDistance, Convert.ToUInt32(Marshal.SizeOf(typeof(double))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetHomeCrossDistance", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("SetHomeCrossDistance: " + HomeCrossDistance, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定馬達INP邏輯訊號
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetINP(int axisNo, enmINPEnable Enable, enmINPLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxInpEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetINP", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxInpEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //2)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxInpLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetINP", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxInpLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }


        /// <summary>
        /// set INI trigger stop function
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetIN1Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)Mode
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN1StopReact), ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN1Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN1StopReact: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //2)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN1StopLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN1Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN1StopLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //3)開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN1StopEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN1Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN1StopEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }
        CommandStatus IMotionCard.SETIN1Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            return SetIN1Stop(axisNo, Enable, Mode, Logic);
        }

        /// <summary>
        /// set IN2 trigger stop function
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetIN2Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)Mode
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN2StopReact), ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN2Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN2StopReact: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //2)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN2StopLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN2Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN2StopLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //3)開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN2StopEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN2Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN2StopEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;


        }
        CommandStatus IMotionCard.SETIN2Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            return SetIN2Stop(axisNo, Enable, Mode, Logic);
        }

        /// <summary>
        /// set IN4 trigger stop function
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetIN4Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)Mode
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN4StopReact), ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN4Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN4StopReact: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //2)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN4StopLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN4Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN4StopLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //3)開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN4StopEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN4Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN4StopEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }
        CommandStatus IMotionCard.SETIN4Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            return SetIN4Stop(axisNo, Enable, Mode, Logic);
        }

        /// <summary>
        /// set IN5 trigger stop function
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetIN5Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //1)Mode
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN5StopReact),ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN5Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN5StopReact: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //2)更改邏輯訊號
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN5StopLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN5Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN5StopLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            //3)開啟Enabled
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxIN5StopEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetIN5Stop", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxIN5StopEnable: " + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }
        CommandStatus IMotionCard.SETIN5Stop(int axisNo, enmTriggerStopEnable Enable, enmTriggerStopMode Mode, enmTriggerStopLogic Logic)
        {
            return SetIN5Stop(axisNo, Enable, Mode, Logic);
        }

        /// <summary>
        /// 設定馬達Pulse In邏輯訊號
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPulseIn(int axisNo, enmPulseInMode Mode, enmPulseInLogic Logic)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxPulseInMode), ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPulseIn", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxPulseInMode: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            int _Logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxPulseInLogic), ref _Logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPulseIn", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxPulseInLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定馬達最大脈波率
        /// </summary>
        /// <param name="Frequency"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetMaxPulseFrequency(int axisNo, enmEncodePulseInFrequency Frequency)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            uint _Frequency = (uint)Frequency;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxPulseInMaxFreq), ref _Frequency, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetMaxPulseFrequency", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxPulseInMaxFreq: " + Convert.ToUInt32(Frequency), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;

        }

        public CommandStatus SetPulseOutReverse(int axisNo, enmPulseOutReverse pulseOutReverse)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //CFG_AxPulseOutReverse
            ErrorCode errCode = default(ErrorCode);
            int _pulseOutReverse = (int)pulseOutReverse;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxPulseOutReverse), ref _pulseOutReverse, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPulseOutReverse", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxPulseOutReverse: " + Convert.ToInt32(pulseOutReverse), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }
        /// <summary>
        /// 設定馬達Pulse Out Mode
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetPulseOutMode(int axisNo, enmPulseOutMode Mode)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            int _Mode = (int)Mode;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], Convert.ToUInt32(PropertyID.CFG_AxPulseOutMode), ref _Mode, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetPulseOutMode", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_AxPulseOutMode: " + Convert.ToInt32(Mode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定馬達EMG邏輯訊號 Hi/Lo
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetEMG(enmEmgLogic Logic)
        {
            ErrorCode errCode = default(ErrorCode);
            int _logic = (int)Logic;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(mDeviceHandle, Convert.ToUInt32(PropertyID.CFG_DevEmgLogic), ref _logic, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetEMG", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_DevEmgLogic: " + Convert.ToInt32(Logic), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        #endregion


        public uint AxisNum
        {
            get
            {
                return _AxisNum;
            }
            set
            {
             _AxisNum = value   ;
            }
        }
        uint _AxisNum;
        public double[] EndArray { get; set; }
        public double[] CenArray { get; set; }

        public CommandStatus Initial( SMotionConnectParameter item)
        {
            ErrorCode errCode = default(ErrorCode);
            uint mAxisNo = 0;
            uint[] mSlaveDevs = new uint[16];
            uint mAxesPerDev = new uint();
            uint mAxisNumber = 0;
            uint buffLen = 0;
            int mCardNo = 0;
            switch (item.CardType)
            {
                case enmMotionCardType.PCI_1245:
                    mCardNo = item.PCI_1245.CardNo;
                    break;
                case enmMotionCardType.PCI_1285:
                    mCardNo = item.PCI_1285.CardNo;
                    break;
            }

            //Int 直接是卡號 不處理
            if (mCardNo.ToString().Length > 8)
            {
                //Hex
            }
            else if (mCardNo.ToString().Length == 8)
            {
                mCardNo =Convert.ToInt32(Convert.ToString( int.Parse(mCardNo.ToString()),10));
                //將Hex轉為Integer使用
                //資料長度不足,判定為CardID 0.1.2..etc
            }
            else
            {
                ErrorCode Result = 0;
                DEV_LIST[] CurAvailableDevs = new Advantech.Motion.DEV_LIST[Motion.MAX_DEVICES];
                Result = (ErrorCode)Motion.mAcm_GetAvailableDevs(CurAvailableDevs, Motion.MAX_DEVICES,ref deviceCount);
                //取得可用Device清單
                if (Result !=ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009005), "Error_1009005", eMessageLevel.Error);
                    gEqpMsg.AddHistoryAlarm("Error_1009005", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009005), eMessageLevel.Error);
                    //[Can Not Open Device]
                    MDateLog.gSyslog.Save("ErrorCode" + Enum.Parse(typeof(ErrorCode), errCode.ToString()).ToString());
                    return CommandStatus.Alarm;
                }
                if (mCardNo < CurAvailableDevs.Count())
                {
                    mCardNo =(int) CurAvailableDevs[mCardNo].DeviceNum;
                    //取得對應的卡號
                }
                else
                {
                    mCardNo = 0;
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009001), "Error_1009001", eMessageLevel.Error);
                    gEqpMsg.AddHistoryAlarm("Error_1009001", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009001), eMessageLevel.Error);
                    //[Can Not Open Device]
                    MDateLog.gSyslog.Save("ErrorCode" + Enum.Parse(typeof(ErrorCode), errCode.ToString()).ToString());
                    return CommandStatus.Alarm;
                }

            }

            errCode = (ErrorCode)Motion.mAcm_DevOpen((uint)mCardNo,ref mDeviceHandle);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009002), "Error_1009002", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009002", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009002), eMessageLevel.Error);
                //[Can Not Open Device]
                MDateLog.gSyslog.Save("ErrorCode" + Enum.Parse(typeof(ErrorCode), errCode.ToString()).ToString());
                return CommandStatus.Alarm;
            }

            buffLen = 4;
            errCode = (ErrorCode)Motion.mAcm_GetProperty(mDeviceHandle, Convert.ToUInt32(PropertyID.FT_DevAxesCount),ref mAxesPerDev, ref buffLen);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009003), "Error_1009003", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009003", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009003), eMessageLevel.Error);
                //[Get Property Error]
                return CommandStatus.Alarm;
            }
            mAxisNumber = mAxesPerDev;
            buffLen = 64;
            errCode =(ErrorCode) Motion.mAcm_GetProperty(mDeviceHandle, Convert.ToUInt32(PropertyID.CFG_DevSlaveDevs), mSlaveDevs,ref buffLen);
            if (errCode == ErrorCode.SUCCESS)
            {
                mAxisNo = 0;
                while (mSlaveDevs[mAxisNo] != 0)
                {
                    mAxisNumber += mAxesPerDev;
                    mAxisNo += 1;
                }
            }
            mAxisCount = mAxisNumber;
            Create(mAxisCount);
            mAxisHandle = new IntPtr[mAxisCount];
            for (mAxisNo = 0; mAxisNo <= mAxisCount - 1; mAxisNo++)
            {
                //Open every Axis and get the each Axis Handle 
                //And Initial property for each Axis 
                //Open Axis 
                errCode = (ErrorCode)Motion.mAcm_AxOpen(mDeviceHandle, Convert.ToUInt16(mAxisNo), ref mAxisHandle[mAxisNo]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009004), "Error_1009004", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("(" + mAxisNo + ") Handle: " + mAxisHandle[mAxisNo].ToString(), "", eMessageLevel.Error);
                    gEqpMsg.AddHistoryAlarm("Error_1009004", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009004), eMessageLevel.Error);
                    //[Open Axis Failed]
                    return CommandStatus.Alarm;
                }

                //Reset Command Counter 
                double cmdPosition = new double();
                cmdPosition = 0;
                errCode = (ErrorCode)Motion.mAcm_AxSetCmdPosition(mAxisHandle[mAxisNo], cmdPosition);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009004), "Error_1009004", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Axis: (" + mAxisNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: Initial", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command Position:" + cmdPosition, "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                    gEqpMsg.AddHistoryAlarm("Error_1009004", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009004), eMessageLevel.Error);
                }
            }

            uint Value = 0;

            errCode = (ErrorCode)Motion.mAcm_GetU32Property(mDeviceHandle, Convert.ToUInt32(PropertyID.CFG_DevCompensateTableEnable),ref Value);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009004), "Error_1009004", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: mAcm_GetU32Property", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
            }

            if ((Value == 1))
            {
                mEnableOffsetTable = true;
            }
            else
            {
                mEnableOffsetTable = false;
            }

            //Motion.mAcm_SetProperty(mDeviceHandle, CUInt(PropertyID.PAR_AxJerk), 1, 1) '設定為S-Curve

            //For i = 0 To 3
            //    Motion.mAcm_SetProperty(mAxisHandle(i), CUInt(PropertyID.PAR_AxJerk), 1, 1)
            //Next

            return CommandStatus.Sucessed;
        }

        public CommandStatus Close()
        {
            ErrorCode errCode = default(ErrorCode);
            UInt16[] usAxisState = new UInt16[32];
            uint mAxisNo = 0;
            //Stop Every Axes 
            for (mAxisNo = 0; mAxisNo <= mAxisCount - 1; mAxisNo++)
            {
                errCode = (ErrorCode)Motion.mAcm_AxGetState(mAxisHandle[mAxisNo],ref  usAxisState[AxisNum]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: Close", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("mAcm_AxGetState", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Axis: (" + mAxisNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                }
                if (usAxisState[AxisNum] == Convert.ToUInt32(AxisState.STA_AX_ERROR_STOP))
                {
                    errCode = (ErrorCode)Motion.mAcm_AxResetError(mAxisHandle[AxisNum]);
                    if (errCode != ErrorCode.SUCCESS)
                    {
                        MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                        MDateLog.gSyslog.Save("Command: Close", "", eMessageLevel.Error);
                        MDateLog.gSyslog.Save("mAcm_AxResetError", "", eMessageLevel.Error);
                        MDateLog.gSyslog.Save("Axis: (" + mAxisNo + ")", "", eMessageLevel.Error);
                        MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                    }
                }
                errCode = (ErrorCode)Motion.mAcm_AxStopDec(mAxisHandle[AxisNum]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: Close", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("mAcm_AxStopDec", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Axis: (" + mAxisNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                }
            }
            //Close Axes 
            for (mAxisNo = 0; mAxisNo <= mAxisCount - 1; mAxisNo++)
            {
                errCode = (ErrorCode)Motion.mAcm_AxClose(ref mAxisHandle[mAxisNo]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: Close", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("mAcm_AxClose", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Axis: (" + mAxisNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                }
            }
            mAxisCount = 0;
            //Close Device 
            errCode = (ErrorCode)Motion.mAcm_DevClose(ref mDeviceHandle);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: Close", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_DevClose", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            mDeviceHandle = IntPtr.Zero;
            return CommandStatus.Sucessed;
        }

        public string GetErrorMessage(int axisNo)
        {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000003), "Error_1000003", eMessageLevel.Error);
            MDateLog.gSyslog.Save("GetErrorMessage", "", eMessageLevel.Error);
            return "GetErrorMessage." + MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000003);
        }

        uint DeviceNum = 0;
        uint deviceCount = 0;
        /// <summary>軸數</summary>
        /// <remarks></remarks>

        uint mAxisCount = 0;

        public CommandStatus SetCurve(int axisNo, eCurveMode curveMode)
        {
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(mAxisHandle[axisNo], CUInt(PropertyID.PAR_AxJerk), onOff, 8)
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(mAxisHandle[axisNo], (uint)PropertyID.PAR_AxJerk, Convert.ToInt32(curveMode));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetCurve", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_AxJerk:" + curveMode, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        List<sGantry> mGantryList = new List<sGantry>();
        //Dim mMasterAxis As Integer = -1
        //Dim mSlaveAxis As Integer = -1
        public CommandStatus SetGantry(int masterAxis, int slaveAxis)
        {
            if (masterAxis < 0)
            {
                //gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error)
                MDateLog.gSyslog.Save("Axis: (" + masterAxis + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (masterAxis > mAxisHandle.GetUpperBound(0))
            {
                //gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error)
                MDateLog.gSyslog.Save("Axis: (" + masterAxis + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (slaveAxis < 0)
            {
                //gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error)
                MDateLog.gSyslog.Save("Axis: (" + slaveAxis + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (slaveAxis > mAxisHandle.GetUpperBound(0))
            {
                //gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error)
                MDateLog.gSyslog.Save("Axis: (" + slaveAxis + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            //與既有資料比對, 如果沒有就新增記錄
            if (mGantryList.Count == 0)
            {
                mGantryList.Add(new sGantry(masterAxis, slaveAxis));
            }
            else
            {
                bool isNotExist = false;
                for (int mGroupNo = 0; mGroupNo <= mGantryList.Count - 1; mGroupNo++)
                {
                    if (mGantryList[mGroupNo].mMasterAxis != masterAxis & mGantryList[mGroupNo].mSlaveAxis != slaveAxis)
                    {
                        isNotExist = true;
                    }
                }
                if (isNotExist)
                {
                    mGantryList.Add(new sGantry(masterAxis, slaveAxis));
                }
            }


            try
            {
                short src = 0;
                short dir = 0;
                errCode = (ErrorCode)Motion.mAcm_AxGantryInAx(mAxisHandle[slaveAxis], mAxisHandle[masterAxis], src, dir);
                if (errCode != ErrorCode.SUCCESS)
                {
                    //gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error)
                    //gSyslog.Save("Axis: (" & masterAxis & ")", "" ,eMessageLevel.Error)
                    //gSyslog.Save("Command: SetGantry", "" ,eMessageLevel.Error)
                    //gSyslog.Save("Slave Axis: (" & slaveAxis & ")", "" ,eMessageLevel.Error)
                    //gSyslog.Save("Error Code: " & Hex(errCode) & " " & errCode.ToString(), "" ,eMessageLevel.Error)
                    //gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "" ,gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error)
                    return CommandStatus.Alarm;
                }
                return CommandStatus.Sucessed;

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000004), "Error_1000004", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

        }

        public CommandStatus SetScale(int axis, decimal scale)
        {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000003), "Error_1000003", eMessageLevel.Error);
            MDateLog.gSyslog.Save("SetScale", "", eMessageLevel.Error);
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetEMG(int axis, enmEmgLogic logic)
        {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000003), "Error_1000003", eMessageLevel.Error);
            MDateLog.gSyslog.Save("SetEMG", "", eMessageLevel.Error);
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHeaterSV(int axisNo, int valve)
        {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000003), "Error_1000003", eMessageLevel.Error);
            MDateLog.gSyslog.Save("SetHeaterSV", "", eMessageLevel.Error);
            return CommandStatus.Sucessed;
        }

        public string ReadHeaterPV(int axisNo)
        {
            MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1000003), "Error_1000003", eMessageLevel.Error);
            MDateLog.gSyslog.Save("ReadHeaterPV", "", eMessageLevel.Error);
            return "0";
        }

        #region "群組動作"

        /// <summary>宣告軸的旗標</summary>
        /// <remarks></remarks>
        //Implements IMotionCard.m_GpHand
        IntPtr[] m_GpHand = new IntPtr[8];

        /// <summary>兩軸直線插補運動</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpMoveLinearAbsXYZ(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);
            double[] pos = new double[32];
            pos[0] = (double)SyncParameter.TargetPos[0];
            pos[1] = (double)SyncParameter.TargetPos[1];
            pos[2] = (double)SyncParameter.TargetPos[2];
            uint axisNum = 3;
            errCode =(ErrorCode) Motion.mAcm_GpMoveLinearAbs(m_GpHand[SyncParameter.CardParameter.GroupNo], pos, ref axisNum);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: Moving", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            mSyncSafeInposition[SyncParameter.CardParameter.GroupNo].Restart();
            //計時開始

            return CommandStatus.Sucessed;
        }
        /// <summary>
        /// 群組軸同時移動動作
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpMovePath(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);
            IntPtr _pathHandle = new IntPtr(0);
            errCode = (ErrorCode)Motion.mAcm_GpMovePath(m_GpHand[SyncParameter.CardParameter.GroupNo], _pathHandle);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: Moving", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            mSyncSafeInposition[SyncParameter.CardParameter.GroupNo].Restart();
            //計時開始
            //Debug.Print("GpMovePath.Timer.Restart")
            SyncParameter.PathCount = 0;
            return CommandStatus.Sucessed;

        }

        /// <summary>0:使用速度交接模式 1:禁用速度交接模式</summary>
        /// <remarks></remarks>

        ushort mMoveMode;
        /// <summary>設定運行模式</summary>
        /// <param name="syncParameter"></param>
        /// <param name="runMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GPSetRunMode(ref CSyncParameter syncParameter, eRunMode runMode, int blendingTime = 1024)
        {
            switch (runMode)
            {
                case eRunMode.BufferMode:
                    //[Note]:不支援速度前瞻、禁用速度交接
                    GpSetBlendingTime(ref syncParameter, 0);
                    GpSetSpeedForward(ref syncParameter, enmSFEnable.Disabled);
                    GpSetCurve(ref syncParameter, eCurveMode.SCurve);
                    mMoveMode = 2;//1;

                    return CommandStatus.Sucessed;
                case eRunMode.BlendingMode:
                    //[Note]:不支援速度前瞻、速度交接
                    GpSetBlendingTime(ref syncParameter, blendingTime);
                    GpSetSpeedForward(ref syncParameter, enmSFEnable.Disabled);
                    GpSetCurve(ref syncParameter, eCurveMode.TCurve);
                    mMoveMode = 0;

                    return CommandStatus.Sucessed;
                case eRunMode.FlyMode:
                    //[Note]:速度交接
                    GpSetBlendingTime(ref syncParameter, 0);
                    GpSetSpeedForward(ref syncParameter, enmSFEnable.Enable);
                    GpSetCurve(ref syncParameter, eCurveMode.TCurve);
                    mMoveMode = 0;
                    return CommandStatus.Sucessed;
            }
            return CommandStatus.Alarm;
        }

        /// <summary>
        /// 暫停移動Path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpPauseMovePath(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);
            ushort State = 0;
            errCode = (ErrorCode)Motion.mAcm_GpResetPath(ref m_GpHand[SyncParameter.CardParameter.GroupNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PauseMovePath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_GpResetPath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            errCode = (ErrorCode)Motion.mAcm_GpGetState(m_GpHand[SyncParameter.CardParameter.GroupNo],ref State);
            if (errCode == ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PauseMovePath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_GpGetState", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if ((GroupState)State == GroupState.STA_Gp_ErrorStop)
            {
                errCode = (ErrorCode)Motion.mAcm_GpResetError(m_GpHand[SyncParameter.CardParameter.GroupNo]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: PauseMovePath", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("mAcm_GpResetError", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                    return CommandStatus.Alarm;
                }
            }
            errCode = (ErrorCode)Motion.mAcm_GpStopEmg(m_GpHand[SyncParameter.CardParameter.GroupNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: PauseMovePath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_GpStopEmg", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;


        }

        /// <summary>
        /// 清除移動Path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpClearMovePath(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);
            ushort State = 0;

            //[說明]:Clear system path buffer. If there is group executing path, the path 
            //       motion will be stopped
            errCode = (ErrorCode)Motion.mAcm_GpResetPath(ref m_GpHand[SyncParameter.CardParameter.GroupNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: ClearMovePath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_GpResetPath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            
            errCode = (ErrorCode)Motion.mAcm_GpGetState(m_GpHand[SyncParameter.CardParameter.GroupNo], ref State);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: ClearMovePath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_GpGetState", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //[說明]:If the group is in STA_GP_ERROR_STOP state, the state will be 
            //       changed to STA_GP_READY after calling this function.
            if ((GroupState)State == GroupState.STA_Gp_ErrorStop)
            {
                errCode = (ErrorCode)Motion.mAcm_GpResetError(m_GpHand[SyncParameter.CardParameter.GroupNo]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: ClearMovePath", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("mAcm_GpResetError", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                    return CommandStatus.Alarm;
                }
            }

            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 群組add Axis模式
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddAxis(ref CSyncParameter SyncParameter, List<int> AxisNoList)
        {
            ErrorCode errCode = default(ErrorCode);
            uint buffLen = 0;
            uint AxesInfoInGp = new uint();
            uint AxCountInGp = 0;
            //無清單
            if (AxisNoList.Count == 0)
            {
                return CommandStatus.Sucessed;
            }
            //對於清單中每一筆
            for (int AxisIndex = 0; AxisIndex <= AxisNoList.Count - 1; AxisIndex++)
            {
                //加入AXisNoList(Index)的軸號指定的Handle
                errCode =(ErrorCode) Motion.mAcm_GpAddAxis(ref m_GpHand[SyncParameter.CardParameter.GroupNo], mAxisHandle[AxisNoList[AxisIndex]]);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: GroupAddAxes", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("mAcm_GpAddAxis", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                    return CommandStatus.Alarm;
                }
                //Debug.Print("m_GpHand " & m_GpHand.ToString)
                //`add axis success 
                AxCountInGp += 1;
                buffLen = 4;
                errCode = (ErrorCode)Motion.mAcm_GetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.CFG_GpAxesInGroup), ref AxesInfoInGp, ref buffLen);
                if (errCode != ErrorCode.SUCCESS)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Command: GroupAddAxes", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("CFG_GpAxesInGroup", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                    return CommandStatus.Alarm;
                }

            }
            Debug.Print("Group Handle: " + m_GpHand[SyncParameter.CardParameter.GroupNo].ToString());
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// Speed Forward Function
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetSpeedForward(ref CSyncParameter SyncParameter, enmSFEnable Enable)
        {

            ErrorCode errCode = default(ErrorCode);
            int _Enable = (int)Enable;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.CFG_GpSFEnable), ref _Enable, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetSpeedForward", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_GpSFEnable:" + Convert.ToInt32(Enable), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// Blending Time
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetBlendingTime(ref CSyncParameter SyncParameter, int Time)
        {
            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.CFG_GpBldTime), ref Time, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetBlendingTime", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("CFG_GpBldTime:" + Time, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定初速度
        /// </summary>
        /// <param name="VelLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetVelLow(ref CSyncParameter SyncParameter, decimal VelLow)
        {
            ErrorCode errCode = default(ErrorCode);
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], CUInt(PropertyID.PAR_GpVelLow), CDbl(VelLow), CUInt(Marshal.SizeOf(GetType(Double))))
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.PAR_GpVelLow), Convert.ToDouble(VelLow));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GroupSetVelLow", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_GpVelLow:" + VelLow, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// 設定最大速度
        /// </summary>
        /// <param name="VelHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetVelHigh(ref CSyncParameter SyncParameter, decimal VelHigh)
        {
            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.PAR_GpVelHigh), Convert.ToDouble(VelHigh));
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], CUInt(PropertyID.PAR_GpVelHigh), CDbl(VelHigh), CUInt(Marshal.SizeOf(GetType(Double))))
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GpSetVelHigh", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_GpVelHigh:" + VelHigh, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定移動加速度Move Acc
        /// </summary>
        /// <param name="Acc"></param>
        /// <remarks></remarks>
        public CommandStatus GpSetAcc(ref CSyncParameter SyncParameter, decimal Acc)
        {
            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.PAR_GpAcc), Convert.ToDouble(Acc));
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], CUInt(PropertyID.PAR_GpAcc), CDbl(Acc), CUInt(Marshal.SizeOf(GetType(Double))))
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GroupSetAcc", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_GpAcc:" + Acc, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        /// <summary>
        /// 設定移動減速度
        /// </summary>
        /// <param name="Dec"></param>
        /// <remarks></remarks>
        public CommandStatus GpSetDec(ref CSyncParameter SyncParameter, decimal Dec)
        {
            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.PAR_GpDec), Convert.ToDouble(Dec));
            //errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], CUInt(PropertyID.PAR_GpDec), CDbl(Dec), CUInt(Marshal.SizeOf(GetType(Double))))
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GroupSetDec", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_GpDec:" + Dec, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;
        }

        /// <summary>
        /// Move 完成訊號
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpMoveDone(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);
            ushort GpState = 0;

            errCode = (ErrorCode)Motion.mAcm_GpGetState(m_GpHand[SyncParameter.CardParameter.GroupNo],ref GpState);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GpMoveDone", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("mAcm_GpGetState", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            if ((GroupState)GpState == GroupState.STA_Gp_ErrorStop)
            {
                return CommandStatus.Alarm;
            }
            if (GpState != 1)
            {
                return CommandStatus.Warning;
            }

            //未啟動
            if (mSyncSafeInposition[SyncParameter.CardParameter.GroupNo].ElapsedMilliseconds == 0)
            {
                return CommandStatus.Sucessed;
            }
            else if (mSyncSafeInposition[SyncParameter.CardParameter.GroupNo].ElapsedMilliseconds < 50)
            {
                return CommandStatus.Warning;
            }
            return CommandStatus.Sucessed;

        }

        /// <summary>[加入點的路徑]</summary>
        /// <param name="SyncParameter">同動參數</param>
        /// <param name="IsEndPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddDotPath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {

            ErrorCode errCode = default(ErrorCode);
            UInt16 MoveCmd = default(UInt16);
            double VelFH = 0;
            double VelFL = 0;

            if (IsEndPath == true)
            {
                MoveCmd = Convert.ToUInt16(PathCmd.EndPath);
            }
            else
            {
                MoveCmd = Convert.ToUInt16(SyncParameter.Cmd);
            }

            VelFH = Convert.ToDouble(SyncParameter.Velocity.VelHigh);
            VelFL = Convert.ToDouble(SyncParameter.Velocity.VelLow);

            EndArray[0] = Convert.ToInt32(SyncParameter.TargetPos[0]);
            //[X]
            EndArray[1] = Convert.ToInt32(SyncParameter.TargetPos[1]);
            //[Y]
            EndArray[2] = Convert.ToInt32(SyncParameter.TargetPos[2]);
            //[Z]
            EndArray[3] = Convert.ToInt32(SyncParameter.TargetPos[3]);
            //[B]
            EndArray[4] = Convert.ToInt32(SyncParameter.TargetPos[4]);
            //[C]

            switch ((PathCmd)MoveCmd)
            {
                case PathCmd.Abs2DLine:
                    AxisNum = 2;
                    break;
                case PathCmd.Abs3DLine:
                    AxisNum = 3;
                    break;
                case PathCmd.Abs4DDirect:
                    AxisNum = 4;
                    break;
                case PathCmd.Abs5DDirect:
                    AxisNum = 5;
                    break;
            }
            //Debug.Print("Cmd:" & MoveCmd & " Mode:" & mMoveMode & " FH:" & VelFH & " FL:" & VelFL & " End(" & EndArray(0) & "," & EndArray(1) & "," & EndArray(2) & ")")
            //[Note]:MoveMode:0 表示使能速度交接模式(直角)  Blending
            //                1     禁用速度交接模式(弧角)  No Blending 
            //20171016
            errCode = (ErrorCode)Motion.mAcm_GpAddPath(m_GpHand[SyncParameter.CardParameter.GroupNo], MoveCmd, mMoveMode, VelFH, VelFL, EndArray, null, ref _AxisNum);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: AddMovePath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Move Command: " + MoveCmd, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("VelFH: " + VelFH, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("VelFL: " + VelFL, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("End Psition: (" + EndArray[0].ToString() + "," + EndArray[1].ToString() + "," + EndArray[2].ToString() + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            //gSyslog.Save("Card( " & SyncParameter.CardParameter.ItemNo & ") AddMovePath Cmd:" & MoveCmd & " Pos(" & EndArray(0) & "," & EndArray(1) & "," & EndArray(2) & "," & EndArray(3) & "," & EndArray(4) & ") FH: " & VelFH)
            SyncParameter.PathCount += 1;
            return CommandStatus.Sucessed;

        }

        /// <summary>[加入圓弧的路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="IsEndPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpAddArcPath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            ErrorCode errCode = default(ErrorCode);
            UInt16 MoveCmd = default(UInt16);
            double VelFH = 0;
            double VelFL = 0;

            if (IsEndPath == true)
            {
                MoveCmd = Convert.ToUInt16(PathCmd.EndPath);
            }
            else
            {
                MoveCmd = Convert.ToUInt16(SyncParameter.Cmd);
            }

            VelFH = Convert.ToDouble(SyncParameter.Velocity.VelHigh);
            VelFL = Convert.ToDouble(SyncParameter.Velocity.VelLow);

            EndArray[0] = Convert.ToInt32(SyncParameter.TargetPos[0]);
            //[X]
            EndArray[1] = Convert.ToInt32(SyncParameter.TargetPos[1]);
            //[Y]
            EndArray[2] = Convert.ToInt32(SyncParameter.TargetPos[2]);
            //[Z]
            EndArray[3] = Convert.ToInt32(SyncParameter.TargetPos[3]);
            //[B]
            EndArray[4] = Convert.ToInt32(SyncParameter.TargetPos[4]);
            //[C]

            CenArray[0] = Convert.ToInt32(SyncParameter.CenterPos[0]);
            //[X]
            CenArray[1] = Convert.ToInt32(SyncParameter.CenterPos[1]);
            //[Y]
            CenArray[2] = Convert.ToInt32(SyncParameter.CenterPos[2]);
            //[Z]
            CenArray[3] = Convert.ToInt32(SyncParameter.CenterPos[3]);
            //[B]
            CenArray[4] = Convert.ToInt32(SyncParameter.CenterPos[4]);
            //[C]
            //Debug.Print("Cmd:" & MoveCmd & " Mode:" & mMoveMode & " FH:" & VelFH & " FL:" & VelFL & " End(" & EndArray(0) & "," & EndArray(1) & "," & EndArray(2) & ")")

            //[Note]:MoveMode:0 表示使能速度交接模式(直角)  Blending
            //                1     禁用速度交接模式(弧角)  No Blending 
            //20171016
            errCode = (ErrorCode)Motion.mAcm_GpAddPath(m_GpHand[SyncParameter.CardParameter.GroupNo], MoveCmd, mMoveMode, VelFH, VelFL, EndArray, CenArray, ref _AxisNum);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: AddArcPath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Move Command: " + MoveCmd, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("VelFH: " + VelFH, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("VelFL: " + VelFL, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("End Position: " + EndArray[0].ToString() + "," + EndArray[1].ToString(), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Center Position: " + CenArray[0].ToString() + "," + CenArray[1].ToString(), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "",MDateLog. gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            SyncParameter.PathCount += 1;
            return CommandStatus.Sucessed;

        }

        public CommandStatus GpAddDwell(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);

            //[Note]:MoveMode:0 表示使能速度交接模式(直角)  Blending
            //                1     禁用速度交接模式(弧角)  No Blending 
            errCode = (ErrorCode)Motion.mAcm_GpAddPath(m_GpHand[SyncParameter.CardParameter.GroupNo], (ushort)PathCmd.GPDELAY, mMoveMode, Convert.ToDouble(SyncParameter.GpDelay), 0, null, null, ref  _AxisNum);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: AddArcPath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Move Command: " + PathCmd.GPDELAY, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("DelayTime: " + SyncParameter.GpDelay, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            SyncParameter.PathCount += 1;
            return CommandStatus.Sucessed;

        }

        /// <summary>[取得目前路徑執行之狀態]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="remainCount"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpGetPathStatus(ref CSyncParameter SyncParameter, ref long remainCount)
        {

            ErrorCode errCode = default(ErrorCode);
            uint mIndex = 0;
            uint mFreeCnt = 0;
            uint mCurCmd = default(UInt16);
            uint _remainCount=(uint)remainCount;
            errCode = (ErrorCode)Motion.mAcm_GpGetPathStatus(m_GpHand[SyncParameter.CardParameter.GroupNo], ref mIndex, ref mCurCmd, ref _remainCount, ref mFreeCnt);
            //Debug.Print("GpGetPathStatus: " & mIndex & " , " & mCurCmd & " , " & remainCount & " , " & mFreeCnt)
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GpGetPathStatus", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            remainCount = (long)_remainCount;
            return CommandStatus.Sucessed;
        }

        /// <summary>[路徑清除]</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpResetPath(ref CSyncParameter SyncParameter)
        {
            ErrorCode errCode = default(ErrorCode);

            errCode = (ErrorCode)Motion.mAcm_GpResetPath(ref m_GpHand[SyncParameter.CardParameter.GroupNo]);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GpResetPath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        #endregion

        /// <summary>將Port開放</summary>
        /// <param name="AxisNo">Port號</param>
        /// <param name="DOChannel">Bit</param>
        /// <param name="OnOFF"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus DOOutput(int AxisNo, ushort DOChannel, enmCardIOONOFF OnOFF = enmCardIOONOFF.eOFF)
        {
            if (AxisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + AxisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (AxisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + AxisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);

            errCode = (ErrorCode)Motion.mAcm_AxDoSetBit(mAxisHandle[AxisNo], DOChannel, Convert.ToByte(OnOFF));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: AddArcPath", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + AxisNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("DOChannel: " + DOChannel, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("OnOFF: " + OnOFF, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                gEqpMsg.AddHistoryAlarm("Error_1009006", "CMotion_PCI_1245", "", MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>設定參考平面</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object GpSetRefPlane(ref CSyncParameter SyncParameter, ePlane plane)
        {
            ErrorCode errCode = default(ErrorCode);
            int _plane = (int)plane;
            errCode = (ErrorCode)Motion.mAcm_SetProperty(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.PAR_GpRefPlane),ref _plane, Convert.ToUInt32(Marshal.SizeOf(typeof(UInt32))));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: SetGpRefPlane", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("PAR_GpRefPlane:" + Convert.ToInt32(plane), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        /// <summary>[設定T or S Curve]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="curveMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GpSetCurve(ref CSyncParameter SyncParameter, eCurveMode curveMode)
        {
            if (SyncParameter.CardParameter.GroupNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (SyncParameter.CardParameter.GroupNo >= m_GpHand.Count())
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_SetF64Property(m_GpHand[SyncParameter.CardParameter.GroupNo], Convert.ToUInt32(PropertyID.PAR_GpJerk), Convert.ToInt32(curveMode));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: GpSetCurve", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Group: (" + SyncParameter.CardParameter.GroupNo + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("GpSetCurve:" + Convert.ToInt32(curveMode), "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }

            return CommandStatus.Sucessed;
        }

        //Eason 20170313 
        /// <summary>
        /// 表格補正
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
            if (axisNo < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo2 < 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo2 + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (axisNo2 > mAxisHandle.GetUpperBound(0))
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo2 + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (pitchX == 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo2 + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            if (pitchY == 0)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo2 + ")", "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }


            int mCountX = offsetX.GetUpperBound(0) + 1;
            int mCountY = offsetX.GetUpperBound(1) + 1;
            int mSize = mCountX * mCountY - 1;
            float[] mOffsetX = new float[mSize + 1];
            float[] mOffsetY = new float[mSize + 1];
            for (int mXNo = 0; mXNo <= mCountX - 1; mXNo++)
            {
                for (int mYNo = 0; mYNo <= mCountY - 1; mYNo++)
                {
                    mOffsetX[mYNo + mXNo * mCountY] =(float) offsetX[mXNo, mYNo];
                    mOffsetY[mYNo + mXNo * mCountY] = (float)offsetY[mXNo, mYNo];
                }
            }

            ErrorCode errCode = default(ErrorCode);
            errCode = (ErrorCode)Motion.mAcm_Dev2DCompensateTable(mDeviceHandle, mAxisHandle[axisNo], mAxisHandle[axisNo2], (float)originPosX, (float)originPosY, (float)pitchX, (float)pitchY, mOffsetX, mOffsetY, (uint)(offsetX.GetUpperBound(0) + 1), (uint)(offsetX.GetUpperBound(1) + 1));
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Axis: (" + axisNo + ") & (" + axisNo2 + ")", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: Acm_Dev2DCompensateTable", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            return CommandStatus.Sucessed;

        }

        public CommandStatus Dev2DCompensateTableEnable(bool Enable)
        {
            ErrorCode errCode = default(ErrorCode);
            uint Value = 0;
            if ((Enable))
            {
                Value = 1;
            }
            errCode = (ErrorCode)Motion.mAcm_SetU32Property(mDeviceHandle, Convert.ToUInt32(PropertyID.CFG_DevCompensateTableEnable), Value);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: Acm_Dev2DCompensateTable", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            mEnableOffsetTable = (Value == 1 ? true : false);
            return CommandStatus.Sucessed;

        }

        public CommandStatus GetCompensatePosition(int axisNo, ref double OffsetValue)
        {
            ErrorCode errCode = default(ErrorCode);
            double Value = 0;

            errCode = (ErrorCode)Motion.mAcm_AxGetCompensatePosition(mAxisHandle[axisNo],ref Value);
            if (errCode != ErrorCode.SUCCESS)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1009006), "Error_1009006", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Command: mAcm_AxGetCompensatePosition", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Error Code: " + Convert.ToString((int)errCode,16) + " " + errCode.ToString(), "", eMessageLevel.Error);
                return CommandStatus.Alarm;
            }
            OffsetValue = Value;
            return CommandStatus.Sucessed;

        }

        #region "IDisposable Support"
        // 偵測多餘的呼叫
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {

                if (disposing)
                {
                }

            }
            this.disposedValue = true;
        }


        // 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
       
        #endregion

    }
}

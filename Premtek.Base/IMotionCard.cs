using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;


namespace Premtek.Base
{
    /// <summary>將MotionStatus拆除</summary>
    /// <remarks></remarks>
    public enum AxisState
    {
        STA_AX_DISABLE = 0,
        STA_AX_READY = 1,
        STA_AX_STOPPING = 2,
        STA_AX_ERROR_STOP = 3,
        STA_AX_HOMING = 4,
        STA_AX_PTP_MOT = 5,
        STA_AX_CONTI_MOT = 6,
        STA_AX_SYNC_MOT = 7,
        STA_AX_EXT_JOG = 8,
        STA_AX_EXT_MPG = 9
    }

    /// <summary>馬達類型(決定Function作動方式)</summary>
    /// <remarks></remarks>
    public enum eMotorType
    {
        /// <summary>伺服馬達</summary>
        /// <remarks></remarks>
        ServoMotor = 0,
        /// <summary>步進馬達</summary>
        /// <remarks></remarks>
        SteppingMotor = 1,
        /// <summary>電動缸</summary>
        /// <remarks></remarks>
        ElectricCylinder = 2,
        /// <summary>[預設類型]</summary>
        /// <remarks></remarks>
        None = 3
    }
    /// <summary>座標方向類型</summary>
    /// <remarks></remarks>
    public enum CoordinateType
    {
        /// <summary>對使用者為 第一象限 X軸座標</summary>
        /// <remarks></remarks>
        Coordinate1X = 0,
        /// <summary>對使用者為 第一象限 Y軸座標</summary>
        /// <remarks></remarks>
        Coordinate1Y = 1,
        /// <summary>對使用者為 第一象限 Z軸座標</summary>
        /// <remarks></remarks>
        Coordinate1Z = 2,
        /// <summary>對使用者為 第一象限 對X軸旋轉座標(逆時針為正)</summary>
        /// <remarks></remarks>
        Coordinate1A = 3,
        /// <summary>對使用者為 第一象限 對Y軸旋轉座標(逆時針為正)</summary>
        /// <remarks></remarks>
        Coordinate1B = 4,
        /// <summary>對使用者為 第一象限 對Z軸旋轉座標(逆時針為正)</summary>
        /// <remarks></remarks>
        Coordinate1C = 5
    }


    /// <summary>
    /// 方向
    /// </summary>
    /// <remarks></remarks>
    public enum eDirection
    {
        /// <summary>正向</summary>
        /// <remarks></remarks>
        Positive = 0,
        /// <summary>反向</summary>
        /// <remarks></remarks>
        Negative = 1
    }


    /// <summary>[記錄軸的所有參數] </summary>
    /// <remarks></remarks>
    public struct SMotor
    {
        /// <summary>軸名稱</summary>
        /// <remarks></remarks>
        public string AxisName;
        /// <summary>"顯示"的座標方向,不是實際的</summary>
        /// <remarks></remarks>
        public CoordinateType Coordinate;
        /// <summary>復歸參數</summary>
        /// <remarks></remarks>
        public SHomeParameter HomeParameter;
        /// <summary>極限設定</summary>
        /// <remarks></remarks>
        public SLimit Limit;
        /// <summary>速度設定</summary>
        /// <remarks></remarks>
        public SVelocity Velocity;
        /// <summary>驅動器參數</summary>
        /// <remarks></remarks>
        public SDriverParameter Parameter;
        /// <summary>運動控制卡參數</summary>
        /// <remarks></remarks>
        public sCardParameter CardParameter;
        /// <summary>IO狀態</summary>
        /// <remarks></remarks>
        public IOStatus MotionIOStatus;
        /// <summary>運動狀態</summary>
        /// <remarks></remarks>
        //Status
        public AxisState MotionStatus;

        ///20171013 新增get 軸運動狀態參數
        /// <summary>軸運動狀態</summary>
        /// <remarks></remarks>
        public int AxisMotionStatus;

        /// <summary>到位穩定時間(ms)</summary>
        /// <remarks></remarks>
        public double InpositionStableTime;
        /// <summary>讀取單軸設定檔</summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Load(string strFileName)
        {

            try
            {
                string strSection = null;
                strSection = "HomeParameter";

                Enum.TryParse<enmHomeMode>(CIni.ReadIniString(strSection, "HomeMode", strFileName, "11"), out this.HomeParameter.HomeMode);
                Enum.TryParse<eDirection>(CIni.ReadIniString(strSection, "HomeDirection", strFileName, "1"), out this.HomeParameter.HomeDirection);
                Enum.TryParse<enmHomeExSwitchMode>(CIni.ReadIniString(strSection, "HomeExSwitchMode", strFileName, "2"), out this.HomeParameter.HomeExSwitchMode);
                double.TryParse(CIni.ReadIniString(strSection, "HomeCrossDistance", strFileName, "100"), out this.HomeParameter.HomeCrossDistance);
                double.TryParse(CIni.ReadIniString(strSection, "HomeOffset", strFileName, "0"), out this.HomeParameter.HomeOffset);
                bool.TryParse( CIni.ReadIniString(strSection, "IsHomeDouble", strFileName, "False"), out this.HomeParameter.IsHomeDouble);

                //[Note]:預設為MODE7_AbsSearch  Move (Dir) ->Search ORG ->Stop
                Enum.TryParse<enmHomeMode>(CIni.ReadIniString(strSection, "HomeDoubleMode", strFileName, "6"), out this.HomeParameter.HomeDoubleMode);

                strSection = "Limit";
                decimal.TryParse(CIni.ReadIniString(strSection, "PosivtiveLimit", strFileName, "200"), out this.Limit.PosivtiveLimit);
                decimal.TryParse(CIni.ReadIniString(strSection, "NegativeLimit", strFileName, "-1"), out this.Limit.NegativeLimit);
                Enum.TryParse<enmLimitEnable>(CIni.ReadIniString(strSection, "HLimitEnable", strFileName, Convert.ToInt32(enmLimitEnable.Enabled).ToString()), out this.Limit.HLimitEnable);
                Enum.TryParse<enmLimitLogic>(CIni.ReadIniString(strSection, "HLimitLogic", strFileName, Convert.ToInt32(enmLimitLogic.HighActive).ToString()), out this.Limit.HLimitLogic);
                Enum.TryParse<enmLimitStopMode>(CIni.ReadIniString(strSection, "HLimitStopMode", strFileName, Convert.ToInt32(enmLimitStopMode.MotorImmediatelyStop).ToString()), out this.Limit.HLimitStopMode);

                strSection = "Velocity";
                decimal.TryParse(CIni.ReadIniString(strSection, "HomeVelLow", strFileName, "0.2"), out this.Velocity.HomeVelLow);
                decimal.TryParse(CIni.ReadIniString(strSection, "HomeVelHigh", strFileName, "20"), out this.Velocity.HomeVelHigh);
                decimal.TryParse(CIni.ReadIniString(strSection, "HomeAcc", strFileName, "1000"), out this.Velocity.HomeAcc);
                decimal.TryParse(CIni.ReadIniString(strSection, "HomeDec", strFileName, "1000"), out this.Velocity.HomeDec);

                decimal.TryParse(CIni.ReadIniString(strSection, "VelLow", strFileName, "0"), out this.Velocity.VelLow);
                decimal.TryParse(CIni.ReadIniString(strSection, "VelHigh", strFileName, "100"), out this.Velocity.VelHigh);
                decimal.TryParse(CIni.ReadIniString(strSection, "Acc", strFileName, "9800"), out this.Velocity.Acc);
                decimal.TryParse(CIni.ReadIniString(strSection, "Dec", strFileName, "9800"), out this.Velocity.Dec);

                double.TryParse(CIni.ReadIniString(strSection, "MaxAcc", strFileName, "490000"), out this.Velocity.MaxAcc);
                double.TryParse(CIni.ReadIniString(strSection, "MaxDec", strFileName, "490000"), out this.Velocity.MaxDec);
                double.TryParse(CIni.ReadIniString(strSection, "MaxVel", strFileName, "1200"), out this.Velocity.MaxVel);

                decimal.TryParse(CIni.ReadIniString(strSection, "AccRatio", strFileName, "1"), out this.Velocity.AccRatio);
                decimal.TryParse(CIni.ReadIniString(strSection, "DecRatio", strFileName, "1"), out this.Velocity.DecRatio);


                strSection = "Parameter";
                decimal.TryParse(CIni.ReadIniString(strSection, "PPU", strFileName, "1"), out this.Parameter.PPU);
                decimal.TryParse(CIni.ReadIniString(strSection, "Scale", strFileName, "1000"), out this.Parameter.Scale);
                Enum.TryParse<eDirection>(CIni.ReadIniString(strSection, "Direction", strFileName, "0"), out this.Parameter.Direction);
                Enum.TryParse<enmPulseInLogic>(CIni.ReadIniString(strSection, "PulseInDirection", strFileName, Convert.ToInt32(enmPulseInLogic.NotInverseDirection).ToString()), out this.Parameter.PulseInDirection);
                Enum.TryParse<enmPulseInMode>(CIni.ReadIniString(strSection, "PulseInMode", strFileName, Convert.ToInt32(enmPulseInMode.e4XAB).ToString()), out this.Parameter.PulseInMode);
                Enum.TryParse<enmEncodePulseInFrequency>(CIni.ReadIniString(strSection, "PulseInMaxFreq", strFileName, Convert.ToInt32(enmEncodePulseInFrequency.e1M).ToString()), out this.Parameter.PulseInMaxFreq);
                Enum.TryParse<enmPulseOutMode>(CIni.ReadIniString(strSection, "PulseOutMode", strFileName, Convert.ToInt32(enmPulseOutMode.CW_CCW).ToString()), out this.Parameter.PulseOutMode);
                Enum.TryParse<enmPulseOutReverse>(CIni.ReadIniString(strSection, "PulseOutReverse", strFileName, Convert.ToInt32(enmPulseOutReverse.Revserse_Disable).ToString()), out this.Parameter.PulseOutReverse);
                Enum.TryParse<enmAlarmLogic>(CIni.ReadIniString(strSection, "AlarmLogic", strFileName, Convert.ToInt32(enmAlarmLogic.HighActive).ToString()), out this.Parameter.AlarmLogic);
                Enum.TryParse<enmAlarmStopMode>(CIni.ReadIniString(strSection, "AlarmStopMode", strFileName, Convert.ToInt32(enmAlarmStopMode.MotorDeceleratesStop).ToString()), out this.Parameter.AlarmStopMode);
                Enum.TryParse<enmAlarmEnable>(CIni.ReadIniString(strSection, "AlarmEnable", strFileName, Convert.ToInt32(enmAlarmEnable.Disable).ToString()), out this.Parameter.AlarmEnable);
                Enum.TryParse<enmBacklashEnable>(CIni.ReadIniString(strSection, "BacklashEnable", strFileName, Convert.ToInt32(enmBacklashEnable.Disable).ToString()), out this.Parameter.BacklashEnable);
                Enum.TryParse<enmOrgLogic>(CIni.ReadIniString(strSection, "OrgLogic", strFileName, Convert.ToInt32(enmOrgLogic.LowActive).ToString()), out this.Parameter.OrgLogic);
                Enum.TryParse<enmEZLogic>(CIni.ReadIniString(strSection, "EZLogic", strFileName, Convert.ToInt32(enmEZLogic.LowActive).ToString()), out this.Parameter.EZLogic);
                Enum.TryParse<enmHomeReset>(CIni.ReadIniString(strSection, "HomeReset", strFileName, Convert.ToInt32(enmHomeReset.Enable).ToString()), out this.Parameter.HomeReset);
                Enum.TryParse<enmINPEnable>(CIni.ReadIniString(strSection, "INPEnable", strFileName, Convert.ToInt32(enmINPEnable.Enable).ToString()), out this.Parameter.INPEnable);
                Enum.TryParse<enmINPLogic>(CIni.ReadIniString(strSection, "INPLogic", strFileName, Convert.ToInt32(enmINPLogic.LowActive).ToString()), out this.Parameter.INPLogic);
                Enum.TryParse<enmTriggerStopEnable>(CIni.ReadIniString(strSection, "TriggerStopEnable1", strFileName, Convert.ToInt32(enmTriggerStopEnable.Disabled).ToString()), out this.Parameter.TriggerStopEnable1);
                Enum.TryParse<enmTriggerStopEnable>(CIni.ReadIniString(strSection, "TriggerStopEnable2", strFileName, Convert.ToInt32(enmTriggerStopEnable.Disabled).ToString()), out this.Parameter.TriggerStopEnable2);
                Enum.TryParse<enmTriggerStopEnable>(CIni.ReadIniString(strSection, "TriggerStopEnable3", strFileName, Convert.ToInt32(enmTriggerStopEnable.Disabled).ToString()), out this.Parameter.TriggerStopEnable3);
                Enum.TryParse<enmTriggerStopEnable>(CIni.ReadIniString(strSection, "TriggerStopEnable4", strFileName, Convert.ToInt32(enmTriggerStopEnable.Disabled).ToString()), out this.Parameter.TriggerStopEnable4);
                Enum.TryParse<enmTriggerStopEnable>(CIni.ReadIniString(strSection, "TriggerStopEnable5", strFileName, Convert.ToInt32(enmTriggerStopEnable.Disabled).ToString()), out this.Parameter.TriggerStopEnable5);
                Enum.TryParse<enmTriggerStopLogic>(CIni.ReadIniString(strSection, "TriggerStopLogic1", strFileName, Convert.ToInt32(enmTriggerStopLogic.LowActive).ToString()), out this.Parameter.TriggerStopLogic1);
                Enum.TryParse<enmTriggerStopLogic>(CIni.ReadIniString(strSection, "TriggerStopLogic2", strFileName, Convert.ToInt32(enmTriggerStopLogic.LowActive).ToString()), out this.Parameter.TriggerStopLogic2);
                Enum.TryParse<enmTriggerStopLogic>(CIni.ReadIniString(strSection, "TriggerStopLogic3", strFileName, Convert.ToInt32(enmTriggerStopLogic.LowActive).ToString()), out this.Parameter.TriggerStopLogic3);
                Enum.TryParse<enmTriggerStopLogic>(CIni.ReadIniString(strSection, "TriggerStopLogic4", strFileName, Convert.ToInt32(enmTriggerStopLogic.LowActive).ToString()), out this.Parameter.TriggerStopLogic4);
                Enum.TryParse<enmTriggerStopLogic>(CIni.ReadIniString(strSection, "TriggerStopLogic5", strFileName, Convert.ToInt32(enmTriggerStopLogic.LowActive).ToString()), out this.Parameter.TriggerStopLogic5);
                Enum.TryParse<enmTriggerStopMode>(CIni.ReadIniString(strSection, "TriggerStopMode1", strFileName, Convert.ToInt32(enmTriggerStopMode.Decelerating).ToString()), out this.Parameter.TriggerStopMode1);
                Enum.TryParse<enmTriggerStopMode>(CIni.ReadIniString(strSection, "TriggerStopMode2", strFileName, Convert.ToInt32(enmTriggerStopMode.Decelerating).ToString()), out this.Parameter.TriggerStopMode2);
                Enum.TryParse<enmTriggerStopMode>(CIni.ReadIniString(strSection, "TriggerStopMode3", strFileName, Convert.ToInt32(enmTriggerStopMode.Decelerating).ToString()), out this.Parameter.TriggerStopMode3);
                Enum.TryParse<enmTriggerStopMode>(CIni.ReadIniString(strSection, "TriggerStopMode4", strFileName, Convert.ToInt32(enmTriggerStopMode.Decelerating).ToString()), out this.Parameter.TriggerStopMode4);
                Enum.TryParse<enmTriggerStopMode>(CIni.ReadIniString(strSection, "TriggerStopMode5", strFileName, Convert.ToInt32(enmTriggerStopMode.Decelerating).ToString()), out this.Parameter.TriggerStopMode5);
                Enum.TryParse<enmLatchEnable>(CIni.ReadIniString(strSection, "LatchEnable", strFileName, Convert.ToInt32(enmLatchEnable.Enable).ToString()), out this.Parameter.LatchEnable);
                Enum.TryParse<enmLatchPLogic>(CIni.ReadIniString(strSection, "LatchLogic", strFileName, Convert.ToInt32(enmLatchPLogic.LowActive).ToString()), out this.Parameter.LatchLogic);
                Enum.TryParse<enmErcEnableMode>(CIni.ReadIniString(strSection, "ErcEnable", strFileName, Convert.ToInt32(enmErcEnableMode.Disable).ToString()), out this.Parameter.ErcEnable);
                Enum.TryParse<enmErcLogic>(CIni.ReadIniString(strSection, "ErcLogic", strFileName, Convert.ToInt32(enmErcLogic.HighActive).ToString()), out this.Parameter.ErcLogic);
                Enum.TryParse<enmExternalDrive>(CIni.ReadIniString(strSection, "ExternalDriveAxis", strFileName, Convert.ToInt32(enmExternalDrive.Axis_0).ToString()), out this.Parameter.ExternalDriveAxis);
                Enum.TryParse<enmExternalDriveEnable>(CIni.ReadIniString(strSection, "ExternalDriveEnable", strFileName, Convert.ToInt32(enmExternalDriveEnable.Disabled).ToString()), out this.Parameter.ExternalDriveEnable);
                Enum.TryParse<enmExternalDrivePulseInMode>(CIni.ReadIniString(strSection, "ExternalDrivePulseInMode", strFileName, Convert.ToInt32(enmExternalDrive.Axis_0).ToString()), out this.Parameter.ExternalDrivePulseInMode);

                Enum.TryParse<eDirection>(CIni.ReadIniString(strSection, "ButtonDirection", strFileName, "0"), out this.Parameter.ButtonDirection);
                Enum.TryParse<eMotorType>(CIni.ReadIniString(strSection, "MotorType", strFileName, "0"), out this.Parameter.MotorType);
                bool.TryParse(CIni.ReadIniString(strSection, "IsEncoderExist", strFileName, "True"), out this.Parameter.IsEncoderExist);
                

                double.TryParse(CIni.ReadIniString(strSection, "InpositionStableTime", strFileName, "10"), out this.InpositionStableTime);

                strSection = "CardParameter";

                int.TryParse(CIni.ReadIniString(strSection, "AxisNo", strFileName, "0"), out CardParameter.AxisNo);
                Enum.TryParse<enmMotionCardType>(CIni.ReadIniString(strSection, "CardType", strFileName, "0"), out CardParameter.CardType);
                Enum.TryParse<enmAxisType>(CIni.ReadIniString(strSection, "AxisType", strFileName, "0"), out CardParameter.AxisType);
                int.TryParse(CIni.ReadIniString(strSection, "Ticket", strFileName, "0"), out CardParameter.ItemNo);
                this.AxisName = CIni.ReadIniString(strSection, "AxisName", strFileName, "");
                //Soni 2017.03.22 沒該軸, 不顯示
                Enum.TryParse<CoordinateType>(CIni.ReadIniString(strSection, "Coordinate", strFileName, "0"), out this.Coordinate);
                return true;

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002010), "Error_1002010", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002010) + ex.Message);
                return false;
            }

        }


        /// <summary>儲存單軸設定檔</summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Save(string strFileName)
        {

            try
            {
                string strSection = null;
                //strFileName = Application.StartupPath & "\Motor\XAxis.ini"
                strSection = "HomeParameter";

                CIni.SaveIniString(strSection, "HomeMode", Convert.ToInt32(HomeParameter.HomeMode).ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeDirection", Convert.ToInt32(HomeParameter.HomeDirection).ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeExSwitchMode", Convert.ToInt32(HomeParameter.HomeExSwitchMode).ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeCrossDistance", HomeParameter.HomeCrossDistance.ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeOffset", HomeParameter.HomeOffset.ToString(), strFileName);
                CIni.SaveIniString(strSection, "IsHomeDouble", HomeParameter.IsHomeDouble.ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeDoubleMode", Convert.ToInt32(HomeParameter.HomeDoubleMode).ToString(), strFileName);

                strSection = "Limit";

                CIni.SaveIniString(strSection, "PosivtiveLimit", Limit.PosivtiveLimit.ToString(), strFileName);
                CIni.SaveIniString(strSection, "NegativeLimit", Limit.NegativeLimit.ToString(), strFileName);
                CIni.SaveIniString(strSection, "HLimitEnable", Convert.ToInt32(Limit.HLimitEnable).ToString(), strFileName);
                CIni.SaveIniString(strSection, "HLimitLogic", Convert.ToInt32(Limit.HLimitLogic).ToString(), strFileName);
                CIni.SaveIniString(strSection, "HLimitStopMode", Convert.ToInt32(Limit.HLimitStopMode).ToString(), strFileName);

                strSection = "Velocity";
                ;
                CIni.SaveIniString(strSection, "HomeVelLow", Velocity.HomeVelLow.ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeVelHigh", Velocity.HomeVelHigh.ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeAcc", Velocity.HomeAcc.ToString(), strFileName);
                CIni.SaveIniString(strSection, "HomeDec", Velocity.HomeDec.ToString(), strFileName);
                CIni.SaveIniString(strSection, "VelLow", Velocity.VelLow.ToString(), strFileName);
                CIni.SaveIniString(strSection, "VelHigh", Velocity.VelHigh.ToString(), strFileName);
                CIni.SaveIniString(strSection, "Acc", Velocity.Acc.ToString(), strFileName);
                CIni.SaveIniString(strSection, "Dec", Velocity.Dec.ToString(), strFileName);
                CIni.SaveIniString(strSection, "MaxAcc", Velocity.MaxAcc.ToString(), strFileName);
                CIni.SaveIniString(strSection, "MaxDec", Velocity.MaxDec.ToString(), strFileName);
                CIni.SaveIniString(strSection, "MaxVel", Velocity.MaxVel.ToString(), strFileName);
                CIni.SaveIniString(strSection, "AccRatio", Velocity.AccRatio.ToString(), strFileName);
                CIni.SaveIniString(strSection, "DecRatio", Velocity.DecRatio.ToString(), strFileName);

                strSection = "Parameter";
                CIni.SaveIniString(strSection, "PPU", Parameter.PPU, strFileName);
                CIni.SaveIniString(strSection, "Scale", Parameter.Scale, strFileName);
                CIni.SaveIniString(strSection, "Direction", Convert.ToInt32(Parameter.Direction), strFileName);
                CIni.SaveIniString(strSection, "PulseInDirection", Convert.ToInt32(Parameter.PulseInDirection), strFileName);
                CIni.SaveIniString(strSection, "PulseInMode", Convert.ToInt32(Parameter.PulseInMode), strFileName);
                CIni.SaveIniString(strSection, "PulseInMaxFreq", Convert.ToInt32(Parameter.PulseInMaxFreq), strFileName);
                CIni.SaveIniString(strSection, "PulseOutMode", Convert.ToInt32(Parameter.PulseOutMode), strFileName);
                CIni.SaveIniString(strSection, "PulseOutReverse", Convert.ToInt32(Parameter.PulseOutReverse), strFileName);

                CIni.SaveIniString(strSection, "AlarmLogic", Convert.ToInt32(Parameter.AlarmLogic), strFileName);
                CIni.SaveIniString(strSection, "AlarmStopMode", Convert.ToInt32(Parameter.AlarmStopMode), strFileName);
                CIni.SaveIniString(strSection, "AlarmEnable", Convert.ToInt32(Parameter.AlarmEnable), strFileName);

                CIni.SaveIniString(strSection, "BacklashEnable", Convert.ToInt32(Parameter.BacklashEnable), strFileName);
                CIni.SaveIniString(strSection, "OrgLogic", Convert.ToInt32(Parameter.OrgLogic), strFileName);
                CIni.SaveIniString(strSection, "EZLogic", Convert.ToInt32(Parameter.EZLogic), strFileName);
                CIni.SaveIniString(strSection, "HomeReset", Convert.ToInt32(Parameter.HomeReset), strFileName);
                CIni.SaveIniString(strSection, "INPEnable", Convert.ToInt32(Parameter.INPEnable), strFileName);
                CIni.SaveIniString(strSection, "INPLogic", Convert.ToInt32(Parameter.INPLogic), strFileName);

                CIni.SaveIniString(strSection, "TriggerStopEnable1", Convert.ToInt32(Parameter.TriggerStopEnable1), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopEnable2", Convert.ToInt32(Parameter.TriggerStopEnable2), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopEnable3", Convert.ToInt32(Parameter.TriggerStopEnable3), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopEnable4", Convert.ToInt32(Parameter.TriggerStopEnable4), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopEnable5", Convert.ToInt32(Parameter.TriggerStopEnable5), strFileName);

                CIni.SaveIniString(strSection, "TriggerStopLogic1", Convert.ToInt32(Parameter.TriggerStopLogic1), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopLogic2", Convert.ToInt32(Parameter.TriggerStopLogic2), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopLogic3", Convert.ToInt32(Parameter.TriggerStopLogic3), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopLogic4", Convert.ToInt32(Parameter.TriggerStopLogic4), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopLogic5", Convert.ToInt32(Parameter.TriggerStopLogic5), strFileName);

                CIni.SaveIniString(strSection, "TriggerStopMode1", Convert.ToInt32(Parameter.TriggerStopMode1), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopMode2", Convert.ToInt32(Parameter.TriggerStopMode2), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopMode3", Convert.ToInt32(Parameter.TriggerStopMode3), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopMode4", Convert.ToInt32(Parameter.TriggerStopMode4), strFileName);
                CIni.SaveIniString(strSection, "TriggerStopMode5", Convert.ToInt32(Parameter.TriggerStopMode5), strFileName);

                CIni.SaveIniString(strSection, "LatchEnable", Convert.ToInt32(Parameter.LatchEnable), strFileName);
                CIni.SaveIniString(strSection, "LatchLogic", Convert.ToInt32(Parameter.LatchLogic), strFileName);

                CIni.SaveIniString(strSection, "ErcEnable", Convert.ToInt32(Parameter.ErcEnable), strFileName);
                CIni.SaveIniString(strSection, "ErcLogic", Convert.ToInt32(Parameter.ErcLogic), strFileName);

                CIni.SaveIniString(strSection, "ExternalDriveAxis", Convert.ToInt32(Parameter.ExternalDriveAxis), strFileName);
                CIni.SaveIniString(strSection, "ExternalDriveEnable", Convert.ToInt32(Parameter.ExternalDriveEnable), strFileName);
                CIni.SaveIniString(strSection, "ExternalDrivePulseInMode", Convert.ToInt32(Parameter.ExternalDrivePulseInMode), strFileName);

                CIni.SaveIniString(strSection, "ButtonDirection", Convert.ToInt32(Parameter.ButtonDirection), strFileName);
                CIni.SaveIniString(strSection, "MotorType", Convert.ToInt32(Parameter.MotorType), strFileName);
                CIni.SaveIniString(strSection, "IsEncoderExist", Parameter.IsEncoderExist, strFileName);
                CIni.SaveIniString(strSection, "InpositionStableTime", this.InpositionStableTime, strFileName);

                strSection = "CardParameter";
                CIni.SaveIniString(strSection, "AxisNo", CardParameter.AxisNo, strFileName);
                CIni.SaveIniString(strSection, "CardType", Convert.ToInt32(CardParameter.CardType), strFileName);
                CIni.SaveIniString(strSection, "AxisType", Convert.ToInt32(CardParameter.AxisType), strFileName);
                CIni.SaveIniString(strSection, "Ticket", Convert.ToInt32(CardParameter.ItemNo), strFileName);

                CIni.SaveIniString(strSection, "AxisName", this.AxisName, strFileName);
                CIni.SaveIniString(strSection, "Coordinate", Convert.ToInt32(this.Coordinate), strFileName);


                return true;

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002011), "Error_1002011", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002011) + ex.Message);
                return false;
            }
        }

    }

    /// <summary>[回Home參數] </summary>
    /// <remarks></remarks>
    public struct SHomeParameter
    {
        /// <summary>單步復歸偏移量(不是HomeOffset)</summary>
        /// <remarks></remarks>
        public double HomeCrossDistance;
        /// <summary>原點復歸模式</summary>
        /// <remarks></remarks>
        public enmHomeMode HomeMode;
        public enmHomeExSwitchMode HomeExSwitchMode;
        /// <summary>復歸方向</summary>
        /// <remarks></remarks>
        public eDirection HomeDirection;
        /// <summary>原點復歸偏移量</summary>
        /// <remarks></remarks>
        public double HomeOffset;
        /// <summary>[判斷是否執行二次復歸(此為非正常流程)]</summary>
        /// <remarks></remarks>
        public bool IsHomeDouble;
        /// <summary>[執行二次復歸之原點復歸模式(此為第一次復歸的方式，第二次復歸使用原本的原點復歸方式)]</summary>
        /// <remarks></remarks>
        public enmHomeMode HomeDoubleMode;
    }

    /// <summary>
    /// 回原點模式
    /// </summary>
    /// <remarks></remarks>
    public enum enmHomeMode
    {
        Abs = 0,
        Lmt = 1,
        Ref = 2,
        Abs_Ref = 3,
        Abs_NegRef = 4,
        Lmt_Ref = 5,
        AbsSearch = 6,
        LmtSeaarch = 7,
        AbsSearch_Ref = 8,
        AbsSearch_NegRef = 9,
        LmtSearch_Ref = 10,
        AbsSearchReFind = 11,
        LmtSearchReFind = 12,
        AbsSearchReFind_Ref = 13,
        AbsSearchReFind_NegRe = 14,
        LmtSearchReFind_Ref = 15
    }

    /// <summary>
    /// Setting the stopping condition of Acm_AxHomeEx.
    /// </summary>
    /// <remarks></remarks>
    public enum enmHomeExSwitchMode
    {
        /// <summary>[When sensor is ON(Active)] </summary>
        /// <remarks></remarks>
        LevelOn = 0,
        /// <summary>[When sensor is OFF(Non-active)]</summary>
        /// <remarks></remarks>
        LevelOff = 1,
        /// <summary>[When OFF to ON transition in sensor]</summary>
        /// <remarks></remarks>
        RisingEdge = 2,
        /// <summary>[When ON to OFF transition in sensor]</summary>
        /// <remarks></remarks>
        FallingEdge = 3
    }

    /// <summary>[Limit]</summary>
    /// <remarks></remarks>
    public struct SLimit
    {
        /// <summary>正極限(mm)</summary>
        /// <remarks></remarks>
        public decimal PosivtiveLimit;
        /// <summary>負極限(mm)</summary>
        /// <remarks></remarks>

        public decimal NegativeLimit;
        /// <summary>
        /// 硬體極限致能
        /// </summary>
        /// <remarks></remarks>
        public enmLimitEnable HLimitEnable;
        /// <summary>
        /// 硬體極限邏輯
        /// </summary>
        /// <remarks></remarks>
        public enmLimitLogic HLimitLogic;
        /// <summary>硬體極限停止模式</summary>
        /// <remarks></remarks>
        public enmLimitStopMode HLimitStopMode;
    }

    /// <summary>[Velocity] </summary>
    /// <remarks></remarks>
    public struct SVelocity
    {
        /// <summary>[回Home初速度]</summary>
        /// <remarks></remarks>
        public decimal HomeVelLow;
        /// <summary>[回Home最大速]</summary>
        /// <remarks></remarks>
        public decimal HomeVelHigh;
        /// <summary>[回Home加速度] </summary>
        /// <remarks></remarks>
        public decimal HomeAcc;
        /// <summary>[回Home減速度] </summary>
        /// <remarks></remarks>
        public decimal HomeDec;
        /// <summary>[初速度]</summary>
        /// <remarks></remarks>
        public decimal VelLow;
        /// <summary>[最大速]</summary>
        /// <remarks></remarks>
        public decimal VelHigh;
        /// <summary>[加速度] </summary>
        /// <remarks></remarks>
        public decimal Acc;
        /// <summary>[減速度]</summary>
        /// <remarks></remarks>
        public decimal Dec;
        /// <summary>[速度上限值]</summary>
        /// <remarks></remarks>
        public double MaxVel;
        /// <summary>[加速度上限值]</summary>
        /// <remarks></remarks>
        public double MaxAcc;
        /// <summary>[減速度上限值]</summary>
        /// <remarks></remarks>
        public double MaxDec;
        /// <summary>[加速比例設定(0~1)]</summary>
        /// <remarks></remarks>
        public decimal AccRatio;
        /// <summary>[減速比例設定(0~1)]</summary>
        /// <remarks></remarks>
        public decimal DecRatio;
    }

    /// <summary>驅動器參數</summary>
    /// <remarks></remarks>
    public struct SDriverParameter
    {
        /// <summary>轉換比例</summary>
        /// <remarks></remarks>
        public decimal Scale;
        /// <summary>單位脈衝</summary>
        /// <remarks></remarks>
        public decimal PPU;
        /// <summary>運動方向</summary>
        /// <remarks></remarks>
        //TODO: Direction要移除
        public eDirection Direction;

        #region "PulseIn"
        /// <summary>脈衝輸入(Encoder)方向</summary>
        /// <remarks></remarks>
        public enmPulseInLogic PulseInDirection;
        /// <summary>脈衝輸入方式</summary>
        /// <remarks></remarks>
        public enmPulseInMode PulseInMode;
        /// <summary>編碼器最大輸入頻率</summary>
        /// <remarks></remarks>
        #endregion
        public enmEncodePulseInFrequency PulseInMaxFreq;
        /// <summary>輸出方向反轉(碰極限不反轉者)</summary>
        /// <remarks></remarks>
        public enmPulseOutReverse PulseOutReverse;
        /// <summary>脈衝輸出模式</summary>
        /// <remarks></remarks>
        #region "Alarm"
        public enmPulseOutMode PulseOutMode;
        /// <summary>異警致能</summary>
        /// <remarks></remarks>
        public enmAlarmEnable AlarmEnable;
        /// <summary>異警邏輯</summary>
        /// <remarks></remarks>
        public enmAlarmLogic AlarmLogic;
        /// <summary>異警停止模式</summary>
        /// <remarks></remarks>
        #endregion
        public enmAlarmStopMode AlarmStopMode;
        /// <summary>背隙修正</summary>
        /// <remarks></remarks>
        public enmBacklashEnable BacklashEnable;
        /// <summary>原點邏輯</summary>
        /// <remarks></remarks>
        public enmOrgLogic OrgLogic;
        /// <summary>
        /// Z相邏輯
        /// </summary>
        /// <remarks></remarks>
        public enmEZLogic EZLogic;
        /// <summary>復歸重置</summary>
        /// <remarks></remarks>
        public enmHomeReset HomeReset;
        /// <summary>到位訊號致能</summary>
        /// <remarks></remarks>
        public enmINPEnable INPEnable;
        /// <summary>
        /// 到位訊號邏輯
        /// </summary>
        /// <remarks></remarks>
        public enmINPLogic INPLogic;
        /// <summary>
        /// 栓鎖致能
        /// </summary>
        /// <remarks></remarks>
        public enmLatchEnable LatchEnable;
        /// <summary>
        /// 栓鎖邏輯
        /// </summary>
        /// <remarks></remarks>

        public enmLatchPLogic LatchLogic;
        public enmTriggerStopEnable TriggerStopEnable1;
        public enmTriggerStopEnable TriggerStopEnable2;
        public enmTriggerStopEnable TriggerStopEnable3;
        public enmTriggerStopEnable TriggerStopEnable4;
        public enmTriggerStopEnable TriggerStopEnable5;
        public enmTriggerStopMode TriggerStopMode1;
        public enmTriggerStopMode TriggerStopMode2;
        public enmTriggerStopMode TriggerStopMode3;
        public enmTriggerStopMode TriggerStopMode4;
        public enmTriggerStopMode TriggerStopMode5;
        public enmTriggerStopLogic TriggerStopLogic1;
        public enmTriggerStopLogic TriggerStopLogic2;
        public enmTriggerStopLogic TriggerStopLogic3;
        public enmTriggerStopLogic TriggerStopLogic4;

        public enmTriggerStopLogic TriggerStopLogic5;
        public enmErcEnableMode ErcEnable;

        public enmErcLogic ErcLogic;
        public enmExternalDrive ExternalDriveAxis;
        public enmExternalDriveEnable ExternalDriveEnable;

        public enmExternalDrivePulseInMode ExternalDrivePulseInMode;
        /// <summary>編碼器是否存在</summary>
        /// <remarks>如果不存在,則GetPositionValue讀取命令位置</remarks>
        public bool IsEncoderExist;
        /// <summary>馬達類型</summary>
        /// <remarks></remarks>
        public eMotorType MotorType;
        /// <summary>介面按鍵方向</summary>
        /// <remarks></remarks>
        public eDirection ButtonDirection;
    }

    /// <summary>軸對應卡片參數</summary>
    /// <remarks></remarks>
    public struct sCardParameter
    {
        /// <summary>卡片型號</summary>
        /// <remarks></remarks>
        public enmMotionCardType CardType;
        /// <summary>對Motion註冊票券(卡號)</summary>
        /// <remarks></remarks>

        public int ItemNo;
        /// <summary>軸型號</summary>
        /// <remarks></remarks>
        public enmAxisType AxisType;
        /// <summary>卡片軸號</summary>
        /// <remarks></remarks>
        public int AxisNo;
        /// <summary>所屬群號</summary>
        /// <remarks></remarks>
        public int GroupNo;
    }

    public enum enmAxisType
    {
        /// <summary>不適用,未定義</summary>
        /// <remarks></remarks>
        Undefined = 0,
        /// <summary>RS-485 東方CRK系列</summary>
        /// <remarks></remarks>
        HM60163C = 1,
        /// <summary>東方RKII系列</summary>
        /// <remarks></remarks>
        RK2 = 2,
        /// <summary>士林電機 微電腦溫度控制器系列</summary>
        /// <remarks></remarks>
        WT404 = 3
    }

    /// <summary>運動控制卡型號</summary>
    /// <remarks></remarks>
    public enum enmMotionCardType
    {
        /// <summary>虛擬</summary>
        /// <remarks></remarks>
        None = 0,
        /// <summary>Advantech PCI-1245</summary>
        /// <remarks></remarks>
        PCI_1245 = 1,
        /// <summary>Advantech PCI-1285</summary>
        /// <remarks></remarks>
        PCI_1285 = 2,
        /// <summary>ModBus</summary>
        /// <remarks></remarks>
        ModBus = 3
    }

    /// <summary>[移動速度之快中慢]</summary>
    /// <remarks></remarks>
    public enum SpeedType
    {
        /// <summary>慢速</summary>
        /// <remarks></remarks>
        Slow = 0,
        /// <summary>中速</summary>
        /// <remarks></remarks>
        Medium = 1,
        /// <summary>快速</summary>
        /// <remarks></remarks>
        Fast = 2
    }

    /// <summary>命令狀態</summary>
    /// <remarks></remarks>
    public enum CommandStatus
    {
        /// <summary>命令異常</summary>
        /// <remarks></remarks>
        Alarm = 0,
        /// <summary>命令成功</summary>
        /// <remarks></remarks>
        Sucessed = 1,
        /// <summary>警告</summary>
        /// <remarks></remarks>
        Warning = 2,
        /// <summary>發送中</summary>
        /// <remarks></remarks>
        Sending = 3
    }

    /// <summary>
    /// Setting the signal source of Latch
    /// </summary>
    /// <remarks></remarks>
    public enum LTCSource
    {
        DI = 0,
        EZ = 1,
        ORG = 2,
        MEL = 3
    }

    /// <summary>
    /// Setting of active logic for LTC signal
    /// </summary>
    /// <remarks></remarks>
    public enum LTCLogic
    {
        RisingEdge = 0,
        FallingEdge = 1
    }

    /// <summary>
    /// INP function enable/disable
    /// </summary>
    /// <remarks></remarks>
    public enum INPEnable
    {
        Disable = 0,
        Enable = 1
    }

    /// <summary>
    /// Setting of active logic for INP signal
    /// </summary>
    /// <remarks></remarks>
    public enum INPLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmONOFF
    {
        eOff = 0,
        eON = 1
    }

    /// <summary>
    /// encode max pulse in frequency
    /// </summary>
    /// <remarks></remarks>
    public enum enmEncodePulseInFrequency
    {
        e500K = 0,
        e1M = 1,
        e2M = 2,
        e4M = 3
    }

    public enum enmAlarmEnable
    {
        Disable = 0,
        Enable = 1
    }

    public enum enmAlarmLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmAlarmStopMode
    {
        MotorImmediatelyStop = 0,
        MotorDeceleratesStop = 1
    }

    public enum enmBacklashEnable
    {
        Disable = 0,
        Enable = 1
    }

    public enum enmErcEnableMode
    {
        Disable = 0,
        ErcOutputWhenHomeFinish = 1
    }

    public enum enmErcLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmExternalDrive
    {
        //[說明]:In PCI-1265/PCI-1245/PCI-1245V/PCI-1245E, only support 0
        Axis_0 = 0,
        Axis_1 = 1,
        Axis_2 = 2,
        Axis_3 = 3
    }

    public enum enmExternalDriveEnable
    {
        //[說明]:In PCI-1265/PCI-1245/PCI-1245V/PCI-1245E, only support 0
        Disabled = 0,
        Enabled = 1
    }

    public enum enmExternalDrivePulseInMode
    {
        e1XAB = 0,
        e2XAB = 1,
        e4XAB = 2,
        eCWCCW = 3
    }

    public enum enmINPEnable
    {
        Disabled = 0,
        Enable = 1
    }

    public enum enmINPLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmTriggerStopEnable
    {
        Disabled = 0,
        Enable = 1
    }

    public enum enmTriggerStopMode
    {
        SuddenStop = 0,
        Decelerating = 1
    }

    public enum enmTriggerStopLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmEmgLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmOrgLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmLimitStopMode
    {
        MotorImmediatelyStop = 0,
        MotorDeceleratesStop = 1
    }

    public enum enmLimitLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmLimitEnable
    {
        Disabled = 0,
        Enabled = 1
    }

    public enum enmEZLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmPositionType
    {
        CommandPosition = 0,
        ActualPosition = 1
    }

    public enum enmLatchEnable
    {
        Disabled = 0,
        Enable = 1
    }

    public enum enmLatchPLogic
    {
        LowActive = 0,
        HighActive = 1
    }

    public enum enmCompareSource
    {
        CommandPosition = 0,
        ActualPosition = 1
    }

    public enum enmCardIOONOFF
    {
        eOFF = 0,
        eON = 1
    }

    /// <summary>
    /// In PCI-1245/1245V/1245E/1265, the default value is 0.
    /// </summary>
    /// <remarks></remarks>
    public enum enmCompareMethod
    {
        /// <summary>[大於 Position Counter]</summary>
        /// <remarks></remarks>
        More = 0,
        /// <summary>[小於 Position Counter]</summary>
        /// <remarks></remarks>
        Small = 1,
        /// <summary>[等於 Counter (Directionless)(Not support)] </summary>
        /// <remarks></remarks>
        Equal = 2
    }

    public enum enmCompareEnable
    {
        Disabled = 0,
        Enable = 1
    }

    /// <summary>
    /// 脈衝輸入邏輯
    /// </summary>
    /// <remarks></remarks>
    public enum enmPulseInLogic
    {
        /// <summary>
        /// 非反向
        /// </summary>
        /// <remarks></remarks>
        NotInverseDirection = 0,
        /// <summary>
        /// 反向
        /// </summary>
        /// <remarks></remarks>
        InverseDirection = 1
    }
    public enum enmPulseOutReverse
    {
        Revserse_Disable = 0,
        Reverse_Enable = 1
    }

    public enum enmPulseOutMode
    {
        OUT_DIR = 1,
        OUT_DIR_OUT_NegativeLogic = 2,
        OUT_DIR_DIR_NegativeLogic = 4,
        OUT_DIR_OUTDIR_NegativeLogic = 8,
        CW_CCW = 16,
        CW_CCV_CWCCW_NegativeLogic = 32,
        AB_Phase = 64,
        BA_Phase = 128,
        CW_CCV_OUT_NegativeLogic = 256,
        CW_CCV_DIR_NegativeLogic = 512
    }

    public enum enmPulseInMode
    {
        e1XAB = 0,
        e2XAB = 1,
        e4XAB = 2,
        eCWCCW = 3
    }


    public enum enmHomeReset
    {
        Disabled = 0,
        Enable = 1
    }

    public enum enmSFEnable
    {
        Disabled = 0,
        Enable = 1
    }

    public enum eCurveMode
    {
        /// <summary>[T Curve 移動]</summary>
        /// <remarks></remarks>
        TCurve = 0,
        /// <summary>[S Curve 移動]</summary>
        /// <remarks></remarks>
        SCurve = 1
    }

    public enum ePlane
    {
        /// <summary>XY平面</summary>
        /// <remarks></remarks>
        XY = 0,
        /// <summary>YZ平面</summary>
        /// <remarks></remarks>
        YZ = 1,
        /// <summary>XZ平面</summary>
        /// <remarks></remarks>
        XZ = 2
    }

    /// <summary>多軸同動運行模式</summary>
    /// <remarks></remarks>
    public enum eRunMode
    {
        /// <summary>当MoveMode为非交接模式。在这种模式中，每个路径都包括加速和减速的整个过程。这种不支持速度前瞻功能，因此应禁用CFG_GpSFEnable。</summary>
        /// <remarks></remarks>
        BufferMode = 0,
        /// <summary>当MoveMode为交接模式，且 CFG_GpBldTime 的值大于 0。这种不支持 S 型速度曲线。当通过 CFG_SFEnable 启用速度前瞻功能时，由于路径运动中使用的所有速度参数为群组速度设置，因此无需使用参数FL 和 FH，所有路径支持相同的驱动速度。比如，CFG_SFEnable = Disable，BlendingTime>0。</summary>
        /// <remarks></remarks>
        BlendingMode = 1,
        /// <summary>当 MoveMode 为交接模式，且 CFG_GpBldTime 的值为 0。当通过 CFG_SFEnable 启用速度前瞻功能时，由于路径运动中使用的所有速度参数为群组速度设置，因此无需使用参数 FL 和 FH，所有路径支持相同的驱动速度。</summary>
        /// <remarks></remarks>
        FlyMode = 2
    }


    /// <summary>軸卡介面定義</summary>
    /// <remarks></remarks>
    public interface IMotionCard
    {

        /// <summary>軸數</summary>
        /// <remarks></remarks>

        int AxisCount { get; set; }
        uint AxisNum { get; set; }
        /// <summary>命令發送狀態</summary>
        /// <remarks></remarks>

        CommandStatus cmdStatus { get; set; }

        //Sub SetAxisParam(ByVal index As Integer, ByRef param As SMotor)
        //Function GetAxisParam(ByVal index As Integer) As SMotor
        /// <summary>
        /// 馬達回原點
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus Home(int axisNo, uint homeMode, uint homeDirection);

        CommandStatus SetHomeOffset(int axisNo, decimal homeOffset);
        CommandStatus SetHeaterSV(int axisNo, int valve);
        string ReadHeaterPV(int axisNo);

        CommandStatus SlowStop(int axisNo, decimal dec);

        /// <summary>
        /// 取得Courent Position數值(即Freedback數值)
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        string GetPositionValue(int axisNo);

        /// <summary>
        /// 取得Courent Command數值
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetCommandValue(int axisNo, ref decimal pos);
        /// <summary>
        /// 設定Compare
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetCompare(int axisNo, enmCompareEnable Enable, enmCompareSource Type, enmCompareMethod Method);


        /// <summary>
        /// Set compare data for the specified axis
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetCampareData(int axisNo, decimal startPos, decimal endPos, int interval);

        /// <summary>
        /// Get current compare data in the comparator.
        /// </summary>
        /// <param name="Pos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetCompareValue(int axisNo, ref decimal Pos);

        /// <summary>
        /// Wait Table Stop
        /// </summary>
        /// <param name="TimeOut">TimeOut上限</param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus WaitTableStop(int axisNo, decimal TimeOut = 1000);

        /// <summary>
        /// 設定馬達狀態 IO
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus IOSet(int axisNo);

        /// <summary>
        /// 設定馬達Backlash
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetBacklash(int axisNo, enmBacklashEnable Enable);

        /// <summary>
        /// 設定External Drive
        /// </summary>
        /// <param name="Drive"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetExternalDrive(int axisNo, enmExternalDrive Drive, enmExternalDriveEnable Enable, enmExternalDrivePulseInMode Mode);
        /// <summary>
        /// 設定馬達輸出反轉
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseOutReverse"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetPulseOutReverse(int axisNo, enmPulseOutReverse pulseOutReverse);
        /// <summary>
        /// 設定馬達Pulse Out Mode
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetPulseOutMode(int axisNo, enmPulseOutMode Mode);

        /// <summary>
        /// 設定馬達EMG邏輯訊號 Hi/Lo
        /// </summary>
        /// <param name="Logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetEMG(enmEmgLogic Logic);



        double[] EndArray { get; set; }

        double[] CenArray { get; set; }
        /// <summary>兩軸直線插補運動</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpMoveLinearAbsXYZ(ref CSyncParameter SyncParameter);
        /// <summary>[群組移動]</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpMovePath(ref CSyncParameter SyncParameter);

        /// <summary>[加入點的路徑]</summary>
        /// <param name="SyncParameter">連續同動參數</param>
        /// <param name="IsEndPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpAddDotPath(ref CSyncParameter SyncParameter, bool IsEndPath = false);

        /// <summary>[加入圓弧之路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="IsEndPath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpAddArcPath(ref CSyncParameter SyncParameter, bool IsEndPath = false);

        /// <summary>[加入等待時間之路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpAddDwell(ref CSyncParameter SyncParameter);

        /// <summary>
        /// 暫停移動Path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpPauseMovePath(ref CSyncParameter SyncParameter);


        /// <summary>
        /// 清除移動Path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpClearMovePath(ref CSyncParameter SyncParameter);


        /// <summary>
        /// 群組add Axis模式
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpAddAxis(ref CSyncParameter SyncParameter, List<int> AxisNoList);


        /// <summary>
        /// Speed Forward Function
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpSetSpeedForward(ref CSyncParameter SyncParameter, enmSFEnable Enable);


        /// <summary>
        /// Blengding Time
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpSetBlendingTime(ref CSyncParameter SyncParameter, int Time);

        //jimmy 20170823
        /// <summary>
        /// 啟用軸卡軟體極限(負)
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="MelEnable"></param>
        /// <returns></returns>
        CommandStatus GpSetSwMelEnable(int axisNo, bool MelEnable);
        /// <summary>
        /// 啟用軸卡軟體極限(正)
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="PelEnable"></param>
        /// <returns></returns>
        CommandStatus GpSetSwPelEnable(int axisNo, bool PelEnable);

        //===20170821================================================================================================================
        /// <summary>
        /// 設定負方向軸卡極限
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="negativeLimit"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetSNEL(int axisNo, decimal negativeLimit);
        /// <summary>
        /// 設定正方向軸卡極限
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="positiveLimit"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetSPEL(int axisNo, decimal positiveLimit);
        /// <summary>
        /// 設定正方向軟件限位的反應模式
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="PelReact"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetSPELReact(int axisNo, bool PelReact);
        /// <summary>
        /// 設定負方向軸卡限位的反應模式
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="MelReact"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetSNELReact(int axisNo, bool MelReact);
        /// <summary>
        /// 設定同動初速度
        /// </summary>
        /// <param name="VelLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpSetVelLow(ref CSyncParameter SyncParameter, decimal VelLow);


        /// <summary>
        /// 設定最大速度
        /// </summary>
        /// <param name="VelHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpSetVelHigh(ref CSyncParameter SyncParameter, decimal VelHigh);
        /// <summary>
        /// 設定移動加速度Move Acc
        /// </summary>
        /// <param name="Acc"></param>
        /// <remarks></remarks>
        CommandStatus GpSetAcc(ref CSyncParameter SyncParameter, decimal Acc);
        /// <summary>
        /// 設定移動減速度
        /// </summary>
        /// <param name="Dec"></param>
        /// <remarks></remarks>
        CommandStatus GpSetDec(ref CSyncParameter SyncParameter, decimal Dec);
        /// <summary>[設定最大移動加速度]</summary>
        /// <param name="axis"></param>
        /// <param name="MaxAcc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetMaxAcc(int axis, decimal maxAcc);
        /// <summary>[設定最大移動減速度]</summary>
        /// <param name="axis"></param>
        /// <param name="MaxDec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetMaxDec(int axis, decimal maxDec);
        /// <summary>[設定最大移動速度]</summary>
        /// <param name="axis"></param>
        /// <param name="maxVel"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetMaxVel(int axis, decimal maxVel);
        /// <summary>設定參考平面</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        object GpSetRefPlane(ref CSyncParameter SyncParameter, ePlane plane);
        /// <summary>[設定運行模式]</summary>
        /// <param name="syncParameter"></param>
        /// <param name="runMode"></param>
        /// <param name="blendingTime"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GPSetRunMode(ref CSyncParameter syncParameter, eRunMode runMode, int blendingTime = 1024);
        /// <summary>[完成路徑移動]</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpMoveDone(ref CSyncParameter SyncParameter);
        /// <summary>[取得目前路徑執行之狀態]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="remainCount"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpGetPathStatus(ref CSyncParameter SyncParameter, ref long remainCount);
        /// <summary>[清空路徑]</summary>
        /// <param name="SyncParameter"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpResetPath(ref CSyncParameter SyncParameter);
        /// <summary>[設定 T or S Curve]</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="curveMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GpSetCurve(ref CSyncParameter SyncParameter, eCurveMode curveMode);

        /// <summary>取得詳細錯誤訊息</summary>
        /// <param name="axisNo">軸號</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string GetErrorMessage(int axisNo);
        CommandStatus Initial(SMotionConnectParameter item);

        /// <summary>[設定T、S Curve]</summary>
        /// <param name="axisNo"></param>
        /// <param name="curveMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetCurve(int axisNo, eCurveMode curveMode);

        CommandStatus Close();
        /// <summary>設定轉換比例(pulse/mm)</summary>
        /// <param name="axis"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetScale(int axis, decimal scale);
        /// <summary>每單位Pulse數,虛擬單位</summary>
        /// <param name="axis"></param>
        /// <param name="ppu"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetPPU(int axis, decimal ppu);
        /// <summary>設定最大速度(mm/s)</summary>
        /// <param name="velHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetVelHigh(int axis, decimal velHigh);
        /// <summary>設定初速度(mm/s)</summary>
        /// <param name="velHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetVelLow(int axis, decimal velHigh);
        /// <summary>設定加速度(mm/s^2)</summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetAcc(int axis, decimal acc);
        /// <summary>設定減速度(mm/s^2)</summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetDec(int axis, decimal dec);
        /// <summary>相對移動</summary>
        /// <param name="Dist"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus RelMove(int axis, decimal Dist);
        /// <summary>絕對移動</summary>
        /// <param name="absPos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus AbsMove(int axis, decimal absPos);
        /// <summary>等速移動</summary>
        /// <param name="axis"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus VelMove(int axis, eDirection dir);
        /// <summary>移動完成</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus MoveFinish(int axis);

        /// <summary>取得運動接點狀態</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus CheckMotorStatus(int axis, ref IOStatus IOStatus);
        /// <summary>取得軸狀態</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetAxisState(int axis, ref uint status);

        ///20171013 新增get 軸狀態
        /// <summary>取得軸運動狀態</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetMotionState(int axis, ref int status);

        /// <summary>軸異常重置</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus AxisResetError(int axis);
        /// <summary>
        /// 設定快速回Home之速度
        /// </summary>
        /// <param name="VelHigh">速度</param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeVelHigh(int axisNo, decimal velHigh);
        /// <summary>設定慢速回Home速度</summary>
        /// <param name="axis"></param>
        /// <param name="velLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeVelLow(int axis, decimal velLow);
        /// <summary>設定回Home加速度</summary>
        /// <param name="axis"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeAcc(int axis, decimal acc);
        /// <summary>設定回Home減速度</summary>
        /// <param name="axis"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeDec(int axis, decimal dec);

        /// <summary>復歸完成</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus HomeFinish(int axis);
        /// <summary>設定當前位置座標</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetPosition(int axis, decimal pos);
        /// <summary>馬達激磁</summary>
        /// <param name="axis"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus Servo(int axis, enmONOFF state);
        /// <summary></summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus EmgStop(int axis);

        /// <summary>取得目前速度</summary>
        /// <param name="axis"></param>
        /// <param name="velocity"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetCurrentVel(int axis, ref decimal velocity);

        /// <summary>Error Clear</summary>
        /// <param name="axis"></param>
        /// <param name="enableMode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetERC(int axis, enmErcEnableMode enableMode, enmErcLogic logic);
        /// <summary>設定硬體極限訊號</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHEL(int axis, enmLimitEnable enable, enmLimitLogic logic, enmLimitStopMode stopMode);
        /// <summary>設定ORG訊號</summary>
        /// <param name="axis"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetORG(int axis, enmOrgLogic logic);
        /// <summary>設定Encoder Z相邏輯訊號</summary>
        /// <param name="axis"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetEZ(int axis, enmEZLogic logic);
        /// <summary>復歸後命令,位置歸零</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeReset(int axis, enmHomeReset enable);
        /// <summary>特殊復歸方式,停止條件</summary>
        /// <param name="axis"></param>
        /// <param name="homeExSwitchMode">HomeEx停止條件</param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeExSwitchMode(int axis, enmHomeExSwitchMode homeExSwitchMode);
        /// <summary>在Sensor範圍內時,要往外移動多遠再找Sensor</summary>
        /// <param name="axis"></param>
        /// <param name="homeCrossDistance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetHomeCrossDistance(int axis, decimal homeCrossDistance);
        /// <summary>設定警告邏輯</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetALM(int axis, enmAlarmEnable enable, enmAlarmLogic logic, enmAlarmStopMode stopMode);
        /// <summary>到位致能/判定邏輯</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetINP(int axis, enmINPEnable enable, enmINPLogic logic);
        /// <summary>緊停邏輯</summary>
        /// <param name="axis"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetEMG(int axis, enmEmgLogic logic);
        /// <summary>觸發停止功能1</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SETIN1Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic);
        /// <summary>觸發停止功能2</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SETIN2Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic);
        /// <summary>觸發停止功能4</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SETIN4Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic);
        /// <summary>觸發停止功能5</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SETIN5Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic);

        /// <summary>編碼器輸入方式</summary>
        /// <param name="axis"></param>
        /// <param name="mode"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetPulseIn(int axis, enmPulseInMode mode, enmPulseInLogic logic);
        /// <summary>編碼器最大輸入頻率</summary>
        /// <param name="axis"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetMaxPulseFrequency(int axis, enmEncodePulseInFrequency frequency);

        /// <summary>設定Latch</summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetLatch(int axis, enmLatchEnable enable, enmLatchPLogic logic);
        /// <summary>取得Latch位置</summary>
        /// <param name="axis"></param>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetLatchPosition(int axis, enmPositionType type, ref decimal pos);
        /// <summary>取得Latch旗標</summary>
        /// <param name="axis"></param>
        /// <param name="latch"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus GetLatchFlag(int axis, ref bool latch);
        /// <summary>清除Latch位置資料與旗標</summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus ResetLatch(int axis);
        /// <summary>雙軸同動(gantry)</summary>
        /// <param name="masterAxis">主動軸索引</param>
        /// <param name="slaveAxis">從動軸索引</param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus SetGantry(int masterAxis, int slaveAxis);

        /// <summary>軸卡接點輸出</summary>
        /// <param name="AxisNo"></param>
        /// <param name="DOChannel"></param>
        /// <param name="OnOFF"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        CommandStatus DOOutput(int AxisNo, ushort DOChannel, enmCardIOONOFF OnOFF = enmCardIOONOFF.eOFF);

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
        CommandStatus Dev2DCompensateTable(int axisNo, int axisNo2, decimal originPosX, decimal originPosY, decimal pitchX, decimal pitchY, ref decimal[,] offsetX, ref decimal[,] offsetY);

        CommandStatus Dev2DCompensateTableEnable(bool Enable);
        CommandStatus GetCompensatePosition(int axisNo, ref double OffsetValue);

    }



}

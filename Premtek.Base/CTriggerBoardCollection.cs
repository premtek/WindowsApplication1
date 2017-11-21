using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading.Tasks;
using ProjectCore;
using Premtek.Base;

namespace Premtek.Base
{

	#region "全域"
	/// <summary>[觸發版類型]</summary>
	/// <remarks></remarks>
	public enum eTriggerBoardType
	{
		/// <summary>虛擬</summary>
		/// <remarks></remarks>
		None = 0,
		/// <summary>Trigger3.0</summary>
		/// <remarks></remarks>
		Trigger30 = 1
	}

	#endregion

	#region "連線參數"

	/// <summary>[使用Trigger Board 3.0的序列埠]</summary>
	/// <remarks></remarks>
	public struct sTriggerBoard30Connect
	{
		/// <summary>[通訊連接埠號]</summary>
		/// <remarks></remarks>
		public string PortName;
		/// <summary>[通訊序列傳輸速率]</summary>
		/// <remarks></remarks>
		public string BaudRate;
		/// <summary>通訊逾時之時間(ms)</summary>
		/// <remarks></remarks>
		public decimal TimeOutTimes;
	}

	/// <summary>[觸發版連線參數]</summary>
	/// <remarks></remarks>
	public struct sTriggerBoardConnectionParameter
	{
		/// <summary>[硬體型號(類型)]</summary>
		/// <remarks></remarks>
		public eTriggerBoardType CardType;
		/// <summary>[Trigger Board 3.0的通訊設定]</summary>
		/// <remarks></remarks>
		public sTriggerBoard30Connect TriggerBoard30;
		/// <summary>[硬體型號之名稱(字串表示)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string CardTypeString {
			get {
				switch (CardType) {
					case eTriggerBoardType.None:
						return "None";
					case eTriggerBoardType.Trigger30:
						return "Trigger 3.0";
					default:
						return "Unknown";
				}
			}
		}
	}
}
namespace Premtek.Base
{

	#endregion

	#region "接點參數"
	/// <summary>[觸發版接點參數]</summary>
	/// <remarks></remarks>
	public struct sTriggerBoardParameter
	{
		/// <summary>[點位名稱]</summary>
		/// <remarks></remarks>
		public string Name;
		/// <summary>[硬體型號(類型)]</summary>
		/// <remarks></remarks>
		public eTriggerBoardType CardType;
		/// <summary>[位置(Ethernet)]</summary>
		/// <remarks></remarks>
		public long IP;
		/// <summary>[埠號(Ethernet)]</summary>
		/// <remarks></remarks>
		public long Port;
		/// <summary>[埠號(RS232)]</summary>
		/// <remarks></remarks>
		public string PortName;
		/// <summary>[通訊傳輸速率(RS232)]</summary>
		/// <remarks></remarks>
		public string BaudRate;
		/// <summary>[通訊逾時之時間(ms)]</summary>
		/// <remarks></remarks>
		public decimal TimerOutTimes;
		/// <summary>[Item索引]</summary>
		/// <remarks></remarks>
		public int ItemNo;
	}


	#endregion

	/// <summary>[觸發版物件類別]</summary>
	/// <remarks></remarks>
	public class CTriggerBoardCollection
	{

		/// <summary>[卡片初始化狀態]</summary>
		/// <remarks></remarks>
		public bool IsCardIntialOK {
			get { return mIsCardIntialOK; }
		}
		/// <summary>[卡片初始化狀態]</summary>
		/// <remarks></remarks>

		private bool mIsCardIntialOK;
		/// <summary>[是否走Simulation模式]</summary>
		/// <value></value>
		/// <remarks></remarks>
		public bool IsSimulationType {
			set { mIsSimulationType = value; }
		}
		/// <summary>[是否走Simulation模式]</summary>
		/// <remarks></remarks>

		private bool mIsSimulationType;
		/// <summary>[連線設定參數]</summary>
		/// <remarks></remarks>

		public sTriggerBoardConnectionParameter[] TBConnectionParameter = new sTriggerBoardConnectionParameter[Convert.ToInt16(MCommonDefine.enmTriggerBoard.Max + 1)];
		/// <summary>[接點參數設定]</summary>
		/// <remarks></remarks>

		public sTriggerBoardParameter[] TBParameter = new sTriggerBoardParameter[Convert.ToInt16(MCommonDefine.enmTriggerBoard.Max + 1)];
		/// <summary>[SaveConnectionParameter]</summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Save(string fileName)
		{
			string mSection = null;
			int mI = 0;
			for (mI = 0; mI <= this.TBConnectionParameter.Count() - 1; mI++) {
				mSection = "TriggerBoardNo" + (mI + 1).ToString();
                MReadSaveIni.SaveIniString(mSection, "CardType", Convert.ToInt32(this.TBConnectionParameter[mI].CardType).ToString(), fileName);
                switch (this.TBConnectionParameter[mI].CardType)
                {
					case eTriggerBoardType.None:

						break;
					case eTriggerBoardType.Trigger30:
                        MReadSaveIni.SaveIniString(mSection, "TriggerBoard30-PortName", this.TBConnectionParameter[mI].TriggerBoard30.PortName, fileName);
                        MReadSaveIni.SaveIniString(mSection, "TriggerBoard30-BaudRate", this.TBConnectionParameter[mI].TriggerBoard30.BaudRate, fileName);
                        MReadSaveIni.SaveIniString(mSection, "TriggerBoard30-TimeOutTimes", this.TBConnectionParameter[mI].TriggerBoard30.TimeOutTimes.ToString(), fileName);

						break;
				}
			}
			return true;
		}

		/// <summary>[LoadTriggerConnection]</summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Load(string fileName)
		{
			string mSection = null;
			int mI = 0;
			for (mI = 0; mI <= this.TBConnectionParameter.Count() - 1; mI++) {
				mSection = "TriggerBoardNo" + (mI + 1).ToString();
				
                this.TBConnectionParameter[mI].CardType = (eTriggerBoardType)Convert.ToInt32(MReadSaveIni.ReadIniString(mSection, "CardType", fileName, "0"));
                switch (this.TBConnectionParameter[mI].CardType)
                {
					case eTriggerBoardType.None:

						break;
					case eTriggerBoardType.Trigger30:
                        this.TBConnectionParameter[mI].TriggerBoard30.PortName = MReadSaveIni.ReadIniString(mSection, "TriggerBoard30-PortName", fileName, "0");
                        this.TBConnectionParameter[mI].TriggerBoard30.BaudRate = MReadSaveIni.ReadIniString(mSection, "TriggerBoard30-BaudRate", fileName, "115200");
                        this.TBConnectionParameter[mI].TriggerBoard30.TimeOutTimes = Convert.ToDecimal(MReadSaveIni.ReadIniString(mSection, "TriggerBoard30-TimeOutTimes", fileName, "500"));

						break;
				}
			}
			return true;
		}

		/// <summary>[卡集合]</summary>
		/// <remarks></remarks>

		public List<ITriggerBoard> Items = new List<ITriggerBoard>();

        #region "Definitions"
        /// <summary>[Error Message]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetErrMsg(int triggerNo)
        {
            if (mIsSimulationType == true)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                return "";
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0))
            {
                return "";
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count)
            {
                return "";
            }
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].ErrMsg;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerNo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SetErrMsg(int triggerNo,string value)
        {
            if (mIsSimulationType == true)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                return "";
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0))
            {
                return "";
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count)
            {
                return "";
            }
            Items[TBParameter[triggerNo].ItemNo].ErrMsg = value;
            return "OK";
        }
       
        /// <summary>[忙碌中]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsBusy(int triggerNo)
        {
            if (mIsSimulationType == true)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                return false;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0))
            {
                return false;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count)
            {
                return false;
            }
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].IsBusy;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return false;
            }
        }


        /// <summary>[TimeOut(逾時)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool IsTimeOut(int triggerNo) {
					if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					return false;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					return false;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					return false;
				}
            try {
                return Items[TBParameter[triggerNo].ItemNo].IsTimeOut;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return true;
            }
			
		}
        /// <summary>[取得Timeout時間]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetTimeoutTimes(int triggerNo)
        {
            if (mIsSimulationType == true)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                return 0;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0))
            {
                return 0;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count)
            {
                return 0;
            }
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].TimeoutTimes;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return 0;
            }
        }
        /// <summary>
        /// 設定Timeout時間
        /// </summary>
        /// <param name="triggerNo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetTimeoutTimes(int triggerNo, int value)
        {
            Items[TBParameter[triggerNo].ItemNo].TimeoutTimes = value;
            return 1;
        }	
		
		/// <summary>[Is Port Open?]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool PortIsOpen(int triggerNo) {
			
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					return false;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					return false;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					return false;
				}
            try {
                return Items[TBParameter[triggerNo].ItemNo].PortIsOpen;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return false;
            }
			
		}
        /// <summary>[是否初始化成功]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsInitialOK(int triggerNo)
        {
            if (mIsSimulationType == true) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                return false;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
                return false;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
                return false;
            }
            try {
                return Items[TBParameter[triggerNo].ItemNo].IsInitialOK;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return false;
            }
        }
        /// <summary>[f Command的續傳量(F Command資料續傳-->f Command)]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int TransmissionResumingOfStepCount(int triggerNo) {

            if (mIsSimulationType == true) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                return -1;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
                return -1;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
                return -1;
            }
            try {
                return Items[TBParameter[triggerNo].ItemNo].TransmissionResumingOfStepCount;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                return -1;
            }
        }
		
		/// <summary>[J Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus JetParamRecipe(int triggerNo)
        {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try {
                return Items[TBParameter[triggerNo].ItemNo].JetParamRecipe;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
			
		}
        /// <summary>[G Command之回傳字串(將參數丟給Trigger Board)]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public MCommonTriggerBoard.sReceiveStatus JetParameter(int triggerNo)
        {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
            if (mIsSimulationType == true) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            try {
                return Items[TBParameter[triggerNo].ItemNo].JetParameter;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
        }

		/// <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus VisionRecipe(int triggerNo)  
			 {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				try {
                    return Items[TBParameter[triggerNo].ItemNo].VisionRecipe;
				} catch (Exception ex) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
					MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
					Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
					mStatus.STR = "";
					mStatus.Status = false;
					mStatus.Value = "";
					return mStatus;
				
			}
		}

        /// <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public MCommonTriggerBoard.sReceiveStatus JetRecipe(int triggerNo)
        {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
            if (mIsSimulationType == true) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            try {
                return Items[TBParameter[triggerNo].ItemNo].JetRecipe;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
        }
		/// <summary>[T Command之回傳字串(固定頻率打點)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus CycleRecipe(int triggerNo)
        { 
				MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				try {
                    return Items[TBParameter[triggerNo].ItemNo].CycleRecipe;
				} catch (Exception ex) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
					MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
					Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
					mStatus.STR = "";
					mStatus.Status = false;
					mStatus.Value = "";
					return mStatus;
				}
			}
		
		/// <summary>[P Command之回傳字串(固定間距打點)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus PitchRecipe(int triggerNo)
                         {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].PitchRecipe;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
		}
		/// <summary>[X Command之回傳字串(Dispensing Run)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus DispenseRun(int triggerNo)
                         {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].DispenseRun;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }			
		}
		/// <summary>[D Command之回傳字串(Dummy Run)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus DummyRun(int triggerNo)
                         {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try {
                return Items[TBParameter[triggerNo].ItemNo].DummyRun;
            } catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
		}
		/// <summary>[S Command之回傳字串(閥體溫度、膠管壓力、閥體電源)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus Parameter(int triggerNo)
                         {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].Parameter;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
		}
		/// <summary>[C Command之回傳字串(點膠觸發數)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus DispenseCounts(int triggerNo)

             {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "0";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "0";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "0";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].DispenseCounts;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "0";
                return mStatus;
            }
		}
		/// <summary>[O Command之回傳字串(取像觸發數)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus VisionCounts(int triggerNo)

             {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "0";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "0";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "0";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].VisionCounts;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "0";
                return mStatus;
            }
		}
		/// <summary>[V Command之回傳字串(韌體版本)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus Version(int triggerNo)

             {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].Version;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
		}
		/// <summary>[B Command之回傳字串(點膠真實Cycle)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus CycleArray(int triggerNo)
 {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].CycleArray;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }			
		}
		/// <summary>[E Command之回傳字串(異常代號)]</summary>
		/// <param name="triggerNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public MCommonTriggerBoard.sReceiveStatus ErrorCode(int triggerNo)
                         {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}

				//[Note]:卡不存在
				if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
				//[Note]:資料對應有問題
				if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
					mStatus.STR = "";
					mStatus.Status = true;
					mStatus.Value = "";
					return mStatus;
				}
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].ErrorCode;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
		}
        /// <summary>[c Command之回傳字串(異常清除)]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public MCommonTriggerBoard.sReceiveStatus ResetAlarm(int triggerNo)
        {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
            if (mIsSimulationType == true) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].ResetAlarm;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
        }

        /// <summary>[E Command之回傳字串(異常代號)]</summary>
        /// <param name="triggerNo"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public MCommonTriggerBoard.sReceiveStatus Temperature(int triggerNo)
        {
            MCommonTriggerBoard.sReceiveStatus mStatus = default(MCommonTriggerBoard.sReceiveStatus);
            if (mIsSimulationType == true) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
                MDateLog.gSyslog.Save("Simulation");
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }

            //[Note]:卡不存在
            if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            //[Note]:資料對應有問題
            if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
                mStatus.STR = "";
                mStatus.Status = true;
                mStatus.Value = "";
                return mStatus;
            }
            try
            {
                return Items[TBParameter[triggerNo].ItemNo].Temperature;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Temperature");
                mStatus.STR = "";
                mStatus.Status = false;
                mStatus.Value = "";
                return mStatus;
            }
        }
		#endregion

		#region "Properties"
		/// <summary>[初始化]</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Initial(sTriggerBoardConnectionParameter[] cardParameters)
		{
			try {
				string mErrMessage = "";
				int mI = 0;

				if (mIsSimulationType == true) {
					mIsCardIntialOK = false;
					return true;
				}

				for (mI = cardParameters.GetLowerBound(0); mI <= cardParameters.GetUpperBound(0); mI++) {
					//[Note]:依卡型號做初始化
					switch (cardParameters[mI].CardType) {
						case eTriggerBoardType.None:
							//[Note]:虛擬觸發板
							Items.Add(new CTriggerBoard_Virtual());
							TBParameter[mI].ItemNo = mI;

							break;
						case eTriggerBoardType.Trigger30:
							Items.Add(new CTriggerBoard_30());
							TBParameter[mI].ItemNo = mI;
							if (Items[Items.Count - 1].Initial(cardParameters[mI].TriggerBoard30.PortName, cardParameters[mI].TriggerBoard30.BaudRate) == false) {
								mErrMessage += "" + cardParameters[mI].CardTypeString + "初始化失敗.";
							}

							break;
					}
				}

				if (!string.IsNullOrEmpty(mErrMessage)) {
					//[Note]:有錯誤訊息
					mIsCardIntialOK = false;
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015000), "Error_1015000", eMessageLevel.Error);
					MDateLog.gSyslog.Save("Eror Message: " + mErrMessage,"" , eMessageLevel.Error);
					Interaction.MsgBox(mErrMessage, Constants.vbOKOnly, "TriggerBoard Collection");
					return false;
				}

				//[Note]:正常結束
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6004000));
				mIsCardIntialOK = true;
				return true;

			} catch (Exception ex) {
				mIsCardIntialOK = false;
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015000), "Error_1015000", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox("初始化失敗" + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[釋放資源]</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Dispose()
		{
			try {
				int mI = 0;

				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					return true;
				}
				if (mIsCardIntialOK == true) {
					//[Note]:卡片關閉
					for (mI = 0; mI <= Items.Count - 1; mI++) {
						Items[mI].Dispose();
					}
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					return true;
				} else {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[關閉通訊埠連線]</summary>
		/// <remarks></remarks>
		public bool Close()
		{
			try {
				int mI = 0;

				if (mIsSimulationType == true) {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					MDateLog.gSyslog.Save("Simulation");
					return true;
				}
				if (mIsCardIntialOK == true) {
					for (mI = 0; mI <= Items.Count - 1; mI++) {
						if (Items[mI].PortIsOpen == true) {
							//[Note]:關閉通訊埠連線
							Items[mI].Close();
						}
					}
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					return true;
				} else {
					MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}

		}

		/// <summary>[Send Command]</summary>
		/// <param name="cmdBtye"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SendCommandToSerialPort(int triggerNo, byte[] cmdBtye)
		{

			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}
			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}

			try {
                return Items[TBParameter[triggerNo].ItemNo].SendCommandToSerialPort(cmdBtye);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}

		}

		#endregion

		#region "Function"
		/// <summary>[取得目前電腦的序列埠代號]</summary>
		/// <param name="PortIDs"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetPortIDs(int triggerNo, ref string[] portIDs)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetPortIDs(ref portIDs);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}
		/// <summary>[J Command的資料串接]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="is1stStep"></param>
		/// <param name="zoneNo"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddJetParamRecipe(int triggerNo, bool is1stStep, int zoneNo, MCommonTriggerBoard.sTriggerJCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerJCmdParam parameter)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].AddJetParamRecipe(is1stStep, zoneNo, patternStep, isLastStep, parameter);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[將點膠資料丟給Trigger Board(J Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetParamRecipe(int triggerNo, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetJetParamRecipe(waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[Parameter of Jet(只丟參數)(G Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetParameter(int triggerNo,MCommonTriggerBoard.sTriggerGCmdParam parameter, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetJetParameter(parameter, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[L Command的資料串接]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="is1stStep"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddVisionRecipe(int triggerNo, bool is1stStep, MCommonTriggerBoard.sTriggerVisionCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerVisionCmdParam parameter)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].AddVisionRecipe(is1stStep, patternStep, isLastStep, parameter);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[將點膠資料丟給Trigger Board(L Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetVisionRecipe(int triggerNo, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {				
                return Items[TBParameter[triggerNo].ItemNo].SetVisionRecipe(waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[F Command的資料串接]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="is1stStep"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool AddJetRecipe(int triggerNo, bool is1stStep,MCommonTriggerBoard.sTriggerFCmdStep patternStep,
            bool isLastStep,MCommonTriggerBoard.sTriggerFCmdParam parameter )
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {				
                return Items[TBParameter[triggerNo].ItemNo].AddJetRecipe(is1stStep, patternStep, isLastStep, parameter);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[將點膠資料丟給Trigger Board(F Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetRecipe(int triggerNo, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetJetRecipe(waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[f Command的資料串接(F Command資料續傳-->f Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="is1stStep"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddJetRecipeUseTransmissionResuming(int triggerNo, bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {				
                return Items[TBParameter[triggerNo].ItemNo].AddJetRecipeUseTransmissionResuming(is1stStep, patternStep, isLastStep, parameter);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@AddJetRecipeUseTransmissionResuming");
				return false;
			}
		}

		/// <summary>[將點膠資料丟給Trigger Board(f Command-->F Command的續傳)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetRecipeByTransmissionResuming(int triggerNo, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetJetRecipeByTransmissionResuming(waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@SetJetRecipeByTransmissionResuming");
				return false;
			}
		}

		/// <summary>[Dummy Run(D Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="dispType"></param>
		/// <param name="valveNo"></param>
		/// <param name="zoneNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetDummyRun(int triggerNo, MCommonTriggerBoard.enmTriggerDispType dispType, eValveWorkMode valveNo, int zoneNo, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetDummyRun(dispType, valveNo, zoneNo, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[Free Type Dispensing (X Command)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="dispType"></param>
		/// <param name="valveNo"></param>
		/// <param name="zoneNo"></param>
		/// <param name="degree"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetDispenseRun(int triggerNo, MCommonTriggerBoard.enmTriggerDispType dispType, eValveWorkMode valveNo, int zoneNo, decimal degree, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetDispenseRun(dispType, valveNo, zoneNo, degree, 0, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[固定頻率打點 T Command(Cycle Time)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetCycleRecipe(int triggerNo, MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetCycleRecipe(parameter, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"", eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[固定間距打點 P Command(Pitch)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetPitchRecipe(int triggerNo, MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetPitchRecipe(parameter, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[設定閥體溫度]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="valve"></param>
		/// <param name="value"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetTempture(int triggerNo, eValveWorkMode valve, decimal value, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetTempture(valve, value, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[設定膠管壓力]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="valve"></param>
		/// <param name="value"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetPressure(int triggerNo, eValveWorkMode valve, decimal value, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetPressure(valve, value, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[設定閥體電源開關]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="valve"></param>
		/// <param name="powerOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetValvePower(int triggerNo, eValveWorkMode valve, bool powerOn, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetValvePower(valve, powerOn, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[設定Purge開關]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="valve"></param>
		/// <param name="purgeOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetPurge(int triggerNo, eValveWorkMode valve, bool purgeOn, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetPurge(valve, purgeOn, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}


		//20170920
		/// <summary>[設定SetTempture開關]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="valve"></param>
		/// <param name="purgeOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetTemptureOnOff(int triggerNo, eValveWorkMode valve, bool TemptureOn, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetTemptureOnOff(valve, TemptureOn, waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[回傳打點數]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="dotCounts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetDispenseCounts(int triggerNo, bool waitReturn , ref long dotCounts )
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetDispenseCounts(waitReturn, ref dotCounts);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[回傳取像數]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="dotCounts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetVisionCounts(int triggerNo, bool waitReturn , ref long dotCounts )
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetVisionCounts(waitReturn, ref dotCounts);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[傳回韌體版本]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetVersion(int triggerNo, bool waitReturn , ref string version )
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetVersion(waitReturn, ref version);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[傳回點膠Cycle(真實)]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="cycleArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetDispCycle(int triggerNo, bool waitReturn , ref string cycleArray)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetDispCycle(waitReturn, ref cycleArray);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[傳回異常代號]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="errorCode"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetErrorCode(int triggerNo, bool waitReturn , ref string errorCode )
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetErrorCode(waitReturn, ref errorCode);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}

		/// <summary>[異常清除]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetResetAlarm(int triggerNo, bool waitReturn = false)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].SetResetAlarm(waitReturn);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@Initial");
				return false;
			}
		}


		/// <summary>[傳回閥體溫度]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="tempArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetTemperature(int triggerNo, bool waitReturn , ref string tempArray )
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetTemperature(waitReturn, ref tempArray);
			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@GetTemperature");
				return false;
			}
		}


		//20171010
		/// <summary>[傳回開關狀態]</summary>
		/// <param name="triggerNo"></param>
		/// <param name="waitReturn"></param>
		/// <param name="tempArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetSwitch(int triggerNo, bool waitReturn , ref string tempArray)
		{
			if (mIsSimulationType == true) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");
				MDateLog.gSyslog.Save("Simulation");
				return true;
			}

			//[Note]:卡不存在
			if (triggerNo < TBParameter.GetLowerBound(0) | triggerNo > TBParameter.GetUpperBound(0)) {
				return false;
			}
			//[Note]:資料對應有問題
			if (TBParameter[triggerNo].ItemNo < 0 | TBParameter[triggerNo].ItemNo >= Items.Count) {
				return false;
			}
			try {
                return Items[TBParameter[triggerNo].ItemNo].GetSwitch(waitReturn, ref tempArray);

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				Interaction.MsgBox(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, Constants.vbOKOnly, "CTriggerBoardCollection@GetTemperature");
				return false;
			}
		}

		#endregion

	}
}

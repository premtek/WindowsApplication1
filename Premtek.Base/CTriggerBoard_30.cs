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
using System.Text;
using System.IO.Ports;
using ProjectCore;
using Premtek.Base;

namespace Premtek.Base
{
	public class CTriggerBoard_30 : ITriggerBoard
	{

		#region "Definitions"

		public string ErrMsg { get; set; }

		/// <summary>[f Command(F Command的續傳方式)之陣列大小]</summary>
		/// <remarks></remarks>

		private const int mFCmdTRArraySize = 740;
		/// <summary>[f Command的續傳量(F Command資料續傳-->f Command)]</summary>
		/// <remarks></remarks>

		private const int mTransmissionResumingOfStepCount = 20;
		/// <summary>[SerialPort]</summary>
		/// <remarks></remarks>
		private SerialPort withEventsField_mSerialPort = new SerialPort();
		private SerialPort mSerialPort {
			get { return withEventsField_mSerialPort; }
			set {
				if (withEventsField_mSerialPort != null) {
					withEventsField_mSerialPort.DataReceived -= mSerialPort_DataReceived;
				}
				withEventsField_mSerialPort = value;
				if (withEventsField_mSerialPort != null) {
					withEventsField_mSerialPort.DataReceived += mSerialPort_DataReceived;
				}
			}
		}
		/// <summary>[資料接收暫存]</summary>
		/// <remarks></remarks>
		private StringBuilder mRecievedData;
		/// <summary>[Stopwatch]</summary>
		/// <remarks></remarks>
		private Stopwatch mStopWatch;
		/// <summary>[傳送指令] </summary>
		/// <remarks></remarks>
		private byte[] mSendCmd;
		/// <summary>[J Command 欲傳送的內容(Recipe)]</summary>
		/// <remarks></remarks>
			//[2^20]
		private byte[] mSendJCmdArray = new byte[1048577];
		/// <summary>[紀錄 J Command指到的陣列位置]</summary>
		/// <remarks></remarks>
		private int mJCmdArrayIndex;
		/// <summary>[L Command欲傳送的內容(Recipe)]</summary>
		/// <remarks></remarks>
			//[2^20]
		private byte[] mSendVisionCmdArray = new byte[1048577];
		/// <summary>[F Command欲傳送的內容(Recipe)]</summary>
		/// <remarks></remarks>
			//[2^20]
		private byte[] mSendFCmdArray = new byte[1048577];
		/// <summary>[f Command欲傳送的內容(Recipe)-->F Command的續傳方式]
		/// 此有固定大小之限制(20筆資料量)</summary>
		/// <remarks></remarks>
		private byte[] mSendFCmdTRArray = new byte[mFCmdTRArraySize + 1];
		/// <summary>[紀錄 L Command指到的陣列位置]</summary>
		/// <remarks></remarks>
		private int mVisionCmdArrayIndex;
		/// <summary>[紀錄 F Command指到的陣列位置]</summary>
		/// <remarks></remarks>
		private int mFCmdArrayIndex;
		/// <summary>[紀錄 f Command指到的陣列位置-->F Command的續傳方式]</summary>
		/// <remarks></remarks>
		private int mFCmdTRArrayIndex;
		/// <summary>紀錄已經累計多少個資料內容(Byte) </summary>
		/// <remarks></remarks>
		private int mByteCount;
		/// <summary>[字串分析]</summary>
		/// <remarks></remarks>
		private string mAnalysisString;
		/// <summary>[忙碌中]</summary>
		/// <remarks></remarks>
		bool mIsBusy;
		public bool IsBusy {
			get { return mIsBusy; }
		}
		/// <summary>[TimeOut(逾時)]</summary>
		/// <remarks></remarks>
		bool mIsTimeOut;
		public bool IsTimeOut {
			get {
				try {
					if ((mStopWatch.ElapsedMilliseconds >= mTimeoutTimes)) {
						mIsBusy = false;
						mIsTimeOut = true;
						mSerialPort.DiscardInBuffer();
						mStopWatch.Stop();
						mRecievedData.Length = 0;
					}
					return mIsTimeOut;

				} catch (Exception ex) {
					ErrMsg = ex.ToString();
					if ((mStopWatch.ElapsedMilliseconds >= mTimeoutTimes)) {
						mIsBusy = false;
						mIsTimeOut = true;
						mStopWatch.Stop();
					}
					return mIsTimeOut;
				}
			}
		}

		/// <summary>[設定Timeout時間]</summary>
		/// <remarks></remarks>
		private int mTimeoutTimes;
		public int TimeoutTimes {
			get { return mTimeoutTimes; }
			set { mTimeoutTimes = value; }
		}
		/// <summary>[是否初始化成功]</summary>
		/// <remarks></remarks>
		private bool mIsInitialOK;
		public bool IsInitialOK {
			get { return mIsInitialOK; }
		}

		public int TransmissionResumingOfStepCount {
			get { return mTransmissionResumingOfStepCount; }
		}
		/// <summary>[Is Open(通訊埠連線狀態)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool PortIsOpen {
			get { return mSerialPort.IsOpen; }
		}
		/// <summary>[J Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mJetParamRecipe;
        public MCommonTriggerBoard.sReceiveStatus JetParamRecipe
        {
			get { return mJetParamRecipe; }
		}
		/// <summary>[G Command之回傳字串(將參數丟給Trigger Board)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mJetParameter;
        public MCommonTriggerBoard.sReceiveStatus JetParameter
        {
			get { return mJetParameter; }
		}
		/// <summary>[L Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mVisionRecipe;
        public MCommonTriggerBoard.sReceiveStatus VisionRecipe
        {
			get { return mVisionRecipe; }
		}
		/// <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mJetRecipe;
        public MCommonTriggerBoard.sReceiveStatus JetRecipe
        {
			get { return mJetRecipe; }
		}
		/// <summary>[f Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)(F Command資料續傳-->f Command)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mJetRecipeUseTransmissionResuming;
        public MCommonTriggerBoard.sReceiveStatus JetRecipeUseTransmissionResuming
        {
			get { return mJetRecipeUseTransmissionResuming; }
		}
		/// <summary>[T Command之回傳字串(固定頻率打點)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mCycleRecipe;
        public MCommonTriggerBoard.sReceiveStatus CycleRecipe
        {
			get { return mCycleRecipe; }
		}
		/// <summary>[P Command之回傳字串(固定間距打點)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mPitchRecipe;
        public MCommonTriggerBoard.sReceiveStatus PitchRecipe
        {
			get { return mPitchRecipe; }
		}
		/// <summary>[X Command之回傳字串(Dispensing Run)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mDispenseRun;
        public MCommonTriggerBoard.sReceiveStatus DispenseRun
        {
			get { return mDispenseRun; }
		}
		/// <summary>[D Command之回傳字串(Dummy Run)]
		/// [Note]:Recipe丟完後-->Dummy Run-->Free Type Dispensing</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mDummyRun;
        public MCommonTriggerBoard.sReceiveStatus DummyRun
        {
			get { return mDummyRun; }
		}
		/// <summary>[S Command之回傳字串(閥體溫度、膠管壓力、閥體電源開關)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mParameter;
        public MCommonTriggerBoard.sReceiveStatus Parameter
        {
			get { return mParameter; }
		}
		/// <summary>[C Command之回傳字串(打點數)]
		/// [Note]:上一次Jetting ON~OFF之間打的Dot數量</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mDispenseCounts;
        public MCommonTriggerBoard.sReceiveStatus DispenseCounts
        {
			get { return mDispenseCounts; }
		}
		/// <summary>[O Command之回傳字串(打點數)]
		/// [Note]:上一次Jetting ON~OFF之間打的Dot數量</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mVisionCounts;
        public MCommonTriggerBoard.sReceiveStatus VisionCounts
        {
			get { return mVisionCounts; }
		}
		/// <summary>[V Command之回傳字串(韌體版本)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mVersion;
        public MCommonTriggerBoard.sReceiveStatus Version
        {
			get { return mVersion; }
		}
		/// <summary>[B Command之回傳字串(點膠真實Cycle)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mCycleArray;
        public MCommonTriggerBoard.sReceiveStatus CycleArray
        {
			get { return mCycleArray; }
		}
		/// <summary>[E Command之回傳字串(異常代號)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mErrorCode;
        public MCommonTriggerBoard.sReceiveStatus ErrorCode
        {
			get { return mErrorCode; }
		}
		/// <summary>[c Command之回傳字串(異常清除)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mResetAlarm;
        public MCommonTriggerBoard.sReceiveStatus ResetAlarm
        {
			get { return mResetAlarm; }
		}


		/// <summary>[R Command之回傳字串(閥體溫度)]</summary>
		/// <remarks></remarks>
        private MCommonTriggerBoard.sReceiveStatus mTemperature;
        public MCommonTriggerBoard.sReceiveStatus Temperature
        {
			get { return mTemperature; }
		}

		#endregion

		#region "Properties"
		public CTriggerBoard_30()
		{
			try {
				mSerialPort = new SerialPort();
				mStopWatch = new Stopwatch();
				mRecievedData = new StringBuilder();

			} catch (Exception ex) {
				ErrMsg = ex.ToString();
				mSerialPort = null;
				mStopWatch = null;
				mRecievedData = null;
			}
		}

		public void Dispose()
		{
			try {
				mSerialPort.Dispose();

			} catch (Exception ex) {
				ErrMsg = ex.ToString();
			}
		}

		/// <summary>[初始化]</summary>
		/// <param name="portName"></param>
		/// <param name="baudRate"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Initial(string portName, string baudRate)
		{

			bool mIsPortExist = false;
			//[確認Com Port 是否存在]
			try {
				
                mSerialPort.PortName = "COM" + portName;
				//[連線方式]
                mSerialPort.BaudRate = Convert.ToInt16(baudRate);
				//[每秒傳輸位元]
                mSerialPort.Parity = Parity.None;
				//[同位檢查]
                mSerialPort.DataBits = 8;
				//[資料位元]
                mSerialPort.StopBits = StopBits.One;
				//[停止位元]
                mSerialPort.Handshake = System.IO.Ports.Handshake.None;
				//[流量控制]
                mSerialPort.Encoding = Encoding.ASCII;
				//[資料的編碼格式]
                mSerialPort.RtsEnable = true;
                mSerialPort.ReceivedBytesThreshold = 1;
                mSerialPort.NewLine = "\\r\\n";

				mIsBusy = false;
				mIsTimeOut = false;
				mIsPortExist = false;
				foreach (string GetPortName in SerialPort.GetPortNames()) {
					if (mSerialPort.PortName == GetPortName) {
						mIsPortExist = true;
						break; // TODO: might not be correct. Was : Exit For
					}
				}

				if (mSerialPort.IsOpen == true) {
					mSerialPort.Close();
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Alarm_2016000), "Alarm_2016000", eMessageLevel.Alarm);
                    MDateLog.gSyslog.Save("COM Port:" + mSerialPort.PortName + " BaudRate: " + mSerialPort.BaudRate, "", eMessageLevel.Alarm);
					mIsInitialOK = false;
					return false;
				}

				if (mIsPortExist == true) {
					mSerialPort.Open();
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015000), "INFO_6015000");
                    MDateLog.gSyslog.Save("COM Port:" + mSerialPort.PortName + " BaudRate: " + mSerialPort.BaudRate);
					mIsInitialOK = true;
					return true;
				}

                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015000), "Error_1015000", eMessageLevel.Error);
                MDateLog.gSyslog.Save("COM Port:" + mSerialPort.PortName,"" , eMessageLevel.Error);
				mIsInitialOK = false;
				return false;

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015000), "Error_1015000", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				//[Note:少加這樣項，造成若一開始就沒這個Port，卻還不跳TimeOut
				mIsTimeOut = true;
				mIsInitialOK = false;
				return false;
			}
		}

		/// <summary>[關閉通訊埠連線]</summary>
		/// <remarks></remarks>
		public void Close()
		{
			try {
				if (mSerialPort.IsOpen == true) {
					mSerialPort.Close();
				}
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6015001), "INFO_6015001");

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015001), "Error_1015001", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mSerialPort = null;
			}
		}


		private void mSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			SerialPort mSerialTemp = null;
			int mI = 0;
			bool mRecivedError = false;
			byte[] mDataBuffer = new byte[1024];
			string mSTR = null;
			int mStatus = 0;

			try {
				//[Note]:判斷接收資料是否為字元
				if (e.EventType == SerialData.Chars) {
					mSerialTemp = (SerialPort)sender;
					System.Threading.Thread.CurrentThread.Join(20);

					mSTR = mSerialTemp.ReadExisting();
					mRecievedData.Append(mSTR);
					//Debug.Print("RecievedData: " & mRecievedData.ToString())
					if (mRecievedData.ToString().EndsWith(">") == true) {
						mAnalysisString = mRecievedData.ToString();
						//Debug.Print("RecievedData: " & RecievedData.ToString())
						if ((mSendCmd != null)) {
							switch (mSendCmd[0]) {
								case 74:
									//[J] Recipe of Jet Valve
									mJetParamRecipe.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mJetParamRecipe.STR.Replace(">", "").Replace("J", "").Trim())) {
										mStatus = Convert.ToInt32(mJetParamRecipe.STR.Replace(">", "").Replace("J", "").Trim());
										if (mStatus == 0) {
											mJetParamRecipe.Status = true;
										} else {
											mJetParamRecipe.Status = false;
										}
									} else {
										mJetParamRecipe.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 73:
									break;
								//[I] Recipe of Needle-Jet Valve

								case 65:
									break;
								//[A] Recipe of Auger

								case 71:
									//[G] Parameter of Jet
									mJetParameter.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mJetParameter.STR.Replace(">", "").Replace("G", "").Trim())) {
										mStatus = Convert.ToInt32(mJetParameter.STR.Replace(">", "").Replace("G", "").Trim());
										if (mStatus == 0) {
											mJetParameter.Status = true;
										} else {
											mJetParameter.Status = false;
										}
									} else {
										mJetParameter.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 70:
									//[F] Recipe of Jet(Constant speed)
									mJetRecipe.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mJetRecipe.STR.Replace(">", "").Replace("F", "").Trim())) {
										mStatus = Convert.ToInt32(mJetRecipe.STR.Replace(">", "").Replace("F", "").Trim());
										if (mStatus == 0) {
											mJetRecipe.Status = true;
										} else {
											mJetRecipe.Status = false;
										}
									} else {
										mJetRecipe.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 76:
									//[L] Recipe of Vision trigger
									mVisionRecipe.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mVisionRecipe.STR.Replace(">", "").Replace("L", "").Trim())) {
										mStatus = Convert.ToInt32(mVisionRecipe.STR.Replace(">", "").Replace("L", "").Trim());
										if (mStatus == 0) {
											mVisionRecipe.Status = true;
										} else {
											mVisionRecipe.Status = false;
										}
									} else {
										mVisionRecipe.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 102:
									break;
								//[Note]:f Command不會有回傳資料
								//'[f] F Command 的續傳 --> Recipe of Jet(Constant speed)
								//mJetRecipeUseTransmissionResuming.STR = mAnalysisString
								//'[Note]:資料拆解
								//If IsNumeric(mJetRecipeUseTransmissionResuming.STR.Replace(">", "").Replace("f", "").Trim()) Then
								//    mStatus = CInt(mJetRecipeUseTransmissionResuming.STR.Replace(">", "").Replace("f", "").Trim())
								//    If mStatus = 0 Then
								//        mJetRecipeUseTransmissionResuming.Status = True
								//    Else
								//        mJetRecipeUseTransmissionResuming.Status = False
								//    End If
								//Else
								//    mJetRecipeUseTransmissionResuming.Status = False
								//End If
								//mSerialTemp.DiscardInBuffer()
								//mIsBusy = False
								//mStopWatch.Stop()
								//mRecievedData.Length = 0
								//mRecievedData.Clear()

								case 88:
									//[X] Free type dispensing
									mDispenseRun.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mDispenseRun.STR.Replace(">", "").Replace("X", "").Trim())) {
										mStatus = Convert.ToInt32(mDispenseRun.STR.Replace(">", "").Replace("X", "").Trim());
										if (mStatus == 0) {
											mDispenseRun.Status = true;
										} else {
											mDispenseRun.Status = false;
										}
									} else {
										mDispenseRun.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 68:
									//[D] Dummy Run(Auto Tune)
									mDummyRun.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mDummyRun.STR.Replace(">", "").Replace("D", "").Trim())) {
										mStatus = Convert.ToInt32(mDummyRun.STR.Replace(">", "").Replace("D", "").Trim());
										if (mStatus == 0) {
											mDummyRun.Status = true;
										} else {
											mDummyRun.Status = false;
										}
									} else {
										mDummyRun.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 84:
									//[T] T type dispensing(固定Cycle觸發)
									mCycleRecipe.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mCycleRecipe.STR.Replace(">", "").Replace("T", "").Trim())) {
										mStatus = Convert.ToInt32(mCycleRecipe.STR.Replace(">", "").Replace("T", "").Trim());
										if (mStatus == 0) {
											mCycleRecipe.Status = true;
										} else {
											mCycleRecipe.Status = false;
										}
									} else {
										mCycleRecipe.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 80:
									//[P] P type dispensing(固定Pitch觸發)
									mPitchRecipe.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mPitchRecipe.STR.Replace(">", "").Replace("P", "").Trim())) {
										mStatus = Convert.ToInt32(mPitchRecipe.STR.Replace(">", "").Replace("P", "").Trim());
										if (mStatus == 0) {
											mPitchRecipe.Status = true;
										} else {
											mPitchRecipe.Status = false;
										}
									} else {
										mPitchRecipe.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 67:
									//[C] Check Dispense Counts
									mDispenseCounts.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mDispenseCounts.STR.Replace(">", "").Replace("C", "").Trim())) {
										mStatus = Convert.ToInt32(mDispenseCounts.STR.Replace(">", "").Replace("C", "").Trim());
										mDispenseCounts.Value = mStatus.ToString();
										mDispenseCounts.Status = true;
									} else {
										mDispenseCounts.Value = "0";
										mDispenseCounts.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 66:
									//[B] Check Dispensing Cycle Time
									mCycleArray.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mCycleArray.STR.Replace(">", "").Replace("B", "").Trim())) {
										mStatus = Convert.ToInt32(mCycleArray.STR.Replace(">", "").Replace("B", "").Trim());
										mCycleArray.Value = mStatus.ToString();
										mCycleArray.Status = true;
									} else {
										mCycleArray.Value = "";
										mCycleArray.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 86:
									//[V] Check Version
									mVersion.STR = mAnalysisString;
									//[Note]:資料拆解
									mVersion.Value = mVersion.STR.Replace(">", "").Replace("V", "").Trim();
									mVersion.Status = true;
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 69:
									//[E] Check Error Code
									mErrorCode.STR = mAnalysisString;
									//[Note]:資料拆解
									mErrorCode.Value = mErrorCode.STR.Replace(">", "").Replace("E", "").Trim();
									mErrorCode.Status = true;

									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 99:
									//[c] Alarm Reset
									mResetAlarm.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mResetAlarm.STR.Replace(">", "").Replace("c", "").Trim())) {
										mStatus = Convert.ToInt32(mResetAlarm.STR.Replace(">", "").Replace("c", "").Trim());
										if (mStatus == 0) {
											mResetAlarm.Status = true;
										} else {
											mResetAlarm.Status = false;
										}
									} else {
										mResetAlarm.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 83:
									//[S] Set Parameter
									mParameter.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mParameter.STR.Replace(">", "").Replace("S", "").Trim())) {
										mStatus = Convert.ToInt32(mParameter.STR.Replace(">", "").Replace("S", "").Trim());
										if (mStatus == 0) {
											mParameter.Status = true;
										} else {
											mParameter.Status = false;
										}
									} else {
										mParameter.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 82:
									//[R] Get Temperature
									mTemperature.STR = mAnalysisString;
									//[Note]:
									//20171010
                        
									if (mTemperature.STR.Split(',')[0] == "R 0 ") {
										mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 0 ", "").Trim();
									} else if (mTemperature.STR.Split(',')[0] == "R 1 ") {
										mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 1 ", "").Trim();
									} else if (mTemperature.STR.Split(',')[0] == "R 2 ") {
										mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 2 ", "").Trim();
									} else if (mTemperature.STR.Split(',')[0] == "R 3 ") {
										mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 3 ", "").Trim();
									} else if (mTemperature.STR.Split(',')[0] == "R 4 ") {
										mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 4 ", "").Trim();
									} else if (mTemperature.STR.Split(',')[0] == "R 5 ") {
										mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R 5 ", "").Trim();
									}

									//mTemperature.Value = mTemperature.STR.Replace(">", "").Replace("R", "").Trim()

									mTemperature.Status = true;

									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								case 79:
									//[O]:Get Vision Count
									mVisionCounts.STR = mAnalysisString;
									//[Note]:資料拆解
									if (Information.IsNumeric(mVisionCounts.STR.Replace(">", "").Replace("O", "").Trim())) {
										mStatus = Convert.ToInt32(mVisionCounts.STR.Replace(">", "").Replace("O", "").Trim());
										mVisionCounts.Value = mStatus.ToString();
										mVisionCounts.Status = true;
									} else {
										mVisionCounts.Value = "0";
										mVisionCounts.Status = false;
									}
									mSerialTemp.DiscardInBuffer();
									mIsBusy = false;
									mStopWatch.Stop();
									mRecievedData.Length = 0;
									mRecievedData.Clear();

									break;
								default:
									mRecivedError = false;
									for (mI = 0; mI <= mSendCmd.Length - 1; mI++) {
										if (mSendCmd[mI] != mDataBuffer[mI]) {
											mRecivedError = true;
										}
									}

									if (mRecivedError == false) {
										mSerialTemp.DiscardInBuffer();

										mIsBusy = false;
										mStopWatch.Stop();
										mRecievedData.Length = 0;
									}
									mRecievedData.Clear();

									break;
							}
						}
					}
				}
			} catch (Exception ex) {
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();

			}
		}



		#endregion

		#region "Function"

		/// <summary>[取得目前電腦的序列埠代號]</summary>
		/// <param name="portIDs"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetPortIDs(ref string[] portIDs)
		{
			List<string> mPortNameList = new List<string>();
			foreach (string mGetPortName in SerialPort.GetPortNames()) {
				mPortNameList.Add(mGetPortName.Substring(3));
			}
			portIDs = mPortNameList.ToArray();
			return true;
		}

		/// <summary>[傳送命令]</summary>
		/// <param name="commandBtye"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SendCommandToSerialPort(byte[] commandBtye)
		{
			try {
				mSendCmd = commandBtye;
				if (mSerialPort.IsOpen == true) {
					mIsBusy = true;
					mIsTimeOut = false;
					mSerialPort.Write(mSendCmd, 0, mSendCmd.Length);
					mStopWatch.Restart();
					return true;
				} else {
					return false;
				}

			} catch (Exception ex) {
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				return false;
			}
		}

		/// <summary>[J Command之命令串接]</summary>
		/// <param name="is1stStep"></param>
		/// <param name="zoneNo"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddJetParamRecipe(bool is1stStep, int zoneNo, MCommonTriggerBoard.sTriggerJCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerJCmdParam parameter)
		{
			try {
				const int mScale = 1000;
				//[將mm轉成um(單位轉換)]

				byte[] mZoneNo = new byte[4];
				byte[] mJetTime = new byte[4];
				byte[] mTolerance = new byte[4];
				byte[] mMeasureLength = new byte[4];
				byte[] mMeasurePitch = new byte[4];
				byte[] mPath = new byte[4];
				//[圖樣型態(Dot、Line、Arc)]
				byte[] mStartPosX = new byte[4];
				//[起始點]
				byte[] mStartPosY = new byte[4];
				byte[] mStartPosZ = new byte[4];
				byte[] mEndPosX = new byte[4];
				//[結束點]
				byte[] mEndPosY = new byte[4];
				byte[] mEndPosZ = new byte[4];
				byte[] mCenterX = new byte[4];
				//[中心點]
				byte[] mCenterY = new byte[4];
				byte[] mCenterZ = new byte[4];
				byte[] mDirection = new byte[4];
				//[方向 0:CW  1:CCW]
				byte[] mDotCounts = new byte[4];
				//[點數]
				byte[] mGluePressure = new byte[4];
				//[膠管壓力]
				byte[] mJetPressure = new byte[4];
				//[Jet Pressure]  advjet
				byte[] mPulseTime = new byte[4];
				//[Pulse Time]
				byte[] mFallingVelocity = new byte[4];
				//[FallingVelocity(%)]
				byte[] mStroke = new byte[4];
				//[Stroke(%)]
				byte[] mSpare = new byte[4];
				//[預留]
				byte[] mEnd = new byte[4];
				//[結束字元]

				//[Note]:全套
				//[Note]: "J" + ZoneNo + JetTime + Tolerance + MeasureLength + MeasurePitch + Path + StartPosX + StartPosY + StartPosZ + EndPosX + EndPosY + EndPosZ + CenterX + CenterY + CenterZ 
				//            + Direction + DotCounts + GluePressure + JetPressure + PulseTime + FallingVelocity + Stroke + Spare + Spare
				//24*4+1=97

				//[Note]:半套
				//[Note]:     + Path + StartPosX + StartPosY + StartPosZ + EndPosX + EndPosY + EndPosZ + CenterX + CenterY + CenterZ 
				//            + Direction + DotCounts + GluePressure + JetPressure + PulseTime + FallingVelocity + Stroke + Spare + Spare
				//19*4=76


				mSpare = BitConverter.GetBytes(Convert.ToInt32(0));
				mEnd = BitConverter.GetBytes(Convert.ToInt32(-1));
				mZoneNo = BitConverter.GetBytes(zoneNo);
				if (is1stStep == true) {
                    mJetTime = BitConverter.GetBytes(Convert.ToInt32(parameter.JetTime));
                    mTolerance = BitConverter.GetBytes(Convert.ToInt32(parameter.Tolerance));
                    mMeasureLength = BitConverter.GetBytes(Convert.ToInt32(parameter.MeasureLength));
                    mMeasurePitch = BitConverter.GetBytes(Convert.ToInt32(parameter.MeasurePitch));
				}

                mPath = BitConverter.GetBytes(Convert.ToInt32(patternStep.Path));
                mStartPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosX * mScale));
                mStartPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosY * mScale));
                mStartPosZ = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosZ * mScale));
                mEndPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosX * mScale));
                mEndPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosY * mScale));
                mEndPosZ = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosZ * mScale));
                mCenterX = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosX * mScale));
                mCenterY = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosY * mScale));
                mCenterZ = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosZ * mScale));
                mDirection = BitConverter.GetBytes(Convert.ToInt32(patternStep.Dir));
                mDotCounts = BitConverter.GetBytes(Convert.ToInt32(patternStep.DotCounts));
                mGluePressure = BitConverter.GetBytes(Convert.ToInt32(patternStep.GluePressure));
                mJetPressure = BitConverter.GetBytes(Convert.ToInt32(patternStep.JetPressure));
                mPulseTime = BitConverter.GetBytes(Convert.ToInt32(patternStep.PulseTime));
                mFallingVelocity = BitConverter.GetBytes(Convert.ToInt32(patternStep.FallingVelocity));
                mStroke = BitConverter.GetBytes(Convert.ToInt32(patternStep.Stroke));


				if (is1stStep == true) {
					mSendJCmdArray[0] = Convert.ToByte(char.ConvertToUtf32("J",0)); //char.ConvertToUtf32("J", 0);
					mJCmdArrayIndex = 1;
					//ZoneNo
					for (int mI = 0; mI <= mZoneNo.Length - 1; mI++) {
						mSendJCmdArray[mJCmdArrayIndex + mI] = mZoneNo[mI];
					}
					mJCmdArrayIndex = mJCmdArrayIndex + mZoneNo.Length;
					//JetTime
					for (int mI = 0; mI <= mJetTime.Length - 1; mI++) {
						mSendJCmdArray[mJCmdArrayIndex + mI] = mJetTime[mI];
					}
					mJCmdArrayIndex = mJCmdArrayIndex + mJetTime.Length;
					//Tolerance
					for (int mI = 0; mI <= mTolerance.Length - 1; mI++) {
						mSendJCmdArray[mJCmdArrayIndex + mI] = mTolerance[mI];
					}
					mJCmdArrayIndex = mJCmdArrayIndex + mTolerance.Length;
					//MeasureLength
					for (int mI = 0; mI <= mMeasureLength.Length - 1; mI++) {
						mSendJCmdArray[mJCmdArrayIndex + mI] = mMeasureLength[mI];
					}
					mJCmdArrayIndex = mJCmdArrayIndex + mMeasureLength.Length;
					//MeasurePitch
					for (int mI = 0; mI <= mMeasurePitch.Length - 1; mI++) {
						mSendJCmdArray[mJCmdArrayIndex + mI] = mMeasurePitch[mI];
					}
					mJCmdArrayIndex = mJCmdArrayIndex + mMeasurePitch.Length;
				}

				//Path
				for (int mI = 0; mI <= mPath.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mPath[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mPath.Length;
				//StartPosX
				for (int mI = 0; mI <= mStartPosX.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mStartPosX[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mStartPosX.Length;
				//StartPosY
				for (int mI = 0; mI <= mStartPosY.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mStartPosY[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mStartPosY.Length;
				//StartPosZ
				for (int mI = 0; mI <= mStartPosZ.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mStartPosZ[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mStartPosZ.Length;
				//EndPosX
				for (int mI = 0; mI <= mEndPosX.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mEndPosX[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mEndPosX.Length;
				//EndPosY
				for (int mI = 0; mI <= mEndPosY.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mEndPosY[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mEndPosY.Length;
				//EndPosZ
				for (int mI = 0; mI <= mEndPosZ.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mEndPosZ[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mEndPosZ.Length;
				//CenterX
				for (int mI = 0; mI <= mCenterX.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mCenterX[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mCenterX.Length;
				//CenterY
				for (int mI = 0; mI <= mCenterY.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mCenterY[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mCenterY.Length;
				//CenterZ
				for (int mI = 0; mI <= mCenterZ.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mCenterZ[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mCenterZ.Length;
				//Direction
				for (int mI = 0; mI <= mDirection.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mDirection[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mDirection.Length;
				//DotCounts
				for (int mI = 0; mI <= mDotCounts.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mDotCounts[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mDotCounts.Length;
				//GluePressure
				for (int mI = 0; mI <= mGluePressure.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mGluePressure[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mGluePressure.Length;
				//JetPressure
				for (int mI = 0; mI <= mJetPressure.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mJetPressure[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mJetPressure.Length;
				//PulseTime
				for (int mI = 0; mI <= mPulseTime.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mPulseTime[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mPulseTime.Length;
				//FallingVelocity
				for (int mI = 0; mI <= mFallingVelocity.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mFallingVelocity[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mFallingVelocity.Length;
				//Stroke
				for (int mI = 0; mI <= mStroke.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mStroke[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mStroke.Length;
				//Spare
				for (int mI = 0; mI <= mSpare.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mSpare[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mSpare.Length;
				//Spare
				for (int mI = 0; mI <= mSpare.Length - 1; mI++) {
					mSendJCmdArray[mJCmdArrayIndex + mI] = mSpare[mI];
				}
				mJCmdArrayIndex = mJCmdArrayIndex + mSpare.Length;

				if (isLastStep == true) {
					//End
					for (int mI = 0; mI <= mEnd.Length - 1; mI++) {
						mSendJCmdArray[mJCmdArrayIndex + mI] = mEnd[mI];
					}
					mJCmdArrayIndex = mJCmdArrayIndex + mEnd.Length;
				}

				return true;

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[Recipe of Jet (J Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetParamRecipe(bool waitReturn = false)
		{
			try {
				byte[] mSendArray = new byte[mJCmdArrayIndex];
				int mI = 0;

				for (mI = 0; mI <= mJCmdArrayIndex - 1; mI++) {
					mSendArray[mI] = mSendJCmdArray[mI];
				}
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mJetParamRecipe.Status = false;
					}

					return mJetParamRecipe.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[L Command之命令串接]</summary>
		/// <param name="is1stStep"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddVisionRecipe(bool is1stStep, MCommonTriggerBoard.sTriggerVisionCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerVisionCmdParam parameter)
		{
			try {
				const int mScale = 1000;
				//[將mm轉成um(單位轉換)]
				byte[] mTotalPoints = new byte[4];
				//[總取像數]
				byte[] mApproachPosX = new byte[4];
				//[助跑位置X]
				byte[] mApproachPosY = new byte[4];
				//[助跑位置Y]
				byte[] mPath = new byte[4];
				//[圖樣型態(Dot、Line、Arc)]
				byte[] mDirection = new byte[4];
				//[方向 0:CW  1:CCW]
				byte[] mVelocity = new byte[4];
				//[Stage移動速度]
				byte[] mPointCounts = new byte[4];
				//[取像數]
				byte[] mStartPosX = new byte[4];
				//[起點]
				byte[] mStartPosY = new byte[4];
				byte[] mEndPosX = new byte[4];
				//[終點]
				byte[] mEndPosY = new byte[4];
				byte[] mCenterX = new byte[4];
				//[圓心點]
				byte[] mCenterY = new byte[4];
				byte[] mEnd = new byte[4];
				//[結束字元]
				byte[] mDelayTime = new byte[4];
				//[訊號延遲時間]
				byte[] mWatiTime = new byte[4];
				//[隔多久才會接到下一條線段]
				byte[] mSpare = new byte[4];
				//[空白]

				//[Note]:全套
				//[Note]: "L" + TotalPoints + ApproachPosX + ApproachPosY + DelayTime + (Velocity+Direction+Path) + PointCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
				//13*4+1=53

				//[Note]:半套
				//[Note]:                                                             + (Velocity+Direction+Path) + PointCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
				//9*4=36

				mEnd = BitConverter.GetBytes(Convert.ToInt32(-1));
				if (is1stStep == true) {
					
                    mTotalPoints = BitConverter.GetBytes(Convert.ToInt32(parameter.TotalPointCounts));
                    mApproachPosX = BitConverter.GetBytes(Convert.ToInt32(parameter.ApproachPosX * mScale));
                    mApproachPosY = BitConverter.GetBytes(Convert.ToInt32(parameter.ApproachPosY * mScale));
                    mDelayTime = BitConverter.GetBytes(Convert.ToInt32(parameter.DelayTime * mScale));
				}
				
                mVelocity = BitConverter.GetBytes(Convert.ToInt32(patternStep.Velocity));
                mDirection = BitConverter.GetBytes(Convert.ToInt32(patternStep.Dir));
                mPath = BitConverter.GetBytes(Convert.ToInt32(patternStep.Path));
                mPointCounts = BitConverter.GetBytes(Convert.ToInt32(patternStep.PointCounts));
                mStartPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosX * mScale));
                mStartPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosY * mScale));
                mEndPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosX * mScale));
                mEndPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosY * mScale));
                mCenterX = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosX * mScale));
                mCenterY = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosY * mScale));
                mWatiTime = BitConverter.GetBytes(Convert.ToInt32(patternStep.PathWaitTime * mScale));

				if (is1stStep == true) {
					mSendVisionCmdArray[0] =Convert.ToByte(char.ConvertToUtf32("L", 0));
					mVisionCmdArrayIndex = 1;
					//TotalPoints
					for (int mI = 0; mI <= mTotalPoints.Length - 1; mI++) {
						mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mTotalPoints[mI];
					}
					mVisionCmdArrayIndex = mVisionCmdArrayIndex + mTotalPoints.Length;
					//ApproachPosX
					for (int mI = 0; mI <= mApproachPosX.Length - 1; mI++) {
						mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mApproachPosX[mI];
					}
					mVisionCmdArrayIndex = mVisionCmdArrayIndex + mApproachPosX.Length;
					//ApproachPosY
					for (int mI = 0; mI <= mApproachPosY.Length - 1; mI++) {
						mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mApproachPosY[mI];
					}
					//DelayTime
					mVisionCmdArrayIndex = mVisionCmdArrayIndex + mApproachPosY.Length;
					for (int mI = 0; mI <= mDelayTime.Length - 1; mI++) {
						mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mDelayTime[mI];
					}
					mVisionCmdArrayIndex = mVisionCmdArrayIndex + mDelayTime.Length;
				}

				//(Velocity+Direction+Path)
				mSendVisionCmdArray[mVisionCmdArrayIndex] = mVelocity[0];
				mSendVisionCmdArray[mVisionCmdArrayIndex + 1] = mVelocity[1];
				mSendVisionCmdArray[mVisionCmdArrayIndex + 2] = mDirection[0];
				mSendVisionCmdArray[mVisionCmdArrayIndex + 3] = mPath[0];
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + 4;
				//PointCounts
				for (int mI = 0; mI <= mPointCounts.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mPointCounts[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mPointCounts.Length;
				//StartPosX
				for (int mI = 0; mI <= mStartPosX.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mStartPosX[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mStartPosX.Length;
				//StartPosY
				for (int mI = 0; mI <= mStartPosY.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mStartPosY[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mStartPosY.Length;
				//EndPosX
				for (int mI = 0; mI <= mEndPosX.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mEndPosX[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mEndPosX.Length;
				//EndPosY
				for (int mI = 0; mI <= mEndPosY.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mEndPosY[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mEndPosY.Length;
				//CenterX
				for (int mI = 0; mI <= mCenterX.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mCenterX[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mCenterX.Length;
				//CenterY
				for (int mI = 0; mI <= mCenterY.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mCenterY[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mCenterY.Length;
				//WatiTime
				for (int mI = 0; mI <= mWatiTime.Length - 1; mI++) {
					mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mWatiTime[mI];
				}
				mVisionCmdArrayIndex = mVisionCmdArrayIndex + mWatiTime.Length;

				if (isLastStep == true) {
					//End
					for (int mI = 0; mI <= mEnd.Length - 1; mI++) {
						mSendVisionCmdArray[mVisionCmdArrayIndex + mI] = mEnd[mI];
					}
					mVisionCmdArrayIndex = mVisionCmdArrayIndex + mEnd.Length;
				}

				return true;

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[Recipe of Vision trigger (L Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetVisionRecipe(bool waitReturn = false)
		{
			try {
				byte[] mSendArray = new byte[mVisionCmdArrayIndex];
				int mI = 0;

				for (mI = 0; mI <= mVisionCmdArrayIndex - 1; mI++) {
					mSendArray[mI] = mSendVisionCmdArray[mI];
				}
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mVisionRecipe.Status = false;
					}

					return mVisionRecipe.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[F Command之命令串接]</summary>
		/// <param name="is1stStep"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddJetRecipe(bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter)
		{
			try {
				const int mScale = 1000;
				//[將mm轉成um(單位轉換)]
				byte[] mTotalDots = new byte[4];
				//[總打點數]
				byte[] mApproachPosX = new byte[4];
				//[助跑位置X]
				byte[] mApproachPosY = new byte[4];
				//[助跑位置Y]
				byte[] mPath = new byte[4];
				//[圖樣型態(Dot、Line、Arc)]
				byte[] mDirection = new byte[4];
				//[方向 0:CW  1:CCW]
				byte[] mVelocity = new byte[4];
				//[Stage移動速度]
				byte[] mDotCounts = new byte[4];
				//[點數]
				byte[] mStartPosX = new byte[4];
				//[起點]
				byte[] mStartPosY = new byte[4];
				byte[] mEndPosX = new byte[4];
				//[終點]
				byte[] mEndPosY = new byte[4];
				byte[] mCenterX = new byte[4];
				//[圓心點]
				byte[] mCenterY = new byte[4];
				byte[] mEnd = new byte[4];
				//[結束字元]
				byte[] mWatiTime = new byte[4];
				//[隔多久才會接到下一條線段]
				byte[] mSpare = new byte[4];
				//[空白]

				//[Note]:全套
				//[Note]: "F" + TotalDots + ApproachPosX + ApproachPosY + Spare + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
				//13*4+1=53

				//[Note]:半套
				//[Note]:                                                       + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
				//9*4=36

				mEnd = BitConverter.GetBytes(Convert.ToInt32(-1));
				if (is1stStep == true) {
					
                    mTotalDots = BitConverter.GetBytes(Convert.ToInt32(parameter.TotalDotCounts));
                    mApproachPosX = BitConverter.GetBytes(Convert.ToInt32(parameter.ApproachPosX * mScale));
                    mApproachPosY = BitConverter.GetBytes(Convert.ToInt32(parameter.ApproachPosY * mScale));
					mSpare = BitConverter.GetBytes(Convert.ToInt32(0));
				}
				
                mVelocity = BitConverter.GetBytes(Convert.ToInt32(patternStep.Velocity));
                mDirection = BitConverter.GetBytes(Convert.ToInt32(patternStep.Dir));
                mPath = BitConverter.GetBytes(Convert.ToInt32(patternStep.Path));
                mDotCounts = BitConverter.GetBytes(Convert.ToInt32(patternStep.DotCounts));
                mStartPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosX * mScale));
                mStartPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosY * mScale));
                mEndPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosX * mScale));
                mEndPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosY * mScale));
                mCenterX = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosX * mScale));
                mCenterY = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosY * mScale));
                mWatiTime = BitConverter.GetBytes(Convert.ToInt32(patternStep.PathWaitTime * mScale));

				if (is1stStep == true) {
					mSendFCmdArray[0] = Convert.ToByte(char.ConvertToUtf32("F", 0));
					mFCmdArrayIndex = 1;
					//TotalDots
					for (int mI = 0; mI <= mTotalDots.Length - 1; mI++) {
						mSendFCmdArray[mFCmdArrayIndex + mI] = mTotalDots[mI];
					}
					mFCmdArrayIndex = mFCmdArrayIndex + mTotalDots.Length;
					//ApproachPosX
					for (int mI = 0; mI <= mApproachPosX.Length - 1; mI++) {
						mSendFCmdArray[mFCmdArrayIndex + mI] = mApproachPosX[mI];
					}
					mFCmdArrayIndex = mFCmdArrayIndex + mApproachPosX.Length;
					//ApproachPosY
					for (int mI = 0; mI <= mApproachPosY.Length - 1; mI++) {
						mSendFCmdArray[mFCmdArrayIndex + mI] = mApproachPosY[mI];
					}
					mFCmdArrayIndex = mFCmdArrayIndex + mApproachPosY.Length;
					//Spare
					for (int mI = 0; mI <= mSpare.Length - 1; mI++) {
						mSendFCmdArray[mFCmdArrayIndex + mI] = mSpare[mI];
					}
					mFCmdArrayIndex = mFCmdArrayIndex + mSpare.Length;
				}

				//(Velocity+Direction+Path)
				mSendFCmdArray[mFCmdArrayIndex] = mVelocity[0];
				mSendFCmdArray[mFCmdArrayIndex + 1] = mVelocity[1];
				mSendFCmdArray[mFCmdArrayIndex + 2] = mDirection[0];
				mSendFCmdArray[mFCmdArrayIndex + 3] = mPath[0];
				mFCmdArrayIndex = mFCmdArrayIndex + 4;
				//DotCounts
				for (int mI = 0; mI <= mDotCounts.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mDotCounts[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mDotCounts.Length;
				//StartPosX
				for (int mI = 0; mI <= mStartPosX.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mStartPosX[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mStartPosX.Length;
				//StartPosY
				for (int mI = 0; mI <= mStartPosY.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mStartPosY[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mStartPosY.Length;
				//EndPosX
				for (int mI = 0; mI <= mEndPosX.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mEndPosX[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mEndPosX.Length;
				//EndPosY
				for (int mI = 0; mI <= mEndPosY.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mEndPosY[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mEndPosY.Length;
				//CenterX
				for (int mI = 0; mI <= mCenterX.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mCenterX[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mCenterX.Length;
				//CenterY
				for (int mI = 0; mI <= mCenterY.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mCenterY[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mCenterY.Length;
				//WaitTime
				for (int mI = 0; mI <= mWatiTime.Length - 1; mI++) {
					mSendFCmdArray[mFCmdArrayIndex + mI] = mWatiTime[mI];
				}
				mFCmdArrayIndex = mFCmdArrayIndex + mWatiTime.Length;

				if (isLastStep == true) {
					//End
					for (int mI = 0; mI <= mEnd.Length - 1; mI++) {
						mSendFCmdArray[mFCmdArrayIndex + mI] = mEnd[mI];
					}
					mFCmdArrayIndex = mFCmdArrayIndex + mEnd.Length;
				}

				return true;

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[Recipe of Jet Constant Speed (F Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetRecipe(bool waitReturn = false)
		{
			try {
				byte[] mSendArray = new byte[mFCmdArrayIndex];
				int mI = 0;

				for (mI = 0; mI <= mFCmdArrayIndex - 1; mI++) {
					mSendArray[mI] = mSendFCmdArray[mI];
				}
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mJetRecipe.Status = false;
					}

					return mJetRecipe.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[f Command的資料串接(F Command資料續傳-->f Command)]</summary>
		/// <param name="is1stStep"></param>
		/// <param name="patternStep"></param>
		/// <param name="isLastStep"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool AddJetRecipeUseTransmissionResuming(bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter)
		{
			try {
				const int mScale = 1000;
				//[將mm轉成um(單位轉換)]

				byte[] mTotalDots = new byte[4];
				//[總打點數]
				byte[] mApproachPosX = new byte[4];
				//[助跑位置X]
				byte[] mApproachPosY = new byte[4];
				//[助跑位置Y]
				byte[] mPath = new byte[4];
				//[圖樣型態(Dot、Line、Arc)]
				byte[] mDirection = new byte[4];
				//[方向 0:CW  1:CCW]
				byte[] mVelocity = new byte[4];
				//[Stage移動速度]
				byte[] mDotCounts = new byte[4];
				//[點數]
				byte[] mStartPosX = new byte[4];
				//[起點]
				byte[] mStartPosY = new byte[4];
				byte[] mEndPosX = new byte[4];
				//[終點]
				byte[] mEndPosY = new byte[4];
				byte[] mCenterX = new byte[4];
				//[圓心點]
				byte[] mCenterY = new byte[4];
				byte[] mEnd = new byte[4];
				//[結束字元]
				byte[] mWatiTime = new byte[4];
				//[隔多久才會接到下一條線段]
				byte[] mSpare = new byte[4];
				//[空白]

				//[Note]:全套
				//        "f"  +mTotalDots + ApproachPosX + ApproachPosY 
				//[Note]: "f"  +     0     +       0      +       0      +   Spare   + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line     
				//13*4+1=53

				//[Note]:半套
				//[Note]:                                                            + (Velocity+Direction+Path) + DotCounts + StartPosX + StartPosY + EndPosX + EndPosY + CenterX + CenterY + Delay of next line
				//9*4=36

				mEnd = BitConverter.GetBytes(Convert.ToInt32(-1));
				if (is1stStep == true) {
					mTotalDots = BitConverter.GetBytes(Convert.ToInt32(0));
					mApproachPosX = BitConverter.GetBytes(Convert.ToInt32(0));
					mApproachPosY = BitConverter.GetBytes(Convert.ToInt32(0));
					mSpare = BitConverter.GetBytes(Convert.ToInt32(0));
				}
				
                mVelocity = BitConverter.GetBytes(Convert.ToInt32(patternStep.Velocity));
                mDirection = BitConverter.GetBytes(Convert.ToInt32(patternStep.Dir));
                mPath = BitConverter.GetBytes(Convert.ToInt32(patternStep.Path));
                mDotCounts = BitConverter.GetBytes(Convert.ToInt32(patternStep.DotCounts));
                mStartPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosX * mScale));
                mStartPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.StartPosY * mScale));
                mEndPosX = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosX * mScale));
                mEndPosY = BitConverter.GetBytes(Convert.ToInt32(patternStep.EndPosY * mScale));
                mCenterX = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosX * mScale));
                mCenterY = BitConverter.GetBytes(Convert.ToInt32(patternStep.CenPosY * mScale));
                mWatiTime = BitConverter.GetBytes(Convert.ToInt32(patternStep.PathWaitTime * mScale));

				if (is1stStep == true) {
					mSendFCmdTRArray[0] =Convert.ToByte(char.ConvertToUtf32("f", 0));
					mFCmdTRArrayIndex = 1;
					//TotalDots
					for (int mI = 0; mI <= mTotalDots.Length - 1; mI++) {
						mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mTotalDots[mI];
					}
					mFCmdTRArrayIndex = mFCmdTRArrayIndex + mTotalDots.Length;
					//ApproachPosX
					for (int mI = 0; mI <= mApproachPosX.Length - 1; mI++) {
						mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mApproachPosX[mI];
					}
					mFCmdTRArrayIndex = mFCmdTRArrayIndex + mApproachPosX.Length;
					//ApproachPosY
					for (int mI = 0; mI <= mApproachPosY.Length - 1; mI++) {
						mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mApproachPosY[mI];
					}
					mFCmdTRArrayIndex = mFCmdTRArrayIndex + mApproachPosY.Length;
					//Spare
					for (int mI = 0; mI <= mSpare.Length - 1; mI++) {
						mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mSpare[mI];
					}
					mFCmdTRArrayIndex = mFCmdTRArrayIndex + mSpare.Length;
				}

				//(Velocity+Direction+Path)
				mSendFCmdTRArray[mFCmdTRArrayIndex] = mVelocity[0];
				mSendFCmdTRArray[mFCmdTRArrayIndex + 1] = mVelocity[1];
				mSendFCmdTRArray[mFCmdTRArrayIndex + 2] = mDirection[0];
				mSendFCmdTRArray[mFCmdTRArrayIndex + 3] = mPath[0];
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + 4;
				//DotCounts
				for (int mI = 0; mI <= mDotCounts.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mDotCounts[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mDotCounts.Length;
				//StartPosX
				for (int mI = 0; mI <= mStartPosX.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mStartPosX[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mStartPosX.Length;
				//StartPosY
				for (int mI = 0; mI <= mStartPosY.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mStartPosY[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mStartPosY.Length;
				//EndPosX
				for (int mI = 0; mI <= mEndPosX.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mEndPosX[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mEndPosX.Length;
				//EndPosY
				for (int mI = 0; mI <= mEndPosY.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mEndPosY[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mEndPosY.Length;
				//CenterX
				for (int mI = 0; mI <= mCenterX.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mCenterX[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mCenterX.Length;
				//CenterY
				for (int mI = 0; mI <= mCenterY.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mCenterY[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mCenterY.Length;
				//WaitTime
				for (int mI = 0; mI <= mWatiTime.Length - 1; mI++) {
					mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mWatiTime[mI];
				}
				mFCmdTRArrayIndex = mFCmdTRArrayIndex + mWatiTime.Length;

				if (isLastStep == true) {
					//End
					for (int mI = 0; mI <= mEnd.Length - 1; mI++) {
						mSendFCmdTRArray[mFCmdTRArrayIndex + mI] = mEnd[mI];
					}
					mFCmdTRArrayIndex = mFCmdTRArrayIndex + mEnd.Length;
				}

				return true;

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}

		}

		/// <summary>[將點膠資料丟給Trigger Board(f Command-->F Command的續傳)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetJetRecipeByTransmissionResuming(bool waitReturn = false)
		{
			try {
				//Dim mSendArray(mFCmdTRArrayIndex - 1) As Byte
				byte[] mSendArray = new byte[mFCmdTRArraySize + 1];

				int mI = 0;
				int mJ = 0;

				for (mJ = 0; mJ <= mFCmdTRArraySize; mJ++) {
					if (mJ <= mFCmdTRArrayIndex - 1) {
						for (mI = 0; mI <= mFCmdTRArrayIndex - 1; mI++) {
							mSendArray[mI] = mSendFCmdTRArray[mI];
						}
					} else {
						mSendArray[mJ] = 0;
					}
				}
				SendCommandToSerialPort(mSendArray);

				//[Note]:f Command不會有回傳資料
				mIsBusy = false;
				mIsTimeOut = false;
				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mJetRecipe.Status = false;
					}

					return mJetRecipe.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[Parameter of Jet(只丟參數)(G Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetJetParameter(MCommonTriggerBoard.sTriggerGCmdParam parameter, bool waitReturn = false)
		{
			try {
				int mI = 0;
				byte[] mHeadNo = new byte[4];
				byte[] mPulseTime = new byte[4];
				byte[] mJetTime = new byte[4];
				byte[] mStroke = new byte[4];
				byte[] mGluePressure = new byte[4];
				byte[] mTolerance = new byte[4];
				byte[] mMeasureLength = new byte[4];
				byte[] mMeasurePitch = new byte[4];
				byte[] mMeasureCounts = new byte[4];
				byte[] mMeasureCountsAfter = new byte[4];
				byte[] mSpare = new byte[4];
				byte[] mCloseVoltage = new byte[4];
				byte[] mJetPressure = new byte[4];
				byte[] mSendArray = new byte[53];
				int mArrayCounts = 0;
				byte[] mOpenTime = new byte[4];
				byte[] mCloseTime = new byte[4];
				byte[] mCycleTime = new byte[4];

				//[Note]: "G" + HeadNo + PulseTime + JetTime + Stroke + (CloseTime/OpenTime) + GluePressure + Tolerance + MeasureLength + MeasurePitch + MeasureCounts + CloseVoltage + JetPressure + CycleTime
				//13*4+1=53

				mHeadNo = BitConverter.GetBytes(parameter.HeadNo);
				mPulseTime = BitConverter.GetBytes(parameter.PulseTime);
				mJetTime = BitConverter.GetBytes(parameter.JetTime);
				mStroke = BitConverter.GetBytes(parameter.Stroke);
				mOpenTime = BitConverter.GetBytes(parameter.OpenTime);
				mCloseTime = BitConverter.GetBytes(parameter.CloseTime);
				mGluePressure = BitConverter.GetBytes(parameter.GluePressure);
				mTolerance = BitConverter.GetBytes(parameter.Tolerance);
				mMeasureLength = BitConverter.GetBytes(parameter.MeasureLength);
				mMeasurePitch = BitConverter.GetBytes(parameter.MeasurePitch);
				mMeasureCounts = BitConverter.GetBytes(parameter.MeasureCounts);
				mCloseVoltage = BitConverter.GetBytes(parameter.CloseVoltage);
				mJetPressure = BitConverter.GetBytes(parameter.JetPressure);
				mCycleTime = BitConverter.GetBytes(parameter.CycleTime);

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("G", 0));
				mArrayCounts = 1;
				//HeadNo
				for (mI = 0; mI <= mHeadNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mHeadNo[mI];
				}
				mArrayCounts = mArrayCounts + mHeadNo.Length;
				//PulseTime
				for (mI = 0; mI <= mPulseTime.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mPulseTime[mI];
				}
				mArrayCounts = mArrayCounts + mPulseTime.Length;
				//JetTime
				for (mI = 0; mI <= mJetTime.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mJetTime[mI];
				}
				mArrayCounts = mArrayCounts + mJetTime.Length;
				//Stroke
				for (mI = 0; mI <= mStroke.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mStroke[mI];
				}
				mArrayCounts = mArrayCounts + mStroke.Length;
				//OpenTime/CloseTime
				mSendArray[mArrayCounts] = mCloseTime[0];
				mSendArray[mArrayCounts + 1] = mCloseTime[1];
				mSendArray[mArrayCounts + 2] = mOpenTime[0];
				mSendArray[mArrayCounts + 3] = mOpenTime[1];
				mArrayCounts = mArrayCounts + 4;
				//GluePressure
				for (mI = 0; mI <= mGluePressure.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mGluePressure[mI];
				}
				mArrayCounts = mArrayCounts + mGluePressure.Length;
				//Tolerance
				for (mI = 0; mI <= mTolerance.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mTolerance[mI];
				}
				mArrayCounts = mArrayCounts + mTolerance.Length;
				//MeasureLength
				for (mI = 0; mI <= mMeasureLength.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mMeasureLength[mI];
				}
				mArrayCounts = mArrayCounts + mMeasureLength.Length;
				//MeasurePitch
				for (mI = 0; mI <= mMeasurePitch.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mMeasurePitch[mI];
				}
				mArrayCounts = mArrayCounts + mMeasurePitch.Length;
				//MeasureCounts
				for (mI = 0; mI <= mMeasureCounts.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mMeasureCounts[mI];
				}
				mArrayCounts = mArrayCounts + mMeasureCounts.Length;
				//CloseVoltage
				for (mI = 0; mI <= mCloseVoltage.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mCloseVoltage[mI];
				}
				mArrayCounts = mArrayCounts + mCloseVoltage.Length;
				//JetPressure
				for (mI = 0; mI <= mJetPressure.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mJetPressure[mI];
				}
				mArrayCounts = mArrayCounts + mJetPressure.Length;
				//CycleTime
				for (mI = 0; mI <= mCycleTime.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mCycleTime[mI];
				}
				mArrayCounts = mArrayCounts + mCycleTime.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mJetParameter.Status = false;
					}
					return mJetParameter.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[固定頻率打點 T Command(Cycle Time)]</summary>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetCycleRecipe(MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false)
		{
			try {
				int mI = 0;
				byte[] mZoneNo = new byte[4];
				byte[] mCycleTime = new byte[4];
				byte[] mDotCounts = new byte[4];
				byte[] mGluePressure = new byte[4];
				byte[] mJetPressure = new byte[4];
				byte[] mPulseTime = new byte[4];
				byte[] mOpenTime = new byte[4];
				byte[] mCloseTime = new byte[4];
				byte[] mCloseVoltage = new byte[4];
				byte[] mStroke = new byte[4];
				byte[] mSendArray = new byte[33];
				int mArrayCounts = 0;


				//[Note]: "T" + ZoneNo + CycleTime + Dots + GluePressure + JetPressure + PulseTime + (CloseTime/OpenTime) + (CloseVoltage/Stroke)
				//8*4+1=33

				mZoneNo = BitConverter.GetBytes(Convert.ToInt32(0));
				mCycleTime = BitConverter.GetBytes(parameter.CycleTime);
				mDotCounts = BitConverter.GetBytes(parameter.DotCounts);
				mGluePressure = BitConverter.GetBytes(parameter.GluePressure);
				mJetPressure = BitConverter.GetBytes(parameter.JetPressure);
				mPulseTime = BitConverter.GetBytes(parameter.PulseTime);
				mOpenTime = BitConverter.GetBytes(parameter.OpenTime);
				mCloseTime = BitConverter.GetBytes(parameter.CloseTime);
				mCloseVoltage = BitConverter.GetBytes(parameter.CloseVoltage);
				mStroke = BitConverter.GetBytes(parameter.Stroke);

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("T", 0));
				mArrayCounts = 1;
				//ZoneNumber
				for (mI = 0; mI <= mZoneNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mZoneNo[mI];
				}
				mArrayCounts = mArrayCounts + mZoneNo.Length;
				//CycleTime
				for (mI = 0; mI <= mCycleTime.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mCycleTime[mI];
				}
				mArrayCounts = mArrayCounts + mCycleTime.Length;
				//Dots
				for (mI = 0; mI <= mDotCounts.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mDotCounts[mI];
				}
				mArrayCounts = mArrayCounts + mDotCounts.Length;
				//GluePressure
				for (mI = 0; mI <= mGluePressure.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mGluePressure[mI];
				}
				mArrayCounts = mArrayCounts + mGluePressure.Length;
				//JetPressure
				for (mI = 0; mI <= mJetPressure.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mJetPressure[mI];
				}
				mArrayCounts = mArrayCounts + mJetPressure.Length;
				//PulseTime
				for (mI = 0; mI <= mPulseTime.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mPulseTime[mI];
				}
				mArrayCounts = mArrayCounts + mPulseTime.Length;

				//OpenTime/CloseTime
				mSendArray[mArrayCounts] = mCloseTime[0];
				mSendArray[mArrayCounts + 1] = mCloseTime[1];
				mSendArray[mArrayCounts + 2] = mOpenTime[0];
				mSendArray[mArrayCounts + 3] = mOpenTime[1];
				mArrayCounts = mArrayCounts + 4;

				//Stroke/CloseVoltage
				mSendArray[mArrayCounts] = mCloseVoltage[0];
				mSendArray[mArrayCounts + 1] = mCloseVoltage[1];
				mSendArray[mArrayCounts + 2] = mStroke[0];
				mSendArray[mArrayCounts + 3] = mStroke[1];
				mArrayCounts = mArrayCounts + 4;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mCycleRecipe.Status = false;
					}
					return mCycleRecipe.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[固定間距打點 P Command(Pitch)]</summary>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetPitchRecipe(MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false)
		{
			try {
				int mI = 0;
				byte[] mZoneNo = new byte[4];
				byte[] mPitch = new byte[4];
				byte[] mDotCounts = new byte[4];
				byte[] mGluePressure = new byte[4];
				byte[] mJetPressure = new byte[4];
				byte[] mPulseTime = new byte[4];
				byte[] mOpenTime = new byte[4];
				byte[] mCloseTime = new byte[4];
				byte[] mCloseVoltage = new byte[4];
				byte[] mStroke = new byte[4];
				byte[] mSendArray = new byte[33];
				int mArrayCounts = 0;

				//[Note]: "P" + ZoneNo + Pitch + Dots + GluePressure + JetPressure + PulseTime + (CloseTime/OpenTime) + (CloseVoltage/Stroke)
				//8*4+1=33

				mZoneNo = BitConverter.GetBytes(Convert.ToInt32(0));
				mPitch = BitConverter.GetBytes(parameter.Pitch);
				mDotCounts = BitConverter.GetBytes(parameter.DotCounts);
				mGluePressure = BitConverter.GetBytes(parameter.GluePressure);
				mJetPressure = BitConverter.GetBytes(parameter.JetPressure);
				mPulseTime = BitConverter.GetBytes(parameter.PulseTime);
				mOpenTime = BitConverter.GetBytes(parameter.OpenTime);
				mCloseTime = BitConverter.GetBytes(parameter.CloseTime);
				mCloseVoltage = BitConverter.GetBytes(parameter.CloseVoltage);
				mStroke = BitConverter.GetBytes(parameter.Stroke);

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("P", 0));
				mArrayCounts = 1;
				//ZoneNumber
				for (mI = 0; mI <= mZoneNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mZoneNo[mI];
				}
				mArrayCounts = mArrayCounts + mZoneNo.Length;
				//Pitch
				for (mI = 0; mI <= mPitch.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mPitch[mI];
				}
				mArrayCounts = mArrayCounts + mPitch.Length;
				//Dots
				for (mI = 0; mI <= mDotCounts.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mDotCounts[mI];
				}
				mArrayCounts = mArrayCounts + mDotCounts.Length;
				//GluePressure
				for (mI = 0; mI <= mGluePressure.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mGluePressure[mI];
				}
				mArrayCounts = mArrayCounts + mGluePressure.Length;
				//JetPressure
				for (mI = 0; mI <= mJetPressure.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mJetPressure[mI];
				}
				mArrayCounts = mArrayCounts + mJetPressure.Length;
				//PulseTime
				for (mI = 0; mI <= mPulseTime.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mPulseTime[mI];
				}
				mArrayCounts = mArrayCounts + mPulseTime.Length;

				//OpenTime/CloseTime
				mSendArray[mArrayCounts] = mCloseTime[0];
				mSendArray[mArrayCounts + 1] = mCloseTime[1];
				mSendArray[mArrayCounts + 2] = mOpenTime[0];
				mSendArray[mArrayCounts + 3] = mOpenTime[1];
				mArrayCounts = mArrayCounts + 4;

				//Stroke/CloseVoltage
				mSendArray[mArrayCounts] = mCloseVoltage[0];
				mSendArray[mArrayCounts + 1] = mCloseVoltage[1];
				mSendArray[mArrayCounts + 2] = mStroke[0];
				mSendArray[mArrayCounts + 3] = mStroke[1];
				mArrayCounts = mArrayCounts + 4;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mPitchRecipe.Status = false;
					}
					return mPitchRecipe.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[Dummy Run (Auto Tune)]</summary>
		/// <param name="dispType"></param>
		/// <param name="valveNo"></param>
		/// <param name="zoneNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetDummyRun(MCommonTriggerBoard.enmTriggerDispType dispType, eValveWorkMode valveNo, int zoneNo, bool waitReturn = false)
		{
			try {
				int mI = 0;
				byte[] mZoneNo = new byte[4];
				byte[] mHeadNo = new byte[4];
				byte[] mSpare = new byte[4];
				byte[] mSendArray = new byte[22];
				int mArrayCounts = 0;

				//[Note]: "D" + (J、F、L) + ZoneNo + HeadNo + Spare + Spare + Spare 
				//5*4+2=22
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mZoneNo = BitConverter.GetBytes(zoneNo);
				mHeadNo = BitConverter.GetBytes(Convert.ToInt32(valveNo));
				mSpare = BitConverter.GetBytes(Convert.ToInt32(0));
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("D", 0));
				mArrayCounts = 1;
				switch (dispType) {
                    case MCommonTriggerBoard.enmTriggerDispType.JetParamRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("J", 0));

						break;
                    case MCommonTriggerBoard.enmTriggerDispType.JetRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("F", 0));

						break;
                    case MCommonTriggerBoard.enmTriggerDispType.VisionRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("L", 0));

						break;
					default:
						//[命令錯誤]
						return false;
				}
				mArrayCounts = 2;

				//ZoneNumber
				for (mI = 0; mI <= mZoneNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mZoneNo[mI];
				}
				mArrayCounts = mArrayCounts + mZoneNo.Length;
				//HeadNo
				for (mI = 0; mI <= mHeadNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mHeadNo[mI];
				}
				mArrayCounts = mArrayCounts + mHeadNo.Length;
				//Spare
				for (mI = 0; mI <= mSpare.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mSpare[mI];
				}
				mArrayCounts = mArrayCounts + mSpare.Length;
				//Spare
				for (mI = 0; mI <= mSpare.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mSpare[mI];
				}
				mArrayCounts = mArrayCounts + mSpare.Length;
				//Spare
				for (mI = 0; mI <= mSpare.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mSpare[mI];
				}
				mArrayCounts = mArrayCounts + mSpare.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mDummyRun.Status = false;
					}
					return mDummyRun.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[Free Type Dispensing (X Command)]</summary>
		/// <param name="dispType"></param>
		/// <param name="valveNo"></param>
		/// <param name="zoneNo"></param>
		/// <param name="degree"></param>
		/// <param name="reworkDotCounts"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
        public bool SetDispenseRun(MCommonTriggerBoard.enmTriggerDispType dispType, eValveWorkMode valveNo, int zoneNo, decimal degree, int reworkDotCounts, bool waitReturn = false)
		{
			try {
				int mI = 0;
				byte[] mZoneNo = new byte[4];
				byte[] mHeadNo = new byte[4];
				byte[] mTheta = new byte[4];
				byte[] mSpare = new byte[4];
				byte[] mReworkDotCounts = new byte[4];
				byte[] mSendArray = new byte[22];
				int mArrayCounts = 0;

				//[Note]: "X" + (J、I、A、T、P、F、L、H) + ZoneNo + HeadNo + Theta + non-Rework dot count of F mode + Spare
				//5*4+2=22
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mZoneNo = BitConverter.GetBytes(zoneNo);
				mHeadNo = BitConverter.GetBytes(Convert.ToInt32(valveNo));
				mReworkDotCounts = BitConverter.GetBytes(reworkDotCounts);
				mSpare = BitConverter.GetBytes(Convert.ToInt32(0));
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("X", 0));
				mArrayCounts = 1;
				switch (dispType) {
                    case MCommonTriggerBoard.enmTriggerDispType.JetParamRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("J", 0));
						break;
                    case MCommonTriggerBoard.enmTriggerDispType.NeedleJetParamRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("I", 0));
						break;
                    case MCommonTriggerBoard.enmTriggerDispType.AugerParamRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("A", 0));
						break;
                    case MCommonTriggerBoard.enmTriggerDispType.CycleRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("T", 0));
						break;
                    case MCommonTriggerBoard.enmTriggerDispType.PitchRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("P", 0));
						break;
                    case MCommonTriggerBoard.enmTriggerDispType.JetRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("F", 0));
						break;
                    case MCommonTriggerBoard.enmTriggerDispType.VisionRecipe:
						mSendArray[mArrayCounts] = Convert.ToByte(char.ConvertToUtf32("L", 0));
						break;
					default:
						//[命令錯誤]
						return false;
				}
				mArrayCounts = 2;

				//ZoneNumber
				for (mI = 0; mI <= mZoneNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mZoneNo[mI];
				}
				mArrayCounts = mArrayCounts + mZoneNo.Length;
				//HeadNo
				for (mI = 0; mI <= mHeadNo.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mHeadNo[mI];
				}
				mArrayCounts = mArrayCounts + mHeadNo.Length;
				//Theta
				for (mI = 0; mI <= mTheta.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mTheta[mI];
				}
				mArrayCounts = mArrayCounts + mTheta.Length;
				//ReworkDotCounts
				for (mI = 0; mI <= mReworkDotCounts.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mReworkDotCounts[mI];
				}
				mArrayCounts = mArrayCounts + mReworkDotCounts.Length;
				//Spare
				for (mI = 0; mI <= mSpare.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mSpare[mI];
				}
				mArrayCounts = mArrayCounts + mSpare.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mDispenseRun.Status = false;
					}
					return mDispenseRun.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[設定膠管壓力 (S Command)]</summary>
		/// <param name="valve"></param>
		/// <param name="value"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetPressure(eValveWorkMode valve, decimal value, bool waitReturn = false)
		{
			try {
				int mI = 0;
				int mIndex = 0;
				byte[] mValue = new byte[4];
				byte[] mSendArray = new byte[7];
				int mArrayCounts = 0;

				//[Note]: "S" + (1+Index) + value
				//1*4+1+2=7
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mIndex = Convert.ToInt32(valve);
				mValue = BitConverter.GetBytes(Convert.ToBoolean( value));
                
                mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("S", 0));
				mArrayCounts = 1;
				mSendArray[1] = 1;
				mSendArray[2] = Convert.ToByte(char.ConvertToUtf32(mIndex.ToString(),0));
				mArrayCounts = 3;
				//ZoneNumber
				for (mI = 0; mI <= mValue.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mValue[mI];
				}
				mArrayCounts = mArrayCounts + mValue.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mParameter.Status = false;
					}
					return mParameter.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[設定閥體溫度 (S Command)]</summary>
		/// <param name="valve"></param>
		/// <param name="value"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetTempture(eValveWorkMode valve, decimal value, bool waitReturn = false)
		{
			try {
				int mI = 0;
				int mIndex = 0;
				byte[] mValue = new byte[4];
				byte[] mSendArray = new byte[7];
				int mArrayCounts = 0;

				//[Note]: "S" + (0+Index) + value
				//1*4+1+2=7
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mIndex = Convert.ToInt32(valve);
				mValue = BitConverter.GetBytes(Convert.ToBoolean(value));

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("S", 0));
				mArrayCounts = 1;
				mSendArray[1] = 0;
				mSendArray[2] = Convert.ToByte(mIndex);
				mArrayCounts = 3;
				//ZoneNumber
				for (mI = 0; mI <= mValue.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mValue[mI];
				}
				mArrayCounts = mArrayCounts + mValue.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mParameter.Status = false;
					}
					return mParameter.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[設定Valve Power(Pandora)]</summary>
		/// <param name="valve"></param>
		/// <param name="powerOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetValvePower(eValveWorkMode valve, bool powerOn, bool waitReturn = false)
		{
			try {
				int mI = 0;
				int mIndex = 0;
				byte[] mValue = new byte[4];
				byte[] mSendArray = new byte[7];
				int mArrayCounts = 0;
				float mPowerValue = 0;

				//[Note]: "S" + (2+Index) + value
				//1*4+1+2=7
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mIndex = Convert.ToInt32(valve);
				if (powerOn == true) {
					//[Note]:給TriggerBoard必須是float<-->single
					mPowerValue = 99999;
					//mValue(0) = 128
					//mValue(1) = 79
					//mValue(2) = 195
					//mValue(3) = 71
				} else {
					//[Note]:給TriggerBoard必須是float<-->single
					mPowerValue = -99999;
					//mValue(0) = 128
					//mValue(1) = 79
					//mValue(2) = 195
					//mValue(3) = 199
				}
				mValue = BitConverter.GetBytes(mPowerValue);

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("S", 0));
				mSendArray[1] = 2;
				mSendArray[2] = Convert.ToByte(mIndex);
				mArrayCounts = 3;
				//Value
				for (mI = 0; mI <= mValue.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mValue[mI];
				}
				mArrayCounts = mArrayCounts + mValue.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mParameter.Status = false;
					}
					return mParameter.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[設定Purge On/Off(Pandora)]</summary>
		/// <param name="valve"></param>
		/// <param name="purgeOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetPurge(eValveWorkMode valve, bool purgeOn, bool waitReturn = false)
		{
			try {
				int mI = 0;
				int mIndex = 0;
				byte[] mValue = new byte[4];
				byte[] mSendArray = new byte[7];
				int mArrayCounts = 0;
				float mPowerValue = 0;

				//[Note]: "S" + (9+Index) + value
				//1*4+1+2=7
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mIndex = Convert.ToInt32(valve);
				if (purgeOn == true) {
					//[Note]:給TriggerBoard必須是float<-->single
					mPowerValue = 99999;
					//mValue(0) = 128
					//mValue(1) = 79
					//mValue(2) = 195
					//mValue(3) = 71
				} else {
					//[Note]:給TriggerBoard必須是float<-->single
					mPowerValue = -99999;
					//mValue(0) = 128
					//mValue(1) = 79
					//mValue(2) = 195
					//mValue(3) = 199
				}
				mValue = BitConverter.GetBytes(mPowerValue);

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("S", 0));
				mSendArray[1] = 9;
				mSendArray[2] = Convert.ToByte(mIndex);
				mArrayCounts = 3;
				//Value
				for (mI = 0; mI <= mValue.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mValue[mI];
				}
				mArrayCounts = mArrayCounts + mValue.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mParameter.Status = false;
					}
					return mParameter.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}


		//20170920
		/// <summary>[設定Tempture On/Off(Pandora)]</summary>
		/// <param name="valve"></param>
		/// <param name="purgeOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetTemptureOnOff(eValveWorkMode valve, bool TemptureOn, bool waitReturn = false)
		{
			try {
				int mI = 0;
				int mIndex = 0;
				byte[] mValue = new byte[4];
				byte[] mSendArray = new byte[7];
				int mArrayCounts = 0;
				float mPowerValue = 0;

				//[Note]: "S" + (9+Index) + value
				//1*4+1+2=7
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mIndex = Convert.ToInt32(valve);
				if (TemptureOn == true) {
					//[Note]:給TriggerBoard必須是float<-->single
					mPowerValue = 99999;
					//mValue(0) = 128
					//mValue(1) = 79
					//mValue(2) = 195
					//mValue(3) = 71
				} else {
					//[Note]:給TriggerBoard必須是float<-->single
					mPowerValue = -99999;
					//mValue(0) = 128
					//mValue(1) = 79
					//mValue(2) = 195
					//mValue(3) = 199
				}
				mValue = BitConverter.GetBytes(mPowerValue);

				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("S", 0));
				mSendArray[1] = 3;
				mSendArray[2] = Convert.ToByte(mIndex);
				mArrayCounts = 3;
				//Value
				for (mI = 0; mI <= mValue.Length - 1; mI++) {
					mSendArray[mArrayCounts + mI] = mValue[mI];
				}
				mArrayCounts = mArrayCounts + mValue.Length;

				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mParameter.Status = false;
					}
					return mParameter.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[清除Alarm (c Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool SetResetAlarm(bool waitReturn = false)
		{
			try {
				byte[] mSendArray = new byte[1];

				//[Note]: "c" 
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("c", 0));
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mResetAlarm.Status = false;
					}
					return mResetAlarm.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[詢問打點數]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="dotCounts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetDispenseCounts(bool waitReturn , ref long dotCounts )
		{
			try {
				byte[] mSendArray = new byte[1];

				//[Note]: "C"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("C", 0));
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mDispenseCounts.Status = false;
					}
					if (Information.IsNumeric(mDispenseCounts.Value)) {
						dotCounts = Convert.ToInt64(mDispenseCounts.Value);
					} else {
						dotCounts = 0;
					}
					return mDispenseCounts.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}


		public bool GetVisionCounts(bool waitReturn , ref long dotCounts )
		{
			try {
				byte[] mSendArray = new byte[1];

				//[Note]: "O"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("O", 0));
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mVisionCounts.Status = false;
					}
					if (Information.IsNumeric(mDispenseCounts.Value)) {
						dotCounts = Convert.ToInt64(mVisionCounts.Value);
					} else {
						dotCounts = 0;
					}
					return mDispenseCounts.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[詢問韌體版本 (V Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetVersion(bool waitReturn , ref string version )
		{
			try {
				byte[] mSendArray = new byte[1];

				//[Note]: "V"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("V", 0));
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mVersion.Status = false;
					}
					version = mVersion.Value;
					return mVersion.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[詢問異常代碼 (E Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="errorCode"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetErrorCode(bool waitReturn , ref string errorCode )
		{
			try {
				byte[] mSendArray = new byte[1];

				//[Note]: "E"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("E", 0));
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mErrorCode.Status = false;
					}
					errorCode = mErrorCode.Value;
					return mErrorCode.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		/// <summary>[詢問真實點膠之Cycle]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="cycleArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetDispCycle(bool waitReturn , ref string cycleArray )
		{
			try {
				byte[] mSendArray = new byte[1];

				//[Note]: "B"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("B", 0));
				SendCommandToSerialPort(mSendArray);

				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mCycleArray.Status = false;
					}
					cycleArray = mCycleArray.Value;
					return mCycleArray.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		//20171010
		/// <summary>[詢問閥體溫度]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="tempArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetTemp(bool waitReturn , ref string tempArray )
		{
			try {
				byte[] mSendArray = new byte[2];

				//[Note]: "R"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				//return Temperature(ASCII):R 0,Head0 heater,Head1 heater,Head0 piezo,Head1 piezo
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("R", 0));
				mSendArray[1] = 0;

				SendCommandToSerialPort(mSendArray);
				//Debug.Print("RS232 Temp Cmd")
				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mTemperature.Status = false;
					}
					tempArray = mTemperature.Value;
					return mTemperature.Status;
				} else {
					return true;
				}

			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}
		bool ITriggerBoard.GetTemperature(bool waitReturn , ref string tempArray )
		{
			return GetTemp(waitReturn, ref tempArray);
		}

		//20171010
		/// <summary>[詢問目前開關狀態]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="tempArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetSwitch(bool waitReturn , ref string tempArray )
		{
			try {
				byte[] mSendArray = new byte[2];

				//[Note]: "R"
				//1
				//暫且不取Valve的代號-->目前都是一對一，所以HeadNo都是為0，等改變格式再來改(多一層軸，目前只有卡一層)
				//return HV#0,HV#1,Heater#0,Heater#1,purge#0,purge#1,Glue#0,Glue#1,Jet#0,Jet#1
				mSendArray[0] = Convert.ToByte(char.ConvertToUtf32("R", 0));
				mSendArray[1] = 2;

				SendCommandToSerialPort(mSendArray);
				//Debug.Print("RS232 Temp Cmd")
				if (waitReturn == true) {
					while ((IsBusy == true)) {
						System.Threading.Thread.CurrentThread.Join(1);
						if (IsTimeOut == true) {
							break; // TODO: might not be correct. Was : Exit Do
						}
					}

					if (IsTimeOut == true) {
						mTemperature.Status = false;
					}
					tempArray = mTemperature.Value;
					return mTemperature.Status;
				} else {
					return true;
				}


			} catch (Exception ex) {
				MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1015004), "Error_1015004", eMessageLevel.Error);
				MDateLog.gSyslog.Save("Exception Message:" + ex.Message,"" , eMessageLevel.Error);
				ErrMsg = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mStopWatch.Stop();
				return false;
			}
		}

		#endregion


	}
}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using ProjectCore;
using System.IO.Ports;
using System.Text;
namespace Premtek.Base
{

	/// <summary>使用Keyencec原廠RS232通訊模組</summary>
	/// <remarks></remarks>
	public class CLaserReader_DLRS1A : ILaserReader
	{

		/// <summary>通訊序列埠</summary>
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
		/// <summary>逾時計時器</summary>
		/// <remarks></remarks>

		private Stopwatch mTimeOutStopWatch = new Stopwatch();
		/// <summary>[傳送指令] </summary>
		/// <remarks></remarks>

		private byte[] SendCmd;
        /// <summary>接收資料結果</summary>
        /// <remarks></remarks>
        MCommonDefine.sReceiveStatus[] mResult = new MCommonDefine.sReceiveStatus[4];
		/// <summary>接收資料結果</summary>
		/// <param name="channelNo"></param>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        public MCommonDefine.sReceiveStatus Result(int channelNo)
        {
            return mResult[channelNo];
        }       
		/// <summary>[忙碌中]</summary>
		/// <remarks></remarks>
		bool mIsBusy;
		/// <summary>忙碌中</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool IsBusy {
			get { return mIsBusy; }
		}

		/// <summary>
		/// TimeOut(逾時)
		/// </summary>
		/// <remarks></remarks>
		bool mIsTimeOut;
		public bool IsTimeOut {
			get {
				try {
					if ((mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer)) {
						mIsBusy = false;
						mIsTimeOut = true;
						mSerialPort.DiscardInBuffer();
						mTimeOutStopWatch.Stop();
					}
					return mIsTimeOut;

				} catch (Exception ex) {
					mResult[0].STR = ex.ToString();
					if ((mTimeOutStopWatch.ElapsedMilliseconds >= mTimeoutTimer)) {
						mIsBusy = false;
						mIsTimeOut = true;
						mTimeOutStopWatch.Stop();
					}
					return mIsTimeOut;
				}
			}
		}

		/// <summary>
		/// 設定Timeout時間
		/// </summary>
		/// <remarks></remarks>
		private int mTimeoutTimer = 4000;
		public int TimeoutTimer {
			get { return mTimeoutTimer; }
			set { mTimeoutTimer = value; }
		}

		/// <summary>
		/// Port Is Open
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool PortIsOpen {
			get { return mSerialPort.IsOpen; }
		}

        MCommonDefine.sReceiveStatus ILaserReader.Result
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>更換Recipe</summary>
        /// <param name="ProgramID"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ChangeProgram(int ProgramID)
		{
			Debug.Print("");
			return true;
		}

		/// <summary>關閉連線</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Close()
		{
			return true;
		}



		/// <summary>
		/// ComPort Initial
		/// </summary>
		/// <param name="PortName"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Initial(string PortName, string BaudRate)
		{

			bool IsPortExist = false;
			//[確認Com Port 是否存在]

			try {
				
				mSerialPort.PortName = PortName;
				//[連線方式]
				mSerialPort.BaudRate = Convert.ToInt32(BaudRate);
				//[每秒傳輸位元] 出廠預設值9600
				mSerialPort.Parity = Parity.None;
				//[同位檢查] '出廠預設值None
				mSerialPort.DataBits = 8;
				//[資料位元] '出廠預設值8
				mSerialPort.StopBits = StopBits.One;
				//[停止位元] '出廠預設值1
				mSerialPort.NewLine = "\r\n";
				mSerialPort.Encoding = System.Text.Encoding.ASCII;
				mSerialPort.Handshake = Handshake.None;
				//.Handshake = IO.Ports.Handshake.None                '[流量控制]
				//.Encoding = Encoding.ASCII                          '[資料的編碼格式]
				//.RtsEnable = True
				//.ReceivedBytesThreshold = 1
				//.NewLine = "\r\n"

				mIsBusy = false;
				mIsTimeOut = false;

				IsPortExist = false;
				foreach (string GetPortName in SerialPort.GetPortNames()) {
					if (mSerialPort.PortName == GetPortName) {
						IsPortExist = true;
						break; // TODO: might not be correct. Was : Exit For
					}
				}

				if (mSerialPort.IsOpen == true) {
					mSerialPort.Close();
					return false;
				} else {
					if (IsPortExist == true) {
						mSerialPort.Open();
						return true;
					} else {
						return false;
					}
				}

			} catch (Exception ex) {
				mResult[0].STR = ex.ToString();
				mIsBusy = false;
				//[說明:少加這樣項，造成若一開始就沒這個Port，卻還不跳TimeOut
				mIsTimeOut = true;
				return false;
			}
		}


		public bool EthernetOpen(string IP, int Port)
		{
			MDateLog.gSyslog.Save("EthernetOpen Function is Not Supported at CLaserReader_DLRS1A.");
			return false;
		}

		public bool GetValue(string Mode, ref string value, int aiIndex = 0, bool waitReturn = false)
		{

			string cmd = null;

			if (Mode.Equals("Contact")) {
				cmd = "SR,00,002\r\n" ;
			} else {
                cmd = "SR,00,037\r\n";
			}

			//Dim cmd As String = "SR,00,002" & vbCrLf
			byte[] bytCmd = new byte[cmd.Length + 1];
			bool ret = false;
			ConverterValue(cmd, ref bytCmd);
			ret = SendCommandToSerialPort(bytCmd, waitReturn);
			if (waitReturn) {
				//取得結果
				if (mResult[0].Status) {
					value = mResult[0].Value;
					decimal mDec = default(decimal);

					if (decimal.TryParse(value, out mDec)) {
						value = Math.Round(mDec, 3).ToString();
					}

				}
			}
			return ret;
		}
		private bool ConverterValue(string data, ref byte[] bytCmd)
		{
			char[] chr = data.ToArray();
			bytCmd = new byte[data.Count()];
			for (int i = 0; i <= data.Count() - 1; i++)
            {
				bytCmd[i] = (byte)Strings.Asc(chr[i]);
				//Debug.Print("bytCmd(" & i & ")" & bytCmd(i))
			}

			return true;
		}

		public bool GetVersion(ref string Version)
		{
			Version = "DL-RS1A";
			return true;
		}

		public bool RebootController()
		{
			return true;
		}

		/// <summary>
		/// Send Command
		/// </summary>
		/// <param name="CommandBtye"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private bool SendCommandToSerialPort(byte[] CommandBtye, bool waitReturn = false)
		{
			try {
				SendCmd = CommandBtye;
				if (!mSerialPort.IsOpen) {
					return false;
				}

				mIsBusy = true;
				mIsTimeOut = false;

				mTimeOutStopWatch.Restart();
				mResult[0].Status = false;
				mSerialPort.Write(SendCmd, 0, SendCmd.Length);
				//Debug.Print("TriggerBoard Cmd:" & Chr(CommandBtye(0)))
				if (waitReturn == false) {
					return true;
				}

				do {
					System.Threading.Thread.CurrentThread.Join(1);
					if (mResult[0].Status == true) {
						break; // TODO: might not be correct. Was : Exit Do
					} else if (IsTimeOut) {
						mResult[0].Status = false;
						return false;
					}
				} while (true);
				mTimeOutStopWatch.Stop();
				return true;


			} catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1016003), "Error_1016003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message" + ex.Message,"", eMessageLevel.Error);
				mResult[0].Status = false;
				mResult[0].STR = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				return false;
			}
		}



		/// <summary>
		/// Data Received
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>

		public void mSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			//Dim SerialTemp As SerialPort
			//Dim intReadByte As Integer
			//Dim intTemp As Integer

			//Dim intI As Integer

			//Dim DataBuffer(1023) As Byte
			//Dim length As Integer

			string cmd = null;
			string IdNo = null;
			string DataNo = null;
			string Data = null;

			try {
				mResult[0].STR = mSerialPort.ReadLine();

				string[] SplitedData = mResult[0].STR.Split(',');
				if (SplitedData.Length >= 1) {
					cmd = SplitedData[0];
				} else {
					cmd = "";
				}

				if (SplitedData.Length >= 2) {
					IdNo = SplitedData[1];
					//同一個通訊埠有多個元件
				} else {
					IdNo = "";
				}

				if (SplitedData.Length >= 3) {
					DataNo = SplitedData[2];
					//所屬資料編號
				} else {
					DataNo = "";
				}

				if (SplitedData.Length >= 4) {
					Data = SplitedData[3];
					//實際資料內容
				} else {
					Data = "";
				}

				//讀取
				if (cmd == "SR") {
					//主站
					if (IdNo == "00") {
						//實際值
						if (DataNo == "037" | DataNo=="002") {
							mResult[0].Value = Data;
							mResult[0].Status = true;
							mTimeOutStopWatch.Stop();
							mIsBusy = false;
						}
					}
				}

			} catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1016004), "Error_1016004", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message" + ex.Message,"" , eMessageLevel.Error);
				mResult[0].Status = false;
				mResult[0].STR = ex.ToString();
				mIsBusy = false;
				mIsTimeOut = true;
				mTimeOutStopWatch.Stop();
			}
		}


	}
}

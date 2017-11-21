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

namespace Premtek.Base
{

	/// <summary>原CLaserInterferometer</summary>
	/// <remarks></remarks>
	public class CLaserReader_KeyenceLJV7060_TCP : ILaserReader
	{

		/// <summary>[雷射干涉儀資料陣列大小]</summary>
		/// <remarks></remarks>
		const int gLaserInterferometerUBound = 4;
		/// <summary>[Ethernet settings structure] </summary>
		/// <remarks></remarks>

		private LJV7IF_ETHERNET_CONFIG mEthernetConfig;
		/// <summary>[Device ID (fixed to 0)]</summary>
		/// <remarks></remarks>

		private const  int mDeviceID = 0;



		bool mIsOpen;
		/// <summary>[Ethernet Open]</summary>
		/// <param name="IP"></param>
		/// <param name="Port"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool EthernetOpen(string IP, int Port)
		{

			try {
				Rc Status = Rc.Ok;

				//[說明]:Initialize the DLL
				Status = (Rc)NativeMethods.LJV7IF_Initialize();

				if (!(Status == Rc.Ok)) {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014000), "Error_1014000", eMessageLevel.Error);
					mResult[0].Status = false;
					mResult[0].Value = MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014000);
					//"Initialize the DLL Error"
					mIsOpen = false;
					return false;
				}

				//Return True

				//Dim Status As Rc = Rc.Ok
				string[] IPstring = null;

				//[說明]:IPstring
                IPstring = IP.Split('.');
				if (IPstring.GetUpperBound(0) != 3) {
					mResult[0].Status = false;
					mResult[0].Value = MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014003);
                    //"IP Error"
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014003), "Error_1014003", eMessageLevel.Error);
					mIsOpen = false;
					return false;
				}

				//[說明]:Open the communication path
				//[說明]:USB
				//       NativeMethods.LJV7IF_UsbOpen(Define.DEVICE_ID)

				//[說明]:Generate the settings for Ethernet communication.
				
                mEthernetConfig.abyIpAddress = new byte[] {
					Convert.ToByte(IPstring[0]),
					Convert.ToByte(IPstring[1]),
					Convert.ToByte(IPstring[2]),
					Convert.ToByte(IPstring[3])
				};
				mEthernetConfig.wPortNo = Convert.ToUInt16(Port);

				Status = (Rc)NativeMethods.LJV7IF_EthernetOpen(mDeviceID, ref mEthernetConfig);

				if (!(Status == Rc.Ok)) {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014000), "Error_1014000", eMessageLevel.Error);
					mResult[0].Status = false;
					mResult[0].Value = MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014000);
					//"Ethernet Open Error"
					mIsOpen = false;
					return false;
				}

				mIsOpen = true;
				return true;

			} catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014000), "Error_1014000", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				mResult[0].Status = false;
				mResult[0].STR = ex.ToString();
				mIsOpen = false;
				return false;
			}

		}

		/// <summary>
		/// [Finalize the DLL and Close the communication]
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool Close()
		{

			try {
				Rc Status = Rc.Ok;

				//[說明]:Close the communication
				Status = (Rc)NativeMethods.LJV7IF_CommClose(mDeviceID);
				if (!(Status == Rc.Ok)) {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014001), "Error_1014001", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Close the communication Error","" , eMessageLevel.Error);
					mResult[0].Status = false;
					mResult[0].Value = "Close the communication Error";
					return false;
				}

				//[說明]:Finalize the DLL
				Status = (Rc)NativeMethods.LJV7IF_Finalize();
				if (!(Status == Rc.Ok)) {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014001), "Error_1014001", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Finalize the DLL Error","", eMessageLevel.Error);
					mResult[0].Status = false;
					mResult[0].Value = "Finalize the DLL Error";
					return false;
				}

				return true;

			} catch (Exception ex) {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014001), "Error_1014001", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message,"" , eMessageLevel.Error);
				mResult[0].Status = false;
				mResult[0].STR = ex.ToString();

				return false;
			}

		}

		/// <summary>讀取測高值</summary>
		/// <param name="bit">如一對多時,指定Port</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetValue(string Mode, ref string value, int bit = 0, bool waitReturn = false)
		{
			//Dim value As String '傳回資料
			int intI = 0;
			double[] Data = new double[gLaserInterferometerUBound + 1];
			Rc Status = Rc.Ok;
			LJV7IF_MEASURE_DATA[] measureData = new LJV7IF_MEASURE_DATA[NativeMethods.MeasurementDataCount + 1];

			//[說明]:讀取資料
			Status = (Rc)NativeMethods.LJV7IF_GetMeasurementValue(mDeviceID, measureData);
			if (!(Status == Rc.Ok)) {
				value = "";
                //讀取失敗,無資料
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1014004), "Error_1014004", eMessageLevel.Error);
				//雷射干涉儀1讀值失敗!
				return false;
				//Return False
			}

			//[說明]:
			value = "";
			for (intI = 0; intI <= gLaserInterferometerUBound; intI++) {
				Data[intI] = Convert.ToDouble(measureData[intI].fValue);
				value += Data[intI].ToString() + "|";
			}

			return true;

		}

		/// <summary>
		/// [GetVersion]
		/// </summary>
		/// <param name="Version"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool GetVersion(ref string Version)
		{

			Version = NativeMethods.LJV7IF_GetVersion().ToString("x");
			return true;

		}

		/// <summary>
		/// [RebootController]
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool RebootController()
		{

			Rc Status = Rc.Ok;

			Status = (Rc)NativeMethods.LJV7IF_RebootController(mDeviceID);

			if (!(Status == Rc.Ok)) {
				return false;
			}

			return true;

		}

		/// <summary>
		/// [切換干涉儀Program] 
		/// </summary>
		/// <param name="ProgramID"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool ChangeProgram(int ProgramID)
		{

			Rc Status = Rc.Ok;

			Status = (Rc)NativeMethods.LJV7IF_ChangeActiveProgram(mDeviceID, Convert.ToByte(ProgramID));

			if (!(Status == Rc.Ok)) {
				return false;
			}

			return true;

		}


		public bool IsBusy {
			get { return false; }
		}

		public bool IsTimeOut {
			get { return false; }
		}

		public bool PortIsOpen {
			get { return mIsOpen; }
		}

		public int TimeoutTimer { get; set; }

       MCommonDefine.sReceiveStatus ILaserReader.Result
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Initial(string PortName, string BaudRate)
		{
		MDateLog.gSyslog.Save("Initial Function is Not Supported at CLaserReader_KeyenceLJV7060_TCP.");
			return false;
		}

        //Public Sub ResetState() Implements ILaserReader.ResetState

        //End Sub

        /// <summary>接收資料結果</summary>
        /// <remarks></remarks>
        MCommonDefine.sReceiveStatus[] mResult = new MCommonDefine.sReceiveStatus[4];
        public MCommonDefine.sReceiveStatus Result(int channelNo)
        {
			 return mResult[channelNo]; 
		}
	}
}

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
using Premtek.Base;
using Premtek;

namespace Premtek.Base
{
	public class CTriggerBoard_Virtual : ITriggerBoard
	{

        public MCommonTriggerBoard.sReceiveStatus CycleArray
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus CycleRecipe
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus DispenseRun
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus DispenseCounts
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "0";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus VisionCounts
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "0";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus DummyRun
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus ErrorCode
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus JetParameter
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus JetParamRecipe
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus VisionRecipe
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus JetRecipe
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus JetRecipeUseTransmissionResuming
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus PitchRecipe
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus ResetAlarm
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus Parameter
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus Version
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = "";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

        public MCommonTriggerBoard.sReceiveStatus Temperature
        {
			get {
                MCommonTriggerBoard.sReceiveStatus mStringStatus = default(MCommonTriggerBoard.sReceiveStatus);
				mStringStatus.STR = ",,,";
				mStringStatus.Status = true;
				mStringStatus.Value = "";
				return mStringStatus;
			}
		}

		public string ErrMsg { get; set; }
		public int TimeoutTimes { get; set; }

		public bool PortIsOpen {
			get { return true; }
		}

		public bool IsBusy {
			get { return false; }
		}

		public bool IsInitialOK {
			get { return true; }
		}

		public int TransmissionResumingOfStepCount {
			get { return 20; }
		}

		public bool IsTimeOut {
			get { return false; }
		}

		public bool GetPortIDs(ref string[] portIDs)
		{
			return true;
		}


		public void Dispose()
		{
		}

		public void Close()
		{
		}

		public bool Initial(string portName, string baudRate)
		{
			return true;
		}

		public bool SendCommandToSerialPort(byte[] commandBtye)
		{
			return true;
		}

        public bool AddJetParamRecipe(bool is1stStep, int zoneNo, MCommonTriggerBoard.sTriggerJCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerJCmdParam parameter)
		{
			return true;
		}

		public bool SetJetParamRecipe(bool waitReturn = false)
		{
			return true;
		}

        public bool AddJetRecipe(bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter)
		{
			return true;
		}

		public bool SetJetRecipe(bool waitReturn = false)
		{
			return true;
		}
        public bool AddJetRecipeUseTransmissionResuming(bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter)
		{
			return true;
		}

		public bool SetJetRecipeByTransmissionResuming(bool waitReturn = false)
		{
			return true;
		}
        public bool SetJetParameter(MCommonTriggerBoard.sTriggerGCmdParam parameter, bool waitReturn = false)
		{
			return true;
		}

        public bool SetCycleRecipe(MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false)
		{
			return true;
		}

        public bool SetPitchRecipe(MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false)
		{
			return true;
		}

        public bool SetDummyRun(MCommonTriggerBoard.enmTriggerDispType dispType, eValveWorkMode valveNo, int zoneNo, bool waitReturn = false)
		{
			return true;
		}

        public bool SetDispenseRun(MCommonTriggerBoard.enmTriggerDispType dispType, eValveWorkMode valveNo, int zoneNo, decimal degree, int reworkDotCounts, bool waitReturn = false)
		{
			return true;
		}

		public bool SetPressure(eValveWorkMode valve, decimal value, bool waitReturn = false)
		{
			return true;
		}

		public bool SetTempture(eValveWorkMode valve, decimal value, bool waitReturn = false)
		{
			return true;
		}

		public bool SetValvePower(eValveWorkMode valve, bool powerOn, bool waitReturn = false)
		{
			return true;
		}

		public bool SetPurge(eValveWorkMode valve, bool purgeOn, bool waitReturn = false)
		{
			return true;
		}

		//20170920
		public bool SetTemptureOnOff(eValveWorkMode valve, bool TemptureOn, bool waitReturn = false)
		{
			return true;
		}

		public bool SetResetAlarm(bool waitReturn = false)
		{
			return true;
		}

		public bool GetDispCycle(bool waitReturn , ref string cycleArray )
		{
			cycleArray = "";
			return true;
		}

		public bool GetDispenseCounts(bool waitReturn , ref long dotCounts )
		{
			dotCounts = 0;
			return true;
		}

		public bool GetVisionCounts(bool waitReturn , ref long dotCounts )
		{
			dotCounts = 0;
			return true;
		}

		public bool GetErrorCode(bool waitReturn , ref string errorCode )
		{
			errorCode = "";
			return true;
		}

		public bool GetVersion(bool waitReturn , ref string version )
		{
			version = "";
			return true;
		}

        public bool AddVisionRecipe(bool is1stStep, MCommonTriggerBoard.sTriggerVisionCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerVisionCmdParam parameter)
		{
			return true;
		}

		public bool SetVisionRecipe(bool waitReturn = false)
		{
			return true;
		}

		public bool GetTemperature(bool waitReturn , ref string tempArray )
		{
			return true;
		}

		//20171010
		public bool GetSwitch(bool waitReturn , ref string tempArray )
		{
			return true;
		}

	}
}

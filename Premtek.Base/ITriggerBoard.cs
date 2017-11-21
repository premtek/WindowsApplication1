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
using Premtek;

namespace Premtek.Base
{

	public interface ITriggerBoard
	{

		#region "Definitions"
		/// <summary>[Error Message]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		string ErrMsg { get; set; }
		/// <summary>[忙碌中]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		bool IsBusy { get; }
		/// <summary>TimeOut(逾時)</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		bool IsTimeOut { get; }
		/// <summary>設定Timeout時間</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		int TimeoutTimes { get; set; }
		/// <summary>[Is Port Open?]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		bool PortIsOpen { get; }
		/// <summary>[是否初始化成功]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		bool IsInitialOK { get; }
		/// <summary>[f Command的續傳量(F Command資料續傳-->f Command)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		int TransmissionResumingOfStepCount { get; }
		/// <summary>[J Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>

        MCommonTriggerBoard.sReceiveStatus JetParamRecipe { get; }
		/// <summary>[G Command之回傳字串(將參數丟給Trigger Board)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus JetParameter { get; }
		/// <summary>[L Command之回傳字串(將Vision取像座標資料丟給Trigger Board)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus VisionRecipe { get; }
		/// <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus JetRecipe { get; }
		/// <summary>[f Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)(F Command資料續傳-->f Command)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus JetRecipeUseTransmissionResuming { get; }
		/// <summary>[T Command之回傳字串(固定頻率打點)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus CycleRecipe { get; }
		/// <summary>[P Command之回傳字串(固定間距打點)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus PitchRecipe { get; }
		/// <summary>[X Command之回傳字串(Dispensing Run)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus DispenseRun { get; }
		/// <summary>[D Command之回傳字串(Dummy Run)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus DummyRun { get; }
		/// <summary>[S Command之回傳字串(閥體溫度、膠管壓力、閥體電源開關)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus Parameter { get; }
		/// <summary>[C Command之回傳字串(打點數)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus DispenseCounts { get; }
		/// <summary>[O Command之回傳字串(打點數)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus VisionCounts { get; }
		/// <summary>[V Command之回傳字串(韌體版本)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus Version { get; }
		/// <summary>[B Command之回傳字串(點膠真實Cycle)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus CycleArray { get; }
		/// <summary>[E Command之回傳字串(異常代號)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus ErrorCode { get; }
		/// <summary>[c Command之回傳字串(異常清除)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
        MCommonTriggerBoard.sReceiveStatus ResetAlarm { get; }
		/// <summary>[R Command之回傳字串(閥體溫度)]</summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>

        MCommonTriggerBoard.sReceiveStatus Temperature { get; }

		#endregion

		#region "Properties"

		void Dispose();
		/// <summary>[ComPort Initial]</summary>
		/// <param name="PortName"></param>
		/// <param name="BaudRate"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool Initial(string portName, string baudRate);

		/// <summary>[關閉ComPort]</summary>
		/// <remarks></remarks>

		void Close();
		/// <summary>[Send Command]</summary>
		/// <param name="CommandBtye"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SendCommandToSerialPort(byte[] commandBtye);

		#endregion

		#region "Function"
		/// <summary>[取得目前電腦的序列埠代號]</summary>
		/// <param name="PortIDs"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetPortIDs(ref string[] portIDs);

        /// <summary>[J Command的資料串接]</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        bool AddJetParamRecipe(bool is1stStep, int zoneNo, MCommonTriggerBoard.sTriggerJCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerJCmdParam parameter );

		/// <summary>[將點膠資料丟給Trigger Board(J Command)]</summary>
		/// <param name="WaitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetJetParamRecipe(bool waitReturn = false);

		/// <summary>[Parameter of Jet(只丟參數)(G Command)]</summary>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetJetParameter(MCommonTriggerBoard.sTriggerGCmdParam parameter, bool waitReturn = false);

		/// <summary>[L Command的資料串接]</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool AddVisionRecipe(bool is1stStep, MCommonTriggerBoard.sTriggerVisionCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerVisionCmdParam parameter);

		/// <summary>[將取像座標資料丟給Trigger Board(L Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetVisionRecipe(bool waitReturn = false);

		/// <summary>[F Command的資料串接]</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool AddJetRecipe(bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter);

		/// <summary>[將點膠資料丟給Trigger Board(F Command)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetJetRecipe(bool waitReturn = false);

		/// <summary>[f Command的資料串接(F Command資料續傳-->f Command)]</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool AddJetRecipeUseTransmissionResuming(bool is1stStep, MCommonTriggerBoard.sTriggerFCmdStep patternStep, bool isLastStep, MCommonTriggerBoard.sTriggerFCmdParam parameter);

		/// <summary>[將點膠資料丟給Trigger Board(f Command-->F Command的續傳)]</summary>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetJetRecipeByTransmissionResuming(bool waitReturn = false);

		/// <summary>Dummy Run(D Command)
		/// [Note]:Recipe丟完後-->Dummy Run-->Free Type Dispensing</summary>
		/// <param name="dispType"></param>
		/// <param name="valveNo"></param>
		/// <param name="zoneNo"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetDummyRun(MCommonTriggerBoard.enmTriggerDispType dispType,Premtek.Base.eValveWorkMode valveNo, int zoneNo, bool waitReturn = false);

		/// <summary>Free Type Dispensing (X Command)
		/// [Note]:Recipe丟完後-->Dummy Run-->Free Type Dispensing</summary>
		/// <param name="dispType"></param>
		/// <param name="valveNo"></param>
		/// <param name="zoneNo"></param>
		/// <param name="degree"></param>
		/// <param name="reworkDotCounts"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetDispenseRun(MCommonTriggerBoard.enmTriggerDispType dispType,Premtek.Base.eValveWorkMode valveNo, int zoneNo, decimal degree, int reworkDotCounts, bool waitReturn = false);

		/// <summary>[固定頻率打點 T Command(Cycle Time)]</summary>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetCycleRecipe(MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false);

		/// <summary>[固定間距打點 P Command(Pitch)]</summary>
		/// <param name="parameter"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetPitchRecipe(MCommonTriggerBoard.sTriggerTPCmdParam parameter, bool waitReturn = false);

		/// <summary>[設定閥體溫度]</summary>
		/// <param name="valve"></param>
		/// <param name="value"></param>
		/// <param name="WaitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetTempture(eValveWorkMode valve, decimal value, bool waitReturn = false);

		/// <summary>[Purge開關]</summary>
		/// <param name="valve"></param>
		/// <param name="purgeOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetPurge(eValveWorkMode valve, bool purgeOn, bool waitReturn = false);


		//20170920
		/// <summary>[Tempture開關]</summary>
		/// <param name="valve"></param>
		/// <param name="TemptureOn"></param>
		/// <param name="waitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetTemptureOnOff(eValveWorkMode valve, bool TemptureOn, bool waitReturn = false);

		/// <summary>[設定膠管壓力]</summary>
		/// <param name="valve"></param>
		/// <param name="value"></param>
		/// <param name="WaitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetPressure(eValveWorkMode valve, decimal value, bool waitReturn = false);

		/// <summary>[設定開啟閥的電源]</summary>
		/// <param name="valve"></param>
		/// <param name="powerOn"></param>
		/// <param name="WaitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetValvePower(eValveWorkMode valve, bool powerOn, bool waitReturn = false);

		/// <summary>
		/// [回傳打點數]
		/// [Note]:上一次Jetting ON~OFF之間打的Dot數量(Dispense Counts)</summary>
		/// <param name="WaitReturn"></param>
		/// <param name="dotCounts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetDispenseCounts(bool waitReturn , ref long dotCounts );

		/// <summary>
		/// [回傳打點數]
		/// [Note]:上一次Jetting ON~OFF之間打的Dot數量(Vision Counts)</summary>
		/// <param name="WaitReturn"></param>
		/// <param name="dotCounts"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetVisionCounts(bool waitReturn , ref long dotCounts );

		/// <summary>[傳回韌體版本]</summary>
		/// <param name="WaitReturn"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetVersion(bool waitReturn , ref string version );

		/// <summary>[傳回點膠Cycle(真實)]</summary>
		/// <param name="WaitReturn"></param>
		/// <param name="cycleArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetDispCycle(bool waitReturn , ref string cycleArray);

		/// <summary>[傳回異常代號]</summary>
		/// <param name="WaitReturn"></param>
		/// <param name="errorCode"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetErrorCode(bool waitReturn , ref string errorCode );

		/// <summary>[異常清除]</summary>
		/// <param name="WaitReturn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool SetResetAlarm(bool waitReturn = false);

		/// <summary>[傳回閥體溫度]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="tempArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetTemperature(bool waitReturn , ref string tempArray );

		//20171010
		/// <summary>[傳回開關狀態]</summary>
		/// <param name="waitReturn"></param>
		/// <param name="tempArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetSwitch(bool waitReturn , ref string tempArray );
        //bool SetJetParameter(MCommonTriggerBoard.sTriggerGCmdParam parameter, bool waitReturn);
        #endregion

    }
}

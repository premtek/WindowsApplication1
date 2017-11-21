using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek.Base
{
	/// <summary>溫控器介面定義
	/// </summary>
    public interface ITCInterface
    {
        /// <summary>錯誤訊息</summary>
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
        
        /// <summary>[傳回值 現在溫度]</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        decimal PV { get; }
        /// <summary>[傳回值 目標溫度]</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        decimal SV { get; }
		/// <summary>溫度補償值
		/// </summary>
        decimal PVOS { get; set; }

        /// <summary>讀取資料結構</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        sReceiveStatus Result { get; }

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

		/// <summary>設定目標溫度
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="SVvalue"></param>
		/// <returns></returns>
		bool SetSV(int ID, decimal SVvalue, bool waitReturn );
		/// <summary>讀取現在溫度
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="PVValue"></param>
		/// <returns></returns>
		bool GetPV(int ID, bool waitReturn , ref decimal PVOS);
		/// <summary>設定溫度補償
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="PVOS"></param>
		/// <returns></returns>
		bool SetPVOS(int ID, decimal PVOS, bool waitReturn );
		/// <summary>讀取溫度補償
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="waitReturn"></param>
		/// <param name="PVOS"></param>
		/// <returns></returns>
		bool GetPVOS(int ID, bool waitReturn, ref decimal PVOS);


    }
}

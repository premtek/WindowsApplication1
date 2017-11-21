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

	public interface ILaserReader
	{

		/// <summary>網路連線</summary>
		/// <param name="IP"></param>
		/// <param name="Port"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool EthernetOpen(string IP, int Port);
		/// <summary>COM埠連線</summary>
		/// <param name="PortName"></param>
		/// <param name="BaudRate"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool Initial(string PortName, string BaudRate);
		/// <summary>關閉</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool Close();
		/// <summary>讀取測高值</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetValue(string Mode, ref string data, int aiIndex = 0, bool waitReturn = false);
		// ''' <summary>狀態重置</summary>
		// ''' <remarks></remarks>
		//Sub ResetState()
		/// <summary>取得版本</summary>
		/// <param name="Version"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool GetVersion(ref string Version);
		/// <summary>重啟控制器</summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool RebootController();
		/// <summary>選擇Recipe</summary>
		/// <param name="ProgramID"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		bool ChangeProgram(int ProgramID);
		bool IsTimeOut { get; }
		int TimeoutTimer { get; set; }
		bool PortIsOpen { get; }
		bool IsBusy { get; }
        /// <summary>讀取資料結構</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        MCommonDefine.sReceiveStatus Result { get; }
	}
}

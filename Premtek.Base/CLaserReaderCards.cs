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
using Premtek;

namespace Premtek.Base
{

	/// <summary>雷射干涉儀控制器連線設定</summary>
	/// <remarks></remarks>
	public class CLaserReaderCards
	{
		public int Count {
			get { return Parameters.Count; }
		}
		public List<sLaserReaderConnectionParameter> Parameters = new List<sLaserReaderConnectionParameter>();
		public sLaserReaderConnectionParameter this[int index] {
			get { return Parameters[index]; }
			set { Parameters[index] = value; }
		}

		/// <summary>儲存連線參數設定</summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public object Save(string fileName)
		{
			int CardCount = Parameters.Count;
			string mSection = "LaserReader";
			CIni.SaveIniString(mSection, "CardCount", CardCount, fileName);
			//卡片數量儲存

			for (int mCardNo = 0; mCardNo <= CardCount - 1; mCardNo++) {
				mSection = "Laser" + (mCardNo + 1).ToString() + "-Connection";
                CIni.SaveIniString(mSection, "CardType", Convert.ToInt32(Parameters[mCardNo].CardType), fileName);

                CIni.SaveIniString(mSection, "DL-RS1A-PortName", Parameters[mCardNo].DLRS1A.PortName, fileName);
                CIni.SaveIniString(mSection, "DL-RS1A-BaudRate", Parameters[mCardNo].DLRS1A.BaudRate, fileName);

                CIni.SaveIniString(mSection, "IPAddress", Parameters[mCardNo].LJV7060TCP.IP, fileName);
                CIni.SaveIniString(mSection, "Port", Parameters[mCardNo].LJV7060TCP.Port, fileName);

			}

			return true;
		}
		/// <summary>讀取連線參數設定</summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public object Load(string fileName)
		{
			string mSection = "LaserReader";
			int mCardCount = 0;
			mCardCount = Convert.ToInt32(CIni.ReadIniString(mSection, "CardCount", fileName,((int)MCommonDefine.enmLaserReader.Max + 1).ToString()));
			Parameters.Clear();

			for (int mLaserNo = 0; mLaserNo <= mCardCount - 1; mLaserNo++) {
				mSection = "Laser" + (mLaserNo + 1).ToString() + "-Connection";
				sLaserReaderConnectionParameter mItem = new sLaserReaderConnectionParameter();

                mItem.CardType = (MSystemParameter.enmLaserInterferometerType)Convert.ToInt16(CIni.ReadIniString(mSection, "CardType", fileName, 0));

                mItem.DLRS1A.PortName = CIni.ReadIniString(mSection, "DL-RS1A-PortName", fileName, "COM1");
                mItem.DLRS1A.BaudRate = CIni.ReadIniString(mSection, "DL-RS1A-BaudRate", fileName, "9600");

                mItem.LJV7060TCP.IP = CIni.ReadIniString(mSection, "IPAddress", fileName, "");
                mItem.LJV7060TCP.Port = Convert.ToInt16(CIni.ReadIniString(mSection, "Port", fileName, 0));

				Parameters.Add(mItem);
			}
			return true;
		}

	}
}

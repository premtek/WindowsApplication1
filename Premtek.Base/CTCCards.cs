using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek.Base
{
    public class CTCCards
    {
        public int Count
        {
            get { return Parameters.Count; }
        }
        public List<sTCConnectParameter> Parameters = new List<sTCConnectParameter>();
        public sTCConnectParameter this[int index]
        {
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

            for (int mCardNo = 0; mCardNo <= CardCount - 1; mCardNo++)
            {
                mSection = "Laser" + (mCardNo + 1).ToString() + "-Connection";
                CIni.SaveIniString(mSection, "CardType", Convert.ToInt32(Parameters[mCardNo].CardType), fileName);

                CIni.SaveIniString(mSection, "WT404-COM", Parameters[mCardNo].WT404.COMPort, fileName);
                CIni.SaveIniString(mSection, "WT404-Baud", Parameters[mCardNo].WT404.BaudRate, fileName);


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
            mCardCount = Convert.ToInt32(CIni.ReadIniString(mSection, "CardCount", fileName, 0));
            Parameters.Clear();

            for (int mLaserNo = 0; mLaserNo <= mCardCount - 1; mLaserNo++)
            {
                mSection = "Laser" + (mLaserNo + 1).ToString() + "-Connection";
                sTCConnectParameter mItem = new sTCConnectParameter();

                mItem.CardType = (eTCType)Convert.ToInt16(CIni.ReadIniString(mSection, "CardType", fileName, 0));
                mItem.WT404.COMPort = CIni.ReadIniString(mSection, "WT404-COM", fileName, "COM1");
                mItem.WT404.BaudRate = Convert.ToInt32(CIni.ReadIniString(mSection, "WT404-Baud", fileName, "9600"));
              

                Parameters.Add(mItem);
            }
            return true;
        }
    }
}

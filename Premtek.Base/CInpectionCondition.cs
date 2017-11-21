using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectCore;

namespace Premtek.Base
{
   

    /// <summary>[Purge、FlowRate....檢查的條件為何]</summary>
    /// <remarks></remarks>
    public class CInpectionCondition
    {
        /// <summary>外部接入系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        /// <summary>[程式重啟的時候，Timer是否重新計數]</summary>
        /// <remarks></remarks>
        public bool[] IsReset = new bool[(int)enmValve.Max + 1];
        /// <summary>[紀錄此次開始時間(程式開啟就更新了)]</summary>
        /// <remarks></remarks>
        public System.DateTime[] StartTime = new System.DateTime[(int)enmValve.Max + 1];
        /// <summary>[紀錄上次離開時已經做了多久(程式關閉時必須更新)]</summary>
        /// <remarks></remarks>
        public long[] LastTime = new long[(int)enmValve.Max + 1];
        /// <summary>[盤數(已經做了幾盤了)]</summary>
        /// <remarks></remarks>
        public int[] OnRuns = new int[(int)enmValve.Max + 1];
        /// <summary>時間(已經生產多久了)</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public long OnTimer(int valveNo)
        {
          
                if (IsReset[valveNo] == true)
                {
                    return (long)(StartTime[valveNo] - DateTime.Now).TotalSeconds;
                }
                else
                {
                    return LastTime[valveNo] + (long)(StartTime[valveNo] - DateTime.Now).TotalSeconds;//原計算式單位錯誤, 需驗證.
                }
           
        }

        /// <summary>[存檔]</summary>
        /// <param name="fileName"></param>
        /// <param name="subName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Save(string fileName, string subName)
        {

            string strSection = null;
          
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                strSection = "Valve" + (mValveNo + 1).ToString();
                CIni.SaveIniString(strSection, subName + "_IsReset" + (mValveNo + 1).ToString(), this.IsReset[mValveNo], fileName);
                CIni.SaveIniString(strSection, subName + "_StartTime" + (mValveNo + 1).ToString(), this.StartTime[mValveNo], fileName);
                CIni.SaveIniString(strSection, subName + "_LastTime" + (mValveNo + 1).ToString(), this.LastTime[mValveNo], fileName);
                CIni.SaveIniString(strSection, subName + "_OnRuns" + (mValveNo + 1).ToString(), this.OnRuns[mValveNo], fileName);
            }
            return true;

        }
        /// <summary>[讀檔]</summary>
        /// <param name="fileName"></param>
        /// <param name="subName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Load(string fileName, string subName)
        {
            string strSection = null;
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                strSection = "Valve" + (mValveNo + 1).ToString();
                bool.TryParse(CIni.ReadIniString(strSection, subName + "_IsReset" + (mValveNo + 1).ToString(), fileName, 0), out this.IsReset[mValveNo]);
                long.TryParse(CIni.ReadIniString(strSection, subName + "_LastTime" + (mValveNo + 1).ToString(), fileName, 0),out this.LastTime[mValveNo]);
                int.TryParse(CIni.ReadIniString(strSection, subName + "_OnRuns" + (mValveNo + 1).ToString(), fileName, 0), out this.OnRuns[mValveNo]);
            }
            return true;
        }

        public void ResetRuns()
        {
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                this.OnRuns[mValveNo] = 0;
            }
        }
    }

}

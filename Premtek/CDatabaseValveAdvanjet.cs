using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>氣動閥資料庫- HV2000
    /// </summary>
    public class CDatabaseValveAdvanjet
    {
        /// <summary>閥名稱
        /// </summary>
        public string Name;
        /// <summary>填充時間(msec)
        /// </summary>
        public decimal RefillTime;
        /// <summary>點膠週期(msec)
        /// </summary>
        public decimal ValveCycleTime;
        /// <summary>每次觸發打點數量
        /// </summary>
        public decimal JetCount;
        /// <summary>閥體壓(MPa)
        /// </summary>
        public decimal ValvePressure;
        /// <summary>排膠時間(Sec)
        /// </summary>
        public decimal PurgeTime;

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CDatabaseValveAdvanjet Clone()
        {
            CDatabaseValveAdvanjet mTemp = new CDatabaseValveAdvanjet();
            mTemp.RefillTime = this.RefillTime;
            mTemp.ValveCycleTime = this.ValveCycleTime;
            mTemp.JetCount = this.JetCount;
            mTemp.ValvePressure = this.ValvePressure;
            mTemp.PurgeTime = this.PurgeTime;
            return mTemp;
        }

        /// <summary>儲存步驟參數
        /// </summary>
        /// <param name="groupNo">類型</param>
        /// <param name="valveName">閥控器名稱</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(int groupNo, string fileName)
        {
            string sectionName = "ValveDatabase" + groupNo ;
            string keyNameStart = "Advanjet_";
            CIni.SaveIniString(sectionName, keyNameStart + "RefillTime", this.RefillTime.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "ValveCycleTime", this.ValveCycleTime.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "JetCount", this.JetCount.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "ValvePressure", this.ValvePressure.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "PurgeTime", this.PurgeTime.ToString(), fileName);
            return ErrorCode.Success;
        }

        /// <summary>讀取步驟參數
        /// </summary>
        /// <param name="groupNo">類型</param>
        /// <param name="valveName">閥控器名稱</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(int groupNo,  string fileName)
        {
            string sectionName = "ValveDatabase" + groupNo;
            string keyNameStart = "Advanjet_";
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "RefillTime", fileName, 0), out this.RefillTime);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "ValveCycleTime", fileName, 0), out this.ValveCycleTime);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "JetCount", fileName, 0), out this.JetCount);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "ValvePressure", fileName, 0), out this.ValvePressure);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "PurgeTime", fileName, 0), out this.PurgeTime);
            return ErrorCode.Success;
        }

    }
}

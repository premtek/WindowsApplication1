using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    
    /// <summary>噴射閥資料庫
    /// </summary>
    public class CDatabaseValveJet
    {
        /// <summary>閥名稱
        /// </summary>
        public string Name;
        /// <summary>上升時間(ms)
        /// </summary>
        public decimal RisingTime;
        /// <summary>打開時間(msec)
        /// </summary>
        public decimal ValveOnTime;
        /// <summary>下降時間
        /// </summary>
        public decimal FallingTime;
        /// <summary>點膠週期(msec)
        /// </summary>
        public decimal ValveCycleTime;
        /// <summary>每次觸發打點數量
        /// </summary>
        public decimal JetCount;
        /// <summary>閥頭溫度(度C)
        /// </summary>
        public decimal NozzleTemperature;
        /// <summary>開閥行程(%)
        /// </summary>
        public decimal Stroke;
        /// <summary>關閥電壓(Volt.)
        /// </summary>
        public decimal CloseVoltage;
        /// <summary>排膠時間(Sec)
        /// </summary>
        public decimal PurgeTime;

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CDatabaseValveJet Clone()
        {
            CDatabaseValveJet mTemp = new CDatabaseValveJet();
            mTemp.Name = this.Name;
            mTemp.RisingTime = this.RisingTime;
            mTemp.ValveOnTime = this.ValveOnTime;
            mTemp.FallingTime = this.FallingTime;
            mTemp.ValveCycleTime = this.ValveCycleTime;
            mTemp.JetCount = this.JetCount;
            mTemp.NozzleTemperature = this.NozzleTemperature;
            mTemp.Stroke = this.Stroke;
            mTemp.CloseVoltage = this.CloseVoltage;
            mTemp.PurgeTime = this.PurgeTime;
            return mTemp;
        }

        /// <summary>儲存步驟參數
        /// </summary>
        /// <param name="groupNo">類型</param>
        /// <param name="valveName">閥控器名稱</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(int groupNo,  string fileName)
        {
            string sectionName = "ValveDatabase" + groupNo;
            string keyNameStart = "Jet_";
            CIni.SaveIniString(sectionName, keyNameStart + "Name", this.Name, fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "RisingTime", this.RisingTime.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "ValveOnTime", this.ValveOnTime.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "FallingTime", this.FallingTime.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "ValveCycleTime", this.ValveCycleTime.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "JetCount", this.JetCount.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "NozzleTemperature", this.NozzleTemperature.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "Stroke", this.Stroke.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "CloseVoltage", this.CloseVoltage.ToString(), fileName);
            CIni.SaveIniString(sectionName, keyNameStart + "PurgeTime", this.PurgeTime.ToString(), fileName);
            return ErrorCode.Success;
        }

        /// <summary>讀取步驟參數
        /// </summary>
        /// <param name="groupNo">類型</param>
        /// <param name="valveName">閥控器名稱</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(int groupNo, string fileName)
        {
            string sectionName = "ValveDatabase" + groupNo;
            string keyNameStart = "Jet_";
            this.Name = CIni.ReadIniString(sectionName, keyNameStart + "Name", fileName,"");
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "RisingTime", fileName, 0.05M), out this.RisingTime);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "ValveOnTime", fileName, 1), out this.ValveOnTime);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "FallingTime", fileName, 0.05M), out this.FallingTime);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "ValveCycleTime", fileName, 2), out this.ValveCycleTime);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "JetCount", fileName, 1), out this.JetCount);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "NozzleTemperature", fileName, 30), out this.NozzleTemperature);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "Stroke", fileName, 70), out this.Stroke);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "CloseVoltage", fileName, 100), out this.CloseVoltage);
            decimal.TryParse(CIni.ReadIniString(sectionName, keyNameStart + "PurgeTime", fileName, 60), out this.PurgeTime);
            return ErrorCode.Success;
        }

    }
}

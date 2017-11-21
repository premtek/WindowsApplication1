using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>閥通訊類型
    /// </summary>
    public enum ValveType
    {
        /// <summary>虛擬閥
        /// </summary>
        Virtual,
        /// <summary>噴射閥
        /// </summary>
        JetValve,
        /// <summary>氣動閥,
        /// </summary>
        Advanjet,
        /// <summary>螺桿閥
        /// </summary>
        AugerValve,
        /// <summary>氣壓閥
        /// </summary>
        TimePressureValve,
        /// <summary>噴霧/塗覆閥
        /// </summary>
        CoatingValve,
    }
    public class CDatabaseValve
    {
        /// <summary>閥形式
        /// </summary>
        public ValveType Type;
        /// <summary>噴射閥
        /// </summary>
        public CDatabaseValveJet Jet;
        /// <summary>氣動閥
        /// </summary>
        public CDatabaseValveAdvanjet Advanjet;

        public CDatabaseValve()
        {
            Type = ValveType.Virtual;
            Jet = new CDatabaseValveJet();
            Advanjet = new CDatabaseValveAdvanjet();
        }
        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CDatabaseValve Clone()
        {
            CDatabaseValve mTemp = new CDatabaseValve();
            mTemp.Jet = this.Jet.Clone();
            mTemp.Advanjet = this.Advanjet.Clone();
            mTemp.Type = this.Type;
            return mTemp;
        }
        /// <summary>儲存參數
        /// </summary>
        /// <param name="groupNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(int groupNo, string fileName)
        {

            string sectionName = "ValveDatabase" + groupNo;
            string keyNameStart = "Valve_";

            CIni.SaveIniString(sectionName, keyNameStart + "Type", Convert.ToInt32(this.Type).ToString(), fileName);
            Jet.Save(groupNo, fileName);
            Advanjet.Save(groupNo, fileName);

            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="groupNo">資料庫編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(int groupNo, string fileName)
        {
            string sectionName = "ValveDatabase" + groupNo;

            string keyNameStart = "Valve_";

            this.Type = (ValveType)Enum.Parse(typeof(ValveType), CIni.ReadIniString(sectionName, keyNameStart + "Type", fileName, 0));
            Jet.Load(groupNo, fileName);
            Advanjet.Load(groupNo, fileName);

            return ErrorCode.Success;
        }

    }
}

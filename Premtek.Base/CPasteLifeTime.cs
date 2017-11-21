using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;

namespace Premtek.Base
{
    using System.Diagnostics;
    /// <summary>
    /// Lift Time
    /// </summary>
    /// <remarks></remarks>
    public class CPasteLifeTime
    {
        /// <summary>外部配接系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        /// <summary>[是否有提示過膠材計時壽命到期]</summary>
        /// <remarks></remarks>
        public bool[] LifeTimeAlarm = new bool[(int)enmValve.Max + 1];
        /// <summary>[是否有提示過膠材計數壽命到期]</summary>
        /// <remarks></remarks>
        public bool[] LifeCountAlarm = new bool[(int)enmValve.Max + 1];
        /// <summary>[Glue No1 剩下的時間]</summary>
        /// <remarks></remarks>
        public System.DateTime[] StartLifeTime = new System.DateTime[(int)enmValve.Max + 1];
        /// <summary>[Glue No1 噴了幾顆Chip]</summary>
        /// <remarks></remarks>
        public long[] DotsCount = new long[(int)enmValve.Max + 1];
        /// <summary>儲存膠材壽命</summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Save(string fileName)
        {
            string strSection = null;
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                strSection = "Valve" + (mValveNo + 1).ToString();
                CIni.SaveIniString(strSection, "PasteLifeTime_StartLifeTime" + (mValveNo + 1).ToString(), this.StartLifeTime[mValveNo].ToString(), fileName);
                CIni.SaveIniString(strSection, "PasteLifeTime_DotsCount" + (mValveNo + 1).ToString(), this.DotsCount[mValveNo].ToString(), fileName);
            }
            return true;
        }

        /// <summary>讀取膠材壽命</summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Load(string fileName)
        {
            string strSection = null;
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                strSection = "Valve" + (mValveNo + 1).ToString();
                DateTime.TryParse(CIni.ReadIniString(strSection, "PasteLifeTime_StartLifeTime" + (mValveNo + 1).ToString(), fileName, DateTime.Now.ToString()), out this.StartLifeTime[mValveNo]);
                long.TryParse(CIni.ReadIniString(strSection, "PasteLifeTime_DotsCount" + (mValveNo + 1).ToString(), fileName, 0), out this.DotsCount[mValveNo]);
            }
            return true;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek.Base
{
    using ProjectCore;

    /// <summary>
    /// Alarm Code對應工具
    /// </summary>
    /// <remarks></remarks>
    public class CAlarmIDMap
    {
        /// <summary>
        /// 取得軸錯誤代碼Mapping
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="alid"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetAxisALID(int axisNo, int alid)
        {
            const int alidBase = 30000;
            if (!mAxisALIDMap.ContainsKey(axisNo))
            {
                return alid;
            }
            return alid - alidBase + mAxisALIDMap[axisNo];
        }


        /// <summary>
        /// 讀取ALID對照表
        /// </summary>
        /// <remarks></remarks>

        Dictionary<int, int> mAxisALIDMap = new Dictionary<int, int>();
        public CAlarmIDMap()
        {
            //TODO: 先直接配接,後續再調整.
            //Select Case gSSystemParameter.enmMachineType
            //    Case enmMachineType.DCSW_800AQ
            mAxisALIDMap.Clear();
            mAxisALIDMap.Add(0, 30000);
            mAxisALIDMap.Add(2, 31000);
            mAxisALIDMap.Add(3, 32000);
            mAxisALIDMap.Add(1, 33000);
            mAxisALIDMap.Add(4, 34000);
            //mALIDMap.Add(enmAxis.CAxis, 35000)

            //mALIDMap.Add(enmAxis.Conveyor1, 37000)
            //mALIDMap.Add(enmAxis.Conveyor2, 38000)
            mAxisALIDMap.Add(5, 39000);
            mAxisALIDMap.Add(13, 40000);
            mAxisALIDMap.Add(8, 42000);
            mAxisALIDMap.Add(10, 43000);
            mAxisALIDMap.Add(11, 44000);
            mAxisALIDMap.Add(9, 45000);
            //mALIDMap.Add(enmAxis.EAxis, 46000)
            //mALIDMap.Add(enmAxis.FAxis, 47000)
            mAxisALIDMap.Add(16, 60000);
            mAxisALIDMap.Add(18, 61000);
            mAxisALIDMap.Add(19, 62000);
            mAxisALIDMap.Add(17, 63000);
            //mALIDMap.Add(enmAxis.HAxis, 64000)
            //mALIDMap.Add(enmAxis.IAxis, 65000)
            mAxisALIDMap.Add(20, 67000);
            mAxisALIDMap.Add(22, 68000);
            mAxisALIDMap.Add(23, 69000);
            mAxisALIDMap.Add(21, 70000);
            //mALIDMap.Add(enmAxis.KAxis, 71000)
            //mALIDMap.Add(enmAxis.LAxis, 72000)
            //End Select
        }

    }

}

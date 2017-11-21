using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>陣列資料</summary>
    public class CArrayInfo
    {
        /// <summary>
        /// 所在層
        /// </summary>
        public int LevelNo = 0;
        /// <summary>所在層內第幾個陣列</summary>
        public int RoomNo = 0;
        /// <summary>A方向陣列內的索引</summary>
        public int ASideNo = 0;
        /// <summary>B方向陣列內的索引</summary>
        public int BSideNo = 0;
        /// <summary>A方向數量
        /// </summary>
        public int ACount;
        /// <summary>B方向數量
        /// </summary>
        public int BCount;
        /// <summary>建構子(Constructor, Ctor)
        /// </summary>
        public CArrayInfo()
        {

        }
        /// <summary>建構子(Constructor, Ctor)
        /// </summary>
        public CArrayInfo(int levelNo, int roomNo, int aSideNo, int bSideNo)
        {
            LevelNo = levelNo;
            RoomNo = roomNo;
            ASideNo = aSideNo;
            BSideNo = bSideNo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>座標資料格式
    /// </summary>
    public struct sPos
    {
       public decimal PosX;
       public decimal PosY;
       public decimal PosZ;
       public decimal PosB;
       public decimal PosC;
    }
 

    /// <summary>位置點座標
    /// </summary>
    public class CPosPoint
    {
        /// <summary>X軸座標(mm)
        /// </summary>
        public decimal X;
        /// <summary>Y軸座標(mm)
        /// </summary>
        public decimal Y;
        /// <summary>Z軸座標(mm)
        /// </summary>
        public decimal Z;
        /// <summary>A軸座標(mm) 繞X軸旋轉(Tilt)
        /// </summary>
        public decimal A;
        /// <summary>B軸座標(mm) 繞Y軸旋轉(Tilt)
        /// </summary>
        public decimal B;
        /// <summary>C軸座標(mm) 繞Z軸旋轉(Rotation)
        /// </summary>
        public decimal C;

        /// <summary>複製</summary>
        /// <returns>複製資料</returns>
        public CPosPoint Clone()
        {
            CPosPoint _Temp = new CPosPoint();
            _Temp.X = this.X;
            _Temp.Y = this.Y;
            _Temp.Z = this.Z;
            _Temp.A = this.A;
            _Temp.B = this.B;
            _Temp.C = this.C;
            return _Temp;
        }
  
    }
}

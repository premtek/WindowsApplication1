using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek.Base
{
    /// <summary>連續同動軸</summary>
    /// <remarks></remarks>
    public class CSyncParameter
    {

        public sCardParameter CardParameter;
        /// <summary>同動軸清單 為本卡軸號, 注意是否與enmAxis混用</summary>
        /// <remarks></remarks>
        public List<int> SyncAxisNo = new List<int>();
        /// <summary>群組命令</summary>
        /// <remarks></remarks>
        public Advantech.Motion.PathCmd Cmd;
        /// <summary>目標位置</summary>
        /// <remarks></remarks>
        public decimal[] TargetPos = new decimal[8];
        /// <summary>中心位置</summary>
        /// <remarks></remarks>
        public decimal[] CenterPos = new decimal[8];
        /// <summary>軟體正極限</summary>
        /// <remarks></remarks>
        public decimal[] SPEL = new decimal[8];
        /// <summary>軟體負極限</summary>
        /// <remarks></remarks>
        public decimal[] SNEL = new decimal[8];
        /// <summary>單位轉換(pulse/mm)</summary>
        /// <remarks></remarks>
        public decimal[] Scale = new decimal[8];
        /// <summary>等待時間(ms)</summary>
        /// <remarks></remarks>
        public decimal GpDelay;
        /// <summary>速度設定</summary>
        /// <remarks></remarks>
        public SVelocity Velocity;
        /// <summary>最大速度設定</summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void SetVelHigh(decimal value)
        {
            Velocity.VelHigh = value;
        }
        /// <summary>初速度設定</summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void SetVelLow(decimal value)
        {
            Velocity.VelLow = value;
        }
        /// <summary>加速度設定</summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void SetAcc(decimal value)
        {
            Velocity.Acc = value;
        }
        /// <summary>減速度設定</summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void SetDec(decimal value)
        {
            Velocity.Dec = value;
        }
        /// <summary>路徑串接計數</summary>
        /// <remarks></remarks>

        public int PathCount;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectIO;
using ProjectCore;

namespace Premtek
{
    /// <summary>雙動缸作動狀態列舉
    /// </summary>
    /// <remarks>不一定與真實氣缸方向相同</remarks>
    public enum eDoubleActionCylinderAction
    {
        /// <summary>未致動/縮回/較安全
        /// </summary>
        Unactuated,
        /// <summary>致動/伸出/較危險
        /// </summary>
        Actuated,
    }

    /// <summary>雙動氣缸
    /// </summary>
    public class CCylinder
    {
        /// <summary>致動輸出接點索引
        /// </summary>
        public int DOActuate = -1;
        /// <summary>縮回輸出接點索引
        /// </summary>
        public int DOUnactuate = -1;

        /// <summary>致動檢知接點索引
        /// </summary>
        public int DIActuated = -1;
        /// <summary>縮回檢知接點索引
        /// </summary>
        public int DIUnactuated = -1;
        /// <summary>外部配接DO輸出物件
        /// </summary>
        public CDOCollection DOCollection { get; set; }
        /// <summary>外部配接DI輸入物件
        /// </summary>
        public CDICollection DICollection { get; set; }
        /// <summary>作動計時
        /// </summary>
        System.Diagnostics.Stopwatch _StopWatch = new System.Diagnostics.Stopwatch();

        /// <summary>逾時時間(ms) 預設:5000ms.
        /// </summary>
        public decimal Timout = 5000;

        /// <summary>氣缸作動
        /// </summary>
        /// <param name="act">作動方向</param>
        /// <returns></returns>
        public ErrorCode Action(eDoubleActionCylinderAction act)
        {
            if (DOCollection == null) System.Diagnostics.Debug.Assert(false);
            switch (act)
            {
                case eDoubleActionCylinderAction.Actuated://伸出
                    DOCollection.SetState(DOActuate, true);//內部有-1保護, 不另處理
                    DOCollection.SetState(DOUnactuate, false);
                    _StopWatch.Restart();
                    break;

                default:
                    DOCollection.SetState(DOActuate, false);//縮回
                    DOCollection.SetState(DOUnactuate, true);
                    _StopWatch.Restart();
                    break;
            }
            return ErrorCode.Success;
        }

        /// <summary>氣缸到位
        /// </summary>
        /// <param name="act">作動方向</param>
        /// <returns>到位: Success, 等待中: Running, 逾時:Failed</returns>
        public ErrorCode InPos(eDoubleActionCylinderAction act)
        {
            switch (act)
            {
                case eDoubleActionCylinderAction.Actuated://伸出
                    //內部有-1保護, 不另處理
                    if ((DICollection != null) && (DICollection.GetState(DIActuated, true) == true) && (DICollection.GetState(DIUnactuated, false) == false))
                    {
                        _StopWatch.Stop();
                        return ErrorCode.Success;
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    if (_StopWatch.ElapsedMilliseconds > this.Timout)
                    {
                        _StopWatch.Stop();
                        return ErrorCode.Failed;
                    }
                    break;
                default://縮回
                    if ((DICollection != null) && (DICollection.GetState(DIUnactuated, true) == true) && (DICollection.GetState(DIActuated, false) == false))
                    {
                        _StopWatch.Stop();
                        return ErrorCode.Success;
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    if (_StopWatch.ElapsedMilliseconds > this.Timout)
                    {
                        _StopWatch.Stop();
                        return ErrorCode.Failed;
                    }
                    break;
            }
            return ErrorCode.Running;
        }



    }
}

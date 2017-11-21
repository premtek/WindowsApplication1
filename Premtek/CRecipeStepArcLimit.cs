using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>弧限制條件
    /// </summary>
    public static class CRecipeStepArcLimit
    {
        /// <summary>弧至少3點
        /// </summary>
        private static int _Dotmin = 3;
        /// <summary>最多不限
        /// </summary>
        private static int _Dotmax = int.MaxValue;

        /// <summary>速度下限 (觸發板限制) (mm/s)
        /// </summary>
        private static decimal _velmin = 5;
        /// <summary>速度上限(mm/s) 硬體規格
        /// </summary>
        private static decimal _velmax = 1000;

        /// <summary>最小間距(mm)
        /// </summary>
        private static decimal _pitchmin = 0.01M;
        /// <summary>最大間距(mm)
        /// </summary>
        private static decimal _pitchmax = 30M;

        /// <summary>取得點數上下限
        /// </summary>
        /// <param name="length">弧長(mm)</param>
        /// <param name="radius">半徑(mm)</param>
        /// <param name="acc">加速度(mm/s^2)</param>
        /// <param name="cycleTime">打點週期(Sec)</param>
        /// <param name="dotmin">最少點數</param>
        /// <param name="dotmax">最大點數</param>
        /// <returns></returns>
        public static ErrorCode GetDotLimit(decimal length, decimal radius, decimal acc, decimal cycleTime, out int dotmin, out int dotmax)
        {
            decimal _dotmin1 = 0;
            decimal _dotmax1 = decimal.MaxValue;
            decimal _dotmin2 = 0;
            decimal _dotmax2 = decimal.MaxValue;

            _dotmin1 = (int)(length / _pitchmax) + 1;
            _dotmax1 = (int)(length / _pitchmin) + 1;

            decimal _velmax1 =(decimal) Math.Sqrt((double)(acc * radius));//離心速度限制
            _velmax1 = Math.Min(_velmax1, _velmax);
            decimal _pitchmin1 = _velmin * cycleTime;
            decimal _pitchmax1 = _velmax1 * cycleTime;
            _dotmin2 = (int)(length / _pitchmax1) + 1;
            _dotmax2 = (int)(length / _pitchmin1) + 1;

            dotmin = (int)Math.Max(_dotmin1, _dotmin2);
            dotmin = (int)Math.Max(_Dotmin, (decimal)dotmin);

            dotmax = (int)Math.Min(_dotmax1, _dotmax2);
            dotmax = (int)Math.Min(_Dotmax, (decimal)dotmax);

            return ErrorCode.Success;
        }

        /// <summary>取得速度上下限
        /// </summary>
        /// <param name="length">弧長(mm)</param>
        /// <param name="radius">半徑(mm)</param>
        /// <param name="acc">加速度(mm/s^2)</param>
        /// <param name="cycleTime">打點週期(Sec)</param>
        /// <param name="velmin">速度下限(mm/s)</param>
        /// <param name="velmax">速度上限(mm/s)</param>
        /// <returns></returns>
        public static ErrorCode GetVelLimit(decimal length, decimal radius, decimal acc, decimal cycleTime, out decimal velmin, out decimal velmax)
        {
            decimal _velmin1 = 0;
            decimal _velmax1 = decimal.MaxValue;
            decimal _pitchmin1 = _pitchmin;
            decimal _pitchmax1 = Math.Min(_pitchmax, length / (_Dotmin - 1));
            if (cycleTime <= 0)
            {
                velmin = 0;
                velmax = 0;
                return ErrorCode.Failed;
            }
            _velmin1 = _pitchmin1 / cycleTime;
            _velmax1 = _pitchmax1 / cycleTime;
            decimal _velmax2 = (decimal)Math.Sqrt((double)(acc * radius));//離心速度限制
            velmin = Math.Max(_velmin, _velmin1);
            velmax = Math.Min(_velmax, _velmax1);
            velmax = Math.Min(velmax, _velmax2);

            return ErrorCode.Success;
        }

        /// <summary>取得重量上下限
        /// </summary>
        /// <param name="length">弧長(mm)</param>
        /// <param name="radius">半徑(mm)</param>
        /// <param name="cycleTime">打點週期(Sec)</param>
        /// <param name="avgWeight">單點均重(mg)</param>
        /// <param name="weightmin">重量下限(mg)</param>
        /// <param name="weightmax">重量上限(mg)</param>
        /// <returns></returns>
        public static ErrorCode GetWeightLimit(decimal length, decimal radius, decimal acc, decimal cycleTime, decimal avgWeight, out decimal weightmin, out decimal weightmax)
        {
            if (avgWeight <= 0)
            {
                weightmin = 0;
                weightmax = 0;
                return ErrorCode.Failed;
            }
            int dotmin = 0;
            int dotmax = 0;
            if (GetDotLimit(length, radius, acc,cycleTime, out dotmin, out dotmax) != ErrorCode.Success)
            {
                weightmin = 0;
                weightmax = 0;
                return ErrorCode.Failed;
            }
            weightmin = (decimal)dotmin * avgWeight;
            weightmax = (decimal)dotmax * avgWeight;
            return ErrorCode.Success;
        }



    }
}

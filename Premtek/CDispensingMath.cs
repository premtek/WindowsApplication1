using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>點膠用數學運算
    /// </summary>
    public static class CDispensingMath
    {

        /// <summary>
        /// 計算跨越交越速度, 距離只算XY軸部分
        /// </summary>
        /// <param name="maxVelLimit">最大速度限制(mm/s)</param>
        /// <param name="acc">加速度(mm/s)</param>
        /// <param name="dec">減速度(mm/s)</param>
        /// <param name="lastPos">前一位置</param>
        /// <param name="nowPos">後一位置</param>
        /// <param name="verticalTime">等速段時間(Sec)</param>
        /// <param name="velocity">交越速度(mm/s)</param>
        /// <returns>True: 成功 False:異常</returns>
        /// <remarks></remarks>
        public static bool GetCrossVelocityXY(decimal maxVelLimit, decimal acc, decimal dec, sPos lastPos, sPos nowPos, decimal verticalTime, ref decimal velocity)
        {
            decimal mDistance = (decimal)Math.Sqrt(Math.Pow((double)(nowPos.PosX - lastPos.PosX), 2) + Math.Pow((double)(nowPos.PosY - lastPos.PosY), 2));
            return GetCrossVelocity(maxVelLimit, acc, dec, mDistance, verticalTime, ref velocity);
        }

        /// <summary>
        /// 計算跨越交越速度, 距離只算XY軸部分
        /// </summary>
        /// <param name="maxVelLimit">最大速度限制(mm/s)</param>
        /// <param name="acc">加速度(mm/s)</param>
        /// <param name="dec">減速度(mm/s)</param>
        /// <param name="lastPos">前一位置</param>
        /// <param name="nowPos">後一位置</param>
        /// <param name="totalTime">行程時間(Sec)</param>
        /// <param name="velocity">交越速度(mm/s)</param>
        /// <returns>True: 成功 False:異常</returns>
        /// <remarks></remarks>
        public static bool GetCrossVelocityXYByTotalTime(decimal maxVelLimit, decimal acc, decimal dec, sPos lastPos, sPos nowPos, decimal totalTime, ref decimal velocity)
        {
            decimal mDistance = (decimal)Math.Sqrt(Math.Pow((double)(nowPos.PosX - lastPos.PosX), 2) + Math.Pow((double)(nowPos.PosY - lastPos.PosY), 2));
            return GetCrossVelocityByTotalTime(maxVelLimit, acc, dec, mDistance, totalTime, ref velocity);
        }

        /// <summary>
        /// 計算交越速度
        /// </summary>
        /// <param name="maxVelLimit">最大速度限制(mm/s)</param>
        /// <param name="acc">加速度(mm/s)</param>
        /// <param name="dec">減速度(mm/s)</param>
        /// <param name="distance">移動量(mm)</param>
        /// <param name="totalTime">行程時間(Sec)</param>
        /// <param name="velocity">交越速度(mm/s)</param>
        /// <returns>True: 成功 False:異常</returns>
        /// <remarks></remarks>
        public static bool GetCrossVelocityByTotalTime(decimal maxVelLimit, decimal acc, decimal dec, decimal distance, decimal totalTime, ref decimal velocity)
        {
            decimal A = (1 / (2 * acc) + 1 / (2 * dec));
            decimal B = -totalTime;
            decimal C = distance;
            //虛數無法運算
            if (Math.Pow((double)B, 2) < (double)(4 * A * C))
            {
                return false;
            }
            //X=(-B+Sqrt(B^2-4AC))/(2A)
            //只取單側, 因Tv必須正, 則B必為正. Acc,Dec必須正, 則A必為正. 
            //開根號必須為正才能到這.
            //(負+正)/正
            //無負方向可能.
            decimal OneOver2A = 0.5M / A;
            //1/2A
            double BSquareSub4AC = Math.Pow((double)B, 2) - (double)(4 * A * C);
            velocity = ((decimal)Math.Sqrt((double)BSquareSub4AC) - B) * OneOver2A;
            //有設定, 且求出值大於設定值才加保護
            if (maxVelLimit > 0 && velocity > maxVelLimit)
            {
                velocity = maxVelLimit;
            }

            return true;
        }
        /// <summary>
        /// 計算跨越交越速度
        /// </summary>
        /// <param name="maxVelLimit">最大速度限制(mm/s)</param>
        /// <param name="acc">加速度(mm/s)</param>
        /// <param name="dec">減速度(mm/s)</param>
        /// <param name="distance">移動量(mm)</param>
        /// <param name="verticalTime">等速段時間(Sec)</param>
        /// <param name="velocity">交越速度(mm/s)</param>
        /// <returns>True: 成功 False:異常</returns>
        /// <remarks></remarks>
        public static bool GetCrossVelocity(decimal maxVelLimit, decimal acc, decimal dec, decimal distance, decimal verticalTime, ref decimal velocity)
        {
            //P = Pa + Pv + Pd = V^2/(2Acc) + V^2/(2Dec) + V*Tv
            //整理可得 (1/2Acc + 1/2Dec)V^2+Tv*V-P=0
            //AX^2+BX+C=0 
            decimal A = (1 / (2 * acc) + 1 / (2 * dec));
            decimal B = verticalTime;
            decimal C = -distance;
            //虛數無法運算
            if (Math.Pow((double)B, 2) < (double)(4 * A * C))
            {
                return false;
            }
            //X=(-B+Sqrt(B^2-4AC))/(2A)
            //只取單側, 因Tv必須正, 則B必為正. Acc,Dec必須正, 則A必為正. 
            //開根號必須為正才能到這.
            //(負+正)/正
            //無負方向可能.
            decimal OneOver2A = 0.5M / A;
            //1/2A
            double BSquareSub4AC = Math.Pow((double)B, 2) - (double)(4 * A * C);
            velocity = ((decimal)Math.Sqrt((double)BSquareSub4AC) - B) * OneOver2A;
            //有設定, 且求出值大於設定值才加保護
            if (maxVelLimit > 0 && velocity > maxVelLimit)
            {
                velocity = maxVelLimit;
            }
            if (velocity > 1000)//硬體限制
            {
                velocity = 1000;
            }
            return true;
        }


        /// <summary>計算最大切線速度(mm/s)
        /// </summary>
        /// <param name="acc">加速度(mm/s^2)</param>
        /// <param name="radius">半徑(mm)</param>
        /// <returns>最大切線速度(mm/s)</returns>
        public static decimal GetMaxTangentialVelocity(decimal acc, decimal radius)
        {
            decimal _maxVel = (decimal)Math.Sqrt((double)(acc * radius));
            return _maxVel;
        }

        /// <summary>取後TiltShiftPosX
        /// </summary>
        /// <param name="dDispenseGapGap"></param>
        /// <param name="tiltangle"></param>
        /// <returns></returns>
        public static decimal GetTiltShiftPosX(decimal dDispenseGapGap, decimal tiltangle)
        {

            if (tiltangle == 0) return 0;
            decimal dShiftPosX;
            double radians;
            radians = (double)tiltangle * Math.PI / 180;
            dShiftPosX = dDispenseGapGap * (decimal)Math.Tan(radians);
            return dShiftPosX;
        }

        /// <summary>取得直線加速延伸後的開始位置
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <param name="Xe"></param>
        /// <param name="Ye"></param>
        /// <param name="accDistance"></param>
        /// <param name="startPosX"></param>
        /// <param name="startPosY"></param>
        /// <returns></returns>
        public static ErrorCode GetLineAccStartPos(decimal Xs, decimal Ys, decimal Xe, decimal Ye, decimal accDistance, out decimal startPosX,out decimal startPosY)
        {
            decimal mdx = Xe - Xs;
            decimal mdy = Ye - Ys;
            decimal mr = Convert.ToDecimal(Math.Sqrt((double)(mdx * mdx + mdy * mdy)));
            startPosX = Xs;
            startPosY = Ys;

            if (mr == 0)
            {
                return ErrorCode.Failed;//無法判定
            }

            decimal _OffsetX = accDistance * mdx / mr;
            decimal _OffsetY = accDistance * mdy / mr;

            startPosX = Xs - _OffsetX;
            startPosY = Ys - _OffsetY;
            return ErrorCode.Success;
        }

        /// <summary>取得直線減速延伸後的結束位置
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <param name="Xe"></param>
        /// <param name="Ye"></param>
        /// <param name="decDistance"></param>
        /// <param name="endPosX"></param>
        /// <param name="endPosY"></param>
        /// <returns></returns>
        public static ErrorCode GetLineDecEndPos(decimal Xs, decimal Ys, decimal Xe, decimal Ye, decimal decDistance, out decimal endPosX, out decimal endPosY)
        {
            decimal mdx = Xe - Xs;
            decimal mdy = Ye - Ys;
            decimal mr = Convert.ToDecimal(Math.Sqrt((double)(mdx * mdx + mdy * mdy)));
            endPosX = Xe;
            endPosY = Ye;

            if (mr == 0)
            {
                return ErrorCode.Failed;//無法判定
            }

            decimal _OffsetX = decDistance * mdx / mr;
            decimal _OffsetY = decDistance * mdy / mr;

            endPosX = Xe + _OffsetX;
            endPosY = Ye + _OffsetY;
            return ErrorCode.Success;
        }
    }
}

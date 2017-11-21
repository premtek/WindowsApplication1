using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>單一定位結果
    /// </summary>
    public class CAlignResult
    {
        /// <summary>畫面上的特徵位置X(pixel)
        /// </summary>
        public decimal TranslationX = 0;
        /// <summary>畫面上的特徵位置Y(pixel)
        /// </summary>
        public decimal TranslationY = 0;
        /// <summary>畫面上的特徵角度(度)
        /// </summary>
        public decimal Rotation = 0;
        /// <summary>特徵分數
        /// </summary>
        public decimal Score = 100;
        /// <summary>特徵位置的實際座標X(mm)
        /// </summary>
        public decimal absPosX = 0;
        /// <summary>特徵位置的實際座標Y(mm)
        /// </summary>
        public decimal absPosY = 0;

        public CAlignResult Clone()
        {
            CAlignResult mTemp = new CAlignResult();
            mTemp.TranslationX = this.TranslationX;
            mTemp.TranslationY = this.TranslationY;
            mTemp.Rotation = this.Rotation;
            mTemp.Score = this.Score;
            mTemp.absPosX = this.absPosX;
            mTemp.absPosY = this.absPosY;
            return mTemp;
        }
    }
}

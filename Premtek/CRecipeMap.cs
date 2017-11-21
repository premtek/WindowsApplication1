using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>MAP設定
    /// </summary>
    public class CRecipeMap: IDisposable
    {
        #region "IDisposable"

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).

                }
                else
                {   
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeMap()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>分切比 左
        /// </summary>
        public decimal SplitedLeft;
        /// <summary>分切比 右
        /// </summary>
        public decimal SplitedRight;

        /// <summary>Map代號
        /// </summary>
        int _MapNo;

        public CRecipeMap(int mapNo)
        {
            _MapNo = mapNo;
            SplitedLeft = 5;
            SplitedRight = 5;
        }
        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeMap Clone()
        {
            CRecipeMap _Temp = new CRecipeMap(_MapNo);
            _Temp.SplitedLeft = this.SplitedLeft;
            _Temp.SplitedRight = this.SplitedRight;
            return _Temp;
        }

        /// <summary>儲存參數
        /// </summary>
        /// <param name="mapNo">MAP編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string fileName)
        {
            string _SectionName = "Map";
            string _KeyNameStart = "Map" + (_MapNo + 1).ToString() + "_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "SplitedLeft", this.SplitedLeft.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "SplitedRight", this.SplitedRight.ToString(), fileName);
            return ErrorCode.Success;
        }
        /// <summary>讀取參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="mapNo">MAP編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load( string fileName)
        {
            string _SectionName = "Map";
            string _KeyNameStart = "Map" + (_MapNo + 1).ToString() + "_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "SplitedLeft", fileName, 10), out this.SplitedLeft);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "SplitedRight", fileName, 10), out this.SplitedRight);
            return ErrorCode.Success;
        }
    }
}

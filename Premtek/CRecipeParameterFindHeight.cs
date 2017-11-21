using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>測高用參數
    /// </summary>
    public class CRecipeParameterFindHeight : IDisposable
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
        ~CRecipeParameterFindHeight()
        {
            Dispose(false);
        }
        #endregion


        /// <summary>測高規格上界容許誤差(mm)
        /// </summary>
        public decimal UpperTolerance;
        /// <summary>測高規格下界容許誤差(mm)
        /// </summary>
        public decimal LowerTolerance;

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeParameterFindHeight Clone()
        {
            CRecipeParameterFindHeight _Temp = new CRecipeParameterFindHeight();
            _Temp.UpperTolerance = this.UpperTolerance;
            _Temp.LowerTolerance = this.LowerTolerance;
            return _Temp;
        }

        /// <summary>儲存參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="groupNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string key, string fileName)
        {
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "FindHeight_";

            CIni.SaveIniString(_SectionName, _KeyNameStart + "UpperTolerance", this.UpperTolerance.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "LowerTolerance", this.LowerTolerance.ToString(), fileName);

            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="stepNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string key, string fileName)
        {
            string _SectionName = "StepParameter" + key.ToString();
            string _KeyNameStart = "FindHeight_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "UpperTolerance", fileName, 0), out this.UpperTolerance);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "LowerTolerance", fileName, 0), out this.LowerTolerance);
            
            return ErrorCode.Success;
        }
    }
}

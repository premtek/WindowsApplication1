﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
   public class CRecipeStepTimesUp:IDisposable
    {
        /// <summary>延遲時間(Sec)</summary>
        public decimal DelayTimeInSec;
       /// <summary>判定計時器名稱
       /// </summary>
        public string Name;
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
                    this.DelayTimeInSec = decimal.Zero;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStepTimesUp()
        {
            Dispose(false);
        }
        #endregion

        public CRecipeStepTimesUp Clone()
        {
            CRecipeStepTimesUp _Temp = new CRecipeStepTimesUp();
            _Temp.DelayTimeInSec = this.DelayTimeInSec;
            _Temp.Name = this.Name;
            return _Temp;
        }
        /// <summary>儲存步驟參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="stepNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string patternName, int stepNo, string fileName)
        {
            string _SectionName = patternName + "_Step";
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Delay_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "DelayTimeInSec", this.DelayTimeInSec.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Name", this.Name, fileName);
            return ErrorCode.Success;
        }
        /// <summary>讀取步驟參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="stepNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string patternName, int stepNo, string fileName)
        {
            string _SectionName = patternName + "_Step";
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Delay_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "DelayTimeInSec", fileName,0), out this.DelayTimeInSec);
            this.Name = CIni.ReadIniString(_SectionName, _KeyNameStart + "Name", fileName, "T0");
            return ErrorCode.Success;
        }
    }
}

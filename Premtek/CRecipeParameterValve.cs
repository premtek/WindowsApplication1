using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>點膠閥參數
    /// </summary>
    public class CRecipeParameterValve:IDisposable
    {
        /// <summary>使用閥1(出膠)
        /// </summary>
        public bool UseValve;
 
        /// <summary>閥控制器參數設定檔
        /// </summary>
        public string ValveCtrlName;

        
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
                    this.ValveCtrlName = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeParameterValve()
        {
            Dispose(false);
        }
        #endregion


        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeParameterValve Clone()
        {
            CRecipeParameterValve _Temp = new CRecipeParameterValve();
            _Temp.UseValve = this.UseValve;
        
            _Temp.ValveCtrlName = this.ValveCtrlName;
            return _Temp;
        }

        /// <summary>儲存參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="groupNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string key, int valveNo, string fileName)
        {
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Valve" + (valveNo + 1).ToString() + "_";

            CIni.SaveIniString(_SectionName, _KeyNameStart + "UseValve", this.UseValve.ToString(), fileName);

            CIni.SaveIniString(_SectionName, _KeyNameStart + "ValveCtrlName", this.ValveCtrlName, fileName);


            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="patternName">膠路名稱</param>
        /// <param name="stepNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string key, int valveNo, string fileName)
        {
            string _SectionName = "StepParameter" + key.ToString();
            string _KeyNameStart = "Valve" + (valveNo + 1).ToString() + "_";

            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "UseValve", fileName,0), out this.UseValve);
     
            this.ValveCtrlName = CIni.ReadIniString(_SectionName, _KeyNameStart + "ValveCtrlName", fileName,"");

            return ErrorCode.Success;
        }

    }
    
}

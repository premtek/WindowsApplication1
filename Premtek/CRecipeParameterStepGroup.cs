using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>參數類型 - 各步驟可能不一樣的參數
    /// </summary>
    public class CRecipeParameterStepGroup : IDisposable
    {
        /// <summary>線參數
        /// </summary>
        public CRecipeParameterLine Line;
        /// <summary>點參數
        /// </summary>
        public CRecipeParameterDot Dot;
        /// <summary>閥1參數
        /// </summary>
        public CRecipeParameterValve Valve1;
        /// <summary>閥2參數
        /// </summary>
        public CRecipeParameterValve Valve2;
        /// <summary>測高參數
        /// </summary>
        public CRecipeParameterFindHeight FindHeight;
        /// <summary>使用閥控制器參數
        /// </summary>
        public bool EnableValveCtrl;

        public CRecipeParameterStepGroup()
        {
            Line = new CRecipeParameterLine();
            Dot = new CRecipeParameterDot();
            Valve1 = new CRecipeParameterValve();
            Valve2 = new CRecipeParameterValve();
            FindHeight = new CRecipeParameterFindHeight();
        }

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
                    this.Line = null;
                    this.Dot = null;
                    this.Valve1.Dispose();
                    this.Valve2.Dispose();
                    this.FindHeight.Dispose();
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeParameterStepGroup()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeParameterStepGroup Clone()
        {
            CRecipeParameterStepGroup _Temp = new CRecipeParameterStepGroup();
            _Temp.EnableValveCtrl = this.EnableValveCtrl;
            _Temp.Line = this.Line.Clone();
            _Temp.Dot = this.Dot.Clone();
            _Temp.Valve1 = this.Valve1.Clone();
            _Temp.Valve2 = this.Valve2.Clone();
            _Temp.FindHeight = this.FindHeight.Clone();
            return _Temp;
        }

        /// <summary>存檔
        /// </summary>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>參見ErrorCode定義</returns>
        public ErrorCode Save(string key, string fileName)
        {
            this.Line.Save(key, fileName);
            this.Dot.Save(key, fileName);
            this.Valve1.Save(key, 0, fileName);
            this.Valve2.Save(key, 1, fileName);
            this.FindHeight.Save(key, fileName);
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Valve_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EnableValveCtrl", this.EnableValveCtrl.ToString(), fileName);
            return ErrorCode.Success;
        }
        /// <summary>讀檔
        /// </summary>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>參見ErrorCode定義</returns>
        public ErrorCode Load(string key, string fileName)
        {
            this.Line.Load(key, fileName);
            this.Dot.Load(key, fileName);
            this.Valve1.Load(key, 0, fileName);
            this.Valve2.Load(key, 1, fileName);
            this.FindHeight.Load(key, fileName);
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Valve_";
            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EnableValveCtrl", fileName, 0), out this.EnableValveCtrl);
            return ErrorCode.Success;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    
    /// <summary>Recipe步驟 線開始</summary>
    public class CRecipeStepContiStart : IDisposable
    {
        /// <summary>終點</summary>
        public CPosPoint Pos = new CPosPoint();
    
        /// <summary>移動速度(mm/s)
        /// </summary>
        public decimal Velocity = 100;
        public string ArrayInfo = "";

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
                    this.Pos = null;
                    this.ArrayInfo = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStepContiStart()
        {
            Dispose(false);
        }
        #endregion


        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStepContiStart Clone()
        {
            CRecipeStepContiStart _Temp = new CRecipeStepContiStart();
           
            _Temp.Pos.X = this.Pos.X;
            _Temp.Pos.Y = this.Pos.Y;
            _Temp.Pos.Z = this.Pos.Z;
            _Temp.Pos.A = this.Pos.A;
            _Temp.Pos.B = this.Pos.B;
            _Temp.Pos.C = this.Pos.C;
           
            _Temp.Velocity = this.Velocity;
            _Temp.ArrayInfo = this.ArrayInfo;
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_ContiStart_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosX", this.Pos.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosY", this.Pos.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosZ", this.Pos.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosA", this.Pos.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosB", this.Pos.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosC", this.Pos.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Velocity", this.Velocity.ToString(), fileName);
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_ContiStart_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosX", fileName, 0), out this.Pos.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosY", fileName, 0), out this.Pos.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosZ", fileName, 0), out this.Pos.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosA", fileName, 0), out this.Pos.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosB", fileName, 0), out this.Pos.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosC", fileName, 0), out this.Pos.C);
     
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Velocity", fileName, 0), out this.Velocity);
            return ErrorCode.Success;
        }
    }
}

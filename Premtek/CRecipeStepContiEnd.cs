using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>Recipe步驟 線結束</summary>
    public class CRecipeStepContiEnd : IDisposable
    {
        /// <summary>終點</summary>
        public CPosPoint Pos = new CPosPoint();
        /// <summary>打點數 預設:1點
        /// </summary>
        public int DotCount = 1;
        /// <summary>膠量(mg) 預設:1mg
        /// </summary>
        public decimal Weight = 1;
        /// <summary>點膠速度(mm/s)
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
        ~CRecipeStepContiEnd()
        {
            Dispose(false);
        }
        #endregion


        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStepContiEnd Clone()
        {
            CRecipeStepContiEnd _Temp = new CRecipeStepContiEnd();
            _Temp.DotCount = this.DotCount;
            _Temp.Pos.X = this.Pos.X;
            _Temp.Pos.Y = this.Pos.Y;
            _Temp.Pos.Z = this.Pos.Z;
            _Temp.Pos.A = this.Pos.A;
            _Temp.Pos.B = this.Pos.B;
            _Temp.Pos.C = this.Pos.C;
            _Temp.Weight = this.Weight;
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_ContiEnd_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosX", this.Pos.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosY", this.Pos.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosZ", this.Pos.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosA", this.Pos.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosB", this.Pos.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosC", this.Pos.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "DotCount", this.DotCount.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Weight", this.Weight.ToString(), fileName);
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_ContiEnd_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosX", fileName, 0), out this.Pos.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosY", fileName, 0), out this.Pos.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosZ", fileName, 0), out this.Pos.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosA", fileName, 0), out this.Pos.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosB", fileName, 0), out this.Pos.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosC", fileName, 0), out this.Pos.C);
            int.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "DotCount", fileName, 0), out this.DotCount);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Weight", fileName, 0), out this.Weight);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Velocity", fileName, 0), out this.Velocity);
            return ErrorCode.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>Recipe步驟 線參數</summary>
    public class CRecipeStepLine : IDisposable
    {
        /// <summary>起點</summary>
        public CPosPoint Start = new CPosPoint();
        /// <summary>終點</summary>
        public CPosPoint End = new CPosPoint();

        /// <summary>點膠速度(mm/s) 預設:100mm/s
        /// </summary>
        public decimal Velocity = 100;
        /// <summary>打點數 預設:100點
        /// </summary>
        public int DotCount = 100;
        /// <summary>膠量(mg) 預設:1mg
        /// </summary>
        public decimal Weight = 1;

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
                    this.Start = null;
                    this.End = null;
                    this.ArrayInfo = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStepLine()
        {
            Dispose(false);
        }
        #endregion
        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStepLine Clone()
        {
            CRecipeStepLine _Temp = new CRecipeStepLine();
            _Temp.Start = this.Start.Clone();
            _Temp.End = this.End.Clone();
            _Temp.Velocity = this.Velocity;
            _Temp.DotCount = this.DotCount;
            _Temp.Weight = this.Weight;
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Line_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "StartX", this.Start.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "StartY", this.Start.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "StartZ", this.Start.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "StartA", this.Start.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "StartB", this.Start.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "StartC", this.Start.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndX", this.End.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndY", this.End.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndZ", this.End.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndA", this.End.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndB", this.End.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndC", this.End.C.ToString(), fileName);

            CIni.SaveIniString(_SectionName, _KeyNameStart + "Velocity", this.Velocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "DotCount", this.DotCount.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Weight", this.Weight.ToString(), fileName);
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Line_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "StartX", fileName, 0), out this.Start.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "StartY", fileName, 0), out this.Start.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "StartZ", fileName, 0), out this.Start.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "StartA", fileName, 0), out this.Start.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "StartB", fileName, 0), out this.Start.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "StartC", fileName, 0), out this.Start.C);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndX", fileName, 0), out this.End.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndY", fileName, 0), out this.End.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndZ", fileName, 0), out this.End.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndA", fileName, 0), out this.End.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndB", fileName, 0), out this.End.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndC", fileName, 0), out this.End.C);

            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Velocity", fileName, 0), out this.Velocity);
            int.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "DotCount", fileName, 0), out this.DotCount);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Weight", fileName, 0), out this.Weight);
            return ErrorCode.Success;
        }
    }
}

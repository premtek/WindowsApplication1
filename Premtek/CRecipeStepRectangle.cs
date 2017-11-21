using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{

    /// <summary>Recipe步驟 矩形參數 </summary>
    public class CRecipeStepRectangle:IDisposable
    {
        /// <summary>開始點</summary>
        public CPosPoint Start = new CPosPoint();
        /// <summary>結束點</summary>
        public CPosPoint End = new CPosPoint();
        /// <summary>矩形角度</summary>
        public decimal Angle=0;
        /// <summary>是否矩形填滿</summary>
        public bool FullFilled= false;

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
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStepRectangle()
        {
            Dispose(false);
        }
        #endregion


        public CRecipeStepRectangle Clone()
        {
            CRecipeStepRectangle _Temp = new CRecipeStepRectangle();
            _Temp.Start = this.Start.Clone();
            _Temp.End = this.End.Clone();
            _Temp.Angle = this.Angle;
            _Temp.FullFilled = this.FullFilled;
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Rectangle_";
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

            CIni.SaveIniString(_SectionName, _KeyNameStart + "Angle", this.Angle.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "FullFilled",this.FullFilled.ToString(), fileName);

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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Rectangle_";
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

            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Angle", fileName, 0), out this.Angle);
            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "FullFilled", fileName, 0), out this.FullFilled);
          
            return ErrorCode.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>執行順序</summary>
    public enum RouteMethod
    {
        /// <summary>S型走法</summary>
        SType,
        /// <summary>Z型走法</summary>
        ZType,
        /// <summary>S型走法(起終點順序反轉)
        /// </summary>
        STypeReverse,
        /// <summary>Z型走法(起終點順序反轉)
        /// </summary>
        ZTypeReverse,
    }
    /// <summary>Recipe步驟 陣列參數</summary>
    public class CRecipeStepArray:IDisposable
    {
        /// <summary>路線走法
        /// </summary>
        public RouteMethod Method = RouteMethod.SType;
        /// <summary>陣列基準點</summary>
        public CPosPoint Origin = new CPosPoint();
        /// <summary>A方向</summary>
        public CPosPoint ASide = new CPosPoint();
        /// <summary>B方向</summary>
        public CPosPoint BSide = new CPosPoint();
        /// <summary>A方向重覆次數
        /// </summary>
        public int ACount = 1;
        /// <summary>B方向重覆次數</summary>
        public int BCount = 1;
        /// <summary>陣列執行的Pattern</summary>
        public string Pattern="";

        public string ArrayInfo="";
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
                    this.Origin = null;
                    this.ASide = null;
                    this.BSide = null;
                    this.Pattern = null;
                    this.ArrayInfo = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStepArray()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStepArray Clone()
        {
            CRecipeStepArray _Temp = new CRecipeStepArray();
            _Temp.Origin = this.Origin.Clone();
            _Temp.ASide = this.ASide.Clone();
            _Temp.BSide = this.BSide.Clone();
            _Temp.ACount = this.ACount;
            _Temp.BCount = this.BCount;
            _Temp.Pattern = this.Pattern;
            _Temp.Method = this.Method;
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Array_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginX", this.Origin.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginY", this.Origin.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginZ", this.Origin.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginA", this.Origin.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginB", this.Origin.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginC", this.Origin.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ASideX", this.ASide.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ASideY", this.ASide.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ASideZ", this.ASide.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ASideA", this.ASide.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ASideB", this.ASide.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ASideC", this.ASide.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BSideX", this.BSide.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BSideY", this.BSide.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BSideZ", this.BSide.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BSideA", this.BSide.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BSideB", this.BSide.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BSideC", this.BSide.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ACount", this.ACount.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BCount", this.BCount.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pattern", this.Pattern.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Method", Convert.ToInt32(this.Method).ToString(), fileName);
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Array_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginX", fileName, 0), out this.Origin.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginY", fileName, 0), out this.Origin.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginZ", fileName, 0), out this.Origin.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginA", fileName, 0), out this.Origin.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginB", fileName, 0), out this.Origin.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginC", fileName, 0), out this.Origin.C);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ASideX", fileName, 0), out this.ASide.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ASideY", fileName, 0), out this.ASide.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ASideZ", fileName, 0), out this.ASide.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ASideA", fileName, 0), out this.ASide.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ASideB", fileName, 0), out this.ASide.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ASideC", fileName, 0), out this.ASide.C);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BSideX", fileName, 0), out this.BSide.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BSideY", fileName, 0), out this.BSide.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BSideZ", fileName, 0), out this.BSide.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BSideA", fileName, 0), out this.BSide.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BSideB", fileName, 0), out this.BSide.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BSideC", fileName, 0), out this.BSide.C);
            Int32.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ACount", fileName, 1), out this.ACount);
            Int32.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BCount", fileName, 1), out this.BCount);
            this.Pattern = CIni.ReadIniString(_SectionName, _KeyNameStart + "Pattern", fileName,"Default");
            Enum.TryParse<RouteMethod>(CIni.ReadIniString(_SectionName, _KeyNameStart + "Method", fileName, "0"), out this.Method);
            return ErrorCode.Success;
        }

    }
}

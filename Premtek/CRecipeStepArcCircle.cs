using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>圓弧產生方式</summary>
    public enum ArcCircleMethod
    {
        /// <summary>三點定圓弧</summary>
        By3Pts,
        /// <summary>圓心定圓弧</summary>
        ByCenter,
    }
    /// <summary>圓弧方向(圓心定圓弧才有用)</summary>
    public enum ArcCircleDirection
    {
        /// <summary>順時針</summary>
        ClockWise,
        /// <summary>逆時針</summary>
        CounterClockWise,
    }
    /// <summary>Recipe步驟 圓弧參數</summary>
    public class CRecipeStepArcCircle:IDisposable
    {
        /// <summary>起點</summary>
        public CPosPoint Start;
        /// <summary>終點</summary>
        public CPosPoint End;
        /// <summary>中點</summary>
        public CPosPoint Middle ;
        /// <summary>圓心</summary>
        public CPosPoint Center;
        /// <summary>圓弧決定方式: 預設三點定圓弧</summary>
        public ArcCircleMethod Method ;
        /// <summary>預設順時針</summary>
        public ArcCircleDirection Direction;
        /// <summary>弧夾角(圓為360度)
        /// </summary>
        public decimal Angle;

        /// <summary>點膠速度(mm/s) 預設:100mm/s
        /// </summary>
        public decimal Velocity =100;
        /// <summary>打點數 預設:100點
        /// </summary>
        public int DotCount = 100;
        /// <summary>膠量(mg) 預設:1mg
        /// </summary>
        public decimal Weight=1;

        public string ArrayInfo="";

        public CRecipeStepArcCircle()
        {
            Start = new CPosPoint();
            End = new CPosPoint();
            Middle = new CPosPoint();
            Center = new CPosPoint();
            Method = ArcCircleMethod.By3Pts;
            Direction = ArcCircleDirection.ClockWise;
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
                    this.Start = null;
                    this.End = null;
                    this.Middle = null;
                    this.Center = null;
                    this.ArrayInfo = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStepArcCircle()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStepArcCircle Clone()
        {
            CRecipeStepArcCircle _Temp = new CRecipeStepArcCircle();            
            _Temp.Start = this.Start.Clone();
            _Temp.End = this.End.Clone();
            _Temp.Middle = this.Middle.Clone();
            _Temp.Center = this.Center.Clone();
            _Temp.Method = this.Method;
            _Temp.Direction = this.Direction;
            _Temp.Angle = this.Angle;
            _Temp.Velocity = this.Velocity;
            _Temp.DotCount = this.DotCount;
            _Temp.Weight = this.Weight;
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Arc_";
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
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MiddleX", this.Middle.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MiddleY", this.Middle.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MiddleZ", this.Middle.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MiddleA", this.Middle.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MiddleB", this.Middle.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MiddleC", this.Middle.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "CenterX", this.Center.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "CenterY", this.Center.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "CenterZ", this.Center.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "CenterA", this.Center.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "CenterB", this.Center.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "CenterC", this.Center.C.ToString(), fileName);

            CIni.SaveIniString(_SectionName, _KeyNameStart + "Method", Convert.ToInt32(this.Method).ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Direction", Convert.ToInt32(this.Direction).ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Angle", this.Angle.ToString(), fileName);
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Arc_";
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
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MiddleX", fileName, 0), out this.Middle.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MiddleY", fileName, 0), out this.Middle.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MiddleZ", fileName, 0), out this.Middle.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MiddleA", fileName, 0), out this.Middle.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MiddleB", fileName, 0), out this.Middle.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MiddleC", fileName, 0), out this.Middle.C);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "CenterX", fileName, 0), out this.Center.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "CenterY", fileName, 0), out this.Center.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "CenterZ", fileName, 0), out this.Center.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "CenterA", fileName, 0), out this.Center.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "CenterB", fileName, 0), out this.Center.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "CenterC", fileName, 0), out this.Center.C);

            this.Method = (ArcCircleMethod)Enum.Parse(typeof(ArcCircleMethod), CIni.ReadIniString(_SectionName, _KeyNameStart + "Method", fileName, 0));
            this.Direction = (ArcCircleDirection)Enum.Parse(typeof(ArcCircleDirection), CIni.ReadIniString(_SectionName, _KeyNameStart + "Direction", fileName, 0));
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Angle", fileName, 90), out this.Angle);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Velocity", fileName, 100), out this.Velocity);
            int.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "DotCount", fileName, 3), out this.DotCount);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Weight", fileName, 0.1M), out this.Weight);
            return ErrorCode.Success;
        }
    }
}


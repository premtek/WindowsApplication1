using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>步驟工作類型
    /// </summary>
    public enum eStepWorkType
    {
        /// <summary>打點
        /// </summary>
        Dot,
        /// <summary>畫單一線
        /// </summary>
        Line,
        /// <summary>畫單一弧
        /// </summary>
        Arc,
        /// <summary>畫單一圓
        /// </summary>
        Circle,
        /// <summary>方形繪製
        /// </summary>
        Rectangle,
        /// <summary>呼叫別的子程式至目前程式(不定位)
        /// </summary>
        Pattern,
        /// <summary>將指定行號程式做陣列
        /// </summary>
        Array,
        /// <summary>增加一行延時指令, 單位為Sec秒
        /// </summary>
        Delay,
        /// <summary>清膠動作
        /// </summary>
        Purge,
        /// <summary>測高動作
        /// </summary>
        FindHeight,
        /// <summary>Underfill陣列, 對內部橫向展開
        /// </summary>
        UFArray,
        /// <summary>計時器開始
        /// </summary>
        TimerStart,
        /// <summary>計時器條件
        /// </summary>
        TimesUp,
        /// <summary>秤重
        /// </summary>
        Weight,
        /// <summary>線開始
        /// </summary>
        ContiStart,
        /// <summary>線結束
        /// </summary>
        ContiEnd,
        /// <summary>線中點
        /// </summary>
        LineMid,
        /// <summary>弧中點
        /// </summary>
        ArcMid,
    }

    /// <summary>Recipe Step資料結構
    /// </summary>
    public class CRecipeStep:IDisposable
    {
        /// <summary>上層物件
        /// </summary>
        public CRecipePattern Parent;

        /// <summary>作業方式
        /// </summary>
        public eStepWorkType WorkType;
        /// <summary>使用資料</summary>
        public string Type = "Default";
        /// <summary>註解</summary>
        public string Remark = "";
        
        /// <summary>介面判斷參數設定是否OK可存檔
        /// </summary>
        public bool IsCorrect;

        /// <summary>呼叫副程式
        /// </summary>
        public CRecipeStepPattern Pattern = new CRecipeStepPattern();
        /// <summary>測高資料集
        /// </summary>
        public CRecipeHeightGroup FindHeight ;
     
        /// <summary>打點
        /// </summary>
        public CRecipeStepDot Dot = new CRecipeStepDot();
        /// <summary>畫線
        /// </summary>
        public CRecipeStepLine Line = new CRecipeStepLine();
        /// <summary>圓弧
        /// </summary>
        public CRecipeStepArcCircle Arc = new CRecipeStepArcCircle();
        /// <summary>陣列
        /// </summary>
        public CRecipeStepArray Array = new CRecipeStepArray();
        /// <summary>延遲
        /// </summary>
        public CRecipeStepDelay Delay = new CRecipeStepDelay();
        /// <summary>矩形
        /// </summary>
        public CRecipeStepRectangle Rectangle = new CRecipeStepRectangle();
        /// <summary>計時器開始
        /// </summary>
        public CRecipeStepTimerStart TimerStart = new CRecipeStepTimerStart();
        /// <summary>計時器條件
        /// </summary>
        public CRecipeStepTimesUp TimesUp = new CRecipeStepTimesUp();
        /// <summary>弧中點
        /// </summary>
        public CRecipeStepArcMid ArcMid = new CRecipeStepArcMid();
        /// <summary>線結束
        /// </summary>
        public CRecipeStepContiEnd ContiEnd = new CRecipeStepContiEnd();
        /// <summary>線開始
        /// </summary>
        public CRecipeStepContiStart ContiStart = new CRecipeStepContiStart();
        /// <summary>線中點
        /// </summary>
        public CRecipeStepLineMid LineMid = new CRecipeStepLineMid();
        public CRecipeStepPurge Purge = new CRecipeStepPurge();
        /// <summary>所屬陣列資料
        /// </summary>
        public string ArrayInfo = "";

        /// <summary>A側Map絕對索引
        /// </summary>
        public int AbsIdxA;
        /// <summary>B側Map絕對索引
        /// </summary>
        public int AbsIdxB;
        /// <summary>是否執行該步驟(預設執行)
        /// </summary>
        public bool Enabled = true;
        /// <summary>使用哪一張Map對應
        /// </summary>
        public int MapNo;

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
                    this.Pattern.Dispose();
                    this.FindHeight.Dispose();
                    this.Dot.Dispose();
                    this.Line.Dispose();
                    this.Arc.Dispose();
                    this.Array .Dispose();
                    this.Delay .Dispose();
                    this.Rectangle  .Dispose();
                    this.Remark = null;
                    this.ArrayInfo = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeStep()
        {
            Dispose(false);
        }
        #endregion
       
        public CRecipeStep(CRecipePattern parent)
        {
            this.Parent = parent;
            int _ConveyorCount = 2;
            int _MachineStageCount=2;
            this.FindHeight = new CRecipeHeightGroup(_ConveyorCount, _MachineStageCount);
        }
        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStep Clone()
        {
            CRecipeStep _Temp = new CRecipeStep(null);
            
            _Temp.AbsIdxA = this.AbsIdxA;
            _Temp.AbsIdxB = this.AbsIdxB;
            _Temp.Arc = this.Arc.Clone();
            _Temp.ArcMid = this.ArcMid.Clone();
            _Temp.Array = this.Array.Clone();
            
            _Temp.Delay = this.Delay.Clone();
            _Temp.Dot = this.Dot.Clone();
            _Temp.Enabled = this.Enabled;
            _Temp.FindHeight = this.FindHeight.Clone();
            _Temp.Line = this.Line.Clone();
            _Temp.MapNo = this.MapNo;
            _Temp.Parent = this.Parent;
            _Temp.Pattern = this.Pattern.Clone();
            _Temp.Rectangle = this.Rectangle.Clone();
            _Temp.ContiEnd = this.ContiEnd.Clone();
            _Temp.Remark = this.Remark;
            
            _Temp.TimerStart = this.TimerStart;
            _Temp.TimesUp = this.TimesUp;
            _Temp.Type = this.Type;
            _Temp.WorkType = this.WorkType;
            
            return _Temp;
        }

        public ErrorCode Save(string patternName, int stepNo, string fileName)
        {
            string _SectionName = patternName + "_Step";
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Step_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "WorkType", Convert.ToInt32(this.WorkType).ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Type", this.Type, fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Remark", this.Remark, fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "AbsIdxA", this.AbsIdxA.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "AbsIdxB", this.AbsIdxB.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Enabled", this.Enabled.ToString(), fileName);

            Pattern.Save(patternName, stepNo, fileName);
            FindHeight.Save(patternName, stepNo, fileName);
            
            Dot.Save(patternName, stepNo, fileName);
            Line.Save(patternName, stepNo, fileName);
            Arc.Save(patternName, stepNo, fileName);
            ArcMid.Save(patternName, stepNo, fileName);
            Array.Save(patternName, stepNo, fileName);
            Delay.Save(patternName, stepNo, fileName);
            Rectangle.Save(patternName, stepNo, fileName);
            TimerStart.Save(patternName, stepNo, fileName);
            TimesUp.Save(patternName, stepNo, fileName);
            ContiEnd.Save(patternName, stepNo, fileName);
            return ErrorCode.Success;
        }

        public ErrorCode Load(string patternName, int stepNo, string fileName)
        {
            string _SectionName = patternName + "_Step";
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Step_";
            this.WorkType = (eStepWorkType)Enum.Parse(typeof(eStepWorkType), CIni.ReadIniString(_SectionName, _KeyNameStart + "WorkType", fileName,0));
            this.Type = CIni.ReadIniString(_SectionName, _KeyNameStart + "Type", fileName,0);
            this.Remark = CIni.ReadIniString(_SectionName, _KeyNameStart + "Remark", fileName,"");
            Int32.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "AbsIdxA", fileName,0), out this.AbsIdxA);
            Int32.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "AbsIdxB", fileName, 0), out this.AbsIdxB);
            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Enabled", fileName, 0), out this.Enabled);
            
            Pattern.Load(patternName, stepNo, fileName);
            FindHeight.Load(patternName, stepNo, fileName);
            Dot.Load(patternName, stepNo, fileName);
            Line.Load(patternName, stepNo, fileName);
            Arc.Load(patternName, stepNo, fileName);
            ArcMid.Load(patternName, stepNo, fileName);
            Array.Load(patternName, stepNo, fileName);
            Delay.Load(patternName, stepNo, fileName);
            Rectangle.Load(patternName, stepNo, fileName);
            TimerStart.Load(patternName, stepNo, fileName);
            TimesUp.Load(patternName, stepNo, fileName);
            ContiEnd.Load(patternName, stepNo, fileName);
            return ErrorCode.Success;
        }
    }



}

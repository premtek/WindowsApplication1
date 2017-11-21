using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{

    /// <summary>
    /// Recipe Pattern資料結構
    /// </summary>
    public class CRecipePattern
    {
        /// <summary>名稱
        /// </summary>
        public string Name ;

        /// <summary>上層物件
        /// </summary>
        public CRecipe Parent;


        /// <summary>步驟資料清單</summary>
        public List<CRecipeStep> Step;

        /// <summary>定位點資料
        /// </summary>
        public CRecipeAlignGroup Align ;


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
                    this.Parent = null;
                    for (int i = 0; i < this.Step.Count; i++)
                    {
                        this.Step[i].Dispose();
                    }
                    this.Align.Dispose();
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipePattern()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>建構子
        /// </summary>
        /// <param name="parent"></param>
        public CRecipePattern(CRecipe parent)
        {
            this.Parent = parent;
            Name = "Unknown";
            Step = new List<CRecipeStep>();
            Align = new CRecipeAlignGroup();
        }

        /// <summary>複製</summary>
        /// <returns></returns>
        public CRecipePattern Clone()
        {
            CRecipePattern _Temp = new CRecipePattern(null);
            _Temp.Name = this.Name;
            _Temp.Parent = this.Parent;
            _Temp.Align = this.Align.Clone();
            _Temp.Step.Clear();
            for (int i = 0; i < this.Step.Count; i++)
            {
                _Temp.Step.Add(this.Step[i].Clone());
            }
            return _Temp;
        }

        public ErrorCode Save(string patternName, string fileName)
        {
            string _SectionName = patternName + "_Step";
            string _KeyNameStart = "Step";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Count", this.Step.Count.ToString(), fileName);
            this.Align.Save(patternName, fileName);
            for (int _StepNo = 0; _StepNo < this.Step.Count; _StepNo++)
            {
                this.Step[_StepNo].Save(patternName, _StepNo, fileName);
            }
            return ErrorCode.Success;
        }
        public ErrorCode Load(string patternName, string fileName)
        {
            string _SectionName = patternName + "_Step";
            string _KeyNameStart = "Step";
            this.Align.Load(patternName, fileName);
            int _StepCount = Convert.ToInt32(CIni.ReadIniString(_SectionName, _KeyNameStart + "Count", fileName, "0"));
            this.Step.Clear();
            for (int _StepNo = 0; _StepNo < _StepCount; _StepNo++)
            {
                CRecipeStep _Temp = new CRecipeStep(this);
                _Temp.Load(patternName, _StepNo, fileName);
                this.Step.Add(_Temp);
            }
            return ErrorCode.Success;
        }
        /// <summary>取得測高步驟
        /// </summary>
        /// <returns></returns>
        public CRecipeHeightGroup GetHeightStep()
        {
            CRecipeHeightGroup _Temp = null;
            for (int i = this.Step.Count -1; i >=0; i--)//由後往前找最後面的測高步驟
            {
                switch (this.Step[i].WorkType)
                {
                    case eStepWorkType.FindHeight:
                        _Temp = this.Step[i].FindHeight.Clone();
                        return _Temp;
                }
            }
            //都沒找到為null
            return _Temp;
        }
    }

}

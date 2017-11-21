using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Premtek
{

    public enum enmAlignType
    {
        /// <summary>不定位
        /// </summary>
        None = -1,
        /// <summary>單點定位
        /// </summary>
        DevicePos1 = 0,
        /// <summary>兩點定位
        /// </summary>
        DevicePos2 = 1,
        /// <summary>SkipMark+兩點定位
        /// </summary>
        DevicePos3 = 2,
    }
    /// <summary>異常(失效)處理方式
    /// </summary>
    public enum FailedReaction
    {
        /// <summary>忽略不處理
        /// </summary>
        None,
        /// <summary>提示警告
        /// </summary>
        Caution,
        /// <summary>異常停止
        /// </summary>
        Stop,
    }
   
    /// <summary>定位資料群組</summary>
    public class CRecipeAlignGroup : IDisposable
    {
        /// <summary>備註
        /// </summary>
        public string Remark;
        /// <summary>定位方式, 預設單點定位
        /// </summary>
        public enmAlignType Type;

        
        public Dictionary<int , Dictionary<int, CRecipeAlignStage>> Align = new Dictionary<int,Dictionary<int,CRecipeAlignStage>>();
        /// <summary>忽略定位結果
        /// </summary>
        public bool ByPassResult;
        /// <summary>定位失敗處理方式 (預設不處理)
        /// </summary>
        public FailedReaction AlignFailed;
        /// <summary>陣列資訊
        /// </summary>
        public string ArrayInfo;
        /// <summary>絕對索引
        /// </summary>
        public int AbsIdxA;
        /// <summary>絕對索引
        /// </summary>
        public int AbsIdxB;

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
                    for (int _ConveyorNo = 0; _ConveyorNo < 2; _ConveyorNo++)
                    {
                        for (int _MachineStageNo = 0; _MachineStageNo < 2; _MachineStageNo++)
                        {
                            Align[_ConveyorNo][_MachineStageNo].Align1.Dispose();
                            Align[_ConveyorNo][_MachineStageNo].Align2.Dispose();
                            Align[_ConveyorNo][_MachineStageNo].Align3.Dispose();
                            Align[_ConveyorNo][_MachineStageNo].Align1 = null;
                            Align[_ConveyorNo][_MachineStageNo].Align2 = null;
                            Align[_ConveyorNo][_MachineStageNo].Align3 = null;
                            Align[_ConveyorNo][_MachineStageNo].Origin = null;
                            Align[_ConveyorNo][_MachineStageNo].Result = null;
                        }
                    }
                   
                    this.Remark = null;
                    this.ArrayInfo = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeAlignGroup()
        {
            Dispose(false);
        }
        #endregion

        public CRecipeAlignGroup()
        {

            Type = enmAlignType.DevicePos1;
            Align.Clear();
            for (int conveyorNo = 0; conveyorNo < 2; conveyorNo++)
            {
                Dictionary<int, CRecipeAlignStage> _tempConveyor = new Dictionary<int, CRecipeAlignStage>();
                for (int machineStageNo = 0; machineStageNo < 2; machineStageNo++)
                {
                    CRecipeAlignStage _Temp = new CRecipeAlignStage();
                    _Temp.Align1 = new CRecipeAlignPos();
                    _Temp.Align2 = new CRecipeAlignPos();
                    _Temp.Align3 = new CRecipeAlignPos();
                    _Temp.Origin = new CPosPoint();
                    _Temp.Result = new List<CAlignResult>();
                    _tempConveyor.Add(machineStageNo, _Temp);
                }
                Align.Add(conveyorNo, _tempConveyor);
            }
            
            
            AlignFailed = FailedReaction.None;
            Remark = "";
        }
        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeAlignGroup Clone()
        {
            CRecipeAlignGroup _Temp = new CRecipeAlignGroup();
            _Temp.Remark = this.Remark;
            _Temp.Align.Clear();
            for (int conveyorNo = 0; conveyorNo < 2; conveyorNo++)
            {
                Dictionary<int, CRecipeAlignStage> _tempConveyor = new Dictionary<int, CRecipeAlignStage>();
                for (int machineStageNo = 0; machineStageNo < 2; machineStageNo++)
                {
                    CRecipeAlignStage _TempAlign = new CRecipeAlignStage();
                    _TempAlign.Align1 = this.Align[conveyorNo][machineStageNo].Align1.Clone();
                    _TempAlign.Align2 = this.Align[conveyorNo][machineStageNo].Align2.Clone();
                    _TempAlign.Align3 = this.Align[conveyorNo][machineStageNo].Align3.Clone();
                    _TempAlign.Origin = this.Align[conveyorNo][machineStageNo].Origin.Clone();
                    _TempAlign.Result = new List<CAlignResult>(); //_Temp.Result 還沒寫Clone...
                    _tempConveyor.Add(machineStageNo, _TempAlign);
                }
                _Temp.Align.Add(conveyorNo, _tempConveyor);
            }
            
       
            _Temp.AlignFailed = this.AlignFailed;
            _Temp.ByPassResult = this.ByPassResult;
           
            _Temp.Type = this.Type;
            _Temp.ArrayInfo = this.ArrayInfo;
            _Temp.AbsIdxA = this.AbsIdxA;
            _Temp.AbsIdxB = this.AbsIdxB;
            return _Temp;
        }
        /// <summary>儲存參數
        /// </summary>
        /// <param name="patternName">所屬Pattern</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string patternName, string fileName)
        {
            string _SectionName = patternName + "_Align";
            string _KeyNameStart = patternName + "_Align_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Remark", this.Remark, fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Type", Convert.ToInt32(this.Type).ToString(), fileName);
            for (int _ConveyorNo = 0; _ConveyorNo < Align.Count; _ConveyorNo++)
            {
                for (int _MachineStageNo = 0; _MachineStageNo < Align[_ConveyorNo].Count; _MachineStageNo++)
                {
                    Align[_ConveyorNo][_MachineStageNo].Save(patternName, _ConveyorNo, _MachineStageNo, fileName);
                }
            }
            
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ByPassResult", Convert.ToInt32(this.ByPassResult).ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "AlignFailed", Convert.ToInt32(this.AlignFailed).ToString(), fileName);
            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="stepNo">所屬Pattern</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string patternName, string fileName)
        {
            string _SectionName = patternName + "_Align";
            string _KeyNameStart = patternName + "_Align_";
            this.Remark = CIni.ReadIniString(_SectionName, _KeyNameStart + "Remark", fileName, "");
            this.Type = (enmAlignType)Enum.Parse(typeof(enmAlignType), CIni.ReadIniString(_SectionName, _KeyNameStart + "Type", fileName, "-1"));


            if (this.Align == null)
            {
                this.Align = new Dictionary<int, Dictionary<int, CRecipeAlignStage>>();
            }
            for (int _ConveyorNo = 0; _ConveyorNo < Align.Count; _ConveyorNo++)
            {
                for (int _MachineStageNo = 0; _MachineStageNo < Align[_ConveyorNo].Count; _MachineStageNo++)
                {
                    Align[_ConveyorNo][_MachineStageNo].Load(patternName, _ConveyorNo, _MachineStageNo, fileName);
                }
            }
            
            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ByPassResult", fileName, 0), out this.ByPassResult);
            Enum.TryParse<FailedReaction>(CIni.ReadIniString(_SectionName, _KeyNameStart + "AlignFailed", fileName, 0), out this.AlignFailed);

            return ErrorCode.Success;
        }

    }
}

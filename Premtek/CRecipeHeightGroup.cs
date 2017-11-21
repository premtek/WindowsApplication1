using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>測高資料群組
    /// </summary>
    public class CRecipeHeightGroup : IDisposable
    {
        public Dictionary<int, Dictionary<int, CRecipeStepFindHeight>> Height = new Dictionary<int, Dictionary<int, CRecipeStepFindHeight>>();
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
                            Height[_ConveyorNo][_MachineStageNo].ArrayInfo = null;
                            Height[_ConveyorNo][_MachineStageNo].Dispose(); 
                        }
                    }
                    this.ArrayInfo = null;
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeHeightGroup()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>傳送帶數量
        /// </summary>
        int _ConveyorCount;
        /// <summary>單機平台數量
        /// </summary>
        int _MachineStageCount;

        public CRecipeHeightGroup(int conveyorCount, int machineStageCount)
        {
            _ConveyorCount = conveyorCount;
            _MachineStageCount = machineStageCount;
            Height.Clear();
            for (int conveyorNo = 0; conveyorNo < conveyorCount; conveyorNo++)
            {
                Dictionary<int, CRecipeStepFindHeight> _tempConveyor = new Dictionary<int, CRecipeStepFindHeight>();
                for (int machineStageNo = 0; machineStageNo < machineStageCount; machineStageNo++)
                {
                    CRecipeStepFindHeight _Temp = new CRecipeStepFindHeight();
                    _Temp.ArrayInfo = "";
                    _Temp.Pos = new CPosPoint();
                    _tempConveyor.Add(machineStageNo, _Temp);
                }
                Height.Add(conveyorNo, _tempConveyor);
            }
            
        }
        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeHeightGroup Clone()
        {
            CRecipeHeightGroup _Temp = new CRecipeHeightGroup(_ConveyorCount,_MachineStageCount);

            _Temp.Height.Clear();
            for (int conveyorNo = 0; conveyorNo < _ConveyorCount; conveyorNo++)
            {
                Dictionary<int, CRecipeStepFindHeight> _tempConveyor = new Dictionary<int, CRecipeStepFindHeight>();
                for (int machineStageNo = 0; machineStageNo < _MachineStageCount; machineStageNo++)
                {
                    CRecipeStepFindHeight _TempHeight = new CRecipeStepFindHeight();
                    _TempHeight.ArrayInfo = this.Height[conveyorNo][machineStageNo].ArrayInfo;
                    _TempHeight.Pos = this.Height[conveyorNo][machineStageNo].Pos.Clone();
                    _tempConveyor.Add(machineStageNo, _TempHeight);
                }
                _Temp.Height.Add(conveyorNo, _tempConveyor);
            }
            
            _Temp.ArrayInfo = this.ArrayInfo;
            _Temp.AbsIdxA = this.AbsIdxA;
            _Temp.AbsIdxB = this.AbsIdxB;
            return _Temp;
        }
        /// <summary>儲存參數
        /// </summary>
        /// <param name="patternName">所屬Pattern</param>
        /// <param name="stepNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string patternName,int stepNo, string fileName)
        {
            string _SectionName = patternName + "_Height";
            string _KeyNameStart = patternName + "_Height_";
         
            for (int _ConveyorNo = 0; _ConveyorNo < Height.Count; _ConveyorNo++)
            {
                for (int _MachineStageNo = 0; _MachineStageNo < Height[_ConveyorNo].Count; _MachineStageNo++)
                {
                    Height[_ConveyorNo][_MachineStageNo].Save(patternName, _ConveyorNo, _MachineStageNo,stepNo, fileName);
                }
            }
            
            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="stepNo">所屬Pattern</param>
        /// <param name="patternName">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string patternName, int stepNo, string fileName)
        {
            string _SectionName = patternName + "_Height";
            string _KeyNameStart = patternName + "_Height_";

            if (this.Height == null)
            {
                this.Height = new Dictionary<int, Dictionary<int, CRecipeStepFindHeight>>();
            }
            for (int _ConveyorNo = 0; _ConveyorNo < Height.Count; _ConveyorNo++)
            {
                for (int _MachineStageNo = 0; _MachineStageNo < Height[_ConveyorNo].Count; _MachineStageNo++)
                {
                    Height[_ConveyorNo][_MachineStageNo].Load(patternName, _ConveyorNo, _MachineStageNo, stepNo, fileName);
                }
            }
            
            return ErrorCode.Success;
        }

    }
}

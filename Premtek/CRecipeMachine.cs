using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    public class CRecipeMachine : IDisposable
    {
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
                    for (int i = 0; i < AlignList.Count; i++)
                    {
                        AlignList[i].Dispose();
                    }
                    AlignList.Clear();
                    for (int i = 0; i < HeightList.Count; i++)
                    {
                        HeightList[i].Dispose();
                    }
                    this.HeightList.Clear();
                    this.Main = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipeMachine()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>Recipe上層
        /// </summary>
        public CRecipe Parent;
        /// <summary>展開後定位清單
        /// </summary>
        public List<CRecipeAlignGroup> AlignList;
        public CRecipeMachine(int stage)
        {
            switch (stage)
            {
                case 0:
                    Main = "Main";
                    break;

                default:
                    Main = "Main" + (stage + 1).ToString();
                    break;
            }
            AlignList = new List<CRecipeAlignGroup>();
            HeightList = new List<CRecipeHeightGroup>();
        }
        public string Main ;
        public bool IsUseValve1()
        {
            bool mUseValve1 = false;
            for (int mStepNo = 0; mStepNo < Parent.Pattern[Main].Step.Count; mStepNo++)
            {
                string _Type = Parent.Pattern[Main].Step[mStepNo].Type;
                if (Parent.StepGroup[_Type].Valve1.UseValve)
                {
                    mUseValve1 = true;
                }
            }
            return mUseValve1;
        }

        public bool IsUseValve2()
        {
            bool mUseValve2 = false;
            for (int mStepNo = 0; mStepNo < Parent.Pattern[Main].Step.Count; mStepNo++)
            {
                string _Type = Parent.Pattern[Main].Step[mStepNo].Type;
                if (Parent.StepGroup[_Type].Valve2.UseValve)
                {
                    mUseValve2 = true;
                }
            }
            return mUseValve2;
        }
        /// <summary>取得展開後, 定位起點位置
        /// </summary>
        /// <param name="stageNo">平台</param>
        /// <param name="startPosX">起點位置</param>
        /// <param name="startPosY">起點位置</param>
        /// <returns></returns>
        public ErrorCode GetAlignStartPos(int conveyorNo,int stageNo, out decimal startPosX, out decimal startPosY, out decimal startPosZ)
        {
            startPosX = 0;
            startPosY = 0;
            startPosZ = 0;
            if (AlignList == null)
            {
                return ErrorCode.Failed;
            }

            decimal _Xs = decimal.MinValue;
            decimal _Ys = decimal.MinValue;
            decimal _Xe = decimal.MinValue;
            decimal _Ye = decimal.MinValue;
            decimal _Zs = decimal.MinValue;
            decimal _Ze = decimal.MinValue;

            for (int i = 0; i < AlignList.Count; i++)
            {
                switch (AlignList[i].Type)
                {
                    case enmAlignType.None:
                        break;

                    case enmAlignType.DevicePos1:
                        if (_Xs == decimal.MinValue)
                        {
                            _Xs = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.X;
                        }
                        else
                        {
                            _Xe = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.X;
                        }
                        if (_Ys == decimal.MinValue)
                        {
                            _Ys = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Y;
                        }
                        else
                        {
                            _Ye = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Y;
                        }
                        if (_Zs == decimal.MinValue)
                        {
                            _Zs = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Z;
                        }
                        else
                        {
                            _Ze = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Z;
                        }
                        break;

                    case enmAlignType.DevicePos2://不支援
                        if (_Xs == decimal.MinValue)
                        {
                            startPosX = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.X;
                            startPosY = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Y;
                            startPosZ = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Z;
                        }
                        else
                        {
                            startPosX = _Xs;
                            startPosY = _Ys;
                            startPosZ = _Zs;
                        }
                        return ErrorCode.Success;

                    case enmAlignType.DevicePos3://不支援
                        if (_Xs == decimal.MinValue)
                        {
                            startPosX = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.X;
                            startPosY = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Y;
                            startPosZ = AlignList[i].Align[conveyorNo][stageNo].Align1.Pos.Z;
                        }
                        else
                        {
                            startPosX = _Xs;
                            startPosY = _Ys;
                            startPosZ = _Zs;
                        }
                        return ErrorCode.Success;
                }
                if (_Ye != decimal.MinValue)//起終點有值
                {
                    break;
                }
            }

            if (_Xs == decimal.MinValue)//沒有起點..
            {
                startPosX = 0;
                startPosY = 0;
                startPosZ = 0;
                return ErrorCode.Failed;
            }
            if (_Xe == decimal.MinValue)//沒有終點
            {
                startPosX = _Xs;
                startPosY = _Ys;
                startPosZ = _Zs;
                return ErrorCode.Success;
            }
            //起終點路徑延伸
            CDispensingMath.GetLineAccStartPos(_Xs, _Ys, _Xe, _Ye, Parent.FOFAccDistance, out startPosX, out startPosY);
            return ErrorCode.Success;
        }
        /// <summary>展開後測高清單
        /// </summary>
        public List<CRecipeHeightGroup> HeightList;

        /// <summary>是否需要做定位流程(任一定位資料OK)
        /// </summary>
        /// <param name="stageNo">哪個Stage</param>
        /// <returns></returns>
        public bool IsNeedFids(int conveyorNo,int stageNo)
        {
            bool _Need = false;
            string entry = Main;//進入點
            if (this.AlignList.Count == 0)
            {
                return _Need;
            }
            for (int i = 0; i < this.AlignList.Count; i++)
            {
                switch (this.AlignList[i].Type)
                {
                    case enmAlignType.None:
                    case enmAlignType.DevicePos1:
                        if (this.AlignList[i].Align[conveyorNo][stageNo].Align1.Secne != "")//第一點場景存在
                        {
                            _Need = true;
                            return _Need;
                        }
                        break;
                    case enmAlignType.DevicePos2:
                        if (this.AlignList[i].Align[conveyorNo][stageNo].Align1.Secne != "")
                        {
                            _Need = true;
                            return _Need;
                        }
                        if (this.AlignList[i].Align[conveyorNo][stageNo].Align2.Secne != "")
                        {
                            _Need = true;
                            return _Need;
                        }
                        break;
                    case enmAlignType.DevicePos3:
                        if (this.AlignList[i].Align[conveyorNo][stageNo].Align1.Secne != "")
                        {
                            _Need = true;
                            return _Need;
                        }
                        if (this.AlignList[i].Align[conveyorNo][stageNo].Align2.Secne != "")
                        {
                            _Need = true;
                            return _Need;
                        }
                        if (this.AlignList[i].Align[conveyorNo][stageNo].Align3.Secne != "")
                        {
                            _Need = true;
                            return _Need;
                        }
                        break;
                }

            }

            return _Need;
        }

    }
}

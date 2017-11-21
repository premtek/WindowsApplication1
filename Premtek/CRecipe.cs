using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Premtek
{
    public enum ErrorCode
    {
        /// <summary>成功</summary>
        Success,
        /// <summary>失敗</summary>
        Failed,
        /// <summary>運行中
        /// </summary>
        Running,
    }

    public class CRecipe : IDisposable
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
                    foreach (string key in this.Pattern.Keys)
                    {
                        this.Pattern[key].Dispose();
                    }
                    this.Pattern.Clear();
                    foreach (string key in this.StepGroup.Keys)
                    {
                        this.StepGroup[key].Dispose();
                    }
                    this.StepGroup.Clear();
                    for (int i = 0; i < this.Machine.Count; i++)
                    {
                        this.Machine[i].Dispose();
                    }
                    this.Machine.Clear();
                    for (int i = 0; i < this.Map.Count; i++)
                    {
                        this.Map[i].Dispose();
                    }
                    this.Map.Clear();
                    this.FileName = null;
                    //Debug.Print("CRecipeAlignGroup Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
        ~CRecipe()
        {
            Dispose(false);
        }
        #endregion


        /// <summary>Pattern清單</summary>
        public Dictionary<string, Premtek.CRecipePattern> Pattern;
        /// <summary>Step參數清單
        /// </summary>
        public Dictionary<string, CRecipeParameterStepGroup> StepGroup;

        public List<CRecipeMap> Map;
        /// <summary>飛拍加減速距離(延伸)
        /// </summary>
        internal decimal FOFAccDistance = 2000;

        public List<CRecipeMachine> Machine;

        /// <summary>完整檔案名稱
        /// </summary>
        string FileName;

        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        public CRecipe Clone()
        {
            CRecipe _Temp = new CRecipe(this.Machine.Count);
            _Temp.Pattern.Clear();
            foreach (string key in this.Pattern.Keys)
            {
                _Temp.Pattern.Add(key, this.Pattern[key].Clone());
            }
            _Temp.StepGroup.Clear();
            foreach (string key in this.StepGroup.Keys)
            {
                _Temp.StepGroup.Add(key, this.StepGroup[key].Clone());
            }
            _Temp.Machine.Clear();
            for (int i = 0; i < Machine.Count; i++)
            {
                _Temp.Machine.Add(new CRecipeMachine(i));
            }
            _Temp.FileName = this.FileName;
            _Temp.Map.Clear();
            for (int i = 0; i < this.Map.Count; i++)
            {
                _Temp.Map.Add(this.Map[i].Clone());
            }
            return _Temp;
        }


        /// <summary>建構子(Constructor, Ctor)
        /// </summary>
        public CRecipe(int machineCount)//建立物件時, 預設提供主進入點main~4.
        {
            Pattern = new Dictionary<string, Premtek.CRecipePattern>();
            StepGroup = new Dictionary<string, CRecipeParameterStepGroup>();
            Machine = new List<CRecipeMachine>();
            Map = new List<CRecipeMap>();
            this.Machine.Clear();
            this.Map.Clear();
            for (int i = 0; i < machineCount; i++)
            {
                CRecipeMachine _Machine = new CRecipeMachine(i);
                _Machine.Parent = this;
                this.Machine.Add(_Machine);
                CRecipePattern _Pattern = new CRecipePattern(this);
                _Pattern.Name = this.Machine[i].Main;
                Pattern.Add(this.Machine[i].Main, _Pattern);

                CRecipeMap _Map = new CRecipeMap(i);
                this.Map.Add(_Map);
            }

            this.FileName = "";//預設無路徑
        }

        /// <summary>存檔
        /// </summary>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>參見ErrorCode定義</returns>
        public ErrorCode Save(string fileName)
        {
            CIni.SaveIniString("Recipe", "PatternCount", Pattern.Count.ToString(), fileName);
            int _patterNo = 1;
            if (Pattern != null)
            {
                foreach (string key in Pattern.Keys)
                {
                    CIni.SaveIniString("PatternName", "PatternName" + _patterNo.ToString(), key.ToString(), fileName);
                    Pattern[key].Save(key, fileName);
                    _patterNo++;
                }
            }
            else
            {
                return ErrorCode.Failed;
            }
            if (StepGroup != null)
            {
                CIni.SaveIniString("Recipe", "StepParameterCount", StepGroup.Count.ToString(), fileName);
            }
            else
            {
                return ErrorCode.Failed;
            }
            int _No = 1;
            foreach (string key in this.StepGroup.Keys)
            {
                CIni.SaveIniString("StepParameter", "StepParameter" + _No.ToString(), key.ToString(), fileName);
                StepGroup[key].Save(key, fileName);
                _No++;
            }
            for (int i = 0; i < Map.Count; i++)
            {
                Map[i].Save(fileName);
            }
            return ErrorCode.Success;
        }
        /// <summary>讀檔
        /// </summary>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>參見ErrorCode定義</returns>
        public ErrorCode Load(string fileName)
        {
            this.FileName = fileName;
            int _PatternCount = 0;
            Int32.TryParse(CIni.ReadIniString("Recipe", "PatternCount", fileName, 1), out _PatternCount);
            Pattern.Clear();
            for (int i = 1; i <= _PatternCount; i++)
            {
                string patternName = CIni.ReadIniString("PatternName", "PatternName" + i.ToString(), fileName, "Default");
                if (patternName == "main") patternName = "Main";
                if (patternName == "main2") patternName = "Main2";
                if (patternName == "main3") patternName = "Main3";
                if (patternName == "main4") patternName = "Main4";
                if ((patternName != "") && (patternName != null))//空白或空物件不接受
                {
                    if (!Pattern.ContainsKey(patternName))//沒有重覆才能加
                    {
                        CRecipePattern _Pattern = new CRecipePattern(this);
                        _Pattern.Name = patternName;
                        _Pattern.Load(patternName, fileName);
                        Pattern.Add(patternName, _Pattern);
                    }
                }
            }
            int _No = 0;
            Int32.TryParse(CIni.ReadIniString("Recipe", "StepParameterCount", fileName, 0), out _No);
            StepGroup.Clear();
            for (int i = 1; i <= _No; i++)
            {
                string key = CIni.ReadIniString("StepParameter", "StepParameter" + _No.ToString(), fileName, "Default");
                if (!StepGroup.ContainsKey(key))
                {
                    StepGroup.Add(key, new CRecipeParameterStepGroup());
                }
            }
            if (!StepGroup.ContainsKey("Default"))//如果沒有Default, 自動加一組.
            {
                StepGroup.Add("Default", new CRecipeParameterStepGroup());
            }
            for (int i = 0; i < Map.Count; i++)
            {
                Map[i].Load(fileName);
            }
            return ErrorCode.Success;
        }

        /// <summary>巨集展開 前處理</summary>
        /// <returns>經前處理後的Recipe, 裡面只會有一個Pattern</returns>
        /// <remarks>新增指令需確認此處</remarks>
        public CRecipe PreProcess()
        {
            CRecipe _Temp = new CRecipe(this.Machine.Count);//開新資料結構 實際執行的Map
            _Temp.Pattern.Clear();

            for (int _MachineNo = 0; _MachineNo < this.Machine.Count; _MachineNo++)//每個進入點
            {
                _Temp.Machine[_MachineNo].AlignList.Clear(); //清除定位資料重新安排
                _Temp.Machine[_MachineNo].HeightList.Clear();//清除測高資料重新安排

                string _PatternID = this.Machine[_MachineNo].Main;//進入點名稱
                if (Pattern.ContainsKey(_PatternID))//需要進入點
                {
                    CRecipePattern _Pattern = new CRecipePattern(this);
                    _Pattern.Name = this.Machine[_MachineNo].Main;
                    _Temp.Pattern.Add(_PatternID, _Pattern); //主進入點


                    //===  全域定位點 ===
                    CRecipeAlignGroup _Align = this.Pattern[_PatternID].Align.Clone();
                    if (_Align.Type != enmAlignType.None)//要定位才有
                    {
                        _Align.ArrayInfo = "0,0,0,0,";
                        _Temp.Machine[_MachineNo].AlignList.Add(_Align);//定位資料清單}
                    }
                    //===  全域定位點 ===


                    //===  全域測高點 ===
                    CRecipeHeightGroup _Height = this.Pattern[_PatternID].GetHeightStep();
                    if (_Height != null)
                    {
                        _Height.ArrayInfo = "0,0,0,0,";
                        _Temp.Machine[_MachineNo].HeightList.Add(_Height);
                    }
                    //===  全域測高點 ===

                    int _RoomNo = 0;//該層第幾個陣列

                    for (int _StepNo = 0; _StepNo < this.Pattern[_PatternID].Step.Count; _StepNo++)//對目前Pattern的資料展開
                    {
                        switch (this.Pattern[_PatternID].Step[_StepNo].WorkType)
                        {
                            case eStepWorkType.Pattern:
                            case eStepWorkType.Arc:
                            case eStepWorkType.Circle:
                            case eStepWorkType.Delay:
                            case eStepWorkType.Dot:
                            case eStepWorkType.FindHeight:
                            case eStepWorkType.Line:
                            case eStepWorkType.Purge:
                                _Temp.Pattern[_PatternID].Step.Add(this.Pattern[_PatternID].Step[_StepNo].Clone());
                                break;
                            case eStepWorkType.Rectangle:
                                _Temp.Pattern[_PatternID].Step.AddRange(ExpandRectangle(this.Pattern[_PatternID].Step[_StepNo], 0, 0));
                                break;
                            case eStepWorkType.Array:
                                List<CArrayInfo> ArrayInfoList = new List<CArrayInfo>();
                                ArrayInfoList.Add(new CArrayInfo(0, _RoomNo, 0, 0));

                                List<CRecipeStep> _stepList = new List<CRecipeStep>();
                                List<CRecipeAlignGroup> _alignList = new List<CRecipeAlignGroup>();
                                List<CRecipeHeightGroup> _HeightList = new List<CRecipeHeightGroup>();

                                ExpandArray(this.Pattern[_PatternID].Step[_StepNo].Array, 0, 0, ArrayInfoList, out _stepList, out _alignList, out _HeightList);

                                _Temp.Pattern[_PatternID].Step.AddRange(_stepList);
                                for (int i = 0; i < _alignList.Count; i++)
                                {
                                    _Temp.Machine[_MachineNo].AlignList.Add(_alignList[i]);//定位清單
                                }
                                for (int i = 0; i < _HeightList.Count; i++)
                                {
                                    _Temp.Machine[_MachineNo].HeightList.Add(_HeightList[i]);//測高清單
                                }
                                _RoomNo++;
                                break;
                            default:
                                Debug.Assert(false);//跳錯誤是為了提醒這邊沒改到.
                                break;
                        }
                    }

                }
                for (int _ConveyorNo = 0; _ConveyorNo < 2; _ConveyorNo++)
                {
                    for (int _StageNo = 0; _StageNo < 2; _StageNo++)
                    {
                        for (int i = 0; i < _Temp.Machine[_MachineNo].AlignList.Count; i++)
                        {
                            Debug.Print("MAC: " + _MachineNo + "軌道" + (_ConveyorNo + 1).ToString() + " Stage" + (_StageNo + 1).ToString() + " 定位清單" + i.ToString() + ":" + "\tAlign1(" + _Temp.Machine[_MachineNo].AlignList[i].Align[_ConveyorNo][_StageNo].Align1.Pos.X + "," + _Temp.Machine[_MachineNo].AlignList[i].Align[_ConveyorNo][_StageNo].Align1.Pos.Y + ") Info: " + _Temp.Machine[_MachineNo].AlignList[i].ArrayInfo);
                        }
                        for (int i = 0; i < _Temp.Machine[_MachineNo].HeightList.Count; i++)
                        {
                            Debug.Print("MAC: " + _MachineNo + "軌道" + (_ConveyorNo + 1).ToString() + " Stage" + (_StageNo + 1).ToString() + " 測高清單" + i.ToString() + ":" + "\tAlign1(" + _Temp.Machine[_MachineNo].HeightList[i].Height[_ConveyorNo][_StageNo].Pos.X + "," + _Temp.Machine[_MachineNo].HeightList[i].Height[_ConveyorNo][_StageNo].Pos.Y + ") Info: " + _Temp.Machine[_MachineNo].HeightList[i].ArrayInfo);
                        }
                    }
                }


            }

            return _Temp;
        }

        /// <summary>矩形展開</summary>
        /// <param name="rect">投入矩形資料</param>
        /// <param name="baseOriginX">展開的基準點X</param>
        /// <param name="baseOriginY">展開的基準點Y</param>
        /// <returns>展開後的矩形資料</returns>
        List<CRecipeStep> ExpandRectangle(CRecipeStep rect, decimal baseOriginX, decimal baseOriginY)
        {
            //TODO: 考慮在介面處理此功能展開.
            List<CRecipeStep> _Temp = new List<CRecipeStep>();
            if (rect.Rectangle.FullFilled == true)//填滿
            {
                Debug.Assert(false);
            }
            else//外框
            {
                decimal _Slope = Convert.ToDecimal(Math.Tan(Convert.ToDouble(rect.Rectangle.Angle) * Math.PI / 180));//斜率 =角度取Tan 對邊/底邊

                //角度點, 可視為 終點於該線段的垂足
                //套用垂足公式
                CRecipeStep _Line1 = new CRecipeStep(rect.Parent);
                _Line1.WorkType = eStepWorkType.Line;
                _Line1.Type = rect.Type;
                _Line1.Line.Start.X = rect.Rectangle.Start.X;
                _Line1.Line.Start.Y = rect.Rectangle.Start.Y;
                CMath.GetFootPoint(rect.Rectangle.Start.X, rect.Rectangle.Start.Y, _Slope, rect.Rectangle.End.X, rect.Rectangle.End.Y, out _Line1.Line.End.X, out _Line1.Line.End.Y);//垂足公式
                _Temp.Add(_Line1);//左上到右上

                CRecipeStep _Line2 = new CRecipeStep(rect.Parent);
                _Line2.WorkType = eStepWorkType.Line;
                _Line2.Type = rect.Type;
                _Line2.Line.Start.X = _Line1.Line.End.X;
                _Line2.Line.Start.Y = _Line1.Line.End.Y;
                _Line2.Line.End.X = rect.Rectangle.End.X;
                _Line2.Line.End.Y = rect.Rectangle.End.Y;
                _Temp.Add(_Line2);//右上到右下

                CRecipeStep _Line3 = new CRecipeStep(rect.Parent);
                _Line3.WorkType = eStepWorkType.Line;
                _Line3.Type = rect.Type;
                _Line3.Line.Start.X = rect.Rectangle.End.X;
                _Line3.Line.Start.Y = rect.Rectangle.End.Y;
                CMath.GetFootPoint(rect.Rectangle.End.X, rect.Rectangle.End.Y, _Slope, rect.Rectangle.Start.X, rect.Rectangle.Start.Y, out _Line3.Line.End.X, out _Line3.Line.End.Y);//垂足公式
                _Temp.Add(_Line3);//右下到左下

                CRecipeStep _Line4 = new CRecipeStep(rect.Parent);
                _Line4.WorkType = eStepWorkType.Line;
                _Line4.Type = rect.Type;
                _Line4.Line.Start.X = _Line3.Line.End.X;
                _Line4.Line.Start.Y = _Line3.Line.End.Y;
                _Line4.Line.End.X = rect.Rectangle.Start.X;
                _Line4.Line.End.Y = rect.Rectangle.Start.Y;
                _Temp.Add(_Line4);//左下到左上
            }
            return _Temp;
        }

        /// <summary>將陣列資訊轉為字串</summary>
        /// <param name="arrayInfoList">陣列資訊</param>
        /// <returns>輸出字串</returns>
        string GetArrayInfoString(List<CArrayInfo> arrayInfoList)
        {
            string _ArrayString = "";
            for (int i = 0; i < arrayInfoList.Count; i++)
            {
                _ArrayString += arrayInfoList[i].LevelNo + "," + arrayInfoList[i].RoomNo + "," + arrayInfoList[i].ASideNo + "," + arrayInfoList[i].BSideNo + ",";
            }
            return _ArrayString;
        }

        /// <summary>各層陣列索引轉絕對索引
        /// </summary>
        /// <param name="arrayInfoList">各層陣列資料</param>
        /// <param name="AbsIdxA">絕對索引</param>
        /// <param name="AbsIdxB">絕對索引</param>
        /// <returns></returns>
        bool GetArrayAbsIdx(List<CArrayInfo> arrayInfoList, out int AbsIdxA, out int AbsIdxB)
        {
            AbsIdxA = 0;
            AbsIdxB = 0;
            if (arrayInfoList == null)//無法判定
            {
                return false;
            }
            int[] _ACount = new int[arrayInfoList.Count];
            int[] _BCount = new int[arrayInfoList.Count];

            for (int levelNo = arrayInfoList.Count - 1; levelNo >= 0; levelNo--)
            {
                if (arrayInfoList[levelNo] == null)//資料不存在
                {
                    return false;
                }
                if (levelNo == (arrayInfoList.Count - 1))//最小區塊
                {
                    _ACount[levelNo] = arrayInfoList[levelNo].ACount;
                    _BCount[levelNo] = arrayInfoList[levelNo].BCount;
                    AbsIdxA = arrayInfoList[levelNo].ASideNo;
                    AbsIdxB = arrayInfoList[levelNo].BSideNo;
                }
                else
                {
                    _ACount[levelNo] = arrayInfoList[levelNo].ACount * _ACount[levelNo + 1];
                    _BCount[levelNo] = arrayInfoList[levelNo].BCount * _BCount[levelNo + 1];
                    AbsIdxA += _ACount[levelNo + 1] * arrayInfoList[levelNo].ASideNo;
                    AbsIdxB += _BCount[levelNo + 1] * arrayInfoList[levelNo].BSideNo;
                }
            }
            return true;
        }

        /// <summary>陣列展開</summary>
        /// <param name="array">投入陣列資料</param>
        /// <param name="baseOriginX">展開的基準點X</param>
        /// <param name="baseOriginY">展開的基準點Y</param>
        /// <param name="arrayInfoList">叫用此陣列的資料</param>
        /// <param name="stepList">展開後的膠路陣列資料</param>
        /// <param name="alignList">展開後的定位陣列資料</param>
        /// <param name="heightList">展開後的測高陣列資料</param>
        /// <returns>展開後的陣列資料</returns>
        void ExpandArray(CRecipeStepArray array, decimal baseOriginX, decimal baseOriginY, List<CArrayInfo> arrayInfoList, out List<CRecipeStep> stepList, out List<CRecipeAlignGroup> alignList, out List<CRecipeHeightGroup> heightList)
        {
            List<CRecipeStep> _StepList = new List<CRecipeStep>();//輸出的資料
            List<CRecipeAlignGroup> _AlignList = new List<CRecipeAlignGroup>();//輸出的定位資料
            List<CRecipeHeightGroup> _HeightList = new List<CRecipeHeightGroup>();//輸出的測高資料

            string _PatternName = array.Pattern;
            if (this.Pattern.ContainsKey(_PatternName))
            {

                int _RoomNo = 0;
                switch (array.Method) //如要加入搜尋方向 在這段改展開順序
                {
                    case RouteMethod.ZType:
                        /*
                         *   1   2   3
                         *   4   5   6
                         *   7   8   9
                         */
                        for (int _AsideNo = 0; _AsideNo < array.ACount; _AsideNo++)
                        {
                            for (int _BsideNo = 0; _BsideNo < array.BCount; _BsideNo++)
                            {
                                SubExpandArray(_StepList, _PatternName, _RoomNo, _AsideNo, _BsideNo, _AlignList, _HeightList, array, baseOriginX, baseOriginY, arrayInfoList);
                            }
                        }
                        break;
                    case RouteMethod.ZTypeReverse:
                        /*
                         *  9   8   7
                         *  6   5   4
                         *  3   2   1
                         */
                        for (int _AsideNo = array.ACount - 1; _AsideNo >= 0; _AsideNo--)
                        {
                            for (int _BsideNo = array.BCount - 1; _BsideNo >= 0; _BsideNo--)
                            {
                                SubExpandArray(_StepList, _PatternName, _RoomNo, _AsideNo, _BsideNo, _AlignList, _HeightList, array, baseOriginX, baseOriginY, arrayInfoList);
                            }
                        }
                        break;
                    case RouteMethod.SType:
                        /*
                         *  1   2   3
                         *  6   5   4
                         *  7   8   9
                         */
                        for (int _AsideNo = 0; _AsideNo < array.ACount; _AsideNo++)
                        {
                            int remainder = _AsideNo % 2;//求餘數 等於Mod
                            if (remainder == 0)
                            {
                                for (int _BsideNo = 0; _BsideNo < array.BCount; _BsideNo++)
                                {
                                    SubExpandArray(_StepList, _PatternName, _RoomNo, _AsideNo, _BsideNo, _AlignList, _HeightList, array, baseOriginX, baseOriginY, arrayInfoList);
                                }
                            }
                            else
                            {
                                for (int _BsideNo = array.BCount - 1; _BsideNo >= 0; _BsideNo--)
                                {
                                    SubExpandArray(_StepList, _PatternName, _RoomNo, _AsideNo, _BsideNo, _AlignList, _HeightList, array, baseOriginX, baseOriginY, arrayInfoList);
                                }
                            }

                        }
                        break;
                    case RouteMethod.STypeReverse:
                        /*
                         *  9   8   7
                         *  4   5   6
                         *  3   2   1
                         */
                        for (int _AsideNo = array.ACount - 1; _AsideNo >= 0; _AsideNo--)
                        {
                            int remainder = _AsideNo % 2;//求餘數 等於Mod
                            if (remainder != 0)
                            {
                                for (int _BsideNo = 0; _BsideNo < array.BCount; _BsideNo++)
                                {
                                    SubExpandArray(_StepList, _PatternName, _RoomNo, _AsideNo, _BsideNo, _AlignList, _HeightList, array, baseOriginX, baseOriginY, arrayInfoList);
                                }
                            }
                            else
                            {
                                for (int _BsideNo = array.BCount - 1; _BsideNo >= 0; _BsideNo--)
                                {
                                    SubExpandArray(_StepList, _PatternName, _RoomNo, _AsideNo, _BsideNo, _AlignList, _HeightList, array, baseOriginX, baseOriginY, arrayInfoList);
                                }
                            }
                        }
                        break;
                }
            }
            stepList = _StepList;
            alignList = _AlignList;
            heightList = _HeightList;
            return;
        }

        /// <summary>陣列展開單格動作
        /// </summary>
        /// <param name="StepList">點膠步驟清單</param>
        /// <param name="patternName">陣列展開的Pattern</param>
        /// <param name="roomNo">該層第幾個陣列</param>
        /// <param name="aSideNo"></param>
        /// <param name="bSideNo"></param>
        /// <param name="alignList"></param>
        /// <param name="heightList"></param>
        /// <param name="array"></param>
        /// <param name="baseOriginX"></param>
        /// <param name="baseOriginY"></param>
        /// <param name="arrayInfoList"></param>
        /// <returns></returns>
        bool SubExpandArray(List<CRecipeStep> StepList, string patternName, int roomNo, int aSideNo, int bSideNo, List<CRecipeAlignGroup> alignList, List<CRecipeHeightGroup> heightList, CRecipeStepArray array, decimal baseOriginX, decimal baseOriginY, List<CArrayInfo> arrayInfoList)
        {
            List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

            _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
            if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
            {
                return false;
            }
            //=== 陣列資訊 ===
            CArrayInfo mArrayInfo = new CArrayInfo();
            mArrayInfo.LevelNo = _ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo + 1;
            mArrayInfo.RoomNo = roomNo;//TODO:待確認++時機
            mArrayInfo.ASideNo = aSideNo;
            mArrayInfo.BSideNo = bSideNo;
            mArrayInfo.ACount = array.ACount;
            mArrayInfo.BCount = array.BCount;
            _ArrayInfoList.Add(mArrayInfo);
            string _arrayInfo = GetArrayInfoString(_ArrayInfoList);
            int _AbsIdxA = 0;
            int _AbsIdxB = 0;
            GetArrayAbsIdx(_ArrayInfoList, out _AbsIdxA, out _AbsIdxB);//取得絕對索引 TODO:陣列切割時, 會動到AbsIdx, 待處理

            decimal _originX = aSideNo * (array.ASide.X - array.Origin.X) + bSideNo * (array.BSide.X - array.Origin.X) + baseOriginX;
            decimal _originY = aSideNo * (array.ASide.Y - array.Origin.Y) + bSideNo * (array.BSide.Y - array.Origin.Y) + baseOriginY;
            //=== 陣列資訊 ===

            //=== 陣列展開定位資訊 ===
            CRecipeAlignGroup _Align = this.Pattern[patternName].Align.Clone();
            for (int _ConveyorNo = 0; _ConveyorNo < _Align.Align.Count; _ConveyorNo++)
            {
                for (int _StageNo = 0; _StageNo < _Align.Align[_ConveyorNo].Count; _StageNo++)
                {
                    _Align.Align[_ConveyorNo][_StageNo].Align1.Pos.X += _originX;
                    _Align.Align[_ConveyorNo][_StageNo].Align1.Pos.Y += _originY;
                    _Align.Align[_ConveyorNo][_StageNo].Align2.Pos.X += _originX;
                    _Align.Align[_ConveyorNo][_StageNo].Align2.Pos.Y += _originY;
                }
            }

            _Align.ArrayInfo = _arrayInfo;
            _Align.AbsIdxA = _AbsIdxA;
            _Align.AbsIdxB = _AbsIdxB;
            alignList.Add(_Align);
            //=== 陣列展開定位資訊 ===

            //=== 陣列展開測高資訊 ===
            CRecipeHeightGroup _Height = this.Pattern[patternName].GetHeightStep();
            if (_Height != null)
            {
                for (int _ConveyorNo = 0; _ConveyorNo < _Height.Height.Count; _ConveyorNo++)
                {
                    for (int _MachineStageNo = 0; _MachineStageNo < _Height.Height[_ConveyorNo].Count; _MachineStageNo++)
                    {
                        _Height.Height[_ConveyorNo][_MachineStageNo].Pos.X += _originX;
                        _Height.Height[_ConveyorNo][_MachineStageNo].Pos.Y += _originY;
                    }
                }

                _Height.ArrayInfo = _arrayInfo;
                heightList.Add(_Height);
            }
            //=== 陣列展開測高資訊 ===

            for (int mArrayStepNo = 0; mArrayStepNo < this.Pattern[patternName].Step.Count; mArrayStepNo++)
            {
                CRecipeStep _Step = this.Pattern[patternName].Step[mArrayStepNo].Clone();

                _Step.ArrayInfo = _arrayInfo;
                _Step.AbsIdxA = _AbsIdxA;
                _Step.AbsIdxB = _AbsIdxB;

                switch (_Step.WorkType)
                {
                    case eStepWorkType.Pattern:
                        _Step.Pattern.Origin.X += _originX;
                        _Step.Pattern.Origin.Y += _originY;
                        StepList.Add(_Step);

                        break;
                    case eStepWorkType.Arc:
                    case eStepWorkType.Circle:
                        _Step.Arc.Start.X += _originX;
                        _Step.Arc.Start.Y += _originY;
                        _Step.Arc.Middle.X += _originX;
                        _Step.Arc.Middle.Y += _originY;
                        _Step.Arc.Center.X += _originX;
                        _Step.Arc.Center.Y += _originY;
                        _Step.Arc.End.X += _originX;
                        _Step.Arc.End.Y += _originY;
                        StepList.Add(_Step);
                        break;

                    case eStepWorkType.Delay:
                        StepList.Add(_Step);
                        break;
                    case eStepWorkType.Dot:
                        _Step.Dot.Pos.X += _originX;
                        _Step.Dot.Pos.Y += _originY;
                        StepList.Add(_Step);
                        break;
                    case eStepWorkType.FindHeight:
                        for (int _ConveyorNo = 0; _ConveyorNo < _Step.FindHeight.Height.Count; _ConveyorNo++)
                        {
                            for (int _StageNo = 0; _StageNo < _Step.FindHeight.Height[_ConveyorNo].Count; _StageNo++)
                            {
                                _Step.FindHeight.Height[_ConveyorNo][_StageNo].Pos.X += _originX;
                                _Step.FindHeight.Height[_ConveyorNo][_StageNo].Pos.Y += _originY;
                            }
                        }

                        StepList.Add(_Step);
                        break;
                    case eStepWorkType.Line:
                        _Step.Line.Start.X += _originX;
                        _Step.Line.Start.Y += _originY;
                        _Step.Line.End.X += _originX;
                        _Step.Line.End.Y += _originY;
                        StepList.Add(_Step);
                        break;
                    case eStepWorkType.Purge:
                        StepList.Add(_Step);
                        break;
                    case eStepWorkType.Rectangle:
                        StepList.AddRange(ExpandRectangle(this.Pattern[patternName].Step[mArrayStepNo], _originX, _originY));//矩形巨集展開
                        break;
                    case eStepWorkType.UFArray:
                        List<CRecipeStep> sUFList;
                        List<CRecipeAlignGroup> aUFList;
                        List<CRecipeHeightGroup> hUFList;
                        ExpandUFArray(this.Pattern[patternName].Step[mArrayStepNo].Array, _originX, _originY, _ArrayInfoList, out sUFList, out aUFList, out hUFList);
                        StepList.AddRange(sUFList);//陣列遞迴展開
                        alignList.AddRange(aUFList);//陣列遞迴展開
                        heightList.AddRange(hUFList);//陣列遞迴展開
                        break;
                    case eStepWorkType.Array:
                        List<CRecipeStep> sList;
                        List<CRecipeAlignGroup> aList;
                        List<CRecipeHeightGroup> hList;
                        ExpandArray(this.Pattern[patternName].Step[mArrayStepNo].Array, _originX, _originY, _ArrayInfoList, out sList, out aList, out hList);
                        StepList.AddRange(sList);//陣列遞迴展開
                        alignList.AddRange(aList);//陣列遞迴展開
                        heightList.AddRange(hList);//陣列遞迴展開
                        //roomNo++; //在這放??
                        break;
                    default:
                        Debug.Assert(false);//跳錯誤是為了提醒這邊沒改到.
                        break;
                }
            }
            return true;
        }

        /// <summary>UF陣列展開</summary>
        /// <param name="array">投入陣列資料</param>
        /// <param name="baseOriginX">展開的基準點X</param>
        /// <param name="baseOriginY">展開的基準點Y</param>
        /// <param name="arrayInfoList">叫用此陣列的資料</param>
        /// <param name="stepList">展開後的膠路陣列資料</param>
        /// <param name="alignList">展開後的定位陣列資料</param>
        /// <param name="heightList">展開後的測高陣列資料</param>
        /// <returns>展開後的陣列資料</returns>
        /// <remarks>展開方式為對陣列內每一步展開後, 再展開下一步.</remarks>
        void ExpandUFArray(CRecipeStepArray array, decimal baseOriginX, decimal baseOriginY, List<CArrayInfo> arrayInfoList, out List<CRecipeStep> stepList, out List<CRecipeAlignGroup> alignList, out List<CRecipeHeightGroup> heightList)
        {
            List<CRecipeStep> _StepList = new List<CRecipeStep>();//輸出的資料
            List<CRecipeAlignGroup> _AlignList = new List<CRecipeAlignGroup>();//輸出的定位資料
            List<CRecipeHeightGroup> _HeightList = new List<CRecipeHeightGroup>();//輸出的測高資料

            string _PatternName = array.Pattern;
            if (this.Pattern.ContainsKey(_PatternName))
            {

                int _RoomNo = 0;
                switch (array.Method) //如要加入搜尋方向 在這段改展開順序
                {
                    case RouteMethod.ZType:
                        /*
                         *   1   2   3
                         *   4   5   6
                         *   7   8   9
                         */
                        for (int mArrayStepNo = 0; mArrayStepNo < this.Pattern[_PatternName].Step.Count; mArrayStepNo++)
                        {
                            for (int _AsideNo = 0; _AsideNo < array.ACount; _AsideNo++)
                            {
                                for (int _BsideNo = 0; _BsideNo < array.BCount; _BsideNo++)
                                {

                                    List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

                                    _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
                                    if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
                                    {
                                        continue;
                                    }
                                    SubExpandUFArray(array, baseOriginX, baseOriginY, ref _StepList, ref _AlignList, ref _HeightList, _PatternName, _RoomNo, mArrayStepNo, _AsideNo, _BsideNo, _ArrayInfoList);
                                }
                            }
                        }
                        break;
                    case RouteMethod.ZTypeReverse:
                        /*
                         *  9   8   7
                         *  6   5   4
                         *  3   2   1
                         */
                        for (int mArrayStepNo = 0; mArrayStepNo < this.Pattern[_PatternName].Step.Count; mArrayStepNo++)
                        {
                            for (int _AsideNo = array.ACount - 1; _AsideNo >= 0; _AsideNo--)
                            {
                                for (int _BsideNo = array.BCount - 1; _BsideNo >= 0; _BsideNo--)
                                {
                                    List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

                                    _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
                                    if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
                                    {
                                        continue;
                                    }
                                    SubExpandUFArray(array, baseOriginX, baseOriginY, ref _StepList, ref _AlignList, ref _HeightList, _PatternName, _RoomNo, mArrayStepNo, _AsideNo, _BsideNo, _ArrayInfoList);

                                }
                            }
                        }
                        break;
                    case RouteMethod.SType:
                        /*
                         *  1   2   3
                         *  6   5   4
                         *  7   8   9
                         */
                        for (int mArrayStepNo = 0; mArrayStepNo < this.Pattern[_PatternName].Step.Count; mArrayStepNo++)
                        {
                            for (int _AsideNo = 0; _AsideNo < array.ACount; _AsideNo++)
                            {
                                int remainder = _AsideNo % 2;//求餘數 等於Mod
                                if (remainder == 0)
                                {
                                    for (int _BsideNo = 0; _BsideNo < array.BCount; _BsideNo++)
                                    {
                                        List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

                                        _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
                                        if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
                                        {
                                            continue;
                                        }
                                        SubExpandUFArray(array, baseOriginX, baseOriginY, ref _StepList, ref _AlignList, ref _HeightList, _PatternName, _RoomNo, mArrayStepNo, _AsideNo, _BsideNo, _ArrayInfoList);
                                    }
                                }
                                else
                                {
                                    for (int _BsideNo = array.BCount - 1; _BsideNo >= 0; _BsideNo--)
                                    {
                                        List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

                                        _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
                                        if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
                                        {
                                            continue;
                                        }
                                        SubExpandUFArray(array, baseOriginX, baseOriginY, ref _StepList, ref _AlignList, ref _HeightList, _PatternName, _RoomNo, mArrayStepNo, _AsideNo, _BsideNo, _ArrayInfoList);
                                    }
                                }

                            }
                        }
                       
                        break;
                    case RouteMethod.STypeReverse:
                        /*
                         *  9   8   7
                         *  4   5   6
                         *  3   2   1
                         */
                        for (int mArrayStepNo = 0; mArrayStepNo < this.Pattern[_PatternName].Step.Count; mArrayStepNo++)
                        {
                            for (int _AsideNo = array.ACount - 1; _AsideNo >= 0; _AsideNo--)
                            {
                                int remainder = _AsideNo % 2;//求餘數 等於Mod
                                if (remainder != 0)
                                {
                                    for (int _BsideNo = 0; _BsideNo < array.BCount; _BsideNo++)
                                    {
                                        List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

                                        _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
                                        if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
                                        {
                                            continue;
                                        }
                                        SubExpandUFArray(array, baseOriginX, baseOriginY, ref _StepList, ref _AlignList, ref _HeightList, _PatternName, _RoomNo, mArrayStepNo, _AsideNo, _BsideNo, _ArrayInfoList);
                                    }
                                }
                                else
                                {
                                    for (int _BsideNo = array.BCount - 1; _BsideNo >= 0; _BsideNo--)
                                    {
                                        List<CArrayInfo> _ArrayInfoList;//陣列展開資料備份

                                        _ArrayInfoList = arrayInfoList.GetRange(0, arrayInfoList.Count);//陣列展開資料備份
                                        if (_ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo >= 32) //最大層數限定
                                        {
                                            continue;
                                        }
                                        SubExpandUFArray(array, baseOriginX, baseOriginY, ref _StepList, ref _AlignList, ref _HeightList, _PatternName, _RoomNo, mArrayStepNo, _AsideNo, _BsideNo, _ArrayInfoList);
                                    }
                                }
                            }
                        }
                       
                        break;
                }
            }
            stepList = _StepList;
            alignList = _AlignList;
            heightList = _HeightList;
            return;
        }

        /// <summary>UF展開的子陣列
        /// </summary>
        /// <param name="array">投入陣列資料</param>
        /// <param name="baseOriginX">展開的基準點X</param>
        /// <param name="baseOriginY">展開的基準點Y</param>
        /// <param name="_StepList">展開後的膠路陣列資料</param>
        /// <param name="_AlignList">展開後的定位陣列資料</param>
        /// <param name="_HeightList">展開後的測高陣列資料</param>
        /// <param name="_PatternName">展開的Pattern</param>
        /// <param name="_RoomNo">該層第幾個陣列</param>
        /// <param name="mArrayStepNo">所在步驟</param>
        /// <param name="_AsideNo">X方向索引</param>
        /// <param name="_BsideNo">Y方向索引</param>
        /// <param name="_ArrayInfoList">叫用此陣列的資料</param>
        private void SubExpandUFArray(CRecipeStepArray array, decimal baseOriginX, decimal baseOriginY, ref List<CRecipeStep> _StepList, ref List<CRecipeAlignGroup> _AlignList, ref List<CRecipeHeightGroup> _HeightList, string _PatternName, int _RoomNo, int mArrayStepNo, int _AsideNo, int _BsideNo, List<CArrayInfo> _ArrayInfoList)
        {
            //=== 陣列資訊 ===
            CArrayInfo mArrayInfo = new CArrayInfo();
            mArrayInfo.LevelNo = _ArrayInfoList[_ArrayInfoList.Count - 1].LevelNo + 1;
            mArrayInfo.RoomNo = _RoomNo;//TODO:待確認++時機
            mArrayInfo.ASideNo = _AsideNo;
            mArrayInfo.BSideNo = _BsideNo;
            mArrayInfo.ACount = array.ACount;
            mArrayInfo.BCount = array.BCount;
            _ArrayInfoList.Add(mArrayInfo);
            string _arrayInfo = GetArrayInfoString(_ArrayInfoList);
            int _AbsIdxA = 0;
            int _AbsIdxB = 0;
            GetArrayAbsIdx(_ArrayInfoList, out _AbsIdxA, out _AbsIdxB);//取得絕對索引 TODO:陣列切割時, 會動到AbsIdx, 待處理

            decimal _originX = _AsideNo * (array.ASide.X - array.Origin.X) + _BsideNo * (array.BSide.X - array.Origin.X) + baseOriginX;
            decimal _originY = _AsideNo * (array.ASide.Y - array.Origin.Y) + _BsideNo * (array.BSide.Y - array.Origin.Y) + baseOriginY;
            //=== 陣列資訊 ===

            //=== 陣列展開定位資訊 ===
            CRecipeAlignGroup _Align = this.Pattern[_PatternName].Align.Clone();
            for (int _ConveyorNo = 0; _ConveyorNo < _Align.Align.Count; _ConveyorNo++)
            {
                for (int _StageNo = 0; _StageNo < _Align.Align[_ConveyorNo].Count; _StageNo++)
                {
                    _Align.Align[_ConveyorNo][_StageNo].Align1.Pos.X += _originX;
                    _Align.Align[_ConveyorNo][_StageNo].Align1.Pos.Y += _originY;
                    _Align.Align[_ConveyorNo][_StageNo].Align2.Pos.X += _originX;
                    _Align.Align[_ConveyorNo][_StageNo].Align2.Pos.Y += _originY;
                }
            }

            _Align.ArrayInfo = _arrayInfo;
            _Align.AbsIdxA = _AbsIdxA;
            _Align.AbsIdxB = _AbsIdxB;
            _AlignList.Add(_Align);
            //=== 陣列展開定位資訊 ===

            //=== 陣列展開測高資訊 ===
            CRecipeHeightGroup _Height = this.Pattern[_PatternName].GetHeightStep();
            if (_Height != null)
            {
                for (int _ConveyorNo = 0; _ConveyorNo < _Height.Height.Count; _ConveyorNo++)
                {
                    for (int _MachineStageNo = 0; _MachineStageNo < _Height.Height[_ConveyorNo].Count; _MachineStageNo++)
                    {
                        _Height.Height[_ConveyorNo][_MachineStageNo].Pos.X += _originX;
                        _Height.Height[_ConveyorNo][_MachineStageNo].Pos.Y += _originY;
                    }
                }

                _Height.ArrayInfo = _arrayInfo;
                _HeightList.Add(_Height);
            }
            //=== 陣列展開測高資訊 ===

            CRecipeStep _Step = this.Pattern[_PatternName].Step[mArrayStepNo].Clone();

            _Step.ArrayInfo = _arrayInfo;
            _Step.AbsIdxA = _AbsIdxA;
            _Step.AbsIdxB = _AbsIdxB;

            switch (_Step.WorkType)
            {
                case eStepWorkType.Pattern:
                    _Step.Pattern.Origin.X += _originX;
                    _Step.Pattern.Origin.Y += _originY;
                    _StepList.Add(_Step);

                    break;
                case eStepWorkType.Arc:
                case eStepWorkType.Circle:
                    _Step.Arc.Start.X += _originX;
                    _Step.Arc.Start.Y += _originY;
                    _Step.Arc.Middle.X += _originX;
                    _Step.Arc.Middle.Y += _originY;
                    _Step.Arc.Center.X += _originX;
                    _Step.Arc.Center.Y += _originY;
                    _Step.Arc.End.X += _originX;
                    _Step.Arc.End.Y += _originY;
                    _StepList.Add(_Step);
                    break;

                case eStepWorkType.Delay:
                    _StepList.Add(_Step);
                    break;
                case eStepWorkType.Dot:
                    _Step.Dot.Pos.X += _originX;
                    _Step.Dot.Pos.Y += _originY;
                    _StepList.Add(_Step);
                    break;
                case eStepWorkType.FindHeight:
                    for (int _ConveyorNo = 0; _ConveyorNo < _Step.FindHeight.Height.Count; _ConveyorNo++)
                    {
                        for (int _StageNo = 0; _StageNo < _Step.FindHeight.Height[_ConveyorNo].Count; _StageNo++)
                        {
                            _Step.FindHeight.Height[_ConveyorNo][_StageNo].Pos.X += _originX;
                            _Step.FindHeight.Height[_ConveyorNo][_StageNo].Pos.Y += _originY;
                        }
                    }

                    _StepList.Add(_Step);
                    break;
                case eStepWorkType.Line:
                    _Step.Line.Start.X += _originX;
                    _Step.Line.Start.Y += _originY;
                    _Step.Line.End.X += _originX;
                    _Step.Line.End.Y += _originY;
                    _StepList.Add(_Step);
                    break;
                case eStepWorkType.Purge:
                    _StepList.Add(_Step);
                    break;
                case eStepWorkType.Rectangle:
                    _StepList.AddRange(ExpandRectangle(this.Pattern[_PatternName].Step[mArrayStepNo], _originX, _originY));//矩形巨集展開
                    break;
                case eStepWorkType.UFArray:
                    List<CRecipeStep> sUFList;
                    List<CRecipeAlignGroup> aUFList;
                    List<CRecipeHeightGroup> hUFList;
                    ExpandUFArray(this.Pattern[_PatternName].Step[mArrayStepNo].Array, _originX, _originY, _ArrayInfoList, out sUFList, out aUFList, out hUFList);
                    _StepList.AddRange(sUFList);//陣列遞迴展開
                    _AlignList.AddRange(aUFList);//陣列遞迴展開
                    _HeightList.AddRange(hUFList);//陣列遞迴展開
                    break;
                case eStepWorkType.Array:
                    List<CRecipeStep> sList;
                    List<CRecipeAlignGroup> aList;
                    List<CRecipeHeightGroup> hList;
                    ExpandArray(this.Pattern[_PatternName].Step[mArrayStepNo].Array, _originX, _originY, _ArrayInfoList, out sList, out aList, out hList);
                    _StepList.AddRange(sList);//陣列遞迴展開
                    _AlignList.AddRange(aList);//陣列遞迴展開
                    _HeightList.AddRange(hList);//陣列遞迴展開
                    //roomNo++; //在這放??
                    break;
                default:
                    Debug.Assert(false);//跳錯誤是為了提醒這邊沒改到.
                    break;
            }
        }

    }
}

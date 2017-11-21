using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;
using MapData;

namespace Premtek
{
    /// <summary>MAP工具
    /// </summary>
    public class CToolKitMap
    {

        /// <summary>[A機B機MapData]
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public clsMapData[] gMapData = new clsMapData[(int)MCommonDefine.enmMachineStation.MaxMachine];

        /// <summary>將BinMap資料匯入Dictionary
        /// </summary>
        /// <param name="stage">平台Pattern</param>
        /// <returns></returns>
        /// <remarks>各別Stage各呼叫此函式一次, 即可將Map資料轉入Stage的步驟中.</remarks>
        public ErrorCode GetBinMap(List<bool[,]> Map, ref CRecipePattern stage)
        {
            //TODO: Parallel.For改善
            for (int _stepNo = 0; _stepNo < stage.Step.Count; _stepNo++)//對於每一步, 由MAP撈資料決定該步是否執行.
            {
                int _idxA = stage.Step[_stepNo].AbsIdxA;//步驟紀錄的Map絕對索引
                int _idxB = stage.Step[_stepNo].AbsIdxB;//步驟紀錄的Map絕對索引
                bool _MapEnabled = true;//預設啟用, 撈不到資料的也是要點.
                if (Map != null)//MAP存在
                {
                    if ((_idxA >= Map[stage.Step[_stepNo].MapNo].GetLowerBound(0)) && (_idxA <= Map[stage.Step[_stepNo].MapNo].GetUpperBound(0)))//嘗試從Map讀取資料
                    {
                        if ((_idxB >= Map[stage.Step[_stepNo].MapNo].GetLowerBound(1)) && (_idxB <= Map[stage.Step[_stepNo].MapNo].GetUpperBound(1)))
                        {
                            _MapEnabled = Map[stage.Step[_stepNo].MapNo][_idxA, _idxB];
                        }
                    }
                }

                if (_MapEnabled == false)
                {
                    stage.Step[_stepNo].Enabled = _MapEnabled;
                }
                //預設Enabled開啟, 如果Map要點(True), 但是步驟在Recipe內封印(False), 則不點. (不被Map覆蓋)

            }
            return ErrorCode.Success;
        }


        /// <summary>[Mapping Data轉換處理]
        /// </summary>
        /// <param name="machineNo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool CoverMapData(MCommonDefine.enmMachineStation machineNo, string path, clsMapData.enmDirection NotchDirection)
        {
            MCommonDefine.enmStage mStageNo1 = default(MCommonDefine.enmStage);
            MCommonDefine.enmStage mStageNo2 = default(MCommonDefine.enmStage);

            try
            {
                switch (machineNo)
                {
                    case MCommonDefine.enmMachineStation.MachineA:
                        mStageNo1 = MCommonDefine.enmStage.No1;
                        mStageNo2 = MCommonDefine.enmStage.No2;

                        break;
                    case MCommonDefine.enmMachineStation.MachineB:
                        mStageNo1 = MCommonDefine.enmStage.No3;
                        mStageNo2 = MCommonDefine.enmStage.No4;
                        break;
                }

                //[Note]:根據進料的資訊、取出Wafer Map的檔案路徑、名稱
                if ((MapToData(machineNo, path) == false))
                {
                    return false;
                }

                //'[Note]:取得Notch方向
                gMapData[(int)machineNo].ResetMapNotch(NotchDirection);
                
                //[Note]:比對Mapping Data是否相符, 無此資訊則不比對
                //if (gMapData[(int)machineNo].Information.Type != "N/A" & !string.IsNullOrEmpty(gMapData[(int)machineNo].Information.Type)) {
                //    if (gMapData[(int)machineNo].Information.Type != gCRecipe.ProductType) {
                //        return false;
                //    }
                //}

                ///[Note]:合併NodeToMap裡的Node, 取得總陣列大小
                //int maxColumn = 1000;
                //int maxRow = 1000;
                //int[,] trayArray = new int[maxColumn + 1, maxRow + 1];
                //if ((gCRecipe.NodeToMap(mStageNo1) != null)) {
                //    foreach (string node2Map_loopVariable in gCRecipe.NodeToMap(mStageNo1)) {
                //        node2Map = node2Map_loopVariable;
                //        if (gCRecipe.Node(mStageNo1).ContainsKey(node2Map)) {
                //            int x = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingX - 1;
                //            int y = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingY - 1;
                //            CMultiArrayAdapter nodeArray = new CMultiArrayAdapter(gCRecipe.Node(mStageNo1)(node2Map).Array);
                //            int column = nodeArray.GetMemoryCountX;
                //            int row = nodeArray.GetMemoryCountY;
                //            for (c = 0; c <= column - 1; c++) {
                //                for (r = 0; r <= row - 1; r++) {
                //                    trayArray(x + c, y + r) += 1;
                //                }
                //            }
                //        }
                //    }
                //}

                //if ((gCRecipe.NodeToMap(mStageNo2) != null)) {
                //    foreach (string node2Map_loopVariable in gCRecipe.NodeToMap(mStageNo2)) {
                //        node2Map = node2Map_loopVariable;
                //        if (gCRecipe.Node(mStageNo2).ContainsKey(node2Map)) {
                //            int x = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingX - 1;
                //            int y = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingY - 1;
                //            CMultiArrayAdapter nodeArray = new CMultiArrayAdapter(gCRecipe.Node(mStageNo2)(node2Map).Array);
                //            int column = nodeArray.GetMemoryCountX;
                //            int row = nodeArray.GetMemoryCountY;
                //            for (c = 0; c <= column - 1; c++) {
                //                for (r = 0; r <= row - 1; r++) {
                //                    trayArray(x + c, y + r) += 1;
                //                }
                //            }
                //        }
                //    }
                //}


                //int nodeColumn = 0;
                //for (int i = 0; i <= maxColumn; i++) {
                //    if ((trayArray[i, 0] != 1)) {
                //        nodeColumn = i;
                //        break; // TODO: might not be correct. Was : Exit For
                //    }
                //}

                //int nodeRow = 0;
                //for (int i = 0; i <= maxRow; i++) {
                //    if ((trayArray[0, i] != 1)) {
                //        nodeRow = i;
                //        break; // TODO: might not be correct. Was : Exit For
                //    }
                //}

                ////[Note]:比對Map與Node List陣列大小
                //if (((gMapData[(int)machineNo].Substrates[0].Columns != nodeColumn) | (gMapData[(int)machineNo].Substrates[0].Rows != nodeRow))) {
                //    return false;
                //}

                ////[Note]:檢查Node陣列是否為一個完整的矩形陣列,並排除重複(value > 1)或遺漏(value = 0)的地方
                //for (int c = 0; c <= nodeColumn - 1; c++) {
                //    for (int r = 0; r <= nodeRow - 1; r++) {
                //        if ((trayArray[c, r] != 1)) {
                //            return false;
                //        }
                //    }
                //}

                ////[Note]:將MappingData丟入StageMap
                //if ((gCRecipe.NodeToMap(mStageNo1) != null)) {
                //    foreach (void node_loopVariable in gCRecipe.NodeToMap(mStageNo1)) {
                //        node = node_loopVariable;
                //        if (MapDataCoverToStageMap(gMapData(machineNo), gStageMap(mStageNo1).Node(node), gCRecipe.Node(mStageNo1)(node)) == false) {
                //            return false;
                //        }
                //    }
                //}

                ////[Note]:將MappingData丟入StageMap
                //if ((gCRecipe.NodeToMap(mStageNo2) != null)) {
                //    foreach (void node_loopVariable in gCRecipe.NodeToMap(mStageNo2)) {
                //        node = node_loopVariable;
                //        if (MapDataCoverToStageMap(gMapData(machineNo), gStageMap(mStageNo2).Node(node), gCRecipe.Node(mStageNo2)(node)) == false) {
                //            return false;
                //        }
                //    }
                //}

                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ASE/PII Map 讀檔
        /// </summary>
        /// <param name="machineNo">A/B機</param>
        /// <param name="path">ASE/PII Map 檔案路徑</param>
        /// <returns>成功/失敗</returns>
        /// <remarks></remarks>
        public bool MapToData(MCommonDefine.enmMachineStation machineNo, string path)
        {
            try
            {
                string[] arrStr = path.Split(new string[] { @"\" }, StringSplitOptions.None);
                string[] fileName = arrStr[arrStr.Length - 1].Split(new string[] { "." }, StringSplitOptions.None);
                string savePath = @"D:\PIIData\MappingData\Source\";
                string filePath = @"D:\PIIData\MappingData\Source\" + fileName[0] + ".csv";

                if ((gMapData[(int)machineNo].OpenFile(path)))
                {
                    return true;
                }
                else if ((WaferMapConvertToPIIMap(path, savePath)))
                {
                    if (gMapData[(int)machineNo].OpenFile(filePath))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>[將客戶端Wafer Map轉成PII Map(Cary)]</summary>
        /// <param name="ReadFileName"></param>
        /// <param name="SavePath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool WaferMapConvertToPIIMap(string ReadFileName, string SavePath)
        {
            try
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(ReadFileName);
                char[] cLine = null;
                int iRowCount = 0;
                int iColumnCount = 0;
                int i = 0;
                int j = 0;

                using (System.IO.StreamReader sr = new System.IO.StreamReader(ReadFileName))
                {
                    string[] arrStr = sr.ReadLine().Split(new string[] {" "}, StringSplitOptions.None);
                    iColumnCount = arrStr[0].Length;
                }

                cLine = objReader.ReadLine().ToCharArray();
                char[,] Die = new char[65537, cLine.Length + 1];

                //' calculate Row number & column number
                do
                {
                    for (j = 0; j <= cLine.Length - 1; j++)
                    {
                        Die[iRowCount, j] = cLine[j];
                    }
                    iRowCount += 1;
                    cLine = objReader.ReadLine().ToCharArray();
                } while (!(cLine.Length == 0));
                objReader.Close();

                System.IO.StreamWriter file;
                string[] split1 = null;
                string SaveFilenNme = null;

                if (!System.IO.Directory.Exists(SavePath))
                {
                    System.IO.Directory.CreateDirectory(SavePath);
                }

                split1 = ReadFileName.Split(new string[] { "."}, StringSplitOptions.None);
                split1 = split1[0].Split(new string[] {"\\"}, StringSplitOptions.None);
                SaveFilenNme = split1[split1.Length - 1];
                SaveFilenNme = SavePath + SaveFilenNme + ".csv";
                file =new System.IO.StreamWriter(SaveFilenNme);

                //' build "WAFER INFORMATION" 
                file.WriteLine("[WAFER INFORMATION]");
                file.WriteLine("VERSION,Ver9.3");
                file.WriteLine("LOT ID,N/A");
                file.WriteLine("PRODUCT ID,N/A");
                file.WriteLine("DIE SIZE X,N/A");
                file.WriteLine("DIE SIZE Y,N/A");
                file.WriteLine("COLUMN," + iColumnCount);
                file.WriteLine("ROW," + iRowCount);
                file.WriteLine("CHECK IN TIME,N/A");
                file.WriteLine("CHECK OUT TIME,N/A");
                file.WriteLine("TOTAL TIME,N/A");
                file.WriteLine("RECIPE NAME,N/A");
                file.WriteLine("TYPE,N/A");
                file.WriteLine("PITCH,N/A");
                file.WriteLine("USER,N/A");
                file.WriteLine("MACHINE TYPE,N/A");
                file.WriteLine("NOTCH,180");

                //' build "WAFER MAP" 
                file.WriteLine("[WAFER MAP]");
                file.Write("COLUMN");
                for (j = 0; j <= iColumnCount - 1; j++)
                {
                    file.Write("," + j + 1);
                }
                file.WriteLine();
                for (i = 0; i <= iRowCount - 1; i++)
                {
                    file.Write("ROW" + i + 1);
                    for (j = 0; j <= iColumnCount - 1; j++)
                    {
                        file.Write("," + Die[i, j]);
                    }
                    file.WriteLine();
                }

                //' build "CYCLE MAP" 
                file.WriteLine("[CYCLE MAP]");
                file.Write("COLUMN");
                for (j = 0; j <= iColumnCount - 1; j++)
                {
                    file.Write("," + j + 1);
                }
                file.WriteLine();
                for (i = 0; i <= iRowCount - 1; i++)
                {
                    file.Write("ROW" + i + 1);

                    for (j = 0; j <= iColumnCount - 1; j++)
                    {
                        if (char.IsLetterOrDigit(Die[i, j]))
                        {
                            //If ((Die(i, j) = "X") Or (Die(i, j) = ".") Or (Die(i, j) = "@") Or (Die(i, j) = "=")) Then '' change the value to '0' except 'X' '.' '@'  '='
                            //' change the value to '0' except '.'
                            if ((Die[i, j] == '.'))
                            {
                                file.Write("," + Die[i, j]);
                            }
                            else
                            {
                                file.Write(",0");
                            }
                        }
                        else
                        {
                            file.Write("," + Die[i, j]);
                        }
                    }
                    file.WriteLine();
                }

                //' build "EXPLAIN" 
                file.WriteLine("[EXPLAIN]");
                file.WriteLine(".,EMPTY");
                file.WriteLine("X,BAD DIE");
                file.WriteLine("0~FF,[WAFER MAP] BIN NUMBER");
                file.WriteLine("0~FF,[CYCLE MAP] CYCLE TIME");
                file.WriteLine("N,NOTCH");
                file.Close();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

    }

}

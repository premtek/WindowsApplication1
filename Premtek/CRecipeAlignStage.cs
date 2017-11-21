using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>定位資料-平台
    /// </summary>
   public class CRecipeAlignStage
    {
        /// <summary>單/兩點定位, 第一定位點資料
        /// </summary>
        public CRecipeAlignPos Align1;
        /// <summary>兩點定位, 第二定位點資料
        /// </summary>
        public CRecipeAlignPos Align2;
        /// <summary>有無定位, 第三定位點資料
        /// </summary>
        public CRecipeAlignPos Align3;
        /// <summary>群組基準點, 相當於點膠基準點
        /// </summary>
        public CPosPoint Origin;
        /// <summary>定位結果
        /// </summary>
        public List<CAlignResult> Result;

        public CRecipeAlignStage()
        {
          
            Align1 = new CRecipeAlignPos();
            Align2 = new CRecipeAlignPos();
            Align3 = new CRecipeAlignPos();
            Origin = new CPosPoint();
            Result = new List<CAlignResult>();
        }

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeAlignStage Clone()
        {
            CRecipeAlignStage _Temp = new CRecipeAlignStage();
            _Temp.Align1 = this.Align1.Clone();
            _Temp.Align2 = this.Align2.Clone();
            _Temp.Origin = this.Origin.Clone();
            //_Temp.Result 還沒寫Clone...
            _Temp.Align3 = this.Align3.Clone();
           
            return _Temp;
        }

        /// <summary>儲存參數
        /// </summary>
        /// <param name="patternName">所屬Pattern</param>
        /// <param name="conveyorNo">定位資料所在Conveyor</param>
        /// <param name="stageNo">定位資料所在平台</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string patternName,int conveyorNo, int stageNo, string fileName)
        {
            string _SectionName = patternName + "_AlignC" + (conveyorNo+1).ToString() + "S" + (stageNo+1).ToString();
            string _KeyNameStart = patternName + "_Align_";
     
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1X", this.Align1.Pos.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1Y", this.Align1.Pos.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1Z", this.Align1.Pos.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1A", this.Align1.Pos.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1B", this.Align1.Pos.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1C", this.Align1.Pos.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos1Secne", this.Align1.Secne, fileName);

            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2X", this.Align2.Pos.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2Y", this.Align2.Pos.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2Z", this.Align2.Pos.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2A", this.Align2.Pos.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2B", this.Align2.Pos.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2C", this.Align2.Pos.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos2Secne", this.Align2.Secne, fileName);

            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3X", this.Align3.Pos.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3Y", this.Align3.Pos.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3Z", this.Align3.Pos.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3A", this.Align3.Pos.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3B", this.Align3.Pos.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3C", this.Align3.Pos.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Pos3Secne", this.Align3.Secne, fileName);

            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginX", this.Origin.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginY", this.Origin.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginZ", this.Origin.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginA", this.Origin.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginB", this.Origin.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "OriginC", this.Origin.C.ToString(), fileName);

           
            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="stepNo">所屬Pattern</param>
        /// <param name="conveyorNo">定位資料所在Conveyor</param>
        /// <param name="stageNo">定位資料所在平台</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string patternName, int conveyorNo, int stageNo, string fileName)
        {
            string _SectionName = patternName + "_Align" + (conveyorNo + 1).ToString() + "S" + (stageNo + 1).ToString();
            string _KeyNameStart = patternName + "_Align_";
           
            if (this.Align1 == null)
            {
                this.Align1 = new CRecipeAlignPos();
            }
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1X", fileName, 0), out this.Align1.Pos.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1Y", fileName, 0), out this.Align1.Pos.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1Z", fileName, 0), out this.Align1.Pos.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1A", fileName, 0), out this.Align1.Pos.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1B", fileName, 0), out this.Align1.Pos.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1C", fileName, 0), out this.Align1.Pos.C);
            this.Align1.Secne = CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1Secne", fileName, "Default");

            if (this.Align2 == null)
            {
                this.Align2 = new CRecipeAlignPos();
            }
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2X", fileName, 0), out this.Align2.Pos.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2Y", fileName, 0), out this.Align2.Pos.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2Z", fileName, 0), out this.Align2.Pos.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2A", fileName, 0), out this.Align2.Pos.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2B", fileName, 0), out this.Align2.Pos.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2C", fileName, 0), out this.Align2.Pos.C);
            this.Align2.Secne = CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos2Secne", fileName, "Default");

            if (this.Align3 == null)
            {
                this.Align3 = new CRecipeAlignPos();
            }
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos3X", fileName, 0), out this.Align3.Pos.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos3Y", fileName, 0), out this.Align3.Pos.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos3Z", fileName, 0), out this.Align3.Pos.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos3A", fileName, 0), out this.Align3.Pos.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos3B", fileName, 0), out this.Align3.Pos.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos3C", fileName, 0), out this.Align3.Pos.C);
            this.Align3.Secne = CIni.ReadIniString(_SectionName, _KeyNameStart + "Pos1Secne", fileName, "Default");

            if (this.Origin == null)
            {
                this.Origin = new CPosPoint();
            }
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginX", fileName, 0), out this.Origin.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginY", fileName, 0), out this.Origin.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginZ", fileName, 0), out this.Origin.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginA", fileName, 0), out this.Origin.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginB", fileName, 0), out this.Origin.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "OriginC", fileName, 0), out this.Origin.C);

            return ErrorCode.Success;
        }
    }
}

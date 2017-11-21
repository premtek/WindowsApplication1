using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>定位
    /// </summary>
    public class CRecipeStepAlign
    {
        /// <summary>原點(旋轉中心/點膠基準點)</summary>
        public CPosPoint Origin = new CPosPoint();
        /// <summary>群組(場景)</summary>
        public string Group = "Default";

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeStepAlign Clone()
        {
            CRecipeStepAlign _Temp = new CRecipeStepAlign();
            _Temp.Group = this.Group;
            _Temp.Origin = this.Origin.Clone();
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Align_";
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosX", this.Origin.X.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosY", this.Origin.Y.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosZ", this.Origin.Z.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosA", this.Origin.A.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosB", this.Origin.B.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "PosC", this.Origin.C.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "Group", this.Group, fileName);
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
            string _KeyNameStart = "Step" + (stepNo + 1).ToString() + "_Align_";
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosX", fileName), out this.Origin.X);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosY", fileName), out this.Origin.Y);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosZ", fileName), out this.Origin.Z);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosA", fileName), out this.Origin.A);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosB", fileName), out this.Origin.B);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "PosC", fileName), out this.Origin.C);
            this.Group = CIni.ReadIniString(_SectionName, _KeyNameStart + "Group", fileName);
            return ErrorCode.Success;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    public enum BacktrackDirection
    {
        /// <summary>不斷膠
        /// </summary>
        None,
        /// <summary>順向抹平
        /// </summary>
        Forward,
        /// <summary>反向抹平
        /// </summary>
        Backward,
        /// <summary>上抬後順向(向前)
        /// </summary>
        UpFoward,
        /// <summary>上抬後反向(向後)
        /// </summary>
        UpBackward,
        /// <summary>向斜前方拉
        /// </summary>
        AboveForard,
        /// <summary>向斜後方拉
        /// </summary>
        AboveBackward,
    }
    /// <summary>畫線參數
    /// </summary>
    public class CRecipeParameterLine
    {
        /// <summary>延伸路徑
        /// </summary>
        public bool JetMode = false;
        /// <summary>路徑銜接(先用Int, 避免後面要擴充.)
        /// </summary>
        public int NonJetFly = 0;
        /// <summary>XY到位穩定時間(Sec)
        /// </summary>
        public decimal XYStableTime;
        /// <summary>Z到位穩定時間(Sec)
        /// </summary>
        public decimal ZStableTime;
        /// <summary>Z軸下降速度(mm/s)
        /// </summary>
        public decimal ZDownVelocity;
        /// <summary>Z軸下降加速度(mm/s^2)
        /// </summary>
        public decimal ZDownAcc;
        
        /// <summary>加速段延伸路徑距離(mm)
        /// </summary>
        public decimal AccDistance=0;//延伸路徑
        
        /// <summary>開閥後等待時間(Sec)
        /// </summary>
        public decimal ValveOnDelayTime;


        /// <summary>減速段延伸路徑距離(mm)
        /// </summary>
        public decimal DecDistance = 0;//延伸路徑

        /// <summary>提前關膠距離(mm)
        /// </summary>
        public decimal EndAheadDistance;
        /// <summary>移動結束後等待時間(Sec)
        /// </summary>
        public decimal MoveEndDelayTime;
        /// <summary>真空回吸時間(Sec)
        /// </summary>
        public decimal SuckBackTime;
        /// <summary>斷膠等待時間(Sec)
        /// </summary>
        public decimal BacktrackDelayTime;
        /// <summary>斷膠高度(mm)
        /// </summary>
        public decimal BacktrackHeight;
        /// <summary>斷膠平移距離(mm)
        /// </summary>
        public decimal BacktrackLength;
        /// <summary>斷膠移動速度
        /// </summary>
        public decimal BacktrackVelocity;
        /// <summary>斷膠平移方向
        /// </summary>
        public BacktrackDirection BacktrackDirection;
        /// <summary>上抬高度(閃元件高度)(mm_
        /// </summary>
        public decimal RetractHeight;
        /// <summary>上抬速度(mm/s)
        /// </summary>
        public decimal RetractVelocity;
        /// <summary>上抬加速度(mm/s)
        /// </summary>
        public decimal RetractAcc;

        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeParameterLine Clone()
        {
            CRecipeParameterLine _Temp = new CRecipeParameterLine();
            _Temp.JetMode = this.JetMode;
            _Temp.NonJetFly = this.NonJetFly;
            _Temp.XYStableTime = this.XYStableTime;
            _Temp.ZStableTime = this.ZStableTime;
            _Temp.ZDownVelocity = this.ZDownVelocity;
            _Temp.ZDownAcc = this.ZDownAcc;
            _Temp.AccDistance = this.AccDistance;
            _Temp.ValveOnDelayTime = this.ValveOnDelayTime;
            _Temp.DecDistance = this.DecDistance;
            _Temp.MoveEndDelayTime = this.MoveEndDelayTime;
            _Temp.SuckBackTime = this.SuckBackTime;
            _Temp.BacktrackDelayTime = this.BacktrackDelayTime;
            _Temp.BacktrackHeight = this.BacktrackHeight;
            _Temp.BacktrackLength = this.BacktrackLength;
            _Temp.BacktrackVelocity = this.BacktrackVelocity;
            _Temp.RetractHeight = this.RetractHeight;
            _Temp.RetractVelocity = this.RetractVelocity;
            _Temp.RetractAcc = this.RetractAcc;
            _Temp.EndAheadDistance = this.EndAheadDistance;
            _Temp.BacktrackDirection = this.BacktrackDirection;
            return _Temp;
        }

        /// <summary>儲存參數
        /// </summary>
        /// <param name="typeNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string key, string fileName)
        {
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Line_";

            CIni.SaveIniString(_SectionName, _KeyNameStart + "JetMode", this.JetMode.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "NonJetFly", this.NonJetFly.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "XYStableTime", this.XYStableTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ZStableTime", this.ZStableTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ZDownVelocity", this.ZDownVelocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ZDownAcc", this.ZDownAcc.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "AccDistance", this.AccDistance.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ValveOnDelayTime", this.ValveOnDelayTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "DecDistance", this.DecDistance.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "MoveEndDelayTime", this.MoveEndDelayTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "SuckBackTime", this.SuckBackTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackDelayTime", this.BacktrackDelayTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackHeight", this.BacktrackHeight.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackLength", this.BacktrackLength.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackVelocity", this.BacktrackVelocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "RetractHeight", this.RetractHeight.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "RetractVelocity", this.RetractVelocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "RetractAcc", this.RetractAcc.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "EndAheadDistance", this.EndAheadDistance.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackDirection", Convert.ToInt32(this.BacktrackDirection).ToString(), fileName);
            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="stepNo">步驟編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string key, string fileName)
        {
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Line_";

            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "JetMode", fileName, 0M), out this.JetMode);
            int.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "NonJetFly", fileName, 0M), out this.NonJetFly);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "XYStableTime", fileName, 0M), out this.XYStableTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ZStableTime", fileName, 0M), out this.ZStableTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ZDownVelocity", fileName, 30M), out this.ZDownVelocity);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ZDownAcc", fileName, 1960), out this.ZDownAcc);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "AccDistance", fileName, 2), out this.AccDistance);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ValveOnDelayTime", fileName, 0), out this.ValveOnDelayTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "DecDistance", fileName, 2), out this.DecDistance);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "MoveEndDelayTime", fileName, 0), out this.MoveEndDelayTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "SuckBackTime", fileName, 0), out this.SuckBackTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackDelayTime", fileName, 0), out this.BacktrackDelayTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackHeight", fileName, 0), out this.BacktrackHeight);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackLength", fileName, 0), out this.BacktrackLength);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackVelocity", fileName, 0), out this.BacktrackVelocity);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "RetractHeight", fileName, 0), out this.RetractHeight);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "RetractVelocity", fileName, 100), out this.RetractVelocity);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "RetractAcc", fileName, 4900), out this.RetractAcc);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "EndAheadDistance", fileName, 0), out this.EndAheadDistance);
            Enum.TryParse<BacktrackDirection>(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackDirection", fileName, 0), out this.BacktrackDirection);
            return ErrorCode.Success;
        }

    }
}
